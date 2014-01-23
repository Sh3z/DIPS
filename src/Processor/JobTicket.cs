﻿using DIPS.Processor.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor
{
    public class JobTicket : IJobTicket
    {
        public JobTicket( JobRequest req )
        {
            if( req == null )
            {
                throw new ArgumentNullException( "req" );
            }

            Request = req;
            Cancelled = false;
        }



        public event EventHandler JobStarted;

        public event EventHandler JobCompleted;


        public bool Cancelled
        {
            get;
            private set;
        }

        public JobRequest Request
        {
            get;
            private set;
        }

        public JobResult Result
        {
            get;
            internal set;
        }

        public void Cancel()
        {
            Cancelled = true;
        }


        internal void OnJobStarted()
        {
            if( JobStarted != null )
            {
                JobStarted( this, EventArgs.Empty );
            }
        }

        internal void OnJobCompleted()
        {
            if( JobCompleted != null )
            {
                JobCompleted( this, EventArgs.Empty );
            }
        }
    }
}