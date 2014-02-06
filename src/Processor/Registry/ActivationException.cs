using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor.Registry
{
    /// <summary>
    /// Thrown when the <see cref="AlgorithmActivator"/> class cannot
    /// activate a plugin.
    /// </summary>
    public class ActivationException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ActivationException"/> class.
        /// </summary>
        /// <param name="err">A string describing the error.</param>
        public ActivationException( string err )
            : base( err )
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ActivationException"/> class.
        /// </summary>
        /// <param name="err">A string describing the error.</param>
        /// <param name="innerException">The Exception that is the cause of the
        /// current Exception.</param>
        public ActivationException( string err, Exception innerException )
            : base( err, innerException )
        {
        }
    }
}
