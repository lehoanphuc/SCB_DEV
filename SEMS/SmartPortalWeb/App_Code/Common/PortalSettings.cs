using System;
using System.Collections.Generic;

using System.Web;
using SmartPortal.BLL;
using SmartPortal.Model;
using SmartPortal.Common.Utilities;

/// <summary>
/// Summary description for PortalSettings
/// </summary>
namespace SmartPortal.Common
{
    public class PortalSettings
    {
        //bien chua caa cai dat portal
        public SettingsModel portalSetting;

        public PortalSettings()
        {
            portalSetting = new SettingsBLL().LoadPortalSettings();
        }
    }
}