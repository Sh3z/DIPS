using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DIPS.Processor.XML;
using System.Linq;
using System.Xml.Linq;
using DIPS.Processor.Plugin;
using System.Diagnostics;
using DIPS.Processor.Client;
using DIPS.Processor.XML.Compilation;
using DIPS.Processor.Client.JobDeployment;
using DIPS.Util.Compression;
using System.Drawing;

namespace DIPS.Tests.Processor.XML
{
    /// <summary>
    /// Summary description for JobBuilderProcessTests
    /// </summary>
    [TestClass]
    public class JobBuilderProcessTests
    {
        public JobBuilderProcessTests()
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

        /// <summary>
        /// Gets or sets the name of the test file used for Bitmap tests.
        /// </summary>
        public string TestFileName
        {
            get;
            set;
        }


        private Image _createTestImage()
        {
            return new Bitmap( Image.FromFile( TestFileName ) );
        }


        /// <summary>
        /// Tests attempting to build a null definition.
        /// </summary>
        [TestMethod]
        public void TestBuild_NullDefinition()
        {
            JobBuilderProcess builder = new JobBuilderProcess();
            XElement xml = builder.Build( null );

            Assert.IsNotNull( xml );
            Assert.AreEqual( "algorithm", xml.Name );
        }

        /// <summary>
        /// Tests building Xml for a definition with no properties.
        /// </summary>
        [TestMethod]
        public void TestBuild_NoProperties()
        {
            string algorithmName = "Test";
            JobBuilderProcess builder = new JobBuilderProcess();
            AlgorithmDefinition d = new AlgorithmDefinition( algorithmName, null );
            XElement xml = builder.Build( d );

            Assert.AreEqual( "algorithm", xml.Name );
            Assert.IsNotNull( xml.Attribute( "name" ) );
            Assert.AreEqual( algorithmName, xml.Attribute( "name" ).Value );
            Assert.IsFalse( xml.Descendants( "properties" ).Any() );
        }

        /// <summary>
        /// Tests building Xml for a definition with one property.
        /// </summary>
        [TestMethod]
        public void TestBuild_OneProperty()
        {
            string algorithmName = "Test";
            JobBuilderProcess builder = new JobBuilderProcess();
            Property p = new Property( "Value", typeof( double ) );
            p.Value = 1d;
            AlgorithmDefinition d = new AlgorithmDefinition( algorithmName, new [] { p } );
            XElement xml = builder.Build( d );
            Debug.WriteLine( xml );

            Assert.AreEqual( "algorithm", xml.Name );
            Assert.IsNotNull( xml.Attribute( "name" ) );
            Assert.AreEqual( algorithmName, xml.Attribute( "name" ).Value );
            Assert.IsTrue( xml.Descendants( "properties" ).Any() );

            XElement props = xml.Descendants( "properties" ).First();
            Assert.AreEqual( 1, props.Descendants( "property" ).Count() );

            XElement prop = xml.Descendants( "property" ).First();
            Assert.AreEqual( "property", prop.Name );

            XAttribute name = prop.Attribute( "name" );
            Assert.IsNotNull( name );
            Assert.AreEqual( "Value", name.Value );

            XAttribute type = prop.Attribute( "type" );
            Assert.IsNotNull( type );
            Assert.AreEqual( typeof( double ), Type.GetType( type.Value ) );

            XElement value = prop.Descendants( "value" ).FirstOrDefault();
            Assert.IsNotNull( value );
            Assert.AreEqual( "1", value.Value );
        }

        /// <summary>
        /// Tests building a job input without an identifier or compression.
        /// </summary>
        [TestMethod]
        public void TestBuildInput_NoIdentifier_NoCompression()
        {
            Image img = _createTestImage();
            JobInput i = new JobInput( img );
            JobBuilderProcess p = new JobBuilderProcess();
            XElement element = p.BuildInput( i );

            Assert.AreEqual( "input", element.Name );
            Assert.IsNull( element.Attribute( "identifier" ) );

            XNode child = element.FirstNode;
            Assert.IsTrue( child.NodeType == System.Xml.XmlNodeType.CDATA );

            XCData data = (XCData)child;
            string providedImage = data.Value;
            string expectedImage = System.Text.Encoding.Default.GetString( CompressionAssistant.ImageToBytes( img ) );
            Assert.AreEqual( expectedImage, providedImage );
        }

        /// <summary>
        /// Tests building an input with an identifier and no compression.
        /// </summary>
        [TestMethod]
        public void TestBuildInput_Identifier_NoCompression()
        {
            object identifier = "Test";
            Image img = _createTestImage();
            JobInput i = new JobInput( img );
            i.Identifier = identifier;
            JobBuilderProcess p = new JobBuilderProcess();
            XElement element = p.BuildInput( i );

            Assert.AreEqual( "input", element.Name );

            Assert.IsNotNull( element.Attribute( "identifier" ) );
            Assert.AreEqual( identifier, element.Attribute( "identifier" ).Value );

            XNode child = element.FirstNode;
            Assert.IsTrue( child.NodeType == System.Xml.XmlNodeType.CDATA );

            XCData data = (XCData)child;
            string providedImage = data.Value;
            string expectedImage = System.Text.Encoding.Default.GetString( CompressionAssistant.ImageToBytes( img ) );
            Assert.AreEqual( expectedImage, providedImage );
        }

        /// <summary>
        /// Tests building an input with no identifier, with gzip compression.
        /// </summary>
        [TestMethod]
        public void TestBuildInput_NoIdentifier_Compression()
        {
            ICompressor compressor = new GZipCompressor();
            Image img = _createTestImage();
            JobInput i = new JobInput( img );
            i.Compressor = compressor;
            JobBuilderProcess p = new JobBuilderProcess();
            XElement element = p.BuildInput( i );

            Assert.AreEqual( "input", element.Name );
            Assert.IsNull( element.Attribute( "identifier" ) );

            Assert.IsNotNull( element.Attribute( "compressor" ) );
            Assert.AreEqual( "gzip", element.Attribute( "compressor" ).Value.ToLower() );

            XNode child = element.FirstNode;
            Assert.IsTrue( child.NodeType == System.Xml.XmlNodeType.CDATA );

            XCData data = (XCData)child;
            string providedImage = data.Value;
            string expectedImage = System.Text.Encoding.Default.GetString( CompressionAssistant.Compress( img, compressor ) );
            Assert.AreEqual( expectedImage, providedImage );
        }
    }
}
