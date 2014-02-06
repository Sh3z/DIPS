using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DIPS.Processor;
using DIPS.Processor.Persistence;
using System.Drawing;
using DIPS.Processor.Plugin;
using DIPS.Processor.Client.JobDeployment;

namespace DIPS.Tests.Processor
{
    /// <summary>
    /// Summary description for JobTests
    /// </summary>
    [TestClass]
    public class JobTests
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
        /// Tests constructing a job with a null definition.
        /// </summary>
        [TestMethod]
        [ExpectedException( typeof( ArgumentNullException ) )]
        public void TestConstructor_NullDefinition()
        {
            Job j = new Job( null );
        }

        /// <summary>
        /// Tests constructing a job with a valid definition.
        /// </summary>
        [TestMethod]
        public void TestConstructor_ValidDefinition()
        {
            JobDefinition d = new JobDefinition( new AlgorithmPlugin[] { }, new DudPersister() );
            Job j = new Job( d );
        }

        /// <summary>
        /// Tests running a job that succeeds.
        /// </summary>
        [TestMethod]
        public void TestRun_NoException()
        {
            JobInput i = new JobInput( Image.FromFile( "img.bmp" ) );
            DudPersister p = new DudPersister();
            JobDefinition d = new JobDefinition(
                new AlgorithmPlugin[] { new GoodPlugin() }, p );
            d.Inputs.Add( i );
            Job j = new Job( d );
            bool didComplete = j.Run();

            Assert.IsTrue( didComplete );
            Assert.IsNull( j.Exception );
            Assert.IsTrue( p.DidPersist );
        }

        /// <summary>
        /// Tests running a job where a plugin throws an exception.
        /// </summary>
        [TestMethod]
        public void TestRun_JobException()
        {
            JobInput i = new JobInput( Image.FromFile( "img.bmp" ) );
            JobDefinition d = new JobDefinition(
                new AlgorithmPlugin[] { new BadPlugin() }, new DudPersister() );
            d.Inputs.Add( i );
            Job j = new Job( d );
            bool didComplete = j.Run();

            Assert.IsFalse( didComplete );
            Assert.IsNotNull( j.Exception );
            Assert.AreEqual( typeof( NotImplementedException ), j.Exception.GetType() );
        }


        class DudPersister : IJobPersister
        {
            public bool DidPersist
            {
                get;
                private set;
            }

            public void Persist( Image output, object identifier )
            {
                DidPersist = true;
            }
        }

        class GoodPlugin : AlgorithmPlugin
        {
            public override void Run()
            {
                Output = Input;
            }
        }

        class BadPlugin : AlgorithmPlugin
        {
            public override void Run()
            {
                throw new NotImplementedException();
            }
        }
    }
}
