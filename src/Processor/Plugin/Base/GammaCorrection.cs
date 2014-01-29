using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor.Plugin.Base
{
    public class GammaCorrection : AlgorithmPlugin
    {
        public GammaCorrection()
        {
            _gammaProperty = new Property( "Gamma", typeof( double ), true );
        }

        public override void Run( RunArgs args )
        {
            Property gammaProperty = args.Properties.PropertyForName( "Gamma" );
            if( gammaProperty == null )
            {
                throw new AlgorithmException( "No Gamma property provided (required)" );
            }

            if( gammaProperty.Type != _gammaProperty.Type )
            {
                throw new PropertyTypeException( _gammaProperty, gammaProperty.Type );
            }

            double gamma = (double)gammaProperty.Value;
            
        }

        public override ICollection<Property> CreatePropertyDefinitions()
        {
            List<Property> properties = new List<Property>();
            properties.Add( _gammaProperty );

            return properties;
        }


        private Property _gammaProperty;
    }
}
