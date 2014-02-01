using DIPS.Util.Compression;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace DIPS.Processor.Client
{
    /// <summary>
    /// Provides building of complex <see cref="Property"/> objects. This
    /// class cannot be inherited.
    /// </summary>
    public sealed class PropertyBuilder
    {
        public string Name
        {
            get;
            set;
        }

        public Type PropertyType
        {
            get;
            set;
        }

        public Type PublicType
        {
            get;
            set;
        }

        public ICompressor Compressor
        {
            get;
            set;
        }

        public IValueConverter Converter
        {
            get;
            set;
        }

        public object DefaultValue
        {
            get;
            set;
        }

        public Property Build()
        {
            Property property = new Property( Name, PropertyType );
            if( PublicType != null )
            {
                if( Converter == null )
                {
                    throw new InvalidOperationException( "Public type specified with no converter." );
                }

                property.Type = PublicType;
                property.Converter = Converter;
            }
            else
            {
                property.Type = PropertyType;
            }

            property.Value = _resolveDefaultValue( property.Type );
            property.Compressor = Compressor;
            return property;
        }


        /// <summary>
        /// Resolves an appropriate default value for the property.
        /// </summary>
        /// <param name="propertyType">The Type of the new Property.</param>
        /// <returns>The default value provided, or a default value for
        /// non-nullable types when null is provided</returns>
        private object _resolveDefaultValue( Type propertyType )
        {
            if( DefaultValue == null && propertyType.IsValueType )
            {
                return Activator.CreateInstance( propertyType );
            }
            else
            {
                return DefaultValue;
            }
        }
    }
}
