using MLApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Matlab
{
    /// <summary>
    /// Represents a command that encapsulates a single statement to execute
    /// within Matlab.
    /// </summary>
    public class SingleStatementMatlabCommand : MatlabCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SingleStatementMatlabCommand"/> class.
        /// </summary>
        /// <param name="session">The <see cref="MatlabSession"/> this
        /// <see cref="SingleStatementMatlabCommand"/> is valid within,</param>
        /// <param name="cmd">The command to execute.</param>
        /// <exception cref="ArgumentException">cmd is null or empty.</exception>
        /// <exception cref="ArgumentNullException">session is null.</exception>
        public SingleStatementMatlabCommand( MatlabSession session, string cmd )
            : base( session )
        {
            if( string.IsNullOrEmpty( cmd ) )
            {
                throw new ArgumentException( "cmd" );
            }

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
        /// Executes this <see cref="SingleStatementMatlabCommand"/>.
        /// </summary>
        /// <exception cref="InvalidSessionException">the command has been
        /// invalidated.</exception>
        public override void Execute()
        {
            Session.ThrowIfInvalid();
            Output = Session.Matlab.Execute( Input );
        }
    }
}
