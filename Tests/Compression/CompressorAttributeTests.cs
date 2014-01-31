using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DIPS.Util.Compression;

namespace DIPS.Tests.Util
{
    /// <summary>
    /// Summary description for CompressorAttributeTests
    /// </summary>
    [TestClass]
    public class CompressorAttributeTests
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
        /// Tests the constructor with a null identifier.
        /// </summary>
        [TestMethod]
        [ExpectedException( typeof( ArgumentException ) )]
        public void TestConstructor_NullIdentifier()
        {
            CompressorAttribute a = new CompressorAttribute( null );
        }

        /// <summary>
        /// Tests the constructor with a empty identifier.
        /// </summary>
        [TestMethod]
        [ExpectedException( typeof( ArgumentException ) )]
        public void TestConstructor_EmptyIdentifier()
        {
            CompressorAttribute a = new CompressorAttribute( string.Empty );
        }

        /// <summary>
        /// Tests the constructor with a valid identifier.
        /// </summary>
        [TestMethod]
        public void TestConstructor_ValidIdentifier()
        {
            string id = "ID";
            CompressorAttribute a = new CompressorAttribute( id );
            Assert.AreEqual( id, a.Identifier );
        }
    }
}
