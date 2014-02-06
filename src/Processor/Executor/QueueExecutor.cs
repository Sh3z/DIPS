using DIPS.Processor.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DIPS.Processor.Executor
{
    public class QueueExecutor
    {
        public QueueExecutor( IJobQueue queue, IWorker worker )
        {
            if( queue == null )
            {
                throw new ArgumentNullException( "queue" );
            }

            if( worker == null )
            {
                throw new ArgumentNullException( "worker" );
            }

            AutoStart = false;
            _stop = false;
            _queue = queue;
            _worker = worker;
            _queue.JobAdded += job_added;
        }

        public event EventHandler ExhaustedQueue;

        public bool AutoStart
        {
            get;
            set;
        }

        public bool IsRunning
        {
            get
            {
                lock( this )
                {
                    return _jobThread != null && _jobThread.IsAlive;
                }
            }
        }

        public void Start()
        {
            lock( this )
            {
                if( IsRunning == false )
                {
                    _jobThread = new Thread( run );
                    _jobThread.Start();
                }
            }
        }

        public void Stop()
        {
            lock( this )
            {
                if( IsRunning )
                {
                    _stop = true;
                }
            }
        }

        public Task<JobResult> ProcessSync( IJobTicket toProcess )
        {
            Func<object, JobResult> function = new Func<object, JobResult>( process_sync );
            return Task<JobResult>.Factory.StartNew( function, toProcess );
        }

        private JobResult process_sync( object ticket )
        {
            IJobTicket theTicket = ticket as IJobTicket;
            _worker.Work( theTicket );
            return theTicket.Result;
        }


        private void job_added( object sender, EventArgs e )
        {
            lock( this )
            {
                if( IsRunning == false && AutoStart )
                {
                    Start();
                }
            }
        }

        private void run()
        {
            while( _stop == false )
            {
                if( _queue.HasPendingJobs )
                {
                    run_next_job();
                }
                else
                {
                    notify_exhausted();
                    break;
                }
            }

            _stop = false;
            _jobThread = null;
        }

        private void run_next_job()
        {
            IJobTicket req = _queue.Dequeue();
            run_job( req );
        }

        private void run_job( IJobTicket ticket )
        {
            if( ticket.Cancelled == false )
            {
                _worker.Work( ticket );
            }
        }

        private void notify_exhausted()
        {
            if( ExhaustedQueue != null )
            {
                ExhaustedQueue( this, EventArgs.Empty );
            }
        }


        private IJobQueue _queue;
        private IWorker _worker;
        private Thread _jobThread;
        private bool _stop;
    }
}
