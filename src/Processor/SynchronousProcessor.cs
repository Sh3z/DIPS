using DIPS.Processor.Client;
using DIPS.Processor.Executor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor
{
    /// <summary>
    /// Executes jobs on the calling thread.
    /// </summary>
    public class SynchronousProcessor : ISynchronousProcessor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SynchronousProcessor"/>
        /// class.
        /// </summary>
        /// <param name="handler">The <see cref="IRequestHandler"/> to execute
        /// jobs against.</param>
        public SynchronousProcessor( IRequestHandler handler )
        {
            if( handler == null )
            {
                throw new ArgumentNullException( "handler" );
            }

            _worker = handler;
        }


        /// <summary>
        /// Processes the provided <see cref="JobRequest"/> and returns the
        /// result. This can be run asynchronously.
        /// </summary>
        /// <param name="request">The <see cref="JobRequest"/> providing
        /// information about the job to run.</param>
        /// <returns>A <see cref="Task"/> which computes the <see cref="JobResult"/>
        /// from the given request.</returns>
        public Task<JobResult> Process( JobRequest request )
        {
            return Task.Factory.StartNew<JobResult>( () => _runJob( request ) );
        }


        /// <summary>
        /// Executes the job and returns the result
        /// </summary>
        /// <param name="req">The job definition to execute.</param>
        /// <returns>The result from processin the job.</returns>
        private JobResult _runJob( JobRequest req )
        {
            JobTicket ticket = new JobTicket( req, null );
            _worker.RunJob( ticket );
            return ticket.Result;
        }


        /// <summary>
        /// Contains the object used to execute the job.
        /// </summary>
        private IRequestHandler _worker;
    }
}
