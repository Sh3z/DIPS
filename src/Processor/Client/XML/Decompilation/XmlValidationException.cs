using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DIPS.Processor.XML.Decompilation
{
    /// <summary>
    /// Represents an error pertaining to the validation of Job Xml.
    /// </summary>
    public class XmlValidationException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="XmlValidationException"/>
        /// class with the failed node.
        /// </summary>
        /// <param name="node">The <see cref="XNode"/> that failed validation.</param>
        public XmlValidationException( XNode node )
            : base( string.Format( "Error validating Xml: {0}", node ) )
        {
        }
    }
}
