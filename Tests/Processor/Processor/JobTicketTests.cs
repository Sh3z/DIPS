using DIPS.Processor;
using DIPS.Processor.Client;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Tests.Processor
{
    /// <summary>
    /// Summary description for QueueExecutorTests
    /// </summary>
    [TestClass]
    public class JobTicketTests
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

        [TestMethod]
        [ExpectedException( typeof( ArgumentNullException ) )]
        public void TestConstructor_NullRequest()
        {
            JobTicket ticket = new JobTicket( null );
        }

        [TestMethod]
        public void TestConstructor_ValidRequest()
        {
            JobRequest req = new JobRequest( Algorithm.CreateWithSteps( new List<IAlgorithmStep>() ), new List<IImageSource>() );
            JobTicket ticket = new JobTicket( req );

            Assert.AreSame( req, ticket.Request );
            Assert.IsFalse( ticket.Cancelled );
        }
    }
}
