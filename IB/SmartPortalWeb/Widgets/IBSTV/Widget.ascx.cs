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

using System.Text;
using SmartPortal.Common.Utilities;
using SmartPortal.ExceptionCollection;
using SmartPortal.Constant;

public partial class Widgets_IBSTV_Widget : WidgetBase
{
    string IPCERRORCODE;
    string IPCERRORDESC;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblAlert.Text = "";
            litPager.Text = "";

            if (!IsPostBack)
            {
                #region load loại giao dịch
                //load tran app
                DataSet dsTranApp = new SmartPortal.IB.Schedule().LoadTransferType(Utility.KillSqlInjection(SmartPortal.Constant.IPC.ISSCHEDULE), Utility.KillSqlInjection(SmartPortal.Constant.IPC.YES), ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE != "0")
                {
                    throw new SmartPortal.ExceptionCollection.IPCException(IPCERRORDESC);
                }
                DataTable dtTranApp = new DataTable();
                dtTranApp = dsTranApp.Tables[0];
                if (dtTranApp.Rows.Count != 0)
                {
                    ddlTransactionType.DataSource = dtTranApp;
                    ddlTransactionType.DataTextField = "PAGETITLE";
                    ddlTransactionType.DataValueField = "TRANCODE";
                    ddlTransactionType.DataBind();
                    //ddlTransactionType.Items.Remove(ddlTransactionType.Items.FindByValue("IBINTERBANKTRANSFER"));
                }
                #endregion

                BindData();
            }
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], "Widgets_IBSTV_Widget", "Page_Load", IPCex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_IBSTV_Widget", "Page_Load", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    public void BindData()
    {
        DataSet dsSchedule = new SmartPortal.IB.Schedule().Load(txtScheduleName.Text, ddlTransactionType.SelectedValue, Session["userID"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
        if (IPCERRORCODE == "0")
        {
            if (dsSchedule.Tables[0].Rows.Count == 0)
            {
                rptSTV.DataSource = null;

            }
            else
                rptSTV.DataSource = dsSchedule;
            rptSTV.DataBind();

            if (dsSchedule.Tables[0].Rows.Count == 0)
            {
                btnDelete.Visible = false;
                lblAlert.Text = "<div style='text-align:center;padding-bottom:10px;font-weight:bold;color:red'>" + Resources.labels.datanotfound + "</div>";
            }
            else
            {
                btnDelete.Visible = true;
            }

        }
        if (!IBCheckPermitPageAction(IPC.ACTIONPAGE.ADD))
        {
            Button1.Visible = false;
        }
        if (!IBCheckPermitPageAction(IPC.ACTIONPAGE.DELETE))
        {
            btnDelete.Visible = false;
        }
    }
    protected void rptSTV_OnItemCommandommand(object source, RepeaterCommandEventArgs e)
    {
        string commandName = e.CommandName;
        if (!IBCheckPermitPageAction(commandName)) return;

        string commandArg = e.CommandArgument.ToString();
        string[] pros = commandArg.Split('|');
        switch (commandName)
        {
            case IPC.ACTIONPAGE.DELETE:
                DataSet ProcessAppTable = new DataSet();
                ProcessAppTable = new SmartPortal.IB.Schedule().DeleteScheduleByID(pros[0], ref IPCERRORCODE, ref IPCERRORDESC);
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
                RedirectToActionPage(IPC.ACTIONPAGE.DETAILS, "&ID=" + pros[0]+ "&trcd="+ pros[1]);
                break;
            default:
                return;
        }
    }
    protected void rptSTV_OnItemDataBounddaBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            RepeaterItem item = e.Item;
            if (item.ItemType == ListItemType.AlternatingItem || item.ItemType == ListItemType.Item)
            {
                LinkButton lblview = (LinkButton)e.Item.FindControl("lblview");
                LinkButton lbDelete = (LinkButton)e.Item.FindControl("lbDelete");

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
    protected void Button1_Click1(object sender, EventArgs e)
    {
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/default.aspx?p=117"));
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        if (!IBCheckPermitPageAction(IPC.ACTIONPAGE.DELETE)) return;
        string lst = hdId.Value;
        if (lst.Length > 0)
        {
            string[] arrSplit = lst.Split('#');
            foreach (string item in arrSplit)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    string[] pros = item.Split('|');
                    DataSet ProcessAppTable = new DataSet();
                    ProcessAppTable = new SmartPortal.IB.Schedule().DeleteScheduleByID(pros[0], ref IPCERRORCODE, ref IPCERRORDESC);
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
            lblError.Visible = true;
        }

        hdId.Value = string.Empty;
    }
    private void ShowPopUpMsg(string msg)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("alert('");
        sb.Append(msg.Replace("\n", "\\n").Replace("\r", "").Replace("'", "\\'"));
        sb.Append("');");
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "showalert", sb.ToString(), true);
    }
    protected static string getScheType(object dataItem)
    {
        string result = string.Empty;
        string status = Convert.ToString(DataBinder.Eval(dataItem, "SCHEDULETYPE"));
        switch (status)
        {
            case "WEEKLY":
                result = Resources.labels.hangtuan;
                break;
            case "DAILY":
                result = Resources.labels.hangngay;
                break;
            case "MONTHLY":
                result = Resources.labels.hangthang;
                break;
            case "ONETIME":
                result = Resources.labels.motlan;
                break;
        }
        return result;
    }
}
