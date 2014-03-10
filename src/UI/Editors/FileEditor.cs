using DIPS.Unity.Implementations;
using DIPS.Util.Commanding;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using Xceed.Wpf.Toolkit.PropertyGrid;
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;

namespace DIPS.UI.Editors
{
    /// <summary>
    /// Represents the editor used to choose files.
    /// </summary>
    public class FileEditor : CommandingEditor
    {
        /// <summary>
        /// Creates and returns the <see cref="ICommand"/> applicable
        /// to this <see cref="CommandingEditor"/>.
        /// </summary>
        /// <returns>The <see cref="ICommand"/> used by this
        /// <see cref="CommandingEditor"/>.</returns>
        public override ICommand CreateCommand()
        {
            if( _dialogCommand == null )
            {
                _dialogCommand = new RelayCommand( _showDialog );
            }

            return _dialogCommand;
        }

        /// <summary>
        /// Creates and returns a <see cref="FrameworkElement"/>
        /// used to present the information within this
        /// <see cref="CommandingEditor"/>.
        /// </summary>
        /// <returns>A <see cref="FrameworkElement"/> used to present
        /// the information within this
        /// <see cref="CommandingEditor"/>.</returns>
        public override FrameworkElement CreateUI()
        {
            if( _fileDisplayTextBox == null )
            {
                _fileDisplayTextBox = new TextBox();
            }

            return _fileDisplayTextBox;
        }


        /// <summary>
        /// Runs the commanding logic
        /// </summary>
        /// <param name="parameter">Not used</param>
        private void _showDialog( object parameter )
        {
            FilePickerService s = new FilePickerService();
            if( s.SelectPath() )
            {
                _fileDisplayTextBox.Text = s.Path;
                Property.Value = s.Path;
            }
        }


        /// <summary>
        /// Contains the command used to present the dialog
        /// </summary>
        private ICommand _dialogCommand;

        /// <summary>
        /// Contains the TextBox used to display the current file.
        /// </summary>
        private TextBox _fileDisplayTextBox;
    }
}
