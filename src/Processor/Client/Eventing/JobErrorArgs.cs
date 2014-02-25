using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor.Client.Eventing
{
    /// <summary>
    /// Contains event information pertaining to errors occuring during
    /// the execution of a job.
    /// </summary>
    [Serializable]
    public class JobErrorArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JobErrorArgs"/>
        /// class.
        /// </summary>
        /// <param name="error">The <see cref="Exception"/> that is the
        /// cause of the error.</param>
        /// <exception cref="ArgumentNullException">error is null</exception>
        public JobErrorArgs( Exception error )
        {
            if( error == null )
            {
                throw new ArgumentNullException( "error" );
            }

            Exception = error;
        }


        /// <summary>
        /// Gets the <see cref="Exception"/> that caused the job to
        /// error.
        /// </summary>
        public Exception Exception
        {
            get;
            private set;
        }
    }
}
