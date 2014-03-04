using DIPS.Processor.Client;
using DIPS.Processor.Registry;
using DIPS.Processor.XML.Compilation;
using DIPS.Processor.XML.Decompilation;
using DIPS.Processor.XML.Pipeline;
using DIPS.Util.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DIPS.Processor.Pipeline
{
    /// <summary>
    /// Represents the pipeline management module of the server.
    /// </summary>
    public class PipelineManager : IPipelineManager
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PipelineManager"/>
        /// class.
        /// </summary>
        /// <param name="repo">The repository of pipeline information.</param>
        /// <param name="algs">The algorithm registry object.</param>
        public PipelineManager( IPipelineRepository repo, IAlgorithmRegistrar algs )
        {
            if( repo == null )
            {
                throw new ArgumentNullException( "repo" );
            }

            if( algs == null )
            {
                throw new ArgumentNullException( "algs" );
            }

            _repo = repo;
            _algorithmRegistry = algs;
        }

        /// <summary>
        /// Gets the set of available processes that can be used to build
        /// a pipeline.
        /// </summary>
        public IEnumerable<AlgorithmDefinition> AvailableProcesses
        {
            get
            {
                return _algorithmRegistry.KnownAlgorithms;
            }
        }

        /// <summary>
        /// Encapsulates the state of a pipeline into Xml for later restoration.
        /// </summary>
        /// <param name="processes">The set of processes within the pipeline to
        /// persist into Xml.</param>
        /// <returns>An <see cref="XDocument"/> describing the pipeline.</returns>
        public XDocument SavePipeline( Client.PipelineDefinition processes )
        {
            if( processes == null )
            {
                return null;
            }

            PipelinePersistanceProcess process = new PipelinePersistanceProcess( _repo.ToDictionary() );
            XmlBuilder builder = new XmlBuilder( process );
            foreach( AlgorithmDefinition def in processes )
            {
                builder.Algorithms.Add( def );
            }

            builder.Build();
            return builder.Xml;
        }

        /// <summary>
        /// Restores the former state of a pipeline from Xml.
        /// </summary>
        /// <param name="pipeline">The <see cref="XDocument"/> describing the
        /// pipeline.</param>
        /// <returns>The set of processes within the pipeline as described in
        /// the Xml.</returns>
        public Client.PipelineDefinition RestorePipeline( XDocument pipeline )
        {
            if( pipeline == null )
            {
                return new Client.PipelineDefinition();
            }

            var factories = _repo.ToDictionary();
            PipelineXmlDecompiler decompiler = new PipelineXmlDecompiler( factories );
            DecompilationVisitor visitor = new DecompilationVisitor( decompiler );
            PipelineXmlValidator validator = new PipelineXmlValidator( visitor, factories.Keys );
            validator.ThrowOnError = true;
            XmlTraverser traverser = new XmlTraverser( validator );

            try
            {
                traverser.Traverse( pipeline );
                var algorithms = visitor.Algorithms;
                _updateAlgorithmNames( algorithms );
                return new Client.PipelineDefinition( algorithms );
            }
            catch( Exception e )
            {
                throw new ArgumentException( "Error restoring Pipeline Xml.", e );
            }
        }


        /// <summary>
        /// Updates the display names of the algorithms using the algorithm reg
        /// </summary>
        /// <param name="algorithms">The algorithms to provide names for</param>
        private void _updateAlgorithmNames( IEnumerable<AlgorithmDefinition> algorithms )
        {
            foreach( AlgorithmDefinition d in algorithms )
            {
                d.DisplayName = _algorithmRegistry.NameForIdentifier( d.AlgorithmName );
            }
        }


        /// <summary>
        /// Retains the pipeline repo object
        /// </summary>
        private IPipelineRepository _repo;

        /// <summary>
        /// Retains the algorithm registry object
        /// </summary>
        private IAlgorithmRegistrar _algorithmRegistry;
    }
}
