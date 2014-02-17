using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DIPS.Database.Objects;
using DIPS.ViewModel.Commands;

namespace DIPS.ViewModel.UserInterfaceVM
{
    public class LoadNewDsStep3ViewModel : BaseViewModel
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
        
        
        public ObservableCollection<Technique> ListOfTechniques { get; set; }
        public ComboBoxItem PostProcessAction { get; set; }

        public ICommand ProcessFilesCommand { get; set; }

        public LoadNewDsStep3ViewModel()
        {
            SetupCommands();
        }

        private void ProcessFiles(object obj)
        {

            if (PostProcessAction.Content.ToString() == "Shut down")
            {
                MessageBoxResult result = MessageBox.Show("You have chosen to turn off the computer after processing - are you sure?", "Shut down computer after processing?", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    MessageBox.Show("Images processed.", "Processing Complete", MessageBoxButton.OK, MessageBoxImage.Information);
                    Process.Start("shutdown", "/s /t 0");
                }
            }
            else if (PostProcessAction.Content.ToString() == "Sleep")
            {
                MessageBoxResult resultSleep = MessageBox.Show("You have chosen to put the computer into hibernate mode after processing - are you sure?", "Hibernate computer after processing?", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (resultSleep == MessageBoxResult.Yes)
                {
                    MessageBox.Show("Images processed.", "Processing Complete", MessageBoxButton.OK, MessageBoxImage.Information);
                    // Hibernate
                    Process.Start("shutdown", "/h /f");
                }

            }
            else
            {
                MessageBox.Show("Images processed", "Processing complete.", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            OverallFrame.Content = BaseViewModel._MainViewModel;
        }

        private void SetupCommands()
        {
            ProcessFilesCommand = new RelayCommand(new Action<object>(ProcessFiles));
        }

    }
}
