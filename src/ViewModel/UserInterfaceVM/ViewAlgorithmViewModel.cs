using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using Database;
using Database.Repository;
using DIPS.Database.Objects;
using DIPS.Processor.Client;
using DIPS.Unity;
using DIPS.ViewModel.Commands;

namespace DIPS.ViewModel.UserInterfaceVM
{
    public class ViewAlgorithmViewModel : BaseViewModel
    {
        public ICommand AddNewAlgorithmCommand { get; set; }

        private List<Technique> _allTechniques;

        public List<Technique> AllTechniques
        {
            get { return _allTechniques; }
            set
            {
                _allTechniques = value;
                OnPropertyChanged();
            }
        }

        public object SelectedTechnique { get; set; }
        
        public IProcessingService Service
        {
            get;
            set;
        }

        public ViewAlgorithmViewModel()
        {
            SetupCommands();
            GetAllAlgorithmPlans();
        }

        private void GetAllAlgorithmPlans()
        {
            AllTechniques = new List<Technique>();
            ImageProcessingRepository imgProRep = new ImageProcessingRepository();

            AllTechniques = imgProRep.getAllTechnique();
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

        private void EditAlgorithm(object obj)
        {
            OverallFrame.Content = _AlgorithmBuilderViewModel;


        }
    }
}
