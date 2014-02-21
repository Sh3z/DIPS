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
    public class TreeViewPatientViewModel : TreeViewItemViewModel
    {
        private readonly Patient _patient;

        public TreeViewPatientViewModel(Patient patient) : base(null, true)
        {
            _patient = patient;
        }

        public string PatientID
        {
            get { return _patient.patientID; }
        }

        public string PatientName
        {
            get { return _patient.patientName; }
        }

        protected override void LoadChildren()
        {
            ImageRepository imgRepository = new ImageRepository();
            ObservableCollection<Patient> listofPatients = imgRepository.generateTreeView();
            
                foreach (ImageDataset imgDs in _patient.dataSet)
                {
                    base.Children.Add(new TreeViewImageDatasetViewModel(imgDs, this));
                }
               
        }
    }
}
