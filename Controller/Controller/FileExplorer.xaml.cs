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
using Coding4Fun.Toolkit.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Input;

namespace Controller
{
    public partial class FileExplorer : PhoneApplicationPage
    {
        private const int BUFFER_SIZE = 4096;
        private String RFileName;
        private Int64 noOfPackets;

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
            App.isNavigatedFromFileExplorer = true;
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
            folderOrFileModel = ((sender as MenuItem).DataContext as FolderFileModel);
            String filePath = App.PresentWorkingDirectory + folderOrFileModel.FolderOrFileName;

            App.client.Send(JsonConvert.SerializeObject(new ExplorerSignal(ExplorerSignal.DOWNLOAD_FILE, filePath)) + "#");
            String noOfPackets = App.client.Receive();
           
            try
            {
                this.noOfPackets = Convert.ToInt64(noOfPackets);
                await WriteToFile(folderOrFileModel.FolderOrFileName);
            }
            catch
            {
                //throw;
            }

        }

        private async Task WriteToFile(String folderOrFileName)
        {
            Local = Windows.Storage.ApplicationData.Current.LocalFolder;
            DataFolder = await Local.CreateFolderAsync("Downloaded Files",
                    CreationCollisionOption.OpenIfExists);
            InputPrompt fileName = null;

            String fileSize = String.Empty;
            if (noOfPackets < 512 / 2)
                fileSize = "Size :" + (noOfPackets * 4).ToString() + " KB";
            else if (noOfPackets > 512 / 2  && noOfPackets < 524288 / 2)
                fileSize = "Size :" + Math.Round((Decimal)((noOfPackets * 4) / 1024.0), 1, MidpointRounding.AwayFromZero).ToString() + " MB";
            else if (noOfPackets > 524288 / 2)
                fileSize = "Size :" + Math.Round((Decimal)((noOfPackets * 4) / (1024.0 * 1024.0)), 2, MidpointRounding.AwayFromZero).ToString() + " GB";

            try
            {
                file = await DataFolder.CreateFileAsync(folderOrFileName,
                    CreationCollisionOption.FailIfExists);
                RFileName = folderOrFileName;
                for (int i = 0; i < this.noOfPackets; ++i)
                {
                    int j = 0;
                    bool visible = true;
                    byte[] fileBytes = App.client.ReceiveInBytes();

                    using (var s = await file.OpenStreamForWriteAsync())
                    {
                        s.Seek(0, SeekOrigin.End);
                        await s.WriteAsync(fileBytes, 0, fileBytes.Length);
                        int percentage = (int)(((float)i / noOfPackets) * 100.0);

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
                            var toast = new ToastPrompt
                            {
                                Title = "Controller",
                                Message = "Download Completed",
                                FontSize = 20,
                                ImageSource = new BitmapImage(new Uri("..\\ApplicationIcon.png", UriKind.RelativeOrAbsolute))
                            };
                            toast.Show();
                        }

                        App.ExplorerModel.LoadData(new DownloadModel()
                        {
                            ID = (App.ExplorerModel.DownloadedFiles.Count + j).ToString(),
                            FileName = RFileName,
                            PercentageCompleted = percentage.ToString() + "% done ...",
                            FileSize = fileSize,
                            ProgressMin = "0",
                            ProgressMax = "100",
                            ProgressValue = percentage.ToString(),
                            Visibility = visible,
                            FileExtension = Path.GetExtension(folderOrFileName),
                        });
                    }
                }
            }
            catch (Exception)
            {
                fileName = new InputPrompt();
                fileName.Title = "Save As..";
                fileName.Message = "File already exists!";
                //fileName.InputScope = new System.Windows.Input.InputScope { Names = { new InputScopeName() { NameValue = InputScopeNameValue.AddressCountryName } } };
                fileName.Completed += fileName_Completed;
                fileName.Show();
            }
        }

        private async void fileName_Completed(object sender, PopUpEventArgs<string, PopUpResult> e)
        {
            if (e.PopUpResult == PopUpResult.Ok)
            {
                RFileName = e.Result;
                if (!RFileName.Contains('.'))
                {
                    RFileName += folderOrFileModel.FileExtension;
                }
                Local = Windows.Storage.ApplicationData.Current.LocalFolder;
                DataFolder = await Local.CreateFolderAsync("Downloaded Files",
                        CreationCollisionOption.OpenIfExists);
                file = await DataFolder.CreateFileAsync(RFileName);

                String fileSize = String.Empty;
                if (noOfPackets < 512 / 2)
                    fileSize = "Size :" + (noOfPackets * 4).ToString() + " KB";
                else if (noOfPackets > 512 / 2 && noOfPackets < 524288 / 2)
                    fileSize = "Size :" + Math.Round((Decimal)((noOfPackets * 4) / 1024.0), 1, MidpointRounding.AwayFromZero).ToString() + " MB";
                else if (noOfPackets > 524288 / 2)
                    fileSize = "Size :" + Math.Round((Decimal)((noOfPackets * 4) / (1024.0 * 1024.0)), 2, MidpointRounding.AwayFromZero).ToString() + " GB";

                for (int i = 0; i < this.noOfPackets; ++i)
                {
                    int j = 0;
                    bool visible = true;
                    byte[] fileBytes = App.client.ReceiveInBytes();

                    using (var s = await file.OpenStreamForWriteAsync())
                    {
                        s.Seek(0, SeekOrigin.End);
                        await s.WriteAsync(fileBytes, 0, fileBytes.Length);
                        int percentage = (int)(((float)i / noOfPackets) * 100.0);

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
                            var toast = new ToastPrompt
                            {
                                Title = "Controller",
                                Message = "Download Completed",
                                FontSize = 20,
                                ImageSource = new BitmapImage(new Uri("..\\ApplicationIcon.png", UriKind.RelativeOrAbsolute))
                            };
                            toast.Show();
                        }

                        App.ExplorerModel.LoadData(new DownloadModel()
                        {
                            ID = (App.ExplorerModel.DownloadedFiles.Count + j).ToString(),
                            FileName = RFileName,
                            PercentageCompleted = percentage.ToString() + "% done ...",
                            FileSize = fileSize,
                            ProgressMin = "0",
                            ProgressMax = "100",
                            ProgressValue = percentage.ToString(),
                            Visibility = visible,
                            FileExtension = Path.GetExtension(RFileName),
                        });
                    }
                }
            }
        }

        private void Computer_Click(object sender, RoutedEventArgs e)
        {
            FolderFileModel folderOrFileModel = ((sender as MenuItem).DataContext as FolderFileModel);
            String filePath = App.PresentWorkingDirectory + folderOrFileModel.FolderOrFileName;

            App.client.Send(JsonConvert.SerializeObject(new ExplorerSignal(ExplorerSignal.OPEN_FILE, filePath)) + "#");
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

        public StorageFolder Local { get; set; }

        public StorageFolder DataFolder { get; set; }

        public StorageFile file { get; set; }

        public FolderFileModel folderOrFileModel { get; set; }
    }
}