using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Globalization;
using Antlr.Runtime.JavaExtensions;
using SmartPortal.ExceptionCollection;
using SmartPortal.Common.Utilities;
using SmartPortal.IB;
using System.Text.RegularExpressions;
using System.Text;
using Newtonsoft.Json;
using SmartPortal.DAL;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Information;
using SmartPortal.Common;

public partial class widgets_IBGiftPayment_widget_ascx : WidgetBase
{
    string IPCERRORCODE;
    string IPCERRORDESC;
    static bool isApprove = false;
    private DataTable TABLE
    {
        get { return ViewState["TABLE"] as DataTable; }
        set
        {
            ViewState["TABLE"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        lblTextError.Text = "";
        if (!IsPostBack)
        {
            GetEGiftCard();
            LoadDataForDropDownGiftCardType();
            ddlGiftCardType_SelectedIndexChanged(sender, e);
            ShowGiftCardPanel();
            load_unit();
            LoadAccountInfo();
        }
    }
    private void load_unit()
    {
        try
        {
            string errorcode = "";
            string errorDesc = "";
            DataSet ds = new SmartPortal.IB.Account().getAccount(Session["userID"].ToString(), "IBBUYGIFTCARD", "", ref errorcode, ref errorDesc);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].DefaultView.Count > 0)
            {
                if (!IsPostBack)
                {
                    ds.Tables[0].DefaultView.RowFilter = "accttype in ('DD', 'CD') and status = 'A' and CCYID = 'LAK'";
                    ddlSenderAccount.DataSource = ds.Tables[0].DefaultView;
                    ddlSenderAccount.DataValueField = ds.Tables[0].Columns["ACCTNO"].ColumnName.ToString();
                    ddlSenderAccount.DataBind();
                    //thaity modify at 26/6/2014 for permission
                    ds.Tables[0].DefaultView.RowFilter = "accttype in ('DD', 'CD') and status not in ('CLS','S','M','V')";
                }
            }
            else
            {
                throw new BusinessExeption("User not register DD Account To Transfer.");
            }

        }
        catch (BusinessExeption bex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["notregtransfer"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, bex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["notregtransfer"], Request.Url.Query);
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    }

    private void ResetGiftCard()
    {
        hdfSerialNumber.Value = hdfCurrency.Value =
            hdfACCTCCYAMOUNT.Value = hdfAMOUNTBCY.Value = hdfDEBITEXCHANGEBCY.Value = hdfCREDITEXCHANGEBCY.Value = hdfCROSSRATE.Value = string.Empty;
    }

    private void LoadAccountInfo()
    {
        try
        {
            string account = ddlSenderAccount.SelectedItem.Text.ToString();
            string Acctype = ddlSenderAccount.SelectedItem.Value.ToString();
            string ErrorCode = string.Empty;
            string ErrorDesc = string.Empty;
            string User = Session["userID"].ToString();
            Account acct = new Account();
            DataSet ds = new DataSet();
            ds = acct.GetInfoDD(User, account, ref ErrorCode, ref ErrorDesc);
            ShowDD(account, ds);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_IBBuyTopup_Widget", "LoadAccountInfo", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    private void ShowGiftCardPanel()
    {
        Disall();
        pnGift2.Visible = btnContineAcc.Visible = true;
        ResetGiftCard();
    }
    void Disall()
    {
        btnConfim.Visible = false;
        btnContineAcc.Visible = false;
        pnConfim.Visible = false;
        pnGift2.Visible = false;
        btnBack.Visible = false;
        pnOTP.Visible = false;
        btnAction.Visible = false;
        pnFinish.Visible = false;
    }
    

    private void LoadDataForDropDownGiftCardType()
    {
        try
        {
            DataView view = new DataView(TABLE);
            view.Sort = "brand";
            DataTable dtType = view.ToTable(true, "brand");
            ddlGiftCardType.DataSource = dtType;
            ddlGiftCardType.DataTextField = "brand";
            ddlGiftCardType.DataValueField = "brand";
            ddlGiftCardType.DataBind();


            //DataTable dtType = new SmartPortal.IB.GiftCard().GetAllGiftCardType();
            //ddlGiftCardType.DataSource = dtType;
            //ddlGiftCardType.DataTextField = "CardName";
            //ddlGiftCardType.DataValueField = "CardID";
            //ddlGiftCardType.DataBind();
        }
        catch
        {
            ddlGiftCardType.DataSource = new DataTable();
            ddlGiftCardType.DataBind();
        }
    }

   
    public void ddlSenderAccount_SelectedIndexChanged(object sender, EventArgs eventArgs)
    {
        LoadAccountInfo();
    }

    public void ddlGiftCardType_SelectedIndexChanged(object sender, EventArgs eventArgs)
    {
        LoadDataForDropdownDenomination(sender, eventArgs);
    }

    private void LoadDataForDropdownDenomination(object sender, EventArgs eventArgs)
    {
        try
        {
            DataTable result = new DataTable();
            result.Columns.Add("denomination", typeof(string));
            result.Columns.Add("denomination_id", typeof(string));
            DataRow[] rows = TABLE.Select("brand ='" + ddlGiftCardType.SelectedValue.ToString() + "'", "actual_price");
            for (int i = 0; i < rows.Length; i++)
            {
                DataRow row = result.NewRow();
                row["denomination_id"] = rows[i]["denomination_id"].ToString();
                row["denomination"] = rows[i]["denomination"].ToString();
                result.Rows.Add(row);
            }
            ddldenom.DataSource = result.DefaultView.ToTable(true, "denomination_id", "denomination");
            ddldenom.DataValueField = "denomination_id";
            ddldenom.DataTextField = "denomination";
            ddldenom.DataBind();
        }
        catch (Exception ex)
        {
            ddldenom.DataSource = new DataTable();
            ddldenom.DataBind();
            //lblTextError.Text = "Can not found Denomination of " + ddlGiftCardType.SelectedItem.Text;
            SmartPortal.Common.Log.RaiseError(ex.ToString(), this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
        }
    }
    

    protected void DropDownListOTP_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlLoaiXacThuc.SelectedValue.ToString() == "ESMSOTP")
            {
                btnSendOTP.Visible = true;
                if (btnSendOTP.Text == "ReSend")
                {
                    btnAction.Enabled = true;
                }
                else
                {
                    btnAction.Enabled = false;
                }
            }
            else
            {
                btnAction.Enabled = true;
                btnSendOTP.Visible = false;
            }
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(IPCex.ToString(), this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(IPCex.ToString(), Request.Url.Query);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    protected void btnSendOTP_Click(object sender, EventArgs e)
    {
        string errorcode = "";
        string errorDesc = "";
        try
        {

            btnSendOTP.Text = "ReSend";
            SmartPortal.SEMS.OTP.SendSMSOTP(Session["userID"].ToString(), ref errorcode, ref errorDesc);
            switch (errorcode)
            {
                case "0":
                    lblTextError.Text = "Send SMS OTP success."; btnAction.Enabled = true;
                    break;
                case "7003":
                    lblTextError.Text = "User does not register SMS OTP"; btnAction.Enabled = false;
                    break;
                default:
                    lblTextError.Text = errorDesc; btnAction.Enabled = false;
                    break;
            }
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(IPCex.ToString(), this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(IPCex.ToString(), Request.Url.Query);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
        btnSendOTP.Enabled = true;

    }
    private void ShowDD(string acctno, DataSet ds)
    {
        try
        {
            if (ds.Tables[0].Rows.Count == 1)
            {
                lblLastTranDate.Text = Utility.FormatDatetime(ds.Tables[0].Rows[0][SmartPortal.Constant.IPC.LASTTRANSDATE].ToString(), "dd/MM/yyyy", DateTimeStyle.DateMMM);
                lblAvailableBal.Text = lblBalanceSender.Text = lblEndBalanceSender.Text = Utility.FormatMoney(ds.Tables[0].Rows[0][SmartPortal.Constant.IPC.AVAILABLEBALANCE].ToString(), ds.Tables[0].Rows[0][SmartPortal.Constant.IPC.CCYID].ToString().Trim());
                lblAvailableBalCCYID.Text = lblSenderCCYID.Text = lblEndSenderCCYID.Text = lblEndSenderCCYID.Text = hdfACCTCCY.Value = ds.Tables[0].Rows[0][SmartPortal.Constant.IPC.CCYID].ToString();
                lblAccName.Text = lblEndSenderName.Text = ds.Tables[0].Rows[0]["customername"].ToString();
                hdfBranchID.Value = ds.Tables[0].Rows[0]["chinhanh"].ToString();
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    private bool ValidateGiftCard()
    {
        try
        {
            if (string.IsNullOrEmpty(hdfAcctNo.Value) || string.IsNullOrEmpty(hdfGiftCardType.Value) || string.IsNullOrEmpty(hdfDenominations.Value) || string.IsNullOrEmpty(hdfEquivalentAmount.Value) || string.IsNullOrEmpty(hdfCurrency.Value) || string.IsNullOrEmpty(hdfACCTCCYAMOUNT.Value) || string.IsNullOrEmpty(hdfFEEACCTCCY.Value) || string.IsNullOrEmpty(hdfACCTCCY.Value) || string.IsNullOrEmpty(hdfAMOUNTBCY.Value) || string.IsNullOrEmpty(hdfDEBITEXCHANGEBCY.Value) || string.IsNullOrEmpty(hdfCREDITEXCHANGEBCY.Value) || string.IsNullOrEmpty(hdfCROSSRATE.Value)) return false;
            return true;
        }
        catch
        {
            return false;
        }
    }

    protected void btnContineAcc_Click(object sender, EventArgs e)
    {
        try
        {
            DataRow[] rows = TABLE.Select("brand ='" + ddlGiftCardType.SelectedValue.ToString() + "' and denomination_id ='" + ddldenom.SelectedValue.ToString() + "'");
            hdfAcctNo.Value = Utility.KillSqlInjection(ddlSenderAccount.SelectedValue);
            hdfGiftCardType.Value = Utility.KillSqlInjection(ddlGiftCardType.SelectedItem.Text);
            hdfDenominations.Value = Utility.KillSqlInjection(ddldenom.SelectedItem.Text.ToString());
            hdfEquivalentAmount.Value = Utility.KillSqlInjection(rows[0]["actual_price"].ToString());
            hdfCurrency.Value = "USD";

            Hashtable ht = new GiftCard().CalFeeGiftCard(Session["userID"].ToString(), hdfAcctNo.Value, hdfCurrency.Value, Utility.KillSqlInjection(rows[0]["category"].ToString()), Utility.FormatMoneyInput(hdfEquivalentAmount.Value, hdfCurrency.Value), hdfBranchID.Value, hdfACCTCCY.Value);

            if (ht["IPCERRORCODE"].ToString().Equals("0"))
            {
                hdfACCTCCYAMOUNT.Value = ht["ACCTAMOUNT"].ToString();
                hdfFEEACCTCCY.Value = ht["FEEACCTCCY"].ToString();
                hdfAMOUNTBCY.Value = ht["AMOUNTBCY"].ToString();
                hdfDEBITEXCHANGEBCY.Value = ht["DEBITEXCHANGEBCY"].ToString();
                hdfCREDITEXCHANGEBCY.Value = ht["CREDITEXCHANGEBCY"].ToString();
                hdfCROSSRATE.Value = ht["CROSSRATE"].ToString();
            }
            else
            {
                lblTextError.Text = ht["IPCERRORDESC"].ToString();
                return;
            }
            ShowConfirmPanel();
        }
        catch (BusinessExeption bex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["notregtransfer"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, bex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["notregtransfer"], Request.Url.Query);
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    private void ShowConfirmPanel()
    {
        Disall();
        pnConfim.Visible = btnConfim.Visible = btnBack.Visible = true;
        SetDetailConfirmPanel();
    }
    private void ShowOTP()
    {
        Disall();
        pnOTP.Visible = btnAction.Visible = btnBack.Visible = true;
    }

    private void SetDetailConfirmPanel()
    {
        try
        {
            lblSenderAccount.Text = hdfAcctNo.Value;
            lblGiftType.Text = hdfGiftCardType.Value;
            lblDenomination.Text = hdfDenominations.Value;
            lblConfirmAmount.Text = Utility.FormatMoney((double.Parse(hdfACCTCCYAMOUNT.Value.ToString()) + double.Parse(hdfFEEACCTCCY.Value.ToString())).ToString(), hdfACCTCCY.Value);
            lblConfirmAmountCCY.Text = hdfACCTCCY.Value;
            lblEquivalentAmountConfirm.Text = Utility.FormatMoney(hdfEquivalentAmount.Value, hdfCurrency.Value);
            lblEquiAmountCurrency.Text = hdfCurrency.Value;
            lblConfirmFee.Text = Utility.FormatMoney(hdfFEEACCTCCY.Value, hdfACCTCCY.Value);
            lblConfirmFeeCurrency.Text = hdfACCTCCY.Value;
        }
        catch (BusinessExeption bex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["notregtransfer"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, bex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["notregtransfer"], Request.Url.Query);
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        if(pnConfim.Visible == true || pnFinish.Visible == true)
        {
            ShowGiftCardPanel();
        }
        else if(pnOTP.Visible == true)
        {
            ShowConfirmPanel();
        }
    }
    protected void btnConfim_Click(object sender, EventArgs e)
    {
        
        try
        {
            if (double.Parse(lblBalanceSender.Text) <= double.Parse(lblConfirmAmount.Text))
            {
                lblTextError.Text = Resources.labels.balancecompareequivalentamount;
                return;
            }
            ShowOTP();

            SmartPortal.IB.Transactions objTran = new SmartPortal.IB.Transactions();
            DataTable dt = new DataTable();
            dt = objTran.LoadAuthenType(Session["userID"].ToString());
            ddlLoaiXacThuc.DataSource = dt;
            ddlLoaiXacThuc.DataTextField = "TYPENAME";
            ddlLoaiXacThuc.DataValueField = "AUTHENTYPE";
            ddlLoaiXacThuc.DataBind();
            //edit by vutran 18/08/2014: sua loi ko hien nut send neu chi co SMSOTP
            if (ddlLoaiXacThuc.SelectedValue.ToString() == "ESMSOTP")
            {
                btnSendOTP.Visible = true;
            }
            else
            {
                btnSendOTP.Visible = false;
            }
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void btnAction_Click(object sender, EventArgs e)
    {
        try
        {
            ShowOTP();
            if (!ValidateGiftCard())
            {
                ShowGiftCardPanel();
                return;
            }
            string OTPcode = txtOTP.Text;
            txtOTP.Text = string.Empty;
            DataRow[] rows = TABLE.Select("brand ='" + ddlGiftCardType.SelectedValue.ToString() + "' and denomination_id ='" + ddldenom.SelectedValue.ToString() + "'");

            Hashtable htResult = new GiftCard().BuyGiftCard(
                Session["userID"].ToString(),
                ddlLoaiXacThuc.SelectedValue,
                OTPcode,
                hdfAcctNo.Value,
                hdfACCTCCY.Value,
                hdfGiftCardType.Value,
                hdfDenominations.Value,
                hdfCurrency.Value,
                Utility.FormatMoneyInput(hdfACCTCCYAMOUNT.Value, hdfACCTCCY.Value),
                Utility.FormatMoneyInput(hdfFEEACCTCCY.Value, hdfACCTCCY.Value),
                Utility.FormatMoneyInput(hdfEquivalentAmount.Value, hdfCurrency.Value),
                Utility.FormatMoneyInput(hdfAMOUNTBCY.Value, hdfACCTCCY.Value),
                Utility.FormatMoneyInput(hdfDEBITEXCHANGEBCY.Value, hdfACCTCCY.Value),
                Utility.FormatMoneyInput(hdfCREDITEXCHANGEBCY.Value, hdfACCTCCY.Value),
                Utility.FormatMoneyInput(hdfCROSSRATE.Value, hdfACCTCCY.Value),
                Utility.KillSqlInjection(rows[0]["category"].ToString()),
                lblEndSenderName.Text
                );

            if (htResult["IPCERRORCODE"].ToString().Equals("0"))
            {
                try
                {
                    lblFinishTranRef.Text = htResult["TRANSID"].ToString();
                    lblTextError.Text = Resources.labels.transactionsuccessful;
                    lblFinishTranTime.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
                    lblFinishGiftCode.Text = htResult["PINCODE"].ToString();
                    load_unit();
                    ddlSenderAccount.SelectedValue = lblendSenderAccount.Text = hdfAcctNo.Value;
                    LoadAccountInfo();
                    Disall();
                    lblFinishGiftcardType.Text = hdfGiftCardType.Value;
                    lblFinishDenomination.Text = hdfDenominations.Value;
                    lblFinishEquivalentAmount.Text = Utility.FormatMoney(hdfEquivalentAmount.Value, hdfCurrency.Value);
                    lblFinishAmountCurrency.Text = lblFinishFeeCurrency.Text = hdfACCTCCY.Value;
                    lblFininhEquiAmountCurrency.Text = hdfCurrency.Value;
                    lblFinishAmount.Text = Utility.FormatMoney((double.Parse(hdfACCTCCYAMOUNT.Value.ToString()) + double.Parse(hdfFEEACCTCCY.Value.ToString())).ToString(), hdfACCTCCY.Value);
                    lblFinishFee.Text = Utility.FormatMoney(hdfFEEACCTCCY.Value, hdfACCTCCY.Value);
                    pnFinish.Visible = btnBack.Visible = true;
                }
                catch(Exception ex)
                {
                    SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_IBTransferInBAC1_Widget", "btnApply_Click", ex.ToString(), Request.Url.Query);
                    lblTextError.Text = htResult["IPCERRORDESC"].ToString();
                }
            }
            else if (htResult["IPCERRORCODE"].ToString().Equals("9999"))
            {
                SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"],
                    Request.Url.Query);
            }
            else
            {
                lblTextError.Text = htResult["IPCERRORDESC"].ToString();
            }
        }
        catch(Exception ex)
        {
            lblTextError.Text = Resources.labels.transactionerror;
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_IBTransferInBAC1_Widget", "btnApply_Click", ex.ToString(), Request.Url.Query);
        }
    }
    protected void GetEGiftCard()
    {
        try
        {
            DataTable dt = new SmartPortal.IB.GiftCard().GETEGIFTCARD(ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                if (dt.Rows.Count > 0)
                {
                    DataTable result = new DataTable();
                    result.Columns.Add("category", typeof(string));
                    result.Columns.Add("brand", typeof(string));
                    result.Columns.Add("denomination", typeof(string));
                    result.Columns.Add("denomination_id", typeof(string));
                    result.Columns.Add("actual_price", typeof(decimal));
                    result.Columns.Add("imgurl", typeof(string));
                    DataRow[] rows = dt.Select();
                    for (int i = 0; i < rows.Length; i++)
                    {
                        DataRow row = result.NewRow();
                        row["category"] = rows[i]["category"].ToString();
                        row["brand"] = rows[i]["brand"].ToString();
                        row["denomination"] = rows[i]["denomination"].ToString();
                        row["denomination_id"] = rows[i]["denomination_id"].ToString();
                        row["actual_price"] = decimal.Parse(rows[i]["actual_price"].ToString());
                        row["imgurl"] = rows[i]["imgurl"].ToString();
                        result.Rows.Add(row);
                    }

                    TABLE = result;
                }
            }
            else
            {
                lblTextError.Text = "The system cannot load the list of gift cards!";
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_IBTaxPayment_Widget", "CheckAmount", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
}
