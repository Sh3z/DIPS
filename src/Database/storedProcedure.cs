using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Database
{
    public class storedProcedure
    {
        SqlConnection con = new SqlConnection(staticVariables.sql);

        public void insert()
        {
            con.Open();

            if (staticVariables.patientExist == false)
            {
                insertPatient();
                insertName();
                insertImageInfo();
                insertImageFile();
            }
            else if (staticVariables.sameSeries == false)
            {
                insertImageInfo();
                insertImageFile();
            }
            else if (staticVariables.imageExist == false)
            {
                insertImageFile();
            }

            con.Close();

            staticVariables.seriesID = 0;
            staticVariables.patientExist = false;
            staticVariables.imageExist = false;
            staticVariables.sameSeries = false;
        }

        void insertPatient()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("spr_InsertPatient_v001", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = staticVariables.pID;

                if (staticVariables.pBday == "") cmd.Parameters.Add("@birthday", SqlDbType.VarChar).Value = DBNull.Value;
                else cmd.Parameters.Add("@birthday", SqlDbType.VarChar).Value = staticVariables.pBday;

                if (staticVariables.age == "") cmd.Parameters.Add("@age", SqlDbType.VarChar).Value = DBNull.Value;
                else cmd.Parameters.Add("@age", SqlDbType.VarChar).Value = staticVariables.age;

                if (staticVariables.sex == "") cmd.Parameters.Add("@sex", SqlDbType.Char, 1).Value = DBNull.Value;
                else cmd.Parameters.Add("@sex", SqlDbType.Char, 1).Value = staticVariables.sex;

                cmd.Parameters.Add("@series", SqlDbType.Int).Value = 1;
                staticVariables.databaseID = (Int32)cmd.ExecuteScalar();
                Console.WriteLine("Patient Success");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.WriteLine("");
            }
        }

        void insertName()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("spr_InsertName_v001", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = staticVariables.databaseID;

                if (staticVariables.patientName == "") cmd.Parameters.Add("@pName", SqlDbType.VarChar).Value = DBNull.Value;
                else cmd.Parameters.Add("@pName", SqlDbType.VarChar).Value = staticVariables.patientName;
                cmd.ExecuteNonQuery();
                Console.WriteLine("Name Success");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.WriteLine("");
            }
        }

        void insertImageInfo()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("spr_InsertImageProperties_v001", con);
                cmd.CommandType = CommandType.StoredProcedure; 

                cmd.Parameters.Add("@id", SqlDbType.Int).Value = staticVariables.databaseID;

                if (staticVariables.modality == "") cmd.Parameters.Add("@modality", SqlDbType.VarChar).Value = DBNull.Value;
                else cmd.Parameters.Add("@modality", SqlDbType.VarChar).Value = staticVariables.modality;

                if (staticVariables.imgDateTime == "") cmd.Parameters.Add("@imgDateTime", SqlDbType.DateTime).Value = DBNull.Value;
                else cmd.Parameters.Add("@imgDateTime", SqlDbType.DateTime).Value = staticVariables.imgDateTime;

                if (staticVariables.bodyPart == "") cmd.Parameters.Add("@bodyPart", SqlDbType.VarChar).Value = DBNull.Value;
                else cmd.Parameters.Add("@bodyPart", SqlDbType.VarChar).Value = staticVariables.bodyPart;

                if (staticVariables.studyDesc == "") cmd.Parameters.Add("@studyDesc", SqlDbType.VarChar).Value = DBNull.Value;
                else cmd.Parameters.Add("@studyDesc", SqlDbType.VarChar).Value = staticVariables.studyDesc;

                if (staticVariables.seriesDesc == "") cmd.Parameters.Add("@seriesDesc", SqlDbType.VarChar).Value = DBNull.Value;
                else cmd.Parameters.Add("@seriesDesc", SqlDbType.VarChar).Value = staticVariables.seriesDesc;

                if (staticVariables.sliceThickness == "") cmd.Parameters.Add("@sliceThick", SqlDbType.VarChar).Value = DBNull.Value;
                else cmd.Parameters.Add("@sliceThick", SqlDbType.VarChar).Value = staticVariables.sliceThickness;

                staticVariables.seriesID = (Int32)cmd.ExecuteScalar();
                Console.WriteLine("Images Success");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.WriteLine("");
            }
        }

        void insertImageFile()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("spr_InsertImages_v001", con);
                cmd.CommandType = CommandType.StoredProcedure;

                saveImage image = new saveImage();
                byte[] blob = image.blob();

                cmd.Parameters.Add("@imgID", SqlDbType.VarChar).Value = staticVariables.seriesID;

                if (staticVariables.imgNumber == "") cmd.Parameters.Add("@imgNum", SqlDbType.VarChar).Value = DBNull.Value;
                else cmd.Parameters.Add("@imgNum", SqlDbType.VarChar).Value = staticVariables.imgNumber;

                if (blob == null) cmd.Parameters.Add("@imgBlob", SqlDbType.VarBinary, blob.Length).Value = DBNull.Value;
                else cmd.Parameters.Add("@imgBlob", SqlDbType.VarBinary, blob.Length).Value = blob;

                cmd.Parameters.Add("@process", SqlDbType.Bit).Value = 0;
                cmd.ExecuteNonQuery();
                Console.WriteLine("Image File Success");
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
