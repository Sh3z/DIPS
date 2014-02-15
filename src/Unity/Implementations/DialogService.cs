using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DIPS.Unity.Implementations
{
    /// <summary>
    /// Represents the service capable of displaying simple dialogs.
    /// </summary>
    public class DialogService : IDialogService
    {
        /// <summary>
        /// Sets the abstract summary of the dialog.
        /// </summary>
        public string Abstract
        {
            get;
            set;
        }

        /// <summary>
        /// Sets the body text of the dialog.
        /// </summary>
        public string Body
        {
            get;
            set;
        }

        /// <summary>
        /// Sets the button(s) to present within the dialog
        /// </summary>
        public MessageBoxButton Button
        {
            get;
            set;
        }

        /// <summary>
        /// Sets the image to present within the dialog.
        /// </summary>
        public MessageBoxImage Image
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the result from displaying the dialog.
        /// </summary>
        public MessageBoxResult Result
        {
            get;
            private set;
        }

        /// <summary>
        /// Presents the dialog modally before returning to the caller.
        /// </summary>
        public void ShowDialog()
        {
            Result = MessageBox.Show( Body, Abstract, Button, Image );
        }
    }
}
