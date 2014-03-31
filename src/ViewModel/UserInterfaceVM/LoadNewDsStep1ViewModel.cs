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
using System.Diagnostics;
using DIPS.Unity;
using Microsoft.Practices.Unity;
using DIPS.Database;

namespace DIPS.ViewModel.UserInterfaceVM
{
    public class LoadNewDsStep1ViewModel : BaseViewModel
    {
        #region Properties
        public ICommand ProgressToStep2Command
        {
            get
            {
                return _progressToNextStepCmd;
            }
        }
        [DebuggerBrowsable( DebuggerBrowsableState.Never )]
        private RelayCommand _progressToNextStepCmd;

        public ICommand OpenFileDialogCommand { get; set; }
        public ICommand ClearFieldsCommand { get; set; }

        public ICommand RemoveFileFromListCommand
        {
            get
            {
                return _removeSelectedItemCmd;
            }
        }
        [DebuggerBrowsable( DebuggerBrowsableState.Never )]
        private RelayCommand _removeSelectedItemCmd;

        private FileInfo _selectedFileItem;

        public FileInfo SelectedFileItem
        {
            get { return _selectedFileItem; }
            set
            {
                _selectedFileItem = value;
                OnPropertyChanged();
                _removeSelectedItemCmd.ExecutableStateChanged();
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

        public bool RecursivleyLoadInputs
        {
            get
            {
                return _recursivleyLoadInputs;
            }
            set
            {
                _recursivleyLoadInputs = value;
                OnPropertyChanged();
            }
        }
        [DebuggerBrowsable( DebuggerBrowsableState.Never )]
        private bool _recursivleyLoadInputs;

        public ICommand RemoveAllInputs
        {
            get
            {
                return _removeAllInputsCmd;
            }
        }
        [DebuggerBrowsable( DebuggerBrowsableState.Never )]
        private RelayCommand _removeAllInputsCmd;

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
            _progressToNextStepCmd = new RelayCommand(new Action<object>(ConfirmAndMoveToStep2), _canMoveToNextStep);
            OpenFileDialogCommand = new RelayCommand(new Action<object>(SelectFilesForDataset));
            ClearFieldsCommand = new RelayCommand(new Action<object>(ClearFields));
            _removeSelectedItemCmd = new RelayCommand(new Action<object>(RemoveFileFromList), _canRemoveSelectedFile);
            _removeAllInputsCmd = new RelayCommand( _removeAllInputs, _canRemoveAllInputs );

            ListOfFiles = new ObservableCollection<FileInfo>();
            ListOfFiles.CollectionChanged += _filesCollectionModified;
        }

        

        public void ClearFields(object obj)
        {
            ListOfFiles.Clear();
        }

        private bool _canRemoveSelectedFile( object obj )
        {
            return SelectedFileItem != null;
        }

        public void RemoveFileFromList(object obj)
        {
            if (SelectedFileItem != null)
            {
                ListOfFiles.Remove(SelectedFileItem);
            }
        }

        private bool _canMoveToNextStep( object obj )
        {
            return ListOfFiles.Count > 0;
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
            if( RecursivleyLoadInputs )
            {
                IDirectoryPicker picker = GlobalContainer.Instance.Container.Resolve<IDirectoryPicker>();
                if( picker.Resolve() )
                {
                    _loadFilesFromDirectoryAndSubDirectories( picker.Directory );
                }
            }
            else
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

                if( isOkay == true )
                {
                    foreach( string file in dialogOpen.FileNames )
                    {
                        _addFileToCurrentSet( file );
                    }
                }
            }
        }

        private bool _canRemoveAllInputs( object obj )
        {
            return ListOfFiles.Count > 0;
        }

        private void _removeAllInputs( object obj )
        {
            ListOfFiles.Clear();
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

        private void _filesCollectionModified( object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e )
        {
            _removeAllInputsCmd.ExecutableStateChanged();
            _progressToNextStepCmd.ExecutableStateChanged();
        }

        private void _loadFilesFromDirectoryAndSubDirectories( string directory )
        {
            if( Directory.Exists( directory ) == false )
            {
                return;
            }

            foreach( var file in Directory.GetFiles( directory ) )
            {
                _addFileToCurrentSet( file );
            }

            foreach( var subDir in Directory.GetDirectories( directory ) )
            {
                _loadFilesFromDirectoryAndSubDirectories( subDir );
            }
        }

        private void _addFileToCurrentSet( string file )
        {
            verifyDicom dicom = new verifyDicom();
            // Make sure the file is legal
            if( dicom.verify( file )  )
            {
                FileInfo fileInfo = new FileInfo( file );
                ListOfFiles.Add( fileInfo );
            }
        }

        #endregion
    }
}
