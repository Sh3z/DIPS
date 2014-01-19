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
            else
            {
                getImageSeries();
                insertImageFile();
            }

            con.Close();

            staticVariables.imageSeries = 1;
            staticVariables.patientExist = false;
            staticVariables.sameSeries = false;
        }

        void insertPatient()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("spr_InsertPatient_v001", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = staticVariables.pID;
                cmd.Parameters.Add("@birthday", SqlDbType.VarChar).Value = staticVariables.pBday;
                cmd.Parameters.Add("@age", SqlDbType.VarChar).Value = staticVariables.age;
                cmd.Parameters.Add("@sex", SqlDbType.Char, 1).Value = staticVariables.sex;
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
                cmd.Parameters.Add("@fname", SqlDbType.VarChar).Value = staticVariables.firstName;
                cmd.Parameters.Add("@lname", SqlDbType.VarChar).Value = staticVariables.lastName;
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
                String imageID = "IMG" + staticVariables.databaseID + "(" + staticVariables.imageSeries + ")";

                SqlCommand cmd = new SqlCommand("spr_InsertImageProperties_v001", con);
                cmd.CommandType = CommandType.StoredProcedure; 
                cmd.Parameters.Add("@imgID", SqlDbType.VarChar).Value = imageID;
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = staticVariables.databaseID;
                cmd.Parameters.Add("@imgDateTime", SqlDbType.DateTime).Value = staticVariables.imgDateTime;
                cmd.Parameters.Add("@bodyPart", SqlDbType.VarChar).Value = staticVariables.bodyPart;
                cmd.Parameters.Add("@studyDesc", SqlDbType.VarChar).Value = staticVariables.studyDesc;
                cmd.Parameters.Add("@seriesDesc", SqlDbType.VarChar).Value = staticVariables.seriesDesc;
                cmd.Parameters.Add("@sliceThick", SqlDbType.VarChar).Value = staticVariables.sliceThickness;
                cmd.ExecuteNonQuery();
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

                String imgID = "IMG" + staticVariables.databaseID + "(" + staticVariables.imageSeries + ")";
                saveImage image = new saveImage();
                byte[] blob = image.blob();

                cmd.Parameters.Add("@fID", SqlDbType.VarChar).Value = imgID + "_" + staticVariables.imgNumber;
                cmd.Parameters.Add("@imgID", SqlDbType.VarChar).Value = imgID;
                cmd.Parameters.Add("@imgBlob", SqlDbType.VarBinary, blob.Length).Value = blob;
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

        void getImageSeries()
        {
            SqlCommand cmd = new SqlCommand("spr_RetrieveSeriesAvailable_v001", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@databaseID", SqlDbType.Int).Value = staticVariables.databaseID;
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                staticVariables.imageSeries = reader.GetInt32(0);
                break;
            }
            reader.Close();
        }
    }
}
