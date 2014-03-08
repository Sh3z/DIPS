using DIPS.ViewModel.Commands;
using DIPS.ViewModel.UserInterfaceVM.JobTracking;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DIPS.ViewModel.UserInterfaceVM
{
    /// <summary>
    /// Represents the <see cref="PostProcessingOptions"/> used to specify
    /// multiply post-processing actions.
    /// </summary>
    public class MultiHandlerOptions : PostProcessingOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MultiHandlerOptions"/>
        /// class.
        /// </summary>
        public MultiHandlerOptions()
        {
            ChosenHandlers = new ObservableCollection<ResultsHandlerViewModel>();
        }


        /// <summary>
        /// Gets the collection of <see cref="LoadedHandler"/> the user
        /// has chosen.
        /// </summary>
        public ObservableCollection<ResultsHandlerViewModel> ChosenHandlers
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the command used to remove a handler from the
        /// ChosenHandlers collection.
        /// </summary>
        public ICommand RemoveHandlerCommand
        {
            get
            {
                return _removeHandler;
            }
        }
        [DebuggerBrowsable( DebuggerBrowsableState.Never )]
        private RelayCommand _removeHandler;

        /// <summary>
        /// Gets an identifier for this kind of
        /// <see cref="PostProcessingOptions"/>.
        /// </summary>
        public override string Identifier
        {
            get
            {
                return "Multiple";
            }
        }

        /// <summary>
        /// In a dervied class, gets an <see cref="ICommand"/> used
        /// to reset the state of this <see cref="PostProcessingOption"/>s.
        /// </summary>
        public override ICommand Reset
        {
            get
            {
                return _resetCommand;
            }
        }
        [DebuggerBrowsable( DebuggerBrowsableState.Never )]
        private RelayCommand _resetCommand;

        /// <summary>
        /// Creates the <see cref="IJobResultsHandler"/> represented by the
        /// settings within this <see cref="PostProcessingOptions"/>.
        /// </summary>
        /// <returns>A <see cref="IJobResultsHandler"/> represented by the properties
        /// within this <see cref="PostProcessingOptions"/></returns>
        public override IJobResultsHandler CreateHandler()
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Performs the RemoveHandler.CanExecute logic
        /// </summary>
        /// <param name="parameter">N/A</param>
        /// <returns>true if the currently selected handler is within the
        /// chosen handlers set</returns>
        private bool _canExecuteRemoveHandler( object parameter )
        {
            return SelectedHandler != null && ChosenHandlers.Contains( SelectedHandler );
        }

        /// <summary>
        /// Performs the RemoveHandler.Execute logic
        /// </summary>
        /// <param name="parameter">N/A</param>
        private void _removeSelectedHandler( object parameter )
        {
            ChosenHandlers.Remove( SelectedHandler );
        }

        /// <summary>
        /// Performs the ResetCommand.CanExecute logic
        /// </summary>
        /// <param name="parameter">N/A</param>
        /// <returns>True if the ChosenHandlers collection contains
        /// any elements.</returns>
        private bool _canResetHandlers( object parameter )
        {
            return ChosenHandlers.Any();
        }

        /// <summary>
        /// Performs the ResetCommand.Execute logic.
        /// </summary>
        /// <param name="parameter">N/A</param>
        private void _resetHandlers( object parameter )
        {
            ChosenHandlers.Clear();
        }
    }
}
