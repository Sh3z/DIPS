using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Objects;

namespace Database.Unity
{
    public interface IFilterTreeView
    {
        String PatientID { set; }
        DateTime DateFrom { set; }
        DateTime DateTo { set; }
        Boolean IsMale { set; }
        Boolean IsFemale { set; }
        Filter OverallFilter { set; }

        void OpenDialog();
        void ApplyFilter();
    }

    }
