using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor.Client
{
    /// <summary>
    /// Represents the module of the <see cref="IProcessingService"/> used
    /// to enqueue jobs.
    /// </summary>
    public interface IJobManager
    {
        /// <summary>
        /// Enqueues a new job to be executed.
        /// </summary>
        /// <param name="job">The <see cref="JobRequest"/> detailing the
        /// job.</param>
        /// <returns>An <see cref="IJobTicket"/> providing job monitoring and
        /// result-tracking capabilities.</returns>
        IJobTicket EnqueueJob( JobRequest job );

        /// <summary>
        /// Deletes the results of a previously complete job.
        /// </summary>
        /// <param name="ticket">The <see cref="IJobTicket"/> representing the
        /// job to be deleted.</param>
        /// <returns><c>true</c> if the results associated with the
        /// <see cref="JobTicket"/> have been deleted.</returns>
        bool DeleteResults( IJobTicket ticket );

        /// <summary>
        /// Resolves the <see cref="JobResult"/> for a previously executed job.
        /// </summary>
        /// <param name="ticket">The <see cref="IJobTicket"/> representing the
        /// job.</param>
        /// <returns>The <see cref="JobResult"/> created through the execution of
        /// the job represented by the <see cref="IJobTicket"/>.</returns>
        JobResult GetResult( IJobTicket ticket );
    }
}
