using DIPS.Processor.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.ViewModel.UserInterfaceVM.JobTracking
{
    /// <summary>
    /// Represents the <see cref="IJobResultsHandler"/> which allows for
    /// multiple handlers to work with the same results.
    /// </summary>
    public class CompositeHandler : Collection<IJobResultsHandler>, IJobResultsHandler
    {
        /// <summary>
        /// Handles the results of a finised job.
        /// </summary>
        /// <param name="completeJob">The <see cref="IJobTicket"/> representing
        /// the finished job.</param>
        public void HandleResults( IJobTicket completeJob )
        {
            foreach( IJobResultsHandler handler in this )
            {
                _tryHandle( completeJob, handler );
            }
        }


        /// <summary>
        /// Attempts to run a single handler
        /// </summary>
        /// <param name="ticket">The finished job</param>
        /// <param name="handler">The job handler</param>
        private void _tryHandle( IJobTicket ticket, IJobResultsHandler handler )
        {
            try
            {
                handler.HandleResults( ticket );
            }
            catch { }
        }
    }
}
