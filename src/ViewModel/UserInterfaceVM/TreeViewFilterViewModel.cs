using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Database;
using Database.Objects;
using Database.Unity;
using DIPS.Database.Objects;
using DIPS.Unity;
using DIPS.ViewModel.Commands;
using Microsoft.Practices.Unity;
using DIPS.Util.Commanding;
using Database.Repository;

namespace DIPS.ViewModel.UserInterfaceVM
{
    public class TreeViewFilterViewModel:BaseViewModel
    {

        #region Properties

        public string PatientID { set; get; }
        public string Modality { set; get; }
        public string Batch { set; get; }
        public DateTime DateFrom { set; get; }
        public DateTime DateTo { set; get; }
        public bool IsMale { set; get; }
        public bool IsFemale { set;get; }

        public Filter OverallFilter { set;get; }

        public ICommand ApplyFilterCommand { get; set; }
        public ICommand CancelFilterSelection { get; set; }

        private IFilterTreeView _filterImageView;

        public IFilterTreeView FilterTreeView
        {
            get { return _filterImageView; }
            set { _filterImageView = value; }
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

        private Boolean _showName;

        public Boolean ShowName
        {
            get { return _showName; }
            set {
                if (value == true)
                {
                    Boolean verified = false;
                    AdminRepository admin = new AdminRepository();
                    verified = admin.verified();
                    if (verified == true) _showName = value; 
                }
                else _showName = value;
            }
        }

        private object _hideWindow;

        public object HideWindow
        {
            get { return _hideWindow; }
            set
            {
                _hideWindow = value;
                HideDialog(null);
            }
        }
        #endregion

        #region Constructor
        public TreeViewFilterViewModel()
        {
            DateFrom = DateTime.Today.AddYears(-2);
            DateTo = DateTime.Today;
            ConfigureCommands();
            SendParameters();
        }
        #endregion

        #region Methods
        private void ConfigureCommands()
        {
            ApplyFilterCommand = new RelayCommand(new Action<object>(AssignFilter));
            CancelFilterSelection = new RelayCommand(new Action<object>(HideDialog));
        }

        public void HideDialog(object obj)
        {
            if (FilterTreeView == null)
            {
                Container = GlobalContainer.Instance.Container;
                FilterTreeView = Container.Resolve<IFilterTreeView>();
            }
            
            FilterTreeView.HideDialog();
        }

        private void SendParameters()
        {
            if (FilterTreeView != null)
            {
                FilterTreeView.PatientID = PatientID;
                FilterTreeView.DateFrom = DateFrom;
                FilterTreeView.DateTo = DateTo;
                FilterTreeView.IsFemale = IsFemale;
                FilterTreeView.IsMale = IsMale;
                FilterTreeView.ShowNames = ShowName;
                FilterTreeView.Modality = Modality;
                FilterTreeView.Batch = Batch;
            }
            
        }
        
        private void AssignFilter(object obj)
        {
            if (FilterTreeView == null)
            {
                 Container = GlobalContainer.Instance.Container;
                 FilterTreeView = Container.Resolve<IFilterTreeView>();
            }
           
            if (FilterTreeView != null)
            {
                SendParameters();
                FilterTreeView.PrepareParameters();

                ObservableCollection<Patient> dataset = FilterTreeView.ApplyFilter();

                if (dataset != null)
                {
                    _ViewExistingDatasetViewModel.TopLevelViewModel = new TreeViewGroupPatientsViewModel(dataset);
                    _ViewExistingDatasetViewModel.ToggleFilter = true;
                }
                
                HideDialog(null);
            }
            
        } 
        #endregion
    }
}
