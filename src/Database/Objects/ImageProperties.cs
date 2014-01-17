using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Database.Objects
{
    public class ImageProperties
    {
        private int _gamma;

        public int Gamma
        {
            get { return _gamma; }
            set { _gamma = value; }
        }

        private int _smoothingRadius;

        public int SmoothingRadius
        {
            get { return _smoothingRadius; }
            set { _smoothingRadius = value; }
        }
        
        
    }
}
