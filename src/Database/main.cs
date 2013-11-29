using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace DIPS.Database
{
    class main
    {
        static void Main(string[] args)
        {
            readDicom read = new readDicom();

            var watch = Stopwatch.StartNew();
            String path = @"C:\Users\Yeh\Desktop\Folders\MRI Scans\";
            List<String> allFiles = new List<String>();
            allFiles = Directory.GetFiles(path, "*", SearchOption.AllDirectories).ToList();
            foreach (String s in allFiles)
            {
                staticVariables.readFile = s;
                read.read();
            }
            watch.Stop();
            Console.WriteLine("");
            Console.WriteLine(allFiles.Count+" Files in "+(watch.ElapsedMilliseconds/1000)+" seconds");
            
           printTables print = new printTables();
           print.print();
        }
    }
}
