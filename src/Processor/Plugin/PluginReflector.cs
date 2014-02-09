using DIPS.Processor.Client;
using DIPS.Util.Compression;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

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
        /// is not annotated with the <see cref="AlgorithmAttribute"/>
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
        /// Constructs an <see cref="AlgorithmDefinition"/> from the provided
        /// <see cref="Type"/>.
        /// </summary>
        /// <param name="pluginType">The <see cref="Type"/> to construct an
        /// <see cref="AlgorithmDefinition"/> for.</param>
        /// <returns>An <see cref="AlgorithmDefinition"/> representing the
        /// structure of the <see cref="Type"/> provided.</returns>
        /// <exception cref="ArgumentNullException">pluginType is null.</exception>
        /// <exception cref="ArgumentException">pluginType is not a subclass of
        /// <see cref="AlgorithmPlugin"/>; the supplied <see cref="Type"/>
        /// is not annotated with the <see cref="AlgorithmAttribute"/>
        /// attribute.</exception>
        public static AlgorithmDefinition CreateDefinition( Type pluginType )
        {
            if( pluginType == null )
            {
                throw new ArgumentNullException( "pluginType" );
            }
            else
            {
                return _createDefinition( pluginType );
            }
        }


        /// <summary>
        /// Creates the AlgorithmDefinition from the incoming type
        /// </summary>
        /// <param name="processType">The type to construct the definition for</param>
        /// <returns>An AlgorithmDefinition representing the incoming type</returns>
        private static AlgorithmDefinition _createDefinition( Type processType )
        {
            if( processType.IsSubclassOf( typeof( AlgorithmPlugin ) ) == false )
            {
                throw new ArgumentException(
                    string.Format( "Type {0} is not a subclass of {1}", processType, typeof( AlgorithmPlugin ) ) );
            }

            AlgorithmAttribute identifier = _getIdentifierAttribute( processType );
            if( identifier == null )
            {
                throw new ArgumentException( "Supplied AlgorithmPlugin is not annotated correctly." );
            }

            IEnumerable<Property> properties = _gatherAlgorithmProperties( processType );
            AlgorithmDefinition d = new AlgorithmDefinition( identifier.PluginName, properties );

            // Add the metadata if it has been provided.
            AlgorithmMetadataAttribute attr = _getMetadataAttribute( processType );
            if( attr != null )
            {
                d.DisplayName = attr.DisplayName;
                d.Description = attr.Description;
            }

            return d;
        }

        /// <summary>
        /// Gets the identifier attribute from the incoming type
        /// </summary>
        /// <param name="processType">The type to locate the attribute from</param>
        /// <returns>The PluginIdentifierAttribute foudn in the type, or null if one can't
        /// be found</returns>
        private static AlgorithmAttribute _getIdentifierAttribute( Type processType )
        {
            var attributes = processType.GetCustomAttributes();
            return attributes.FirstOrDefault( x => x.GetType() == typeof( AlgorithmAttribute ) ) as AlgorithmAttribute;
        }

        /// <summary>
        /// Gets the metata attribtue from the incoming type
        /// </summary>
        /// <param name="processType">The type to locate the attribute from</param>
        /// <returns>The AlgorithmMetadataAttribute found in the type, or null if one
        /// can't be found</returns>
        private static AlgorithmMetadataAttribute _getMetadataAttribute( Type processType )
        {
            var attributes = processType.GetCustomAttributes();
            return attributes.FirstOrDefault( x => x.GetType() == typeof( AlgorithmMetadataAttribute ) ) as AlgorithmMetadataAttribute;
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
                var varAttrs = property.GetCustomAttributes().OfType<AlgorithmPropertyAttribute>();
                if( varAttrs.Any() )
                {
                    AlgorithmPropertyAttribute attr = varAttrs.First();
                    Property p = _createProperty( property, attr );
                    properties.Add( p );
                }
            }

            return properties;
        }

        /// <summary>
        /// Constructs the Property definition using the CLR information and
        /// the metadata provided in the attribute
        /// </summary>
        /// <param name="property">The reflected information about the property
        /// within the class</param>
        /// <param name="attr">The additional information provided by the
        /// plugin</param>
        /// <returns>A Property definition</returns>
        private static Property _createProperty( PropertyInfo property, AlgorithmPropertyAttribute attr )
        {
            _guardBadAttribute( attr );

            PropertyBuilder b = new PropertyBuilder();
            b.Name = attr.VariableIdentifier;
            b.PropertyType = property.PropertyType;
            b.PublicType = attr.PublicType;
            b.DefaultValue = attr.DefaultValue;

            if( attr.CompressorType != null )
            {
                b.Compressor = Activator.CreateInstance( attr.CompressorType ) as ICompressor;
            }

            if( attr.PublicTypeConverter != null )
            {
                b.Converter = Activator.CreateInstance( attr.PublicTypeConverter ) as IValueConverter;
            }

            return b.Build();
        }

        /// <summary>
        /// Scrutinizes the variable attribute and ensures it has been annotated correctly.
        /// </summary>
        /// <param name="attr">The attribute to scrutinize.</param>
        private static void _guardBadAttribute( AlgorithmPropertyAttribute attr )
        {
            if( attr.CompressorType != null )
            {
                CompressorAttribute cattr = attr.CompressorType.GetCustomAttribute(
                    typeof( CompressorAttribute ) ) as CompressorAttribute;
                if( cattr == null )
                {
                    throw new ArgumentException( "Compressor type provided not annotated." );
                }

                if( attr.CompressorType.GetInterfaces().Contains( typeof( ICompressor ) ) == false )
                {
                    throw new ArgumentException( "Compressor type does not implement ICompressor." );
                }
            }

            if( attr.PublicType != null )
            {
                if( attr.PublicTypeConverter == null )
                {
                    throw new ArgumentException( "Faux type converter not provided." );
                }

                if( attr.PublicTypeConverter.GetInterfaces().Contains( typeof( IValueConverter ) ) == false )
                {
                    throw new ArgumentException( "PublicTypeConverter does not implement IValueConverter." );
                }
            }
        }
    }
}
