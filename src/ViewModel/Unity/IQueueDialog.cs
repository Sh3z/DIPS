using DIPS.ViewModel.UserInterfaceVM.JobTracking;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.ViewModel.Unity
{
    public interface IQueueDialog
    {
        void ShowDialog( IJobTracker jobs );
    }
}
