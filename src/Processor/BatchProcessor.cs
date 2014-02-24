using DIPS.Processor.Client;
using DIPS.Processor.Executor;
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
    public class BatchProcessor
    {
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


        public bool IsProcessing
        {
            get
            {
                return _executor.IsRunning;
            }
        }

        public int PendingJobs
        {
            get
            {
                return _queue.NumberOfJobs;
            }
        }


        public IJobTicket Enqueue( JobRequest req )
        {
            JobTicket ticket = new JobTicket( req, _queue );
            _queue.Enqueue( ticket );
            ticket.State = JobState.InQueue;
            return ticket;
        }

        public void StartProcessing()
        {
            _executor.Start();
        }

        public void StopProcessing()
        {
            _executor.Stop();
        }


        private JobQueue _queue;
        private QueueExecutor _executor;
    }
}
