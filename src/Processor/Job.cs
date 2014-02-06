using DIPS.Processor.Client.JobDeployment;
using DIPS.Processor.Persistence;
using DIPS.Processor.Plugin;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor
{
    /// <summary>
    /// Represents a job to be executed.
    /// </summary>
    public class Job
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Job"/> class.
        /// </summary>
        /// <param name="plugins"></param>
        /// <param name="persister"></param>
        public Job( JobDefinition definition )
        {
            if( definition == null )
            {
                throw new ArgumentNullException( "definition" );
            }

            _definition = definition;
        }


        /// <summary>
        /// Contains the <see cref="Exception"/> that occured as this job
        /// executed.
        /// </summary>
        public Exception Exception
        {
            get;
            private set;
        }


        /// <summary>
        /// Runs this <see cref="Job"/> using the information within the
        /// definition.
        /// </summary>
        /// <returns><c>true</c> if this <see cref="Job"/> completed
        /// successfully; <c>false</c> otherwise.</returns>
        /// <remarks>
        /// If this method returns <c>false</c>, check the Exception property
        /// for any errors that occured.
        /// </remarks>
        public bool Run()
        {
            try
            {
                _run();
                return true;
            }
            catch( Exception e )
            {
                Exception = e;
                return false;
            }
        }


        /// <summary>
        /// Begins the job execution logic.
        /// </summary>
        private void _run()
        {
            foreach( var input in _definition.Inputs )
            {
                _runJob( input );
            }
        }

        /// <summary>
        /// Executes the job and persists the result.
        /// </summary>
        /// <param name="input">The input to the job.</param>
        private void _runJob( JobInput input )
        {
            Image jobInput = input.Input;
            foreach( AlgorithmPlugin plugin in _definition.Processes )
            {
                plugin.Input = jobInput;
                plugin.Run();
                jobInput = plugin.Output ?? jobInput;
            }

            _definition.Persister.Persist( jobInput, input.Identifier );
        }


        /// <summary>
        /// Contains the information about this job.
        /// </summary>
        private JobDefinition _definition;
    }
}
