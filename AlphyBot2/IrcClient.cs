﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace AlphyBot2
{
    class IrcClient
    {
        private string userName;
        private string channel;

        private TcpClient tcpClient;
        private StreamReader inputStream;
        private StreamWriter outputStream;

        public IrcClient(string ip, int port, string userName, string password)
        {
            this.userName = userName;

            tcpClient = new TcpClient(ip, port);
            inputStream = new StreamReader(tcpClient.GetStream());
            outputStream = new StreamWriter(tcpClient.GetStream());

            outputStream.WriteLine("PASS " + password);
            outputStream.WriteLine("NICK " + userName);
            outputStream.WriteLine("USER " + userName + " 8 * :" + userName);
            outputStream.Flush();

        }

        public void JoinRoom(string channel)
        {
            this.channel = channel;

            outputStream.WriteLine("JOIN #" + channel);
            outputStream.Flush();
        }

        public void SendIrcMessage(string message)
        {
            outputStream.WriteLine(message);
            outputStream.Flush();
        }

        public void SendChatMessage(string message)
        {
            SendIrcMessage(":" + userName + "!" + userName + "@" + userName 
                + ".tmi.twitch.tv PRIVMSG #" + channel + " :" + message);

            Log.ChatMessage(Log.GetTimestamp(DateTime.Now), userName, "#" + channel, message);
        }

        public string ReadIrcMessage()
        {
            string message = inputStream.ReadLine();
            return message;
        }
    }
}
