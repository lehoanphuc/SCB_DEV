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
using SmartPortal.Constant;
using SmartPortal.ExceptionCollection;

public partial class Widgets_IBCorpUserManagement_Widget : WidgetBase
{
    string IPCERRORCODE;
    string IPCERRORDESC;
   
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Session["NGUOIQUANTRI"] = null;
            if (!IsPostBack)
            {
                //load phong ban
                DataSet ds = new SmartPortal.SEMS.Dept().GetAllByUID(Session["userID"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE != "0")
                {
                    throw new SmartPortal.ExceptionCollection.IPCException(IPCERRORCODE);
                }
                //get contractno
                string contractNo = "";
                DataTable userTableTemp = (new SmartPortal.SEMS.User().GetFullUserByUID(Session["userID"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
                if (userTableTemp.Rows.Count != 0)
                {
                    contractNo = userTableTemp.Rows[0]["CONTRACTNO"].ToString();
                }

                DataTable deptTable = new DataTable();
                deptTable = ds.Tables[0];

                dllDept.DataSource = deptTable;
                dllDept.DataTextField = "DEPTNAME";
                dllDept.DataValueField = "DEPTID";
                dllDept.DataBind();

                //lay thong tin user theo phong ban
                DataTable userTable = new DataTable();
                DataSet dsUser = new SmartPortal.SEMS.User().GetUserByDeptID(dllDept.SelectedValue, contractNo, ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE != "0")
                {
                    throw new SmartPortal.ExceptionCollection.IPCException(IPCERRORCODE);
                }
                else
                {
                    gvUser.DataSource = dsUser;
                    gvUser.DataBind();
                }

            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }


    protected void binAction()
    {
        try
        {
            DataSet ds = new SmartPortal.SEMS.Dept().GetAllByUID(Session["userID"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE != "0")
            {
                throw new SmartPortal.ExceptionCollection.IPCException(IPCERRORCODE);
            }
            //get contractno
            string contractNo = "";
            DataTable userTableTemp = (new SmartPortal.SEMS.User().GetFullUserByUID(Session["userID"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
            if (userTableTemp.Rows.Count != 0)
            {
                contractNo = userTableTemp.Rows[0]["CONTRACTNO"].ToString();
            }

            DataTable deptTable = new DataTable();
            deptTable = ds.Tables[0];

            dllDept.DataSource = deptTable;
            dllDept.DataTextField = "DEPTNAME";
            dllDept.DataValueField = "DEPTID";
            dllDept.DataBind();

            //lay thong tin user theo phong ban
            DataTable userTable = new DataTable();
            DataSet dsUser = new SmartPortal.SEMS.User().GetUserByDeptID(dllDept.SelectedValue, contractNo, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE != "0")
            {
                throw new SmartPortal.ExceptionCollection.IPCException(IPCERRORCODE);
            }
            else
            {
                gvUser.DataSource = dsUser;
                gvUser.DataBind();
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
        try
        {
            Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=194&a=add&did=" + dllDept.SelectedValue.ToString()), false);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
    protected void gvUser_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                DataRowView drv = e.Item.DataItem as DataRowView;
                RepeaterItem item = e.Item;
                Label lblStatus = (Label)e.Item.FindControl("lblStatus");
                LinkButton lbEdit = (LinkButton)e.Item.FindControl("lbEdit");
                LinkButton lbDelete = (LinkButton)e.Item.FindControl("lbDelete");
                LinkButton lblUserID = (LinkButton)e.Item.FindControl("lblUserID");
                switch (drv["STATUS"].ToString().Trim())
                {
                    case SmartPortal.Constant.IPC.NEW:
                        lblStatus.Text = Resources.labels.connew;
                        break;
                    case SmartPortal.Constant.IPC.DELETE:
                        lblStatus.Text = Resources.labels.condelete;
                        break;
                    case SmartPortal.Constant.IPC.ACTIVE:
                        lblStatus.Text = Resources.labels.conactive;
                        break;
                    case SmartPortal.Constant.IPC.BLOCK:
                        lblStatus.Text = Resources.labels.conblock;
                        break;
                    case SmartPortal.Constant.IPC.PENDING:
                        lblStatus.Text = Resources.labels.conpending;
                        break;
                    case SmartPortal.Constant.IPC.REJECT:
                        lblStatus.Text = Resources.labels.conreject;
                        break;
                    case SmartPortal.Constant.IPC.INACTIVE:
                        lblStatus.Text = Resources.labels.inactive;
                        break;
                }
                if (drv["STATUS"].ToString().Trim() == SmartPortal.Constant.IPC.DELETE)
                {
                    lbEdit.Enabled = false;
                    lbEdit.OnClientClick = string.Empty;
                    lbDelete.Enabled = false;
                    lbDelete.OnClientClick = string.Empty;
                    lblUserID.Enabled = false;
                    lblUserID.OnClientClick = string.Empty;
                    
                }
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }

    protected void gvUser_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            string commandName = e.CommandName;
            string commandArg = e.CommandArgument.ToString();
            string[] pros = commandArg.Split('|');
            switch (commandName)
            {
                case IPC.DETAILS:
                    if (!IBCheckPermitPageAction("DETAILS")) return;
                    RedirectToActionPage(IPC.ACTIONPAGE.DETAILS, "&uid=" + commandArg, false);
                    //Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=192&a=viewdetail&uid=" + commandArg), false);
                    break;
                case IPC.EDIT:
                    if (!IBCheckPermitPageAction("EDIT")) return;
                    RedirectToActionPage(IPC.ACTIONPAGE.EDIT, "&uid=" + commandArg + "&did=" + dllDept.SelectedValue.ToString(), false);
                    //Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=196&a=edit&uid=" + commandArg + "&did=" + dllDept.SelectedValue.ToString()), false);
                    break;
                case IPC.DELETE:
                    //Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=198&uid=" + commandArg), false);
                    if (!IBCheckPermitPageAction("DELETE")) return;
                    new SmartPortal.SEMS.User().DeleteUserByID(commandArg, SmartPortal.Constant.IPC.DELETE, ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE != "0")
                    {
                        throw new IPCException(IPCERRORDESC);
                    }
                    binAction();
                    //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "EndRequestHandler();", true);
                    lblAlert.Text = Resources.labels.deleteusersuccessfull;
                    break;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
}
