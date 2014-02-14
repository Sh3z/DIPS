using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DIPS.Processor.Client;

namespace DIPS.ViewModel.UserInterfaceVM
{
    public class MainNaviViewModel : Window
    {
        public MainNaviViewModel()
        {
            //InitializeComponent();
            _main = new MainViewModel();
            //contentFrame.Navigate( _main );
        }


        /// <summary>
        /// Gets or sets the <see cref="IProcessingService"/> connected to
        /// the active DIPS processor.
        /// </summary>
        
        public IProcessingService Service
        {
            get
            {
                return _main.Service;
            }
            set
            {
                _main.Service = value;
            }
        }



        /// <summary>
        /// Contains a reference to the root page view.
        /// </summary>
        private MainViewModel _main;
    }
}
