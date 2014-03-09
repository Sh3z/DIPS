using DIPS.Util.Commanding;
using DIPS.ViewModel.Commands;
using DIPS.ViewModel.UserInterfaceVM.JobTracking;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DIPS.ViewModel.UserInterfaceVM
{
    /// <summary>
    /// Represents the <see cref="PostProcessingOptions"/> used to specify
    /// multiple post-processing actions.
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
            ChosenHandlers.CollectionChanged += _chosenHandlersChanged;
            _removeHandler = new RelayCommand( _removeSelectedHandler, _canExecuteRemoveHandler );
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
        public ICommand RemoveHandler
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
        /// Creates the <see cref="IJobResultsHandler"/> represented by the
        /// settings within this <see cref="PostProcessingOptions"/>.
        /// </summary>
        /// <returns>A <see cref="IJobResultsHandler"/> represented by the properties
        /// within this <see cref="PostProcessingOptions"/></returns>
        public override IJobResultsHandler CreateHandler()
        {
            CompositeHandler h = new CompositeHandler();
            foreach( IJobResultsHandler handler in ChosenHandlers )
            {
                h.Add( handler );
            }

            return h;
        }


        /// <summary>
        /// Occurs when the selected handler is changed.
        /// </summary>
        protected override void OnSelectedHandlerChanged()
        {
            base.OnSelectedHandlerChanged();

            _removeHandler.ExecutableStateChanged();
        }


        /// <summary>
        /// Performs the RemoveHandler.CanExecute logic
        /// </summary>
        /// <param name="parameter">N/A</param>
        /// <returns>true if the currently selected handler is within the
        /// chosen handlers set</returns>
        private bool _canExecuteRemoveHandler( object parameter )
        {
            return  SelectedHandler != null &&
                    ChosenHandlers.Contains( SelectedHandler );
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
        /// Occurs when the contents of the ChosenHandlers collection is
        /// modified
        /// </summary>
        /// <param name="sender">N/A</param>
        /// <param name="e">N/A</param>
        private void _chosenHandlersChanged( object sender, NotifyCollectionChangedEventArgs e )
        {
            IsValid = ChosenHandlers.Any();
        }
    }
}
