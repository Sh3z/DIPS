using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor.Client
{
    /// <summary>
    /// Represents a processor that can be used to immediatley process and
    /// return a result on the same thread as the caller.
    /// </summary>
    public interface ISynchronousProcessor
    {
        /// <summary>
        /// Processes the provided <see cref="JobRequest"/> and returns the
        /// result. This can be run asynchronously.
        /// </summary>
        /// <param name="request">The <see cref="JobRequest"/> providing
        /// information about the job to run.</param>
        /// <returns>A <see cref="Task"/> which computes the <see cref="JobResult"/>
        /// from the given request.</returns>
        Task<JobResult> Process( JobRequest request );
    }
}
