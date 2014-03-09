using DIPS.Processor.Client;
using DIPS.Util.Commanding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.ViewModel.Commands
{
    /// <summary>
    /// Represents the command used to cancel jobs.
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
            return  parameter is IJobTicket && (
                    ((IJobTicket)parameter).State == JobState.InQueue ||
                    ((IJobTicket)parameter).State == JobState.Running );
        }

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">Data used by the command. If the command
        /// does not require data to be passed, this object can be set to
        /// null.</param>
        public override void Execute( object parameter )
        {
            IJobTicket ticket = parameter as IJobTicket;
            ticket.Cancel();
        }
    }
}
