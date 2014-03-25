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
    [Serializable]
    public class FileSystemPersister : IJobPersister
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FileSystemPersister"/>
        /// class.
        /// </summary>
        /// <param name="dir">The directory to save jobs to.</param>
        /// <exception cref="ArgumentException">dir is null or empty.</exception>
        /// <exception cref="IOException">the directory provided does not exist,
        /// and this persister cannot create it.</exception>
        public FileSystemPersister( string dir )
        {
            if( string.IsNullOrEmpty( dir ) )
            {
                throw new ArgumentException( "dir" );
            }

            if( Directory.Exists( dir ) == false )
            {
                _createDirectory( dir );
            }

            _identifiers = new List<PersistedJobIdentifiers>();
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
        /// Gets the generated identifier for the input.
        /// </summary>
        /// <param name="jobID">The identifier of the job the input was saved
        /// against.</param>
        /// <param name="originalIdentifier">The original identifier given to the
        /// input.</param>
        /// <returns>The unique identifier for the input provided by the
        /// persister.</returns>
        public Guid GetPersistedIdentifier( Guid jobID, object originalIdentifier )
        {
            PersistedJobIdentifiers id = _getOrCreateIDSet( jobID );
            Guid? theID = id.GetIdentifier( originalIdentifier );
            if( theID.HasValue == false )
            {
                throw new ArgumentException( "Result not persisted with identifier" );
            }

            return theID.Value;
        }


        /// <summary>
        /// Persits the output of a job.
        /// </summary>
        /// <param name="jobID">The unique identifier of the job to save
        /// the image against.</param>
        /// <param name="output">The <see cref="Image"/> generated from a
        /// complete job.</param>
        /// <param name="identifier">The identifier for the input provided
        /// by the client.</param>
        public void Persist( Guid jobID, Image output, object identifier )
        {
            PersistedJobIdentifiers id = _getOrCreateIDSet( jobID );
            Guid inputID = _createIdentifier( identifier, id );
            string idAsString = inputID.ToString();
            string fullPath = _resolvePath( idAsString, jobID );
            using( Stream stream = File.Create( fullPath ) )
            {
                output.Save( stream, ImageFormat.Png );
            }
        }

        

        /// <summary>
        /// Loads a particular object back from the storage this
        /// <see cref="IJobPersister"/> is maintaining a connection to.
        /// </summary>
        /// <param name="jobID">The unique identifier of the job to load the
        /// resilts for.</param>
        /// <param name="identifier">The identifier of the particular
        /// image to reload.</param>
        /// <returns>The <see cref="PersistedResult"/> of the image represented
        /// by the identifier, or null if no image with the given identifier
        /// exists.</returns>
        public PersistedResult Load( Guid jobID, object identifier )
        {
            IEnumerable<PersistedResult> results = Load( jobID );
            if( results.Any() )
            {
                return results.FirstOrDefault( x => x.Identifier.Equals( identifier ) );
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
        /// <param name="jobID">The unique identifier of the job to load the
        /// resilts for.</param>
        /// <returns>A set of <see cref="PersistedResult"/>s from the job this
        /// <see cref="IJobPersister"/> has previously persisted.</returns>
        public IEnumerable<PersistedResult> Load( Guid jobID )
        {
            ICollection<PersistedResult> results = new List<PersistedResult>();
            string dir = string.Format( @"{0}/{{{1}}}", TargetDirectory, jobID );
            if( Directory.Exists( dir ) )
            {
                results = _loadFromDir( jobID, dir );
            }

            return results;
        }

        /// <summary>
        /// Deletes all the results from a particular job from the storage.
        /// </summary>
        /// <param name="jobID">The unique identifier of the job to delete
        /// the results for.</param>
        /// <returns><c>true</c> if the results from the job were deleted
        /// successfully; <c>false</c> otherwise.</returns>
        public bool Delete( Guid jobID )
        {
            string dir = string.Format( @"{0}/{{{1}}}", TargetDirectory, jobID );
            if( Directory.Exists( dir ) )
            {
                Directory.Delete( dir, true );
                PersistedJobIdentifiers id = _getOrCreateIDSet( jobID );
                _identifiers.Remove( id );
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Deletes a result from a particular job with the associated identifier
        /// from the storage.
        /// </summary>
        /// <param name="jobID">The unique identifier of the job to delete
        /// the results for.</param>
        /// <param name="identifier">The identifier given to the input to be
        /// deleted.</param>
        /// <returns><c>true</c> if the result from the job was deleted
        /// successfully; <c>false</c> otherwise.</returns>
        public bool Delete( Guid jobID, object identifier )
        {
            PersistedJobIdentifiers id = _getOrCreateIDSet( jobID );
            Guid? theID = id.GetIdentifier( identifier );
            if( theID.HasValue == false )
            {
                return false;
            }

            string path = string.Format( @"{0}/{{{1}}}/{2}.png", TargetDirectory, jobID, theID.Value.ToString() );
            if( File.Exists( path ) )
            {
                File.Delete( path );
                id.RemoveOriginalIdentifier( identifier );
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// Gets or creates the id set object for the job
        /// </summary>
        /// <param name="jobGuid">The job to retrieve the set for</param>
        /// <returns>The job ID set object</returns>
        private PersistedJobIdentifiers _getOrCreateIDSet( Guid jobGuid )
        {
            PersistedJobIdentifiers id = _identifiers.FirstOrDefault( x => x.JobID == jobGuid );
            if( id == null )
            {
                id = new PersistedJobIdentifiers( jobGuid );
                _identifiers.Add( id );
            }

            return id;
        }

        /// <summary>
        /// Creates an identifier in the set and returns it for the given
        /// raw id
        /// </summary>
        /// <param name="identifier">The identifier provided by the client</param>
        /// <param name="id">The id set to add to</param>
        /// <returns>The created id for the provided identifier</returns>
        private Guid _createIdentifier( object identifier, PersistedJobIdentifiers id )
        {
            if( identifier == null )
            {
                return id.CreateIdentifier();
            }
            else
            {
                return id.CreateIdentifier( identifier );
            }
        }

        /// <summary>
        /// Loads all previous results from a given directory.
        /// </summary>
        /// <param name="jobID">The identifier of the job represented by
        /// the directory</param>
        /// <param name="dir">The directory to load from</param>
        /// <returns>An enumerable set of previous results.</returns>
        private ICollection<PersistedResult> _loadFromDir( Guid jobID, string dir )
        {
            ICollection<PersistedResult> results = new List<PersistedResult>();
            var files = Directory.EnumerateFiles( dir, "*.png" );
            foreach( string fileName in files )
            {
                PersistedResult result = _createResult( jobID, fileName );
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
        /// <param name="jobID">The id of the job the result came from</param>
        /// <param name="fileName">The path to the file</param>
        /// <returns>A PersistedResult instance, or null if the file
        /// cannot be converted.</returns>
        private PersistedResult _createResult( Guid jobID, string fileName )
        {
            try
            {
                Image img = _loadImage( fileName );
                string id = Path.GetFileNameWithoutExtension( fileName );
                Guid idAsGuid = Guid.Parse( id );
                PersistedJobIdentifiers ids = _getOrCreateIDSet( jobID );
                object theID = ids.GetOriginalIdentifier( idAsGuid );
                return new PersistedResult( img, theID );
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Loads the Image from the file
        /// </summary>
        /// <param name="path">The path to the file containing the image</param>
        /// <returns>The Image object represented by the file</returns>
        private Image _loadImage( string path )
        {
            using( Stream stream = File.Open( path, FileMode.Open ) )
            {
                Image tmp = Image.FromStream( stream );
                return new Bitmap( tmp );
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
        /// Retains the current id for jobs with no identifiers.
        /// </summary>
        private int _id;

        /// <summary>
        /// Maintains the set of identifiers for each job.
        /// </summary>
        private ICollection<PersistedJobIdentifiers> _identifiers;
    }
}
