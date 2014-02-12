using DIPS.Matlab;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor.Plugin.Matlab
{
    /// <summary>
    /// Represents an abstract definition of a value-type sent to Matlab.
    /// </summary>
    public interface IParameterValue : IDisposable
    {
        /// <summary>
        /// Puts the value of this <see cref="IParameterValue"/> into the
        /// Matlab instance provided.
        /// </summary>
        /// <param name="name">The name to give to the value placed
        /// in the workspace.</param>
        /// <param name="workspace">The <see cref="Workspace"/> the
        /// value of this <see cref="IParameterValue"/> should be set.</param>
        void Put( string name, Workspace workspace );
    }
}
