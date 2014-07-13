using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller.ViewModels
{
    public class FolderFileModel : INotifyPropertyChanged
    {

        
        private int _id;
        public int ID
        {
            get
            {
                return _id;
            }

            set
            {
                if (value != _id)
                {
                    _id = value;
                    NotifyPropertyChanged("ID");
                }
            }
        }

        private String _folderOrFileName;
        public String FolderOrFileName
        {
            get
            {
                return _folderOrFileName;
            }
            set
            {
                if (value != _folderOrFileName)
                {
                    _folderOrFileName = value;
                    NotifyPropertyChanged("FolderOrFileName");
                }
            }
        }

       /* private String _folderOrFilePath;
        public String FolderOrFilePath
        {
            get
            {
                return _folderOrFilePath;
            }

            set
            {
                if (value != _folderOrFilePath)
                {
                    _folderOrFilePath = value;
                    NotifyPropertyChanged("FolderOrFilePath");
                }
            }
        }
        */
        private string _imageUri;

        public string ImageUri {
            get
            {
                return _imageUri;
            }
            set
            {
                if (value != _imageUri)
                {
                    _imageUri = value;
                    NotifyPropertyChanged("ImageUri");
                }
            }
            
        }
        private bool _isFolder;
        public bool IsFolder
        {
            get
            {
                return _isFolder;
            }

            set
            {
                if (value != _isFolder)
                {
                    _isFolder = value;
                    NotifyPropertyChanged("IsFolder");
                }

                if(IsFolder)
                    ImageUri = "/Assets/DiskImage.png";
                else
                    ImageUri = "/Assets/File.png";
             
            }
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
