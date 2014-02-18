using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database;
using DIPS.Database.Objects;

namespace DIPS.ViewModel.UserInterfaceVM
{
    public class TreeViewGroupPatientsViewModel
    {
        private readonly ReadOnlyCollection<TreeViewPatientViewModel> _patients;

        public TreeViewGroupPatientsViewModel(ObservableCollection<Patient> patients)
        {
           _patients = new ReadOnlyCollection<TreeViewPatientViewModel>(
               (from patient in patients
                    select new TreeViewPatientViewModel(patient))
                    .ToList());
        }

        public ReadOnlyCollection<TreeViewPatientViewModel> Patients
        {
            get { return _patients; }
        }
    }
}
