using System;

namespace DIPS.Processor.Client
{
    /// <summary>
    /// Represents an entry into the queueing system for a single job.
    /// </summary>
    public interface IJobTicket
    {
        /// <summary>
        /// Occurs when the job is cancelled.
        /// </summary>
        event EventHandler JobCancelled;

        /// <summary>
        /// Occurs when the job is complete.
        /// </summary>
        event EventHandler JobCompleted;

        /// <summary>
        /// Occurs when the job has begun.
        /// </summary>
        event EventHandler JobStarted;

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
