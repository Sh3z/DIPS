using Database.Connection;
using Database.Objects;
using DIPS.Database;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Database
{
    public class ProcessRepository
    {
        public void processFolder(String folderPath)
        {
            try
            {
                string[] files = Directory.GetFiles(folderPath, "*", SearchOption.AllDirectories);
                Console.WriteLine(files.Length + " Files to Process");

                Process task = new Process();
                Log.Created = false;
                Log.NeedUpdate = false;

                if (Log.CodecRegistration == false)
                {
                    registerCodec();
                    Log.CodecRegistration = true;
                }

                foreach (String filePath in files)
                {
                    DicomInfo dicom = new DicomInfo();
                    dicom.readFile = filePath;
                    task.processDicom(dicom,filePath);
                    Log.Series = dicom.seriesID;
                }

                if (Log.NeedUpdate == true)
                {
                    DAOLog log = new DAOLog();
                    log.update(Log.Series);
                }
            }
            catch (Exception e) { Console.WriteLine(e); }
        }

        private void registerCodec()
        {
            Dicom.Codec.DcmRleCodec.Register();
            Dicom.Codec.Jpeg.DcmJpegCodec.Register();
            Dicom.Codec.Jpeg2000.DcmJpeg2000Codec.Register();
            Dicom.Codec.JpegLs.DcmJpegLsCodec.Register();
        }
    }
}
