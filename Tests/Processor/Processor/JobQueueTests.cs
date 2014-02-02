using DIPS.Processor;
using DIPS.Processor.Client;
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

        [TestMethod]
        public void TestConstructor()
        {
            JobQueue queue = new JobQueue();
            Assert.IsFalse( queue.HasPendingJobs );
        }

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

        [TestMethod]
        public void TestEnqueue_ValidRequest()
        {
            JobQueue queue = new JobQueue();
            bool eventFired = false;
            queue.JobAdded += ( s, e ) => eventFired = true;

            Assert.Inconclusive( "Rewrite test" );

            //queue.Enqueue( ticket );
            //Assert.IsTrue( queue.HasPendingJobs );
            //Assert.IsTrue( eventFired );
        }

        [TestMethod]
        public void TestDequeue_NoJobs()
        {
            JobQueue queue = new JobQueue();
            IJobTicket req = queue.Dequeue();
            Assert.IsNull( req );
        }

        [TestMethod]
        public void TestDequeue_OneJob()
        {
            JobQueue queue = new JobQueue();
            Assert.Inconclusive( "Rewrite test" );

            //JobRequest req = new JobRequest( Algorithm.CreateWithSteps( new List<IAlgorithmStep>() ), new List<IImageSource>() );
            //JobTicket ticket = new JobTicket( req );
            //queue.Enqueue( ticket );

            //IJobTicket dequeued = queue.Dequeue();
            //Assert.AreSame( ticket, dequeued );
            //Assert.IsFalse( queue.HasPendingJobs );
        }

        [TestMethod]
        public void TestDequeue_MultipleJobs()
        {
            Assert.Inconclusive( "Rewrite test" );
            //JobQueue queue = new JobQueue();
            
            //// Add 3 jobs
            //for( int i = 0; i < 3; i++ )
            //{
            //    JobRequest req = new JobRequest( Algorithm.CreateWithSteps( new List<IAlgorithmStep>() ), new List<IImageSource>() );
            //    JobTicket ticket = new JobTicket( req );
            //    queue.Enqueue( ticket );
            //}

            //// Dequeue each job one-by-one and assert the queue state
            //IJobTicket dequeued = queue.Dequeue();
            //Assert.IsTrue( queue.HasPendingJobs );
            //dequeued = queue.Dequeue();
            //Assert.IsTrue( queue.HasPendingJobs );
            //dequeued = queue.Dequeue();
            //Assert.IsFalse( queue.HasPendingJobs );
        }
    }
}
