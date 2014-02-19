using DIPS.Util.Compression;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor.Client.JobDeployment
{
    [Serializable]
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

        public ICompressor Compressor
        {
            get;
            set;
        }

        public object Identifier
        {
            get;
            set;
        }
    }
}
