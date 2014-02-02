using DIPS.Processor.Registry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DIPS.Processor.XML.Decompilation
{
    /// <summary>
    /// Represents the visitor used to ensure the Xml provided by the traverser
    /// is in a valid format before executing the behaviour of another
    /// <see cref="IJobXmlVisitor"/>.
    /// </summary>
    public class JobValidationVisitor : XmlVisitorDecorator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JobValidationVisitor"/>
        /// class.
        /// </summary>
        /// <param name="args">The set of <see cref="XmlValidatorArgs"/> to
        /// use when validating Xml.</param>
        /// <exception cref="NullReferenceException">args is null.</exception>
        public JobValidationVisitor( XmlValidatorArgs args )
            : base( args.Visitor )
        {
            _validationArgs = args;
        }


        /// <summary>
        /// Performs the visiting logic against an <see cref="XNode"/> representing
        /// an <see cref="AlgorithmDefinition"/>.
        /// </summary>
        /// <param name="xml">The <see cref="XNode"/> containing the algorithm
        /// information.</param>
        public override void VisitAlgorithm( XNode xml )
        {
            if( _validateAlgorithm( xml ) )
            {
                DecoratedVisitor.VisitAlgorithm( xml );
            }
            else
            {
                _throwIfNecessary( xml );
            }
        }

        /// <summary>
        /// Performs the visiting logic against an <see cref="XNode"/> representing
        /// an input to the job.
        /// </summary>
        /// <param name="xml">The <see cref="XNode"/> representing an input to a
        /// job.</param>
        public override void VisitInput( XNode xml )
        {
            if( _validateInput( xml ) )
            {
                DecoratedVisitor.VisitInput( xml );
            }
            else
            {
                _throwIfNecessary( xml );
            }
        }


        /// <summary>
        /// Throws an XmlValidationException if the args specify we should throw
        /// when invalid Xml is provided.
        /// </summary>
        /// <param name="errNode">The troublesome node.</param>
        private void _throwIfNecessary( XNode errNode )
        {
            if( _validationArgs.ThrowOnError )
            {
                throw new XmlValidationException( errNode );
            }
        }

        /// <summary>
        /// Performs the validation of the input Xml.
        /// </summary>
        /// <param name="xml">The Xml to validate.</param>
        /// <returns>true if the Xml is valid.</returns>
        private bool _validateInput( XNode xml )
        {
            if( _validationArgs.InputValidator != null )
            {
                return _validationArgs.InputValidator( xml );
            }

            if( xml.NodeType != System.Xml.XmlNodeType.Element )
            {
                return false;
            }

            XElement element = (XElement)xml;


            return true; // todo
        }


        /// <summary>
        /// Performs the validation of the algorithm Xml.
        /// </summary>
        /// <param name="xml">The Xml to validate.</param>
        /// <returns>true if the Xml is valid.</returns>
        private bool _validateAlgorithm( XNode xml )
        {
            if( _validationArgs.AlgorithmValidator != null )
            {
                return _validationArgs.AlgorithmValidator( xml );
            }

            if( xml.NodeType != System.Xml.XmlNodeType.Element )
            {
                return false;
            }

            XElement element = (XElement)xml;
            if( element.Attribute( "name" ) == null )
            {
                return false;
            }

            string algorithmName = element.Attribute( "name" ).Value;
            if( RegistryCache.Cache.HasCachedAlgorithm( algorithmName ) == false )
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


        /// <summary>
        /// Contains the set of validation args in use.
        /// </summary>
        private XmlValidatorArgs _validationArgs;
    }
}
