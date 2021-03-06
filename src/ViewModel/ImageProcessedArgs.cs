﻿using System;
using System.Drawing;

namespace DIPS.ViewModel
{
    /// <summary>
    /// Provides event information as an image is processed.
    /// </summary>
    public class ImageProcessedArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new <see cref="ImageProcessedArgs"/> instance.
        /// </summary>
        /// <param name="original">The <see cref="Bitmap"/> provided to the processing algorithm.</param> 
        /// <param name="processed">The <see cref="Bitmap"/> returned by the algorithm.</param>
        public ImageProcessedArgs( Bitmap original, Bitmap processed )
        {
            Unprocessed = original;
            Processed = processed;
        }


        /// <summary>
        /// Gets the <see cref="Bitmap"/> used by the processing algorithm. 
        /// </summary>
        public Bitmap Unprocessed
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the <see cref="Bitmap"/> generated by the processing algorithm.
        /// </summary>
        public Bitmap Processed
        {
            get;
            private set;
        }
    }
}
