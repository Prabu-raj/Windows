using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Controller.Resources;
using System.Windows;
using Newtonsoft.Json;

namespace Controller.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            this.Items = new ObservableCollection<ItemViewModel>();
        }

        public ObservableCollection<ItemViewModel> Items { get; private set; }

        public string LocalizedSampleProperty
        {
            get
            {
                return AppResources.SampleProperty;
            }
        }

        public bool IsDataLoaded
        {
            get;
            private set;
        }

        public void LoadData()
        {
            String message = String.Empty;

            message = App.client.Receive();
            SystemDetails systemDetails = new SystemDetails();
            try
            {
                systemDetails = JsonConvert.DeserializeObject<SystemDetails>(message);
            }
            catch (JsonReaderException )
            {
                MessageBox.Show("JSonReader Exception");
            }


            for (int i = 0; i < systemDetails.DriveLabel.Length; ++i)
            {
                
                if (systemDetails.DriveLabel[i] != null)
                {
                    this.Items.Add(new ItemViewModel()
                    {
                        ID = i.ToString(),
                        DriveName = systemDetails.DriveName[i],
                        DriveLabel = systemDetails.DriveLabel[i],
                        DriveSizeUsed = systemDetails.DriveSizeUsed[i] + " GB Used",
                        DriveSizeFree = systemDetails.DriveSizeFree[i],
                        ProgressMax = systemDetails.DriveTotalSize[i],
                        ProgressMin = "0",
                        ProgressValue = systemDetails.DriveSizeUsed[i],
                        MachineName = systemDetails.MachineName
                    });
                }
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
    }
}