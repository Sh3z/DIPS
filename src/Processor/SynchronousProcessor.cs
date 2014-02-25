using DIPS.Processor.Client;
using DIPS.Processor.Persistence;
using DIPS.Processor.Pipeline;
using DIPS.Processor.Worker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor
{
    /// <summary>
    /// Executes jobs on the calling thread.
    /// </summary>
    public class SynchronousProcessor : ISynchronousProcessor, ITicketCancellationHandler
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SynchronousProcessor"/>
        /// class.
        /// </summary>
        /// <param name="handler">The <see cref="IWorker"/> to execute
        /// jobs against.</param>
        public SynchronousProcessor( IWorker handler )
        {
            if( handler == null )
            {
                throw new ArgumentNullException( "handler" );
            }

            _worker = handler;
        }


        public IPipelineFactory Factory
        {
            get;
            set;
        }


        /// <summary>
        /// Sets the successive <see cref="ITicketCancellationHandler"/> to
        /// this instance.
        /// </summary>
        /// <remarks>
        /// In the event the current <see cref="ITicketCancellationHandler"/>
        /// cannot handle the cancellation of a ticket, it should instead delegate
        /// to the successor. If no successor is set, the request should be ignored.
        /// </remarks>
        ITicketCancellationHandler ITicketCancellationHandler.Successor
        {
            set
            {
                throw new InvalidOperationException( "Synchronous processes cannot be cancelled." );
            }
        }

        /// <summary>
        /// Handles the cancellation of the job represented by the ticket.
        /// </summary>
        /// <param name="ticket">The <see cref="IJobTicket"/> that has been
        /// cancelled.</param>
        /// <returns>true if the request has been handled; false otherwise.</returns>
        bool ITicketCancellationHandler.Handle( IJobTicket ticket )
        {
            // We don't permit cancelling.
            return false;
        }


        /// <summary>
        /// Processes the provided <see cref="JobRequest"/> and returns the
        /// result. This can be run asynchronously.
        /// </summary>
        /// <param name="request">The <see cref="JobRequest"/> providing
        /// information about the job to run.</param>
        /// <returns>A <see cref="Task"/> which computes the <see cref="JobResult"/>
        /// from the given request.</returns>
        public Task<JobResult> Process( JobRequest request )
        {
            return Task.Factory.StartNew<JobResult>( () => _runJob( request ) );
        }


        /// <summary>
        /// Executes the job and returns the result
        /// </summary>
        /// <param name="req">The job definition to execute.</param>
        /// <returns>The result from processin the job.</returns>
        private JobResult _runJob( JobRequest req )
        {
            JobTicket ticket = new JobTicket( req, this );
            WorkerArgs args = new WorkerArgs( new MemoryPersister(), Factory );
            args.Ticket = ticket;
            _worker.Work( args );
            return ticket.Result;
        }


        /// <summary>
        /// Contains the object used to execute the job.
        /// </summary>
        private IWorker _worker;
    }
}
