using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DIPS.Processor.Client;
using DIPS.Unity;
using DIPS.ViewModel.Commands;

namespace DIPS.ViewModel.UserInterfaceVM
{
    public class ViewAlgorithmViewModel : BaseViewModel
    {
        public ICommand AddNewAlgorithmCommand { get; set; }

        public IProcessingService Service
        {
            get;
            set;
        }

        public ViewAlgorithmViewModel()
        {
            SetupCommands();
        }

        private void SetupCommands()
        {
            AddNewAlgorithmCommand = new RelayCommand(new Action<object>(AddNewAlgorithm));
        }

        private void AddNewAlgorithm(object obj)
        {
            OverallFrame.Content = _AlgorithmBuilderViewModel;
            _AlgorithmBuilderViewModel.Container = GlobalContainer.Instance.Container; 
            
            foreach( var algorithm in Service.PipelineManager.AvailableProcesses )
            {
                AlgorithmViewModel viewModel = new AlgorithmViewModel( algorithm );
                _AlgorithmBuilderViewModel.AvailableAlgorithms.Add(viewModel);
            }
        }
    }
}
