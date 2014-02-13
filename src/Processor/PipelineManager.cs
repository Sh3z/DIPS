using DIPS.Processor.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DIPS.Processor
{
    public class PipelineManager : IPipelineManager
    {
        /// <summary>
        /// Gets the set of available processes that can be used to build
        /// a pipeline.
        /// </summary>
        public IEnumerable<AlgorithmDefinition> AvailableProcesses
        {
            get
            {
                if( _processes == null )
                {
                    lock( this )
                    {
                        if( _processes == null )
                        {
                            _setProcesses();
                        }
                    }
                }

                return _processes;
            }
        }
        [DebuggerBrowsable( DebuggerBrowsableState.Never )]
        private IEnumerable<AlgorithmDefinition> _processes;

        /// <summary>
        /// Encapsulates the state of a pipeline into Xml for later restoration.
        /// </summary>
        /// <param name="processes">The set of processes within the pipeline to
        /// persist into Xml.</param>
        /// <returns>An <see cref="XDocument"/> describing the pipeline.</returns>
        public XDocument SavePipeline( IEnumerable<AlgorithmDefinition> processes )
        {
            return null;
        }

        /// <summary>
        /// Restores the former state of a pipeline from Xml.
        /// </summary>
        /// <param name="pipeline">The <see cref="XDocument"/> describing the
        /// pipeline.</param>
        /// <returns>The set of processes within the pipeline as described in
        /// the Xml.</returns>
        public IEnumerable<AlgorithmDefinition> RestorePipeline( XDocument pipeline )
        {
            return null;
        }



        private void _setProcesses()
        {
        }
    }
}
