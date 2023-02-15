using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartPortal.Common.Utilities;
using SmartPortal.Constant;

public partial class Widgets_SEMSSETUPSMS_Widget : WidgetBase
{
    string IPCERRORCODE = String.Empty;
    string IPCERRORDESC = String.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            btnSave.Visible = CheckPermitPageAction(IPC.ACTIONPAGE.EDIT);
            lblError.Text = string.Empty;
            if (!IsPostBack)
            {
                LoadDll();
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    protected void LoadDll()
    {
        try
        {
            DataSet ds = new SmartPortal.SEMS.Transactions().GetListTranCodeSms(ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE != "0")
            {
                throw new SmartPortal.ExceptionCollection.IPCException(IPCERRORDESC);
            }
            ddlTranType.DataSource = ds;
            ddlTranType.DataTextField = "REQUESTTEMPLATE";
            ddlTranType.DataValueField = "IPCTRANCODE";
            ddlTranType.DataBind();

            ddlTranType.SelectedIndex = 0;
            ddlTranType_OnSelectedIndexChanged(null, EventArgs.Empty);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable tableTemplate = (DataTable)ViewState["SMSTEMPLATE"];
            DataRow[] dr = tableTemplate.Select("SENDTYPE = '" + ddlSendType.SelectedValue.ToString() + "' and LANG = '" + ddlLanguage.SelectedValue.ToString() + "'");
            txtResponseTemplate.Text = dr[0]["RESPONSETEMPLATE"].ToString();
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (!CheckPermitPageAction(IPC.ACTIONPAGE.EDIT)) return;
            new SmartPortal.SEMS.Transactions().UpdateTemplateSMS(Utility.KillSqlInjection(ddlTranType.SelectedValue.ToString()), Utility.KillSqlInjection(ddlSendType.SelectedValue.ToString()), Utility.KillSqlInjection(ddlLanguage.SelectedValue.ToString()), Utility.KillSqlInjection(txtResponseTemplate.Text.Trim()), ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                lblError.Text = Resources.labels.updatesuccessfully;
                DataSet ds = new SmartPortal.SEMS.Transactions().GetTemplateSmsByTranCode(Utility.KillSqlInjection(ddlTranType.SelectedValue.ToString()), ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE != "0")
                {
                    throw new SmartPortal.ExceptionCollection.IPCException(IPCERRORDESC);
                }
                DataTable dt = ds.Tables[0];
                ViewState["SMSTEMPLATE"] = dt;
            }
            else
            {
                throw new SmartPortal.ExceptionCollection.IPCException(IPCERRORDESC);
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    protected void ddlTranType_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = new SmartPortal.SEMS.Transactions().GetTemplateSmsByTranCode(Utility.KillSqlInjection(ddlTranType.SelectedValue.ToString()), ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE != "0")
            {
                throw new SmartPortal.ExceptionCollection.IPCException(IPCERRORDESC);
            }
            DataTable dt = ds.Tables[0];
            ViewState["SMSTEMPLATE"] = dt;

            DataTable dtSendType = dt;
            dtSendType.DefaultView.Sort = "[SENDTYPE] DESC";
            ddlSendType.DataSource = dtSendType.DefaultView.ToTable(true, "SENDTYPE");
            ddlSendType.DataTextField = "SENDTYPE";
            ddlSendType.DataValueField = "SENDTYPE";
            ddlSendType.DataBind();

            DataTable dtLang = dt;
            ddlLanguage.DataSource = dtLang.DefaultView.ToTable(true, "LANG");
            ddlLanguage.DataTextField = "LANG";
            ddlLanguage.DataValueField = "LANG";
            ddlLanguage.DataBind();

            DataSet dsParam = new SmartPortal.SEMS.Transactions().GetListParamSmsByTranCode(Utility.KillSqlInjection(ddlTranType.SelectedValue.ToString()), ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE != "0")
            {
                throw new SmartPortal.ExceptionCollection.IPCException(IPCERRORDESC);
            }
            rptParam.DataSource = dsParam;
            rptParam.DataBind();

            btnSearch_Click(null, EventArgs.Empty);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
}