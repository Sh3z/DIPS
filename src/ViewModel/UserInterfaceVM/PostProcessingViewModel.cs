using DIPS.Util.Commanding;
using DIPS.Util.Extensions;
using DIPS.ViewModel.Commands;
using DIPS.ViewModel.UserInterfaceVM.JobTracking;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace DIPS.ViewModel.UserInterfaceVM
{
    /// <summary>
    /// Represents the <see cref="ViewModel"/> used for the post-processing
    /// UI.
    /// </summary>
    public class PostProcessingViewModel : BaseViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PostProcessingViewModel"/>
        /// class.
        /// </summary>
        /// <param name="factory">The <see cref="IHandlerFactory"/> providing
        /// information on the currently loaded handlers.</param>
        /// <param name="store">The <see cref="PostProcessingStore"/> containing
        /// details on the available kinds of options.</param>
        /// <exception cref="ArgumentNullException">factory or store are null</exception>
        public PostProcessingViewModel( IHandlerFactory factory, PostProcessingStore store )
        {
            if( factory == null )
            {
                throw new ArgumentNullException( "factory" );
            }

            if( store == null )
            {
                throw new ArgumentNullException( "store" );
            }

            _factory = factory;
            _store = store;
            _factory.HandlerRegistered += _handlerRegistered;
            AvailableOptions = new ObservableCollection<PostProcessingOptions>();
            _continueCommand = new RelayCommand( _continue, _canContinue );

            foreach( string identifier in _store.AvailableOptions )
            {
                PostProcessingOptions options = _store[identifier];
                options.Factory = _factory;
                AvailableOptions.Add( options );
            }

            if( AvailableOptions.Any() )
            {
                CurrentOptions = AvailableOptions.OrderBy( x => x.Identifier ).First();
            }

            _updateAvailableHandlers();

            GoBackCommand = new RelayCommand(new Action<object>(_goBack));
        }


        /// <summary>
        /// Gets or sets the overall containing Frame.
        /// </summary>
        public Frame OverallFrame
        {
            get;
            set;
        }

        public ICommand GoBackCommand { get; set; }

        /// <summary>
        /// Gets the collection of available post-processing option types.
        /// </summary>
        public ObservableCollection<PostProcessingOptions> AvailableOptions
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the currently chosen <see cref="PostProcessingOptions"/>.
        /// </summary>
        public PostProcessingOptions CurrentOptions
        {
            get
            {
                return _currentOptions;
            }
            set
            {
                if( _currentOptions != null )
                {
                    _currentOptions.PropertyChanged -= _optionsPropertyChanged;
                }

                _currentOptions = value;

                if( _currentOptions != null )
                {
                    _currentOptions.PropertyChanged += _optionsPropertyChanged;
                }

                _continueCommand.ExecutableStateChanged();
                OnPropertyChanged();
            }
        }
        [DebuggerBrowsable( DebuggerBrowsableState.Never )]
        private PostProcessingOptions _currentOptions;

        /// <summary>
        /// Gets the command used to load custom handlers.
        /// </summary>
        public ICommand LoadCustomHandlerCommand
        {
            get
            {
                return _loadHandlersCommand;
            }
        }
        [DebuggerBrowsable( DebuggerBrowsableState.Never )]
        private UnityCommand _loadHandlersCommand;

        /// <summary>
        /// Gets the <see cref="ICommand"/> used to continue to the next
        /// step.
        /// </summary>
        public ICommand Continue
        {
            get
            {
                return _continueCommand;
            }
        }
        [DebuggerBrowsable( DebuggerBrowsableState.Never )]
        private RelayCommand _continueCommand;


        /// <summary>
        /// Creates and returns the post-processing object represented
        /// by the current view-model.
        /// </summary>
        /// <returns>The <see cref="IJobResultsHandler"/> designed by the
        /// user to use in post-processing.</returns>
        public IJobResultsHandler CreatePostProcessor()
        {
            if( CurrentOptions == null )
            {
                return null;
            }
            else
            {
                return CurrentOptions.CreateHandler();
            }
        }


        /// <summary>
        /// Invoked when a new handler is registered within the factory
        /// </summary>
        /// <param name="sender">The factory</param>
        /// <param name="e">Event information</param>
        private void _handlerRegistered( object sender, HandlerRegisteredArgs e )
        {
            ResultsHandlerViewModel vm = new ResultsHandlerViewModel( e.Handler );
            _registerHandlerVM( vm );
        }

        /// <summary>
        /// Updates the collection of available handlers
        /// </summary>
        private void _updateAvailableHandlers()
        {
            AvailableOptions.ForEach( x => x.AvailableHandlers.Clear() );
            foreach( LoadedHandler handler in _factory.LoadedHandlers )
            {
                ResultsHandlerViewModel vm = new ResultsHandlerViewModel( handler );
                _registerHandlerVM( vm );
            }
        }

        /// <summary>
        /// Registers a new handler within the individual options
        /// </summary>
        /// <param name="vm">The view-model to register</param>
        private void _registerHandlerVM( ResultsHandlerViewModel vm )
        {
            foreach( PostProcessingOptions options in AvailableOptions )
            {
                options.AvailableHandlers.Add( vm );
            }
        }

        /// <summary>
        /// Provides the Continue.CanExecute logic
        /// </summary>
        /// <param name="parameter">N/A</param>
        /// <returns>true if the user has provided sufficient information
        /// to continue</returns>
        private bool _canContinue( object parameter )
        {
            return CurrentOptions != null && CurrentOptions.IsValid;
        }

        /// <summary>
        /// Provides the Continue.Execute logic
        /// </summary>
        /// <param name="parameter">N/A</param>
        private void _continue( object parameter )
        {
            this.OverallFrame.Content = BaseViewModel._LoadNewDsStep3ViewModel;
            BaseViewModel._LoadNewDsStep3ViewModel.Handler = CreatePostProcessor();
        }

        /// <summary>
        /// Occurs when a property within the current options object has
        /// been modified
        /// </summary>
        /// <param name="sender">N/A</param>
        /// <param name="e">Event information</param>
        private void _optionsPropertyChanged( object sender, PropertyChangedEventArgs e )
        {
            string lowerName = e.PropertyName.ToLower();
            if( lowerName == "isvalid" )
            {
                _continueCommand.ExecutableStateChanged();
            }
        }


        /// <summary>
        /// Retains the handler factory.
        /// </summary>
        private IHandlerFactory _factory;

        /// <summary>
        /// Retains the options store.
        /// </summary>
        private PostProcessingStore _store;

        private void _goBack(object obj)
        {
            OverallFrame.Content = _LoadNewDsStep2ViewModel;
        }
    }
}
