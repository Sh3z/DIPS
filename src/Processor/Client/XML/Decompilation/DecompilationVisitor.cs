using DIPS.Processor.Client;
using DIPS.Processor.Client.JobDeployment;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Xml.Linq;

namespace DIPS.Processor.XML.Decompilation
{
    /// <summary>
    /// Represents the visitor used to decompile Xml back into
    /// <see cref="AlgorithmDefinition"/> and <see cref="ProcessInput"/>
    /// objects.
    /// </summary>
    public class DecompilationVisitor : IXmlVisitor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DecompilationVisitor"/>.
        /// </summary>
        /// <param name="decompiler">The <see cref="IAlgorithmXmlDecompiler"/>
        /// providing the translation logic between Xml and object.</param>
        /// <exception cref="ArgumentNullException">decompiler is null</exception>
        public DecompilationVisitor( IAlgorithmXmlDecompiler decompiler )
        {
            if( decompiler == null )
            {
                throw new ArgumentNullException( "decompiler" );
            }

            _decompiler = decompiler;
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
            try
            {
                AlgorithmDefinition d = _decompiler.DecompileAlgorithm( xml );
                Algorithms.Add( d );
            }
            catch( Exception e )
            {
                throw new XmlDecompilationException( e.Message, e );
            }
        }

        /// <summary>
        /// Performs the visiting logic against an <see cref="XNode"/> representing
        /// an input to the job.
        /// </summary>
        /// <param name="xml">The <see cref="XNode"/> representing an input to a
        /// job.</param>
        public void VisitInput( XNode xml )
        {
            try
            {
                JobInput i = _decompiler.DecompileInput( xml );
                Inputs.Add( i );
            }
            catch( Exception e )
            {
                throw new XmlDecompilationException( e.Message, e );
            }
        }


        /// <summary>
        /// Contains the decompilation logic used by this visitor.
        /// </summary>
        private IAlgorithmXmlDecompiler _decompiler;
    }
}
