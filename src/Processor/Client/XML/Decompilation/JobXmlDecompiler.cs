using DIPS.Processor.Client;
using DIPS.Processor.Client.JobDeployment;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Xml;
using System.Xml.Linq;

namespace DIPS.Processor.XML.Decompilation
{
    /// <summary>
    /// Provides decompilation behaviour for job Xml.
    /// </summary>
    public class JobXmlDecompiler : IAlgorithmXmlDecompiler
    {
        /// <summary>
        /// Decompiles an Xml node back into an <see cref="AlgorithmDefinition"/>.
        /// </summary>
        /// <param name="algorithmNode">The <see cref="XNode"/> representing the
        /// <see cref="AlgorithmDefinition"/>.</param>
        /// <returns>An <see cref="AlgorithmDefinition"/> object represented by
        /// the provided Xml.</returns>
        public AlgorithmDefinition DecompileAlgorithm( XNode algorithmNode )
        {
            if( algorithmNode.NodeType != XmlNodeType.Element )
            {
                throw new ArgumentException( "An XElement is required." );
            }

            XElement element = (XElement)algorithmNode;
            IEnumerable<Property> properties = new List<Property>();
            var propertiesNodes = element.Descendants( "properties" );
            if( propertiesNodes.Any() )
            {
                properties = _parseProperties( propertiesNodes.First() );
            }

            string name = element.Attribute( "name" ).Value;
            return new AlgorithmDefinition( name, properties );
        }

        /// <summary>
        /// Decompiles an Xml node back into a <see cref="JobInput"/>.
        /// </summary>
        /// <param name="inputNode">The <see cref="XNode"/> represnting the
        /// <see cref="JobInput."/></param>
        /// <returns>A <see cref="JobInput"/> object represented by the provided
        /// Xml.</returns>
        public JobInput DecompileInput( XNode inputNode )
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Parses the properties node.
        /// </summary>
        /// <param name="properties">The "properties" node from Xml</param>
        /// <returns>The set of Property objects represented by the Xml.</returns>
        private IEnumerable<Property> _parseProperties( XElement properties )
        {
            ICollection<Property> props = new List<Property>();
            foreach( var propertyNode in properties.Descendants( "property" ) )
            {
                Property property = _parseProperty( propertyNode );
                props.Add( property );
            }

            return props;
        }

        /// <summary>
        /// Parses a single property from Xml
        /// </summary>
        /// <param name="property">The proeprty element</param>
        /// <returns>A Property instance representing the Xml.</returns>
        private Property _parseProperty( XElement property )
        {
            string typeAsString = property.Attribute( "type" ).Value;
            Type propertyType = Type.GetType( typeAsString );
            TypeConverter valConverter = TypeDescriptor.GetConverter( propertyType );
            string valueAsString = property.Attribute( "value" ).Value;

            XAttribute converterAttr = property.Attribute( "converter" );
            IValueConverter converter = _resolveConverter( converterAttr );

            PropertyBuilder builder = new PropertyBuilder();
            builder.Name = property.Attribute( "name" ).Value;
            builder.PropertyType = propertyType;
            builder.DefaultValue = valConverter.ConvertFromString( valueAsString );
            builder.Converter = converter;
            return builder.Build();
        }

        /// <summary>
        /// Resolves the converter from the attribute
        /// </summary>
        /// <param name="converterAttr">The converter in use</param>
        /// <returns>The value converter or null</returns>
        private IValueConverter _resolveConverter( XAttribute converterAttr )
        {
            IValueConverter converter = null;
            if( converterAttr != null )
            {
                try
                {
                    Type converterType = Type.GetType( converterAttr.Value );
                    converter = Activator.CreateInstance( converterType ) as IValueConverter;
                }
                catch( Exception e )
                {
                    Debug.WriteLine( "Could not resolve converter: " + e );
                }
            }

            return converter;
        }
    }
}
