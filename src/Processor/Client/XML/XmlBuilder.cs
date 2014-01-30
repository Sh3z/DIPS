using DIPS.Processor.Client;
using DIPS.Processor.Plugin;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DIPS.Processor.XML
{
    /// <summary>
    /// Provides building functionality of DIPS Xml documents used to create
    /// processing job requests.
    /// </summary>
    public class XmlBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="XmlBuilder"/> class.
        /// </summary>
        public XmlBuilder()
        {
            _algorithms = new List<AlgorithmDefinition>();
            _inputs = new List<Bitmap>();
        }


        /// <summary>
        /// Gets the generated <see cref="XDocument"/> from running the
        /// build procedure against this <see cref="XmlBuilder"/>.
        /// </summary>
        public XDocument Xml
        {
            get;
            private set;
        }


        /// <summary>
        /// Appends an <see cref="AlgorithmDefinition"/> to this builder.
        /// </summary>
        /// <param name="definition"></param>
        public void AppendAlgorithm( AlgorithmDefinition definition )
        {
            _algorithms.Add( definition );
        }

        public void BuildJob()
        {
            Xml = new XDocument();
            Xml.Add( new XDeclaration( "1.0", "UTF-8", "yes" ) );
            
        }

        public void BuildProcess()
        {
            Xml = new XDocument();
            Xml.Add( new XDeclaration( "1.0", "UTF-8", "yes" ) );

            XElement algorithm = new XElement( "algorithm-chain" );
            foreach( AlgorithmDefinition definition in _algorithms )
            {
                XAttribute name = new XAttribute( "name", definition.AlgorithmName );

                XElement alg = new XElement( "algorithm" );
            }
        }



        /// <summary>
        /// Contains the set of algorithm definitions in use;
        /// </summary>
        private ICollection<AlgorithmDefinition> _algorithms;

        /// <summary>
        /// Contains the set of image inputs.
        /// </summary>
        private ICollection<Bitmap> _inputs;
    }
}
