using Database.Repository;
using DIPS.Database;
using DIPS.Processor.Client;
using DIPS.Processor.Client.JobDeployment;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.ViewModel.UserInterfaceVM.JobTracking
{
    /// <summary>
    /// Represents the <see cref="IJobResultsHandler"/> used to save
    /// results to the database.
    /// </summary>
    public class SaveResultsHandler : IJobResultsHandler
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
                    _saveWithoutIdentifier( image );
                }
                else
                {
                    _saveWithIdentifier( image, matchingInput );
                }
            }
        }

        /// <summary>
        /// Saves the processed image to the database without relating it to
        /// a stored input
        /// </summary>
        /// <param name="image">The processed image to save</param>
        private void _saveWithoutIdentifier( IProcessedImage image )
        {
            // Todo for Joe 
            readImage reader = new readImage();
            byte[] blob = reader.ImageToByteArray(image.Output);

            ProcessedImageRepository processed = new ProcessedImageRepository();
            processed.saveImage(null, blob);
        }

        /// <summary>
        /// Saves the processed image to the database with its input
        /// </summary>
        /// <param name="image">The image to be saved</param>
        /// <param name="input">The corresponding input</param>
        private void _saveWithIdentifier( IProcessedImage image, JobInput input )
        {
            // Todo for Joe
            FileInfo file = (FileInfo)image.Identifier;
            readImage reader = new readImage();
            byte[] blob = reader.ImageToByteArray(image.Output);

            ProcessedImageRepository processed = new ProcessedImageRepository();
            processed.saveImage(file,blob);
        }
    }
}
