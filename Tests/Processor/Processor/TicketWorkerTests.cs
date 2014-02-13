using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DIPS.Processor.Executor;
using DIPS.Processor;
using DIPS.Processor.Client;
using DIPS.Processor.Client.JobDeployment;
using DIPS.Processor.Plugin;
using DIPS.Processor.Persistence;
using System.Drawing;
using DIPS.Processor.Registry;

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
            TicketWorker w = new TicketWorker( null, new DudPersister() );
        }

        /// <summary>
        /// Tests creating a worker with a null persister
        /// </summary>
        [TestMethod]
        [ExpectedException( typeof( ArgumentNullException ) )]
        public void TestConstructor_NullPersister()
        {
            ProcessPluginRepository r = new ProcessPluginRepository();
            RegistryCache.Cache.Initialize( r );
            RegistryFactory factory = new RegistryFactory( r );
            TicketWorker w = new TicketWorker( factory, null );
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
            ProcessPluginRepository re = new ProcessPluginRepository();
            RegistryCache.Cache.Initialize( re );
            RegistryFactory factory = new RegistryFactory( re );
            TicketWorker w = new TicketWorker( factory, new DudPersister() );

            bool didError = false;
            bool didFinish = false;
            ticket.JobError += ( s, e ) => didError = true;
            ticket.JobCompleted += ( s, e ) => didFinish = true;
            w.Work( ticket );

            Assert.IsTrue( didError );
            Assert.IsFalse( didFinish );

            JobResult result = ticket.Result;
            Assert.AreEqual( JobState.Error, result.Result );
        }

        /// <summary>
        /// Tests working a job where the plugin encounters an exception
        /// </summary>
        [TestMethod]
        public void TestWork_PluginException()
        {
            ObjectJobDefinition d = new ObjectJobDefinition(
                new[] { new AlgorithmDefinition( "Test", new Property[] { } ) },
                new[] { new JobInput( Image.FromFile( "img.bmp" ) ) } );
            JobRequest r = new JobRequest( d );
            JobTicket ticket = new JobTicket( r, new DudCancellationHandler() );
            TicketWorker w = new TicketWorker( new DudPluginFactory(), new DudPersister() );

            bool didError = false;
            bool didFinish = false;
            ticket.JobError += ( s, e ) => didError = true;
            ticket.JobCompleted += ( s, e ) => didFinish = true;
            w.Work( ticket );

            Assert.IsTrue( didError );
            Assert.IsFalse( didFinish );

            JobResult result = ticket.Result;
            Assert.AreEqual( JobState.Error, result.Result );
            Assert.AreEqual( typeof( NotImplementedException ), result.Exception.GetType() );
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

        class DudPersister : IJobPersister
        {
            public void Persist( Guid jobID, System.Drawing.Image output, object identifier )
            {
            }

            public PersistedResult Load( Guid jobID, object identifier )
            {
                return null;
            }

            public IEnumerable<PersistedResult> Load( Guid jobID )
            {
                return null;
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
            public override void Run( object obj )
            {
                throw new NotImplementedException();
            }
        }
    }
}
