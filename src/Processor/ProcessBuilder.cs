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
    /// Represents the object used to build complex processes using the
    /// algorithm definitions.
    /// </summary>
    public class ProcessBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessBuilder"/>
        /// class.
        /// </summary>
        /// <param name="factory">The <see cref="IPluginFactory"/> used to
        /// resolve plugins.</param>
        /// <exception cref="ArgumentNullException">factory is null.</exception>
        public ProcessBuilder( IPluginFactory factory )
        {
            if( factory == null )
            {
                throw new ArgumentNullException( "factory" );
            }

            _factory = factory;
        }


        /// <summary>
        /// Adds an algorithm to the builder, returning an indicating whether
        /// a plugin has been resolved.
        /// </summary>
        /// <param name="algorithm">The <see cref="AlgorithmDefinition"/>
        /// describing the algorithm.</param>
        /// <returns><c>true</c> if the definition has been successfully converted
        /// into an object; <c>false</c> otherwise.</returns>
        public bool AddAlgorithm( AlgorithmDefinition algorithm )
        {
            AlgorithmPlugin plugin = _factory.Manufacture( algorithm );
            if( plugin == null )
            {
                return false;
            }
            else
            {
                _plugins.Add( plugin );
                return true;
            }
        }

        


        /// <summary>
        /// Contains the set of generated plugins.
        /// </summary>
        private ICollection<AlgorithmPlugin> _plugins;

        /// <summary>
        /// Contains the factory used to resolve plugins.
        /// </summary>
        private IPluginFactory _factory;
    }
}
