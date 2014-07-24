using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Controller.ViewModels
{
   public class MainFolderOrFileModel : INotifyPropertyChanged
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
                            fileOrFolderName = folderOrFileDetails.FolderOrFileName[i]  + folderOrFileDetails.FileExtension[i];
                        }
                        else if (folderOrFileDetails.FolderOrFileName[i] != String.Empty) {
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
                           // FolderOrFilePath = replace(folderOrFileDetails.FolderOrFilePath[i]),
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
            
            this.IsDataLoaded = true;
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

       /*private String replace(String replacementString)
       {
           return replacementString.Replace('?', '\\');
       } */
    }
}
