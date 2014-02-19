using DIPS.Processor.Plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor
{
    /// <summary>
    /// Represents a single pending process within a pipeline.
    /// </summary>
    public class PipelineEntry
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PipelineEntry"/>
        /// class.
        /// </summary>
        /// <param name="plugin">The <see cref="AlgorithmPlugin"/> to be
        /// executed.</param>
        /// <exception cref="ArgumentNullException">plugin is null.</exception>
        public PipelineEntry( AlgorithmPlugin plugin )
        {
            if( plugin == null )
            {
                throw new ArgumentNullException( "plugin" );
            }

            Process = plugin;
        }


        /// <summary>
        /// Gets the <see cref="AlgorithmPlugin"/> to be ran as part of a job.
        /// </summary>
        public AlgorithmPlugin Process
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the input to the process.
        /// </summary>
        public object ProcessInput
        {
            get;
            set;
        }
    }
}
