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

namespace Database
{
    public class ImageRepository
    {
        public List<ImageDataset> generateTreeView()
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

                string currentID;
                string prevID = "null";

                allDatasetsActive = new List<ImageDataset>();
                ObservableCollection<PatientImage> imageCollectionDS = null;
                ImageDataset imgDS = null;
                PatientImage img = null;
                Patient patient = null;

                 while (data.Read())
                {
                    img = new PatientImage();
                    currentID = data.GetString(0);

                    if (!currentID.Equals(prevID))
                    {
                        imageCollectionDS = new ObservableCollection<PatientImage>();
                        imgDS = new ImageDataset(currentID, imageCollectionDS);
                        allDatasetsActive.Add(imgDS);
                    }

                    img.patientID = currentID;
                    img.imgID = data.GetString(1);
                    imageCollectionDS.Add(img);
                    prevID = currentID;
                }
                 data.Close();
                 con.Close();
            }
            catch (Exception e) { }

            return allDatasetsActive;
        }
    }
}
