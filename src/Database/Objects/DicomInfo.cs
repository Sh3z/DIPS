using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Database
{
    public class DicomInfo
    {
        private int _databaseID = 0;
        private int _seriesID = 0;
        private Boolean _fileReadable;
        private Boolean _patientExist = false;
        private Boolean _sameSeries = false;
        private Boolean _imageExist = false;
        private String _studyUID = "";
        private String _seriesUID = "";
        private String _imageUID = "";
        private String _patientID = "";
        private String _patientName = "";
        private String _sex = "";
        private String _patientBday = "";
        private String _age = "";
        private String _imgNumber = "";
        private String _modality = "";
        private String _imgDateTime = "";
        private String _bodyPart = "";
        private String _studyDesc = "";
        private String _seriesDesc = "";
        private String _sliceThickness = "";
        private String _readFile = "";

        public int databaseID
        {
            get { return _databaseID; }
            set { _databaseID = value; }
        }

        public int seriesID
        {
            get { return _seriesID; }
            set { _seriesID = value; }
        }

        public Boolean fileReadable
        {
            get { return _fileReadable; }
            set { _fileReadable = value; }
        }

        public Boolean patientExist
        {
            get { return _patientExist; }
            set { _patientExist = value; }
        }

        public Boolean sameSeries
        {
            get { return _sameSeries; }
            set { _sameSeries = value; }
        }

        public Boolean imageExist
        {
            get { return _imageExist; }
            set { _imageExist = value; }
        }

        public String studyUID
        {
            get { return _studyUID; }
            set { _studyUID = value; }
        }

        public String seriesUID
        {
            get { return _seriesUID; }
            set { _seriesUID = value; }
        }

        public String imageUID
        {
            get { return _imageUID; }
            set { _imageUID = value; }
        }
        

        public String pID
        {
            get { return _patientID; }
            set { _patientID = value; }
        }

        public String patientName
        {
            get { return _patientName; }
            set { _patientName = value; }
        }

        public String sex
        {
            get { return _sex; }
            set { _sex = value; }
        }

        public String pBday
        {
            get { return _patientBday; }
            set { _patientBday = value; }
        }

        public String age
        {
            get { return _age; }
            set { _age = value; }
        }

        public String imgNumber
        {
            get { return _imgNumber; }
            set { _imgNumber = value; }
        }

        public String modality
        {
            get { return _modality; }
            set { _modality = value; }
        }

        public String imgDateTime
        {
            get { return _imgDateTime; }
            set { _imgDateTime = value; }
        }

        public String bodyPart
        {
            get { return _bodyPart; }
            set { _bodyPart = value; }
        }

        public String studyDesc
        {
            get { return _studyDesc; }
            set { _studyDesc = value; }
        }

        public String seriesDesc
        {
            get { return _seriesDesc; }
            set { _seriesDesc = value; }
        }

        public String sliceThickness
        {
            get { return _sliceThickness; }
            set { _sliceThickness = value; }
        }

        public String readFile
        {
            get { return _readFile; }
            set { _readFile = value; }
        }

    }
}
