using DIPS.Processor.Client;
using DIPS.Processor.Client.JobDeployment;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace DIPS.Tests.Processor.Client
{
    /// <summary>
    /// Summary description for JobRequestTests
    /// </summary>
    [TestClass]
    public class JobRequestTests
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
        /// Tests attempting to construct a <see cref="JobRequest"/> with no definition.
        /// </summary>
        [TestMethod]
        [ExpectedException( typeof( ArgumentNullException ) )]
        public void TestConstructor_NullAlgorithm()
        {
            JobRequest r = new JobRequest( null );
        }

        /// <summary>
        /// Tests attempting to construct a <see cref="JobRequest"/> with valid arguments.
        /// </summary>
        [TestMethod]
        public void TestConstructor_ValidArguments()
        {
            ObjectJobDefinition d = new ObjectJobDefinition( new AlgorithmDefinition[] { }, new JobInput[] { } );
            JobRequest r = new JobRequest( d );

            Assert.AreEqual( d, r.Job );
        }
    }
}
