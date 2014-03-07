using DIPS.ViewModel.UserInterfaceVM.JobTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.ViewModel.Unity
{
    /// <summary>
    /// Represents the object used to specify how to process results
    /// upon completion.
    /// </summary>
    public interface IPostProcessingResolver
    {
        /// <summary>
        /// Sets the <see cref="IHandlerFactory"/> containing the available
        /// handler information.
        /// </summary>
        IHandlerFactory Handlers
        {
            set;
        }

        /// <summary>
        /// Gets the chosen <see cref="IJobResultsHandler"/>.
        /// </summary>
        IJobResultsHandler ChosenHandler
        {
            get;
        }

        /// <summary>
        /// Resolves how to process jobs. This blocks the caller.
        /// </summary>
        /// <returns><c>true</c> if post-processing settings were
        /// specified.</returns>
        bool Resolve();
    }
}
