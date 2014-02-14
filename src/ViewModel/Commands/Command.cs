using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DIPS.ViewModel.Commands
{
    /// <summary>
    /// Defines a command. This class is abstract.
    /// </summary>
    public abstract class Command : ICommand
    {
        /// <summary>
        /// Occurs when changes occur that affect whether or not the
        /// command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged;


        /// <summary>
        /// Defines the method that determines whether the command can
        /// execute in its current state.
        /// </summary>
        /// <param name="parameter">Data used by the command. If the
        /// command does not require data to be passed, this object can be
        /// set to null.</param>
        /// <returns>true if this command can be executed; otherwise,
        /// false.</returns>
        public abstract bool CanExecute( object parameter );

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">Data used by the command. If the command
        /// does not require data to be passed, this object can be set to
        /// null.</param>
        public abstract void Execute( object parameter );


        /// <summary>
        /// Notifies the base class the state of the current commands
        /// CanExecute value has changed.
        /// </summary>
        protected void OnCanExecuteChanged()
        {
            if( CanExecuteChanged != null )
            {
                CanExecuteChanged( this, EventArgs.Empty );
            }
        }
    }
}
