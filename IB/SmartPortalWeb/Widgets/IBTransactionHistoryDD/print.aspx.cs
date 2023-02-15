using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SmartPortal.IB;
using SmartPortal.Common.Utilities;

public partial class Widgets_IBTransactionHistoryDD_print : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Culture = "en-US";
                UICulture = "en-US";
                if (Session["tmpl"] != null)
                {
                    LoadDetailAccount();
                    ltrStatement.Text = Session["tmpl"].ToString();
                }
            }
        }
        catch (Exception ex)
        {
        }
    }

    private void LoadDetailAccount()
    {
        lbAccountName.Text = string.Empty;
        lbAccountNo.Text = string.Empty;
        lbBranch.Text = string.Empty;
        lbCurrency.Text = string.Empty;
        lbOpenDate.Text = string.Empty;
        //
        string ErrorCode = string.Empty;
        string ErrorDesc = string.Empty;
        DataSet ds = new DataSet();
        Account acct = new Account();
        ds = acct.GetInfoDD(Session["userID"].ToString(), SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["ACCTNO"].ToString(), ref ErrorCode, ref ErrorDesc);
        if (ds.Tables[0].Rows.Count == 1)
        {
            lbAccountNo.Text = ds.Tables[0].Rows[0]["accountno"].ToString();
            lbAccountName.Text = ds.Tables[0].Rows[0]["acctname"].ToString();
            lbCurrency.Text = ds.Tables[0].Rows[0][SmartPortal.Constant.IPC.CCYID].ToString();
            lbOpenDate.Text = SmartPortal.Common.Utilities.Utility.FormatDatetime(ds.Tables[0].Rows[0][SmartPortal.Constant.IPC.OPENDATE].ToString(), "dd/MM/yyyy", DateTimeStyle.DateMMM);
            
            string[] strBranch = ds.Tables[0].Rows[0]["branch"].ToString().Split('.');
            DataSet dsBranch = acct.GetBranch(strBranch[0].ToString());
            if (dsBranch.Tables[0].Rows.Count == 1)
            {
                lbBranch.Text = dsBranch.Tables[0].Rows[0][SmartPortal.Constant.IPC.BRANCHNAME].ToString();
            }
        }
    }
}