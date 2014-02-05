using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DIPS.Database.Objects;
using System.Data.SqlClient;
using System.Data;
using System.Collections.ObjectModel;
using DIPS.Database;
using Database.Objects;

namespace Database
{
    public static class ImageRepository
    {
        public static List<ImageDataset> generateTreeView()
        {
            Technique t = new Technique();
            List<ImageDataset> allDatasetsActive = null;

            try
            {
                SqlConnection con = new SqlConnection(staticVariables.sql);
                con.Open();
                SqlCommand cmd = new SqlCommand("spr_TreeView_v001", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader data = cmd.ExecuteReader();

                allDatasetsActive = DatabaseToList(data);
                
                data.Close();
                con.Close();
            }
            catch (Exception e) { }

            return allDatasetsActive;
        }

        public static List<ImageDataset> generateCustomTreeView(Filter filter)
        {
            Technique t = new Technique();
            List<ImageDataset> allDatasetsActive = null;
            DateTime invalidDate = new DateTime();
            invalidDate = DateTime.Parse("01/01/0001 00:00:00");

            int dateCompareResultFrom = DateTime.Compare(invalidDate,filter.AcquisitionDateFrom);
            int dateCompareResultTo = DateTime.Compare(invalidDate,filter.AcquisitionDateTo);

            try
            {
                SqlConnection con = new SqlConnection(staticVariables.sql);
                con.Open();
                SqlCommand cmd = new SqlCommand("spr_CustomList_v001", con);
                cmd.CommandType = CommandType.StoredProcedure;
                if(filter.PatientID!=null) 
                    cmd.Parameters.Add("@IDEquals", SqlDbType.VarChar).Value = filter.PatientID;
                if (filter.AcquisitionDateFrom != null && dateCompareResultFrom != 0)
                    cmd.Parameters.Add("@AcquireBetweenFrom", SqlDbType.Date).Value = filter.AcquisitionDateFrom;
                if (filter.AcquisitionDateTo != null && dateCompareResultTo != 0) 
                    cmd.Parameters.Add("@AcquireBetweenTo", SqlDbType.Date).Value = filter.AcquisitionDateTo;
                cmd.Parameters.Add("@Sex", SqlDbType.VarChar).Value = filter.Gender;
                SqlDataReader data = cmd.ExecuteReader();

                allDatasetsActive = DatabaseToList(data);

                data.Close();
                con.Close();
            }
            catch (Exception e) { }

            return allDatasetsActive;
        }

        private static List<ImageDataset> DatabaseToList(SqlDataReader data)
        {
            List<ImageDataset> dataSets = null;
            string currentID;
            string prevID = "null";

            dataSets = new List<ImageDataset>();
            ObservableCollection<PatientImage> imageCollectionDS = null;
            ImageDataset imgDS = null;
            PatientImage img = null;
            Patient patient = null;

            while (data.Read())
            {
                img = new PatientImage();
                currentID = data.GetString(data.GetOrdinal("Patient ID"));

                if (!currentID.Equals(prevID))
                {
                    imageCollectionDS = new ObservableCollection<PatientImage>();
                    imgDS = new ImageDataset(currentID, imageCollectionDS);
                    dataSets.Add(imgDS);
                }

                img.patientID = currentID;
                img.imgID = data.GetInt32(data.GetOrdinal("File ID"));
                imageCollectionDS.Add(img);
                prevID = currentID;
            }

            return dataSets;
        }


        public static List<String> retrieveImageProperties(String fileID)
        {
            List<String> properties = new List<String>();
            SqlConnection con = new SqlConnection(staticVariables.sql);
            con.Open();
            SqlCommand cmd = new SqlCommand("spr_SelectProperties_v001", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@fileID", SqlDbType.VarChar).Value = fileID;
            SqlDataReader dataReader = cmd.ExecuteReader();
            while (dataReader.Read())
            {
                properties.Add("Birthdate : " + dataReader.GetString(dataReader.GetOrdinal("Birthdate")));
                properties.Add("Age : " + dataReader.GetString(dataReader.GetOrdinal("Age")));
                properties.Add("Sex : " + dataReader.GetString(dataReader.GetOrdinal("Sex")));
                properties.Add("Image Date Time : " + dataReader.GetDateTime(dataReader.GetOrdinal("Image Date Time")).ToString());
                properties.Add("Body Part : " + dataReader.GetString(dataReader.GetOrdinal("Body Part")));
                properties.Add("Study Description : " + dataReader.GetString(dataReader.GetOrdinal("Study Description")));
                properties.Add("Series Description : " + dataReader.GetString(dataReader.GetOrdinal("Series Description")));
                properties.Add("Slice Thickness : " + dataReader.GetString(dataReader.GetOrdinal("Slice Thickness")));

                //foreach(String s in properties) System.Diagnostics.Debug.WriteLine("hehe " + s);
                break;
            }
            dataReader.Close();
            con.Close();
            return properties;
        }
    }
}
