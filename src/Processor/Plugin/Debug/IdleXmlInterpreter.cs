using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DIPS.Processor.Plugin.Debug
{
    /// <summary>
    /// Represents the object used to convert the properties of the
    /// <see cref="Idle"/> plugin to and from Xml
    /// </summary>
    [PipelineXmlOriginator( typeof( Idle ) )]
    public class IdleXmlInterpreter : IPipelineXmlInterpreter
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
            IdleProperties p = parameterObject as IdleProperties;
            if( p == null )
            {
                return new XElement( "properties" );
            }

            XElement secondsE = new XElement( "property",
                new XAttribute( "seconds", p.Seconds ) );
            return new XElement( "properties", secondsE );
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
            IdleProperties p = new IdleProperties();
            if( parameterXml == null )
            {
                return p;
            }

            XElement props = parameterXml.Descendants( "property" ).FirstOrDefault();
            if( props == null )
            {
                return p;
            }

            XAttribute secondsAttr = props.Attribute( "seconds" );
            if( secondsAttr != null )
            {
                int seconds = 0;
                int.TryParse( secondsAttr.Value, out seconds );
                p.Seconds = seconds;
            }

            return p;
        }
    }
}
