using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Collections;
using System.Reflection;

namespace FTP_ConsoleApp
{

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                string msg = Convert.ToString(DateTime.Now.ToString("yyyy-MM-dd H:mm:ss"));
                Paths.SetPaths();   //File Source & Desitnation Path...
                string[] sourceFilePath = Directory.GetFiles(Paths.sourcePath);
                ArrayList fileName = new ArrayList();
                ArrayList ArchivePath = new ArrayList();

                for (int i = 0; i < sourceFilePath.Length; i++)
                {
                    fileName.Add(Path.GetFileName(sourceFilePath[i].ToString()));
                    ArchivePath.Add(Paths.destPath + fileName[i].ToString());
                }

                //Credential Setting...               
                Credentials.SetCredentials();
                string FTP_Url = Credentials.FTPServerIP;
                string FTP_UserName = Credentials.username;
                string FTP_Password = Credentials.password;
                string FTP_FolderPath = Credentials.FTPServerFolder;
                bool IsSuccess = false;

                for (int i = 0; i < sourceFilePath.Length; i++)
                {
                    IsSuccess = FTP.UploadFileToFTP(sourceFilePath[i].ToString(), FTP_Url, FTP_UserName, FTP_Password, FTP_FolderPath);
                    if (IsSuccess)
                    {
                        msg += " > " + Path.GetFileName(sourceFilePath[i].ToString()) + " Uploaded...";
                        Log.WriteLog(msg);
                        Console.WriteLine(msg);
                        for (int j = i; j < i + 1; j++)
                        {
                            if (!File.Exists(ArchivePath[j].ToString()))
                            {
                                File.Move(sourceFilePath[j].ToString(), ArchivePath[j].ToString());
                            }
                            else
                            {
                                File.Delete(ArchivePath[j].ToString());
                                File.Move(sourceFilePath[j].ToString(), ArchivePath[j].ToString());
                            }
                        }
                    }
                    else
                    {
                        msg += " > " + Path.GetFileName(sourceFilePath[i].ToString()) + " not Uploaded...";
                        Log.WriteLog(msg);
                        Console.WriteLine(msg);
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                    }
                }

                if (sourceFilePath.Length != 0)
                {
                    msg = string.Empty;
                    msg += "> Upload process completed...";
                    Console.WriteLine(msg);
                }
                else
                {
                    msg += " > No file available to upload (Folder Empty)...";
                    Log.WriteLog(msg);
                    Console.WriteLine(msg);
                }

            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Log.WriteLog(DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + " > " + ex.Message);
                Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + " > " + ex.Message);
                Console.ForegroundColor = ConsoleColor.DarkGreen;
            }
            finally
            {
                System.Threading.Thread.Sleep(5000);
                //Console.ReadLine();
            }
        }
    }
}
