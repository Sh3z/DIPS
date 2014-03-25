using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Database
{
    public class Codec
    {
        public void register()
        {
            Dicom.Codec.DcmRleCodec.Register();
            Dicom.Codec.Jpeg.DcmJpegCodec.Register();
            Dicom.Codec.Jpeg2000.DcmJpeg2000Codec.Register();
            Dicom.Codec.JpegLs.DcmJpegLsCodec.Register();
        }
    }
}
