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
        public TicketWorker( IPluginFactory factory )
        {
            if( factory == null )
            {
                throw new ArgumentNullException( "factory" );
            }

            _factory = factory;
        }


        public void Work( IJobTicket req )
        {
            JobTicket ticket = req as JobTicket;
            ticket.OnJobStarted();

            JobBuilder builder = new JobBuilder( _factory );
            builder.Persister = new FileSystemPersister( ticket, FileSystemPersister.OutputDataPath );
            builder.ApplyDefinition( ticket.Request.Job );
            if( builder.Build() )
            {
                _runJob( builder.Job, ticket );
            }
            else
            {
                ticket.OnJobError();
            }
        }


        private void _runJob( Job job, JobTicket ticket )
        {
            if( job.Run() )
            {
                ticket.OnJobCompleted();
            }
            else
            {
                //Exception err = job.Exception;
                ticket.OnJobError();
            }
        }


        private IPluginFactory _factory;
    }
}
