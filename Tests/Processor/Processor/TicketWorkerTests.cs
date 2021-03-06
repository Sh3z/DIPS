﻿using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DIPS.Processor;
using DIPS.Processor.Client;
using DIPS.Processor.Client.JobDeployment;
using DIPS.Processor.Plugin;
using DIPS.Processor.Persistence;
using System.Drawing;
using DIPS.Processor.Registry;
using DIPS.Processor.Client.Sinks;
using System.Threading;
using DIPS.Processor.Worker;
using DIPS.Processor.Pipeline;
using DIPS.Processor.Client.Eventing;

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
        /// Tests running an invalid job.
        /// </summary>
        [TestMethod]
        public void TestWork_EmptyJob()
        {
            ObjectJobDefinition d = new ObjectJobDefinition(
                new PipelineDefinition(
                    new AlgorithmDefinition[] { } ),
                new JobInput[] { } );
            JobRequest r = new JobRequest( d );
            JobTicket ticket = new JobTicket( r, new DudCancellationHandler() );
            ProcessPluginRepository re = new ProcessPluginRepository();
            RegistryCache.Cache.Initialize( re );
            RegistryFactory factory = new RegistryFactory( re );
            TicketWorker w = new TicketWorker();
            WorkerArgs args = new WorkerArgs( new DudPersister(), new BadPipelineFactory() );
            args.Ticket = ticket;

            bool didError = false;
            bool didFinish = false;
            TicketSink s = new TicketSink();
            ticket.Sinks.Add( s );
            s.JobCompleted += ( se, e ) => didFinish = true;
            w.Work( args );

            // Events are dispatched on a seperate thread, so let it run.
            Thread.Sleep( 15 );

            Assert.IsTrue( didFinish );

            JobResult result = ticket.Result;
            Assert.AreEqual( JobState.Complete, result.Result );
        }

        /// <summary>
        /// Tests working a job where the plugin encounters an exception
        /// </summary>
        [TestMethod]
        public void TestWork_PluginException()
        {
            ObjectJobDefinition d = new ObjectJobDefinition(
                new PipelineDefinition(
                    new[] { new AlgorithmDefinition( "Test", new Property[] { } ) } ),
                new[] { new JobInput( Image.FromFile( "img.bmp" ) ) } );
            JobRequest r = new JobRequest( d );
            JobTicket ticket = new JobTicket( r, new DudCancellationHandler() );
            TicketWorker w = new TicketWorker();
            WorkerArgs args = new WorkerArgs( new DudPersister(), new BadPipelineFactory() );
            args.Ticket = ticket;

            bool didError = false;
            bool didFinish = false;
            TicketSink s = new TicketSink();
            ticket.Sinks.Add( s );
            s.JobError += ( se, e ) => didError = true;
            s.JobCompleted += ( se, e ) => didFinish = true;
            w.Work( args );

            // Events are dispatched on a seperate thread, so let it run.
            Thread.Sleep( 15 );

            Assert.IsTrue( didError );
            Assert.IsFalse( didFinish );

            JobResult result = ticket.Result;
            Assert.AreEqual( JobState.Error, result.Result );
            Assert.AreEqual( typeof( NotImplementedException ), result.Exception.GetType() );
        }

        /// <summary>
        /// Tests the input processed event is fired correctly
        /// </summary>
        [TestMethod]
        public void TestWork_InputProcessedFired()
        {
            ObjectJobDefinition d = new ObjectJobDefinition(
                new PipelineDefinition(
                    new[] { new AlgorithmDefinition( "Test", new Property[] { } ) } ),
                new[] { new JobInput( Image.FromFile( "img.bmp" ) ) { Identifier = "Test" } } );
            JobRequest r = new JobRequest( d );
            JobTicket ticket = new JobTicket( r, new DudCancellationHandler() );
            TicketWorker w = new TicketWorker();
            WorkerArgs args = new WorkerArgs( new DudPersister(), new DudPipelineFactory() );
            args.Ticket = ticket;

            bool didProcess = false;
            InputProcessedArgs ie = null;
            TicketSink s = new TicketSink();
            ticket.Sinks.Add( s );
            s.InputProcessed += ( se, e ) => { didProcess = true; ie = e; };

            w.Work( args );

            Thread.Sleep( 5 );

            Assert.IsTrue( didProcess );
            Assert.IsNotNull( ie );
            Assert.AreEqual( "Test", ie.Identifier );
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
                return new PersistedResult[] {};
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

        class DudPluginFactory : IPluginFactory
        {
            public AlgorithmPlugin Manufacture( AlgorithmDefinition algorithm )
            {
                return new BadPlugin();
            }
        }

        class BadPipelineFactory : IPipelineFactory
        {
            public Pipeline CreatePipeline( PipelineDefinition def )
            {
                Pipeline p = new Pipeline();
                p.Add( new PipelineEntry( new BadPlugin() ) );
                return p;
            }
        }

        class BadPlugin : AlgorithmPlugin
        {
            public override void Run( object obj )
            {
                throw new NotImplementedException();
            }
        }

        class DudPipelineFactory : IPipelineFactory
        {

            public Pipeline CreatePipeline( PipelineDefinition def )
            {
                return new Pipeline();
            }
        }
    }
}
