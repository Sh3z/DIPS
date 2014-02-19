using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Util.Extensions
{
    /// <summary>
    /// Provides extension methods against the <see cref="Type"/> class.
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Determines whether this <see cref="Type"/> implements a specific
        /// interface.
        /// </summary>
        /// <typeparam name="T">The type of the interface to determine is
        /// implemented.</typeparam>
        /// <param name="type">The <see cref="Type"/> to scrutinize.</param>
        /// <returns><c>true</c> if this <see cref="Type"/> implements the interface
        /// represented by the generic parameter; <c>false</c> otherwise.</returns>
        public static bool Implements<T>( this Type type )
        {
            if( type == null || typeof( T ).IsInterface == false )
            {
                return false;
            }
            else
            {
                return type.GetInterfaces().Contains( typeof( T ) );
            }
        }
    }
}
