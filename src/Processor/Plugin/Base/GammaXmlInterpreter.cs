using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DIPS.Processor.Plugin.Base
{
    [PipelineXmlOriginator( typeof( GammaCorrection ) )]
    public class GammaXmlInterpreter : IPipelineXmlInterpreter
    {
        /// <summary>
        /// Converts the parameter object provided by the
        /// <see cref="AlgorithmDefinition"/> containing the properties
        /// to persist.
        /// </summary>
        /// <param name="parameterObject">The value of the algorithms
        /// parameter object</param>
        /// <returns>The <see cref="XElement"/> describing the properties
        /// within the object.</returns>
        public XElement CreateXml( ICloneable parameterObject )
        {
            if( parameterObject is GammaProperties == false )
            {
                return new XElement( "properties" );
            }

            GammaProperties p = parameterObject as GammaProperties;
            return new XElement( "properties",
                new XElement( "property",
                    new XAttribute( "name", "gamma" ),
                    new XAttribute( "value", p.Gamma ) ) );
        }

        /// <summary>
        /// Converts the provided Xml back into the appropriate parameter
        /// object for the algorithm
        /// </summary>
        /// <param name="parameterXml">The <see cref="XElement"/> describing the properties
        /// within the object.</param>
        /// <returns>The appropriate object used to describe the parameters
        /// for the process.</returns>
        public ICloneable CreateObject( XElement parameterXml )
        {
            if( parameterXml == null )
            {
                return GammaProperties.Default;
            }
            else
            {
                GammaProperties p = new GammaProperties();
                _populateProperties( parameterXml, p );
                return p;
            }
        }


        /// <summary>
        /// Populates the properties object using the provided xml
        /// </summary>
        /// <param name="parameterXml">The Xml to populate the properties
        /// with</param>
        /// <param name="p">The properties object to populate</param>
        private void _populateProperties( XElement parameterXml, GammaProperties p )
        {
            var props = parameterXml.Descendants( "property" );
            if( props.Any() == false )
            {
                return;
            }

            var gammaProp = ( from prop in props
                              let nameAttr = prop.Attribute( "name" )
                              where nameAttr != null
                                    && nameAttr.Value == "gamma"
                                    && prop.Attribute( "value" ) != null
                              select prop ).FirstOrDefault();
            if( gammaProp != null )
            {
                double gamma = p.Gamma;
                double.TryParse( gammaProp.Attribute( "value" ).Value, out gamma );
                p.Gamma = gamma;
            }
        }
    }
}
