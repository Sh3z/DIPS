using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DIPS.Processor.Registry;
using DIPS.Processor.Plugin;
using DIPS.Processor.Client;

namespace DIPS.Tests.Processor.Registry
{
    /// <summary>
    /// Summary description for RegistryCacheTests
    /// </summary>
    [TestClass]
    public class RegistryCacheTests
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
            var loadedPlugins = RegistryCache.Cache.KnownAlgorithms;

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
            Type type = RegistryCache.Cache.FetchType( null );

            Assert.IsNull( type );
        }

        /// <summary>
        /// Attempts to resolve the type for an algorithm that is cached.
        /// </summary>
        [TestMethod]
        public void TestResolveType_ValidDefinition()
        {
            AlgorithmDefinition d = RegistryCache.Cache.KnownAlgorithms.FirstOrDefault( x => x.AlgorithmName.ToLower() == "gamma" );
            Type type = RegistryCache.Cache.FetchType( d.AlgorithmName );

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
            bool cached = RegistryCache.Cache.KnowsAlgorithm( "unknown" );
            Assert.IsFalse( cached );
        }

        /// <summary>
        /// Tests determining whether the registry cache has cached an algorithm
        /// that exists.
        /// </summary>
        [TestMethod]
        public void TestHasCachedAlgorithm_KnownName()
        {
            bool cached = RegistryCache.Cache.KnowsAlgorithm( "gamma" );
            Assert.IsTrue( cached );
        }
    }
}
