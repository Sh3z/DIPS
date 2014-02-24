using DIPS.Processor.Client;
using DIPS.Processor.Persistence;
using DIPS.Processor.Pipeline;
using DIPS.Processor.Worker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DIPS.Processor.Executor
{
    public class QueueExecutor : IDisposable
    {
        public QueueExecutor( IJobQueue queue )
        {
            if( queue == null )
            {
                throw new ArgumentNullException( "queue" );
            }

            AutoStart = false;
            _stop = false;
            _queue = queue;
            _queue.JobAdded += job_added;
        }


        void IDisposable.Dispose()
        {
            Stop();
        }


        public event EventHandler ExhaustedQueue;

        public bool AutoStart
        {
            get;
            set;
        }

        public IJobPersister Persister
        {
            get;
            set;
        }

        public IPluginFactory PluginFactory
        {
            get;
            set;
        }

        public IWorker Worker
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
                    if( Worker == null )
                    {
                        throw new InvalidOperationException( "No worker provided" );
                    }

                    if( PluginFactory == null )
                    {
                        throw new InvalidOperationException( "No plugin factory provided" );
                    }

                    if( Persister == null )
                    {
                        throw new InvalidOperationException( "No persister provided" );
                    }

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
                WorkerArgs args = new WorkerArgs( Persister, new PluginPipelineFactory( PluginFactory ) );
                args.Ticket = ticket;
                Worker.Work( args );
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
