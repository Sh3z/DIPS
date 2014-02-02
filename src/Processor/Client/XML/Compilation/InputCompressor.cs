using DIPS.Util.Compression;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor.XML.Compilation
{
    /// <summary>
    /// Provides helper methods to compress/decompress <see cref="Image"/>
    /// objects serialized in Xml.
    /// </summary>
    public static class InputCompressor
    {
        /// <summary>
        /// Converts an <see cref="Image"/> into an array of it's
        /// <see cref="byte"/>s.
        /// </summary>
        /// <param name="image">The <see cref="Image"/> object to convert
        /// into <see cref="byte"/>s.</param>
        /// <returns>A one-dimensional array of <see cref="byte"/>s representing
        /// the <see cref="Image"/> provided.</returns>
        public static byte[] ImageToBytes( Image image )
        {
            byte[] byteArray = new byte[0];

            if( image != null )
            {
                using( MemoryStream stream = new MemoryStream() )
                {
                    image.Save( stream, ImageFormat.Bmp );
                    stream.Close();

                    byteArray = stream.ToArray();
                }
            }

            return byteArray;
        }

        /// <summary>
        /// Compresses an <see cref="Image"/> into an array of
        /// <see cref="byte"/>s.
        /// </summary>
        /// <param name="input">The <see cref="Image"/> to convert into
        /// <see cref="byte"/>s and compress.</param>
        /// <param name="compressor">The <see cref="ICompressor"/> to use in the
        /// compression.</param>
        /// <returns>A one-dimensional array of <see cref="byte"/>s representing
        /// the image, compressed.</returns>
        /// <exception cref="ArgumentNullException">compressor is null.</exception>
        public static byte[] Compress( Image input, ICompressor compressor )
        {
            if( compressor == null )
            {
                throw new ArgumentNullException( "compressor" );
            }

            byte[] imgBytes = new byte[0];
            byte[] b = ImageToBytes( input );
            return compressor.Compress( b );
        }

        /// <summary>
        /// Decompresses an <see cref="Image"/> object, represented
        /// by the array of <see cref="byte"/>s.
        /// </summary>
        /// <param name="imgInput">The <see cref="byte"/> array representing
        /// the <see cref="Image"/>.</param>
        /// <param name="compressor">The <see cref="ICompressor"/> to use in
        /// the decompression.</param>
        /// <returns>An <see cref="Image"/> represented by the inbound
        /// array of <see cref="byte"/>s.</returns>
        /// <exception cref="ArgumentException">the <see cref="byte"/> array
        /// does not represent a previously compressed
        /// <see cref="Image"/>.</exception>
        /// <exception cref="ArgumentNullException">imgInput or compressor are
        /// null.</exception>
        public static Image Decompress( byte[] imgInput, ICompressor compressor )
        {
            if( imgInput == null )
            {
                throw new ArgumentNullException( "imgInput" );
            }

            if( compressor == null )
            {
                throw new ArgumentNullException( "compressor" );
            }

            try
            {
                using( var byteStream = new MemoryStream( imgInput ) )
                {
                    byte[] decompressed = compressor.Decompress( byteStream.ToArray() );
                    using( var decompStream = new MemoryStream( decompressed ) )
                    {
                        Image img = Image.FromStream( decompStream );
                        return new Bitmap( img );
                    }
                }
            }
            catch( Exception e )
            {
                string err = "Input bytes do not represent compressed image.";
                throw new ArgumentException( err, e );
            }
        }
    }
}
