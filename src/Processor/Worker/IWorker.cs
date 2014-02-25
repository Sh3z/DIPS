using DIPS.Processor.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor.Worker
{
    /// <summary>
    /// Represents the object used to execute job work.
    /// </summary>
    public interface IWorker : ITicketCancellationHandler
    {
        /// <summary>
        /// Performs the job work procedure.
        /// </summary>
        /// <param name="args">The information required to complete the
        /// job.</param>
        void Work( WorkerArgs args );
    }
}
