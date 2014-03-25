using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DIPS.Unity
{
    /// <summary>
    /// Represents the object maintaining a reference to the 
    /// <see cref="SynchronizationContext"/> used to dispatch calls
    /// to the UI thread.
    /// </summary>
    public interface IUIContext
    {
        /// <summary>
        /// Gets the <see cref="SynchronizationContext"/> of the UI
        /// thread within the application.
        /// </summary>
        SynchronizationContext Context
        {
            get;
        }
    }
}
