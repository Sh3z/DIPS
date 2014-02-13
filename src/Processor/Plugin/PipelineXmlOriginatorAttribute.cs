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
        public PipelineXmlOriginatorAttribute( Type pluginType )
        {
            if( pluginType == null )
            {
                throw new ArgumentNullException( "pluginType" );
            }


        }
    }
}
