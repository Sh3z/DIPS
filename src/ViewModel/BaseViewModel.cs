using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using DIPS.ViewModel.UserInterfaceVM;

namespace DIPS.ViewModel
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public static BaseViewModel ViewModel { get; set; }
        
        public static Frame OverallFrame { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        readonly public static ViewExistingDatasetViewModel _ViewExistingDatasetViewModel = new ViewExistingDatasetViewModel();
        readonly public static LoadNewDsStep1ViewModel _LoadNewDsStep1ViewModel = new LoadNewDsStep1ViewModel();
        readonly public static CreateAlgorithmViewModel _CreateAlgorithmViewModel = new CreateAlgorithmViewModel();
        readonly public static LoadNewDsStep2ViewModel _LoadNewDsStep2ViewModel = new LoadNewDsStep2ViewModel();
        readonly public static LoadNewDsStep3ViewModel _LoadNewDsStep3ViewModel = new LoadNewDsStep3ViewModel();

        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
