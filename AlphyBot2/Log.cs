using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphyBot2
{
    class Log
    {
        // Console logs a twitch chat message
        static public void chatMessage(string timeStamp, string user, string channel, string text)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("[" + timeStamp + "]");
            Console.ResetColor();
            Console.WriteLine(" <" + user + "> " + channel + " : " + text);
        }

        // Logs a system message (e.g. response from the IRC server that isn't a chat message)
        static public void systemMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
