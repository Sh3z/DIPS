using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml.Linq;
using DIPS.Processor.XML;
using DIPS.Processor.Plugin;
using System.Diagnostics;

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







        private void _printXml( XNode node )
        {
            Debug.WriteLine( node.ToString() );
        }
    }
}
