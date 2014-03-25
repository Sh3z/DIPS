using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DIPS.Processor.XML.Pipeline;
using DIPS.Processor.Client;
using System.Xml.Linq;
using System.Linq;
using DIPS.Processor.Plugin;

namespace DIPS.Tests.Processor.XML
{
    /// <summary>
    /// Summary description for PipelinePersistenceProcessTests
    /// </summary>
    [TestClass]
    public class PipelinePersistenceProcessTests
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
        /// Tests constructing the process without any factories
        /// </summary>
        [TestMethod]
        [ExpectedException( typeof( ArgumentNullException ) )]
        public void TestConstructor_NullFactories()
        {
            PipelinePersistanceProcess p = new PipelinePersistanceProcess( null );
        }

        /// <summary>
        /// Tests building the Xml of a process when there are no factories able to
        /// identify it
        /// </summary>
        [TestMethod]
        [ExpectedException( typeof( ArgumentException ) )]
        public void TestBuild_UnknownAlgorithm()
        {
            IDictionary<string, IPipelineXmlInterpreter> factories = new Dictionary<string, IPipelineXmlInterpreter>();
            factories.Add( "Unknown", new DudInterpreter() );
            PipelinePersistanceProcess p = new PipelinePersistanceProcess( factories );
            AlgorithmDefinition d = new AlgorithmDefinition( "Test", new Property[] { } );
            d.ParameterObject = new Cloneable();
            p.Build( d );
        }

        /// <summary>
        /// Tests building process Xml when it has no parameter object
        /// </summary>
        [TestMethod]
        public void TestBuild_NoParamObj()
        {
            IDictionary<string, IPipelineXmlInterpreter> factories = new Dictionary<string, IPipelineXmlInterpreter>();
            factories.Add( "Test", new DudInterpreter() );
            PipelinePersistanceProcess p = new PipelinePersistanceProcess( factories );
            AlgorithmDefinition d = new AlgorithmDefinition( "Test", new Property[] { } );
            XElement xml = p.Build( d );

            XAttribute nameAttr = xml.Attribute( "name" );
            Assert.IsNotNull( nameAttr );
            Assert.AreEqual( "Test", nameAttr.Value );
            Assert.IsFalse( xml.Descendants( "properties" ).Any() );
        }

        /// <summary>
        /// Tests building process Xml with a param object
        /// </summary>
        [TestMethod]
        public void TestBuild_WithParams()
        {
            DudInterpreter interpreter = new DudInterpreter();
            IDictionary<string, IPipelineXmlInterpreter> factories = new Dictionary<string, IPipelineXmlInterpreter>();
            factories.Add( "Test", interpreter );
            PipelinePersistanceProcess p = new PipelinePersistanceProcess( factories );
            AlgorithmDefinition d = new AlgorithmDefinition( "Test", new Property[] { } );
            d.ParameterObject = new Cloneable();
            XElement xml = p.Build( d );

            Assert.IsTrue( xml.Descendants( "properties" ).Any() );
            Assert.IsTrue( interpreter.DidCallCreateXml );
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

            public XElement CreateXml( ICloneable parameterObject )
            {
                DidCallCreateXml = true;
                return new XElement( "properties" );
            }

            public ICloneable CreateObject( XElement parameterXml )
            {
                DidCallCreateObject = true;
                return new Cloneable();
            }
        }

        class Cloneable : ICloneable
        {
            public object Clone()
            {
                return new Cloneable();
            }
        }
    }
}
