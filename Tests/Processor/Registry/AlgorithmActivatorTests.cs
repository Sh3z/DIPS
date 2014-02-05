using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DIPS.Processor.Plugin;
using DIPS.Processor.Registry;
using DIPS.Processor.Client;

namespace DIPS.Tests.Processor.Registry
{
    /// <summary>
    /// Summary description for AlgorithmActivatorTests
    /// </summary>
    [TestClass]
    public class AlgorithmActivatorTests
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
        /// Tests checking if null can be activated.
        /// </summary>
        [TestMethod]
        public void TestCanActivate_NullDefinition()
        {
            AlgorithmActivator activator = new AlgorithmActivator();
            bool canActivate = activator.CanActivate( null );
            Assert.IsFalse( canActivate );
        }

        /// <summary>
        /// Tests checking if an unregistered algorithm can be activated.
        /// </summary>
        [TestMethod]
        public void TestCanActivate_NotCached()
        {
            AlgorithmActivator activator = new AlgorithmActivator();
            AlgorithmDefinition d = new AlgorithmDefinition( "unknown", new Property[] { } );
            bool canActivate = activator.CanActivate( d );
            Assert.IsFalse( canActivate );
        }

        /// <summary>
        /// Tests checking if a plugin without a parameterless constructor can
        /// be activated.
        /// </summary>
        [TestMethod]
        public void TestCanActivate_NoParameterlessConstructor()
        {
            Assert.Inconclusive( "Cannot unit test at present." );
        }

        /// <summary>
        /// Tests checking if a valid algorithm can be activated.
        /// </summary>
        [TestMethod]
        public void TestCanActivate_ValidDefinition()
        {
            AlgorithmActivator activator = new AlgorithmActivator();
            AlgorithmDefinition d = new AlgorithmDefinition( "gamma",
                new Property[] { new Property( "gamma", typeof( double ) ) } );
            bool canActivate = activator.CanActivate( d );
            Assert.IsTrue( canActivate );
        }

        /// <summary>
        /// Tests activating a null plugin.
        /// </summary>
        [TestMethod]
        [ExpectedException( typeof( ArgumentException ) )]
        public void TestActivate_NullDefinition()
        {
            AlgorithmActivator activator = new AlgorithmActivator();
            activator.Activate( null );
        }

        /// <summary>
        /// Tests activating a plugin with no properties.
        /// </summary>
        [TestMethod]
        public void TestActivate_NoProperties()
        {
            AlgorithmActivator activator = new AlgorithmActivator();
            AlgorithmDefinition d = new AlgorithmDefinition( "gamma", new Property[] {} );
            AlgorithmPlugin p = activator.Activate( d );

            Assert.IsNotNull( p );

            // Todo more testing logic.
        }

        /// <summary>
        /// Tests activating a plugin with a property that is unknown to the
        /// plugin.
        /// </summary>
        [TestMethod]
        public void TestActivate_OneProperty_UnknownName()
        {
            AlgorithmActivator activator = new AlgorithmActivator();
            AlgorithmDefinition d = new AlgorithmDefinition( "gamma",
                new Property[] { new Property( "unknown", typeof( string ) ) } );
            AlgorithmPlugin p = activator.Activate( d );

            Assert.IsNotNull( p );

            // Todo more testing logic.
        }

        /// <summary>
        /// Tests activating a plugin with a property that has the wrong type.
        /// </summary>
        [TestMethod]
        public void TestActivate_OneProperty_UnknownType()
        {
            AlgorithmActivator activator = new AlgorithmActivator();
            AlgorithmDefinition d = new AlgorithmDefinition( "gamma",
                new Property[] { new Property( "gamma", typeof( string ) ) } );
            AlgorithmPlugin p = activator.Activate( d );

            Assert.IsNotNull( p );

            // Todo more testing logic.
        }
    }
}
