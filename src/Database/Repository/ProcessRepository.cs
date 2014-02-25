using Database.Connection;
using Database.Objects;
using DIPS.Database;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Repository
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
    }
}
