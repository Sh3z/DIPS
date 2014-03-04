using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace DIPS.ViewModel.Converters
{
    public class SingleItemToEnumerableConverter : IValueConverter
    {
        public object Convert( object value, Type targetType, object parameter, System.Globalization.CultureInfo culture )
        {
            return new object[] { value };
        }

        public object ConvertBack( object value, Type targetType, object parameter, System.Globalization.CultureInfo culture )
        {
            if( value is IEnumerable<object> )
            {
                return ( (IEnumerable<object>)value ).First();
            }
            else
            {
                return Binding.DoNothing;
            }
        }
    }
}
