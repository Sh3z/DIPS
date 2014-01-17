using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Database.Objects
{
    public class Technique
    {
        private int _ID;

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        private String _Name;

        public String Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        
    }
}
