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
        /// Initializes a new instance of the <see cref="MemoryPersister"/>
        /// class.
        /// </summary>
        public MemoryPersister()
        {
            _resultsMap = new Dictionary<Guid, ICollection<PersistedResult>>();
        }


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
            _storeResult( jobID, r );
        }

        /// <summary>
        /// Loads a particular object back from the storage this
        /// <see cref="IJobPersister"/> is maintaining a connection to.
        /// </summary>
        /// <param name="jobID">The unique identifier of the job to load the
        /// results for.</param>
        /// <param name="identifier">The identifier of the particular
        /// image to reload.</param>
        /// <returns>The <see cref="PersistedResult"/> of the image represented
        /// by the identifier, or null if no image with the given identifier
        /// exists.</returns>
        public PersistedResult Load( Guid jobID, object identifier )
        {
            var jobResults = Load( jobID );
            if( _resultsMap.Any() )
            {
                return jobResults.FirstOrDefault( x => identifier.Equals( x.Identifier ) );
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
            if( _resultsMap.ContainsKey( jobID ) )
            {
                return _resultsMap[jobID];
            }
            else
            {
                return new List<PersistedResult>();
            }
        }


        /// <summary>
        /// Saves the result to the provided job.
        /// </summary>
        /// <param name="job">The job associated with the result.</param>
        /// <param name="result">The result to save</param>
        private void _storeResult( Guid job, PersistedResult result )
        {
            ICollection<PersistedResult> results = null;
            _resultsMap.TryGetValue( job, out results );
            if( results != null )
            {
                results.Add( result );
            }
            else
            {
                results = new List<PersistedResult>();
                results.Add( result );
                _resultsMap.Add( job, results );
            }
        }


        /// <summary>
        /// Contains the job id to results mapping.
        /// </summary>
        private IDictionary<Guid, ICollection<PersistedResult>> _resultsMap;

        /// <summary>
        /// Contains the numeric sequence identifier.
        /// </summary>
        private int _sequence;
    }
}
