using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathWorks.MATLAB.NET.Utility;

namespace DIPS.Processor.Plugin.Base
{
    public class MatlabProcess : AlgorithmPlugin
    {
        public override void Run( RunArgs args )
        {
            Property matlabProperty = args.Properties.PropertyForName( _matlabProperty );
            if( matlabProperty.Type != typeof( byte[] ) )
            {
                string msg = string.Format("Invalid type for property \"{0}\"", _matlabProperty);
                throw new AlgorithmException( msg );
            }

            byte[] rawMatlab = matlabProperty.Value as byte[];

        }

        public override ICollection<Property> CreatePropertyDefinitions()
        {
            List<Property> properties = new List<Property>();
            properties.Add( new Property( _matlabProperty, typeof( byte[] ) ) );

            return properties;
        }


        private static readonly string _matlabProperty = "MatlabFile";
    }
}
