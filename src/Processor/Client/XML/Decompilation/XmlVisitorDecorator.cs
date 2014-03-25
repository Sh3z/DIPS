using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DIPS.Processor.XML.Decompilation
{
    /// <summary>
    /// Represents an <see cref="IXmlVisitor"/> that supplies additional
    /// compound behaviour against an <see cref="IXmlVisitor"/>. This class
    /// is abstract.
    /// </summary>
    public abstract class XmlVisitorDecorator : IXmlVisitor
    {
        /// <summary>
        /// Initializes a new instance of the abstract <see cref="XmlVisitorDecorator"/>
        /// class.
        /// </summary>
        /// <param name="decoratedVisitor"></param>
        protected XmlVisitorDecorator( IXmlVisitor decoratedVisitor )
        {
            if( decoratedVisitor == null )
            {
                throw new ArgumentNullException( "decoratedVisitor" );
            }

            DecoratedVisitor = decoratedVisitor;
        }


        /// <summary>
        /// Gets the decorated <see cref="IXmlVisitor"/> object this
        /// <see cref="XmlVisitorDecorater"/> is decorating.
        /// </summary>
        protected IXmlVisitor DecoratedVisitor
        {
            get;
            private set;
        }


        /// <summary>
        /// Performs the visiting logic against an <see cref="XNode"/> representing
        /// an <see cref="AlgorithmDefinition"/>.
        /// </summary>
        /// <param name="xml">The <see cref="XNode"/> containing the algorithm
        /// information.</param>
        public abstract void VisitAlgorithm( XNode xml );

        /// <summary>
        /// Performs the visiting logic against an <see cref="XNode"/> representing
        /// an input to the job.
        /// </summary>
        /// <param name="xml">The <see cref="XNode"/> representing an input to a
        /// job.</param>
        public abstract void VisitInput( XNode xml );
    }
}
