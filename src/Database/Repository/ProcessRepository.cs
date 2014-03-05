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
        public void processDicom(String filePath)
        {
            try
            {

                Process task = new Process();

                if (Log.CodecRegistration == false)
                {

                    Log.CodecRegistration = true;
                }

                DicomInfo dicom = new DicomInfo();
                dicom.readFile = filePath;
                task.processDicom(dicom, filePath);
                Log.Series = dicom.seriesID;

            }
            catch (Exception e) { Console.WriteLine(e); }
        }
    }
}
