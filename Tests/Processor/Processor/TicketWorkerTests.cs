using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DIPS.Processor.Executor;
using DIPS.Processor;
using DIPS.Processor.Client;
using DIPS.Processor.Client.JobDeployment;
using DIPS.Processor.Plugin;

namespace DIPS.Tests.Processor
{
    /// <summary>
    /// Summary description for TicketWorkerTests
    /// </summary>
    [TestClass]
    public class TicketWorkerTests
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
        /// Tests constructing the worker with a null factory.
        /// </summary>
        [TestMethod]
        [ExpectedException( typeof( ArgumentNullException ) )]
        public void TestConstructor_NullFactory()
        {
            TicketWorker w = new TicketWorker( null );
        }

        /// <summary>
        /// Tests running an invalid job.
        /// </summary>
        [TestMethod]
        public void TestWork_InvalidJob()
        {
            ObjectJobDefinition d = new ObjectJobDefinition(
                new AlgorithmDefinition[] { },
                new JobInput[] { } );
            JobRequest r = new JobRequest( d );
            JobTicket ticket = new JobTicket( r, new DudCancellationHandler() );
            TicketWorker w = new TicketWorker( new RegistryFactory() );

            bool didError = false;
            bool didFinish = false;
            ticket.JobError += ( s, e ) => didError = true;
            ticket.JobCompleted += ( s, e ) => didFinish = true;
            w.Work( ticket );

            Assert.IsTrue( didError );
            Assert.IsFalse( didFinish );
        }

        /// <summary>
        /// Tests working a job where the plugin encounters an exception
        /// </summary>
        [TestMethod]
        public void TestWork_PluginException()
        {
            ObjectJobDefinition d = new ObjectJobDefinition(
                new [] { new AlgorithmDefinition( "Test", new Property[] {} ) },
                new JobInput[] { } );
            JobRequest r = new JobRequest( d );
            JobTicket ticket = new JobTicket( r, new DudCancellationHandler() );
            TicketWorker w = new TicketWorker( new RegistryFactory() );

            bool didError = false;
            bool didFinish = false;
            ticket.JobError += ( s, e ) => didError = true;
            ticket.JobCompleted += ( s, e ) => didFinish = true;
            w.Work( ticket );

            Assert.IsTrue( didError );
            Assert.IsFalse( didFinish );
        }



        class DudCancellationHandler : ITicketCancellationHandler
        {
            public ITicketCancellationHandler Successor
            {
                set { throw new NotImplementedException(); }
            }

            public bool Handle( IJobTicket ticket )
            {
                throw new NotImplementedException();
            }
        }

        class DudPluginFactory : IPluginFactory
        {
            public AlgorithmPlugin Manufacture( AlgorithmDefinition algorithm )
            {
                return new BadPlugin();
            }
        }

        class BadPlugin : AlgorithmPlugin
        {
            public override void Run()
            {
                throw new NotImplementedException();
            }
        }
    }
}
