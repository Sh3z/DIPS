using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor.Client.Eventing
{
    /// <summary>
    /// Provides event information as an input has been processed.
    /// </summary>
    [Serializable]
    public class InputProcessedArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InputProcessedArgs"/>
        /// class.
        /// </summary>
        /// <param name="identifier">The identifier provided to the
        /// image</param>
        /// <param name="image">The output of the processing procedure.</param>
        /// <exception cref="ArgumentNullExcepion">image is null</exception>
        public InputProcessedArgs( object identifier, Image image )
        {
            if( image == null )
            {
                throw new ArgumentNullException( "image" );
            }

            Identifier = identifier;
            Image = image;
        }


        /// <summary>
        /// Gets the processed <see cref="Image"/> from the processor.
        /// </summary>
        public Image Image
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the identifier provided by the client for the associated
        /// image.
        /// </summary>
        public object Identifier
        {
            get;
            private set;
        }
    }
}
