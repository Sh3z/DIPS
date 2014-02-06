using DIPS.Processor.Client;
using DIPS.Processor.Plugin;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor.Registry
{
    /// <summary>
    /// Provides access to the information within the DIPS registry. This class
    /// cannot be inherited.
    /// </summary>
    public sealed class RegistryCache : IAlgorithmRegistrar
    {
        /// <summary>
        /// Static initializer.
        /// </summary>
        static RegistryCache()
        {
            _singleton = new RegistryCache();
        }

        /// <summary>
        /// Private constructor to enforce singleton.
        /// </summary>
        private RegistryCache()
        {
            _pluginCache = new Dictionary<AlgorithmDefinition, Type>();
            Initialize();
        }


        /// <summary>
        /// Gets the singleton <see cref="RegistryCache"/> instance.
        /// </summary>
        public static RegistryCache Cache
        {
            get
            {
                return _singleton;
            }
        }
        [DebuggerBrowsable( DebuggerBrowsableState.Never )]
        private static RegistryCache _singleton;


        /// <summary>
        /// Initializes the cache using values from the registry.
        /// </summary>
        /// <remarks>
        /// This is automatically called on construction and does not need to be
        /// called again, unless the registry is modified.
        /// </remarks>
        public void Initialize()
        {
            try
            {
                _pluginCache.Clear();
                RegistryKey dips = Microsoft.Win32.Registry.LocalMachine.OpenSubKey( _regLoc );
                _initializePlugins( dips.OpenSubKey( "Plugins" ) );
            }
            catch( Exception e )
            {
                Debug.Write( "DIPS registry keys in unknown state:\n" + e );
            }
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
        /// Initializes the plugins from the registry key
        /// </summary>
        /// <param name="pluginsKey">The key in the registry to initialize
        /// from.</param>
        private void _initializePlugins( RegistryKey pluginsKey )
        {
            foreach( string pluginName in pluginsKey.GetValueNames() )
            {
                string pluginDLL = pluginsKey.GetValue( pluginName ) as string;
                Assembly pluginAssembly = Assembly.LoadFrom( pluginDLL );
                _cachePluginAssembly( pluginAssembly );
            }
        }

        /// <summary>
        /// Caches all the plugins from the target assembly
        /// </summary>
        /// <param name="assembly">The assembly to cache the plugins from</param>
        private void _cachePluginAssembly( Assembly assembly )
        {
            var validTypes = assembly.GetTypes().Where( _isValidType );
            foreach( var type in validTypes )
            {
                AlgorithmDefinition definition = PluginReflector.CreateDefinition( type );
                _pluginCache.Add( definition, type );
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
            return type.GetCustomAttributes().OfType<PluginIdentifierAttribute>().Any()
                && type.IsSubclassOf( typeof( AlgorithmPlugin ) );
        }


        /// <summary>
        /// Contains the root location of the DIPS key within the registry.
        /// </summary>
        private static readonly string _regLoc = @"SOFTWARE\Wow6432Node\DIPS";

        /// <summary>
        /// Contains the definition -> type pairings of all plugins loaded.
        /// </summary>
        private IDictionary<AlgorithmDefinition, Type> _pluginCache;
    }
}
