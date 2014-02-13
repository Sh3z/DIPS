using DIPS.Processor.Client;
using DIPS.Processor.Client.JobDeployment;
using DIPS.Processor.Plugin;
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
    /// Represents the <see cref="IAlgorithmXmlDecompiler"/> used to persist
    /// the state of a pipeline.
    /// </summary>
    public class PipelineXmlDecompiler : IAlgorithmXmlDecompiler
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PipelineXmlDecompiler"/>
        /// class.
        /// </summary>
        /// <param name="factories">The algorithm name to pipeline factory pairings
        /// providing translation between objects and Xml.</param>
        /// <exception cref="ArgumentNullException">factories is null.</exception>
        public PipelineXmlDecompiler( IDictionary<string, IPipelineXmlInterpreter> factories )
        {
            if( factories == null )
            {
                throw new ArgumentNullException( "factories" );
            }

            _factories = factories;
        }


        /// <summary>
        /// Decompiles an Xml node back into an <see cref="AlgorithmDefinition"/>.
        /// </summary>
        /// <param name="algorithmNode">The <see cref="XNode"/> representing the
        /// <see cref="AlgorithmDefinition"/>.</param>
        /// <returns>An <see cref="AlgorithmDefinition"/> object represented by
        /// the provided Xml.</returns>
        public AlgorithmDefinition DecompileAlgorithm( XNode algorithmNode )
        {
            if( algorithmNode.NodeType != System.Xml.XmlNodeType.Element )
            {
                throw new ArgumentException( "An XElement is required" );
            }

            object paramObj = null;
            XElement element = (XElement)algorithmNode;
            XAttribute nameAttr = element.Attribute( "name" );
            string name = nameAttr.Value;
            AlgorithmDefinition d = new AlgorithmDefinition( name, new Property[] { } );
            XNode propertyNode = element.FirstNode;
            if( propertyNode != null && propertyNode.NodeType == System.Xml.XmlNodeType.Element )
            {
                if( _factories.ContainsKey( name ) == false )
                {
                    throw new ArgumentException( "Unknown process provided" );
                }

                XElement propElement = (XElement)propertyNode;
                IPipelineXmlInterpreter factory = _factories[name];
                d.ParameterObject = factory.CreateObject( propElement );
            }

            return d;
        }

        /// <summary>
        /// Decompiles an Xml node back into a <see cref="JobInput"/>.
        /// </summary>
        /// <param name="inputNode">The <see cref="XNode"/> represnting the
        /// <see cref="JobInput."/></param>
        /// <returns>A <see cref="JobInput"/> object represented by the provided
        /// Xml.</returns>
        public JobInput DecompileInput( XNode inputNode )
        {
            throw new InvalidOperationException( "PipelineXmlDecompile cannot decompile inputs" );
        }


        /// <summary>
        /// Contains the set of factories used in the decompilation procedure.
        /// </summary>
        private IDictionary<string, IPipelineXmlInterpreter> _factories;
    }
}
