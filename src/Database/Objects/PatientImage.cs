using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.UI.Objects
{
    public class PatientImage
    {
        private int _imgID;

        public int imgID
        {
            get { return _imgID; }
            set { _imgID = value; }
        }

        private String _name;

        public String name
        {
            get { return _name; }
            set { _name = value; }
        }

        private int _datasetID;

        public int datasetID
        {
            get { return _datasetID; }
            set { _datasetID = value; }
        }

        private int _patientID;

        public int patientID
        {
            get { return _patientID; }
            set { _patientID = value; }
        }
    }
}
