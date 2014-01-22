using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor.Client
{
    /// <summary>
    /// Encapsulates the state of an ongoing job for providing the steps of an algorithm
    /// with information for their application. This class cannot be inherited.
    /// </summary>
    public sealed class JobState
    {
        public JobState( IEnumerable<IAlgorithmStep> previous, Bitmap original, Bitmap current )
        {
            if( previous == null )
            {
                throw new ArgumentNullException( "previous" );
            }

            if( original == null )
            {
                throw new ArgumentNullException( "original" );
            }

            if( current == null )
            {
                throw new ArgumentNullException( "current" );
            }

            PreviousSteps = previous;
            OriginalBitmap = original;
            CurrentBitmap = current;
        }

        public IEnumerable<IAlgorithmStep> PreviousSteps
        {
            get;
            private set;
        }

        public Bitmap OriginalBitmap
        {
            get;
            private set;
        }

        public Bitmap CurrentBitmap
        {
            get;
            private set;
        }

        public Bitmap ProcessedBitmap
        {
            get;
            set;
        }
    }
}
