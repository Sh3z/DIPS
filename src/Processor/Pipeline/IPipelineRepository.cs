using DIPS.Processor.Plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor.Pipeline
{
    /// <summary>
    /// Represents the repository of pipeline information.
    /// </summary>
    public interface IPipelineRepository
    {
        /// <summary>
        /// Fetches the <see cref="IPipelineXmlInterpreter"/> for a given
        /// process.
        /// </summary>
        /// <param name="processName">The name of the process to fetch
        /// the <see cref="IPipelineXmlInterpreter"/> for.</param>
        /// <returns>The <see cref="IPipelineXmlInterpreter"/> capable
        /// of saving or restoring the state of the associated
        /// process.</returns>
        IPipelineXmlInterpreter FetchInterpreter( string processName );

        /// <summary>
        /// Converts the contents of this <see cref="IPipelineRepository"/>
        /// into dictionary form.
        /// </summary>
        /// <returns>The name/interpreter pairings stored within this
        /// repository.</returns>
        IDictionary<string, IPipelineXmlInterpreter> ToDictionary();
    }
}
