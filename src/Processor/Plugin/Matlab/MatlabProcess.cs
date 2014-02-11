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


        private void _execute( MatlabProperties p )
        {
            // Save the script to a tmp location, run it, then delete it
            _saveScript( p );

            MLApp.MLAppClass matlab = new MLApp.MLAppClass();
            foreach( MatlabParameter param in p.Parameters )
            {
                IParameterValue value = param.Value;
                value.Put( matlab, param.Name, param.Workspace );
            }

            string cdcmd = string.Format( "cd {0}/{1}", _scriptTmpDir, _scriptTmpFileName );
            //matlab.Execute( cdcmd );
            //matlab.Execute( string.Format( "open {1}", _scriptTmpFileName ) );
            //matlab.Execute( string.Format( "dbstop {0}", _scriptTmpFileName ) );
            //matlab.Execute( "output = execute" );
        }

        private void _saveScript( MatlabProperties p )
        {
            _scriptTmpFileName = Path.GetFileName( p.ScriptFile );
            _scriptTmpDir = string.Format( @"{0}/tmp", Directory.GetCurrentDirectory() );

            if( Directory.Exists( _scriptTmpDir ) == false )
            {
                Directory.CreateDirectory( _scriptTmpDir );
            }

            File.WriteAllBytes( string.Format( @"{0}/{1}", _scriptTmpDir, _scriptTmpFileName ), p.SerializedFile );
        }

        private void _deleteScript()
        {
            if( File.Exists( _scriptTmpDir ) )
            {
                File.Delete( _scriptTmpDir );
            }
        }


        private string _scriptTmpDir;

        private string _scriptTmpFileName;
    }
}
