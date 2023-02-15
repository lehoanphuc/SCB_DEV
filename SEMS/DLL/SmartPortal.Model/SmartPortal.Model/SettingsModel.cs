using System;
using System.Collections.Generic;

using System.Text;

namespace SmartPortal.Model
{
    public class SettingsModel
    {
        private string _serviveIDDefault;

        public string ServiceIDDefault
        {
            get { return _serviveIDDefault; }
            set { _serviveIDDefault = value; }
        }
        private string _smtpServer;

        public string SmtpServer
        {
            get { return _smtpServer; }
            set { _smtpServer = value; }
        }
        private int _smtpPort;

        public int SmtpPort
        {
            get { return _smtpPort; }
            set { _smtpPort = value; }
        }
        private string _smtpUserName;

        public string SmtpUserName
        {
            get { return _smtpUserName; }
            set { _smtpUserName = value; }
        }
        private string _smtpPassword;

        public string SmtpPassword
        {
            get { return _smtpPassword; }
            set { _smtpPassword = value; }
        }
        private string _logPath;

        public string LogPath
        {
            get { return _logPath; }
            set { _logPath = value; }
        }
        private int _roleAdminID;

        public int RoleAdminID
        {
            get { return _roleAdminID; }
            set { _roleAdminID = value; }
        }

        private string _userNameDefault;

        public string UserNameDefault
        {
            get { return _userNameDefault; }
            set { _userNameDefault = value; }
        }
       
        private int _portalDefaultID;

        public int PortalDefaultID
        {
            get { return _portalDefaultID; }
            set { _portalDefaultID = value; }
        }
        private string _pageDefaultID;

        public string PageDefaultID
        {
            get { return _pageDefaultID; }
            set { _pageDefaultID = value; }
        }

        private string _DefaultLang;

        public string DefaultLang
        {
            get { return _DefaultLang; }
            set { _DefaultLang = value; }
        }

        private bool _SMTPSSL;

        public bool SMTPSSL
        {
            get { return _SMTPSSL; }
            set { _SMTPSSL = value; }
        }

        private string _SMTPCCEmail;

        public string SMTPCCEmail
        {
            get { return _SMTPCCEmail; }
            set { _SMTPCCEmail = value; }
        }

        private string _SMTPBCCEmail;

        public string SMTPBCCEmail
        {
            get { return _SMTPBCCEmail; }
            set { _SMTPBCCEmail = value; }
        }
        private string _SMTPEmailDelivery;

        public string SMTPEmailDelivey
        {
            get { return _SMTPEmailDelivery; }
            set { _SMTPEmailDelivery = value; }
        }

    }
}
