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

        private String _driveName;
       
        public String DriveName
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

        private String _driveSizeUsed;

        public String DriveSizeUsed
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

        private String _driveSizeFree;

        public String DriveSizeFree
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

        private String _driveLabel;

        public String DriveLabel
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

        private String _machineName;

        public String MachineName
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