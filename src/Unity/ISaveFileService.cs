using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Unity
{
    /// <summary>
    /// Represents the service providing the ability to select a path
    /// to save a file.
    /// </summary>
    public interface ISaveFileService
    {
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
