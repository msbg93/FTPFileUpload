using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;

namespace FTP_ConsoleApp
{
    class Log
    {
        static string logFile = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\ErrorLog.txt";

        public static void WriteLog(string msg)
        {
            if (!File.Exists(logFile))
            {
                File.Create(logFile).Dispose();
                File.AppendAllText(logFile, msg + Environment.NewLine + Environment.NewLine);
            }
            else
            {
                File.AppendAllText(logFile, msg + Environment.NewLine + Environment.NewLine);
            }
        }
    }
}
