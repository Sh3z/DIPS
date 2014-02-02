using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DIPS.Processor.XML;
using System.Linq;
using System.Xml.Linq;
using DIPS.Processor.Plugin;
using System.Diagnostics;
using DIPS.Processor.Client;

namespace DIPS.Tests.Processor.XML
{
    /// <summary>
    /// Summary description for JobBuilderProcessTests
    /// </summary>
    [TestClass]
    public class JobBuilderProcessTests
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
    }
}
