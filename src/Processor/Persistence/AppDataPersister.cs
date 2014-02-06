using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor.Persistence
{
    /// <summary>
    /// Represents the job persister used to save jobs to the Windows
    /// APPDATA directory.
    /// </summary>
    public class AppDataPersister : IJobPersister
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppDataPersister"/>
        /// class.
        /// </summary>
        /// <param name="ticket">The <see cref="JobTicket"/> this persister
        /// will save jobs for.</param>
        /// <exception cref="ArgumentNullException">ticket is null.</exception>
        public AppDataPersister( JobTicket ticket )
        {
            if( ticket == null )
            {
                throw new ArgumentNullException( "ticket" );
            }

            _ticket = ticket;
        }


        /// <summary>
        /// Gets the root directory of the DIPS AppData path.
        /// </summary>
        public static string RootDataPath
        {
            get
            {
                return string.Format( @"{0}/DIPS", Environment.GetFolderPath( Environment.SpecialFolder.ApplicationData ) );
            }
        }

        /// <summary>
        /// Gets the output directory of the DIPS AppData path.
        /// </summary>
        public static string OutputDataPath
        {
            get
            {
                return string.Format( @"{0}/Output", RootDataPath );
            }
        }


        /// <summary>
        /// Persits the output of a job.
        /// </summary>
        /// <param name="output">The <see cref="Image"/> generated from a
        /// complete job.</param>
        /// <param name="identifier">The identifier for the input provided
        /// by the client.</param>
        public void Persist( Image output, object identifier )
        {
            string appDataPath = Environment.GetFolderPath( Environment.SpecialFolder.ApplicationData );
            if( Directory.Exists( appDataPath ) == false )
            {
                throw new IOException( "AppData cannot be resolved." );
            }

            Guid jobGuid = _ticket.JobID;
            string fullPath = _resolvePath( identifier, jobGuid );
            using( Stream stream = File.Create( fullPath ) )
            {
                output.Save( stream, ImageFormat.Png );
            }
        }


        /// <summary>
        /// Resolves the full path to save the image to.
        /// </summary>
        /// <param name="identifier">The identifier provided</param>
        /// <param name="jobGuid">The Guid of the ticket</param>
        /// <returns>The path to the file to save the image to.</returns>
        private string _resolvePath( object identifier, Guid jobGuid )
        {
            string jobPath = string.Format( @"{0}/{{{1}}}", OutputDataPath, jobGuid );
            if( Directory.Exists( jobPath ) == false )
            {
                Directory.CreateDirectory( jobPath );
            }

            string fileName = identifier != null ? identifier.ToString() : null;
            if( string.IsNullOrEmpty( fileName ) )
            {
                fileName = string.Format( @"output_{0}", _id );
                _id++;
            }

            return string.Format( @"{0}/{1}.png", jobPath, fileName );
        }


        /// <summary>
        /// Retains the ticket of the job.
        /// </summary>
        private JobTicket _ticket;

        /// <summary>
        /// Retains the current id for jobs with no identifiers.
        /// </summary>
        private int _id;
    }
}
