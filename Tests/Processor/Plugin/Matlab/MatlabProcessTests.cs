using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DIPS.Processor.Plugin;
using DIPS.Processor.Plugin.Matlab;
using System.Drawing;

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

        /// <summary>
        /// Tests running a script that does not provide the output variable.
        /// </summary>
        [TestMethod]
        [ExpectedException( typeof( AlgorithmException ) )]
        public void TestRun_NoOutputVariable()
        {
            string filePath = "NoOutputTest.m";
            Image img = Image.FromFile( "input.bmp" );
            MatlabProperties props = new MatlabProperties();
            props.ScriptFile = filePath;
            MatlabProcess p = new MatlabProcess();
            p.Input = img;
            p.Run( props );
        }

        /// <summary>
        /// Tests running a script where the output variable is an unexpected type
        /// </summary>
        [TestMethod]
        [ExpectedException( typeof( AlgorithmException ) )]
        public void TestRun_OutputVariableBadType()
        {
            string filePath = "BadOutputTest.m";
            Image img = Image.FromFile( "input.bmp" );
            MatlabProperties props = new MatlabProperties();
            props.ScriptFile = filePath;
            MatlabProcess p = new MatlabProcess();
            p.Input = img;
            p.Run( props );
        }

        /// <summary>
        /// Tests running a script where the output points to a bad file
        /// </summary>
        [TestMethod]
        [ExpectedException( typeof( AlgorithmException ) )]
        public void TestRun_OutputPointsToBadFile()
        {
            string filePath = "BadFileOutputTest.m";
            Image img = Image.FromFile( "input.bmp" );
            MatlabProperties props = new MatlabProperties();
            props.ScriptFile = filePath;
            MatlabProcess p = new MatlabProcess();
            p.Input = img;
            p.Run( props );
        }

        /// <summary>
        /// Tests running a valid set of instructions
        /// </summary>
        [TestMethod]
        public void TestRun_OutputPointsToFile()
        {
            string filePath = "ValidScript.m";
            Image img = Image.FromFile( "input.bmp" );
            MatlabProperties props = new MatlabProperties();
            props.ScriptFile = filePath;
            MatlabProcess p = new MatlabProcess();
            p.Input = img;
            p.Run( props );

            Assert.IsNotNull( p.Output );
        }
    }
}
