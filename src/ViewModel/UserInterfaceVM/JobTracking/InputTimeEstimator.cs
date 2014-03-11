using DIPS.Processor.Client;
using DIPS.Processor.Client.Eventing;
using DIPS.Processor.Client.Sinks;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.ViewModel.UserInterfaceVM.JobTracking
{
    /// <summary>
    /// Represents the object used to calculate a rolling average of
    /// the job time remaining.
    /// </summary>
    public class InputTimeEstimator : ViewModel, IDisposable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InputTimeEstimator"/>
        /// class.
        /// </summary>
        /// <param name="ticket">The <see cref="IJobTicket"/> used to
        /// estimate the duration of the job of.</param>
        /// <exception cref="ArgumentNullException">ticket is null</exception>
        public InputTimeEstimator( IJobTicket ticket )
        {
            if( ticket == null )
            {
                throw new ArgumentNullException( "ticket" );
            }

            _previousDurations = new List<TimeSpan>();
            _ticket = ticket;
            _sink = new TicketSink();
            _ticket.Sinks.Add( _sink );
            _sink.InputStarted += _inputStarted;
            _sink.InputProcessed += _inputProcessed;
        }

        


        void IDisposable.Dispose()
        {
            _ticket.Sinks.Remove( _sink );
        }


        /// <summary>
        /// Gets the current estimate each input will require to be processed
        /// in, or null if no estimate has been calculated.
        /// </summary>
        public TimeSpan? DurationEstimate
        {
            get
            {
                return _estimate;
            }
            private set
            {
                _estimate = value;
                OnPropertyChanged();
            }
        }
        [DebuggerBrowsable( DebuggerBrowsableState.Never )]
        private TimeSpan? _estimate;


        /// <summary>
        /// Occurs when an input begins processing
        /// </summary>
        /// <param name="sender">N/A</param>
        /// <param name="e">N/A</param>
        private void _inputStarted( object sender, EventArgs e )
        {
            _currentStart = DateTime.Now;
        }

        /// <summary>
        /// Occurs when an input is fully processed
        /// </summary>
        /// <param name="sender">N/A</param>
        /// <param name="e">Event information</param>
        private void _inputProcessed( object sender, InputProcessedArgs e )
        {
            if( _currentStart != null )
            {
                TimeSpan duration = DateTime.Now - (DateTime)_currentStart;
                _currentStart = null;
                _previousDurations.Add( duration );
                _recalculateAverage();
            }
        }

        /// <summary>
        /// Updates the current rolling average from the collection of
        /// retained values
        /// </summary>
        private void _recalculateAverage()
        {
            if( _previousDurations.Any() )
            {
                // Calculate the average number of milliseconds and use it
                // to create a new timespan
                var avgTicks = _previousDurations.Average( x => x.Ticks );
                long avg = Convert.ToInt64( avgTicks );
                DurationEstimate = new TimeSpan( avg );
            }
            else
            {
                DurationEstimate = null;
            }
        }


        private IJobTicket _ticket;
        private TicketSink _sink;
        private ICollection<TimeSpan> _previousDurations;
        private DateTime? _currentStart;
    }
}
