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
            if( PublicType != null && Converter == null )
            {
                throw new InvalidOperationException( "Public type specified with no converter." );
            }
            else
            {
                property.Type = PublicType;
                property.Converter = Converter;
            }

            property.Value = DefaultValue;
            property.Compressor = Compressor;
            return property;
        }
    }
}
