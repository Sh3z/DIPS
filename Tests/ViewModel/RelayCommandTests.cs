using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DIPS.ViewModel.Commands;
using DIPS.Util.Commanding;

namespace DIPS.Tests.ViewModel
{
    /// <summary>
    /// Summary description for RelayCommandTests
    /// </summary>
    [TestClass]
    public class RelayCommandTests
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
        /// Tests constructing the command without any execution function.
        /// </summary>
        [TestMethod]
        [ExpectedException( typeof( ArgumentNullException ) )]
        public void TestConstructor_NullExecute()
        {
            RelayCommand c = new RelayCommand( null );
        }

        /// <summary>
        /// Tests constructing the command without any predicate
        /// </summary>
        [TestMethod]
        public void TestConstructor_NullCanExecute()
        {
            RelayCommand c = new RelayCommand( ( o ) => { }, null );
        }

        /// <summary>
        /// Tests CanExecute when no can execute logic has been provided
        /// </summary>
        [TestMethod]
        public void TestCanExecute_NoPredicate()
        {
            RelayCommand c = new RelayCommand( ( o ) => { }, null );
            Assert.IsTrue( c.CanExecute( null ) );
        }

        /// <summary>
        /// Tests CanExecute when can execute logic has been provided
        /// </summary>
        [TestMethod]
        public void TestCanExecute_WithPredicate()
        {
            Predicate<object> p = ( o ) => { return (bool)o; };
            RelayCommand c = new RelayCommand( ( o ) => { }, p );
            Assert.IsFalse( c.CanExecute( false ) );
            Assert.IsTrue( c.CanExecute( true ) );
        }

        /// <summary>
        /// Tests executing the command
        /// </summary>
        [TestMethod]
        public void TestExecute()
        {
            bool didExec = false;
            Action<object> a = ( o ) => didExec = true;
            RelayCommand c = new RelayCommand( a );
            c.Execute( null );

            Assert.IsTrue( didExec );
        }
    }
}
