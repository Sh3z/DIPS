using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Database;
using DIPS.Database.Objects;
using DIPS.Processor.Client;
using DIPS.Unity;
using DIPS.ViewModel.Commands;
using Microsoft.Expression.Interactivity.Core;
using Microsoft.Practices.Unity;
using DIPS.ViewModel.UserInterfaceVM.JobTracking;

namespace DIPS.ViewModel.UserInterfaceVM
{
    public class MainViewModel : BaseViewModel
    {
        /// <summary>
        /// The current view.
        /// </summary>
        private static BaseViewModel _currentViewModel;

        public static BaseViewModel CurrentViewModel
        {
            get
            {
                return _currentViewModel;
            }
            set
            {
                if (_currentViewModel == value)
                    return;
                _currentViewModel = value;
            }
        }

        /// <summary>
        /// Static instances of the ViewModels.
        /// </summary>
       
        public ICommand ViewExistingDataSetCommand { get; set; }
        public ICommand ViewProcessDataSetCommand { get; set; }
        public ICommand ViewCreateAlgorithmCommand { get; set; }
        public ICommand ViewAboutCommand { get; set; }
    
        public IProcessingService Service { get; set; }

        public MainViewModel(Frame theFrame)
        {
            SetupCommands();
            OverallFrame = theFrame;

            OngoingJobsViewModel vm = new OngoingJobsViewModel();
            vm.Handler = new SaveResultsHandler();
            GlobalContainer.Instance.Container.RegisterInstance<IJobTracker>( vm );
        }

        private void SetupCommands()
        {
            ViewExistingDataSetCommand = new RelayCommand(new Action<object>(ShowExistingDataSet));
            ViewProcessDataSetCommand = new RelayCommand(new Action<object>(ShowProcessDataSet));
            ViewCreateAlgorithmCommand = new RelayCommand(new Action<object>( ShowCreateAlgorithm));
            ViewAboutCommand = new RelayCommand(new Action<object>(ShowAbout));
        }

        private static void ShowExistingDataSet(object obj)
        {
            ImageRepository imgRepository = new ImageRepository();
            
            OverallFrame.Content = _ViewExistingDatasetViewModel;
        }

        private static void ShowProcessDataSet(object obj)
        {
            OverallFrame.Content = _LoadNewDsStep1ViewModel;
            
            if (_LoadNewDsStep1ViewModel.ListOfFiles != null)
            {
                 _LoadNewDsStep1ViewModel.ListOfFiles.Clear();
            }
        }

        private void ShowCreateAlgorithm(object obj)
        {
            OverallFrame.Content = _AlgorithmBuilderViewModel;

            _AlgorithmBuilderViewModel.Container = GlobalContainer.Instance.Container;

            foreach (var algorithm in Service.PipelineManager.AvailableProcesses)
            {
                AlgorithmViewModel viewModel = new AlgorithmViewModel(algorithm);
                _AlgorithmBuilderViewModel.AvailableAlgorithms.Add(viewModel);
            }
        }

        private void ShowAbout(object obj)
        {
            
        }
    }


    }
