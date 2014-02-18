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

        public ViewExistingDatasetViewModel()
        {
            
        }

        public void GetPatientsForTreeview()
        {
            ImageRepository repo = new ImageRepository();
            PatientsList = repo.generateTreeView();
        }

    }
}
