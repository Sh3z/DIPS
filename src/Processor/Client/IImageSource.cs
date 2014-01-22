using System.Drawing;

namespace DIPS.Processor.Client
{
    /// <summary>
    /// Represents the source of a <see cref="Bitmap"/> to be processed within a job.
    /// </summary>
    public interface IImageSource
    {
        /// <summary>
        /// Gets the <see cref="Bitmap"/> represented by this <see cref="IImageSource"/>.
        /// </summary>
        Bitmap Image
        {
            get;
        }
    }
}
