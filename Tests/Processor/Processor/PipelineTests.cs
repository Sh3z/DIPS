using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DIPS.Processor.Pipeline;
using DIPS.Processor.Plugin;

namespace DIPS.Tests.Processor
{
    /// <summary>
    /// Summary description for PipelineTests
    /// </summary>
    [TestClass]
    public class PipelineTests
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
        /// Tests adding a null pipeline element
        /// </summary>
        [TestMethod]
        public void TestAdd_NullElement()
        {
            Pipeline p = new Pipeline();
            p.Add( null );
            Assert.AreEqual( 0, p.Count );
        }

        /// <summary>
        /// Tests adding a non-null pipeline element
        /// </summary>
        [TestMethod]
        public void TestAdd_NonNullElement()
        {
            PipelineEntry e = new PipelineEntry( new DudPlugin() );
            Pipeline p = new Pipeline();
            p.Add( e );
            Assert.AreEqual( 1, p.Count );
        }


        class DudPlugin : AlgorithmPlugin
        {
            public override void Run( object parameterObject )
            {
                throw new NotImplementedException();
            }
        }
    }
}
