using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DIPS.Compression;

namespace DIPS.Tests.Util
{
    /// <summary>
    /// Summary description for CompressorFactoryTests
    /// </summary>
    [TestClass]
    public class CaseInsensitiveComparatorTests
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
        /// Tests comparing two strings with the same value and case.
        /// </summary>
        [TestMethod]
        public void TestCompare_SameCase()
        {
            string a = "Hello";
            string b = "Hello";
            CaseInsensitiveComparator c = new CaseInsensitiveComparator();
            Assert.IsTrue( c.Equals( a, b ) );
            Assert.IsTrue( c.Equals( b, a ) );
        }

        /// <summary>
        /// Tests comparing two strings with the same value but different case.
        /// </summary>
        [TestMethod]
        public void TestCompare_DifferentCase()
        {
            string a = "Hello";
            string b = "hello";
            CaseInsensitiveComparator c = new CaseInsensitiveComparator();
            Assert.IsTrue( c.Equals( a, b ) );
            Assert.IsTrue( c.Equals( b, a ) );
        }
    }
}
