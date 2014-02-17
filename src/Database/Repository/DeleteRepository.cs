using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Repository
{
    public class DeleteRepository
    {
        public void RemovePatient(String patientID)
        {
            try
            {
                executeQuery(patientID, "spr_DeletePatientImages_v001");
                executeQuery(patientID, "spr_DeletePatientImageProperties_v001");
                executeQuery(patientID, "spr_DeletePatientName_v001");
                executeQuery(patientID, "spr_DeletePatient_v001");
            }
            catch (Exception e) { Console.WriteLine(e); }
        }

        private void executeQuery(String patientID, String procedure)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionManager.getConnection))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(procedure, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@patientID", SqlDbType.VarChar).Value = patientID;
                cmd.ExecuteNonQuery();
            }
        }
    }
}
