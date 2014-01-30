using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DIPS.Processor.Registry;
using DIPS.Processor.Plugin;

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
            var loadedPlugins = RegistryCache.Cache.GetLoadedPlugins();

            Assert.IsTrue( loadedPlugins.Count() > 0 );

            AlgorithmDefinition d = loadedPlugins.FirstOrDefault( x => x.AlgorithmName.ToLower() == "gamma" );
            Assert.IsNotNull( d );
        }
    }
}
