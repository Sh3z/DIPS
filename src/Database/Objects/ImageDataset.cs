using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace DIPS.Database.Objects
{
    public class ImageDataset
    {
        private int _dsID;
        
        public int dsID
        {
            get { return this._dsID; }
            set { this._dsID = value; }
        }

        private String _name;

        public String name
        {
            get { return this._name; }
            set { this._name = value; }
        }

        private ObservableCollection<PatientImage> _relatedImages;

        public ObservableCollection<PatientImage> relatedImages
        {
            get { return _relatedImages; }
            set { _relatedImages = value; }
        }

        public ImageDataset(string dsName, ObservableCollection<PatientImage> images)
        {
            this.name = dsName;
            this.relatedImages = images;
        }

        public ImageDataset()
        {

        }
        
    }
}
