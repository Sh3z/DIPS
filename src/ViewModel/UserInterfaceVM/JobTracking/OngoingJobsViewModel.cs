using DIPS.Processor.Client;
using DIPS.Unity;
using DIPS.Util.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Practices.Unity;
using System.Threading;

namespace DIPS.ViewModel.UserInterfaceVM.JobTracking
{
    /// <summary>
    /// Represents the presentation logic behind managing a set of running jobs.
    /// </summary>
    public class OngoingJobsViewModel : ViewModel, IJobTracker
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OngoingJobsViewModel"/>
        /// class.
        /// </summary>
        public OngoingJobsViewModel()
        {
            Pending = new ObservableCollection<JobViewModel>();
            Finished = new ObservableCollection<JobViewModel>();
            CancelJob = new CancelJobCommand();
            _context = GlobalContainer.Instance.Container.Resolve<IUIContext>();
        }


        void IDisposable.Dispose()
        {
            _disposeJobs();
            Pending.Clear();
            Finished.Clear();
        }


        /// <summary>
        /// Gets a collection of <see cref="JobViewModel"/>s that are
        /// waiting to be ran.
        /// </summary>
        public ObservableCollection<JobViewModel> Pending
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets a collection of <see cref="JobViewModel"/>s that have
        /// finished (either in error or in full)
        /// </summary>
        public ObservableCollection<JobViewModel> Finished
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the currently executing <see cref="JobViewModel"/>.
        /// </summary>
        public JobViewModel Current
        {
            get
            {
                return _current;
            }
            private set
            {
                _current = value;
                OnPropertyChanged();
            }
        }
        [DebuggerBrowsable( DebuggerBrowsableState.Never )]
        private JobViewModel _current;

        /// <summary>
        /// Adds a new job to the tracker.
        /// </summary>
        /// <param name="ticket">The <see cref="IJobTicket"/> to begin
        /// tracking.</param>
        public void Add( IJobTicket ticket )
        {
            JobViewModel vm = new JobViewModel( ticket );
            switch( ticket.State )
            {
                case JobState.InQueue:
                    vm.JobStarted += _jobStarted;
                    vm.JobFinished += _jobFinished;
                    _safeViewModelAction( Pending.Add, vm );
                    break;

                case JobState.Running:
                    vm.JobStarted += _jobStarted;
                    vm.JobFinished += _jobFinished;
                    Current = vm;
                    break;

                default:
                    _safeViewModelAction( Finished.Add, vm );
                    break;
            }
        }

        /// <summary>
        /// Gets an <see cref="ICommand"/> used to cancel <see cref="JobViewModel"/>s.
        /// </summary>
        public ICommand CancelJob
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the <see cref="IJobResultsHandler"/> called
        /// when the results of a pocessing job are complete.
        /// </summary>
        public IJobResultsHandler Handler
        {
            get;
            set;
        }


        /// <summary>
        /// Occurs when a contained JobViewModel has begun
        /// </summary>
        /// <param name="sender">The JobViewModel</param>
        /// <param name="e">N/A</param>
        private void _jobStarted( object sender, EventArgs e )
        {
            Current = (JobViewModel)sender;
            _safeViewModelAction( x => Pending.Remove( x ), Current );
        }

        /// <summary>
        /// Occurs when a contained JobViewModel has finished
        /// </summary>
        /// <param name="sender">The JobViewModel</param>
        /// <param name="e">N/A</param>
        private void _jobFinished( object sender, EventArgs e )
        {
            Current = null;
            _safeViewModelAction( x => Pending.Remove( x ), (JobViewModel)sender );
            if( Handler != null )
            {
                JobViewModel vm = (JobViewModel)sender;
                Handler.HandleResults( vm.Ticket );
            }
        }

        /// <summary>
        /// Disposes of the elements within the jobs collections
        /// </summary>
        private void _disposeJobs()
        {
            foreach( JobViewModel job in Pending.Union( Finished ) )
            {
                job.JobFinished -= _jobFinished;
                job.JobStarted -= _jobStarted;
                ( (IDisposable)job ).Dispose();
            }
        }

        /// <summary>
        /// Performs an action with a view model on the UI thread
        /// </summary>
        /// <param name="action">The action to perform</param>
        /// <param name="item">The view model to perform the action with</param>
        private void _safeViewModelAction( Action<JobViewModel> action, JobViewModel item )
        {
            SendOrPostCallback c = x => action( item );
            _context.Context.Send( c, null );
        }


        /// <summary>
        /// Maintains a reference to the current UI context
        /// </summary>
        private IUIContext _context;
    }
}
