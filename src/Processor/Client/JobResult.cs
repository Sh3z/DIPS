using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor.Client
{
    public sealed class JobResult
    {
        public JobResult( Algorithm algorithm, IDictionary<IImageSource, Bitmap> results )
        {
            if( algorithm == null )
            {
                throw new ArgumentNullException( "algorithm" );
            }

            if( results == null )
            {
                throw new ArgumentNullException( "results" );
            }

            Algorithm = algorithm;
            _results = results;
        }

        public Algorithm Algorithm
        {
            get;
            private set;
        }

        public IEnumerable<IImageSource> Images
        {
            get
            {
                return _results.Keys;
            }
        }


        public Bitmap ResultForSource( IImageSource source )
        {
            return _results[source];
        }


        private IDictionary<IImageSource, Bitmap> _results;
    }
}
