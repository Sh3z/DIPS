using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DIPS.Util.Remoting;

namespace DIPS.Tests.Util
{
    /// <summary>
    /// Summary description for EventSinkTests
    /// </summary>
    [TestClass]
    public class EventSinkTests
    {
        /// <summary>
        /// Gets or sets the test context which provides
        /// information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get;
            set;
        }


        /// <summary>
        /// Tests invoking a null-named event
        /// </summary>
        [TestMethod]
        [ExpectedException( typeof( ArgumentException ) )]
        public void TestInvoke_NullEventName()
        {
            EventSink s = new DudSink();
            s.InvokeEvent( null, EventArgs.Empty );
        }

        /// <summary>
        /// Tests invoking an empty-named event
        /// </summary>
        [TestMethod]
        [ExpectedException( typeof( ArgumentException ) )]
        public void TestInvoke_EmptyEventName()
        {
            EventSink s = new DudSink();
            s.InvokeEvent( string.Empty, EventArgs.Empty );
        }

        /// <summary>
        /// Tests invoking an event without a sender
        /// </summary>
        [TestMethod]
        [ExpectedException( typeof( ArgumentNullException ) )]
        public void TestInvoke_NullSender()
        {
            EventSink s = new DudSink();
            s.InvokeEvent( "TestEvent", null, EventArgs.Empty );
        }

        /// <summary>
        /// Tests attempting to invoke an event that does not exist
        /// </summary>
        [TestMethod]
        [ExpectedException( typeof( RemotingException ) )]
        public void TestInvoke_UnknownEvent()
        {
            EventSink s = new DudSink();
            s.InvokeEvent( "DoesNotExist", this, EventArgs.Empty );
        }

        /// <summary>
        /// Tests invoking an event where the handler throws an error
        /// </summary>
        [TestMethod]
        [ExpectedException( typeof( RemotingException ) )]
        public void TestInvoke_HandlerThrowsException_ThrowOnError()
        {
            DudSink s = new DudSink();
            s.ThrowOnInvocationFailure = true;
            s.TestEvent += ( se, e ) => { throw new Exception(); };
            s.InvokeEvent( "TestEvent", this, EventArgs.Empty );
        }

        /// <summary>
        /// Tests invoking an event where the handler throws an error
        /// </summary>
        [TestMethod]
        public void TestInvoke_HandlerThrowsException_DoNotThrowOnError()
        {
            bool didInvoke = false;
            DudSink s = new DudSink();
            s.ThrowOnInvocationFailure = false;
            s.TestEvent += ( se, e ) => { didInvoke = true; throw new Exception(); };
            s.InvokeEvent( "TestEvent", this, EventArgs.Empty );

            Assert.IsTrue( didInvoke );
        }

        /// <summary>
        /// Tests invoking a valid event
        /// </summary>
        [TestMethod]
        public void TestInvoke_ValidSetup()
        {
            bool didInvoke = false;
            DudSink s = new DudSink();
            s.TestEvent += ( se, e ) => { didInvoke = true; };
            s.InvokeEvent( "TestEvent", this, EventArgs.Empty );

            Assert.IsTrue( didInvoke );
        }

        /// <summary>
        /// Tests invoking an unsupported event type
        /// </summary>
        [TestMethod]
        [ExpectedException( typeof( EventSignatureException ) )]
        public void TestInvoke_InvalidEventType()
        {
            DudSink s = new DudSink();
            s.BadEvent += () => { };
            s.InvokeEvent( "BadEvent", EventArgs.Empty );
        }


        class DudSink : EventSink
        {
            public event EventHandler TestEvent;

            public event BadEventType BadEvent;
        }

        delegate void BadEventType();
    }
}
