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

namespace DIPS.Processor.Queue
{
    /// <summary>
    /// Represents the object used to dequeue and run elements within
    /// an <see cref="IJobQueue"/>.
    /// </summary>
    public class QueueExecutor : IDisposable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QueueExecutor"/>
        /// class.
        /// </summary>
        /// <param name="queue">The <see cref="IJobQueue"/> this
        /// <see cref="QueueExecutor"/> will execute the elements
        /// within.</param>
        /// <exception cref="ArgumentNullException">queue is null.</exception>
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


        /// <summary>
        /// Occurs when the <see cref="IJobQueue"/> being executed has
        /// run out of elements.
        /// </summary>
        public event EventHandler ExhaustedQueue;


        /// <summary>
        /// Gets or sets whether this <see cref="QueueExecutor"/> should
        /// automatically begin executing when the <see cref="IJobQueue"/>
        /// being monitored has a job added.
        /// </summary>
        public bool AutoStart
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the <see cref="IJobPersister"/> to use when
        /// saving results of completed jobs.
        /// </summary>
        public IJobPersister Persister
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the <see cref="IPluginFactory"/> to use to
        /// resolve pipelines from their definitions.
        /// </summary>
        public IPluginFactory PluginFactory
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the <see cref="IWorker"/> used to perform the
        /// work against jobs.
        /// </summary>
        public IWorker Worker
        {
            get;
            set;
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="QueueExecutor"/>
        /// is executing jobs.
        /// </summary>
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


        /// <summary>
        /// Begins the threaded execution of jobs within the queue.
        /// </summary>
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

        /// <summary>
        /// Stops the threaded execution of jobs within the queue.
        /// </summary>
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


        /// <summary>
        /// Occurs when a job is added to the queue.
        /// </summary>
        /// <param name="sender">N/A</param>
        /// <param name="e">N/A</param>
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

        /// <summary>
        /// Represents the start of the threaded procedure.
        /// </summary>
        private void run()
        {
            while( _stop == false )
            {
                _threadLoop();
                if( _queue.HasPendingJobs )
                {
                    break;
                }
            }

            _stop = false;
            _jobThread = null;
        }

        /// <summary>
        /// Represents a single loop of the processing thread.
        /// </summary>
        private void _threadLoop()
        {
            if( _queue.HasPendingJobs )
            {
                run_next_job();
            }
            else
            {
                notify_exhausted();
            }
        }

        /// <summary>
        /// Executes the next job in the queue.
        /// </summary>
        private void run_next_job()
        {
            IJobTicket req = _queue.Dequeue();
            if( req.Cancelled == false )
            {
                WorkerArgs args = new WorkerArgs( Persister, new PluginPipelineFactory( PluginFactory ) );
                args.Ticket = req;
                Worker.Work( args );
            }
        }

        /// <summary>
        /// Fires the ExhaustedQueue event
        /// </summary>
        private void notify_exhausted()
        {
            if( ExhaustedQueue != null )
            {
                ExhaustedQueue( this, EventArgs.Empty );
            }
        }


        /// <summary>
        /// Contains the queue this executor runs jobs from.
        /// </summary>
        private IJobQueue _queue;
        
        /// <summary>
        /// Contains a reference to the job thead.
        /// </summary>
        private Thread _jobThread;

        /// <summary>
        /// Contains a value indicating whether the thread should stop.
        /// </summary>
        private bool _stop;
    }
}
