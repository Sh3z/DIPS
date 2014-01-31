using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DIPS.Util.Compression;

namespace DIPS.Tests.Util
{
    /// <summary>
    /// Summary description for GZipCompressorTests
    /// </summary>
    [TestClass]
    public class GZipCompressorTests
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
        /// Tests compressing null bytes.
        /// </summary>
        [TestMethod]
        public void TestCompress_NullBytes()
        {
            GZipCompressor c = new GZipCompressor();
            byte[] compressed = c.Compress( null );
            Assert.IsNotNull( compressed );
            Assert.AreEqual( 0, compressed.Length );
        }

        /// <summary>
        /// Tests decompressing null bytes.
        /// </summary>
        [TestMethod]
        public void TestDecompress_NullBytes()
        {
            GZipCompressor c = new GZipCompressor();
            byte[] decompressed = c.Decompress( null );
            Assert.IsNotNull( decompressed );
            Assert.AreEqual( 0, decompressed.Length );
        }

        /// <summary>
        /// Tests decompressing previously compressed bytes.
        /// </summary>
        [TestMethod]
        public void TestDecompress_CompressedBytes()
        {
            GZipCompressor c = new GZipCompressor();
            string str = "Hello";
            byte[] strBytes = System.Text.Encoding.UTF8.GetBytes( str );
            byte[] compressed = c.Compress( strBytes );
            byte[] decompressed = c.Decompress( compressed );
            string strDecompressed = System.Text.Encoding.Default.GetString( decompressed );

            Assert.AreEqual( str, strDecompressed );
        }
    }
}
