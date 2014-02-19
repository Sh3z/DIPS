using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Database
{
    public static class DicomInfo
    {
        private static int _databaseID = 0;
        private static int _seriesID = 0;
        private static Boolean _fileReadable;
        private static Boolean _codecRegistration = false;
        private static Boolean _logCreated = false;
        private static Boolean _logNeedUpdate = false;
        private static Boolean _patientExist = false;
        private static Boolean _sameSeries = false;
        private static Boolean _imageExist = false;
        private static String _patientID = "";
        private static String _patientName = "";
        private static String _sex = "";
        private static String _patientBday = "";
        private static String _age = "";
        private static String _imgNumber = "";
        private static String _modality = "";
        private static String _imgDateTime = "";
        private static String _bodyPart = "";
        private static String _studyDesc = "";
        private static String _seriesDesc = "";
        private static String _sliceThickness = "";
        private static String _readFile = "";

        public static int databaseID
        {
            get { return _databaseID; }
            set { _databaseID = value; }
        }

        public static int seriesID
        {
            get { return _seriesID; }
            set { _seriesID = value; }
        }

        public static Boolean codecRegistration
        {
            get { return _codecRegistration; }
            set { _codecRegistration = value; }
        }

        public static Boolean fileReadable
        {
            get { return _fileReadable; }
            set { _fileReadable = value; }
        }

        public static Boolean logCreated
        {
            get { return _logCreated; }
            set { _logCreated = value; }
        }

        public static Boolean logNeedUpdate
        {
            get { return _logNeedUpdate; }
            set { _logNeedUpdate = value; }
        }
        
        public static Boolean patientExist
        {
            get { return _patientExist; }
            set { _patientExist = value; }
        }

        public static Boolean sameSeries
        {
            get { return _sameSeries; }
            set { _sameSeries = value; }
        }

        public static Boolean imageExist
        {
            get { return _imageExist; }
            set { _imageExist = value; }
        }

        public static String pID
        {
            get { return _patientID; }
            set { _patientID = value; }
        }

        public static String patientName
        {
            get { return _patientName; }
            set { _patientName = value; }
        }

        public static String sex
        {
            get { return _sex; }
            set { _sex = value; }
        }

        public static String pBday
        {
            get { return _patientBday; }
            set { _patientBday = value; }
        }

        public static String age
        {
            get { return _age; }
            set { _age = value; }
        }

        public static String imgNumber
        {
            get { return _imgNumber; }
            set { _imgNumber = value; }
        }

        public static String modality
        {
            get { return _modality; }
            set { _modality = value; }
        }

        public static String imgDateTime
        {
            get { return _imgDateTime; }
            set { _imgDateTime = value; }
        }

        public static String bodyPart
        {
            get { return _bodyPart; }
            set { _bodyPart = value; }
        }

        public static String studyDesc
        {
            get { return _studyDesc; }
            set { _studyDesc = value; }
        }

        public static String seriesDesc
        {
            get { return _seriesDesc; }
            set { _seriesDesc = value; }
        }

        public static String sliceThickness
        {
            get { return _sliceThickness; }
            set { _sliceThickness = value; }
        }

        public static String readFile
        {
            get { return _readFile; }
            set { _readFile = value; }
        }

    }
}
