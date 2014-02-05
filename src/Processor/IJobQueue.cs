using DIPS.Processor.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor
{
    /// <summary>
    /// Represents the queue of processing jobs to be executed.
    /// </summary>
    public interface IJobQueue
    {
        /// <summary>
        /// Occurs when a job is added to this <see cref="IJobQueue"/>.
        /// </summary>
        event EventHandler JobAdded;

        /// <summary>
        /// Gets a value indicating whether this <see cref="IJobQueue"/>
        /// has more jobs to process.
        /// </summary>
        bool HasPendingJobs
        {
            get;
        }

        /// <summary>
        /// Gets the number of jobs remaining within this <see cref="IJobQueue"/>.
        /// </summary>
        int NumberOfJobs
        {
            get;
        }

        /// <summary>
        /// Enqueues a new job to this <see cref="IJobQueue"/>.
        /// </summary>
        /// <param name="req">The <see cref="IJobTicket"/> representing the
        /// job information.</param>
        void Enqueue( IJobTicket req );

        /// <summary>
        /// Dequeues the next job to execute.
        /// </summary>
        /// <returns>The <see cref="IJobTicket"/> representing the
        /// job.</returns>
        IJobTicket Dequeue();
    }
}
