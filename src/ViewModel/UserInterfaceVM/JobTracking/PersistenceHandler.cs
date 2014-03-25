using DIPS.Processor.Client;
using DIPS.Processor.Client.JobDeployment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.ViewModel.UserInterfaceVM.JobTracking
{
    /// <summary>
    /// Represents the <see cref="IJobResultsHandler"/> used to persist
    /// results to a data-store. This class is abstract.
    /// </summary>
    public abstract class PersistenceHandler : IJobResultsHandler
    {
        /// <summary>
        /// Handles the results of a finised job.
        /// </summary>
        /// <param name="completeJob">The <see cref="IJobTicket"/> representing
        /// the finished job.</param>
        public void HandleResults( IJobTicket completeJob )
        {
            JobResult result = completeJob.Result;
            if( result.Result == JobState.Complete )
            {
                _saveImages( completeJob.Request.Job.GetInputs(), result.Images );
            }
        }


        /// <summary>
        /// Creates a copy of this <see cref="PersistenceHandler"/>.
        /// </summary>
        /// <returns>A new <see cref="PersistenceHandler"/> that is a copy
        /// of this <see cref="PersistenceHandler"/>.</returns>
        public abstract object Clone();


        /// <summary>
        /// Saves the output from the processor to the data-store. This is invoked
        /// when no corresponding input is found.
        /// </summary>
        /// <param name="output">The output from the processor.</param>
        protected abstract void Save( IProcessedImage output );

        /// <summary>
        /// Saves the output from the processor to the data-store.
        /// </summary>
        /// <param name="input">The input object provided to the processor.</param>
        /// <param name="output">The output from the processor.</param>
        protected abstract void Save( JobInput input, IProcessedImage output );


        /// <summary>
        /// Saves the provided set of images to the database
        /// </summary>
        /// <param name="inputs">The corresponding inputs</param>
        /// <param name="images">The images to be saved</param>
        private void _saveImages( IEnumerable<JobInput> inputs, ProcessedImageSet images )
        {
            foreach( IProcessedImage image in images )
            {
                // Locate the input with the same ID for linking.
                var matchingInput = ( from input in inputs
                                      where input.Identifier == image.Identifier
                                      select input ).FirstOrDefault();
                if( matchingInput == null )
                {
                    Save( image );
                }
                else
                {
                    Save( matchingInput, image );
                }
            }
        }
    }
}
