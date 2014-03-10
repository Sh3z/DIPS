using Database;
using Database.Dicom;
using Database.Objects;
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

        public void patientExist(DicomInfo dicom)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionManager.getConnection))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spr_CheckStudyUIDExist_v001", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@studyUID", SqlDbType.VarChar).Value = dicom.studyUID;
                SqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.Read())
                {
                    dicom.patientExist = true;
                    dicom.databaseID = dataReader.GetInt32(0);
                }
                dataReader.Close();
            }
        }

        public void seriesExist(DicomInfo dicom)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionManager.getConnection))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spr_CheckSeriesUIDExist_v001", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@seriesUID", SqlDbType.VarChar).Value = dicom.seriesUID;
                SqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.Read())
                {
                    dicom.sameSeries = true;
                    dicom.seriesID = dataReader.GetInt32(0);
                    Log.NeedUpdate = true;
                }
                dataReader.Close();
            }
        }

        public void patientExistBackup(DicomInfo dicom)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionManager.getConnection))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spr_CheckPatientExist_v001", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@birthdate", SqlDbType.VarChar).Value = dicom.pBday;
                cmd.Parameters.Add("@age", SqlDbType.VarChar).Value = dicom.age;
                cmd.Parameters.Add("@sex", SqlDbType.Char).Value = dicom.sex;

                SqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    

                    if (allMatched(dicom, dataReader))
                    {
                        dicom.patientExist = true;
                        dicom.databaseID = dataReader.GetInt32(dataReader.GetOrdinal("Patient ID"));
                        dicom.sameSeries = true;
                        Log.NeedUpdate = true;
                        dicom.seriesID = dataReader.GetInt32(dataReader.GetOrdinal("Series ID"));
                        break;
                    }
                }
                dataReader.Close();
            }
        }

        private Boolean allMatched(DicomInfo dicom, SqlDataReader dataReader)
        {
            if (!dicom.modality.Equals(dataReader.GetString(dataReader.GetOrdinal("Modality")))) return false;
            if (!dicom.bodyPart.Equals(dataReader.GetString(dataReader.GetOrdinal("Body Parts")))) return false;
            if (!dicom.studyDesc.Equals(dataReader.GetString(dataReader.GetOrdinal("Study Description")))) return false;
            if (!dicom.seriesDesc.Equals(dataReader.GetString(dataReader.GetOrdinal("Series Description")))) return false;
            return true;
        }

        public void updatePatientSeries(DicomInfo dicom)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionManager.getConnection))
            {
                conn.Open();
                SqlCommand cmd3 = new SqlCommand("spr_UpdateSeriesAvailable_v001", conn);
                cmd3.CommandType = CommandType.StoredProcedure;
                cmd3.Parameters.Add("@databaseID", SqlDbType.Int).Value = dicom.databaseID;
                cmd3.ExecuteNonQuery();
            }
        }

        public void retrieveImageNumber(DicomInfo dicom)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionManager.getConnection))
            {
                conn.Open();
                SqlCommand cmd2 = new SqlCommand("spr_RetrieveImageNumber_v001", conn);
                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.Parameters.Add("@databaseID", SqlDbType.VarChar).Value = dicom.databaseID;
                cmd2.Parameters.Add("@classID", SqlDbType.VarChar).Value = dicom.seriesID;
                cmd2.Parameters.Add("@number", SqlDbType.VarChar).Value = dicom.imgNumber;
                int count = (Int32)cmd2.ExecuteScalar();

                if(count!=0) dicom.imageExist = true;
            }
        }
    }
}
