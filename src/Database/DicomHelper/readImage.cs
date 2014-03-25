using Dicom.Imaging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Database
{
    public class readImage
    {
        public byte[] blob(string path)
        {
            DicomImage img = new DicomImage(path);
            Image im = img.RenderImage();
            return ImageToByteArray(im);
        }

        public byte[] ImageToByteArray(Image im)
        {
            byte[] blob = null;
            try
            {
                MemoryStream ms = new MemoryStream();
                im.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                blob = ms.ToArray();
                ms.Close();
            }
            catch (Exception e) 
            {
                Console.WriteLine(e);
                blob = null;
            }
            return blob;
        }
    }
}
