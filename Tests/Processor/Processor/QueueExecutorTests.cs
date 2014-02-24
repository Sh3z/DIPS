using DIPS.Processor;
using DIPS.Processor.Client;
using DIPS.Processor.Client.JobDeployment;
using DIPS.Processor.Executor;
using DIPS.Processor.Persistence;
using DIPS.Processor.Queue;
using DIPS.Processor.Worker;
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
            QueueExecutor executor = new QueueExecutor( null );
        }

        [TestMethod]
        public void TestConstructor_ValidArgs()
        {
            QueueExecutor executor = new QueueExecutor( new JobQueue() );
        }

        [TestMethod]
        public void TestWork_NoJobs()
        {
            JobQueue queue = new JobQueue();
            QueueExecutor executor = new QueueExecutor( queue );
            executor.Worker = new WorkerImpl();
            executor.PluginFactory = new DudFactory();
            executor.Persister = new DudPersister();

            // Shouldn't get any exceptions
            executor.Start();
        }

        [TestMethod]
        [ExpectedException( typeof( InvalidOperationException ) )]
        public void TestWork_NoWorker()
        {
            ObjectJobDefinition d = new ObjectJobDefinition(
                new PipelineDefinition(
                    new AlgorithmDefinition[] { } ),
                new JobInput[] { } );
            JobRequest r = new JobRequest( d );
            JobTicket ticket = new JobTicket( r, new DudCancellationHandler() );
            JobQueue queue = new JobQueue();
            QueueExecutor executor = new QueueExecutor( queue );
            bool didComplete = false;
            executor.ExhaustedQueue += ( s, e ) => didComplete = true;
            queue.Enqueue( ticket );

            executor.Start();
        }

        [TestMethod]
        [ExpectedException( typeof( InvalidOperationException ) )]
        public void TestWork_NoPluginFactory()
        {
            ObjectJobDefinition d = new ObjectJobDefinition(
                new PipelineDefinition(
                    new AlgorithmDefinition[] { } ),
                new JobInput[] { } );
            JobRequest r = new JobRequest( d );
            JobTicket ticket = new JobTicket( r, new DudCancellationHandler() );
            JobQueue queue = new JobQueue();
            QueueExecutor executor = new QueueExecutor( queue );
            bool didComplete = false;
            executor.ExhaustedQueue += ( s, e ) => didComplete = true;
            queue.Enqueue( ticket );

            executor.Worker = new TicketWorker();
            executor.Start();
        }

        [TestMethod]
        [ExpectedException( typeof( InvalidOperationException ) )]
        public void TestWork_NoPersister()
        {
            ObjectJobDefinition d = new ObjectJobDefinition(
                new PipelineDefinition(
                    new AlgorithmDefinition[] { } ),
                new JobInput[] { } );
            JobRequest r = new JobRequest( d );
            JobTicket ticket = new JobTicket( r, new DudCancellationHandler() );
            JobQueue queue = new JobQueue();
            QueueExecutor executor = new QueueExecutor( queue );
            bool didComplete = false;
            executor.ExhaustedQueue += ( s, e ) => didComplete = true;
            queue.Enqueue( ticket );

            executor.Worker = new TicketWorker();
            executor.PluginFactory = new DudFactory();
            executor.Start();
        }

        [TestMethod]
        public void TestWork_OneJob()
        {
            ObjectJobDefinition d = new ObjectJobDefinition(
                new PipelineDefinition(
                    new AlgorithmDefinition[] { } ),
                new JobInput[] { } );
            JobRequest r = new JobRequest( d );
            JobTicket ticket = new JobTicket( r, new DudCancellationHandler() );
            JobQueue queue = new JobQueue();
            WorkerImpl worker = new WorkerImpl();
            QueueExecutor executor = new QueueExecutor( queue );
            executor.Worker = worker;
            executor.PluginFactory = new DudFactory();
            executor.Persister = new DudPersister();
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

        class DudPersister : IJobPersister
        {
            public void Persist( Guid jobID, System.Drawing.Image output, object identifier )
            {
                throw new NotImplementedException();
            }

            public PersistedResult Load( Guid jobID, object identifier )
            {
                throw new NotImplementedException();
            }

            public IEnumerable<PersistedResult> Load( Guid jobID )
            {
                throw new NotImplementedException();
            }

            public bool Delete( Guid jobID )
            {
                throw new NotImplementedException();
            }

            public bool Delete( Guid jobID, object identifier )
            {
                throw new NotImplementedException();
            }
        }

        class DudFactory : IPluginFactory
        {
            public DIPS.Processor.Plugin.AlgorithmPlugin Manufacture( AlgorithmDefinition algorithm )
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

        public void Work( WorkerArgs job )
        {
            DidWork = true;
        }

        public ITicketCancellationHandler Successor
        {
            set { throw new NotImplementedException(); }
        }

        public bool Handle( IJobTicket ticket )
        {
            throw new NotImplementedException();
        }
    }
}
