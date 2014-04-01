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
    /// bilatral smoothing.
    /// </summary>
    public class BilatralSmoother : ISmoother
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BilatralSmoother"/>
        /// class.
        /// </summary>
        public BilatralSmoother()
        {
            Size = 3;
            Colour = 1;
            Space = 1;
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
                if( value > 0 )
                {
                    _size = value;
                }
            }
        }
        [DebuggerBrowsable( DebuggerBrowsableState.Never )]
        private int _size;

        /// <summary>
        /// Gets or sets the colour sigma.
        /// </summary>
        [Description( "The colour sigma" )]
        public int Colour
        {
            get
            {
                return _colour;
            }
            set
            {
                if( value > 0 )
                {
                    _colour = value;
                }
            }
        }
        [DebuggerBrowsable( DebuggerBrowsableState.Never )]
        private int _colour;


        /// <summary>
        /// Gets or sets the space sigma.
        /// </summary>
        [Description( "The space sigma" )]
        public int Space
        {
            get
            {
                return _space;
            }
            set
            {
                if( value > 0 )
                {
                    _space = value;
                }
            }
        }
        [DebuggerBrowsable( DebuggerBrowsableState.Never )]
        private int _space;

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
