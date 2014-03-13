using DIPS.Database.Objects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Objects
{
    public class ProcessTechnique
    {

        public ProcessTechnique(String algorithm, ObservableCollection<PatientImage> images)
        {
            this.algorithm = algorithm;
            this.images = images;
        }

        private ObservableCollection<PatientImage> _images;

	    public ObservableCollection<PatientImage> images
	    {
	    	get { return _images;}
	    	set { _images = value;}
	    }

        private String _algorithm;

        public String algorithm
        {
            get { return _algorithm; }
            set { _algorithm = value; }
        }
        
    }
}
