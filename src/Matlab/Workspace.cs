using MLApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Matlab
{
    /// <summary>
    /// Represents a single workspace used by a Matlab instance.
    /// </summary>
    public class Workspace
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Workspace"/> class.
        /// </summary>
        /// <param name="matlab">The current <see cref="MatlabSession"/> this
        /// <see cref="Workspace"/> belongs to.</param>
        /// <param name="name">The identifier to give to this
        /// <see cref="Workspace"/>.</param>
        /// <exception cref="ArgumentException">name is null or empty.</exception>
        /// <exception cref="ArgumentNullException">session is null.</exception>
        public Workspace( MatlabSession session, string name )
        {
            if( string.IsNullOrEmpty( name ) )
            {
                throw new ArgumentException( "name" );
            }

            if( session == null )
            {
                throw new ArgumentNullException( "session" );
            }

            WorkspaceName = name;
            _session = session;
        }


        /// <summary>
        /// Gets the identifier for this <see cref="Workspace"/>.
        /// </summary>
        public string WorkspaceName
        {
            get;
            private set;
        }


        /// <summary>
        /// Puts an arbitrary object onto this <see cref="Workspace"/>.
        /// </summary>
        /// <param name="name">The name to assign the corresponding value within
        /// the workspace.</param>
        /// <param name="value">The value to assign.</param>
        /// <exception cref="InvalidSessionException">the Matlab session has
        /// expired.</exception>
        /// <exception cref="ArgumentException">name is null or empty.</exception>
        public void PutObject( string name, object value )
        {
            _session.ThrowIfInvalid();

            if( string.IsNullOrEmpty( name ) )
            {
                throw new ArgumentException( "name" );
            }

            _session.Matlab.PutWorkspaceData( name, WorkspaceName, value );
        }

        /// <summary>
        /// Gets the value of a variable within this <see cref="Workspace"/>.
        /// </summary>
        /// <param name="name">The identifier of the variable to extract.</param>
        /// <returns>The value of the variable identified by the name.</returns>
        /// <exception cref="InvalidSessionException">the Matlab session has
        /// expired.</exception>
        /// <exception cref="MatlabException">the variable has not been set, or
        /// the value cannot be accessed.</exception>
        public object GetVariable( string name )
        {
            _session.ThrowIfInvalid();

            if( string.IsNullOrEmpty( name ) )
            {
                throw new ArgumentException( "name" );
            }

            return _tryGetVariable( name );
        }


        /// <summary>
        /// Attempts to resolve the variable in this space with the provided
        /// identifier. If an exception occurs, it is wrapped in a MatlabException
        /// </summary>
        /// <param name="name">The name of the variable to retrieve</param>
        /// <returns>The value of the variable</returns>
        private object _tryGetVariable( string name )
        {
            _session.ThrowIfInvalid();

            try
            {
                object output = null;
                _session.Matlab.GetWorkspaceData( name, WorkspaceName, out output );
                return output;
            }
            catch( Exception e )
            {
                string err = string.Format(
                    "Exception while retrieving variable \"{0}\"",
                    name );
                throw new MatlabException( err, e );
            }
        }


        /// <summary>
        /// Contains the Matlab session this workspace belongs to.
        /// </summary>
        private MatlabSession _session;
    }
}
