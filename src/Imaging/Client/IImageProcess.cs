using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Femore.Imaging.Client
{
    /// <summary>
    /// Represents a process which is executed against a <see cref="Bitmap"/> to produce a different image.
    /// </summary>
    /// <remarks>
    /// Classes which implement this interface should be serializable.
    /// </remarks>
    public interface IImageProcess : IEquatable<IImageProcess>
    {
        Bitmap ProcessedImage
        {
            get;
        }

        void Process( Bitmap image );
    }
}
