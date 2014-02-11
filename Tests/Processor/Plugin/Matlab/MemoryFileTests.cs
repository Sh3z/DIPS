using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DIPS.Processor.Plugin.Matlab;

namespace DIPS.Tests.Processor.Plugin.Matlab
{
    /// <summary>
    /// Summary description for MemoryFileTests
    /// </summary>
    [TestClass]
    public class MemoryFileTests
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
        /// Tests ensuring the default values are set by the constructor.
        /// </summary>
        [TestMethod]
        public void TestConstructor()
        {
            MemoryFile f = new MemoryFile();

            Assert.AreEqual( string.Empty, f.Path );
            Assert.IsFalse( f.HasCopy );
            Assert.IsNotNull( f.RawCopy );
            Assert.AreEqual( 0, f.RawCopy.Length );
        }

        /// <summary>
        /// Tests setting a file that does not exist.
        /// </summary>
        [TestMethod]
        public void TestSetFile_DoesNotExist()
        {
            MemoryFile f = new MemoryFile();
            f.Path = "doesnotexist";
            f.Refresh();

            Assert.IsFalse( f.HasCopy );
            Assert.IsNotNull( f.RawCopy );
            Assert.AreEqual( 0, f.RawCopy.Length );
        }

        /// <summary>
        /// Tests setting a valid file
        /// </summary>
        [TestMethod]
        public void TestSetFile_FileExists()
        {
            string path = "TestFile.txt";
            MemoryFile f = new MemoryFile();
            f.Path = path;
            f.Refresh();

            Assert.IsTrue( f.HasCopy );
            Assert.AreEqual( path, f.Path );
            Assert.AreNotEqual( 0, f.RawCopy.Length );
        }
    }
}
