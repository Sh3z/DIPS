using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.ViewModel
{
    /// <summary>
    /// Represents the object providing information to the
    /// <see cref="PersistPipelineCommand"/> class.
    /// </summary>
    public interface IPipelineInfo : INotifyPropertyChanged
    {
        /// <summary>
        /// Gets or sets the name given to the current pipeline.
        /// </summary>
        string PipelineName
        {
            get;
            set;
        }

        string PipelineID
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the observable collection of process view models defining
        /// the pipeline.
        /// </summary>
        ObservableCollection<AlgorithmViewModel> SelectedProcesses
        {
            get;
        }
    }
}
