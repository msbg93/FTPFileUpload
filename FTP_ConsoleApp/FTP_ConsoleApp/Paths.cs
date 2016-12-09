using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;

namespace FTP_ConsoleApp
{
    class Paths
    {
        public static string sourcePath { get; set; }
        public static string destPath { get; set; }

        public static void SetPaths()
        {          
            //string file_name = Directory.GetCurrentDirectory()+ "/Paths.txt";
            string file_name = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)+"\\Paths.txt";
            //string file_name = "Paths.txt";
            using (StreamReader sw = new StreamReader(file_name, true))
            {
                string sourceDestPaths = sw.ReadToEnd();
                string[] paths = sourceDestPaths.Split('|');

                sourcePath = paths[0];
                destPath = paths[1];
            }
        }
    }
}
