using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DIPS.Processor.XML.Decompilation;
using System.Xml.Linq;

namespace DIPS.Tests.Processor.XML
{
    /// <summary>
    /// Summary description for JobValidationVisitorTests
    /// </summary>
    [TestClass]
    public class JobValidationVisitorTests
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
        /// Tests constructing the validator with null args.
        /// </summary>
        [TestMethod]
        [ExpectedException( typeof( NullReferenceException ) )]
        public void TestConstructor_NullArgs()
        {
            JobValidationVisitor v = new JobValidationVisitor( null );
        }

        /// <summary>
        /// Tests constructing the visitor with a valid set of args.
        /// </summary>
        [TestMethod]
        public void TestConstructor_ValidArgs()
        {
            TestVisitor v = new TestVisitor();
            XmlValidatorArgs a = new XmlValidatorArgs( v );
            JobValidationVisitor visitor = new JobValidationVisitor( a );
        }

        /// <summary>
        /// Tests validating an algorithm with no name, and we expect no exception.
        /// </summary>
        [TestMethod]
        public void TestValidateAlgorithm_NoName_NoThrow()
        {
            TestVisitor v = new TestVisitor();
            XmlValidatorArgs a = new XmlValidatorArgs( v );
            JobValidationVisitor visitor = new JobValidationVisitor( a );

            XNode xml = new XElement( "algorithm" );
            visitor.VisitAlgorithm( xml );
            Assert.IsFalse( v.VisitedAlgorithm );
        }

        /// <summary>
        /// Tests validating an algorithm with a name not registered within the
        /// cache, and no exception is expected.
        /// </summary>
        [TestMethod]
        public void TestValidateAlgorithm_NameNotInCache_NoThrow()
        {
            TestVisitor v = new TestVisitor();
            XmlValidatorArgs a = new XmlValidatorArgs( v );
            JobValidationVisitor visitor = new JobValidationVisitor( a );

            XNode xml = new XElement( "algorithm", new XAttribute( "name", "unknown" ) );
            visitor.VisitAlgorithm( xml );
            Assert.IsFalse( v.VisitedAlgorithm );
        }

        /// <summary>
        /// Tests validating an algorithm where a property has no name, and
        /// no exception is expected.
        /// </summary>
        [TestMethod]
        public void TestValidateAlgorithm_PropertyNoName_NoThrow()
        {
            TestVisitor v = new TestVisitor();
            XmlValidatorArgs a = new XmlValidatorArgs( v );
            JobValidationVisitor visitor = new JobValidationVisitor( a );

            XNode xml = new XElement( "algorithm", new XAttribute( "name", "gamma" ),
                new XElement( "properties",
                    new XElement( "property",
                        new XAttribute( "type", typeof( double ) ),
                        new XAttribute( "value", "1" ) ) ) );
            visitor.VisitAlgorithm( xml );
            Assert.IsFalse( v.VisitedAlgorithm );
        }

        /// <summary>
        /// Tests validating an algorithm where a property has no type,
        /// and no exception is expected.
        /// </summary>
        [TestMethod]
        public void TestValidateAlgorithm_PropertyNoType_NoThrow()
        {
            TestVisitor v = new TestVisitor();
            XmlValidatorArgs a = new XmlValidatorArgs( v );
            JobValidationVisitor visitor = new JobValidationVisitor( a );

            XNode xml = new XElement( "algorithm", new XAttribute( "name", "gamma" ),
                new XElement( "properties",
                    new XElement( "property",
                        new XAttribute( "name", "gamma" ),
                        new XAttribute( "value", "1" ) ) ) );
            visitor.VisitAlgorithm( xml );
            Assert.IsFalse( v.VisitedAlgorithm );
        }

        /// <summary>
        /// Tests validating an algorithm where a property has a type that
        /// does not represent a valid type, and no exception is expected.
        /// </summary>
        [TestMethod]
        public void TestValidateAlgorithm_PropertyBadType_NoThrow()
        {
            TestVisitor v = new TestVisitor();
            XmlValidatorArgs a = new XmlValidatorArgs( v );
            JobValidationVisitor visitor = new JobValidationVisitor( a );

            XNode xml = new XElement( "algorithm", new XAttribute( "name", "gamma" ),
                new XElement( "properties",
                    new XElement( "property",
                        new XAttribute( "name", "gamma" ),
                        new XAttribute( "type", "notatype" ),
                        new XAttribute( "value", "1" ) ) ) );
            visitor.VisitAlgorithm( xml );
            Assert.IsFalse( v.VisitedAlgorithm );
        }

        /// <summary>
        /// Tests validating an algorithm where a property has no value, and
        /// no exception is expected.
        /// </summary>
        [TestMethod]
        public void TestValidateAlgorithm_PropertyNoValue_NoThrow()
        {
            TestVisitor v = new TestVisitor();
            XmlValidatorArgs a = new XmlValidatorArgs( v );
            JobValidationVisitor visitor = new JobValidationVisitor( a );

            XNode xml = new XElement( "algorithm", new XAttribute( "name", "gamma" ),
                new XElement( "properties",
                    new XElement( "property",
                        new XAttribute( "name", "gamma" ),
                        new XAttribute( "type", "notatype" ) ) ) );
            visitor.VisitAlgorithm( xml );
            Assert.IsFalse( v.VisitedAlgorithm );
        }

        /// <summary>
        /// Tests validating an algorithm that is valid.
        /// </summary>
        [TestMethod]
        public void TestValidateAlgorithm_ValidXml()
        {
            TestVisitor v = new TestVisitor();
            XmlValidatorArgs a = new XmlValidatorArgs( v );
            JobValidationVisitor visitor = new JobValidationVisitor( a );

            XNode xml = new XElement( "algorithm", new XAttribute( "name", "gamma" ),
                new XElement( "properties",
                    new XElement( "property",
                        new XAttribute( "name", "gamma" ),
                        new XAttribute( "type", typeof( double ) ),
                        new XAttribute( "value", "1" ) ) ) );
            visitor.VisitAlgorithm( xml );
            Assert.IsTrue( v.VisitedAlgorithm );
        }

        /// <summary>
        /// Tests validating an algorithm that contains some invalid Xml.
        /// </summary>
        [TestMethod]
        [ExpectedException( typeof( XmlValidationException ) )]
        public void TestValidateAlgorithm_InvalidXml()
        {
            TestVisitor v = new TestVisitor();
            XmlValidatorArgs a = new XmlValidatorArgs( v );
            a.ThrowOnError = true;
            JobValidationVisitor visitor = new JobValidationVisitor( a );

            XNode xml = new XElement( "algorithm", new XAttribute( "name", "gamma" ),
                new XElement( "properties",
                    new XElement( "property",
                        new XAttribute( "name", "gamma" ),
                        new XAttribute( "type", "notatype" ) ) ) );
            visitor.VisitAlgorithm( xml );
        }


        class TestVisitor : IJobXmlVisitor
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
