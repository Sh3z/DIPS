using DIPS.Util.Compression;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor.Plugin
{
    /// <summary>
    /// Exposes variables of a plugin exposed using the <see cref="PluginIdentifierAttribute"/>
    /// to metadata. This class cannot be inherited.
    /// </summary>
    [AttributeUsage( AttributeTargets.Property )]
    public sealed class PluginVariableAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PluginVariableAttribute"/>
        /// class.
        /// </summary>
        /// <param name="identifier">The unique name of the identifier of the
        /// variable.</param>
        /// <param name="defaultValue">The default value associated with the
        /// property.</param>
        public PluginVariableAttribute( string identifier, object defaultValue )
        {
            if( string.IsNullOrEmpty( identifier ) )
            {
                throw new ArgumentException( "identifier" );
            }

            VariableIdentifier = identifier;
            DefaultValue = defaultValue;
        }


        /// <summary>
        /// Gets the unique name of the variables identifier.
        /// </summary>
        public string VariableIdentifier
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the default value associated with the variable.
        /// </summary>
        public object DefaultValue
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the <see cref="Type"/> of the object used to compress
        /// the value of the variable.
        /// </summary>
        public Type CompressorType
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the faux type exposes to clients. This overrides the default
        /// type of the property.
        /// </summary>
        /// <remarks>
        /// If this property is set, you must provide a valid converter.
        /// </remarks>
        public Type PublicType
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the type of the <see cref="IValueConverter"/> used to
        /// convert between the PublicType and true type of the variable.
        /// </summary>
        public Type PublicTypeConverter
        {
            get;
            set;
        }
    }
}
