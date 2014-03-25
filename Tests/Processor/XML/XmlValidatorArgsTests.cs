using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DIPS.Processor.XML.Decompilation;

namespace DIPS.Tests.Processor.XML
{
    /// <summary>
    /// Summary description for XmlValidatorArgsTests
    /// </summary>
    [TestClass]
    public class XmlValidatorArgsTests
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
        /// Tests constructing the args with a null visitor.
        /// </summary>
        [TestMethod]
        [ExpectedException( typeof( ArgumentNullException ) )]
        public void TestConstructor_NullVisitor()
        {
            XmlValidatorArgs args = new XmlValidatorArgs( null );
        }

        /// <summary>
        /// Tests constructing the args with a valid visitor.
        /// </summary>
        [TestMethod]
        public void TestConstructor_ValidVisitor()
        {
            TestVisitor v = new TestVisitor();
            XmlValidatorArgs args = new XmlValidatorArgs( v );

            Assert.AreEqual( v, args.Visitor );
            Assert.IsFalse( args.ThrowOnError );
            Assert.IsNull( args.InputValidator );
            Assert.IsNull( args.AlgorithmValidator );
        }


        class TestVisitor : IXmlVisitor
        {
            public bool VisitedAlgorithm
            {
                get;
                private set;
            }

            public bool VisitedInput
            {
                get;
                private set;
            }

            public void VisitAlgorithm( System.Xml.Linq.XNode xml )
            {
                VisitedAlgorithm = true;
            }

            public void VisitInput( System.Xml.Linq.XNode xml )
            {
                VisitedInput = true;
            }
        }
    }
}
