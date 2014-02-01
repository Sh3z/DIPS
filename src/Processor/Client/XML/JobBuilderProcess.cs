using DIPS.Processor.Client;
using DIPS.Processor.Plugin;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DIPS.Processor.XML
{
    public class JobBuilderProcess : IBuilderProcess
    {
        /// <summary>
        /// Constructs Xml using the <see cref="AlgorithmDefinition"/>.
        /// </summary>
        /// <param name="definition">The <see cref="AlgorithmDefinition"/>
        /// detailing the process and its properties.</param>
        /// <returns>An <see cref="XElement"/> representing the
        /// <see cref="AlgorithmDefinition"/>.</returns>
        public XElement Build( AlgorithmDefinition definition )
        {
            if( definition == null )
            {
                return new XElement( "algorithm" );
            }

            const string algElementName = "algorithm";
            XAttribute nameAttr = new XAttribute( "name", definition.AlgorithmName );
            if( definition.Properties.Any() == false )
            {
                return new XElement( algElementName, nameAttr );
            }

            ICollection<XElement> properties = new List<XElement>();
            foreach( Property property in definition.Properties )
            {
                XAttribute name = new XAttribute( "name", property.Name );
                XAttribute type = new XAttribute( "type", property.Type );
                XElement value = new XElement( "value", property.Value );
                XElement propertyXml = new XElement( "property", name, type, value );
                properties.Add( propertyXml );
            }

            XElement propertiesXml = new XElement( "properties", properties );
            return new XElement( algElementName, nameAttr, propertiesXml );
        }

        /// <summary>
        /// Constructs Xml using the <see cref="Image"/>.
        /// </summary>
        /// <param name="input">The <see cref="Image"/> to be used as input
        /// in a job.</param>
        /// <returns>An <see cref="XElement"/> representing the
        /// <see cref="Image"/>.</returns>
        /// <exception cref="InvalidOperationException">this
        /// <see cref="IBuilderProcess"/> does not support building
        /// inputs.</exception>
        public XElement BuildInput( Image input )
        {
            return null;
        }
    }
}
