using DIPS.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Repository
{
    public class BackupRepository
    {
        public void CreateBackup(String filePath)
        {
            if (ConnectionManager.ValidConnection == true)
            {
                using (SqlConnection conn = new SqlConnection(ConnectionManager.getConnection))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("spr_CreateBackup_v001", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@filePath", SqlDbType.VarChar).Value = filePath;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void RestoreBackup(String filePath)
        {
            if (ConnectionManager.ValidMasterConnection == true)
            {
                using (SqlConnection conn = new SqlConnection(ConnectionManager.getMasterConnection))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("spr_RestoreBackup_v001", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@filePath", SqlDbType.VarChar).Value = filePath;
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
