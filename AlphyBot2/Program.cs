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
        public class Message
        {
            public string Text { get; set; }
            public string Channel { get; set; }
            public string UserName { get; set; }
            public string TimeStamp { get; set; }
            public Message(){}
            public Message(string text, string channel, string userName, string timeStamp)
            {
                Text = text;
                Channel = channel;
                UserName = userName;
                TimeStamp = timeStamp;
            }
        }

        public static String GetTimestamp(DateTime value)
        {
            return value.ToString("yyyy-MM-dd HH:mm:ss");
        }

        static void Main(string[] args)
        {

            // Join a Twitch Channel
            IrcClient irc = new IrcClient("irc.twitch.tv", 6667, "alphybot", "oauth:supersecretpasswordxd");
            irc.joinRoom("alphuite");
            irc.sendChatMessage("Hi! AlphyBot online since " + GetTimestamp(DateTime.Now));

            // Main Loop
            while(true)
            {
                // Read last IRC message
                string message = irc.readIrcMessage();
                Message m = new Message();

                // Parse the IRC message for twitch message
                Regex regex = new Regex(@"\:\w+!\w+@\w+.tmi.twitch.tv PRIVMSG #\w+ :.+");
                Match match = regex.Match(message);
                if (match.Success)
                {
                    // Create Message
                    m.UserName = match.Value.Split('!')[0].Replace(':', ' ').Trim();
                    m.Text = Regex.Replace(match.Value, "\\:\\w+!\\w+@\\w+.tmi.twitch.tv PRIVMSG #\\w+ :", " ").Trim();
                    m.Channel = Regex.Replace(match.Value, "\\:\\w+!\\w+@\\w+.tmi.twitch.tv PRIVMSG ", " ").Split(':')[0].Trim();
                    m.TimeStamp = GetTimestamp(DateTime.Now);

                    // Write Message
                    Log.chatMessage(m.TimeStamp, m.UserName, m.Channel, m.Text);

                    // Test Chat-Command
                    if (m.Text.StartsWith("!test"))
                    {
                        irc.sendChatMessage("it works mayhaps!");
                    }

                } else
                {
                    // Write all IRC messages that aren't parsed as Twitch chat messages.
                    Log.systemMessage(message);
                }

                // Reply with a Pong to the server, in case of Ping
                if (message.Equals("PING :tmi.twitch.tv"))
                {
                    irc.sendIrcMessage("PONG :tmi.twitch.tv");
                    Log.systemMessage("PONG :tmi.twitch.tv"); // Just to make sure
                }

            }
        }
    }
}
