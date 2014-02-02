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
        /// <returns>A one-dimensional array of <see cref="byte"/>s representing
        /// the image, compressed.</returns>
        public static byte[] Compress( Image input )
        {
            byte[] imgBytes = new byte[0];
            byte[] b = ImageToBytes( input );
            using( MemoryStream ms = new MemoryStream() )
            {
                using( GZipStream z = new GZipStream( ms, CompressionMode.Compress, true ) )
                {
                    z.Write( b, 0, b.Length );
                }

                imgBytes = ms.ToArray();
            }

            return imgBytes;
        }

        /// <summary>
        /// Decompresses an <see cref="Image"/> object, represented
        /// by the array of <see cref="byte"/>s.
        /// </summary>
        /// <param name="imgInput">The <see cref="byte"/> array representing
        /// the <see cref="Image"/>.</param>
        /// <returns>An <see cref="Image"/> represented by the inbound
        /// array of <see cref="byte"/>s.</returns>
        /// <exception cref="ArgumentException">the <see cref="byte"/> array
        /// does not represent a previously compressed
        /// <see cref="Image"/>.</exception>
        public static Image Decompress( byte[] imgInput )
        {
            if( imgInput == null )
            {
                throw new ArgumentNullException( "imgInput" );
            }

            Image retImage = null;
            try
            {
                using( Stream stream = new MemoryStream() )
                {
                    using( var byteStream = new MemoryStream( imgInput ) )
                    {
                        using( var z = new GZipStream( byteStream, CompressionMode.Decompress ) )
                        {
                            z.CopyTo( stream );
                        }
                    }

                    retImage = Image.FromStream( stream );
                }
            }
            catch( Exception e )
            {
                string err = "Input bytes do not represent compressed image.";
                throw new ArgumentException( err, e );
            }

            return new Bitmap( retImage );
        }
    }
}
