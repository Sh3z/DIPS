using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor.XML
{
    /// <summary>
    /// Represents an exception from the <see cref="XmlBuilder"/> class.
    /// </summary>
    public class XmlBuilderException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="XmlBuilderException"/>
        /// class with the specified error message.
        /// </summary>
        /// <param name="error">The message that describes the error.</param>
        public XmlBuilderException( string error )
            : base( error )
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlBuilderException"/>
        /// class with the specified error message.
        /// </summary>
        /// <param name="error">The message that describes the error.</param>
        /// <param name="innerException">The exception that is the cause of the current
        /// exception.</param>
        public XmlBuilderException( string error, Exception innerException )
            : base( error, innerException )
        {
        }
    }
}
