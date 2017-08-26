using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

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

        static void Main(string[] args)
        {

            // Join a Twitch Channel
            IrcClient irc = new IrcClient("irc.twitch.tv", 6667, "alphybot", "oauth:supersecretpasswordxd");
            
	    irc.JoinRoom("alphuite");
            // Main Loop
            while(true)
            {
                // Read last IRC message
                string message = irc.ReadIrcMessage();
                Message message;

                // Parse the IRC message for twitch message
                Regex regex = new Regex(@"\:\w+!\w+@\w+.tmi.twitch.tv PRIVMSG #\w+ :.+");
                Match match = regex.Match(message);
                if (match.Success)
                {
                    // Create Message
                    message.UserName = match.Value.Split('!')[0].Replace(':', ' ').Trim();
                    message.Text = Regex.Replace(match.Value, "\\:\\w+!\\w+@\\w+.tmi.twitch.tv PRIVMSG #\\w+ :", " ").Trim();
                    message.Channel = Regex.Replace(match.Value, "\\:\\w+!\\w+@\\w+.tmi.twitch.tv PRIVMSG ", " ").Split(':')[0].Trim();
                    message.TimeStamp = Log.GetTimestamp(DateTime.Now);

                    // Write Message
                    Log.ChatMessage(message.TimeStamp, mesaage.UserName, message.Channel, message.Text);

                    // Test Chat-Command
                    if (message.Text.StartsWith("!test"))
                    {
                        irc.SendChatMessage("This is a test command!");
                    }

                } else
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
