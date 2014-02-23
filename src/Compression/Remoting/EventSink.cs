using DIPS.Util.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Util.Remoting
{
    /// <summary>
    /// Represents a container for events to be fired by an
    /// <see cref="EventSinkContainer"/>. This class is abstract.
    /// </summary>
    [Serializable]
    public abstract class EventSink
    {
        /// <summary>
        /// Gets or sets whether this <see cref="EventSink"/> should throw
        /// an exception when attempting to invoke a delegate that throws.
        /// </summary>
        public bool ThrowOnInvocationFailure
        {
            get;
            set;
        }


        /// <summary>
        /// Invokes an event with the given name and event information. The
        /// sender of the event is this <see cref="EventSink"/>.
        /// </summary>
        /// <param name="name">The name of the event to invoke within this
        /// <see cref="EventSink"/>.</param>
        /// <param name="e">The information associated with the event
        /// firing.</param>
        /// <exception cref="ArgumentException">name is null or empty</exception>
        /// <exception cref="RemotingException">an error occurs while invoking
        /// the event</exception>
        /// <exception cref="EventSignatureException">the event is not in the correct
        /// event-style format</exception>
        public void InvokeEvent( string name, EventArgs e )
        {
            InvokeEvent( name, this, e );
        }

        /// <summary>
        /// Invokes an event with the given name and event information.
        /// </summary>
        /// <param name="name">The name of the event to invoke within this
        /// <see cref="EventSink"/>.</param>
        /// <param name="sender">The source of the event firing</param>
        /// <param name="e">The information associated with the event
        /// firing.</param>
        /// <exception cref="ArgumentException">name is null or empty</exception>
        /// <exception cref="ArgumentNullException">sender is null</exception>
        /// <exception cref="RemotingException">an error occurs while invoking
        /// the event</exception>
        /// <exception cref="EventSignatureException">the event is not in the correct
        /// event-style format</exception>
        public void InvokeEvent( string name, object sender, EventArgs e )
        {
            if( string.IsNullOrEmpty( name ) )
            {
                throw new ArgumentException( "name" );
            }

            if( sender == null )
            {
                throw new ArgumentNullException( "sender" );
            }

            e = e ?? EventArgs.Empty;
            _runInvoke( name, sender, e );
        }


        /// <summary>
        /// Performs the event invocation logic
        /// </summary>
        /// <param name="name">The name of the event to invoke</param>
        /// <param name="sender">The event sender</param>
        /// <param name="e">The event information</param>
        private void _runInvoke( string name, object sender, EventArgs e )
        {
            EventInfo theEvent = _eventForName( name );
            if( theEvent == null )
            {
                throw new RemotingException( "No event with name \"" + name + "\" found" );
            }

            _invokeEvent( sender, e, theEvent );
        }

        /// <summary>
        /// Resolves the event within the subclass with the given name
        /// </summary>
        /// <param name="name">The name of the event</param>
        /// <returns>The reflected EventInfo, or null if the event does not
        /// exist</returns>
        private EventInfo _eventForName( string name )
        {
            Type t = this.GetType();
            return t.GetEvent( name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance );
        }

        /// <summary>
        /// Invokes the reflected event
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event information</param>
        /// <param name="theEvent">The event to invoke</param>
        private void _invokeEvent( object sender, EventArgs e, EventInfo theEvent )
        {
            Delegate eventDelegate = _invocationsForEvent( theEvent );
            if( eventDelegate == null )
            {
                // The event isn't subscribed to.
                return;
            }

            if( _validateEvent( eventDelegate ) == false )
            {
                throw new EventSignatureException( theEvent );
            }
            else
            {
                _invoke( eventDelegate, sender, e );
            }
        }

        /// <summary>
        /// Gets the invocatable delegates for the event
        /// </summary>
        /// <param name="ev">The reflected event information</param>
        /// <returns>The delegate to invoke</returns>
        private Delegate _invocationsForEvent( EventInfo ev )
        {
            try
            {
                FieldInfo evField = GetType().GetField( ev.Name, BindingFlags.Instance | BindingFlags.NonPublic );
                return (Delegate)evField.GetValue( this );
            }
            catch( Exception e )
            {
                string err = "Error resolving delegates for event \"" + ev.Name + "\"";
                throw new RemotingException( err, e );
            }
        }

        /// <summary>
        /// Determines whether the method represented by the delegate is valid
        /// </summary>
        /// <param name="d">The delegate event method</param>
        /// <returns>true if the method is in a valid event format</returns>
        private bool _validateEvent( Delegate d )
        {
            MethodInfo method = d.Method;
            ParameterInfo[] paramSet = method.GetParameters();
            if( paramSet.Length != 2 )
            {
                return false;
            }

            if( paramSet[0].ParameterType != typeof( object ) )
            {
                return false;
            }

            Type t2 = paramSet[1].ParameterType;
            bool isType = t2 == typeof( EventArgs );
            bool isSubclass = t2.IsSubclassOf( typeof( EventArgs ) );
            return isType || isSubclass;
        }

        /// <summary>
        /// Invokes the delegate, throwing an exception if one occurs and
        /// ThrowOnInvocationFailure is true
        /// </summary>
        /// <param name="method">The method to invoke</param>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event information</param>
        private void _invoke( Delegate method, object sender, EventArgs e )
        {
            try
            {
                _invokeDelegate( method, sender, e );
            }
            catch( Exception ex )
            {
                if( ThrowOnInvocationFailure )
                {
                    string err = "Error invoking sink event";
                    Exception re = new RemotingException( err, ex );
                    re.Data.Add( "delegate", method );
                    throw re;
                }
            }
        }

        /// <summary>
        /// Invokes the method(s) represented by the delegate
        /// </summary>
        /// <param name="method">The method to invoke</param>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event information</param>
        private void _invokeDelegate( Delegate method, object sender, EventArgs e )
        {
            object[] parameters = new[] { sender, e };
            method.Method.Invoke( method.Target, parameters );
        }
    }
}
