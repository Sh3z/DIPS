using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor.Client
{
    /// <summary>
    /// Represents the completion states of a job.
    /// </summary>
    public enum JobState
    {
        /// <summary>
        /// The job is currently within the queue
        /// </summary>
        InQueue,

        /// <summary>
        /// The job is in progress
        /// </summary>
        Running,

        /// <summary>
        /// The job was cancelled before it could complete.
        /// </summary>
        Cancelled,

        /// <summary>
        /// The job encountered a critical error during execution and
        /// could not continue.
        /// </summary>
        Error,

        /// <summary>
        /// The job completed successfully.
        /// </summary>
        Complete
    }
}
