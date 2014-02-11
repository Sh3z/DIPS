using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DIPS.Matlab;

namespace DIPS.Tests.Matlab
{
    /// <summary>
    /// Summary description for MatlabEngineTests
    /// </summary>
    [TestClass]
    public class MatlabEngineTests
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
        /// Tests creating a command without any command text
        /// </summary>
        [TestMethod]
        [ExpectedException( typeof( ArgumentException ) )]
        public void TestCreateCommand_NullCommand()
        {
            MatlabEngine engine = new MatlabEngine();
            engine.CreateCommand( null );
        }

        /// <summary>
        /// Tests creating a command when the session is invalid.
        /// </summary>
        [TestMethod]
        [ExpectedException( typeof( MatlabException ) )]
        public void TestCreateCommand_InvalidSession()
        {
            string cmdText = "b=a*a";
            MatlabEngine engine = new MatlabEngine();
            engine.Shutdown();
            engine.CreateCommand( cmdText );
        }

        /// <summary>
        /// Tests creating a command with valid command text
        /// </summary>
        [TestMethod]
        public void TestCreateCommand_ValidArgs()
        {
            string cmdText = "b=a*a";
            MatlabEngine engine = new MatlabEngine();
            MatlabCommand cmd = engine.CreateCommand( cmdText );

            Assert.AreEqual( cmdText, cmd.Input );
        }
    }
}
