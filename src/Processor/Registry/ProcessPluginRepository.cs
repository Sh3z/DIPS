using DIPS.Processor.Client;
using DIPS.Processor.Plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor.Registry
{
    /// <summary>
    /// Represents a repository of loaded processes.
    /// </summary>
    public class ProcessPluginRepository : IPluginRegistry, IAlgorithmRegistrar
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessPluginRepository"/>.
        /// </summary>
        public ProcessPluginRepository()
        {
            _pluginCache = new Dictionary<AlgorithmDefinition, Type>();
        }


        /// <summary>
        /// Gets a set of the known algorithms, exposed by identifier and
        /// properties.
        /// </summary>
        public IEnumerable<AlgorithmDefinition> KnownAlgorithms
        {
            get
            {
                return _pluginCache.Keys;
            }
        }

        /// <summary>
        /// Initializes this <see cref="IPluginRegistry"/> from the loaded
        /// <see cref="Assembly"/>.
        /// </summary>
        /// <param name="assembly">The <see cref="Assembly"/> specified within
        /// the Windows registry containing plugins.</param>
        public void Initialize( Assembly assembly )
        {
            var validTypes = assembly.GetTypes().Where( _isValidType );
            foreach( var type in validTypes )
            {
                AlgorithmDefinition definition = PluginReflector.CreateDefinition( type );
                _pluginCache.Add( definition, type );
            }
        }

        /// <summary>
        /// Resolves the <see cref="Type"/> of the algorithm with the given
        /// identifier.
        /// </summary>
        /// <param name="algorithmName">The identifier of the algorithm to resolv
        /// the type for.</param>
        /// <returns>The <see cref="Type"/> of the algorithm represented by the given
        /// identifier; null if no algorithm is associated with the given
        /// identifier.</returns>
        public Type FetchType( string algorithmName )
        {
            Type output = null;

            var match = _pluginCache.Keys
                    .FirstOrDefault( x => x.AlgorithmName == algorithmName );
            if( match != null )
            {
                output = _pluginCache[match];
            }

            return output;
        }

        /// <summary>
        /// Determines whether this <see cref="IAlgorithmRegistrar"/> is aware
        /// of an algorithm with the provided identifier.
        /// </summary>
        /// <param name="algorithmName">The identifier of the algorithm.</param>
        /// <returns><c>true</c> if this <see cref="IAlgorithmRegistrar"/> is aware
        /// of the algorithm with the given identifier; <c>false</c>
        /// otherwise.</returns>
        public bool KnowsAlgorithm( string algorithmName )
        {
            if( _pluginCache.Any() == false )
            {
                return false;
            }
            else
            {
                return _pluginCache.Keys.Any( x => x.AlgorithmName == algorithmName );
            }
        }

        /// <summary>
        /// Determines whether the incoming type is an algorithm definition.
        /// </summary>
        /// <param name="type">The type to structinize</param>
        /// <returns>true if the type is annotated with the PluginIdentifierAttribute
        /// class, and the type subclasses AlgorithmPlugin</returns>
        private bool _isValidType( Type type )
        {
            return type.GetCustomAttribute( typeof( AlgorithmAttribute ) ) != null
                && type.IsSubclassOf( typeof( AlgorithmPlugin ) );
        }


        /// <summary>
        /// Contains the definition -> type pairings of all plugins loaded.
        /// </summary>
        private IDictionary<AlgorithmDefinition, Type> _pluginCache;
    }
}
