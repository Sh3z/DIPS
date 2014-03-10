using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.ViewModel.UserInterfaceVM.JobTracking
{
    /// <summary>
    /// Represents an <see cref="IJobResultsHandler"/> loaded through
    /// reflection.
    /// </summary>
    [DebuggerDisplay( "{DisplayName}" )]
    public class LoadedHandler : ICloneable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IJobResultsHandler"/>
        /// class.
        /// </summary>
        /// <param name="handler">The <see cref="IJobResultsHandler"/> loaded
        /// through reflection.</param>
        /// <exception cref="ArgumentNullException">handler is null.</exception>
        public LoadedHandler( IJobResultsHandler handler )
        {
            if( handler == null )
            {
                throw new ArgumentNullException( "handler" );
            }

            Handler = handler;
        }


        /// <summary>
        /// Gets the underlying <see cref="IJobResultsHandler"/>.
        /// </summary>
        public IJobResultsHandler Handler
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the display name for the handler.
        /// </summary>
        public string DisplayName
        {
            get;
            set;
        }


        /// <summary>
        /// Returns a copy of this <see cref="LoadedHandler"/>.
        /// </summary>
        /// <returns>A <see cref="LoadedHandler"/> that is identical
        /// to this <see cref="LoadedHandler"/>.</returns>
        public object Clone()
        {
            return new LoadedHandler( (IJobResultsHandler)Handler.Clone() )
            {
                DisplayName = this.DisplayName
            };
        }
    }
}
