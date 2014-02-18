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
                DicomInfo.logCreated = false;
                foreach (String s in files)
                {
                    DicomInfo.readFile = s;
                    task.processDicom();
                }
            }
            catch (Exception e) { Console.WriteLine(e); }
        }
    }
}
