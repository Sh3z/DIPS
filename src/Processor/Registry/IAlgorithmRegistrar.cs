using DIPS.Processor.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor.Registry
{
    /// <summary>
    /// Represents a maintained registry of algorithms.
    /// </summary>
    public interface IAlgorithmRegistrar
    {
        /// <summary>
        /// Gets a set of the known algorithms, exposed by identifier and
        /// properties.
        /// </summary>
        IEnumerable<AlgorithmDefinition> KnownAlgorithms
        {
            get;
        }

        /// <summary>
        /// Determines whether this <see cref="IAlgorithmRegistrar"/> is aware
        /// of an algorithm with the provided identifier.
        /// </summary>
        /// <param name="algorithmName">The identifier of the algorithm.</param>
        /// <returns><c>true</c> if this <see cref="IAlgorithmRegistrar"/> is aware
        /// of the algorithm with the given identifier; <c>false</c>
        /// otherwise.</returns>
        bool KnowsAlgorithm( string algorithmName );

        /// <summary>
        /// Resolves the <see cref="Type"/> of the algorithm with the given
        /// identifier.
        /// </summary>
        /// <param name="algorithmName">The identifier of the algorithm to resolv
        /// the type for.</param>
        /// <returns>The <see cref="Type"/> of the algorithm represented by the given
        /// identifier; null if no algorithm is associated with the given
        /// identifier.</returns>
        Type FetchType( string algorithmName );
    }
}
