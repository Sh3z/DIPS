using DIPS.Processor.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor
{
    /// <summary>
    /// Provides management of jobs within a service instance.
    /// </summary>
    public class JobManager : IJobManager
    {
        public JobManager( IPluginFactory pluginFactory )
        {
            _factory = pluginFactory;
            _processor = new BatchProcessor( pluginFactory );
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
        /// Contains the factory providing access to the loaded plugins.
        /// </summary>
        private IPluginFactory _factory;

        /// <summary>
        /// Contains the underlying processing module.
        /// </summary>
        private BatchProcessor _processor;
    }
}
