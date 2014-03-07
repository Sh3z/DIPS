using Database.Repository;
using DIPS.Database;
using DIPS.Processor.Client;
using DIPS.Processor.Client.JobDeployment;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    [DisplayName( "Save to Database" )]
    [Handler( "Database" )]
    public class SaveToDatabaseHandler : PersistenceHandler
    {
        /// <summary>
        /// Saves the output from the processor to the data-store. This is invoked
        /// when no corresponding input is found.
        /// </summary>
        /// <param name="output">The output from the processor.</param>
        protected override void Save( IProcessedImage output )
        {
            readImage reader = new readImage();
            byte[] blob = reader.ImageToByteArray( output.Output );

            ProcessedImageRepository processed = new ProcessedImageRepository();
            processed.saveImage( null, blob );
        }

        /// <summary>
        /// Saves the output from the processor to the data-store.
        /// </summary>
        /// <param name="input">The input object provided to the processor.</param>
        /// <param name="output">The output from the processor.</param>
        protected override void Save( JobInput input, IProcessedImage output )
        {
            FileInfo file = (FileInfo)output.Identifier;
            readImage reader = new readImage();
            byte[] blob = reader.ImageToByteArray( output.Output );

            ProcessedImageRepository processed = new ProcessedImageRepository();
            processed.saveImage( file, blob );
        }
    }
}
