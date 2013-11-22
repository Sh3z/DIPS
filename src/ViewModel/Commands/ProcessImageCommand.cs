using DIPS.Imaging.Client;
using DIPS.Imaging.Core;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Input;

namespace DIPS.ViewModel.Commands
{
    /// <summary>
    /// Defines the command used to execute the processing algorithm against a single image.
    /// </summary>
    public class ProcessImageCommand : ICommand
    {
        public ProcessImageCommand( Algorithm processingAlgorithm )
        {
            _algorithm = processingAlgorithm;
        }


        /// <summary>
        /// Gets or sets the <see cref="Bitmap"/> which will be processed when this commad is
        /// executed.
        /// </summary>
        public Bitmap Image
        {
            get
            {
                return _image;
            }
            set
            {
                _image = value;
                if( CanExecuteChanged != null )
                {
                    CanExecuteChanged( this, EventArgs.Empty );
                }
            }
        }
        [DebuggerBrowsable( DebuggerBrowsableState.Never )]
        private Bitmap _image;

        /// <summary>
        /// Occurs when an image has been processed.
        /// </summary>
        public event EventHandler<ImageProcessedArgs> ImageProcessed;

        /// <summary>
        /// Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <param name="parameter">Data used by the command. If the command does not require data
        /// to be passed, this object can be set to null.</param>
        /// <returns>true if this command can be executed; otherwise, false.</returns>
        public bool CanExecute( object parameter )
        {
            return Image != null;
        }

        /// <summary>
        /// Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">Data used by the command. If the command does not require
        /// data to be passed, this object can be set to null.</param>
        public void Execute( object parameter )
        {
            // For this prototype, we will simply create a processor and execute it.
            // In the final release, factories and other doohickies will be used to further
            // seperate the processes.
            var result = PrototypeProcessor.Process( Image, _algorithm );
            if( ImageProcessed != null )
            {
                ImageProcessedArgs args = new ImageProcessedArgs( Image, result );
                ImageProcessed( this, args );
            }
        }


        private Algorithm _algorithm;
    }
}
