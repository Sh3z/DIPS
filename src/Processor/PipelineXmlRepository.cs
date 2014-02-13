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
    /// Represents the repository exposing pipeline Xml factories.
    /// </summary>
    public class PipelineXmlRepository : IPluginRegistry, IPipelineRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PipelineXmlRepository"/>
        /// class.
        /// </summary>
        public PipelineXmlRepository()
        {
            _interpreters = new Dictionary<string, IPipelineXmlInterpreter>();
        }


        /// <summary>
        /// Initializes this <see cref="IPluginRegistry"/> from the loaded
        /// <see cref="Assembly"/>.
        /// </summary>
        /// <param name="assembly">The <see cref="Assembly"/> specified within
        /// the Windows registry containing plugins.</param>
        public void Initialize( Assembly assembly )
        {
            foreach( Type type in assembly.GetTypes().Where( _isValidType ) )
            {
                _registerType( type );
            }
        }

        /// <summary>
        /// Fetches the <see cref="IPipelineXmlInterpreter"/> for a given
        /// process.
        /// </summary>
        /// <param name="processName">The name of the process to fetch
        /// the <see cref="IPipelineXmlInterpreter"/> for.</param>
        /// <returns>The <see cref="IPipelineXmlInterpreter"/> capable
        /// of saving or restoring the state of the associated
        /// process.</returns>
        public IPipelineXmlInterpreter FetchInterpreter( string processName )
        {
            IPipelineXmlInterpreter interpreter = null;
            if( processName != null && _interpreters.ContainsKey( processName ) )
            {
                _interpreters.TryGetValue( processName, out interpreter );
            }

            return interpreter;
        }

        /// <summary>
        /// Converts the contents of this <see cref="IPipelineRepository"/>
        /// into dictionary form.
        /// </summary>
        /// <returns>The name/interpreter pairings stored within this
        /// repository.</returns>
        public IDictionary<string, IPipelineXmlInterpreter> ToDictionary()
        {
            return new Dictionary<string, IPipelineXmlInterpreter>( _interpreters );
        }


        /// <summary>
        /// Determines whether the incoming type is valid
        /// </summary>
        /// <param name="type">The incoming type</param>
        /// <returns>true if the type is attributed correctly and
        /// implements the Xml interface</returns>
        private bool _isValidType( Type type )
        {
            PipelineXmlOriginatorAttribute attr =
                type.GetCustomAttribute(
                    typeof( PipelineXmlOriginatorAttribute ) ) as PipelineXmlOriginatorAttribute;
            if( attr == null )
            {
                return false;
            }

            return type.GetInterfaces().Any( x => x == typeof( IPipelineXmlInterpreter ) );
        }


        /// <summary>
        /// Registers the incoming valid type in this repo
        /// </summary>
        /// <param name="type">The type to register</param>
        private void _registerType( Type type )
        {
            try
            {
                PipelineXmlOriginatorAttribute attr =
                    type.GetCustomAttribute(
                        typeof( PipelineXmlOriginatorAttribute ) ) as PipelineXmlOriginatorAttribute;
                Type pluginType = attr.PluginType;
                AlgorithmAttribute pluginAttr =
                    pluginType.GetCustomAttribute( typeof( AlgorithmAttribute ) ) as AlgorithmAttribute;
                string pluginID = pluginAttr.PluginName;

                IPipelineXmlInterpreter interpreter = Activator.CreateInstance( type ) as IPipelineXmlInterpreter;
                _interpreters.Add( pluginID, interpreter );
            }
            catch( Exception e )
            {

            }
        }


        /// <summary>
        /// Contains the name -> interpreter pairings
        /// </summary>
        private IDictionary<string, IPipelineXmlInterpreter> _interpreters;
    }
}
