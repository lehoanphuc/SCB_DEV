using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using log4net.Repository.Hierarchy;
using log4net;
using log4net.Layout;
using log4net.Appender;
using log4net.Core;

namespace Utility
{
    public class ProcessLog
    {
        private static ILog log = CreateFileAppender(Common.FILELOGTYPE.LOGFILEPATH, true);
        private static ILog MsgInOutLog = CreateFileAppender(Common.FILELOGTYPE.LOGMSGINOUT);
        private static ILog XmlToFieldLog = CreateFileAppender(Common.FILELOGTYPE.LOGXMLTOFIELD);
        private static ILog LogMessage = CreateFileAppender(Common.FILELOGTYPE.LOGMSGSYSTEM);
        private static ILog LogDoStore = CreateFileAppender(Common.FILELOGTYPE.LOGDOSTOREINFO);

        // Create a new file appender
        private static ILog CreateFileAppender(string appenderName, bool AddRoot = false, int maxSizeRollBackups = 25, string maxFileSize = "50MB", string datePattern = "-yyyy-MM-dd")
        {
            try
            {
                ILog log = LogManager.GetLogger(appenderName);
                Logger l = (Logger)log.Logger;
                l.Additivity = false;
                l.Level = Level.All;


                PatternLayout patternLayout = new PatternLayout();
                patternLayout.ConversionPattern = "%date %level - %message%newline%-----------------------------------------------------------------------------------%newline";
                patternLayout.ActivateOptions();
                string logPath = string.Empty;
                try
                {
                    logPath = ConfigurationManager.AppSettings.Get(appenderName);
                    if (logPath == null)
                        throw new Exception("Missing config");
                }
                catch
                {
                    //logPath = $"C://Logs//{appenderName}//{appenderName}";
                }

                RollingFileAppender roller = new RollingFileAppender();
                roller.Name = appenderName;
                roller.AppendToFile = true;
                roller.File = logPath;
                roller.Layout = patternLayout;
                roller.MaxSizeRollBackups = maxSizeRollBackups;
                roller.MaximumFileSize = maxFileSize;
                roller.RollingStyle = RollingFileAppender.RollingMode.Size;
                roller.StaticLogFileName = false;
                roller.DatePattern = datePattern;
                roller.LockingModel = new FileAppender.MinimalLock();
                roller.ActivateOptions();
                if (AddRoot)
                {
                    Hierarchy hierarchy = (Hierarchy)LogManager.GetRepository();
                    hierarchy.Root.AddAppender(roller);

                    MemoryAppender memory = new MemoryAppender();
                    memory.ActivateOptions();

                    hierarchy.Root.AddAppender(memory);

                    hierarchy.Root.Level = Level.All;
                    hierarchy.Configured = true;
                }

                l.AddAppender(roller);
                return log;
            }
            catch
            {
                return null;
            }
        }

        #region Static Function
        public static void LogError(Exception ex, string location, string MethodName)
        {
            try
            {
                string strLine = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff: ") +
                    "From: " + location.Substring(0, location.IndexOf(',')) + " |Function: " + MethodName + "|Error Description: " + ex.Message;
                if (ex.InnerException != null && ex.InnerException.Message != "")
                    strLine = strLine + "|Inner Exception: " + ex.InnerException.Message;
                try
                {
                    strLine += "|Line: " + new StackTrace(ex, true).GetFrame(0).GetFileLineNumber();
                }
                catch { }

                log.Error(strLine);

            }
            catch (Exception exnew)
            {
                //throw new Exception(exnew.Message);
            }
        }

        public static void LogError(Exception ex, string location, string MethodName, bool ShowMessage)
        {
            try
            {
                string strLine = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff: ") +
                    "From: " + location.Substring(0, location.IndexOf(',')) + " |Function: " + MethodName + "|Error Description: " + ex.Message;
                if (ex.InnerException != null && ex.InnerException.Message != "")
                    strLine = strLine + "|Inner Exception: " + ex.InnerException.Message;
                try
                {
                    strLine += "|Line: " + new StackTrace(ex, true).GetFrame(0).GetFileLineNumber();
                }
                catch { }

                log.Error(strLine);

            }
            catch
            {
                //throw new Exception("Error Write Error Log");
            }
            if (ShowMessage == true)
                MessageBox.Show(ex.Message, "Intelligent Payment Center", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void LogInformation(string Information)
        {
            try
            {
                log.Info(Information);
            }
            catch
            {
                //throw new Exception(exnew.Message);
            }
        }

        public static void LogInformation(string Information, string logType = "")
        {
            try
            {
                switch (logType)
                {
                    case Common.FILELOGTYPE.LOGMSGSYSTEM:
                        LogMessage.Info(Information);
                        break;
                    case Common.FILELOGTYPE.LOGMSGINOUT:
                        MsgInOutLog.Info(Information);
                        break;
                    case Common.FILELOGTYPE.LOGXMLTOFIELD:
                        XmlToFieldLog.Info(Information);
                        break;
                    case Common.FILELOGTYPE.LOGDOSTOREINFO:
                        LogDoStore.Info(Information);
                        break;
                    default:
                        log.Error(Information);
                        break;
                }
            }
            catch
            {
                //throw new Exception(exnew.Message);
            }
        }

        public static void LogDebug(Exception ex, string location, string MethodName)
        {
            try
            {
                if (ConfigurationManager.AppSettings.Get("Debug").ToUpper() != "TRUE") return;
            }
            catch
            {
                return;
            }
            try
            {
                string strLine = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff: ") +
                    "From: " + location.Substring(0, location.IndexOf(',')) + " |Function: " + MethodName + " |Error Description: " + ex.Message;

                if (ex.InnerException != null && ex.InnerException.Message != "")
                    strLine = strLine + " |Inner Exception: " + ex.InnerException.Message;

                log.Debug(strLine);

            }
            catch (Exception exnew)
            {
                //throw new Exception(exnew.Message);
            }
        }

        public static void LogDebug(string Information, string location, string MethodName, Exception ex)
        {

            try
            {
                if (ConfigurationManager.AppSettings.Get("Debug").ToUpper() != "TRUE") return;

            }
            catch
            {
                return;
            }

            try
            {

                string strLine = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff: ") +
                    "From: " + location.Substring(0, location.IndexOf(',')) + " |Function: " + MethodName + " |Information: " + Information +
                    " |Error Description: " + ex.Message;

                if (ex.InnerException != null && ex.InnerException.Message != "")
                    strLine = strLine + " |Inner Exception: " + ex.InnerException.Message;

                log.Debug(strLine);

            }
            catch (Exception exnew)
            {
                //throw new Exception(exnew.Message);
            }
        }

        public static void LogDebug(string Information, string location, string MethodName)
        {
            try
            {
                if (ConfigurationManager.AppSettings.Get("Debug").ToUpper() != "TRUE") return;

            }
            catch
            {
                return;
            }

            try
            {

                string strLine = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff: ") +
                    "From: " + location.Substring(0, location.IndexOf(',')) + " |Function: " + MethodName + " |Information: " + Information;

                log.Debug(strLine);

            }
            catch (Exception exnew)
            {
                //throw new Exception(exnew.Message);
            }
        }

        public static void LogATM(string Information)
        {
            try
            {
                log.Info(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff: ") + Information);

            }
            catch
            {
                //throw new Exception(exnew.Message);
            }
        }

        public static void LogATMDetail(string Information)
        {
            try
            {
                log.Info(Information);

            }
            catch
            {
                //throw new Exception(exnew.Message);
            }
        }

        public static void LogEvent(string Source, string Information)
        {
            try
            {
                EventLog.WriteEntry(Source, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff: ") + Information);
            }
            catch
            {
                //throw new Exception(exnew.Message);
            }
        }

        #endregion
    }
}