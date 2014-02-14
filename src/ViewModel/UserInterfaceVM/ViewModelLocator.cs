using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.ViewModel.UserInterfaceVM
{
    public class ViewModelLocator
    {
        private static MainViewModel _main;

        public MainViewModel Main
        {
            get { return _main; }
        }

        public ViewModelLocator()
        {
            _main = new MainViewModel();
        }
    }
}
