using DIPS.Processor.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor
{
    public interface IJobQueue
    {
        event EventHandler JobAdded;

        bool HasPendingJobs
        {
            get;
        }

        void Enqueue( IJobTicket req );

        IJobTicket Dequeue();
    }
}
