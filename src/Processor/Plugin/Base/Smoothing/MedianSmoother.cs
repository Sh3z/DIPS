using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor.Plugin.Base.Smoothing
{
    /// <summary>
    /// Represents the <see cref="ISmoother"/> used to perform
    /// median smoothing.
    /// </summary>
    public class MedianSmoother : ISmoother
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MedianSmoother"/>
        /// class.
        /// </summary>
        public MedianSmoother()
        {
            Size = 3;
        }


        /// <summary>
        /// Gets or sets the size of the smoothing kernel.
        /// </summary>
        [Description( "The size of the smoothing kernel" )]
        [DisplayName( "Kernel Size" )]
        public int Size
        {
            get;
            set;
        }


        /// <summary>
        /// Smoothes an inbound image using the properties set against
        /// this <see cref="ISmoother"/>.
        /// </summary>
        /// <param name="img">The <see cref="Image"/> to be
        /// smoothed.</param>
        /// <returns>An <see cref="IImage"/> representing the smoothed
        /// version.</returns>
        public IImage Smooth( Image<Rgb, byte> img )
        {
            return img.SmoothMedian( Size );
        }

        /// <summary>
        /// Creates a copy of this <see cref="MedianSmoother"/>.
        /// </summary>
        /// <returns>A new instance of this <see cref="MedianSmoother"/>
        /// with the same properties.</returns>
        public object Clone()
        {
            return new MedianSmoother()
            {
                Size = this.Size
            };
        }
    }
}
