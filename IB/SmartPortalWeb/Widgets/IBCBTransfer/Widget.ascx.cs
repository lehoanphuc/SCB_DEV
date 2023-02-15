using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Globalization;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Image = System.Web.UI.WebControls.Image;

using SmartPortal.ExceptionCollection;
using SmartPortal.Common.Utilities;
using SmartPortal.IB;
using SmartPortal.DAL;
using System.IO;
using System.Collections.Generic;
using System.Data.SqlClient;
using SmartPortal.Common;
using System.Threading;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Drawing;
using SmartPortal.Model;

public partial class Widgets_IBCBTransfer_Widget : WidgetBase
{
    
    public DataTable TBLDOCUMENTCROSS
    {
        get { return ViewState["TBLDOCUMENTCROSS"] != null ? (DataTable)ViewState["TBLDOCUMENTCROSS"] : createtable(); }
        set { ViewState["TBLDOCUMENTCROSS"] = TBLDOCUMENTCROSS; }
    }
    public DataTable TBLDOCUMENTCORP
    {
        get { return ViewState["TBLDOCUMENTCORP"] != null ? (DataTable)ViewState["TBLDOCUMENTCORP"] : createtable(); }
        set { ViewState["TBLDOCUMENTCORP"] = TBLDOCUMENTCORP; }
    }
    static Account acct = new Account();
    string IPCERRORCODE = string.Empty;
    string IPCERRORDESC = string.Empty;

    public string IPCTRANCODE = "IBCBTRANSFER";

    private DataTable createtable()
    {
        DataTable dtdocument = new DataTable();
        DataColumn filename = new DataColumn("FileName");
        DataColumn fileexten = new DataColumn("FileExtension");
        DataColumn fileverify = new DataColumn("FileVerify");
        DataColumn filesize = new DataColumn("FileSize");
        DataColumn desc = new DataColumn("Description");
        DataColumn base64 = new DataColumn("Base64");

        dtdocument.Columns.Add(filename);
        dtdocument.Columns.Add(fileexten);
        dtdocument.Columns.Add(fileverify);
        dtdocument.Columns.Add(filesize);
        dtdocument.Columns.Add(desc);
        dtdocument.Columns.Add(base64);
        return dtdocument;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            
            lblError.Text = String.Empty;
            txtExpDate.Attributes.Add("max", "2200-01-01");
            txtIssDate.Attributes.Add("max", DateTime.Now.ToString("yyyy-MM-dd"));
            txtAmount.Attributes.Add("onkeyup", "ntt('" + txtAmount.ClientID + "','" + lblText.ClientID + "',event)");
            if (!IsPostBack)
            {
                hdUserType.Value = Session["accType"].ToString();
                checktypeAccount();
                //LoadBankandSwiftCode();
                LoadPurpose();
                LoadddlChosefee();
                loadIDType();
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }

    }
    private void checktypeAccount()
    {
        try
        {
            if (Session["accType"].ToString() != "IND")
            {
                pnRemitterIND.Visible = false;
                pnRemitterMTK.Visible = true;
            }
            else
            {
                pnRemitterIND.Visible = true;
                pnRemitterMTK.Visible = false;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    private void LoadPurpose()
    {
        try
        {
            ddlpaymentpurpose.Items.Insert(0, new ListItem("Personal Transfer/Support Family", "1"));
            ddlpaymentpurpose.Items.Insert(1, new ListItem("Education payment", "2"));
            ddlpaymentpurpose.Items.Insert(2, new ListItem("Goods Payment/Servide Payment/Etc. Payment", "3"));


            ddlpaymentpurpose_SelectedIndexChanged(null, EventArgs.Empty);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    private void LoadddlChosefee()
    {
        try
        {
            ddlchoosefee.Items.Insert(0, new ListItem(Resources.labels.feesha, "SHA"));
            ddlchoosefee.Items.Insert(1, new ListItem(Resources.labels.applicant, "OUR"));
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    private void LoadBankBySwiftCode()
    {
        try
        {
            DataSet ds = new DataSet();

            Hashtable hasInput = new Hashtable();
            Hashtable hasOutput = new Hashtable();

            hasInput = new Hashtable(){
                {"IPCTRANCODE", "CBGETBANKBYSWIFTCODE"},
                {"SERVICEID", "IB"},
                {"SOURCEID", "IB"},
                {"REVERSAL", "N"},
                {"SWIFTCODE", Utility.KillSqlInjection(txtSwiftCode.Text.Trim())}
            };
            hasOutput = new SmartPortal.IB.Transactions().CommonProcessTrans(hasInput, ref IPCERRORCODE, ref IPCERRORDESC);

            if (IPCERRORCODE.Equals("0"))
            {
                ds = (DataSet)hasOutput[SmartPortal.Constant.IPC.DATASET];
                if(ds!= null)
                {
                    DataTable dt = ds.Tables[0];
                    if(dt.Rows.Count > 0)
                    {
                        txtBankName.Text = dt.Rows[0]["Text"].ToString();
                        string value = dt.Rows[0]["Value"].ToString();
                        string[] values = value.Split(',');
                        hdSelectedBankID.Value = values[0].ToString();
                        hdSelectefCCYID.Value = values[2].ToString();
                        ddlCcyID.DataSource = dt;
                        ddlCcyID.DataTextField = "Currency";
                        ddlCcyID.DataValueField = "Currency";
                        ddlCcyID.DataBind();
                        //Core Down
                        LoadAccount();
                    }
                    else
                    {
                        lblError.Text = Resources.labels.invalidswiftcode;
                        return;
                    }
                }
            }
            else
            {
                lblError.Text = IPCERRORDESC;
                return;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    private void LoadAccount()
    {
        try
        {
            DataSet ds = new DataSet();
            Account acct = new Account();
            ds = acct.GetListOfAccounts(Session["userID"].ToString(), IPCTRANCODE, "IBCBTransferWLTOBANK", "DD,CD", "", ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE.Equals("0"))
            {
                ds.Tables[0].DefaultView.RowFilter = "TYPEID IN ('DD', 'CD') AND STATUSCD in ('A','Y') AND CCYID in ('" + hdSelectefCCYID.Value + "')";
                if (ds.Tables[0].DefaultView.Count == 0)
                {
                    lblError.Text = "You have no account with currency is USD or THB";
                    pnBeneficiary.Visible = false;
                    pnpaymentinfor.Visible = false;
                    pnPurpose.Visible = false;
                    pnRemitterIND.Visible = false;
                    pnRemitterMTK.Visible = false;
                }
                else
                {
                    ddlSenderAccount.DataSource = ds.Tables[0].DefaultView;
                    ddlSenderAccount.DataTextField = "ACCOUNTNO";
                    ddlSenderAccount.DataValueField = "UNIQUEID";
                    ddlSenderAccount.DataBind();
                    ddlSenderAccount_SelectedIndexChanged(null, EventArgs.Empty);
                }
            }
            else
            {
                throw new SmartPortal.ExceptionCollection.IPCException(IPCERRORCODE);
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    private void LoadAccountInfo()
    {
        try
        {
            string account = ddlSenderAccount.SelectedItem.Value.ToString();
            Hashtable hashtable = acct.GetInfoAccount(Session["userID"].ToString(), account, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE.Equals("0"))
            {
                hdBalanceSender.Value = lblAvailableBal.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(SmartPortal.Common.Utilities.Utility.FormatStringCore(hashtable["AVAILABLEBALANCE"].ToString()), hashtable["CCYID"].ToString());
                lblAvailableBalCCYID.Text = hashtable["CCYID"].ToString();
                lblCurrency.Text = hashtable["CCYID"].ToString();
                hdTypeID.Value = hashtable["TYPEID"].ToString();
                hdSenderBranch.Value = hashtable["BRANCHID"].ToString();
                hdFullName.Value = hashtable["FULLNAME"].ToString();
            }
            else
            {
                throw new SmartPortal.ExceptionCollection.IPCException(IPCERRORCODE);
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void rptDocument_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            DataTable dt = (DataTable)ViewState["TBLDOCUMENTCROSS"];
            dt.Rows.RemoveAt(e.Item.ItemIndex);
            ViewState["TBLDOCUMENTCROSS"] = dt;

            if(dt.Rows.Count != 0)
            {
                rptDocument.DataSource = dt;
                rptDocument.DataBind();
            }
            else
            {
                rptDocument.Visible = false;
            }
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
            btnAction.Enabled = false;
            SmartPortal.IB.Transactions objTran = new SmartPortal.IB.Transactions();
            DataTable dt = objTran.LoadAuthenType(Session["userID"].ToString());
            ddlLoaiXacThuc.DataSource = dt;
            ddlLoaiXacThuc.DataTextField = "TYPENAME";
            ddlLoaiXacThuc.DataValueField = "AUTHENTYPE";
            ddlLoaiXacThuc.DataBind();

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
                    lblError.Text = Resources.labels.notregotp;
                    btnAction.Enabled = false;
                    break;
                default:
                    lblError.Text = IPCERRORDESC;
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

    protected void ddlLoaiXacThuc_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnAction.Enabled = false;
        //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "key", "resetOTP();", true);
        //pnSendOTP.Visible = ddlLoaiXacThuc.SelectedValue.ToString() == "ESMSOTP";
    }

    
    protected void btnNext_Click(object sender, EventArgs e)
    {
        try
        {
            if (validate1())
            {
                pnBeneficiary.Visible = false;
                pnRemitterIND.Visible = false;
                pnRemitterMTK.Visible = false;
                pnPurpose.Visible = true;
                if (Session["accType"].ToString() != "IND")
                {
                    pnuploadfileCorp.Visible = true;
                }
                else
                {
                    pnuploadfileCorp.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    protected void btnBack1_Click(object sender, EventArgs e)
    {

        try
        {
            pnBeneficiary.Visible = true;
            checktypeAccount();
            pnPurpose.Visible = false;
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    protected void btnNext1_Click(object sender, EventArgs e)
    {
        try
        {
            if (validate2())
            {
                pnBeneficiary.Visible = false;
                pnRemitterIND.Visible = false;
                pnRemitterMTK.Visible = false;
                pnPurpose.Visible = false;
                pnpaymentinfor.Visible = true;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    protected void btnBack2_Click(object sender, EventArgs e)
    {
        try
        {
            pnBeneficiary.Visible = false;
            pnRemitterIND.Visible = false;
            pnRemitterMTK.Visible = false;
            pnPurpose.Visible = true;
            pnpaymentinfor.Visible = false;
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
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
                LoadLoaiXacThuc();
            }
            pnOTP.Visible = true;
            pnConfirm.Visible = false;
            pnBeneficiary.Visible = false;
            pnRemitterIND.Visible = false;
            pnRemitterMTK.Visible = false;
            pnPurpose.Visible = false;
            pnpaymentinfor.Visible = false;
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    protected void ddlSenderAccount_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            LoadAccountInfo();
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void loadIDType()
    {
        #region load KYC
        DataSet dsKYC = new DataSet();
        dsKYC = new WesternUnion().Getkyc(string.Empty, ref IPCERRORCODE, ref IPCERRORDESC);
        if (IPCERRORCODE == "0")
        {
            if (dsKYC.Tables.Count > 0)
                if (dsKYC.Tables[0].Rows.Count > 0)
                {
                    ddlIdType.DataSource = dsKYC;
                    ddlIdType.DataValueField = "KycId";
                    ddlIdType.DataTextField = "KycName";
                    ddlIdType.DataBind();
                }
        }
        #endregion
    }
    //protected void ddlBank_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        string value = ddlBank.SelectedValue.ToString();
    //        string[] values = value.Split(',');
    //        hdSelectedBankID.Value = values[0].ToString();
    //        //txtSwiftCode.Text = values[1].ToString();
    //        hdSelectefCCYID.Value = values[2].ToString();
    //        //Core Down
    //        LoadAccount();
    //    }
    //    catch (Exception ex)
    //    {
    //        SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
    //        SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
    //    }
    //}
    private bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email.Trim();
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    protected Boolean validate1()
    {
        try
        {
            if(Session["accType"].ToString() == "IND")
            {
                string sendername = Utility.KillSqlInjection(txtSenderName.Text);
                string senderphone = Utility.KillSqlInjection(txtSenderPhone.Text.Trim());
                string senderadd = Utility.KillSqlInjection(txtSenderAddress.Text);
                string idnumber = Utility.KillSqlInjection(txtIdNumber.Text);
                string ExpiredDate = Utility.KillSqlInjection(txtExpDate.Text);
                string IssueDate = Utility.KillSqlInjection(txtIssDate.Text);
                if (!String.IsNullOrEmpty(sendername))
                {
                    if (!String.IsNullOrEmpty(idnumber))
                    {
                        if (!String.IsNullOrEmpty(ExpiredDate))
                        {
                            #region check Expired Date
                            try
                            {
                                DateTime date = Convert.ToDateTime(Utility.KillSqlInjection(txtExpDate.Text));
                                
                                if (date <= DateTime.Now)
                                {
                                    lblError.Text = "Invalid Expired Date.";
                                    return false;
                                }
                            }
                            catch
                            {
                                lblError.Text = "Invalid Expired Date.";
                                return false;
                            }
                            #endregion
                            if (!String.IsNullOrEmpty(IssueDate))
                            {
                                #region check Issue Date
                                try
                                {
                                    DateTime issdate = Convert.ToDateTime(Utility.KillSqlInjection(txtIssDate.Text));
                                    if (issdate >= DateTime.Now || issdate >= Convert.ToDateTime(Utility.KillSqlInjection(txtExpDate.Text)))
                                    {
                                        lblError.Text = "Invalid Issue Date.";
                                        return false;
                                    }
                                }
                                catch
                                {
                                    lblError.Text = "Invalid Issue Date.";
                                    return false;
                                }
                                #endregion
                                if (!String.IsNullOrEmpty(senderphone))
                                {
                                    if (!String.IsNullOrEmpty(senderadd))
                                    {
                                    }
                                    else
                                    {
                                        lblError.Text = Resources.labels.bancannhapdiachi;
                                        return false;
                                    }
                                }
                                else
                                {
                                    lblError.Text = Resources.labels.bannhapsodienthoai;
                                    return false;
                                }
                            }
                            else
                            {
                                lblError.Text = Resources.labels.inputissuedate;
                                return false;
                            }
                        }
                        else
                        {
                            lblError.Text = Resources.labels.inputexpireddate;
                            return false;
                        }
                        
                    }
                    else
                    {
                        lblError.Text = Resources.labels.inputidnumber;
                        return false;
                    }
                    
                }
                else
                {
                    lblError.Text = Resources.labels.bancannhaptennguoigui;
                    return false;
                }
            }
            else
            {
                string entername = Utility.KillSqlInjection(txtEnterName.Text);
                string enterphone = Utility.KillSqlInjection(txtEnterPhone.Text.Trim());
                string enteraddress = Utility.KillSqlInjection(txtEnterAddress.Text);
                string enterlicense = Utility.KillSqlInjection(txtEnterLicense.Text);
                string entertax = Utility.KillSqlInjection(txtEnterTax.Text);
                if (!String.IsNullOrEmpty(entername))
                {
                    if (!String.IsNullOrEmpty(enterphone))
                    {
                        if (!String.IsNullOrEmpty(enteraddress))
                        {
                            if (!String.IsNullOrEmpty(enterlicense) && !String.IsNullOrEmpty(entertax))
                            {
                            }
                            else
                            {
                                lblError.Text = Resources.labels.bancannhapmasothuegpkd;
                                return false;
                            }
                        }
                        else
                        {
                            lblError.Text = Resources.labels.inputenteraddress;
                            return false;
                        }
                    }
                    else
                    {
                        lblError.Text = Resources.labels.inputenterphone;
                        return false;
                    }
                }
                else
                {
                    lblError.Text = Resources.labels.inputentername;
                    return false;
                }
            }
            string benname = Utility.KillSqlInjection(txtReceiverName.Text);
            string benaccount = Utility.KillSqlInjection(txtReceiverAcc.Text);
            string benaddress = Utility.KillSqlInjection(txtReceiverAdd.Text);
            string benphone = Utility.KillSqlInjection(txtReceiverPhone.Text);
            string benemail = Utility.KillSqlInjection(txtReceiverMail.Text);
            if (!String.IsNullOrEmpty(benname))
            {
                if (!String.IsNullOrEmpty(benaccount))
                {
                    if (!String.IsNullOrEmpty(benaddress))
                    {
                      
                            if (!String.IsNullOrEmpty(benemail))
                            {
                                if (!IsValidEmail(benemail))
                                {
                                    lblError.Text = Resources.labels.emailkhongdinhdang;
                                    return false;
                                }
                            }
                    }
                    else
                    {
                        lblError.Text = Resources.labels.inputbenaddress;
                        return false;
                    }
                }
                else
                {
                    lblError.Text = Resources.labels.inputbenaccount;
                    return false;
                }
            }
            else
            {
                lblError.Text = Resources.labels.inputbenname;
                return false;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
        return true;
    }
    protected Boolean validate2()
    {
        try
        {
            //File Corp
            if (Session["accType"].ToString() != "IND")
            {
                if (FUDocument.HasFiles)
                {
                    DataTable dt = createtable();

                    double maxfilesize = 1 * 1024 * 1024;
                    string[] allowedExtensions = { ".PDF", ".PNG", ".JPG", ".JPEG" };
                    bool checkExtensions = false;

                    foreach (HttpPostedFile file in FUDocument.PostedFiles)
                    {
                        String fileExtension = System.IO.Path.GetExtension(file.FileName).ToString();
                        for (int i = 0; i < allowedExtensions.Length; i++)
                        {
                            if (fileExtension.ToUpper() == allowedExtensions[i])
                            {
                                checkExtensions = true;
                                break;
                            }
                        }
                        if (checkExtensions == false)
                        {
                            lblError.Text = "Only Upload .pdf .png .jpg .jpeg Files ";
                            return false;
                        }
                        if (file.ContentLength > maxfilesize)
                        {
                            lblError.Text = Resources.labels.uploadlimit1MB;
                            return false;
                        }
                        System.IO.Stream fs = file.InputStream;
                        string filename = System.IO.Path.GetFileNameWithoutExtension(file.FileName);
                        double filesize = (double)file.ContentLength / 1024;
                        if (dt.Rows.Count > 0)
                        {
                            int count = dt.Select("FileName = '" + filename + "'").Length;
                            if (count > 0)
                            {
                                filename += "_" + Path.GetRandomFileName();
                            }
                        }
                        try
                        {
                            System.IO.BinaryReader br = new System.IO.BinaryReader(fs);
                            Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                            string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                            dt.Rows.Add(filename, fileExtension.ToUpper(), Resources.labels.chitiettrasoatgiaodich, Math.Ceiling(filesize).ToString() + "KB", Resources.labels.chitiettrasoatgiaodich, base64String);
                        }
                        catch { }
                    }
                    ViewState["TBLDOCUMENTCORP"] = dt;
                }
                else
                {
                    lblError.Text = Resources.labels.banvuilongchonfilexacminh;
                    return false;
                }
            }
            DataTable dtcross = (DataTable)ViewState["TBLDOCUMENTCROSS"];
            if(dtcross.Rows.Count <= 0)
            {
                lblError.Text = "You need to Upload atleast 1 file";
                return false;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
        return true;
    }
    protected void ddlpaymentpurpose_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            rptDocument.DataSource = createtable();
            ViewState["TBLDOCUMENTCROSS"] = createtable();
            rptDocument.DataBind();
            if (ddlpaymentpurpose.SelectedValue.Equals("1"))
            {
                pnlReason1.Visible = true;
                pnlReason2.Visible = false;
                pnlReason3.Visible = false;
            }else if (ddlpaymentpurpose.SelectedValue.Equals("2"))
            {
                pnlReason1.Visible = false;
                pnlReason2.Visible = true;
                pnlReason3.Visible = false;
            }
            else
            {
                pnlReason1.Visible = false;
                pnlReason2.Visible = false;
                pnlReason3.Visible = true;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    #region upload file
    protected void UploadFile(FileUpload fileupload, string desc)
    {
        DataTable dt = TBLDOCUMENTCROSS;
        double maxfilesize = 5 * 1024 * 1024;
        string[] allowedExtensions = {".PDF", ".PNG", ".JPG", ".JPEG" };
        bool checkExtensions = false;
        
            foreach (HttpPostedFile file in fileupload.PostedFiles)
            {
                String fileExtension = System.IO.Path.GetExtension(file.FileName).ToString();
                for (int i = 0; i < allowedExtensions.Length; i++)
                {
                    if (fileExtension.ToUpper() == allowedExtensions[i])
                    {
                        checkExtensions = true;
                        break;
                    }
                }
                if (checkExtensions == false)
                {
                    lblError.Text = "Extensions " + fileExtension + " not support, Only Upload .pdf .png .jpg .jpeg Files ";
                    return;
                }
                if (file.ContentLength > maxfilesize)
                {
                    lblError.Text = "File " + file.FileName + "is too large. Maximum file size permitted is 5MB ";
                    return;
                }
                System.IO.Stream fs = file.InputStream;
                string filename = System.IO.Path.GetFileNameWithoutExtension(file.FileName);
                double filesize = (double)file.ContentLength / 1024;
                if (dt.Rows.Count > 0)
                {
                    int count = dt.Select("FileName = '" + filename + "'").Length;
                    if (count > 0)
                    {
                        filename += "_" + Path.GetRandomFileName();
                }
                }
                try
                {
                    System.IO.BinaryReader br = new System.IO.BinaryReader(fs);
                    Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                    string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                    dt.Rows.Add(filename, fileExtension.ToUpper(), desc, Math.Ceiling(filesize).ToString() + "KB", desc, base64String);
                }
                catch { }
            }
            ViewState["TBLDOCUMENTCROSS"] = dt;
            rptDocument.DataSource = dt;
            rptDocument.DataBind();
            rptDocument.Visible = true;
    }
    protected void btnUpLoadFile_Click(object sender, EventArgs e)
    {
        DataTable dt = createtable();
        ViewState["TBLDOCUMENTCROSS"] = dt;
        try
        {
            if (ddlpaymentpurpose.SelectedValue.Equals("1"))
            {
                if (fileDocument.HasFiles)
                {
                    UploadFile(fileDocument, Resources.labels.idcardpassport);
                }
                else
                {
                    lblError.Text = "Please select at least 1 file!!!";
                    return;
                }
            }
            else
            {
                if (ddlpaymentpurpose.SelectedValue.Equals("2"))
                {
                    if (FileUpload2.HasFiles && FileUpload3.HasFiles)
                    {
                        UploadFile(FileUpload2, Resources.labels.idcardpassport);
                        UploadFile(FileUpload3, Resources.labels.invoiceetc);
                    }
                    else
                    {
                        lblError.Text = "Please select at least one file for each type of document !!!";
                        return;
                    }
                }
                else
                {
                    if (FileUpload4.HasFiles && FileUpload5.HasFiles && FileUpload6.HasFiles && FileUpload7.HasFiles && FileUpload8.HasFiles)
                    {
                        UploadFile(FileUpload4, Resources.labels.idcardpassport);
                        UploadFile(FileUpload5, Resources.labels.enterpriseregis);
                        UploadFile(FileUpload6, Resources.labels.certification);
                        UploadFile(FileUpload7, Resources.labels.InvoicePurchase);
                        UploadFile(FileUpload8, Resources.labels.CustomsDeclaration);
                    }
                    else
                    {
                        lblError.Text = "Please select at least one file for each type of document !!!";
                        return;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    #endregion

    protected void btnNext2_Click(object sender, EventArgs e)
    {
        try
        {
            string senderName = string.Empty;
            string senderacctno = string.Empty;
            string balanceSender = string.Empty;
            string acctCCYID = string.Empty;

            string limit = string.Empty;
            string limitccyid = string.Empty;
            string texterror = string.Empty;
            DataSet ds = new DataSet();

            Hashtable hasInput = new Hashtable();
            Hashtable hasOutput = new Hashtable();

            hasInput = new Hashtable(){
                {"IPCTRANCODE", "MBCHECKLIMITTRANSFER"},
                {"SERVICEID", "IB"},
                {"SOURCEID", "IB"},
                {"REVERSAL", "N"},
                {"CCYID", lblCurrency.Text.Trim()}
            };
            hasOutput = new SmartPortal.IB.Transactions().CommonProcessTrans(hasInput, ref IPCERRORCODE, ref IPCERRORDESC);

            if (IPCERRORCODE.Equals("0"))
            {
                ds = (DataSet)hasOutput[SmartPortal.Constant.IPC.DATASET];
            }
            if (ds!=null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                limit = ds.Tables[0].Rows[0]["LIMIT"].ToString();
                limitccyid = ds.Tables[0].Rows[0]["CCYID"].ToString();
                texterror = ds.Tables[0].Rows[0]["TEXTERROR"].ToString();
            }
            if (lblCurrency.Text.Trim().Equals(limitccyid.Trim()) && SmartPortal.Common.Utilities.Utility.isDouble(txtAmount.Text.Trim(), true) > SmartPortal.Common.Utilities.Utility.isDouble(limit.Trim(), true) && ddlpaymentpurpose.SelectedValue.Trim() =="1")
            {
                lblError.Text = texterror;
                return;
            }
                if (string.IsNullOrEmpty(txtAmount.Text.Trim()) || txtAmount.Text.Equals("0"))
            {
                lblError.Text = Resources.labels.invalidamount;
                return;
            }  
            if (!ddlCcyID.SelectedValue.ToString().Trim().Equals(lblCurrency.Text.ToString().Trim()))
            {
                lblError.Text = Resources.labels.err_diffcurrency;
                return;
            }
            lblSenderName.Text = Utility.KillSqlInjection(txtSenderName.Text);
            if (Session["accType"].ToString() == "IND")
            {
                lblIdType.Text = Utility.KillSqlInjection(ddlIdType.SelectedItem.Text);
                lblIdNumber.Text = Utility.KillSqlInjection(txtIdNumber.Text);
                lblExpDate.Text = Convert.ToDateTime(Utility.KillSqlInjection(txtExpDate.Text)).ToString("dd/MM/yyyy");
                lblIssDate.Text = Convert.ToDateTime(Utility.KillSqlInjection(txtIssDate.Text)).ToString("dd/MM/yyyy");
                lblCountry.Text = Utility.KillSqlInjection(lblSenderCountry.Text);
            }
            lblSenderAddress.Text = Utility.KillSqlInjection(txtSenderAddress.Text);
            lblSenderPhone.Text = Utility.KillSqlInjection(txtSenderPhone.Text.Trim());

            lblEnterName.Text = Utility.KillSqlInjection(txtEnterName.Text);
            lblEnterAddress.Text = Utility.KillSqlInjection(txtEnterAddress.Text);
            lblEnterLicense.Text = Utility.KillSqlInjection(txtEnterLicense.Text);
            lblEnterPhone.Text = Utility.KillSqlInjection(txtEnterPhone.Text.Trim());
            lblEnterTaxcode.Text = Utility.KillSqlInjection(txtEnterTax.Text);

            lblBankName.Text = Utility.KillSqlInjection(txtBankName.Text);
            lblSwiftCode.Text = Utility.KillSqlInjection(txtSwiftCode.Text.Trim());

            lblBenName.Text = Utility.KillSqlInjection(txtReceiverName.Text);
            lblBenAccount.Text = Utility.KillSqlInjection(txtReceiverAcc.Text.Trim());
            lblBenAddress.Text = Utility.KillSqlInjection(txtReceiverAdd.Text);
            lblBenEmail.Text = Utility.KillSqlInjection(txtReceiverMail.Text);
            lblBenPhone.Text = Utility.KillSqlInjection(txtReceiverPhone.Text);

             senderacctno = lblSenderAccount.Text = Utility.KillSqlInjection(ddlSenderAccount.SelectedItem.Text);
            //CHECK RECEIVER ACCOUNT IS EXISTS
            SmartPortal.IB.Account objAcct = new SmartPortal.IB.Account();

            Hashtable hasSender = objAcct.GetInfoAccount(Session["userID"].ToString(), senderacctno, ref IPCERRORCODE, ref IPCERRORDESC);
            if (hasSender[SmartPortal.Constant.IPC.IPCERRORCODE].Equals("0"))
            {
                 senderName = hasSender["FULLNAME"].ToString();
                 balanceSender = Utility.FormatStringCore(hasSender["AVAILABLEBALANCE"].ToString());
                 acctCCYID = hasSender["CCYID"].ToString();
            }
            else
            {
                lblError.Text = "Error : " + hasSender[SmartPortal.Constant.IPC.IPCERRORDESC].ToString();
                return;
            }

            lblBalanceSender.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(balanceSender, acctCCYID);

            lblSenderCCYID.Text = acctCCYID;

            // CHECK AMOUNT
            if (SmartPortal.Common.Utilities.Utility.isDouble(txtAmount.Text.Trim(), true) > SmartPortal.Common.Utilities.Utility.isDouble(balanceSender, true))
            {
                lblError.Text = Resources.labels.amountinvalid;
                return;
            }

            lblAmount.Text = Utility.FormatMoneyInputToView(txtAmount.Text.Trim(), acctCCYID);
            lblPhi.Text = Utility.KillSqlInjection(ddlchoosefee.SelectedItem.Text);

            lblFeeCCYID.Text = acctCCYID;
            lbCCYID.Text = acctCCYID;
            lblCCYIDTotalAmout.Text = acctCCYID;

            #region tinh phi
            string senderfee = "0";

            if (hdTypeID.Value.Equals("WLM"))
            {
                hdTrancode.Value = "IBCBTRANSFERWLTOBANK";
            }
            else
            {
                hdTrancode.Value = "IBCBTRANSFER";
            }
            Hashtable HTCheckAmount = new Account().CheckAmountPayment(Session["userID"].ToString(),
            hdTrancode.Value, ddlSenderAccount.SelectedValue, Utility.KillSqlInjection(txtReceiverAcc.Text.Trim()), SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtAmount.Text.Trim(), lblCurrency.Text), ref IPCERRORCODE, ref IPCERRORDESC);
            if (!IPCERRORCODE.ToString().Equals("0"))
            {
                lblError.Text = IPCERRORDESC;
                return;
            }
            #region tinh fee
            DataTable iRead;
            SqlParameter p1 = new SqlParameter();
            p1.ParameterName = "@USERID";
            p1.Value = Session["userID"].ToString();
            p1.SqlDbType = SqlDbType.VarChar;

            SqlParameter p2 = new SqlParameter();
            p2.ParameterName = "@IPCTRANCODE";
            p2.Value = hdTrancode.Value;
            p2.SqlDbType = SqlDbType.VarChar;

            SqlParameter p3 = new SqlParameter();
            p3.ParameterName = "@AMOUNT";
            p3.Value = SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtAmount.Text.Trim(), lblCurrency.Text);
            p3.SqlDbType = SqlDbType.VarChar;

            SqlParameter p4 = new SqlParameter();
            p4.ParameterName = "@FACCTNO";
            p4.Value = ddlSenderAccount.SelectedValue;
            p4.SqlDbType = SqlDbType.VarChar;

            SqlParameter p5 = new SqlParameter();
            p5.ParameterName = "@TACCTNO";
            p5.Value = lblBenAccount.Text.Trim();
            p5.SqlDbType = SqlDbType.VarChar;

            SqlParameter p6 = new SqlParameter();
            p6.ParameterName = "@CCYID";
            p6.Value = lblCurrency.Text;
            p6.SqlDbType = SqlDbType.VarChar;

            SqlParameter p7 = new SqlParameter();
            p7.ParameterName = "@TCITY";
            p7.Value = "";
            p7.SqlDbType = SqlDbType.VarChar;

            SqlParameter p8 = new SqlParameter();
            p8.ParameterName = "@TBankID";
            p8.Value = hdSelectedBankID.Value;
            p8.SqlDbType = SqlDbType.VarChar;

            SqlParameter p9 = new SqlParameter();
            p9.ParameterName = "@FeePayer";
            p9.Value = ddlchoosefee.SelectedValue;
            p9.SqlDbType = SqlDbType.VarChar;

            iRead = DataAccess.GetFromDataTable("SEMS_CALCULATORSWIFTTRANSFEE", p1, p2, p3, p4, p5, p6, p7, p8, p9);
            
            if (IPCERRORCODE.Equals("0"))
            {
                if (iRead.Rows[0]["feeSenderAmt"] != null)
                {
                    SmartPortal.Common.Log.RaiseError("", this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, "senderfee Before parse: " + iRead.Rows[0]["feeSenderAmt"].ToString(), Request.Url.Query);
                    senderfee = Utility.FormatMoney(iRead.Rows[0]["feeSenderAmt"].ToString(), acctCCYID);
                }
            }
            else
            {
                lblError.Text = IPCERRORDESC;
                return;
            }
            #endregion
            CultureInfo culture = new CultureInfo("en-US");
            CultureInfo.CurrentCulture.NumberFormat = culture.NumberFormat;
            double sendfee = Double.Parse(senderfee, culture);
            double totalfee = sendfee;
            SmartPortal.Common.Log.RaiseError("", this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, "senderfee After parse: " + sendfee, Request.Url.Query);
            SmartPortal.Common.Log.RaiseError("", this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, "Total Fee After parse: " + totalfee, Request.Url.Query);

            double amount = Double.Parse(txtAmount.Text.Trim(), culture);
            double totalamount = amount + totalfee;
            #endregion
            //if (amount <= totalfee)
            //{
            //    lblError.Text = Resources.labels.amountgreaterthanfee;
            //    return;
            //}
            SmartPortal.Common.Log.RaiseError("", this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, "Total Fee After parse and calculate: " + totalfee, Request.Url.Query);
            

            lblPhiAmount.Text = Utility.FormatMoneyInputToView(sendfee.ToString(), acctCCYID);

            lblFinalAmount.Text = Utility.FormatMoneyInput(amount.ToString(), acctCCYID);
            lblFinalFee.Text = Utility.FormatMoneyInput(senderfee, acctCCYID);
            lblPhiAmount.Text = Utility.FormatMoneyInputToView(totalfee.ToString(), acctCCYID);
            lblTotalAmount.Text = Utility.FormatMoneyInputToView(totalamount.ToString(), acctCCYID);

            lblPupose.Text = Utility.KillSqlInjection(ddlpaymentpurpose.SelectedItem.Text);
            lblDetailPupose.Text = Utility.KillSqlInjection(txtDetailRemittance.Text);

            DataTable dt = (DataTable)ViewState["TBLDOCUMENTCROSS"];

            if(dt.Rows.Count > 0)
            {
                rptConfirmDocument.DataSource = dt;
                rptConfirmDocument.DataBind();
            }
            else
            {
                rptConfirmDocument.DataSource = new DataTable();
                rptConfirmDocument.DataBind();
                rptConfirmDocument.Visible = false;
            }

            pnConfirm.Visible = true;
            if (Session["accType"].ToString() != "IND")
            {
                pnConfirmIND.Visible = false;
                pnConfirmMTK.Visible = true;
            }
            else
            {
                pnConfirmIND.Visible = true;
                pnConfirmMTK.Visible = false;
            }
            pnBeneficiary.Visible = false;
            pnRemitterIND.Visible = false;
            pnRemitterMTK.Visible = false;
            pnPurpose.Visible = false;
            pnpaymentinfor.Visible = false;
            checkTerms_CheckedChanged(null, EventArgs.Empty);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void btnBackConfirm_Click(object sender, EventArgs e)
    {
        try
        {
            pnOTP.Visible = false;
            pnConfirm.Visible = true;
            if (Session["accType"].ToString() != "IND")
            {
                pnConfirmIND.Visible = false;
                pnConfirmMTK.Visible = true;
            }
            else
            {
                pnConfirmIND.Visible = true;
                pnConfirmMTK.Visible = false;
            }
            pnBeneficiary.Visible = false;
            pnRemitterIND.Visible = false;
            pnRemitterMTK.Visible = false;
            pnPurpose.Visible = false;
            pnpaymentinfor.Visible = false;
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    protected void btnBack3_Click(object sender, EventArgs e)
    {
        try
        {
            pnConfirm.Visible = false;
            pnBeneficiary.Visible = false;
            pnRemitterIND.Visible = false;
            pnRemitterMTK.Visible = false;
            pnPurpose.Visible = false;
            pnpaymentinfor.Visible = true;
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    protected void btnAction_Click(object sender, EventArgs e)
    {
        Object m_lock = new Object();

        try
        {
            lock (m_lock)
            {
                string UserId = Session["userID"].ToString();
                string trancode = Utility.KillSqlInjection(hdTrancode.Value);
                string authenType = ddlLoaiXacThuc.SelectedValue;
                string authenCode = Utility.KillSqlInjection(txtOTP.Text.Trim());

                string SenderName = string.Empty;
                string SenderPhone = string.Empty;
                string SenderAddress = string.Empty;
                string SenderIdType = string.Empty;
                string SenderIdNumber = string.Empty;
                string SenderCountry = string.Empty;
                string ExpDate = string.Empty;
                string IssDate = string.Empty;

                string EnterName = string.Empty;
                string EnterAddress = string.Empty;
                string EnterPhone = string.Empty;
                string EnterLicense = string.Empty;
                string EnterTax = string.Empty;

                
                //receiver infor

                string BankName = Utility.KillSqlInjection(lblBankName.Text);
                string SwiftCode = Utility.KillSqlInjection(lblSwiftCode.Text);
                string BenName = Utility.KillSqlInjection(lblBenName.Text);
                string BenAccount = Utility.KillSqlInjection(lblBenAccount.Text);
                string BenPhone = Utility.KillSqlInjection(lblBenPhone.Text);
                string BenAddress = Utility.KillSqlInjection(lblBenAddress.Text);
                string BenMail = Utility.KillSqlInjection(lblBenEmail.Text);

                string Purpose = Utility.KillSqlInjection(ddlpaymentpurpose.SelectedItem.Text);
                string PurposeDetail = Utility.KillSqlInjection(txtDetailRemittance.Text);

                string SenderBranchID = Utility.KillSqlInjection(hdSenderBranch.Value.Trim());
                string SenderAccount = Utility.KillSqlInjection(ddlSenderAccount.SelectedValue.Trim());
                
                
                string Amount = Utility.FormatMoneyInput(lblFinalAmount.Text, lblSenderCCYID.Text);

                string Ccyid = Utility.KillSqlInjection(lblCurrency.Text);

                string lblDetail = Utility.KillSqlInjection(lblPupose.Text);

                string didnumber = string.Empty;
                string cidnumber = string.Empty;

               


                DataTable dt = TBLDOCUMENTCROSS;
                DataTable dtcorp = TBLDOCUMENTCORP;

                DataTable dtdocument = new DataTable();
                dtdocument = dt.Copy();
                dtdocument.Merge(dtcorp, false);

                string OTPcode = txtOTP.Text;
                txtOTP.Text = "";

                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //sender infor
                if (Session["accType"].ToString() == "IND")
                {
                    SenderName = Utility.KillSqlInjection(lblSenderName.Text);
                    SenderPhone = Utility.KillSqlInjection(lblSenderPhone.Text);
                    SenderAddress = Utility.KillSqlInjection(lblSenderAddress.Text);
                    SenderIdType = Utility.KillSqlInjection(lblIdType.Text);
                    SenderIdNumber = Utility.KillSqlInjection(lblIdNumber.Text);
                    SenderCountry = Utility.KillSqlInjection(lblCountry.Text);
                    ExpDate = Utility.KillSqlInjection(lblExpDate.Text);
                    IssDate = Utility.KillSqlInjection(lblIssDate.Text);
                }
                else
                {
                    SenderName = Utility.KillSqlInjection(lblEnterName.Text);
                    SenderAddress = Utility.KillSqlInjection(lblEnterAddress.Text);
                    SenderPhone = Utility.KillSqlInjection(lblEnterPhone.Text);
                    EnterLicense = Utility.KillSqlInjection(lblEnterLicense.Text);
                    EnterTax = Utility.KillSqlInjection(lblEnterTaxcode.Text);
                }
                hasInput = new Hashtable(){
                {"IPCTRANCODE", trancode},
                {"SERVICEID", "IB"},
                {"SOURCEID", "IB"},
                {"REVERSAL", "N"},

                {"USERID", UserId},
                {"AMOUNT", SmartPortal.Common.Utilities.Utility.FormatMoneyInput(lblFinalAmount.Text, lblCurrency.Text)},
                {"TOTALAMOUNT", SmartPortal.Common.Utilities.Utility.FormatMoneyInput(lblTotalAmount.Text, lblCurrency.Text)},
                {"ACCTNO", ddlSenderAccount.SelectedValue},
                {"CCYID", lblCurrency.Text},
                {"TCITY", ""},
                {"BANKID", hdSelectedBankID.Value},
                {"FEETYPE", ddlchoosefee.SelectedValue},
                {"DOCUMENT", dtdocument},
                {"CONTRACTTYPE", Session["accType"].ToString()},
                {"RECEIVERNAME",BenName },
                {"RECEIVERPHONE", BenPhone },
                {"SENDERIDTYPE", SenderIdType},
                {"SENDERIDNUM", SenderIdNumber},
                {"SENDERCOUNTRY", SenderCountry},
                {"EXPIREDDATE", ExpDate},
                {"ISSUEDATE", IssDate},
                {"SENDERNAME", SenderName},
                {"SENDERADDRESS",SenderAddress},
                {"SENDERPHONE",SenderPhone},
                {"RECEIVERADDRESS",BenAddress},
                {"RECEIVERACCOUNT",BenAccount},
                {"RECEIVERBANKCODE",SwiftCode},
                {"RECEIVERBANKNAME",BankName},
                {"RECEIVEREMAIL",BenMail},
                {"AUTHENTYPE",authenType},
                {"AUTHENCODE",authenCode},
                {"TRANDESC", "Crossborder transfer"},
                {"PURPOSE", Purpose},
                {"DETAILPURPOSE", PurposeDetail},
                {"LICENSE", EnterLicense},
                {"TAX", EnterTax},
                {"ACCTYPE", Session["accType"].ToString()}
            };

                hasOutput = new SmartPortal.IB.Transactions().CommonProcessTrans(hasInput, ref IPCERRORCODE, ref IPCERRORDESC);

                if (!hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].Equals("0"))
                {

                    txtOTP.Text = "";
                    btnPrint.Visible = false;
                    switch (hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString().Trim())
                    {
                        case SmartPortal.Constant.IPC.ERRORCODE.OTPINVALID:
                            lblError.Text = hasOutput[SmartPortal.Constant.IPC.IPCERRORDESC].ToString().Trim();
                            if (DateTime.Compare(DateTime.Now, Convert.ToDateTime(ViewState["timeReSendOTP"].ToString())) > 0)
                            {
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "key", "resetOTP();", true);
                            }
                            return;
                        case SmartPortal.Constant.IPC.ERRORCODE.WATTINGBANKAPPROVE:
                            lblError.Text = Resources.labels.wattingbankapprove;
                            pnResultTransaction.Visible = true;
                            break;
                        case SmartPortal.Constant.IPC.ERRORCODE.WATTINGPARTOWNERAPPROVE:
                            lblError.Text = Resources.labels.wattingpartownerapprove;
                            pnResultTransaction.Visible = true;
                            break;
                        case SmartPortal.Constant.IPC.ERRORCODE.WATTINGUSERAPPROVE:
                            lblError.Text = Resources.labels.wattinguserapprove;
                            pnResultTransaction.Visible = true;
                            break;
                        case SmartPortal.Constant.IPC.ERRORCODE.NOTREGOTP:
                            lblError.Text = hasOutput[SmartPortal.Constant.IPC.IPCERRORDESC].ToString().Trim();
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "key", "resetOTP();", true);
                            return;
                        case SmartPortal.Constant.IPC.ERRORCODE.AUTHENTYPEINVALID:
                            lblError.Text = Resources.labels.authentypeinvalid;
                            return;
                        default:
                            lblError.Text = string.IsNullOrEmpty(hasOutput[SmartPortal.Constant.IPC.IPCERRORDESC].ToString()) ? "Transaction error!" : hasOutput[SmartPortal.Constant.IPC.IPCERRORDESC].ToString();
                            return;
                    }
                    pnConfirm.Visible = false;
                    btnApply.Enabled = false;
                    //btnBackTransfer.Enabled = false;
                    btnView.Visible = false;
                    pnResultTransaction.Visible = false;
                }
                pnOTP.Visible = false;
                pnConfirm.Visible = false;
                pnBeneficiary.Visible = false;
                pnRemitterIND.Visible = false;
                pnRemitterMTK.Visible = false;
                pnPurpose.Visible = false;
                pnpaymentinfor.Visible = false;
                pnResultTransaction.Visible = true;
                if (Session["accType"].ToString() != "IND")
                {
                    pnResultIND.Visible = false;
                    pnResultMTK.Visible = true;
                }
                else
                {
                    pnResultIND.Visible = true;
                    pnResultMTK.Visible = false;
                }


                #region LOAD RESULT TRANSFER
                string errorDesc = string.Empty;
                SmartPortal.IB.Account acct = new SmartPortal.IB.Account();

                LoadAccountInfo();

                if(hasOutput["IPCTRANSID"] != null)
                {
                    lblEndTransactionNo.Text = hasOutput["IPCTRANSID"].ToString();
                }
                else
                {
                    lblendtransaction.Visible = false;
                    lblEndTransactionNo.Visible = false;
                }
                
                lblEndDateTime.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");

                lblResSendername.Text = SenderName;
                lblResSenderPhone.Text = SenderPhone;
                lblResSenderAcc .Text = SenderAccount;
                lblResSenderAddress .Text = SenderAddress;

                lblResEnterName.Text = SenderName;
                lblResEnterAddress.Text = SenderAddress;

                lblResEnterPhone.Text = SenderPhone;
                lblResEnterLicense.Text = Utility.KillSqlInjection(lblEnterLicense.Text);
                lblResEnterTaxCode.Text = Utility.KillSqlInjection(lblEnterTaxcode.Text);
                lblResBankName.Text = BankName;
                lblResSwiftCode.Text = SwiftCode;
                lblResReceiverName.Text = BenName;
                lblResReceiverAcc.Text = BenAccount;
                lblResReceiverAdd.Text = BenAddress;
                lblResReceiverPhone.Text = BenPhone;
                lblResReceiverEmail.Text = BenMail;
                lblResIdtype.Text = SenderIdType;
                lblResIdNumber.Text = SenderIdNumber;
                lblResCountry.Text = SenderCountry;
                lblResExpDate.Text = ExpDate;
                lblResIssDate.Text = IssDate;
                lblResBalance.Text = Utility.FormatMoney(Utility.FormatStringCore(hdBalanceSender.Value), lblSenderCCYID.Text.Trim());
                lblResTotalAmount.Text = Utility.FormatMoney(Utility.FormatStringCore(Utility.FormatMoneyInput(lblTotalAmount.Text, lblSenderCCYID.Text)), lblSenderCCYID.Text.Trim());
                lblResCCYIDTongtien.Text = Utility.KillSqlInjection(lblCurrency.Text);
                lblResCCYIDbalance.Text = Utility.KillSqlInjection(lblCurrency.Text);
                lblResAmount.Text = Utility.FormatMoney(Utility.FormatStringCore(Amount), lblSenderCCYID.Text.Trim());
                lblResCCYIDamount.Text = Utility.KillSqlInjection(lblCurrency.Text);
                lblResFeeType.Text = Utility.KillSqlInjection(lblPhi.Text);
                lblResFeeAmount.Text = Utility.FormatMoney(Utility.FormatStringCore(lblFinalFee.Text), lblSenderCCYID.Text.Trim());

                lblResCCYITFee.Text = Utility.KillSqlInjection(lblCurrency.Text);
                lblResPurpo.Text = Utility.KillSqlInjection(lblPupose.Text);
                lblResDetailPurpo.Text = Utility.KillSqlInjection(lblDetailPupose.Text);

                #endregion


                if (hasOutput["IPCERRORCODE"].Equals("0"))
                {
                    lblError.Text = Resources.labels.transactionsuccessful;
                    btnPrint.Visible = true;
                    btnView.Visible = true;
                    //ghi vo session dung in

                    Hashtable hasPrint = new Hashtable();
                    hasPrint.Add("status", Resources.labels.thanhcong);
                    hasPrint.Add("senderName", SenderName);
                    hasPrint.Add("senderAccount", SenderAccount);
                    hasPrint.Add("senderBalance", lblResBalance.Text);
                    hasPrint.Add("ccyid", lblSenderCCYID.Text);
                    hasPrint.Add("senderidtype", lblResIdtype.Text);
                    hasPrint.Add("senderidnumber", lblResIdNumber.Text);
                    hasPrint.Add("sendercountry", lblResCountry.Text);
                    hasPrint.Add("expireddate", lblResExpDate.Text);
                    hasPrint.Add("issuedate", lblResIssDate.Text);
                    hasPrint.Add("receiverName", BenName);
                    hasPrint.Add("receiverAccount", BenAccount);
                    hasPrint.Add("recieverBank", BankName);
                    hasPrint.Add("swiftCode", SwiftCode);
                    hasPrint.Add("recieverPhone", BenPhone);
                    hasPrint.Add("recieverEmail", BenMail);
                    hasPrint.Add("recieverAddress", BenAddress);
                    hasPrint.Add("tranID", hasOutput["IPCTRANSID"] != null ? hasOutput["IPCTRANSID"].ToString() : "");
                    hasPrint.Add("tranDate", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                    hasPrint.Add("amount", lblResAmount.Text);
                    hasPrint.Add("amountchu", txtChu.Value.ToString());
                    hasPrint.Add("totalAmount", lblResTotalAmount.Text);
                    hasPrint.Add("feeType", lblPhi.Text);
                    hasPrint.Add("feeAmount", lblPhiAmount.Text);
                    hasPrint.Add("purPose", lblResPurpo.Text);
                    hasPrint.Add("DetailpurPose", lblResDetailPurpo.Text);
                    hasPrint.Add("desc", "Crossborder transfer");

                    Session["print"] = hasPrint;

                    btnPrint.Visible = true;
                    btnView.Visible = true;
                }
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

    protected void btnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=IB_CBTransfer"));
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        if (Session["print"] == null)
        {
            Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/" + System.Configuration.ConfigurationManager.AppSettings["loginpage"]));
        }
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        if (Session["print"] == null)
        {
            Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/" + System.Configuration.ConfigurationManager.AppSettings["loginpage"]));
        }
    }

    protected void txtSwiftCode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            LoadBankBySwiftCode();
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    protected void checkTerms_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (checkTerms.Checked)
            {
                btnApply.Enabled = true;
            }
            else
            {
                btnApply.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
}
