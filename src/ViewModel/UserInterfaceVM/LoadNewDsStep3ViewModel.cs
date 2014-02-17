using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DIPS.Database.Objects;

namespace DIPS.ViewModel.UserInterfaceVM
{
    public class LoadNewDsStep3ViewModel : BaseViewModel
    {
        public ObservableCollection<FileInfo> ListOfFiles { get; set; }
        public ObservableCollection<Technique> ListofTechniques { get; set; }
    }
}
