using Database.Objects;
using DIPS.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Connection
{
    public class DAOLog
    {
        public void create()
        {
            using (SqlConnection conn = new SqlConnection(ConnectionManager.getConnection))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spr_CreateLog_v001", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();

                SqlCommand cmd2 = new SqlCommand("spr_DeleteExcessLog_v001", conn);
                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.ExecuteNonQuery();
            }
            Log.Created = true;
        }

        public void update(int series)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionManager.getConnection))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spr_UpdateModifiedDate_v001", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = series;
                cmd.ExecuteNonQuery();
            }
        }
    }
}
