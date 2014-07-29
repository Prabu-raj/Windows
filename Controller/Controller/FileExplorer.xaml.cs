using System;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Controller.ViewModels;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;


namespace Controller
{
    public partial class FileExplorer : PhoneApplicationPage
    {
        private const int BUFFER_SIZE = 2048;
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
                if ((FileOrFolderList.SelectedItem as FolderFileModel).IsFolder)
                {
                    App.PresentWorkingDirectory += (FileOrFolderList.SelectedItem as FolderFileModel).FolderOrFileName + '\\';
                    App.client.Send(JsonConvert.SerializeObject(new ExplorerSignal(ExplorerSignal.GET_FILES, App.PresentWorkingDirectory)) + "#");
                    App.FolderFileModels.LoadData();
                }
            }
            else
            {
                return;
            }
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            App.PresentWorkingDirectory = getDirectory(ref e);
            String[] temp = App.PresentWorkingDirectory.Split('\\');

            if (temp.Length > 2)
            {
                e.Cancel = true;
                App.client.Send(JsonConvert.SerializeObject(new ExplorerSignal(ExplorerSignal.GET_FILES, App.PresentWorkingDirectory)) + "#");
                App.FolderFileModels.LoadData();
            }
            else
            {
                e.Cancel = false;
            }


        }

        private string getDirectory(ref System.ComponentModel.CancelEventArgs e)
        {
            String[] temp = App.PresentWorkingDirectory.Split('\\');

            String workingDirectory = String.Empty;

            for (int i = 0; i < temp.Length - 2; ++i)
            {
                if (temp[i] != String.Empty)
                    workingDirectory += temp[i] + "\\";
            }

            return workingDirectory;
        }


        private async void Download_Click(object sender, RoutedEventArgs e)
        {
            FolderFileModel folderOrFileModel = ((sender as MenuItem).DataContext as FolderFileModel);
            String filePath = App.PresentWorkingDirectory + folderOrFileModel.FolderOrFileName;

            App.client.Send(JsonConvert.SerializeObject(new ExplorerSignal(ExplorerSignal.DOWNLOAD_FILE, filePath)) + "#");
            String noOfPackets = App.client.Receive();

            await WriteToFile(folderOrFileModel.FolderOrFileName, Convert.ToInt16(noOfPackets));
        }

        private async Task WriteToFile(String folderOrFileName, int noOfPackets)
        {
            StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;
            var dataFolder = await local.CreateFolderAsync("Downloaded Files",
                    CreationCollisionOption.OpenIfExists);
            var file = await dataFolder.CreateFileAsync(folderOrFileName,
                    CreationCollisionOption.ReplaceExisting);

            for (int i = 0; i < noOfPackets; ++i)
            {
                byte[] fileBytes = App.client.ReceiveInBytes();

                using (var s = await file.OpenStreamForWriteAsync())
                {
                    s.Seek(0, SeekOrigin.End);
                    await s.WriteAsync(fileBytes, 0, fileBytes.Length);
                }
            }
            await Windows.System.Launcher.LaunchFileAsync(file);
        }

        private void Computer_Click(object sender, RoutedEventArgs e)
        {
            FolderFileModel folderOrFileModel = ((sender as MenuItem).DataContext as FolderFileModel);
            String filePath = App.PresentWorkingDirectory + folderOrFileModel.FolderOrFileName;

            App.client.Send(JsonConvert.SerializeObject(new ExplorerSignal(ExplorerSignal.OPEN_FILE, filePath)) + "#");
        }


        private void BuildLocalizedApplicationBar()
        {
            // set the page's app licationbar to a new instance of applicationbar.
            ApplicationBar = new ApplicationBar();
            ApplicationBarIconButton downloadButton = new ApplicationBarIconButton();

            downloadButton.IconUri = new Uri("/Assets/download.png", UriKind.Relative);
            downloadButton.Text = "Download";
            downloadButton.Click += downloadButton_Click;

            ApplicationBar.Buttons.Add(downloadButton);
            
        }

        private void downloadButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("You Clicked Me");
        }

        private void DownloadList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void MainLongListSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DownloadList_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

    }
}