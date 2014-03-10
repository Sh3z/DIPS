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
    /// Represents the <see cref="ViewModel"/> used to present internal
    /// information about the chosen kind of post-processing. This class
    /// is abstract.
    /// </summary>
    public abstract class PostProcessingOptions : ViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PostProcessingOptions"/>
        /// abstract class.
        /// </summary>
        protected PostProcessingOptions()
        {
            AvailableHandlers = new ObservableCollection<ResultsHandlerViewModel>();
        }


        /// <summary>
        /// Gets or sets the <see cref="IHandlerFactory"/> used by this
        /// <see cref="PostProcessingOptions"/>.
        /// </summary>
        public IHandlerFactory Factory
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the collection of available <see cref="LoadedHandler"/>
        /// from the factory.
        /// </summary>
        public ObservableCollection<ResultsHandlerViewModel> AvailableHandlers
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the currently selected <see cref="IJobResultsHandler"/>.
        /// </summary>
        public ResultsHandlerViewModel SelectedHandler
        {
            get
            {
                return _selectedHandler;
            }
            set
            {
                _selectedHandler = value;
                OnPropertyChanged();
                OnSelectedHandlerChanged();
            }
        }
        [DebuggerBrowsable( DebuggerBrowsableState.Never )]
        private ResultsHandlerViewModel _selectedHandler;

        /// <summary>
        /// Gets a value indicating whether the post-processor represented
        /// by this <see cref="PostProcessingOptions"/> is in a valid
        /// state.
        /// </summary>
        public bool IsValid
        {
            get
            {
                return _isValid;
            }
            protected set
            {
                _isValid = value;
                OnPropertyChanged();
            }
        }
        [DebuggerBrowsable( DebuggerBrowsableState.Never )]
        private bool _isValid;

        /// <summary>
        /// In a dervied class, gets an identifier for this kind of
        /// <see cref="PostProcessingOptions"/>.
        /// </summary>
        public abstract string Identifier
        {
            get;
        }

        /// <summary>
        /// In a dervied class, creates the <see cref="IJobResultsHandler"/>
        /// represented by the settings within this <see cref="PostProcessingOptions"/>.
        /// </summary>
        /// <returns>A <see cref="IJobResultsHandler"/> represented by the properties
        /// within this <see cref="PostProcessingOptions"/></returns>
        public abstract IJobResultsHandler CreateHandler();


        /// <summary>
        /// Occurs when the selected handler is changed.
        /// </summary>
        protected virtual void OnSelectedHandlerChanged()
        {
        }
    }
}
