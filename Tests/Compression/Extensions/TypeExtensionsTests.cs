using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DIPS.Util.Extensions;

namespace DIPS.Tests.Util.Extensions
{
    /// <summary>
    /// Summary description for TypeExtensionsTests
    /// </summary>
    [TestClass]
    public class TypeExtensionsTests
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
        /// Tests asserting if a null type implements an interface.
        /// </summary>
        [TestMethod]
        public void TestImplements_NullType()
        {
            Type t = null;
            Assert.IsFalse( t.Implements<IDisposable>() );
        }

        /// <summary>
        /// Tests asserting if a type implements an object that is not an interface
        /// </summary>
        [TestMethod]
        public void TestImplements_NotAnInterface()
        {
            Type t = typeof( Disposable );
            Assert.IsFalse( t.Implements<Disposable>() );
        }

        /// <summary>
        /// Tests asserting if a type implements an interface it does implement
        /// </summary>
        [TestMethod]
        public void TestImplements_ValidArgs()
        {
            Type t = typeof( Disposable );
            Assert.IsTrue( t.Implements<IDisposable>() );
        }


        class Disposable : IDisposable
        {
            public void Dispose()
            {
                throw new NotImplementedException();
            }
        }
    }
}
