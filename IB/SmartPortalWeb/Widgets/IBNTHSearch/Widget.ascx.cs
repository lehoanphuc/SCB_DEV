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

using SmartPortal.Common;
using SmartPortal.Common.Utilities;
using SmartPortal.Constant;
using SmartPortal.ExceptionCollection;
using SmartPortal.IB;

public partial class Widgets_IBNTHSearch_Widget : WidgetBase
{
    public static bool isAscend = false;
    private const string ASCENDING = " ASC";
    private const string DESCENDING = " DESC";
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblAlert.Text = "";
            lblError.Text = "";

            if (!IsPostBack)
            {
                //load role của teller
                // Session["userID"] = "24485000";
                DataSet dsTranApp = new SmartPortal.IB.Schedule().LoadTransferType(Utility.KillSqlInjection(SmartPortal.Constant.IPC.ISTEMPLATE), Utility.KillSqlInjection(SmartPortal.Constant.IPC.YES), ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE != "0")
                {
                    throw new SmartPortal.ExceptionCollection.IPCException(IPCERRORDESC);
                }
                DataTable dtTranApp = new DataTable();
                dtTranApp = dsTranApp.Tables[0];
                if (dtTranApp.Rows.Count != 0)
                {
                    ddltrans.DataSource = dtTranApp;
                    ddltrans.DataTextField = "PAGETITLE";
                    ddltrans.DataValueField = "TRANCODE";
                    ddltrans.DataBind();
                    ddltrans.Items.Insert(0, new ListItem(Resources.labels.tatca, ""));
                }
                BindData();
            }
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], "Widgets_IBNTHSearch_Widget", "Page_Load", IPCex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_IBNTHSearch_Widget", "Page_Load", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    void BindData()
    {
        try
        {

            DataSet dtProcess = new DataSet();
            dtProcess = new SmartPortal.IB.Account().GetReceiverList("", Session["userID"].ToString(), Utility.KillSqlInjection(txtReceiver.Text.Trim()), Utility.KillSqlInjection(txtAccount.Text.Trim()), Utility.KillSqlInjection(ddltrans.SelectedValue), "", "", "", "", ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                rptProcessList.DataSource = dtProcess;
                rptProcessList.DataBind();
                if (dtProcess.Tables[0].Rows.Count > 0)
                {
                    btnDelete.Visible = true;
                    rptProcessList.Visible = true;
                }
                else
                {
                    rptProcessList.Visible = false;
                    btnDelete.Visible = false;
                    lblAlert.Text = "<div style='width:100%; padding-top:10px; padding-bottom:10px; text-align:center;'>" + Resources.labels.datanotfound + "</div>";
                }
            }
            else
            {
                throw new IPCException(IPCERRORDESC);
            }
            if (!IBCheckPermitPageAction(IPC.ACTIONPAGE.ADD))
            {
                btnAddNew.Visible = false;
            }
            if (!IBCheckPermitPageAction(IPC.ACTIONPAGE.DELETE))
            {
                btnDelete.Visible = false;
            }
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=113"));
        }
        catch (Exception ex)
        { 

        }
        
    }
    protected void rptProcessList_OnItemCommandommand(object source, RepeaterCommandEventArgs e)
    {
        string commandName = e.CommandName;
        if (!IBCheckPermitPageAction(commandName)) return;

        string commandArg = e.CommandArgument.ToString();
        string[] pros = commandArg.Split('|');
        switch (commandName)
        {
            case IPC.ACTIONPAGE.DELETE:
                Account objAcct = new Account();
                objAcct.DeleteReceiverList(pros[0], Session["userID"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE == "0")
                {
                    BindData();
                    lblError.Text = Resources.labels.xoahanmucthanhcong1;
                }
                else
                {
                    if (IPCERRORCODE == "91780")
                    {
                        lblError.Text = IPCERRORDESC;
                    }
                    else
                    {
                        throw new SmartPortal.ExceptionCollection.IPCException(SmartPortal.Constant.IPC.ERRORCODE.IPC);
                    }
                }
                break;
            case IPC.ACTIONPAGE.DETAILS:
                RedirectToActionPage(IPC.ACTIONPAGE.DETAILS, "&nid=" + pros[0], false);
                break;
            default:
                return;
        }
    }
    protected void rptProcessList_OnItemDataBounddaBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            RepeaterItem item = e.Item;
            if (item.ItemType == ListItemType.AlternatingItem || item.ItemType == ListItemType.Item)
            {
                LinkButton lbDelete = (LinkButton)e.Item.FindControl("lbDelete");  
                LinkButton lblview = (LinkButton)e.Item.FindControl("lblview");

                if (!IBCheckPermitPageAction(IPC.ACTIONPAGE.DELETE))
                {
                    lbDelete.Enabled = false;
                    lbDelete.OnClientClick = string.Empty;
                }
                if (!IBCheckPermitPageAction(IPC.ACTIONPAGE.DETAILS))
                {
                    lblview.Enabled = false;
                    lblview.OnClientClick = string.Empty;
                }
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string lst = hdId.Value;
        try
        {
            if (!IBCheckPermitPageAction(IPC.ACTIONPAGE.DELETE)) return;
            if (lst.Length > 0)
            {
                string[] arrSplit = lst.Split('#');
                foreach (string item in arrSplit)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        string[] pros = item.Split('|');
                        //new SmartPortal.IB.CorpUser().DeleteCorpUserlevel(pros[0], pros[1], ref IPCERRORCODE,
                        //    ref IPCERRORDESC);
                        Account objAcct = new Account();
                        objAcct.DeleteReceiverList(pros[0], Session["userID"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
                        if (IPCERRORCODE == "0")
                        {
                            BindData();
                            lblError.Text = Resources.labels.xoahanmucthanhcong1;
                        }
                        else
                        {
                            if (IPCERRORCODE == "91780")
                            {
                                lblError.Text = IPCERRORDESC;
                                hdId.Value = string.Empty;
                            }
                            else
                            {
                                throw new SmartPortal.ExceptionCollection.IPCException(SmartPortal.Constant.IPC
                                    .ERRORCODE.IPC);
                            }

                            return;
                        }
                    }
                }
            }
            else
            {
                lblError.Text = Resources.labels.chooserecordtodelete;
            }

            hdId.Value = string.Empty;
        }
        catch (Exception ex)
        {

        }

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Session["search"] = "true";
        hdId.Value = string.Empty;
        BindData();        
    }
    protected static string GetStatus(object dataItem)
    {
        string result = string.Empty;
        string status = Convert.ToString(DataBinder.Eval(dataItem, "STATUS"));
        switch (status)
        {
            case SmartPortal.Constant.IPC.NEW:
                result = Resources.labels.choduyet;
                break;
            case SmartPortal.Constant.IPC.ACTIVE:
                result = Resources.labels.duyet;
                break;
            case SmartPortal.Constant.IPC.REJECT:
                result = Resources.labels.khongduyet;
                break;
        }
        return result;
    }
}
