using DIPS.Processor.Client;
using DIPS.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.ViewModel.UserInterfaceVM.JobTracking
{
    /// <summary>
    /// Represents an <see cref="ICommand"/> used to cancel jobs.
    /// </summary>
    public class CancelJobCommand : Command
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
            if( parameter is JobViewModel == false )
            {
                return false;
            }

            JobViewModel job = parameter as JobViewModel;
            return job.IsCancelled == false;
        }

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">Data used by the command. If the command
        /// does not require data to be passed, this object can be set to
        /// null.</param>
        public override void Execute( object parameter )
        {
            JobViewModel vm = parameter as JobViewModel;
            IJobTicket ticket = vm.Ticket;
            ticket.Cancel();
        }
    }
}
