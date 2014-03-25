using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor.Plugin
{
    /// <summary>
    /// Exposes classes responsible for generating Xml mementos of pipeline
    /// plugins. This class cannot be inherited.
    /// </summary>
    public sealed class PipelineXmlOriginatorAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="PipelineXmlOriginatorAttribute"/> class.
        /// </summary>
        /// <param name="pluginType">The <see cref="Type"/> of the plugin
        /// the annotated class creates Xml for.</param>
        public PipelineXmlOriginatorAttribute( Type pluginType )
        {
            if( pluginType == null )
            {
                throw new ArgumentNullException( "pluginType" );
            }

            if( pluginType.IsSubclassOf( typeof( AlgorithmPlugin ) ) == false )
            {
                throw new ArgumentException(
                    "Type provided to PipelineXmlOriginatorAttribtue must subclass AlgorithmPlugin" );
            }

            PluginType = pluginType;
        }


        /// <summary>
        /// Gets the <see cref="Type"/> of the plugin the associated Xml
        /// originator creates Xml for.
        /// </summary>
        public Type PluginType
        {
            get;
            private set;
        }
    }
}
