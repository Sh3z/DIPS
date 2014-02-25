using DIPS.Processor.Client;
using DIPS.Processor.Plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor.Pipeline
{
    /// <summary>
    /// Represents the <see cref="IPipelineFactory"/> that creates
    /// <see cref="Pipeline"/>s using the plugin system.
    /// </summary>
    public class PluginPipelineFactory : IPipelineFactory
    {
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="PluginPipelineFactory"/> class.
        /// </summary>
        /// <param name="factory">The <see cref="IPluginFactory"/> used
        /// to convert individual algorithms into plugins.</param>
        /// <exception cref="ArgumentNullException">factory is null</exception>
        public PluginPipelineFactory( IPluginFactory factory )
        {
            if( factory == null )
            {
                throw new ArgumentNullException( "factory" );
            }

            _factory = factory;
        }


        /// <summary>
        /// Creates a new <see cref="Pipeline"/> from the provided definition.
        /// </summary>
        /// <param name="def">The <see cref="PipelineDefinition"/> describing
        /// the pipeline.</param>
        /// <returns>A <see cref="Pipeline"/> able to perform the processing described
        /// in the definition.</returns>
        public Pipeline CreatePipeline( PipelineDefinition def )
        {
            Pipeline p = new Pipeline();
            if( def == null )
            {
                return p;
            }

            foreach( var definition in def )
            {
                PipelineEntry entry = _tryCreateEntry( definition );
                p.Add( entry );
            }

            return p;
        }


        /// <summary>
        /// Attempts to create the plugin with the specified definition
        /// </summary>
        /// <param name="def">The definition provided by the client</param>
        /// <returns>The PipelineEntry represented by the definition,
        /// or null if an error occurs</returns>
        private PipelineEntry _tryCreateEntry( AlgorithmDefinition def )
        {
            PipelineEntry entry = null;
            try
            {
                AlgorithmPlugin p = _factory.Manufacture( def );
                if( p != null )
                {
                    entry = new PipelineEntry( p );
                    entry.ProcessInput = (ICloneable)def.ParameterObject.Clone();
                }
            }
            catch
            {
                // Todo logging
            }

            return entry;
        }


        /// <summary>
        /// Contains the factory that converts client definitions into plugins.
        /// </summary>
        private IPluginFactory _factory;
    }
}
