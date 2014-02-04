using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Database
{
    public class checkExist
    {
        public void retriveDatabase()
        {
            SqlConnection con = new SqlConnection(staticVariables.sql);
            con.Open();

            SqlCommand cmd = new SqlCommand("spr_CheckPatientExist_v001", con);
            cmd.CommandType = CommandType.StoredProcedure;

            if (staticVariables.pBday == "") cmd.Parameters.Add("@birthdate", SqlDbType.VarChar).Value = DBNull.Value;
            else cmd.Parameters.Add("@birthdate", SqlDbType.VarChar).Value = staticVariables.pBday;

            if (staticVariables.age == "") cmd.Parameters.Add("@age", SqlDbType.VarChar).Value = DBNull.Value;
            else cmd.Parameters.Add("@age", SqlDbType.VarChar).Value = staticVariables.age;

            if (staticVariables.sex == "") cmd.Parameters.Add("@sex", SqlDbType.Char).Value = DBNull.Value;
            else cmd.Parameters.Add("@sex", SqlDbType.Char).Value = staticVariables.sex;

            if (staticVariables.patientName == "") cmd.Parameters.Add("@pName", SqlDbType.VarChar).Value = DBNull.Value;
            else cmd.Parameters.Add("@pName", SqlDbType.VarChar).Value = staticVariables.patientName;

            SqlDataReader dataReader = cmd.ExecuteReader();

            while (dataReader.Read())
            {
                staticVariables.patientExist = true;
                Boolean modelMatch = staticVariables.modality.Equals(dataReader.GetString(dataReader.GetOrdinal("Modality")));
                Boolean bodyMatch = staticVariables.bodyPart.Equals(dataReader.GetString(dataReader.GetOrdinal("Body Parts")));
                Boolean studyMatch = staticVariables.studyDesc.Equals(dataReader.GetString(dataReader.GetOrdinal("Study Description")));
                Boolean seriesMatch = staticVariables.seriesDesc.Equals(dataReader.GetString(dataReader.GetOrdinal("Series Description")));

                if ((modelMatch==true && bodyMatch == true) && (studyMatch == true && seriesMatch == true))
                {
                    staticVariables.sameSeries = true;
                    staticVariables.databaseID = dataReader.GetInt32(dataReader.GetOrdinal("Patient ID"));
                    staticVariables.seriesID = dataReader.GetInt32(dataReader.GetOrdinal("Series ID"));
                    break;
                }
            }
            dataReader.Close();

            if (staticVariables.patientExist == false)
            {
                SqlCommand cmd2 = new SqlCommand("spr_RetrieveID_v001", con);
                cmd2.CommandType = CommandType.StoredProcedure;
                while (true)
                {
                    staticVariables.pID = generateID();
                    cmd2.Parameters.Clear();
                    cmd2.Parameters.Add("@pID", SqlDbType.VarChar).Value = staticVariables.pID;
                    SqlDataReader reader = cmd2.ExecuteReader();
                    if (reader.Read() == false)
                    {
                        reader.Close();
                        break;
                    }
                    else reader.Close();
                    continue;
                }
            }
            else if (staticVariables.sameSeries == false)
            {
                SqlCommand cmd3 = new SqlCommand("spr_UpdateSeriesAvailable_v001", con);
                cmd3.CommandType = CommandType.StoredProcedure;
                cmd3.Parameters.Add("@databaseID", SqlDbType.Int).Value = staticVariables.databaseID;
                cmd3.ExecuteNonQuery();
            }
            else if (staticVariables.sameSeries == true)
            {
                 SqlCommand cmd2 = new SqlCommand("spr_RetrieveImageNumber_v001", con);
                 cmd2.CommandType = CommandType.StoredProcedure;
                 cmd2.Parameters.Add("@databaseID", SqlDbType.VarChar).Value = staticVariables.databaseID;
                 cmd2.Parameters.Add("@classID", SqlDbType.VarChar).Value = staticVariables.seriesID;
                 SqlDataReader reader = cmd2.ExecuteReader();

                 

                 while (reader.Read())
                 {
                     String imageNumber = reader.GetString(reader.GetOrdinal("Image Number"));
                     if (imageNumber.Equals(staticVariables.imgNumber))
                     {
                         staticVariables.imageExist = true;
                         break;
                     }
                 }
                 reader.Close();
            }
         
            con.Close();
        }

        String generateID()
        {
            Random rand = new Random(System.DateTime.Now.Millisecond);
            String alphabet = "";
            for (int i = 0; i < 2; i++)
            {
                char alpha = 'A';
                alpha = (char)(alpha + rand.Next(0, 26));
                alphabet += alpha;
            }
            String patientID = "";
            if (staticVariables.pBday.Equals("")) patientID = alphabet + rand.Next(10000000, 17000000);
            else patientID = alphabet + staticVariables.pBday;

            return patientID;
        }
    }
}
