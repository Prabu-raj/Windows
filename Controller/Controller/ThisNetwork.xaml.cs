using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Controller.ViewModels;

namespace Controller
{
    public partial class ThisNetwork : PhoneApplicationPage
    {
        //private SocketClient client = null;

        public ThisNetwork()
        {
            InitializeComponent();
            DataContext = App.ViewModel;
        }

        // Load data for the ViewModel Items
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }

        }

        // Handle selection changed on LongListSelector
        private void MainLongListSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MainLongListSelector.SelectedItem == null)
                return;

            App.client.Send((MainLongListSelector.SelectedItem as ItemViewModel).DriveName + "#");
            NavigationService.Navigate(new Uri("/FileExplorer.xaml", UriKind.RelativeOrAbsolute));

            MainLongListSelector.SelectedItem = null;
        }
    }
}