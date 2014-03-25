using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor.Plugin
{
    /// <summary>
    /// Represents a generic error within a plugin.
    /// </summary>
    [Serializable]
    public class PluginException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PluginException"/> class.
        /// </summary>
        public PluginException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PluginException"/> class
        /// with the specified error message.
        /// </summary>
        /// <param name="err">The reason for the Exception.</param>
        public PluginException( string err )
            : base( err )
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PluginException"/> class
        /// with the specified error message and cause of exception.
        /// </summary>
        /// <param name="err">The reason for the Exception.</param>
        /// <param name="innerException">The Exception that is the cause of the current
        /// Exception.</param>
        public PluginException( string err, Exception innerException )
            : base( err, innerException )
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PluginException"/> class
        /// with serialized data.
        /// </summary>
        /// <param name="info">The System.Runtime.Serialization.SerializationInfo
        /// that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The System.Runtime.Serialization.StreamingContext
        /// that contains contextual information about the source or destination.</param>
        protected PluginException( SerializationInfo info, StreamingContext context )
            : base( info, context )
        {
        }
    }
}
