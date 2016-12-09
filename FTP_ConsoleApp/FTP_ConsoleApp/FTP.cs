using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace FTP_ConsoleApp
{
    class FTP
    {
        public static bool UploadFileToFTP(string source, string FTPUrl, string uname, string pword, string FTPFolder)
        {
            bool IsSuccess;
            try
            {
                string filename = Path.GetFileName(source);
                string ftpfullpath = FTPUrl + FTPFolder + filename;
                //string ftpfullpath = FTPUrl + "/DAILY.NAV/" + filename;
                FtpWebRequest ftp = (FtpWebRequest)FtpWebRequest.Create(ftpfullpath);
                ftp.Method = WebRequestMethods.Ftp.UploadFile;

                ftp.Proxy = new WebProxy();
                ftp.Credentials = new NetworkCredential(uname, pword);

                //StreamReader sourceStream = new StreamReader(source);
                //byte[] fileContents = Encoding.UTF8.GetBytes(sourceStream.ReadToEnd());                
                ////byte[] fileContents = File.ReadAllBytes(source);
                //sourceStream.Close();
                //ftp.ContentLength = fileContents.Length;

                ftp.KeepAlive = true;
                ftp.UseBinary = false;
                ftp.UsePassive = true;
                //ftp.EnableSsl = false;

                using (FileStream fs = File.OpenRead(source))
                {
                    byte[] buffer = new byte[fs.Length];
                    fs.Read(buffer, 0, buffer.Length);
                    //fs.Close();
                    //fs.Dispose();
                    using (Stream ftpstream = ftp.GetRequestStream())
                    {
                        ftpstream.Write(buffer, 0, buffer.Length);
                        string text = File.ReadAllText(source);
                        Console.WriteLine(text);
                        //ftpstream.Close();
                        //ftpstream.Dispose();
                    }
                }
                IsSuccess = true;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Log.WriteLog(DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + " > " + ex.Message);
                Console.WriteLine(ex.Message);
                IsSuccess = false;
            }
            return IsSuccess;
        }
    }
}
