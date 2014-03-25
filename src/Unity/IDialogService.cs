using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DIPS.Unity
{
    /// <summary>
    /// Represents the service capable of displaying simple dialogs.
    /// </summary>
    public interface IDialogService
    {
        /// <summary>
        /// Sets the abstract summary of the dialog.
        /// </summary>
        string Abstract
        {
            set;
        }

        /// <summary>
        /// Sets the body text of the dialog.
        /// </summary>
        string Body
        {
            set;
        }

        /// <summary>
        /// Sets the button(s) to present within the dialog
        /// </summary>
        MessageBoxButton Button
        {
            set;
        }

        /// <summary>
        /// Sets the image to present within the dialog.
        /// </summary>
        MessageBoxImage Image
        {
            set;
        }

        /// <summary>
        /// Gets the result from displaying the dialog.
        /// </summary>
        MessageBoxResult Result
        {
            get;
        }

        /// <summary>
        /// Presents the dialog modally before returning to the caller.
        /// </summary>
        void ShowDialog();
    }
}
