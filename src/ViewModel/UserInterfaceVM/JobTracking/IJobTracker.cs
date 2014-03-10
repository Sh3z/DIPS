using DIPS.Processor.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.ViewModel.UserInterfaceVM.JobTracking
{
    /// <summary>
    /// Represents the object used to track running jobs
    /// </summary>
    public interface IJobTracker : INotifyPropertyChanged, IDisposable
    {
        /// <summary>
        /// Gets a collection of <see cref="JobViewModel"/>s that are
        /// waiting to be ran.
        /// </summary>
        ObservableCollection<JobViewModel> Pending
        {
            get;
        }

        /// <summary>
        /// Gets a collection of <see cref="JobViewModel"/>s that have
        /// finished (either in error or in full)
        /// </summary>
        ObservableCollection<JobViewModel> Finished
        {
            get;
        }

        /// <summary>
        /// Gets the currently executing <see cref="JobViewModel"/>.
        /// </summary>
        JobViewModel Current
        {
            get;
        }

        /// <summary>
        /// Adds a new job to the tracker.
        /// </summary>
        /// <param name="ticket">The <see cref="IJobTicket"/> to begin
        /// tracking.</param>
        /// <param name="handler">The <see cref="IJobResultsHandler"/> used to
        /// handle the results of a complete job.</param>
        void Add( IJobTicket ticket, IJobResultsHandler handler );
    }
}
