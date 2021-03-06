﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DIPS.Processor.Client;
using System.Collections.Generic;
using System.Linq;

namespace DIPS.Tests.Processor.Client
{
    /// <summary>
    /// Summary description for AlgorithmDefinitionTests
    /// </summary>
    [TestClass]
    public class AlgorithmDefinitionTests
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
        /// Tests attempting to create an AlgorithmDefinition instance with a
        /// null name.
        /// </summary>
        [TestMethod]
        [ExpectedException( typeof( ArgumentException ) )]
        public void TestConstructor_NullName()
        {
            AlgorithmDefinition d = new AlgorithmDefinition( null, new List<Property>() );
        }

        /// <summary>
        /// Tests attempting to create an AlgorithmDefinition instance with a
        /// empty name.
        /// </summary>
        [TestMethod]
        [ExpectedException( typeof( ArgumentException ) )]
        public void TestConstructor_EmptyName()
        {
            AlgorithmDefinition d = new AlgorithmDefinition( string.Empty, new List<Property>() );
        }

        /// <summary>
        /// Tests attempting to create an AlgorithmDefinition instance with a
        /// null set of properties.
        /// </summary>
        [TestMethod]
        public void TestConstructor_NullProperties()
        {
            AlgorithmDefinition d = new AlgorithmDefinition( "Test", null );
            Assert.IsNotNull( d.Properties );
            Assert.AreEqual( 0, d.Properties.Count );
        }

        /// <summary>
        /// Tests attempting to create an AlgorithmDefinition instance with an
        /// empty set of properties.
        /// </summary>
        [TestMethod]
        public void TestConstructor_EmptyProperties()
        {
            AlgorithmDefinition d = new AlgorithmDefinition( "Test", new List<Property>() );
            Assert.IsNotNull( d.Properties );
            Assert.AreEqual( 0, d.Properties.Count );
        }

        /// <summary>
        /// Tests attempting to create a valid AlgorithmDefinition instance.
        /// </summary>
        [TestMethod]
        public void TestConstructor_ValidArgs()
        {
            Property p = new Property( "TestProperty", typeof( int ) );
            IEnumerable<Property> properties = new List<Property> { p };
            string definitionName = "Test";
            AlgorithmDefinition d = new AlgorithmDefinition( definitionName, properties );

            Assert.AreEqual( definitionName, d.AlgorithmName );
            Assert.AreEqual( 1, d.Properties.Count );
            Assert.IsTrue( d.Properties.Contains( p ) );
        }

        /// <summary>
        /// Tests cloning an AlgorithmDefinition
        /// </summary>
        [TestMethod]
        public void TestClone()
        {
            Property p = new Property( "TestProperty", typeof( int ) );
            IEnumerable<Property> properties = new List<Property> { p };
            string definitionName = "Test";
            AlgorithmDefinition d = new AlgorithmDefinition( definitionName, properties );
            AlgorithmDefinition clone = d.Clone() as AlgorithmDefinition;

            Assert.AreEqual( d.AlgorithmName, clone.AlgorithmName );
            Assert.AreEqual( d.Description, clone.Description );
            Assert.AreEqual( d.DisplayName, clone.DisplayName );
            Assert.AreEqual( d.ParameterObject, clone.ParameterObject );
            Assert.AreEqual( d.Properties.Count(), clone.Properties.Count() );
            Assert.IsTrue( d.Properties.SequenceEqual( clone.Properties ) );
        }
    }
}
