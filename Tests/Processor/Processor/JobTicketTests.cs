using DIPS.Processor;
using DIPS.Processor.Client;
using DIPS.Processor.Client.JobDeployment;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        /// <summary>
        /// Tests attempting to create a ticket with a null request.
        /// </summary>
        [TestMethod]
        [ExpectedException( typeof( ArgumentNullException ) )]
        public void TestConstructor_NullRequest()
        {
            JobTicket ticket = new JobTicket( null, new DudHandler() );
        }

        /// <summary>
        /// Tests attempting to create a ticket with a null handler.
        /// </summary>
        [TestMethod]
        [ExpectedException( typeof( ArgumentNullException ) )]
        public void TestConstructor_NullHandler()
        {
            IJobDefinition d = new DudDefinition();
            JobRequest r = new JobRequest( d );
            JobTicket ticket = new JobTicket( r, null );
        }

        /// <summary>
        /// Tests attempting to create a ticket with a valid request.
        /// </summary>
        [TestMethod]
        public void TestConstructor_ValidRequest()
        {
            IJobDefinition d = new DudDefinition();
            JobRequest r = new JobRequest( d );
            JobTicket t = new JobTicket( r, new DudHandler() );

            Assert.AreEqual( r, t.Request );
            Assert.IsNull( t.Result );
            Assert.IsNotNull( t.JobID );
            Debug.WriteLine( t.JobID );
            Assert.IsFalse( t.Cancelled );
        }


        class DudDefinition : IJobDefinition
        {
            public PipelineDefinition GetAlgorithms()
            {
                throw new NotImplementedException();
            }

            public IEnumerable<JobInput> GetInputs()
            {
                throw new NotImplementedException();
            }
        }

        class DudHandler : ITicketCancellationHandler
        {
            public ITicketCancellationHandler Successor
            {
                get;
                set;
            }

            public bool Handle( IJobTicket ticket )
            {
                return false;
            }
        }
    }
}
