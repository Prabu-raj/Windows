using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Controller.ViewModels
{
    public class ItemViewModel : INotifyPropertyChanged
    {

        private string _id;

        public string ID
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

        private string _driveName;
       
        public string DriveName
        {
            get
            {
                return _driveName;
            }
            set
            {
                if (value != _driveName)
                {
                    _driveName = value;
                    NotifyPropertyChanged("DriveName");
                }
            }
        }

        private string _driveSizeUsed;
       
        public string DriveSizeUsed
        {
            get
            {
                return _driveSizeUsed;
            }
            set
            {
                if (value != _driveSizeUsed)
                {
                    _driveSizeUsed = value;
                    NotifyPropertyChanged("DriveSizeUsed");
                }
            }
        }

        private string _driveSizeFree;
        
        public string DriveSizeFree
        {
            get
            {
                return _driveSizeFree;
            }
            set
            {
                if (value != _driveSizeFree)
                {
                    _driveSizeFree = value;
                    NotifyPropertyChanged("DriveSizeFree");
                }
            }
        }

        private string _driveLabel;

        public string DriveLabel
        {
            get
            {
                return _driveLabel;
            }
            set
            {
                if (value != _driveLabel)
                {
                    _driveLabel = value;
                    NotifyPropertyChanged("DriveLabel");
                }
            }
        }

        private string _progressMin;

        public string ProgressMin
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

        private string _machineName;

        public string MachineName
        {
            get
            {
                return _machineName;
            }

            set
            {
                if (value != _machineName)
                {
                    _machineName = value;
                    NotifyPropertyChanged("Machine Name");
                }
            }
        }

        private string _progressMax;

        public string ProgressMax
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

        private string _progressValue;

        public string ProgressValue
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