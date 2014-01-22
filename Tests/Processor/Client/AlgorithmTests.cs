using DIPS.Processor.Client;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace DIPS.Tests.Processor.Client
{
    /// <summary>
    /// Summary description for AlgorithmTests
    /// </summary>
    [TestClass]
    public class AlgorithmTests
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
        /// Tests attempting to create an algorithm with null steps.
        /// </summary>
        [TestMethod]
        public void TestCreateWithSteps_NullSteps()
        {
            Algorithm theAlg = Algorithm.CreateWithSteps( null );
            Assert.AreEqual( 0, theAlg.NumberOfSteps );
        }

        /// <summary>
        /// Tests attempting to create an algorithm with null steps.
        /// </summary>
        [TestMethod]
        public void TestCreateWithSteps_WithStep()
        {
            var theStep = new AlgorithmStepImpl();
            Algorithm theAlg = Algorithm.CreateWithSteps( new[] { theStep } );
            Assert.AreEqual( 1, theAlg.NumberOfSteps );
            Assert.AreEqual( theStep, theAlg.First() );
        }

        /// <summary>
        /// Tests attempting to add null to an <see cref="Algorithm"/> when it has not been sealed.
        /// </summary>
        [TestMethod]
        public void TestAdd_NullNotSealed()
        {
            Algorithm theAlg = Algorithm.CreateWithSteps( new[] { new AlgorithmStepImpl() } );
            theAlg.Add( null );

            Assert.AreEqual( 1, theAlg.NumberOfSteps );
        }
        
        /// <summary>
        /// Tests attempting to add a step to an <see cref="Algorithm"/> when it has not been
        /// sealed.
        /// </summary>
        [TestMethod]
        public void TestAdd_NotNullNotSealed()
        {
            Algorithm theAlg = Algorithm.CreateWithSteps( new[] { new AlgorithmStepImpl() } );
            theAlg.Add( new AlgorithmStepImpl() );

            Assert.AreEqual( 2, theAlg.NumberOfSteps );
        }

        /// <summary>
        /// Tests attempting to add null to an <see cref="Algorithm"/> after it has been sealed.
        /// </summary>
        [TestMethod]
        [ExpectedException( typeof( InvalidOperationException ) )]
        public void TestAdd_NullSealed()
        {
            Algorithm theAlg = Algorithm.CreateWithSteps( new[] { new AlgorithmStepImpl() } );
            theAlg.Seal();

            // Next line should throw the exception.
            theAlg.Add( null );
        }

        /// <summary>
        /// Tests attempting to add a step to an <see cref="Algorithm"/> after it has been
        /// sealed.
        /// </summary>
        [TestMethod]
        [ExpectedException( typeof( InvalidOperationException ) )]
        public void TestAdd_NotNullSealed()
        {
            Algorithm theAlg = Algorithm.CreateWithSteps( new[] { new AlgorithmStepImpl() } );
            theAlg.Seal();

            // Next line should throw the exception.
            theAlg.Add( new AlgorithmStepImpl() );
        }

        /// <summary>
        /// Tests attempting to remove a step from an <see cref="Algorithm"/> before it has
        /// been sealed.
        /// </summary>
        [TestMethod]
        public void TestRemove_NotSealed()
        {
            var theStep = new AlgorithmStepImpl();
            Algorithm theAlg = Algorithm.CreateWithSteps( new[] { theStep } );
            Assert.AreEqual( theStep, theAlg.First() );

            bool didRemove = theAlg.Remove( theStep );
            Assert.IsTrue( didRemove );
            Assert.AreEqual( 0, theAlg.NumberOfSteps );
        }

        /// <summary>
        /// Tests attempting to remove a step from an <see cref="Algorithm"/> after it has
        /// been sealed.
        /// </summary>
        [TestMethod]
        [ExpectedException( typeof( InvalidOperationException ) )]
        public void TestRemove_Sealed()
        {
            var theStep = new AlgorithmStepImpl();
            Algorithm theAlg = Algorithm.CreateWithSteps( new[] { theStep } );
            theAlg.Seal();

            // Next line will throw the exception.
            theAlg.Remove( theStep );
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
