using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Compression
{
    /// <summary>
    /// Provides GZip compression against incoming byte arrays.
    /// </summary>
    [Compressor( "GZip" )]
    public class GZipCompressor : ICompressor
    {
        /// <summary>
        /// Compresses an array of <see cref="byte"/>s into a new array.
        /// </summary>
        /// <param name="toCompress">The <see cref="byte"/> array to
        /// compress.</param>
        /// <returns>An array of <see cref="byte"/>s representing the
        /// compressed form of the input.</returns>
        public byte[] Compress( byte[] toCompress )
        {
            if( toCompress == null )
            {
                return new byte[0];
            }

            using( MemoryStream ms = new MemoryStream() )
            {
                using( GZipStream z = new GZipStream( ms, CompressionMode.Compress, true ) )
                {
                    z.Write( toCompress, 0, toCompress.Length );
                }

                return ms.ToArray();
            }
        }

        /// <summary>
        /// Decompresses an array of <see cref="byte"/>s back into their
        /// original state.
        /// </summary>
        /// <param name="toDecompress">The array of <see cref="byte"/>s to
        /// decompress.</param>
        /// <returns>An array of <see cref="byte"/>s representing their original
        /// state.</returns>
        public byte[] Decompress( byte[] toDecompress )
        {
            if( toDecompress == null )
            {
                return new byte[0];
            }

            using( MemoryStream stream = new MemoryStream() )
            {
                using( Stream byteStream = new MemoryStream( toDecompress ) )
                {
                    using( var z = new GZipStream( byteStream, CompressionMode.Decompress ) )
                    {
                        z.CopyTo( stream );
                    }
                }

                return stream.ToArray();
            }
        }
    }
}
