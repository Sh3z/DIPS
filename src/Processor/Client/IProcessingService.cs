using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DIPS.Processor.Client
{
    /// <summary>
    /// Represents a DIPS processing service.
    /// </summary>
    public interface IProcessingService
    {
        /// <summary>
        /// Gets the <see cref="IJobManager"/> module of this
        /// <see cref="IProcessingService"/>.
        /// </summary>
        IJobManager JobManager
        {
            get;
        }

        /// <summary>
        /// Gets the <see cref="IPipelineManager"/> module of this
        /// <see cref="IProcessingService"/>.
        /// </summary>
        IPipelineManager PipelineManager
        {
            get;
        }

        /// <summary>
        /// Creates and returns a new <see cref="ISynchronousProcessor"/> clients
        /// can use to compute jobs on their own threads.
        /// </summary>
        /// <returns>An <see cref="ISynchronousProcessor"/> instance for running
        /// jobs within the client.</returns>
        ISynchronousProcessor CreateSynchronousProcessor();
    }
}
