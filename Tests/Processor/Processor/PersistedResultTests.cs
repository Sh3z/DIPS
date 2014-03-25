using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DIPS.Processor.Persistence;
using System.Drawing;

namespace DIPS.Tests.Processor
{
    /// <summary>
    /// Summary description for PersistedResultTests
    /// </summary>
    [TestClass]
    public class PersistedResultTests
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
        /// Tests constructing a PersistedResult with a null image and valid
        /// string id.
        /// </summary>
        [TestMethod]
        [ExpectedException( typeof( ArgumentNullException ) )]
        public void TestConstructor_NullImage_ValidString()
        {
            PersistedResult r = new PersistedResult( null, "Test" );
        }

        /// <summary>
        /// Tests constructing a Persisted result with a valid image and string
        /// </summary>
        [TestMethod]
        public void TestConstructor_ValidImage_ValidString()
        {
            string id = "Test";
            Image img = Image.FromFile( "img.bmp" );
            PersistedResult r = new PersistedResult( img, id );

            Assert.AreEqual( id, r.Identifier );
            Assert.AreEqual( img, r.Output );
        }

        /// <summary>
        /// Tests constructing a Persisted result with a valid image and integer 
        /// </summary>
        [TestMethod]
        public void TestConstructor_ValidImage_ValidNumber()
        {
            int id = 0;
            Image img = Image.FromFile( "img.bmp" );
            PersistedResult r = new PersistedResult( img, id );

            Assert.AreEqual( id, r.Identifier );
            Assert.AreEqual( img, r.Output );
        }
    }
}
