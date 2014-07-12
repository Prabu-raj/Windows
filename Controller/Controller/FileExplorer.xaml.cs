using System;
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
    public partial class FileExplorer : PhoneApplicationPage
    {
        public FileExplorer()
        {
            InitializeComponent();
            DataContext = App.FolderFileModel;
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (!App.FolderFileModel.IsDataLoaded)
            {
                App.FolderFileModel.LoadData();
            }
        }
        private void FileOrFolderList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}