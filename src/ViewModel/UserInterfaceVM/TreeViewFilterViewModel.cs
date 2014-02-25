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

namespace DIPS.ViewModel.UserInterfaceVM
{
    public class TreeViewFilterViewModel:BaseViewModel
    {

        #region Properties

        public string PatientID { set; get; }
        public DateTime DateFrom { set; get; }
        public DateTime DateTo { set; get; }
        public bool IsMale { set; get; }
        public bool IsFemale { set;get; }
        public Filter OverallFilter { set;get; }

        public ICommand ApplyFilterCommand { get; set; }

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
        #endregion

        #region Constructor
        public TreeViewFilterViewModel()
        {
            ConfigureCommands();
        } 
        #endregion

        #region Methods
        private void ConfigureCommands()
        {
            ApplyFilterCommand = new RelayCommand(new Action<object>(AssignFilter));
        }

        private void SendParameters()
        {
            FilterTreeView.PatientID = PatientID;
            FilterTreeView.DateFrom = DateFrom;
            FilterTreeView.DateTo = DateTo;
            FilterTreeView.IsFemale = IsFemale;
            FilterTreeView.IsMale = IsMale;
        }

        private void AssignFilter(object obj)
        {
            Container = GlobalContainer.Instance.Container;

            FilterTreeView = Container.Resolve<IFilterTreeView>();

            if (FilterTreeView != null)
            {
                SendParameters();
                FilterTreeView.PrepareParameters();

                ObservableCollection<Patient> dataset = FilterTreeView.ApplyFilter();

                if (dataset != null)
                {
                    _ViewExistingDatasetViewModel.TopLevelViewModel = new TreeViewGroupPatientsViewModel(dataset);
                }
                
                FilterTreeView.HideDialog();
            }
            
        } 
        #endregion
    }
}
