using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Unity
{
    public interface IConnectionView
    {
        String DataSource { set; }
        String Catalog { set; }
        String Security { set; }
        String Extra { set; }

        void OpenDialog();
        void ApplySetting();
        void HideDialog();
    }
}
