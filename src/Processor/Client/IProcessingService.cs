using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DIPS.Processor.Client
{
    /// <summary>
    /// Represents a DIPS processing service.
    /// </summary>
    public interface IProcessingService
    {
        /// <summary>
        /// Gets the <see cref="XDocument"/> detailing the currently installed
        /// algorithms.
        /// </summary>
        XDocument AlgorithmDefinitions
        {
            get;
        }

        /// <summary>
        /// Creates and returns a new <see cref="ISynchronousProcessor"/> clients
        /// can use to compute jobs on their own threads.
        /// </summary>
        /// <returns>An <see cref="ISynchronousProcessor"/> instance for running
        /// jobs within the client.</returns>
        ISynchronousProcessor CreateSynchronousProcessor();

        /// <summary>
        /// Enqueues a new job to be executed.
        /// </summary>
        /// <param name="job">The <see cref="JobRequest"/> detailing the
        /// job.</param>
        /// <returns>An <see cref="IJobTicket"/> providing job monitoring and
        /// result-tracking capabilities.</returns>
        IJobTicket EnqueueJob( JobRequest job );

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
