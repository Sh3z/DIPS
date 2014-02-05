using DIPS.Processor.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor.Queue
{
    /// <summary>
    /// Represents the job queue and cancellation handler.
    /// </summary>
    public class JobQueue : IJobQueue, ITicketCancellationHandler
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JobQueue"/> class.
        /// </summary>
        public JobQueue()
        {
            _internalCollection = new List<IJobTicket>();
        }


        /// <summary>
        /// Occurs when a job is added to this <see cref="IJobQueue"/>.
        /// </summary>
        public event EventHandler JobAdded;

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
        /// Gets the number of jobs remaining within this <see cref="IJobQueue"/>.
        /// </summary>
        public int NumberOfJobs
        {
            get
            {
                lock( this )
                {
                    return _internalCollection.Count;
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="IJobQueue"/>
        /// has more jobs to process.
        /// </summary>
        public bool HasPendingJobs
        {
            get
            {
                lock( this )
                {
                    return _internalCollection.Any();
                }
            }
        }

        /// <summary>
        /// Enqueues a new job to this <see cref="IJobQueue"/>.
        /// </summary>
        /// <param name="req">The <see cref="IJobTicket"/> representing the
        /// job information.</param>
        public void Enqueue( IJobTicket req )
        {
            if( req == null )
            {
                return;
            }

            lock( this )
            {
                _internalCollection.Add( req );
                notifyJobAdded();
            }
        }

        /// <summary>
        /// Dequeues the next job to execute.
        /// </summary>
        /// <returns>The <see cref="IJobTicket"/> representing the
        /// job.</returns>
        public IJobTicket Dequeue()
        {
            lock( this )
            {
                if( _internalCollection.Any() )
                {
                    IJobTicket ticket = _internalCollection.First();
                    _internalCollection.RemoveAt( 0 );
                    return ticket;
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Handles the cancellation of the job represented by the ticket.
        /// </summary>
        /// <param name="ticket">The <see cref="IJobTicket"/> that has been
        /// cancelled.</param>
        /// <returns>true if the request has been handled; false otherwise.</returns>
        public bool Handle( IJobTicket ticket )
        {
            lock( this )
            {
                if( Successor != null )
                {
                    return Successor.Handle( ticket );
                }
                else
                {
                    return _selfHandleCancellation( ticket );
                }
            }
        }


        /// <summary>
        /// Executes the cancellation-handling code determined by this class.
        /// </summary>
        /// <param name="ticket">The ticket to cancel</param>
        /// <returns>true if the cancellation was handled, false otherwise.</returns>
        private bool _selfHandleCancellation( IJobTicket ticket )
        {
            if( _internalCollection.Contains( ticket ) )
            {
                _internalCollection.Remove( ticket );
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Notifies any listeners that a job has been added.
        /// </summary>
        private void notifyJobAdded()
        {
            if( JobAdded != null )
            {
                JobAdded( this, EventArgs.Empty );
            }
        }


        /// <summary>
        /// Contains the internal queue. We use a list to allow deletion.
        /// </summary>
        private IList<IJobTicket> _internalCollection;
    }
}
