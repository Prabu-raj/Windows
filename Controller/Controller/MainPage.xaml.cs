using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Controller.Resources;
using Microsoft.Xna.Framework.Input;
using System.Windows.Media;
using System.Windows.Input;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework;
using System.Threading;
using System.Net.Sockets;
using Windows.Networking.Connectivity;
using Microsoft.Phone.Reactive;
using System.Text.RegularExpressions;
using System.IO;
using Windows.Storage;
using System.Threading.Tasks;

namespace Controller
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            Thread.Sleep(500);
        }

        
        private void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            // Make sure we can perform this action with valid data
            if (ValidateInput())
            {

                // Instantiate the SocketClient
                App.client = new SocketClient();
                App.IPAddress = txtRemoteHost.Text;
                App.client.Connect(App.IPAddress, 1212);
                String isConnected = App.client.Receive();

                if (isConnected == "connected")
                {
                    String width = (Application.Current.Host.Content.ActualWidth - 24).ToString();
                    String height = (Application.Current.Host.Content.ActualHeight - 99).ToString();
                    App.client.Send(width + '$' + height + '#');
                    NavigationService.Navigate(new Uri("/Option.xaml", UriKind.RelativeOrAbsolute));
                }
                else
                {
                    MessageBox.Show("Can't Connect to the Server");
                }
            }
        }

        #region UI Validation
        private bool ValidateInput()
        {
            // txtInput must contain some text

            if (String.IsNullOrWhiteSpace(txtRemoteHost.Text))
            {
                MessageBox.Show("IP cannot be empty..");
                return false;
            }
            else if (!IsValidIP(txtRemoteHost.Text))
            {
                MessageBox.Show("Not a Valid IP");
                return false;
            }

            return true;
        }

        #endregion
        private bool IsValidIP(String ip)
        {
            string[] parts = ip.Split('.');
            if (parts.Length < 4)
            {
                return false;
            }
            else
            {
                foreach (string part in parts)
                {
                    byte checkPart = 0;
                    if (!byte.TryParse(part, out checkPart))
                    {
                        return false;
                    }
                }
                return true;
            }
        }
    }
}