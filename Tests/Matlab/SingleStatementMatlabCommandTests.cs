using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DIPS.Matlab;
using MLApp;

namespace DIPS.Tests.Matlab
{
    /// <summary>
    /// Summary description for MatlabCommandTests
    /// </summary>
    [TestClass]
    public class SingleStatementMatlabCommandTests
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
        /// Tests constructing the command with a null session.
        /// </summary>
        [TestMethod]
        [ExpectedException( typeof( ArgumentNullException ) )]
        public void TestConstructor_NullSession()
        {
            SingleStatementMatlabCommand cmd = new SingleStatementMatlabCommand( null, "b=a*a" );
        }

        /// <summary>
        /// Tests constructing a command with a null command
        /// </summary>
        [TestMethod]
        [ExpectedException( typeof( ArgumentException ) )]
        public void TestConstructor_NullCommand()
        {
            MLAppClass matlab = MatlabTestInstance.Instance;
            MatlabSession session = new MatlabSession( matlab );
            SingleStatementMatlabCommand cmd = new SingleStatementMatlabCommand( session, null );
        }

        /// <summary>
        /// Tests constructing a command with valid args
        /// </summary>
        [TestMethod]
        public void TestConstructor_ValidArgs()
        {
            string cmdInput = "b=a*a";
            MLAppClass matlab = MatlabTestInstance.Instance;
            MatlabSession session = new MatlabSession( matlab );
            SingleStatementMatlabCommand cmd = new SingleStatementMatlabCommand( session, cmdInput );

            Assert.AreEqual( cmdInput, cmd.Input );
        }

        /// <summary>
        /// Tests executing a command after the session has invalidated
        /// </summary>
        [TestMethod]
        [ExpectedException( typeof( MatlabException ) )]
        public void TestExecute_InvalidSession()
        {
            string cmdInput = "b=a*a";
            MLAppClass matlab = MatlabTestInstance.Instance;
            MatlabSession session = new MatlabSession( matlab );
            SingleStatementMatlabCommand cmd = new SingleStatementMatlabCommand( session, cmdInput );
            session.Valid = false;

            cmd.Execute();
        }

        /// <summary>
        /// Tests executing a valid command.
        /// </summary>
        [TestMethod]
        public void TestExecute_ValidConditions()
        {
            string cmdInput = "b=a*a";
            MLAppClass matlab = MatlabTestInstance.Instance;
            MatlabSession session = new MatlabSession( matlab );
            SingleStatementMatlabCommand cmd = new SingleStatementMatlabCommand( session, cmdInput );

            cmd.Execute();

            // So long as Matlab tried to run something this should be regarded
            // as a success. It's up the client to make sure they called everything
            // correctly.
            Assert.IsFalse( string.IsNullOrEmpty( cmd.Output ) );
        }
    }
}
