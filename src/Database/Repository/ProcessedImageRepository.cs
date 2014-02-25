using Dicom;
using Dicom.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Repository
{
    public class ProcessedImageRepository
    {
        //extract identifier(UID) from file
        public String getIdentifier(FileStream fs)
        {
            DicomFileFormat dff = new DicomFileFormat();
            dff.Load(fs, DicomReadOptions.Default);
            return dff.Dataset.GetValueString(DicomTags.SOPInstanceUID);
        }

        //Saves the processed image to the database with its input
        public void withIdentifier(String identifier, byte[] blob)
        {
            int series = 0;
            String imgNum = "";

            using (SqlConnection conn = new SqlConnection(ConnectionManager.getConnection))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spr_RetrieveImageInfo_v001", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@identifier", SqlDbType.VarChar).Value = identifier;
                SqlDataReader data = cmd.ExecuteReader();

                data.Read();
                series = data.GetInt32(data.GetOrdinal("seriesID"));
                imgNum = data.GetString(data.GetOrdinal("imageNumber"));

                SqlCommand cmd2 = new SqlCommand("spr_InsertImages_v001", conn);
                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.Parameters.Add("@imgID", SqlDbType.Int).Value = series;
                cmd2.Parameters.Add("@imgUID", SqlDbType.VarChar).Value = identifier;
                cmd2.Parameters.Add("@imgNum", SqlDbType.VarChar).Value = imgNum;
                cmd2.Parameters.Add("@imgBlob", SqlDbType.VarBinary, blob.Length).Value = blob;
                cmd2.Parameters.Add("@process", SqlDbType.Bit).Value = 1;
            }
        }

        //Saves the processed image to the database without input
        public void withoutIdentifier(String identifier, byte[] blob)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionManager.getConnection))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spr_InsertImages_v001", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@imgUID", SqlDbType.VarChar).Value = identifier;
                cmd.Parameters.Add("@imgBlob", SqlDbType.VarBinary, blob.Length).Value = blob;
                cmd.ExecuteNonQuery();
            }
        }
    }
}
