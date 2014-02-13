using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DIPS.Processor.XML.Decompilation
{
    /// <summary>
    /// Traverses Xml and provides visiting of appropriate nodes.
    /// </summary>
    public class XmlTraverser
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="XmlTraverser"/> class.
        /// </summary>
        /// <param name="visitor">The <see cref="IXmlVisitor"/> passed into
        /// various nodes of the Xml.</param>
        /// <exception cref="ArgumentNullException">visitor is null.</exception>
        public XmlTraverser( IXmlVisitor visitor )
        {
            if( visitor == null )
            {
                throw new ArgumentNullException( "visitor" );
            }

            _visitor = visitor;
        }

        /// <summary>
        /// Begins the traversal procedure of the Xml document.
        /// </summary>
        /// <param name="xml">The <see cref="XDocument"/> in which to
        /// traverse.</param>
        public void Traverse( XDocument xml )
        {
            if( xml == null )
            {
                return;
            }

            foreach( XNode node in xml.Descendants( "input" ) )
            {
                _visitor.VisitInput( node );
            }

            foreach( XNode node in xml.Descendants( "algorithm" ) )
            {
                _visitor.VisitAlgorithm( node );
            }
        }


        /// <summary>
        /// Contains the visitor we accept into portions of the document.
        /// </summary>
        private IXmlVisitor _visitor;
    }
}
