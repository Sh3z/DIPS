using Database.Objects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Repository
{
    public class SelectedImageRepository
    {

        public byte[] getUnprocessedImage(String fileID)
        {
            byte[] image = null;
            using (SqlConnection conn = new SqlConnection(ConnectionManager.getConnection))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spr_RetrieveImage_v001", conn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@fID", SqlDbType.VarChar).Value = fileID;
                SqlDataReader dataReader = cmd.ExecuteReader();

                dataReader.Read();
                image = (byte[])dataReader.GetValue(dataReader.GetOrdinal("imageBlob"));
                SelectedImage.UID = dataReader.GetString(dataReader.GetOrdinal("imageUID"));
                dataReader.Close();
            }
            return image;
        }

        public byte[] getProcessedImage(String fileID,String algorithm)
        {
            byte[] processed = null;
            using (SqlConnection conn = new SqlConnection(ConnectionManager.getConnection))
            {
                conn.Open();
                SqlCommand cmd2 = new SqlCommand("spr_RetrieveProcessedImage_v001", conn);
                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.Parameters.Add("@fileID", SqlDbType.Int).Value = Int32.Parse(fileID);
                cmd2.Parameters.Add("@processMethod", SqlDbType.VarChar).Value = algorithm;
                processed = (byte[]) cmd2.ExecuteScalar();
            }
            return processed;
        }

        public void updateAlgorithmList(String imageUID)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionManager.getConnection))
            {
                conn.Open();
                SqlCommand cmd2 = new SqlCommand("spr_RetreiveTechniqueUsed_v001", conn);
                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.Parameters.Add("@imageUID", SqlDbType.VarChar).Value = imageUID;
                SqlDataReader data2 = cmd2.ExecuteReader();

                if (data2.HasRows) SelectedImage.AlgorithmCollection = new ObservableCollection<Algorithm>();
                else SelectedImage.AlgorithmCollection = null;

                while (data2.Read())
                {
                    Algorithm algo = new Algorithm();
                    algo.AlgorithmName = data2.GetString(data2.GetOrdinal("Algorithm"));
                    SelectedImage.AlgorithmCollection.Add(algo);
                }
                data2.Close();
            }
        }
    }
}
