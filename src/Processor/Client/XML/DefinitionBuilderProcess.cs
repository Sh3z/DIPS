using DIPS.Processor.Plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace DIPS.Processor.XML
{
    /// <summary>
    /// Represents the builder process used to construct an algorithm's
    /// definition in Xml.
    /// </summary>
    /// <remarks>
    /// The Xml generated provides a way of expressing the structure of the
    /// algorithm, rather than convey parameter information. It appears as:
    /// <example>
    ///     <algorithm name="gaussian">
    ///         <properties>
    ///             <property name="factor" type="System.Double" default="3" />
    ///         </properties>
    ///     </algorithm>
    /// </example>
    /// </remarks>
    public class DefinitionBuilderProcess : IBuilderProcess
    {
        /// <summary>
        /// Constructs Xml using the <see cref="AlgorithmDefinition"/>.
        /// </summary>
        /// <param name="definition">The <see cref="AlgorithmDefinition"/>
        /// detailing the process and its properties.</param>
        /// <returns>An <see cref="XElement"/> representing the the
        /// <see cref="AlgorithmDefinition"/>.</returns>
        public XElement Build( AlgorithmDefinition definition )
        {
            if( definition == null )
            {
                throw new ArgumentNullException( "definition" );
            }
            
            XAttribute nameAttr = new XAttribute( "name", definition.AlgorithmName );
            if( definition.Properties.Any() == false )
            {
                return new XElement( "algorithm", nameAttr );
            }
            else
            {
                ICollection<XElement> properties = _createProperties( definition.Properties );
                XElement propertiesElement = new XElement( "properties", properties );
                return new XElement( "algorithm", nameAttr, propertiesElement );
            }
        }


        /// <summary>
        /// Converts the set of properties into a set of XElements
        /// </summary>
        /// <param name="properties">The properties to convert</param>
        /// <returns>A set of Xml elements representing the properties</returns>
        private ICollection<XElement> _createProperties( ISet<Property> properties )
        {
            ICollection<XElement> props = new List<XElement>();
            foreach( var property in properties )
            {
                XAttribute name = new XAttribute( "name", property.Name );
                XAttribute type = new XAttribute( "type", property.Type.AssemblyQualifiedName );
                XAttribute defaultVal = new XAttribute( "default-value", property.Value );
                XElement prop = new XElement( "property", name, type, defaultVal );
                props.Add( prop );
            }

            return props;
        }
    }
}
