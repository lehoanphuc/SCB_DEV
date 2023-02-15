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
    private static int tabWalletVisibility;
    private static int tabAMVisibility;

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
    public static int TabWalletVisibility
    {
        get { return tabWalletVisibility; }
    }
    public static int TabAMVisibility
    {
        get { return tabAMVisibility; }
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
        tabWalletVisibility = int.Parse(ConfigurationManager.AppSettings["tabWalletVisibility"].ToString());
        tabAMVisibility = int.Parse(ConfigurationManager.AppSettings["tabAMVisibility"].ToString());
        int total = tabIBVisibility + tabPhoneVisibility + tabSMSVisibility + tabMobileVisibility + tabWalletVisibility+ tabAMVisibility;

        tabName = string.Empty;

        if (tabMobileVisibility == 1 )
            tabName += "'" + Resources.labels.mobilebanking + "'";
            
        if (tabWalletVisibility == 1 && total == 1)
            tabName += "'" + Resources.labels.walletbanking + "'";
        else if (tabWalletVisibility == 1 && total > 1)
            tabName += ",'" + Resources.labels.walletbanking + "'";
         
        if (tabAMVisibility == 1 && total == 1)
            tabName += "'" + Resources.labels.agentmerchant + "'";
        else if (tabAMVisibility == 1 && total > 1)
            tabName += ",'" + Resources.labels.agentmerchant + "'";

        if (tabSMSVisibility == 1 && total == 1)
            tabName += "'" + Resources.labels.smsbanking + "'";
        else if (tabSMSVisibility == 1 && total > 1)
            tabName += ",'" + Resources.labels.smsbanking + "'";

        if (tabIBVisibility == 1 && total == 1)
            tabName += "'" + Resources.labels.internetbanking + "'";
        else if (tabIBVisibility == 1 && total > 1)
            tabName += ",'" + Resources.labels.internetbanking + "'";

        if (tabPhoneVisibility == 1 && total == 1)
            tabName += "'" + Resources.labels.phonebanking + "'";
        else if (tabPhoneVisibility == 1 && total > 1)
            tabName += ",'" + Resources.labels.phonebanking + "'";


        tabName = ReplaceFistChar(tabName);


        //if (tabIBVisibility == 1)
        //    tabName += "'" + Resources.labels.internetbanking + "'";

        //if (tabMobileVisibility == 1 && total == 1)
        //    tabName += "'" + Resources.labels.mobilebanking + "'";
        //else if (tabMobileVisibility == 1 && total > 1)
        //    tabName += ",'" + Resources.labels.mobilebanking + "'";

        //if (tabPhoneVisibility == 1 && total == 1)
        //    tabName += "'" + Resources.labels.phonebanking + "'";
        //else if (tabPhoneVisibility == 1 && total > 1)
        //    tabName += ",'" + Resources.labels.phonebanking + "'";

        //if (tabWalletVisibility == 1 && total == 1)
        //    tabName += "'" + Resources.labels.walletbanking + "'";
        //else if (tabWalletVisibility == 1 && total > 1)
        //    tabName += ",'" + Resources.labels.walletbanking + "'";
        //if (tabSMSVisibility == 1 && total == 1)
        //    tabName += "'" + Resources.labels.smsbanking + "'";
        //else if (tabSMSVisibility == 1 && total > 1)
        //    tabName += ",'" + Resources.labels.smsbanking + "'";
        //tabName = ReplaceFistChar(tabName);

    }
    public static string ReplaceFistChar(string str)
    {
        if (str == null)
            return null;

        if (str.Length > 1)
            if (str[0].ToString() == ",")
            {
                return str.Substring(1);
            }
        return str;
    }
}