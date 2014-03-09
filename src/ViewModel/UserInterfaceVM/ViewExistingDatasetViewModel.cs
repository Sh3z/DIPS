using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Database;
using Database.Unity;
using DIPS.Database.Objects;
using DIPS.Unity;
using DIPS.ViewModel.Commands;
using Microsoft.Practices.Unity;
using Database.Repository;
using DIPS.ViewModel.Unity;
using DIPS.Util.Commanding;

namespace DIPS.ViewModel.UserInterfaceVM
{
    public class ViewExistingDatasetViewModel :BaseViewModel
    {

        #region Properties
        private ObservableCollection<Patient> _PatientsList;

        public ObservableCollection<Patient> PatientsList
        {
            get { return _PatientsList; }
            set
            {
                _PatientsList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<ImageProperties> _propertiesList;

        public ObservableCollection<ImageProperties> PropertiesList
        {
            get { return _propertiesList; }
            set { _propertiesList = value; }
        }

        public TreeViewGroupPatientsViewModel TopLevelViewModel
        {
            get { return _topLevel; }
            set
            {
                _topLevel = value;
                OnPropertyChanged();
            }
        }
        private TreeViewGroupPatientsViewModel _topLevel;

        private BitmapImage _imgUnprocessed;

        public BitmapImage ImgUnprocessed
        {
            get { return _imgUnprocessed; }
            set
            {
                _imgUnprocessed = value;
                OnPropertyChanged();
            }
        }

        private BitmapImage _imgProcessed;

        public BitmapImage ImgProcessed
        {
            get { return _imgProcessed; }
            set
            {
                _imgProcessed = value;
                OnPropertyChanged();
            }
        }

        private String _imageInfo;

        public String ImageInfo
        {
            get { return _imageInfo; }
            set
            {
                _imageInfo = value;
                OnPropertyChanged();
            }
        }

        public ICommand OpenFilterDialogCommand { get; set; }
        public ICommand RefreshTreeviewCommand { get; set; }
        public UnityCommand OpenQueueCommand { get; set; }

        public IUnityContainer Container
        {
            get
            {
                return _container;
            }
            set
            {
                _container = value;
            }
        }
        private IUnityContainer _container;

        private Boolean _isSelected;

        public Boolean IsSelected
        {
            get { return _isSelected; }
            set
            {
                TreeViewPatientViewModel tvpv = new TreeViewPatientViewModel(null);
                _isSelected = value;
                OnPropertyChanged();
                Boolean transform = true;
                AdminRepository admin = new AdminRepository();
                if (_isSelected == true) transform = admin.verified();

                if (transform) GetPatientsForTreeview(null);
                else
                {
                    _isSelected = false;
                    OnPropertyChanged();
                }
            }
        }

        private Boolean _nameFilterSelected;

        public Boolean NameFilterSelected
        {
            get { return _nameFilterSelected; }
            set
            {
                _nameFilterSelected = value;
                
            }
        }
        
        private IFilterTreeView _filterImageView;

        public IFilterTreeView FilterTreeView
        {
            get { return _filterImageView; }
            set { _filterImageView = value; }
        }

        private IQueueDialog _queueDialog;

        public IQueueDialog QueueDialog
        {
            get { return _queueDialog; }
            set { _queueDialog = value; }
        }
        

        private Boolean _toggleFilter;

        public Boolean ToggleFilter
        {
            get { return _toggleFilter; }
            set
            {
                _toggleFilter = value;
                OnPropertyChanged();

                if (_toggleFilter == true)
                {
                    if (FilterTreeView != null)
                    {
                        GetPatientsFiltered();
                    }
                }
                else
                {
                    GetPatientsForTreeview(null);
                }
            }
        }
        #endregion

        #region Constructor
        public ViewExistingDatasetViewModel()
        {
            SetupCommands();
            GetPatientsForTreeview(null);
        } 
        #endregion

        #region Methods
        private void SetupCommands()
        {
            OpenFilterDialogCommand = new RelayCommand(new Action<object>(OpenFilterDialog));
            RefreshTreeviewCommand = new RelayCommand(new Action<object>(GetPatientsForTreeview));
            OpenQueueCommand = new PresentQueueCommand();
            OpenQueueCommand.Container = GlobalContainer.Instance.Container;
        }

        private void OpenFilterDialog(object obj)
        {

            if (FilterTreeView == null)
            {
                Container = GlobalContainer.Instance.Container;
                FilterTreeView = Container.Resolve<IFilterTreeView>();
            }
           
            FilterTreeView.OpenDialog();
        }

        private void GetPatientsForTreeview(object obj)
        {
            ImageRepository repo = new ImageRepository();
            PatientsList = repo.generateTreeView(_isSelected);
            TreeViewGroupPatientsViewModel tvpv = new TreeViewGroupPatientsViewModel(PatientsList);

            ImgUnprocessed = null;
            ImgProcessed = null;
            ImageInfo = string.Empty;
            
            TopLevelViewModel = tvpv;
        }

        private void GetPatientsFiltered()
        {
            ImageRepository repo = new ImageRepository();
            PatientsList = FilterTreeView.ApplyFilter();
            TreeViewGroupPatientsViewModel tvpv = new TreeViewGroupPatientsViewModel(PatientsList);

            TopLevelViewModel = tvpv;
        }
        
        #endregion


    }
}
