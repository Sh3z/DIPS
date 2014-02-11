using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DIPS.Matlab
{
    /// <summary>
    /// Represents the <see cref="MatlabCommand"/> used to execute multiple
    /// commands in a single call.
    /// </summary>
    public class CompositeMatlabCommand : MatlabCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CompositeMatlabCommand"/>
        /// class.
        /// </summary>
        /// <param name="session">The <see cref="MatlabSession"/> this
        /// <see cref="MatlabCommand"/> is associated with.</param>
        /// <param name="cmds">The set of individual commands to execute.</param>
        /// <exception cref="ArgumentNullException">session or cmds are null.</exception>
        public CompositeMatlabCommand( MatlabSession session, IEnumerable<string> cmds )
            : base( session )
        {
            if( cmds == null )
            {
                throw new ArgumentNullException( "session" );
            }

            _commands = new List<SingleStatementMatlabCommand>();
            foreach( string cmd in cmds )
            {
                SingleStatementMatlabCommand command = new SingleStatementMatlabCommand( session, cmd );
                _commands.Add( command );
            }
        }


        /// <summary>
        /// Gets the set of outputs from each executed line.
        /// </summary>
        public IEnumerable<string> Outputs
        {
            get
            {
                return _commands.Select( x => x.Output );
            }
        }


        /// <summary>
        /// Executes the logic of this <see cref="MatlabCommand"/>.
        /// </summary>
        public override void Execute()
        {
            Session.ThrowIfInvalid();

            foreach( ICommand cmd in _commands )
            {
                cmd.Execute( null );
            }
        }


        /// <summary>
        /// Contains the set of commands to be executed individually.
        /// </summary>
        private ICollection<SingleStatementMatlabCommand> _commands;
    }
}
