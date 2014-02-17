using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Database.Objects
{
    public class PatientImage
    {
        private int _imgID;

        public int imgID
        {
            get { return _imgID; }
            set { _imgID = value; }
        }

        public override string ToString()
        {
            return imgID.ToString();
        }
    }
}
