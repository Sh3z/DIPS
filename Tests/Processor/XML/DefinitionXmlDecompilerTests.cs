using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DIPS.Processor.Client;
using DIPS.Processor.XML.Decompilation;
using System.Xml.Linq;
using System.Linq;

namespace DIPS.Tests.Processor.XML
{
    /// <summary>
    /// Summary description for DefinitionXmlDecompilerTests
    /// </summary>
    [TestClass]
    public class DefinitionXmlDecompilerTests
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
            DefinitionXmlDecompiler d = new DefinitionXmlDecompiler();
            d.DecompileAlgorithm( t );
        }

        /// <summary>
        /// Tests decompiling an algorithm with no properties.
        /// </summary>
        [TestMethod]
        public void TestDecompileAlgorithm_NoProperties()
        {
            DefinitionXmlDecompiler decompiler = new DefinitionXmlDecompiler();
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
            DefinitionXmlDecompiler decompiler = new DefinitionXmlDecompiler();
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
    }
}
