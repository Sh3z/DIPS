using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DIPS.Processor.XML.Decompilation
{
    /// <summary>
    /// Represents the Xml validator that ensures definition Xml is valid.
    /// </summary>
    public class DefinitionXmlValidator : ValidationVisitor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DefinitionXmlValidator"/>
        /// class.
        /// </summary>
        /// <param name="visitor">The <see cref="IXmlVisitor"/> this
        /// <see cref="XmlVisitorDecorate"/> decorates.</param>
        public DefinitionXmlValidator( IXmlVisitor visitor )
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
            if( element.Name != "algorithm" )
            {
                return false;
            }

            XAttribute nameAttr = element.Attribute( "name" );
            if( nameAttr == null )
            {
                return false;
            }

            if( string.IsNullOrEmpty( nameAttr.Value ) )
            {
                return false;
            }

            // Algorithms may not have properties!
            var properties = element.Descendants( "properties" );
            if( properties.Any() )
            {
                return _validateProperties( properties.First() );
            }
            else
            {
                return true;
            }
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
            throw new InvalidOperationException( "Job definitions do not support inputs." );
        }


        /// <summary>
        /// Validates the properties of an algorithm.
        /// </summary>
        /// <param name="properties">The properties to validate.</param>
        /// <returns>true if all properties are valid.</returns>
        private bool _validateProperties( XElement properties )
        {
            var subProperties = properties.Descendants( "property" );
            foreach( var property in subProperties )
            {
                if( _validateProperty( property ) == false )
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Validates a single property of an algorithm.
        /// </summary>
        /// <param name="property">The property to validate</param>
        /// <returns>true if the property is valid</returns>
        private bool _validateProperty( XElement property )
        {
            XAttribute nameAttr = property.Attribute( "name" );
            if( nameAttr == null )
            {
                return false;
            }

            if( string.IsNullOrEmpty( nameAttr.Value ) )
            {
                return false;
            }

            XAttribute typeAttr = property.Attribute( "type" );
            if( typeAttr == null )
            {
                return false;
            }

            string typeName = typeAttr.Value;
            try
            {
                Type.GetType( typeName );
            }
            catch
            {
                return false;
            }

            XAttribute valueAttr = property.Attribute( "value" );
            if( valueAttr == null )
            {
                // May be using nested element.
                var valueElements = property.Descendants( "value" );
                if( valueElements.Any() == false )
                {
                    return false;
                }
            }

            return true;
        }
    }
}
