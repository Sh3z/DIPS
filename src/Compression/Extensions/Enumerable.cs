using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Util.Extensions
{
    /// <summary>
    /// Provides extension methods against IEnumerable objects.
    /// </summary>
    public static class Enumerable
    {
        /// <summary>
        /// Performs the provided action against every object returned by the
        /// enumerator.
        /// </summary>
        /// <typeparam name="T">The type argument representing the type of objects
        /// retuned by the enumerator.</typeparam>
        /// <param name="enumerable">The <see cref="IEnumerable"/> providing the set
        /// of items.</param>
        /// <param name="action">The action to perform against each item returned
        /// by the enumerable.</param>
        /// <exception cref="ArgumentNullException">enumerable or action are
        /// null.</exception>
        public static void ForEach<T>( this IEnumerable<T> enumerable, Action<T> action )
        {
            if( enumerable == null )
            {
                throw new ArgumentNullException( "enumerable" );
            }

            if( action == null )
            {
                throw new ArgumentNullException( "action" );
            }

            foreach( T item in enumerable )
            {
                action( item );
            }
        }
    }
}
