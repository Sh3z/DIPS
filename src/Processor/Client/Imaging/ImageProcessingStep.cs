using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor.Client.Imaging
{
    /// <summary>
    /// Represents a step of an <see cref="Algorithm"/> used to execute an image process.
    /// </summary>
    public class ImageProcessingStep : IAlgorithmStep
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ImageProcessingStep"/> class with
        /// the <see cref="IImageProcess"/> to be executed when this step is ran.
        /// </summary>
        /// <param name="process">The <see cref="IImageProcess"/> that will be executed
        /// when the <see cref="Algorithm"/> executes this step.</param>
        public ImageProcessingStep( IImageProcess process )
        {
            if( process == null )
            {
                throw new ArgumentNullException( "process" );
            }

            Process = process;
        }

        /// <summary>
        /// Gets the <see cref="IImageProcess"/> that will be executed when this step of
        /// an algorithm is executed.
        /// </summary>
        public IImageProcess Process
        {
            get;
            private set;
        }

        /// <summary>
        /// Executes this step of the algorithm with the information available within the
        /// <see cref="JobState"/>.
        /// </summary>
        /// <param name="stateOfJob">An instance of the <see cref="JobState"/> class used
        /// to provide this step with execution information.</param>
        public void Run( JobState stateOfJob )
        {
            Bitmap toProcess = stateOfJob.CurrentBitmap;
            stateOfJob.ProcessedBitmap = Process.Execute( toProcess );
        }
    }
}
