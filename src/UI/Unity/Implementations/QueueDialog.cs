using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DIPS.UI.Pages;
using DIPS.Unity;
using DIPS.ViewModel.UserInterfaceVM.JobTracking;

namespace DIPS.UI.Unity.Implementations
{
    public class QueueDialog : IQueueDialog
    {
        private object _current;

        public object Current
        {
            private get { return _current; }
            set {_current = value; }
        }

        public ObservableCollection<object> Pending { set; private get; }

        public ObservableCollection<object> Finished { set; private get; }

        private Queue QueueDialogProperty { set; get; }

        public void ShowDialog()
        {
            if (QueueDialogProperty == null || QueueDialogProperty.IsActive == false)
            {
                QueueDialogProperty = new Queue();
            }

            if (QueueDialogProperty.Visibility == Visibility.Hidden)
            {
                QueueDialogProperty.Visibility = Visibility.Visible;
            }
            else
            {
                QueueDialogProperty.ShowDialog();
            }
        }

        public void HideDialog()
        {
            if (QueueDialogProperty != null)
            {
                QueueDialogProperty.Hide();
            }
        }
    }
}
