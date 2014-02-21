using DIPS.Processor.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.ViewModel.UserInterfaceVM.JobTracking
{
    /// <summary>
    /// Represents a single ongoing job within the processor.
    /// </summary>
    public class JobViewModel : ViewModel, IDisposable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JobViewModel"/> class.
        /// </summary>
        /// <param name="job">The <see cref="IJobTicket"/> representing the job
        /// presented by this <see cref="JobViewModel"/>.</param>
        /// <exception cref="ArgumentNullException">job is null</exception>
        public JobViewModel( IJobTicket job )
        {
            if( job == null )
            {
                throw new ArgumentNullException( "job" );
            }

            _status = string.Empty;
            _longStatus = string.Empty;
            _isRunning = false;
            Ticket = job;
            Ticket.JobCancelled += _onJobCancelled;
            Ticket.JobCompleted += _onJobComplete;
            Ticket.JobError += _onJobError;
            Ticket.JobStarted += _onJobStarted;
            _updateFromState();
        }


        void IDisposable.Dispose()
        {
            Ticket.JobCancelled -= _onJobCancelled;
            Ticket.JobCompleted -= _onJobComplete;
            Ticket.JobError -= _onJobError;
            Ticket.JobStarted -= _onJobStarted;
            Ticket = null;
        }


        /// <summary>
        /// Gets the ticket representing the job within this view-model.
        /// </summary>
        public IJobTicket Ticket
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the current status of this job represented by this
        /// <see cref="JobViewModel"/> in a presentable form.
        /// </summary>
        public string Status
        {
            get
            {
                return _status;
            }
            private set
            {
                _status = value;
                OnPropertyChanged();
            }
        }
        [DebuggerBrowsable( DebuggerBrowsableState.Never )]
        private string _status;

        /// <summary>
        /// Gets further information abut the current state of the job.
        /// </summary>
        public string LongStatus
        {
            get
            {
                return _longStatus;
            }
            private set
            {
                _longStatus = value;
                OnPropertyChanged();
            }
        }
        [DebuggerBrowsable( DebuggerBrowsableState.Never )]
        private string _longStatus;

        /// <summary>
        /// Gets whether the job represented by this view-model is
        /// still running
        /// </summary>
        public bool IsRunning
        {
            get
            {
                return _isRunning;
            }
            private set
            {
                _isRunning = value;
                OnPropertyChanged();
            }
        }
        [DebuggerBrowsable( DebuggerBrowsableState.Never )]
        private bool _isRunning;

        /// <summary>
        /// Gets whether the job represented by this <see cref="JobViewModel"/>
        /// has been cancelled.
        /// </summary>
        public bool IsCancelled
        {
            get
            {
                return _isCancelled;
            }
            private set
            {
                _isCancelled = value;
                OnPropertyChanged();
            }
        }
        [DebuggerBrowsable( DebuggerBrowsableState.Never )]
        private bool _isCancelled;


        /// <summary>
        /// Occurs when the job begins.
        /// </summary>
        /// <param name="sender">N/A</param>
        /// <param name="e">N/A</param>
        private void _onJobStarted( object sender, EventArgs e )
        {
            IsRunning = true;
            _updateFromState();
        }

        /// <summary>
        /// Occurs when the job encounters an error
        /// </summary>
        /// <param name="sender">N/A</param>
        /// <param name="e">N/A</param>
        private void _onJobError( object sender, EventArgs e )
        {
            IsRunning = false;
            _updateFromState();
        }

        /// <summary>
        /// Occurs when the job is complete
        /// </summary>
        /// <param name="sender">N/A</param>
        /// <param name="e">N/A</param>
        private void _onJobComplete( object sender, EventArgs e )
        {
            IsRunning = false;
            _updateFromState();
        }

        /// <summary>
        /// Occurs when the job is cancelled
        /// </summary>
        /// <param name="sender">N/A</param>
        /// <param name="e">N/A</param>
        private void _onJobCancelled( object sender, EventArgs e )
        {
            IsRunning = false;
            IsCancelled = true;
            _updateFromState();
        }

        /// <summary>
        /// Updates the state information using the state on the ticket
        /// </summary>
        private void _updateFromState()
        {
            switch( Ticket.State )
            {
                case JobState.InQueue:
                    Status = StatusStrings.Queued;
                    LongStatus = StatusStrings.QueuedDesc;
                    break;

                case JobState.Running:
                    Status = StatusStrings.Running;
                    LongStatus = StatusStrings.RunningDesc;
                    break;

                case JobState.Cancelled:
                    Status = StatusStrings.Cancelled;
                    LongStatus = StatusStrings.CancelleDesc;
                    break;

                case JobState.Complete:
                    Status = StatusStrings.Complete;
                    LongStatus = StatusStrings.CompleteDesc;
                    break;

                case JobState.Error:
                    Status = StatusStrings.Error;
                    Exception err = Ticket.Result.Exception;
                    string details = string.Format( StatusStrings.ErrorDesc, err.Message );
                    LongStatus = details;
                    break;
            }
        }
    }
}
