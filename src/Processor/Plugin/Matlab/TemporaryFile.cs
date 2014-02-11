using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor.Plugin.Matlab
{
    /// <summary>
    /// Represents a temporary file on-disk for allowing other functions to
    /// access bytes as a file.
    /// </summary>
    public class TemporaryFile : IDisposable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TemporaryFile"/> class
        /// that will output a file into the current directory.
        /// </summary>
        /// <param name="fileName">The name (with extension) to give to the new
        /// file.</param>
        /// <param name="fileContents">The raw contents to provide to the
        /// file.</param>
        /// <exception cref="ArgumentException">fileName is null or empty.</exception>
        /// <exception cref="ArgumentNullException">fileContents is null.</exception>
        /// <exception cref="IOException">file cannot be created within the current
        /// directory.</exception>
        public TemporaryFile( string fileName, byte[] fileContents )
        {
            if( string.IsNullOrEmpty( fileName ) )
            {
                throw new ArgumentException( "fileName" );
            }

            if( fileContents == null )
            {
                throw new ArgumentNullException( "fileContents" );
            }

            FilePath = string.Format( @"{0}/{1}", Directory.GetCurrentDirectory(), fileName );
            _saveFile( fileContents );
        }


        /// <summary>
        /// Gets the temporary path given to the file.
        /// </summary>
        public string FilePath
        {
            get;
            private set;
        }


        /// <summary>
        /// Disposes of the resources held by this <see cref="TemporaryFile"/> -
        /// i.e. deletes the file.
        /// </summary>
        public void Dispose()
        {
            if( File.Exists( FilePath ) )
            {
                File.Delete( FilePath );
            }
        }


        /// <summary>
        /// Saves the contents of the file to the current path.
        /// </summary>
        /// <param name="file">The bytes to save to the file.</param>
        private void _saveFile( byte[] file )
        {
            try
            {
                File.WriteAllBytes( FilePath, file );
            }
            catch( Exception e )
            {
                throw new IOException( "Unable to save file. See inner exception.", e );
            }
        }
    }
}
