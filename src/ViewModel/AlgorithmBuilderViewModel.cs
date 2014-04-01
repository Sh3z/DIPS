using System.Windows.Input;
using Database.Repository;
using DIPS.Processor.Client;
using DIPS.ViewModel.Commands;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DIPS.Util.Commanding;
using System.Collections.Specialized;
using GongSolutions.Wpf.DragDrop;
using System.Windows;
using System.IO;
using DIPS.Database.Objects;
using DIPS.Util.Extensions;
using DIPS.Unity;

namespace DIPS.ViewModel
{
    /// <summary>
    /// Represents the object providing the presentation logic for the
    /// algorithm builder view.
    /// </summary>
    public class AlgorithmBuilderViewModel : BaseViewModel, IPipelineInfo, IDropTarget
    {
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="AlgorithmBuilderViewModel"/> class.
        /// </summary>
        public AlgorithmBuilderViewModel()
        {
            AvailableAlgorithms = new ObservableCollection<AlgorithmViewModel>();
            SelectedProcesses = new ObservableCollection<AlgorithmViewModel>();
            SavePipeline = new PersistPipelineCommand( this );
            LoadPipeline = new LoadPipelineCommand( this );
            SavePipelineDatabase = new PersistPipelineDatabaseCommand( this );
            MoveDirectToStep3 = new RelayCommand(new Action<object>(_progressToStep3));

            FinishButtonCommand = new RelayCommand( new Action<object>( ProgressToMainOrStep2 ) );
            _clearSelectedAlgorithms = new RelayCommand( new Action<object>( ClearSelectedCommand ), _canClearSelectedAlgorithms );

            SelectedProcesses.CollectionChanged += _chosenProcessesCollectionChanged;

            ListofTechniques = new ObservableCollection<Technique>();
            ImageProcessingRepository imgProRep = new ImageProcessingRepository();

            ListofTechniques = imgProRep.getAllTechnique();

            TechniqueAlgorithms = new ObservableCollection<AlgorithmViewModel>();

        }

        public Technique ChosenTechnique
        {
            get
            {
                return _chosenTechnique;
            }
            set
            {
                _chosenTechnique = value;
                OnPropertyChanged();
            }
        }
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private Technique _chosenTechnique;

        private ObservableCollection<Technique> _listOfTechniques;

        public ObservableCollection<Technique> ListofTechniques
        {
            get { return _listOfTechniques; }
            set
            {
                _listOfTechniques = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="IUnityContainer"/> used in the current
        /// application.
        /// </summary>
        public IUnityContainer Container
        {
            get
            {
                return _container;
            }
            set
            {
                _container = value;
                SavePipeline.Container = value;
                LoadPipeline.Container = value;
                SavePipelineDatabase.Container = value;
            }
        }
        [DebuggerBrowsable( DebuggerBrowsableState.Never )]
        private IUnityContainer _container;

        /// <summary>
        /// Gets the collection of <see cref="AlgorithmViewModel"/>s available
        /// for building a technique with.
        /// </summary>
        public ObservableCollection<AlgorithmViewModel> AvailableAlgorithms
        {
            get;
            set;
        }

        private ObservableCollection<FileInfo> _listOfFiles;

        public ObservableCollection<FileInfo> ListOfFiles
        {
            get { return _listOfFiles; }
            set
            {
                _listOfFiles = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<AlgorithmViewModel> _techniqueAlgorithms;

        public ObservableCollection<AlgorithmViewModel> TechniqueAlgorithms
        {
            get { return _techniqueAlgorithms; }
            set
            {
                _techniqueAlgorithms = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets the collection of <see cref="AlgorithmViewModel"/>s chosen
        /// for the current composite algorithm.
        /// </summary>
        public ObservableCollection<AlgorithmViewModel> SelectedProcesses
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the selected <see cref="AlgorithmViewModel"/>.
        /// </summary>
        public AlgorithmViewModel SelectedAlgorithm
        {
            get
            {
                return _selectedAlgorithm;
            }
            set
            {
                _selectedAlgorithm = value;
                OnPropertyChanged();
            }
        }
        [DebuggerBrowsable( DebuggerBrowsableState.Never )]
        private AlgorithmViewModel _selectedAlgorithm;

        /// <summary>
        /// Gets or sets the name given to the current pipeline.
        /// </summary>
        public string PipelineName
        {
            get
            {
                return _pipelineName;
            }
            set
            {
                _pipelineName = value;
                OnPropertyChanged();
            }
        }

        private string _pipelineID;

        public string PipelineID
        {
            get { return _pipelineID; }
            set
            {
                _pipelineID = value;
                OnPropertyChanged();
            }
        }

        [DebuggerBrowsable( DebuggerBrowsableState.Never )]
        private string _pipelineName;

        /// <summary>
        /// Gets the command used to persist the pipeline.
        /// </summary>
        public UnityCommand SavePipeline
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the command used to load the pipeline.
        /// </summary>
        public UnityCommand LoadPipeline
        {
            get;
            private set;
        }

        public ICommand MoveDirectToStep3 { get; set; }

        public UnityCommand SavePipelineDatabase
        {
            get;
            private set;
        }

        public Boolean FromLoadStep2 { get; set; }
        public Boolean FromViewAlgorithms { get; set; }
        public ICommand FinishButtonCommand { get; set; }

        public ICommand ClearSelectedAlgorithmsCommand
        {
            get
            {
                return _clearSelectedAlgorithms;
            }
        }
        [DebuggerBrowsable( DebuggerBrowsableState.Never )]
        private RelayCommand _clearSelectedAlgorithms;

        void IDropTarget.DragOver( IDropInfo dropInfo )
        {
            dropInfo.NotHandled = true;
        }

        void IDropTarget.Drop( IDropInfo dropInfo )
        {
            bool notHandled = true;
            if( dropInfo.Data is AlgorithmViewModel )
            {
                AlgorithmViewModel clone = (AlgorithmViewModel)( (AlgorithmViewModel)dropInfo.Data ).Clone();
                SelectedProcesses.Add( clone );
                notHandled = false;
            }

            dropInfo.NotHandled = notHandled;
        }

        private Visibility _goBackButtonState;

        public Visibility GoBackButtonState
        {
            get { return _goBackButtonState; }
            set { 
                  _goBackButtonState = value;
                  OnPropertyChanged();
                }
        }
        

        private void ProgressToMainOrStep2(object obj)
        {
            if( FromLoadStep2 )
            {
                OverallFrame.Content = _LoadNewDsStep2ViewModel;
                if( _LoadNewDsStep2ViewModel.ListofTechniques != null )
                {
                    _LoadNewDsStep2ViewModel.ListofTechniques.Clear();

                    ImageProcessingRepository imgProRep = new ImageProcessingRepository();
                    _LoadNewDsStep2ViewModel.ListofTechniques = imgProRep.getAllTechnique();
                }
            } else if (FromViewAlgorithms)
            {
                _ViewAlgorithmViewModel.GetAllAlgorithmPlans();
                OverallFrame.Content = _ViewAlgorithmViewModel;
            } else
            {
                OverallFrame.Content = _ViewExistingDatasetViewModel;
            }
        }

        public void ClearSelectedCommand( object obj )
        {
            SelectedProcesses.Clear();
        }

        private bool _canClearSelectedAlgorithms( object parameter )
        {
            return SelectedProcesses.Any();
        }

        /// <summary>
        /// Occurs when the selected processes collection is modified
        /// </summary>
        /// <param name="sender">N/A</param>
        /// <param name="e">Event information</param>
        private void _chosenProcessesCollectionChanged( object sender, NotifyCollectionChangedEventArgs e )
        {
            _clearSelectedAlgorithms.ExecutableStateChanged();
            switch( e.Action )
            {
                case NotifyCollectionChangedAction.Add:
                    foreach( AlgorithmViewModel vm in e.NewItems.OfType<AlgorithmViewModel>() )
                    {
                        vm.IsRemovable = true;
                        vm.RemovalRequested += _algorithmRemovalRequested;
                    }
                    break;

                case NotifyCollectionChangedAction.Remove:
                    var items = e.OldItems ?? new List<AlgorithmViewModel>();
                    foreach( AlgorithmViewModel vm in items.OfType<AlgorithmViewModel>() )
                    {
                        vm.RemovalRequested -= _algorithmRemovalRequested;
                    }
                    break;
            }
        }

        /// <summary>
        /// Occurs when an algorithm has requested removal from the selected
        /// algorithms collection
        /// </summary>
        /// <param name="sender">The algorithm</param>
        /// <param name="e">N/A</param>
        private void _algorithmRemovalRequested( object sender, EventArgs e )
        {
            AlgorithmViewModel vm = sender as AlgorithmViewModel;
            if( vm != null && SelectedProcesses.Contains( vm ) )
            {
                SelectedProcesses.Remove( vm );
            }
        }

        private void _progressToStep3(object obj)
        {
            OverallFrame.Content = BaseViewModel._PostProcessingViewModel;

            BaseViewModel._LoadNewDsStep3ViewModel.ListOfFiles.Clear();
            this.ListOfFiles.ForEach(BaseViewModel._LoadNewDsStep3ViewModel.ListOfFiles.Add);
            BaseViewModel._LoadNewDsStep3ViewModel.PipelineAlgorithms.Clear();

            _updateAlgorithmsInTechnique();

            TechniqueAlgorithms.ForEach(_addAlgorithmToStep3);

            if (ChosenTechnique != null)
            {
                BaseViewModel._LoadNewDsStep3ViewModel.PipelineName = ChosenTechnique.Name;
            }
            else { BaseViewModel._LoadNewDsStep3ViewModel.PipelineName = "Quick Build"; }
        }

        private void _addAlgorithmToStep3(AlgorithmViewModel viewModel)
        {
            viewModel.IsRemovable = false;
            BaseViewModel._LoadNewDsStep3ViewModel.PipelineAlgorithms.Add(viewModel);
        }

        private void _updateAlgorithmsInTechnique()
        {
                TechniqueAlgorithms.Clear();
            
                IUnityContainer c = GlobalContainer.Instance.Container;
                IPipelineManager manager = c.Resolve<IPipelineManager>();

                if (SelectedProcesses != null)
                {
                     foreach (var process in SelectedProcesses)
                             {
                                TechniqueAlgorithms.Add(process);
                             }
                }
        } 
    }

}
