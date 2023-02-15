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
using SmartPortal.BLL;
using SmartPortal.Common;
using SmartPortal.Common.Utilities;
using SmartPortal.Constant;
using SmartPortal.ExceptionCollection;
using SmartPortal.Model;


public partial class Widgets_IBCorpUserApproveProcess_Widget : WidgetBase
{
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = string.Empty;
            if (!IsPostBack)
            {
                btnAddNew.Visible = IBCheckPermitPageAction(IPC.ACTIONPAGE.ADD);
                btnDelete.Visible = IBCheckPermitPageAction(IPC.ACTIONPAGE.DELETE);

                DataSet dsContract = new SmartPortal.IB.Transactions().GetUserOfContractNoByUID(Utility.KillSqlInjection(Session["userID"].ToString()), "", ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE != "0")
                {
                    throw new SmartPortal.ExceptionCollection.IPCException(IPCERRORCODE);
                }
                DataTable dtContract = new DataTable();
                dtContract = dsContract.Tables[0];
                if (dtContract.Rows.Count != 0)
                {
                    lblContractNo.Text = dtContract.Rows[0]["CONTRACTNO"].ToString();
                }

                DataSet dsResult = new SmartPortal.SEMS.Product().GetTranNameByContractNo(Utility.KillSqlInjection(lblContractNo.Text), ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE != "0")
                {
                    throw new IPCException(IPCERRORDESC);
                }
                ddltrans.DataSource = dsResult;
                ddltrans.DataTextField = "PAGENAME";
                ddltrans.DataValueField = "TRANCODE";
                ddltrans.DataBind();
                ddltrans.Items.Insert(0, new ListItem(Resources.labels.tatca, "ALL"));

                ddlCCYID.DataSource = new SmartPortal.SEMS.Product().LoaddAllCCYID(ref IPCERRORCODE, ref IPCERRORDESC);
                ddlCCYID.DataTextField = "CCYID";
                ddlCCYID.DataValueField = "CCYID";
                ddlCCYID.DataBind();
                ddlCCYID.Items.Insert(0, new ListItem(Resources.labels.tatca, ""));

                BindData();
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }

    void BindData()
    {
        try
        {
            DataSet dtProcess = new DataSet();
            dtProcess = new SmartPortal.IB.Transactions().SearchCorpUserApproveProcess(Utility.KillSqlInjection(txtapptranID.Text.Trim()), Utility.KillSqlInjection(ddltrans.SelectedValue), Utility.KillSqlInjection(lblContractNo.Text), Utility.KillSqlInjection(ddlCCYID.SelectedValue), ref IPCERRORCODE, ref IPCERRORDESC);

            if (IPCERRORCODE == "0")
            {
                rptProcessList.DataSource = dtProcess;
                rptProcessList.DataBind();
            }
            else
            {
                throw new IPCException(IPCERRORCODE);
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        if (!IBCheckPermitPageAction(IPC.ACTIONPAGE.ADD)) return;
        RedirectToActionPage(IPC.ACTIONPAGE.ADD, string.Empty);
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
                        new SmartPortal.IB.Transactions().DeleteProcess(item, ref IPCERRORCODE, ref IPCERRORDESC);
                        if (IPCERRORCODE == "0")
                        {
                            BindData();
                            lblError.Text = Resources.labels.xoaquytrinhduyetthanhcong;
                        }
                        else
                        {
                            if (IPCERRORCODE != IPC.ERRORCODE.IPC)
                            {
                                //ErrorCodeModel EM = new ErrorCodeModel();
                                //EM = new ErrorBLL().Load(Utility.IsInt(IPCERRORCODE), System.Globalization.CultureInfo.CurrentCulture.ToString());
                                lblError.Text = IPCERRORDESC;
                                hdId.Value = string.Empty;
                            }
                            else
                            {
                                throw new SmartPortal.ExceptionCollection.IPCException(SmartPortal.Constant.IPC.ERRORCODE.IPC);
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
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            BindData();
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }

    protected void rptProcessList_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            string commandName = e.CommandName; 
            if (!IBCheckPermitPageAction(commandName)) return;

            string commandArg = e.CommandArgument.ToString();
            switch (commandName)
            {
                case IPC.ACTIONPAGE.DETAILS:
                    RedirectToActionPage(IPC.ACTIONPAGE.DETAILS, "&" + SmartPortal.Constant.IPC.WORKFLOWID + "=" + commandArg, false);
                    break;
                case IPC.ACTIONPAGE.EDIT:
                    RedirectToActionPage(IPC.ACTIONPAGE.EDIT, "&" + SmartPortal.Constant.IPC.WORKFLOWID + "=" + commandArg, false);
                    break;
                case IPC.ACTIONPAGE.DELETE:
                    new SmartPortal.IB.Transactions().DeleteProcess(commandArg, ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE == "0")
                    {
                        BindData();
                        lblError.Text = Resources.labels.xoaquytrinhduyetthanhcong;
                    }
                    else
                    {
                        if (IPCERRORCODE != IPC.ERRORCODE.IPC)
                        {
                            //ErrorCodeModel EM = new ErrorCodeModel();
                            //EM = new ErrorBLL().Load(Utility.IsInt(IPCERRORCODE), System.Globalization.CultureInfo.CurrentCulture.ToString());
                            lblError.Text = IPCERRORDESC;
                        }
                        else
                        {
                            throw new SmartPortal.ExceptionCollection.IPCException(SmartPortal.Constant.IPC.ERRORCODE.IPC);
                        }
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

    protected void rptProcessList_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            RepeaterItem item = e.Item;
            if (item.ItemType == ListItemType.AlternatingItem || item.ItemType == ListItemType.Item)
            {
                LinkButton lbDetails = (LinkButton)e.Item.FindControl("lbDetails");
                LinkButton lbEdit = (LinkButton)e.Item.FindControl("lbEdit");
                LinkButton lbDelete = (LinkButton)e.Item.FindControl("lbDelete");

                if (!IBCheckPermitPageAction(IPC.ACTIONPAGE.DETAILS))
                {
                    lbDetails.Enabled = false;
                    lbDetails.OnClientClick = string.Empty;
                }

                if (!IBCheckPermitPageAction(IPC.ACTIONPAGE.EDIT))
                {
                    lbEdit.Enabled = false;
                    lbEdit.OnClientClick = string.Empty;
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
