using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Windows.Storage;

namespace Controller.ViewModels
{
    public class FileModel 
    {
        public ObservableCollection<DownloadModel> DownloadedFiles { get; private set; }
        public ObservableCollection<FolderFileModel> FilesOrFolders { get; private set; }
        public bool IsDataLoaded { get; set; }

        public FileModel()
        {
            this.DownloadedFiles = new ObservableCollection<DownloadModel>();
            this.FilesOrFolders = new ObservableCollection<FolderFileModel>();
        }

        public void LoadData()
        {
            //new Thread(new ThreadStart(CreateDownloadedFileGroup)).Start();
            //new Thread(new ThreadStart(CreateFileGroup)).Start();
            CreateDownloadedFileGroup();
            CreateFileGroup();
            this.IsDataLoaded = true;
        }

        public void LoadData(DownloadModel tempData)
        {
          //new Thread(() => CreateDownloadedFileGroup(tempData)).Start();
            CreateDownloadedFileGroup(tempData);
        }

        private void CreateFileGroup()
        {
            String data = String.Empty;
            data = App.client.Receive();

            FolderOrFileDetails folderOrFileDetails = new FolderOrFileDetails();
            try
            {
                folderOrFileDetails = (JsonConvert.DeserializeObject<FolderOrFileDetails>(data)) as FolderOrFileDetails;
            }
            catch (JsonException e)
            {
                MessageBox.Show(e.ToString());
                return;
            }


            try
            {
                this.FilesOrFolders.Clear();

                for (int i = 0; i < folderOrFileDetails.FolderOrFileName.Length; ++i)
                {
                    String fileOrFolderName = String.Empty;

                    if (folderOrFileDetails.FolderOrFileName != null)
                    {
                        if (!folderOrFileDetails.IsFolder[i] && folderOrFileDetails.FolderOrFileName[i] != String.Empty)
                        {
                            fileOrFolderName = folderOrFileDetails.FolderOrFileName[i] + folderOrFileDetails.FileExtension[i];
                        }
                        else if (folderOrFileDetails.FolderOrFileName[i] != String.Empty)
                        {
                            fileOrFolderName = folderOrFileDetails.FolderOrFileName[i];
                        }
                        else
                        {
                            continue;
                        }

                        this.FilesOrFolders.Add(new FolderFileModel()
                        {
                            ID = i,
                            FolderOrFileName = fileOrFolderName,
                            FileExtension = folderOrFileDetails.FileExtension[i],
                            IsFolder = folderOrFileDetails.IsFolder[i]

                        });
                    }
                }

            }
            catch (Exception e)
            {

                MessageBox.Show(e.ToString());
            }
        }

        private async void CreateDownloadedFileGroup()
        {
            int i = 0;
            this.DownloadedFiles.Clear();
            StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;
            var dataFolder = await local.CreateFolderAsync("Downloaded Files",
                    CreationCollisionOption.OpenIfExists);
            IReadOnlyCollection<StorageFile> files = await dataFolder.GetFilesAsync();
            foreach (StorageFile file in files)
            {
                DownloadedFiles.Insert(0, new DownloadModel()
                {
                    ID = i.ToString(),
                    FileName = files.ElementAt(i).Name,
                    ProgressMin = "0",
                    ProgressMax = "100",
                    ProgressValue = "100",
                    Visibility = false,
                    FileExtension = Path.GetExtension(files.ElementAt(i).Name),
                });
                ++i;
            }
        }

        private void CreateDownloadedFileGroup(DownloadModel tempData)
        {
            for(int j = 0; j < this.DownloadedFiles.Count; ++j)  {
                if ((this.DownloadedFiles.ElementAt(j).ID).Equals(tempData.ID))
                {
                    this.DownloadedFiles.RemoveAt(j);
                }
            }
            DownloadedFiles.Insert(0, tempData);
        }
    }
}
