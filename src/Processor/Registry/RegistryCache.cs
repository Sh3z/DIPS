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
    public sealed class RegistryCache
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
        /// Gets an enumerable set of the loaded plugins.
        /// </summary>
        /// <returns>An enumerable set of <see cref="AlgorithmDefinition"/>s
        /// representing the loaded plugins.</returns>
        public IEnumerable<AlgorithmDefinition> GetLoadedPlugins()
        {
            return _pluginCache.Keys;
        }

        /// <summary>
        /// Resolves the true <see cref="Type"/> of the algorithm represented
        /// by the provided <see cref="AlgorithmDefinition"/> instance.
        /// </summary>
        /// <param name="definition">The cached <see cref="AlgorithmDefinition"/>
        /// object.</param>
        /// <returns>The real <see cref="Type"/> of the plugin represented by the
        /// <see cref="AlgorithmDefinition"/>, or null if one cannot be found.</returns>
        public Type ResolveType( AlgorithmDefinition definition )
        {
            Type output = null;

            if( definition != null )
            {
                _pluginCache.TryGetValue( definition, out output );
            }
            
            return output;
        }

        /// <summary>
        /// Resolves the <see cref="AlgorithmPlugin"/> represented by the
        /// <see cref="AlgorithmDefinition"/>.
        /// </summary>
        /// <param name="definition">The <see cref="AlgorithmDefinition"/> to
        /// resolve the plugin for.</param>
        /// <returns>The <see cref="AlgorithmPlugin"/> instance represented by
        /// the <see cref="AlgorithmDefinition"/>, or null if no plugin is defined.</returns>
        public AlgorithmPlugin ResolvePlugin( AlgorithmDefinition definition )
        {
            if( definition == null )
            {
                return null;
            }
            else
            {
                return _generatePluginFromDefinition( definition );
            }
        }

        /// <summary>
        /// Determines whether a named algorithm has been loaded into the
        /// cache.
        /// </summary>
        /// <param name="algorithmName">The name of the algorithm to determine
        /// has been cached.</param>
        /// <returns><c>true</c> if an algorithm with the given name has been
        /// cached; otherwise, <c>false</c>.</returns>
        public bool HasCachedAlgorithm( string algorithmName )
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
        /// Creates and returns the AlgorithmPlugin from the given definition
        /// </summary>
        /// <param name="definition">The definition of the plugin to create</param>
        /// <returns>The AlgorithmPlugin instance, or null if it is not defined</returns>
        private AlgorithmPlugin _generatePluginFromDefinition( AlgorithmDefinition definition )
        {
            Type pluginType = ResolveType( definition );
            if( pluginType == null )
            {
                return null;
            }
            else
            {
                return Activator.CreateInstance( pluginType ) as AlgorithmPlugin;
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
