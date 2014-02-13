using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DIPS.Processor;
using DIPS.Processor.Registry;
using DIPS.Processor.Plugin.Base;
using DIPS.Processor.Client;
using System.Linq;
using DIPS.Processor.Pipeline;

namespace DIPS.Tests.Processor
{
    /// <summary>
    /// Summary description for PipelineManagerTests
    /// </summary>
    [TestClass]
    public class PipelineManagerTests
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
        /// Tests saving null processes into Xml
        /// </summary>
        [TestMethod]
        public void TestSavePipeline_NullProcessess()
        {
            ProcessPluginRepository algRepo = new ProcessPluginRepository();
            PipelineXmlRepository pipeRepo = new PipelineXmlRepository();
            RegistryCache.Cache.Initialize( pipeRepo );
            RegistryCache.Cache.Initialize( algRepo );
            PipelineManager m = new PipelineManager( pipeRepo, algRepo );
            var xml = m.SavePipeline( null );

            Assert.IsNull( xml );
        }

        /// <summary>
        /// Tests saving a valid process into Xml.
        /// </summary>
        [TestMethod]
        public void TestSavePipeline_ValidProcess()
        {
            GammaProperties p = new GammaProperties() { Gamma = 3 };
            AlgorithmDefinition def = new AlgorithmDefinition( "gamma", new Property[] { } );
            def.ParameterObject = p;
            ProcessPluginRepository algRepo = new ProcessPluginRepository();
            PipelineXmlRepository pipeRepo = new PipelineXmlRepository();
            RegistryCache.Cache.Initialize( pipeRepo );
            RegistryCache.Cache.Initialize( algRepo );
            PipelineManager m = new PipelineManager( pipeRepo, algRepo );
            var xml = m.SavePipeline( new AlgorithmDefinition[] { def } );

            // Todo - additional test logic beyond not expecting exceptions
        }

        /// <summary>
        /// Tests restoring a pipeline from nothing
        /// </summary>
        [TestMethod]
        public void TestRestorePipline_NullXml()
        {
            ProcessPluginRepository algRepo = new ProcessPluginRepository();
            PipelineXmlRepository pipeRepo = new PipelineXmlRepository();
            RegistryCache.Cache.Initialize( pipeRepo );
            RegistryCache.Cache.Initialize( algRepo );
            PipelineManager m = new PipelineManager( pipeRepo, algRepo );
            var pipe = m.RestorePipeline( null );

            Assert.IsNotNull( pipe );
            Assert.AreEqual( 0, pipe.Count() );
        }

        /// <summary>
        /// Tests restoring a pipeline from valid Xml
        /// </summary>
        [TestMethod]
        public void RestorePipe_ValidXml()
        {
            GammaProperties p = new GammaProperties() { Gamma = 3 };
            AlgorithmDefinition def = new AlgorithmDefinition( "gamma", new Property[] { } );
            def.ParameterObject = p;
            ProcessPluginRepository algRepo = new ProcessPluginRepository();
            PipelineXmlRepository pipeRepo = new PipelineXmlRepository();
            RegistryCache.Cache.Initialize( pipeRepo );
            RegistryCache.Cache.Initialize( algRepo );
            PipelineManager m = new PipelineManager( pipeRepo, algRepo );
            var xml = m.SavePipeline( new AlgorithmDefinition[] { def } );
            var pipes = m.RestorePipeline( xml );

            Assert.AreEqual( 1, pipes.Count() );

            AlgorithmDefinition d = pipes.First();
            Assert.AreEqual( def.AlgorithmName, d.AlgorithmName );
            Assert.IsNotNull( def.ParameterObject );
            Assert.AreEqual( typeof( GammaProperties ), d.ParameterObject.GetType() );
            Assert.AreEqual( 3, ( (GammaProperties)d.ParameterObject ).Gamma );
        }
    }
}
