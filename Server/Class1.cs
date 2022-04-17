using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Device.Gpio;

namespace Server
{
    class Class1
    {
        GpioController controller = new GpioController(PinNumberingScheme.Board);
        Class3 ListWriter;

        public bool relay1_history = false;
        public bool relay2_history = false;
        public bool relay3_history = false;
        public bool relay4_history = false;
        public bool relayALL_history = false;

        public string button1_command = "NONE";
        public string button2_command = "NONE";

        string return_data;

        public Class1(Class3 listwriter)
        {
            ListWriter = listwriter;
            
            controller.OpenPin(3, PinMode.Output);
            controller.OpenPin(5, PinMode.Output);
            controller.OpenPin(8, PinMode.Output);
            controller.OpenPin(7, PinMode.Output);
 
            controller.Write(3, PinValue.High);
            controller.Write(5, PinValue.High);
            controller.Write(8, PinValue.High);
            controller.Write(7, PinValue.High);

            controller.OpenPin(10, PinMode.Input);
            controller.OpenPin(19, PinMode.Input);

            Thread thread_button1 = new Thread(Button1_Read);
            thread_button1.Start();

            Thread thread_button2 = new Thread(Button2_Read);
            thread_button2.Start();
        }

        public void setButton1Command(string command) 
        {
            button1_command = command;
        }

        public void setButton2Command(string command) 
        {
            button2_command = command;
        }

        private void Button1_Read()
        {
            while (true)
            {
                if (controller.Read(10) == PinValue.Low)
                {
                    if (!button1_command.EndsWith("NONE")) 
                    {
                        if (button1_command.StartsWith("relayAll"))
                        {
                            if(button1_command.EndsWith("ON"))
                            {
                                relayAll("ON");   
                            }
                            else if(button1_command.EndsWith("OFF"))
                            {
                                relayAll("OFF");                                   
                            }
                            else
                            {
                                relayAll("TOGGLE");                                     
                            }
                        }
                        else if (button1_command.StartsWith("relay1"))
                        {
                            if(button1_command.EndsWith("ON"))
                            {
                                relay1("ON");   
                            }
                            else if(button1_command.EndsWith("OFF"))
                            {
                                relay1("OFF");                                   
                            }
                            else
                            {
                                relay1("TOGGLE");                                     
                            }
                        }
                        else if (button1_command.StartsWith("relay2"))
                        {
                            if(button1_command.EndsWith("ON"))
                            {
                                relay2("ON");   
                            }
                            else if(button1_command.EndsWith("OFF"))
                            {
                                relay2("OFF");                                   
                            }
                            else
                            {
                                relay2("TOGGLE");                                     
                            }
                        }
                        else if (button1_command.StartsWith("relay3"))
                        {
                            if(button1_command.EndsWith("ON"))
                            {
                                relay3("ON");   
                            }
                            else if(button1_command.EndsWith("OFF"))
                            {
                                relay3("OFF");                                   
                            }
                            else
                            {
                                relay3("TOGGLE");                                     
                            }
                        }
                        else
                        {
                            if(button1_command.EndsWith("ON"))
                            {
                                relay4("ON");   
                            }
                            else if(button1_command.EndsWith("OFF"))
                            {
                                relay4("OFF");                                   
                            }
                            else
                            {
                                relay4("TOGGLE");                                     
                            }
                        }
                    }
                    else 
                    {
                        ListWriter.Broadcast("Button 1 Pressed");
                        Console.WriteLine("Button 1 Pressed");
                    }
                    Thread.Sleep(1000);
                }
            }
        }

        private void Button2_Read()
        {
            string relayarg;

            while (true)
            {
                if (controller.Read(19) == PinValue.Low)
                {
                    if (!button2_command.EndsWith("NONE"))
                    {
                        if (button2_command.StartsWith("relayAll"))
                        {
                            if(button2_command.EndsWith("ON"))
                            {
                                relayAll("ON");   
                            }
                            else if(button2_command.EndsWith("OFF"))
                            {
                                relayAll("OFF");                                   
                            }
                            else
                            {
                                relayAll("TOGGLE");                                     
                            }
                        }
                        else if (button2_command.StartsWith("relay1"))
                        {
                            if(button2_command.EndsWith("ON"))
                            {
                                relay1("ON");   
                            }
                            else if(button2_command.EndsWith("OFF"))
                            {
                                relay1("OFF");                                   
                            }
                            else
                            {
                                relay1("TOGGLE");                                     
                            }
                        }
                        else if (button2_command.StartsWith("relay2"))
                        {
                            if(button2_command.EndsWith("ON"))
                            {
                                relay2("ON");   
                            }
                            else if(button2_command.EndsWith("OFF"))
                            {
                                relay2("OFF");                                   
                            }
                            else
                            {
                                relay2("TOGGLE");                                     
                            }
                        }
                        else if (button2_command.StartsWith("relay3"))
                        {
                            if(button2_command.EndsWith("ON"))
                            {
                                relay3("ON");   
                            }
                            else if(button2_command.EndsWith("OFF"))
                            {
                                relay3("OFF");                                   
                            }
                            else
                            {
                                relay3("TOGGLE");                                     
                            }
                        }
                        else
                        {
                            if(button2_command.EndsWith("ON"))
                            {
                                relay4("ON");   
                            }
                            else if(button2_command.EndsWith("OFF"))
                            {
                                relay4("OFF");                                   
                            }
                            else
                            {
                                relay4("TOGGLE");                                     
                            }
                        }
                    }
                    else
                    {
                        ListWriter.Broadcast("Button 2 Pressed");
                        Console.WriteLine("Button 2 Pressed");
                    }
                    Thread.Sleep(1000);
                }
            }
        }
        public string getRelay(string str)
        {
            formatRelay(str);
            return return_data;
        }

        private void formatRelay(string str)
        {
            switch (str)
            {
                case "1":
                    if (relay1_history == true)
                    {
                        return_data = "relay1 ON";
                        break;
                    }
                    else
                    {
                        return_data = "relay1 OFF";
                        break;
                    }
                case "2":
                    if (relay2_history == true)
                    {
                        return_data = "relay2 ON";
                        break;
                    }
                    else
                    {
                        return_data = "relay2 OFF";
                        break;
                    }

                case "3":
                    if (relay3_history == true)
                    {
                        return_data = "relay3 ON";
                        break;
                    }
                    else
                    {
                        return_data = "relay3 OFF";
                        break;
                    }

                case "4":
                    if (relay4_history == true)
                    {
                        return_data = "relay4 ON";
                        break;
                    }
                    else
                    {
                        return_data = "relay4 OFF";
                        break;
                    }
                default:
                    break;
            }
        }



        public void relayAll(string state)
        {
            if (state == "ON")
            {
                controller.Write(3, PinValue.Low);
                controller.Write(5, PinValue.Low);
                controller.Write(7, PinValue.Low);
                controller.Write(8, PinValue.Low);

                relayALL_history = true;
                relay1_history = true;
                relay2_history = true;
                relay3_history = true;
                relay4_history = true;


                ListWriter.Broadcast("relayALL ON");
            }
            else if (state == "OFF")
            {
                controller.Write(3, PinValue.High);
                controller.Write(5, PinValue.High);
                controller.Write(8, PinValue.High);
                controller.Write(7, PinValue.High);

                relayALL_history = false;
                relay1_history = false;
                relay2_history = false;
                relay3_history = false;
                relay4_history = false;

                ListWriter.Broadcast("relayALL OFF");
            }
            else
            {
                if (relayALL_history == true)
                {
                    controller.Write(3, PinValue.Low);
                    controller.Write(5, PinValue.Low);
                    controller.Write(7, PinValue.Low);
                    controller.Write(8, PinValue.Low);

                    relayALL_history = false;
                    relay1_history = false;
                    relay2_history = false;
                    relay3_history = false;
                    relay4_history = false;

                    ListWriter.Broadcast("relayALL OFF");
                }
                else
                {
                    controller.Write(3, PinValue.High);
                    controller.Write(5, PinValue.High);
                    controller.Write(7, PinValue.High);
                    controller.Write(8, PinValue.High);

                    relayALL_history = true;
                    relay1_history = true;
                    relay2_history = true;
                    relay3_history = true;
                    relay4_history = true;

                    ListWriter.Broadcast("relayALL ON");
                }
            }
        }

        public void relay1(string state)
        {
            if (state == "ON")
            {
                controller.Write(3, PinValue.Low);
                relay1_history = true;
                ListWriter.Broadcast("relay1 ON");
            }
            else if (state == "OFF")
            {
                controller.Write(3, PinValue.High);
                relay1_history = false;
                ListWriter.Broadcast("relay1 OFF");
            }
            else
            {
                if (relay1_history == true)
                {
                    relay1_history = false;
                    controller.Write(3, PinValue.High);
                    ListWriter.Broadcast("relay1 OFF");
                }
                else
                {
                    relay1_history = true;
                    controller.Write(3, PinValue.Low);
                    ListWriter.Broadcast("relay1 ON");
                }
            }
        }

        public void relay2(string state)
        {
            if (state == "ON")
            {
                relay2_history = true;
                controller.Write(5, PinValue.Low);
                ListWriter.Broadcast("relay2 ON");
            }
            else if (state == "OFF")
            {
                relay2_history = false;
                controller.Write(5, PinValue.High);
                ListWriter.Broadcast("relay2 OFF");
            }
            else
            {
                if (relay2_history == true)
                {
                    relay2_history = false;
                    controller.Write(5, PinValue.High);
                    ListWriter.Broadcast("relay2 OFF");
                }
                else
                {
                    relay2_history = true;
                    controller.Write(5, PinValue.Low);
                    ListWriter.Broadcast("relay2 ON");
                }
            }
        }

        public void relay3(string state)
        {
            if (state == "ON")
            {
                relay3_history = true;
                controller.Write(8, PinValue.Low);
                ListWriter.Broadcast("relay3 ON");
            }
            else if (state == "OFF")
            {
                relay2_history = false;
                controller.Write(8, PinValue.High);
                ListWriter.Broadcast("relay3 OFF");
            }
            else
            {
                if (relay3_history == true)
                {
                    relay3_history = false;
                    controller.Write(8, PinValue.High);
                    ListWriter.Broadcast("relay3 OFF");
                }
                else
                {
                    relay3_history = true;
                    controller.Write(8, PinValue.Low);
                    ListWriter.Broadcast("relay3 ON");
                }
            }
        }

        public void relay4(string state)
        {
            if (state == "ON")
            {
                relay4_history = true;
                controller.Write(7, PinValue.Low);
                ListWriter.Broadcast("relay4 ON");
            }
            else if (state == "OFF")
            {
                relay4_history = false;
                controller.Write(7, PinValue.High);
                ListWriter.Broadcast("relay4 OFF");
            }
            else
            {
                if (relay4_history == true)
                {
                    relay4_history = false;
                    controller.Write(7, PinValue.High);
                    ListWriter.Broadcast("relay4 OFF");
                }
                else
                {
                    relay4_history = true;
                    controller.Write(7, PinValue.Low);
                    ListWriter.Broadcast("relay4 ON");
                }
            }
        }
    }
}
