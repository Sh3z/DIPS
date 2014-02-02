using DIPS.Processor.Client.JobDeployment;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace DIPS.Processor.Client
{
    /// <summary>
    /// Represents a process request from the client. This class cannot be inherited.
    /// </summary>
    public sealed class JobRequest
    {
        public JobRequest( IJobDefinition job )
        {
            if( job == null )
            {
                throw new ArgumentNullException( "job" );
            }

            Job = job;
        }



        public IJobDefinition Job
        {
            get;
            private set;
        }
    }
}
