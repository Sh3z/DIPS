using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public static class ConnectionManager
    {
        public static void changeDataSource(String newValue)
        {
            DIPSConnection.Default.DataSource = newValue;
            rebuildConnection();
        }

        public static void changeCatalog(String newValue)
        {
            DIPSConnection.Default.Catalog = newValue;
            rebuildConnection();
        }

        public static void changeSecurity(String newValue)
        {
            DIPSConnection.Default.Security = newValue;
            rebuildConnection();
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
                            DIPSConnection.Default.Catalog + "; Integrated Security = " + DIPSConnection.Default.Security;

            DIPSConnection.Default.MasterConnection = "Data Source = " + DIPSConnection.Default.DataSource +
                                "; Initial Catalog = master; Integrated Security = " +
                                DIPSConnection.Default.Security;
            DIPSConnection.Default.Save();
        }
    }
}
