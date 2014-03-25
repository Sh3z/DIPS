using DIPS.Matlab;
using DIPS.UI.Editors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Design;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor.Plugin.Matlab.Parameters
{
    /// <summary>
    /// Represents a file value to be provided to a script.
    /// </summary>
    public class FileValue : IParameterValue
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FileValue"/> class.
        /// </summary>
        public FileValue()
        {
            _file = new MemoryFile();
        }


        /// <summary>
        /// Gets or sets the path to the file to be sent to the script.
        /// </summary>
        [Description( "The path to the file to provide to the script. " +
            "Refer to the workspace variable for the true path during script execution." )]
        [Editor( typeof( FileEditor ), typeof( UITypeEditor ) )]
        public string Path
        {
            get
            {
                return _file.Path;
            }
            set
            {
                _file.Path = value;
                _file.Refresh();
            }
        }
        [DebuggerBrowsable( DebuggerBrowsableState.Never )]
        private MemoryFile _file;

        /// <summary>
        /// Gets a value indicating whether this <see cref="FileValue"/> is
        /// in possession of a valid value.
        /// </summary>
        [Browsable( true )]
        [Description( "Indicates if the selected file has been loaded correctly" )]
        public bool IsValid
        {
            get
            {
                return _file.RawCopy.Length > 0;
            }
        }

        /// <summary>
        /// Gets the raw copy of the file.
        /// </summary>
        [Browsable( false )]
        public byte[] Bytes
        {
            get
            {
                return _file.RawCopy;
            }
            set
            {
                _file.RawCopy = value ?? new byte[0];
            }
        }


        /// <summary>
        /// Puts the value of this <see cref="IParameterValue"/> into the
        /// Matlab instance provided.
        /// </summary>
        /// <param name="name">The name to give to the value placed
        /// in the workspace.</param>
        /// <param name="workspace">The <see cref="Workspace"/> the
        /// value of this <see cref="IParameterValue"/> should be set.</param>
        public void Put( string name, Workspace workspace )
        {
            _createTmpDirIfNecessary();
            _writeFileToTmpDir( name );
            workspace.PutObject( name, _tmpPath );
        }

        void IDisposable.Dispose()
        {
            if( File.Exists( _tmpPath ) )
            {
                File.Delete( _tmpPath );
            }
        }


        /// <summary>
        /// Creates the temporary directory if it has not yet been created.
        /// </summary>
        private void _createTmpDirIfNecessary()
        {
            string dir = string.Format( @"{0}/tmp", Directory.GetCurrentDirectory() );
            if( Directory.Exists( dir ) == false )
            {
                Directory.CreateDirectory( dir );
            }
        }

        /// <summary>
        /// Writes the contents of the file to the temporary directory
        /// </summary>
        /// <param name="name">The name given to the script variable for the
        /// file</param>
        private void _writeFileToTmpDir( string name )
        {
            string ext = System.IO.Path.GetExtension( Path );
            _tmpPath = string.Format( @"{0}/tmp/{1}{2}", Directory.GetCurrentDirectory(), name, ext );
            File.WriteAllBytes( _tmpPath, _file.RawCopy );
        }


        /// <summary>
        /// Contains the path to the file in the temporary directory.
        /// </summary>
        private string _tmpPath;
    }
}
