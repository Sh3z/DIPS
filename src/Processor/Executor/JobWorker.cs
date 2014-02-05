using DIPS.Processor.Client;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor.Executor
{
    class JobWorker : IRequestHandler
    {
        public void RunJob( IJobTicket req )
        {
            JobTicket ticket = req as JobTicket;
            ticket.OnJobStarted();

            Dictionary<IImageSource, Bitmap> results = new Dictionary<IImageSource, Bitmap>();
            foreach( var imgSource in req.Request.Job.GetInputs() )
            {
                //Bitmap result = execute_image( req.Request, imgSource );
                //results.Add( imgSource, result );
            }

            ticket.Result = new JobResult( req.Result.Algorithm, results );
            ticket.OnJobCompleted();
        }

        private Bitmap execute_image( JobRequest req, IImageSource imgSource )
        {
            Bitmap original = imgSource.Image;
            Bitmap current = original.Clone() as Bitmap;
            List<IAlgorithmStep> previous = new List<IAlgorithmStep>();
            //foreach( var algorithmStep in req.Algorithm )
            //{
            //    JobState state = new JobState( new List<IAlgorithmStep>( previous ), original, current );
            //    algorithmStep.Run( state );
            //    current = state.ProcessedBitmap ?? current;
            //}

            return current;
        }
    }
}
