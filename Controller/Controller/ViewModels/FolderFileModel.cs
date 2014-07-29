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
                    ImageUri = "/Assets/Folder.png";
                else
                {
                    setImageURI();
                }
                
             
            }
        }

        private void setImageURI()
        {
            switch (FileExtension)
            {
                case ".acc":
                    ImageUri = Extensions.ACC;
                    break;
                case ".ai":
                    ImageUri = Extensions.AI;
                    break;
                case ".aiff":
                    ImageUri = Extensions.AIFF;
                    break;
                case ".avi":
                    ImageUri = Extensions.AVI;
                    break;
                case ".bmp":
                    ImageUri = Extensions.BMP;
                    break;
                case ".c":
                    ImageUri = Extensions.C;
                    break;
                case ".cpp":
                    ImageUri = Extensions.CPP;
                    break;
                case ".css":
                    ImageUri = Extensions.CSS;
                    break;
                case ".dat":
                    ImageUri = Extensions.DAT;
                    break;
                case ".dmg":
                    ImageUri = Extensions.DMG;
                    break;
                case ".doc":
                    ImageUri = Extensions.DOC;
                    break;
                case ".dotx":
                    ImageUri = Extensions.DOTX;
                    break;
                case ".dwg":
                    ImageUri = Extensions.DWG;
                    break;
                case ".dxf":
                    ImageUri = Extensions.DXF;
                    break;
                case ".eps":
                    ImageUri = Extensions.EPS;
                    break;
                case ".exe":
                    ImageUri = Extensions.EXE;
                    break;
                case ".flv":
                    ImageUri = Extensions.FLV;
                    break;
                case ".gif":
                    ImageUri = Extensions.GIF;
                    break;
                case ".h":
                    ImageUri = Extensions.H;
                    break;
                case ".hpp":
                    ImageUri = Extensions.HPP;
                    break;
                case ".html":
                    ImageUri = Extensions.HTML;
                    break;
                case ".ics":
                    ImageUri = Extensions.ICS;
                    break;
                case ".iso":
                    ImageUri = Extensions.ISO;
                    break;
                case ".java":
                    ImageUri = Extensions.JAVA;
                    break;
                case ".jpg":
                    ImageUri = Extensions.JPG;
                    break;
                case ".js":
                    ImageUri = Extensions.JS;
                    break;
                case ".key":
                    ImageUri = Extensions.KEY;
                    break;
                case ".less":
                    ImageUri = Extensions.LESS;
                    break;
                case ".mid":
                    ImageUri = Extensions.MID;
                    break;
                case ".mp3":
                    ImageUri = Extensions.MP3;
                    break;
                case ".mp4":
                    ImageUri = Extensions.MP4;
                    break;
                case ".mpg":
                    ImageUri = Extensions.MPG;
                    break;
                case ".odf":
                    ImageUri = Extensions.ODF;
                    break;
                case ".ods":
                    ImageUri = Extensions.ODS;
                    break;
                case ".odt":
                    ImageUri = Extensions.ODT;
                    break;
                case ".otp":
                    ImageUri = Extensions.OTP;
                    break;
                case ".ots":
                    ImageUri = Extensions.OTS;
                    break;
                case ".ott":
                    ImageUri = Extensions.OTT;
                    break;
                case ".pdf":
                    ImageUri = Extensions.PDF;
                    break;
                case ".php":
                    ImageUri = Extensions.PHP;
                    break;
                case ".png":
                    ImageUri = Extensions.PNG;
                    break;
                case ".ppt":
                    ImageUri = Extensions.PPT;
                    break;
                case ".psd":
                    ImageUri = Extensions.PSD;
                    break;
                case ".py":
                    ImageUri = Extensions.PY;
                    break;
                case ".qt":
                    ImageUri = Extensions.QT;
                    break;
                case ".rar":
                    ImageUri = Extensions.RAR;
                    break;
                case ".rb":
                    ImageUri = Extensions.RB;
                    break;
                case ".rtf":
                    ImageUri = Extensions.RTF;
                    break;
                case ".sass":
                    ImageUri = Extensions.SASS;
                    break;
                case ".scss":
                    ImageUri = Extensions.SCSS;
                    break;
                case ".sql":
                    ImageUri = Extensions.SQL;
                    break;
                case ".tga":
                    ImageUri = Extensions.TGA;
                    break;
                case ".tgz":
                    ImageUri = Extensions.TGZ;
                    break;
                case ".tiff":
                    ImageUri = Extensions.TIFF;
                    break;
                case ".txt":
                    ImageUri = Extensions.TXT;
                    break;
                case ".wav":
                    ImageUri = Extensions.WAV;
                    break;
                case ".xls":
                    ImageUri = Extensions.XLS;
                    break;
                case ".xlsx":
                    ImageUri = Extensions.XLSX;
                    break;
                case ".xml":
                    ImageUri = Extensions.XML;
                    break;
                case ".yml":
                    ImageUri = Extensions.YML;
                    break;
                case ".zip":
                    ImageUri = Extensions.ZIP;
                    break;
                default :
                    ImageUri = Extensions.PAGE;
                    break;
            }
        }


        private String _fileExtension;
        public String FileExtension
        {

            get
            {
                return _fileExtension;
            }

            set
            {
                if (value != _fileExtension)
                {
                    _fileExtension = value;
                }
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
