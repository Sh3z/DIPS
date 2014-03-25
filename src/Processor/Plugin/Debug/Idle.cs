using DIPS.Processor.Plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DIPS.Processor.Plugin.Debug
{
    /// <summary>
    /// Represents a plugin that sits idly for a few minutes to simulate
    /// long jobs.
    /// </summary>
    [Algorithm( "idle", ParameterObjectType = typeof( IdleProperties ) )]
    [AlgorithmMetadata( "Idle" )]
    public class Idle : AlgorithmPlugin
    {
        /// <summary>
        /// Executes the algorithm represented by this <see cref="AlgorithmPlugin"/>.
        /// </summary>
        /// <param name="parameterObject">An object of the type provided by the
        /// <see cref="AlgorithmAttribute"/> describing the properties used by this
        /// <see cref="AlgorithmPlugin"/>.</param>
        /// <exception cref="AlgorithmException">an internal exception has occured. This
        /// is accessed through the inner exception property.</exception>
        public override void Run( object parameterObject )
        {
            IdleProperties p = parameterObject as IdleProperties;
            if( p != null )
            {
                // 'Seconds' is actually ms, so multiply up to seconds
                Thread.Sleep( p.Seconds * 1000 );
            }
        }
    }
}
