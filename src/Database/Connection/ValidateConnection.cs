using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Database.Connection
{
    public static class ValidateConnection
    {
        public static void validateConnection()
        {
            Thread normal = new Thread(new ThreadStart(openConn));
            Thread master = new Thread(new ThreadStart(openMasterConn));
            normal.Start();
            master.Start();
            normal.Join();
        }

        private static void openConn()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionManager.getConnection))
                {
                    conn.Open();
                    ConnectionManager.ValidConnection = true;
                }
            }
            catch
            {
                ConnectionManager.ValidConnection = false;
            }
        }

        private static void openMasterConn()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionManager.getMasterConnection))
                {
                    conn.Open();
                    ConnectionManager.ValidMasterConnection = true;
                }
            }
            catch
            {
                ConnectionManager.ValidMasterConnection = false;
            }
        }
    }
}
