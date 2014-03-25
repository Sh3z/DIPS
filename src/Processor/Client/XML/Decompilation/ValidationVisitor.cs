using DIPS.Processor.Registry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DIPS.Processor.XML.Decompilation
{
    /// <summary>
    /// Represents the visitor used to ensure the Xml provided by the traverser
    /// is in a valid format before executing the behaviour of another
    /// <see cref="IXmlVisitor"/>. This class is abstract.
    /// </summary>
    public abstract class ValidationVisitor : XmlVisitorDecorator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationVisitor"/>
        /// class.
        /// </summary>
        /// <param name="visitor">The <see cref="IXmlVisitor"/> that is called
        /// when nodes are validated correctly.</param>
        /// <exception cref="NullReferenceException">visitor is null.</exception>
        protected ValidationVisitor( IXmlVisitor visitor )
            : base( visitor )
        {
        }


        /// <summary>
        /// Gets or sets whether this <see cref="ValidationVisitor"/> should throw an
        /// exception when an invalid node is detected.
        /// </summary>
        public bool ThrowOnError
        {
            get;
            set;
        }


        /// <summary>
        /// Performs the visiting logic against an <see cref="XNode"/> representing
        /// an <see cref="AlgorithmDefinition"/>.
        /// </summary>
        /// <param name="xml">The <see cref="XNode"/> containing the algorithm
        /// information.</param>
        public override void VisitAlgorithm( XNode xml )
        {
            if( IsAlgorithmValid( xml ) )
            {
                DecoratedVisitor.VisitAlgorithm( xml );
            }
            else
            {
                _throwIfNecessary( xml );
            }
        }

        /// <summary>
        /// Performs the visiting logic against an <see cref="XNode"/> representing
        /// an input to the job.
        /// </summary>
        /// <param name="xml">The <see cref="XNode"/> representing an input to a
        /// job.</param>
        public override void VisitInput( XNode xml )
        {
            if( IsInputValid( xml ) )
            {
                DecoratedVisitor.VisitInput( xml );
            }
            else
            {
                _throwIfNecessary( xml );
            }
        }


        /// <summary>
        /// Executes the algorithm-node validation logic.
        /// </summary>
        /// <param name="algorithmNode">The <see cref="XNode"/> representing an
        /// algorithm.</param>
        /// <returns><c>true</c> if the algorithm represented by the
        /// <see cref="XNode"/> is valid; false otherwise.</returns>
        protected abstract bool IsAlgorithmValid( XNode algorithmNode );

        /// <summary>
        /// Executes the input-node validation logic.
        /// </summary>
        /// <param name="algorithmNode">The <see cref="XNode"/> representing an
        /// input.</param>
        /// <returns><c>true</c> if the input represented by the
        /// <see cref="XNode"/> is valid; false otherwise.</returns>
        protected abstract bool IsInputValid( XNode inputNode );


        /// <summary>
        /// Throws an XmlValidationException if the args specify we should throw
        /// when invalid Xml is provided.
        /// </summary>
        /// <param name="errNode">The troublesome node.</param>
        private void _throwIfNecessary( XNode errNode )
        {
            if( ThrowOnError )
            {
                throw new XmlValidationException( errNode );
            }
        }
    }
}
