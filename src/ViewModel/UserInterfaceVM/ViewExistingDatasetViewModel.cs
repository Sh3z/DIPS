using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Database;
using Database.Objects;
using Database.Unity;
using DIPS.Database.Objects;
using DIPS.Unity;
using DIPS.ViewModel.Commands;
using Microsoft.Practices.Unity;
using Database.Repository;

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
        public ICommand OpenQueueCommand { get; set; }

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

                if (transform) GetPatientsForTreeview();
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
                    GetPatientsForTreeview();
                }
            }
        }
        
        
        #endregion

        #region Constructor
        public ViewExistingDatasetViewModel()
        {
            SetupCommands();
            GetPatientsForTreeview();
        } 
        #endregion

        #region Methods
        private void SetupCommands()
        {
            OpenFilterDialogCommand = new RelayCommand(new Action<object>(OpenFilterDialog));
            OpenQueueCommand = new RelayCommand(new Action<object>(OpenQueueDialog));
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

        private void OpenQueueDialog(object obj)
        {
            if (QueueDialog == null)
            {
                Container = GlobalContainer.Instance.Container;
                QueueDialog = Container.Resolve<IQueueDialog>();
            }

            QueueDialog.ShowDialog();
        }

        private void GetPatientsForTreeview()
        {
            ImageRepository repo = new ImageRepository();
            PatientsList = repo.generateTreeView(_isSelected);
            TreeViewGroupPatientsViewModel tvpv = new TreeViewGroupPatientsViewModel(PatientsList);

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
