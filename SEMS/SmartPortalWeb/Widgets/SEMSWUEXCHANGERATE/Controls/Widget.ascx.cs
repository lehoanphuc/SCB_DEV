using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using SmartPortal.Common.Utilities;
using SmartPortal.Constant;
using SmartPortal.ExceptionCollection;
using SmartPortal.SEMS;

public partial class Widgets_SEMSEXCHANGERATE_Controls_Widget : WidgetBase
{
    string ACTION = string.Empty;
    string IPCERRORCODE = string.Empty;
    string IPCERRORDESC = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            txtAmount.Attributes.Add("onkeypress", "return isNumberK(event)");
            txtAmount.Attributes.Add("onkeyup", "return isNumberK(event)");
            ACTION = GetActionPage();
            lblError.Text = "";
            if (!IsPostBack)
            {
                LoadDLL();
                BindData();
            }
            ddlCCYID_SelectedIndexChanged(sender, e);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"],
                this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message,
                Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"],
                Request.Url.Query);
        }
    }

    void BindData()
    {
        try
        {
            switch (ACTION)
            {
                case IPC.ACTIONPAGE.ADD:
                    txtExchangeID.Enabled = false;
                    break;
                default:
                    string ID = GetParamsPage(IPC.ID)[0].Trim();
                    DataSet dtProcess = new DataSet();
                    dtProcess = new SmartPortal.SEMS.ExchangeRate().GetWuExchange(Utility.KillSqlInjection(ID), Utility.KillSqlInjection(""), Utility.KillSqlInjection("All"), Utility.KillSqlInjection("All"), 1, 0, ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE == "0")
                    {
                        DataTable exchangerate = new DataTable();
                        exchangerate = dtProcess.Tables[0];
                        if (exchangerate.Rows.Count > 0)
                        {
                            txtExchangeID.Text = exchangerate.Rows[0]["ExchangeID"].ToString();
                            txtExchangeName.Text = exchangerate.Rows[0]["ExchangeName"].ToString();
                            txtAmount.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(exchangerate.Rows[0]["Amount"].ToString(), "");

                            ddlCCYID.SelectedValue = exchangerate.Rows[0]["ExchangeID"].ToString();
                            ddlCountry.SelectedValue = exchangerate.Rows[0]["CountryCode"].ToString();
                        }
                        else
                        {
                            throw new IPCException(IPCERRORDESC);
                        }
                    }
                    else
                    {
                        throw new IPCException(IPCERRORCODE);
                    }
                    break;
            }


            #region Enable/Disable theo Action
            switch (ACTION)
            {
                case IPC.ACTIONPAGE.ADD:
                    txtExchangeID.Enabled = false;
                    break;
                case IPC.ACTIONPAGE.DETAILS:
                    pnAdd.Enabled = false;
                    btnClear.Visible = false;
                    btsave.Visible = false;
                    break;
                case IPC.ACTIONPAGE.EDIT:
                    txtExchangeID.Enabled = false;
                    break;
            }
            #endregion
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"],
                this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message,
                Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"],
                Request.Url.Query);
        }

    }

    void LoadDLL()
    {
        try
        {
            DataTable dtCurrency = new ExchangeRate().GetAllCCYID(ref IPCERRORCODE, ref IPCERRORDESC).Tables[0];
            if (dtCurrency.Rows.Count > 0)
            {
                ddlCCYID.DataSource = dtCurrency;
                ddlCCYID.DataTextField = "Currency";
                ddlCCYID.DataValueField = "ExchangeID";
                ddlCCYID.DataBind();
                ddlCCYID.Items.Insert(ddlCCYID.Items.Count, new ListItem("Other", "OTHER"));
            }
            else
            {
                ddlCCYID.Items.Clear();
                ddlCCYID.Items.Add(new ListItem(Resources.labels.nothing, ""));
            }

            DataTable country = new ExchangeRate().GetAllCountry("", ref IPCERRORCODE, ref IPCERRORDESC).Tables[0];
            if (country.Rows.Count > 0)
            {
                ddlCountry.DataSource = country;
                ddlCountry.DataValueField = "COUNTRYCODES";
                ddlCountry.DataTextField = "COUNTRYNAME";
                ddlCountry.DataBind();
            }
            else
            {
                ddlCountry.Items.Clear();
                ddlCountry.Items.Add(new ListItem(Resources.labels.nothing, ""));
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"],
                this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message,
                Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"],
                Request.Url.Query);
        }
    }

    protected void btback_Click(object sender, EventArgs e)
    {
        RedirectBackToMainPage();
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        try
        {
            txtExchangeName.Text = "";
            txtAmount.Text = "";
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }

    protected void btsave_Click(object sender, EventArgs e)
    {
        try
        {
            new SmartPortal.SEMS.ExchangeRate().InsertExchangeRate(
                 Utility.KillSqlInjection(txtExchangeName.Text.Trim())
                , Utility.KillSqlInjection(txtExchangeID.Text.Trim())
                , Utility.KillSqlInjection(ddlCountry.SelectedValue.Trim())
                ,SmartPortal.Common.Utilities.Utility.isDouble(txtAmount.Text.Trim(),true), Session["userID"].ToString()
                ,Utility.KillSqlInjection(txtCcyid.Text.Trim()),ACTION, ref IPCERRORCODE, ref IPCERRORDESC);
            if(IPCERRORCODE == "0")
            {
                switch (ACTION)
                {
                    case IPC.ACTIONPAGE.ADD:
                        lblError.Text = Resources.labels.addsuccessfully;
                        break;
                    default:
                        lblError.Text = "Edit exchange successfullly";
                        break;
                }
                
            }
            else
            {
                lblError.Text = IPCERRORDESC;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }

    protected void ddlCCYID_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if(ddlCCYID.SelectedValue == "OTHER")
            {
                pnccyid.Visible = true;
                txtCcyid.Text = "";
            }
            else
            {
                pnccyid.Visible = false;
                txtCcyid.Text = ddlCCYID.SelectedItem.Text;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
}

