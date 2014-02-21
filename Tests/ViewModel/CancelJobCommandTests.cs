using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DIPS.ViewModel.UserInterfaceVM.JobTracking;
using DIPS.Processor.Client;

namespace DIPS.Tests.ViewModel
{
    /// <summary>
    /// Summary description for CancelJobCommandTests
    /// </summary>
    [TestClass]
    public class CancelJobCommandTests
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
        /// Tests checking if the command can execute with an object that is
        /// not a job view model
        /// </summary>
        [TestMethod]
        public void TestCanExecute_NotAJobViewModel()
        {
            CancelJobCommand c = new CancelJobCommand();
            Assert.IsFalse( c.CanExecute( null ) );
            Assert.IsFalse( c.CanExecute( string.Empty ) );
        }

        /// <summary>
        /// Tests checkig if the command can execute with a job that has previously
        /// been cancelled
        /// </summary>
        [TestMethod]
        public void TestCanExecute_CancelledJob()
        {
            CancelJobCommand c = new CancelJobCommand();
            Ticket t = new Ticket();
            JobViewModel v = new JobViewModel( t );
            t.Cancel();
            Assert.IsFalse( c.CanExecute( v ) );
        }

        /// <summary>
        /// Tests checking if the command can execute with a job that has not
        /// been cancelled
        /// </summary>
        [TestMethod]
        public void TestCanExecute_UncancelledJob()
        {
            CancelJobCommand c = new CancelJobCommand();
            Ticket t = new Ticket();
            JobViewModel v = new JobViewModel( t );
            Assert.IsTrue( c.CanExecute( v ) );
        }

        /// <summary>
        /// Tests executing the command
        /// </summary>
        [TestMethod]
        public void TestExecute()
        {
            CancelJobCommand c = new CancelJobCommand();
            Ticket t = new Ticket();
            JobViewModel v = new JobViewModel( t );
            c.Execute( v );
            Assert.IsTrue( v.IsCancelled );
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
