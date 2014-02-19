using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Database.Objects
{
    public class Patient
    {
        private String _patientID;

        public String patientID
        {
            get { return this._patientID; }
            set { this._patientID = value; }
        }

        private String _patientName;

        public String patientName
        {
            get { return _patientName; }
            set { _patientName = value; }
        }
        

        private ObservableCollection<ImageDataset> _dataSet;

        public ObservableCollection<ImageDataset> dataSet
        {
            get { return _dataSet; }
            set { _dataSet = value; }
        }

        public Patient(String patientID, String patientName, ObservableCollection<ImageDataset> dataSet)
        {
            this.patientID = patientID;
            this.patientName = patientName;
            this.dataSet = dataSet;
        }
        
    }
}
