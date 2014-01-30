using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DIPS.Processor.Plugin;

namespace DIPS.Tests.Processor.Plugin
{
    /// <summary>
    /// Summary description for PluginReflectorTests
    /// </summary>
    [TestClass]
    public class PluginReflectorTests
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
        /// Tests attempting to reflect null.
        /// </summary>
        [TestMethod]
        [ExpectedException( typeof( ArgumentNullException ) )]
        public void TestCreateDefinition_NullPlugin()
        {
            PluginReflector.CreateDefinition( null );
        }

        /// <summary>
        /// Tests attempting to reflect a plugin that has not been annotated.
        /// </summary>
        [TestMethod]
        [ExpectedException( typeof( ArgumentException ) )]
        public void TestCreateDefinition_NonAnnotatedPlugin()
        {
            PluginReflector.CreateDefinition( new NonAnnotatedPlugin() );
        }

        /// <summary>
        /// Tests attempting to reflect a plugin that has been annotated, and
        /// has no properties.
        /// </summary>
        [TestMethod]
        public void TestCreateDefinition_AnnotatedNoProperties()
        {
            AlgorithmDefinition d = PluginReflector.CreateDefinition( new AnnotatedPlugin() );

            Assert.AreEqual( "Plugin", d.AlgorithmName );
            Assert.AreEqual( 0, d.Properties.Count );
        }

        /// <summary>
        /// Tests attempting to reflect a plugin that has been annotated, and has
        /// one property.
        /// </summary>
        [TestMethod]
        public void TestCreateDefinition_AnnotatedOneProperty()
        {
            AlgorithmDefinition d = PluginReflector.CreateDefinition( new AnnotatedPluginWithProperty() );

            Assert.AreEqual( "Plugin", d.AlgorithmName );
            Assert.AreEqual( 1, d.Properties.Count );

            Property p = d.Properties.First();
            Assert.AreEqual( "Value", p.Name );
            Assert.AreEqual( typeof( double ), p.Type );
        }


        class NonAnnotatedPlugin : AlgorithmPlugin
        {
            public override void Run()
            {
                throw new NotImplementedException();
            }
        }

        [PluginIdentifier( "Plugin" )]
        class AnnotatedPlugin : AlgorithmPlugin
        {
            public override void Run()
            {
                throw new NotImplementedException();
            }
        }

        [PluginIdentifier( "Plugin" )]
        class AnnotatedPluginWithProperty : AlgorithmPlugin
        {
            [PluginVariable( "Value" )]
            public double Value
            {
                get;
                set;
            }

            public override void Run()
            {
                throw new NotImplementedException();
            }
        }
    }
}
