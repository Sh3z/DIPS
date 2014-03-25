using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor.Plugin
{
    /// <summary>
    /// Represents an error during the exception of an algorithm plugin.
    /// </summary>
    public class AlgorithmException : PluginException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AlgorithmException"/> class.
        /// </summary>
        public AlgorithmException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AlgorithmException"/> class with
        /// the reason for the Exception.
        /// </summary>
        /// <param name="reason">The reason for the error.</param>
        public AlgorithmException( string reason )
            : base( reason )
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AlgorithmException"/> class with
        /// the reason for the Exception, and the inner Exception that occured.
        /// </summary>
        /// <param name="reason">The reason for the error.</param>
        /// <param name="innerException">The internal Exception that is the cause for this
        /// Exception.</param>
        public AlgorithmException( string reason, Exception innerException )
            : base( reason, innerException )
        {
        }
    }
}
