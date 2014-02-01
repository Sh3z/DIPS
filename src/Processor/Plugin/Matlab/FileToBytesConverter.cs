using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace DIPS.Processor.Plugin.Matlab
{
    /// <summary>
    /// Converts a filepath into the contents of the file.
    /// </summary>
    public class FileToBytesConverter : IValueConverter
    {
        /// <summary>
        /// Reads in the file given at the specified path and converts it into
        /// bytes.
        /// </summary>
        /// <param name="value">The path to the file.</param>
        /// <param name="targetType">Target type (byte[]).</param>
        /// <param name="parameter">Not used.</param>
        /// <param name="culture">Not used.</param>
        /// <returns>The bytes of the incoming file.</returns>
        public object Convert( object value, Type targetType, object parameter, System.Globalization.CultureInfo culture )
        {
            if( targetType != typeof( byte[] ) )
            {
                return new byte[0];
            }

            if( value is string == false )
            {
                return new byte[0];
            }

            string path = value as string;
            if( File.Exists( path ) == false )
            {
                return new byte[0];
            }

            return File.ReadAllBytes( path );
        }

        /// <summary>
        /// Not implemented.
        /// </summary>
        /// <param name="value">N/A</param>
        /// <param name="targetType">N/A</param>
        /// <param name="parameter">N/A</param>
        /// <param name="culture">N/A</param>
        /// <returns>null.</returns>
        public object ConvertBack( object value, Type targetType, object parameter, System.Globalization.CultureInfo culture )
        {
            return null;
        }
    }
}
