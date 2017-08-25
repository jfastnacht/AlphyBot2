using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphyBot2
{
    class Log
    {
        public static String GetTimestamp(DateTime value)
        {
            return value.ToString("yyyy-MM-dd HH:mm:ss");
        }

        // Console logs a twitch chat message
        static public void ChatMessage(string timeStamp, string user, string channel, string text)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("[" + timeStamp + "]");
            Console.ResetColor();
            Console.WriteLine(" <" + user + "> " + channel + " : " + text);
        }

        // Logs a system message (e.g. response from the IRC server that isn't a chat message)
        static public void SystemMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        // Logs a custom error message
        static public void ErrorMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("[{0}] ", GetTimestamp(DateTime.Now));
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
