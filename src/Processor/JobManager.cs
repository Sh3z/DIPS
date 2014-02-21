using DIPS.Processor.Client;
using DIPS.Processor.Persistence;
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
        public JobManager( IPluginFactory pluginFactory, IJobPersister persister )
        {
            _factory = pluginFactory;
            _persister = persister;
            _processor = new BatchProcessor( pluginFactory, _persister );
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
            _tickets.Add( ticket );
            if( _processor.IsProcessing == false )
            {
                _processor.StartProcessing();
            }

            return ticket;
        }

        /// <summary>
        /// Deletes the results of a previously complete job.
        /// </summary>
        /// <param name="ticket">The <see cref="IJobTicket"/> representing the
        /// job to be deleted.</param>
        /// <returns><c>true</c> if the results associated with the
        /// <see cref="JobTicket"/> have been deleted.</returns>
        public bool DeleteResults( IJobTicket ticket )
        {
            if( _tickets.Contains( ticket ) && ticket is JobTicket )
            {
                return _persister.Delete( ( (JobTicket)ticket ).JobID );
            }
            else
            {
                return false;
            }
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
            JobTicket theTicket = ticket as JobTicket;
            Guid jobID = theTicket.JobID;
            var results = _persister.Load( jobID );
            if( results.Any() )
            {
                return new JobResult( results );
            }
            else
            {
                return new JobResult( new ArgumentException( "Job results not present" ) );
            }
        }


        /// <summary>
        /// Contains the persister to use for saving/deleting jobs.
        /// </summary>
        private IJobPersister _persister;

        /// <summary>
        /// Contains a set of all tickets created.
        /// </summary>
        private ICollection<IJobTicket> _tickets;

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
