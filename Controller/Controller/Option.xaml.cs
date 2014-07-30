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
                //NavigationService.Navigate(new Uri("/ThisNetwork.xaml", UriKind.RelativeOrAbsolute));
                //MessageBox.Show("File_Browser");
                NavigationService.Navigate(new Uri("/ThisNetwork.xaml?msg=" + "Controller.Option", UriKind.Relative)); 
            }
            else if (item.Name == "Mouse_Control")
            {
                //MessageBox.Show("File_Browser");
                NavigationService.Navigate(new Uri("/MousePad.xaml", UriKind.RelativeOrAbsolute));
            }
        }
    }
}