using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using DIPS.Unity;
using DIPS.ViewModel.UserInterfaceVM.JobTracking;
using DIPS.ViewModel.Unity;

namespace DIPS.ViewModel.Commands
{
    /// <summary>
    /// Represents the <see cref="ICommand"/> used to present the
    /// <see cref="IQueueDialog"/>.
    /// </summary>
    public class PresentQueueCommand : UnityCommand
    {
        /// <summary>
        /// Defines the method that determines whether the command can
        /// execute in its current state.
        /// </summary>
        /// <param name="parameter">Data used by the command. If the
        /// command does not require data to be passed, this object can be
        /// set to null.</param>
        /// <returns>true if this command can be executed; otherwise,
        /// false.</returns>
        public override bool CanExecute( object parameter )
        {
            return  Container != null &&
                    Container.Contains<IJobTracker>() &&
                    Container.Contains<IQueueDialog>();
        }

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">Data used by the command. If the command
        /// does not require data to be passed, this object can be set to
        /// null.</param>
        public override void Execute( object parameter )
        {
            IJobTracker jobs = Container.Resolve<IJobTracker>();
            IQueueDialog dialog = Container.Resolve<IQueueDialog>();
            dialog.ShowDialog( jobs );
        }
    }
}
