using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Database.Objects
{
    public class PatientImage
    {
        private String _imgID;

        public String imgID
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

        private String _patientID;

        public String patientID
        {
            get { return _patientID; }
            set { _patientID = value; }
        }
    }
}
