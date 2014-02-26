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

        public void insertPatient(DicomInfo dicom)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionManager.getConnection))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("spr_InsertPatient_v001", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = dicom.pID;

                    cmd.Parameters.Add("@birthday", SqlDbType.VarChar).Value = dicom.pBday;
                    cmd.Parameters.Add("@age", SqlDbType.VarChar).Value = dicom.age;
                    cmd.Parameters.Add("@sex", SqlDbType.Char, 1).Value = dicom.sex;
                    cmd.Parameters.Add("@series", SqlDbType.Int).Value = 1;

                    dicom.databaseID = (Int32)cmd.ExecuteScalar();
                    Console.WriteLine("Patient Success");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.WriteLine("");
            }
        }

        public void insertName(DicomInfo dicom)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionManager.getConnection))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("spr_InsertName_v001", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = dicom.databaseID;

                    cmd.Parameters.Add("@pName", SqlDbType.VarChar).Value = dicom.patientName;
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

        public void insertImageInfo(DicomInfo dicom)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionManager.getConnection))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("spr_InsertImageProperties_v001", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = dicom.databaseID;

                    cmd.Parameters.Add("@modality", SqlDbType.VarChar).Value = dicom.modality;
                    cmd.Parameters.Add("@imgDateTime", SqlDbType.DateTime).Value = dicom.imgDateTime;
                    cmd.Parameters.Add("@bodyPart", SqlDbType.VarChar).Value = dicom.bodyPart;
                    cmd.Parameters.Add("@studyDesc", SqlDbType.VarChar).Value = dicom.studyDesc;
                    cmd.Parameters.Add("@seriesDesc", SqlDbType.VarChar).Value = dicom.seriesDesc;
                    cmd.Parameters.Add("@sliceThick", SqlDbType.VarChar).Value = dicom.sliceThickness;

                    dicom.seriesID = (Int32)cmd.ExecuteScalar();
                    Console.WriteLine("Images Success");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.WriteLine("");
            }
        }

        public void insertImageFile(DicomInfo dicom ,String filePath)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionManager.getConnection))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("spr_InsertImages_v001", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    readImage image = new readImage();
                    byte[] blob = image.blob(filePath);

                    cmd.Parameters.Add("@imgID", SqlDbType.Int).Value = dicom.seriesID;
                    cmd.Parameters.Add("@imgUID", SqlDbType.VarChar).Value = dicom.imageUID;
                    cmd.Parameters.Add("@imgNum", SqlDbType.VarChar).Value = dicom.imgNumber;
                    if (blob != null) cmd.Parameters.Add("@imgBlob", SqlDbType.VarBinary, blob.Length).Value = blob;
                    
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
