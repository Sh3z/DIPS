using DIPS.Processor.Client;
using DIPS.Processor.Persistence;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor.Executor
{
    public class TicketWorker : IWorker
    {
        public TicketWorker( IPluginFactory factory, IJobPersister persister )
        {
            if( factory == null )
            {
                throw new ArgumentNullException( "factory" );
            }

            if( persister == null )
            {
                throw new ArgumentNullException( "persister" );
            }

            _factory = factory;
            _persister = persister;
        }


        public void Work( IJobTicket req )
        {
            JobTicket ticket = req as JobTicket;
            ticket.OnJobStarted();

            JobBuilder builder = new JobBuilder( _factory );
            builder.Persister = _persister;
            builder.ApplyDefinition( ticket.Request.Job );
            if( builder.Build() )
            {
                _runJob( builder.Job, ticket );
            }
            else
            {
                Exception err = new Exception( "Error constructing job from definition." );
                ticket.Result = new JobResult( err );
                ticket.OnJobError();
            }
        }


        private void _runJob( Job job, JobTicket ticket )
        {
            if( job.Run() )
            {
                IEnumerable<PersistedResult> results = _persister.Load( ticket.JobID );
                ticket.Result = new JobResult( results );
                ticket.OnJobCompleted();
            }
            else
            {
                Exception err = job.Exception;
                ticket.Result = new JobResult( err );
                ticket.OnJobError();
            }
        }


        private IPluginFactory _factory;
        private IJobPersister _persister;
    }
}
