using System;
using System.Configuration;
using System.Data;
using System.Web.UI.WebControls;
using SmartPortal.BLL;
using SmartPortal.Common.Utilities;
using SmartPortal.Constant;
using SmartPortal.ExceptionCollection;
using SmartPortal.Model;


public partial class Widgets_IBCorpUserLevel_Widgets : WidgetBase
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

                DataSet dsContract = new SmartPortal.IB.Transactions().GetUserOfContractNoByUID(Utility.KillSqlInjection(Session["userID"].ToString()), "", ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE != "0")
                {
                    throw new SmartPortal.ExceptionCollection.IPCException(IPCERRORCODE);
                }
                DataTable dtContract = new DataTable();
                dtContract = dsContract.Tables[0];
                if (dtContract.Rows.Count != 0)
                {
                    lbctr_no.Text = dtContract.Rows[0]["CONTRACTNO"].ToString();
                }

                ddlGroup.DataSource = IPC.DICGROUPID;
                ddlGroup.DataTextField = "Value";
                ddlGroup.DataValueField = "Key";
                ddlGroup.DataBind();
                ddlGroup.Items.Insert(0, new ListItem(Resources.labels.tatca, ""));

                BindData();
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    void BindData()
    {
        try
        {
            DataSet dtProcess = new DataSet();
            dtProcess = new SmartPortal.IB.CorpUser().SearchCorpUserlevel(Utility.KillSqlInjection(ddlGroup.SelectedValue.ToString()), Utility.KillSqlInjection(lbctr_no.Text), Utility.KillSqlInjection(txtKeyword.Text.Trim()), ref IPCERRORCODE, ref IPCERRORDESC);

            if (IPCERRORCODE == "0")
            {
                rptUserLevel.DataSource = dtProcess;
                rptUserLevel.DataBind();
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
        RedirectToActionPage(IPC.ACTIONPAGE.ADD, "&" + SmartPortal.Constant.IPC.CONTRACTNO + "=" + lbctr_no.Text);
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
                        new SmartPortal.IB.CorpUser().DeleteCorpUserlevel(pros[0], pros[1], ref IPCERRORCODE,
                            ref IPCERRORDESC);
                        if (IPCERRORCODE == "0")
                        {
                            BindData();
                            lblError.Text = Resources.labels.deletegroupsuccessfully;
                        }
                        else
                        {
                            if (IPCERRORCODE == "91780")
                            {
                                //ErrorCodeModel EM = new ErrorCodeModel();
                                //EM = new ErrorBLL().Load(IPCERRORCODE == IPC.ERRORCODE.IPC ? 6768 : Utility.IsInt(SmartPortal.Constant.IPC.ERRORCODE.ACTIVEUSERLEVEL), System.Globalization.CultureInfo.CurrentCulture.ToString());
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
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    protected void rptUserLevel_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            RepeaterItem item = e.Item;
            if (item.ItemType == ListItemType.AlternatingItem || item.ItemType == ListItemType.Item)
            {
                LinkButton lbEdit = (LinkButton)e.Item.FindControl("lbEdit");
                LinkButton lbDelete = (LinkButton)e.Item.FindControl("lbDelete");

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

    protected void rptUserLevel_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            string commandName = e.CommandName;
            if (!IBCheckPermitPageAction(commandName)) return;

            string commandArg = e.CommandArgument.ToString();
            string[] pros = commandArg.Split('|');
            switch (commandName)
            {
                case IPC.ACTIONPAGE.EDIT:
                    RedirectToActionPage(IPC.ACTIONPAGE.EDIT, "&" + SmartPortal.Constant.IPC.GROUPID + "=" + pros[0] + "&" + SmartPortal.Constant.IPC.CONTRACTNO + "=" + pros[1], false);
                    break;
                case IPC.ACTIONPAGE.DELETE:
                    new SmartPortal.IB.CorpUser().DeleteCorpUserlevel(pros[0], pros[1], ref IPCERRORCODE,
                        ref IPCERRORDESC);
                    if (IPCERRORCODE == "0")
                    {
                        BindData();
                        lblError.Text = Resources.labels.deletegroupsuccessfully;
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

}
