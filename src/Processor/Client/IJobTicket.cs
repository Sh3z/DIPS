using DIPS.Processor.Client.Sinks;
using DIPS.Util.Remoting;
using System;

namespace DIPS.Processor.Client
{
    /// <summary>
    /// Represents an entry into the queueing system for a single job.
    /// </summary>
    public interface IJobTicket
    {
        /// <summary>
        /// Gets the <see cref="ISinkContainer"/> used to dispatch events
        /// pertaining to this <see cref="IJobTicket"/>.
        /// </summary>
        ISinkContainer<TicketSink> Sinks
        {
            get;
        }

        /// <summary>
        /// Gets the current <see cref="JobState"/> this job is in.
        /// </summary>
        JobState State
        {
            get;
        }

        /// <summary>
        /// Gets the original <see cref="JobRequest"/> provided by the client.
        /// </summary>
        JobRequest Request
        {
            get;
        }

        /// <summary>
        /// Gets the constructed <see cref="JobResult"/> for the job.
        /// </summary>
        JobResult Result
        {
            get;
        }

        /// <summary>
        /// Gets a value indicating whether this job was cancelled by the client.
        /// </summary>
        bool Cancelled
        {
            get;
        }

        /// <summary>
        /// Cancels this job. If it is running, it is stopped during execution. If
        /// it has not run, it is removed from the queue. If the job has already finished,
        /// nothing occurs.
        /// </summary>
        void Cancel();
    }
}
