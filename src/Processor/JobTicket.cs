using DIPS.Processor.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor
{
    /// <summary>
    /// Represents an entry into the queueing system for a single job.
    /// </summary>
    public class JobTicket : IJobTicket
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JobTicket"/> class.
        /// </summary>
        /// <param name="req">The <see cref="JobRequest"/> this ticket
        /// represents within the queue.</param>
        /// <exception cref="ArgumentNullException">req is null.</exception>
        public JobTicket( JobRequest req )
        {
            if( req == null )
            {
                throw new ArgumentNullException( "req" );
            }

            Request = req;
            Cancelled = false;
            JobID = Guid.NewGuid();
        }


        /// <summary>
        /// Gets the unique identifier for this ticket.
        /// </summary>
        public Guid JobID
        {
            get;
            private set;
        }

        /// <summary>
        /// Occurs when the job is cancelled.
        /// </summary>
        public event EventHandler JobCancelled;

        /// <summary>
        /// Occurs when the job has begun.
        /// </summary>
        public event EventHandler JobStarted;

        /// <summary>
        /// Occurs when the job is complete.
        /// </summary>
        public event EventHandler JobCompleted;


        /// <summary>
        /// Gets a value indicating whether this job was cancelled by the client.
        /// </summary>
        public bool Cancelled
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the original <see cref="JobRequest"/> provided by the client.
        /// </summary>
        public JobRequest Request
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the constructed <see cref="JobResult"/> for the job.
        /// </summary>
        public JobResult Result
        {
            get;
            internal set;
        }

        /// <summary>
        /// Cancels this job. If it is running, it is stopped during execution. If
        /// it has not run, it is removed from the queue. If the job has already finished,
        /// nothing occurs.
        /// </summary>
        public void Cancel()
        {
            Cancelled = true;
        }


        internal void OnJobStarted()
        {
            if( JobStarted != null )
            {
                JobStarted( this, EventArgs.Empty );
            }
        }

        internal void OnJobCompleted()
        {
            if( JobCompleted != null )
            {
                JobCompleted( this, EventArgs.Empty );
            }
        }

        internal void OnJobCancelled()
        {
            if( JobCancelled != null )
            {
                JobCancelled( this, EventArgs.Empty );
            }
        }
    }
}
