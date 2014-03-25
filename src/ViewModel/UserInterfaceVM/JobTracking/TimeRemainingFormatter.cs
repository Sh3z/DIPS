using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace DIPS.ViewModel.UserInterfaceVM.JobTracking
{
    public class TimeRemainingFormatter : IValueConverter
    {
        /// <summary>
        /// Formats the incoming <see cref="DateTime"/> into a string indicating
        /// the remaining time.
        /// </summary>
        /// <param name="value">The <see cref="DateTime"/>.</param>
        /// <param name="targetType">Ignored, implicitly string.</param>
        /// <param name="parameter">N/A</param>
        /// <param name="culture">N/A</param>
        /// <returns>The string form of the <see cref="DateTime"/>.</returns>
        public object Convert( object value, Type targetType, object parameter, CultureInfo culture )
        {
            if( value is TimeSpan )
            {
                return _formatSpan( (TimeSpan)value );
            }
            else if( value == null )
            {
                return "Calculating...";
            }
            else
            {
                return Binding.DoNothing;
            }
        }

        /// <summary>
        /// Not implemented.
        /// </summary>
        /// <param name="value">N/A</param>
        /// <param name="targetType">N/A</param>
        /// <param name="parameter">N/A</param>
        /// <param name="culture">N/A</param>
        /// <returns>Binding.DoNothing</returns>
        public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
        {
            return Binding.DoNothing;
        }


        /// <summary>
        /// Formats the incoming time into a string
        /// </summary>
        /// <param name="span">The time to format</param>
        /// <returns>The formatted time</returns>
        private string _formatSpan( TimeSpan span )
        {
            // If less than an hour, display minutes and seconds
            if( span.Hours == 0 )
            {
                // If less than a minute, display the seconds
                if( span.Minutes == 0 )
                {
                    return span.Seconds + " seconds";
                }

                string format = "{0} minutes {1} seconds";
                return string.Format( format, span.Minutes, span.Seconds );
            }

            // Otherwise just display hours
            return span.Hours + " hours";
        }
    }
}
