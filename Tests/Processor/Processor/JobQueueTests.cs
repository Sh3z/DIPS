using DIPS.Processor;
using DIPS.Processor.Client;
using DIPS.Processor.Client.JobDeployment;
using DIPS.Processor.Queue;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Tests.Processor
{
    /// <summary>
    /// Summary description for JobQueueTests
    /// </summary>
    [TestClass]
    public class JobQueueTests
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
        /// Tests constructing a JobQueue.
        /// </summary>
        [TestMethod]
        public void TestConstructor()
        {
            JobQueue queue = new JobQueue();
            Assert.IsFalse( queue.HasPendingJobs );
            Assert.AreEqual( 0, queue.NumberOfJobs );
        }

        /// <summary>
        /// Tests enqueueing an object that is not a job.
        /// </summary>
        [TestMethod]
        public void TestEnqueue_InvalidRequest()
        {
            JobQueue queue = new JobQueue();
            bool eventFired = false;
            queue.JobAdded += ( s, e ) => eventFired = true;

            queue.Enqueue( null );
            Assert.IsFalse( queue.HasPendingJobs );
            Assert.IsFalse( eventFired );
        }

        /// <summary>
        /// Tests enqueueing a valid job.
        /// </summary>
        [TestMethod]
        public void TestEnqueue_ValidRequest()
        {
            JobQueue queue = new JobQueue();
            bool eventFired = false;
            queue.JobAdded += ( s, e ) => eventFired = true;

            IJobDefinition d = new DudDefinition();
            JobRequest r = new JobRequest( d );
            JobTicket t = new JobTicket( r );
            queue.Enqueue( t );

            Assert.IsTrue( eventFired );
            Assert.AreEqual( 1, queue.NumberOfJobs );
            Assert.IsTrue( queue.HasPendingJobs );
        }

        /// <summary>
        /// Tests dequeueing from an empty JobQueue.
        /// </summary>
        [TestMethod]
        public void TestDequeue_NoJobs()
        {
            JobQueue queue = new JobQueue();
            IJobTicket req = queue.Dequeue();
            Assert.IsNull( req );
        }

        /// <summary>
        /// Tests enqueueing, then dequeueing a job.
        /// </summary>
        [TestMethod]
        public void TestDequeue_OneJob()
        {
            JobQueue queue = new JobQueue();
            bool eventFired = false;

            IJobDefinition d = new DudDefinition();
            JobRequest r = new JobRequest( d );
            JobTicket t = new JobTicket( r );
            queue.Enqueue( t );

            IJobTicket t1 = queue.Dequeue();
            Assert.AreEqual( t, t1 );
            Assert.AreEqual( 0, queue.NumberOfJobs );
            Assert.IsFalse( queue.HasPendingJobs );
        }

        /// <summary>
        /// Tests enqueueing several jobs, then dequeueing them to assert
        /// they are dequeued in the correct order.
        /// </summary>
        [TestMethod]
        public void TestDequeue_MultipleJobs()
        {
            JobQueue queue = new JobQueue();
            IJobDefinition d = new DudDefinition();
            JobRequest r = new JobRequest( d );
            JobTicket t = new JobTicket( r );
            queue.Enqueue( t );
            JobRequest r2 = new JobRequest( d );
            JobTicket t2 = new JobTicket( r2 );
            queue.Enqueue( t2 );

            IJobTicket ta = queue.Dequeue();
            Assert.AreEqual( t, ta );
            Assert.IsTrue( queue.HasPendingJobs );
            Assert.AreEqual( 1, queue.NumberOfJobs );

            IJobTicket tb = queue.Dequeue();
            Assert.AreEqual( t2, tb );
            Assert.IsFalse( queue.HasPendingJobs );
            Assert.AreEqual( 0, queue.NumberOfJobs );
        }

        /// <summary>
        /// Tests cancelling an object the queue cannot handle and
        /// has no successor.
        /// </summary>
        [TestMethod]
        public void TestHandleCancel_Unhandleable_NoSuccessor()
        {
            JobQueue queue = new JobQueue();
            bool result = queue.Handle( null );
            Assert.IsFalse( result );
        }

        /// <summary>
        /// Tests cancelling an object the queue can handle.
        /// </summary>
        [TestMethod]
        public void TestHandleCancel_Handleable()
        {
            JobQueue queue = new JobQueue();
            IJobDefinition d = new DudDefinition();
            JobRequest r = new JobRequest( d );
            JobTicket t = new JobTicket( r );
            queue.Enqueue( t );
            bool result = queue.Handle( t );

            Assert.IsTrue( result );
            Assert.IsFalse( queue.HasPendingJobs );
        }


        class DudDefinition : IJobDefinition
        {
            public IEnumerable<AlgorithmDefinition> GetAlgorithms()
            {
                throw new NotImplementedException();
            }

            public IEnumerable<JobInput> GetInputs()
            {
                throw new NotImplementedException();
            }
        }
    }
}
