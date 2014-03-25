using DIPS.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Dicom
{
    public class PatientID
    {
        public String generate()
        {
            String patientID = null;
            using (SqlConnection conn = new SqlConnection(ConnectionManager.getConnection))
            {
                conn.Open();
                String sMonth = DateTime.Now.ToString("MMM");
                SqlCommand command = new SqlCommand("spr_RetrieveNextPatientID_v001", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@keyword", SqlDbType.VarChar).Value = sMonth;
                String Count = command.ExecuteScalar().ToString();
                if (Count.Length < 3) Count = Count.PadLeft(3, '0');

                patientID = sMonth + "_" + Count;
            }
            return patientID;
        }
    }
}
