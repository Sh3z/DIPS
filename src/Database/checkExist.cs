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

            SqlCommand cmd = new SqlCommand("spr_SelectPatient_v001", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@birthdate", SqlDbType.VarChar).Value = staticVariables.pBday;
            cmd.Parameters.Add("@age", SqlDbType.VarChar).Value = staticVariables.age;
            cmd.Parameters.Add("@sex", SqlDbType.Char).Value = staticVariables.sex;
            cmd.Parameters.Add("@fname", SqlDbType.VarChar).Value = staticVariables.firstName;
            cmd.Parameters.Add("@lname", SqlDbType.VarChar).Value = staticVariables.lastName;
            SqlDataReader dataReader = cmd.ExecuteReader();

            String a, b, c;
            while(dataReader.Read())
            {
                a = dataReader.GetString(1);
                b = dataReader.GetString(2);
                c = dataReader.GetString(3);
                Boolean aa = staticVariables.bodyPart.Equals(a);
                Boolean bb = staticVariables.studyDesc.Equals(b);
                Boolean cc = staticVariables.seriesDesc.Equals(c);

                if (aa == true && (bb == true && cc == true))
                {
                    staticVariables.patientExist = true;
                    staticVariables.databaseID = dataReader.GetInt32(0);
                    dataReader.Close();
                    break;
                }
            }
            if (staticVariables.patientExist == false)
            {
                dataReader.Close();
                while (true)
                {
                    staticVariables.pID = generateID();
                    String sql = "select id from patient where patientID='" + staticVariables.pID + "'";
                    SqlCommand cmd2 = new SqlCommand(sql, con);
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
           if(staticVariables.pBday.Equals("NULL")) patientID = alphabet + rand.Next(50000000,99999999);
           else patientID = alphabet + staticVariables.pBday;

            return patientID;
        }
    }
}
