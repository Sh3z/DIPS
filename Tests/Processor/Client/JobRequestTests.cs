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
            IEnumerable<IImageSource> images = new List<IImageSource>();

            // Should throw ArgumentNullException due to null algorithm.
            JobRequest req = new JobRequest( null, images );
        }

        /// <summary>
        /// Tests attempting to construct a <see cref="JobRequest"/> with null images..
        /// </summary>
        [TestMethod]
        [ExpectedException( typeof( ArgumentNullException ) )]
        public void TestConstructor_NullImages()
        {
            Algorithm algorithm = Algorithm.CreateWithSteps( new[] { new AlgorithmStepImpl() } );

            // Should throw ArgumentNullException due to null images.
            JobRequest req = new JobRequest( algorithm, null );
        }

        /// <summary>
        /// Tests attempting to construct a <see cref="JobRequest"/> with valid arguments.
        /// </summary>
        [TestMethod]
        public void TestConstructor_ValidArguments()
        {
            Algorithm algorithm = Algorithm.CreateWithSteps( new[] { new AlgorithmStepImpl() } );
            IEnumerable<IImageSource> images = new List<IImageSource>();
            JobRequest req = new JobRequest( algorithm, images );

            Assert.AreEqual( algorithm, req.Algorithm );
            Assert.AreEqual( images, req.Images );
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
