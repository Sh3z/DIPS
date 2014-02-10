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
            AvailableAlgorithms = new ObservableCollection<AlgorithmViewModel>();
            ComposedAlgorithms = new ObservableCollection<AlgorithmViewModel>();
        }


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
        public ObservableCollection<AlgorithmViewModel> ComposedAlgorithms
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
    }
}
