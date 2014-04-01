using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor.Plugin.Base.Smoothing
{
    /// <summary>
    /// Represents the <see cref="ISmoother"/> used to perform
    /// gaussian smoothing.
    /// </summary>
    public class GaussianSmoother : ISmoother
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GaussianSmoother"/>
        /// class.
        /// </summary>
        public GaussianSmoother()
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
            get
            {
                return _size;
            }
            set
            {
                if( _size > 2 )
                {
                    _size = value;
                }
            }
        }
        [DebuggerBrowsable( DebuggerBrowsableState.Never )]
        private int _size;


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
            return img.SmoothGaussian( Size );
        }

        /// <summary>
        /// Creates a copy of this <see cref="GaussianSmoother"/>.
        /// </summary>
        /// <returns>A new instance of this <see cref="GaussianSmoother"/>
        /// with the same properties.</returns>
        public object Clone()
        {
            return new GaussianSmoother()
            {
                Size = this.Size
            };
        }
    }
}
