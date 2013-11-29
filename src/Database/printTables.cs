using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Database
{
    class printTables
    {
        public void print()
        {
            SqlConnection con = new SqlConnection("Data Source=.\\Yeh;Initial Catalog=medicalImaging;Integrated Security=True");
            String line = "select * from patient; select * from imageVariables; select * from images;";

            SqlDataAdapter adpt = new SqlDataAdapter(line, con);
            DataSet ds = new DataSet();
            adpt.Fill(ds);

            Console.WriteLine("Patient Table");
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                for (int i = 0; i < 5; i++) Console.Write(dr[i].ToString() + " ");
                Console.WriteLine("");
            }

            Console.WriteLine("Images Info Table");
            foreach (DataRow dr in ds.Tables[1].Rows)
            {
                for(int i=0; i<7; i++) Console.Write(dr[i].ToString() + " ");
                Console.WriteLine("");
            }

            int count = 0;
            Console.WriteLine("images Table");
            foreach (DataRow dr in ds.Tables[2].Rows)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (i == 2)
                    {
                        FileStream fs = new FileStream(@"C:\Users\Yeh\Desktop\DicomImage\" + count + ".gif", FileMode.Create);
                        byte[] temp = (byte[])dr[i];
                        fs.Write(temp, 0, Convert.ToInt32(temp.Length));
                        fs.Close();
                        count++;
                    }
                }
                Console.WriteLine("");
            }
        }
    }
}
