using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Input;
using DIPS.Database.Objects;
using DIPS.Processor.Client;
using DIPS.Unity;
using DIPS.ViewModel.Commands;
using DIPS.Util.Extensions;
using Microsoft.Practices.Unity;
using System.Diagnostics;

namespace DIPS.ViewModel.UserInterfaceVM
{
    public class LoadNewDsStep2ViewModel : BaseViewModel, IPipelineInfo
    {
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

        public IProcessingService Service { get; set; }

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

        public Technique ChosenTechnique
        {
            get
            {
                return _chosenTechnique;
            }
            set
            {
                _chosenTechnique = value;
                _updateAlgorithmsInTechnique( value );
                OnPropertyChanged();
            }
        }
        [DebuggerBrowsable( DebuggerBrowsableState.Never )]
        private Technique _chosenTechnique;

        public ObservableCollection<AlgorithmViewModel> TechniqueAlgorithms
        {
            get;
            private set;
        }

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
         
        public Technique AvailableTechSelectedItem { get; set; }
        public Technique SelectedTechSelectedItem { get; set; }

        public RelayCommand ProgressToStep3Command { get; set; }
        public ICommand BuildAlgorithmCommand { get; set; }
        public UnityCommand LoadFromFile
        {
            get;
            set;
        }

        public LoadNewDsStep2ViewModel()
        {
            ListofTechniques = new ObservableCollection<Technique>();
            TechniqueAlgorithms = new ObservableCollection<AlgorithmViewModel>();
            SetupCommands();
        }

        private bool _canProgressToStep3( object obj )
        {
            return TechniqueAlgorithms.Any();
        }

        private void ProgressToStep3(object obj)
        {
            OverallFrame.Content = BaseViewModel._LoadNewDsStep3ViewModel;

            BaseViewModel._LoadNewDsStep3ViewModel.ListOfFiles= this.ListOfFiles;
            BaseViewModel._LoadNewDsStep3ViewModel.PipelineAlgorithms.Clear();
            TechniqueAlgorithms.ForEach( BaseViewModel._LoadNewDsStep3ViewModel.PipelineAlgorithms.Add );
            BaseViewModel._LoadNewDsStep3ViewModel.PipelineName = ( this as IPipelineInfo ).PipelineName;
        }


        private bool _canBuildAlgorithm( object obj )
        {
            return GlobalContainer.Instance.Container.Contains<IPipelineManager>();
        }

        private void BuildAlgorithm(object obj)
        {
            OverallFrame.Content = BaseViewModel._AlgorithmBuilderViewModel;

            _AlgorithmBuilderViewModel.Container = GlobalContainer.Instance.Container;

            IPipelineManager manager = GlobalContainer.Instance.Container.Resolve<IPipelineManager>();
            _AlgorithmBuilderViewModel.AvailableAlgorithms.Clear();

            foreach (var algorithm in manager.AvailableProcesses)
            {
                AlgorithmViewModel viewModel = new AlgorithmViewModel(algorithm);
                _AlgorithmBuilderViewModel.AvailableAlgorithms.Add(viewModel);
            }
        }

        private void SetupCommands()
        {
            ProgressToStep3Command = new RelayCommand(new Action<object>(ProgressToStep3), _canProgressToStep3);
            BuildAlgorithmCommand = new RelayCommand(new Action<object>(BuildAlgorithm), _canBuildAlgorithm);
            LoadFromFile = new LoadPipelineCommand( this );
            LoadFromFile.Container = GlobalContainer.Instance.Container;

            TechniqueAlgorithms.CollectionChanged += ( s, e ) => ProgressToStep3Command.ExecutableStateChanged();
        }

        private void _updateAlgorithmsInTechnique( Technique value )
        {
            TechniqueAlgorithms.Clear();
            if( value != null )
            {
                // To-do when database side is complete
            }
        }


        string IPipelineInfo.PipelineName
        {
            get;
            set;
        }

        ObservableCollection<AlgorithmViewModel> IPipelineInfo.SelectedProcesses
        {
            get { return TechniqueAlgorithms; }
        }
    }
}
