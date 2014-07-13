using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
    class FolderOrFileDetails
    {
        public const string END_EXPLORER = "END";
       
        private String[] _folderOrFileName ;
        public String[] FolderOrFileName
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
                }
            }
        }

        private String[] _fileExtension;
        public String[] FileExtension
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

        private String[] _folderOrFilePath;
        public String[] FolderOrFilePath
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

                }
            }
        }

        private bool[] _isFolder;
        public bool[] IsFolder
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
                }
            }
        }
    }
}
