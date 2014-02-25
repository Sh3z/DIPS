using DIPS.Util.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DIPS.ViewModel.UserInterfaceVM.JobTracking
{
    /// <summary>
    /// Represents the presentation logic behind managing a set of running jobs.
    /// </summary>
    public class OngoingJobsViewModel : ViewModel, IDisposable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OngoingJobsViewModel"/>
        /// class.
        /// </summary>
        public OngoingJobsViewModel()
        {
            CancelJob = new CancelJobCommand();
            Jobs = new ObservableCollection<JobViewModel>();
        }


        void IDisposable.Dispose()
        {
            Action<IDisposable> dispose = x => x.Dispose();
            Jobs.Cast<IDisposable>().ForEach( dispose );
            Jobs.Clear();
        }


        /// <summary>
        /// Gets an <see cref="ICommand"/> used to cancel <see cref="JobViewModel"/>s.
        /// </summary>
        public ICommand CancelJob
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets an observable collection of the ongoing jobs.
        /// </summary>
        public ObservableCollection<JobViewModel> Jobs
        {
            get;
            private set;
        }
    }
}
