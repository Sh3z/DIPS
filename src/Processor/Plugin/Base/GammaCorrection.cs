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
    /// <summary>
    /// Represents the <see cref="AlgorithmPlugin"/> used to correct
    /// the gamma of an image.
    /// </summary>
    [Algorithm( "gamma", ParameterObjectType = typeof( GammaProperties ) )]
    [AlgorithmMetadata( "Gamma Correction",
        Description = "Corrects Gamma level of input using provided target Gamma." )]
    public class GammaCorrection : AlgorithmPlugin
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
            if( parameterObject is GammaProperties == false )
            {
                throw new AlgorithmException( "Provided object not a GammaProperties instance" );
            }

            GammaProperties p = parameterObject as GammaProperties;
            Bitmap bmp = new Bitmap( Input );
            Image<Rgb, byte> img = new Image<Rgb, byte>( bmp );
            img._GammaCorrect( p.Gamma );
            Output = img.Bitmap;
        }
    }
}
