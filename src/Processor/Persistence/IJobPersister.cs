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
        /// <param name="jobID">The unique identifier of the job to save
        /// the image against.</param>
        /// <param name="output">The <see cref="Image"/> generated from a
        /// complete job.</param>
        /// <param name="identifier">The identifier for the input provided
        /// by the client.</param>
        void Persist( Guid jobID, Image output, object identifier );

        /// <summary>
        /// Loads a particular object back from the storage this
        /// <see cref="IJobPersister"/> is maintaining a connection to.
        /// </summary>
        /// <param name="jobID">The unique identifier of the job to load the
        /// resilts for.</param>
        /// <param name="identifier">The identifier of the particular
        /// image to reload.</param>
        /// <returns>The <see cref="PersistedResult"/> of the image represented
        /// by the identifier, or null if no image with the given identifier
        /// exists.</returns>
        PersistedResult Load( Guid jobID, object identifier );

        /// <summary>
        /// Loads all persisted results  from the storage this
        /// <see cref="IJobPersister"/> is maintaining a connection to.
        /// </summary>
        /// <param name="jobID">The unique identifier of the job to load the
        /// results for.</param>
        /// <returns>A set of <see cref="PersistedResult"/>s from the job this
        /// <see cref="IJobPersister"/> has previously persisted.</returns>
        IEnumerable<PersistedResult> Load( Guid jobID );

        /// <summary>
        /// Deletes all the results from a particular job from the storage.
        /// </summary>
        /// <param name="jobID">The unique identifier of the job to delete
        /// the results for.</param>
        /// <returns><c>true</c> if the results from the job were deleted
        /// successfully; <c>false</c> otherwise.</returns>
        bool Delete( Guid jobID );

        /// <summary>
        /// Deletes a result from a particular job with the associated identifier
        /// from the storage.
        /// </summary>
        /// <param name="jobID">The unique identifier of the job to delete
        /// the results for.</param>
        /// <param name="identifier">The identifier given to the input to be
        /// deleted.</param>
        /// <returns><c>true</c> if the result from the job was deleted
        /// successfully; <c>false</c> otherwise.</returns>
        bool Delete( Guid jobID, object identifier );
    }
}
