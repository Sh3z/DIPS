using DIPS.Processor.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor.Plugin
{
    /// <summary>
    /// Represents an object that accepts a set of <see cref="Property"/>
    /// objects and interprets them into a new purpose.
    /// </summary>
    public interface IPropertyInterpreter
    {
        /// <summary>
        /// Interprets the incoming set of <see cref="Property"/> objects.
        /// </summary>
        /// <param name="properties">The <see cref="PropertySet"/> containing
        /// an aggregation of property values.</param>
        void Interpret( PropertySet properties );
    }
}
