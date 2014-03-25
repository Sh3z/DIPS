using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace DIPS.ViewModel.Converters
{
    public class DecimalToPercentageConverter : IValueConverter
    {
        public object Convert( object value, Type targetType, object parameter, System.Globalization.CultureInfo culture )
        {
            if( value is decimal )
            {
                return string.Format( "{0}%", decimal.Round( (decimal)value ) );
            }
            else
            {
                return Binding.DoNothing;
            }
        }

        public object ConvertBack( object value, Type targetType, object parameter, System.Globalization.CultureInfo culture )
        {
            return Binding.DoNothing;
        }
    }
}
