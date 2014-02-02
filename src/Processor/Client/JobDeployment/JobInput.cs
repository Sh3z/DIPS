using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor.Client.JobDeployment
{
    public sealed class JobInput
    {
        public JobInput( Image input )
        {
            if( input == null )
            {
                throw new ArgumentNullException( "input" );
            }

            Input = input;
        }


        public Image Input
        {
            get;
            private set;
        }

        public object Identifier
        {
            get;
            set;
        }
    }
}
