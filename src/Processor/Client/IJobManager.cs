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
    }
}
