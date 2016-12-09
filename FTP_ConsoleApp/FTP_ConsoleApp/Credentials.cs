using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using Encryption;

namespace FTP_ConsoleApp
{
    class Credentials
    {
        //private static string FTPServerIP = string.Empty;
        //private static string username = string.Empty;
        //private static string password = string.Empty;

        public static string FTPServerIP { get; set; }
        public static string username { get; set; }
        public static string password { get; set; }
        public static string FTPServerFolder { get; set; }        
        //private static string EncryptionPass = "AMIMG";

        public static void SetCredentials()
        {
            //string file_name = Directory.GetCurrentDirectory()+ "/Server.txt";
            //string file_name = "Server.txt";
            string file_name = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Server.txt";
            // string file_name =  "/Server.txt";
            StreamReader sw = new StreamReader(file_name, true);
            string ServerCredential = sw.ReadToEnd();
            //ServerCredential = StringCipher.Decrypt(ServerCredential, EncryptionPass);
            string[] server = ServerCredential.Split('|');
           
            FTPServerIP = "ftp://" + server[0];
            username = server[1];
            password = server[2];
            FTPServerFolder = server[3].Trim();
        }        
    }
}
