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
using SmartPortal.BLL;
using SmartPortal.Common;
using SmartPortal.Common.Utilities;
using SmartPortal.Constant;
using SmartPortal.ExceptionCollection;
using SmartPortal.Model;

public partial class Widgets_IBCorpUserLevel_Control_Widget : WidgetBase
{
    string IPCERRORCODE = string.Empty;
    string IPCERRORDESC = string.Empty;
    private string ACTION = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = string.Empty;
            ACTION = GetActionPage();
            pnConfirm.Visible = true;
            pnResult.Visible = false;
            if (!IsPostBack)
            {
                ddlGroup.DataSource = IPC.DICGROUPID;
                ddlGroup.DataTextField = "Value";
                ddlGroup.DataValueField = "Key";
                ddlGroup.DataBind();

                switch (ACTION)
                {
                    case IPC.ACTIONPAGE.ADD:
                        txtNameGroup.Text = string.Empty;
                        txtShortNameGroup.Text = string.Empty;
                        txtDescGroup.Text = string.Empty;
                        break;
                    case IPC.ACTIONPAGE.EDIT:
                        ddlGroup.Enabled = false;
                        BindData();
                        break;
                }
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
            string group = GetParamsPage(IPC.GROUPID)[0].Trim();
            string ctrno = GetParamsPage(IPC.CONTRACTNO)[0].Trim();
            DataSet dsGroup = new SmartPortal.IB.CorpUser().DetailsCorpUserlevel(group, Session["UserID"].ToString(), ctrno, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE != "0")
            {
                throw new SmartPortal.ExceptionCollection.IPCException(IPCERRORDESC);
            }
            DataTable dtGroup = new DataTable();
            dtGroup = dsGroup.Tables[0];
            if (dtGroup.Rows.Count != 0)
            {
                ddlGroup.SelectedValue = dtGroup.Rows[0]["GroupID"].ToString();
                txtNameGroup.Text = dtGroup.Rows[0]["GroupName"].ToString();
                txtShortNameGroup.Text = dtGroup.Rows[0]["GroupShortName"].ToString();
                txtDescGroup.Text = dtGroup.Rows[0]["GroupDesc"].ToString();
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void btnLuu_Click(object sender, EventArgs e)
    {
        try
        {
            if (!IBCheckPermitPageAction(ACTION)) return;

            string ctrno = GetParamsPage(IPC.CONTRACTNO)[0].Trim();
            switch (ACTION)
            {
                case IPC.ACTIONPAGE.ADD:
                    new SmartPortal.IB.CorpUser().AddCorpUserLevel(ddlGroup.SelectedValue, ctrno,Session["userID"].ToString(), Utility.KillSqlInjection(txtNameGroup.Text.Trim()), Utility.KillSqlInjection(txtShortNameGroup.Text.Trim()), Utility.KillSqlInjection(txtDescGroup.Text.Trim()), ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE == "0")
                    {
                        lblError.Text = Resources.labels.addusergroupsuccessfully;
                        pnConfirm.Visible = false;
                        pnResult.Visible = true;
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
                        return;
                    }
                    break;
                case IPC.ACTIONPAGE.EDIT:
                    new SmartPortal.IB.CorpUser().UpdateCorpUserlevel(ddlGroup.SelectedValue, ctrno, Session["userID"].ToString(), Utility.KillSqlInjection(txtNameGroup.Text.Trim()), Utility.KillSqlInjection(txtShortNameGroup.Text.Trim()), Utility.KillSqlInjection(txtDescGroup.Text.Trim()), ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE == "0")
                    {
                        lblError.Text = Resources.labels.updateusergroupsuccessfully;
                        pnConfirm.Visible = false;
                        pnResult.Visible = true;
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
                        return;
                    }
                    break;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        RedirectBackToMainPage();
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        RedirectToActionPage(IPC.ACTIONPAGE.ADD, "&" + SmartPortal.Constant.IPC.CONTRACTNO + "=" + GetParamsPage(IPC.CONTRACTNO)[0].Trim());
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        RedirectBackToMainPage();
    }
}
