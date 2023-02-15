using SmartPortal.BLL;
using SmartPortal.Common.Utilities;
using SmartPortal.Constant;
using SmartPortal.ExceptionCollection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Widgets_SEMSPrefix_Controls_Widget : WidgetBase
{

    string ACTION = "";
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    string Pagemain = "1111";

    protected void Page_Load(object sender, EventArgs e)
    {
        ACTION = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["a"].ToString().Trim();
        if (!IsPostBack)
        {
            DataSet dsa = new SmartPortal.SEMS.Topup().GetSupplierList(ref IPCERRORCODE, ref IPCERRORDESC);
            ddlSupplierID.DataTextField = "SMSSUPPLIERID";
            ddlSupplierID.DataValueField = "SMSSUPPLIERID";
            ddlSupplierID.DataSource = dsa;
            ddlSupplierID.DataBind();
            DataSet ds = new SmartPortal.SEMS.Topup().GetTecoList(ref IPCERRORCODE, ref IPCERRORDESC);
            ddltelcoid.DataTextField = "TelcoName";
            ddltelcoid.DataValueField = "TelcoID";
            ddltelcoid.DataSource = ds;
            ddltelcoid.SelectedValue = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["b"].ToString();
            ddltelcoid.DataBind();
            DataSet dsGroup = new SmartPortal.SEMS.Topup().GetAllGroupId(ref IPCERRORCODE, ref IPCERRORDESC);
            ddlGroupid.DataTextField = "GROUPNAME";
            ddlGroupid.DataValueField = "GROUPID";
            ddlGroupid.DataSource = dsGroup;
            ddlGroupid.DataBind();
            BindData();
        }
    }

    private void BindData()
    {
        try
        {
            DataSet ds = new DataSet();
            DataTable custTable = new DataTable();
            switch (ACTION)
            {
                case IPC.ACTIONPAGE.DETAILS:
                    lbltitle.Text = "Prefix Information";
                    ddlSupplierID.Enabled = false;
                    txtPrefix.Enabled = false;
                    txtCountryPrefix.Enabled = false;
                    txtCountryName.Enabled = false;
                    ddlGroupid.Enabled = false;
                    ddltelcoid.Enabled = false;
                    txtphonelen.Enabled = false;
                    btsave.Visible = false;
                    btnClear.Visible = false;
     
                    ds = new SmartPortal.SEMS.User().ViewPrefix(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["ID"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE != "0")
                    {
                        throw new IPCException(IPCERRORDESC);
                    }
                    else
                    {
                        custTable = ds.Tables[0];
                    }
                    if (custTable.Rows.Count != 0)
                    {
                        ddlSupplierID.Text = custTable.Rows[0]["SUPPLIERID"].ToString();
                        txtPrefix.Text = custTable.Rows[0]["PREFIX"].ToString();
                        txtCountryPrefix.Text = custTable.Rows[0]["COUNTRYPREFIX"].ToString();
                        txtCountryName.SelectedValue = custTable.Rows[0]["COUNTRYNAME"].ToString();
                        ddlGroupid.SelectedValue= custTable.Rows[0]["GROUPID"].ToString();
                        ddltelcoid.Text = custTable.Rows[0]["TelcoID"].ToString();
                        txtphonelen.SelectedValue = custTable.Rows[0]["PhoneLen"].ToString();
                    }
                    break;
                case "ADD":
                    lbltitle.Text = "Add new Prefix";
                    ddltelcoid.Enabled = false;
                    break;
                case "EDIT":
                    txtPrefix.Enabled = true;
                    ddltelcoid.Enabled = false;
                    btnClear.Visible = false;
                    lbltitle.Text = "Edit Prefix";
                    ds = new SmartPortal.SEMS.User().ViewPrefix(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["ID"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE != "0")
                    {
                        throw new IPCException(IPCERRORDESC);
                    }
                    else
                    {
                        custTable = ds.Tables[0];
                    }

                    if (custTable.Rows.Count != 0)
                    {
                        ddlSupplierID.Text = custTable.Rows[0]["SUPPLIERID"].ToString();
                        txtPrefix.Text = custTable.Rows[0]["PREFIX"].ToString();
                        txtCountryPrefix.Text = custTable.Rows[0]["COUNTRYPREFIX"].ToString();
                        txtCountryName.SelectedValue = custTable.Rows[0]["COUNTRYNAME"].ToString();
                        ddlGroupid.SelectedValue = custTable.Rows[0]["GROUPID"].ToString();
                        ddltelcoid.Text = custTable.Rows[0]["TelcoID"].ToString();
                        txtphonelen.SelectedValue = custTable.Rows[0]["PhoneLen"].ToString();
                    }

                    break;

            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSPrefix_Widget", "gvPrefix_RowDataBound", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = new DataSet();

            switch (ACTION)
            {
                case "ADD":

                    if (txtPrefix.Text == "")
                    {
                        lblError.Text = "Prefix can not be blank";
                        txtPrefix.Focus();
                        break;
                    }
                    else if (txtCountryPrefix.Text == "")
                    {
                        lblError.Text = "Country Prefix can not be blank";
                        txtCountryPrefix.Focus();
                        break;
                    }
                    else
                    {
                        ds = new SmartPortal.SEMS.User().AddPrefix(ddlSupplierID.SelectedValue,
                        Utility.KillSqlInjection(txtPrefix.Text.Trim()),
                        Utility.KillSqlInjection(txtCountryPrefix.Text.Trim()), Utility.KillSqlInjection(txtCountryName.Text.Trim()),
                        Utility.KillSqlInjection(ddlGroupid.SelectedValue.Trim()), Utility.KillSqlInjection(ddltelcoid.Text.Trim()),
                        Utility.KillSqlInjection(txtphonelen.SelectedValue.Trim()), ref IPCERRORCODE, ref IPCERRORDESC);
                        if (IPCERRORDESC == "A")
                        {
                            ddlSupplierID.Enabled = false;
                            txtPrefix.Enabled = false;
                            ddltelcoid.Enabled = false;
                            txtCountryPrefix.Enabled = false;
                            txtCountryName.Enabled = false;
                            ddlGroupid.Enabled = false;
                            txtphonelen.Enabled = false;
                            lblError.Text = Resources.labels.thanhcong;
                            btsave.Enabled = false;
                            break;
                        }
                        else
                        {
                            lblError.Text = "Prefix is already exist";
                            break;
                        }
                    }
                case "EDIT":
                    if (Utility.KillSqlInjection(txtPrefix.Text.Trim()) == SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["ID"].ToString())
                    {
                        if (txtPrefix.Text == "")
                        {
                            lblError.Text = "Prefix can not be blank";
                            txtPrefix.Focus();
                            break;
                        }
                        else if (txtCountryPrefix.Text == "")
                        {
                            lblError.Text = "Country Prefix can not be blank";
                            txtCountryPrefix.Focus();
                            break;
                        }
                        ds = new SmartPortal.SEMS.User().EditPrefix(Utility.KillSqlInjection(txtPrefix.Text.Trim()),
                        Utility.KillSqlInjection(ddlSupplierID.Text.Trim()),
                        Utility.KillSqlInjection(txtCountryPrefix.Text.Trim()), Utility.KillSqlInjection(txtCountryName.Text.Trim()),
                        Utility.KillSqlInjection(ddlGroupid.SelectedValue.Trim()), Utility.KillSqlInjection(ddltelcoid.Text.Trim()),
                        Utility.KillSqlInjection(txtphonelen.SelectedValue.Trim()), ref IPCERRORCODE, ref IPCERRORDESC);
                        if (IPCERRORCODE == "0")
                        {
                            lblError.Text = Resources.labels.thanhcong;
                            btsave.Enabled = false;
                            ddlSupplierID.Enabled = false;
                            txtCountryPrefix.Enabled = false;
                            txtCountryName.Enabled = false;
                            ddlGroupid.Enabled = false;
                            txtPrefix.Enabled = false;
                            ddltelcoid.Enabled = false;
                            txtphonelen.Enabled = false;
                            btnClear.Visible = false;
                            break;
                        }
                        else
                        {
                            lblError.Text = "Prefix is already exist";
                            break;
                        }
                  
                    }
                    else
                    {
                        if (txtPrefix.Text == "")
                        {
                            lblError.Text = "Prefix can not be blank";
                            txtPrefix.Focus();
                            break;
                        }
                        else if (txtCountryPrefix.Text == "")
                        {
                            lblError.Text = "Country Prefix can not be blank";
                            txtCountryPrefix.Focus();
                            break;
                        }
                        //else if (txtgroupid.Text == "")
                        //{
                        //    lblError.Text = "Group ID can not be blank";
                        //    txtgroupid.Focus();
                        //    break;
                        //}
                        else
                        {
                            ds = new SmartPortal.SEMS.User().AddPrefix(ddlSupplierID.SelectedValue,
                        Utility.KillSqlInjection(txtPrefix.Text.Trim()),
                        Utility.KillSqlInjection(txtCountryPrefix.Text.Trim()), Utility.KillSqlInjection(txtCountryName.SelectedValue.Trim()),
                        Utility.KillSqlInjection(ddlGroupid.SelectedValue.Trim()), Utility.KillSqlInjection(ddltelcoid.Text.Trim()),
                        Utility.KillSqlInjection(txtphonelen.SelectedValue.Trim()), ref IPCERRORCODE, ref IPCERRORDESC);
                            DataSet dsa = new DataSet();

                            dsa = new SmartPortal.SEMS.User().DeletePrefix(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["ID"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
                            if (IPCERRORDESC == "A")
                            {
                                lblError.Text = Resources.labels.thanhcong;
                                btsave.Visible = false;
                                ddlSupplierID.Enabled = false;
                                txtPrefix.Enabled = false;
                                txtCountryPrefix.Enabled = false;
                                txtCountryName.Enabled = false;
                                ddlGroupid.Enabled = false;
                                ddltelcoid.Enabled = false;
                                txtphonelen.Enabled = false;
                                btnClear.Visible = false;
                                break;
                            }
                            else
                            {
                                lblError.Text = "Prefix is already exist";
                                break;
                            }
                        }
                    }
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSPrefix_Widget", "gvPrefix_RowDataBound", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }

    }
    protected void btback_OnClick(object sender, EventArgs e)
    {
        try
        {
            var urlb = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["b"] != null ? SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["b"].ToString() : "";
            if (urlb != "")
            {
                string param = "&a=LIST&cid=" + urlb;
                string pageid = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["p"].ToString();
                string link = PagesBLL.GetLinkMaster_Page(pageid) + param;
                Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL(link), false);
            }
            else
            {
                string link = PagesBLL.GetLinkMaster_Page("1108");
                Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL(link));
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSPrefix_Widget", "gvPrefix_RowDataBound", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ddlSupplierID.Enabled = true;
        txtPrefix.Text = string.Empty;
        txtCountryPrefix.Text = string.Empty;
        lblError.Text = string.Empty;
        txtPrefix.Enabled = true;
        txtCountryPrefix.Enabled = true;
        txtCountryName.Enabled = true;
        ddlGroupid.Enabled = true;
        txtphonelen.Enabled = true;
        lblError.Enabled = true;
        btsave.Enabled = true;
    }

}