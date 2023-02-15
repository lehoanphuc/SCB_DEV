using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace eMailService
{
    public class ProcessLog
    {
        public static void LogError(Exception ex, string location, string MethodName)
        {
            System.IO.FileStream fs;
            System.IO.StreamWriter sw;
            string strErrorLogFilePath = "";

            try
            {
                strErrorLogFilePath = ConfigurationManager.AppSettings.Get("LogFilePath");
            }
            catch
            {
                strErrorLogFilePath = @"C:\IPCErrorLog.txt";
            }
            if (strErrorLogFilePath == null)
                strErrorLogFilePath = @"C:\IPCErrorLog.txt";

            try
            {
                if (System.IO.File.Exists(strErrorLogFilePath) == false)
                {
                    fs = new System.IO.FileStream(strErrorLogFilePath, System.IO.FileMode.CreateNew);
                }
                else
                {
                    fs = new System.IO.FileStream(strErrorLogFilePath, System.IO.FileMode.Append);
                }
                sw = new System.IO.StreamWriter(fs);
                string strLine = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff: ") +
                    "From: " + location.Substring(0, location.IndexOf(',')) + " |Function: " + MethodName + "|Error Description: " + ex.Message;
                if (ex.InnerException != null && ex.InnerException.Message != "")
                    strLine = strLine + "|Inner Exception: " + ex.InnerException.Message;

                sw.WriteLine(strLine);
                sw.Flush();
                sw.Close();
                fs.Close();
            }
            catch (Exception exnew)
            {
                //throw new Exception(exnew.Message);
            }
        }

        public static void LogInformation(string Information)
        {
            System.IO.FileStream fs;
            System.IO.StreamWriter sw;
            string strErrorLogFilePath = "";

            try
            {
                strErrorLogFilePath = ConfigurationManager.AppSettings.Get("LogFilePath");
            }
            catch
            {
                strErrorLogFilePath = @"C:\eMailErrorLog.txt";
            }
            if (strErrorLogFilePath == null)
                strErrorLogFilePath = @"C:\eMailErrorLog.txt";

            try
            {
                if (System.IO.File.Exists(strErrorLogFilePath) == false)
                {
                    fs = new System.IO.FileStream(strErrorLogFilePath, System.IO.FileMode.CreateNew);
                }
                else
                {
                    fs = new System.IO.FileStream(strErrorLogFilePath, System.IO.FileMode.Append);
                }
                sw = new System.IO.StreamWriter(fs);
                sw.WriteLine("==========================================================================================");
                sw.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff: ") + Information);
                sw.WriteLine("==========================================================================================");
                sw.Flush();
                sw.Close();
                fs.Close();
            }
            catch
            {
                //throw new Exception(exnew.Message);
            }
        }
    }
}
