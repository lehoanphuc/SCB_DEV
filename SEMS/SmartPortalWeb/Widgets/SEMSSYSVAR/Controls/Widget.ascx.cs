using Newtonsoft.Json;
using SmartPortal.Constant;
using SmartPortal.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartPortal.Common;

public partial class Widgets_EBASYSVAR_Control_Widget : WidgetBase
{
    string ACTION = "";
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        ACTION = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["a"].ToString().Trim();
        if (!IsPostBack)
        {
            BindData();
        }
    }
    void BindData()
    {
        try
        {
            vlcombobox.Visible = false;
            Label1.Visible = false;
            switch (ACTION)
            {
                case IPC.ACTIONPAGE.ADD:
                    switch (ddlType.SelectedValue)
                    {
                        case "String":
                        case "Int":
                            txtValueCode.Visible = true;
                            txtEffectiveDate.Visible = false;
                            ddlReference.Visible = false;
                            txtValueName.Attributes.Add("class", "form-control");
                            break;
                        case "Combobox":
                            txtValueCode.Visible = false;
                            txtEffectiveDate.Visible = false;
                            ddlReference.Visible = true;
                            break;
                        case "Datetime":
                            txtValueCode.Visible = false;
                            txtEffectiveDate.Visible = true;
                            ddlReference.Visible = false;
                            break;
                        default:
                            txtValueCode.Visible = true;
                            txtEffectiveDate.Visible = false;
                            ddlReference.Visible = false;
                            break;
                    }
                    break;
                case IPC.ACTIONPAGE.EDIT:
                    if (CheckPermitPageAction(IPC.ACTIONPAGE.EDIT))
                    {
                        txtValueName.Enabled = false;
                        txtvardesc.Enabled = false;
                        ddlType.Enabled = false;
                        string varname = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cid"].ToString().Trim();
                        DataSet ds = new SmartPortal.SEMS.EBASYSVAR().ViewDetail(varname, ref IPCERRORCODE, ref IPCERRORDESC);
                        if (IPCERRORCODE == "0")
                        {
                            DataTable valuelistTable = new DataTable();
                            valuelistTable = ds.Tables[0];
                            if (valuelistTable.Rows.Count != 0)
                            {
                                txtValueCode.Text = valuelistTable.Rows[0]["VARVALUE"].ToString();
                                txtValueName.Text = valuelistTable.Rows[0]["VARNAME"].ToString();
                                txtvardesc.Text = valuelistTable.Rows[0]["VARDESC"].ToString();
                                ddlType.SelectedValue = valuelistTable.Rows[0]["Type"].ToString();
                                var walvaluelist = valuelistTable.Rows[0]["Reference"].ToString();
                                switch (ddlType.SelectedValue)
                                {
                                    case "String":
                                    case "Email":
                                    case "Int":
                                    case "Hidden":
                                        txtValueCode.Visible = true;
                                        txtEffectiveDate.Visible = false;
                                        ddlReference.Visible = false;
                                        txtValueName.Attributes.Add("class", "form-control");
                                        break;
                                    case "Combobox":
                                        txtValueCode.Visible = false;
                                        txtEffectiveDate.Visible = false;
                                        vlcombobox.Visible = true;
                                        vlcombobox.Enabled = false;
                                        Label1.Visible = true;
                                        hidensv.Visible = false;
                                        if (walvaluelist != null)
                                        {
                                            DataSet list = DataAccess.FillDataSet(walvaluelist);
                                            if (list.Tables.Count > 0)
                                            {
                                                DataTable dt = new DataTable();
                                                dt = list.Tables[0];
                                                DataColumn FeeID = new DataColumn("DataText");
                                                ddlReference.DataSource = dt;
                                                

                                                DataRow[] filteredRows;
                                                if (dt.Columns.Count == 0)
                                                {
                                                    filteredRows = dt.Select(string.Format("{0} LIKE '%{1}%'", dt.Columns[0].ToString(), txtValueCode.Text));
                                                }
                                                else
                                                {
                                                    filteredRows = dt.Select(string.Format("{0} LIKE '%{1}%'", dt.Columns[1].ToString(), txtValueCode.Text));
                                                }
                                                if (filteredRows.Length > 0)
                                                {
                                                    switch (dt.Columns.Count)
                                                    {
                                                        case 0:
                                                            ddlReference.DataTextField = "";
                                                            ddlReference.DataValueField = "";
                                                            ddlReference.SelectedValue = txtValueCode.Text;
                                                            vlcombobox.Text = txtValueCode.Text;
                                                            break;
                                                        case 1:
                                                            ddlReference.DataTextField = dt.Columns[0].ToString();
                                                            ddlReference.DataValueField = txtValueCode.Text;
                                                            ddlReference.SelectedValue = dt.Columns[0].ToString();
                                                            vlcombobox.Text = txtValueCode.Text;
                                                            break;
                                                        case 2:
                                                            ddlReference.DataTextField = dt.Columns["DataText"] is DBNull ? dt.Columns[0].ToString(): dt.Columns["DataText"].ToString();
                                                            ddlReference.DataValueField = dt.Columns["DataValue"] is DBNull ? dt.Columns[1].ToString() : dt.Columns["DataValue"].ToString();
                                                            ddlReference.SelectedValue = txtValueCode.Text;
                                                            vlcombobox.Text = txtValueCode.Text;
                                                            break;
                                                        default:
                                                            ddlReference.DataTextField = dt.Columns["DataText"] is DBNull ? dt.Columns[0].ToString() : dt.Columns["DataText"].ToString();
                                                            ddlReference.DataValueField = dt.Columns["DataValue"] is DBNull ? dt.Columns[1].ToString() : dt.Columns["DataValue"].ToString();
                                                            ddlReference.SelectedValue = txtValueCode.Text;
                                                            vlcombobox.Text = txtValueCode.Text;
                                                            break;
                                                    }
                                                }
                                                else
                                                {
                                                    txtValueCode.Visible = true;
                                                    ddlReference.Visible = false;
                                                    vlcombobox.Visible = false;
                                                    Label1.Visible = false;
                                                    txtValueCode.Text = txtValueCode.Text;
                                                }
                                                ddlReference.DataBind();
                                                //ddlReference.Items.Insert(0, new ListItem("No Reference", ""));
                                            }
                                            else
                                            {
                                                txtValueCode.Visible = true;
                                                ddlReference.Visible = false;
                                                Label1.Visible = false;
                                                vlcombobox.Visible = false;
                                                txtValueCode.Text = txtValueCode.Text;
                                            }
                                        }
                                        break;
                                    case "Datetime":
                                        txtValueCode.Visible = false;
                                        txtEffectiveDate.Visible = true;
                                        ddlReference.Visible = false;
                                        txtEffectiveDate.Text = valuelistTable.Rows[0]["VARVALUE"].ToString();
                                        break;
                                    default:
                                        txtValueCode.Visible = true;
                                        txtEffectiveDate.Visible = false;
                                        ddlReference.Visible = false;
                                        break;
                                }
                            }
                        }
                    }
                    else
                    {
                        lblError.Text = Resources.labels.accessdenied;
                    }
                    break;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "SEMSSYSVAR", "BindData", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        switch (ACTION)
        {
            case IPC.ACTIONPAGE.ADD:
                try
                {
                    if (CheckPermitPageAction(IPC.ACTIONPAGE.ADD))
                    {
                        string valCode = string.Empty;
                        string valname = string.Empty;
                        string valType = string.Empty;
                        string vardesc = string.Empty;
                        string valdate = string.Empty;
                        valType = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlType.SelectedValue.ToString().Trim());
                        switch (valType)
                        {
                            case "String":
                            case "Int":
                            case "Combobox":
                            case "Hidden":
                                valCode = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtValueCode.Text.Trim());
                                break;
                            case "Datetime":
                                valCode = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtEffectiveDate.Text.ToString().Trim());
                                break;
                            case "Email":
                                string [] value = txtValueCode.Text.Split(',');
                                foreach (var item in value)
                                {
                                        if (!IsValidEmail(item) && !Validator.IsValidEmail(item))
                                        {
                                        lblError.Text = Resources.labels.emailkhongdinhdang;
                                        return;
                                        }
                                }
                                valCode = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtEffectiveDate.Text.ToString().Trim());
                                break;
                            default:
                                valCode = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtValueCode.Text.Trim());
                                break;
                        }
                        if (!validate(valCode))
                        {
                            lblError.Text = Resources.labels.bancannhapvaluecode;
                        }
                        valname = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtValueName.Text.Trim());
                        vardesc = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtvardesc.Text.Trim());
                        new SmartPortal.SEMS.EBASYSVAR().Insert(valname, valCode, valType, vardesc, Session["userName"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
                        if (IPCERRORCODE == "0")
                        {
                            lblError.Text = Resources.labels.editebasysvarsuccessfully;
                            Panel1.Enabled = false;
                            btsave.Visible = false;
                        }
                        else
                        {
                            lblError.Text = IPCERRORDESC;
                        }
                    }
                    else
                    {
                        lblError.Text = Resources.labels.accessdenied;
                    }
                }
                catch (Exception ex)
                {
                    SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "SEMSSYSVAR", "btnAddNew_Click_ADD", ex.ToString(), Request.Url.Query);
                    SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
                }
                break;
            case IPC.ACTIONPAGE.EDIT:
                try
                {
                    if (CheckPermitPageAction(IPC.ACTIONPAGE.EDIT))
                    {
                        string valCode = string.Empty;
                        string valname = string.Empty;
                        string valType = string.Empty;
                        string vardesc = string.Empty;
                        string valdate = string.Empty;
                        valType = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlType.SelectedValue.ToString().Trim());
                        switch (valType)
                        {
                            case "String":
                            case "Hidden":
                            case "Int":
                                valCode = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtValueCode.Text.Trim());
                                break;
                            case "Datetime":
                                valCode = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtEffectiveDate.Text.ToString().Trim());
                                break;
                            case "Email":
                                string[] value = txtValueCode.Text.Split(',');
                                foreach (var item in value)
                                {
                                    if (!IsValidEmail(item) && !Validator.IsValidEmail(item))
                                    {
                                        lblError.Text = Resources.labels.emailkhongdinhdang;
                                        return;
                                    }
                                }
                                valCode = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtValueCode.Text.ToString().Trim());
                                break;
                            case "Combobox":
                                valCode = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlReference.SelectedValue.ToString().Trim());
                                if ((valCode == "" || valCode == "0") && SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtValueCode.Text.Trim()) != "")
                                {
                                    valCode = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtValueCode.Text.Trim());
                                }
                                break;
                            default:
                                valCode = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtValueCode.Text.Trim());
                                break;
                        }

                        if (!validate(valCode))
                        {
                            lblError.Text = Resources.labels.bancannhapvaluecode;
                        }
                        valname = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtValueName.Text.Trim());
                        vardesc = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtvardesc.Text.Trim());
                        new SmartPortal.SEMS.EBASYSVAR().Update(valname, valCode, valType, vardesc, Session["userName"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
                        if (IPCERRORCODE == "0")
                        {
                            lblError.Text = Resources.labels.editebasysvarsuccessfully;
                            Panel1.Enabled = false;
                            btsave.Visible = false;
                        }
                        else
                        {
                            lblError.Text = IPCERRORDESC;
                        }
                    }
                    else
                    {
                        lblError.Text = Resources.labels.accessdenied;
                    }
                }
                catch (Exception ex)
                {
                    SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "SEMSSYSVAR", "btnAddNew_Click_EDIT", ex.ToString(), Request.Url.Query);
                    SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
                }
                break;
            default:
                break;
        }
    }

    private bool validate(string value)
    {
        if (!String.IsNullOrEmpty(value))
        {
            return true;
        }
        return false;

    }
    protected void changeType(object sender, EventArgs e)
    {
        try
        {
            var type = ddlType.SelectedValue.ToString().Trim();
            int checktype = 0;
            DateTime date = DateTime.Now;
            switch (ddlType.SelectedValue)
            {
                case "String":
                case "Int":
                    txtValueCode.Visible = true;
                    txtEffectiveDate.Visible = false;
                    ddlReference.Visible = false;
                    break;
                case "Combobox":
                    txtValueCode.Visible = true;
                    txtEffectiveDate.Visible = false;
                    ddlReference.Visible = false;
                    break;
                case "Datetime":
                    txtValueCode.Visible = false;
                    txtEffectiveDate.Visible = true;
                    ddlReference.Visible = false;
                    break;
                default:
                    txtValueCode.Visible = true;
                    txtEffectiveDate.Visible = false;
                    ddlReference.Visible = false;
                    break;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "SEMSSYSVAR", "changeType", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    /// <summary>
    /// set value for combobox
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void changevalue(object sender, EventArgs e)
    {
        try
        {
            vlcombobox.Text = ddlReference.SelectedValue.ToString();
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "SEMSSYSVAR", "changevalue", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    public static bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return true;
        }
        catch
        {
            return false;
        }
    }
}