using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Database.Objects;

namespace DIPS.Database.Objects
{
    public class ImageDataset
    {
        private int _seriesID;

        public int seriesID
        {
            get { return _seriesID; }
            set { _seriesID = value; }
        }
        
        private String _series;

        public String series
        {
            get { return _series; }
            set { _series = value; }
        }

        private ObservableCollection<ProcessTechnique> _relatedTechnique;

        public ObservableCollection<ProcessTechnique> relatedTechnique
        {
            get { return _relatedTechnique; }
            set { _relatedTechnique = value; }
        }

        public ImageDataset(int seriesID, string dsSeries, ObservableCollection<ProcessTechnique> techniques)
        {
            this.seriesID = seriesID;
            this.series = dsSeries;
            this.relatedTechnique = techniques;
        }

        public ImageDataset()
        {

        }
        
    }
}
