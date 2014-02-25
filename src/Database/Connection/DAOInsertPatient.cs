using Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Database
{
    public class DAOInsertPatient
    {

        public void insertPatient()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionManager.getConnection))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("spr_InsertPatient_v001", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = DicomInfo.pID;

                    cmd.Parameters.Add("@birthday", SqlDbType.VarChar).Value = DicomInfo.pBday;
                    cmd.Parameters.Add("@age", SqlDbType.VarChar).Value = DicomInfo.age;
                    cmd.Parameters.Add("@sex", SqlDbType.Char, 1).Value = DicomInfo.sex;

                    cmd.Parameters.Add("@series", SqlDbType.Int).Value = 1;
                    DicomInfo.databaseID = (Int32)cmd.ExecuteScalar();
                    Console.WriteLine("Patient Success");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.WriteLine("");
            }
        }

        public void insertName()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionManager.getConnection))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("spr_InsertName_v001", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = DicomInfo.databaseID;

                    cmd.Parameters.Add("@pName", SqlDbType.VarChar).Value = DicomInfo.patientName;
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Name Success");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.WriteLine("");
            }
        }

        public void insertImageInfo()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionManager.getConnection))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("spr_InsertImageProperties_v001", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = DicomInfo.databaseID;

                    cmd.Parameters.Add("@modality", SqlDbType.VarChar).Value = DicomInfo.modality;
                    cmd.Parameters.Add("@imgDateTime", SqlDbType.DateTime).Value = DicomInfo.imgDateTime;
                    cmd.Parameters.Add("@bodyPart", SqlDbType.VarChar).Value = DicomInfo.bodyPart;
                    cmd.Parameters.Add("@studyDesc", SqlDbType.VarChar).Value = DicomInfo.studyDesc;
                    cmd.Parameters.Add("@seriesDesc", SqlDbType.VarChar).Value = DicomInfo.seriesDesc;
                    cmd.Parameters.Add("@sliceThick", SqlDbType.VarChar).Value = DicomInfo.sliceThickness;

                    DicomInfo.seriesID = (Int32)cmd.ExecuteScalar();
                    Console.WriteLine("Images Success");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.WriteLine("");
            }
        }

        public void insertImageFile()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionManager.getConnection))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("spr_InsertImages_v001", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    readImage image = new readImage();
                    byte[] blob = image.blob();

                    cmd.Parameters.Add("@imgID", SqlDbType.Int).Value = DicomInfo.seriesID;

                    cmd.Parameters.Add("@imgNum", SqlDbType.VarChar).Value = DicomInfo.imgNumber;
                    if (blob != null) cmd.Parameters.Add("@imgBlob", SqlDbType.VarBinary, blob.Length).Value = blob;

                    cmd.Parameters.Add("@process", SqlDbType.Bit).Value = 0;
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Image File Success");
                }
            }
            catch (SqlException e)
            {
                if (e.Number == 2627)
                {
                    Console.WriteLine("image existed");
                }
                else
                {
                    Console.WriteLine(e);
                    Console.WriteLine("");
                }
            }
        }
    }
}
