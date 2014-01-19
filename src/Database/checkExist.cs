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
            cmd.Parameters.Add("@birthdate", SqlDbType.VarChar).Value = staticVariables.pBday;
            cmd.Parameters.Add("@age", SqlDbType.VarChar).Value = staticVariables.age;
            cmd.Parameters.Add("@sex", SqlDbType.Char).Value = staticVariables.sex;
            cmd.Parameters.Add("@fname", SqlDbType.VarChar).Value = staticVariables.firstName;
            cmd.Parameters.Add("@lname", SqlDbType.VarChar).Value = staticVariables.lastName;
            SqlDataReader dataReader = cmd.ExecuteReader();

            while (dataReader.Read())
            {
                staticVariables.patientExist = true;
                Boolean bodyMatch = staticVariables.bodyPart.Equals(dataReader.GetString(1));
                Boolean studyMatch = staticVariables.studyDesc.Equals(dataReader.GetString(2));
                Boolean seriesMatch = staticVariables.seriesDesc.Equals(dataReader.GetString(3));

                if (bodyMatch == true && (studyMatch == true && seriesMatch == true))
                {
                    staticVariables.sameSeries = true;
                    staticVariables.databaseID = dataReader.GetInt32(0);
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
                SqlCommand cmd2 = new SqlCommand("spr_RetrieveSeriesAvailable_v001", con);
                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.Parameters.Add("@databaseID", SqlDbType.Int).Value = staticVariables.databaseID;
                SqlDataReader reader = cmd2.ExecuteReader();
                while (reader.Read())
                {
                    staticVariables.imageSeries = reader.GetInt32(0);
                    staticVariables.imageSeries++;
                    break;
                }
                reader.Close();

                SqlCommand cmd3 = new SqlCommand("spr_UpdateSeriesNo_v001", con);
                cmd3.CommandType = CommandType.StoredProcedure;
                cmd3.Parameters.Add("@series", SqlDbType.Int).Value = staticVariables.imageSeries;
                cmd3.Parameters.Add("@databaseID", SqlDbType.Int).Value = staticVariables.databaseID;
                cmd3.ExecuteNonQuery();
            }
         
            con.Close();
        }

        String generateID()
        {
            Random rand = new Random(System.DateTime.Now.Millisecond);
            String alphabet = "";
            for (int i = 0; i < 4; i++)
            {
                char alpha = 'A';
                alpha = (char)(alpha + rand.Next(0, 26));
                alphabet += alpha;
            }
            String patientID = "";
            if (staticVariables.pBday.Equals("NULL")) patientID = alphabet + rand.Next(50000000, 99999999);
            else patientID = alphabet + staticVariables.pBday;

            return patientID;
        }
    }
}
