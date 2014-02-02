using DIPS.Processor.Client;
using DIPS.Processor.Client.JobDeployment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DIPS.Processor.XML.Decompilation
{
    /// <summary>
    /// Represents the visitor used to decompile Xml back into
    /// <see cref="AlgorithmDefinition"/> and <see cref="ProcessInput"/>
    /// objects.
    /// </summary>
    public class AlgorithmDecompilerVisitor : IJobXmlVisitor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AlgorithmDecompilerVisitor"/>.
        /// </summary>
        public AlgorithmDecompilerVisitor()
        {
            Algorithms = new List<AlgorithmDefinition>();
            Inputs = new List<JobInput>();
        }


        /// <summary>
        /// Gets a collection of the decompiled <see cref="AlgorithmDefinition"/>s.
        /// </summary>
        public ICollection<AlgorithmDefinition> Algorithms
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets a collection of the decompiled <see cref="JobInput"/>s.
        /// </summary>
        public ICollection<JobInput> Inputs
        {
            get;
            private set;
        }


        /// <summary>
        /// Performs the visiting logic against an <see cref="XNode"/> representing
        /// an <see cref="AlgorithmDefinition"/>.
        /// </summary>
        /// <param name="xml">The <see cref="XNode"/> containing the algorithm
        /// information.</param>
        public void VisitAlgorithm( XNode xml )
        {

        }

        /// <summary>
        /// Performs the visiting logic against an <see cref="XNode"/> representing
        /// an input to the job.
        /// </summary>
        /// <param name="xml">The <see cref="XNode"/> representing an input to a
        /// job.</param>
        public void VisitInput( XNode xml )
        {

        }
    }
}
