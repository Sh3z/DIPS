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
            if( args.Properties.Contains( "Gamma" ) == false )
            {
                throw new AlgorithmException( "No Gamma property provided (required)" );
            }

            Property gammaProperty = args.Properties.PropertyForName( "Gamma" );
            if( gammaProperty.IsOfType( _gammaProperty.Type ) == false )
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
