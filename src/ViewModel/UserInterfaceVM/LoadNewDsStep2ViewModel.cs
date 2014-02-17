using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;
using DIPS.Database.Objects;
using DIPS.ViewModel.Commands;

namespace DIPS.ViewModel.UserInterfaceVM
{
    public class LoadNewDsStep2ViewModel : BaseViewModel
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

        private ObservableCollection<Technique> _listOfTechniques;

        public ObservableCollection<Technique> ListofTechniques
        {
            get { return _listOfTechniques; }
            set
            {
                _listOfTechniques = value; 
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Technique> _listofSelectedTechniques;

        public ObservableCollection<Technique> ListofSelectedTechniques
        {
            get { return _listofSelectedTechniques; }
            set
            {
                _listofSelectedTechniques = value; 
                OnPropertyChanged();
            }
        }
         
        public Technique AvailableTechSelectedItem { get; set; }
        public Technique SelectedTechSelectedItem { get; set; }

        public ICommand ProgressToStep3Command { get; set; }
        public ICommand AddTechToSelectedCommand { get; set; }
        public ICommand DeSelectTechCommand { get; set; }
        public ICommand BuildAlgorithmCommand { get; set; }

        public LoadNewDsStep2ViewModel()
        {
            ListofTechniques = new ObservableCollection<Technique>();
            ListofSelectedTechniques = new ObservableCollection<Technique>();
            
            SetupCommands();
        }

         private void ProgressToStep3(object obj)
         {
             OverallFrame.Content = BaseViewModel._LoadNewDsStep3ViewModel;

             BaseViewModel._LoadNewDsStep3ViewModel.ListOfFiles= this.ListOfFiles;
             BaseViewModel._LoadNewDsStep3ViewModel.ListOfTechniques = this.ListofSelectedTechniques;
         }

        
        private void DeSelectTech(object obj)
        {
            if (ListofSelectedTechniques.Count > 0 && SelectedTechSelectedItem != null)
            {
                Technique tempTech = new Technique();

                tempTech = (Technique)SelectedTechSelectedItem;

                ListofSelectedTechniques.Remove((Technique)tempTech);
                ListofTechniques.Add(tempTech);
            }
        }

        private void BuildAlgorithm(object obj)
        {
            OverallFrame.Content = BaseViewModel._CreateAlgorithmViewModel;
        }

        private void PassToSelectedTech(object obj)
        {
            if (ListofTechniques.Count > 0 && AvailableTechSelectedItem != null)
            {
                Technique tempTech = new Technique();

                tempTech = (Technique)AvailableTechSelectedItem;

                ListofTechniques.Remove((Technique)tempTech);
                ListofSelectedTechniques.Add(tempTech);
            }

        }

        private void SetupCommands()
        {
            ProgressToStep3Command = new RelayCommand(new Action<object>(ProgressToStep3));
            AddTechToSelectedCommand = new RelayCommand(new Action<object>(PassToSelectedTech));
            DeSelectTechCommand = new RelayCommand(new Action<object>(DeSelectTech));
            BuildAlgorithmCommand = new RelayCommand(new Action<object>(BuildAlgorithm));
        }

        
    }
}
