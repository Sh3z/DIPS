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

namespace DIPS.ViewModel
{
    /// <summary>
    /// Represents the object providing the presentation logic for the
    /// algorithm builder view.
    /// </summary>
    public class AlgorithmBuilderViewModel : ViewModel, IPipelineInfo
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
            private set;
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
    }
}
