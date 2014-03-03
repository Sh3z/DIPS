using DIPS.Util.Extensions;
using DIPS.ViewModel.UserInterfaceVM.JobTracking;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;

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
        public QueueViewModel( IJobTracker tracker )
        {
            if( tracker == null )
            {
                throw new ArgumentNullException( "tracker" );
            }

            _tracker = tracker;
            _tracker.Pending.CollectionChanged += _pendingChanged;
            _tracker.Finished.CollectionChanged += _finishedChanged;
            Entries = new ObservableCollection<JobViewModel>();
            IsPresentingQueued = false;
        }


        /// <summary>
        /// Gets or sets whether the currently queued elements are
        /// being displayed.
        /// </summary>
        public bool IsPresentingQueued
        {
            get
            {
                return _isPresentingQueued;
            }
            set
            {
                _isPresentingQueued = value;
                _updateDisplayedEntries();
                OnPropertyChanged();
            }
        }
        [DebuggerBrowsable( DebuggerBrowsableState.Never )]
        private bool _isPresentingQueued;

        /// <summary>
        /// Gets the set of currently displayed entries.
        /// </summary>
        public ObservableCollection<JobViewModel> Entries
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the selected job.
        /// </summary>
        public JobViewModel SelectedJob
        {
            get
            {
                return _selectedJob;
            }
            set
            {
                _selectedJob = value;
                OnPropertyChanged();
            }
        }
        [DebuggerBrowsable( DebuggerBrowsableState.Never )]
        private JobViewModel _selectedJob;


        /// <summary>
        /// Updates the elements of the Entries collection based on
        /// the type of entries we are currently displaying.
        /// </summary>
        private void _updateDisplayedEntries()
        {
            Entries.Clear();
            if( IsPresentingQueued )
            {
                _tracker.Pending.ForEach( Entries.Add );
            }
            else
            {
                _tracker.Finished.ForEach( Entries.Add );
            }
        }

        private void _pendingChanged( object sender, NotifyCollectionChangedEventArgs e )
        {
            if( IsPresentingQueued )
            {
                _updateEntries( e );
            }
        }

        private void _finishedChanged( object sender, NotifyCollectionChangedEventArgs e )
        {
            if( IsPresentingQueued == false )
            {
                _updateEntries( e );
            }
        }

        private void _updateEntries( NotifyCollectionChangedEventArgs e )
        {
            Action<JobViewModel> theAction = null;
            IList items = null;
            if( e.Action == NotifyCollectionChangedAction.Add )
            {
                items = e.NewItems;
                theAction = Entries.Add;
            }
            else
            {
                items = e.OldItems;
                theAction = x => Entries.Remove( x );
            }

            foreach( object item in items )
            {
                theAction( (JobViewModel)item );
            }
        }


        /// <summary>
        /// Contains the tracking instance we provide presentation logic
        /// against.
        /// </summary>
        private IJobTracker _tracker;
    }
}
