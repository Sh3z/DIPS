using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Xceed.Wpf.Toolkit;
using Xceed.Wpf.Toolkit.PropertyGrid;
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;

namespace DIPS.UI.Editors
{
    /// <summary>
    /// Represents the <see cref="ITypeEditor"/> used to modify a collection.
    /// This class is abstract.
    /// </summary>
    public abstract class CollectionEditor : ITypeEditor
    {
        /// <summary>
        /// Gets the set of <see cref="Type"/>s that can be created and added to the
        /// collection.
        /// </summary>
        public abstract IList<Type> NewItemTypes
        {
            get;
        }


        /// <summary>
        /// Creates and returns a <see cref="FrameworkElement"/> allowing editing
        /// of the provided <see cref="PropertyItem"/> as a collection.
        /// </summary>
        /// <param name="propertyItem">The <see cref="PropertyItem"/> to be treated
        /// as a collection.</param>
        /// <returns>A <see cref="FrameworkElement"/> representing a collection
        /// editor.</returns>
        public FrameworkElement ResolveEditor( PropertyItem propertyItem )
        {
            _property = propertyItem;

            DockPanel container = new DockPanel();
            container.LastChildFill = true;

            Button openDialogButton = new Button();
            openDialogButton.Content = "...";
            openDialogButton.Click += new RoutedEventHandler( _onButtonClick );
            openDialogButton.MinWidth = 25;
            DockPanel.SetDock( openDialogButton, Dock.Right );

            _collectionInformationTextBox = new TextBox();
            _collectionInformationTextBox.Text = "(Collection)";
            _collectionInformationTextBox.IsReadOnly = true;
            

            container.Children.Add( openDialogButton );
            container.Children.Add( _collectionInformationTextBox );

            return container;
        }


        /// <summary>
        /// Occurs when the button used to select files has been clicked.
        /// </summary>
        /// <param name="sender">N/A</param>
        /// <param name="e">N/A</param>
        private void _onButtonClick( object sender, RoutedEventArgs e )
        {
            CollectionControlDialog dialog = new CollectionControlDialog( _property.PropertyType, NewItemTypes );
            Binding binding = new Binding( "Value" );
            binding.Source = _property;
            binding.Mode = _property.IsReadOnly ? BindingMode.OneWay : BindingMode.TwoWay;
            BindingOperations.SetBinding( dialog, CollectionControlDialog.ItemsSourceProperty, binding );
            dialog.ShowDialog();
        }



        private TextBox _collectionInformationTextBox;

        private PropertyItem _property;
    }
}
