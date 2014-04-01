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
    /// blur smoothing.
    /// </summary>
    public class BlurSmoother : ISmoother
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BlurSmoother"/>
        /// class.
        /// </summary>
        public BlurSmoother()
        {
            Width = 3;
            Height = 3;
        }


        /// <summary>
        /// Gets or sets the smoothing kernel width.
        /// </summary>
        [Description( "The width of the smoothing kernel" )]
        [DisplayName( "Kernel Width" )]
        public int Width
        {
            get
            {
                return _width;
            }
            set
            {
                if( value > 0 )
                {
                    _width = value;
                }
            }
        }
        [DebuggerBrowsable( DebuggerBrowsableState.Never )]
        private int _width;

        /// <summary>
        /// Gets or sets the smoothing kernel height.
        /// </summary>
        [Description( "The height of the smoothing kernel" )]
        [DisplayName( "Kernel Width" )]
        public int Height
        {
            get
            {
                return _height;
            }
            set
            {
                if( value > 0 )
                {
                    _height = value;
                }
            }
        }
        [DebuggerBrowsable( DebuggerBrowsableState.Never )]
        private int _height;


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
            return img.SmoothBlur( Width, Height );
        }

        /// <summary>
        /// Creates a copy of this <see cref="BlurSmoother"/>.
        /// </summary>
        /// <returns>A new instance of this <see cref="BlurSmoother"/>
        /// with the same properties.</returns>
        public object Clone()
        {
            return new BlurSmoother()
            {
                Height = this.Height,
                Width = this.Width
            };
        }
    }
}
