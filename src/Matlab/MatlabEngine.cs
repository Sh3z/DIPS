using MLApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Matlab
{
    /// <summary>
    /// Represents a Matlab instance and provides for common usage of its
    /// main functions.
    /// </summary>
    public class MatlabEngine : IDisposable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MatlabEngine"/> class.
        /// </summary>
        public MatlabEngine()
        {
            _session = new MatlabSession( new MLAppClass() );
            Global = new Workspace( _session, "global" );
            Base = new Workspace( _session, "base" );
        }


        void IDisposable.Dispose()
        {
            Shutdown();
        }


        /// <summary>
        /// Gets the "global" <see cref="Workspace"/> used by this
        /// <see cref="MatlabEngine"/>.
        /// </summary>
        public Workspace Global
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the "base" <see cref="Workspace"/> used by this
        /// <see cref="MatlabEngine"/>.
        /// </summary>
        public Workspace Base
        {
            get;
            private set;
        }


        /// <summary>
        /// Creates and returns a <see cref="SingleStatementMatlabCommand"/> that can be used
        /// within the current <see cref="MatlabEngine"/>.
        /// </summary>
        /// <param name="cmd">The command text to be executed when the command
        /// is ran.</param>
        /// <returns>A <see cref="SingleStatementMatlabCommand"/> applicable to this
        /// <see cref="MatlabEngine"/>.</returns>
        /// <exception cref="ArgumentException">cmd is null or empty.</exception>
        public MatlabCommand CreateCommand( string cmd )
        {
            _session.ThrowIfInvalid();

            if( string.IsNullOrEmpty( cmd ) )
            {
                throw new ArgumentException( "cmd" );
            }

            return new SingleStatementMatlabCommand( _session, cmd );
        }

        /// <summary>
        /// Creates and returns a <see cref="SingleStatementMatlabCommand"/> capable of executing
        /// multiple statements.
        /// </summary>
        /// <param name="cmds">The collection of command inputs that form the
        /// command.</param>
        /// <returns>A <see cref="SingleStatementMatlabCommand"/> applicable to this
        /// <see cref="MatlabEngine"/>.</returns>
        /// <exception cref="ArgumentNullException">cmds is null</exception>
        public MatlabCommand CreateCommand( IEnumerable<string> cmds )
        {
            _session.ThrowIfInvalid();

            if( cmds == null )
            {
                throw new ArgumentNullException( "cmds" );
            }

            return new CompositeMatlabCommand( _session, cmds );
        }

        /// <summary>
        /// Shuts down this <see cref="MatlabEngine"/> and invalidates any
        /// attached <see cref="Workspace"/>s or <see cref="SingleStatementMatlabCommand"/>s.
        /// </summary>
        public void Shutdown()
        {
            _session.Valid = false;
            _closeEngine();
        }


        /// <summary>
        /// Shuts down the matlab engine.
        /// </summary>
        private void _closeEngine()
        {
            try
            {
                _session.Matlab.Execute( "com.mathworks.mlservices.MLEditorServices.closeAll" );
                _session.Matlab.Quit();
            }
            catch( Exception e )
            {
                string err = "Error while shutting down engine. See inner exception.";
                throw new MatlabException( err, e );
            }
        }


        /// <summary>
        /// Contains the session for this Engine.
        /// </summary>
        private MatlabSession _session;
    }
}
