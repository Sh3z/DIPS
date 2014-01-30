using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor.Plugin
{
    /// <summary>
    /// Provides helper functionality to reflect <see cref="AlgorithmPlugin"/>
    /// subclasses into skeleton definitions.
    /// </summary>
    public static class PluginReflector
    {
        /// <summary>
        /// Gathers the metadata from the <see cref="AlgorithmPlugin"/> and
        /// returns an <see cref="AlgorithmDefinition"/> representing its
        /// structure.
        /// </summary>
        /// <param name="plugin">The <see cref="AlgorithmPlugin"/> concrete
        /// class to construct a definition of.</param>
        /// <returns>An <see cref="AlgorithmDefinition"/> representing the structure
        /// of the <see cref="AlgorithmPlugin"/>.</returns>
        /// <exception cref="ArgumentNullException">plugin is null</exception>
        /// <exception cref="ArgumentException">the supplied <see cref="AlgorithmPlugin"/>
        /// is not annotated with the <see cref="PluginIdentifierAttribute"/>
        /// attribute.</exception>
        public static AlgorithmDefinition CreateDefinition( AlgorithmPlugin plugin )
        {
            if( plugin == null )
            {
                throw new ArgumentNullException( "plugin" );
            }
            else
            {
                return _createDefinition( plugin.GetType() );
            }
        }


        /// <summary>
        /// Creates the AlgorithmDefinition from the incoming type
        /// </summary>
        /// <param name="processType">The type to construct the definition for</param>
        /// <returns>An AlgorithmDefinition representing the incoming type</returns>
        private static AlgorithmDefinition _createDefinition( Type processType )
        {
            PluginIdentifierAttribute identifier = _getIdentifierAttribute( processType );
            if( identifier == null )
            {
                throw new ArgumentException( "Supplied AlgorithmPlugin is not annotated correctly." );
            }

            IEnumerable<Property> properties = _gatherAlgorithmProperties( processType );
            return new AlgorithmDefinition( identifier.PluginName, properties );
        }

        /// <summary>
        /// Gets the identifier attribute from the incoming type
        /// </summary>
        /// <param name="processType">The type to locate the attribute from</param>
        /// <returns>The PluginIdentifierAttribute foudn in the type, or null if one can't
        /// be found</returns>
        private static PluginIdentifierAttribute _getIdentifierAttribute( Type processType )
        {
            var attributes = processType.GetCustomAttributes();
            return attributes.FirstOrDefault( x => x.GetType() == typeof( PluginIdentifierAttribute ) ) as PluginIdentifierAttribute;
        }


        /// <summary>
        /// Constructs a set of Property objects representing the annotated properties
        /// in the type
        /// </summary>
        /// <param name="processType">The type to gather the properties from</param>
        /// <returns>A set of Property objects representing the annotated properties</returns>
        private static IEnumerable<Property> _gatherAlgorithmProperties( Type processType )
        {
            List<Property> properties = new List<Property>();
            foreach( PropertyInfo property in processType.GetProperties() )
            {
                var varAttrs = property.GetCustomAttributes().OfType<PluginVariableAttribute>();
                if( varAttrs.Any() )
                {
                    PluginVariableAttribute attr = varAttrs.First();
                    Property p = new Property( attr.VariableIdentifier, property.PropertyType );
                    properties.Add( p );
                }
            }

            return properties;
        }
    }
}
