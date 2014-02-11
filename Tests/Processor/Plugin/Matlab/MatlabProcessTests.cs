using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DIPS.Processor.Plugin;
using DIPS.Processor.Plugin.Matlab;

namespace DIPS.Tests.Processor.Plugin.Matlab
{
    /// <summary>
    /// Summary description for MatlabProcessTests
    /// </summary>
    [TestClass]
    public class MatlabProcessTests
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
        /// Tests calling the parameterless Run procedure.
        /// </summary>
        [TestMethod]
        [ExpectedException( typeof( AlgorithmException ) )]
        public void TestRun()
        {
            MatlabProcess p = new MatlabProcess();
            p.Run();
        }

        /// <summary>
        /// Tests running the process with null properties.
        /// </summary>
        [TestMethod]
        [ExpectedException( typeof( AlgorithmException ) )]
        public void TestRun_NullProperties()
        {
            MatlabProcess p = new MatlabProcess();
            p.Run( null );
        }

        /// <summary>
        /// Tests running the process without a script.
        /// </summary>
        [TestMethod]
        [ExpectedException( typeof( AlgorithmException ) )]
        public void TestRun_NoScript()
        {
            MatlabProperties props = new MatlabProperties();
            MatlabProcess p = new MatlabProcess();
            p.Run( props );
        }
    }
}
