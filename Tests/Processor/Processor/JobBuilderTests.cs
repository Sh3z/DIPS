using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DIPS.Processor;
using DIPS.Processor.Client;
using DIPS.Processor.Client.JobDeployment;
using System.Drawing;
using DIPS.Processor.Persistence;
using DIPS.Processor.Plugin;

namespace DIPS.Tests.Processor
{
    /// <summary>
    /// Summary description for JobBuilderTests
    /// </summary>
    [TestClass]
    public class JobBuilderTests
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
        /// Tests attempting to construct a JobBuilder without a plugin factory
        /// </summary>
        [TestMethod]
        [ExpectedException( typeof( ArgumentNullException ) )]
        public void TestConstructor_NullFactory()
        {
            JobBuilder b = new JobBuilder( null );
        }

        /// <summary>
        /// Tests attempting to construct a JobBuilder with a valid factory
        /// </summary>
        [TestMethod]
        public void TestConstructor_ValidFactory()
        {
            IPluginFactory factory = new DudFactory();
            JobBuilder b = new JobBuilder( factory );

            Assert.AreEqual( 0, b.AlgorithmCount );
            Assert.AreEqual( 0, b.InputCount );
        }

        /// <summary>
        /// Tests appending a null algorithm.
        /// </summary>
        [TestMethod]
        public void TestAppendAlgorithm_NullAlgorithm()
        {
            IPluginFactory factory = new DudFactory();
            JobBuilder b = new JobBuilder( factory );
            b.AppendAlgorithm( null );

            Assert.AreEqual( 0, b.AlgorithmCount );
        }

        /// <summary>
        /// Tests appending a valid algorithm.
        /// </summary>
        [TestMethod]
        public void TestAppendAlgorithm_ValidAlgorithm()
        {
            IPluginFactory factory = new DudFactory();
            JobBuilder b = new JobBuilder( factory );
            b.AppendAlgorithm( new AlgorithmDefinition( "Test", new Property[] { } ) );

            Assert.AreEqual( 1, b.AlgorithmCount );
        }

        /// <summary>
        /// Tests appending a null input
        /// </summary>
        [TestMethod]
        public void TestAppendInput_NullInput()
        {
            IPluginFactory factory = new DudFactory();
            JobBuilder b = new JobBuilder( factory );
            b.AppendInput( null );

            Assert.AreEqual( 0, b.InputCount );
        }

        /// <summary>
        /// Tests appending a valid input.
        /// </summary>
        [TestMethod]
        public void TestAppendInput_ValidInput()
        {
            IPluginFactory factory = new DudFactory();
            JobBuilder b = new JobBuilder( factory );
            JobInput i = new JobInput( Image.FromFile( "img.bmp" ) );
            b.AppendInput( i );

            Assert.AreEqual( 1, b.InputCount );
        }

        /// <summary>
        /// Tests applying a null definition
        /// </summary>
        [TestMethod]
        public void TestApplyDefinition_NullDefinition()
        {
            IPluginFactory factory = new DudFactory();
            JobBuilder b = new JobBuilder( factory );
            b.ApplyDefinition( null );

            Assert.AreEqual( 0, b.AlgorithmCount );
            Assert.AreEqual( 0, b.InputCount );
        }

        /// <summary>
        /// Tests applying a valid definition.
        /// </summary>
        [TestMethod]
        public void TestApplyDefinition_ValidDefinition()
        {
            ObjectJobDefinition d = new ObjectJobDefinition(
                new[] { new AlgorithmDefinition( "Test", new Property[] { } ) },
                new[] { new JobInput( Image.FromFile( "img.bmp" ) ) } );
            IPluginFactory factory = new DudFactory();
            JobBuilder b = new JobBuilder( factory );
            b.ApplyDefinition( d );

            Assert.AreEqual( 1, b.AlgorithmCount );
            Assert.AreEqual( 1, b.InputCount );
        }

        /// <summary>
        /// Tests building a Job with valid params, but no persister.
        /// </summary>
        [TestMethod]
        public void TestBuild_NoPersister()
        {
            ObjectJobDefinition d = new ObjectJobDefinition(
                new[] { new AlgorithmDefinition( "Test", new Property[] { } ) },
                new[] { new JobInput( Image.FromFile( "img.bmp" ) ) } );
            IPluginFactory factory = new DudFactory();
            JobBuilder b = new JobBuilder( factory );
            b.ApplyDefinition( d );
            bool built = b.Build();

            Assert.IsFalse( built );
        }

        /// <summary>
        /// Tests building a job with no algorithms.
        /// </summary>
        [TestMethod]
        public void TestBuild_NoAlgorithms()
        {
            IJobPersister p = new DudPersister();
            ObjectJobDefinition d = new ObjectJobDefinition(
                new AlgorithmDefinition[] {},
                new[] { new JobInput( Image.FromFile( "img.bmp" ) ) } );
            IPluginFactory factory = new DudFactory();
            JobBuilder b = new JobBuilder( factory );
            b.Persister = p;
            b.ApplyDefinition( d );
            bool built = b.Build();

            Assert.IsFalse( built );
        }

        /// <summary>
        /// Tests building a valid job.
        /// </summary>
        [TestMethod]
        public void TestBuild_ValidBuild()
        {
            IJobPersister p = new DudPersister();
            ObjectJobDefinition d = new ObjectJobDefinition(
                new[] { new AlgorithmDefinition( "Test", new Property[] { } ) },
                new[] { new JobInput( Image.FromFile( "img.bmp" ) ) } );
            IPluginFactory factory = new DudFactory();
            JobBuilder b = new JobBuilder( factory );
            b.Persister = p;
            b.ApplyDefinition( d );
            bool built = b.Build();

            Assert.IsTrue( built );
        }


        class DudFactory : IPluginFactory
        {
            public DIPS.Processor.Plugin.AlgorithmPlugin Manufacture( DIPS.Processor.Client.AlgorithmDefinition algorithm )
            {
                return new DudPlugin();
            }
        }

        class DudPlugin : AlgorithmPlugin
        {
            public override void Run()
            {
                throw new NotImplementedException();
            }
        }

        class DudPersister : IJobPersister
        {
            public void Persist( Image output, object identifier )
            {
                throw new NotImplementedException();
            }

            public PersistedResult Load( object identifier )
            {
                throw new NotImplementedException();
            }

            public IEnumerable<PersistedResult> Load()
            {
                throw new NotImplementedException();
            }
        }
    }
}
