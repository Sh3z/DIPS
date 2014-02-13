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
        /// <param name="availableFactories">The set of available factory names for
        /// the nodes.</param>
        public PipelineXmlValidator( IXmlVisitor visitor, IEnumerable<string> availableFactories )
            : base( visitor )
        {
            if( availableFactories == null )
            {
                throw new ArgumentNullException( "availableFactories" );
            }

            _availableFactories = availableFactories;
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
            if( algorithmNode.NodeType != System.Xml.XmlNodeType.Element )
            {
                return false;
            }

            XElement element = (XElement)algorithmNode;
            if( element.Name != "algorithm" )
            {
                return false;
            }

            XAttribute nameAttr = element.Attribute( "name" );
            if( nameAttr == null )
            {
                return false;
            }

            if( string.IsNullOrEmpty( nameAttr.Value ) )
            {
                return false;
            }

            // If the algorithm has a properties node, we need the correct factory.
            if( element.Descendants( "properties" ).Any() )
            {
                return _availableFactories.Contains( nameAttr.Value );
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Executes the input-node validation logic.
        /// </summary>
        /// <param name="algorithmNode">The <see cref="XNode"/> representing an
        /// input.</param>
        /// <returns><c>true</c> if the input represented by the
        /// <see cref="XNode"/> is valid; false otherwise.</returns>
        protected override bool IsInputValid( XNode inputNode )
        {
            throw new InvalidOperationException( "Pipeline definitions do not support inputs." );
        }


        /// <summary>
        /// Contains the set of available factory names.
        /// </summary>
        private IEnumerable<string> _availableFactories;
    }
}
