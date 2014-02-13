using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml.Linq;
using DIPS.Processor.XML.Decompilation;

namespace DIPS.Tests.Processor.XML
{
    /// <summary>
    /// Summary description for DefinitionXmlValidatorTests
    /// </summary>
    [TestClass]
    public class DefinitionXmlValidatorTests
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
        /// Tests validating a node that is not an XElement
        /// </summary>
        [TestMethod]
        public void TestValidateAlgorithm_NotAnElement()
        {
            XText text = new XText( "test" );
            DudVisitor visitor = new DudVisitor();
            DefinitionXmlValidator validator = new DefinitionXmlValidator( visitor );
            validator.VisitAlgorithm( text );

            Assert.IsFalse( visitor.DidVisitAlgorithm );
        }

        /// <summary>
        /// Tests validating an XElement with the wrong name
        /// </summary>
        [TestMethod]
        public void TestValidateAlgorithm_InvalidElementName()
        {
            XElement element = new XElement( "BadElement" );
            DudVisitor visitor = new DudVisitor();
            DefinitionXmlValidator validator = new DefinitionXmlValidator( visitor );
            validator.VisitAlgorithm( element );

            Assert.IsFalse( visitor.DidVisitAlgorithm );
        }

        /// <summary>
        /// Tests validating an algorithm without the name attribute
        /// </summary>
        [TestMethod]
        public void TestValidateAlgorithm_NoAlgorithmNameAttribute()
        {
            XElement element = new XElement( "algorithm" );
            DudVisitor visitor = new DudVisitor();
            DefinitionXmlValidator validator = new DefinitionXmlValidator( visitor );
            validator.VisitAlgorithm( element );

            Assert.IsFalse( visitor.DidVisitAlgorithm );
        }

        /// <summary>
        /// Tests validating an algorithm with the name attribute, but with
        /// no value
        /// </summary>
        [TestMethod]
        public void TestValidateAlgorithm_NameAttributeNoValue()
        {
            XElement element = new XElement( "algorithm", new XAttribute( "name", string.Empty ) );
            DudVisitor visitor = new DudVisitor();
            DefinitionXmlValidator validator = new DefinitionXmlValidator( visitor );
            validator.VisitAlgorithm( element );

            Assert.IsFalse( visitor.DidVisitAlgorithm );
        }

        /// <summary>
        /// Tests validating an algorithm with no properties.
        /// </summary>
        [TestMethod]
        public void TestValidateAlgorithm_NoProperties()
        {
            XElement element = new XElement( "algorithm", new XAttribute( "name", "test" ) );
            DudVisitor visitor = new DudVisitor();
            DefinitionXmlValidator validator = new DefinitionXmlValidator( visitor );
            validator.VisitAlgorithm( element );

            Assert.IsTrue( visitor.DidVisitAlgorithm );
        }

        /// <summary>
        /// Tests validating an algorithm with a property missing the name attribute.
        /// </summary>
        [TestMethod]
        public void TestValidateAlgorithm_OneProperty_NoNameAttribute()
        {
            XElement element = new XElement(
                "algorithm", new XAttribute( "name", "test" ),
                new XElement( "properties", new XElement( "property" ) ) );
            DudVisitor visitor = new DudVisitor();
            DefinitionXmlValidator validator = new DefinitionXmlValidator( visitor );
            validator.VisitAlgorithm( element );

            Assert.IsFalse( visitor.DidVisitAlgorithm );
        }

        /// <summary>
        /// Tests validating a property with a name attribute that has no value
        /// </summary>
        [TestMethod]
        public void TestValidateAlgorithm_OneProperty_NoNameValue()
        {
            XElement element = new XElement(
                "algorithm", new XAttribute( "name", "test" ),
                new XElement( "properties",
                    new XElement( "property", new XAttribute( "name", string.Empty ) ) ) );
            DudVisitor visitor = new DudVisitor();
            DefinitionXmlValidator validator = new DefinitionXmlValidator( visitor );
            validator.VisitAlgorithm( element );

            Assert.IsFalse( visitor.DidVisitAlgorithm );
        }

        /// <summary>
        /// Tests validating an algorithm with one property without the type
        /// attribute
        /// </summary>
        [TestMethod]
        public void TestValidateAlgorithm_OneProperty_NoTypeAttribute()
        {
            XElement element = new XElement(
                "algorithm", new XAttribute( "name", "test" ),
                new XElement( "properties",
                    new XElement( "property",
                        new XAttribute( "name", "test" ) ) ) );
            DudVisitor visitor = new DudVisitor();
            DefinitionXmlValidator validator = new DefinitionXmlValidator( visitor );
            validator.VisitAlgorithm( element );

            Assert.IsFalse( visitor.DidVisitAlgorithm );
        }

        /// <summary>
        /// Tests validating an algorithm with a property containing an invalid type.
        /// </summary>
        [TestMethod]
        public void TestValidateAlgorithm_OneProperty_InvalidTypeValue()
        {
            XElement element = new XElement(
                "algorithm", new XAttribute( "name", "test" ),
                new XElement( "properties",
                    new XElement( "property",
                        new XAttribute( "name", string.Empty ),
                        new XAttribute( "type", "notatype" ) ) ) );
            DudVisitor visitor = new DudVisitor();
            DefinitionXmlValidator validator = new DefinitionXmlValidator( visitor );
            validator.VisitAlgorithm( element );

            Assert.IsFalse( visitor.DidVisitAlgorithm );
        }

        /// <summary>
        /// Tests validating an algorithm with a property with no value
        /// </summary>
        [TestMethod]
        public void TestValidateAlgorithm_OneProperty_NoValueAttr_NoValueElement()
        {
            XElement element = new XElement(
                "algorithm", new XAttribute( "name", "test" ),
                new XElement( "properties",
                    new XElement( "property",
                        new XAttribute( "name", string.Empty ),
                        new XAttribute( "type", typeof( double ) ) ) ) );
            DudVisitor visitor = new DudVisitor();
            DefinitionXmlValidator validator = new DefinitionXmlValidator( visitor );
            validator.VisitAlgorithm( element );

            Assert.IsFalse( visitor.DidVisitAlgorithm );
        }

        /// <summary>
        /// Tests validating an algorithm with a property contained within a
        /// nested element
        /// </summary>
        [TestMethod]
        public void TestValidateAlgorithm_OneProperty_NoValueAttr_ValueElement()
        {
            XElement element = new XElement(
                "algorithm", new XAttribute( "name", "test" ),
                new XElement( "properties",
                    new XElement( "property",
                        new XAttribute( "name", "test" ),
                        new XAttribute( "type", typeof( double ) ),
                        new XElement( "value" ) ) ) );
            DudVisitor visitor = new DudVisitor();
            DefinitionXmlValidator validator = new DefinitionXmlValidator( visitor );
            validator.VisitAlgorithm( element );

            Assert.IsTrue( visitor.DidVisitAlgorithm );
        }

        /// <summary>
        /// Tests validating an algorithm with a property containing a value
        /// as an attribute
        /// </summary>
        [TestMethod]
        public void TestValidateAlgorithm_OneProperty_ValueAttr()
        {
            XElement element = new XElement(
                "algorithm", new XAttribute( "name", "test" ),
                new XElement( "properties",
                    new XElement( "property",
                        new XAttribute( "name", "test" ),
                        new XAttribute( "type", typeof( double ) ),
                        new XAttribute( "value", "1" ) ) ) );
            DudVisitor visitor = new DudVisitor();
            DefinitionXmlValidator validator = new DefinitionXmlValidator( visitor );
            validator.VisitAlgorithm( element );

            Assert.IsTrue( visitor.DidVisitAlgorithm );
        }


        class DudVisitor : IXmlVisitor
        {
            public bool DidVisitAlgorithm
            {
                get;
                set;
            }

            public void VisitAlgorithm( XNode xml )
            {
                DidVisitAlgorithm = true;
            }

            public void VisitInput( XNode xml )
            {
            }
        }
    }
}
