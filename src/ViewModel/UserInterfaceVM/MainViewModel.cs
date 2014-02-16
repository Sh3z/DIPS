using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using DIPS.Processor.Client;
using DIPS.ViewModel.Commands;

namespace DIPS.ViewModel.UserInterfaceVM
{
    public class MainViewModel
    {
        public ICommand ViewExistingDataSetCommand { get; set; }
        public ICommand ViewProcessDataSetCommand { get; set; }
        public ICommand ViewCreateAlgorithmCommand { get; set; }
        public ICommand ViewAboutCommand { get; set; }
    
        public IProcessingService Service { get; set; }

        public MainViewModel()
        {
            SetupCommands();
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
           
            //LoadNewDSStep1 loadDS1 = new LoadNewDSStep1();
        }

        private static void ShowProcessDataSet(object obj)
        {
            
        }

        private void ShowCreateAlgorithm(object obj)
        {
            
        }

        private void ShowAbout(object obj)
        {
            
        }
    }


    }
