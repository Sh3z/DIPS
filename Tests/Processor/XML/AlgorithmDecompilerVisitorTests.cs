using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DIPS.Processor.XML.Decompilation;
using System.Xml.Linq;
using System.Linq;
using DIPS.Processor.Client;

namespace DIPS.Tests.Processor.XML
{
    /// <summary>
    /// Summary description for AlgorithmDecompilerVisitorTests
    /// </summary>
    [TestClass]
    public class AlgorithmDecompilerVisitorTests
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
        /// Tests decompiling an algorithm with no properties.
        /// </summary>
        [TestMethod]
        public void TestVisitAlgorithm_NoProperties()
        {
            AlgorithmDecompilerVisitor visitor = new AlgorithmDecompilerVisitor();
            XElement xml = new XElement( "algorithm",
                new XAttribute( "name", "gamma" ) );
            visitor.VisitAlgorithm( xml );

            Assert.AreEqual( 1, visitor.Algorithms.Count );

            AlgorithmDefinition algorithm = visitor.Algorithms.First();
            Assert.AreEqual( "gamma", algorithm.AlgorithmName );
            Assert.AreEqual( 0, algorithm.Properties.Count );
        }

        /// <summary>
        /// Tests decompiling an algorithm with one property of a primitive type.
        /// </summary>
        [TestMethod]
        public void TestVisitAlgorithm_OneProperty_Primitive()
        {
            AlgorithmDecompilerVisitor visitor = new AlgorithmDecompilerVisitor();
            XElement xml = new XElement( "algorithm",
                new XAttribute( "name", "gamma" ),
                new XElement( "properties",
                    new XElement( "property",
                        new XAttribute( "name", "gamma" ),
                        new XAttribute( "type", typeof( double ) ),
                        new XAttribute( "value", "1" ) ) ) );
            visitor.VisitAlgorithm( xml );

            Assert.AreEqual( 1, visitor.Algorithms.Count );

            AlgorithmDefinition algorithm = visitor.Algorithms.First();
            Assert.AreEqual( 1, algorithm.Properties.Count );

            Property gamma = algorithm.Properties.First();
            Assert.AreEqual( "gamma", gamma.Name );
            Assert.AreEqual( typeof( double ), gamma.Type );
            Assert.IsNull( gamma.Converter );
            Assert.IsNull( gamma.Compressor );
            Assert.AreEqual( 1d, gamma.Value );
        }
    }
}
