using DIPS.Processor.Client;
using DIPS.Processor.Persistence;
using DIPS.Processor.Pipeline;
using DIPS.Processor.Registry;
using DIPS.Processor.Worker;
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
            ProcessPluginRepository algRepo = new ProcessPluginRepository();
            PipelineXmlRepository pipeRepo = new PipelineXmlRepository();
            RegistryCache.Cache.Initialize( pipeRepo );
            RegistryCache.Cache.Initialize( algRepo );
            _pluginFactory = new RegistryFactory( algRepo );
            _persister = new FileSystemPersister( FileSystemPersister.OutputDataPath );
            JobManager = new JobManager( _pluginFactory, _persister );
            PipelineManager = new PipelineManager( pipeRepo, algRepo );
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
        /// Creates and returns a new <see cref="ISynchronousProcessor"/> clients
        /// can use to compute jobs on their own threads.
        /// </summary>
        /// <returns>An <see cref="ISynchronousProcessor"/> instance for running
        /// jobs within the client.</returns>
        public ISynchronousProcessor CreateSynchronousProcessor()
        {
            IJobPersister persister = new MemoryPersister();
            IWorker worker = new TicketWorker();
            var processor = new SynchronousProcessor( worker );
            processor.Factory = new PluginPipelineFactory( _pluginFactory );
            return processor;
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


        /// <summary>
        /// Maintains the current persister for saving and loading job results
        /// </summary>
        private IJobPersister _persister;

        /// <summary>
        /// Maintains the current pipeline factory component
        /// </summary>
        private IPluginFactory _pluginFactory;
    }
}
