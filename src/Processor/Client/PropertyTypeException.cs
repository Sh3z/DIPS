using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor.Client
{
    /// <summary>
    /// Represents an error pertaining to the type of property receieved by an algorithm.
    /// </summary>
    public class PropertyTypeException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyTypeException"/> class.
        /// </summary>
        /// <param name="property">The <see cref="Property"/> this exception pertains to.</param>
        /// <param name="received">The received <see cref="Type"/> that is incompatible with that
        /// found within the property.</param>
        public PropertyTypeException( Property property, Type received )
            : base( string.Format( "Invalid type for property \"{0}\" (expected {1}, received {2})",
                        property.Name, property.Type, received ) )
        {
        }
    }
}
