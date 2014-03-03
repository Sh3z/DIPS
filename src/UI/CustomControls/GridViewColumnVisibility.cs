using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DIPS.UI.CustomControls
{
    /// <summary>
    /// Provides attached visibility behaviour for <see cref="GridViewColumn"/>s.
    /// </summary>
    public static class GridViewColumnVisibility
    {
        /// <summary>
        /// Dependency Property initializer.
        /// </summary>
        static GridViewColumnVisibility()
        {
            _originalWidths = new Dictionary<GridViewColumn, double>();

            IsVisibleProperty = DependencyProperty.RegisterAttached(
                "IsVisible", typeof( bool ), typeof( GridViewColumnVisibility ),
                new UIPropertyMetadata( true, _onVisibilityChanged ) );
        }


        /// <summary>
        /// Identifies the IsVisible attached property.
        /// </summary>
        public static readonly DependencyProperty IsVisibleProperty;


        /// <summary>
        /// Gets a value indicating whether the column is visible.
        /// </summary>
        /// <param name="col">The <see cref="GridViewColumn"/> to resolve is
        /// visible.</param>
        /// <returns>true if the column is currently visible.</returns>
        public static bool GetIsVisible( GridViewColumn col )
        {
            return (bool)col.GetValue( IsVisibleProperty );
        }

        /// <summary>
        /// Sets the visibility of a <see cref="GridViewColumn"/>.
        /// </summary>
        /// <param name="col">The <see cref="GridViewColumn"/> to set the
        /// visibility of.</param>
        /// <param name="isVisible">Whether the <see cref="GridViewColumn"/>
        /// is currently visible.</param>
        public static void SetIsVisible( GridViewColumn col, bool isVisible )
        {
            col.SetValue( IsVisibleProperty, isVisible );
        }


        /// <summary>
        /// Occurs when the visibility state of a <see cref="GridViewColumn"/>
        /// changes.
        /// </summary>
        /// <param name="d">The <see cref="GridViewColumn"/></param>
        /// <param name="e">Event information.</param>
        private static void _onVisibilityChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            GridViewColumn c = d as GridViewColumn;
            if( GetIsVisible( c ) )
            {
                c.Width = _originalWidths[c];
            }
            else
            {
                _originalWidths[c] = c.Width;
                c.Width = 0;
            }
        }


        /// <summary>
        /// Maintains the original widths of columns.
        /// </summary>
        private static IDictionary<GridViewColumn, double> _originalWidths;
    }
}
