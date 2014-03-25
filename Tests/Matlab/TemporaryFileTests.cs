using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DIPS.Matlab;
using System.IO;

namespace DIPS.Tests.Matlab
{
    /// <summary>
    /// Summary description for TemporaryFileTests
    /// </summary>
    [TestClass]
    public class TemporaryFileTests
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
        /// Tests creating a temporary file with no name.
        /// </summary>
        [TestMethod]
        [ExpectedException( typeof( ArgumentException ) )]
        public void TestConstructor_NullName()
        {
            byte[] bytes = new byte[0];
            TemporaryFile f = new TemporaryFile( null, bytes );
        }

        /// <summary>
        /// Tests creating a temporary file with an empty name
        /// </summary>
        [TestMethod]
        [ExpectedException( typeof( ArgumentException ) )]
        public void TestConstructor_EmptyName()
        {
            byte[] bytes = new byte[0];
            TemporaryFile f = new TemporaryFile( string.Empty, bytes );
        }

        /// <summary>
        /// Tests creating a temporary file with null bytes
        /// </summary>
        [TestMethod]
        [ExpectedException( typeof( ArgumentNullException ) )]
        public void TestConstructor_NullBytes()
        {
            string name = "Test.txt";
            TemporaryFile f = new TemporaryFile( name, null );
        }

        /// <summary>
        /// Tests creating a valid temporary file. This test will release the
        /// file.
        /// </summary>
        [TestMethod]
        public void TestConstructor_Valid_NonAutoRelease()
        {
            string name = "Test.txt";
            byte[] bytes = File.ReadAllBytes( "TestFile.txt" );
            TemporaryFile f = new TemporaryFile( name, bytes );

            Assert.IsTrue( File.Exists( f.FilePath ) );

            File.Delete( f.FilePath );
        }

        /// <summary>
        /// Tests creating a valid temporary file and auto-releasing the file.
        /// </summary>
        [TestMethod]
        public void TestConstructor_Valid_AutoRelease()
        {
            string name = "Test.txt";
            byte[] bytes = File.ReadAllBytes( "TestFile.txt" );
            string path = string.Empty;
            using( TemporaryFile f = new TemporaryFile( name, bytes ) )
            {
                path = f.FilePath;
                Assert.IsTrue( File.Exists( path ) );
            }

            Assert.IsFalse( File.Exists( path ) );
        }
    }
}
