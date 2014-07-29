using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller.ViewModels
{
    public class DownloadModel : INotifyPropertyChanged
    {

        private String _id;

        public String ID
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

        private String _progressMin;

        public String ProgressMin
        {
            get
            {
                return _progressMin;
            }
            set
            {
                if (value != _progressMin)
                {
                    _progressMin = value;
                    NotifyPropertyChanged("ProgressMin");
                }
            }
        }

        private String _progressMax;

        public String ProgressMax
        {
            get
            {
                return _progressMax;
            }
            set
            {
                if (value != _progressMax)
                {
                    _progressMax = value;
                    NotifyPropertyChanged("ProgressMax");
                }
            }
        }

        private String _progressValue;

        public String ProgressValue
        {
            get
            {
                return _progressValue;
            }

            set
            {
                if (value != _progressValue)
                {
                    _progressValue = value;
                    NotifyPropertyChanged("ProgressValue");
                }
            }
        }

        private String _fileName;

        public String FileName
        {
            get
            {
                return _fileName;
            }
            set
            {
                if (value != _fileName)
                {
                    _fileName = value;
                    NotifyPropertyChanged("FileName");
                }
            }
        }

        private String _visibilityMode;

        public String VisibilityMode
        {
            get
            {
                return _visibilityMode;
            }

            private set
            {
                if (value != _visibilityMode)
                {
                    _visibilityMode = value;
                    NotifyPropertyChanged("VisibilityMode");
                }
            }
        }

        private bool _visibility;

        public bool Visibility
        {
            private get
            {
                return _visibility;
            }
            set
            {
                if (value)
                {
                    _visibilityMode = "Visible";
                    NotifyPropertyChanged("Visibility");
                }
                else
                {
                    _visibilityMode = "Collapsed";
                    NotifyPropertyChanged("Visibility");
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
