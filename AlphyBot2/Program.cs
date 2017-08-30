using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Reflection;
using System.IO;
using System.Xml;

namespace AlphyBot2
{
    class Program
    {

        struct  Message
        {
            public string Text;
            public string Channel;
            public string UserName;
            public string TimeStamp;
            public Message(string text, string channel, string userName, string timeStamp)
            {
                Text = text;
                Channel = channel;
                UserName = userName;
                TimeStamp = timeStamp;
            }
        }

        class Settings
        {
            public static string userName;
            public static string password;
            public static string channel;

            public static void GetSettings()
            {
                XmlDocument settingsXml = new XmlDocument();
                try {
                    // TODO:
                    // - Validate that the elements are in the file (c# verify xml with xsd)
                    settingsXml.Load(AppDomain.CurrentDomain.BaseDirectory + "settings.xml");
                    XmlNodeList foundUserName = settingsXml.GetElementsByTagName("username");
                    XmlNodeList foundPassword = settingsXml.GetElementsByTagName("password");
                    XmlNodeList foundChannel = settingsXml.GetElementsByTagName("channel");

                    userName = foundUserName[0].InnerText;
                    password = foundPassword[0].InnerText;
                    channel = foundChannel[0].InnerText;

                    if(userName.Equals("") || password.Equals("") || channel.Equals(""))
                    {
                        Log.ErrorMessage("Check your settings file for missing variables!");
                        System.Environment.Exit(0);
                    } else
                    {
                        Log.InfoMessage("Loaded settings.ini !");
                    }

                }

                catch(Exception e)
                {
                    Log.ErrorMessage("Error with the Settings file accured");
                    Log.ErrorMessage(e.Data.ToString());
                    System.Environment.Exit(0);
                }
            }
        }

        static void Main(string[] args)
        {
            Settings.GetSettings();

            // Join a Twitch Channel
            IrcClient irc = new IrcClient("irc.twitch.tv", 6667, Settings.userName, Settings.password);
            
	        irc.JoinRoom(Settings.channel);
            // Main Loop
            while(true)
            {
                // Read last IRC message
                string message = irc.ReadIrcMessage();
                Message msg;

                // Parse the IRC message for twitch message
                Regex twitchIrcMessageRegex = new Regex(@":(\w)!\w+@\w+\.tmi\.twitch\.tv PRIVMSG #(\w+) :(.+)");
                Match twitchIrcMessageMatch = twitchIrcMessageRegex.Match(message);

                if (twitchIrcMessageMatch.Success)
                {
                    msg.UserName = twitchIrcMessageMatch.Groups[1].Value;
                    msg.Channel = twitchIrcMessageMatch.Groups[2].Value;
                    msg.Text = twitchIrcMessageMatch.Groups[3].Value;
                    msg.TimeStamp = Log.GetTimestamp(DateTime.Now);
                    // Write Message
                    Log.ChatMessage(msg.TimeStamp, msg.UserName, msg.Channel, msg.Text);

                    // Test Chat-Command
                    if (msg.Text.StartsWith("!test"))
                    {
                        irc.SendChatMessage("This is a test command!");
                    }

                }
                else
                {
                    // Write all IRC messages that aren't parsed as Twitch chat messages.
                    Log.SystemMessage(message);
                }

                // Reply with a Pong to the server, in case of Ping
                if (message.Equals("PING :tmi.twitch.tv"))
                {
                    irc.SendIrcMessage("PONG :tmi.twitch.tv");
                    Log.SystemMessage("PONG :tmi.twitch.tv"); // Just to make sure
                }

            }
        }
    }
}
