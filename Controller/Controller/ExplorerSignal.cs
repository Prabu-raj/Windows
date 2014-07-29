using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
    public class ExplorerSignal
    {

        public const String GET_FILES = "GET_FILES";
        public const String DOWNLOAD_FILE = "DOWNLOAD_FILE";
        public const string END_EXPLORER = "END";
        public const string OPEN_FILE = "OPEN_FILE";
        public ExplorerSignal(String action, String filePath) {
            Action = action;
            FilePath = filePath;
        }

        private String _action;

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

        private String _filePath;

        public String FilePath
        {
            get
            {
                return _filePath;
            }

            set
            {
                if (value != _filePath)
                {
                    _filePath = value;
                }
            }
        }
    }
}
