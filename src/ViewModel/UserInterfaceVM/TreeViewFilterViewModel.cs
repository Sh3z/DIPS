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
        
        public ICommand ApplyFilterCommand { get; set; }

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

        public TreeViewFilterViewModel()
        {
            ConfigureCommands();

        }

        private void ConfigureCommands()
        {
            ApplyFilterCommand = new RelayCommand(new Action<object>(ApplyFilter));
        }

        private void ApplyFilter(object obj)
        {
            //PrepareParameters();
            AssignFilter();
        }

        //private void PrepareParameters()
        //{
        //    TheFilter = new Filter();

        //    TheFilter.PatientID = PatientID;

        //    if (DateFrom != DateTime.MinValue)
        //    {
        //        TheFilter.AcquisitionDateFrom = DateFrom;
        //    }

        //    if (DateTo != DateTime.MinValue)
        //    {
        //        TheFilter.AcquisitionDateTo = DateTo;
        //    }

        //    if (IsFemale == true)
        //    {
        //        TheFilter.Gender = "F";
        //    } else if (IsMale == true)
        //    {
        //        TheFilter.Gender = "M";
        //    }
        //    else
        //    {
        //        TheFilter.Gender = String.Empty;
        //    }
            
            
        //}

        private void AssignFilter()
        {
            //ObservableCollection<Patient> dataset = new ObservableCollection<Patient>();
            //ImageRepository repo = new ImageRepository();
            //dataset = repo.generateCustomTreeView(TheFilter,true);

            //if (dataset != null)
            //{
            //    _ViewExistingDatasetViewModel.TopLevelViewModel = new TreeViewGroupPatientsViewModel(dataset);
            //}
        }
    }
}
