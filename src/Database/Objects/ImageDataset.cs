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
        private String _series;

        public String series
        {
            get { return _series; }
            set { _series = value; }
        }

        private ObservableCollection<PatientImage> _relatedImages;

        public ObservableCollection<PatientImage> relatedImages
        {
            get { return _relatedImages; }
            set { _relatedImages = value; }
        }

        public ImageDataset(string dsSeries, ObservableCollection<PatientImage> images)
        {
            this.series = dsSeries;
            this.relatedImages = images;
        }

        public ImageDataset()
        {

        }
        
    }
}
