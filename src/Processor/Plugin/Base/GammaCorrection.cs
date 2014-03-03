using DIPS.Processor.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.Util;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Drawing;

namespace DIPS.Processor.Plugin.Base
{
    [Algorithm( "gamma", ParameterObjectType = typeof( GammaProperties ) )]
    [AlgorithmMetadata(
        "Gamma Correction",
        Description = "Corrects Gamma level of input using provided target Gamma." )]
    public class GammaCorrection : AlgorithmPlugin
    {
        [AlgorithmProperty( "gamma", 3d )]
        public double Gamma
        {
            get;
            set;
        }

        public override void Run( object parameterObject )
        {
            if( parameterObject is GammaProperties == false )
            {
                throw new ArgumentException( "Provided object not a GammaProperties instance" );
            }

            GammaProperties p = parameterObject as GammaProperties;
            Bitmap bmp = new Bitmap( Input );
            Image<Rgb, byte> img = new Image<Rgb, byte>( bmp );
            img._GammaCorrect( p.Gamma );
            Output = img.Bitmap;
        }
    }
}
