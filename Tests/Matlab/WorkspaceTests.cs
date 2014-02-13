using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DIPS.Matlab;
using MLApp;
using System.Diagnostics;

namespace DIPS.Tests.Matlab
{
    /// <summary>
    /// Summary description for WorkspaceTests
    /// </summary>
    [TestClass]
    public class WorkspaceTests
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
        /// Tests creating a workspace with a null name
        /// </summary>
        [TestMethod]
        [ExpectedException( typeof( ArgumentException ) )]
        public void TestConstructor_NullName()
        {
            MLAppClass app = MatlabTestInstance.Instance;
            MatlabSession session = new MatlabSession( app );
            Workspace space = new Workspace( session, null );
        }

        /// <summary>
        /// Tests creating a workspace with an empty name
        /// </summary>
        [TestMethod]
        [ExpectedException( typeof( ArgumentException ) )]
        public void TestConstructor_EmptyName()
        {
            MLAppClass app = MatlabTestInstance.Instance;
            MatlabSession session = new MatlabSession( app );
            Workspace space = new Workspace( session, string.Empty );
        }

        /// <summary>
        /// Tests creating a workspace without a session.
        /// </summary>
        [TestMethod]
        [ExpectedException( typeof( ArgumentNullException ) )]
        public void TestConstructor_NullSession()
        {
            Workspace space = new Workspace( null, "Base" );
        }

        /// <summary>
        /// Tests creating a workspace with valid arguments
        /// </summary>
        [TestMethod]
        public void TestConstructor_ValidArgs()
        {
            string workspaceName = "Base";
            MLAppClass app = MatlabTestInstance.Instance;
            MatlabSession session = new MatlabSession( app );
            Workspace space = new Workspace( session, workspaceName );

            Assert.AreEqual( workspaceName, space.WorkspaceName );
        }

        /// <summary>
        /// Tests placing an object onto the workspace when the session has expired.
        /// </summary>
        [TestMethod]
        [ExpectedException( typeof( InvalidSessionException ) )]
        public void TestPutObject_ExpiredSession()
        {
            string workspaceName = "Base";
            MLAppClass app = MatlabTestInstance.Instance;
            MatlabSession session = new MatlabSession( app );
            Workspace space = new Workspace( session, workspaceName );
            session.Valid = false;

            space.PutObject( "Obj", new object() );
        }
        
        /// <summary>
        /// Tests placing an object onto the workspace with no name.
        /// </summary>
        [TestMethod]
        [ExpectedException( typeof( ArgumentException ) )]
        public void TestPutObject_NullName()
        {
            string workspaceName = "Base";
            MLAppClass app = MatlabTestInstance.Instance;
            MatlabSession session = new MatlabSession( app );
            Workspace space = new Workspace( session, workspaceName );

            space.PutObject( null, new object() );
        }

        /// <summary>
        /// Tests placing an object onto the workspace.
        /// </summary>
        [TestMethod]
        public void TestPutObject_ValidArgs()
        {
            string workspaceName = "Base";
            MLAppClass app = MatlabTestInstance.Instance;
            MatlabSession session = new MatlabSession( app );
            Workspace space = new Workspace( session, workspaceName );

            space.PutObject( "Obj", new object() );
            string output = app.Execute( "exist Obj" );
            Debug.Write( output );
        }

        /// <summary>
        /// Attempts getting an object from an expired workspace.
        /// </summary>
        [TestMethod]
        [ExpectedException( typeof( InvalidSessionException ) )]
        public void TestGetVariable_ExpiredSession()
        {
            string workspaceName = "base";
            MLAppClass app = MatlabTestInstance.Instance;
            MatlabSession session = new MatlabSession( app );
            Workspace space = new Workspace( session, workspaceName );
            session.Valid = false;

            space.GetVariable( "Obj" );
        }

        /// <summary>
        /// Tests retrieving the value of an unidentified variable.
        /// </summary>
        [TestMethod]
        [ExpectedException( typeof( ArgumentException ) )]
        public void TestGetVariable_NullName()
        {
            string workspaceName = "base";
            MLAppClass app = MatlabTestInstance.Instance;
            MatlabSession session = new MatlabSession( app );
            Workspace space = new Workspace( session, workspaceName );

            space.GetVariable( null );
        }

        /// <summary>
        /// Tests accessing a variable no value has been set for
        /// </summary>
        [TestMethod]
        [ExpectedException( typeof( MatlabException ) )]
        public void TestGetVariable_NotSet()
        {
            string workspaceName = "base";
            MLAppClass app = MatlabTestInstance.Instance;
            MatlabSession session = new MatlabSession( app );
            Workspace space = new Workspace( session, workspaceName );

            space.GetVariable( "obj" );
        }

        /// <summary>
        /// Tests accessing a variable that a value has been set for
        /// </summary>
        [TestMethod]
        public void TestGetVariable_Set()
        {
            string workspaceName = "base";
            MLAppClass app = MatlabTestInstance.Instance;
            MatlabSession session = new MatlabSession( app );
            Workspace space = new Workspace( session, workspaceName );

            space.PutObject( "a", 1d );
            app.Execute( "b=a*2" );
            object output = space.GetVariable( "b" );

            Assert.IsNotNull( output );
            Assert.AreEqual( 2d, output );
        }
    }
}
