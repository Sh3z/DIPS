using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Unity
{
    /// <summary>
    /// Represents the service used to pick a directory.
    /// </summary>
    public interface IDirectoryPicker
    {
        /// <summary>
        /// Gets the full path to the directory chosen by the user.
        /// </summary>
        string Directory
        {
            get;
        }

        /// <summary>
        /// Requests the resolution of the directory.
        /// </summary>
        /// <returns>true if a directory is resolved, false
        /// otherwise</returns>
        bool Resolve();
    }
}
