using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DIPS.Matlab
{
    /// <summary>
    /// Represents the base type of commands used against a <see cref="MatlabEngine"/>.
    /// This class is abstract.
    /// </summary>
    public abstract class MatlabCommand : ICommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MatlabCommand"/>
        /// abstract class.
        /// </summary>
        /// <param name="session">The <see cref="MatlabSession"/> this
        /// <see cref="MatlabCommand"/> is associated with.</param>
        /// <exception cref="ArgumentNullException">session is null.</exception>
        protected MatlabCommand( MatlabSession session )
        {
            if( session == null )
            {
                throw new ArgumentNullException( "session" );
            }

            Session = session;
            Session.SessionValidityChanged += _onSessionValidityChanged;
        }


        /// <summary>
        /// Contains the <see cref="MatlabSession"/> this <see cref="MatlabCommand"/>
        /// belongs to.
        /// </summary>
        protected MatlabSession Session
        {
            get;
            private set;
        }

        /// <summary>
        /// Occurs when the state determining whether this <see cref="MatlabCommand"/>
        /// can execute has changed.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Gets a value used to determine whether this <see cref="MatlabCommand"/>
        /// can execute in its current state.
        /// </summary>
        /// <param name="parameter">Not used.</param>
        /// <returns><c>true</c> if this <see cref="MatlabCommand"/> can
        /// execute as-is; <c>false</c> otherwise.</returns>
        public virtual bool CanExecute( object parameter )
        {
            return Session.Valid;
        }

        /// <summary>
        /// Executes the logic of this <see cref="MatlabCommand"/>.
        /// </summary>
        /// <param name="parameter">Not used.</param>
        void ICommand.Execute( object parameter )
        {
            Execute();
        }

        /// <summary>
        /// Executes the logic of this <see cref="MatlabCommand"/>.
        /// </summary>
        public abstract void Execute();


        /// <summary>
        /// Occurs when the validity of the current session has changed.
        /// </summary>
        /// <param name="sender">N/A</param>
        /// <param name="e">N/A</param>
        private void _onSessionValidityChanged( object sender, EventArgs e )
        {
            if( CanExecuteChanged != null )
            {
                CanExecuteChanged( this, EventArgs.Empty );
            }
        }
    }
}
