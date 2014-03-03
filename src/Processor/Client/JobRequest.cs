using DIPS.Processor.Client.JobDeployment;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace DIPS.Processor.Client
{
    /// <summary>
    /// Represents a process request from the client. This class cannot be inherited.
    /// </summary>
    [Serializable]
    public sealed class JobRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JobRequest"/> class.
        /// </summary>
        /// <param name="job">The <see cref="IJobDefinition"/> containing the
        /// details of the job.</param>
        /// <exception cref="ArgumentNullException">job is null.</exception>
        public JobRequest( IJobDefinition job )
        {
            if( job == null )
            {
                throw new ArgumentNullException( "job" );
            }

            Job = job;
        }


        /// <summary>
        /// Gets or sets a client-side identifier to identify this job.
        /// </summary>
        public object Identifier
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the definition of the job to run.
        /// </summary>
        public IJobDefinition Job
        {
            get;
            private set;
        }
    }
}
