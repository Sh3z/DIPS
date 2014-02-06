using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor.Client
{
    /// <summary>
    /// Represents the information returned by the service upon a complete job.
    /// </summary>
    public class JobResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JobResult"/> class.
        /// </summary>
        /// <param name="images">The set of processed images returned by
        /// the service.</param>
        /// <exception cref="ArgumentNullException">images is null.</exception>
        public JobResult( IEnumerable<IProcessedImage> images )
        {
            if( images == null )
            {
                throw new ArgumentNullException( "images" );
            }

            Images = new ProcessedImageSet( images );
        }

        /// <summary>
        /// Gets the <see cref="ProcessedImageSet"/> containing the results of
        /// each processed image.
        /// </summary>
        public ProcessedImageSet Images
        {
            get;
            private set;
        }
    }
}
