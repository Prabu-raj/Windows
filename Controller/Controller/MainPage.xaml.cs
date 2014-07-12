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

namespace Controller
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            Thread.Sleep(1000);
        }

        private void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            // Make sure we can perform this action with valid data
            if (ValidateInput())
            {

                // Instantiate the SocketClient
                App.client = new SocketClient();

                string result = App.client.Connect(txtRemoteHost.Text, 1212);
                NavigationService.Navigate(new Uri("/Option.xaml", UriKind.RelativeOrAbsolute));
                //NavigationExtensions.Navigate(this.NavigationService, "/Option.xaml", client);
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