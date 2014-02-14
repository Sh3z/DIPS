using DIPS.Processor.Client;
using DIPS.Unity;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DIPS.ViewModel.Commands
{
    /// <summary>
    /// Represents the object providing information to the
    /// <see cref="PersistPipelineCommand"/> class.
    /// </summary>
    public interface IPipelineInfo : INotifyPropertyChanged
    {
        /// <summary>
        /// Gets the name given to the current pipeline.
        /// </summary>
        string PipelineName
        {
            get;
        }

        /// <summary>
        /// Gets the observable collection of process view models defining
        /// the pipeline.
        /// </summary>
        ObservableCollection<AlgorithmViewModel> SelectedProcesses
        {
            get;
        }
    }

    /// <summary>
    /// Defines the command used to persist a pipeline definition.
    /// </summary>
    public class PersistPipelineCommand : UnityCommand
    {
        /// <summary>
        /// Initilializes a new instance of the
        /// <see cref="PersistPipelineCommand"/> class.
        /// </summary>
        /// <param name="info">The source of the selected processes.</param>
        /// <exception cref="ArgumentNullException">info is null.</exception>
        public PersistPipelineCommand( IPipelineInfo info )
        {
            if( info == null )
            {
                throw new ArgumentNullException( "info" );
            }

            _info = info;
            _info.SelectedProcesses.CollectionChanged += _processesChanged;
            _info.PropertyChanged += _infoPropertyChanged;
        }


        /// <summary>
        /// Defines the method that determines whether the
        /// command can execute in its current state.
        /// </summary>
        /// <param name="parameter">Data used by the command.</param>
        /// <returns>true if this command can be executed; otherwise,
        /// false.</returns>
        public override bool CanExecute( object parameter )
        {
            return _info.SelectedProcesses.Any()
                && string.IsNullOrEmpty( _info.PipelineName ) == false
                && Container.Contains<ISaveFileService>()
                && Container.Contains<IPipelineManager>();
        }

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">Data used by the command.</param>
        public override void Execute( object parameter )
        {
            ISaveFileService service = Container.Resolve<ISaveFileService>();
            service.DefaultExtension = ".xml";
            service.DefaultName = _info.PipelineName;
            service.Filter = "Xml Files (.xml)|*.xml";
            if( service.SelectPath() )
            {
                _saveFile( service.Path );
            }
        }


        /// <summary>
        /// Saves the pipeline to the provided path.
        /// </summary>
        /// <param name="path">The path to save the file to.</param>
        private void _saveFile( string path )
        {
            IPipelineManager pipeManager = Container.Resolve<IPipelineManager>();
            XDocument xml = pipeManager.SavePipeline( _info.SelectedProcesses.Select( x => x.Definition ) );
            xml.Save( path );
        }
        
        /// <summary>
        /// Occurs when the set of built processes has changed
        /// </summary>
        /// <param name="sender">N/A</param>
        /// <param name="e">N/A</param>
        private void _processesChanged( object sender, NotifyCollectionChangedEventArgs e )
        {
            OnCanExecuteChanged();
        }

        /// <summary>
        /// Occurs when a property has been modified within the info object
        /// </summary>
        /// <param name="sender">N/A</param>
        /// <param name="e">N/A</param>
        private void _infoPropertyChanged( object sender, PropertyChangedEventArgs e )
        {
            if( e.PropertyName.Equals( "pipelinename", StringComparison.InvariantCultureIgnoreCase ) )
            {
                OnCanExecuteChanged();
            }
        }


        /// <summary>
        /// Maintains the object we can retrieve an enumerator from
        /// </summary>
        private IPipelineInfo _info;
    }
}
