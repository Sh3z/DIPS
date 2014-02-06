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

        /// <summary>
        /// Loads a particular object back from the storage this
        /// <see cref="IJobPersister"/> is maintaining a connection to.
        /// </summary>
        /// <param name="identifier">The identifier of the particular
        /// image to reload.</param>
        /// <returns>The <see cref="PersistedResult"/> of the image represented
        /// by the identifier, or null if no image with the given identifier
        /// exists.</returns>
        PersistedResult Load( object identifier );

        /// <summary>
        /// Loads all persisted results  from the storage this
        /// <see cref="IJobPersister"/> is maintaining a connection to.
        /// </summary>
        /// <returns>A set of <see cref="PersistedResult"/>s from the job this
        /// <see cref="IJobPersister"/> has previously persisted.</returns>
        IEnumerable<PersistedResult> Load();
    }
}
