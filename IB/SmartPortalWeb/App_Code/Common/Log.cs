using System;
using System.Data;
using System.Configuration;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using System.IO;
using System.Data.SqlClient;
using SmartPortal.DAL;


namespace SmartPortal.Common
{
    /// <summary>
    /// Summary description for Log
    /// </summary>
    public class Log
    {
        //Ghi log cac hanh dong cua nguoi dung
        public static bool WriteLog(string IPCTRANCODE, string DATETIME, string USER, string TABLENAME, string CONDITION)
        {
            bool flag;
            try
            {
                string sql = "";
                sql += "INSERT INTO " + TABLENAME + "_HIS SELECT '" + DateTime.Parse(DATETIME).ToString("dd/MM/yyyy HH:mm:ss") + "','" + IPCTRANCODE + "','" + USER + "',A.*" + " FROM " + TABLENAME + " A WHERE " + CONDITION;

                //thuc thi chuoi sql
                SmartPortal.DAL.DataAccess.ExecuteSQL(sql);

                flag = true;
            }
            catch (Exception ex)
            {
                flag = false;
            }

            return flag;
        }


        public static void WriteLogFile(string errorcode, string cls, string fu, string message)
        {
            //VuTT 20181115
            string strErrorLogFilePath = new PortalSettings().portalSetting.LogPath;

            try
            {
                long size = 10 * 1024 * 1024;
                try
                {
                    size = long.Parse(ConfigurationManager.AppSettings.Get("LogFileSize")) * 1024 * 1024;
                }
                catch { }

                try
                {
                    if (!File.Exists(strErrorLogFilePath))
                    {
                        if (!System.IO.Directory.Exists(Path.GetDirectoryName(strErrorLogFilePath)))
                            System.IO.Directory.CreateDirectory(Path.GetDirectoryName(strErrorLogFilePath));

                        File.Create(strErrorLogFilePath).Dispose();
                    }
                    else if ((new FileInfo(strErrorLogFilePath)).Length > size)
                    {
                        string newPath = strErrorLogFilePath + "." + DateTime.Now.ToString("ddMMyyy-HHmmss-ffff");
                        File.Move(strErrorLogFilePath, newPath);
                        File.Create(strErrorLogFilePath).Dispose();
                    }
                }
                catch (Exception ex)
                { }

                string userName = "";
                try
                {
                    userName = HttpContext.Current.Session["userName"].ToString();
                }
                catch { }

                string strLine = errorcode + "  " + DateTime.Now.ToString() + "  " + userName + "  " + cls + "  " + fu + "  " + message;

                using (StreamWriter sr = new StreamWriter(strErrorLogFilePath, true))
                {
                    sr.WriteLine(strLine);
                    sr.Flush();
                    sr.Close();
                }
            }
            catch (Exception exnew)
            {
                //throw new Exception(exnew.Message);
            }
        }

        public static void GoToErrorPage(string errorcode, string returnURL)
        {
            //HttpContext.Current.Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL(System.Configuration.ConfigurationManager.AppSettings["errorpage"] + "&ec=" + errorcode + "&returnurl=" + SmartPortal.Common.Encrypt.EncryptData(returnURL)));
            HttpContext.Current.Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL(System.Configuration.ConfigurationManager.AppSettings["iberrorpage"] + "&ec=" + errorcode + "&returnurl=" + SmartPortal.Common.Encrypt.EncryptData(returnURL)));
        }
        public static void GoToIBErrorPage(string errorcode, string returnURL)
        {
            HttpContext.Current.Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL(System.Configuration.ConfigurationManager.AppSettings["iberrorpage"] + "&ec=" + errorcode + "&returnurl=" + SmartPortal.Common.Encrypt.EncryptData(returnURL)));
        }

        public static void RaiseError(string errorcode, string cls, string fu, string message, string returnURL)
        {
            try
            {
                WriteLogFile(errorcode, cls, fu, message);
            }
            catch { }
            //GoToErrorPage(errorcode,returnURL);
        }

        /// <summary>
        /// Ghi log vào database
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="func"></param>
        /// <param name="thread"></param>
        /// <param name="logger"></param>
        /// <param name="errCode"></param>
        /// <param name="exception"></param>
        public static void WriteLogDatabase(string obj, string method, string thread, string logger, string ip, string table, string field, string oldValue, string newValue, string status = "A", string approver = "")
        {
            SqlParameter p1 = new SqlParameter();
            p1.ParameterName = "@object";
            p1.Value = obj;
            p1.SqlDbType = SqlDbType.NVarChar;

            SqlParameter p2 = new SqlParameter();
            p2.ParameterName = "@method";
            p2.Value = method;
            p2.SqlDbType = SqlDbType.NVarChar;

            SqlParameter p3 = new SqlParameter();
            p3.ParameterName = "@thread";
            p3.Value = thread;
            p3.SqlDbType = SqlDbType.VarChar;

            SqlParameter p4 = new SqlParameter();
            p4.ParameterName = "@logger";
            p4.Value = logger;
            p4.SqlDbType = SqlDbType.VarChar;

            SqlParameter p5 = new SqlParameter();
            p5.ParameterName = "@ip";
            p5.Value = ip;
            p5.SqlDbType = SqlDbType.VarChar;

            SqlParameter p6 = new SqlParameter();
            p6.ParameterName = "@table";
            p6.Value = table;
            p6.SqlDbType = SqlDbType.VarChar;

            SqlParameter p7 = new SqlParameter();
            p7.ParameterName = "@field";
            p7.Value = field;
            p7.SqlDbType = SqlDbType.VarChar;

            SqlParameter p8 = new SqlParameter();
            p8.ParameterName = "@oldvalue";
            p8.Value = oldValue;
            p8.SqlDbType = SqlDbType.NVarChar;

            SqlParameter p9 = new SqlParameter();
            p9.ParameterName = "@newvalue";
            p9.Value = newValue;
            p9.SqlDbType = SqlDbType.NVarChar;

            SqlParameter p10 = new SqlParameter();
            p10.ParameterName = "@status";
            p10.Value = status;
            p10.SqlDbType = SqlDbType.NVarChar;

            SqlParameter p11 = new SqlParameter();
            p11.ParameterName = "@approver";
            p11.Value = approver;
            p11.SqlDbType = SqlDbType.NVarChar;

            DataAccess.Execute("Log_Insert_New", p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
        }
        public static void UpdateLogDatabase(string obj, string field, string approver = "",string type ="")
        {
            SqlParameter p1 = new SqlParameter();
            p1.ParameterName = "@object";
            p1.Value = obj;
            p1.SqlDbType = SqlDbType.NVarChar;

            SqlParameter p2 = new SqlParameter();
            p2.ParameterName = "@field";
            p2.Value = field;
            p2.SqlDbType = SqlDbType.NVarChar;

            SqlParameter p3 = new SqlParameter();
            p3.ParameterName = "@approver";
            p3.Value = approver;
            p3.SqlDbType = SqlDbType.NVarChar;

            SqlParameter p4 = new SqlParameter();
            p4.ParameterName = "@type";
            p4.Value = type;
            p4.SqlDbType = SqlDbType.NVarChar;

            DataAccess.Execute("Log_Update_New", p1,p2,p3, p4);
        }
    }
}