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

            Char keyword;
            if (DicomInfo.sex.Length == 0) keyword = 'U';
            else keyword = DicomInfo.sex[0];

            using (SqlConnection conn = new SqlConnection(ConnectionManager.getConnection))
            {
                conn.Open();
                SqlCommand command = new SqlCommand("spr_RetrieveNextPatientID_v001", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@keyword", SqlDbType.Char).Value = keyword;
                String Count = command.ExecuteScalar().ToString();
                if (Count.Length < 4) Count = Count.PadLeft(4, '0');

                patientID = keyword + "_" + Count;
            }
            return patientID;
        }
    }
}
