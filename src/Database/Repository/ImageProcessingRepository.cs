using DIPS.Database.Objects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Database.Repository
{
    public class ImageProcessingRepository
    {
        public void insertTechnique(String name, XDocument doc)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionManager.getConnection))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spr_InsertTechnique_v001", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = name;
                cmd.Parameters.Add("@technique", SqlDbType.Xml).Value = doc.ToString();
                cmd.ExecuteNonQuery();
            }
        }

        public List<Technique> getAllTechnique()
        {
            List<Technique> list = null;

            using (SqlConnection conn = new SqlConnection(ConnectionManager.getConnection))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spr_RetrieveAllTechnique_v001", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader data = cmd.ExecuteReader();

                while (data.Read())
                {
                    if (list == null) list = new List<Technique>();
                    Technique technique = new Technique();
                    technique.ID = data.GetInt32(data.GetOrdinal("ID"));
                    technique.Name = data.GetString(data.GetOrdinal("name"));
                    list.Add(technique);
                }
                data.Close();
            }

            return list;
        }

        public XDocument getSpecificTechnique(int id)
        {
            String data = null;
            XDocument xml = null;

            using (SqlConnection conn = new SqlConnection(ConnectionManager.getConnection))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spr_RetrieveSelectedTechnique_v001", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                data = cmd.ExecuteScalar().ToString();
            }

            if (data != null) xml = XDocument.Parse(data);
            return xml;
        }

        public void storeProcessedImage(int series, String imgNum, byte[] image)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionManager.getConnection))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("spr_InsertImages_v001", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@imgID", SqlDbType.Int).Value = series;
                    if (!String.IsNullOrEmpty(imgNum)) cmd.Parameters.Add("@imgNum", SqlDbType.VarChar).Value = imgNum;
                    if (image != null) cmd.Parameters.Add("@imgBlob", SqlDbType.VarBinary, image.Length).Value = image;
                    cmd.Parameters.Add("@process", SqlDbType.Bit).Value = 1;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException e)
            {
                if (e.Number == 2627) Console.WriteLine("image existed");
                else Console.WriteLine(e);
            }
        }
    }
}
