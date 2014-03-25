using DIPS.Processor.Client;
using DIPS.Processor.Persistence;
using DIPS.Processor.Pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor.Worker
{
    /// <summary>
    /// Represents the information provided to an <see cref="IWorker"/> to
    /// execute a ticket.
    /// </summary>
    public class WorkerArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WorkerArgs"/> class.
        /// </summary>
        /// <param name="persister">The <see cref="IJobPersister"/> to use
        /// when saving jobs.</param>
        /// <param name="factory">The <see cref="IPipelineFactory"/> to use
        /// to create <see cref="Pipeline"/>s.</param>
        /// <exception cref="ArgumentNullException">persister or factory
        /// are null.</exception>
        public WorkerArgs( IJobPersister persister, IPipelineFactory factory )
        {
            if( persister == null )
            {
                throw new ArgumentNullException( "persister" );
            }

            if( factory == null )
            {
                throw new ArgumentNullException( "factory" );
            }

            Persister = persister;
            PipelineFactory = factory;
        }


        /// <summary>
        /// Gets or sets the <see cref="IJobTicket"/> the <see cref="IWorker"/>
        /// should work with.
        /// </summary>
        public IJobTicket Ticket
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the <see cref="IJobPersister"/> to use when saving jobs.
        /// </summary>
        public IJobPersister Persister
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the <see cref="IPipelineFactory"/> to use to create
        /// <see cref="Pipeline"/>s.
        /// </summary>
        public IPipelineFactory PipelineFactory
        {
            get;
            private set;
        }
    }
}
