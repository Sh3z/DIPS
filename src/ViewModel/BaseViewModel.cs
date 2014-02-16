using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DIPS.ViewModel
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public static BaseViewModel ViewModel { get; set; }
        
        public static Frame OverallFrame { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
