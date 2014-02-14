using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DIPS.Processor.Client;

namespace DIPS.ViewModel.UserInterfaceVM
{
    public class MainViewModel
    {
        public ICommand ViewExistingDataSetCommand { get; private set; }
    
        public IProcessingService Service { get; set; }
    }

    }
