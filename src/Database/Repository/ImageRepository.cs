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
using Database.DicomHelper;

namespace Database
{
    public class ImageRepository
    {
        public ObservableCollection<Patient> generateTreeView(Boolean showName)
        {
            Technique t = new Technique();
            ObservableCollection<Patient> allDatasetsActive = new ObservableCollection<Patient>();

            if (ConnectionManager.ValidConnection == true)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(ConnectionManager.getConnection))
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand("spr_TreeView_v001", conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlDataReader data = cmd.ExecuteReader();

                        allDatasetsActive = DatabaseToList(data, showName);

                        data.Close();
                    }
                }
                catch (Exception e) { Console.WriteLine(e); }
            }

            return allDatasetsActive;
        }

        public ObservableCollection<Patient> generateCustomTreeView(Filter filter, Boolean showName)
        {
            Technique t = new Technique();
            ObservableCollection<Patient> allDatasetsActive = null;
            int dateCompareResultFrom = 0;
            int dateCompareResultTo = 0;

            DateTime invalidDate = new DateTime();
            invalidDate = DateTime.Parse("01/01/0001 00:00:00");
            
            if (filter != null)
            {
                dateCompareResultFrom = DateTime.Compare(invalidDate, filter.AcquisitionDateFrom);
                dateCompareResultTo = DateTime.Compare(invalidDate, filter.AcquisitionDateTo);
            }

            if (ConnectionManager.ValidConnection == true)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(ConnectionManager.getConnection))
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand("spr_CustomList_v001", conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (!String.IsNullOrEmpty(filter.PatientID))
                            cmd.Parameters.Add("@IDContains", SqlDbType.VarChar).Value = filter.PatientID;
                        if (!String.IsNullOrEmpty(filter.Modality))
                            cmd.Parameters.Add("@modality", SqlDbType.VarChar).Value = filter.Modality;
                        if (!String.IsNullOrEmpty(filter.Gender))
                            cmd.Parameters.Add("@Sex", SqlDbType.VarChar).Value = filter.Gender;
                        if (filter.AcquisitionDateFrom != null && dateCompareResultFrom != 0)
                            cmd.Parameters.Add("@AcquireBetweenFrom", SqlDbType.Date).Value = filter.AcquisitionDateFrom;
                        if (filter.AcquisitionDateTo != null && dateCompareResultTo != 0)
                            cmd.Parameters.Add("@AcquireBetweenTo", SqlDbType.Date).Value = filter.AcquisitionDateTo;

                        int batch = 0;
                        Boolean batchNumerical = int.TryParse(filter.Batch, out batch);
                        if (batchNumerical) cmd.Parameters.Add("@Batch", SqlDbType.Int).Value = batch;

                        SqlDataReader data = cmd.ExecuteReader();
                        allDatasetsActive = DatabaseToList(data, showName);
                        data.Close();
                    }
                }
                catch (Exception e) { Console.WriteLine(e); }
            }
            return allDatasetsActive;

        }

        private ObservableCollection<Patient> DatabaseToList(SqlDataReader data, Boolean showName)
        {
            ObservableCollection<Patient> patientList = null;
            ObservableCollection<ImageDataset> dataSets = null;
            ObservableCollection<PatientImage> imageCollectionDS = null;
            ImageDataset imgDS = null;
            PatientImage img = null;
            Patient patient = null;

            Boolean patientExist = false;
            string patientName;
            string currentID;
            string prevID = "null";
            string currentSeries;
            string prevSeries = "null";

            patientList = new ObservableCollection<Patient>();
            Encryption encryptedName = new Encryption();

            while (data.Read())
            {
                img = new PatientImage();
                currentID = data.GetString(data.GetOrdinal("Patient ID"));
                patientName = data.GetString(data.GetOrdinal("Patient Name"));
                currentSeries = data.GetString(data.GetOrdinal("Series"));

                if (!currentID.Equals(prevID))
                {
                    foreach (Patient p in patientList)
                    {
                        if (p.patientID.Equals(currentID))
                        {
                            dataSets = p.dataSet;
                            patientExist = true;
                        }
                    }

                    if (patientExist == false)
                    {
                        dataSets = new ObservableCollection<ImageDataset>();
                        int tableID = data.GetInt32(data.GetOrdinal("Table ID"));
                        if(showName==true) patient = new Patient(currentID, encryptedName.Decrypt(patientName,tableID), dataSets);
                        else patient = new Patient(currentID, currentID, dataSets);
                        patientList.Add(patient);
                    }
                    else patientExist = false;

                    imageCollectionDS = new ObservableCollection<PatientImage>();
                    imgDS = new ImageDataset(currentSeries, imageCollectionDS);
                    dataSets.Add(imgDS);
                }

                else if (!currentSeries.Equals(prevSeries))
                {
                    imageCollectionDS = new ObservableCollection<PatientImage>();
                    imgDS = new ImageDataset(currentSeries, imageCollectionDS);
                    dataSets.Add(imgDS);
                }

                img.imgID = data.GetInt32(data.GetOrdinal("File ID"));
                imageCollectionDS.Add(img);
                
                prevID = currentID;
                prevSeries = currentSeries;
            }

            return patientList;
        }


        public String retrieveImageProperties(String fileID)
        {
            String properties = "null";
            if (ConnectionManager.ValidConnection == true)
            {
                using (SqlConnection conn = new SqlConnection(ConnectionManager.getConnection))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("spr_SelectProperties_v001", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@fileID", SqlDbType.Int).Value = Int32.Parse(fileID);
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    dataReader.Read();

                    properties = "Patient ID : " + dataReader.GetString(dataReader.GetOrdinal("Patient ID"));
                    properties += System.Environment.NewLine;
                    properties += "Birthdate : " + dataReader.GetString(dataReader.GetOrdinal("Birthdate"));
                    properties += System.Environment.NewLine;
                    properties += "Age : " + dataReader.GetString(dataReader.GetOrdinal("Age"));
                    properties += System.Environment.NewLine;
                    properties += "Sex : " + dataReader.GetString(dataReader.GetOrdinal("Sex"));
                    properties += System.Environment.NewLine;
                    properties += "DICOM Acquisition Date : " + dataReader.GetDateTime(dataReader.GetOrdinal("Image Date Time")).ToString();
                    properties += System.Environment.NewLine;
                    properties += "Last Modified Date : " + dataReader.GetDateTime(dataReader.GetOrdinal("Modified Date")).ToString();
                    properties += System.Environment.NewLine;
                    properties += "Body Part : " + dataReader.GetString(dataReader.GetOrdinal("Body Part"));
                    properties += System.Environment.NewLine;
                    properties += "Slice Thickness : " + dataReader.GetString(dataReader.GetOrdinal("Slice Thickness"));
                    properties += System.Environment.NewLine;
                    properties += "Study Description : " + dataReader.GetString(dataReader.GetOrdinal("Study Description"));
                    properties += System.Environment.NewLine;
                    properties += "Series Description : " + dataReader.GetString(dataReader.GetOrdinal("Series Description"));
                    properties += System.Environment.NewLine;
                    properties += "Study Instance UID : " + dataReader.GetString(dataReader.GetOrdinal("Study UID"));
                    properties += System.Environment.NewLine;
                    properties += "Series Instance UID : " + dataReader.GetString(dataReader.GetOrdinal("Series UID"));
                    properties += System.Environment.NewLine;
                    properties += "SOP Instance UID : " + dataReader.GetString(dataReader.GetOrdinal("Image UID"));
                    properties += System.Environment.NewLine;
                    dataReader.Close();
                }
            }
            return properties;
        }
    }
}
