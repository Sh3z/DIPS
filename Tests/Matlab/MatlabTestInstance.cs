using Microsoft.VisualStudio.TestTools.UnitTesting;
using MLApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Tests.Matlab
{
    /// <summary>
    /// Provides a shared Matlab instance for use in testing.
    /// </summary>
    [TestClass]
    public static class MatlabTestInstance
    {
        /// <summary>
        /// Contains the interface to the sharable Matlab instance.
        /// </summary>
        public static MLAppClass Instance
        {
            get;
            private set;
        }


        /// <summary>
        /// Initializes the common instance.
        /// </summary>
        /// <param name="context">N/A</param>
        [AssemblyInitialize]
        public static void Initialize( TestContext context )
        {
            Instance = new MLAppClass();
        }
    }
}
