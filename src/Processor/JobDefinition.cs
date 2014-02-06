using DIPS.Processor.Client.JobDeployment;
using DIPS.Processor.Persistence;
using DIPS.Processor.Plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor
{
    /// <summary>
    /// Represents the information used to run a job.
    /// </summary>
    public class JobDefinition
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JobDefinition"/> class.
        /// </summary>
        /// <param name="jobID">The unique identifier of the job.</param>
        /// <param name="processes">The set of processes to run in the job.</param>
        /// <param name="persister">The persister to use when saving the results of
        /// the job.</param>
        /// <exception cref="ArgumentNullException">processes or persister are null.</exception>
        public JobDefinition( Guid jobID, IEnumerable<AlgorithmPlugin> processes, IJobPersister persister )
        {
            if( processes == null )
            {
                throw new ArgumentNullException( "processes" );
            }

            if( persister == null )
            {
                throw new ArgumentNullException( "persister" );
            }

            Processes = processes;
            Persister = persister;
            Inputs = new List<JobInput>();
        }

        
        /// <summary>
        /// The unique identifier of the job within the processor system.
        /// </summary>
        public Guid JobID
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the set of processes to run as part of the job.
        /// </summary>
        public IEnumerable<AlgorithmPlugin> Processes
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the <see cref="IJobPersister"/> used to persist results from
        /// the job.
        /// </summary>
        public IJobPersister Persister
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets a modifyable collection of <see cref="JobInput"/>s to run.
        /// </summary>
        public ICollection<JobInput> Inputs
        {
            get;
            private set;
        }
    }
}
