using DIPS.Processor.Plugin;
using DIPS.Processor.XML;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;

namespace DIPS.Tests.Processor.XML
{
    /// <summary>
    /// Summary description for DefinitionBuilderProcessTests
    /// </summary>
    [TestClass]
    public class DefinitionBuilderProcessTests
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
        /// Attemps building a null definition.
        /// </summary>
        [TestMethod]
        [ExpectedException( typeof( ArgumentNullException ) )]
        public void TestBuild_NullDefinition()
        {
            DefinitionBuilderProcess process = new DefinitionBuilderProcess();
            XElement element = process.Build( null );
        }

        /// <summary>
        /// Tests building a definition that contains no parameters.
        /// </summary>
        [TestMethod]
        public void TestBuild_NoParameters()
        {
            string definitionName = "Test";
            AlgorithmDefinition d = new AlgorithmDefinition( definitionName, null );
            DefinitionBuilderProcess process = new DefinitionBuilderProcess();
            XElement element = process.Build( d );
            _printXml( element );

            Assert.AreEqual( "algorithm", element.Name );
            var nameAttr = element.Attribute( "name" );
            Assert.AreEqual( definitionName, nameAttr.Value );
        }

        /// <summary>
        /// Tests building a definition with one parameter.
        /// </summary>
        [TestMethod]
        public void TestBuild_OneParameter()
        {
            AlgorithmDefinition d = PluginReflector.CreateDefinition( new ParameterTest() );
            DefinitionBuilderProcess process = new DefinitionBuilderProcess();
            XElement element = process.Build( d );
            _printXml( element );

            var properties = ( from p in element.Descendants( "properties" ) select p ).FirstOrDefault();
            Assert.IsNotNull( properties );

            var allProperties = properties.Descendants( "property" );
            Assert.IsTrue( allProperties.Any() );

            XElement firstProperty = allProperties.First();
            Assert.IsTrue( firstProperty.HasAttributes );

            XAttribute nameAttr = firstProperty.Attribute( "name" );
            Assert.IsNotNull( nameAttr );
            Assert.AreEqual( "Value", nameAttr.Value );

            XAttribute typeAttr = firstProperty.Attribute( "type" );
            Assert.IsNotNull( typeAttr );
            Type type = Type.GetType( typeAttr.Value );
            Assert.AreEqual( typeof( double ), type );

            XAttribute defaultValAttr = firstProperty.Attribute( "default-value" );
            Assert.IsNotNull( defaultValAttr );
            Assert.AreEqual( "1", defaultValAttr.Value );
        }


        private void _printXml( XNode node )
        {
            Debug.WriteLine( node.ToString() );
        }

        [PluginIdentifier( "Test" )]
        class ParameterTest : AlgorithmPlugin
        {
            [PluginVariable( "Value", 1d )]
            public double Value
            {
                get;
                set;
            }

            public override void Run()
            {
                throw new NotImplementedException();
            }
        }
    }
}
