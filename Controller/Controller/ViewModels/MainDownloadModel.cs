﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Controller.ViewModels
{
    public class MainDownloadModel : INotifyPropertyChanged
    {
        public ObservableCollection<DownloadModel> FilesOrFolders { get; private set; }

        public MainDownloadModel()
        {
            this.FilesOrFolders = new ObservableCollection<DownloadModel>();
        }

        public bool IsDataLoaded
        {
            get;
            private set;
        }
        public void LoadData()   {

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

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
