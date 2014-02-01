using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DIPS.Processor.Client;
using System.Windows.Data;
using DIPS.Util.Compression;

namespace DIPS.Tests.Processor.Client
{
    /// <summary>
    /// Summary description for PropertyBuilderTests
    /// </summary>
    [TestClass]
    public class PropertyBuilderTests
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
        /// Tests building a property with no name specified.
        /// </summary>
        [TestMethod]
        [ExpectedException( typeof( ArgumentException ) )]
        public void TestBuild_NoName()
        {
            PropertyBuilder b = new PropertyBuilder();
            b.PropertyType = typeof( string );
            b.Build();
        }

        /// <summary>
        /// Tests building a property with no type specified.
        /// </summary>
        [TestMethod]
        [ExpectedException( typeof( ArgumentNullException ) )]
        public void TestBuild_NoType()
        {
            PropertyBuilder b = new PropertyBuilder();
            b.Name = "Test";
            b.Build();
        }

        /// <summary>
        /// Tests building a simple property with a name and type.
        /// </summary>
        [TestMethod]
        public void TestBuild_NameAndType()
        {
            string propName = "Test";
            Type propType = typeof( double );
            PropertyBuilder b = new PropertyBuilder();
            b.Name = propName;
            b.PropertyType = propType;
            Property p = b.Build();

            Assert.AreEqual( propName, p.Name );
            Assert.AreEqual( propType, p.Type );
            Assert.IsNotNull( p.Value );
            Assert.IsNull( p.Compressor );
            Assert.IsNull( p.Converter );
        }

        /// <summary>
        /// Tests building a property with an overriding public type, but
        /// with no converter specified.
        /// </summary>
        [TestMethod]
        [ExpectedException( typeof( InvalidOperationException ) )]
        public void TestBuild_PublicTypeNoConverter()
        {
            PropertyBuilder b = new PropertyBuilder();
            b.Name = "Test";
            b.PropertyType = typeof( double );
            b.PublicType = typeof( string );
            b.Build();
        }

        /// <summary>
        /// Tests building a propert with an overriding public type, and
        /// converter.
        /// </summary>
        [TestMethod]
        public void TestBuild_PublicTypeWithConverter()
        {
            Type pubType = typeof( string );
            IValueConverter converter = new DudConverter();
            PropertyBuilder b = new PropertyBuilder();
            b.Name = "Test";
            b.PropertyType = typeof( double );
            b.PublicType = pubType;
            b.Converter = converter;
            Property p = b.Build();

            Assert.AreEqual( pubType, p.Type );
            Assert.AreEqual( converter.GetType(), p.Converter.GetType() );
        }

        /// <summary>
        /// Tests building a property with an invalid default value.
        /// </summary>
        [TestMethod]
        [ExpectedException( typeof( ArgumentException ) )]
        public void TestBuild_BadDefaultValue()
        {
            PropertyBuilder b = new PropertyBuilder();
            b.Name = "Test";
            b.PropertyType = typeof( double );
            b.DefaultValue = "Bad";
            b.Build();
        }

        /// <summary>
        /// Tests building a property with a valid default value.
        /// </summary>
        [TestMethod]
        public void TestBuid_ValidDefaultValue()
        {
            object defaultValue = 1d;
            PropertyBuilder b = new PropertyBuilder();
            b.Name = "Test";
            b.PropertyType = typeof( double );
            b.DefaultValue = defaultValue;
            Property p = b.Build();

            Assert.AreEqual( defaultValue, p.Value );
        }

        /// <summary>
        /// Tests building a property with a compressor.
        /// </summary>
        [TestMethod]
        public void TestBuild_WithCompressor()
        {
            ICompressor compressor = new DudCompressor();
            PropertyBuilder b = new PropertyBuilder();
            b.Name = "Test";
            b.PropertyType = typeof( double );
            b.Compressor = compressor;
            Property p = b.Build();

            Assert.AreEqual( compressor.GetType(), p.Compressor.GetType() );
        }


        class DudConverter : IValueConverter
        {
            public object Convert( object value, Type targetType, object parameter, System.Globalization.CultureInfo culture )
            {
                throw new NotImplementedException();
            }

            public object ConvertBack( object value, Type targetType, object parameter, System.Globalization.CultureInfo culture )
            {
                throw new NotImplementedException();
            }
        }

        class DudCompressor : ICompressor
        {
            public byte[] Compress( byte[] toCompress )
            {
                throw new NotImplementedException();
            }

            public byte[] Decompress( byte[] toDecompress )
            {
                throw new NotImplementedException();
            }
        }
    }
}
