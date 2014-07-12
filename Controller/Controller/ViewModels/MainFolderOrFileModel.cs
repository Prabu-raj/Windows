using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Controller.ViewModels
{
   public class MainFolderOrFileModel
    {
        public MainFolderOrFileModel()
        {
            this.FilesOrFolders = new ObservableCollection<FolderFileModel>();
        }
        public ObservableCollection<FolderFileModel> FilesOrFolders { get; private set; }

        public bool IsDataLoaded
        {
            get;
            private set;
        }

        public void LoadData()
        {
            String data = String.Empty;

            data = App.client.Receive();

            FolderOrFileDetails folderOrFileDetails = new FolderOrFileDetails();
            try
            {
                folderOrFileDetails = JsonConvert.DeserializeObject<FolderOrFileDetails>(data);
            }
            catch (JsonReaderException )
            {
                MessageBox.Show("JSonReader Exception");
            }

            for (int i = 0; i < folderOrFileDetails.FolderOrFileName.Length; ++i)
            {
                if (folderOrFileDetails.FolderOrFileName != null)
                {
                    this.FilesOrFolders.Add(new FolderFileModel()
                    {
                        ID = i,
                        FileExtension = folderOrFileDetails.FileExtension[i],
                        FolderOrFileName = folderOrFileDetails.FolderOrFileName[i],
                        FolderOrFilePath = folderOrFileDetails.FolderOrFilePath[i],
                        IsFolder = folderOrFileDetails.IsFolder[i]
                    });
                }
            }

            this.IsDataLoaded = true;
        }
    }
}
