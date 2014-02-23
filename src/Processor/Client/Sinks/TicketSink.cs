using DIPS.Util.Remoting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor.Client.Sinks
{
    /// <summary>
    /// Represents the event sink used by the job system.
    /// </summary>
    [Serializable]
    public class TicketSink : EventSink
    {
        /// <summary>
        /// Occurs when the job is cancelled.
        /// </summary>
        public event EventHandler JobCancelled;

        /// <summary>
        /// Occurs when the job is complete.
        /// </summary>
        public event EventHandler JobCompleted;

        /// <summary>
        /// Occurs when the job encounters an error.
        /// </summary>
        public event EventHandler JobError;

        /// <summary>
        /// Occurs when the job has begun.
        /// </summary>
        public event EventHandler JobStarted;
    }
}
