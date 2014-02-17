using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Objects
{
    public class Filter
    {
        private String _PatientID;

        public String PatientID
        {
            get { return _PatientID; }
            set { _PatientID = value; }
        }

        private DateTime _AcquisitionDateFrom;

        public DateTime AcquisitionDateFrom
        {
            get { return _AcquisitionDateFrom; }
            set { _AcquisitionDateFrom = value; }
        }

        private DateTime _AcquisitionDateTo;

        public DateTime AcquisitionDateTo
        {
            get { return _AcquisitionDateTo; }
            set { _AcquisitionDateTo = value; }
        }

        private String _Gender;

        public String Gender
        {
            get { return _Gender; }
            set { _Gender = value; }
        }
        
    }
}
