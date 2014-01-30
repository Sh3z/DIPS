using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor.Client
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
        public PluginVariableAttribute( string identifier )
        {
            if( string.IsNullOrEmpty( identifier ) )
            {
                throw new ArgumentException( "identifier" );
            }

            VariableIdentifier = identifier;
        }


        /// <summary>
        /// Gets the unique name of the variables identifier.
        /// </summary>
        public string VariableIdentifier
        {
            get;
            private set;
        }
    }
}
