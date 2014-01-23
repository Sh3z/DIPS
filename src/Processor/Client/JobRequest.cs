using System;
using System.Collections.Generic;
using System.Xml.Linq;

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
        /// Initializes a new instance of the <see cref="JobRequest"/> class with the
        /// <see cref="XDocument"/> describing the processing to execute.
        /// </summary>
        /// <param name="job">The <see cref="XDocument"/> specifying how to run the job.</param>
        public JobRequest( XDocument job )
        {
            if( job == null )
            {
                throw new ArgumentNullException( "job" );
            }

            JobXML = job;
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
        /// Gets the <see cref="XDocument"/> to use by the job system.
        /// </summary>
        public XDocument JobXML
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
