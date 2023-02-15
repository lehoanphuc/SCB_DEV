using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for TabCustomerInfoHelper
/// </summary>
public class TabCustomerInfoHelper
{
    private static int tabIBVisibility;
    private static int tabPhoneVisibility;
    private static int tabSMSVisibility;
    private static int tabMobileVisibility;
    private static string tabName = string.Empty;

	public TabCustomerInfoHelper()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static int TabIBVisibility
    {
        get { return tabIBVisibility; }
    }
    public static int TabPhoneVisibility
    {
        get { return tabPhoneVisibility; }
    }

    public static int TabSMSVisibility
    {
        get { return tabSMSVisibility; }
    }

    public static int TabMobileVisibility
    {
        get { return tabMobileVisibility; }
    }

    public static string TabName
    {
        get { return tabName; }
    }

    public static void LoadConfig()
    {
        tabIBVisibility = int.Parse(ConfigurationManager.AppSettings["tabIBVisibility"].ToString());
        tabPhoneVisibility = int.Parse(ConfigurationManager.AppSettings["tabPhoneVisibility"].ToString());
        tabSMSVisibility = int.Parse(ConfigurationManager.AppSettings["tabSMSVisibility"].ToString());
        tabMobileVisibility = int.Parse(ConfigurationManager.AppSettings["tabMobileVisibility"].ToString());
        int total = tabIBVisibility + tabPhoneVisibility + tabSMSVisibility + tabMobileVisibility;

        tabName = string.Empty;
        if (tabIBVisibility == 1)
            tabName += "'" + Resources.labels.internetbanking+"'";
        if (tabSMSVisibility == 1 && total == 1)
            tabName += "'" + Resources.labels.smsbanking + "'";
        else if (tabSMSVisibility == 1 && total > 1)
            tabName += ",'"+ Resources.labels.smsbanking +"'";

        if (tabMobileVisibility == 1 && total == 1)
            tabName += "'" + Resources.labels.mobilebanking +"'";
        else if (tabMobileVisibility == 1 && total > 1)
            tabName += ",'"+Resources.labels.mobilebanking + "'";

        if (tabPhoneVisibility == 1 && total == 1)
            tabName += "'"+ Resources.labels.phonebanking+"'";
        else if (tabPhoneVisibility == 1 && total > 1)
            tabName += ",'"+Resources.labels.phonebanking+"'";
    }
}