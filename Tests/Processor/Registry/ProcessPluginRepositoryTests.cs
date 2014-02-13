using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DIPS.Processor.Registry;
using DIPS.Processor.Plugin;
using DIPS.Processor.Client;

namespace DIPS.Tests.Processor.Registry
{
    /// <summary>
    /// Summary description for ProcessPluginRepositoryTests
    /// </summary>
    [TestClass]
    public class ProcessPluginRepositoryTests
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
        /// Tests loading the known plugin.
        /// </summary>
        /// <remarks>
        /// Plugins will vary on installation and this test may fail in future runs
        /// if the plugin that comes with the processor is renamed.
        /// </remarks>
        [TestMethod]
        public void TestLoadCache()
        {
            ProcessPluginRepository r = new ProcessPluginRepository();
            RegistryCache.Cache.Initialize( r );

            var loadedPlugins = r.KnownAlgorithms;

            Assert.IsTrue( loadedPlugins.Count() > 0 );

            AlgorithmDefinition d = loadedPlugins.FirstOrDefault( x => x.AlgorithmName.ToLower() == "gamma" );
            Assert.IsNotNull( d );
        }

        /// <summary>
        /// Attempts to resolve the type for an algorithm that is not defined.
        /// </summary>
        [TestMethod]
        public void TestResolveType_NullDefinition()
        {
            ProcessPluginRepository r = new ProcessPluginRepository();
            Type type = r.FetchType( null );

            Assert.IsNull( type );
        }

        /// <summary>
        /// Attempts to resolve the type for an algorithm that is cached.
        /// </summary>
        [TestMethod]
        public void TestResolveType_ValidDefinition()
        {
            ProcessPluginRepository r = new ProcessPluginRepository();
            RegistryCache.Cache.Initialize( r );
            AlgorithmDefinition d = r.KnownAlgorithms.FirstOrDefault( x => x.AlgorithmName.ToLower() == "gamma" );
            Type type = r.FetchType( d.AlgorithmName );

            Assert.IsNotNull( type );

            AlgorithmDefinition d2 = PluginReflector.CreateDefinition( type );
            Assert.IsTrue( d.Equals( d2 ) );
        }

        /// <summary>
        /// Tests determining whether the registry cache has cached an algorithm
        /// that does not exist.
        /// </summary>
        [TestMethod]
        public void TestHasCachedAlgorithm_UnknownName()
        {
            ProcessPluginRepository r = new ProcessPluginRepository();
            bool cached = r.KnowsAlgorithm( "unknown" );
            Assert.IsFalse( cached );
        }

        /// <summary>
        /// Tests determining whether the registry cache has cached an algorithm
        /// that exists.
        /// </summary>
        [TestMethod]
        public void TestHasCachedAlgorithm_KnownName()
        {
            ProcessPluginRepository r = new ProcessPluginRepository();
            RegistryCache.Cache.Initialize( r );
            bool cached = r.KnowsAlgorithm( "gamma" );
            Assert.IsTrue( cached );
        }
    }
}
