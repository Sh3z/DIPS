using DIPS.Processor.Client;
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
    public class AlgorithmBuilderViewModel : ViewModel
    {
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="AlgorithmBuilderViewModel"/> class.
        /// </summary>
        public AlgorithmBuilderViewModel()
        {
            AvailableAlgorithms = new ObservableCollection<AlgorithmDefinition>();
            ComposedAlgorithms = new ObservableCollection<AlgorithmDefinition>();
        }


        /// <summary>
        /// Gets the collection of <see cref="AlgorithmDefinition"/>s available
        /// for building a technique with.
        /// </summary>
        public ObservableCollection<AlgorithmDefinition> AvailableAlgorithms
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the collection of <see cref="AlgorithmDefinition"/>s chosen
        /// for the current composite algorithm.
        /// </summary>
        public ObservableCollection<AlgorithmDefinition> ComposedAlgorithms
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the selected <see cref="AlgorithmDefinition"/>.
        /// </summary>
        public AlgorithmDefinition SelectedAlgorithm
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
        private AlgorithmDefinition _selectedAlgorithm;
    }
}
