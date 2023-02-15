using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for TabCustomerInfoHelper
/// </summary>
public class TabCustomerInfoHelperWalletOnly
{
    private static int tabSMSVisibility;
    private static int tabWalletVisibility;
    private static string tabName = string.Empty;

    public TabCustomerInfoHelperWalletOnly()
    {
        //
        // TODO: Add constructor logic here
        //
    }


    public static int TabSMSVisibility
    {
        get { return tabSMSVisibility; }
    }

    public static int TabWalletVisibility
    {
        get { return tabWalletVisibility; }
    }
    public static string TabName
    {
        get { return tabName; }
    }

    public static void LoadConfig()
    {
   
        tabSMSVisibility = int.Parse(ConfigurationManager.AppSettings["tabSMSVisibilityWallet"].ToString());
        tabWalletVisibility = int.Parse(ConfigurationManager.AppSettings["tabWalletVisibility"].ToString());
        int total =  tabSMSVisibility  + tabWalletVisibility;

        tabName = string.Empty;
        if (tabWalletVisibility == 1)
            tabName += "'" + Resources.labels.walletbanking + "'";
        if (tabSMSVisibility == 1 && total == 1)
            tabName += "'" + Resources.labels.smsbanking + "'";
        else if (tabSMSVisibility == 1 && total > 1)
            tabName += ",'" + Resources.labels.smsbanking + "'";
        tabName = ReplaceFistChar(tabName);

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