using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DIPS.Processor.Client;
using DIPS.Processor.XML.Decompilation;
using System.Xml.Linq;
using System.Linq;
using System.IO;
using DIPS.Processor.Client.JobDeployment;
using DIPS.Util.Compression;
using System.Drawing;

namespace DIPS.Tests.Processor.XML
{
    /// <summary>
    /// Summary description for JobXmlDecompilerTests
    /// </summary>
    [TestClass]
    public class JobXmlDecompilerTests
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
        /// Tests decompiling an algorithm from a node that is not an XElement
        /// </summary>
        [TestMethod]
        [ExpectedException( typeof( ArgumentException ) )]
        public void TestDecompileAlgorithm_NotElement()
        {
            XText t = new XText( "Test" );
            JobXmlDecompiler d = new JobXmlDecompiler();
            d.DecompileAlgorithm( t );
        }

        /// <summary>
        /// Tests decompiling an algorithm with no properties.
        /// </summary>
        [TestMethod]
        public void TestDecompileAlgorithm_NoProperties()
        {
            JobXmlDecompiler decompiler = new JobXmlDecompiler();
            XElement xml = new XElement( "algorithm",
                new XAttribute( "name", "gamma" ) );
            AlgorithmDefinition a = decompiler.DecompileAlgorithm( xml );

            Assert.AreEqual( "gamma", a.AlgorithmName );
            Assert.AreEqual( 0, a.Properties.Count );
        }

        /// <summary>
        /// Tests decompiling an algorithm with one property of a primitive type.
        /// </summary>
        [TestMethod]
        public void TestDecompileAlgorithm_OneProperty_Primitive()
        {
            JobXmlDecompiler decompiler = new JobXmlDecompiler();
            XElement xml = new XElement( "algorithm",
                new XAttribute( "name", "gamma" ),
                new XElement( "properties",
                    new XElement( "property",
                        new XAttribute( "name", "gamma" ),
                        new XAttribute( "type", typeof( double ) ),
                        new XAttribute( "value", "1" ) ) ) );
            AlgorithmDefinition a = decompiler.DecompileAlgorithm( xml );

            Assert.AreEqual( 1, a.Properties.Count );

            Property gamma = a.Properties.First();
            Assert.AreEqual( "gamma", gamma.Name );
            Assert.AreEqual( typeof( double ), gamma.Type );
            Assert.IsNull( gamma.Converter );
            Assert.IsNull( gamma.Compressor );
            Assert.AreEqual( 1d, gamma.Value );
        }

        /// <summary>
        /// Tests decompiling an input from a node that is not an XElement
        /// </summary>
        [TestMethod]
        [ExpectedException( typeof( ArgumentException ) )]
        public void TestDecompileInput_NotElement()
        {
            XText t = new XText( "Test" );
            JobXmlDecompiler d = new JobXmlDecompiler();
            d.DecompileInput( t );
        }

        /// <summary>
        /// Tests decompiling an input from a node with no inner node.
        /// </summary>
        [TestMethod]
        [ExpectedException( typeof( ArgumentException ) )]
        public void TestDecompileInput_NoChildNode()
        {
            XElement element = new XElement( "test" );
            JobXmlDecompiler d = new JobXmlDecompiler();
            d.DecompileInput( element );
        }

        /// <summary>
        /// Tests decompiling an input from a node with no inner CDATA node.
        /// </summary>
        [TestMethod]
        [ExpectedException( typeof( ArgumentException ) )]
        public void TestDecompileInput_NoCDATA()
        {
            XElement element = new XElement( "test", new XElement( "test-inner" ) );
            JobXmlDecompiler d = new JobXmlDecompiler();
            d.DecompileInput( element );
        }

        /// <summary>
        /// Tests decompiling an input from a node where a compressor has been
        /// specified, but the factory cannot resolve it.
        /// </summary>
        [TestMethod]
        [ExpectedException( typeof( XmlDecompilationException ) )]
        public void TestDecompileInput_UnknownCompressor()
        {
            byte[] img = File.ReadAllBytes( "compression_test.bmp" );
            string imgAsString = System.Text.Encoding.Default.GetString( img );
            XCData data = new XCData( imgAsString );
            XAttribute compressor = new XAttribute( "compressor", "unknown" );
            XElement element = new XElement( "test", compressor, data );
            JobXmlDecompiler d = new JobXmlDecompiler();
            d.DecompileInput( element );
        }

        /// <summary>
        /// Tests decompiling an uncompressed, non-identified input.
        /// </summary>
        [TestMethod]
        public void TestDecompileInput_NoCompressor_NoIdentifier()
        {
            Image rawImg = Image.FromFile( "compression_test.bmp" );
            byte[] img = CompressionAssistant.ImageToBytes( rawImg );
            string imgAsString = System.Text.Encoding.Default.GetString( img );
            XCData data = new XCData( imgAsString );
            XElement element = new XElement( "test", data );
            JobXmlDecompiler d = new JobXmlDecompiler();
            JobInput i = d.DecompileInput( element );

            Assert.IsNotNull( i.Input );
            Assert.IsNotNull( i.Identifier );
            Assert.AreEqual( string.Empty, i.Identifier );
        }

        /// <summary>
        /// Tests decompiling an input compressed using GZip, with no id.
        /// </summary>
        [TestMethod]
        public void TestDecompileInput_GZipCompressor_NoIdentifier()
        {
            Image rawImg = Image.FromFile( "compression_test.bmp" );
            ICompressor compressor = new GZipCompressor();
            byte[] img = CompressionAssistant.Compress( rawImg, compressor );
            string imgAsString = System.Text.Encoding.Default.GetString( img );
            XCData data = new XCData( imgAsString );
            XAttribute c = new XAttribute( "compressor", "gzip" );
            XElement element = new XElement( "test", c, data );
            JobXmlDecompiler d = new JobXmlDecompiler();
            JobInput i = d.DecompileInput( element );

            Assert.IsNotNull( i.Input );
            Assert.IsNotNull( i.Identifier );
            Assert.AreEqual( string.Empty, i.Identifier );
        }

        /// <summary>
        /// Tests decompiling an uncompressed input with an identifier.
        /// </summary>
        [TestMethod]
        public void TestDecompileInput_NoCompressor_WithIdentifier()
        {
            string id = "test-1";
            Image rawImg = Image.FromFile( "compression_test.bmp" );
            byte[] img = CompressionAssistant.ImageToBytes( rawImg );
            string imgAsString = System.Text.Encoding.Default.GetString( img );
            XCData data = new XCData( imgAsString );
            XAttribute idAttr = new XAttribute( "identifier", id );
            XElement element = new XElement( "test", idAttr, data );
            JobXmlDecompiler d = new JobXmlDecompiler();
            JobInput i = d.DecompileInput( element );

            Assert.IsNotNull( i.Input );
            Assert.AreEqual( id, i.Identifier );
        }
    }
}
