using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DIPS.Processor.Persistence;
using DIPS.Processor;
using DIPS.Processor.Plugin;
using System.Drawing;

namespace DIPS.Tests.Processor
{
    /// <summary>
    /// Summary description for JobDefinitionTests
    /// </summary>
    [TestClass]
    public class JobDefinitionTests
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
        /// Tests constructing a definition with null processes.
        /// </summary>
        [TestMethod]
        [ExpectedException( typeof( ArgumentNullException ) )]
        public void TestConstructor_NullProcesses()
        {
            JobDefinition d = new JobDefinition( Guid.Empty, null, new DudPersister() );
        }

        /// <summary>
        /// Tests constructing a definition with a null persister.
        /// </summary>
        [TestMethod]
        [ExpectedException( typeof( ArgumentNullException ) )]
        public void TestConstructor_NullPersister()
        {
            JobDefinition d = new JobDefinition( Guid.Empty, new PipelineEntry[] { }, null );
        }

        /// <summary>
        /// Tests constructing a definition with a valid set of args.
        /// </summary>
        [TestMethod]
        public void TestConstructor_ValidArgs()
        {
            IEnumerable<PipelineEntry> plugins = new PipelineEntry[] { };
            IJobPersister persister = new DudPersister();
            JobDefinition d = new JobDefinition( Guid.Empty, plugins, persister );

            Assert.AreEqual(Guid.Empty, d.JobID);
            Assert.AreEqual( plugins, d.Processes );
            Assert.AreEqual( persister, d.Persister );
            Assert.IsNotNull( d.Inputs );
            Assert.AreEqual( 0, d.Inputs.Count );
        }


        class DudPersister : IJobPersister
        {
            public void Persist( Guid jobID, Image output, object identifier )
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
    }
}
