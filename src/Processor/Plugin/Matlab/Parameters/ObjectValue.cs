using DIPS.Matlab;
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
        /// <param name="name">The name to give to the value placed
        /// in the workspace.</param>
        /// <param name="workspace">The <see cref="Workspace"/> the
        /// value of this <see cref="IParameterValue"/> should be set.</param>
        public void Put( string name, Workspace workspace )
        {
            workspace.PutObject( name, Value );
        }

        void IDisposable.Dispose()
        {
        }
    }
}
