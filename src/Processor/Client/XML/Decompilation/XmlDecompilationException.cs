using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor.XML.Decompilation
{
    /// <summary>
    /// Represents an error during the decompilation of Xml back into objects.
    /// </summary>
    public class XmlDecompilationException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="XmlDecompilationException"/>
        /// with the provided error message.
        /// </summary>
        /// <param name="err">A description of the decompilation error.</param>
        public XmlDecompilationException( string err )
            : base( err )
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlDecompilationException"/>
        /// with the provided error message and cause of this exception.
        /// </summary>
        /// <param name="err">A description of the decompilation error.</param>
        /// <param name="innerException">The <see cref="Exception"/> that is the cause
        /// of this <see cref="Exception"/>.</param>
        public XmlDecompilationException( string err, Exception innerException )
            : base( err, innerException )
        {
        }
    }
}
