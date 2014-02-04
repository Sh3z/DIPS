using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DIPS.Processor.XML.Decompilation
{
    /// <summary>
    /// Represents the Xml validator that ensures Job Xml is valid.
    /// </summary>
    public class JobXmlValidator : ValidationVisitor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JobXmlValidator"/> class.
        /// </summary>
        /// <param name="visitor">The <see cref="IJobXmlVisitor"/> this
        /// <see cref="XmlVisitorDecorate"/> decorates.</param>
        public JobXmlValidator( IJobXmlVisitor visitor )
            : base( visitor )
        {
        }


        /// <summary>
        /// Executes the algorithm-node validation logic.
        /// </summary>
        /// <param name="algorithmNode">The <see cref="XNode"/> representing an
        /// algorithm.</param>
        /// <returns><c>true</c> if the algorithm represented by the
        /// <see cref="XNode"/> is valid; false otherwise.</returns>
        protected override bool IsAlgorithmValid( XNode algorithmNode )
        {
            if( algorithmNode.NodeType != System.Xml.XmlNodeType.Element )
            {
                return false;
            }

            XElement element = (XElement)algorithmNode;
            if( element.Attribute( "name" ) == null )
            {
                return false;
            }

            // Validate properties if there are any.
            var properties = element.Descendants( "property" );
            if( properties.Any() && _validateProperties( properties ) == false )
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Executes the input-node validation logic.
        /// </summary>
        /// <param name="algorithmNode">The <see cref="XNode"/> representing an
        /// input.</param>
        /// <returns><c>true</c> if the input represented by the
        /// <see cref="XNode"/> is valid; false otherwise.</returns>
        protected override bool IsInputValid( XNode inputNode )
        {
            if( inputNode.NodeType != System.Xml.XmlNodeType.Element )
            {
                return false;
            }

            XElement element = (XElement)inputNode;


            return true; // todo
        }


        /// <summary>
        /// Validates the properties of an algorithm element.
        /// </summary>
        /// <param name="properties">The properties of the algorithm.</param>
        /// <returns>true if the properties are valid.</returns>
        private bool _validateProperties( IEnumerable<XNode> properties )
        {
            foreach( XNode property in properties )
            {
                if( property.NodeType != System.Xml.XmlNodeType.Element )
                {
                    return false;
                }
                else
                {
                    if( _validateProperty( (XElement)property ) == false )
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Validates a single property of an algorithm
        /// </summary>
        /// <param name="property">The property element</param>
        /// <returns>true if the property is valid.</returns>
        private bool _validateProperty( XElement property )
        {
            if( property.Attribute( "name" ) == null )
            {
                return false;
            }

            if( property.Attribute( "type" ) == null )
            {
                return false;
            }

            string typeName = property.Attribute( "type" ).Value;
            if( _validateType( typeName ) == false )
            {
                return false;
            }

            if( property.Attribute( "value" ) == null )
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Validates the incoming type name from Xml
        /// </summary>
        /// <param name="typeName">The name of the type</param>
        /// <returns>true if the type is valid, false otherwise.</returns>
        private bool _validateType( string typeName )
        {
            try
            {
                Type type = Type.GetType( typeName );
                return type != null;
            }
            catch
            {
                return false;
            }
        }
    }
}
