using MLApp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor.Plugin.Matlab.Parameters
{
    /// <summary>
    /// Represents a general-purpose Object value.
    /// </summary>
    public class ObjectValue : IParameterValue
    {
        /// <summary>
        /// Gets or sets the underlying value.
        /// </summary>
        [Description( "Contains the generic value to provide to Matlab." )]
        public object Value
        {
            get;
            set;
        }

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
        public void Put( IMLApp app, string name, string workspace )
        {
            app.PutWorkspaceData( name, workspace, Value );
        }

        void IDisposable.Dispose()
        {
        }
    }
}
