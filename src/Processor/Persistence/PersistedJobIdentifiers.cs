using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor.Persistence
{
    /// <summary>
    /// Maintains a collection of identifiers used to record a persisted
    /// job and its ID.
    /// </summary>
    public class PersistedJobIdentifiers
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PersistedJobIdentifiers"/>
        /// class.
        /// </summary>
        /// <param name="jobID">The unique identifier of the job this
        /// <see cref="PersistedJobIdentifiers"/> maintains records of.</param>
        public PersistedJobIdentifiers( Guid jobID )
        {
            _ids = new Dictionary<Guid, object>();
            _currentInternal = 0;
            JobID = jobID;
        }


        /// <summary>
        /// Gets the unique identifier of the job associated with this
        /// <see cref="PersistedJobIdentifiers"/>.
        /// </summary>
        public Guid JobID
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the set of generated IDs.
        /// </summary>
        public IEnumerable<Guid> IDs
        {
            get
            {
                return _ids.Keys;
            }
        }


        /// <summary>
        /// Creates a persisted identifier using an internal unique identifier.
        /// </summary>
        /// <returns>A unique identifier for use in persistance.</returns>
        public Guid CreateIdentifier()
        {
            Guid g = Guid.NewGuid();
            _ids.Add( g, _currentInternal );
            _currentInternal++;
            return g;
        }

        /// <summary>
        /// Creates a persisted identifier for the given raw identifier.
        /// </summary>
        /// <param name="identifier">The identifier provided by the
        /// client.</param>
        /// <returns>A unique identifier for the object.</returns>
        public Guid CreateIdentifier( object identifier )
        {
            Guid g = Guid.NewGuid();
            _ids.Add( g, identifier );
            return g;
        }

        /// <summary>
        /// Gets the persisted identifier for the provided object.
        /// </summary>
        /// <param name="identifier">The raw identifier from the
        /// client.</param>
        /// <returns>The <see cref="Guid"/> assigned to the specified
        /// identifier, or null if no raw id exists.</returns>
        public Guid? GetIdentifier( object identifier )
        {
            Guid? g = null;
            Func<KeyValuePair<Guid, object>, bool> isPresent = x => x.Value == identifier;
            if( _ids.Any( isPresent ) )
            {
                var match = _ids.First( isPresent );
                g = match.Key;
            }

            return g;
        }

        /// <summary>
        /// Resolves the original identifier for a persisted ID.
        /// </summary>
        /// <param name="id">The persisted ID.</param>
        /// <returns>The original identifier, or null if it does not
        /// exist.</returns>
        public object GetOriginalIdentifier( Guid id )
        {
            object theID = null;
            _ids.TryGetValue( id, out theID );
            return theID;
        }

        /// <summary>
        /// Removes an identifier from this collection.
        /// </summary>
        /// <param name="id">The raw identifier to be removed.</param>
        public void RemoveOriginalIdentifier( object id )
        {
            Func<KeyValuePair<Guid, object>, bool> isPresent = x => x.Value == id;
            if( _ids.Any( isPresent ) )
            {
                var match = _ids.First( isPresent );
                _ids.Remove( match );
            }
        }

        /// <summary>
        /// Removes an identifier from this collection.
        /// </summary>
        /// <param name="id">The persisted identifier to be removed.</param>
        public void RemovePersistedIdentifier( Guid id )
        {
            if( _ids.Keys.Any( x => x == id ) )
            {
                _ids.Remove( id );
            }
        }


        /// <summary>
        /// Maintains the set of added IDs.
        /// </summary>
        private IDictionary<Guid, object> _ids;

        /// <summary>
        /// Contains an identifier for internal ids
        /// </summary>
        private int _currentInternal;
    }
}
