using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Objects;
using Database.Unity;
using DIPS.UI.Pages;

namespace DIPS.UI.Unity.Implementations
{
    public class FilterTreeView : IFilterTreeView
    {
        public string PatientID { set; private get; }
        public DateTime DateFrom { set; private get; }
        public DateTime DateTo { set; private get; }
        public bool IsMale { set; private get; }
        public bool IsFemale { set; private get; }
        public Filter OverallFilter { set; private get; }

        public void OpenDialog()
        {
            TreeViewFilter tvf = new TreeViewFilter();
            tvf.ShowDialog();
        }

        public void ApplyFilter()
        {
            throw new NotImplementedException();
        }
    }
}
