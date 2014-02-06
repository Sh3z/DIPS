using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DIPS.Processor.Persistence;
using DIPS.Processor;
using DIPS.Processor.Client;
using DIPS.Processor.Client.JobDeployment;
using System.Drawing;
using DIPS.Processor.Plugin;
using System.IO;
using System.Linq;

namespace DIPS.Tests.Processor
{
    /// <summary>
    /// Summary description for AppDataPersisterTests
    /// </summary>
    [TestClass]
    public class AppDataPersisterTests
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
        /// Gets or sets the current ticket to use in tests.
        /// </summary>
        private JobTicket CurrentTicket
        {
            get;
            set;
        }

        /// <summary>
        /// Initializer called before tests.
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            JobRequest r = new JobRequest( new TestDefinition() );
            CurrentTicket = new JobTicket( r, new TestCancellation() );
        }

        /// <summary>
        /// Cleaner called between tests.
        /// </summary>
        [TestCleanup]
        public void Clean()
        {
            // Delete all files in our test directory.
            if( Directory.Exists( AppDataPersister.OutputDataPath ) )
            {
                Directory.Delete( AppDataPersister.OutputDataPath, true );
            }
        }


        /// <summary>
        /// Tests constructing the persister with a null ticket
        /// </summary>
        [TestMethod]
        [ExpectedException( typeof( ArgumentNullException ) )]
        public void TestConstructor_NullTicket()
        {
            AppDataPersister persister = new AppDataPersister( null );
        }

        /// <summary>
        /// Tests persisting an image with no identifier.
        /// </summary>
        [TestMethod]
        public void TestPersist_NoIdentifier()
        {
            AppDataPersister persister = new AppDataPersister( CurrentTicket );
            Image toPersist = CurrentTicket.Request.Job.GetInputs().First().Input;
            object identifier = null;
            persister.Persist( toPersist, identifier );

            // File should be called output_0.png
            string path = string.Format( @"{0}/{{{1}}}/output_0.png", AppDataPersister.OutputDataPath, CurrentTicket.JobID );
            bool fileExists = File.Exists( path );
            Assert.IsTrue( fileExists );

            // Persist again, should be output_1.png
            persister.Persist( toPersist, identifier );

            path = string.Format( @"{0}/{{{1}}}/output_1.png", AppDataPersister.OutputDataPath, CurrentTicket.JobID );
            fileExists = File.Exists( path );
            Assert.IsTrue( fileExists );
        }

        /// <summary>
        /// Tests persisting an image with an identifier.
        /// </summary>
        [TestMethod]
        public void TestPersist_WithIdentifier()
        {
            string id = "test";
            AppDataPersister persister = new AppDataPersister( CurrentTicket );
            Image toPersist = CurrentTicket.Request.Job.GetInputs().First().Input;
            persister.Persist( toPersist, id );

            // File should be called output_0.png
            string path = string.Format( @"{0}/{{{1}}}/{2}.png", AppDataPersister.OutputDataPath, CurrentTicket.JobID, id );
            bool fileExists = File.Exists( path );
            Assert.IsTrue( fileExists );
        }


        class TestDefinition : IJobDefinition
        {
            public IEnumerable<AlgorithmDefinition> GetAlgorithms()
            {
                return new AlgorithmDefinition[] {
                    new AlgorithmDefinition("Test", null ) };
            }

            public IEnumerable<JobInput> GetInputs()
            {
                return new[] { new JobInput( Image.FromFile( "img.bmp" ) ) };
            }
        }

        class TestAlgorithm : AlgorithmPlugin
        {
            public override void Run()
            {
                Output = Input;
            }
        }

        class TestCancellation : ITicketCancellationHandler
        {
            public ITicketCancellationHandler Successor
            {
                get;
                set;
            }

            public bool Handle( IJobTicket ticket )
            {
                return false;
            }
        }
    }
}
