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
            DataContext = App.FolderFileModels;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
           App.FolderFileModels.LoadData();
        }

        private void FileOrFolderList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FileOrFolderList.SelectedItem != null)
            {
                App.client.Send((FileOrFolderList.SelectedItem as FolderFileModel).FolderOrFilePath + "#");
                
                App.FolderFileModels.LoadData();
            }
            else
            {
                return;
            }
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            App.PresentWorkingDirectory = (FileOrFolderList.SelectedItem as FolderFileModel).FolderOrFilePath;
           // MessageBox.Show(App.PresentWorkingDirectory);
            App.PresentWorkingDirectory = getDirectory(ref e);
            MessageBox.Show(App.PresentWorkingDirectory);
            App.client.Send(App.PresentWorkingDirectory + "#");
            App.FolderFileModels.LoadData();
        }

        private string getDirectory(ref System.ComponentModel.CancelEventArgs e)
        {
            String[] temp = App.PresentWorkingDirectory.Split('\\');
            String workingDirectory = String.Empty;

            for (int i = 0; i < temp.Length-1; ++i) {
                workingDirectory += temp[i] + "\\";
            }

            if (temp.Length > 1)
            {
                e.Cancel = true;
            }
            else
            {
                e.Cancel = false;
            }
            return workingDirectory;
        }
    }
}