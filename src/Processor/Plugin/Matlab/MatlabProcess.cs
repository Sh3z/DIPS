using DIPS.Matlab;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
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
            using( MatlabEngine engine = new MatlabEngine() )
            {
                _populateEngine( engine, p );
                _putInput( engine );
                var script = _extractScript( p );
                _runScript( engine, script );
                _setOutput( engine );
            }
        }

        private void _populateEngine( MatlabEngine e, MatlabProperties p )
        {
            foreach( MatlabParameter parameter in p.Parameters )
            {
                Workspace w = parameter.Workspace == "Base" ? e.Base : e.Global;
                IParameterValue value = parameter.Value;
                value.Put( parameter.Name, w );
            }
        }

        private void _putInput( MatlabEngine e )
        {
            string name = "Tmp.bmp";
            Input.Save( name, ImageFormat.Bmp );
            e.Base.PutObject( "dipsinput", name );
        }

        private IEnumerable<string> _extractScript( MatlabProperties p )
        {
            string scriptName = Path.GetFileName( p.ScriptFile );
            using( IDisposable tmp = new TemporaryFile( scriptName, p.SerializedFile ) )
            {
                // Remove any blank lines from the script
                var script = File.ReadAllLines( p.ScriptFile );
                return script.Where( x => string.IsNullOrEmpty( x ) == false );
            }
        }

        private void _runScript( MatlabEngine engine, IEnumerable<string> script )
        {
            try
            {
                string currentDir = Directory.GetCurrentDirectory();
                string cdToDir = string.Format( "cd {0}", currentDir );
                MatlabCommand cmd = engine.CreateCommand( cdToDir );
                cmd.Execute();
                MatlabCommand c = engine.CreateCommand( script );
                c.Execute();
            }
            catch( Exception e )
            {
                string err = "Error executing Matlab script. Ensure all variables have been provided " +
                    "and that the script functions within Matlab.";
                throw new AlgorithmException( err, e );
            }
        }

        private void _setOutput( MatlabEngine engine )
        {
            object output = _getDipsOutput( engine.Base );
            if( output is string == false )
            {
                throw new AlgorithmException( "Script did not output file path" );
            }

            string outputStr = string.Empty;
            try
            {
                outputStr = output as string;
                Output = Image.FromFile( outputStr );
            }
            catch( Exception e )
            {
                throw new AlgorithmException( "Script did not output path to image.", e );
            }
        }

        private object _getDipsOutput( Workspace workspace )
        {
            try
            {
                return workspace.GetVariable( "dipsoutput" );
            }
            catch( Exception e )
            {
                string err = "Error accessing DIPS output variable.";
                throw new AlgorithmException( err, e );
            }
        }
    }
}
