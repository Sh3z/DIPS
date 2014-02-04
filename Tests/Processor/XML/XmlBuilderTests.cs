using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DIPS.Processor.XML.Compilation;
using System.Xml.Linq;
using System.Linq;
using DIPS.Processor.Client;

namespace DIPS.Tests.Processor.XML
{
    /// <summary>
    /// Summary description for XmlBuilderTests
    /// </summary>
    [TestClass]
    public class XmlBuilderTests
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
        /// Tests constructing an XmlBuilder with a null process
        /// </summary>
        [TestMethod]
        [ExpectedException( typeof( ArgumentNullException ) )]
        public void TestConstructor_NullProcess()
        {
            XmlBuilder b = new XmlBuilder( null );
        }

        /// <summary>
        /// Tests constructing an XmlBuilder with a valid process.
        /// </summary>
        [TestMethod]
        public void TestConstructor_ValidProcess()
        {
            XmlBuilder b = new XmlBuilder( new DudProcess() );

            Assert.AreEqual( 0, b.Algorithms.Count );
            Assert.AreEqual( 0, b.Inputs.Count );
            Assert.IsNull( b.Xml );
        }

        /// <summary>
        /// Tests building the Xml when no algorithms or inputs have been
        /// specified, and no inner exception occurs.
        /// </summary>
        [TestMethod]
        public void TestBuild_NoAlgorithms_NoInputs_NoException()
        {
            XmlBuilder b = new XmlBuilder( new DudProcess() );
            b.Build();

            Assert.IsNotNull( b.Xml );
            var descendant = b.Xml.Descendants( "test" ).FirstOrDefault();
            Assert.IsNotNull( descendant );
            Assert.IsFalse( descendant.DescendantNodes().Any() );
        }

        /// <summary>
        /// Tests building the Xml when no algorithms or inputs have been
        /// specified, and accessing the processes name throws an exception.
        /// </summary>
        [TestMethod]
        [ExpectedException( typeof( XmlBuilderException ) )]
        public void TestBuild_NoAlgorithms_NoInputs_Exception()
        {
            XmlBuilder b = new XmlBuilder( new ExceptionProcess() );
            b.Build();
        }

        /// <summary>
        /// Tests building Xml with one algorithm.
        /// </summary>
        [TestMethod]
        public void TestBuild_OneAlgorithm_NoInputs()
        {
            IBuilderProcess builder = new JobBuilderProcess();
            XmlBuilder b = new XmlBuilder( builder );
            AlgorithmDefinition d = new AlgorithmDefinition( "test",
                new[] { new Property( "TestProperty", typeof( double ) ) { Value = 1d } } );

            b.Algorithms.Add( d );
            b.Build();
            var algorithms = b.Xml.Descendants( "algorithm" );

            Assert.AreEqual( 1, algorithms.Count() );
            Assert.AreEqual( 1, algorithms.Descendants( "property" ).Count() );
        }


        class DudProcess : IBuilderProcess
        {
            public string RootNodeName
            {
                get { return "test"; }
            }

            public System.Xml.Linq.XElement Build( DIPS.Processor.Client.AlgorithmDefinition definition )
            {
                return new System.Xml.Linq.XElement( "algorithm" );
            }

            public System.Xml.Linq.XElement BuildInput( DIPS.Processor.Client.JobDeployment.JobInput input )
            {
                return new System.Xml.Linq.XElement( "input" );
            }
        }

        class ExceptionProcess : IBuilderProcess
        {
            public string RootNodeName
            {
                get { throw new NotImplementedException(); }
            }

            public XElement Build( DIPS.Processor.Client.AlgorithmDefinition definition )
            {
                throw new NotImplementedException();
            }

            public XElement BuildInput( DIPS.Processor.Client.JobDeployment.JobInput input )
            {
                throw new NotImplementedException();
            }
        }
    }
}
