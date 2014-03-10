using DIPS.Unity.Implementations;
using DIPS.Util.Commanding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Xceed.Wpf.Toolkit.PropertyGrid;
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;

namespace DIPS.UI.Editors
{
    /// <summary>
    /// Represents the editor used to specify directories
    /// </summary>
    public class DirectoryEditor : CommandingEditor
    {
        /// <summary>
        /// Creates and returns the <see cref="ICommand"/> applicable
        /// to this <see cref="CommandingEditor"/>.
        /// </summary>
        /// <returns>The <see cref="ICommand"/> used by this
        /// <see cref="CommandingEditor"/>.</returns>
        public override ICommand CreateCommand()
        {
            if( _command == null )
            {
                _command = new RelayCommand( _pickDirectory );
            }

            return _command;
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
            if( _directoryDisplayTextBox == null )
            {
                _directoryDisplayTextBox = new TextBox();
            }

            return _directoryDisplayTextBox;
        }


        /// <summary>
        /// Runs the commanding logic
        /// </summary>
        /// <param name="parameter">N/A</param>
        private void _pickDirectory( object sender )
        {
            DirectoryPicker p = new DirectoryPicker();
            if( p.Resolve() )
            {
                _directoryDisplayTextBox.Text = p.Directory;
                Property.Value = p.Directory;
            }
        }


        /// <summary>
        /// Contains the command provided to the base class
        /// </summary>
        private ICommand _command;

        /// <summary>
        /// Contains the TextBox used to display the current directory.
        /// </summary>
        private TextBox _directoryDisplayTextBox;
    }
}
