using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Objects
{
    public static class Log
    {
        private static int _seriesID;
        private static Boolean _logCreated = false;
        private static Boolean _logNeedUpdate = false;
        private static Boolean _firstLaunch = false;
        private static String _processName = "Gamma 0.0";

        public static int Series
        {
            get { return _seriesID; }
            set { _seriesID = value; }
        }

        public static Boolean CodecRegistration
        {
            get { return _firstLaunch; }
            set { _firstLaunch = value; }
        }

        public static Boolean Created
        {
            get { return _logCreated; }
            set { _logCreated = value; }
        }

        public static Boolean NeedUpdate
        {
            get { return _logNeedUpdate; }
            set { _logNeedUpdate = value; }
        }

        public static String ProcessName
        {
            get { return _processName; }
            set { _processName = value; }
        }
        
    }
}
