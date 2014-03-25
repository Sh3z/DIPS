using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DIPS.Processor.Plugin.Matlab;
using DIPS.Processor.Plugin.Matlab.Parameters;
using System.Xml.Linq;
using System.Linq;

namespace DIPS.Tests.Processor.Plugin.Matlab
{
    /// <summary>
    /// Summary description for FileParameterTests
    /// </summary>
    [TestClass]
    public class FileParameterTests
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
        /// Tests creating Xml without a file
        /// </summary>
        [TestMethod]
        public void TestCreateXml_NoFile()
        {
            FileParameter p = new FileParameter();
            FileValue v = p.Value as FileValue;
            v.Path = "NoFileHere.txt";
            Assert.IsFalse( v.IsValid );

            XElement xml = p.CreateXml();
            var valueNode = xml.Descendants( "value" ).First();
            XAttribute pathAttr = valueNode.Attribute( "path" );
            Assert.IsNotNull( pathAttr );
            Assert.AreEqual( v.Path, pathAttr.Value );
            Assert.IsNull( valueNode.FirstNode );
        }

        /// <summary>
        /// Tests creating Xml with a file
        /// </summary>
        [TestMethod]
        public void TestCreateXml_WithFile()
        {
            FileParameter p = new FileParameter();
            FileValue v = p.Value as FileValue;
            v.Path = "TestFile.txt";
            Assert.IsTrue( v.IsValid );

            XElement xml = p.CreateXml();
            var valueNode = xml.Descendants( "value" ).First();
            XAttribute pathAttr = valueNode.Attribute( "path" );
            Assert.IsNotNull( pathAttr );
            Assert.AreEqual( v.Path, pathAttr.Value );
            Assert.IsNotNull( valueNode.FirstNode );
            Assert.IsTrue( valueNode.FirstNode.NodeType == System.Xml.XmlNodeType.CDATA );
        }

        /// <summary>
        /// Tests restoring the parameter from Xml with a file
        /// </summary>
        [TestMethod]
        public void TestRestore_WithFile()
        {
            FileParameter p = new FileParameter();
            FileValue v = p.Value as FileValue;
            v.Path = "TestFile.txt";
            XElement xml = p.CreateXml();

            FileParameter p2 = new FileParameter();
            p2.Restore( xml );

            FileValue v2 = p2.Value as FileValue;
            Assert.AreEqual( v.Path, v2.Path );
            Assert.AreEqual( v.Bytes.Length, v2.Bytes.Length );
        }
    }
}
