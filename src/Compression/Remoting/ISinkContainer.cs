using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Util.Remoting
{
    /// <summary>
    /// Represents an abstract container for event sinks.
    /// </summary>
    /// <typeparam name="T">The derived <see cref="EventSink"/> type
    /// this <see cref="ISinkContainer"/> contains.</typeparam>
    public interface ISinkContainer<T> : IDisposable
        where T : EventSink
    {
        /// <summary>
        /// Adds a new sink to this <see cref="ISinkContainer"/>.
        /// </summary>
        /// <param name="sink">The <see cref="EventSink"/> to add.</param>
        void Add( T sink );

        /// <summary>
        /// Removes an existing <see cref="EventSink"/> from this
        /// <see cref="ISinkContainer"/>.
        /// </summary>
        /// <param name="sink">The <see cref="EventSink"/> to remove.</param>
        void Remove( T sink );
    }
}
