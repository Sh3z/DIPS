using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Database
{
    public static class staticVariables
    {
        public static int databaseID = 0;
        public static Boolean patientExist = false;
        public static Boolean sameSeries = false;
        public static String sql = "Data Source=.\\Yeh;Initial Catalog=medicalImaging;Integrated Security=True";

        public static String pID = "";
        public static String firstName = "";
        public static String lastName = "";
        public static String sex = "";
        public static String pBday = "";
        public static String age = "";
        public static int imageSeries = 1;

        public static String imgNumber = "";
        public static String imgDateTime = "";
        public static String bodyPart = "";
        public static String studyDesc = "";
        public static String seriesDesc = "";

        public static String sliceThickness = "";
        public static String readFile = "";
    }
}
