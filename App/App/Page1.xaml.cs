using System;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Timers;
using System.Collections.Generic;
using System.Linq;

namespace App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page1 : ContentPage
    {
        TcpClient tcpclient;
        StreamWriter sWriter;
        Thread thread;

        private static System.Timers.Timer aTimer;
        int counter = 6;
        bool message_lock = false;
        string host_history;
        string token_history;
        
        private string myStringProperty;
        public string MyStringProperty
        {
            get { return myStringProperty; }
            set
            {
                myStringProperty = value;
                OnPropertyChanged(nameof(MyStringProperty));
            }
        }

        static Dictionary<string, string> colours { get; } = new Dictionary<string, string>
        {
            { "Toggle", "TOGGLE"},
            { "Turn On", "ON"},
            { "Turn Off", "OFF"},
            { "Send Message", "NONE"},
        };
        public List<string> Colors { get; } = colours.Keys.ToList();


        static Dictionary<string, string> relays { get; } = new Dictionary<string, string>
        {
            { "Relay1", "relay1"},
            { "Relay2", "relay2"},
            { "Relay3", "relay3"},
            { "Relay4", "relay4"},
            { "RelayAll", "relayAll"},
        };
        public List<string> Relays { get; } = relays.Keys.ToList();

        public string SelectedColor1 { get; set; }
        public string SelectedRelay1 { get; set; }

        public string SelectedColor2 { get; set; }
        public string SelectedRelay2 { get; set; }

        public Page1(TcpClient client, string host, string token)
        {
            InitializeComponent();

            BindingContext = this;
            tcpclient = client;
            host_history = host;
            token_history = token;

            aTimer = new System.Timers.Timer(1000);
            aTimer.Elapsed += OnTimedEvent;

            sWriter = new StreamWriter(tcpclient.GetStream());

            thread = new Thread(Message_Recv);
            thread.Start();
        }

        public void Message_Recv()
        {
            StreamReader sReader = new StreamReader(tcpclient.GetStream());

            while (true)
            {
                try
                {
                    string message = sReader.ReadLine();
                    Console.WriteLine(message);

                    if (message.StartsWith("relay1")) 
                    {
                        if (message.EndsWith("OFF")) 
                        {
                            Device.BeginInvokeOnMainThread(() =>
                            {
                                relayOn1.BackgroundColor = Color.DarkGray;
                                relayOff1.BackgroundColor = Color.DarkOliveGreen;
                            });
                        }
                        else if(message.EndsWith("ON")) 
                        {
                            Device.BeginInvokeOnMainThread(() =>
                            {
                                relayOff1.BackgroundColor = Color.DarkGray;
                                relayOn1.BackgroundColor = Color.DarkOliveGreen;
                            });
                        }
                    }
                    else if (message.StartsWith("relay2"))
                    {
                        if (message.EndsWith("OFF"))
                        {
                            Device.BeginInvokeOnMainThread(() =>
                            {
                                relayOn2.BackgroundColor = Color.DarkGray;
                                relayOff2.BackgroundColor = Color.DarkOliveGreen;
                            });
                        }
                        else if (message.EndsWith("ON"))
                        {
                            Device.BeginInvokeOnMainThread(() =>
                            {
                                relayOff2.BackgroundColor = Color.DarkGray;
                                relayOn2.BackgroundColor = Color.DarkOliveGreen;
                            });
                        }
                    }
                    else if (message.StartsWith("relay3"))
                    {
                        if (message.EndsWith("OFF"))
                        {
                            Device.BeginInvokeOnMainThread(() =>
                            {
                                relayOn3.BackgroundColor = Color.DarkGray;
                                relayOff3.BackgroundColor = Color.DarkOliveGreen;
                            });
                        }
                        else if (message.EndsWith("ON"))
                        {
                            Device.BeginInvokeOnMainThread(() =>
                            {
                                relayOff3.BackgroundColor = Color.DarkGray;
                                relayOn3.BackgroundColor = Color.DarkOliveGreen;
                            });
                        }
                    }
                    else if (message.StartsWith("relay4"))
                    {
                        if (message.EndsWith("OFF"))
                        {
                            Device.BeginInvokeOnMainThread(() =>
                            {
                                relayOn4.BackgroundColor = Color.DarkGray;
                                relayOff4.BackgroundColor = Color.DarkOliveGreen;
                            });
                        }
                        else if (message.EndsWith("ON"))
                        {
                            Device.BeginInvokeOnMainThread(() =>
                            {
                                relayOff4.BackgroundColor = Color.DarkGray;
                                relayOn4.BackgroundColor = Color.DarkOliveGreen;
                            });
                        }
                    }
                    else if (message.StartsWith("relayALL"))
                    {
                        if (message.EndsWith("OFF"))
                        {
                            Device.BeginInvokeOnMainThread(() =>
                            {
                                relayOnALL.BackgroundColor = Color.DarkGray;
                                relayOffALL.BackgroundColor = Color.IndianRed;
                                relayOn4.BackgroundColor = Color.DarkGray;
                                relayOff4.BackgroundColor = Color.DarkOliveGreen;
                                relayOn3.BackgroundColor = Color.DarkGray;
                                relayOff3.BackgroundColor = Color.DarkOliveGreen;
                                relayOn2.BackgroundColor = Color.DarkGray;
                                relayOff2.BackgroundColor = Color.DarkOliveGreen;
                                relayOn1.BackgroundColor = Color.DarkGray;
                                relayOff1.BackgroundColor = Color.DarkOliveGreen;

                            });
                        }
                        else if (message.EndsWith("ON"))
                        {
                            Device.BeginInvokeOnMainThread(() =>
                            {
                                relayOffALL.BackgroundColor = Color.DarkGray;
                                relayOnALL.BackgroundColor = Color.IndianRed;
                                relayOff4.BackgroundColor = Color.DarkGray;
                                relayOn4.BackgroundColor = Color.DarkOliveGreen;
                                relayOff3.BackgroundColor = Color.DarkGray;
                                relayOn3.BackgroundColor = Color.DarkOliveGreen;
                                relayOff2.BackgroundColor = Color.DarkGray;
                                relayOn2.BackgroundColor = Color.DarkOliveGreen;
                                relayOff1.BackgroundColor = Color.DarkGray;
                                relayOn1.BackgroundColor = Color.DarkOliveGreen;
                            });
                        }
                    }
                    else 
                    {
                        MyStringProperty = message;
                    }
                }
                catch (Exception)
                {
                    return;
                }
            }
        }

        void Send_Message(string message) 
        {
            if(message_lock == false) 
            {
                try
                {
                    Console.WriteLine("sending " + message);
                    sWriter.WriteLine(message);
                    sWriter.Flush();
                }
                catch (Exception)
                {
                    message_lock = true;
                    sWriter.Close();
                    tcpclient.Close();
                    thread.Abort();
                    Attempt_Reconnect();
                }
            }
            else 
            {
                return;
            }
        }

        public async void Attempt_Reconnect() 
        {
            if (counter == -1)
            {
                MyStringProperty = $"Attempting to Reconnect";

                tcpclient = new TcpClient();
                try 
                {
                    tcpclient.Connect(host_history, 8000);

                    sWriter = new StreamWriter(tcpclient.GetStream());
                    Send_Message(token_history);

                    thread = new Thread(Message_Recv);
                    thread.Start();

                    message_lock = false;
                }
                catch (Exception) 
                {
                    MyStringProperty = $"RECONNECT FAILED";
                    tcpclient.Close();
                    await Navigation.PushModalAsync(new MainPage());
                }
            }
            else 
            {
                if(counter == 6)
                {
                    MyStringProperty = $"Connection Lost";
                }
                else 
                {
                    MyStringProperty = $"Attempting to reconnect in {counter}";
                }
                counter = counter - 1;
                SetTimer();
            }

        }

        private void SetTimer()
        {         
            aTimer.AutoReset = false;
            aTimer.Enabled = true;
            aTimer.Start();
        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            Attempt_Reconnect();
        }

        public void relayOn1_Clicked(object sender, EventArgs args)
        {
            Send_Message("relay1 ON");
        }

        public void relayOff1_Clicked(object sender, EventArgs args)
        {
            Send_Message("relay1 OFF");
        }

        public void relayToggle1_Clicked(object sender, EventArgs args)
        {
            Send_Message("relay1 TOGGLE");
        }

        public void relayOn2_Clicked(object sender, EventArgs args)
        {
            Send_Message("relay2 ON");
        }

        public void relayOff2_Clicked(object sender, EventArgs args)
        {
            Send_Message("relay2 OFF");
        }

        public void relayToggle2_Clicked(object sender, EventArgs args)
        {
            Send_Message("relay2 TOGGLE");
        }

        public void relayOn3_Clicked(object sender, EventArgs args)
        {
            Send_Message("relay3 ON");
        }

        public void relayOff3_Clicked(object sender, EventArgs args)
        {
            Send_Message("relay3 OFF");
        }

        public void relayToggle3_Clicked(object sender, EventArgs args)
        {
            Send_Message("relay3 TOGGLE");
        }

        public void relayOn4_Clicked(object sender, EventArgs args)
        {
            Send_Message("relay4 ON");
        }

        public void relayOff4_Clicked(object sender, EventArgs args)
        {
            Send_Message("relay4 OFF");
        }

        public void relayToggle4_Clicked(object sender, EventArgs args)
        {
            Send_Message("relay4 TOGGLE");
        }

        public void relayOnALL_Clicked(object sender, EventArgs args)
        {
            Send_Message("relayALL ON");
        }

        public void relayOffALL_Clicked(object sender, EventArgs args)
        {
            Send_Message("relayALL OFF");
        }

        public void relayToggleALL_Clicked(object sender, EventArgs args)
        {
            Send_Message("relayALL TOGGLE");
        }

        public void Picker1_Select(object sender, EventArgs args)
        {
            if (string.IsNullOrEmpty(SelectedColor1) || string.IsNullOrEmpty(SelectedRelay1)) 
            {
                return;
            }
            var selectedColorValue = colours[SelectedColor1];
            var selectedRelayValue = relays[SelectedRelay1];
            Send_Message("button1 " + selectedRelayValue + selectedColorValue);
        }

        public void Picker2_Select(object sender, EventArgs args)
        {
            if (string.IsNullOrEmpty(SelectedColor2) || string.IsNullOrEmpty(SelectedRelay2))
            {
                return;
            }
            var selectedColorValue = colours[SelectedColor2];
            var selectedRelayValue = relays[SelectedRelay2];
            Send_Message("button2 " + selectedRelayValue + " " + selectedColorValue);
        }
    }
}