using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DIPS.Matlab;
using MLApp;

namespace DIPS.Tests.Matlab
{
    /// <summary>
    /// Summary description for MatlabSessionTests
    /// </summary>
    [TestClass]
    public class MatlabSessionTests
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
        /// Tests constructing a session with no matlab object
        /// </summary>
        [TestMethod]
        [ExpectedException( typeof( ArgumentNullException ) )]
        public void TestConstructor_NullMatlab()
        {
            MatlabSession session = new MatlabSession( null );
        }

        /// <summary>
        /// Tests constructing a session with a valid matlab object
        /// </summary>
        [TestMethod]
        public void TestConstructor_ValidMatlab()
        {
            MLAppClass matlab = MatlabTestInstance.Instance;
            MatlabSession session = new MatlabSession( matlab );

            Assert.AreEqual( matlab, session.Matlab );
            Assert.IsTrue( session.Valid );
        }

        /// <summary>
        /// Tests invalidating a session
        /// </summary>
        [TestMethod]
        public void TestInvalidate()
        {
            MLAppClass matlab = MatlabTestInstance.Instance;
            MatlabSession session = new MatlabSession( matlab );
            bool validityChangedFired = true;
            session.SessionValidityChanged += ( s, e ) => validityChangedFired = true;

            session.Valid = false;
            Assert.IsFalse( session.Valid );
            Assert.IsTrue( validityChangedFired );
        }
    }
}
