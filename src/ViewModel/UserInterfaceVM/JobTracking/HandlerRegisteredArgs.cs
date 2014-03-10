using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.ViewModel.UserInterfaceVM.JobTracking
{
    /// <summary>
    /// Provides event information as handlers are loaded into a
    /// <see cref="IHandlerFactory"/>.
    /// </summary>
    public class HandlerRegisteredArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HandlerRegisteredArgs"/>
        /// class.
        /// </summary>
        /// <param name="handler">The <see cref="LoadedHandler"/> loaded from
        /// the factory.</param>
        /// <exception cref="ArgumentNullException">handler is null.</exception>
        public HandlerRegisteredArgs( LoadedHandler handler )
        {
            if( handler == null )
            {
                throw new ArgumentNullException( "handler" );
            }

            Handler = handler;
        }


        /// <summary>
        /// Gets the <see cref="LoadedHandler"/> from the
        /// <see cref="IHandlerFactory"/> that has been loaded.
        /// </summary>
        public LoadedHandler Handler
        {
            get;
            private set;
        }
    }
}
