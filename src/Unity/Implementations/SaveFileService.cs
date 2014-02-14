using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Unity.Implementations
{
    /// <summary>
    /// Represents the service providing the ability to select a path
    /// to save a file.
    /// </summary>
    public class SaveFileService : ISaveFileService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SaveFileService"/> class.
        /// </summary>
        public SaveFileService()
        {
            Path = string.Empty;
        }

        /// <summary>
        /// Gets the selected path, or an empty string if no path is
        /// selected.
        /// </summary>
        public string Path
        {
            get;
            private set;
        }

        /// <summary>
        /// Sets the default name to give to the new file.
        /// </summary>
        public string DefaultName
        {
            get;
            set;
        }

        /// <summary>
        /// Sets the default extension to give to the new file.
        /// </summary>
        public string DefaultExtension
        {
            get;
            set;
        }

        /// <summary>
        /// Sets the filter of the service
        /// </summary>
        public string Filter
        {
            get;
            set;
        }

        /// <summary>
        /// Performs the path-selection routine.
        /// </summary>
        /// <returns>true if a path was selected, false otherwise.</returns>
        public bool SelectPath()
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.FileName = DefaultName;
            dialog.DefaultExt = DefaultExtension;
            dialog.Filter = Filter;
            bool? result = dialog.ShowDialog();

            if( result.HasValue && result.Value )
            {
                Path = dialog.FileName;
                return true;
            }
            else
            {
                Path = string.Empty;
                return false;
            }
        }
    }
}
