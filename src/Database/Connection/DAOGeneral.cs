using Database;
using Database.Dicom;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Database
{
    public class DAOGeneral
    {

        public void createLog()
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
            DicomInfo.logCreated = true;
        }

        public void patientExist()
        {
            DicomInfo.patientExist = false;
            using (SqlConnection conn = new SqlConnection(ConnectionManager.getConnection))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spr_CheckPatientExist_v001", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@birthdate", SqlDbType.VarChar).Value = DicomInfo.pBday;
                cmd.Parameters.Add("@age", SqlDbType.VarChar).Value = DicomInfo.age;
                cmd.Parameters.Add("@sex", SqlDbType.Char).Value = DicomInfo.sex;
                cmd.Parameters.Add("@pName", SqlDbType.VarChar).Value = DicomInfo.patientName;

                SqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    DicomInfo.patientExist = true;

                    if (allMatched(dataReader))
                    {
                        DicomInfo.sameSeries = true;
                        DicomInfo.databaseID = dataReader.GetInt32(dataReader.GetOrdinal("Patient ID"));
                        DicomInfo.seriesID = dataReader.GetInt32(dataReader.GetOrdinal("Series ID"));
                        break;
                    }
                }
                dataReader.Close();
            }
        }

        private Boolean allMatched(SqlDataReader dataReader)
        {
            if (!DicomInfo.modality.Equals(dataReader.GetString(dataReader.GetOrdinal("Modality")))) return false;
            if (!DicomInfo.bodyPart.Equals(dataReader.GetString(dataReader.GetOrdinal("Body Parts")))) return false;
            if (!DicomInfo.studyDesc.Equals(dataReader.GetString(dataReader.GetOrdinal("Study Description")))) return false;
            if (!DicomInfo.seriesDesc.Equals(dataReader.GetString(dataReader.GetOrdinal("Series Description")))) return false;
            return true;
        }

        public void updatePatientSeries()
        {
            using (SqlConnection conn = new SqlConnection(ConnectionManager.getConnection))
            {
                conn.Open();
                SqlCommand cmd3 = new SqlCommand("spr_UpdateSeriesAvailable_v001", conn);
                cmd3.CommandType = CommandType.StoredProcedure;
                cmd3.Parameters.Add("@databaseID", SqlDbType.Int).Value = DicomInfo.databaseID;
                cmd3.ExecuteNonQuery();
            }
        }

        public void retrieveImageNumber()
        {
            using (SqlConnection conn = new SqlConnection(ConnectionManager.getConnection))
            {
                conn.Open();
                SqlCommand cmd2 = new SqlCommand("spr_RetrieveImageNumber_v001", conn);
                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.Parameters.Add("@databaseID", SqlDbType.VarChar).Value = DicomInfo.databaseID;
                cmd2.Parameters.Add("@classID", SqlDbType.VarChar).Value = DicomInfo.seriesID;
                cmd2.Parameters.Add("@number", SqlDbType.VarChar).Value = DicomInfo.imgNumber;
                int count = (Int32)cmd2.ExecuteScalar();

                if(count!=0) DicomInfo.imageExist = true;
            }
        }
    }
}
