using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DIPS.Processor.XML.Decompilation
{
    /// <summary>
    /// Provides constructor information for the <see cref="ValidationVisitor"/>
    /// class. This class cannot be inherited.
    /// </summary>
    public sealed class XmlValidatorArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="XmlValidatorArgs"/>
        /// class.
        /// </summary>
        /// <param name="visitor">The <see cref="IJobXmlVisitor"/> that is
        /// decorated by the validator.</param>
        /// <exception cref="ArgumentNullException">visitor is null.</exception>
        public XmlValidatorArgs( IJobXmlVisitor visitor )
        {
            if( visitor == null )
            {
                throw new ArgumentNullException( "visitor" );
            }

            Visitor = visitor;
            ThrowOnError = false;
        }


        /// <summary>
        /// Gets the <see cref="IJobXmlVisitor"/> being decorated by the
        /// <see cref="ValidationVisitor"/>.
        /// </summary>
        public IJobXmlVisitor Visitor
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the custom function used by the validator to validate
        /// algorithm Xml.
        /// </summary>
        public Func<XNode, bool> AlgorithmValidator
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the custom function used by the validator to validate
        /// input Xml.
        /// </summary>
        public Func<XNode, bool> InputValidator
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets whether to throw an exception when invalid Xml is
        /// provided.
        /// </summary>
        public bool ThrowOnError
        {
            get;
            set;
        }
    }
}
