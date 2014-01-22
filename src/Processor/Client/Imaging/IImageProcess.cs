using System.Drawing;

namespace DIPS.Processor.Client.Imaging
{
    /// <summary>
    /// Represents an image processing technique performed against a <see cref="Bitmap"/>.
    /// </summary>
    public interface IImageProcess
    {
        /// <summary>
        /// Performs the imaging techniques represented by this <see cref="IImageProcess"/>
        /// against the input <see cref="Bitmap"/>.
        /// </summary>
        /// <param name="input">The <see cref="Bitmap"/> representing the input
        /// image to process.</param>
        /// <returns>A <see cref="Bitmap"/> object representing the output from the
        /// process.</returns>
        Bitmap Execute( Bitmap input );
    }
}
