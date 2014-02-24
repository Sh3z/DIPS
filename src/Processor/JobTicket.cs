using DIPS.Processor.Client;
using DIPS.Processor.Client.Eventing;
using DIPS.Processor.Client.Sinks;
using DIPS.Util.Remoting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor
{
    /// <summary>
    /// Represents an entry into the queueing system for a single job.
    /// </summary>
    [Serializable]
    public class JobTicket : IJobTicket
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JobTicket"/> class.
        /// </summary>
        /// <param name="req">The <see cref="JobRequest"/> this ticket
        /// represents within the queue.</param>
        /// <param name="handler">The first element in the chain of objects
        /// that handles this <see cref="JobTicket"/> being cancelled.</param>
        /// <exception cref="ArgumentNullException">req is null.</exception>
        public JobTicket( JobRequest req, ITicketCancellationHandler handler )
        {
            if( req == null )
            {
                throw new ArgumentNullException( "req" );
            }

            if( handler == null )
            {
                throw new ArgumentNullException( "handler" );
            }

            Request = req;
            Cancelled = false;
            JobID = Guid.NewGuid();
            _sink = new EventSinkContainer<TicketSink>();
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
        /// Gets the <see cref="ISinkContainer"/> used to dispatch events
        /// pertaining to this <see cref="IJobTicket"/>.
        /// </summary>
        public ISinkContainer<TicketSink> Sinks
        {
            get
            {
                return _sink;
            }
        }
        [DebuggerBrowsable( DebuggerBrowsableState.Never )]
        private EventSinkContainer<TicketSink> _sink;

        /// <summary>
        /// Gets the current <see cref="JobState"/> this job is in.
        /// </summary>
        public JobState State
        {
            get;
            internal set;
        }

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
            if( _cancellationHandler.Handle( this ) )
            {
                _onJobCancelled();
            }
        }


        internal void OnJobStarted()
        {
            _sink.FireAsync( "JobStarted", this, EventArgs.Empty );
        }

        internal void OnJobCompleted()
        {
            _sink.FireAsync( "JobCompleted", this, EventArgs.Empty );
        }

        internal void OnJobError( Exception e )
        {
            _sink.FireAsync( "JobError", this, new JobErrorArgs( e ) );
        }

        private void _onJobCancelled()
        {
            _sink.FireAsync( "JobCancelled", this, EventArgs.Empty );
        }


        /// <summary>
        /// Contains the first element in the handler chain for cancelling
        /// this ticket.
        /// </summary>
        private ITicketCancellationHandler _cancellationHandler;
    }
}
