using DIPS.Processor.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor.Pipeline
{
    /// <summary>
    /// Represents a factory used to create pipelines from the information
    /// provided by a client.
    /// </summary>
    public interface IPipelineFactory
    {
        /// <summary>
        /// Creates a new <see cref="Pipeline"/> from the provided definition.
        /// </summary>
        /// <param name="def">The <see cref="PipelineDefinition"/> describing
        /// the pipeline.</param>
        /// <returns>A <see cref="Pipeline"/> able to perform the processing described
        /// in the definition.</returns>
        Pipeline CreatePipeline( PipelineDefinition def );
    }
}
