using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public static class ConnectionManager
    {

        public static String DataSource
        {
            get { return DIPSConnection.Default.DataSource; }
            set 
            { 
                DIPSConnection.Default.DataSource = value;
                rebuildConnection(); 
            }
        }

        public static String Catalog
        {
            get { return DIPSConnection.Default.Catalog; }
            set 
            { 
                DIPSConnection.Default.Catalog = value;
                rebuildConnection(); 
            }
        }

        public static String Security
        {
            get { return DIPSConnection.Default.Security; }
            set
            {
                DIPSConnection.Default.Security = value;
                rebuildConnection();
            }
        }

        public static String Extra
        {
            get { return DIPSConnection.Default.Extra; }
            set
            {
                DIPSConnection.Default.Extra = value;
                rebuildConnection();
            }
        }

        public static String getConnection
        {
            get { return DIPSConnection.Default.Connection; }
        }

        public static String getMasterConnection
        {
            get { return DIPSConnection.Default.MasterConnection; }
        }

        private static void rebuildConnection()
        {
            DIPSConnection.Default.Connection = "Data Source = " + DIPSConnection.Default.DataSource + "; Initial Catalog = " +
                            DIPSConnection.Default.Catalog + "; Integrated Security = " + DIPSConnection.Default.Security
                            + "; " + DIPSConnection.Default.Extra;

            DIPSConnection.Default.MasterConnection = "Data Source = " + DIPSConnection.Default.DataSource +
                                "; Initial Catalog = master; Integrated Security = " +
                                DIPSConnection.Default.Security + "; " + DIPSConnection.Default.Extra;
            DIPSConnection.Default.Save();
        }
    }
}
