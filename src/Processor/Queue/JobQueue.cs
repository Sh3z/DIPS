using DIPS.Processor.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor.Queue
{
    public class JobQueue : IJobQueue, ITicketCancellationHandler
    {
        public JobQueue()
        {
            _internalCollection = new List<IJobTicket>();
        }

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
            if( _internalCollection.Contains( ticket ) )
            {
                _internalCollection.Remove( ticket );
                return true;
            }
            else if( Successor != null )
            {
                return Successor.Handle( ticket );
            }
            else
            {
                return false;
            }
        }


        private void notifyJobAdded()
        {
            if( JobAdded != null )
            {
                JobAdded( this, EventArgs.Empty );
            }
        }

        private IList<IJobTicket> _internalCollection;
    }
}
