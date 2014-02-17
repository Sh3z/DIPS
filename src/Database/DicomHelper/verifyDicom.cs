using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Database
{
    public class verifyDicom
    {
        //Quick verification
        public Boolean verify()
        {
            //consist of 128 bytes of file header
            //followed by 4 byte DICOM prefix

            FileStream fs = File.OpenRead(DicomInfo.readFile);
            fs.Seek(128, SeekOrigin.Begin);
            if (fs.ReadByte() == (byte)'D' && fs.ReadByte() == (byte)'I' &&
                fs.ReadByte() == (byte)'C' && fs.ReadByte() == (byte)'M')
            {
                fs.Close();
                return true;
            }
            else
            {
                fs.Close();
                return false;
            }
        }
    }
}
