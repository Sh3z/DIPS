using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace DIPS.ViewModel.Converters
{
    /// <summary>
    /// Represents the converter used to 'flip' a bool value.
    /// </summary>
    public class BooleanNotConverter : IValueConverter
    {
        /// <summary>
        /// Flips a bool into its opposite form.
        /// </summary>
        /// <param name="value">The bool to flip.</param>
        /// <param name="targetType">The target type.</param>
        /// <param name="parameter">Not used</param>
        /// <param name="culture">Not used</param>
        /// <returns>The opposite form of the boolean</returns>
        public object Convert( object value, Type targetType, object parameter, System.Globalization.CultureInfo culture )
        {
            if( value is bool && targetType == typeof( bool ) || targetType == typeof( bool? ) )
            {
                return !( (bool)value );
            }
            else
            {
                return Binding.DoNothing;
            }
        }

        /// <summary>
        /// Flips a bool into its opposite form.
        /// </summary>
        /// <param name="value">The bool to flip.</param>
        /// <param name="targetType">The target type.</param>
        /// <param name="parameter">Not used</param>
        /// <param name="culture">Not used</param>
        /// <returns>The opposite form of the boolean</returns>
        public object ConvertBack( object value, Type targetType, object parameter, System.Globalization.CultureInfo culture )
        {
            return Convert( value, targetType, parameter, null );
        }
    }
}
