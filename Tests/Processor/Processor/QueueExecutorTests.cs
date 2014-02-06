using DIPS.Processor;
using DIPS.Processor.Client;
using DIPS.Processor.Client.JobDeployment;
using DIPS.Processor.Executor;
using DIPS.Processor.Queue;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DIPS.Tests.Processor
{
    /// <summary>
    /// Summary description for QueueExecutorTests
    /// </summary>
    [TestClass]
    public class QueueExecutorTests
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
        [ExpectedException( typeof( ArgumentNullException ) )]
        public void TestConstructor_NullQueue()
        {
            QueueExecutor executor = new QueueExecutor( null, new WorkerImpl() );
        }

        [TestMethod]
        [ExpectedException( typeof( ArgumentNullException ) )]
        public void TestConstructor_NullWorker()
        {
            QueueExecutor executor = new QueueExecutor( new JobQueue(), null );
        }

        [TestMethod]
        public void TestConstructor_ValidArgs()
        {
            QueueExecutor executor = new QueueExecutor( new JobQueue(), new WorkerImpl() );
        }

        [TestMethod]
        public void TestWork_NoJobs()
        {
            JobQueue queue = new JobQueue();
            WorkerImpl worker = new WorkerImpl();
            QueueExecutor executor = new QueueExecutor( queue, worker );

            // Shouldn't get any exceptions
            executor.Start();
        }

        [TestMethod]
        public void TestWork_OneJob()
        {
            ObjectJobDefinition d = new ObjectJobDefinition(
                new AlgorithmDefinition[] { },
                new JobInput[] { } );
            JobRequest r = new JobRequest( d );
            JobTicket ticket = new JobTicket( r, new DudCancellationHandler() );
            JobQueue queue = new JobQueue();
            WorkerImpl worker = new WorkerImpl();
            QueueExecutor executor = new QueueExecutor( queue, worker );
            bool didComplete = false;
            executor.ExhaustedQueue += ( s, e ) => didComplete = true;
            queue.Enqueue( ticket );

            executor.Start();

            while( didComplete == false )
            {
                Thread.Sleep( 1 );
            }

            Assert.IsTrue( worker.DidWork );
        }

        class DudCancellationHandler : ITicketCancellationHandler
        {
            public ITicketCancellationHandler Successor
            {
                get;
                set;
            }

            public bool Handle( IJobTicket ticket )
            {
                throw new NotImplementedException();
            }
        }
    }

    class WorkerImpl : IWorker
    {
        public WorkerImpl()
        {
            DidWork = false;
        }

        public bool DidWork
        {
            get;
            private set;
        }

        public void Work( IJobTicket job )
        {
            DidWork = true;
        }
    }
}
