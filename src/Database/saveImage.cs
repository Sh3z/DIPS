﻿using Dicom.Imaging;
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
    public class saveImage
    {
        public byte[] blob()
        {
            byte[] blob = null;
            try
            {
                DicomImage img = new DicomImage(staticVariables.readFile);
                Image im = img.RenderImage();
                MemoryStream ms = new MemoryStream();
                im.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                blob = ms.ToArray();
                ms.Close();
            }
            catch (Exception e) { }
            return blob;
        }
    }
}
