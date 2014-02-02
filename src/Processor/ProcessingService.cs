using DIPS.Processor.Client;
using DIPS.Processor.Executor;
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
            _processor = new BatchProcessor();
        }


        /// <summary>
        /// Gets the <see cref="XDocument"/> detailing the currently installed
        /// algorithms.
        /// </summary>
        public XDocument AlgorithmDefinitions
        {
            get
            {
                lock( this )
                {
                    if( _algorithms == null )
                    {
                        _createAlgorithmXml();
                    }
                }

                return _algorithms;
            }
        }
        [DebuggerBrowsable( DebuggerBrowsableState.Never )]
        private XDocument _algorithms;

        /// <summary>
        /// Creates and returns a new <see cref="ISynchronousProcessor"/> clients
        /// can use to compute jobs on their own threads.
        /// </summary>
        /// <returns>An <see cref="ISynchronousProcessor"/> instance for running
        /// jobs within the client.</returns>
        public ISynchronousProcessor CreateSynchronousProcessor()
        {
            return new SynchronousProcessor( new JobWorker() );
        }

        /// <summary>
        /// Enqueues a new job to be executed.
        /// </summary>
        /// <param name="job">The <see cref="JobRequest"/> detailing the
        /// job.</param>
        /// <returns>An <see cref="IJobTicket"/> providing job monitoring and
        /// result-tracking capabilities.</returns>
        public IJobTicket EnqueueJob( JobRequest job )
        {
            IJobTicket ticket = _processor.Enqueue( job );
            if( _processor.IsProcessing == false )
            {
                _processor.StartProcessing();
            }

            return ticket;
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
        /// Creates the Xml describing the installed plugins.
        /// </summary>
        private void _createAlgorithmXml()
        {
            XmlBuilder builder = new XmlBuilder( new DefinitionBuilderProcess() );
            foreach( var algorithm in RegistryCache.Cache.GetLoadedPlugins() )
            {
                builder.Algorithms.Add( algorithm );
            }

            builder.Build();
            _algorithms = builder.Xml;
        }


        /// <summary>
        /// Contains the underlying processing module.
        /// </summary>
        private BatchProcessor _processor;
    }
}
