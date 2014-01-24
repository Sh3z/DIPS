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
        public static int seriesID = 0;
        public static Boolean codecRegistration = false;
        public static Boolean patientExist = false;
        public static Boolean sameSeries = false;
        public static Boolean imageExist = false;
        public static String sql = "Data Source=.\\Yeh;Initial Catalog=medicalImaging;Integrated Security=True";

        public static String pID = "";
        public static String patientName = "";
        public static String sex = "";
        public static String pBday = "";
        public static String age = "";


        public static String imgNumber = "";
        public static String modality = "";
        public static String imgDateTime = "";
        public static String bodyPart = "";
        public static String studyDesc = "";
        public static String seriesDesc = "";

        public static String sliceThickness = "";
        public static String readFile = "";
    }
}
