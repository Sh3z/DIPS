using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Repository
{
    public class AdminRepository
    {
        private String adminPass = "admin123";

        public Boolean verified(String pass)
        {
            if (pass.Equals(adminPass)) return true;
            else return false;
        }
    }
}
