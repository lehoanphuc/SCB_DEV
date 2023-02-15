using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using SmartPortal.IB;
using SmartPortal.Common.Utilities;
using SmartPortal.ExceptionCollection;

public partial class Widgets_IBTransactionHistory1_print : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Culture = "en-US";
                UICulture = "en-US";
                if (Session["tmpl"] != null || Session["acctpl"] != null)
                {
                    LoadDetailAccount();
                    ltrStatement.Text = Session["tmpl"].ToString();
                }
                if(Session["searchdate"]!=null)
                {
                    lblsearchdate.Text = Session["searchdate"].ToString();
                }
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    private void LoadDetailAccount()
    {
        string ErrorCode = string.Empty;
        string ErrorDesc = string.Empty;
        Account acct = new Account();
        Hashtable hashtable = acct.GetInfoAccount(Session["userID"].ToString(), Session["acctpl"].ToString(), ref ErrorCode, ref ErrorDesc);
        if (ErrorCode != "0")
        {
            throw new IPCException(ErrorCode);
        }
        if (hashtable != null)
        {
            lblAccountNumber_DD.Text = hashtable["ACCOUNTNO"].ToString();
            lblAccountName_DD.Text = hashtable["FULLNAME"].ToString();
            lblCurrency_DD.Text = hashtable["CCYID"].ToString();
            lblBranch_DD.Text = hashtable["BRANCHNAME"].ToString();
        }
    }
}