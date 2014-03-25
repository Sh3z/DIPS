using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor.Client
{
    /// <summary>
    /// Provides comparison logic to determine whether two
    /// <see cref="IProcessedImage"/> instances contain the same identifier.
    /// </summary>
    public class ProcessedImageIdentifierComparer : IEqualityComparer<IProcessedImage>
    {
        /// <summary>
        /// Determines whether the identifiers of two <see cref="IProcessedImage"/>
        /// objects are equal.
        /// </summary>
        /// <param name="x">The first <see cref="IProcessedImage"/> to
        /// compare.</param>
        /// <param name="y">The second <see cref="IProcessedImage"/> to
        /// compare.</param>
        /// <returns><c>true</c> if both <see cref="IProcessedImage"/>s contain
        /// the same identifier.</returns>
        public bool Equals( IProcessedImage x, IProcessedImage y )
        {
            return GetHashCode( x ) == GetHashCode( y );
        }

        /// <summary>
        /// Generates a hashcode for the provided <see cref="IProcessedImage"/>.
        /// </summary>
        /// <param name="obj">The <see cref="IProcessedImage"/> in which to generate
        /// a hashcode for.</param>
        /// <returns>The hashcode for the provided <see cref="IProcessedImage"/>.</returns>
        public int GetHashCode( IProcessedImage obj )
        {
            return obj.Identifier.GetHashCode();
        }
    }
}
