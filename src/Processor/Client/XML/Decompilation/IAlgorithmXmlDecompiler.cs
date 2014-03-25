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
    /// Encapsulates the decompilation of Xml back into objects.
    /// </summary>
    public interface IAlgorithmXmlDecompiler
    {
        /// <summary>
        /// Decompiles an Xml node back into an <see cref="AlgorithmDefinition"/>.
        /// </summary>
        /// <param name="algorithmNode">The <see cref="XNode"/> representing the
        /// <see cref="AlgorithmDefinition"/>.</param>
        /// <returns>An <see cref="AlgorithmDefinition"/> object represented by
        /// the provided Xml.</returns>
        AlgorithmDefinition DecompileAlgorithm( XNode algorithmNode );

        /// <summary>
        /// Decompiles an Xml node back into a <see cref="JobInput"/>.
        /// </summary>
        /// <param name="inputNode">The <see cref="XNode"/> represnting the
        /// <see cref="JobInput."/></param>
        /// <returns>A <see cref="JobInput"/> object represented by the provided
        /// Xml.</returns>
        JobInput DecompileInput( XNode inputNode );
    }
}
