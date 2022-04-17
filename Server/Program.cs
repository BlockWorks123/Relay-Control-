using System;
using System.Net;
using System.Net.Sockets;

namespace Server
{
    class Program
    {
        private static TcpListener tcpListener = new TcpListener(IPAddress.Any, 8000);
        public static string correct_token = "2610";

        static void Main(string[] args)
        {
            Console.WriteLine("Server Started...");
            tcpListener.Start();
            
            Class3 ListWriter = new Class3();
            Class1 PinController = new Class1(ListWriter);

            while (true) 
            {
                Console.WriteLine("Listening for Clients");
                TcpClient tcpClient = tcpListener.AcceptTcpClient();
                Console.WriteLine("Client Connected");
                
                StreamWriter sWriter = new StreamWriter(tcpClient.GetStream());
                StreamReader tReader = new StreamReader(tcpClient.GetStream());
                string token = tReader.ReadLine();

                if (!String.IsNullOrEmpty(token) && token == correct_token)
                {
                    sWriter.WriteLine("correct");
                    sWriter.Flush();

                    Console.WriteLine("Token Verified");

                    Thread.Sleep(1000);
                    sWriter.WriteLine(PinController.getRelay("1"));
                    sWriter.WriteLine(PinController.getRelay("2"));
                    sWriter.WriteLine(PinController.getRelay("3"));
                    sWriter.WriteLine(PinController.getRelay("4"));
                    sWriter.Flush();
                    
                    Console.WriteLine("Transfering Client");
                    new Class2(tcpClient, PinController, ListWriter);
                }
                else
                {
                    sWriter.WriteLine("incorrect");
                    sWriter.Flush();
                    tcpClient.Close();

                    Console.WriteLine("Closing Client");
                }
            }
        }
    }
}
