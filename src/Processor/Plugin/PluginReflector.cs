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

            Type processType = plugin.GetType();
            var attributes = processType.GetCustomAttributes();
            PluginIdentifierAttribute identifier =
                attributes.FirstOrDefault( x => x.GetType() == typeof( PluginIdentifierAttribute ) ) as PluginIdentifierAttribute;
            if( identifier == null )
            {
                throw new ArgumentException( "Supplied AlgorithmPlugin is not annotated correctly." );
            }

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

            return new AlgorithmDefinition( identifier.PluginName, properties );
        }
    }
}
