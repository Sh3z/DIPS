using Database.Objects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Database.Objects
{
    public class PatientImage
    {
        public PatientImage(int id)
        {
            this.imgID = id;
        }

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
