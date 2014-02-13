using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DIPS.Processor.Plugin.Base;
using System.Xml.Linq;
using System.Linq;

namespace DIPS.Tests.Processor.Plugin.Base
{
    /// <summary>
    /// Summary description for GammaXmlInterpreterTests
    /// </summary>
    [TestClass]
    public class GammaXmlInterpreterTests
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
        /// Tests creating Xml with null parameters
        /// </summary>
        [TestMethod]
        public void TestCreate_NullParams()
        {
            GammaXmlInterpreter i = new GammaXmlInterpreter();
            XElement e = i.CreateXml( null );

            Assert.IsNotNull( e );
            Assert.AreEqual( "properties", e.Name );
        }

        /// <summary>
        /// Tests creating Xml with a valid parameter object
        /// </summary>
        [TestMethod]
        public void TestCreate_ValidParams()
        {
            GammaProperties p = new GammaProperties();
            p.Gamma = 1;
            GammaXmlInterpreter i = new GammaXmlInterpreter();
            XElement e = i.CreateXml( p );

            Assert.AreEqual( 1, e.Descendants( "property" ).Count() );

            XElement prop = e.Descendants( "property" ).First();
            XAttribute nameAttr = prop.Attribute( "name" );
            XAttribute valueAttr = prop.Attribute( "value" );
            Assert.IsNotNull( nameAttr );
            Assert.IsNotNull( valueAttr );
            Assert.AreEqual( "gamma", nameAttr.Value );
            Assert.AreEqual( "1", valueAttr.Value );
        }

        /// <summary>
        /// Tests creating an object with no Xml
        /// </summary>
        [TestMethod]
        public void TestCreateObject_NullXml()
        {
            GammaXmlInterpreter i = new GammaXmlInterpreter();
            object props = i.CreateObject( null );

            Assert.IsNotNull( props );
            Assert.AreEqual( GammaProperties.Default, props );
        }

        /// <summary>
        /// Tests creating the object with valid Xml
        /// </summary>
        [TestMethod]
        public void TestCreateObject_ValidProps()
        {
            XElement xml = new XElement( "properties",
                new XElement( "property",
                    new XAttribute( "name", "gamma" ),
                    new XAttribute( "value", "3" ) ) );
            GammaXmlInterpreter i = new GammaXmlInterpreter();
            object props = i.CreateObject( xml );
            GammaProperties p = props as GammaProperties;

            Assert.AreEqual( 3, p.Gamma );
        }
    }
}
