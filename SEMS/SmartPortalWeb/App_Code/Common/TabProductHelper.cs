using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for TabProductHelper
/// </summary>
public class TabProductHelper
{
    private static int tabMobileVisibility;
    private static int tabWalletVisibility;
    private static int tabAMVisibility;

    private static string tabName = string.Empty;

    public static int TabMobileVisibility
    {
        get { return tabMobileVisibility; }
    }
    public static int TabWalletVisibility
    {
        get { return tabWalletVisibility; }
    }
    public static int TabAMVisibility
    {
        get { return tabAMVisibility; }
    }
    public static void LoadConfig()
    {
        tabMobileVisibility = int.Parse(ConfigurationManager.AppSettings["tabMobileVisibility"].ToString());
        tabWalletVisibility = int.Parse(ConfigurationManager.AppSettings["tabWalletVisibility"].ToString());
        tabAMVisibility = int.Parse(ConfigurationManager.AppSettings["tabAMVisibility"].ToString());
    }
}