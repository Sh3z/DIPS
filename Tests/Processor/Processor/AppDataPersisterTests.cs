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
        /// Gets or sets the current directory to use in testing.
        /// </summary>
        private string CurrentDirectory
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
            CurrentDirectory = Directory.GetCurrentDirectory() + "/TestOutput";
        }

        /// <summary>
        /// Cleaner called between tests.
        /// </summary>
        [TestCleanup]
        public void Clean()
        {
            // Delete all files in our test directory.
            if( Directory.Exists( CurrentDirectory ) )
            {
                Directory.Delete( CurrentDirectory, true );
            }
        }


        /// <summary>
        /// Tests constructing the persister with a null ticket
        /// </summary>
        [TestMethod]
        [ExpectedException( typeof( ArgumentNullException ) )]
        public void TestConstructor_NullTicket()
        {
            FileSystemPersister persister = new FileSystemPersister( null, FileSystemPersister.OutputDataPath );
        }

        /// <summary>
        /// Tests constructing the persister with a null target path.
        /// </summary>
        [TestMethod]
        [ExpectedException( typeof( ArgumentException ) )]
        public void TestConstructor_NullPath()
        {
            FileSystemPersister persister = new FileSystemPersister( CurrentTicket, null );
        }

        /// <summary>
        /// Tests constructing the persister with an empty target path.
        /// </summary>
        [TestMethod]
        [ExpectedException( typeof( ArgumentException ) )]
        public void TestConstructor_EmptyPath()
        {
            FileSystemPersister persister = new FileSystemPersister( CurrentTicket, string.Empty );
        }

        /// <summary>
        /// Tests persisting an image with no identifier.
        /// </summary>
        [TestMethod]
        public void TestPersist_NoIdentifier()
        {
            FileSystemPersister persister = new FileSystemPersister( CurrentTicket, CurrentDirectory );
            Image toPersist = CurrentTicket.Request.Job.GetInputs().First().Input;
            object identifier = null;
            persister.Persist( toPersist, identifier );

            // File should be called output_0.png
            string path = string.Format( @"{0}/{{{1}}}/output_0.png", CurrentDirectory, CurrentTicket.JobID );
            bool fileExists = File.Exists( path );
            Assert.IsTrue( fileExists );

            // Persist again, should be output_1.png
            persister.Persist( toPersist, identifier );

            path = string.Format( @"{0}/{{{1}}}/output_1.png", CurrentDirectory, CurrentTicket.JobID );
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
            FileSystemPersister persister = new FileSystemPersister( CurrentTicket, CurrentDirectory );
            Image toPersist = CurrentTicket.Request.Job.GetInputs().First().Input;
            persister.Persist( toPersist, id );

            // File should be called output_0.png
            string path = string.Format( @"{0}/{{{1}}}/{2}.png", CurrentDirectory, CurrentTicket.JobID, id );
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
