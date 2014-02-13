using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DIPS.Processor.Plugin.Matlab.Parameters;
using System.Xml.Linq;
using System.Linq;
using DIPS.Processor.Plugin;

namespace DIPS.Tests.Processor.Plugin.Matlab
{
    /// <summary>
    /// Summary description for GenericParameterTests
    /// </summary>
    [TestClass]
    public class GenericParameterTests
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
        /// Tests creating Xml when the value is null
        /// </summary>
        [TestMethod]
        public void TestCreateXml_NullValue()
        {
            GenericParameter g = new GenericParameter();
            ObjectValue v = g.Value as ObjectValue;
            Assert.IsNull( v.Value );

            XElement xml = g.CreateXml();
            XElement valEl = xml.Descendants( "value" ).First();
            Assert.IsNotNull( valEl.FirstNode );
            Assert.IsTrue( valEl.FirstNode.NodeType == System.Xml.XmlNodeType.CDATA );
        }

        /// <summary>
        /// Tests creating Xml when the value is a primative
        /// </summary>
        [TestMethod]
        public void TestCreateXml_Primative()
        {
            GenericParameter g = new GenericParameter();
            ObjectValue v = g.Value as ObjectValue;
            v.Value = 1d;

            XElement xml = g.CreateXml();
            XElement valEl = xml.Descendants( "value" ).First();
            Assert.IsNotNull( valEl.FirstNode );
            Assert.IsTrue( valEl.FirstNode.NodeType == System.Xml.XmlNodeType.CDATA );

            GenericParameter g2 = new GenericParameter();
            ObjectValue v2 = g2.Value as ObjectValue;
            g2.Restore( xml );
            Assert.AreEqual( v.Value, v2.Value );
        }

        /// <summary>
        /// Tests creating Xml with an object that cannot be serialized
        /// </summary>
        [TestMethod]
        [ExpectedException( typeof( PluginException ) )]
        public void TestCreateXml_UnserializableValue()
        {
            GenericParameter g = new GenericParameter();
            ObjectValue v = g.Value as ObjectValue;
            v.Value = new NotSerializable();
            XElement xml = g.CreateXml();
        }

        /// <summary>
        /// Tests creating Xml with an object that can be serialized
        /// </summary>
        [TestMethod]
        public void TestCreateXml_SerializableValue()
        {
            GenericParameter g = new GenericParameter();
            ObjectValue v = g.Value as ObjectValue;
            v.Value = new SerializableClass();
            XElement xml = g.CreateXml();
            GenericParameter g2 = new GenericParameter();
            ObjectValue v2 = g2.Value as ObjectValue;
            g2.Restore( xml );

            Assert.IsNotNull( v2.Value );
        }


        class NotSerializable
        {
        }

        [Serializable]
        class SerializableClass
        {
        }
    }
}
