using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.ViewModel.UserInterfaceVM.JobTracking
{
    /// <summary>
    /// Represents the factory used to construct results handlers.
    /// </summary>
    public interface IHandlerFactory : IEnumerable<string>
    {
        /// <summary>
        /// Loads the result handler implementations from the provided
        /// assembly.
        /// </summary>
        /// <param name="assembly">The <see cref="Assembly"/> to load
        /// handler instances from.</param>
        void Load( Assembly assembly );

        /// <summary>
        /// Creates the appropriate results handler for the given
        /// identifier.
        /// </summary>
        /// <param name="identifier">The identifier of the handler.</param>
        /// <returns>The <see cref="IJobResultsHandler"/> associated with
        /// the given identifier.</returns>
        IJobResultsHandler CreateHandler( string identifier );

        /// <summary>
        /// Creates a composite results handler from the given set of
        /// identifiers.
        /// </summary>
        /// <param name="identifiers">The set of identifiers for each handler
        /// to create.</param>
        /// <returns>The <see cref="IJobResultsHandler"/> representing the
        /// composite handler.</returns>
        IJobResultsHandler CreateHandler( params string[] identifiers );
    }
}
