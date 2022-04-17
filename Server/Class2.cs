using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace Server
{

    class Class2
    {
        public TcpClient tcpClient;
        public Class1 PinController;
        public Class3 ListWriter;

        public Class2(TcpClient client, Class1 controller, Class3 listwriter) 
        {
            tcpClient = client;
            PinController = controller;
            ListWriter = listwriter;

            ListWriter.AddUser(tcpClient);
            
            Thread thread = new Thread(Client_Listener);
            thread.Start();

        }

        public static bool Socket_Connected(TcpClient function_client)
        {
            Socket s = function_client.Client;
            bool part1 = s.Poll(1000, SelectMode.SelectRead);
            bool part2 = (s.Available == 0);
            if (part1 && part2)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        
        public void Client_Listener()
        {
            StreamReader reader = new StreamReader(tcpClient.GetStream());

            while (true)
            {
                try
                {
                    string message = reader.ReadLine();

                    if (Socket_Connected(tcpClient))
                    {
                        if (!String .IsNullOrEmpty(message))
                        {
                            if (message.StartsWith("relayALL"))
                            {
                                if (message.EndsWith("ON"))
                                {
                                    PinController.relayAll("ON");
                                }
                                else if (message.EndsWith("OFF"))
                                {
                                    PinController.relayAll("OFF");
                                }
                                else if (message.EndsWith("TOGGLE"))
                                {
                                    PinController.relayAll("TOGGLE");
                                }
                            }
                            else if (message.StartsWith("relay1"))
                            {
                                if (message.EndsWith("ON"))
                                {
                                    PinController.relay1("ON");

                                }
                                else if (message.EndsWith("OFF"))
                                {
                                    PinController.relay1("OFF");
                                }
                                else if (message.EndsWith("TOGGLE"))
                                {
                                    PinController.relay1("TOGGLE");
                                }
                            }
                            else if (message.StartsWith("relay2"))
                            {
                                if (message.EndsWith("ON"))
                                {
                                    PinController.relay2("ON");
                                }
                                else if (message.EndsWith("OFF"))
                                {
                                    PinController.relay2("OFF");
                                }
                                else if (message.EndsWith("TOGGLE"))
                                {
                                    PinController.relay2("TOGGLE");
                                }
                            }
                            else if (message.StartsWith("relay3"))
                            {
                                if (message.EndsWith("ON"))
                                {
                                    PinController.relay3("ON");
                                }
                                else if (message.EndsWith("OFF"))
                                {
                                    PinController.relay3("OFF");
                                }
                                else if (message.EndsWith("TOGGLE"))
                                {
                                    PinController.relay3("TOGGLE");
                                }
                            }
                            else if (message.StartsWith("relay4"))
                            {
                                if (message.EndsWith("ON"))
                                {
                                    PinController.relay4("ON");
                                }
                                else if (message.EndsWith("OFF"))
                                {
                                    PinController.relay4("OFF");
                                }
                                else if (message.EndsWith("TOGGLE"))
                                {
                                    PinController.relay4("TOGGLE");
                                }                 
                            }
                            else if (message.StartsWith("button1")) 
                            {
                                message = message.Replace("button1 ", "");
                                PinController.setButton1Command(message);
                            }
                            else if (message.StartsWith("button2"))
                            {
                                message = message.Replace("button2 ", "");
                                PinController.setButton2Command(message);
                            }
                            else
                            {
                                ListWriter.Broadcast(message);
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Client Disconnected");
                        ListWriter.RemoveUser(tcpClient);
                        tcpClient.Close();
                    }
                }
                catch (Exception)
                {
                    return;
                }
            }
        }
    }
}
