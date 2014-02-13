using DIPS.Processor.Client;
using DIPS.Processor.Executor;
using DIPS.Processor.Persistence;
using DIPS.Processor.Registry;
using DIPS.Processor.XML;
using DIPS.Processor.XML.Compilation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DIPS.Processor
{
    /// <summary>
    /// Represents an instance of the processing service used by DIPS.
    /// </summary>
    public class ProcessingService : IProcessingService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessingService"/>
        /// class.
        /// </summary>
        public ProcessingService()
        {
            JobManager = new JobManager( new RegistryFactory() );
            PipelineManager = new PipelineManager();
        }


        /// <summary>
        /// Gets the <see cref="IJobManager"/> module of this
        /// <see cref="IProcessingService"/>.
        /// </summary>
        public IJobManager JobManager
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the <see cref="IPipelineManager"/> module of this
        /// <see cref="IProcessingService"/>.
        /// </summary>
        public IPipelineManager PipelineManager
        {
            get;
            private set;
        }

        /// <summary>
        /// Returns an enumerable set of <see cref="AlgorithmDefinition"/>s in
        /// object form.
        /// </summary>
        /// <returns>The set of <see cref="AlgorithmDefinition"/>s this service can
        /// understand.</returns>
        public IEnumerable<AlgorithmDefinition> GetAlgorithmDefinitions()
        {
            return RegistryCache.Cache.KnownAlgorithms;
        }

        /// <summary>
        /// Creates and returns a new <see cref="ISynchronousProcessor"/> clients
        /// can use to compute jobs on their own threads.
        /// </summary>
        /// <returns>An <see cref="ISynchronousProcessor"/> instance for running
        /// jobs within the client.</returns>
        public ISynchronousProcessor CreateSynchronousProcessor()
        {
            IJobPersister persister = new MemoryPersister();
            IWorker worker = new TicketWorker( new RegistryFactory(), persister );
            return new SynchronousProcessor( worker );
        }

        /// <summary>
        /// Resolves the <see cref="JobResult"/> for a previously executed job.
        /// </summary>
        /// <param name="ticket">The <see cref="IJobTicket"/> representing the
        /// job.</param>
        /// <returns>The <see cref="JobResult"/> created through the execution of
        /// the job represented by the <see cref="IJobTicket"/>.</returns>
        public JobResult GetResult( IJobTicket ticket )
        {
            return null;
        }
    }
}
