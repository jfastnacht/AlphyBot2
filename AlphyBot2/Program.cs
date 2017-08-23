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
            IrcClient irc = new IrcClient("irc.twitch.tv", 6667, "alphybot", "oauth:supersecretpasswordxd");
            irc.joinRoom("alphuite");

            string a;
            string b;
            b = "";

            while(true)
            {
                string message = irc.readChatMessage();
                a = message;
                if(a != b)
                {
                    Console.WriteLine(a);
                    b = a;
                }
                if (message.Contains("!test"))
                {
                    irc.sendChatMessage("it works mayhaps!");
                }
            }
        }
    }
}
