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
    public class FilePickerService : IFilePickerService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FilePickerService"/> class.
        /// </summary>
        public FilePickerService()
        {
            Path = string.Empty;
        }


        /// <summary>
        /// Sets the mode of this <see cref="IFilePickerService"/>.
        /// </summary>
        public FilePickerMode Mode
        {
            get;
            set;
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
            FileDialog dialog = _createDialog();
            return _showDialog( dialog );
        }


        /// <summary>
        /// Creates an appropriate dialog for the current mode
        /// </summary>
        /// <returns>A FileDialog for browsing the filesystem</returns>
        private FileDialog _createDialog()
        {
            FileDialog dialog = null;
            if( Mode == FilePickerMode.Create )
            {
                dialog = new SaveFileDialog();
                dialog.FileName = DefaultName;
                dialog.DefaultExt = DefaultExtension;
            }
            else
            {
                dialog = new OpenFileDialog();
            }

            dialog.Filter = Filter;
            return dialog;
        }

        /// <summary>
        /// Shows the dialog and performs the appropriate operation
        /// on the path.
        /// </summary>
        /// <param name="dialog">The dialog to display</param>
        /// <returns>true if a path was selected</returns>
        private bool _showDialog( FileDialog dialog )
        {
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
