using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Linq;
using Database;
using Database.Repository;
using DIPS.Database.Objects;
using DIPS.Processor.Client;
using DIPS.Unity;
using DIPS.ViewModel.Commands;
using Microsoft.Practices.Unity;
using DIPS.Util.Commanding;

namespace DIPS.ViewModel.UserInterfaceVM
{
    public class ViewAlgorithmViewModel : BaseViewModel
    {
        public ICommand AddNewAlgorithmCommand { get; set; }

        public IUnityContainer Container { get; set; }

        private IPipelineInfo _info;

        private ObservableCollection<Technique> _allTechniques;

        public ObservableCollection<Technique> AllTechniques
        {
            get { return _allTechniques; }
            set
            {
                _allTechniques = value;
                OnPropertyChanged();
            }
        }

        private object _selectedTechnique;

        public object SelectedTechnique
        {
            get { return _selectedTechnique; }
            set
            {
                _selectedTechnique = value;
                EditAlgorithm();
            }
        }
        
        
        public IProcessingService Service
        {
            get;
            set;
        }

        public ViewAlgorithmViewModel()
        {
            Container = GlobalContainer.Instance.Container;
            SetupCommands();
            GetAllAlgorithmPlans();
        }

        public void GetAllAlgorithmPlans()
        {
            AllTechniques = new ObservableCollection<Technique>();
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

            _AlgorithmBuilderViewModel.PipelineName = string.Empty;
            _AlgorithmBuilderViewModel.PipelineID = string.Empty;
            _AlgorithmBuilderViewModel.SelectedProcesses.Clear();
            _AlgorithmBuilderViewModel.AvailableAlgorithms.Clear();
            _AlgorithmBuilderViewModel.FromViewAlgorithms = true;
            _AlgorithmBuilderViewModel.FromLoadStep2 = false;

            _AlgorithmBuilderViewModel.GoBackButtonState = System.Windows.Visibility.Visible;
            _AlgorithmBuilderViewModel.UseAlgorithmButtonState = System.Windows.Visibility.Hidden;

            PopulateAvailableAlgorithms();
        }

        private void PopulateAvailableAlgorithms()
        {
            _AlgorithmBuilderViewModel.Container = GlobalContainer.Instance.Container;

            if (Container != null)
            {
                Service = Container.Resolve<IProcessingService>();

                if (Service != null)
                {
                    foreach (var algorithm in Service.PipelineManager.AvailableProcesses)
                    {
                        AlgorithmViewModel viewModel = new AlgorithmViewModel(algorithm);
                        viewModel.IsRemovable = false;
                        _AlgorithmBuilderViewModel.AvailableAlgorithms.Add(viewModel);
                    }
                }
            }
        }

        private void EditAlgorithm()
        {
            try
            {
                Technique tech = new Technique();
                tech = (Technique)SelectedTechnique;
                XDocument xDoc = new XDocument();

                ImageProcessingRepository proImgRepos = new ImageProcessingRepository();
                xDoc = proImgRepos.getSpecificTechnique(tech.ID);

                if (xDoc != null)
                {
                    Container = GlobalContainer.Instance.Container;

                    _AlgorithmBuilderViewModel.Container = Container;
                    IPipelineManager manager = Container.Resolve<IPipelineManager>();
                    var restoredPipeline = manager.RestorePipeline(xDoc);
                    _info = _AlgorithmBuilderViewModel;
                    _info.SelectedProcesses.Clear();
                    _info.PipelineName = Path.GetFileNameWithoutExtension(tech.Name);
                    _info.PipelineID = tech.ID.ToString();

                    foreach (var process in restoredPipeline)
                    {
                        _info.SelectedProcesses.Add(new AlgorithmViewModel(process));
                    }

                    _AlgorithmBuilderViewModel.AvailableAlgorithms.Clear();
                    _AlgorithmBuilderViewModel.GoBackButtonState = System.Windows.Visibility.Visible;
                    _AlgorithmBuilderViewModel.UseAlgorithmButtonState = System.Windows.Visibility.Hidden;
                    PopulateAvailableAlgorithms();

                    _AlgorithmBuilderViewModel.FromViewAlgorithms = true;
                    _AlgorithmBuilderViewModel.FromLoadStep2 = false;
                    OverallFrame.Content = _AlgorithmBuilderViewModel;
                }
            }
            catch (Exception e)
            {
                _showErrorDialog();
            }
        }

        private void _showErrorDialog()
        {
            IDialogService service = Container.Resolve<IDialogService>();
            service.Abstract = "Unable to load pipeline";
            service.Body = "Ensure the selected file was generated by this software and " +
                "you are connected to the same service";
            service.Image = System.Windows.MessageBoxImage.Error;
            service.Button = System.Windows.MessageBoxButton.OK;
            service.ShowDialog();
        }
    }
}
