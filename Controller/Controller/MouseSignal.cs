using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
    class MouseSignal
    {
        public const string DRAG = "DRAG";
        public const string SCROLL = "SCROLL";
        public const string TAP = "TAP";
        public const string DOUBLE_TAP = "DOUBLE_TAP";
        public const string HOLD = "HOLD";
        public const string END_SIMULATION = "END";

        private int _xCoordinate;
        private int _yCoordinate;

        private String _action;

        public int XCoordinate
        {
            get
            {
                return _xCoordinate;
            }

            set
            {
                if (value != _xCoordinate)
                {
                    _xCoordinate = value;
                }
            }
        }

        public int YCoordinate
        {

            get
            {
                return _yCoordinate;
            }
            set
            {
                if (value != _yCoordinate)
                {
                    _yCoordinate = value;
                }
            }
        }

        public String Action
        {
            get
            {
                return _action;
            }
            set
            {
                if (value != _action)
                {
                    _action = value;
                }
            }
        }

        public MouseSignal(double x, double y, String action)
        {
            _xCoordinate = (int)x;
            _yCoordinate = (int)y;
            _action = action;
        }

        public MouseSignal(String action)
        {
            _action = action;
        }
    }
}
