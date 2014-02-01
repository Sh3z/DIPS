using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Util
{
    /// <summary>
    /// Provides case-insensitive comparison against strings.
    /// </summary>
    public class CaseInsensitiveComparator : IEqualityComparer<string>
    {
        /// <summary>
        /// Determines whether two strings are equal, case insensitive.
        /// </summary>
        /// <param name="x">The string to compare.</param>
        /// <param name="y">The string to compare against.</param>
        /// <returns>true if both strings contain the same value, ignoring
        /// case.</returns>
        public bool Equals( string x, string y )
        {
            return x.Equals( y, StringComparison.OrdinalIgnoreCase );
        }

        /// <summary>
        /// Resolves the hashcode for a particular string.
        /// </summary>
        /// <param name="obj">The string to resolve the hashcode for.</param>
        /// <returns>The hashcode of the string.</returns>
        public int GetHashCode( string obj )
        {
            return obj.GetHashCode();
        }
    }
}
