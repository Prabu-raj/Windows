using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
    public class SystemDetails
    {
        //This gives the Width and the height of the desktop or laptop screen.
        private int _deskWidth;
        private int _deskHeight;

        //This gives details about the disk.
        private String[] _driveLabel;

        private String[] _driveSizeFree;
        private String[] _driveTotalSize;
        private String[] _driveSizeUsed;

        //Environment Details
        private String _machineName;
        private String _osVersion;

        public String[] DriveSizeUsed
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
                }
            }
        }
        public int DeskWidth
        {
            get
            {
                return _deskWidth;
            }
            set
            {
                if (value != _deskWidth)
                {
                    _deskWidth = value;
                }
            }
        }

        public int DeskHeight
        {
            get
            {
                return _deskHeight;
            }
            set
            {
                if (value != _deskHeight)
                {
                    _deskHeight = value;
                }
            }
        }

        public String[] DriveLabel
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
                }
            }
        }

        public String[] DriveSizeFree
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
                }
            }
        }

        public String[] DriveTotalSize
        {
            get
            {
                return _driveTotalSize;
            }
            set
            {
                if (value != _driveTotalSize)
                {
                    _driveTotalSize = value;
                }
            }
        }

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
                }
            }
        }
        public String OSVersion
        {
            get
            {
                return _osVersion;
            }

            set
            {
                if (value != _osVersion)
                {
                    _osVersion = value;
                }
            }
        }

    }
}
