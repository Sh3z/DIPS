using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using DIPS.Database.Objects;
using DIPS.ViewModel.Commands;
using Microsoft.Win32;

namespace DIPS.ViewModel.UserInterfaceVM
{
    public class LoadNewDsStep1ViewModel : BaseViewModel
    {
        #region Properties
        public ICommand ProgressToStep2Command { get; set; }
        public ICommand OpenFileDialogCommand { get; set; }
        public ICommand ClearFieldsCommand { get; set; }

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

        private ObservableCollection<FileInfo> _listofFiles;

        public ObservableCollection<FileInfo> ListOfFiles
        {
            get { return _listofFiles; }
            set
            {
                _listofFiles = value;
                OnPropertyChanged();
            }
        } 
        #endregion

        #region Constructor
        public LoadNewDsStep1ViewModel()
        {
            SetupCommands();
            LoadTechniqueObjects();
        } 
        #endregion

        #region Methods
        private void SetupCommands()
        {
            ProgressToStep2Command = new RelayCommand(new Action<object>(ConfirmAndMoveToStep2));
            OpenFileDialogCommand = new RelayCommand(new Action<object>(SelectFilesForDataset));
            ClearFieldsCommand = new RelayCommand(new Action<object>(ClearFields));
        }

        private void ClearFields(object obj)
        {
            ListOfFiles.Clear();
        }

        private void ConfirmAndMoveToStep2(object obj)
        {
            if (ValidateFields())
            {
                _LoadNewDsStep2ViewModel.ListOfFiles = ListOfFiles;
                OverallFrame.Content = _LoadNewDsStep2ViewModel;

                if (_LoadNewDsStep2ViewModel != null)
                {
                    _LoadNewDsStep2ViewModel.ListofTechniques.Clear();
                    _LoadNewDsStep2ViewModel.ListofTechniques = new ObservableCollection<Technique>();

                    LoadTechniqueObjects();
                    _LoadNewDsStep2ViewModel.ListofTechniques = ListofTechniques;
                }
            }

        }

        private void SelectFilesForDataset(object obj)
        {
            Stream myStream;
            OpenFileDialog dialogOpen = new OpenFileDialog();
            ;
            //Setup properties for open file dialog
            dialogOpen.InitialDirectory = "C:\\";
            dialogOpen.Filter = @"Bitmaps|*.bmp|Jpgs|*.jpg";
            dialogOpen.FilterIndex = 1;
            dialogOpen.Multiselect = true;
            dialogOpen.Title = "Please select image files which are going to be part of this dataset";

            Nullable<bool> isOkay = dialogOpen.ShowDialog();
            String[] strFiles = dialogOpen.FileNames;

            if (isOkay == true)
            {
                ListOfFiles = new ObservableCollection<FileInfo>();

                foreach (string file in dialogOpen.FileNames)
                {
                    FileInfo uploadFile = new FileInfo(file);
                    ListOfFiles.Add(uploadFile);
                }
            }

        }
        private Boolean ValidateFields()
        {
            if (ListOfFiles.Count == 0)
            {
                MessageBox.Show("No files have been selected for processing.", "No files selected.", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return false;
            }

            return true;
        }

        private void LoadTechniqueObjects()
        {
            ListofTechniques = new ObservableCollection<Technique>();

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
        #endregion
    }
}
