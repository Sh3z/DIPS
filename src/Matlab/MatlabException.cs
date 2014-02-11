using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Matlab
{
    /// <summary>
    /// Represents an error that occurs within the DIPS.Matlab namespace.
    /// </summary>
    [Serializable]
    public class MatlabException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MatlabException"/> class
        /// with the specified error message.
        /// </summary>
        /// <param name="err">The reason for the current Exception being thrown.</param>
        public MatlabException( string err )
            : base( err )
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MatlabException"/> class
        /// with the specified error message and cause of error.
        /// </summary>
        /// <param name="err">The reason for the current Exception being thrown.</param>
        /// <param name="innerException">The Exception that is the cause of the
        /// current Exception.</param>
        public MatlabException( string err, Exception innerException )
            : base( err, innerException )
        {
        }
    }
}
