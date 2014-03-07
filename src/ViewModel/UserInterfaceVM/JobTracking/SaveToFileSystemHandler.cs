using DIPS.Processor.Client;
using DIPS.Processor.Client.JobDeployment;
using DIPS.UI.Editors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.ViewModel.UserInterfaceVM.JobTracking
{
    /// <summary>
    /// Represents the <see cref="IJobResultsHandler"/> used to save
    /// job results to the local file-system.
    /// </summary>
    [DisplayName( "Save to File System" )]
    [Handler( "FileSystem" )]
    public class SaveToFileSystemHandler : PersistenceHandler, INotifyPropertyChanged
    {
        /// <summary>
        /// Occurs when the value of a property changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets or sets the directory to save the results to.
        /// </summary>
        [Description( "The directory to output the results to" )]
        [Editor( typeof( FileEditor ), typeof( UITypeEditor ) )]
        public string OutputDirectory
        {
            get
            {
                return _directory;
            }
            set
            {
                _directory = value;
                if( PropertyChanged != null )
                {
                    PropertyChanged( this, new PropertyChangedEventArgs( "Directory" ) );
                }
            }
        }
        [DebuggerBrowsable( DebuggerBrowsableState.Never )]
        private string _directory;


        /// <summary>
        /// Saves the output from the processor to the data-store. This is invoked
        /// when no corresponding input is found.
        /// </summary>
        /// <param name="output">The output from the processor.</param>
        protected override void Save( IProcessedImage output )
        {
            _validateDirectories();
            _saveOutput( output );
        }

        /// <summary>
        /// Saves the output from the processor to the data-store.
        /// </summary>
        /// <param name="input">The input object provided to the processor.</param>
        /// <param name="output">The output from the processor.</param>
        protected override void Save( JobInput input, IProcessedImage output )
        {
            _validateDirectories();

            string path = string.Format( @"{0}/Input/{1}", OutputDirectory, _coerceIdentifier( input.Identifier ) );
            using( Stream s = File.Create( path ) )
            {
                input.Input.Save( s, ImageFormat.Png );
            }

            _saveOutput( output );
        }


        /// <summary>
        /// Ensures we've been given a directory and that it exists.
        /// </summary>
        private void _validateDirectories()
        {
            if( string.IsNullOrEmpty( OutputDirectory ) )
            {
                // Can't do much with this.
                throw new InvalidOperationException( "No directory specified." );
            }
            else
            {
                if( Directory.Exists( OutputDirectory ) == false )
                {
                    Directory.CreateDirectory( OutputDirectory );
                    string outputs = string.Format( @"{0}/{1}", OutputDirectory, "Output" );
                    string inputs = string.Format( @"{0}/{1}", OutputDirectory, "Input" );
                    Directory.CreateDirectory( outputs );
                    Directory.CreateDirectory( inputs );
                }
            }
        }

        /// <summary>
        /// Saves the output to the target directory
        /// </summary>
        /// <param name="output">The ouput to be saved</param>
        private void _saveOutput( IProcessedImage output )
        {
            string path = string.Format( @"{0}/Output/{1}", OutputDirectory, _coerceIdentifier( output.Identifier ) );
            using( Stream s = File.Create( path ) )
            {
                output.Output.Save( s, ImageFormat.Png );
            }
        }

        /// <summary>
        /// Bit of a hack. Makes sure the identifier is legal.
        /// </summary>
        /// <param name="identifier">The identifier</param>
        /// <returns>A safe form of the identifier.</returns>
        private object _coerceIdentifier( object identifier )
        {
            if( identifier is FileInfo )
            {
                FileInfo id = (FileInfo)identifier;
                return Path.GetFileNameWithoutExtension( id.Name );
            }
            
            // No idea
            return identifier;
        }
    }
}
