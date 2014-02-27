using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Unity
{
    public interface IQueueDialog
    {
        object Current { set; }
        ObservableCollection<object> Pending { set; }
        ObservableCollection<object> Finished { set; }

        void ShowDialog();
    }
}
