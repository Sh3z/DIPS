using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DIPS.Processor.Pipeline;
using DIPS.Processor;
using DIPS.Processor.Persistence;
using DIPS.Processor.Client;
using DIPS.Processor.Plugin;
using System.Drawing;
using System.Linq;

namespace DIPS.Tests.Processor
{
    /// <summary>
    /// Summary description for PluginPipelineFactoryTests
    /// </summary>
    [TestClass]
    public class PluginPipelineFactoryTests
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
        /// Tests constructing the factory without the plugin factory
        /// </summary>
        [TestMethod]
        [ExpectedException( typeof( ArgumentNullException ) )]
        public void TestConstructor_NullFactory()
        {
            PluginPipelineFactory p = new PluginPipelineFactory( null );
        }

        /// <summary>
        /// Tests creating a pipeline from a null definition
        /// </summary>
        [TestMethod]
        public void TestCreatePipeline_NullDefinition()
        {
            IPluginFactory f = new DudFactory();
            PluginPipelineFactory p = new PluginPipelineFactory( f );
            Pipeline pipe = p.CreatePipeline( null );

            Assert.IsNotNull( pipe );
            Assert.AreEqual( 0, pipe.Count );
        }

        /// <summary>
        /// Tests creating a pipeline with a definition with no inner definitions
        /// </summary>
        [TestMethod]
        public void TestCreatePipeline_NoAlgorithms()
        {
            IPluginFactory f = new DudFactory();
            PluginPipelineFactory p = new PluginPipelineFactory( f );
            Pipeline pipe = p.CreatePipeline( new PipelineDefinition() );

            Assert.IsNotNull( pipe );
            Assert.AreEqual( 0, pipe.Count );
        }

        /// <summary>
        /// Tests creating a pipeline where an exception occurs while creating
        /// the plugin
        /// </summary>
        [TestMethod]
        public void TestCreatePipeline_ExceptionCreatingPlugin()
        {
            IPluginFactory f = new BadFactory();
            PipelineDefinition d = new PipelineDefinition();
            AlgorithmDefinition def = new AlgorithmDefinition( "Test", new Property[] { } );
            d.Add( def );
            PluginPipelineFactory p = new PluginPipelineFactory( f );
            Pipeline pipe = p.CreatePipeline( d );

            Assert.IsNotNull( pipe );
            Assert.AreEqual( 0, pipe.Count );
        }

        /// <summary>
        /// Tests creating a pipeline where a factory gives a valid definitions
        /// </summary>
        [TestMethod]
        public void TestCreatePipeline_ValidInnerDefinition()
        {
            IPluginFactory f = new DudFactory();
            PipelineDefinition d = new PipelineDefinition();
            AlgorithmDefinition def = new AlgorithmDefinition( "Test", new Property[] { } );
            d.Add( def );
            PluginPipelineFactory p = new PluginPipelineFactory( f );
            Pipeline pipe = p.CreatePipeline( d );

            Assert.AreEqual( 1, pipe.Count );
            PipelineEntry e = pipe.First();
            Assert.AreEqual( typeof( DudPlugin ), e.Process.GetType() );
        }


        class DudFactory : IPluginFactory
        {
            public DIPS.Processor.Plugin.AlgorithmPlugin Manufacture( DIPS.Processor.Client.AlgorithmDefinition algorithm )
            {
                return new DudPlugin();
            }
        }

        class BadFactory : IPluginFactory
        {
            public DIPS.Processor.Plugin.AlgorithmPlugin Manufacture( AlgorithmDefinition algorithm )
            {
                throw new NotImplementedException();
            }
        }

        class DudPlugin : AlgorithmPlugin
        {
            public override void Run( object obj )
            {
                throw new NotImplementedException();
            }
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
