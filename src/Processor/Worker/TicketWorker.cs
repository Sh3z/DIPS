using DIPS.Processor.Client;
using DIPS.Processor.Client.JobDeployment;
using DIPS.Processor.Persistence;
using DIPS.Processor.Pipeline;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor.Worker
{
    public class TicketWorker : IWorker
    {
        public TicketWorker()
        {
            _cancelPadlock = new object();
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
        public ITicketCancellationHandler Successor
        {
            get;
            set;
        }

        /// <summary>
        /// Handles the cancellation of the job represented by the ticket.
        /// </summary>
        /// <param name="ticket">The <see cref="IJobTicket"/> that has been
        /// cancelled.</param>
        /// <returns>true if the request has been handled; false otherwise.</returns>
        public bool Handle( IJobTicket ticket )
        {
            lock( _cancelPadlock )
            {
                _cancel = _runner.Handle( ticket );
            }

            return _cancel;
        }


        public void Work( WorkerArgs args )
        {
            _currentArgs = args;
            _ticket = _currentArgs.Ticket as JobTicket;
            _ticket.OnJobStarted();

            _runner = new TicketPipelineRunner( _ticket, args.PipelineFactory );
            _runJob( _ticket.Request.Job );

            _currentArgs = null;
            _ticket = null;
            _runner = null;
        }

        private void _runJob( IJobDefinition job )
        {
            foreach( JobInput input in job.GetInputs() )
            {
                if( _handleNextInput( input ) == false )
                {
                    return;
                }
            }

            var results = _currentArgs.Persister.Load( _ticket.JobID );
            _ticket.Result = new JobResult( results );
            _ticket.State = JobState.Complete;
            _ticket.OnJobCompleted();
        }

        private bool _handleNextInput( JobInput input )
        {
            lock( _cancelPadlock )
            {
                if( _cancel )
                {
                    _ticket.Result = JobResult.Cancelled;
                    _ticket.State = JobState.Cancelled;
                    _currentArgs.Persister.Delete( _ticket.JobID );
                    return false;
                }
            }

            return _tryRunInput( input );
        }

        private bool _tryRunInput( JobInput input )
        {
            try
            {
                _runner.Run( _currentArgs.Persister, input );
                return true;
            }
            catch( Exception e )
            {
                JobResult r = new JobResult( e );
                _ticket.Result = r;
                _ticket.State = JobState.Error;
                _ticket.OnJobError( e );
                return false;
            }
        }


        private TicketPipelineRunner _runner;
        private WorkerArgs _currentArgs;
        private JobTicket _ticket;
        private bool _cancel;
        private object _cancelPadlock;
    }
}
