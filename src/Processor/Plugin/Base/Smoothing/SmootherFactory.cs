using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor.Plugin.Base.Smoothing
{
    public static class SmootherFactory
    {
        static SmootherFactory()
        {
            _smoothers = new Dictionary<string, Type>();
            _smoothers.Add( SmoothModeItemsSource.Bilatral, typeof( BilatralSmoother ) );
            _smoothers.Add( SmoothModeItemsSource.Blur, typeof( BlurSmoother ) );
            _smoothers.Add( SmoothModeItemsSource.Gaussian, typeof( GaussianSmoother ) );
            _smoothers.Add( SmoothModeItemsSource.Median, typeof( MedianSmoother ) );
        }

        public static ISmoother Manufacture( string identifier )
        {
            if( string.IsNullOrEmpty( identifier ) )
            {
                return null;
            }

            Type type = null;
            _smoothers.TryGetValue( identifier, out type );
            if( type == null )
            {
                return null;
            }
            else
            {
                return (ISmoother)Activator.CreateInstance( type );
            }
        }


        /// <summary>
        /// Maintains the set of available smoothers.
        /// </summary>
        private static IDictionary<string, Type> _smoothers;
    }
}
