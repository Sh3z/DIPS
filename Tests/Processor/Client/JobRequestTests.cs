using DIPS.Processor.Client;
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
        /// Tests attempting to construct a <see cref="JobRequest"/> with no algorithm.
        /// </summary>
        [TestMethod]
        [ExpectedException( typeof( ArgumentNullException ) )]
        public void TestConstructor_NullAlgorithm()
        {
            Assert.Inconclusive( "Rewrite test" );
        }

        /// <summary>
        /// Tests attempting to construct a <see cref="JobRequest"/> with null images..
        /// </summary>
        [TestMethod]
        [ExpectedException( typeof( ArgumentNullException ) )]
        public void TestConstructor_NullImages()
        {
            Assert.Inconclusive( "Rewrite test" );
        }

        /// <summary>
        /// Tests attempting to construct a <see cref="JobRequest"/> with valid arguments.
        /// </summary>
        [TestMethod]
        public void TestConstructor_ValidArguments()
        {
            Assert.Inconclusive( "Rewrite test" );
        }


        /// <summary>
        /// Represents a blank <see cref="IAlgorithmStep"/> implementation for testing uses.
        /// </summary>
        class AlgorithmStepImpl : IAlgorithmStep
        {
            /// <summary>
            /// Does nothing.
            /// </summary>
            /// <param name="stateOfJob">N/A</param>
            public void Run( JobState stateOfJob )
            {
            }
        }
    }
}
