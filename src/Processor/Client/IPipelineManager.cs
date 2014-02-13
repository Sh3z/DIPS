using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DIPS.Processor.Client
{
    /// <summary>
    /// Represents the module of the <see cref="IProcessingService"/> providing
    /// clients with the ability to create and restore pipelines.
    /// </summary>
    public interface IPipelineManager
    {
        /// <summary>
        /// Gets the set of available processes that can be used to build
        /// a pipeline.
        /// </summary>
        IEnumerable<AlgorithmDefinition> AvailableProcesses
        {
            get;
        }

        /// <summary>
        /// Encapsulates the state of a pipeline into Xml for later restoration.
        /// </summary>
        /// <param name="processes">The set of processes within the pipeline to
        /// persist into Xml.</param>
        /// <returns>An <see cref="XDocument"/> describing the pipeline.</returns>
        XDocument SavePipeline( IEnumerable<AlgorithmDefinition> processes );

        /// <summary>
        /// Restores the former state of a pipeline from Xml.
        /// </summary>
        /// <param name="pipeline">The <see cref="XDocument"/> describing the
        /// pipeline.</param>
        /// <returns>The set of processes within the pipeline as described in
        /// the Xml.</returns>
        IEnumerable<AlgorithmDefinition> RestorePipeline( XDocument pipeline );
    }
}
