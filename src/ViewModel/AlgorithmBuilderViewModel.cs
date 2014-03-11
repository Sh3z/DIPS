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
using System.Windows;

namespace DIPS.ViewModel
{
    /// <summary>
    /// Represents the object providing the presentation logic for the
    /// algorithm builder view.
    /// </summary>
    public class AlgorithmBuilderViewModel : BaseViewModel, IPipelineInfo
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
            SavePipelineDatabase = new PersistPipelineDatabaseCommand(this);

            FinishButtonCommand = new RelayCommand(new Action<object>(ProgressToMainOrStep2));
            ClearSelectedAlgorithmsCommand = new RelayCommand(new Action<object>(ClearSelectedCommand));
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

        /// <summary>
        /// Gets the collection of <see cref="AlgorithmViewModel"/>s chosen
        /// for the current composite algorithm.
        /// </summary>
        public ObservableCollection<AlgorithmViewModel> SelectedProcesses
        {
            get;
            set;
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
            set { 
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

        public UnityCommand SavePipelineDatabase
        {
            get;
            private set;
        }

        public Boolean FromLoadStep2 { get; set; }
        public ICommand FinishButtonCommand { get; set; }
        public ICommand ClearSelectedAlgorithmsCommand { get; set; }

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
            if (FromLoadStep2)
            {
                OverallFrame.Content = _LoadNewDsStep2ViewModel;
                if (_LoadNewDsStep2ViewModel.ListofTechniques != null)
                {
                     _LoadNewDsStep2ViewModel.ListofTechniques.Clear();

                    ImageProcessingRepository imgProRep = new ImageProcessingRepository();
                    _LoadNewDsStep2ViewModel.ListofTechniques = imgProRep.getAllTechnique();
                }
            }
            else
            {
                OverallFrame.Content = _ViewExistingDatasetViewModel;
            }
        }

        public void ClearSelectedCommand(object obj)
        {
            SelectedProcesses.Clear();
        }
            
        }

    }
