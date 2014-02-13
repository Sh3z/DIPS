using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DIPS.Processor.XML.Pipeline;
using System.Xml.Linq;
using DIPS.Processor.XML.Decompilation;
using DIPS.Processor.Plugin;

namespace DIPS.Tests.Processor.XML
{
    /// <summary>
    /// Summary description for PipelineXmlDecompilerTests
    /// </summary>
    [TestClass]
    public class PipelineXmlDecompilerTests
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
        /// Tests constructing the decompiler without any factories
        /// </summary>
        [TestMethod]
        [ExpectedException( typeof( ArgumentNullException ) )]
        public void TestConstructor_NullFactories()
        {
            PipelineXmlDecompiler decompiler = new PipelineXmlDecompiler( null );
        }

        /// <summary>
        /// Tests decompiling an element that is not an XElement
        /// </summary>
        [TestMethod]
        [ExpectedException( typeof( ArgumentException ) )]
        public void TestDecompile_NotXElement()
        {
            IDictionary<string, IPipelineXmlInterpreter> factories = new Dictionary<string, IPipelineXmlInterpreter>();
            factories.Add( "Test", new DudInterpreter() );
            PipelineXmlDecompiler decompiler = new PipelineXmlDecompiler( factories );
            XText element = new XText( "Test" );
            decompiler.DecompileAlgorithm( element );
        }

        /// <summary>
        /// Tests decompiling an element with no properties
        /// </summary>
        [TestMethod]
        public void TestDecompile_NoProperties()
        {
            string name = "Test";
            IDictionary<string, IPipelineXmlInterpreter> factories = new Dictionary<string, IPipelineXmlInterpreter>();
            factories.Add( "Test", new DudInterpreter() );
            PipelineXmlDecompiler decompiler = new PipelineXmlDecompiler( factories );
            XNode element = new XElement( "process", new XAttribute( "name", name ) );
            var d = decompiler.DecompileAlgorithm( element );

            Assert.AreEqual( name, d.AlgorithmName );
            Assert.IsNull( d.ParameterObject );
        }

        /// <summary>
        /// Tests decompiling an element with properties where the factory cannot
        /// be resolved
        /// </summary>
        [TestMethod]
        [ExpectedException( typeof( ArgumentException ) )]
        public void TestDecompile_UnknownProcess()
        {
            string name = "Unknown";
            IDictionary<string, IPipelineXmlInterpreter> factories = new Dictionary<string, IPipelineXmlInterpreter>();
            factories.Add( "Test", new DudInterpreter() );
            PipelineXmlDecompiler decompiler = new PipelineXmlDecompiler( factories );
            XNode element = new XElement(
                "process", new XAttribute( "name", name ),
                new XElement( "properties",
                    new XElement( "property",
                        new XAttribute( "name", "test" ),
                        new XAttribute( "value", "1" ) ) ) );
            decompiler.DecompileAlgorithm( element );
        }

        /// <summary>
        /// Tests decompiling Xml with properties
        /// </summary>
        [TestMethod]
        public void TestDecompile_WithProperty()
        {
            string name = "Test";
            IDictionary<string, IPipelineXmlInterpreter> factories = new Dictionary<string, IPipelineXmlInterpreter>();
            DudInterpreter i = new DudInterpreter();
            factories.Add( "Test", i );
            PipelineXmlDecompiler decompiler = new PipelineXmlDecompiler( factories );
            XNode element = new XElement(
                "process", new XAttribute( "name", name ),
                new XElement( "properties",
                    new XElement( "property",
                        new XAttribute( "name", "test" ),
                        new XAttribute( "value", "1" ) ) ) );
            var d = decompiler.DecompileAlgorithm( element );

            Assert.IsNotNull( d.ParameterObject );
            Assert.IsTrue( i.DidCallCreateObject );
        }


        class DudInterpreter : IPipelineXmlInterpreter
        {
            public bool DidCallCreateXml
            {
                get;
                private set;
            }

            public bool DidCallCreateObject
            {
                get;
                private set;
            }

            public IEnumerable<System.Xml.Linq.XElement> CreateXml( object parameterObject )
            {
                DidCallCreateXml = true;
                return new System.Xml.Linq.XElement[] { };
            }

            public object CreateObject( IEnumerable<System.Xml.Linq.XElement> parameterXml )
            {
                DidCallCreateObject = true;
                return new object();
            }
        }
    }
}
