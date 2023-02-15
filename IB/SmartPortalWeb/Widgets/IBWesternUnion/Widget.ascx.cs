using AjaxControlToolkit;
using SmartPortal.ExceptionCollection;
using SmartPortal.IB;
using System;
using System.Collections;
using System.Configuration;
using System.Data;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility = SmartPortal.Common.Utilities.Utility;
using Image = System.Web.UI.WebControls.Image;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using SmartPortal.Model;
using System.IO;
using System.Collections.Generic;

public partial class Widgets_IBWesternUnion_Widget : WidgetBase
{
    string IPCERRORCODE;
    string IPCERRORDESC;
    public DataTable TBLDOCUMENT
    {
        get { return ViewState["TBLDOCUMENT"] != null ? (DataTable)ViewState["TBLDOCUMENT"] : Utility.createTableDocumnet(); }
        set { ViewState["TBLDOCUMENT"] = TBLDOCUMENT; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        txtexpitydate.Attributes.Add("max", "2200-01-01");
        txtissuedate.Attributes.Add("max", DateTime.Now.ToString("yyyy-MM-dd"));
        txtdateofbirth.Attributes.Add("max", DateTime.Now.ToString("yyyy-MM-dd"));
        try
        {
            txtamountsend.Attributes.Add("onkeyup", "ntt('" + txtamountsend.ClientID + "','',event)");
            txtdateofbirth.Text = DateTime.Now.AddYears(-17).ToString("dd/MM/yyyy");
            txtissuedate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtexpitydate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            if (!IsPostBack)
            {
                load_unit(sender, e);
                ddlSenderAccount_SelectedIndexChanged(sender, e);
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    private void load_unit(object sender, EventArgs e)
    {
        try
        {
            string ErrorCode = string.Empty;
            string ErrorDesc = string.Empty;
            DataSet ds = new DataSet();
            Account acct = new Account();
            ds = acct.GetListOfAccounts(Session["userID"].ToString(), "IBWESTERNUNION", "IBWLWESTERNUNION", "DD,CD,WL", "USD", ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE.Equals("0"))
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ds.Tables[0].DefaultView.RowFilter = "TYPEID IN ('DD', 'CD','WLM') AND STATUSCD in ('A','Y')";
                    ddlSenderAccount.DataSource = ds.Tables[0].DefaultView;
                    ddlSenderAccount.DataTextField = ds.Tables[0].Columns["ACCOUNTNO"].ColumnName.ToString();
                    ddlSenderAccount.DataValueField = ds.Tables[0].Columns["UNIQUEID"].ColumnName.ToString();
                    ddlSenderAccount.DataBind();
                }
                else
                {
                    lblTextError.Text = "User have no account USD";
                }
            }
            else
            {
                lblTextError.Text = "User have no account USD";
            }

            //load all dropdown list
            loadDDL();

            DataSet data = new DataSet();
            data = new User().GetFullUserByUID(Session["userID"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);

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

    private void loadDDL()
    {
        try
        {
            DataSet ds = new DataSet();
            WesternUnion western = new WesternUnion();
            ds = western.GetSourceofFunds(ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                dllSourceFund.DataSource = ds.Tables[0].DefaultView;
                dllSourceFund.DataTextField = ds.Tables[0].Columns["NAME"].ColumnName.ToString();
                dllSourceFund.DataValueField = ds.Tables[0].Columns["NO"].ColumnName.ToString();
                dllSourceFund.DataBind();
            }

            ds = western.GetMobileCountryCode(ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                ddlmobilecode.DataSource = ds.Tables[0].DefaultView;
                ddlmobilecode.DataTextField = ds.Tables[0].Columns["MOBILE"].ColumnName.ToString();
                ddlmobilecode.DataValueField = ds.Tables[0].Columns["MOBILECODE"].ColumnName.ToString();
                ddlmobilecode.DataBind();

                ddlCountryCode.DataSource = ds.Tables[0].DefaultView;
                ddlCountryCode.DataTextField = ds.Tables[0].Columns["MOBILE"].ColumnName.ToString();
                ddlCountryCode.DataValueField = ds.Tables[0].Columns["MOBILECODE"].ColumnName.ToString();
                ddlCountryCode.DataBind();
            }

            #region load Occupation
            ds = western.GetOccupation(ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                dllOccupation.DataSource = ds.Tables[0].DefaultView;
                dllOccupation.DataTextField = ds.Tables[0].Columns["NAME"].ColumnName.ToString();
                dllOccupation.DataValueField = ds.Tables[0].Columns["NO"].ColumnName.ToString();
                dllOccupation.DataBind();
            }
            #endregion 

            #region Load country
            ds = western.GetCountry(ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                ddlCountryPay.DataSource = ds.Tables[0].DefaultView;
                ddlCountryPay.DataTextField = ds.Tables[0].Columns["PayoutCountryName"].ColumnName.ToString();
                ddlCountryPay.DataValueField = ds.Tables[0].Columns["PayoutCountry"].ColumnName.ToString();
                ddlCountryPay.DataBind();

                ddlNationality.DataSource = ds.Tables[1].DefaultView;
                ddlNationality.DataTextField = ds.Tables[1].Columns["COUNTRYNAME"].ColumnName.ToString();
                ddlNationality.DataValueField = ds.Tables[1].Columns["COUNTRYCODE"].ColumnName.ToString();
                ddlNationality.DataBind();

                ddlcountryofbirth.DataSource = ds.Tables[1].DefaultView;
                ddlcountryofbirth.DataTextField = ds.Tables[1].Columns["COUNTRYNAME"].ColumnName.ToString();
                ddlcountryofbirth.DataValueField = ds.Tables[1].Columns["COUNTRYCODE"].ColumnName.ToString();
                ddlcountryofbirth.DataBind();

                dllCountryPermanent.DataSource = ds.Tables[1].DefaultView;
                dllCountryPermanent.DataTextField = ds.Tables[1].Columns["COUNTRYNAME"].ColumnName.ToString();
                dllCountryPermanent.DataValueField = ds.Tables[1].Columns["COUNTRYCODE"].ColumnName.ToString();
                dllCountryPermanent.DataBind();

                ddlSenderCountry.DataSource = ds.Tables[1].DefaultView;
                ddlSenderCountry.DataTextField = ds.Tables[1].Columns["COUNTRYNAME"].ColumnName.ToString();
                ddlSenderCountry.DataValueField = ds.Tables[1].Columns["COUNTRYCODE"].ColumnName.ToString();
                ddlSenderCountry.DataBind();
            }

            #endregion

            #region load Purpose
            ds = western.GetPurpose(ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                ddlPurpose.DataSource = ds.Tables[0].DefaultView;
                ddlPurpose.DataTextField = ds.Tables[0].Columns["NAME"].ColumnName.ToString();
                ddlPurpose.DataValueField = ds.Tables[0].Columns["NO"].ColumnName.ToString();
                ddlPurpose.DataBind();
            }
            #endregion

            #region load KYC
            DataSet dsKYC = new DataSet();
            dsKYC = western.Getkyc(string.Empty, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                if (dsKYC.Tables.Count > 0)
                    if (dsKYC.Tables[0].Rows.Count > 0)
                    {
                        ddlKYC.DataSource = dsKYC;
                        ddlKYC.DataValueField = "KycId";
                        ddlKYC.DataTextField = "KycName";
                        ddlKYC.DataBind();
                    }
            }
            #endregion
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    }

    protected void ddlSenderAccount_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (lblTextError.Text == "")
            {
                string account = hdSenderAccount.Value = ddlSenderAccount.SelectedItem.Value.ToString();
                string Acctype = ddlSenderAccount.SelectedItem.Text.ToString();

                string ErrorCode = string.Empty;
                string ErrorDesc = string.Empty;
                string User = Session["userID"].ToString();
                Account acct = new Account();
                Hashtable ht = new Hashtable();
                ht = acct.GetInfoAccount(User, account, ref ErrorCode, ref ErrorDesc);
                hdTypeID.Value = ht["TYPEID"].ToString();
                ShowDD(account, ht);
            }
            else
            {
                pnTIB.Visible = false;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_IBBuyTopup_Widget", "LoadAccountInfo", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    private void ShowDD(string acctno, Hashtable htact)
    {
        try
        {
            if (htact["TYPEID"] != null)
            {
                hdActTypeSender.Value = htact["TYPEID"].ToString();
            }

            if (htact["AVAILABLEBALANCE"] != null && htact[SmartPortal.Constant.IPC.CCYID] != null)
            {
                hdavailablebalan.Value = htact["AVAILABLEBALANCE"].ToString();
                lblAvailableBal.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(SmartPortal.Common.Utilities.Utility.FormatStringCore(htact["AVAILABLEBALANCE"].ToString()), htact[SmartPortal.Constant.IPC.CCYID].ToString().Trim());
            }
            if (htact[SmartPortal.Constant.IPC.CCYID] != null)
            {
                lblAvailableBalCCYID.Text = htact[SmartPortal.Constant.IPC.CCYID].ToString().Trim();
            }
            if (htact[SmartPortal.Constant.IPC.FULLNAME] != null)
            {
                hdSenderName.Value = htact[SmartPortal.Constant.IPC.FULLNAME].ToString();
            }
            if (htact[SmartPortal.Constant.IPC.BRANCHID] != null)
            {
                hdSenderBranch.Value = htact[SmartPortal.Constant.IPC.BRANCHID].ToString();
            }

            if (hdTypeID.Value.Equals("WLM"))
            {
                hdSenderAccount.Value = htact["UNIQUEID"].ToString().Split('M')[0];
                hdTrancode.Value = "IBWLWESTERNUNION";
            }
            else
            {
                hdTrancode.Value = "IBWESTERNUNION";
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_IBBuyTopup_Widget", "LoadAccountInfo", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    protected void btncontinute_Click(object sender, EventArgs e)
    {
        try
        {
            lblTextError.Text = "";
            if (txtamountsend.Text.Trim() == "" || txtamountsend.Text.Equals("0"))
            {
                lblTextError.Text = "Invalid amount, amount must be number or greater than 0 ";
                return;
            }
            if (String.IsNullOrEmpty(txtmobilephone.Text) && String.IsNullOrEmpty(txttelephone.Text))
            {
                lblTextError.Text = "You must input telephone numer or mobiphone number.";
                return;
            }

            if (!String.IsNullOrEmpty(txtEmail.Text))
            {
                if (new EmailAddressAttribute().IsValid(txtEmail.Text))
                {

                }
                else
                {
                    lblTextError.Text = "Please spell Email address correctly";
                    return;
                }
            }
            if (SmartPortal.Common.Utilities.Utility.isDouble(hdavailablebalan.Value, true) < (SmartPortal.Common.Utilities.Utility.isDouble(txtamountsend.Text, true) + SmartPortal.Common.Utilities.Utility.isDouble(hdfee.Value, true)))
            {
                lblTextError.Text = Resources.labels.balancecompareequivalentamount;
                return;
            }

            

            txtissuedate.Text = lbendissuedate.Text;
            txtexpitydate.Text = lbendexdate.Text;
            txtdateofbirth.Text = lbenddob.Text;

            pnTIB.Visible = false;
            pnTIB1.Visible = true;
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_IBBuyTopup_Widget", "LoadAccountInfo", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    protected void btnback1_Click(object sender, EventArgs e)
    {
        try
        {
            lbendissuedate .Text = txtissuedate.Text;
            lbendexdate.Text = txtexpitydate.Text;
            lbenddob.Text = txtdateofbirth.Text;
            pnTIB.Visible = true;
            pnTIB1.Visible = false;
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_IBBuyTopup_Widget", "LoadAccountInfo", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    protected void ddlLoaiXacThuc_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnAction.Enabled = false;
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "key", "resetOTP();", true);
    }

    protected void btnRequest_Click(object sender, EventArgs e)
    {
        try
        {
            lblTextError.Text = "";
            #region check Date of birth
            try
            {
                DateTime dob1 = Convert.ToDateTime(txtdateofbirth.Text);
                int now = int.Parse(DateTime.Now.ToString("yyyyMMdd"));
                int dob = int.Parse(dob1.ToString("yyyyMMdd"));
                int age = (now - dob) / 10000;
                if (age < 15)
                {
                    lblTextError.Text = "Customers must be over 15 years old.";
                    return;
                }
            }
            catch
            {
                lblTextError.Text = "Invalid Date of birth.";
                return;
            }
            #endregion

            #region check Issue Date
            try
            {
                DateTime issdate = Convert.ToDateTime(txtissuedate.Text);
                if (issdate >= DateTime.Now)
                {
                    lblTextError.Text = "Invalid Issue Date.";
                    return;
                }
            }
            catch
            {
                lblTextError.Text = "Invalid Issue Date.";
                return;
            }
            #endregion

            #region check Expired Date
            try
            {
                DateTime exdate = Convert.ToDateTime(txtexpitydate.Text);
                int iss = int.Parse(DateTime.Now.ToString("yyyyMMdd"));
                int EX = int.Parse(exdate.ToString("yyyyMMdd"));
                int rate = (EX - iss) / 10000;
                if (rate > 500 || exdate < DateTime.Now)
                {
                    lblTextError.Text = "Invalid Expired Date.";
                    return;
                }
            }
            catch
            {
                lblTextError.Text = "Invalid Expired Date.";
                return;
            }
            #endregion

            DataTable dt = Utility.UploadFile(FUDocument, lblTextError);
            if (dt.Rows.Count > 0)
            {
                ViewState["TBLDOCUMENT"] = dt;
                lbfileupload.Text = FUDocument.FileName;
            }
            else
            {
                lblTextError.Text = Resources.labels.banvuilongchonfilexacminh;
                return;
            }

            #region Load information

            lblSenderFirstName.Text = txtsenderfirstname.Text;
            lblSenderLastName.Text = txtsenderlastname.Text;
            lblSenderAccount.Text = ddlSenderAccount.SelectedItem.ToString();
            lblBalanceSender.Text = lblAvailableBal.Text;
            lblBalanceccyidSender.Text = lblAvailableBalCCYID.Text;
            lbpayoutcountry.Text = ddlCountryPay.SelectedItem.ToString();

            lbSenderUnit.Text = txtSenderUnit.Text;
            lbSenderdistrict.Text = txtSenderdistrict.Text;
            lbsendercity.Text = txtSenderCity.Text;
            lbsenderProvince.Text = txtProvince.Text;
            lblpostcode.Text = txtpostcode.Text;
            lbSendercountry.Text = ddlSenderCountry.SelectedItem.ToString();
            lbmobiphonecoode.Text = ddlmobilecode.SelectedItem.ToString();
            lblContrycode.Text = ddlCountryCode.SelectedItem.ToString();
            lblTelephone.Text = txttelephone.Text;
            lblMobile.Text = txtmobilephone.Text;
            lblofficephone.Text = txtOfficephone.Text;
            lblemail.Text = txtEmail.Text;

            lbidtype.Text = ddlKYC.SelectedItem.ToString();
            lbIDnumber.Text = txtIDnumber.Text;
            lbexpiry.Text = Convert.ToDateTime(txtexpitydate.Text).ToString("dd/MM/yyyy");
            lbissuedate.Text = Convert.ToDateTime(txtissuedate.Text).ToString("dd/MM/yyyy");
            lbdateodbirth.Text = Convert.ToDateTime(txtdateofbirth.Text).ToString("dd/MM/yyyy");
            lbsourceoffund.Text = dllSourceFund.SelectedItem.Text;
            lboccupation.Text = dllOccupation.SelectedItem.ToString();
            lbnation.Text = ddlNationality.SelectedItem.Text;
            lbgender.Text = ddlGender.SelectedItem.Text;
            lbpurpose.Text = ddlPurpose.SelectedItem.Text;
            lblcoment.Text = txtcomment.Text;
            lnicunit.Text = txtIDunit.Text;
            lbicdistrict.Text = txtIDdistrict.Text;
            lciccity.Text = txtIDProvince.Text;
            lbCountry.Text = ddlSenderCountry.SelectedItem.ToString();
            lblicCoutryofbirth.Text = ddlcountryofbirth.SelectedItem.ToString();

            lbReceiverfistname.Text = txtReceiverfirstname.Text.Trim();
            lbReceiverlastname.Text = txtReceiverlastname.Text.Trim();

            lbtranferamount.Text = hdAmount.Value = txtamountsend.Text;
            lbtransferfee.Text = hdfee.Value;
            lbtransfertotal.Text = hdTotalAmount.Value;
            #endregion

            lbendexdate.Text = txtexpitydate.Text;
            lbendissuedate.Text = txtissuedate.Text;
            lbenddob.Text = txtdateofbirth.Text;

            pnConfirm.Visible = true;
            pnTIB.Visible = false;
            pnTIB1.Visible = false;
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    protected void btnApply_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["isSendFirst"] == null || (int)ViewState["isSendFirst"] == 0)
            {
                LoadLoaiXacThuc();
            }
            else
            {
                if (DateTime.Compare(DateTime.Now, Convert.ToDateTime(ViewState["timeReSendOTP"].ToString())) > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "key", "resetOTP();", true);
                }
            }
            pnConfirm.Visible = false;
            pnOTP.Visible = true;
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    private void LoadLoaiXacThuc()
    {
        try
        {
            ViewState["isSendFirst"] = 1;
            SmartPortal.IB.Transactions objTran = new SmartPortal.IB.Transactions();
            DataTable dt = objTran.LoadAuthenType(Session["userID"].ToString());
            ddlLoaiXacThuc.DataSource = dt;
            ddlLoaiXacThuc.DataTextField = "TYPENAME";
            ddlLoaiXacThuc.DataValueField = "AUTHENTYPE";
            ddlLoaiXacThuc.DataBind();
            ddlLoaiXacThuc.SelectedValue = "ESMSOTP";
            //ddlLoaiXacThuc.Enabled = false;

            //if (ddlLoaiXacThuc.SelectedValue.ToString() == "ESMSOTP")
            //{
            //    btnSendOTP_Click(null, EventArgs.Empty);
            //}
            btnSendOTP.Text = Resources.labels.send;
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    protected void btnSendOTP_Click(object sender, EventArgs e)
    {
        try
        {
            ddlLoaiXacThuc.Enabled = false;
            btnAction.Enabled = true;
            btnSendOTP.Text = Resources.labels.resend;
            if (ddlLoaiXacThuc.SelectedValue.ToString() == "ESMSOTP")
            {
                SmartPortal.SEMS.OTP.SendSMSOTP(Session["userID"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
            }
            else
            {
                SmartPortal.SEMS.OTP.SendSMSOTPCORP(Session["userID"].ToString(), ddlLoaiXacThuc.SelectedValue.ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
                lbnotice.Visible = true;
            }
            if (IPCERRORCODE != "0")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "key", "resetOTP();", true);
            }
            switch (IPCERRORCODE)
            {
                case "0":
                    int time;
                    if (ddlLoaiXacThuc.SelectedValue.ToString() == "ESMSOTP")
                    {
                        time = !String.IsNullOrEmpty(ConfigurationManager.AppSettings["OTPtimeexpires"]) ? int.Parse(ConfigurationManager.AppSettings["OTPtimeexpires"].ToString()) : 20;
                    }
                    else
                    {
                        time = !String.IsNullOrEmpty(ConfigurationManager.AppSettings["OTPCorptimeexpires"]) ? int.Parse(ConfigurationManager.AppSettings["OTPCorptimeexpires"].ToString()) : 20;
                    }
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "key", "countdownOTP(" + time + ");", true);
                    ViewState["timeReSendOTP"] = DateTime.Now.AddSeconds(time);
                    break;
                case "7003":
                    lblTextError.Text = Resources.labels.notregotp;
                    btnAction.Enabled = false;
                    break;
                default:
                    lblTextError.Text = IPCERRORDESC;
                    btnAction.Enabled = false;
                    break;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    //protected void btnSendOTP_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        try
    //        {
    //            lblTextError.Text = string.Empty;
    //            ddlLoaiXacThuc.Enabled = false;
    //            btnAction.Enabled = true;
    //            SmartPortal.SEMS.OTP.SendSMSOTP(Session["userID"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
    //            if (IPCERRORCODE != "0")
    //            {
    //                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "key", "resetOTP();", true);
    //            }
    //            switch (IPCERRORCODE)
    //            {
    //                case "0":
    //                    int time = !String.IsNullOrEmpty(ConfigurationManager.AppSettings["OTPtimeexpires"]) ? int.Parse(ConfigurationManager.AppSettings["OTPtimeexpires"].ToString()) : 20;
    //                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "key", "countdownOTP(" + time + ");", true);
    //                    ViewState["timeReSendOTP"] = DateTime.Now.AddSeconds(time);
    //                    break;
    //                case "7003":
    //                    lblTextError.Text = Resources.labels.notregotp;
    //                    btnAction.Enabled = false;
    //                    break;
    //                default:
    //                    lblTextError.Text = IPCERRORDESC;
    //                    btnAction.Enabled = false;
    //                    break;
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
    //            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
    //        SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
    //    }
    //}

    protected void btnBackConfirm_Click(object sender, EventArgs e)
    {
        try
        {
            pnOTP.Visible = false;
            pnConfirm.Visible = true;
            pnTIB.Visible = false;
            pnTIB1.Visible = false;
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    protected void btnBackTRF_Click(object sender, EventArgs e)
    {
        try
        {
            pnOTP.Visible = false;
            pnConfirm.Visible = false;
            pnTIB.Visible = false;
            pnTIB1.Visible = true;
            txtissuedate.Text = lbendissuedate.Text;
            txtexpitydate.Text = lbendexdate.Text;
            txtdateofbirth.Text = lbenddob.Text;


        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    protected void btnAction_Click(object sender, EventArgs e)
    {
        try
        {
            #region Prepare Transfers
            DataTable tbldocument = (DataTable)ViewState["TBLDOCUMENT"];
            Hashtable hb = new Hashtable();
            hb.Add("ACCTNO", hdSenderAccount.Value);
            hb.Add("AMOUNT", SmartPortal.Common.Utilities.Utility.isDouble(hdAmount.Value, true));
            hb.Add("USERID", Session["userID"].ToString());
            hb.Add("CCYID", "USD");
            hb.Add("SENDERFIRSTNAME", lblSenderFirstName.Text);
            hb.Add("SENDERLASTNAME", lblSenderLastName.Text);
            hb.Add("SENDERNAME", lblSenderLastName.Text + " " + lblSenderFirstName.Text);
            hb.Add("PAYCOUNTRY", ddlCountryPay.SelectedValue.ToString());

            hb.Add("SENDERUNIT", txtSenderUnit.Text.Trim());
            hb.Add("SENDERDISTRICT", txtSenderdistrict.Text.Trim());
            hb.Add("SENDERCITY", txtSenderCity.Text.Trim());
            hb.Add("SENDERPROVINCE", txtProvince.Text.Trim());
            hb.Add("SENDERCOUNTRY", ddlSenderCountry.SelectedValue.Trim());
            hb.Add("POSTCODE", txtpostcode.Text.Trim());
            hb.Add("COUNTRYCODE", ddlCountryCode.SelectedValue);
            hb.Add("TELEPHONE", txttelephone.Text);
            hb.Add("MOBILEPHONECODE", ddlmobilecode.SelectedValue);
            hb.Add("PHONENO", txtmobilephone.Text);
            hb.Add("EMAIL", txtEmail.Text);

            hb.Add("ICTYPE", ddlKYC.SelectedValue);
            hb.Add("ICNUMBER", txtIDnumber.Text.Trim());
            hb.Add("ISSUECOUNTRY", "LA");
            hb.Add("EXDATE", lbexpiry.Text.Trim());
            hb.Add("ISSUEDATE", lbissuedate.Text.Trim());
            hb.Add("DATEOFBIRTH", lbdateodbirth.Text.Trim());
            hb.Add("OFFICEPHONE", txtOfficephone.Text.Trim());
            hb.Add("OCCUPATION", dllOccupation.SelectedValue);
            hb.Add("PURPOSE", ddlPurpose.SelectedValue);
            hb.Add("SOURCEOFUND", dllSourceFund.SelectedValue);
            hb.Add("ICUNIT", txtIDunit.Text.Trim());
            hb.Add("ICDISTRICT", txtIDdistrict.Text.Trim());
            hb.Add("ICPROVINCE", txtIDProvince.Text.Trim());
            hb.Add("ICCOUNTRY", dllCountryPermanent.SelectedValue.Trim());
            hb.Add("BIRTHCOUTRY", ddlcountryofbirth.SelectedValue);
            hb.Add("NATIONLITY", ddlNationality.SelectedValue);
            hb.Add("GENDER", ddlGender.SelectedValue);
            hb.Add("COMMENT", txtcomment.Text.Trim());
            if (tbldocument != null)
            {
                hb.Add("DOCUMENT", tbldocument);
            }

            hb.Add("REFIRSTNAME", txtReceiverfirstname.Text.Trim());
            hb.Add("RELASTNAME", txtReceiverlastname.Text.Trim());
            hb.Add("RECEIVERNAME", txtReceiverfirstname.Text.Trim() + " " + txtReceiverlastname.Text.Trim());

            #endregion

            Hashtable hashtable = new Hashtable();
            WesternUnion western = new WesternUnion();
            hashtable = western.WesternUnionTrans(hdTrancode.Value, hb, ddlLoaiXacThuc.SelectedValue, txtOTP.Text.Trim(), ref IPCERRORCODE, ref IPCERRORDESC);

            if (!hashtable[SmartPortal.Constant.IPC.IPCERRORCODE].Equals("0"))
            {
                txtOTP.Text = "";
                btnPrint.Visible = false;
                btnView.Visible = false;
                switch (hashtable[SmartPortal.Constant.IPC.IPCERRORCODE].ToString().Trim())
                {
                    case SmartPortal.Constant.IPC.ERRORCODE.OTPINVALID:
                        lblTextError.Text = hashtable[SmartPortal.Constant.IPC.IPCERRORDESC].ToString().Trim();
                        if (DateTime.Compare(DateTime.Now, Convert.ToDateTime(ViewState["timeReSendOTP"].ToString())) > 0)
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "key", "resetOTP();", true);
                        }
                        return;
                    case SmartPortal.Constant.IPC.ERRORCODE.WATTINGBANKAPPROVE:
                        lblTextError.Text = Resources.labels.wattingbankapprove;
                        break;
                    case SmartPortal.Constant.IPC.ERRORCODE.WATTINGPARTOWNERAPPROVE:
                        lblTextError.Text = Resources.labels.wattingpartownerapprove;
                        break;
                    case SmartPortal.Constant.IPC.ERRORCODE.WATTINGUSERAPPROVE:
                        lblTextError.Text = Resources.labels.wattinguserapprove;
                        break;
                    case SmartPortal.Constant.IPC.ERRORCODE.NOTREGOTP:
                        lblTextError.Text = hashtable[SmartPortal.Constant.IPC.IPCERRORDESC].ToString().Trim();
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "key", "resetOTP();", true);
                        return;
                    case SmartPortal.Constant.IPC.ERRORCODE.AUTHENTYPEINVALID:
                        lblTextError.Text = Resources.labels.authentypeinvalid;
                        return;
                    case "00":
                        lblTextError.Text = Resources.labels.transactionsuccessful;
                        break;
                    default:
                        lblTextError.Text = string.IsNullOrEmpty(hashtable[SmartPortal.Constant.IPC.IPCERRORDESC].ToString()) ? "Transaction error!" : hashtable[SmartPortal.Constant.IPC.IPCERRORDESC].ToString();
                        return;
                }
            }

            Hashtable tmpl = new Hashtable();
            if (IPCERRORCODE == "0" || IPCERRORCODE == "00")
            {
                btnPrint.Visible = true;
                btnView.Visible = true;
                lblTextError.Text = Resources.labels.transactionsuccessful;
                tmpl.Add("IPCTRANSID", hashtable["IPCTRANSID"].ToString());
                lblEndTransactionNo.Text = hashtable["IPCTRANSID"].ToString();
            }

            #region Load transaction success

            Account acct = new Account();
            Hashtable hbaccno = acct.GetInfoAccount(Session["userID"].ToString(), ddlSenderAccount.SelectedItem.ToString().Trim(), ref IPCERRORCODE, ref IPCERRORDESC);
            double total = SmartPortal.Common.Utilities.Utility.isDouble(hdAmount.Value, true) + SmartPortal.Common.Utilities.Utility.isDouble(hdfee.Value, true);
            if (IPCERRORCODE.Equals("0"))
            {
                lblEndSenderAccount.Text = ddlSenderAccount.SelectedItem.ToString().Trim();
                lblEndBalanceSender.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(SmartPortal.Common.Utilities.Utility.FormatStringCore(hbaccno["AVAILABLEBALANCE"].ToString()), hbaccno["CCYID"].ToString());
                lblEndAmountCCYID.Text = lblBalanceCCYID.Text = hbaccno["CCYID"].ToString();

                #region Load print

                tmpl.Add("TRANDATE", DateTime.Now.ToString());
                tmpl.Add("SENDERNAME", lblSenderFirstName.Text + " " + lblSenderLastName.Text);
                tmpl.Add("SENDERACCNO", ddlSenderAccount.SelectedItem.ToString().Trim());
                tmpl.Add("PAYCOUNTRY", ddlCountryPay.SelectedValue.ToString());

                tmpl.Add("SENDERUNIT", txtSenderUnit.Text.Trim());
                tmpl.Add("SENDERDISTRICT", txtSenderdistrict.Text.Trim());
                tmpl.Add("SENDERCITY", txtSenderCity.Text.Trim());
                tmpl.Add("SENDERPROVINCE", txtProvince.Text.Trim());
                tmpl.Add("SENDERCOUNTRY", ddlSenderCountry.SelectedItem.ToString());
                tmpl.Add("POSTCODE", txtpostcode.Text.Trim());
                tmpl.Add("COUNTRYCODE", ddlCountryCode.SelectedItem.ToString());
                tmpl.Add("TELEPHONE", txttelephone.Text);
                tmpl.Add("MOBILEPHONECODE", ddlmobilecode.SelectedItem.ToString());
                tmpl.Add("PHONENO", txtmobilephone.Text);
                tmpl.Add("EMAIL", txtEmail.Text);

                tmpl.Add("ICTYPE", ddlKYC.SelectedItem.ToString());
                tmpl.Add("ICNUMBER", txtIDnumber.Text.Trim());
                tmpl.Add("ISSUECOUNTRY", "LA");
                tmpl.Add("EXDATE", txtexpitydate.Text.Trim());
                tmpl.Add("ISSUEDATE", txtissuedate.Text.Trim());
                tmpl.Add("DATEOFBIRTH", txtdateofbirth.Text.Trim());
                tmpl.Add("OFFICEPHONE", txtOfficephone.Text.Trim());
                tmpl.Add("OCCUPATION", dllOccupation.SelectedItem.ToString());
                tmpl.Add("PURPOSE", ddlPurpose.SelectedValue);
                tmpl.Add("SOURCEOFUND", dllSourceFund.SelectedItem.ToString());
                tmpl.Add("ICUNIT", txtIDunit.Text.Trim());
                tmpl.Add("ICDISTRICT", txtIDdistrict.Text.Trim());
                tmpl.Add("ICPROVINCE", txtIDProvince.Text.Trim());
                tmpl.Add("ICCOUNTRY", dllCountryPermanent.SelectedItem.ToString());
                tmpl.Add("BIRTHCOUTRY", ddlcountryofbirth.SelectedItem.ToString());
                tmpl.Add("NATIONLITY", ddlNationality.SelectedItem.ToString());
                tmpl.Add("GENDER", ddlGender.SelectedItem.ToString());
                tmpl.Add("COMMENT", txtcomment.Text.Trim());
                tmpl.Add("FILE", lbfileupload.Text);

                tmpl.Add("FIRSTNAME", lbReceiverfistname.Text);
                tmpl.Add("LASTNAME", lbReceiverlastname.Text);
                tmpl.Add("AMOUNT", SmartPortal.Common.Utilities.Utility.FormatMoney(hdAmount.Value, "USD"));
                tmpl.Add("feeSenderAmt", hdfee.Value);
                tmpl.Add("TOTAL", SmartPortal.Common.Utilities.Utility.FormatMoney(total.ToString(), "USD"));
                tmpl.Add("RCCYID", hdexchangeratecciyd.Value);
                tmpl.Add("STATUS", "Transfer send to Western union");

                Session["print"] = tmpl;
                #endregion
            }

            lblEndSenderName.Text = txtsenderfirstname.Text;
            lblEndSenderLastName.Text = txtsenderlastname.Text;
            lbendpaycountry.Text = ddlCountryPay.SelectedItem.ToString();
            lbendunit.Text = txtSenderUnit.Text;
            lbenddistrict.Text = txtSenderdistrict.Text;
            lbendCity.Text = txtSenderCity.Text;
            lbendprovince.Text = txtProvince.Text;
            lbendcountry.Text = ddlSenderCountry.SelectedItem.ToString();
            lbendtelephone.Text = txttelephone.Text;
            lbendmobilephone.Text = txtmobilephone.Text;
            lbendemail.Text = txtEmail.Text;
            lblendpostcode.Text = txtpostcode.Text;
            lblendcountrycode.Text = ddlCountryCode.SelectedItem.ToString();
            lblendmobilephonecode.Text = ddlmobilecode.SelectedItem.ToString();

            lbendidtype.Text = ddlKYC.SelectedItem.ToString();
            lbendidnumber.Text = txtIDnumber.Text;
            lbendexdate.Text = lbexpiry.Text;
            lbendissuedate.Text = lbissuedate.Text;
            lbenddob.Text = lbdateodbirth.Text;
            lblendoffcice.Text = txtOfficephone.Text;
            lbendsource.Text = dllSourceFund.SelectedItem.Text;
            lbendocc.Text = dllOccupation.SelectedItem.ToString();
            lbendnation.Text = ddlNationality.SelectedItem.Text;
            lbendgender.Text = ddlGender.SelectedItem.Text;
            lbendpurpose.Text = ddlPurpose.SelectedItem.Text;
            lblendiccomment.Text = txtcomment.Text;
            lblendicunit.Text = txtIDunit.Text;
            lblendicdistrict.Text = txtIDdistrict.Text;
            lblendiccity.Text = txtIDProvince.Text;
            lblendiccountry.Text = ddlSenderCountry.SelectedItem.ToString();
            lbliccountryofbirth.Text = ddlcountryofbirth.SelectedItem.ToString();
            lbendfileupload.Text = lbfileupload.Text;


            lblEndReceiverFirstName.Text = lbReceiverfistname.Text;
            lblEndReceiverLastName.Text = lbReceiverlastname.Text;

            lblEndDateTime.Text = DateTime.Now.ToString();
            lblEndPhiAmount.Text = hdfee.Value;
            lblEndPhi.Text = "Sender";
            lblEndAmount.Text = hdAmount.Value;
            lbendtotal.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(total.ToString(), "USD");
            #endregion


            #region Visable pannel
            pnTIB.Visible = false;
            pnTIB1.Visible = false;
            pnOTP.Visible = false;
            pnConfirm.Visible = false;
            pnResult.Visible = true;
            #endregion


        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=IB_WESTERNUNION"));
    }

    protected void txtamountsend_TextChanged(object sender, EventArgs e)
    {
        try
        {
            lblTextError.Text = "";
            if (String.IsNullOrEmpty(txtamountsend.Text))
            {
                lblTextError.Text = "Invalid amount, amount must be number or greater than 0 ";
                lbsendfee.Text = "0.00";
                lbtotalAmount.Text = "0.00";
                btncontinute.Enabled = false;
            }
            else
            {
                btncontinute.Enabled = true;
                object[] _object = new object[] { Session["userID"].ToString(), Utility.KillSqlInjection(hdTrancode.Value.Trim()), SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtamountsend.Text.Trim(), "USD"), ddlSenderAccount.SelectedValue, ddlSenderAccount.SelectedValue, "USD", "", ddlCountryPay.SelectedValue };
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                hasInput.Add("IPCTRANCODE", "IB_WUGETFEE");
                hasInput.Add("SOURCEID", "IB");
                hasInput.Add("SOURCETRANREF", "010");
                hasInput.Add("TRANDESC", "Get WU FEE");
                hasInput.Add("PARA", _object);
                hasOutput = new SmartPortal.IB.Transactions().CommonProcessTrans(hasInput, ref IPCERRORCODE, ref IPCERRORDESC);

                if (IPCERRORCODE == "0")
                {
                    hdfee.Value = lbsendfee.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(hasOutput["feeSenderAmt"].ToString(), "USD");
                    lbtotalAmount.Text = hdTotalAmount.Value = SmartPortal.Common.Utilities.Utility.FormatMoney((SmartPortal.Common.Utilities.Utility.isDouble(hdfee.Value, true) + SmartPortal.Common.Utilities.Utility.isDouble(txtamountsend.Text.Trim(), true)).ToString(), "USD");

                }
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    protected void ddlCountryPay_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtamountsend_TextChanged(sender, e);
    }
}
