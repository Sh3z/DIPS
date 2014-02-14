using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DIPS.ViewModel.Commands
{
    /// <summary>
    /// Represents an <see cref="ICommand"/> whereby the implementation
    /// resides inside the view-model. This class cannot be inherited.
    /// </summary>
    public sealed class RelayCommand : ICommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RelayCommand"/> class.
        /// </summary>
        /// <param name="execute">The function representing the execution
        /// logic.</param>
        /// <exception cref="ArgumentNullException">execute is null</exception>
        public RelayCommand( Action<object> execute )
        {
            if( execute == null )
            {
                throw new ArgumentNullException( "execute" );
            }

            _execute = execute;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RelayCommand"/> class.
        /// </summary>
        /// <param name="execute">The function representing the execution
        /// logic.</param>
        /// <param name="canExecute">The predicate representing the can execute
        /// logic.</param>
        /// <exception cref="ArgumentNullException">execute is null</exception>
        public RelayCommand( Action<object> execute, Predicate<object> canExecute )
            : this( execute )
        {
            _canExecute = canExecute;
        }


        /// <summary>
        /// Occurs when the value of CanExecute within this <see cref="RelayCommand"/>
        /// has changed.
        /// </summary>
        public event EventHandler CanExecuteChanged;


        /// <summary>
        /// Determines whether this <see cref="RelayCommand"/> can execute.
        /// </summary>
        /// <param name="parameter">The <see cref="object"/> to provide to
        /// the commanding logic.</param>
        /// <returns><c>true</c> if this <see cref="RelayCommand"/> can be
        /// executed.</returns>
        public bool CanExecute( object parameter )
        {
            if( _canExecute == null )
            {
                return true;
            }
            else
            {
                return _canExecute( parameter );
            }
        }


        /// <summary>
        /// Executes the logic of this <see cref="RelayCommand"/>.
        /// </summary>
        /// <param name="parameter">The <see cref="object"/> to provide to
        /// the commanding logic.</param>
        public void Execute( object parameter )
        {
            _execute( parameter );
        }


        /// <summary>
        /// Contains the execution logic
        /// </summary>
        private Action<object> _execute;

        /// <summary>
        /// Contains the optional CanExecute logi
        /// </summary>
        private Predicate<object> _canExecute;
    }
}
