using DIPS.Util.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DIPS.Util.Remoting
{
    /// <summary>
    /// Represents a generic container for <see cref="EventSink"/>s.
    /// </summary>
    /// <typeparam name="T">The derived <see cref="EventSink"/> type
    /// this <see cref="ISinkContainer"/> contains.</typeparam>
    public class EventSinkContainer<T> : ISinkContainer<T>
        where T : EventSink
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventSinkContainer"/>
        /// class.
        /// </summary>
        public EventSinkContainer()
        {
            _sinks = new List<T>();
        }


        void IDisposable.Dispose()
        {
            _sinks.ForEach( Remove );
        }


        /// <summary>
        /// Adds a new sink to this <see cref="ISinkContainer"/>.
        /// </summary>
        /// <param name="sink">The <see cref="EventSink"/> to add.</param>
        public void Add( T sink )
        {
            lock( _sinks )
            {
                if( sink != null )
                {
                    _sinks.Add( sink );
                }
            }
        }

        /// <summary>
        /// Removes an existing <see cref="EventSink"/> from this
        /// <see cref="ISinkContainer"/>.
        /// </summary>
        /// <param name="sink">The <see cref="EventSink"/> to remove.</param>
        public void Remove( T sink )
        {
            lock( _sinks )
            {
                if( sink != null )
                {
                    _sinks.Add( sink );
                }
            }
        }

        /// <summary>
        /// Fires an event across all attached sinks on the calling thread.
        /// The source of the event is this <see cref="EventSinkContainer"/>.
        /// </summary>
        /// <param name="eventName">The name of the event to invoke.</param>
        /// <param name="e">The information associated with the event
        /// firing.</param>
        public void FireSync( string eventName, EventArgs e )
        {
            FireSync( eventName, this, e );
        }

        /// <summary>
        /// Fires an event across all attached sinks on the calling thread.
        /// </summary>
        /// <param name="eventName">The name of the event to invoke.</param>
        /// <param name="sender">The source of the event firing.</param>
        /// <param name="e">The information associated with the event
        /// firing.</param>
        /// <exception cref="ArgumentException">eventName is null or
        /// empty.</exception>
        /// <exception cref="ArgumentNullException">sender is null.</exception>
        /// <exception cref="RemotingException">an error occurs while invoking
        /// the event.</exception>
        public void FireSync( string eventName, object sender, EventArgs e )
        {
            if( string.IsNullOrEmpty( eventName ) )
            {
                throw new ArgumentException( "eventName" );
            }

            if( sender == null )
            {
                throw new ArgumentNullException( "sender" );
            }

            e = e ?? EventArgs.Empty;
            _invokeOnSinks( eventName, sender, e );
        }


        /// <summary>
        /// Fires an event across all attached sinks on a new thread.
        /// The source of the event is this <see cref="EventSinkContainer"/>.
        /// </summary>
        /// <param name="eventName">The name of the event to invoke.</param>
        /// <param name="e">The information associated with the event
        /// firing.</param>
        /// <exception cref="ArgumentException">eventName is null or
        /// empty.</exception>
        /// <exception cref="ArgumentNullException">sender is null.</exception>
        /// <exception cref="RemotingException">an error occurs while invoking
        /// the event.</exception>
        public void FireAsync( string eventName, EventArgs e )
        {
            FireAsync( eventName, this, e );
        }

        /// <summary>
        /// Fires an event across all attached sinks on a new thread.
        /// </summary>
        /// <param name="eventName">The name of the event to invoke.</param>
        /// <param name="sender">The source of the event firing.</param>
        /// <param name="e">The information associated with the event
        /// firing.</param>
        /// <exception cref="ArgumentException">eventName is null or
        /// empty.</exception>
        /// <exception cref="ArgumentNullException">sender is null.</exception>
        /// <exception cref="RemotingException">an error occurs while invoking
        /// the event.</exception>
        public void FireAsync( string eventName, object sender, EventArgs e )
        {
            ThreadStart s = () => _invokeAsync( eventName, sender, e );
            Thread t = new Thread( s );
            t.Start();
        }


        /// <summary>
        /// Begins the threaded invocation
        /// </summary>
        /// <param name="eventName">The event to invoke</param>
        /// <param name="sender">The source of the event</param>
        /// <param name="e">The event information</param>
        private void _invokeAsync( string eventName, object sender, EventArgs e )
        {
            FireSync( eventName, sender, e );
        }

        /// <summary>
        /// Invokes an event against all sinks
        /// </summary>
        /// <param name="eventName">The name of the event to invoke</param>
        /// <param name="sender">The source of the event</param>
        /// <param name="e">The event information</param>
        private void _invokeOnSinks( string eventName, object sender, EventArgs e )
        {
            lock( _sinks )
            {
                foreach( T sink in _sinks )
                {
                    _tryInvokeOnSink( sink, eventName, sender, e );
                }
            }
        }

        /// <summary>
        /// Attempts to invoke an event on a single sink, trapping any exceptions
        /// </summary>
        /// <param name="sink">The sink to invoke the event on</param>
        /// <param name="eventName">The name of the event to invoke</param>
        /// <param name="sender">The source of the event</param>
        /// <param name="e">The event information</param>
        private void _tryInvokeOnSink( EventSink sink, string eventName, object sender, EventArgs e )
        {
            try
            {
                sink.InvokeEvent( eventName, sender, e );
            }
            catch( Exception ex )
            {
                // Todo logging
            }
        }


        /// <summary>
        /// Contains the internal sink collection.
        /// </summary>
        private ICollection<T> _sinks;
    }
}
