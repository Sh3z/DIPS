using DIPS.Processor.Plugin;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Tests.Processor.Plugin
{
    /// <summary>
    /// Summary description for PropertyTests
    /// </summary>
    [TestClass]
    public class PropertyTests
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
        /// Tests attempting to create a Property with a null name.
        /// </summary>
        [TestMethod]
        [ExpectedException( typeof( ArgumentException ) )]
        public void TestConstructor_NullName()
        {
            Property p = new Property( null, typeof( int ) );
        }

        /// <summary>
        /// Tests attempting to create a Property with an empty name.
        /// </summary>
        [TestMethod]
        [ExpectedException( typeof( ArgumentException ) )]
        public void TestConstructor_EmptyName()
        {
            Property p = new Property( string.Empty, typeof( int ) );
        }

        /// <summary>
        /// Tests attempting to create a Property with a null type.
        /// </summary>
        [TestMethod]
        [ExpectedException( typeof( ArgumentNullException ) )]
        public void TestConstructor_NullType()
        {
            Property p = new Property( "Test", null );
        }

        /// <summary>
        /// Tests attempting to create a valid Property.
        /// </summary>
        [TestMethod]
        public void TestConstructor_ValidArgs()
        {
            Property p = new Property( "Test", typeof( int ) );
            Assert.AreEqual( "Test", p.Name );
            Assert.AreEqual( typeof( int ), p.Type );
            Assert.IsNull( p.Value );
            Assert.IsFalse( p.IsRequired );
        }

        /// <summary>
        /// Tests attempting to create a valid property that is required.
        /// </summary>
        [TestMethod]
        public void TestConstructor_Valid_Required()
        {
            Property p = new Property( "Test", typeof( int ), true );
            Assert.IsTrue( p.IsRequired );
        }

        /// <summary>
        /// Tests attempting to set a Property's value, where the value
        /// is the correct type
        /// </summary>
        [TestMethod]
        public void TestSetValue_SameType()
        {
            Property p = new Property( "Test", typeof( int ) );
            p.Value = 5;
            Assert.AreEqual( 5, p.Value );
        }

        /// <summary>
        /// Tests attempting to set a Property's value, where the value
        /// is a subtype of the required type
        /// </summary>
        [TestMethod]
        public void TestSetValue_SubType()
        {
            Property p = new Property( "Test", typeof( Base ) );
            SubBase b = new SubBase();
            p.Value = b;
            Assert.AreEqual( b, p.Value );

            p = new Property( "Test", typeof( ICollection<String> ) );
            List<String> list = new List<String>();
            p.Value = list;
            Assert.AreEqual( list, p.Value );
        }

        /// <summary>
        /// Tests asserting if a Property is represented by a null type
        /// </summary>
        [TestMethod]
        public void TestIsOfType_NullType()
        {
            Property p = new Property( "Test", typeof( int ) );
            Assert.IsFalse( p.IsOfType( null ) );
        }

        /// <summary>
        /// Tests asserting if a Property is represented by specific types.
        /// </summary>
        [TestMethod]
        public void TestIsOfType_ValidArg()
        {
            Property p = new Property( "Test", typeof( int ) );
            Assert.IsTrue( p.IsOfType( typeof( int ) ) );
            Assert.IsFalse( p.IsOfType( typeof( string ) ) );

            p = new Property( "Test", typeof( Base ) );
            Assert.IsTrue( p.IsOfType( typeof( Base ) ) );
            Assert.IsTrue( p.IsOfType( typeof( SubBase ) ) );
        }


        class Base
        {
        }

        class SubBase : Base
        {
        }
    }
}
