using System.Windows;
using System.Windows.Controls;
using DIPS.Util.Extensions;
using DIPS.ViewModel.Commands;
using DIPS.ViewModel.UserInterfaceVM.JobTracking;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;

namespace DIPS.ViewModel.UserInterfaceVM
{
    /// <summary>
    /// Represents the view-model of the queueing component.
    /// </summary>
    public class QueueViewModel : BaseViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QueueViewModel"/>
        /// class.
        /// </summary>
        /// <param name="tracker">The <see cref="IJobTracker"/> to
        /// present within the queue.</param>
        /// <exception cref="ArgumentNullException">tracker is
        /// null.</exception>
        public QueueViewModel(IJobTracker tracker)
        {
            if (tracker == null)
            {
                throw new ArgumentNullException("tracker");
            }

            CancelCommand = new CancelJobCommand();
            _tracker = tracker;
            _tracker.PropertyChanged += _trackerPropertyChanged;
            _tracker.Pending.CollectionChanged += _pendingChanged;
            _tracker.Finished.CollectionChanged += _finishedChanged;
            Entries = new ObservableCollection<JobViewModel>();
            IsPresentingQueued = false;
            CurrentJob = _tracker.Current;
        }


        /// <summary>
        /// Gets the <see cref="ICommand"/> used to cancel queued jobs.
        /// </summary>
        public Command CancelCommand { get; private set; }

        /// <summary>
        /// Gets or sets whether the currently queued elements are
        /// being displayed.
        /// </summary>
        public bool IsPresentingQueued
        {
            get { return _isPresentingQueued; }
            set
            {
                _isPresentingQueued = value;
                _updateDisplayedEntries();
                OnPropertyChanged();
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)] private bool _isPresentingQueued;

        /// <summary>
        /// Gets the set of currently displayed entries.
        /// </summary>
        public ObservableCollection<JobViewModel> Entries { get; private set; }

        public ComboBoxItem PostProcessAction { get; set; }

        /// <summary>
        /// Gets or sets the selected job.
        /// </summary>
        public JobViewModel SelectedJob
        {
            get { return _selectedJob; }
            set
            {
                _selectedJob = value;
                CancelCommand.ExecutableStateChanged();
                OnPropertyChanged();
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)] private JobViewModel _selectedJob;

        /// <summary>
        /// Gets or sets the currently executing job.
        /// </summary>
        public JobViewModel CurrentJob
        {
            get { return _currentJob; }
            set
            {
                _currentJob = value;
                OnPropertyChanged();
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)] private JobViewModel _currentJob;


        /// <summary>
        /// Updates the elements of the Entries collection based on
        /// the type of entries we are currently displaying.
        /// </summary>
        private void _updateDisplayedEntries()
        {
            Entries.Clear();
            if (IsPresentingQueued)
            {
                _tracker.Pending.ForEach(Entries.Add);
            }
            else
            {
                _tracker.Finished.ForEach(Entries.Add);
            }
        }

        private void _pendingChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (IsPresentingQueued)
            {
                _updateEntries(e);
            }
        }

        private void _finishedChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (IsPresentingQueued == false)
            {
                _updateEntries(e);
            }

            _trackerPendingCheckAndAction();
        }

        private void _updateEntries(NotifyCollectionChangedEventArgs e)
        {
            Action<JobViewModel> theAction = null;
            IList items = null;
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                items = e.NewItems;
                theAction = Entries.Add;
            }
            else
            {
                items = e.OldItems;
                theAction = x => Entries.Remove(x);
            }

            foreach (object item in items)
            {
                theAction((JobViewModel) item);
            }
        }

        private void _trackerPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            string lowerName = e.PropertyName.ToLower();
            if (lowerName == "current")
            {
                CurrentJob = _tracker.Current;
            }
        }

        private void _trackerPendingCheckAndAction()
        {
            if (_tracker != null)
            {
                if (_tracker.Pending.Count == 0 && PostProcessAction != null)
            {
                    if (PostProcessAction.Content.ToString() == "Shut down")
                {
                    Process.Start("shutdown", "/s /t 0");
                }

                else if (PostProcessAction.Content.ToString() == "Sleep")
                {
                    // Hibernate
                    Process.Start("shutdown", "/h /f");
                }
            }
           }
            
        }
        /// <summary>
        /// Contains the tracking instance we provide presentation logic
        /// against.
        /// </summary>
        private IJobTracker _tracker;
    }
}
