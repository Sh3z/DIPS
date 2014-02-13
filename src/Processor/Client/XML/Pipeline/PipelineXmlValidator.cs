using DIPS.Processor.XML.Decompilation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DIPS.Processor.XML.Pipeline
{
    /// <summary>
    /// Represents the Xml validator used to ensure pipeline Xml is valid.
    /// </summary>
    public class PipelineXmlValidator : ValidationVisitor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PipelineXmlValidator"/>
        /// class.
        /// </summary>
        /// <param name="visitor">The <see cref="IXmlVisitor"/> this
        /// <see cref="XmlVisitorDecorate"/> decorates.</param>
        public PipelineXmlValidator( IXmlVisitor visitor )
            : base( visitor )
        {
        }


        /// <summary>
        /// Executes the algorithm-node validation logic.
        /// </summary>
        /// <param name="algorithmNode">The <see cref="XNode"/> representing an
        /// algorithm.</param>
        /// <returns><c>true</c> if the algorithm represented by the
        /// <see cref="XNode"/> is valid; false otherwise.</returns>
        protected override bool IsAlgorithmValid( XNode algorithmNode )
        {
            throw new NotImplementedException();
        }

        protected override bool IsInputValid( XNode inputNode )
        {
            throw new NotImplementedException();
        }
    }
}
