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
            DataContext = App.ExplorerModel;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            App.ExplorerModel.LoadData();
        }

        private void FileOrFolderList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FileOrFolderList.SelectedItem != null)
            {
                if ((FileOrFolderList.SelectedItem as FolderFileModel).IsFolder)
                {
                    App.PresentWorkingDirectory += (FileOrFolderList.SelectedItem as FolderFileModel).FolderOrFileName + '\\';
                    App.client.Send(JsonConvert.SerializeObject(new ExplorerSignal(ExplorerSignal.GET_FILES, App.PresentWorkingDirectory)) + "#");
                    App.ExplorerModel.LoadData();
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
                App.ExplorerModel.LoadData();
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
                int j = 0;
                bool visible = true;
                byte[] fileBytes = App.client.ReceiveInBytes();

                using (var s = await file.OpenStreamForWriteAsync())
                {
                    s.Seek(0, SeekOrigin.End);
                    await s.WriteAsync(fileBytes, 0, fileBytes.Length);
                    int percentage = (int) (((float)i / noOfPackets) * 100.0);
                   
                    if (i == 0)
                    {
                        j = 1;
                    }
                    else
                    {
                        j = 0;
                    }
                    if (i == noOfPackets - 1)
                    {
                        visible = false;
                    }
                    App.ExplorerModel.LoadData(new DownloadModel()
                    {
                        ID = (App.ExplorerModel.DownloadedFiles.Count + j).ToString(),
                        FileName = folderOrFileName,
                        ProgressMin = "0",
                        ProgressMax = "100",
                        ProgressValue = percentage.ToString(),
                        Visibility = visible,
                        FileExtension = Path.GetExtension(folderOrFileName),
                    });
                }
            }
            //await Windows.System.Launcher.LaunchFileAsync(file);
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

        private async void DownloadList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            String fileName = (DownloadList.SelectedItem as DownloadModel).FileName;

            StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;
            var dataFolder = await local.GetFolderAsync("Downloaded Files");
            await Windows.System.Launcher.LaunchFileAsync(await dataFolder.GetFileAsync(fileName));
        }
    }
}