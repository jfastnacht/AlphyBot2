using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphyBot2
{
    class Program
    {
        static void Main(string[] args)
        {
            // Join a Twitch Channel
            IrcClient irc = new IrcClient("irc.twitch.tv", 6667, "alphybot", "oauth:supersecretpasswordxd");
            irc.joinRoom("alphuite");
            
            // Main Loop
            while(true)
            {
                // Read last IRC message
                string message = irc.readIrcMessage();

                // Echo **all** IRC messages
                Console.WriteLine(message);

                // Test Chat-Command
                if (message.Contains("!test"))
                {
                    irc.sendChatMessage("it works mayhaps!");
                }

                // Reply with a Pong to the server, in case of Ping
                if (message.Equals("PING :tmi.twitch.tv"))
                {
                    irc.sendIrcMessage("PONG :tmi.twitch.tv");
                    Console.WriteLine("PONG :tmi.twitch.tv"); // Just to make sure
                }
            }
        }
    }
}
