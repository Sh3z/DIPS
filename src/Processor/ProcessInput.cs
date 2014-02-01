using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor
{
    public class ProcessInput
    {
        public ProcessInput( Image input )
        {
            if( input == null )
            {
                throw new ArgumentNullException( "input" );
            }

            Image = input;
        }

        public Image Image
        {
            get;
            private set;
        }

        public object Identifier
        {
            get;
            set;
        }

        public bool HasIdentifier
        {
            get
            {
                return Identifier != null;
            }
        }
    }
}
