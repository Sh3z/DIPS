using DIPS.Processor.Client;
using DIPS.Processor.Client.Eventing;
using DIPS.Processor.Client.Sinks;
using DIPS.Util.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

            TimeEnqueued = DateTime.Now;
            Identifier = job.Request.Identifier;
            Inputs = new ObservableCollection<InputViewModel>();
            job.Request.Job
                .GetInputs()
                .ForEach( x => Inputs.Add( new InputViewModel( x ) ) );
            _status = string.Empty;
            _longStatus = string.Empty;
            _isRunning = false;
            _sink = new TicketSink();
            _sink.JobCancelled += _onJobCancelled;
            _sink.JobCompleted += _onJobComplete;
            _sink.JobError += _onJobError;
            _sink.JobStarted += _onJobStarted;
            _sink.InputProcessed += _inputProcessed;
            Ticket = job;
            Ticket.Sinks.Add( _sink );
            _updateFromState();
        }


        void IDisposable.Dispose()
        {
            if( Ticket != null )
            {
                Ticket.Sinks.Remove( _sink );
                Ticket = null;
            }

            _sink.JobCancelled -= _onJobCancelled;
            _sink.JobCompleted -= _onJobComplete;
            _sink.JobError -= _onJobError;
            _sink.JobStarted -= _onJobStarted;
            _sink = null;
        }


        /// <summary>
        /// Occurs when the job represented by this <see cref="JobViewModel"/>
        /// has begun.
        /// </summary>
        public event EventHandler JobStarted;


        /// <summary>
        /// Occurs when the job represented by this <see cref="JobViewModel"/>
        /// has finished in full.
        /// </summary>
        public event EventHandler JobFinished;


        /// <summary>
        /// Gets the ticket representing the job within this view-model.
        /// </summary>
        public IJobTicket Ticket
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the identifier given to the job.
        /// </summary>
        public object Identifier
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the <see cref="DateTime"/> when this <see cref="JobViewModel"/>
        /// was enqueued.
        /// </summary>
        public DateTime TimeEnqueued
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the <see cref="DateTime"/> the job began running.
        /// </summary>
        public DateTime TimeBegan
        {
            get
            {
                return _timeBegan;
            }
            set
            {
                _timeBegan = value;
                OnPropertyChanged();
            }
        }
        [DebuggerBrowsable( DebuggerBrowsableState.Never )]
        private DateTime _timeBegan;

        /// <summary>
        /// Gets the <see cref="DateTime"/> when this <see cref="JobViewModel"/>
        /// finished processing.
        /// </summary>
        public DateTime TimeFinished
        {
            get
            {
                return _timeFinished;
            }
            set
            {
                _timeFinished = value;
                JobDuration = value - TimeBegan;
                OnPropertyChanged();
            }
        }
        [DebuggerBrowsable( DebuggerBrowsableState.Never )]
        private DateTime _timeFinished;

        /// <summary>
        /// Gets the <see cref="TimeSpan"/> taken by the job to complete.
        /// </summary>
        public TimeSpan JobDuration
        {
            get
            {
                return _duration;
            }
            private set
            {
                _duration = value;
                OnPropertyChanged();
            }
        }
        [DebuggerBrowsable( DebuggerBrowsableState.Never )]
        private TimeSpan _duration;

        /// <summary>
        /// Gets the collection of <see cref="InputViewModel"/>s associated
        /// with the current job.
        /// </summary>
        public ObservableCollection<InputViewModel> Inputs
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the selected <see cref="InputViewModel"/>.
        /// </summary>
        public InputViewModel SelectedInput
        {
            get
            {
                return _selectedInput;
            }
            set
            {
                _selectedInput = value;
                OnPropertyChanged();
            }
        }
        [DebuggerBrowsable( DebuggerBrowsableState.Never )]
        private InputViewModel _selectedInput;

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
        /// Gets or sets the number of processed inputs.
        /// </summary>
        public int InputsProcessed
        {
            get
            {
                return _inputsProcessed;
            }
            set
            {
                _inputsProcessed = value;
                OnPropertyChanged();
                OnPropertyChanged( "EstimatedCompletion" );
            }
        }
        [DebuggerBrowsable( DebuggerBrowsableState.Never )]
        private int _inputsProcessed;

        /// <summary>
        /// Gets an estimate of the % completion of the job.
        /// </summary>
        public decimal EstimatedCompletion
        {
            get
            {
                decimal count = (decimal)Inputs.Count;
                if( count == 0 )
                {
                    count++;
                }

                return ( (decimal)( (decimal)InputsProcessed / count ) ) * 100;
            }
        }


        /// <summary>
        /// Occurs when the job begins.
        /// </summary>
        /// <param name="sender">N/A</param>
        /// <param name="e">N/A</param>
        private void _onJobStarted( object sender, EventArgs e )
        {
            TimeBegan = DateTime.Now;
            IsRunning = true;
            _updateFromState();
            if( JobStarted != null )
            {
                JobStarted( this, EventArgs.Empty );
            }
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
            _fireIfComplete();
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
            _fireIfComplete();
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
            _fireIfComplete();
        }

        /// <summary>
        /// Occurs when an input has been processed
        /// </summary>
        /// <param name="sender">N/A</param>
        /// <param name="e">Event information</param>
        private void _inputProcessed( object sender, InputProcessedArgs e )
        {
            // Provide the info to the relevant input
            InputsProcessed++;
            var inputWithID = ( from input in Inputs
                                where input.Identifier == e.Identifier
                                select input ).FirstOrDefault();
            if( inputWithID != null )
            {
                inputWithID.Output = e.Image;
                inputWithID.IsProcessed = true;
            }
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
                    TimeFinished = DateTime.Now;
                    break;

                case JobState.Complete:
                    Status = StatusStrings.Complete;
                    LongStatus = StatusStrings.CompleteDesc;
                    TimeFinished = DateTime.Now;
                    break;

                case JobState.Error:
                    Status = StatusStrings.Error;
                    Exception err = Ticket.Result.Exception;
                    string details = string.Format( StatusStrings.ErrorDesc, err.Message );
                    LongStatus = details;
                    TimeFinished = DateTime.Now;
                    break;
            }
        }

        /// <summary>
        /// Fires the JobFinished event if the job has produced a result
        /// </summary>
        private void _fireIfComplete()
        {
            if( Ticket.Result != null && JobFinished != null )
            {
                JobFinished( this, EventArgs.Empty );
            }
        }


        /// <summary>
        /// Contains the event sink used by the ticket.
        /// </summary>
        private TicketSink _sink;
    }
}
