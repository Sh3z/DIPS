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
    public class DecompilationVisitorTests
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
        /// Tests attempting to construct a DecompilationVisitor with no
        /// decompiler.
        /// </summary>
        [TestMethod]
        [ExpectedException( typeof( ArgumentNullException ) )]
        public void TestConstructor_NullDecompiler()
        {
            DecompilationVisitor v = new DecompilationVisitor( null );
        }

        /// <summary>
        /// Tests attempting to construct a DecompilationVisitor with a
        /// valid decompiler.
        /// </summary>
        [TestMethod]
        public void TestConstructor_ValidDecompiler()
        {
            DecompilationVisitor v = new DecompilationVisitor( new DudDecompiler() );

            Assert.AreEqual( 0, v.Algorithms.Count );
            Assert.AreEqual( 0, v.Inputs.Count );
        }


        class DudDecompiler : IAlgorithmXmlDecompiler
        {
            public AlgorithmDefinition DecompileAlgorithm( XNode algorithmNode )
            {
                throw new NotImplementedException();
            }

            public DIPS.Processor.Client.JobDeployment.JobInput DecompileInput( XNode inputNode )
            {
                throw new NotImplementedException();
            }
        }
    }
}
