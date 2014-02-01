using DIPS.Processor.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor
{
    /// <summary>
    /// Represents an outstanding job to be processed.
    /// </summary>
    public class ProcessingJob
    {
        public ProcessingJob( IJobTicket ticket, Algorithm algorithm )
        {
            if( ticket == null )
            {
                throw new ArgumentNullException( "ticket" );
            }

            if( algorithm == null )
            {
                throw new ArgumentNullException( "algorithm" );
            }

            Ticket - ticket;
            Algorithm = algorithm;
            Inputs = new List<ProcessInput>();
        }


        public IJobTicket Ticket
        {
            get;
            private set;
        }

        public Algorithm Algorithm
        {
            get;
            private set;
        }

        public ICollection<ProcessInput> Inputs
        {
            get;
            private set;
        }
    }
}
