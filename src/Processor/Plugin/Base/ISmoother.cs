using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor.Plugin.Base
{
    /// <summary>
    /// Represents a processing algorithm used to smooth an image.
    /// </summary>
    public interface ISmoother : ICloneable
    {
        /// <summary>
        /// Smoothes an inbound image using the properties set against
        /// this <see cref="ISmoother"/>.
        /// </summary>
        /// <param name="img">The <see cref="Image"/> to be
        /// smoothed.</param>
        /// <returns>An <see cref="IImage"/> representing the smoothed
        /// version.</returns>
        IImage Smooth( Image<Rgb, byte> img );
    }
}
