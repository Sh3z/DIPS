using DIPS.Processor.Client;
using DIPS.Processor.Client.JobDeployment;
using DIPS.Processor.Plugin;
using DIPS.Processor.XML.Compilation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DIPS.Processor.XML.Pipeline
{
    /// <summary>
    /// Represents the <see cref="IBuilderProcess"/> used to encapsulate
    /// a pipeline in Xml for later restoration.
    /// </summary>
    public class PipelinePersistanceProcess : IBuilderProcess
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PipelinePersistanceProcess"/>.
        /// </summary>
        /// <param name="factories">The set of algorithm name to factory pairings providing
        /// persistance of algorithm property values.</param>
        /// <exception cref="ArgumentNullException">factories is null</exception>
        public PipelinePersistanceProcess( IDictionary<string, IPipelineXmlInterpreter> factories )
        {
            if( factories == null )
            {
                throw new ArgumentNullException( "factories" );
            }

            _factories = factories;
        }


        /// <summary>
        /// Gets the name to give to the root node of the final Xml document.
        /// </summary>
        public string RootNodeName
        {
            get
            {
                return "pipeline";
            }
        }

        /// <summary>
        /// Constructs Xml using the <see cref="AlgorithmDefinition"/>.
        /// </summary>
        /// <param name="definition">The <see cref="AlgorithmDefinition"/>
        /// detailing the process and its properties.</param>
        /// <returns>An <see cref="XElement"/> representing the
        /// <see cref="AlgorithmDefinition"/>.</returns>
        public XElement Build( AlgorithmDefinition definition )
        {
            string algName = definition.AlgorithmName;
            XAttribute name = new XAttribute( "name", algName );

            if( definition.ParameterObject != null )
            {
                if( _factories.ContainsKey( algName ) == false )
                {
                    throw new ArgumentException( "Unknown algorithm provided" );
                }

                IPipelineXmlInterpreter factory = _factories[algName];
                var props = factory.CreateXml( definition.ParameterObject );
                XElement properties = new XElement( "properties", props );
                return new XElement( "process", name, properties );
            }
            else
            {
                return new XElement( "process", name );
            }
        }

        /// <summary>
        /// Constructs Xml using the <see cref="JobInput"/>.
        /// </summary>
        /// <param name="input">The <see cref="JobInput"/> to be used as input
        /// in a job.</param>
        /// <returns>An <see cref="XElement"/> representing the
        /// <see cref="JobInput"/>.</returns>
        /// <exception cref="InvalidOperationException">this
        /// <see cref="IBuilderProcess"/> does not support building
        /// inputs.</exception>
        public XElement BuildInput( JobInput input )
        {
            throw new InvalidOperationException( "PipelinePersistanceProcess cannot built inputs." );
        }


        /// <summary>
        /// Contains the set of factories to use in persisting properties.
        /// </summary>
        private IDictionary<string, IPipelineXmlInterpreter> _factories;
    }
}
