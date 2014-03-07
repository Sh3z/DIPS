using DIPS.Util.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.ViewModel.UserInterfaceVM.JobTracking
{
    /// <summary>
    /// Represents the factory used to construct results handlers.
    /// </summary>
    public class HandlerFactory : IHandlerFactory
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HandlerFactory"/>
        /// class.
        /// </summary>
        public HandlerFactory()
        {
            _handlers = new Dictionary<string, LoadedHandler>();
        }


        /// <summary>
        /// Loads the result handler implementations from the provided
        /// assembly.
        /// </summary>
        /// <param name="assembly">The <see cref="Assembly"/> to load
        /// handler instances from.</param>
        public void Load( Assembly assembly )
        {
            try
            {
                Type[] types = assembly.GetTypes();
                var validTypes = from type in types
                                 let handlerAttr = type.GetCustomAttribute( typeof( HandlerAttribute ) ) as HandlerAttribute
                                 where  handlerAttr != null &&
                                        type.Implements<IJobResultsHandler>()
                                 select new
                                 {
                                     Type = type,
                                     Identifier = handlerAttr.Identifier
                                 };
                foreach( var validType in validTypes )
                {
                    if( _handlers.ContainsKey( validType.Identifier ) == false )
                    {
                        LoadedHandler h = new LoadedHandler( (IJobResultsHandler)Activator.CreateInstance( validType.Type ) );
                        object[] nameAttr = validType.Type.GetCustomAttributes( typeof( DisplayNameAttribute ), false );
                        if( nameAttr.Any() && nameAttr.First() is DisplayNameAttribute )
                        {
                            DisplayNameAttribute a = (DisplayNameAttribute)nameAttr[0];
                            h.DisplayName = a.DisplayName;
                        }

                        _handlers.Add( validType.Identifier, h );
                    }
                }
            }
            catch { }
        }

        /// <summary>
        /// Creates the appropriate results handler for the given
        /// identifier.
        /// </summary>
        /// <param name="identifier">The identifier of the handler.</param>
        /// <returns>The <see cref="IJobResultsHandler"/> associated with
        /// the given identifier.</returns>
        public IJobResultsHandler CreateHandler( string identifier )
        {
            if( _handlers.ContainsKey( identifier ) )
            {
                LoadedHandler h = _handlers[identifier];
                return (IJobResultsHandler)h.Handler.Clone();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Creates a composite results handler from the given set of
        /// identifiers.
        /// </summary>
        /// <param name="identifiers">The set of identifiers for each handler
        /// to create.</param>
        /// <returns>The <see cref="IJobResultsHandler"/> representing the
        /// composite handler.</returns>
        public IJobResultsHandler CreateHandler( params string[] identifiers )
        {
            if( identifiers == null || identifiers.Length == 0 )
            {
                return new CompositeHandler();
            }
            else
            {
                CompositeHandler handler = new CompositeHandler();
                identifiers.ForEach( x => handler.Add( CreateHandler( x ) ) );
                return handler;
            }
        }

        /// <summary>
        /// Returns an enumerator used to enumerate through the collection
        /// of registered handler identifiers.
        /// </summary>
        /// <returns>A System.Collections.Generic.IEnumerator used to enumerate
        /// through the set of registered handlers.</returns>
        public IEnumerator<string> GetEnumerator()
        {
            return _handlers.Keys.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }


        /// <summary>
        /// Retains the set of registered handlers.
        /// </summary>
        private IDictionary<string, LoadedHandler> _handlers;
    }
}
