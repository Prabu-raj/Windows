using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace Controller
{
    public partial class Option : PhoneApplicationPage
    {
        //SocketClient client;
        public Option()
        {
            InitializeComponent();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox list = (sender) as ListBox;
            ListBoxItem item = (list.SelectedItem) as ListBoxItem;

            if (item.Name == "File_Browser")
            {
                App.client.Send("FILE_BROWSER#");
                NavigationService.Navigate(new Uri("/ThisNetwork.xaml", UriKind.RelativeOrAbsolute));
            }
            else if (item.Name == "Mouse_Control")
            {
                App.client.Send("MOUSE_CONTROL#");
                NavigationService.Navigate(new Uri("/MousePad.xaml", UriKind.RelativeOrAbsolute));
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
        }

    }
}