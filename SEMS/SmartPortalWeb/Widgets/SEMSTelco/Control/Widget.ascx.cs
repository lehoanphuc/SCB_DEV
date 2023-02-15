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

using SmartPortal.Common.Utilities;
using SmartPortal.Constant;
using SmartPortal.ExceptionCollection;

public partial class Widgets_SEMSTelco_Control_Widget : WidgetBase
{

    string ACTION = "";
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    string PageEdit = "1109";
    string PageBack = "1108";
    protected void Page_Load(object sender, EventArgs e)
    {
        ACTION = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["a"].ToString().Trim();
        lblcardamountpn.Attributes.Add("onkeyup", "executeComma2('" + lblcardamountpn.ClientID + "',event)");
        lblrealmoneypn.Attributes.Add("onkeyup", "executeComma2('" + lblrealmoneypn.ClientID + "',event)");

        if (!IsPostBack)
        {
            GridViewPaging.Visible = false;
            BindData();
        }
        GridViewPaging.pagingClickArgs += new EventHandler(GridViewPaging_Click);
    }

    private void GridViewPaging_Click(object sender, EventArgs e)
    {
        gvPrefix.PageSize = Convert.ToInt32(((DropDownList)GridViewPaging.FindControl("PageRowSize")).SelectedValue);
        gvPrefix.PageIndex = Convert.ToInt32(((TextBox)GridViewPaging.FindControl("SelectedPageNo")).Text) - 1;
        BindData();
    }
    #region binddata
    void BindData()
    {
        try
        {
            DataSet ds = new DataSet();
            DataTable custTable = new DataTable();
            switch (ACTION)
            {
                case "viewdetail":
                case "DETAILS":
                    btnsavetelco.Visible = false;
                    btnback.Visible = false;
                    lblTelecom.Enabled = false;
                    lblShortName.Enabled = false;
                    lblEloadTeco.Enabled = false;
                    lblEpinTeco.Enabled = false;
                    lblGLAccBalance.Enabled = false;
                    lblGLAccFee.Enabled = false;
                    lblWlFee.Enabled = false;
                    lblWlBalance.Enabled = false;
                    lblEloadBillerCode.Enabled = false;
                    lblEPinBillerCode.Enabled = false;
                    btnClear.Visible = false;
                    pnSearch.Visible = false;
                    divButtonbar.Visible = false;
                    divaddnew.Visible = false;
                    divSearchCardID.Visible = false;
                    lblstatustelco.Enabled = false;
                    lbltitle.Text = "View Detail Telco";
                    Button2.Visible = false;
                    btsave.Visible = false;
                    var cardid = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["ID"].ToString();
                    ds = new SmartPortal.SEMS.User().GetalltelcoDetail(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["ID"].ToString(), "ALL", "ALL", "ALL", 0, 0, ref IPCERRORCODE, ref IPCERRORDESC);
                    DataSet dsa = new DataSet();
                    dsa = new SmartPortal.SEMS.User().GetAllPrefix("ALL", "", "", "",
                        SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["ID"].ToString(), gvPrefix.PageSize, gvPrefix.PageIndex * gvPrefix.PageSize,
                        ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE != "0")
                    {
                        throw new IPCException(IPCERRORDESC);
                    }
                    else
                    {
                        custTable = ds.Tables[1];
                    }
                    if (custTable.Rows.Count != 0)
                    {
                        lblTelecom.Text = custTable.Rows[0]["TelcoName"].ToString();
                        lblShortName.Text = custTable.Rows[0]["ShortName"].ToString();
                        lblstatustelco.Text = custTable.Rows[0]["Statustel"].ToString();
                        lblEloadBillerCode.Text = custTable.Rows[0]["EloadBillerCode"].ToString();
                        lblEPinBillerCode.Text = custTable.Rows[0]["EPinBillerCode"].ToString();
                        lblEloadTeco.SelectedValue = custTable.Rows[0]["ELoad"].ToString();
                        lblEpinTeco.SelectedValue = custTable.Rows[0]["EPin"].ToString();
                        lblGLAccBalance.Text = custTable.Rows[0]["SUNDRYACCTNOBANK"].ToString();
                        lblGLAccFee.Text = custTable.Rows[0]["INCOMEACCTNOBANK"].ToString();
                        lblWlBalance.Text = custTable.Rows[0]["SUNDRYACCTNOWALLET"].ToString();
                        lblWlFee.Text = custTable.Rows[0]["INCOMEACCTNOWALLET"].ToString();

                    }
                    gvPrefix.DataSource = dsa;
                    gvPrefix.DataBind();
                    if (dsa.Tables[0].Rows.Count > 0)
                    {
                        GridViewPaging.Visible = true;
                        ((HiddenField)GridViewPaging.FindControl("TotalRows")).Value = dsa.Tables[0].Rows[0]["TRECORDCOUNT"].ToString();
                    }
                    else
                    {
                        GridViewPaging.Visible = false;
                    }

                    break;
                case "addnew":
                case "ADD":
                    lbltitle.Text = "Add New Telecom";
                    pnCard.Visible = false;
                    btsave.Visible = false;
                    pnSearch.Visible = false;
                    divaddnew.Visible = false;
                    divSearchCardID.Visible = false;
                    divButtonbar.Visible = false;
                    divTable.Visible = false;
               
                    Button8.Visible = false;
                    lblstatustelco.Enabled = false;
                    pnCard.Visible = false;
                    break;
                case "edit":
                case "EDIT":
                    lblTelecom.Enabled = false;
                    lblShortName.Enabled = false;
                    lbltitle.Text = "Edit Telecom";
                    divaddnew.Visible = false;
                    divSearchCardID.Visible = false;
                    divButtonbar.Visible = false;
                    divTable.Visible = false;
                    lblGLAccBalance.Enabled = false;
                    lblGLAccFee.Enabled = false;
                    lblWlBalance.Enabled = false;
                    lblWlFee.Enabled = false;
                    pnCard.Visible = false;
                    btsave.Visible = false;
                    btnClear.Enabled = false;
                    Button8.Visible = false;
                    ds = new SmartPortal.SEMS.User().GetalltelcoDetail(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cid"].ToString(), Utility.KillSqlInjection(txtShortNameSearch.Text.Trim()), Utility.KillSqlInjection(txtCardamountSearch.Text.Trim()), ddlStatusSearch.SelectedValue, 0, 0, ref IPCERRORCODE, ref IPCERRORDESC);

                    if (IPCERRORCODE != "0")
                    {
                        throw new IPCException(IPCERRORDESC);
                    }
                    else
                    {
                        custTable = ds.Tables[1];
                    }
                    if (custTable.Rows.Count != 0)
                    {
                        lblTelecom.Text = custTable.Rows[0]["TelcoName"].ToString();
                        lblShortName.Text = custTable.Rows[0]["ShortName"].ToString();
                        lblEloadBillerCode.Text = custTable.Rows[0]["EloadBillerCode"].ToString();
                        lblEPinBillerCode.Text = custTable.Rows[0]["EPinBillerCode"].ToString();
                        lblstatustelco.Text = custTable.Rows[0]["Statustel"].ToString();
                        lblEloadTeco.SelectedValue = custTable.Rows[0]["ELoad"].ToString();
                        lblEpinTeco.SelectedValue = custTable.Rows[0]["EPin"].ToString();
                        lblGLAccBalance.Text = custTable.Rows[0]["SUNDRYACCTNOBANK"].ToString();
                        lblGLAccFee.Text = custTable.Rows[0]["INCOMEACCTNOBANK"].ToString();
                        lblWlBalance.Text = custTable.Rows[0]["SUNDRYACCTNOWALLET"].ToString();
                        lblWlFee.Text = custTable.Rows[0]["INCOMEACCTNOWALLET"].ToString();
                    }
                    break;
            }
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], "Widgets_SEMSTelco_Control_Widget", "BindData", IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSTelco_Control_Widget", "BindData", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    #endregion

    #region gvPrefix
    protected void gvPrefix_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            Label lblprefix;
            Label lblcountryprefix;
            Label lblgroupid;
            Label lblphoenlen;
            Label lbltelconame;
            //Label lblSupplierid;


            DataRowView drv;

            var ddlGroupid = new DropDownList();
            DataSet dsGroup = new SmartPortal.SEMS.Topup().GetAllGroupId(ref IPCERRORCODE, ref IPCERRORDESC);
            ddlGroupid.DataTextField = "GROUPNAME";
            ddlGroupid.DataValueField = "GROUPID";
            ddlGroupid.DataSource = dsGroup;
            ddlGroupid.DataBind();

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                drv = (DataRowView)e.Row.DataItem;

                lblprefix = (Label)e.Row.FindControl("lblprefix");
                //lblSupplierid = (Label)e.Row.FindControl("lblSupplierid");
                lblcountryprefix = (Label)e.Row.FindControl("lblcountryprefix");
                lblgroupid = (Label)e.Row.FindControl("lblgroupid");
                lbltelconame = (Label)e.Row.FindControl("lbltelconame");
                lblphoenlen = (Label)e.Row.FindControl("lblphoenlen");
                //lblSupplierid.Text = drv["SUPPLIERID"].ToString();
                lblprefix.Text = drv["PREFIX"].ToString();
                lbltelconame.Text = drv["TelcoName"].ToString();
                lblcountryprefix.Text = drv["COUNTRYPREFIX"].ToString();
                lblgroupid.Text = ddlGroupid.Items.FindByValue(drv["GROUPID"].ToString()) == null ? ddlGroupid.Items[0].Text : ddlGroupid.Items.FindByValue(drv["GROUPID"].ToString()).ToString();
                lblphoenlen.Text = drv["PhoneLen"].ToString();

            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSPrefix_Widget", "gvPrefix_RowDataBound", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    }


    #endregion gvprefix

    #region button back
    protected void Button8_Click(object sender, EventArgs e)
    {
        if (ACTION.Equals("addcard"))
            Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=" + PageEdit + "&a=edit&cid=" + SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cid"].ToString()));
        if (ACTION.Equals("editcard"))
            Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=" + PageEdit + "&a=edit&cid=" + SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["b"].ToString()));
        else
            Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=" + PageBack));
    }
    #endregion

    #region buttonsave and button delete
    protected void btsave_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();

        switch (ACTION)
        {
            case "addnew":
            case "ADD":
                lblError.Text = "";
                if (lblTelecom.Text == "")
                {
                    lblError.Text = "Telecom Name can not be blank";
                    lblTelecom.Focus();
                    break;
                }
                else if (lblShortName.Text == "")
                {
                    lblError.Text = "Telecom Short Name can not be blank";
                    lblShortName.Focus();
                    break;
                }
                else
                {
                    ds = new SmartPortal.SEMS.User().Createtelco(Utility.KillSqlInjection(lblTelecom.Text.Trim()), Utility.KillSqlInjection(lblShortName.Text.Trim()), Utility.KillSqlInjection(lblEloadBillerCode.Text.Trim()), Utility.KillSqlInjection(lblEPinBillerCode.Text.Trim()), "A", lblEloadTeco.Text, lblEpinTeco.Text,
                        Utility.KillSqlInjection(lblGLAccBalance.Text.Trim()),
                         Utility.KillSqlInjection(lblGLAccFee.Text.Trim()),
                          Utility.KillSqlInjection(lblWlBalance.Text.Trim()),
                           Utility.KillSqlInjection(lblWlFee.Text.Trim()),
                       ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORDESC == "A")
                    {
                        lblError.Text = "Add telecom information successful";
                        lblTelecom.Enabled = false;
                        lblShortName.Enabled = false;
                        lblEloadBillerCode.Enabled = false;
                        lblEPinBillerCode.Enabled = false;
                        lblEloadTeco.Enabled = false;
                        lblEpinTeco.Enabled = false;
                        lblGLAccBalance.Enabled = false;
                        lblGLAccFee.Enabled = false;
                        lblWlFee.Enabled = false;
                        lblWlBalance.Enabled = false;
                        btnsavetelco.Enabled = false;
                        break;
                    }
                    else
                    {
                        lblError.Text = "Telecom is already exist";
                        lblTelecom.Enabled = true;
                        lblShortName.Enabled = true;
                        lblEloadBillerCode.Enabled = true;
                        lblEPinBillerCode.Enabled = true;
                        lblEloadTeco.Enabled = true;
                        btnsavetelco.Enabled = true;
                        lblEpinTeco.Enabled = true;
                        lblGLAccBalance.Enabled = true;
                        lblGLAccFee.Enabled = true;
                        lblWlFee.Enabled = true;
                        lblWlBalance.Enabled = true;
                        break;
                    }
                }
            case "EDIT":

                ds = new SmartPortal.SEMS.User().Updatetelco(Utility.KillSqlInjection(lblShortName.Text.Trim()), Utility.KillSqlInjection(lblTelecom.Text.Trim()),
               Utility.KillSqlInjection(lblEloadBillerCode.Text.Trim()), Utility.KillSqlInjection(lblEPinBillerCode.Text.Trim()),
           lblEloadTeco.SelectedValue, lblEpinTeco.SelectedValue, lblstatustelco.SelectedValue,
               Utility.KillSqlInjection(lblGLAccBalance.Text.Trim()),
                         Utility.KillSqlInjection(lblGLAccFee.Text.Trim()),
                          Utility.KillSqlInjection(lblWlBalance.Text.Trim()),
                           Utility.KillSqlInjection(lblWlFee.Text.Trim())
               , ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE != "0") lblError.Text = "Edit telecom information error";
                else
                {
                    lblTelecom.Enabled = false;
                    lblShortName.Enabled = false;
                    lblEloadBillerCode.Enabled = false;
                    lblEPinBillerCode.Enabled = false;
                    btsave.Visible = false;
                    lblstatustelco.Enabled = false;
                    btnsavetelco.Visible = false;
                    lblEloadTeco.Enabled = false;
                    lblEpinTeco.Enabled = false;
                    lblGLAccBalance.Enabled = false;
                    lblGLAccFee.Enabled = false;
                    lblWlFee.Enabled = false;
                    lblWlBalance.Enabled = false;
                    lblError.Text = "Edit telecom information successful ";
                }
                BindData();
                break;
        }
    }


    private bool IsSuccess()
    {
        return IPCERRORCODE.Equals("0") ? true : false;
    }

    private void ShowError(string msg = "")
    {
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ShowError", "ShowError('" + msg + "');", true);
    }

    protected void btnSearchCard_Click(object sender, EventArgs e)
    {
        try
        {
            BindData();
        }
        catch (Exception ex)
        {

        }
    }

    #endregion

    #region button addcard
    protected void Button2_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = new DataSet();
            DataTable custTable = new DataTable();
            ds = new SmartPortal.SEMS.User().GetalltelcoDetail(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cid"].ToString(), "ALL", "ALL", "ALL", 0, 0, ref IPCERRORCODE, ref IPCERRORDESC);

            if (IPCERRORCODE != "0")
            {
                throw new IPCException(IPCERRORDESC);
            }
            else
            {
                custTable = ds.Tables[1];
            }

            Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=" + PageEdit + "&a=addcard&cid=" + custTable.Rows[0]["TelcoID"].ToString()));
        }
        catch (Exception ex)
        {

        }

    }
    #endregion
    protected void btnClear_Click(object sender, EventArgs e)
    {
        lblError.Text = string.Empty;
        lblTelecom.Text = string.Empty;
        lblShortName.Text = string.Empty;
        lblEPinBillerCode.Text = string.Empty;
        lblEloadBillerCode.Text = string.Empty;
        lblWlFee.Text = string.Empty;
        lblWlBalance.Text = string.Empty;
        lblGLAccFee.Text = string.Empty;
        lblGLAccBalance.Text = string.Empty;
        lblGLAccBalance.Enabled = true;
        lblGLAccFee.Enabled = true;
        lblWlFee.Enabled = true;
        lblWlBalance.Enabled = true;
        lblEloadTeco.Enabled = true;
        lblEpinTeco.Enabled = true;
        lblTelecom.Enabled = true;
        lblShortName.Enabled = true;
        lblEloadBillerCode.Enabled = true;
        lblEPinBillerCode.Enabled = true;
        btnsavetelco.Enabled = true;
    }
}