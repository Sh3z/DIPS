using DIPS.Processor.Client;
using DIPS.Unity;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DIPS.ViewModel.Commands
{
    /// <summary>
    /// Represents the command used to load existing pipeline definitions.
    /// </summary>
    public class LoadPipelineCommand : UnityCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoadPipelineCommand"/>
        /// class.
        /// </summary>
        /// <param name="info">The <see cref="IPipelineInfo"/> that is the
        /// target of loading operations.</param>
        /// <exception cref="ArgumentNullException">info is null.</exception>
        public LoadPipelineCommand( IPipelineInfo info )
        {
            if( info == null )
            {
                throw new ArgumentNullException( "info" );
            }

            _info = info;
        }

        /// <summary>
        /// Defines the method that determines whether the command can
        /// execute in its current state.
        /// </summary>
        /// <param name="parameter">Data used by the command. If the
        /// command does not require data to be passed, this object can be
        /// set to null.</param>
        /// <returns>true if this command can be executed; otherwise,
        /// false.</returns>
        public override bool CanExecute( object parameter )
        {
            return  Container.Contains<IFilePickerService>()
                    && Container.Contains<IPipelineManager>();
        }

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">Data used by the command. If the command
        /// does not require data to be passed, this object can be set to
        /// null.</param>
        public override void Execute( object parameter )
        {
            IFilePickerService service = Container.Resolve<IFilePickerService>();
            service.Mode = FilePickerMode.Open;
            service.Filter = "Xml Files (.xml)|*.xml";
            if( service.SelectPath() )
            {
                _loadFile( service.Path );
            }
        }


        /// <summary>
        /// Loads the pipeline at the provided path
        /// </summary>
        /// <param name="path">The path of the persisted pipeline file</param>
        private void _loadFile( string path )
        {
            IPipelineManager manager = Container.Resolve<IPipelineManager>();
            XDocument doc = XDocument.Load( path );
            var restoredPipeline = manager.RestorePipeline( doc );
            _info.SelectedProcesses.Clear();
            _info.PipelineName = Path.GetFileNameWithoutExtension( path );

            foreach( var process in restoredPipeline )
            {
                _info.SelectedProcesses.Add( new AlgorithmViewModel( process ) );
            }
        }


        /// <summary>
        /// Contains the target of load operations.
        /// </summary>
        private IPipelineInfo _info;
    }
}
