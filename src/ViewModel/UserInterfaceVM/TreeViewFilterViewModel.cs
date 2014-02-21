using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Database;
using Database.Objects;
using DIPS.Database.Objects;
using DIPS.ViewModel.Commands;

namespace DIPS.ViewModel.UserInterfaceVM
{
    public class TreeViewFilterViewModel:BaseViewModel
    {
        private String _patientID;

        public String PatientID
        {
            get { return _patientID; }
            set
            {
                _patientID = value;
                OnPropertyChanged();
            }
        }

        private DateTime _dateFrom;

        public DateTime DateFrom
        {
            get { return _dateFrom; }
            set { _dateFrom = value; }
        }

        private DateTime _dateTo;

        public DateTime DateTo
        {
            get { return _dateTo; }
            set { _dateTo = value; }
        }

        private Boolean _isMale;

        public Boolean IsMale
        {
            get { return _isMale; }
            set { _isMale = value; }
        }

        private Boolean _isFemale;

        public Boolean IsFemale
        {
            get { return _isFemale; }
            set { _isFemale = value; }
        }
        
        public ICommand ApplyFilterCommand { get; set; }

        private Filter TheFilter { get; set; }
    

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
            PrepareParameters();
            AssignFilter();
        }

        private void PrepareParameters()
        {
            TheFilter = new Filter();

            TheFilter.PatientID = PatientID;

            if (DateFrom != DateTime.MinValue)
            {
                TheFilter.AcquisitionDateFrom = DateFrom;
            }

            if (DateTo != DateTime.MinValue)
            {
                TheFilter.AcquisitionDateTo = DateTo;
            }

            if (IsFemale == true)
            {
                TheFilter.Gender = "F";
            } else if (IsMale == true)
            {
                TheFilter.Gender = "M";
            }
            else
            {
                TheFilter.Gender = String.Empty;
            }
            
            
        }

        private void AssignFilter()
        {
            ObservableCollection<Patient> dataset = new ObservableCollection<Patient>();
            ImageRepository repo = new ImageRepository();
            dataset = repo.generateCustomTreeView(TheFilter);

            if (dataset != null)
            {
                _ViewExistingDatasetViewModel.TopLevelViewModel = new TreeViewGroupPatientsViewModel(dataset);
            }
        }
    }
}
