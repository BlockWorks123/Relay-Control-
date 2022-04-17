using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using Xamarin.Forms.Xaml;

namespace App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        TcpClient tcpClient = new TcpClient();
        StreamReader sReader;

        public MainPage()
        {
            InitializeComponent();
        }

        private async void Server_Start(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                button.Text = "Connecting...";
            });

            string host = address.Text;
            string password = token.Text;

            try 
            {
                tcpClient.Connect(host, 8000);
                
                Thread.Sleep(5000);
                StreamWriter sWriter = new StreamWriter(tcpClient.GetStream());
                sWriter.WriteLine(password);
                sWriter.Flush();

                sReader = new StreamReader(tcpClient.GetStream());
                string if_correct = sReader.ReadLine();

                if (if_correct == "correct")
                {
                    await Navigation.PushModalAsync(new Page1(tcpClient, host, password));
                }
                else
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        button.Text = "Connect";
                    });

                    await DisplayAlert("Alert", "Password was Incorrect", "OK");
                    return;
                }
            }
            catch (Exception) 
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    button.Text = "Connect";
                });

                await DisplayAlert("Alert", "Connection was Refused", "OK");
                return;
            }
        }
    }
}
