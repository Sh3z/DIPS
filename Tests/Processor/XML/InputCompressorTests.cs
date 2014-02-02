using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DIPS.Processor.XML;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Diagnostics;
using DIPS.Processor.XML.Compilation;

namespace DIPS.Tests.Processor.XML
{
    /// <summary>
    /// Summary description for InputCompressorTests
    /// </summary>
    [TestClass]
    public class InputCompressorTests
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InputCompressorTests"/>.
        /// </summary>
        public InputCompressorTests()
        {
            TestFileName = "compression_test.bmp";
        }


        /// <summary>
        /// Gets or sets the test context which provides
        /// information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get;
            set;
        }

        private Image _createTestImage()
        {
            return new Bitmap( Image.FromFile( TestFileName ) );
        }


        /// <summary>
        /// Gets or sets the name of the test file used for Bitmap tests.
        /// </summary>
        public string TestFileName
        {
            get;
            set;
        }

        /// <summary>
        /// Tests attempting to get the bytes out of a null Image.
        /// </summary>
        [TestMethod]
        public void TestImageToBytes_NullImage()
        {
            byte[] bytes = InputCompressor.ImageToBytes( null );
            Assert.IsNotNull( bytes );
            Assert.AreEqual( 0, bytes.Length );
        }

        /// <summary>
        /// Tests getting the bytes from a valid Image.
        /// </summary>
        [TestMethod]
        public void TestImageToByte_ValidImage()
        {
            Image testImg = _createTestImage();
            byte[] imgBytes = InputCompressor.ImageToBytes( testImg );
            Assert.IsNotNull( imgBytes );
            
            // Todo - further assertions.
        }

        /// <summary>
        /// Tests attempting to compress a null Bitmap.
        /// </summary>
        [TestMethod]
        public void TestCompress_NullBitmap()
        {
            byte[] bytes = InputCompressor.Compress( null );
            Assert.IsNotNull( bytes );
            Assert.AreEqual( 0, bytes.Length );
        }

        /// <summary>
        /// Tests attempting to compress a valid Bitmap.
        /// </summary>
        [TestMethod]
        public void TestCompress_ValidBitmap()
        {
            Image test = _createTestImage();
            byte[] bytes = InputCompressor.Compress( test );

            // Assert is smaller by comparing the number of bytes in the compressed
            // result is less than that of the original image
            byte[] originalBytes = InputCompressor.ImageToBytes( test );

            Debug.WriteLine( string.Format( "Original size: {0}", originalBytes.Length ) );
            Debug.WriteLine( string.Format( "Compressed size: {0}", bytes.Length ) );
            Assert.IsTrue( bytes.Length < originalBytes.Length );
        }

        /// <summary>
        /// Tests attempting to decompress null bytes.
        /// </summary>
        [TestMethod]
        [ExpectedException( typeof( ArgumentNullException ) )]
        public void TestDecompress_NullBytes()
        {
            Image decompressed = InputCompressor.Decompress( null );
        }

        /// <summary>
        /// Tests attempting to decompress raw bytes that do not form an Image.
        /// </summary>
        [TestMethod]
        [ExpectedException( typeof( ArgumentException ) )]
        public void TestDecompress_NotAnImage()
        {
            // Use the bytes of another file.
            byte[] notImageBytes = File.ReadAllBytes( "NotAnImage.txt" );
            Image thisWillFail = InputCompressor.Decompress( notImageBytes );
        }

        /// <summary>
        /// Tests attempting to decompress a validly-compressed imaged.
        /// </summary>
        [TestMethod]
        public void TestDecompress_ValidImage()
        {
            // Grab an image, compress it, decompress and assert the bytes are the same.
            Image img = _createTestImage();
            byte[] imgBytes = InputCompressor.ImageToBytes( img );
            byte[] compressed = InputCompressor.Compress( img );
            Image decompressed = InputCompressor.Decompress( compressed );
            byte[] decompressedBytes = InputCompressor.ImageToBytes( decompressed );

            Assert.AreEqual( imgBytes.Length, decompressedBytes.Length );

            for( int index = 0; index < imgBytes.Length; index++ )
            {
                Assert.AreEqual( imgBytes[index], decompressedBytes[index] );
            }
        }
    }
}
