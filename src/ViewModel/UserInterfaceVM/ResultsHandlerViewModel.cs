using DIPS.ViewModel.UserInterfaceVM.JobTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.ViewModel.UserInterfaceVM
{
    /// <summary>
    /// Represents the <see cref="ViewModel"/> for a single
    /// <see cref="IJobResultsHandler"/>.
    /// </summary>
    public class ResultsHandlerViewModel : ViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResultsHandlerViewModel"/>
        /// class.
        /// </summary>
        /// <param name="handler">The <see cref="LoadedHandler"/> to be presented</param>
        public ResultsHandlerViewModel( LoadedHandler handler )
        {
            if( handler == null )
            {
                throw new ArgumentNullException( "handler" );
            }

            Name = handler.DisplayName;
            Handler = handler.Handler;
        }


        /// <summary>
        /// Gets the name to present for the handler.
        /// </summary>
        public string Name
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the handler object being presented.
        /// </summary>
        public IJobResultsHandler Handler
        {
            get;
            private set;
        }
    }
}
