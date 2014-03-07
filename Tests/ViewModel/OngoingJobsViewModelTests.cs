using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DIPS.Processor.Client;
using DIPS.ViewModel.UserInterfaceVM.JobTracking;
using System.Linq;

namespace DIPS.Tests.ViewModel
{
    /// <summary>
    /// Summary description for OngoingJobsViewModelTests
    /// </summary>
    [TestClass]
    public class OngoingJobsViewModelTests
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
        /// Tests adding a job that is in the job queue
        /// </summary>
        [TestMethod]
        public void TestAdd_QueuedJob()
        {
            TicketImpl ticket = new TicketImpl();
            ticket.State = JobState.InQueue;
            OngoingJobsViewModel vm = new OngoingJobsViewModel();
            vm.Add( ticket, new DudHandler() );

            Assert.IsNull( vm.Current );
            Assert.AreEqual( 0, vm.Finished.Count );
            Assert.AreEqual( 1, vm.Pending.Count );
            Assert.AreEqual( ticket, vm.Pending.First().Ticket );
        }

        /// <summary>
        /// Tests adding a queued job that transitions to running
        /// </summary>
        [TestMethod]
        public void TestJobTransition_QueuedToRunning()
        {
            TicketImpl ticket = new TicketImpl();
            ticket.State = JobState.InQueue;
            OngoingJobsViewModel vm = new OngoingJobsViewModel();
            vm.Add( ticket, new DudHandler() );
            ticket._sink.FireSync( "JobStarted", EventArgs.Empty );

            Assert.IsNotNull( vm.Current );
            Assert.AreEqual( ticket, vm.Current.Ticket );
            Assert.AreEqual( 0, vm.Pending.Count );
        }

        /// <summary>
        /// Tests adding a queued job that transitions to running, then to
        /// finished
        /// </summary>
        [TestMethod]
        public void TestJobTransition_QueuedToFinished()
        {
            TicketImpl ticket = new TicketImpl();
            ticket.State = JobState.InQueue;
            OngoingJobsViewModel vm = new OngoingJobsViewModel();
            vm.Add( ticket, new DudHandler() );
            ticket._sink.FireSync( "JobStarted", EventArgs.Empty );
            ticket.Result = new JobResult( new List<IProcessedImage>() );
            ticket._sink.FireSync( "JobCompleted", EventArgs.Empty );
            Assert.IsNull( vm.Current );
            Assert.AreEqual( 1, vm.Finished.Count );
        }

        /// <summary>
        /// Tests adding a queued job that transitions to running, then to
        /// cancelled
        /// </summary>
        [TestMethod]
        public void TestJobTransition_RunningToCancelled()
        {
            TicketImpl ticket = new TicketImpl();
            ticket.State = JobState.InQueue;
            OngoingJobsViewModel vm = new OngoingJobsViewModel();
            vm.Add( ticket, new DudHandler() );
            ticket._sink.FireSync( "JobStarted", EventArgs.Empty );
            ticket.Result = JobResult.Cancelled;
            ticket._sink.FireSync( "JobCancelled", EventArgs.Empty );
            Assert.IsNull( vm.Current );
            Assert.AreEqual( 1, vm.Finished.Count );
        }

        /// <summary>
        /// Tests adding a queued job that transitions to running, then to
        /// an error state
        /// </summary>
        [TestMethod]
        public void TestJobTransition_RunningToError()
        {
            TicketImpl ticket = new TicketImpl();
            ticket.State = JobState.InQueue;
            OngoingJobsViewModel vm = new OngoingJobsViewModel();
            vm.Add( ticket, new DudHandler() );
            ticket._sink.FireSync( "JobStarted", EventArgs.Empty );
            ticket.Result = new JobResult( new Exception() );
            ticket._sink.FireSync( "JobError", EventArgs.Empty );
            Assert.IsNull( vm.Current );
            Assert.AreEqual( 1, vm.Finished.Count );
        }


        class TicketImpl : IJobTicket
        {
            public TicketImpl()
            {
                _sink = new Util.Remoting.EventSinkContainer<Processor.Client.Sinks.TicketSink>();
            }


            public Util.Remoting.ISinkContainer<Processor.Client.Sinks.TicketSink> Sinks
            {
                get { return _sink; }
            }
            public Util.Remoting.EventSinkContainer<Processor.Client.Sinks.TicketSink> _sink;

            public JobState State
            {
                get;
                set;
            }

            public JobRequest Request
            {
                get { throw new NotImplementedException(); }
            }

            public JobResult Result
            {
                get;
                set;
            }

            public bool Cancelled
            {
                get { throw new NotImplementedException(); }
            }

            public void Cancel()
            {
                throw new NotImplementedException();
            }
        }

        class DudHandler : IJobResultsHandler
        {
            public void HandleResults( IJobTicket completeJob )
            {
            }
        }
    }
}
