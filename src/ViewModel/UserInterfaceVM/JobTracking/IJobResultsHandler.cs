using DIPS.Processor.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.ViewModel.UserInterfaceVM.JobTracking
{
    /// <summary>
    /// Represents the object responsible for handling the post-processing
    /// of complete jobs.
    /// </summary>
    public interface IJobResultsHandler
    {
        /// <summary>
        /// Handles the results of a finised job.
        /// </summary>
        /// <param name="completeJob">The <see cref="IJobTicket"/> representing
        /// the finished job.</param>
        void HandleResults( IJobTicket completeJob );
    }
}
