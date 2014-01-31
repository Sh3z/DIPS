using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Util.Compression
{
    /// <summary>
    /// Represents a raw-byte compressor used for compressing and decompressing
    /// <see cref="byte"/> arrays.
    /// </summary>
    public interface ICompressor
    {
        /// <summary>
        /// Compresses an array of <see cref="byte"/>s into a new array.
        /// </summary>
        /// <param name="toCompress">The <see cref="byte"/> array to
        /// compress.</param>
        /// <returns>An array of <see cref="byte"/>s representing the
        /// compressed form of the input.</returns>
        byte[] Compress( byte[] toCompress );

        /// <summary>
        /// Decompresses an array of <see cref="byte"/>s back into their
        /// original state.
        /// </summary>
        /// <param name="toDecompress">The array of <see cref="byte"/>s to
        /// decompress.</param>
        /// <returns>An array of <see cref="byte"/>s representing their original
        /// state.</returns>
        byte[] Decompress( byte[] toDecompress );
    }
}
