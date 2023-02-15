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
using SmartPortal.Constant;
using SmartPortal.ExceptionCollection;

public partial class Widgets_IBTransferTemplateList_Widget : WidgetBase
{
    string IPCERRORCODE = string.Empty;
    string IPCERRORDESC = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = string.Empty;

            if (!IsPostBack)
            {
                btnAddNew.Visible = IBCheckPermitPageAction(IPC.ACTIONPAGE.ADD);
                btnDelete.Visible = IBCheckPermitPageAction(IPC.ACTIONPAGE.DELETE);

                #region load loại giao dịch
                //load tran app
                DataSet dsTranApp = new SmartPortal.IB.Schedule().LoadTransferType(Utility.KillSqlInjection(SmartPortal.Constant.IPC.ISTEMPLATE), Utility.KillSqlInjection(SmartPortal.Constant.IPC.YES), ref IPCERRORCODE, ref IPCERRORDESC);
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
                }
                #endregion

                BindData();
            }
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], "Widgets_IBTransferTemplateList_Widget", "Page_Load", IPCex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_IBTransferTemplateList_Widget", "Page_Load", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    public void BindData()
    {
        try
        {
            DataSet dsTemplateTransfer = new SmartPortal.IB.Transfer().Load(txtTempName.Text, ddlTransactionType.SelectedValue, Session["userID"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                rptSTV.DataSource = dsTemplateTransfer;
                rptSTV.DataBind();
                if (dsTemplateTransfer.Tables[0].Rows.Count > 0)
                {
                    btnDelete.Visible = true;
                    rptSTV.Visible = true;
                }
                else
                {
                    rptSTV.Visible = false;
                    btnDelete.Visible = false;
                    lblAlert.Text = "<div style='width:100%; padding-top:10px; padding-bottom:10px; text-align:center;'>" + Resources.labels.datanotfound + "</div>";
                }
            }
            else
            {
                throw new IPCException(IPCERRORCODE);
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        if (!IBCheckPermitPageAction(IPC.ACTIONPAGE.ADD)) return;
        RedirectToActionPage(IPC.ACTIONPAGE.ADD, string.Empty);
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            BindData();
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
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
                        new SmartPortal.IB.Transfer().DeleteTemplateByID(pros[0], ref IPCERRORCODE, ref IPCERRORDESC);
                        if (IPCERRORCODE == "0")
                        {
                            BindData();
                            lblError.Text = Resources.labels.huymauthanhcong;
                        }
                        else
                        {
                            throw new SmartPortal.ExceptionCollection.IPCException(IPCERRORCODE);
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
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void rptSTV_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            string commandName = e.CommandName;
            if (!IBCheckPermitPageAction(commandName)) return;

            string commandArg = e.CommandArgument.ToString();
            string[] pros = commandArg.Split('|');
            switch (commandName)
            {
                case IPC.ACTIONPAGE.DETAILS:
                    switch (pros[1])
                    {
                        case SmartPortal.Constant.IPC.TIB:
                            Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("/Default.aspx?p=89&tid=" + pros[0]), false);
                            break;
                        case SmartPortal.Constant.IPC.BAC:
                            Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("/Default.aspx?p=91&tid=" + pros[0]), false);
                            break;
                    }
                    break;
                case IPC.ACTIONPAGE.DELETE:
                    new SmartPortal.IB.Transfer().DeleteTemplateByID(pros[0], ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE == "0")
                    {
                        BindData();
                        lblError.Text = Resources.labels.huymauthanhcong;
                    }
                    else
                    {
                        throw new SmartPortal.ExceptionCollection.IPCException(IPCERRORCODE);
                    }
                    break;
                default:
                    return;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }

    protected void rptSTV_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            RepeaterItem item = e.Item;
            if (item.ItemType == ListItemType.AlternatingItem || item.ItemType == ListItemType.Item)
            {
                LinkButton lbDetail = (LinkButton)e.Item.FindControl("lbDetail");
                LinkButton lbDelete = (LinkButton)e.Item.FindControl("lbDelete");

                if (!IBCheckPermitPageAction(IPC.ACTIONPAGE.DETAILS))
                {
                    lbDetail.Enabled = false;
                    lbDetail.OnClientClick = string.Empty;
                }

                if (!IBCheckPermitPageAction(IPC.ACTIONPAGE.DELETE))
                {
                    lbDelete.Enabled = false;
                    lbDelete.OnClientClick = string.Empty;
                }
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
}
