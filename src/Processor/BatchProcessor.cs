using DIPS.Processor.Client;
using DIPS.Processor.Executor;
using DIPS.Processor.Persistence;
using DIPS.Processor.Queue;
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
            _queue = new JobQueue();
            _executor = new QueueExecutor( _queue, new TicketWorker( factory, persister ) );
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
