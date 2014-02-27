using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DIPS.ViewModel.UserInterfaceVM.JobTracking;
using Microsoft.Practices.Unity;

namespace DIPS.ViewModel.UserInterfaceVM
{
    public class QueueViewModel : BaseViewModel
    {
        #region Properties
        private IUnityContainer _container;

        public IUnityContainer Container
        {
            get { return _container; }
            set
            {
                _container = value; 
                OnPropertyChanged();
            }
        }

        private ObservableCollection<JobViewModel> _pending;

        public ObservableCollection<JobViewModel> Pending
        {
            get { return _pending; }
            set
            {
                _pending = value;
                OnPropertyChanged();
            }
        }

        private JobViewModel _inProgress;

        public JobViewModel InProgress
        {
            get { return _inProgress; }
            set
            {
                _inProgress = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<JobViewModel> _complete;

        public ObservableCollection<JobViewModel> Complete
        {
            get { return _complete; }
            set
            {
                _complete = value;
                OnPropertyChanged();
            }
        } 
        #endregion


        #region Constructor
        public QueueViewModel()
        {
            IJobTracker tracker = Container.Resolve<IJobTracker>();
            Complete = tracker.Finished;
            InProgress = tracker.Current;
            Pending = tracker.Pending;
        } 
        #endregion

    }
}
