using MLApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Matlab
{
    /// <summary>
    /// Represents a command that can be executed against a Matlab engine.
    /// </summary>
    public class MatlabCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MatlabCommand"/> class.
        /// </summary>
        /// <param name="session">The <see cref="MatlabSession"/> this
        /// <see cref="MatlabCommand"/> is valid within,</param>
        /// <param name="cmd">The command to execute.</param>
        /// <exception cref="ArgumentException">cmd is null or empty.</exception>
        /// <exception cref="ArgumentNullException">session is null.</exception>
        public MatlabCommand( MatlabSession session, string cmd )
        {
            if( session == null )
            {
                throw new ArgumentNullException( "session" );
            }

            if( string.IsNullOrEmpty( cmd ) )
            {
                throw new ArgumentException( "cmd" );
            }

            _session = session;
            Input = cmd;
        }


        /// <summary>
        /// Gets the input command to dispatch to Matlab.
        /// </summary>
        public string Input
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the output of the command from Matlab.
        /// </summary>
        public string Output
        {
            get;
            private set;
        }

        /// <summary>
        /// Executes this <see cref="MatlabCommand"/>.
        /// </summary>
        /// <exception cref="MatlabException">the command has been
        /// invalidated.</exception>
        public void Execute()
        {
            _session.ThrowIfInvalid();
            Output = _session.Matlab.Execute( Input );
        }


        /// <summary>
        /// Contains the session this command belongs to.
        /// </summary>
        private MatlabSession _session;
    }
}
