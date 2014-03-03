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
    /// bilatral smoothing.
    /// </summary>
    public class BilatralSmoother : ISmoother
    {
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
        /// Gets or sets the colour sigma.
        /// </summary>
        [Description( "The colour sigma" )]
        public int Colour
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the space sigma.
        /// </summary>
        [Description( "The space sigma" )]
        public int Space
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
            return img.SmoothBilatral( Size, Colour, Space );
        }

        /// <summary>
        /// Creates a copy of this <see cref="BilatralSmoother"/>.
        /// </summary>
        /// <returns>A new instance of this <see cref="BilatralSmoother"/>
        /// with the same properties.</returns>
        public object Clone()
        {
            return new BilatralSmoother()
            {
                Size = this.Size,
                Colour = this.Colour,
                Space = this.Space
            };
        }
    }
}
