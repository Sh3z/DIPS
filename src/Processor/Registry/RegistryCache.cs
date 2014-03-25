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
            _assemblies = new List<Assembly>();
            _initialize();
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
        /// Initializes the provided <see cref="IPluginRegistry"/> with
        /// the information provided within the Windows registry.
        /// </summary>
        /// <param name="registry">The <see cref="IPluginRegistry"/> to
        /// initialize.</param>
        public void Initialize( IPluginRegistry registry )
        {
            foreach( Assembly assembly in _assemblies )
            {
                registry.Initialize( assembly );
            }
        }


        /// <summary>
        /// Initializes the cache from the values in the registry
        /// </summary>
        private void _initialize()
        {
            try
            {
                RegistryKey dips = Microsoft.Win32.Registry.LocalMachine.OpenSubKey( _regLoc );
                _loadAssemblies( dips.OpenSubKey( "Plugins" ) );
            }
            catch( Exception e )
            {

            }
        }

        /// <summary>
        /// Loads the assemblies from the provided key
        /// </summary>
        /// <param name="source">The source of the plugins key</param>
        private void _loadAssemblies( RegistryKey source )
        {
            foreach( string name in source.GetValueNames() )
            {
                string value = source.GetValue( name ) as string;
                _tryLoadAssembly( value );
            }
        }

        /// <summary>
        /// Attempts to load and cache the assembly with the given name
        /// </summary>
        /// <param name="value">The name of the assembly</param>
        private void _tryLoadAssembly( string value )
        {
            try
            {
                Assembly assembly = Assembly.LoadFrom( value );
                _assemblies.Add( assembly );
            }
            catch( Exception e )
            {

            }
        }


        /// <summary>
        /// Contains the root location of the DIPS key within the registry.
        /// </summary>
        private static readonly string _regLoc = @"SOFTWARE\Wow6432Node\DIPS";

        /// <summary>
        /// Maintains the set of loaded assemblies from the Registry.
        /// </summary>
        private ICollection<Assembly> _assemblies;
    }
}
