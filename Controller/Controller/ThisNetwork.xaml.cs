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
using Newtonsoft.Json;

namespace Controller
{
    public partial class ThisNetwork : PhoneApplicationPage
    {
        //private SocketClient client = null;
        private String Message;

        public ThisNetwork()
        {
            InitializeComponent();
            DataContext = App.ViewModel;
        }

        // Load data for the ViewModel Items
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
           
            if (!App.isTaken)
            {
                NavigationContext.QueryString.TryGetValue("msg", out Message);
                App.isTaken = true;
            }
            if (!App.isNavigatedFromFileExplorer || Message.Equals("Controller.Option"))
            {
                App.client.Send("FILE_BROWSER#");
                Message = String.Empty;
            }
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
            App.PresentWorkingDirectory = (MainLongListSelector.SelectedItem as ItemViewModel).DriveName;
           // MessageBox.Show((MainLongListSelector.SelectedItem as ItemViewModel).DriveName);
            NavigationService.Navigate(new Uri("/FileExplorer.xaml", UriKind.RelativeOrAbsolute));
            MainLongListSelector.SelectedItem = null;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            //base.OnNavigatedFrom(e);
            try
            {
                if (e != null)
                {
                    if (e.Content.ToString().Equals("Controller.Option"))
                    {
                        App.client.Send(ExplorerSignal.END_EXPLORER + "#");
                    }
                }

            }
            catch (Exception)
            {

            }

        }
    }
}