using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using SmartPortal.Common;
using SmartPortal.Common.Utilities;
using SmartPortal.ExceptionCollection;
using System.Globalization;
using SmartPortal.BLL;
using SmartPortal.Constant;
using SmartPortal.DAL;
using SmartPortal.Model;
using SmartPortal.SEMS;

public partial class Widgets_SEMSSwiftFeeManagement_Widget : WidgetBase
{
    private int size = 0;
    public static bool isAscend = false;
    string ACTION = string.Empty;
    string IPCERRORCODE = string.Empty;
    string IPCERRORDESC = string.Empty;
    public static string APPTRANID;
    public static int Fuid = 1;

    //check validate flag false cho nhap so am, true khong nhap so am
    public bool allowNegativeFee = true;
    DataTable ProDeTable = new DataTable();
    double reFromLimit, reToLimit, roToLimit, roFromLimit;
    string roFeePayer, reFeePayer, roBankID, reBankID, roCCYID, reCCYID, reFeeType, roFeeType;
    bool IsLadder, roIsLadder;
 

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = string.Empty;
            txtFromSW.Attributes.Add("onkeypress", "executeComma('" + txtFromSW.ClientID + "')");
            txtFromOTHB.Attributes.Add("onkeypress", "executeComma('" + txtFromOTHB.ClientID + "')");
            txtToSW.Attributes.Add("onkeypress", "executeComma('" + txtToSW.ClientID + "')");
            txtToOTHB.Attributes.Add("onkeypress", "executeComma('" + txtToOTHB.ClientID + "')");
            if (allowNegativeFee)
            {
                txtAmountSW.Attributes.Add("onkeypress", "executeComma('" + txtAmountSW.ClientID + "')");
                txtMinSW.Attributes.Add("onkeypress", "executeComma('" + txtMinSW.ClientID + "')");
                txtMaxSW.Attributes.Add("onkeypress", "executeComma('" + txtMaxSW.ClientID + "')");
                txtFromSW.Attributes.Add("onkeyup", "executeComma('" + txtFromSW.ClientID + "')");
                txtToSW.Attributes.Add("onkeyup", "executeComma('" + txtToSW.ClientID + "')");
                txtAmountSW.Attributes.Add("onkeyup", "executeComma('" + txtAmountSW.ClientID + "')");
                txtMinSW.Attributes.Add("onkeyup", "executeComma('" + txtMinSW.ClientID + "')");
                txtMaxSW.Attributes.Add("onkeyup", "executeComma('" + txtMaxSW.ClientID + "')");

                txtAmountOTHB.Attributes.Add("onkeypress", "executeComma('" + txtAmountOTHB.ClientID + "')");
                txtMinOTHB.Attributes.Add("onkeypress", "executeComma('" + txtMinOTHB.ClientID + "')");
                txtMaxOTHB.Attributes.Add("onkeypress", "executeComma('" + txtMaxOTHB.ClientID + "')");
                txtFromOTHB.Attributes.Add("onkeyup", "executeComma('" + txtFromOTHB.ClientID + "')");
                txtToOTHB.Attributes.Add("onkeyup", "executeComma('" + txtToOTHB.ClientID + "')");
                txtAmountOTHB.Attributes.Add("onkeyup", "executeComma('" + txtAmountOTHB.ClientID + "')");
                txtMinOTHB.Attributes.Add("onkeyup", "executeComma('" + txtMinOTHB.ClientID + "')");
                txtMaxOTHB.Attributes.Add("onkeyup", "executeComma('" + txtMaxOTHB.ClientID + "')");

            }
            else
            {
                txtAmountSW.Attributes.Add("onkeypress", "return isNumberK(event)");
                txtMinSW.Attributes.Add("onkeypress", "return isNumberK(event)");
                txtMaxSW.Attributes.Add("onkeypress", "return isNumberK(event)");
                txtAmountSW.Attributes.Add("onkeyup", "return isNumberK(event)");
                txtMinSW.Attributes.Add("onkeyup", "return isNumberK(event)");
                txtMaxSW.Attributes.Add("onkeyup", "return isNumberK(event)");
                txtFromSW.Attributes.Add("onkeyup", "executeComma('" + txtFromSW.ClientID + "')");
                txtToSW.Attributes.Add("onkeyup", "executeComma('" + txtToSW.ClientID + "')");

                txtAmountOTHB.Attributes.Add("onkeypress", "return isNumberK(event)");
                txtMinOTHB.Attributes.Add("onkeypress", "return isNumberK(event)");
                txtMaxOTHB.Attributes.Add("onkeypress", "return isNumberK(event)");
                txtAmountOTHB.Attributes.Add("onkeyup", "return isNumberK(event)");
                txtMinOTHB.Attributes.Add("onkeyup", "return isNumberK(event)");
                txtMaxOTHB.Attributes.Add("onkeyup", "return isNumberK(event)");
                txtFromOTHB.Attributes.Add("onkeyup", "executeComma('" + txtFromOTHB.ClientID + "')");
                txtToOTHB.Attributes.Add("onkeyup", "executeComma('" + txtToOTHB.ClientID + "')");
            }
            if (!IsPostBack)
            {
                ViewState["FEEDETAILS"] = null;

                ddlCCYID.DataSource = new SmartPortal.SEMS.Product().LoaddAllCCYID(ref IPCERRORCODE, ref IPCERRORDESC);
                ddlCCYID.DataTextField = "CCYID";
                ddlCCYID.DataValueField = "CCYID";
                ddlCCYID.DataBind();
                ddlCCYID.Items.Remove("LAK");

                DataSet dtBank = new SmartPortal.SEMS.Partner().GetCBBankALL(ddlCCYID.SelectedValue.Trim(), ref IPCERRORCODE, ref IPCERRORDESC);
                ddlBank.DataSource = dtBank;
                ddlBank.DataTextField = "BankName";
                ddlBank.DataValueField = "BankID";
                ddlBank.DataBind();
                ddlBank.Items.Insert(0, new ListItem(Resources.labels.allforbank, ""));



                ddlBankSWCode.DataSource = dtBank;
                ddlBankSWCode.DataTextField = "SwiftCode";
                ddlBankSWCode.DataValueField = "BankID";
                ddlBankSWCode.DataBind();
                 

                BindData();

            }
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(IPCex.ToString(), this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(IPCex.Message, Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    void BindData()
    {
        DataTable tblRoleDefault = new DataTable();
        try
        {
            DataSet dsfee = new SmartPortal.SEMS.InterBank().LoadSwiftFee(ref IPCERRORCODE, ref IPCERRORDESC);
            if (dsfee.Tables.Count != 0)
            {
                ProDeTable = dsfee.Tables[0];
                if (ProDeTable.Rows.Count != 0)
                {
                    //bind data to panel


                    DataTable tblTransDetailsEdit = new DataTable();
                    DataTable tblTempt = new DataTable();
                    DataColumn Feetype = new DataColumn("FEETYPE");
                    DataColumn From = new DataColumn("FROMLIMIT");
                    From.DataType = typeof(decimal);
                    DataColumn To = new DataColumn("TOLIMIT");
                    To.DataType = typeof(decimal);
                    DataColumn Min = new DataColumn("MINAMOUNT");
                    Min.DataType = typeof(decimal);
                    DataColumn Max = new DataColumn("MAXAMOUNT");
                    Max.DataType = typeof(decimal);
                    DataColumn Rate = new DataColumn("RATE");
                    DataColumn FixAmount = new DataColumn("FIXAMOUNT");
                    FixAmount.DataType = typeof(decimal);
                    DataColumn IsLadder = new DataColumn("ISLADDER");
                    DataColumn Feepayer = new DataColumn("FEEPAYER");
                    DataColumn Currency = new DataColumn("CCYID");
                    DataColumn BankID = new DataColumn("BANKID");
                    DataColumn FkID = new DataColumn("FkID");
                    DataColumn Swcode = new DataColumn("SWCODE");


                    tblTransDetailsEdit.Columns.AddRange(new DataColumn[] { Feetype, From, To, Min, Max, Rate, FkID, FixAmount, IsLadder, Feepayer, BankID, Currency, Swcode });
                    foreach (DataRow ro in ProDeTable.Rows)
                    {
                        DataRow r = tblTransDetailsEdit.NewRow();
                        if (ro["FROMLIMIT"] is DBNull && ro["TOLIMIT"] is DBNull && ro["MINAMOUNT"] is DBNull && ro["MAXAMOUNT"] is DBNull && ro["FIXAMOUNT"] is DBNull && ro["RATE"] is DBNull)
                        {
                            break;
                        }
                        r["FEETYPE"] = ro["FEETYPE"].ToString();
                        r["TOLIMIT"] = ro["TOLIMIT"].ToString();
                        r["FROMLIMIT"] = ro["FROMLIMIT"].ToString();
                        r["MINAMOUNT"] = ro["MINAMOUNT"].ToString();
                        r["MAXAMOUNT"] = ro["MAXAMOUNT"].ToString();
                        r["RATE"] = ro["RATE"].ToString();
                        r["FkID"] = SmartPortal.Common.Utilities.Utility.generateUniqueID();
                        r["ISLADDER"] = (bool)ro["ISLADDER"];
                        r["FIXAMOUNT"] = ro["FIXAMOUNT"].ToString();
                        r["FEEPAYER"] = ro["FEEPAYER"];
                        r["CCYID"] = ro["CCYID"].ToString();
                        r["BANKID"] = ro["BANKID"].ToString();
                        r["SWCODE"] = ro["SWCODE"].ToString();
                        tblTransDetailsEdit.Rows.Add(r);
                    }
                    if (tblTransDetailsEdit.Rows.Count > 0)
                    {
                        ViewState["FEEDETAILS"] = tblTransDetailsEdit;
                    }
                    else
                    {
                        ViewState["FEEDETAILS"] = null;
                    }
                    LoadGridView(null, EventArgs.Empty);
                }
            }
            else
            {
                lblError.Text = "<p class='divDataNotFound'>" + Resources.labels.datanotfound + "</p>";

            }
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(IPCex.ToString(), this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(IPCex.Message, Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void gvFeeSwiftDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvFeeSwiftDetails.PageIndex = e.NewPageIndex;
            DataTable tblTransDetails = (DataTable)ViewState["FEEDETAILS"];
            DataView dv = new DataView(tblTransDetails);
            gvFeeSwiftDetails.DataSource = dv;
            gvFeeSwiftDetails.DataBind();
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void btsave_Click(object sender, EventArgs e)
    {

        DataTable tblswiftfee = new DataTable();
        if (ViewState["FEEDETAILS"] != null)
        {
            tblswiftfee = (DataTable)ViewState["FEEDETAILS"];
            DataColumnCollection columns = tblswiftfee.Columns;
            if (columns.Contains("FkID"))
            {
                tblswiftfee.Columns.Remove("FkID");
            }
            if (columns.Contains("SWCODE"))
            {
                tblswiftfee.Columns.Remove("SWCODE");
            }
            new SmartPortal.SEMS.InterBank().InsertSwiftFee(tblswiftfee, ref IPCERRORCODE, ref IPCERRORDESC);

            if (IPCERRORCODE == "0")
            {
                lblError.Text = Resources.labels.themphigiaodichthanhcong;
                pnFeeDetails.Enabled = false;
                btsave.Enabled = false;
                if (ViewState["FEEDETAILS"] != null)
                {
                    LoadGridView(null, EventArgs.Empty);
                }
            }
            else
            {
                lblError.Text = IPCERRORDESC;
                return;
            }
        }
        else
        {
            lblError.Text = "Please add fee before save!";
            return;
        }      
    }
    protected void btback_Click(object sender, EventArgs e)
    {
        RedirectBackToMainPage();
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        lblError.Text = string.Empty;
        pnFee.Enabled = true;
        ViewState["FEEDETAILS"] = null;
        tbLadderFeeSwift.Visible = false;
        pnFeeDetails.Enabled = true;
        tbLadderFeeOtherbank.Visible = false;
        ddlCCYID.SelectedIndex = 0;
        txtAmountSW.Text = string.Empty;
        txtAmountOTHB.Text = string.Empty;
        txtAmountSW.Enabled = true;
        txtAmountOTHB.Enabled = true;
        cbIsLadderSW.Checked = false;
        cbIsLadderOTHB.Checked = false;
        txtFromSW.Text = "0";
        txtFromOTHB.Text = "0";
        txtToSW.Text = "0";
        txtToOTHB.Text = "0";
        txtMinSW.Text = "0";
        txtMinOTHB.Text = "0";
        txtMaxSW.Text = "0";
        txtMaxOTHB.Text = "0";
        txtRateSW.Text = "0";
        txtRateOTHB.Text = "0";
        ddlPayer.SelectedIndex = 0;
        ddlBank.SelectedIndex = 0;
        ddlFeetype.SelectedIndex = 0;
        btsave.Enabled = true;
        BindData();

    }
    protected void gvFeeSwiftDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            Label lblfeetype, lblfrom, lblto, lblmin, lblmax, lblRate, lblFkID, lblLadder, lblfixedfee, lblfeepayer, lblBankName, lblCurrency, lblswcode;
            LinkButton lbdelete;

            DataRowView drv;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                drv = (DataRowView)e.Row.DataItem;
                lblfeetype = (Label)e.Row.FindControl("lblFeeType");
                lblfrom = (Label)e.Row.FindControl("lblfrom");
                lblto = (Label)e.Row.FindControl("lblto");
                lblmin = (Label)e.Row.FindControl("lblmin");
                lblmax = (Label)e.Row.FindControl("lblmax");
                lblRate = (Label)e.Row.FindControl("lblRate");
                lblLadder = (Label)e.Row.FindControl("lblLadder");
                lblfixedfee = (Label)e.Row.FindControl("lblfixedfee");
                lblfeepayer = (Label)e.Row.FindControl("lblfeepayer");
                lblCurrency = (Label)e.Row.FindControl("lblCurrency");
                lblBankName = (Label)e.Row.FindControl("lblBankName");
                lblswcode = (Label)e.Row.FindControl("lblswcode");
                lbdelete = (LinkButton)e.Row.FindControl("lbdelete");
                lblFkID = (Label)e.Row.FindControl("lblFkID");



                lblfeepayer.Text = SmartPortal.Common.Utilities.Utility.KillSqlInjection(drv["FEEPAYER"].ToString());
                lblCurrency.Text = SmartPortal.Common.Utilities.Utility.KillSqlInjection(drv["CCYID"].ToString());
                lblfeetype.Text = SmartPortal.Common.Utilities.Utility.KillSqlInjection(drv["FEETYPE"].ToString());
                lblfrom.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(drv["FROMLIMIT"].ToString(), ddlCCYID.SelectedValue);
                lblto.Text = double.Parse(drv["TOLIMIT"].ToString()) == -1 ? Resources.labels.unlimit : SmartPortal.Common.Utilities.Utility.FormatMoney(drv["TOLIMIT"].ToString(), ddlCCYID.SelectedValue);
                lblmin.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(drv["MINAMOUNT"].ToString(), ddlCCYID.SelectedValue);
                lblmax.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(drv["MAXAMOUNT"].ToString(), ddlCCYID.SelectedValue);
                lblfixedfee.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(drv["FIXAMOUNT"].ToString(), ddlCCYID.SelectedValue);
                lblRate.Text = drv["RATE"].ToString();
                try
                {

                    lblFkID.Text = drv["FkID"].ToString();
                }
                catch (Exception exception)
                {
                    lblFkID.Text = "";
                }
                if (drv["ISLADDER"].ToString() == "True")
                {
                    lblLadder.Text = "<img src='widgets/SEMSSWIFTFeeManagement/Images/check.png' style='width: 20px; height: 20px; margin-bottom:1px;'/>";
                }
                if (drv["ISLADDER"].ToString() == "False")
                {
                    lblLadder.Text = "<img src='widgets/SEMSSWIFTFeeManagement/Images/nocheck.png'style='width: 20px; height: 20px; margin-bottom:1px;'/>";
                }

                try
                {
                    lblBankName.Text = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlBank.Items.FindByValue(drv["BANKID"].ToString()).Text);
                    lblswcode.Text = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlBankSWCode.Items.FindByValue(drv["BANKID"].ToString()).Text);  drv["SWCODE"].ToString();
                }
               
                                    
                catch (Exception exception)
                {
                    lblBankName.Text = string.Empty;
                }
                hdCounter.Value = "0";
                hdPageSize.Value = size.ToString();
            }
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(IPCex.ToString(), this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(IPCex.Message, Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    protected void btnSaveDetailsSW_Click(object sender, EventArgs e)
    {
        try
        {

            lblError.Text = string.Empty;
            DataTable tblTransDetails = new DataTable();


            if (cbIsLadderSW.Checked)
            {
                if (SmartPortal.Common.Utilities.Utility.isDouble(txtFromSW.Text, true) < 0)
                {
                    lblError.Text = Resources.labels.hanmuctinhphikhonghople;
                    return;
                }
                else if (!cbToLimitSW.Checked && SmartPortal.Common.Utilities.Utility.isDouble(txtToSW.Text, true) <= 0)
                {
                    lblError.Text = Resources.labels.hanmuctinhphikhonghople;
                    return;
                }
                else if (!cbToLimitSW.Checked && SmartPortal.Common.Utilities.Utility.isDouble(txtToSW.Text, true) <=
                         SmartPortal.Common.Utilities.Utility.isDouble(txtFromSW.Text, true))
                {
                    lblError.Text = Resources.labels.hanmuctinhphikhonghople;
                    return;
                }
                else if (SmartPortal.Common.Utilities.Utility.isDouble(txtMinSW.Text, true) < 0 && allowNegativeFee)
                {
                    lblError.Text = Resources.labels.hanmuctinhphikhonghople;
                    return;
                }
                else if (SmartPortal.Common.Utilities.Utility.isDouble(txtMaxSW.Text, true) <= 0 && allowNegativeFee)
                {
                    lblError.Text = Resources.labels.hanmuctinhphikhonghople;
                    return;
                }
                else if (SmartPortal.Common.Utilities.Utility.isDouble(txtMaxSW.Text, true) <
                         SmartPortal.Common.Utilities.Utility.isDouble(txtMinSW.Text, true))
                {
                    lblError.Text = Resources.labels.hanmuctinhphikhonghople;
                    return;
                }

                try
                {
                    if (SmartPortal.Common.Utilities.Utility.isDouble(txtRateSW.Text, true) < 0 && allowNegativeFee)
                    {
                        lblError.Text = Resources.labels.hanmuctinhphikhonghople;
                        return;
                    }
                    else if (SmartPortal.Common.Utilities.Utility.isDouble(txtRateSW.Text, true) > 100)
                    {
                        lblError.Text = Resources.labels.hanmuctinhphikhonghople;
                        return;
                    }
                }
                catch (Exception exception)
                {
                    lblError.Text = Resources.labels.hanmuctinhphikhonghople;
                    return;
                }
            }

            DataTable dt = new DataTable();
            dt.Columns.Add("BANKID", typeof(string));
            try
            {
                if (string.IsNullOrEmpty(ddlBank.SelectedValue))
                {
                    foreach (ListItem item in ddlBank.Items)
                    {
                        if (!string.IsNullOrEmpty(item.Value))
                            dt.Rows.Add(new object[] { item.Value });
                    }
                }
                else
                {
                    dt.Rows.Add(new object[] { ddlBank.SelectedValue.Trim() });
                }

            }
            catch (Exception exception)
            {
                if (ddlBank.Items.Count == 0)
                {
                    lblError.Text = Resources.labels.nothing;
                    return;
                }

            }
            if (dt.Rows.Count > 0)
            {

                // tạo table tạm 
                DataTable tblTempt = new DataTable();
                DataColumn Feetype = new DataColumn("FEETYPE");
                DataColumn From = new DataColumn("FROMLIMIT");
                From.DataType = typeof(decimal);
                DataColumn To = new DataColumn("TOLIMIT");
                To.DataType = typeof(decimal);
                DataColumn Min = new DataColumn("MINAMOUNT");
                Min.DataType = typeof(decimal);
                DataColumn Max = new DataColumn("MAXAMOUNT");
                Max.DataType = typeof(decimal);
                DataColumn Rate = new DataColumn("RATE");
                DataColumn FixAmount = new DataColumn("FIXAMOUNT");
                FixAmount.DataType = typeof(decimal);
                DataColumn isLadder = new DataColumn("ISLADDER");
                DataColumn Feepayer = new DataColumn("FEEPAYER");
                DataColumn Currency = new DataColumn("CCYID");
                DataColumn BankID = new DataColumn("BANKID");
                DataColumn SwiftcCode = new DataColumn("SWCODE");
                DataColumn FkID = new DataColumn("FkID");
                tblTempt.Columns.AddRange(new DataColumn[]
                {
                   Feetype, From, To, Min, Max, Rate, FkID, FixAmount, isLadder,
                    Feepayer ,BankID ,Currency ,SwiftcCode
                });
                if (ViewState["FEEDETAILS"] == null)
                {
                    foreach (DataRow dataRow in dt.Rows)
                    {
                        DataRow r = tblTempt.NewRow();

                        r["FEETYPE"] = SmartPortal.Common.Utilities.Utility.KillSqlInjection("SWIFTFEE");
                        r["TOLIMIT"] = cbToLimitSW.Checked
                            ? "-1"
                            : SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtToSW.Text.Trim(),
                                ddlCCYID.SelectedValue);
                        r["FROMLIMIT"] =
                            SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtFromSW.Text.Trim(),
                                ddlCCYID.SelectedValue);
                        r["MINAMOUNT"] =
                            SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtMinSW.Text, ddlCCYID.SelectedValue);
                        r["MAXAMOUNT"] =
                            SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtMaxSW.Text.Trim(),
                                ddlCCYID.SelectedValue);
                        r["RATE"] = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtRateSW.Text.Trim());
                        r["FkID"] = SmartPortal.Common.Utilities.Utility.generateUniqueID();
                        r["ISLADDER"] = cbIsLadderSW.Checked.ToString();
                        r["FIXAMOUNT"] = txtAmountSW.Visible
                            ? SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtAmountSW.Text.Trim(),
                                ddlCCYID.SelectedValue)
                            : "0";
                        r["FEEPAYER"] = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlPayer.SelectedValue.Trim());
                        r["CCYID"] =
                            SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlCCYID.SelectedValue.Trim());
                        r["BANKID"] =
                            SmartPortal.Common.Utilities.Utility.KillSqlInjection(dataRow["BANKID"].ToString());
                        r["SWCODE"] =
                            SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlBankSWCode.SelectedValue = dataRow["BANKID"].ToString());

                        tblTempt.Rows.Add(r);
                    }
                    ViewState["FEEDETAILS"] = tblTempt;
                    LoadGridView(null, EventArgs.Empty);
                }
                else
                {

                    tblTransDetails = (DataTable)ViewState["FEEDETAILS"];
                    bool validate = true;
                    reFromLimit = SmartPortal.Common.Utilities.Utility.isDouble(txtFromSW.Text.Trim(), true);
                    reToLimit = cbToLimitSW.Checked
                        ? -1
                        : SmartPortal.Common.Utilities.Utility.isDouble(txtToSW.Text.Trim(), true);
                    IsLadder = cbIsLadderSW.Checked;
                    reFeePayer = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlPayer.SelectedValue);
                    reFeeType = SmartPortal.Common.Utilities.Utility.KillSqlInjection("SWIFTFEE");
                    reCCYID = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlCCYID.SelectedValue);
                    foreach (DataRow dataRow in dt.Rows)
                    {
                        reBankID =
                            SmartPortal.Common.Utilities.Utility.KillSqlInjection(dataRow["BANKID"].ToString());


                        if (!validate) break;
                        foreach (DataRow row in tblTransDetails.Rows)
                        {
                            DataRow r = tblTempt.NewRow();
                            roToLimit = SmartPortal.Common.Utilities.Utility.isDouble(row["TOLIMIT"].ToString(), true);
                            roFromLimit = SmartPortal.Common.Utilities.Utility.isDouble(row["FROMLIMIT"].ToString(), true);
                            roIsLadder = bool.Parse(row["ISLADDER"].ToString());
                            roFeePayer = row["FEEPAYER"].ToString();
                            roFeeType = row["FEETYPE"].ToString().Equals("SWIFTFEE") ? row["FEETYPE"].ToString() : string.Empty;
                            roBankID = row["BANKID"].ToString();
                            roCCYID = row["CCYID"].ToString();

                            if (ValidateFee("SWIFTFEE"))
                            {

                                r["FEETYPE"] = reFeeType;
                                r["TOLIMIT"] = cbToLimitSW.Checked
                                    ? "-1"
                                    : SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtToSW.Text.Trim(),
                                        ddlCCYID.SelectedValue);
                                r["FROMLIMIT"] = SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtFromSW.Text.Trim(),
                                    ddlCCYID.SelectedValue.ToString());
                                r["MINAMOUNT"] =
                                    SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtMinSW.Text.Trim(),
                                        ddlCCYID.SelectedValue.ToString());
                                r["MAXAMOUNT"] =
                                    SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtMaxSW.Text.Trim(),
                                        ddlCCYID.SelectedValue.ToString());
                                r["RATE"] = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtRateSW.Text.Trim());
                                r["FkID"] = SmartPortal.Common.Utilities.Utility.generateUniqueID();
                                lblError.Text = string.Empty;
                                r["FIXAMOUNT"] = txtAmountSW.Visible
                                    ? SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtAmountSW.Text.Trim(),
                                        ddlCCYID.SelectedValue)
                                    : "0";
                                r["ISLADDER"] = cbIsLadderSW.Checked.ToString();
                                r["FEEPAYER"] = reFeePayer;
                                r["CCYID"] = reCCYID;
                                r["BANKID"] = reBankID;

                                r["SWCODE"] =
                                    SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlBankSWCode.Items.FindByValue(reBankID).Text);
                            }
                            else
                            {
                                validate = false;
                                lblError.Text = Resources.labels.datontaifee;
                                break;
                            }

                            tblTempt.Rows.Add(r);
                            break;
                        }
                    }

                    if (validate)
                    {
                        tblTransDetails.Merge(tblTempt);
                        ViewState["FEEDETAILS"] = tblTransDetails;
                        LoadGridView(null, EventArgs.Empty);
                    }
                    else
                    {
                        return;
                    }
                }

            }

        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(IPCex.ToString(), this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(IPCex.Message, Request.Url.Query);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    }
    protected void btnSaveDetailsOTHB_Click(object sender, EventArgs e)
    {
        try
        {

            lblError.Text = string.Empty;
            DataTable tblTransDetails = new DataTable();



            if (cbIsLadderOTHB.Checked)
            {
                if (SmartPortal.Common.Utilities.Utility.isDouble(txtFromOTHB.Text, true) < 0)
                {
                    lblError.Text = Resources.labels.hanmuctinhphikhonghople;
                    return;
                }
                else if (!cbToLimitOTHB.Checked && SmartPortal.Common.Utilities.Utility.isDouble(txtToOTHB.Text, true) <= 0)
                {
                    lblError.Text = Resources.labels.hanmuctinhphikhonghople;
                    return;
                }
                else if (!cbToLimitOTHB.Checked && SmartPortal.Common.Utilities.Utility.isDouble(txtToOTHB.Text, true) <=
                         SmartPortal.Common.Utilities.Utility.isDouble(txtFromOTHB.Text, true))
                {
                    lblError.Text = Resources.labels.hanmuctinhphikhonghople;
                    return;
                }
                else if (SmartPortal.Common.Utilities.Utility.isDouble(txtMinOTHB.Text, true) < 0 && allowNegativeFee)
                {
                    lblError.Text = Resources.labels.hanmuctinhphikhonghople;
                    return;
                }
                else if (SmartPortal.Common.Utilities.Utility.isDouble(txtMaxOTHB.Text, true) <= 0 && allowNegativeFee)
                {
                    lblError.Text = Resources.labels.hanmuctinhphikhonghople;
                    return;
                }
                else if (SmartPortal.Common.Utilities.Utility.isDouble(txtMaxOTHB.Text, true) <
                         SmartPortal.Common.Utilities.Utility.isDouble(txtMinOTHB.Text, true))
                {
                    lblError.Text = Resources.labels.hanmuctinhphikhonghople;
                    return;
                }

                try
                {
                    if (SmartPortal.Common.Utilities.Utility.isDouble(txtRateOTHB.Text, true) < 0 && allowNegativeFee)
                    {
                        lblError.Text = Resources.labels.hanmuctinhphikhonghople;
                        return;
                    }
                }
                catch (Exception exception)
                {
                    lblError.Text = Resources.labels.hanmuctinhphikhonghople;
                    return;
                }
            }

            DataTable dt = new DataTable();
            dt.Columns.Add("BANKID", typeof(string));
            try
            {
                if (string.IsNullOrEmpty(ddlBank.SelectedValue))
                {
                    foreach (ListItem item in ddlBank.Items)
                    {
                        if (!string.IsNullOrEmpty(item.Value))
                            dt.Rows.Add(new object[] { item.Value });
                    }
                }
                else
                {
                    dt.Rows.Add(new object[] { ddlBank.SelectedValue.Trim() });
                }

            }
            catch (Exception exception)
            {
                if (ddlBank.Items.Count == 0)
                {
                    lblError.Text = Resources.labels.nothing;
                    return;
                }

            }
            if (dt.Rows.Count > 0)
            {

                // tạo table tạm 
                DataTable tblTempt = new DataTable();
                DataColumn From = new DataColumn("FROMLIMIT");
                From.DataType = typeof(decimal);
                DataColumn To = new DataColumn("TOLIMIT");
                To.DataType = typeof(decimal);
                DataColumn Min = new DataColumn("MINAMOUNT");
                Min.DataType = typeof(decimal);
                DataColumn Max = new DataColumn("MAXAMOUNT");
                Max.DataType = typeof(decimal);
                DataColumn Rate = new DataColumn("RATE");
                DataColumn FixAmount = new DataColumn("FIXAMOUNT");
                FixAmount.DataType = typeof(decimal);
                DataColumn Feetype = new DataColumn("FEETYPE");
                DataColumn isLadder = new DataColumn("ISLADDER");
                DataColumn Feepayer = new DataColumn("FEEPAYER");
                DataColumn Currency = new DataColumn("CCYID");
                DataColumn BankID = new DataColumn("BANKID");
                DataColumn SwiftcCode = new DataColumn("SWCODE");
                DataColumn FkID = new DataColumn("FkID");
                tblTempt.Columns.AddRange(new DataColumn[]
                {
                   Feetype, From, To, Min, Max, Rate, FkID, FixAmount, isLadder
                    ,Feepayer , BankID,Currency,SwiftcCode
                });
                if (ViewState["FEEDETAILS"] == null)
                {
                    foreach (DataRow dataRow in dt.Rows)
                    {
                        DataRow r = tblTempt.NewRow();

                        r["FEETYPE"] = SmartPortal.Common.Utilities.Utility.KillSqlInjection("OTHERBANKFEE");
                        r["TOLIMIT"] = cbToLimitOTHB.Checked
                            ? "-1"
                            : SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtToOTHB.Text.Trim(),
                                ddlCCYID.SelectedValue);
                        r["FROMLIMIT"] =
                            SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtFromOTHB.Text.Trim(),
                                ddlCCYID.SelectedValue);
                        r["MINAMOUNT"] =
                            SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtMinOTHB.Text, ddlCCYID.SelectedValue);
                        r["MAXAMOUNT"] =
                            SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtMaxOTHB.Text.Trim(),
                                ddlCCYID.SelectedValue);
                        r["RATE"] = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtRateOTHB.Text.Trim());
                        r["FkID"] = SmartPortal.Common.Utilities.Utility.generateUniqueID();
                        r["ISLADDER"] = cbIsLadderOTHB.Checked.ToString();
                        r["FIXAMOUNT"] = txtAmountOTHB.Visible
                            ? SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtAmountOTHB.Text.Trim(),
                                ddlCCYID.SelectedValue)
                            : "0";
                        r["FEEPAYER"] = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlPayer.SelectedValue.Trim());
                        r["CCYID"] =
                            SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlCCYID.SelectedValue.Trim());
                        r["BANKID"] =
                            SmartPortal.Common.Utilities.Utility.KillSqlInjection(dataRow["BANKID"].ToString());

                        r["SWCODE"] =
                            SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlBankSWCode.Items.FindByValue(dataRow["BANKID"].ToString()).Text);


                        tblTempt.Rows.Add(r);
                    }
                    ViewState["FEEDETAILS"] = tblTempt;
                    LoadGridView(null, EventArgs.Empty);
                }
                else
                {

                    tblTransDetails = (DataTable)ViewState["FEEDETAILS"];
                    bool validate = true;
                    reFromLimit = SmartPortal.Common.Utilities.Utility.isDouble(txtFromOTHB.Text.Trim(), true);
                    reToLimit = cbToLimitOTHB.Checked
                        ? -1
                        : SmartPortal.Common.Utilities.Utility.isDouble(txtToOTHB.Text.Trim(), true);
                    IsLadder = cbIsLadderOTHB.Checked;
                    reCCYID = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlCCYID.SelectedValue);
                    reFeePayer = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlPayer.SelectedValue);
                    reFeeType = SmartPortal.Common.Utilities.Utility.KillSqlInjection("OTHERBANKFEE");
                    foreach (DataRow dataRow in dt.Rows)
                    {
                        reBankID =
                            SmartPortal.Common.Utilities.Utility.KillSqlInjection(dataRow["BANKID"].ToString());


                        if (!validate) break;
                        foreach (DataRow row in tblTransDetails.Rows)
                        {
                            DataRow r = tblTempt.NewRow();
                            roToLimit = SmartPortal.Common.Utilities.Utility.isDouble(row["TOLIMIT"].ToString(), true);
                            roFromLimit = SmartPortal.Common.Utilities.Utility.isDouble(row["FROMLIMIT"].ToString(), true);
                            roIsLadder = bool.Parse(row["ISLADDER"].ToString());


                            if (ValidateFee("OTHERBANKFEE"))
                            {

                                r["FEETYPE"] = SmartPortal.Common.Utilities.Utility.KillSqlInjection("OTHERBANKFEE"); ;
                                r["TOLIMIT"] = cbToLimitOTHB.Checked
                                    ? "-1"
                                    : SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtToOTHB.Text.Trim(),
                                        ddlCCYID.SelectedValue);
                                r["FROMLIMIT"] = SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtFromOTHB.Text.Trim(),
                                    ddlCCYID.SelectedValue.ToString());
                                r["MINAMOUNT"] =
                                    SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtMinOTHB.Text.Trim(),
                                        ddlCCYID.SelectedValue.ToString());
                                r["MAXAMOUNT"] =
                                    SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtMaxOTHB.Text.Trim(),
                                        ddlCCYID.SelectedValue.ToString());
                                r["RATE"] = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtRateOTHB.Text.Trim());
                                r["FkID"] = SmartPortal.Common.Utilities.Utility.generateUniqueID();
                                lblError.Text = string.Empty;
                                r["FIXAMOUNT"] = txtAmountOTHB.Visible
                                    ? SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtAmountOTHB.Text.Trim(),
                                        ddlCCYID.SelectedValue)
                                    : "0";
                                r["ISLADDER"] = cbIsLadderOTHB.Checked.ToString();
                                r["FEEPAYER"] = reFeePayer;
                                r["CCYID"] = reCCYID;
                                r["BANKID"] = reBankID;

                                r["SWCODE"] =
                                    SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlBankSWCode.Items.FindByValue(reBankID).Text);

                            }
                            else
                            {
                                validate = false;
                                lblError.Text = Resources.labels.datontaifee;
                                break;
                            }

                            tblTempt.Rows.Add(r);
                            break;
                        }
                    }

                    if (validate)
                    {
                        tblTransDetails.Merge(tblTempt, true, MissingSchemaAction.Ignore);
                        ViewState["FEEDETAILS"] = tblTransDetails;
                        LoadGridView(null, EventArgs.Empty);
                    }
                    else
                    {
                        return;
                    }
                }

            }

        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(IPCex.ToString(), this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(IPCex.Message, Request.Url.Query);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    }
    protected void LoadGridView(object sender, EventArgs e)
    {
        try
        {
            gvFeeSwiftDetails.PageIndex = 0;
            DataTable tblTransDetails = (DataTable)ViewState["FEEDETAILS"];
            if (tblTransDetails != null)
            {
                DataView dv = new DataView(tblTransDetails);
                dv.Sort = "[" + tblTransDetails.Columns["FEETYPE"].ColumnName + "] ASC, [" + tblTransDetails.Columns["FROMLIMIT"].ColumnName + "] ASC, [" + tblTransDetails.Columns["FEEPAYER"].ColumnName + "] ASC, [" + tblTransDetails.Columns["CCYID"].ColumnName + "] ASC, [" + tblTransDetails.Columns["BANKID"].ColumnName + "]  ASC";              
                gvFeeSwiftDetails.DataSource = dv;
                gvFeeSwiftDetails.DataBind();
                if (tblTransDetails != null && tblTransDetails.Rows.Count > 0)
                {
                    pnGV.Visible = true;
                }
                else
                {
                    pnGV.Visible = false;
                }
            }
            else
            {
                pnGV.Visible = false;
            }
            

        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(IPCex.ToString(), this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(IPCex.Message, Request.Url.Query);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    }
    protected void gvFeeSwiftDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DataTable tblTransDetailsDel = (DataTable)ViewState["FEEDETAILS"];
        Label lblFkID = (Label)gvFeeSwiftDetails.Rows[e.RowIndex].FindControl("lblFkID");
        DataRow[] dr = tblTransDetailsDel.Select("FkID='" + lblFkID.Text.Trim() + "'");
        string feetype = dr[0]["FEETYPE"].ToString();
        string bankid = dr[0]["BANKID"].ToString();
        string feepayer = dr[0]["FEEPAYER"].ToString();
        string ccyid = dr[0]["CCYID"].ToString();
        string fromlimit = dr[0]["FROMLIMIT"].ToString();
        string tolimit = dr[0]["TOLIMIT"].ToString();
        new SmartPortal.SEMS.InterBank().DeleteSwiftFee(feetype, bankid, feepayer, ccyid, fromlimit, tolimit, ref IPCERRORCODE, ref IPCERRORDESC);
        if (IPCERRORCODE == "0")
        {
            lblError.Text = Resources.labels.xoaphigiaodichthanhcong;
            tblTransDetailsDel.Rows.Remove(dr[0]);
            ViewState["FEEDETAILS"] = tblTransDetailsDel;
        }
        LoadGridView(null, EventArgs.Empty);
    }
    protected void cbIsLadderSW_CheckedChanged(object sender, EventArgs e)
    {
        if (cbIsLadderSW.Checked)
        {
            txtAmountSW.Text = "";
            txtAmountSW.Enabled = false;
            tbLadderFeeSwift.Visible = true;
        }
        else
        {
            txtAmountSW.Text = "";
            txtAmountSW.Enabled = true;
            tbLadderFeeSwift.Visible = false;
        }
        txtFromSW.Text = "0";
        txtToSW.Text = "0";
        txtMinSW.Text = "0";
        txtMaxSW.Text = "0";
        txtRateSW.Text = "0";
    }
    protected void cbIsLadderOTHB_CheckedChanged(object sender, EventArgs e)
    {
        if (cbIsLadderOTHB.Checked)
        {
            txtAmountOTHB.Text = "";
            txtAmountOTHB.Enabled = false;
            tbLadderFeeOtherbank.Visible = true;
        }
        else
        {
            txtAmountOTHB.Text = "";
            txtAmountOTHB.Enabled = true;
            tbLadderFeeOtherbank.Visible = false;
        }
        txtFromOTHB.Text = "0";
        txtToOTHB.Text = "0";
        txtMinOTHB.Text = "0";
        txtMaxOTHB.Text = "0";
        txtRateOTHB.Text = "0";
    }


    protected void cbToLimitSW_OnCheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (cbToLimitSW.Checked)
            {
                txtToSW.Text = string.Empty;
                txtToSW.Enabled = false;
            }
            else
            {
                txtToSW.Text = "0";
                txtToSW.Enabled = true;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void cbToLimitOTHB_OnCheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (cbToLimitOTHB.Checked)
            {
                txtToOTHB.Text = string.Empty;
                txtToOTHB.Enabled = false;
            }
            else
            {
                txtToOTHB.Text = "0";
                txtToOTHB.Enabled = true;
            }
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
            string bankid = ddlBank.SelectedValue.Trim();
            string feetype = ddlFeetype.SelectedValue.Trim();
            string feepayer = ddlPayer.SelectedValue.Trim();
            string ccyid = ddlCCYID.SelectedValue.Trim();
            DataTable tblSwiftfee = new InterBank().GetSwiftfeebycondition(bankid, feetype, feepayer, ccyid, ref IPCERRORCODE, ref IPCERRORDESC).Tables[0];
            if (tblSwiftfee.Rows.Count != 0)
            {

                DataTable tblTransDetails = new DataTable();
                DataTable tblTempt = new DataTable();
                DataColumn Feetype = new DataColumn("FEETYPE");
                DataColumn From = new DataColumn("FROMLIMIT");
                From.DataType = typeof(decimal);
                DataColumn To = new DataColumn("TOLIMIT");
                To.DataType = typeof(decimal);
                DataColumn Min = new DataColumn("MINAMOUNT");
                Min.DataType = typeof(decimal);
                DataColumn Max = new DataColumn("MAXAMOUNT");
                Max.DataType = typeof(decimal);
                DataColumn Rate = new DataColumn("RATE");
                DataColumn FixAmount = new DataColumn("FIXAMOUNT");
                FixAmount.DataType = typeof(decimal);
                DataColumn IsLadder = new DataColumn("ISLADDER");
                DataColumn Feepayer = new DataColumn("FEEPAYER");
                DataColumn Currency = new DataColumn("CCYID");
                DataColumn BankID = new DataColumn("BANKID");
                DataColumn SwiftcCode = new DataColumn("SWCODE");
                DataColumn FkID = new DataColumn("FkID");

                tblTransDetails.Columns.AddRange(new DataColumn[] { Feetype, From, To, Min, Max, Rate, FkID, FixAmount, IsLadder, Feepayer, BankID, SwiftcCode, Currency });
                foreach (DataRow ro in tblSwiftfee.Rows)
                {
                    DataRow r = tblTransDetails.NewRow();
                    if (ro["FROMLIMIT"] is DBNull && ro["TOLIMIT"] is DBNull && ro["MINAMOUNT"] is DBNull && ro["MAXAMOUNT"] is DBNull && ro["FIXAMOUNT"] is DBNull && ro["RATE"] is DBNull)
                    {
                        break;
                    }
                    r["FEETYPE"] = ro["FEETYPE"].ToString();
                    r["TOLIMIT"] = ro["TOLIMIT"].ToString();
                    r["FROMLIMIT"] = ro["FROMLIMIT"].ToString();
                    r["MINAMOUNT"] = ro["MINAMOUNT"].ToString();
                    r["MAXAMOUNT"] = ro["MAXAMOUNT"].ToString();
                    r["RATE"] = ro["RATE"].ToString();
                    r["FkID"] = SmartPortal.Common.Utilities.Utility.generateUniqueID();
                    r["ISLADDER"] = (bool)ro["ISLADDER"];
                    r["FIXAMOUNT"] = ro["FIXAMOUNT"].ToString();
                    r["FEEPAYER"] = ro["FEEPAYER"];
                    r["CCYID"] = ro["CCYID"].ToString();
                    r["BANKID"] = ro["BANKID"].ToString();
                    //r["SWCODE"] = ro["SWCODE"].ToString();
                    tblTransDetails.Rows.Add(r);
                }
                if (tblTransDetails.Rows.Count > 0)
                {
                    ViewState["FEEDETAILS"] = tblTransDetails;
                }
                else
                {
                    ViewState["FEEDETAILS"] = null;
                }
            }
            else
            {
                ViewState["FEEDETAILS"] = null;
                lblError.Text = "<p class='divDataNotFound'>" + Resources.labels.datanotfound + "</p>";
                
            }
            LoadGridView(null, EventArgs.Empty);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
    protected bool ValidateFee(string feetype)
    {
        bool flag = false;


        DataTable tblTransDetails = (DataTable)ViewState["FEEDETAILS"];
        foreach (DataRow row in tblTransDetails.Rows)
        {
            reFeeType = feetype;
            roFeePayer = row["FEEPAYER"].ToString();
            roToLimit = SmartPortal.Common.Utilities.Utility.isDouble(row["TOLIMIT"].ToString(), true);
            roFromLimit = SmartPortal.Common.Utilities.Utility.isDouble(row["FROMLIMIT"].ToString(), true);
            roFeeType = row["FEETYPE"].ToString();
            roBankID = row["BANKID"].ToString();
            roCCYID = row["CCYID"].ToString();
            roIsLadder = bool.Parse(row["ISLADDER"].ToString());
            bool isLadderRight = (roToLimit <= reFromLimit && roToLimit != -1) || (roFromLimit >= reToLimit && reToLimit != -1);

            if (reFeePayer.Equals(roFeePayer) && reCCYID.Equals(roCCYID) && (reFeeType.Equals(roFeeType))) //cung fee payer
            {
                if (reBankID.Equals(roBankID)) //cung bank
                {
                    if (roIsLadder && IsLadder)
                    {
                        flag = isLadderRight;
                        if (!flag) return flag;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    flag = true;
                }
            }
            else
            {
                flag = true;
            }
        }
        return flag;
    }


    public void RedirectBackToMainPage()
    {
        string pageid = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["p"].ToString();
        string link = PagesBLL.GetLinkMaster_Page(pageid);
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL(link));

    }

    protected void ddlCCYID_OnSelectedIndexChanged(object sender, EventArgs e)
    {

        DataSet dtBank = new SmartPortal.SEMS.Partner().GetCBBankALL(ddlCCYID.SelectedValue.Trim(), ref IPCERRORCODE, ref IPCERRORDESC);
        ddlBank.DataSource = dtBank;
        ddlBank.DataTextField = "BankName";
        ddlBank.DataValueField = "BankID";
        ddlBank.DataBind();
        ddlBank.Items.Insert(0, new ListItem(Resources.labels.allforbank, ""));

    }

}
