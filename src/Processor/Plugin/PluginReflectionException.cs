using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor.Plugin
{
    /// <summary>
    /// Represents an error in analyzing a plugin by the <see cref="PluginReflector"/>.
    /// </summary>
    public class PluginReflectionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PluginReflectionException"/>
        /// with the provided error message.
        /// </summary>
        /// <param name="err">The reason for this Exception.</param>
        public PluginReflectionException( string err )
            : base( err )
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PluginReflectionException"/>
        /// with the provided error message and inner cause of the exception.
        /// </summary>
        /// <param name="err">The reason for this Exception.</param>
        /// <param name="innerException">The Exception that is the cause of the
        /// current Exception.</param>
        public PluginReflectionException( string err, Exception innerException )
            : base( err, innerException )
        {
        }
    }
}
