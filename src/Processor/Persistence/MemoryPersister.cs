using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor.Persistence
{
    /// <summary>
    /// Represents the job persister that retains the results of a job
    /// in memory.
    /// </summary>
    public class MemoryPersister : IJobPersister
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
        public void Persist( Guid jobID, Image output, object identifier )
        {
            PersistedResult r = null;
            if( identifier is string )
            {
                r = new PersistedResult( output, (string)identifier );
            }
            else
            {
                r = new PersistedResult( output, _sequence );
            }

            _sequence++;
            _results.Add( r );
        }

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
        public PersistedResult Load( Guid jobID, object identifier )
        {
            if( _results.Any() )
            {
                return _results.First( x => identifier.Equals( x.RestoredIdentifier ) );
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Loads all persisted results  from the storage this
        /// <see cref="IJobPersister"/> is maintaining a connection to.
        /// </summary>
        /// <param name="jobID">The unique identifier of the job to load the
        /// resilts for.</param>
        /// <returns>A set of <see cref="PersistedResult"/>s from the job this
        /// <see cref="IJobPersister"/> has previously persisted.</returns>
        public IEnumerable<PersistedResult> Load( Guid jobID )
        {
            return _results;
        }


        /// <summary>
        /// Contains the set of results from the job.
        /// </summary>
        private ICollection<PersistedResult> _results;

        /// <summary>
        /// Contains the numeric sequence identifier.
        /// </summary>
        private int _sequence;
    }
}
