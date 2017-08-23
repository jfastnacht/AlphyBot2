using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace AlphyBot2
{
    class Program
    {
        static void Main(string[] args)
        {
            IrcClient irc = new IrcClient("irc.twitch.tv", 6667, "alphybot", "password");
            irc.joinRoom("alphuite");

            Console.WriteLine("This is a test message xd");

            string a;
            string b;
            while (true)
            {
                string message = irc.readMessage();
                a = message;
                if (a != b)
                {
                    Console.WriteLine(a);
                    a = b;
                }
                

                if (message.Contains("!test"))
                {
                    irc.sendChatMessage("tested!");
                }
            }
        }
    }
}
