using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DIPS.Processor.Plugin.Matlab;
using DIPS.Matlab;
using System.Xml.Linq;

namespace DIPS.Tests.Processor.Plugin.Matlab
{
    /// <summary>
    /// Summary description for MatlabParameterTests
    /// </summary>
    [TestClass]
    public class MatlabParameterTests
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
        /// Tests creating and persisting the abstract instance
        /// </summary>
        [TestMethod]
        public void TestCreateAndRestoreXml()
        {
            string name = "Test";
            string workspace = "Base";
            TestInstance i = new TestInstance();
            i.Name = name;
            i.Workspace = workspace;
            XElement xml = i.CreateXml();
            TestInstance i2 = new TestInstance();
            i2.Restore( xml );

            Assert.AreEqual( i.Name, i2.Name );
            Assert.AreEqual( i.Type, i2.Type );
            Assert.AreEqual( i.Workspace, i2.Workspace );
        }


        class TestInstance : MatlabParameter
        {
            public override ParameterType Type
            {
                get { return ParameterType.Object; }
            }

            protected override IParameterValue CreateValue()
            {
                return new DudValue();
            }

            protected override System.Xml.Linq.XElement CreateValueXml()
            {
                return new System.Xml.Linq.XElement( "Test" );
            }

            protected override void RestoreValue( System.Xml.Linq.XElement xml )
            {
            }
        }

        class DudValue : IParameterValue
        {
            public void Put( string name, Workspace workspace )
            {
            }

            public void Dispose()
            {
            }
        }
    }
}
