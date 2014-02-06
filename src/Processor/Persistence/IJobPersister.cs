using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor.Persistence
{
    /// <summary>
    /// Represents the object used to persist job outputs.
    /// </summary>
    public interface IJobPersister
    {
        /// <summary>
        /// Persits the output of a job.
        /// </summary>
        /// <param name="output">The <see cref="Image"/> generated from a
        /// complete job.</param>
        /// <param name="identifier">The identifier for the input provided
        /// by the client.</param>
        void Persist( Image output, object identifier );
    }
}
