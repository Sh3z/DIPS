using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Util.Remoting
{
    /// <summary>
    /// Represents an error in using an <see cref="EventSink"/> to fire
    /// a delegate that is not in a standard format.
    /// </summary>
    [Serializable]
    public class EventSignatureException : RemotingException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventSignatureException"/>
        /// class.
        /// </summary>
        /// <param name="e">The <see cref="EventInfo"/> that is the cause for the
        /// current exception.</param>
        public EventSignatureException( EventInfo e )
            : base( string.Format( "Event \"{0}\" does not accept correct parameter types",
                    e.Name ) )
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventSignatureException"/> class
        /// with serialized data.
        /// </summary>
        /// <param name="info">The serialized information.</param>
        /// <param name="context">The serialization streaming context.</param>
        protected EventSignatureException( SerializationInfo info, StreamingContext context )
            : base( info, context )
        {
        }
    }
}
