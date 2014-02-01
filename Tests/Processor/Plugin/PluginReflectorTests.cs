using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DIPS.Processor.Plugin;
using DIPS.Util.Compression;
using System.Windows.Data;
using DIPS.Processor.Client;

namespace DIPS.Tests.Processor.Plugin
{
    /// <summary>
    /// Summary description for PluginReflectorTests
    /// </summary>
    [TestClass]
    public class PluginReflectorTests
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
        /// Tests attempting to reflect a plugin that has not been annotated.
        /// </summary>
        [TestMethod]
        [ExpectedException( typeof( ArgumentException ) )]
        public void TestCreateDefinition_NonAnnotatedPlugin()
        {
            PluginReflector.CreateDefinition( new NonAnnotatedPlugin() );
        }

        /// <summary>
        /// Tests attempting to reflect a plugin that has been annotated, and
        /// has no properties.
        /// </summary>
        [TestMethod]
        public void TestCreateDefinition_AnnotatedNoProperties()
        {
            AlgorithmDefinition d = PluginReflector.CreateDefinition( new AnnotatedPlugin() );

            Assert.AreEqual( "Plugin", d.AlgorithmName );
            Assert.AreEqual( 0, d.Properties.Count );
        }

        /// <summary>
        /// Tests attempting to reflect a plugin that has been annotated, and has
        /// one property.
        /// </summary>
        [TestMethod]
        public void TestCreateDefinition_AnnotatedOneProperty()
        {
            AlgorithmDefinition d = PluginReflector.CreateDefinition( new AnnotatedPluginWithProperty() );

            Assert.AreEqual( "Plugin", d.AlgorithmName );
            Assert.AreEqual( 1, d.Properties.Count );

            Property p = d.Properties.First();
            Assert.AreEqual( "Value", p.Name );
            Assert.AreEqual( typeof( double ), p.Type );
        }

        /// <summary>
        /// Tests attempting to create an AlgorithmDefinition from an unknown
        /// superclass
        /// </summary>
        [TestMethod]
        [ExpectedException( typeof( ArgumentException ) )]
        public void TestCreateDefinition_UnknownSuperclass()
        {
            PluginReflector.CreateDefinition( typeof( string ) );
        }

        /// <summary>
        /// Tests attempting to create an AlgorithmDefinition by type, from
        /// a known super class, with no internal parameters
        /// </summary>
        [TestMethod]
        public void TestCreateDefinition_KnownSuperclass_NoParameters()
        {
            AlgorithmDefinition d = PluginReflector.CreateDefinition( typeof( AnnotatedPlugin ) );

            Assert.AreEqual( "Plugin", d.AlgorithmName );
            Assert.AreEqual( 0, d.Properties.Count );
        }

        /// <summary>
        /// Tests attempting to create an AlgorithmDefinition by type, from
        /// a known super class, with one internal parameter
        /// </summary>
        [TestMethod]
        public void TestCreateDefinition_KnownSuperclass_OneParameter()
        {
            AlgorithmDefinition d = PluginReflector.CreateDefinition( typeof( AnnotatedPluginWithProperty ) );

            Assert.AreEqual( "Plugin", d.AlgorithmName );
            Assert.AreEqual( 1, d.Properties.Count );

            Property p = d.Properties.First();
            Assert.AreEqual( "Value", p.Name );
            Assert.AreEqual( typeof( double ), p.Type );
            Assert.AreEqual( 3d, p.Value );
        }

        /// <summary>
        /// Tests creating an algorithm definition where a property specifies a converter
        /// </summary>
        [TestMethod]
        public void TestCreateDefinition_PropertyWithValidConverter()
        {
            AlgorithmDefinition d = PluginReflector.CreateDefinition( typeof( AnnotatedPluginWithPropertyAndCompressor ) );

            Assert.AreEqual( "Plugin", d.AlgorithmName );
            Assert.AreEqual( 1, d.Properties.Count );

            Property p = d.Properties.First();
            Assert.AreEqual( "Value", p.Name );
            Assert.AreEqual( typeof( string ), p.Type );
            Assert.AreEqual( null, p.Value );
            Assert.AreEqual( typeof( GZipCompressor ), p.Compressor.GetType() );
        }

        [TestMethod]
        [ExpectedException( typeof( ArgumentException ) )]
        public void TestCreateDefinition_PropertyWithFauxTypeNoConverter()
        {
            AlgorithmDefinition d = PluginReflector.CreateDefinition( typeof( AnnotatedPluginWithPropertyFauxType ) );
        }

        [TestMethod]
        [ExpectedException( typeof( ArgumentException ) )]
        public void TestCreateDefinition_PropertyWithFauxTypeInvalidConverter()
        {
            AlgorithmDefinition d = PluginReflector.CreateDefinition( typeof( AnnotatedPluginWithPropertyFauxTypeBadConverter ) );
        }

        [TestMethod]
        public void TestCreateDefinition_PropertyWithFauxTypeWithConverter()
        {
            AlgorithmDefinition d = PluginReflector.CreateDefinition( typeof( AnnotatedPluginWithPropertyFauxTypeAndConverter ) );

            Assert.AreEqual( "Plugin", d.AlgorithmName );
            Assert.AreEqual( 1, d.Properties.Count );

            Property p = d.Properties.First();
            Assert.AreEqual( "Value", p.Name );
            Assert.AreEqual( typeof( string ), p.Type );
            Assert.AreEqual( "1", p.Value );
            Assert.AreEqual( typeof( StringToDoubleConverter ), p.Converter.GetType() );
        }


        class NonAnnotatedPlugin : AlgorithmPlugin
        {
            public override void Run()
            {
                throw new NotImplementedException();
            }
        }

        [PluginIdentifier( "Plugin" )]
        class AnnotatedPlugin : AlgorithmPlugin
        {
            public override void Run()
            {
                throw new NotImplementedException();
            }
        }

        [PluginIdentifier( "Plugin" )]
        class AnnotatedPluginWithProperty : AlgorithmPlugin
        {
            [PluginVariable( "Value", 3d )]
            public double Value
            {
                get;
                set;
            }

            public override void Run()
            {
                throw new NotImplementedException();
            }
        }

        [PluginIdentifier( "Plugin" )]
        class AnnotatedPluginWithPropertyAndCompressor : AlgorithmPlugin
        {
            [PluginVariable( "Value", null, CompressorType = typeof( GZipCompressor ) )]
            public string Value
            {
                get;
                set;
            }

            public override void Run()
            {
                throw new NotImplementedException();
            }
        }

        [PluginIdentifier( "Plugin" )]
        class AnnotatedPluginWithPropertyFauxType : AlgorithmPlugin
        {
            [PluginVariable( "Value", 1d, PublicType = typeof( string ) )]
            public double Value
            {
                get;
                set;
            }

            public override void Run()
            {
                throw new NotImplementedException();
            }
        }

        [PluginIdentifier( "Plugin" )]
        class AnnotatedPluginWithPropertyFauxTypeAndConverter : AlgorithmPlugin
        {
            [PluginVariable( "Value", "1", PublicType = typeof( string ), PublicTypeConverter = typeof( StringToDoubleConverter ) )]
            public double Value
            {
                get;
                set;
            }

            public override void Run()
            {
                throw new NotImplementedException();
            }
        }

        [PluginIdentifier( "Plugin" )]
        class AnnotatedPluginWithPropertyFauxTypeBadConverter : AlgorithmPlugin
        {
            [PluginVariable( "Value", 1d, PublicType = typeof( string ), PublicTypeConverter = typeof( string ) )]
            public double Value
            {
                get;
                set;
            }

            public override void Run()
            {
                throw new NotImplementedException();
            }
        }





        class StringToDoubleConverter : IValueConverter
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
    }
}
