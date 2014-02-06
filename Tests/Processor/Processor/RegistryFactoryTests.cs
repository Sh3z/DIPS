using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DIPS.Processor;
using DIPS.Processor.Plugin;
using DIPS.Processor.Client;

namespace DIPS.Tests.Processor
{
    /// <summary>
    /// Summary description for RegistryFactoryTests
    /// </summary>
    [TestClass]
    public class RegistryFactoryTests
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
        /// Tests manufacturing a plugin using an invalid definition.
        /// </summary>
        [TestMethod]
        public void TestManufacture_InvalidDefintion()
        {
            RegistryFactory factory = new RegistryFactory();
            AlgorithmPlugin p = factory.Manufacture( null );
            Assert.IsNull( p );
        }

        /// <summary>
        /// Tests manufacturing a plugin using a valid definition.
        /// </summary>
        [TestMethod]
        public void TestManufacture_ValidDefinition()
        {
            AlgorithmDefinition d = new AlgorithmDefinition( "gamma", new Property[] {} );
            RegistryFactory factory = new RegistryFactory();
            AlgorithmPlugin p = factory.Manufacture( d );
            Assert.IsNotNull( p );
        }
    }
}
