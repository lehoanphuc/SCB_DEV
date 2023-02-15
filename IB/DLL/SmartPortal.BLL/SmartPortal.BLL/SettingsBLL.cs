using System;
using System.Collections.Generic;

using System.Text;
using System.Data;
using System.Data.SqlClient;
using SmartPortal.DAL;
using SmartPortal.Model;
using SmartPortal.ExceptionCollection;
using System.Security.Permissions;

namespace SmartPortal.BLL
{
    public class SettingsBLL
    {
        /// <summary>
        /// Load Portal Setting
        /// </summary>
        /// <returns></returns>
        public SettingsModel LoadPortalSettings()
        {
            try
            {
                IDataReader iRead;
                SettingsModel SM = new SettingsModel();               

                iRead = DataAccess.GetFromReader("Settings_LoadPortalSettings");

                while (iRead.Read())
                {
                    SM.SmtpServer = iRead["SMTPServer"].ToString();
                    SM.SmtpPort = int.Parse(iRead["SMTPPort"].ToString());
                    SM.SmtpUserName = iRead["SMTPUserName"].ToString();
                    SM.SmtpPassword = iRead["SMTPPassword"].ToString();
                    SM.RoleAdminID = int.Parse(iRead["RoleAdminID"].ToString());
                    SM.UserNameDefault = iRead["UserNameDefault"].ToString();
                    SM.ServiceIDDefault = iRead["ServiceIDDefault"].ToString();
                    SM.LogPath = iRead["LogPath"].ToString();
                    SM.PortalDefaultID = int.Parse(iRead["PortalDefaultID"].ToString());
                    SM.PageDefaultID = iRead["PageDefaultID"].ToString();
                    SM.DefaultLang = iRead["DefaultLang"].ToString();
                    SM.SMTPSSL =bool.Parse(iRead["SMTPSSL"].ToString());
                    SM.SMTPCCEmail = iRead["SMTPCCEmail"].ToString();
                    SM.SMTPBCCEmail = iRead["SMTPBCCEmail"].ToString();
                    SM.SMTPEmailDelivey = iRead["EMAILDELIVERY"].ToString();
                }
                iRead.Close();
                return SM;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LogEmailOut(string from, string display, string to, string cc, string subject, string body, string attachment, string status, string errorcode, string errordesc, int piority = 0)
        {
            SqlParameter[] listParam = new SqlParameter[12];
            listParam[0] = CreateSQLParam("@IPCTRANSID", -1, SqlDbType.Int);
            listParam[1] = CreateSQLParam("@FROM", from, SqlDbType.VarChar);
            listParam[2] = CreateSQLParam("@DISPLAYNAME", display, SqlDbType.NVarChar);
            listParam[3] = CreateSQLParam("@TO", to, SqlDbType.VarChar);
            listParam[4] = CreateSQLParam("@CC", cc, SqlDbType.VarChar);
            listParam[5] = CreateSQLParam("@TITLE", subject, SqlDbType.NVarChar);
            listParam[6] = CreateSQLParam("@MSGCONTENT", body, SqlDbType.NVarChar);
            listParam[7] = CreateSQLParam("@ATTACHMENT", attachment, SqlDbType.NVarChar);
            listParam[8] = CreateSQLParam("@ERRORCODE", errorcode, SqlDbType.VarChar);
            listParam[9] = CreateSQLParam("@ERRORDESC", errordesc, SqlDbType.NVarChar);
            listParam[10] = CreateSQLParam("@STATUS", status, SqlDbType.VarChar);
            listParam[11] = CreateSQLParam("@PIORITY", piority, SqlDbType.Int);

            DataAccess.Execute("SEMS_EMAILOUT_INSERT", listParam);
        }

        private SqlParameter CreateSQLParam(string paramname, object value, SqlDbType SQlType)
        {
            SqlParameter param = new SqlParameter();
            param.ParameterName = paramname;
            param.Value = value;
            param.SqlDbType = SQlType;
            return param;
        }

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="smtpserver"></param>
        /// <param name="smtpport"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="roleAdmin"></param>
        /// <param name="usernamedefault"></param>
        /// <param name="portalDefault"></param>
        /// <param name="pagedefault"></param>
        /// <param name="langdefault"></param>
        /// <param name="usermodi"></param>
        /// <param name="datemodi"></param>
        /// <param name="logpath"></param>
        /// <returns></returns>

        public int Insert(string smtpserver,int smtpport,string username,string password,int roleAdmin,string usernamedefault,int portalDefault,int pagedefault,string langdefault,string usermodi,string datemodi,string logpath)
        {
            try
            {
                int strErr = 0;

                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@smtpserver";
                p1.Value = smtpserver;
                p1.SqlDbType = SqlDbType.VarChar;

                SqlParameter p2 = new SqlParameter();
                p2.ParameterName = "@smtpport";
                p2.Value = smtpport;
                p2.SqlDbType = SqlDbType.Int;

                SqlParameter p3 = new SqlParameter();
                p3.ParameterName = "@username";
                p3.Value = username;
                p3.SqlDbType = SqlDbType.VarChar;

                SqlParameter p4 = new SqlParameter();
                p4.ParameterName = "@password";
                p4.Value = password;
                p4.SqlDbType = SqlDbType.VarChar;

                SqlParameter p5 = new SqlParameter();
                p5.ParameterName = "@roleadministrator";
                p5.Value = roleAdmin;
                p5.SqlDbType = SqlDbType.Int;

                SqlParameter p6 = new SqlParameter();
                p6.ParameterName = "@usernamedefault";
                p6.Value = usernamedefault;
                p6.SqlDbType = SqlDbType.VarChar;

                SqlParameter p7 = new SqlParameter();
                p7.ParameterName = "@portaldefault";
                p7.Value = portalDefault;
                p7.SqlDbType = SqlDbType.VarChar;

                SqlParameter p8 = new SqlParameter();
                p8.ParameterName = "@pagedefault";
                p8.Value = pagedefault;
                p8.SqlDbType = SqlDbType.Int;

                SqlParameter p9 = new SqlParameter();
                p9.ParameterName = "@languagedefault";
                p9.Value = langdefault;
                p9.SqlDbType = SqlDbType.VarChar;

                SqlParameter p10 = new SqlParameter();
                p10.ParameterName = "@usermodified";
                p10.Value = usermodi;
                p10.SqlDbType = SqlDbType.VarChar;

                SqlParameter p11 = new SqlParameter();
                p11.ParameterName = "@datemodified";
                p11.Value = datemodi;
                p11.SqlDbType = SqlDbType.VarChar;

                SqlParameter p12 = new SqlParameter();
                p12.ParameterName = "@logpath";
                p12.Value = logpath;
                p12.SqlDbType = SqlDbType.VarChar;

                return DataAccess.Execute("Settings_Insert", p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12);
                if (strErr == 0)
                {
                    throw new BusinessExeption("Unable Insert Setting");
                }
                else
                {
                    return strErr;
                }
            }
            catch (BusinessExeption bex)
            {
                throw bex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
