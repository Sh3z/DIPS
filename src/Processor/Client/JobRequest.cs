using System;
using System.Collections.Generic;

namespace DIPS.Processor.Client
{
    /// <summary>
    /// Represents a process request from the client. This class cannot be inherited.
    /// </summary>
    public sealed class JobRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JobRequest"/> class with the
        /// <see cref="Algorithm"/> to use when processing each image returned by the
        /// enumerator.
        /// </summary>
        /// <param name="algorithm">The <see cref="Algorithm"/> to use when processing
        /// each image.</param>
        /// <param name="images">The collection of <see cref="IImageSource"/>s to be
        /// processed.</param>
        /// <exception cref="ArgumentNullException">algorithm or images are
        /// null.</exception>
        public JobRequest( Algorithm algorithm, IEnumerable<IImageSource> images )
        {
            if( algorithm == null )
            {
                throw new ArgumentNullException( "algorithm" );
            }

            if( images == null )
            {
                throw new ArgumentNullException( "images" );
            }

            Algorithm = algorithm;
            Images = images;
        }


        /// <summary>
        /// Gets the <see cref="Algorithm"/> to use when processing each input image.
        /// </summary>
        public Algorithm Algorithm
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the enumerable set of <see cref="IImageSource"/>s encapsulating each image
        /// to process.
        /// </summary>
        public IEnumerable<IImageSource> Images
        {
            get;
            private set;
        }
    }
}
