using DIPS.Processor.Client;
using DIPS.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace DIPS.UI.Converters
{
    /// <summary>
    /// Provides conversion logic to display the most appropriate name for
    /// an <see cref="AlgorithmDefinition"/>.
    /// </summary>
    public class AlgorithmDisplayNameConverter : IValueConverter
    {
        /// <summary>
        /// Converts an <see cref="AlgorithmDefinition"/> into the string form,
        /// with either the DisplayName or AlgorithmName.
        /// </summary>
        /// <param name="value">The <see cref="AlgorithmDefinition"/> to be
        /// presented.</param>
        /// <param name="targetType">The target type.</param>
        /// <param name="parameter">Not used.</param>
        /// <param name="culture">Not used.</param>
        /// <returns>The string form of the provided <see cref="AlgorithmDefinition"/>.</returns>
        public object Convert( object value, Type targetType, object parameter, System.Globalization.CultureInfo culture )
        {
            if( targetType != typeof( string ) )
            {
                return null;
            }

            if( value is AlgorithmViewModel == false )
            {
                return string.Empty;
            }

            AlgorithmViewModel d = value as AlgorithmViewModel;
            return d.Definition.DisplayName ?? d.Definition.AlgorithmName;
        }

        /// <summary>
        /// Not implemented.
        /// </summary>
        /// <param name="value">N/A</param>
        /// <param name="targetType">N/A</param>
        /// <param name="parameter">N/A</param>
        /// <param name="culture">N/A</param>
        /// <returns>null</returns>
        public object ConvertBack( object value, Type targetType, object parameter, System.Globalization.CultureInfo culture )
        {
            return null;
        }
    }
}
