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

        public string PatientIdentifier
        {
            get { return _patient.patientIdentifier; }
        }

        protected override void LoadChildren()
        {
            ImageRepository imgRepository = new ImageRepository();
            ObservableCollection<Patient> listofPatients = imgRepository.generateTreeView(true);
            
                foreach (ImageDataset imgDs in _patient.dataSet)
                {
                    base.Children.Add(new TreeViewImageDatasetViewModel(imgDs, this));
                }
               
        }
    }
}
