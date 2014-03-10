using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DIPS.Unity.Implementations
{
    /// <summary>
    /// Represents the service used to pick a directory.
    /// </summary>
    public class DirectoryPicker : IDirectoryPicker
    {
        /// <summary>
        /// Gets the full path to the directory chosen by the user.
        /// </summary>
        public string Directory
        {
            get;
            private set;
        }

        /// <summary>
        /// Requests the resolution of the directory.
        /// </summary>
        /// <returns>true if a directory is resolved, false
        /// otherwise</returns>
        public bool Resolve()
        {
            FolderBrowserDialog d = new FolderBrowserDialog();
            DialogResult r = d.ShowDialog();
            bool success = r == DialogResult.OK;
            if( success )
            {
                Directory = d.SelectedPath;
            }
            else
            {
                Directory = string.Empty;
            }

            return success;
        }
    }
}
