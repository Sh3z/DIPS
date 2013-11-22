using DIPS.Imaging.Client;
using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Imaging.Core.Processes
{
    [Serializable]
    public class GammaCorrection : IImageProcess
    {
        public GammaCorrection()
        {
            Gamma = 1.5d;
        }

        public double Gamma
        {
            get
            {
                return _gamma;
            }
            set
            {
                if( value < 0 )
                {
                    throw new ArgumentOutOfRangeException( "New gamma must be greater than zero." );
                }

                _gamma = value;
            }
        }
        [DebuggerBrowsable( DebuggerBrowsableState.Never )]
        private double _gamma;


        public Bitmap ProcessedImage
        {
            get;
            private set;
        }

        public void Process( Bitmap image )
        {
            Image<Rgb, Byte> theImg = new Image<Rgb, Byte>( image );
            theImg._GammaCorrect( Gamma );
            ProcessedImage = theImg.Bitmap;
        }

        public bool Equals( IImageProcess other )
        {
            if( other is GammaCorrection == false )
            {
                return false;
            }

            GammaCorrection corrector = other as GammaCorrection;
            return Gamma == corrector.Gamma;
        }
    }
}
