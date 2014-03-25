using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor.Plugin.Base
{
    /// <summary>
    /// Represents the <see cref="AlgorithmPlugin"/> used to equalize
    /// the histogram of an image.
    /// </summary>
    [Algorithm( "histogram-equalization" )]
    [AlgorithmMetadata( "Histogram Equalization",
        Description = "Equalizes the images histogram" )]
    public class HistogramEqualization : AlgorithmPlugin
    {
        /// <summary>
        /// Executes the algorithm represented by this <see cref="AlgorithmPlugin"/>.
        /// </summary>
        /// <param name="parameterObject">An object of the type provided by the
        /// <see cref="AlgorithmAttribute"/> describing the properties used by this
        /// <see cref="AlgorithmPlugin"/>.</param>
        /// <exception cref="AlgorithmException">an internal exception has occured. This
        /// is accessed through the inner exception property.</exception>
        public override void Run( object parameterObject )
        {
            Bitmap bmp = new Bitmap( Input );
            Image<Rgb, byte> img = new Image<Rgb, byte>( bmp );
            img._EqualizeHist();
            Output = img.Bitmap;
        }
    }
}
