using DIPS.Processor.Plugin;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DIPS.Processor.XML
{
    /// <summary>
    /// Represents a building mechanism used by an Xml builder.
    /// </summary>
    public interface IBuilderProcess
    {
        /// <summary>
        /// Constructs Xml using the <see cref="AlgorithmDefinition"/>.
        /// </summary>
        /// <param name="definition">The <see cref="AlgorithmDefinition"/>
        /// detailing the process and its properties.</param>
        /// <returns>An <see cref="XElement"/> representing the
        /// <see cref="AlgorithmDefinition"/>.</returns>
        XElement Build( AlgorithmDefinition definition );

        /// <summary>
        /// Constructs Xml using the <see cref="Image"/>.
        /// </summary>
        /// <param name="input">The <see cref="Image"/> to be used as input
        /// in a job.</param>
        /// <returns>An <see cref="XElement"/> representing the
        /// <see cref="Image"/>.</returns>
        /// <exception cref="InvalidOperationException">this
        /// <see cref="IBuilderProcess"/> does not support building
        /// inputs.</exception>
        XElement BuildInput( Image input );
    }
}
