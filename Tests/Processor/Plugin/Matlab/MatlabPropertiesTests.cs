using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DIPS.Processor.Plugin.Matlab;

namespace DIPS.Tests.Processor.Plugin.Matlab
{
    /// <summary>
    /// Summary description for MatlabPropertiesTests
    /// </summary>
    [TestClass]
    public class MatlabPropertiesTests
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
        /// Tests setting a file against the MatlabProperties that does not
        /// exist.
        /// </summary>
        [TestMethod]
        public void TestSetFile_DoesNotExist()
        {
            MatlabProperties p = new MatlabProperties();
            p.ScriptFile = "doesnotexist";

            Assert.IsFalse( p.HasScript );
            Assert.IsNotNull( p.SerializedFile );
            Assert.AreEqual( 0, p.SerializedFile.Length );
        }

        /// <summary>
        /// Tests setting a valid file against the MatlabProperties object
        /// </summary>
        [TestMethod]
        public void TestSetFile_FileExists()
        {
            MatlabProperties p = new MatlabProperties();
            p.ScriptFile = "TestFile.txt";

            Assert.IsTrue( p.HasScript );
            Assert.AreNotEqual( 0, p.SerializedFile.Length );
        }
    }
}
