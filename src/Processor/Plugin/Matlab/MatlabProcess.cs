using DIPS.Matlab;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor.Plugin.Matlab
{
    /// <summary>
    /// Provides the ability to execute Matlab code as a processing step.
    /// </summary>
    [Algorithm( "Matlab", ParameterObjectType = typeof( MatlabProperties ) )]
    public class MatlabProcess : AlgorithmPlugin
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
            if( parameterObject is MatlabProperties == false )
            {
                throw new AlgorithmException( "Cannot execute Matlab process without MatlabProperties" );
            }

            MatlabProperties p = parameterObject as MatlabProperties;
            if( p.HasScript )
            {
                _execute( p );
            }
            else
            {
                throw new AlgorithmException( "No script provided" );
            }
        }


        /// <summary>
        /// Attempts to execute the script. If an exception occurs it is wrapped
        /// by an AlgorithmException
        /// </summary>
        /// <param name="p">The properties to execute the script with.</param>
        private void _tryExecute( MatlabProperties p )
        {
            try
            {
                _execute( p );
            }
            catch( Exception e )
            {
                string err = "Error running MatlabProcess. See inner exception.";
                throw new AlgorithmException( err, e );
            }
        }

        private void _execute( MatlabProperties p )
        {
            string scriptName = Path.GetFileName( p.ScriptFile );
            using( IDisposable tmp = new TemporaryFile( scriptName, p.SerializedFile ) )
            {
                MLApp.MLAppClass matlab = new MLApp.MLAppClass();
                foreach( MatlabParameter param in p.Parameters )
                {
                    IParameterValue value = param.Value;
                    value.Put( matlab, param.Name, param.Workspace );
                }

                matlab.Execute( string.Format( "open {1}", scriptName ) );
                matlab.Execute( "output = execute" );
            }
        }
    }
}
