using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace DIPS.Processor.Plugin.Base
{
    /// <summary>
    /// Represents the available smoothing modes
    /// </summary>
    public class SmoothModeItemsSource : IItemsSource
    {
        /// <summary>
        /// Gets the identifier for the Blur smooth
        /// </summary>
        public static string Blur
        {
            get
            {
                return "Blur";
            }
        }

        /// <summary>
        /// Gets the identifier for the Median smooth
        /// </summary>
        public static string Median
        {
            get
            {
                return "Median";
            }
        }

        /// <summary>
        /// Gets the identifier for the Bilatral smooth
        /// </summary>
        public static string Bilatral
        {
            get
            {
                return "Bilatral";
            }
        }

        /// <summary>
        /// Gets the identifier for the Gaussian smooth.
        /// </summary>
        public static string Gaussian
        {
            get
            {
                return "Gaussian";
            }
        }


        /// <summary>
        /// Creates the available smoothing modes.
        /// </summary>
        /// <returns>A collection of the available smoothing modes.</returns>
        public ItemCollection GetValues()
        {
            ItemCollection c = new ItemCollection();
            c.Add( Bilatral );
            c.Add( Blur );
            c.Add( Gaussian );
            c.Add( Median );
            return c;
        }
    }
}
