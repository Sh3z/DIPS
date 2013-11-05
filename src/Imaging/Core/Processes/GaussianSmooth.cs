using Emgu.CV;
using Emgu.CV.Structure;
using Femore.Imaging.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Femore.Imaging.Core.Processes
{
    [Serializable]
    public class GaussianSmooth : IImageProcess
    {
        public GaussianSmooth()
        {
            SmoothingRadius = 5;
        }

        public int SmoothingRadius
        {
            get
            {
                return _radius;
            }
            set
            {
                if( value < 0 )
                {
                    throw new ArgumentOutOfRangeException( "Radius must be greater than 0." );
                }

                _radius = value;
            }
        }
        [DebuggerBrowsable( DebuggerBrowsableState.Never )]
        private int _radius;


        public Bitmap ProcessedImage
        {
            get;
            private set;
        }

        public void Process( Bitmap image )
        {
            if( image == null )
            {
                throw new ArgumentNullException( "image" );
            }

            Image<Rgb, Byte> theImg = new Image<Rgb, Byte>( image );
            var smoothed = theImg.SmoothGaussian( SmoothingRadius );
            ProcessedImage = smoothed.Bitmap;
        }

        public bool Equals( IImageProcess other )
        {
            if( other is GaussianSmooth == false )
            {
                return false;
            }

            GaussianSmooth smoother = other as GaussianSmooth;
            return SmoothingRadius == smoother.SmoothingRadius;
        }
    }
}
