using DIPS.Processor.Client;
using DIPS.Processor.Plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor
{
    /// <summary>
    /// Represents the factory used to construct <see cref="AlgorithmPlugin"/>
    /// instances.
    /// </summary>
    public interface IPluginFactory
    {
        /// <summary>
        /// Manufactures an appropriate <see cref="AlgorithmPlugin"/> given the
        /// provided <see cref="AlgorithmDefinition"/>.
        /// </summary>
        /// <param name="algorithm">The <see cref="AlgorithmDefinition"/>
        /// descriving the algorithm to create.</param>
        /// <returns>An instance of <see cref="AlgorithmPlugin"/> represented
        /// by the given definition, or null if an algorithm cannot be built
        /// with the provided definition.</returns>
        AlgorithmPlugin Manufacture( AlgorithmDefinition algorithm );
    }
}
