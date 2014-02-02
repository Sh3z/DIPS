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
using System.Xml.Linq;

namespace DIPS.Processor.XML.Decompilation
{
    /// <summary>
    /// Represents the visitor used to decompile Xml back into
    /// <see cref="AlgorithmDefinition"/> and <see cref="ProcessInput"/>
    /// objects.
    /// </summary>
    public class AlgorithmDecompilerVisitor : IJobXmlVisitor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AlgorithmDecompilerVisitor"/>.
        /// </summary>
        public AlgorithmDecompilerVisitor()
        {
            Algorithms = new List<AlgorithmDefinition>();
            Inputs = new List<JobInput>();
        }


        /// <summary>
        /// Gets a collection of the decompiled <see cref="AlgorithmDefinition"/>s.
        /// </summary>
        public ICollection<AlgorithmDefinition> Algorithms
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets a collection of the decompiled <see cref="JobInput"/>s.
        /// </summary>
        public ICollection<JobInput> Inputs
        {
            get;
            private set;
        }


        /// <summary>
        /// Performs the visiting logic against an <see cref="XNode"/> representing
        /// an <see cref="AlgorithmDefinition"/>.
        /// </summary>
        /// <param name="xml">The <see cref="XNode"/> containing the algorithm
        /// information.</param>
        public void VisitAlgorithm( XNode xml )
        {
            if( xml.NodeType != System.Xml.XmlNodeType.Element )
            {
                return;
            }

            XElement element = (XElement)xml;
            IEnumerable<Property> properties = new List<Property>();
            var propertiesNodes = element.Descendants( "properties" );
            if( propertiesNodes.Any() )
            {
                properties = _parseProperties( propertiesNodes.First() );
            }

            string name = element.Attribute( "name" ).Value;
            AlgorithmDefinition d = new AlgorithmDefinition( name, properties );
            Algorithms.Add( d );
        }

        /// <summary>
        /// Performs the visiting logic against an <see cref="XNode"/> representing
        /// an input to the job.
        /// </summary>
        /// <param name="xml">The <see cref="XNode"/> representing an input to a
        /// job.</param>
        public void VisitInput( XNode xml )
        {

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
