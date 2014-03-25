using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DIPS.Processor.Plugin.Matlab;
using System.Globalization;

namespace Matlab
{
    /// <summary>
    /// Summary description for DefinitionBuilderProcessTests
    /// </summary>
    [TestClass]
    public class FileToBytesConverterTests
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
        /// Tests converting to another type than bytes.
        /// </summary>
        [TestMethod]
        public void TestConvert_WrongTargetType()
        {
            FileToBytesConverter c = new FileToBytesConverter();
            byte[] output = (byte[])c.Convert( "TestFile.txt", typeof( string ), null, CultureInfo.CurrentCulture );
            Assert.IsNotNull( output );
            Assert.AreEqual( 0, output.Length );
        }

        /// <summary>
        /// Tests converting an object that does not represent a file path.
        /// </summary>
        [TestMethod]
        public void TestConvert_ValueNotString()
        {
            FileToBytesConverter c = new FileToBytesConverter();
            byte[] output = (byte[])c.Convert( 1, typeof( byte[] ), null, CultureInfo.CurrentCulture );
            Assert.IsNotNull( output );
            Assert.AreEqual( 0, output.Length );
        }

        /// <summary>
        /// Tests converting a file that does not exist.
        /// </summary>
        [TestMethod]
        public void TestConvert_FileDoesntExist()
        {
            FileToBytesConverter c = new FileToBytesConverter();
            byte[] output = (byte[])c.Convert( "BadFile.txt", typeof( byte[] ), null, CultureInfo.CurrentCulture );
            Assert.IsNotNull( output );
            Assert.AreEqual( 0, output.Length );
        }

        /// <summary>
        /// Tests converting a valid file into bytes.
        /// </summary>
        [TestMethod]
        public void TestConvert_ValidArgs()
        {
            FileToBytesConverter c = new FileToBytesConverter();
            byte[] output = (byte[])c.Convert( "TestFile.txt", typeof( byte[] ), null, CultureInfo.CurrentCulture );
            Assert.IsNotNull( output );
            Assert.AreNotEqual( 0, output.Length );
        }
    }
}
