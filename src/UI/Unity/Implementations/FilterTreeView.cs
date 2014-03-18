using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Database;
using Database.Objects;
using Database.Unity;
using DIPS.Database.Objects;
using DIPS.UI.Pages;
using DIPS.ViewModel;

namespace DIPS.UI.Unity.Implementations
{
    public class FilterTreeView : IFilterTreeView
    {
        #region Properties
        public string PatientID { set; private get; }
        public string Modality {set;private get;}
        public string Batch { set; private get; }
        public DateTime DateFrom { set; private get; }
        public DateTime DateTo { set; private get; }
        public bool IsMale { set; private get; }
        public bool IsFemale { set; private get; }
        public Filter OverallFilter { set; private get; }
        public Boolean ShowNames { set; private get; }

        private TreeViewFilter FilterWindow { set; get; }
   
        #endregion

        #region Methods
        public void OpenDialog()
        {
            if (FilterWindow == null || FilterWindow.IsActive == false)
            {
                FilterWindow = new TreeViewFilter();
            }

            if (FilterWindow.Visibility == Visibility.Hidden)
            {
                FilterWindow.Visibility = Visibility.Visible;
            }
            else
            {
                FilterWindow.ShowDialog();
            }
        }

        public ObservableCollection<Patient> ApplyFilter()
        {
            ObservableCollection<Patient> dataset = new ObservableCollection<Patient>();
            ImageRepository repo = new ImageRepository();
            
            dataset = repo.generateCustomTreeView(OverallFilter, ShowNames);

            if (dataset != null)
            {
                return dataset;
            }
            else
            {
                return new ObservableCollection<Patient>();
            }
        }

        public void PrepareParameters()
        {
            OverallFilter = new Filter();

            OverallFilter.PatientID = PatientID;
            OverallFilter.Modality = Modality;
            OverallFilter.Batch = Batch;

            string date = "01/01/1753";
            DateTime minDate = Convert.ToDateTime(date);   

            if (DateFrom < minDate)
            {
                OverallFilter.AcquisitionDateFrom = minDate;
            }
            else OverallFilter.AcquisitionDateFrom = DateFrom;

            if (DateTo > DateTime.Today)
            {
                OverallFilter.AcquisitionDateTo = DateTime.Today.AddDays(1);
            }
            else OverallFilter.AcquisitionDateTo = DateTo.AddDays(1);

            if (IsFemale == true)
            {
                OverallFilter.Gender = "F";
            }
            else if (IsMale == true)
            {
                OverallFilter.Gender = "M";
            }
            else
            {
                OverallFilter.Gender = String.Empty;
            }
        }

        public void HideDialog()
        {
            if (FilterWindow != null)
            {
                FilterWindow.Hide();
            }
        }
        #endregion
    }
}
