using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Matlab
{
    /// <summary>
    /// Retains the contents of a file in memory for transmission.
    /// </summary>
    [Serializable]
    public class MemoryFile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MemoryFile"/> class.
        /// </summary>
        public MemoryFile()
        {
            Path = string.Empty;
            _blankFields();
        }


        /// <summary>
        /// Gets or sets the original path to the file.
        /// </summary>
        public string Path
        {
            get;
            set;
        }

        /// <summary>
        /// Gets a copy of the raw byte information from the file.
        /// </summary>
        public byte[] RawCopy
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="MemoryFile"/> has
        /// made a copy of the associated file.
        /// </summary>
        public bool HasCopy
        {
            get;
            private set;
        }


        /// <summary>
        /// Updates the contents of the RawCopy property with the information
        /// from the file.
        /// </summary>
        public void Refresh()
        {
            if( File.Exists( Path ) )
            {
                _populateFromFile();
            }
            else
            {
                _blankFields();
            }
        }


        /// <summary>
        /// Empties the contents of the copied file, and sets we have yet to
        /// load a file
        /// </summary>
        private void _blankFields()
        {
            RawCopy = new byte[0];
            HasCopy = false;
        }

        /// <summary>
        /// Reads in the bytes from the file. If an exception occurs, the fields
        /// are blanked out
        /// </summary>
        private void _populateFromFile()
        {
            try
            {
                RawCopy = File.ReadAllBytes( Path );
                HasCopy = true;
            }
            catch
            {
                _blankFields();
            }
        }
    }
}
