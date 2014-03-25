using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using DIPS.Processor.Persistence;
using System.Linq;

namespace DIPS.Tests.Processor
{
    /// <summary>
    /// Summary description for MemoryPersisterTests
    /// </summary>
    [TestClass]
    public class MemoryPersisterTests
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
        /// Tests persisting an image without an identifier.
        /// </summary>
        [TestMethod]
        public void TestPersist_NullIdentifier()
        {
            Image img = Image.FromFile( "img.bmp" );
            MemoryPersister persister = new MemoryPersister();
            persister.Persist( Guid.Empty, img, null );

            var images = persister.Load( Guid.Empty );
            Assert.IsTrue( images.Any() );

            PersistedResult r = images.First();
            Assert.AreEqual( 0, r.Identifier );
        }

        /// <summary>
        /// Tests persisting an image with a string identifier.
        /// </summary>
        [TestMethod]
        public void TestPersist_StringIdentifier()
        {
            string id = "id";
            Image img = Image.FromFile( "img.bmp" );
            MemoryPersister persister = new MemoryPersister();
            persister.Persist( Guid.Empty, img, id );

            var images = persister.Load( Guid.Empty );
            Assert.IsTrue( images.Any() );

            PersistedResult r = images.First();
            Assert.AreEqual( id, r.Identifier );
        }

        /// <summary>
        /// Tests loading from a persister with no saved records.
        /// </summary>
        [TestMethod]
        public void TestLoad_NoItems()
        {
            MemoryPersister persister = new MemoryPersister();
            var results = persister.Load( Guid.Empty );

            Assert.IsNotNull( results );
            Assert.IsFalse( results.Any() );
        }

        /// <summary>
        /// Tests loading from jobs with different guids.
        /// </summary>
        [TestMethod]
        public void TestLoad_VaryingJobs()
        {
            Guid jobA = Guid.NewGuid();
            Guid jobB = Guid.NewGuid();
            string idA = "id";
            Image img = Image.FromFile( "img.bmp" );
            MemoryPersister persister = new MemoryPersister();
            persister.Persist( jobA, img, idA );

            var jobBResults = persister.Load( jobB );
            Assert.IsNotNull( jobBResults );
            Assert.IsFalse( jobBResults.Any() );

            var jobAResults = persister.Load( jobA );
            Assert.IsTrue( jobAResults.Any() );
        }

        /// <summary>
        /// Tests loading a specific result from a job when no results
        /// have been saved for the job.
        /// </summary>
        [TestMethod]
        public void TestLoadByIdentifier_NoResultsForJob()
        {
            MemoryPersister persister = new MemoryPersister();
            PersistedResult r = persister.Load( Guid.Empty, "" );

            Assert.IsNull( r );
        }

        /// <summary>
        /// Tests loading for a specific result from a job, where the
        /// job has a result but not for the provided identifier.
        /// </summary>
        [TestMethod]
        public void TestLoadByIdentifier_IdentifierNotPresent()
        {
            string id = "id";
            Image img = Image.FromFile( "img.bmp" );
            MemoryPersister persister = new MemoryPersister();
            persister.Persist( Guid.Empty, img, id );
            PersistedResult r = persister.Load( Guid.Empty, "unknown" );

            Assert.IsNull( r );
        }

        /// <summary>
        /// Tests loading for a specific result for a job that is present.
        /// </summary>
        [TestMethod]
        public void TestLoadByIdentifier_IdentifierPresent()
        {
            string id = "id";
            Image img = Image.FromFile( "img.bmp" );
            MemoryPersister persister = new MemoryPersister();
            persister.Persist( Guid.Empty, img, id );
            PersistedResult r = persister.Load( Guid.Empty, id );

            Assert.IsNotNull( r );
            Assert.AreEqual( id, r.Identifier );
        }

        /// <summary>
        /// Tests deleting all results from an unknown job.
        /// </summary>
        [TestMethod]
        public void TestDeleteAll_UnknownJob()
        {
            MemoryPersister persister = new MemoryPersister();
            bool deleted = persister.Delete( Guid.NewGuid() );

            Assert.IsFalse( deleted );
        }

        /// <summary>
        /// Tests deleting all the results from a valid job.
        /// </summary>
        [TestMethod]
        public void TestDeleteAll_ValidJob()
        {
            string id = "id";
            Image img = Image.FromFile( "img.bmp" );
            MemoryPersister persister = new MemoryPersister();
            persister.Persist( Guid.Empty, img, id );

            Assert.IsTrue( persister.Load( Guid.Empty ).Any() );

            bool deleted = persister.Delete( Guid.Empty );
            Assert.IsTrue( deleted );
            Assert.IsFalse( persister.Load( Guid.Empty ).Any() );
        }
    }
}
