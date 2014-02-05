using System;

namespace DIPS.Processor.Client
{
    public interface IJobTicket
    {
        event EventHandler JobCancelled;

        event EventHandler JobCompleted;

        event EventHandler JobStarted;

        JobRequest Request
        {
            get;
        }

        JobResult Result
        {
            get;
        }

        bool Cancelled
        {
            get;
        }

        void Cancel();
    }
}
