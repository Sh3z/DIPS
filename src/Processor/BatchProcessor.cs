using DIPS.Processor.Client;
using DIPS.Processor.Executor;
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
        public BatchProcessor()
        {
            _queue = new JobQueue();
            _executor = new QueueExecutor( _queue, new JobWorker() );
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

        public Task<JobResult> Process( JobRequest req )
        {
            JobTicket ticket = new JobTicket( req, _queue );
            return _executor.ProcessSync( ticket );
        }

        private JobQueue _queue;
        private QueueExecutor _executor;
    }
}
