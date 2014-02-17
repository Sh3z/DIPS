using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            LoadTechniqueObjects();
        }

         private void ProgressToStep3(object obj)
         {
             OverallFrame.Content = BaseViewModel._LoadNewDsStep3ViewModel;

             BaseViewModel._LoadNewDsStep3ViewModel.ListOfFiles= this.ListOfFiles;
             BaseViewModel._LoadNewDsStep3ViewModel.ListofTechniques = this.ListofSelectedTechniques;
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

        private void LoadTechniqueObjects()
        {

            Technique tech1 = new Technique();
            Technique tech2 = new Technique();
            Technique tech3 = new Technique();
            Technique tech4 = new Technique();

            tech1.ID = 1;
            tech2.ID = 2;
            tech3.ID = 3;
            tech4.ID = 4;

            tech1.Name = "Blurring";
            tech2.Name = "Shading";
            tech3.Name = "Fuzzy";
            tech4.Name = "Whitening";

            ListofTechniques.Add(tech1);
            ListofTechniques.Add(tech2);
            ListofTechniques.Add(tech3);
            ListofTechniques.Add(tech4);
        }
    }
}
