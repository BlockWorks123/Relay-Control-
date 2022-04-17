using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Text;
using System.Threading;

namespace Server
{
    class Class3
    {
        private static List<TcpClient> tcpClientsList = new List<TcpClient>();

        public void AddUser(TcpClient client) 
        {
            tcpClientsList.Add(client);
        }

        public void RemoveUser(TcpClient client)
        {
            tcpClientsList.Remove(client);
        }

        public void Broadcast(string msg)
        {
            foreach (TcpClient client in tcpClientsList)
            {
                try
                {
                    StreamWriter sWriter = new StreamWriter(client.GetStream());
                    sWriter.WriteLine(msg);
                    sWriter.Flush();
                }
                catch
                {
                    return;
                }
            }
        }
    }
}
