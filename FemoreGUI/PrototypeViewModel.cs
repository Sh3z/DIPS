using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Input;
using System.Windows.Media;

namespace FemoreGUI
{
    /// <summary>
    /// Represents the presentation logic used in the prototype application.
    /// </summary>
    public class PrototypeViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PrototypeViewModel"/> class.
        /// </summary>
        public PrototypeViewModel()
        {
            _processCmd = new ProcessImageCommand();
            _processCmd.ImageProcessed += image_processed;
        }


        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;


        /// <summary>
        /// Gets the command used the process an image.
        /// </summary>
        public ICommand ProcessImageCommand
        {
            get
            {
                return _processCmd;
            }
        }
        [DebuggerBrowsable( DebuggerBrowsableState.Never )]
        private ProcessImageCommand _processCmd;

        /// <summary>
        /// Gets or sets the image to be processed.
        /// </summary>
        public Bitmap ImageToProcess
        {
            get
            {
                return _processCmd.Image;
            }
            set
            {
                _processCmd.Image = value;
                property_changed( "ImageToProcess" );
                property_changed( "ToProcessSource" );
            }
        }

        /// <summary>
        /// Gets an <see cref="ImageSource"/> which can be used to display the image to be
        /// processed.
        /// </summary>
        public ImageSource ToProcessSource
        {
            get
            {
                BmpToBmpSourceConverter converter = new BmpToBmpSourceConverter();
                return converter.Convert( ImageToProcess, typeof( ImageSource ), null, null ) as ImageSource;
            }
        }

        /// <summary>
        /// Gets an <see cref="ImageSource"/> which can be used to display the processed
        /// image.
        /// </summary>
        public ImageSource ProcessedSource
        {
            get
            {
                BmpToBmpSourceConverter converter = new BmpToBmpSourceConverter();
                return converter.Convert( _processed, typeof( ImageSource ), null, null ) as ImageSource;
            }
        }

        /// <summary>
        /// Gets or sets the image that has been processed.
        /// </summary>
        public Bitmap ProcessedImage
        {
            get
            {
                return _processed;
            }
            set
            {
                _processed = value;
                property_changed( "ProcessedImage" );
                property_changed( "ProcessedSource" );
            }
        }
        [DebuggerBrowsable( DebuggerBrowsableState.Never )]
        private Bitmap _processed;



        /// <summary>
        /// Fires the OnPropertyChanged event
        /// </summary>
        /// <param name="propertyName">The name of the property that has changed.</param>
        private void property_changed( string propertyName )
        {
            if( PropertyChanged != null )
            {
                PropertyChanged( this, new PropertyChangedEventArgs( propertyName ) );
            }
        }

        /// <summary>
        /// Occurs when an image has been processed.
        /// </summary>
        /// <param name="sender">N/A</param>
        /// <param name="e">Event information.</param>
        private void image_processed( object sender, ImageProcessedArgs e )
        {
            ProcessedImage = e.Processed;
        }
    }
}
