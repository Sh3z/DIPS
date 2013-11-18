using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace DIPS.ViewModel.Converters
{
    /// <summary>
    /// Represents the converter used to transform a <see cref="Bitmap"/> into an <see cref="ImageSource"/>.
    /// </summary>
    public class BitmapToImageSourceConverter : IValueConverter
    {
        /// <summary>
        /// Converts a <see cref="Bitmap"/> into an <see cref="ImageSource"/> for use in Bindings.
        /// </summary>
        /// <param name="value">The <see cref="Bitmap"/></param>
        /// <param name="targetType">Must be typeof(ImageSource).</param>
        /// <param name="parameter">N/A</param>
        /// <param name="culture">N/A</param>
        /// <returns>An <see cref="ImageSouce"/> that can be used to display the provided
        /// <see cref="Bitmap"/>.</returns>
        public object Convert( object value, Type targetType, object parameter, CultureInfo culture )
        {
            if( value is Bitmap == false )
            {
                return Binding.DoNothing;
            }

            MemoryStream ms = new MemoryStream();
            ( (System.Drawing.Bitmap)value ).Save( ms, System.Drawing.Imaging.ImageFormat.Bmp );
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            ms.Seek( 0, SeekOrigin.Begin );
            image.StreamSource = ms;
            image.EndInit();

            return image;
        }

        /// <summary>
        /// Not implemented.
        /// </summary>
        /// <param name="value">N/A</param>
        /// <param name="targetType">N/A</param>
        /// <param name="parameter">N/A</param>
        /// <param name="culture">N/A</param>
        /// <returns><see cref="Binding.DoNothing"/></returns>
        public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
        {
            return Binding.DoNothing;
        }
    }
}
