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
    /// filesystem.
    /// </summary>
    public class FileSystemPersister : IJobPersister
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FileSystemPersister"/>
        /// class.
        /// </summary>
        /// <param name="ticket">The <see cref="JobTicket"/> this persister
        /// will save jobs for.</param>
        /// <param name="dir">The directory to save jobs to.</param>
        /// <exception cref="ArgumentException">dir is null or empty.</exception>
        /// <exception cref="ArgumentNullException">ticket is null.</exception>
        /// <exception cref="IOException">the directory provided does not exist,
        /// and this persister cannot create it.</exception>
        public FileSystemPersister( JobTicket ticket, string dir )
        {
            if( string.IsNullOrEmpty( dir ) )
            {
                throw new ArgumentException( "dir" );
            }

            if( ticket == null )
            {
                throw new ArgumentNullException( "ticket" );
            }

            if( Directory.Exists( dir ) == false )
            {
                _createDirectory( dir );
            }

            _ticket = ticket;
            TargetDirectory = dir;
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
        /// Gets the path to the directory this <see cref="FileSystemPersister"/>
        /// will save files to.
        /// </summary>
        public string TargetDirectory
        {
            get;
            private set;
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
            Guid jobGuid = _ticket.JobID;
            string fullPath = _resolvePath( identifier, jobGuid );
            using( Stream stream = File.Create( fullPath ) )
            {
                output.Save( stream, ImageFormat.Png );
            }
        }

        /// <summary>
        /// Loads a particular object back from the storage this
        /// <see cref="IJobPersister"/> is maintaining a connection to.
        /// </summary>
        /// <param name="identifier">The identifier of the particular
        /// image to reload.</param>
        /// <returns>The <see cref="PersistedResult"/> of the image represented
        /// by the identifier, or null if no image with the given identifier
        /// exists.</returns>
        public PersistedResult Load( object identifier )
        {
            IEnumerable<PersistedResult> results = Load();
            if( results.Any() )
            {
                return results.FirstOrDefault( x => x.RestoredIdentifier.Equals( identifier ) );
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Loads all persisted results  from the storage this
        /// <see cref="IJobPersister"/> is maintaining a connection to.
        /// </summary>
        /// <returns>A set of <see cref="PersistedResult"/>s from the job this
        /// <see cref="IJobPersister"/> has previously persisted.</returns>
        public IEnumerable<PersistedResult> Load()
        {
            ICollection<PersistedResult> results = new List<PersistedResult>();
            string dir = string.Format( @"{0}/{{{1}}}", TargetDirectory, _ticket.JobID );
            if( Directory.Exists( dir ) )
            {
                results = _loadFromDir( dir );
            }

            return results;
        }


        /// <summary>
        /// Loads all previous results from a given directory.
        /// </summary>
        /// <param name="dir">The directory to load from</param>
        /// <returns>An enumerable set of previous results.</returns>
        private ICollection<PersistedResult> _loadFromDir( string dir )
        {
            ICollection<PersistedResult> results = new List<PersistedResult>();
            var files = Directory.EnumerateFiles( dir, "*.png" );
            foreach( string fileName in files )
            {
                PersistedResult result = _createResult( fileName );
                if( result != null )
                {
                    results.Add( result );
                }
            }

            return results;
        }

        /// <summary>
        /// Attempts to create a result for the given file.
        /// </summary>
        /// <param name="fileName">The path to the file</param>
        /// <returns>A PersistedResult instance, or null if the file
        /// cannot be converted.</returns>
        private PersistedResult _createResult( string fileName )
        {
            try
            {
                Image img = null;
                using( Stream stream = File.Open( fileName, FileMode.Open ) )
                {
                    img = Image.FromStream(stream);
                    img = new Bitmap(img);
                }
                
                string id = Path.GetFileNameWithoutExtension( fileName );
                int idAsInt = 0;
                bool parsed = int.TryParse( id, out idAsInt );
                if( parsed )
                {
                    return new PersistedResult( img, idAsInt );
                }
                else
                {
                    return new PersistedResult( img, id );
                }
            }
            catch
            {
                return null;
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
            string jobPath = string.Format( @"{0}/{{{1}}}", TargetDirectory, jobGuid );
            if( Directory.Exists( jobPath ) == false )
            {
                _createDirectory( jobPath );
            }

            string fileName = identifier != null ? identifier.ToString() : null;
            if( string.IsNullOrEmpty( fileName ) )
            {
                fileName = string.Format( @"{0}", _id );
            }

            _id++;
            return string.Format( @"{0}/{1}.png", jobPath, fileName );
        }

        /// <summary>
        /// Creates a directory at the given path.
        /// </summary>
        /// <param name="dir">The path to the directory to create.</param>
        private void _createDirectory( string dir )
        {
            try
            {
                Directory.CreateDirectory( dir );
            }
            catch( Exception e )
            {
                string err = string.Format( "Cannot create directory \"{0}\"", dir );
                throw new IOException( err, e );
            }
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
