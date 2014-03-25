using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;
using Database.Repository;
using DIPS.Processor.Client;
using DIPS.Unity;
using Microsoft.Practices.Unity;

namespace DIPS.ViewModel.Commands
{
    public class PersistPipelineDatabaseCommand : UnityCommand
    {
        /// <summary>
        /// Initilializes a new instance of the
        /// <see cref="PersistPipelineCommand"/> class.
        /// </summary>
        /// <param name="info">The source of the selected processes.</param>
        /// <exception cref="ArgumentNullException">info is null.</exception>
        public PersistPipelineDatabaseCommand( IPipelineInfo info )
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
                && Container.Contains<IFilePickerService>()
                && Container.Contains<IPipelineManager>();
        }

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">Data used by the command.</param>
        public override void Execute( object parameter )
        {
                _saveFile();
        }


        /// <summary>
        /// Saves the pipeline to the provided path.
        /// </summary>
        /// <param name="path">The path to save the file to.</param>
        private void _saveFile()
        {
            IPipelineManager pipeManager = Container.Resolve<IPipelineManager>();
            var processes = _info.SelectedProcesses.Select( x => x.Definition );
            PipelineDefinition p = new PipelineDefinition( processes );
            XDocument xml = pipeManager.SavePipeline( p );
            
            ImageProcessingRepository imgProRep = new ImageProcessingRepository();

            try
            {
                if (string.IsNullOrEmpty(_info.PipelineID)) imgProRep.insertTechnique(_info.PipelineName, xml);
                else imgProRep.updateTechnique(Int32.Parse(_info.PipelineID), _info.PipelineName, xml);
                MessageBox.Show("Algorithm plan saved", "Plan saved", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception e)
            {
                MessageBox.Show("Error occured when saving algorithm plan", "Error", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                throw;
            }
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
