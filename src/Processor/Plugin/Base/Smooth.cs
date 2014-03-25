using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor.Plugin.Base
{
    /// <summary>
    /// Represents the <see cref="AlgorithmPlugin"/> used to smooth an
    /// image.
    /// </summary>
    [Algorithm( "smooth", ParameterObjectType = typeof( SmoothProperties ) )]
    [AlgorithmMetadata( "Smoothing",
        Description = "Smooths an input image using a specific algorithm" )]
    public class Smooth : AlgorithmPlugin
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
            if( parameterObject is SmoothProperties == false )
            {
                throw new AlgorithmException( "Parameter object is not a SmoothProperties instance" );
            }

            SmoothProperties p = parameterObject as SmoothProperties;
            Bitmap bmp = new Bitmap( Input );
            Image<Rgb, byte> img = new Image<Rgb, byte>( bmp );
            IImage output = p.Smoother.Smooth( img );
            Output = output.Bitmap;
        }
    }
}
