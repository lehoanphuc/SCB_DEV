using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Configuration;

namespace SMSGWProcess
{
    public class Logger
    {
        StreamWriter sr;

        public Logger()
        {

        }

        public void Open()
        {
            sr = new StreamWriter("C:\\SMSSpool.txt", true);
        }

        public void Close()
        {
            sr.Close();
        }

        //public void Log(string str)
        //{
        //    System.IO.FileStream fs;
        //    System.IO.StreamWriter sw;
        //    string strErrorLogFilePath = "C:\\SMSSpool.txt";
        //    try
        //    {
        //        if (System.IO.File.Exists(strErrorLogFilePath) == false)
        //        {
        //            fs = new System.IO.FileStream(strErrorLogFilePath, System.IO.FileMode.CreateNew);
        //        }
        //        else
        //        {
        //            fs = new System.IO.FileStream(strErrorLogFilePath, System.IO.FileMode.Append);
        //        }
        //        sw = new System.IO.StreamWriter(fs);
        //        sw.WriteLine(str);
        //        sw.Flush();
        //        sw.Close();
        //        fs.Close();
        //    }
        //    catch (Exception exnew)
        //    {
        //        //throw new Exception(exnew.Message);
        //    }
        //    //Open();
        //    //sr.WriteLine(str);
        //    //Close();
        //}
        public  void Log(string Information)
        {
            System.IO.FileStream fs;
            System.IO.StreamWriter sw;
            string strErrorLogFilePath = "";

            try
            {
                strErrorLogFilePath = ConfigurationManager.AppSettings.Get("LogFilePathGW");
            }
            catch
            {
                strErrorLogFilePath = @"C:\SMSSpoolGW.txt";
            }
            if (strErrorLogFilePath == null)
                strErrorLogFilePath = @"C:\SMSSpoolGW.txt";

            try
            {
                if (System.IO.File.Exists(strErrorLogFilePath) == false)
                {
                    fs = new System.IO.FileStream(strErrorLogFilePath, System.IO.FileMode.CreateNew);
                }
                else
                {
                    //backup
                    try
                    {
                        fs = new System.IO.FileStream(strErrorLogFilePath, System.IO.FileMode.Append);

                        long size = 10 * 1024 * 1024;
                        try
                        {
                            size = long.Parse(ConfigurationManager.AppSettings.Get("LogFileSize")) * 1024 * 1024;
                        }
                        catch
                        { }

                        if (fs.Length > size)
                        {
                            string newPath = strErrorLogFilePath + "." + DateTime.Now.ToString("ddMMyyy-HHmmss-ffff");
                            fs.Close();
                            System.IO.File.Move(strErrorLogFilePath, newPath);
                            fs = new System.IO.FileStream(strErrorLogFilePath, System.IO.FileMode.CreateNew);
                        }
                    }
                    catch (Exception ex)
                    {
                        fs = new System.IO.FileStream(strErrorLogFilePath, System.IO.FileMode.Append);
                    }
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
