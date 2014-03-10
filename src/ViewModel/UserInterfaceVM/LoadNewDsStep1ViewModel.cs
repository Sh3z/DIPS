using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DIPS.Database.Objects;
using DIPS.ViewModel.Commands;
using Microsoft.Win32;
using DIPS.Util.Commanding;

namespace DIPS.ViewModel.UserInterfaceVM
{
    public class LoadNewDsStep1ViewModel : BaseViewModel
    {
        #region Properties
        public ICommand ProgressToStep2Command { get; set; }
        public ICommand OpenFileDialogCommand { get; set; }
        public ICommand ClearFieldsCommand { get; set; }
        public ICommand RemoveFileFromListCommand { get; set; }

        private FileInfo _selectedFileItem;

        public FileInfo SelectedFileItem
        {
            get { return _selectedFileItem; }
            set { _selectedFileItem = value; }
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
        } 
        #endregion

        #region Methods
        private void SetupCommands()
        {
            ProgressToStep2Command = new RelayCommand(new Action<object>(ConfirmAndMoveToStep2));
            OpenFileDialogCommand = new RelayCommand(new Action<object>(SelectFilesForDataset));
            ClearFieldsCommand = new RelayCommand(new Action<object>(ClearFields));
            RemoveFileFromListCommand = new RelayCommand(new Action<object>(RemoveFileFromList));

            ListOfFiles = new ObservableCollection<FileInfo>();
        }

        private void ClearFields(object obj)
        {
            ListOfFiles.Clear();
        }

        private void RemoveFileFromList(object obj)
        {
            if (SelectedFileItem != null)
            {
                ListOfFiles.Remove(SelectedFileItem);
            }
        }

        private void ConfirmAndMoveToStep2(object obj)
        {
            if (ValidateFields())
            {
                _LoadNewDsStep2ViewModel.ListOfFiles = ListOfFiles;
                OverallFrame.Content = _LoadNewDsStep2ViewModel;
            }

        }

        private void SelectFilesForDataset(object obj)
        {
            Stream myStream;
            OpenFileDialog dialogOpen = new OpenFileDialog();
            ;
            //Setup properties for open file dialog
            dialogOpen.InitialDirectory = "C:\\";
            dialogOpen.FilterIndex = 1;
            dialogOpen.Multiselect = true;
            dialogOpen.Title = "Please select image files which are going to be part of this dataset";

            Nullable<bool> isOkay = dialogOpen.ShowDialog();
            String[] strFiles = dialogOpen.FileNames;

            if (isOkay == true)
            {
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

        #endregion
    }
}
