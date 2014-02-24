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
    public class TicketPipelineRunner : ITicketCancellationHandler
    {
        public TicketPipelineRunner( JobTicket ticket, IPipelineFactory factory )
        {
            _cancel = false;
            _cancelPadlock = new object();
            _ticket = ticket;
            PipelineDefinition d = ticket.Request.Job.GetAlgorithms();
            _pipeline = factory.CreatePipeline( d );
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
                if( _ticket == ticket )
                {
                    _cancel = true;
                }
                else if( !_cancel && Successor != null )
                {
                    _cancel = Successor.Handle( ticket );
                }
            }

            return _cancel;
        }

        public void Run( IJobPersister persister, JobInput input )
        {
            Image theInput = _processInput( input );
            if( theInput != null )
            {
                persister.Persist( _ticket.JobID, theInput, input.Identifier );
            }
        }

        private Image _processInput( JobInput input )
        {
            Image theInput = input.Input;
            foreach( PipelineEntry entry in _pipeline )
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


        private JobTicket _ticket;
        private Pipeline.Pipeline _pipeline;
        private bool _cancel;
        private object _cancelPadlock;
    }
}
