using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DIPS.Processor.Plugin;
using DIPS.Processor.Registry;
using DIPS.Processor.Client;

namespace DIPS.Tests.Processor.Registry
{
    /// <summary>
    /// Summary description for AlgorithmActivatorTests
    /// </summary>
    [TestClass]
    public class AlgorithmActivatorTests
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
        /// Tests constructing an activator with a null registrar.
        /// </summary>
        [TestMethod]
        [ExpectedException( typeof( ArgumentNullException ) )]
        public void TestConstructor_NullRegistrar()
        {
            AlgorithmActivator activator = new AlgorithmActivator( null );
        }

        /// <summary>
        /// Tests constructing an activator with a valid registrar.
        /// </summary>
        [TestMethod]
        public void TestConstructor_ValidRegistrar()
        {
            AlgorithmActivator activator = new AlgorithmActivator( new TestRegistrar() );
        }

        /// <summary>
        /// Tests checking if null can be activated.
        /// </summary>
        [TestMethod]
        public void TestCanActivate_NullDefinition()
        {
            AlgorithmActivator activator = new AlgorithmActivator( new TestRegistrar() );
            bool canActivate = activator.CanActivate( null );
            Assert.IsFalse( canActivate );
        }

        /// <summary>
        /// Tests checking if an unregistered algorithm can be activated.
        /// </summary>
        [TestMethod]
        public void TestCanActivate_NotCached()
        {
            AlgorithmActivator activator = new AlgorithmActivator( new TestRegistrar() );
            AlgorithmDefinition d = new AlgorithmDefinition( "unknown", new Property[] { } );
            bool canActivate = activator.CanActivate( d );
            Assert.IsFalse( canActivate );
        }

        /// <summary>
        /// Tests checking if a plugin without a parameterless constructor can
        /// be activated.
        /// </summary>
        [TestMethod]
        public void TestCanActivate_NoParameterlessConstructor()
        {
            AlgorithmActivator activator = new AlgorithmActivator( new TestRegistrar() );
            AlgorithmDefinition d = new AlgorithmDefinition( "BadConstructor", null );
            bool canActivate = activator.CanActivate( d );
            Assert.IsFalse( canActivate );
        }

        /// <summary>
        /// Tests checking if a valid algorithm can be activated.
        /// </summary>
        [TestMethod]
        public void TestCanActivate_ValidDefinition()
        {
            AlgorithmActivator activator = new AlgorithmActivator( new TestRegistrar() );
            AlgorithmDefinition d = new AlgorithmDefinition( "Test",
                new Property[] { new Property( "Test", typeof( double ) ) } );
            bool canActivate = activator.CanActivate( d );
            Assert.IsTrue( canActivate );
        }

        /// <summary>
        /// Tests activating a null plugin.
        /// </summary>
        [TestMethod]
        [ExpectedException( typeof( ActivationException ) )]
        public void TestActivate_NullDefinition()
        {
            AlgorithmActivator activator = new AlgorithmActivator( new TestRegistrar() );
            activator.Activate( null );
        }

        /// <summary>
        /// Tests activating a plugin with no properties.
        /// </summary>
        [TestMethod]
        public void TestActivate_NoProperties()
        {
            AlgorithmActivator activator = new AlgorithmActivator( new TestRegistrar() );
            AlgorithmDefinition d = new AlgorithmDefinition( "Test", new Property[] {} );
            AlgorithmPlugin p = activator.Activate( d );

            Assert.IsNotNull( p );
            Assert.AreEqual( typeof( TestPlugin ), p.GetType() );

            TestPlugin test = p as TestPlugin;
            Assert.AreEqual( 1, test.Test );
        }

        /// <summary>
        /// Tests activating a plugin with a property that is unknown to the
        /// plugin.
        /// </summary>
        [TestMethod]
        public void TestActivate_OneProperty_UnknownName()
        {
            AlgorithmActivator activator = new AlgorithmActivator( new TestRegistrar() );
            AlgorithmDefinition d = new AlgorithmDefinition( "Test",
                new Property[] { new Property( "unknown", typeof( double ) ) } );
            AlgorithmPlugin p = activator.Activate( d );

            Assert.IsNotNull( p );
            Assert.AreEqual( typeof( TestPlugin ), p.GetType() );

            TestPlugin test = p as TestPlugin;
            Assert.AreEqual( 1, test.Test );
        }

        /// <summary>
        /// Tests activating a plugin with a property that has the wrong type.
        /// </summary>
        [TestMethod]
        public void TestActivate_OneProperty_UnknownType()
        {
            AlgorithmActivator activator = new AlgorithmActivator( new TestRegistrar() );
            AlgorithmDefinition d = new AlgorithmDefinition( "Test",
                new Property[] { new Property( "Test", typeof( string ) ) } );
            AlgorithmPlugin p = activator.Activate( d );

            Assert.IsNotNull( p );
            Assert.AreEqual( typeof( TestPlugin ), p.GetType() );

            TestPlugin t = p as TestPlugin;
            Assert.AreEqual( 1, t.Test );
        }

        /// <summary>
        /// Tests activating a plugin with a valid property.
        /// </summary>
        [TestMethod]
        public void TestActivate_OneProperty_KnownType()
        {
            double value = 0d;
            AlgorithmActivator activator = new AlgorithmActivator( new TestRegistrar() );
            AlgorithmDefinition d = new AlgorithmDefinition( "Test",
                new Property[] { new Property( "Test", typeof( double ) ) { Value = value } } );
            AlgorithmPlugin p = activator.Activate( d );

            Assert.IsNotNull( p );
            Assert.AreEqual( typeof( TestPlugin ), p.GetType() );

            TestPlugin t = p as TestPlugin;
            Assert.AreEqual( value, t.Test );
        }

        /// <summary>
        /// Tests activating a plugin that uses the interpreter.
        /// </summary>
        [TestMethod]
        public void TestActivate_Interpreter_NoException()
        {
            AlgorithmActivator activator = new AlgorithmActivator( new InterpreterRegistrar() );
            AlgorithmDefinition d = new AlgorithmDefinition( "Test",
                new Property[] { new Property( "Test", typeof( double ) ) } );
            AlgorithmPlugin p = activator.Activate( d );

            Assert.IsNotNull( p );
            Assert.AreEqual( typeof( TestPluginInterpreter ), p.GetType() );

            TestPluginInterpreter t = p as TestPluginInterpreter;
            Assert.IsTrue( t.DidInterpret );
        }

        /// <summary>
        /// Tests activating a plugin that uses an interpreter, that throws
        /// an exception.
        /// </summary>
        [TestMethod]
        [ExpectedException( typeof( ActivationException ) )]
        public void TestActivate_Interpreter_Exception()
        {
            AlgorithmActivator activator = new AlgorithmActivator( new InterpreterRegistrar() );
            AlgorithmDefinition d = new AlgorithmDefinition( "Test",
                new Property[] { new Property( "throw", typeof( double ) ) } );
            AlgorithmPlugin p = activator.Activate( d );
        }


        class TestPlugin : AlgorithmPlugin
        {
            public TestPlugin()
            {
                Test = 1;
            }

            [AlgorithmProperty( "Test", typeof( double ) )]
            public double Test
            {
                get;
                set;
            }

            public override void Run()
            {
                throw new NotImplementedException();
            }
        }

        class TestPluginNoParameterlessConstructor : AlgorithmPlugin
        {
            public TestPluginNoParameterlessConstructor( double param )
            {
            }

            public override void Run()
            {
                throw new NotImplementedException();
            }
        }

        class TestPluginInterpreter : AlgorithmPlugin, IPropertyInterpreter
        {
            public bool DidInterpret
            {
                get;
                private set;
            }

            public override void Run()
            {
                throw new NotImplementedException();
            }

            public void Interpret( PropertySet properties )
            {
                if( properties.Contains( "throw" ) )
                {
                    throw new Exception();
                }
                else
                {
                    DidInterpret = true;
                }
            }
        }

        class InterpreterRegistrar : IAlgorithmRegistrar
        {

            public IEnumerable<AlgorithmDefinition> KnownAlgorithms
            {
                get { return new[] { new AlgorithmDefinition( "Interpreter", new Property[] { } ) }; }
            }

            public bool KnowsAlgorithm( string algorithmName )
            {
                return true;
            }

            public Type FetchType( string algorithmName )
            {
                return typeof( TestPluginInterpreter );
            }
        }

        class TestRegistrar : IAlgorithmRegistrar
        {
            public IEnumerable<AlgorithmDefinition> KnownAlgorithms
            {
                get
                {
                    return new[] { new AlgorithmDefinition( "Test",
                        new [] { new Property( "Test", typeof( double ) ) } ),
                    new AlgorithmDefinition("ConstructorTest", null)};
                }
            }

            public bool KnowsAlgorithm( string algorithmName )
            {
                return algorithmName == "Test";
            }

            public Type FetchType( string algorithmName )
            {
                return algorithmName == "Test" ?
                    typeof( TestPlugin ) : typeof( TestPluginNoParameterlessConstructor );
            }
        }
    }
}
