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
        public XmlBuilder( IBuilderProcess process )
        {
            if( process == null )
            {
                throw new ArgumentNullException( "process" );
            }

            _process = process;
            _algorithms = new List<AlgorithmDefinition>();
            _inputs = new List<JobInput>();
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
            Xml = new XDocument();
            Xml.Add( new XDeclaration( "1.0", "UTF-8", "yes" ) );

            if( _algorithms.Any() )
            {
                _buildAlgorithms();
            }

            if( _inputs.Any() )
            {
                _buildInputs();
            }
        }

        /// <summary>
        /// Builds all the inputs into the current Xml.
        /// </summary>
        private void _buildInputs()
        {
            ICollection<XElement> inputs = new List<XElement>();
            foreach( var input in _inputs )
            {
                XElement xml = _process.BuildInput( input );
                inputs.Add( xml );
            }

            Xml.Add( new XElement( "inputs" ) );
        }

        /// <summary>
        /// Builds all the algorithms into the current Xml.
        /// </summary>
        private void _buildAlgorithms()
        {
            ICollection<XElement> algorithms = new List<XElement>();
            foreach( var def in _algorithms )
            {
                XElement xml = _process.Build( def );
                algorithms.Add( xml );
            }

            Xml.Add( new XElement( "algorithms", algorithms ) );
        }

        
        /// <summary>
        /// Contains the set of algorithm definitions in use;
        /// </summary>
        private ICollection<AlgorithmDefinition> _algorithms;

        /// <summary>
        /// Contains the set of image inputs.
        /// </summary>
        private ICollection<JobInput> _inputs;

        /// <summary>
        /// Contains the actual building process to use against each algorithm.
        /// </summary>
        private IBuilderProcess _process;
    }
}
