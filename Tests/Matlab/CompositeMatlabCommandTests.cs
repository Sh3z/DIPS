using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DIPS.Matlab;
using MLApp;
using System.Linq;

namespace DIPS.Tests.Matlab
{
    /// <summary>
    /// Summary description for CompositeMatlabCommandTests
    /// </summary>
    [TestClass]
    public class CompositeMatlabCommandTests
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
            CompositeMatlabCommand cmd = new CompositeMatlabCommand( null, new string[] {} );
        }

        /// <summary>
        /// Tests constructing a command with a null set of commands
        /// </summary>
        [TestMethod]
        [ExpectedException( typeof( ArgumentNullException ) )]
        public void TestConstructor_NullCommand()
        {
            MLAppClass matlab = MatlabTestInstance.Instance;
            MatlabSession session = new MatlabSession( matlab );
            CompositeMatlabCommand cmd = new CompositeMatlabCommand( session, null );
        }

        /// <summary>
        /// Tests executing a command after the session has invalidated
        /// </summary>
        [TestMethod]
        [ExpectedException( typeof( InvalidSessionException ) )]
        public void TestExecute_InvalidSession()
        {
            MLAppClass matlab = MatlabTestInstance.Instance;
            MatlabSession session = new MatlabSession( matlab );
            CompositeMatlabCommand cmd = new CompositeMatlabCommand( session, new string[] {} );
            session.Valid = false;

            cmd.Execute();
        }

        /// <summary>
        /// Tests executing a command in valid conditions.
        /// </summary>
        [TestMethod]
        public void TestExecute_ValidSession()
        {
            IEnumerable<string> cmds = new[] { "a=1", "b=a*2", "c=b*b" };
            MLAppClass matlab = MatlabTestInstance.Instance;
            MatlabSession session = new MatlabSession( matlab );
            CompositeMatlabCommand cmd = new CompositeMatlabCommand( session, cmds );

            cmd.Execute();
            Assert.IsFalse( cmd.Outputs.Any( x => x == null ) );
        }
    }
}
