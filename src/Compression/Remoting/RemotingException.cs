using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Util.Remoting
{
    /// <summary>
    /// Represents an error within the <see cref="DIPS.Util.Remoting"/> namespace.
    /// </summary>
    [Serializable]
    public class RemotingException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RemotingException"/> class.
        /// </summary>
        /// <param name="err">The reason for the error being thrown.</param>
        public RemotingException( string err )
            : base( err )
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RemotingException"/> class.
        /// </summary>
        /// <param name="err">The reason for the error being thrown.</param>
        /// <param name="innerException">The cause for the current Exception.</param>
        public RemotingException( string err, Exception innerException )
            : base( err, innerException )
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RemotingException"/> class
        /// with serialized data.
        /// </summary>
        /// <param name="info">The serialized information.</param>
        /// <param name="context">The serialization streaming context.</param>
        protected RemotingException( SerializationInfo info, StreamingContext context )
            : base( info, context )
        {
        }
    }
}
