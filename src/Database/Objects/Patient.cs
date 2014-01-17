using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Database.Objects
{
    public class Patient
    {
        private int _id;

        public int id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        private String _name;

        public String name
        {
            get { return this._name; }
            set { this._name = value; }
        }
        
        
    }
}
