using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Unity
{
    /// <summary>
    /// Represents the possible modes of an <see cref="IFilePickerService"/>.
    /// </summary>
    public enum FilePickerMode
    {
        /// <summary>
        /// A file will be created using the picker
        /// </summary>
        Create,

        /// <summary>
        /// A file will be opened using the picker
        /// </summary>
        Open
    }

    /// <summary>
    /// Represents the service providing the ability to select a path
    /// to save a file.
    /// </summary>
    public interface IFilePickerService
    {
        /// <summary>
        /// Sets the mode of this <see cref="IFilePickerService"/>.
        /// </summary>
        FilePickerMode Mode
        {
            set;
        }

        /// <summary>
        /// Gets the selected path, or an empty string if no path is
        /// selected.
        /// </summary>
        string Path
        {
            get;
        }

        /// <summary>
        /// Sets the default name to give to the new file.
        /// </summary>
        string DefaultName
        {
            set;
        }

        /// <summary>
        /// Sets the default extension to give to the new file.
        /// </summary>
        string DefaultExtension
        {
            set;
        }

        /// <summary>
        /// Sets the filter of the service
        /// </summary>
        string Filter
        {
            set;
        }

        /// <summary>
        /// Performs the path-selection routine.
        /// </summary>
        /// <returns>true if a path was selected, false otherwise.</returns>
        bool SelectPath();
    }
}
