using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor.Client
{
    /// <summary>
    /// Represents the isolated processing service.
    /// </summary>
    public interface IDIPS
    {
        /// <summary>
        /// Gets a value indicating whether this <see cref="IDIPS"/> is initialized
        /// and ready to work.
        /// </summary>
        bool Initialized
        {
            get;
        }

        /// <summary>
        /// Gets the <see cref="IProcessingService"/> retaining all the underlying
        /// processing logic used for processing images and managing jobs.
        /// </summary>
        IProcessingService Processor
        {
            get;
        }


        /// <summary>
        /// Sets this <see cref="IDIPS"/> into a running state.
        /// </summary>
        void Start();

        /// <summary>
        /// Halts this <see cref="IDIPS"/> and shuts down the processor.
        /// </summary>
        void Stop();
    }
}
