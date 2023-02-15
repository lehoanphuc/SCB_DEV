using System;
using System.Collections;
using System.Configuration;
using System.Data;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;


public partial class Widgets_SEMSTransactionsApprove_Widget : WidgetBase
{
    string IPCERRORCODE;
    string IPCERRORDESC;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["tranID"] = null;
            try
            {
                DataSet ds = new SmartPortal.SEMS.Transactions().LoadTranApp(ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE != "0")
                {
                    goto ERROR;
                }
                DataTable dtTran = new DataTable();
                dtTran = ds.Tables[0];

                ddlTransaction.DataSource = dtTran;
                ddlTransaction.DataTextField = "PAGENAME";
                ddlTransaction.DataValueField = "TRANCODE";
                ddlTransaction.DataBind();

                ddlTransaction.Items.Remove(ddlTransaction.Items.FindByValue("IB000499"));

                goto EXIT;
            }
            catch (Exception ex)
            {
                SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSTransactionsApprove_Widget", "Page_Load", ex.ToString(), Request.Url.Query);
                SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

            }
        ERROR:
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], "Widgets_SEMSTransactionsApprove_Widget", "Page_Load", IPCERRORDESC, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);
        EXIT: ;
        }
    }
    protected void btsaveandcont_Click(object sender, EventArgs e)
    {
        string linkApprove="";
        try
        {
            DataSet ds = new SmartPortal.SEMS.Transactions().LoadTranAppByTrancode(ddlTransaction.SelectedValue,ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE != "0")
            {
                goto ERROR;
            }
            DataTable dtTran = new DataTable();
            dtTran = ds.Tables[0];

            if (dtTran.Rows.Count != 0)
            {
                linkApprove = dtTran.Rows[0]["LINKAPPROVE"].ToString();
                linkApprove += "&tc=" + ddlTransaction.SelectedValue;
            }

            goto EXIT;
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSTransactionsApprove_Widget", "btsaveandcont_Click", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    ERROR:
        SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], "Widgets_SEMSTransactionsApprove_Widget", "btsaveandcont_Click", IPCERRORDESC, Request.Url.Query);
        SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);
    EXIT: 
       Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx"+linkApprove));
    }
}
