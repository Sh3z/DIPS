using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DIPS.Util.Compression;

namespace DIPS.Tests.Util
{
    /// <summary>
    /// Summary description for CompressorFactoryTests
    /// </summary>
    [TestClass]
    public class CompressorFactoryTests
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
        /// Tests manufacturing a compressor with a null name.
        /// </summary>
        [TestMethod]
        [ExpectedException( typeof( ArgumentException ) )]
        public void TestManufacture_NullName()
        {
            CompressorFactory.ManufactureCompressor( null );
        }

        /// <summary>
        /// Tests manufacturing a compressor with an empty name.
        /// </summary>
        [TestMethod]
        [ExpectedException( typeof( ArgumentException ) )]
        public void TestManufacture_EmptyName()
        {
            CompressorFactory.ManufactureCompressor( string.Empty );
        }

        /// <summary>
        /// Tests manufacturing a compressor with an unknown identifier.
        /// </summary>
        [TestMethod]
        public void TestManufacture_UnknownName()
        {
            ICompressor compressor = CompressorFactory.ManufactureCompressor( "unknown" );
            Assert.IsNull( compressor );
        }

        /// <summary>
        /// Tests manufacturing a compressor with a known identifier.
        /// </summary>
        [TestMethod]
        public void TestManufacture_KnownName()
        {
            ICompressor compressor = CompressorFactory.ManufactureCompressor( "gzip" );
            Assert.IsNotNull( compressor );
            Assert.AreEqual( typeof( GZipCompressor ), compressor.GetType() );
        }

        /// <summary>
        /// Tests resolving the identifier of a null compressor.
        /// </summary>
        [TestMethod]
        public void TestResolveIdentifier_NullCompressor()
        {
            string id = CompressorFactory.ResolveIdentifier( null );
            Assert.AreEqual( string.Empty, id );
        }
        
        /// <summary>
        /// Tests resolving the identifier of an unknown compressor.
        /// </summary>
        [TestMethod]
        public void TestResolveIdentifier_UnknownCompressor()
        {
            string id = CompressorFactory.ResolveIdentifier( new Compressor() );
            Assert.AreEqual( string.Empty, id );
        }

        /// <summary>
        /// Tests resolving the identifer of a known compressor.
        /// </summary>
        [TestMethod]
        public void TestResolveIdentifier_ValidCompressor()
        {
            ICompressor compressor = new GZipCompressor();
            string id = CompressorFactory.ResolveIdentifier( compressor );
            Assert.AreEqual( "gzip", id.ToLower() );
        }


        class Compressor : ICompressor
        {
            public byte[] Compress( byte[] toCompress )
            {
                throw new NotImplementedException();
            }

            public byte[] Decompress( byte[] toDecompress )
            {
                throw new NotImplementedException();
            }
        }
    }
}
