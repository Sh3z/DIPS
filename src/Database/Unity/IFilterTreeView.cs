using System;
using System.Collections.ObjectModel;
using Database.Objects;
using DIPS.Database.Objects;

namespace Database.Unity
{
    public interface IFilterTreeView
    {
        String PatientID { set; }
        String Batch { set; }
        String Modality { set; }
        DateTime DateFrom { set; }
        DateTime DateTo { set; }
        Boolean IsMale { set; }
        Boolean IsFemale { set; }
        Boolean ShowNames { set; }
        Filter OverallFilter { set; }

        void OpenDialog();
        ObservableCollection<Patient> ApplyFilter();
        void PrepareParameters();
        void HideDialog();
    }

    }
