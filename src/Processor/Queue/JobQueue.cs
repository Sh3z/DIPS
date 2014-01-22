using DIPS.Processor.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor.Queue
{
    public class JobQueue : IJobQueue
    {
        public JobQueue()
        {
            _internalQueue = new Queue<IJobTicket>();
        }

        public event EventHandler JobAdded;

        public int NumberOfJobs
        {
            get
            {
                lock( this )
                {
                    return _internalQueue.Count;
                }
            }
        }

        public bool HasPendingJobs
        {
            get
            {
                lock( this )
                {
                    return _internalQueue.Any();
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
                _internalQueue.Enqueue( req );
                notifyJobAdded();
            }
        }

        public IJobTicket Dequeue()
        {
            lock( this )
            {
                if( _internalQueue.Any() )
                {
                    return _internalQueue.Dequeue();
                }
                else
                {
                    return null;
                }
            }
        }


        private void notifyJobAdded()
        {
            if( JobAdded != null )
            {
                JobAdded( this, EventArgs.Empty );
            }
        }

        private Queue<IJobTicket> _internalQueue;
    }
}
