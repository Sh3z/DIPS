using DIPS.Processor.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor.Worker
{
    public interface IWorker : ITicketCancellationHandler
    {
        void Work( WorkerArgs args );
    }
}
