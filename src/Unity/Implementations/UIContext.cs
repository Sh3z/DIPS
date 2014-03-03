using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DIPS.Unity.Implementations
{
    /// <summary>
    /// Represents the object maintaining a reference to the 
    /// <see cref="SynchronizationContext"/> used to dispatch calls
    /// to the UI thread.
    /// </summary>
    public class UIContext : IUIContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UIContext"/>
        /// class.
        /// </summary>
        /// <exception cref="InvalidOperationException">instance created
        /// on a thread that is not the UI thread.</exception>
        public UIContext()
        {
            Context = SynchronizationContext.Current;
        }


        /// <summary>
        /// Gets the <see cref="SynchronizationContext"/> of the UI
        /// thread within the application.
        /// </summary>
        public SynchronizationContext Context
        {
            get;
            private set;
        }
    }
}
