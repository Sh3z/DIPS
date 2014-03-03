using DIPS.Database;
using DIPS.Processor.Client;
using DIPS.Processor.Client.JobDeployment;
using DIPS.Unity;
using DIPS.ViewModel.UserInterfaceVM.JobTracking;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.ViewModel.Commands
{
    /// <summary>
    /// Represents the source of job information used by the
    /// <see cref="EnqueueJobCommand"/>.
    /// </summary>
    public interface IJobSource
    {
        /// <summary>
        /// Gets the set of files to be input into the processor.
        /// </summary>
        IEnumerable<FileInfo> Files
        {
            get;
        }

        /// <summary>
        /// Gets the constructed pipeline definition to input to the processor.
        /// </summary>
        PipelineDefinition Pipeline
        {
            get;
        }
    }

    /// <summary>
    /// Represents the command used to deploy jobs to the service.
    /// </summary>
    public class EnqueueJobCommand : UnityCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EnqueueJobCommand"/>
        /// class.
        /// </summary>
        /// <param name="source">The <see cref="IJobSource"/> providing the
        /// information on the jobs to deploy.</param>
        /// <exception cref="ArgumentNullException">source is null.</exception>
        public EnqueueJobCommand( IJobSource source )
        {
            if( source == null )
            {
                throw new ArgumentNullException( "source" );
            }

            _source = source;
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
            return  Container != null &&
                    Container.Contains<IProcessingService>() &&
                    Container.Contains<IJobTracker>() &&
                    _source.Files != null && _source.Files.Any() &&
                    _source.Pipeline != null && _source.Pipeline.Any();
        }

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">Data used by the command. If the command
        /// does not require data to be passed, this object can be set to
        /// null.</param>
        public override void Execute( object parameter )
        {
            IProcessingService service = Container.Resolve<IProcessingService>();
            ObjectJobDefinition d = new ObjectJobDefinition( _source.Pipeline, _filesToInputs() );
            JobRequest r = new JobRequest( d );
            IJobTicket t = service.JobManager.EnqueueJob( r );
            IJobTracker tracker = Container.Resolve<IJobTracker>();
            tracker.Add( t );
            _openQueueUI();
        }


        /// <summary>
        /// Converts the files in the current job source in job inputs
        /// </summary>
        /// <returns>the set of job inputs to hand to the processor</returns>
        private IEnumerable<JobInput> _filesToInputs()
        {
            List<JobInput> jobs = new List<JobInput>();
            foreach( FileInfo file in _source.Files )
            {
                try
                {
                    Image img = _extractImage( file );
                    JobInput i = new JobInput( img );
                    i.Identifier = file;
                    jobs.Add( i );
                }
                catch
                {

                }
            }

            return jobs;
        }

        /// <summary>
        /// Extracts the image from the file
        /// </summary>
        /// <param name="file">The file to extract the image from</param>
        /// <returns>The image contained within the file</returns>
        private Image _extractImage( FileInfo file )
        {
            Image theImg = null;
            string path = string.Format( @"{0}/{1}", file.Directory, file.Name );
            string ext = file.Extension.ToLower();
            if( ext == ".dicom" )
            {
                readImage reader = new readImage();
                byte[] bytes = reader.blob( path );
                using( Stream stream = new MemoryStream( bytes ) )
                {
                    Image tmp = Image.FromStream( stream );
                    theImg = new Bitmap( tmp );
                }
            }
            else
            {
                theImg = Image.FromFile( path );
            }

            return theImg;
        }

        /// <summary>
        /// Opens the Queue window if registered within unity
        /// </summary>
        private void _openQueueUI()
        {
            PresentQueueCommand c = new PresentQueueCommand();
            c.Container = this.Container;
            if( c.CanExecute( null ) )
            {
                c.Execute( null );
            }
        }


        /// <summary>
        /// Contains the source of the jobs we deploy.
        /// </summary>
        private IJobSource _source;
    }
}
