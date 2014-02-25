using DIPS.Processor.Client;
using DIPS.Processor.Persistence;
using DIPS.Processor.Queue;
using DIPS.Processor.Worker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor
{
    /// <summary>
    /// Represents the underlying batch processing module
    /// </summary>
    public class BatchProcessor : IDisposable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BatchProcessor"/>
        /// class.
        /// </summary>
        /// <param name="factory">The <see cref="IPluginFactory"/> to use
        /// when converting definitions into jobs.</param>
        /// <param name="persister">The <see cref="IJobPersister"/> to save job
        /// results to</param>
        /// <exception cref="ArgumentNullException">factory or persister are
        /// null</exception>
        public BatchProcessor( IPluginFactory factory, IJobPersister persister )
        {
            if( factory == null )
            {
                throw new ArgumentNullException( "factory" );
            }

            if( persister == null )
            {
                throw new ArgumentNullException( "persister" );
            }

            _queue = new JobQueue();
            _executor = new QueueExecutor( _queue );
            _executor.Worker = new TicketWorker();
            _executor.PluginFactory = factory;
            _executor.Persister = persister;
        }


        void IDisposable.Dispose()
        {
            StopProcessing();
        }


        /// <summary>
        /// Gets a value indicating whether this <see cref="BatchProcessor"/>
        /// is currently processing.
        /// </summary>
        public bool IsProcessing
        {
            get
            {
                return _executor.IsRunning;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="BatchProcessor"/>
        /// has jobs yet to finish.
        /// </summary>
        public int PendingJobs
        {
            get
            {
                return _queue.NumberOfJobs;
            }
        }


        /// <summary>
        /// Enqueues a new job into the processor.
        /// </summary>
        /// <param name="req">The <see cref="JobRequest"/> describing
        /// the job</param>
        /// <returns>A <see cref="IJobTicket"/> providing information
        /// about the job's entry within the processor.</returns>
        public IJobTicket Enqueue( JobRequest req )
        {
            JobTicket ticket = new JobTicket( req, _queue );
            _queue.Enqueue( ticket );
            ticket.State = JobState.InQueue;
            return ticket;
        }

        /// <summary>
        /// Begins background processing of jobs.
        /// </summary>
        public void StartProcessing()
        {
            _executor.Start();
        }

        /// <summary>
        /// Halts background processing of jobs.
        /// </summary>
        public void StopProcessing()
        {
            _executor.Stop();
        }


        /// <summary>
        /// Contains the queue maintaining the set of jobs to run.
        /// </summary>
        private JobQueue _queue;

        /// <summary>
        /// Contains the object used to sequentially dequeue and run jobs
        /// </summary>
        private QueueExecutor _executor;
    }
}
