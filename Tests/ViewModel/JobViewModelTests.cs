using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DIPS.ViewModel.UserInterfaceVM.JobTracking;
using DIPS.Processor.Client;

namespace DIPS.Tests.ViewModel
{
    /// <summary>
    /// Summary description for JobViewModelTests
    /// </summary>
    [TestClass]
    public class JobViewModelTests
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
        /// Tests creating a job with no ticket
        /// </summary>
        [TestMethod]
        [ExpectedException( typeof( ArgumentNullException ) )]
        public void TestConstructor_NullTicket()
        {
            JobViewModel vm = new JobViewModel( null );
        }

        /// <summary>
        /// Tests if the view model updates when the job begins
        /// </summary>
        [TestMethod]
        public void TestReactToStarted()
        {
            _runReactionTest( x => x.FireStarted() );
        }

        /// <summary>
        /// Tests if the view model updates when the job completes
        /// </summary>
        [TestMethod]
        public void TestReactToComplete()
        {
            _runReactionTest( x => x.FireComplete() );
        }

        /// <summary>
        /// Tests if the view model updates when the job errors
        /// </summary>
        [TestMethod]
        public void TestReactToError()
        {
            _runReactionTest( x => x.FireError() );
        }

        /// <summary>
        /// Tests if the view model updates when the job is cancelled
        /// </summary>
        [TestMethod]
        public void TestReactToCancel()
        {
            JobViewModel testVM = _runReactionTest( x => x.Cancel() );
            Assert.IsTrue( testVM.IsCancelled );
        }



        private JobViewModel _runReactionTest( Action<Ticket> action )
        {
            Ticket t = new Ticket();
            JobViewModel vm = new JobViewModel( t );
            bool statusChanged = false;
            bool longStatusChanged = false;
            vm.PropertyChanged += ( s, e ) => statusChanged |= e.PropertyName == "Status";
            vm.PropertyChanged += ( s, e ) => longStatusChanged |= e.PropertyName == "LongStatus";

            action( t );

            Assert.IsTrue( statusChanged );
            Assert.IsTrue( longStatusChanged );
            return vm;
        }


        class Ticket : IJobTicket
        {
            public event EventHandler JobCancelled;

            public event EventHandler JobCompleted;

            public event EventHandler JobError;

            public event EventHandler JobStarted;

            public JobState State
            {
                get;
                set;
            }

            public JobRequest Request
            {
                get;
                set;
            }

            public JobResult Result
            {
                get;
                set;
            }

            public bool Cancelled
            {
                get;
                set;
            }

            public void Cancel()
            {
                Cancelled = true;
                FireCancel();
            }


            public void FireCancel()
            {
                State = JobState.Cancelled;
                JobCancelled( this, EventArgs.Empty );
            }

            public void FireComplete()
            {
                State = JobState.Complete;
                JobCompleted( this, EventArgs.Empty );
            }

            public void FireError()
            {
                State = JobState.Error;
                Result = new JobResult( new Exception() );
                JobError( this, EventArgs.Empty );
            }

            public void FireStarted()
            {
                State = JobState.Running;
                JobStarted( this, EventArgs.Empty );
            }
        }
    }
}
