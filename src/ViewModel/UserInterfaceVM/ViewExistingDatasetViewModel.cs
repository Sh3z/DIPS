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
using DIPS.Database.Objects;
using DIPS.ViewModel.Commands;
using Microsoft.Practices.Unity;

namespace DIPS.ViewModel.UserInterfaceVM
{
    public class ViewExistingDatasetViewModel :BaseViewModel
    {

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

        public TreeViewGroupPatientsViewModel TopLevelViewModel { get { return _topLevel; }
            set
            {
                _topLevel = value;
                OnPropertyChanged();
            } }
        private TreeViewGroupPatientsViewModel _topLevel;

        private BitmapImage _imgUnprocessed;

        public BitmapImage ImgUnprocessed
        {
            get { return _imgUnprocessed;  }
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

        public ViewExistingDatasetViewModel()
        {
            GetPatientsForTreeview();
        }

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
            }
        }
        

        private void SetupCommands()
        {
            OpenFilterDialogCommand = new RelayCommand(new Action<object>(OpenFilterDialog));
        }

        private void OpenFilterDialog(object obj)
        {
            Container = new UnityContainer();
        }

        public void GetPatientsForTreeview()
        {
            ImageRepository repo = new ImageRepository();
            PatientsList = repo.generateTreeView();
            TreeViewGroupPatientsViewModel tvpv = new TreeViewGroupPatientsViewModel(PatientsList);

            TopLevelViewModel = tvpv;
        }


    }
}
