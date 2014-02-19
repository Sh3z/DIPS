using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DIPS.Util.Extensions;

namespace DIPS.Processor.Plugin
{
    /// <summary>
    /// Represents a plugin against the DIPS processing system. This class cannot
    /// be inherited.
    /// </summary>
    /// <remarks>
    /// Objects annotated with this <see cref="Attribute"/> must subclass the <see cref="Plugin"/>
    /// class.
    /// </remarks>
    [AttributeUsage( AttributeTargets.Class, AllowMultiple = false )]
    public sealed class AlgorithmAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AlgorithmAttribute"/>
        /// class with the name of the plugin.
        /// </summary>
        /// <param name="pluginName">The unique name of this plugin.</param>
        public AlgorithmAttribute( string pluginName )
        {
            if( string.IsNullOrEmpty( pluginName ) )
            {
                throw new ArgumentException( "pluginName" );
            }

            PluginName = pluginName;
        }


        /// <summary>
        /// Gets the name of the plugin represented by the class annotated with
        /// this <see cref="AlgorithmAttribute"/>.
        /// </summary>
        public string PluginName
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the <see cref="Type"/> of the object describing the
        /// parameters used by the annotated plugin.
        /// </summary>
        public Type ParameterObjectType
        {
            get
            {
                return _paramObjType;
            }
            set
            {
                if( value.Implements<ICloneable>() )
                {
                    _paramObjType = value;
                }
                else
                {
                    throw new ArgumentException( "Parameter object classes must implement ICloneable" );
                }
            }
        }
        [DebuggerBrowsable( DebuggerBrowsableState.Never )]
        private Type _paramObjType;
    }
}
