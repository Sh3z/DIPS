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
    /// Represents an editor which uses a command to request input
    /// from the user. This class is abstract.
    /// </summary>
    public abstract class CommandingEditor : ITypeEditor
    {
        /// <summary>
        /// Gets the <see cref="PropertyItem"/> used within this
        /// <see cref="CommandingEditor"/>.
        /// </summary>
        protected PropertyItem Property
        {
            get;
            private set;
        }


        /// <summary>
        /// Creates and returns the <see cref="ICommand"/> applicable
        /// to this <see cref="CommandingEditor"/>.
        /// </summary>
        /// <returns>The <see cref="ICommand"/> used by this
        /// <see cref="CommandingEditor"/>.</returns>
        public abstract ICommand CreateCommand();

        /// <summary>
        /// Creates and returns a <see cref="FrameworkElement"/>
        /// used to present the information within this
        /// <see cref="CommandingEditor"/>.
        /// </summary>
        /// <returns>A <see cref="FrameworkElement"/> used to present
        /// the information within this
        /// <see cref="CommandingEditor"/>.</returns>
        public abstract FrameworkElement CreateUI();

        /// <summary>
        /// Creates and returns an editor used to edit a value.
        /// </summary>
        /// <param name="propertyItem">The property item within the
        /// property grid.</param>
        /// <returns>The editor to use when editing the value</returns>
        public FrameworkElement ResolveEditor( PropertyItem propertyItem )
        {
            Property = propertyItem;
            DockPanel container = new DockPanel();
            container.LastChildFill = true;
            Button b = new Button();
            b.Content = "...";
            b.Command = CreateCommand();
            b.Margin = new Thickness( 3, 0, 3, 0 );
            b.MinWidth = 33;
            DockPanel.SetDock( b, Dock.Right );

            FrameworkElement e = CreateUI();
            container.Children.Add( b );
            container.Children.Add( e );
            return container;
        }
    }
}
