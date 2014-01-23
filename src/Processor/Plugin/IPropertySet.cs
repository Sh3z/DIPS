using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor.Plugin
{
    /// <summary>
    /// Represents an aggregation of <see cref="Property"/> values parsed from
    /// job XML, for use by plugin components in their execution.
    /// </summary>
    public interface IPropertySet : ISet<Property>
    {
        /// <summary>
        /// Gets or sets the value of the <see cref="Property"/> within this
        /// <see cref="PropertySet"/> with the specified name.
        /// </summary>
        /// <param name="name">The name of the <see cref="Property"/> to retrieve
        /// or set the value of. Case sensitive.</param>
        /// <returns>The value of the <see cref="Property"/> within this
        /// <see cref="PropertySet"/> with the given name.</returns>
        object this[string name]
        {
            get;
            set;
        }
    }
}
