using DIPS.Processor.Client;
using DIPS.Processor.Client.JobDeployment;
using DIPS.Processor.Persistence;
using DIPS.Processor.Pipeline;
using DIPS.Processor.Plugin;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor.Worker
{
    /// <summary>
    /// Represents the basic <see cref="IWorker"/> used to run jobs.
    /// </summary>
    public class TicketWorker : IWorker
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TicketWorker"/>
        /// class.
        /// </summary>
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
                if( _ticket != null )
                {
                    _cancel = true;
                }
            }

            return _cancel;
        }

        /// <summary>
        /// Performs the job work procedure.
        /// </summary>
        /// <param name="args">The information required to complete the
        /// job.</param>
        public void Work( WorkerArgs args )
        {
            _currentArgs = args;
            _ticket = _currentArgs.Ticket as JobTicket;
            _ticket.OnJobStarted();
            _runJob( _ticket.Request.Job );

            _currentArgs = null;
            _ticket = null;
        }


        /// <summary>
        /// Runs the job specified by the definition
        /// </summary>
        /// <param name="job">The definition of the job to run</param>
        private void _runJob( IJobDefinition job )
        {
            PipelineDefinition d = job.GetAlgorithms();
            Pipeline.Pipeline pipeline = _currentArgs.PipelineFactory.CreatePipeline( d );
            foreach( JobInput input in job.GetInputs() )
            {
                if( _handleNextInput( pipeline, input ) == false )
                {
                    return;
                }
            }

            var results = _currentArgs.Persister.Load( _ticket.JobID );
            _ticket.Result = new JobResult( results );
            _ticket.State = JobState.Complete;
            _ticket.OnJobCompleted();
        }

        /// <summary>
        /// Processes a single input from the job
        /// </summary>
        /// <param name="pipeline">The Pipeline to use in processing</param>
        /// <param name="input">The input to be processed</param>
        /// <returns>true if the process is completed successfully</returns>
        private bool _handleNextInput( Pipeline.Pipeline pipeline, JobInput input )
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

            return _tryRunInput( pipeline, input );
        }

        /// <summary>
        /// Attempts to run an input
        /// </summary>
        /// <param name="pipeline">The pipeline to use in processing</param>
        /// <param name="input">The input to process</param>
        /// <returns>true if the input is processed without error;
        /// false otherwise</returns>
        private bool _tryRunInput( Pipeline.Pipeline pipeline, JobInput input )
        {
            try
            {
                Image theInput = _processInput( pipeline, input );
                if( theInput != null )
                {
                    _currentArgs.Persister.Persist(
                        _ticket.JobID, theInput, input.Identifier );
                }

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

        /// <summary>
        /// Performs the processing procedure against an input
        /// </summary>
        /// <param name="pipeline">The processing pipeline</param>
        /// <param name="input">The input to be processed</param>
        /// <returns>The output from the pipeline, or null if the process
        /// is cancelled</returns>
        private Image _processInput( Pipeline.Pipeline pipeline, JobInput input )
        {
            Image theInput = input.Input;
            foreach( PipelineEntry entry in pipeline )
            {
                lock( _cancelPadlock )
                {
                    if( _cancel )
                    {
                        return null;
                    }
                }

                AlgorithmPlugin plugin = entry.Process;
                plugin.Input = theInput;
                plugin.Run( entry.ProcessInput );
                theInput = plugin.Output;
            }

            return theInput;
        }


        /// <summary>
        /// Contains the current argument object
        /// </summary>
        private WorkerArgs _currentArgs;

        /// <summary>
        /// Contains the current ticket object
        /// </summary>
        private JobTicket _ticket;

        /// <summary>
        /// Contains a value indicating whether this worker should abort
        /// </summary>
        private bool _cancel;

        /// <summary>
        /// Contains a thread-safe 
        /// </summary>
        private object _cancelPadlock;
    }
}
