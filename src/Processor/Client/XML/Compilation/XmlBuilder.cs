using DIPS.Processor.Client;
using DIPS.Processor.Client.JobDeployment;
using DIPS.Processor.Plugin;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DIPS.Processor.XML.Compilation
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
        /// <exception cref="ArgumentNullException">process is null.</exception>
        public XmlBuilder( IBuilderProcess process )
        {
            if( process == null )
            {
                throw new ArgumentNullException( "process" );
            }

            _process = process;
            Algorithms = new List<AlgorithmDefinition>();
            Inputs = new List<JobInput>();
        }

        /// <summary>
        /// Gets a modifyable collection of <see cref="AlgorithmDefinition"/>s this
        /// <see cref="XmlBuilder"/> will use in the build procedure.
        /// </summary>
        public ICollection<AlgorithmDefinition> Algorithms
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets a modifyable collection of <see cref="JobInput"/>s this
        /// <see cref="XmlBuilder"/> will use in the build procedure.
        /// </summary>
        public ICollection<JobInput> Inputs
        {
            get;
            private set;
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
        /// Executes the build procedure and constructs the Xml using
        /// the information provided to this <see cref="XmlBuilder"/>.
        /// </summary>
        public void Build()
        {
            try
            {
                _performBuild();
            }
            catch( Exception e )
            {
                Xml = new XDocument();
                string err = "An Exception occured while building the document.";
                throw new XmlBuilderException( err, e );
            }
        }


        /// <summary>
        /// Builds the Xml.
        /// </summary>
        private void _performBuild()
        {
            Xml = new XDocument( new XDeclaration( "1.0", "UTF-8", "yes" ) );
            ICollection<XNode> children = new List<XNode>();

            if( Algorithms.Any() )
            {
                XNode algorithms = _buildAlgorithms();
                children.Add( algorithms );
            }

            if( Inputs.Any() )
            {
                XNode inputs = _buildInputs();
                children.Add( inputs );
            }

            XElement root = new XElement( _process.RootNodeName, children );
            Xml.Add( root );
        }

        /// <summary>
        /// Builds all the inputs into the current Xml.
        /// </summary>
        /// <returns>An XNode representing all the inputs.</returns>
        private XNode _buildInputs()
        {
            ICollection<XElement> inputs = new List<XElement>();
            foreach( var input in Inputs )
            {
                XElement xml = _process.BuildInput( input );
                inputs.Add( xml );
            }

            return new XElement( "inputs", inputs );
        }

        /// <summary>
        /// Builds all the algorithms into the current Xml.
        /// </summary>
        /// <returns>An XNode representing all the algorithms.</returns>
        private XNode _buildAlgorithms()
        {
            ICollection<XElement> algorithms = new List<XElement>();
            foreach( var def in Algorithms )
            {
                XElement xml = _process.Build( def );
                algorithms.Add( xml );
            }

            return new XElement( "algorithms", algorithms );
        }


        /// <summary>
        /// Contains the actual building process to use against each algorithm.
        /// </summary>
        private IBuilderProcess _process;
    }
}
