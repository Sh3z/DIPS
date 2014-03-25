using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        /// Allows private construction of custom job states.
        /// </summary>
        /// <param name="state">The state to expost.</param>
        private JobResult( JobState state )
        {
            Result = state;
            Images = new ProcessedImageSet();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JobResult"/> class,
        /// representing a job that encountered an error.
        /// </summary>
        /// <param name="err">The <see cref="Exception"/> that caused the
        /// job to terminate.</param>
        /// <exception cref="ArgumentNullException">err is null.</exception>
        public JobResult( Exception err )
        {
            if( err == null )
            {
                throw new ArgumentNullException( "err" );
            }

            Exception = err;
            Result = JobState.Error;
            Images = new ProcessedImageSet();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JobResult"/> class,
        /// representing a complete job.
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
            Result = JobState.Complete;
        }


        /// <summary>
        /// Represents the <see cref="JobResult"/> used to express a cancelled
        /// job.
        /// </summary>
        public static JobResult Cancelled
        {
            get
            {
                return _cancelled;
            }
        }
        [DebuggerBrowsable( DebuggerBrowsableState.Never )]
        private static JobResult _cancelled = new JobResult( JobState.Cancelled );

        /// <summary>
        /// Provides an indication as to how the job finished.
        /// </summary>
        public JobState Result
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the <see cref="Exception"/> that caused the job to stop. If the
        /// job completed successfully or was cancelled, this is null.
        /// </summary>
        public Exception Exception
        {
            get;
            private set;
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
