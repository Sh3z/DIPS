using MLApp;
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
        /// <param name="app">The running <see cref="IMLApp"/> to put the value
        /// of this <see cref="IParameterValue"/> into.</param>
        /// <param name="name">The name to give to the value of this
        /// <see cref="IParameterValue"/> within Matlab.</param>
        /// <param name="workspace">The workspace identifier to set the value
        /// in.</param>
        void Put( IMLApp app, string name, string workspace );
    }
}
