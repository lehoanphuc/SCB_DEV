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
using SmartPortal.Common;
using SmartPortal.Security;
using System.Text;
using SmartPortal.ExceptionCollection;
using SmartPortal.SEMS;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Globalization;
using SmartPortal.Common.Utilities;
using System.Linq;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using System.IO;
using SmartPortal.Model;
using System.ComponentModel.DataAnnotations;

public partial class Widgets_SEMSCreateContractAgentMerchant_Add_Widget : WidgetBase
{
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    string userName;
    string resultUserName = string.Empty;
    string usernameAuthorized;
    string BaseImageOwner = "";
    internal int minlength = int.Parse(ConfigurationManager.AppSettings["minlengthloginname"].ToString());
    internal int maxlength = int.Parse(ConfigurationManager.AppSettings["maxlengthloginname"].ToString());
    static List<string> lsAccNo = new List<string>();
    SmartPortal.SEMS.Common _service = new SmartPortal.SEMS.Common();
    public static string documentId;
    private static int NumberReg = 0;
    public bool showNRIC
    {
        get { return ViewState["showNRIC"] != null ? (bool)ViewState["showNRIC"] : false; }

        set { ViewState["showNRIC"] = value; }
    }
    public bool showPassport
    {
        get { return ViewState["showPassport"] != null ? (bool)ViewState["showPassport"] : false; }

        set { ViewState["showPassport"] = value; }
    }
    public bool showLicense
    {
        get { return ViewState["showLicense"] != null ? (bool)ViewState["showLicense"] : false; }

        set { ViewState["showLicense"] = value; }
    }
    protected void Page_Init(object sender, EventArgs e)
    {
        TabAgentMerchantHelper.LoadConfig();
        LoadKYC();
    }
    public DataTable NGUOIUYQUYEN
    {
        get { return ViewState["NGUOIUYQUYEN"] != null ? (DataTable)ViewState["NGUOIUYQUYEN"] : new DataTable(); }
        set { ViewState["NGUOIUYQUYEN"] = NGUOIUYQUYEN; }
    }
    public string PHONE
    {
        get { return ViewState["PHONE"] != null ? (string)ViewState["PHONE"] : string.Empty; }
        set { ViewState["PHONE"] = value; }
    }
    public string PHONENGUOIUYQUYEN
    {
        get { return ViewState["PHONENGUOIUYQUYEN"] != null ? (string)ViewState["PHONENGUOIUYQUYEN"] : string.Empty; }
        set { ViewState["PHONENGUOIUYQUYEN"] = value; }
    }
    public string USERCTK
    {
        get { return ViewState["USERCTK"] != null ? (string)ViewState["USERCTK"] : string.Empty; }
        set { ViewState["USERCTK"] = value; }
    }
    public string USERUYQUYEN
    {
        get { return ViewState["USERUYQUYEN"] != null ? (string)ViewState["USERUYQUYEN"] : string.Empty; }
        set { ViewState["USERUYQUYEN"] = value; }
    }
    public string IMAGE
    {
        get { return ViewState["IMAGE"] != null ? (string)ViewState["IMAGE"] : string.Empty; }
        set { ViewState["IMAGE"] = value; }
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
    }
    public DataTable TBLDOCUMENT
    {
        get { return ViewState["TBLDOCUMENT"] != null ? (DataTable)ViewState["TBLDOCUMENT"] : new DataTable(); }
        set { ViewState["TBLDOCUMENT"] = TBLDOCUMENT; }
    }
    public string LICENSETYPE
    {
        get { return ViewState["LICENSETYPE"] != null ? (string)ViewState["LICENSETYPE"] : string.Empty; }
        set { ViewState["LICENSETYPE"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string PassTemp = SmartPortal.SEMS.O9Encryptpass.sha_sha256("58765214", "AC80F38C-5229-48BF-B153-7F903C446F7C");
            btnNext.Visible = true;
            lblLog.Visible = false;
            lblError.Text = string.Empty;
            btnSearchWL.Visible = false;
            if (rbTypeNguoiUyQuyen.Checked)
                txtIBTypeUserNameNguoiUyQuyen.Enabled = true;
            else
                txtIBTypeUserNameNguoiUyQuyen.Enabled = false;
            txtAMPhoneNo.Enabled = false;
            UpLoadFile();
            if (!IsPostBack)
            {
                //load file image to uploadfile
                SetDateTime();
                resetImage();
                LoadMerchantCategoryCodes();
                loadCombobox_KYCLevel();

                if (!showNRIC && !showLicense && !showPassport)
                {
                    lblError.Text = Resources.labels.youneedkyctocreatecontract;
                    KYCInFor.Visible = false;
                    btnSearch.Enabled = false;
                }
                IMAGE = GenIDImage(20);
                ViewState["TBLDOCUMENT"] = DocumentContract();
                ddlCustType.Items.Add(new ListItem(Resources.labels.canhan, "P"));
                ddlCustType.Items.Add(new ListItem(Resources.labels.linkage, "J"));
                ddlCustType.Items.Add(new ListItem(Resources.labels.doanhnghiep, "O"));
                ddlGender.Items.Add(new ListItem(Resources.labels.male, "M"));
                ddlGender.Items.Add(new ListItem(Resources.labels.female, "F"));
                ddlUserType.Items.Add(new ListItem("Merchant", "M"));
                ddlUserType.Items.Add(new ListItem("Agent", "A"));
                ddlUserType.SelectedValue = "A";
                ddlSubUserType.Items.Clear();
                ddlMerchantCode.Visible = false;
                lblMerchantCategory.Visible = false;
                DataTable dsUserType = new SmartPortal.SEMS.Services().GetUserType(string.Empty, ddlUserType.SelectedValue, ref IPCERRORCODE, ref IPCERRORDESC).Tables[0];
                ddlSubUserType.DataSource = dsUserType;
                ddlSubUserType.DataTextField = "SUBUSERTYPE";
                ddlSubUserType.DataValueField = "SUBUSERCODE";
                ddlSubUserType.DataBind();
                ddlProduct.Items.Add(new ListItem(Resources.labels.ebankingcanhan, "PCN"));
                ddlProduct.Items.Add(new ListItem(Resources.labels.ebankingdoanhnghiep, "PDN"));
                ddlReGender.Items.Add(new ListItem(Resources.labels.male, "M"));
                ddlReGender.Items.Add(new ListItem(Resources.labels.female, "F"));
                ddlGenderNguoiUyQuyen.Items.Add(new ListItem(Resources.labels.male, "M"));
                ddlGenderNguoiUyQuyen.Items.Add(new ListItem(Resources.labels.female, "F"));

                //hiden panel
                pnCustInfo.Visible = true;
                pnPersonal.Visible = false;


                #region load KYC Type
                DataTable dsKYCtype = new DataTable();

                dsKYCtype = new SmartPortal.SEMS.Customer().LoadKYCType(string.Empty, ref IPCERRORCODE, ref IPCERRORDESC);

                ddlkycType.DataSource = dsKYCtype;
                ddlkycType.DataTextField = "KYCNAME";
                ddlkycType.DataValueField = "KYCCODE";
                ddlkycType.DataBind();
                #endregion

                LoadContractLevelByCombobox();

                #region load nation
                DataSet dsNation = new DataSet();
                object[] _object = new object[] { string.Empty, string.Empty, null, null };
                dsNation = _service.common("WAL_BO_GET_NATION", _object, ref IPCERRORCODE, ref IPCERRORDESC);

                ddlNation.DataSource = dsNation;
                ddlNation.DataTextField = "NATIONNAME";
                ddlNation.DataValueField = "NATIONCODE";
                ddlNation.DataBind();
                #endregion

                #region load level contract

                ddlContractLevel.SelectedValue = new SmartPortal.SEMS.Contract().GetContractLevelID(ref IPCERRORCODE, ref IPCERRORDESC); ;
                #endregion

                //load loai hinh san pham
                LoadProductByUserType();
                //thaity modify at 14/7/2014
                /*if (radLkgCode.Checked || radGrpCode.Checked)
                {                    
                    //thaity test
                    cbxneedtoapprovetransaction.Enabled  = false;
                }
                else
                {
                    cbxneedtoapprovetransaction.Enabled = false;
                    cbxneedtoapprovetransaction.Checked = false;
                }
                //thaity modify at 14/7/2014
                 * */

                //hien thi mac dinh ma hop dong 
                //txtContractNo.Text = SmartPortal.Constant.IPC.CONTRACTNOPREFIX + DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length-8,8);

                string dateExpire_date = new SmartPortal.SEMS.EBASYSVAR().ViewDetail("EXPIRE_DATE", ref IPCERRORCODE, ref IPCERRORDESC).Tables[0].Rows[0]["VARVALUE"].ToString(); ;

                txtContractNo.Enabled = false;
                txtStartDate.Text = SmartPortal.Common.Utilities.Utility.IsDateTime1(SmartPortal.Constant.IPC.LoadWorkingDate()).ToString("dd/MM/yyyy");
                txtEndDate.Text = SmartPortal.Common.Utilities.Utility.IsDateTime1(SmartPortal.Constant.IPC.LoadWorkingDate()).AddYears(int.Parse(dateExpire_date)).AddDays(-1).ToString("dd/MM/yyyy");


                //load chi nhánh
                ddlBranch.DataSource = new SmartPortal.SEMS.Branch().GetAll(ref IPCERRORCODE, ref IPCERRORDESC);

                ddlBranch.DataTextField = "BRANCHNAME";
                ddlBranch.DataValueField = "BRANCHID";
                ddlBranch.DataBind();
                ddlBranch.Enabled = true;

                DataTable dtRegion;



                dtRegion = new SmartPortal.SEMS.Township().GetAllRegion(ref IPCERRORCODE, ref IPCERRORDESC).Tables[0];
                ddlRegion.DataSource = dtRegion;
                ddlRegion.DataTextField = "REGIONNAME";
                ddlRegion.DataValueField = "REGIONID";
                ddlRegion.DataBind();

                LoadTownshipFromRegion(ddlRegion.SelectedValue);

                //load policy to dropdownlist
                DataSet dspolicy = new DataSet();
                string filterIB = "serviceid='IB'";
                string filterSMS = "serviceid='SMS'";
                string filterMB = "serviceid='AM'";
                string stSort = "serviceid asc";

                dspolicy = new SmartPortal.SEMS.USERPOLICY().GetPolicybyCondition(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, Session["userName"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE == "0")
                {
                    DataTable dt = dspolicy.Tables[0];


                    DataTable dtIB = dt.Select(filterIB, stSort).Any() ? dt.Select(filterIB, stSort).CopyToDataTable() : null;
                    DataTable dtSMS = dt.Select(filterSMS, stSort).Any() ? dt.Select(filterSMS, stSort).CopyToDataTable() : null;
                    DataTable dtMB = dt.Select(filterMB, stSort).Any() ? dt.Select(filterMB, stSort).CopyToDataTable() : null;


                    ddlpolicyIB.DataSource = dtIB;
                    ddlpolicySMS.DataSource = dtSMS;
                    ddlpolicyMB.DataSource = dtMB;
                    ddlpolicyMB.DataSource = dtMB;
                    ddlpolicyIBco.DataSource = dtIB;
                    ddlpolicySMSco.DataSource = dtSMS;
                    ddlpolicyMBco.DataSource = dtMB;
                    ddlpolicyWLco.DataSource = dtMB;
                    ddlpolicyIB.DataTextField = "policytx";
                    ddlpolicyIB.DataValueField = "policyid";
                    ddlpolicySMS.DataTextField = "policytx";
                    ddlpolicySMS.DataValueField = "policyid";
                    ddlpolicyMB.DataTextField = "policytx";
                    ddlpolicyMB.DataValueField = "policyid";
                    ddlpolicyMB.DataTextField = "policytx";
                    ddlpolicyMB.DataValueField = "policyid";
                    ddlpolicyIBco.DataTextField = "policytx";
                    ddlpolicyIBco.DataValueField = "policyid";
                    ddlpolicySMSco.DataTextField = "policytx";
                    ddlpolicySMSco.DataValueField = "policyid";
                    ddlpolicyMBco.DataTextField = "policytx";
                    ddlpolicyMBco.DataValueField = "policyid";
                    ddlpolicyWLco.DataTextField = "policytx";
                    ddlpolicyWLco.DataValueField = "policyid";
                    ddlpolicyIB.DataBind();
                    ddlpolicySMS.DataBind();
                    ddlpolicyMB.DataBind();
                    ddlpolicyMB.DataBind();
                    ddlpolicyIBco.DataBind();
                    ddlpolicySMSco.DataBind();
                    ddlpolicyMBco.DataBind();
                    ddlpolicyWLco.DataBind();
                    //set readonly for mb
                    ddlpolicyWLco.Enabled = false;
                    ddlpolicyMBco.Enabled = false;
                }


            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "SEMSCreateContractAgentMerchant", "btnNext_Click", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }

    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        try
        {
            if (!ValidateNext())
            {
                return;
            }
            ddlLoginMethod.SelectedValue = "USERNAME";
            string result = new Customer().CheckPhoneNumberCustInfo(txtPhoneWL.Text, SmartPortal.Constant.IPC.CONTRACTAGENTMERCHANT, ref IPCERRORCODE, ref IPCERRORDESC);
            if (result.Equals("1"))
            {
                lblError.Text = Resources.labels.phonenumberisalreadyregistered;
                return;
            }
            else
            {
                if (CheckIsPhoneNumer(txtPhoneWL.Text.Trim()))
                {
                    PHONE = txtPhoneWL.Text.Trim();
                }
                else
                {
                    lblError.Text = Resources.labels.phonenumberwrong;
                    return;
                }

            }
            if (lblNRICBackNew.Text.Equals("") || lblNRICFontNew.Text.Equals(""))
            {
                lblError.Text = Resources.labels.youhavenotuploadedenoghinformation;
                return;
            }
            #region CoreBaking   
            //check ngày tháng
            if (txtBirth.Text.Trim() != "")
            {
                if (!validatedatetime(txtBirth))
                {
                    lblError.Text = Resources.labels.yearofdatetimemustsmallerthan1753;
                    return;
                }
                if (SmartPortal.Common.Utilities.Utility.IsDateTimeViet(txtBirth.Text.Trim()))
                {

                }
                else
                {
                    throw new IPCException(SmartPortal.Constant.IPC.ERRORCODE.BE);
                }
            }

            if (SmartPortal.Common.Utilities.Utility.IsDateTimeViet(txtIssueDate.Text.Trim()))
            {
                if (!validatedatetime(txtBirth))
                {
                    lblError.Text = Resources.labels.yearofdatetimemustsmallerthan1753;
                    return;
                }
                if (txtBirth.Text.Trim() != "")
                {
                    if (SmartPortal.Common.Utilities.Utility.IsDateTime1(txtIssueDate.Text.Trim()) <= SmartPortal.Common.Utilities.Utility.IsDateTime1(txtBirth.Text.Trim()))
                    {
                        throw new IPCException(SmartPortal.Constant.IPC.ERRORCODE.NCLNS);
                    }
                }
            }
            else
            {
                //throw new IPCException(SmartPortal.Constant.IPC.ERRORCODE.BE);
            }

            if (SmartPortal.Common.Utilities.Utility.IsDateTimeViet(txtStartDate.Text.Trim()))
            {
            }
            else
            {
                throw new IPCException(SmartPortal.Constant.IPC.ERRORCODE.BE);
            }

            if (SmartPortal.Common.Utilities.Utility.IsDateTimeViet(txtEndDate.Text.Trim()))
            {
            }
            else
            {
                throw new IPCException(SmartPortal.Constant.IPC.ERRORCODE.BE);
            }
            //#endregion
            //if (radNewNRIC.Checked && (lblNRICBackNew.Text.Equals("") || lblNRICFontNew.Text.Equals(""))
            //    || radLicense.Checked && (lblNLicenseFont.Text.Equals("") || lblLicenseBack.Text.Equals(""))
            //    || radPassport.Checked && lblPassport.Text.Equals(""))
            //{

            //    lblError.Text = Resources.labels.youhavenotuploadedenoghinformation;
            //    return;
            //}

            switch (ddlSubUserType.SelectedValue)
            {
                case "0103":
                    resultUserName = new Customer().UsernameGeneration("1", ref IPCERRORCODE, ref IPCERRORDESC);
                    break;
                case "0102":
                    resultUserName = new Customer().UsernameGeneration("2", ref IPCERRORCODE, ref IPCERRORDESC);
                    break;
                case "0101":
                    resultUserName = new Customer().UsernameGeneration("3", ref IPCERRORCODE, ref IPCERRORDESC);
                    break;
                case "0301":
                    resultUserName = new Customer().UsernameGeneration("4", ref IPCERRORCODE, ref IPCERRORDESC);
                    break;
                default:
                    resultUserName = new Customer().UsernameGeneration("7", ref IPCERRORCODE, ref IPCERRORDESC);
                    break;
            }

            if (resultUserName.Equals(string.Empty) || resultUserName.Length != 10 || !CheckUserNameExists(resultUserName))
            {
                lblError.Text = Resources.labels.usernamehasexists;
                return;
            }

            txtUserNameMB.Text = resultUserName;

            if (!CheckUserNameExists(txtUserNameMB.Text))
            {
                lblError.Text = Resources.labels.usernamehasexists;
            }

            #endregion
            #region lấy thông tin khách hàng qua người dùng
            txtReFullName.Text = txtFullName.Text;
            txtReBirth.Text = txtBirth.Text;
            ddlReGender.SelectedValue = ddlGender.SelectedValue;
            string usernameInfo = txtPhoneWL.Text;
            if (usernameInfo.Equals(string.Empty))
            {
                txtMBPhoneNo.Enabled = true;
            }
            else
            {
                txtMBPhoneNo.Enabled = false;
            }
            txtAMPhoneNo.Text = txtReMobi.Text = usernameInfo;
            txtReEmail.Text = txtEmail.Text;
            txtReAddress.Text = txtResidentAddr.Text;

            txtReFullName.Enabled = false;
            txtReBirth.Enabled = false;
            ddlReGender.Enabled = false;
            txtReMobi.Enabled = false;
            txtReEmail.Enabled = true;
            txtReAddress.Enabled = false;
            #endregion

            //checkbox
            radAllAccountNguoiUyQuyen.Checked = true;

            //release session
            ReleaseSession();
            //huy du lieu luoi
            gvResultChuTaiKhoan.DataSource = null;
            gvResultChuTaiKhoan.DataBind();

            gvResultNguoiUyQuyen.DataSource = null;
            gvResultNguoiUyQuyen.DataBind();


            //diable account
            ddlAccountUyQuyen.Enabled = false;


            //string strCode = txtCustCodeInfo.Text.Trim() + ddlCustType.SelectedValue.Trim() + DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - 8, 4);
            string ownerUserName = usernameInfo;

            USERCTK = new SmartPortal.SEMS.User().CreatePrimaryKey(string.Empty, string.Empty, "USERID", ref IPCERRORCODE, ref IPCERRORDESC);

            txtUsernameIB.Text = USERCTK;
            txtMBPhoneNo.Text = USERCTK;
            //13/8/2015 minh bỏ comment de lay lai thong tin genuser fix lỗi phone is not the number alert
            //04/09/2015 add lại comment để gán lại qua nút view detail
            // txtIBGenUserNameNguoiUyQuyen.Text = SmartPortal.Common.Utilities.Utility.GetID(txtFullName.Text, txtCustCodeInfo.Text, txtIF.Text, ownerUserName);

            //Edit by VuTran04082014: sample user IB,MB
            #region load tab

            //DataTable dtProduct = new SmartPortal.SEMS.Product().GetProductByCondition(ddlProduct.SelectedValue, "", ddlCustType.SelectedValue, "", ref IPCERRORCODE, ref IPCERRORDESC).Tables[0];
            //string productType = dtProduct.Rows[0]["ProductType"].ToString();
            //if (productType == SmartPortal.Constant.IPC.PRCTYPECONSUMER)
            //{
            //    CONTRACTTYPE = SmartPortal.Constant.IPC.CONTRACTINDIVIDUAL;
            //}
            //else
            //{
            //    CONTRACTTYPE = SmartPortal.Constant.IPC.CONTRACTAGENTMERCHANT;
            //}
            #endregion





            if (int.Parse(ConfigurationManager.AppSettings["IBMBSameUser"].ToString()) == 1)
            {
                ddlpolicyIB.Enabled = false;
            }

            if (int.Parse(ConfigurationManager.AppSettings["MBAMSameUser"].ToString()) == 1)
            {
                //txtAMPhoneNo.Text = txtMBPhoneNo.Text = PHONE;
                //txtMBPhoneNguoiUyQuyen.Text = txtWLNguoiUyQuyen.Text = PHONE;
                txtMBPhoneNo.Text = txtAMPhoneNo.Text = PHONE;
                txtAMPhoneNo.Enabled = false;
                txtWLNguoiUyQuyen.Text = PHONE;

            }
            else
            {
                txtMBPhoneNo.Text = SmartPortal.Common.Utilities.Utility.GetID("", txtCustCodeInfo.Text.Trim(), "", 10) + "1";
                txtMBPhoneNguoiUyQuyen.Text = SmartPortal.Common.Utilities.Utility.GetID("", txtCustCodeInfo.Text.Trim(), "", 10) + "2";
            }
            if (int.Parse(ConfigurationManager.AppSettings["MBSMSSameUser"].ToString()) == 1)
            {
                //txtSMSPhoneNguoiUyQuyen.Text = PHONE;
                //txtSMSPhoneNo.Enabled = txtSMSPhoneNguoiUyQuyen.Enabled = false;
                //txtSMSPhoneNo.Text = PHONE;
                txtSMSPhoneNo.Text = PHONE;
                txtSMSPhoneNo.Enabled = false;
            }


            btnResetNguoiUyQuyen.Attributes.Add("onclick", "return SetUserName('" + txtCustCodeInfo.Text.Trim() + "','" + ddlCustType.SelectedValue.Trim() + "',12)");


            txtUsernameIB.Enabled = false;
            txtIBGenUserNameNguoiUyQuyen.Enabled = false;


            #region Chủ tài khoản
            lblError.Text = "";

            #region lay tat ca cac account cua khach hang
            DataSet dsQT = new DataSet();

            switch (ddlCustType.SelectedValue.Trim())
            {
                case SmartPortal.Constant.IPC.PERSONAL:
                    dsQT = new SmartPortal.SEMS.Customer().GetAcctNo(txtCustCodeInfo.Text.Trim(), ddlCustType.SelectedValue, ref IPCERRORCODE, ref IPCERRORDESC);
                    break;
                    //case SmartPortal.Constant.IPC.PERSONALLKG:
                    //    dsQT = new SmartPortal.SEMS.Customer().GetAcctNo(txtLkgCode.Text.Trim(), ddlCustType.SelectedValue, ref IPCERRORCODE, ref IPCERRORDESC);
                    //    break;
                    //case SmartPortal.Constant.IPC.CORPORATE:
                    //    dsQT = new SmartPortal.SEMS.Customer().GetAcctNo(txtGrpCode.Text.Trim(), ddlCustType.SelectedValue, ref IPCERRORCODE, ref IPCERRORDESC);
                    //    break;
            }

            if (IPCERRORCODE != "0")
            {
                lblError.Text = Resources.labels.khachhangnaykhongtontaitaikhoandedangky;
            }
            if (dsQT.Tables.Count == 0)
            {
                lblError.Text = Resources.labels.khachhangnaykhongtontaitaikhoandedangky;
            }
            if (dsQT.Tables[0].Rows.Count == 0)
            {
                lblError.Text = Resources.labels.khachhangnaykhongtontaitaikhoandedangky;
            }

            DataTable dtAccountQT = new DataTable();
            dtAccountQT = dsQT.Tables[0];
            ViewState["AccountList"] = dtAccountQT;

            if (dtAccountQT.Rows.Count == 0)
            {
                lblError.Text = Resources.labels.khachhangnaykhongtontaitaikhoandedangky;
                return;
            }
            else
            {

                if (!dtAccountQT.Columns.Contains("ACCOUNTNO"))
                {
                    lblError.Text = Resources.labels.custnotexist;
                    return;
                }
                else
                {
                    ddlAccountMB.DataSource = dtAccountQT;
                    ddlAccountMB.DataTextField = "ACCOUNTNO";
                    ddlAccountMB.DataValueField = "ACCOUNTNO";
                    ddlAccountMB.DataBind();
                    ddlAccountMB.Items.Insert(0, new ListItem("ALL", "ALL"));
                }


                //phongtt sms notification fee
                lsAccNo.Clear();
                foreach (DataRow dr in dtAccountQT.Rows)
                {
                    if (!lsAccNo.Contains(dr["ACCOUNTNO"].ToString()))
                    {
                        lsAccNo.Add(dr["ACCOUNTNO"].ToString());
                    }
                }

                //ddlAccount.Items.Insert(0,new ListItem("----------Chọn tài khoản----------",""));

            }
            #endregion

            #region Hien thi tat cac cac role theo serviceid va usertype len cay
            //load for IB
            //LoadDataInTreeview(SmartPortal.Constant.IPC.IB, tvIB, SmartPortal.Constant.IPC.NORMAL);

            //load for SMS
            LoadDataInTreeview(SmartPortal.Constant.IPC.SMS, tvSMS, string.Empty);

            //load for MB
            LoadDataInTreeview(SmartPortal.Constant.IPC.AM, tvMB, SmartPortal.Constant.IPC.MBA);
            //load for EW
            LoadDataInTreeview(SmartPortal.Constant.IPC.AM, tvAM, SmartPortal.Constant.IPC.EAM);
            #endregion

            #region Xóa hết tất cả role chọn
            //foreach (ListItem liIB in cblIB.Items)
            //{

            //    liIB.Selected = false;

            //}
            #endregion

            #region lay role mac dinh
            GetRoleDefault(tvIB, tvSMS, tvMB, tvAM);
            #endregion

            #region show popup chi tiet ve role


            //foreach (ListItem liIB in cblIB.Items)
            //{
            //    //onmouseover="<%=Resources.labels.pagenametip %>" onmouseout="UnTip()"
            //    //Tip('The name of page', LEFT, true, BGCOLOR, '#FF9900', FADEIN, 400)
            //    tblRoleDefault = (new SmartPortal.SEMS.Role().GetTranOfRole(liIB.Value, ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
            //    //add vao thuoc tinh cua listitem
            //    string tooltip = "";
            //    foreach (DataRow row in tblRoleDefault.Rows)
            //    {
            //        tooltip += row["PAGENAME"].ToString() + "<br/>";
            //    }
            //    if (tblRoleDefault.Rows.Count != 0)
            //    {
            //        //add
            //        liIB.Attributes.Add("onmouseover", "Tip('" + tooltip + "', LEFT, true, BGCOLOR, '#FF9900', FADEIN, 400)");
            //        liIB.Attributes.Add("onmouseout", "UnTip()");
            //    }
            //}
            #endregion

            #endregion

            #region Người ủy quyền

            #region lay tat ca cac account cua khach hang
            DataSet dsUyQuyen = new DataSet();
            switch (ddlCustType.SelectedValue.Trim())
            {
                case SmartPortal.Constant.IPC.PERSONAL:
                    dsUyQuyen = new SmartPortal.SEMS.Customer().GetAcctNo(txtCustCodeInfo.Text.Trim(), ddlCustType.SelectedValue, ref IPCERRORCODE, ref IPCERRORDESC);
                    break;
                    //case SmartPortal.Constant.IPC.PERSONALLKG:
                    //    dsUyQuyen = new SmartPortal.SEMS.Customer().GetAcctNo(txtLkgCode.Text.Trim(), ddlCustType.SelectedValue, ref IPCERRORCODE, ref IPCERRORDESC);
                    //    break;
                    //case SmartPortal.Constant.IPC.CORPORATE:
                    //    dsUyQuyen = new SmartPortal.SEMS.Customer().GetAcctNo(txtGrpCode.Text.Trim(), ddlCustType.SelectedValue, ref IPCERRORCODE, ref IPCERRORDESC);
                    //    break;
            }
            if (IPCERRORCODE != "0")
            {
                throw new IPCException(SmartPortal.Constant.IPC.ERRORCODE.IPC);
            }
            DataTable dtAccountUyQuyen = new DataTable();
            dtAccountUyQuyen = dsUyQuyen.Tables[0];
            if (dtAccountUyQuyen.Rows.Count == 0)
            {
                lblError.Text = Resources.labels.khachhangnaykhongtontaitaikhoandedangky;
                return;
            }
            else
            {
                ddlSMSDefaultAcctnoUyQuyen.DataSource = dtAccountUyQuyen;
                ddlSMSDefaultAcctnoUyQuyen.DataTextField = "ACCOUNTNO";
                ddlSMSDefaultAcctnoUyQuyen.DataValueField = "ACCOUNTNO";
                ddlSMSDefaultAcctnoUyQuyen.DataBind();


                ddlAccountUyQuyen.DataSource = dtAccountUyQuyen;
                ddlAccountUyQuyen.DataTextField = "ACCOUNTNO";
                ddlAccountUyQuyen.DataValueField = "ACCOUNTNO";
                ddlAccountUyQuyen.DataBind();



                ddlAccUyQuyen.DataSource = dtAccountUyQuyen;
                ddlAccUyQuyen.DataTextField = "ACCOUNTNO";
                ddlAccUyQuyen.DataValueField = "ACCOUNTNO";
                ddlAccUyQuyen.DataBind();
                ddlAccUyQuyen.Items.Insert(0, new ListItem("ALL", "ALL"));

                //phongtt sms notification fee
                lsAccNo.Clear();
                foreach (DataRow dr in dtAccountUyQuyen.Rows)
                {
                    if (!lsAccNo.Contains(dr["ACCOUNTNO"].ToString()))
                    {
                        lsAccNo.Add(dr["ACCOUNTNO"].ToString());
                    }
                }


                //ddlAccountUyQuyen.Items.Insert(0, new ListItem("----------Chọn tài khoản----------", ""));

            }
            #endregion

            #region Hien thi tat cac cac role theo serviceid va usertype len cay
            //load for IB
            //LoadDataInTreeview(SmartPortal.Constant.IPC.IB, tvIBUyQuyen, ddlUserType.SelectedValue);

            //load for SMS
            LoadDataInTreeview(SmartPortal.Constant.IPC.SMS, tvSMSUyQuyen, ddlUserType.SelectedValue);

            //load for MB
            LoadDataInTreeview(SmartPortal.Constant.IPC.AM, tvMBUyQuyen, SmartPortal.Constant.IPC.MBA);

            //load for Wallet
            LoadDataInTreeview(SmartPortal.Constant.IPC.AM, tvWLUyQuyen, SmartPortal.Constant.IPC.EAM);
            #endregion

            #region lay role mac dinh
            GetRoleDefault(tvIBUyQuyen, tvSMSUyQuyen, tvMBUyQuyen, tvWLUyQuyen);
            #endregion

            #endregion


            pnCustInfo.Visible = false;
            pnPersonal.Visible = true;
            TabDSH.Visible = false;
            txtSMSPhoneNguoiUyQuyen.Enabled = false;
            //luu lai KYCID khi bam next
            loadData();

            if (ViewState["LICENSETYPE"] == null)
            {
                ViewState["LICENSETYPE"] = ddlKYCName.SelectedValue.ToString();
            }

        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(IPCex.ToString(), this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(IPCex.Message, Request.Url.Query);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSCustomerList_Add_Widget", "btnNext_Click", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }

    }
    void ReleaseSession()
    {
        ViewState["CHUTAIKHOAN"] = null;
        ViewState["NGUOIUYQUYEN"] = null;

    }

    void LoadDataInTreeview(string serviceID, TreeView tvPage, string roleType)
    {
        tvPage.Nodes.Clear();
        DataTable tblSS = new DataTable();
        //tblSS = new SmartPortal.SEMS.Role().GetByServiceAndUserType(serviceID, userType);
        tblSS = (new SmartPortal.SEMS.Role().GetRoleDefaultByServiceID(serviceID, ddlProduct.SelectedValue, decimal.Parse(ddlContractLevel.SelectedValue), roleType, ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];

        foreach (DataRow row in tblSS.Rows)
        {
            TreeNode node = new TreeNode(row["ROLENAME"].ToString(), row["ROLEID"].ToString());
            node.ShowCheckBox = true;

            DataSet dsTransaction = new DataSet();
            DataTable tblPage = new DataTable();
            dsTransaction = new SmartPortal.SEMS.Role().GetTranOfRole(row["ROLEID"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                tblPage = dsTransaction.Tables[0];
            }
            foreach (DataRow row1 in tblPage.Rows)
            {
                TreeNode node1 = new TreeNode(row1["PAGENAME"].ToString(), row1["TRANCODE"].ToString());
                node1.ShowCheckBox = false;
                //node1.SelectAction = TreeNodeSelectAction.None;
                node1.ToolTip = row1["PageDescription"].ToString();


                node.ChildNodes.Add(node1);
            }
            if (tblPage.Rows.Count != 0)
            {
                tvPage.Nodes.Add(node);
            }
        }
        tvPage.Attributes.Add("onclick", "OnTreeClick(event)");

    }

    void GetRoleDefault(TreeView treeIB, TreeView treeSMS, TreeView treeMB, TreeView treeAM)
    {
        DataTable tblRoleDefault = new DataTable();
        //lay role mac dinh IB
        tblRoleDefault = (new SmartPortal.SEMS.Role().GetRoleDefaultByServiceID(SmartPortal.Constant.IPC.IB, ddlProduct.SelectedValue, decimal.Parse(ddlContractLevel.SelectedValue), string.Empty, ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];

        foreach (TreeNode liIB in treeIB.Nodes)
        {
            if (tblRoleDefault.Select("ROLEID=" + liIB.Value).Length != 0)
            {
                liIB.Checked = true;

                //check node con (Trancode)
                foreach (TreeNode tnTranCode in liIB.ChildNodes)
                {
                    tnTranCode.Checked = true;
                }
            }
            else
            {
                liIB.Checked = false;
            }
        }

        //lay role mac dinh SMS
        tblRoleDefault = (new SmartPortal.SEMS.Role().GetRoleDefaultByServiceID(SmartPortal.Constant.IPC.SMS, ddlProduct.SelectedValue, decimal.Parse(ddlContractLevel.SelectedValue), SmartPortal.Constant.IPC.NORMAL, ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];

        //vutt 30112016 off by default 4 sms notification right
        foreach (TreeNode liSMS in treeSMS.Nodes)
        {
            DataRow[] dr = tblRoleDefault.Select("ROLEID=" + liSMS.Value);
            if (dr.Length != 0)
            {
                bool flag = !dr[0]["RoleType"].ToString().Equals("SNO");

                liSMS.Checked = flag;

                //check node con (Trancode)
                foreach (TreeNode tnTranCode in liSMS.ChildNodes)
                {
                    tnTranCode.Checked = flag;
                }
            }
            else
            {
                liSMS.Checked = false;
            }
        }


        //lay role mac dinh MOBILE AM
        tblRoleDefault = (new SmartPortal.SEMS.Role().GetRoleDefaultByServiceID(SmartPortal.Constant.IPC.AM, ddlProduct.SelectedValue, decimal.Parse(ddlContractLevel.SelectedValue), SmartPortal.Constant.IPC.MBA, ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];

        foreach (TreeNode liMB in treeMB.Nodes)
        {
            if (tblRoleDefault.Select("ROLEID=" + liMB.Value).Length != 0)
            {
                liMB.Checked = true;

                //check node con (Trancode)
                foreach (TreeNode tnTranCode in liMB.ChildNodes)
                {
                    tnTranCode.Checked = true;
                }
            }
            else
            {
                liMB.Checked = false;
            }
        }
        //lay role mac dinh AM
        tblRoleDefault = (new SmartPortal.SEMS.Role().GetRoleDefaultByServiceID(SmartPortal.Constant.IPC.AM, ddlProduct.SelectedValue, decimal.Parse(ddlContractLevel.SelectedValue), SmartPortal.Constant.IPC.EAM, ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];

        foreach (TreeNode liWL in treeAM.Nodes)
        {
            if (tblRoleDefault.Select("ROLEID=" + liWL.Value).Length != 0)
            {
                liWL.Checked = true;

                //check node con (Trancode)
                foreach (TreeNode tnTranCode in liWL.ChildNodes)
                {
                    tnTranCode.Checked = true;
                }
            }
            else
            {
                liWL.Checked = false;
            }
        }

    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        try
        {
            pnCustInfo.Visible = true;
            pnPersonal.Visible = false;

            SetRadio();
        }
        catch
        {
        }
    }
    protected void btnCustSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (!ValidateSave())
            {
                return;
            }
            string userChuTK = string.Empty;
            string userNguoiUQ = string.Empty;
            string phoneAccount = string.Empty;
            string walletID = string.Empty;
            string UserNameMB = string.Empty;
            string MerchantCode = string.Empty;
            if (ddlUserType.SelectedValue.Equals("M"))
            {
                MerchantCode = ddlMerchantCode.SelectedValue;
            }
            UserNameMB = txtUserNameMB.Text.Trim();
            if (!CheckUserNameExists(UserNameMB) || UserNameMB == "")
            {
                lblError.Text = Resources.labels.usernamehasexists;
                return;
            }
            if (!CheckExistPhoneNumber(txtPhoneWL.Text))
            {
                lblError.Text = Resources.labels.phonenumberisalreadyregistered;
                return;
            }
            else
            {
                PHONE = txtAMPhoneNo.Text.Trim();
                walletID = new SmartPortal.SEMS.User().CreatePrimaryKey(string.Empty, string.Empty, "WALLETID", ref IPCERRORCODE, ref IPCERRORDESC);
            }
            if (ViewState["CHUTAIKHOAN"] == null)
            {
                lblError.Text = Resources.labels.bancandiendayduthongtinchutaikhoan;
                return;
            }

            DataTable tblCHUTAIKHOAN = new DataTable();
            DataTable tblNGUOIUYQUYEN = new DataTable();
            DataTable tblLEVEL2 = new DataTable();
            DataTable tblNGUOIQUANTRI = new DataTable();

            if (ViewState["CHUTAIKHOAN"] != null)
            {
                tblCHUTAIKHOAN = (DataTable)ViewState["CHUTAIKHOAN"];
                if (tblCHUTAIKHOAN.Rows.Count == 0)
                {
                    lblError.Text = Resources.labels.bancandiendayduthongtinchutaikhoan;
                    return;
                }
                userChuTK = tblCHUTAIKHOAN.Rows[0]["colIBUserName"].ToString();
                phoneAccount = tblCHUTAIKHOAN.Rows[0]["colPhone"].ToString();

                DataRow dr = tblCHUTAIKHOAN.Select("").FirstOrDefault(x => (string)x["colRoleType"] == SmartPortal.Constant.IPC.MBA);
                if (dr == null)
                {
                    lblError.Text = Resources.labels.youneedmbservice;
                    return;
                }
            }
            //minh add 26/8/2016 them dk bat buoc phai co email hoac sms phone
            string emailctk = "";
            string smsphonectk = "";
            emailctk = tblCHUTAIKHOAN.Rows[0]["colEmail"].ToString();
            //smsphonectk = tblCHUTAIKHOAN.Rows[0]["colSMSPhone"].ToString();
            foreach (DataRow r in tblCHUTAIKHOAN.Rows)
            {
                if (r["colServiceID"].ToString() == SmartPortal.Constant.IPC.SMS)
                {
                    smsphonectk = r["colSMSPhone"].ToString();
                    break;
                }
            }
            //if (emailctk == string.Empty && smsphonectk == string.Empty)
            //{
            //    lblAlert.Text = Resources.labels.chutaikhoanphaidangkyitnhatemailhoacsmsservice;
            //    return;
            //}



            if (ViewState["NGUOIUYQUYEN"] != null)
            {
                tblNGUOIUYQUYEN = (DataTable)ViewState["NGUOIUYQUYEN"];
                string emailnuq = "";
                string smsphonenuq = "";
                userNguoiUQ = tblNGUOIUYQUYEN.Rows[0]["colIBUserName"].ToString();
                emailnuq = tblNGUOIUYQUYEN.Rows[0]["colEmail"].ToString();
                //string smsphonenuq = "";
                foreach (DataRow r in tblNGUOIUYQUYEN.Rows)
                {
                    if (r["colServiceID"].ToString() == SmartPortal.Constant.IPC.SMS)
                    {
                        smsphonenuq = r["colSMSPhone"].ToString();
                        break;
                    }
                }
                if (emailnuq == string.Empty && smsphonenuq == string.Empty)
                {
                    lblAlert.Text = Resources.labels.nguoiuyquyenphaidangkyitnhatemailhoacsmsservice;
                    return;
                }
            }



            #region Tạo 1 bảng tổng hợp 3 user trên
            DataTable tblSUM = new DataTable();

            DataColumn colFullName = new DataColumn("colFullName");
            DataColumn colLocalname = new DataColumn("colLocalname");
            DataColumn colLoginMethod = new DataColumn("colLoginMethod");
            DataColumn colAuthenType = new DataColumn("colAuthenType");
            DataColumn colLevel = new DataColumn("colLevel");
            DataColumn colBirthday = new DataColumn("colBirthday");
            DataColumn colGender = new DataColumn("colGender");
            DataColumn colPhone = new DataColumn("colPhone");
            DataColumn colEmail = new DataColumn("colEmail");
            DataColumn colAddress = new DataColumn("colAddress");
            DataColumn colUT = new DataColumn("colUT");
            DataColumn colIBUserName = new DataColumn("colIBUserName");
            DataColumn colIBPass = new DataColumn("colIBPass");
            DataColumn colSMSPhone = new DataColumn("colSMSPhone");
            DataColumn colSMSDefaultAcctno = new DataColumn("colSMSDefaultAcctno");
            DataColumn colSMSDefaultLang = new DataColumn("colSMSDefaultLang");
            DataColumn colSMSIsDefault = new DataColumn("colSMSIsDefault");
            DataColumn colSMSPinCode = new DataColumn("colSMSPinCode");
            DataColumn colMBPhone = new DataColumn("colMBPhone");
            DataColumn colMBPass = new DataColumn("colMBPass");
            DataColumn colMBPinCode = new DataColumn("colMBPinCode");
            DataColumn colRoleType = new DataColumn("colRoleType");
            DataColumn colWLPhone = new DataColumn("colWLPhone");
            DataColumn colWLPass = new DataColumn("colWLPass");
            DataColumn colWLPinCode = new DataColumn("colWLPinCode");
            DataColumn colAccount = new DataColumn("colAccount");
            DataColumn colRole = new DataColumn("colRole");
            DataColumn colRoleIDS = new DataColumn("colRoleID");
            DataColumn colTranCodeS = new DataColumn("colTranCode");
            DataColumn colTranCodeID = new DataColumn("colTranCodeID");
            DataColumn colServiceIDS = new DataColumn("colServiceID");
            DataColumn colIBPolicy = new DataColumn("colIBPolicy");
            DataColumn colSMSPolicy = new DataColumn("colSMSPolicy");
            DataColumn colMBPolicy = new DataColumn("colMBPolicy");
            DataColumn colWLPolicy = new DataColumn("colWLPolicy");
            DataColumn colpwdreset = new DataColumn("colpwdreset");
            //thêm TypeID
            DataColumn colTypeID = new DataColumn("colTypeID");


            tblSUM.Columns.Add(colFullName);
            tblSUM.Columns.Add(colLocalname);
            tblSUM.Columns.Add(colLoginMethod);
            tblSUM.Columns.Add(colAuthenType);
            tblSUM.Columns.Add(colLevel);
            tblSUM.Columns.Add(colBirthday);
            tblSUM.Columns.Add(colGender);
            tblSUM.Columns.Add(colPhone);
            tblSUM.Columns.Add(colEmail);
            tblSUM.Columns.Add(colAddress);
            tblSUM.Columns.Add(colUT);
            tblSUM.Columns.Add(colIBUserName);
            tblSUM.Columns.Add(colIBPass);
            tblSUM.Columns.Add(colSMSPhone);
            tblSUM.Columns.Add(colSMSDefaultAcctno);
            tblSUM.Columns.Add(colSMSDefaultLang);
            tblSUM.Columns.Add(colSMSIsDefault);
            tblSUM.Columns.Add(colSMSPinCode);
            tblSUM.Columns.Add(colMBPhone);
            tblSUM.Columns.Add(colMBPass);
            tblSUM.Columns.Add(colMBPinCode);
            tblSUM.Columns.Add(colRoleType);
            tblSUM.Columns.Add(colWLPhone);
            tblSUM.Columns.Add(colWLPass);
            tblSUM.Columns.Add(colWLPinCode);
            tblSUM.Columns.Add(colAccount);
            tblSUM.Columns.Add(colRole);
            tblSUM.Columns.Add(colRoleIDS);
            tblSUM.Columns.Add(colTranCodeS);
            tblSUM.Columns.Add(colTranCodeID);
            tblSUM.Columns.Add(colServiceIDS);
            tblSUM.Columns.Add(colIBPolicy);
            tblSUM.Columns.Add(colSMSPolicy);
            tblSUM.Columns.Add(colMBPolicy);
            tblSUM.Columns.Add(colWLPolicy);
            tblSUM.Columns.Add(colpwdreset);
            tblSUM.Columns.Add(colTypeID);


            //lấy thông tin trong bảng CHUTAIKHOAN
            foreach (DataRow dongCTK in tblCHUTAIKHOAN.Rows)
            {
                DataRow rowNguoiUyQuyen = tblSUM.NewRow();
                rowNguoiUyQuyen["colFullName"] = dongCTK["colFullName"].ToString();
                rowNguoiUyQuyen["colLocalname"] = dongCTK["colLocalname"].ToString();
                rowNguoiUyQuyen["colLoginMethod"] = dongCTK["colLoginMethod"].ToString();
                rowNguoiUyQuyen["colAuthenType"] = dongCTK["colAuthenType"].ToString();
                rowNguoiUyQuyen["colLevel"] = dongCTK["colLevel"].ToString();
                rowNguoiUyQuyen["colBirthday"] = dongCTK["colBirthday"].ToString();
                rowNguoiUyQuyen["colGender"] = dongCTK["colGender"].ToString();
                rowNguoiUyQuyen["colPhone"] = dongCTK["colPhone"].ToString();
                rowNguoiUyQuyen["colEmail"] = dongCTK["colEmail"].ToString();
                rowNguoiUyQuyen["colAddress"] = dongCTK["colAddress"].ToString();
                rowNguoiUyQuyen["colUT"] = ddlContractLevel.SelectedValue;
                rowNguoiUyQuyen["colIBUserName"] = dongCTK["colIBUserName"].ToString();
                rowNguoiUyQuyen["colIBPass"] = dongCTK["colIBPass"].ToString();
                rowNguoiUyQuyen["colSMSPhone"] = dongCTK["colSMSPhone"].ToString();
                rowNguoiUyQuyen["colSMSDefaultAcctno"] = dongCTK["colSMSDefaultAcctno"].ToString();
                rowNguoiUyQuyen["colSMSDefaultLang"] = dongCTK["colSMSDefaultLang"].ToString();
                rowNguoiUyQuyen["colSMSIsDefault"] = dongCTK["colSMSIsDefault"].ToString();
                rowNguoiUyQuyen["colSMSPinCode"] = dongCTK["colSMSPinCode"].ToString();
                rowNguoiUyQuyen["colMBPhone"] = dongCTK["colMBPhone"].ToString();
                rowNguoiUyQuyen["colMBPass"] = dongCTK["colMBPass"].ToString();
                rowNguoiUyQuyen["colMBPinCode"] = dongCTK["colMBPinCode"].ToString();
                rowNguoiUyQuyen["colRoleType"] = dongCTK["colRoleType"].ToString();
                rowNguoiUyQuyen["colWLPhone"] = dongCTK["colWLPhone"].ToString();
                rowNguoiUyQuyen["colWLPass"] = dongCTK["colWLPass"].ToString();
                rowNguoiUyQuyen["colWlPinCode"] = dongCTK["colWLPinCode"].ToString();
                rowNguoiUyQuyen["colAccount"] = dongCTK["colAccount"].ToString();
                rowNguoiUyQuyen["colRole"] = dongCTK["colRole"].ToString();
                rowNguoiUyQuyen["colRoleID"] = dongCTK["colRoleID"].ToString();
                rowNguoiUyQuyen["colTranCode"] = dongCTK["colTranCode"].ToString();
                rowNguoiUyQuyen["colTranCodeID"] = dongCTK["colTranCodeID"].ToString();
                rowNguoiUyQuyen["colServiceID"] = dongCTK["colServiceID"].ToString();
                rowNguoiUyQuyen["colIBPolicy"] = dongCTK["colIBPolicy"].ToString();
                rowNguoiUyQuyen["colSMSPolicy"] = dongCTK["colSMSPolicy"].ToString();
                rowNguoiUyQuyen["colMBPolicy"] = dongCTK["colMBPolicy"].ToString();
                rowNguoiUyQuyen["colWLPolicy"] = dongCTK["colWLPolicy"].ToString();
                rowNguoiUyQuyen["colpwdreset"] = dongCTK["colpwdreset"].ToString();
                rowNguoiUyQuyen["colTypeID"] = SmartPortal.Constant.IPC.CHUTAIKHOAN;


                tblSUM.Rows.Add(rowNguoiUyQuyen);
            }

            //lấy thông tin trong bảng NGUOIUYQUYEN
            foreach (DataRow dongCTK in tblNGUOIUYQUYEN.Rows)
            {
                DataRow rowNguoiUyQuyen = tblSUM.NewRow();
                rowNguoiUyQuyen["colFullName"] = dongCTK["colFullName"].ToString();
                rowNguoiUyQuyen["colLocalname"] = dongCTK["colLocalname"].ToString();
                rowNguoiUyQuyen["colLoginMethod"] = dongCTK["colLoginMethod"].ToString();
                rowNguoiUyQuyen["colAuthenType"] = dongCTK["colAuthenType"].ToString();
                rowNguoiUyQuyen["colLevel"] = dongCTK["colLevel"].ToString();
                rowNguoiUyQuyen["colBirthday"] = dongCTK["colBirthday"].ToString();
                rowNguoiUyQuyen["colGender"] = dongCTK["colGender"].ToString();
                rowNguoiUyQuyen["colPhone"] = dongCTK["colPhone"].ToString();
                rowNguoiUyQuyen["colEmail"] = dongCTK["colEmail"].ToString();
                rowNguoiUyQuyen["colAddress"] = dongCTK["colAddress"].ToString();
                rowNguoiUyQuyen["colUT"] = SmartPortal.Constant.IPC.RP;
                rowNguoiUyQuyen["colIBUserName"] = dongCTK["colIBUserName"].ToString();
                rowNguoiUyQuyen["colIBPass"] = dongCTK["colIBPass"].ToString();
                rowNguoiUyQuyen["colSMSPhone"] = dongCTK["colSMSPhone"].ToString();
                rowNguoiUyQuyen["colSMSDefaultAcctno"] = dongCTK["colSMSDefaultAcctno"].ToString();
                rowNguoiUyQuyen["colSMSDefaultLang"] = dongCTK["colSMSDefaultLang"].ToString();
                rowNguoiUyQuyen["colSMSIsDefault"] = dongCTK["colSMSIsDefault"].ToString();
                rowNguoiUyQuyen["colSMSPinCode"] = dongCTK["colSMSPinCode"].ToString();
                rowNguoiUyQuyen["colMBPhone"] = dongCTK["colMBPhone"].ToString();
                rowNguoiUyQuyen["colMBPass"] = dongCTK["colMBPass"].ToString();
                rowNguoiUyQuyen["colMBPinCode"] = dongCTK["colMBPinCode"].ToString();
                rowNguoiUyQuyen["colRoleType"] = dongCTK["colRoleType"].ToString();
                rowNguoiUyQuyen["colWLPhone"] = dongCTK["colWLPhone"].ToString();
                rowNguoiUyQuyen["colWLPass"] = dongCTK["colWLPass"].ToString();
                rowNguoiUyQuyen["colWlPinCode"] = dongCTK["colWLPinCode"].ToString();
                rowNguoiUyQuyen["colAccount"] = dongCTK["colAccount"].ToString();
                rowNguoiUyQuyen["colRole"] = dongCTK["colRole"].ToString();
                rowNguoiUyQuyen["colRoleID"] = dongCTK["colRoleID"].ToString();
                rowNguoiUyQuyen["colTranCode"] = dongCTK["colTranCode"].ToString();
                rowNguoiUyQuyen["colTranCodeID"] = dongCTK["colTranCodeID"].ToString();
                rowNguoiUyQuyen["colServiceID"] = dongCTK["colServiceID"].ToString();
                rowNguoiUyQuyen["colIBPolicy"] = dongCTK["colIBPolicy"].ToString();
                rowNguoiUyQuyen["colSMSPolicy"] = dongCTK["colSMSPolicy"].ToString();
                rowNguoiUyQuyen["colMBPolicy"] = dongCTK["colMBPolicy"].ToString();
                rowNguoiUyQuyen["colWLPolicy"] = dongCTK["colWLPolicy"].ToString();
                rowNguoiUyQuyen["colpwdreset"] = dongCTK["colpwdreset"].ToString();
                rowNguoiUyQuyen["colTypeID"] = SmartPortal.Constant.IPC.DONGCHUTAIKHOAN;


                tblSUM.Rows.Add(rowNguoiUyQuyen);
            }
            #endregion

            #region Biến thông tin Customer và Contract
            string custID = new SmartPortal.SEMS.User().CreatePrimaryKey(string.Empty, string.Empty, "CUSTID", ref IPCERRORCODE, ref IPCERRORDESC);
            string fullName = txtFullName.Text.Trim();
            string shortName = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtShortName.Text.Trim());
            string DOB = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtBirth.Text.Trim());
            if (DOB == "")
            {
                DOB = SmartPortal.Constant.IPC.DATEDEFAULT;
            }
            string re_addr = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtResidentAddr.Text.Trim());
            string temp_addr = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtTempAddress.Text.Trim());
            string SEX = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlGender.SelectedValue);
            string Nation = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlNation.SelectedValue.Trim());
            string Tel = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtPhoneWL.Text.Trim());
            string FAX = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtFax.Text.Trim());
            string Email = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtEmail.Text.Trim());
            string LicenseType = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ViewState["LICENSETYPE"].ToString());
            string LicenseID = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtNRICNewNumber.Text.Trim());
            string issueDate = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtIssueDate.Text.Trim());
            if (issueDate == "")
            {
                issueDate = SmartPortal.Constant.IPC.DATEDEFAULT;
            }
            string township = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlTownship.SelectedValue.Trim());
            string region = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlRegion.SelectedValue.Trim());
            string issuePlace = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtIssuePlace.Text.Trim());
            string desc = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtDesc.Text.Trim());
            string job = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtJob.Text.Trim());
            string officeAddr = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtOfficeAddr.Text.Trim());
            string officePhone = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtCompanyPhone.Text.Trim());
            string custType = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlCustType.SelectedValue.Trim());
            string cfCode = string.Empty;
            switch (custType)
            {
                case SmartPortal.Constant.IPC.PERSONAL:

                    cfCode = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtCustCodeInfo.Text.Trim());
                    break;
                    //case SmartPortal.Constant.IPC.PERSONALLKG:
                    //    cfCode = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtLkgCode.Text.Trim());
                    //    break;
                    //case SmartPortal.Constant.IPC.CORPORATE:
                    //    cfCode = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtGrpCode.Text.Trim());
                    //    break;
            }
            string branchID = Session["branch"].ToString();
            string custBranchID = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlBranch.SelectedValue);

            string contractNo = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtContractNo.Text.Trim());
            string contractType = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlUserType.SelectedValue.Trim());
            string productID = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlProduct.SelectedValue.Trim());
            string startDate = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtStartDate.Text.Trim());
            string endDate = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtEndDate.Text.Trim());
            string lastModify = SmartPortal.Common.Utilities.Utility.KillSqlInjection(DateTime.Now.ToString("dd/MM/yyyy"));
            string userCreate = SmartPortal.Common.Utilities.Utility.KillSqlInjection(Session["userName"].ToString().Trim());
            string userLastModify = SmartPortal.Common.Utilities.Utility.KillSqlInjection(Session["userName"].ToString().Trim());
            string userApprove = "";
            string status = SmartPortal.Constant.IPC.NEW;
            string allAcct = "Y";
            string isSpecialMan = "";
            //thaity modify at 14/7/2014
            //string needToApprove = (cbxneedtoapprovetransaction.Checked ==true)?"Y":"N";
            //thaity modify at 14/7/2014

            #endregion


            string userType = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlUserType.SelectedValue);
            string deptID = "";
            string uCreateDate = DateTime.Now.ToString("dd/MM/yyyy");
            string tokenID = "";
            string tokenIssueDate = "01/01/1900";
            string smsOTP = "";

            string IBUserName = "";
            string IBPassword = "";

            string SMSPhoneNo = "";
            string SMSDefaultAcctno = "";
            string SMSDefaultLang = "";

            string MBPhoneNo = "";
            string MBPass = "";

            string PHOPhoneNo = "";
            string PHOPass = "";

            if (int.Parse(ConfigurationManager.AppSettings["IBMBSameUser"].ToString()) == 1)
            {
                IBUserName = UserNameMB;
            }

            #region Tạo bảng chứa thông tin user
            DataTable tblUser = new DataTable();
            DataColumn colUserID = new DataColumn("colUserID");
            DataColumn colUContractNo = new DataColumn("colUContractNo");
            DataColumn colULocalName = new DataColumn("colULocalName");
            DataColumn colUFullName = new DataColumn("colUFullName");
            DataColumn colUGender = new DataColumn("colUGender");
            DataColumn colUAddress = new DataColumn("colUAddress");
            DataColumn colUEmail = new DataColumn("colUEmail");
            DataColumn colUPhone = new DataColumn("colUPhone");
            DataColumn colUStatus = new DataColumn("colUStatus");
            DataColumn colUUserCreate = new DataColumn("colUUserCreate");
            DataColumn colUDateCreate = new DataColumn("colUDateCreate");
            DataColumn colUUserModify = new DataColumn("colUUserModify");
            DataColumn colULastModify = new DataColumn("colULastModify");
            DataColumn colUUserApprove = new DataColumn("colUUserApprove");
            DataColumn colUserType = new DataColumn("colUserType");
            DataColumn colUserLevel = new DataColumn("colUserLevel");
            DataColumn colDeptID = new DataColumn("colDeptID");
            DataColumn colTokenID = new DataColumn("colTokenID");
            DataColumn colTokenIssueDate = new DataColumn("colTokenIssueDate");
            DataColumn colSMSOTP = new DataColumn("colSMSOTP");
            DataColumn colSMSBirthday = new DataColumn("colSMSBirthday");
            DataColumn colTypeIDUser = new DataColumn("colTypeID");
            DataColumn colUSContractType = new DataColumn("colUSContractType");
            //add vào table
            tblUser.Columns.Add(colUserID);
            tblUser.Columns.Add(colUContractNo);
            tblUser.Columns.Add(colULocalName);
            tblUser.Columns.Add(colUFullName);
            tblUser.Columns.Add(colUGender);
            tblUser.Columns.Add(colUAddress);
            tblUser.Columns.Add(colUEmail);
            tblUser.Columns.Add(colUPhone);
            tblUser.Columns.Add(colUStatus);
            tblUser.Columns.Add(colUUserCreate);
            tblUser.Columns.Add(colUDateCreate);
            tblUser.Columns.Add(colUUserModify);
            tblUser.Columns.Add(colULastModify);
            tblUser.Columns.Add(colUUserApprove);
            tblUser.Columns.Add(colUserType);
            tblUser.Columns.Add(colUserLevel);
            tblUser.Columns.Add(colDeptID);
            tblUser.Columns.Add(colTokenID);
            tblUser.Columns.Add(colTokenIssueDate);
            tblUser.Columns.Add(colSMSOTP);
            tblUser.Columns.Add(colSMSBirthday);
            tblUser.Columns.Add(colTypeIDUser);
            tblUser.Columns.Add(colUSContractType);

            //tao 1 dong du lieu
            string UID = "";
            for (int i = 0; i < tblSUM.Rows.Count; i++)
            {
                if (tblSUM.Rows[i]["colIBUserName"].ToString().Trim() != UID.Trim())
                {
                    DataRow row2 = tblUser.NewRow();

                    row2["colUserID"] = tblSUM.Rows[i]["colIBUserName"].ToString();
                    row2["colUContractNo"] = contractNo;
                    row2["colULocalName"] = tblSUM.Rows[i]["colLocalname"].ToString();
                    row2["colUFullName"] = tblSUM.Rows[i]["colFullName"].ToString();
                    row2["colUGender"] = tblSUM.Rows[i]["colGender"].ToString();
                    row2["colUAddress"] = tblSUM.Rows[i]["colAddress"].ToString();
                    row2["colUEmail"] = tblSUM.Rows[i]["colEmail"].ToString();
                    row2["colUPhone"] = tblSUM.Rows[i]["colPhone"].ToString();
                    row2["colUStatus"] = status;
                    row2["colUUserCreate"] = userCreate;
                    row2["colUDateCreate"] = uCreateDate;
                    row2["colUUserModify"] = userCreate;
                    row2["colULastModify"] = lastModify;

                    row2["colUUserApprove"] = userApprove;
                    row2["colUserType"] = ddlSubUserType.SelectedValue;
                    row2["colUserLevel"] = tblSUM.Rows[i]["colLevel"].ToString();
                    row2["colDeptID"] = deptID;
                    row2["colTokenID"] = tokenID;
                    row2["colTokenIssueDate"] = tokenIssueDate;
                    row2["colSMSOTP"] = smsOTP;
                    row2["colSMSBirthday"] = tblSUM.Rows[i]["colBirthday"].ToString();
                    row2["colTypeID"] = tblSUM.Rows[i]["colTypeID"].ToString();
                    row2["colUSContractType"] = string.Empty;

                    tblUser.Rows.Add(row2);

                    UID = tblSUM.Rows[i]["colIBUserName"].ToString().Trim();
                }
            }

            #endregion

            #region Tạo bảng chứa user Ibank
            DataTable tblIbankUser = new DataTable();
            DataColumn colUserName = new DataColumn("colUserName");
            DataColumn colIBUserID = new DataColumn("colIBUserID");
            DataColumn colIBPassword = new DataColumn("colIBPassword");
            DataColumn colLastLoginTime = new DataColumn("colLastLoginTime");
            DataColumn colIBStatus = new DataColumn("colIBStatus");
            DataColumn colIBUserCreate = new DataColumn("colIBUserCreate");
            DataColumn colIBDateCreate = new DataColumn("colIBDateCreate");
            DataColumn colIBUserModify = new DataColumn("colIBUserModify");
            DataColumn colIBLastModify = new DataColumn("colIBLastModify");
            DataColumn colIBUserApprove = new DataColumn("colIBUserApprove");
            DataColumn colIBIsLogin = new DataColumn("colIBIsLogin");
            DataColumn colIBDateExpire = new DataColumn("colIBDateExpire");
            DataColumn colIBExpireTime = new DataColumn("colIBExpireTime");
            DataColumn colIBPolicyusr = new DataColumn("colIBPolicyusr");
            DataColumn colpwdresetIB = new DataColumn("colpwdresetIB");
            DataColumn colIBLoginMethod = new DataColumn("colIBLoginMethod");
            DataColumn colIBAuthenType = new DataColumn("colIBAuthenType");

            //add vào table
            tblIbankUser.Columns.Add(colUserName);
            tblIbankUser.Columns.Add(colIBUserID);
            tblIbankUser.Columns.Add(colIBPassword);
            tblIbankUser.Columns.Add(colLastLoginTime);
            tblIbankUser.Columns.Add(colIBStatus);
            tblIbankUser.Columns.Add(colIBUserCreate);
            tblIbankUser.Columns.Add(colIBDateCreate);
            tblIbankUser.Columns.Add(colIBUserModify);
            tblIbankUser.Columns.Add(colIBLastModify);
            tblIbankUser.Columns.Add(colIBUserApprove);
            tblIbankUser.Columns.Add(colIBIsLogin);
            tblIbankUser.Columns.Add(colIBDateExpire);
            tblIbankUser.Columns.Add(colIBExpireTime);
            tblIbankUser.Columns.Add(colIBPolicyusr);
            tblIbankUser.Columns.Add(colpwdresetIB);
            tblIbankUser.Columns.Add(colIBLoginMethod);
            tblIbankUser.Columns.Add(colIBAuthenType);
            try
            {
                string UN = "";
                foreach (DataRow rIBU in tblSUM.Rows)
                {
                    if (rIBU["colIBUserName"].ToString().Trim() != UN.Trim() && rIBU["colIBUserName"].ToString().Trim() != "")
                    {
                        DataRow row3 = tblIbankUser.NewRow();
                        row3["colUserName"] = IBUserName;
                        row3["colIBUserID"] = rIBU["colIBUserName"].ToString();
                        row3["colIBPassword"] = rIBU["colIBPass"].ToString();
                        row3["colLastLoginTime"] = uCreateDate;
                        row3["colIBStatus"] = status;
                        row3["colIBUserCreate"] = userCreate;
                        row3["colIBDateCreate"] = uCreateDate;
                        row3["colIBUserModify"] = userCreate;
                        row3["colIBLastModify"] = lastModify;
                        row3["colIBUserApprove"] = userApprove;
                        row3["colIBIsLogin"] = "0";
                        row3["colIBDateExpire"] = endDate;
                        row3["colIBExpireTime"] = startDate;
                        row3["colIBPolicyusr"] = rIBU["colIBPolicy"].ToString();
                        row3["colpwdresetIB"] = rIBU["colpwdreset"].ToString();
                        row3["colIBLoginMethod"] = SmartPortal.Constant.IPC.USERNAME;
                        row3["colIBAuthenType"] = SmartPortal.Constant.IPC.PASSWORD;

                        tblIbankUser.Rows.Add(row3);
                        //There is no IB tab
                        UN = rIBU["colIBUserName"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {

            }
            //tao 1 dong du lieu

            #endregion

            #region Tạo bảng chứa User SMS
            DataTable tblSMSUser = new DataTable();
            DataColumn colSMSUserID = new DataColumn("colSMSUserID");
            DataColumn colSMSPhoneNo = new DataColumn("colSMSPhoneNo");
            DataColumn colSMSContractNo = new DataColumn("colSMSContractNo");
            DataColumn colSMSIsBroadcast = new DataColumn("colSMSIsBroadcast");
            DataColumn colSMSDefaultAcctnoU = new DataColumn("colSMSDefaultAcctno");
            DataColumn colSMSDefaultLangU = new DataColumn("colSMSDefaultLang");
            DataColumn colSMSIsDefault1 = new DataColumn("colSMSIsDefault1");
            DataColumn colSMSPinCode1 = new DataColumn("colSMSPinCode1");
            DataColumn colSMSStatus = new DataColumn("colSMSStatus");
            DataColumn colSMSPhoneType = new DataColumn("colSMSPhoneType");
            DataColumn colSMSUserCreate = new DataColumn("colSMSUserCreate");
            DataColumn colSMSUserModify = new DataColumn("colSMSUserModify");
            DataColumn colSMSUserApprove = new DataColumn("colSMSUserApprove");
            DataColumn colSMSLastModify = new DataColumn("colSMSLastModify");
            DataColumn colSMSDateCreate = new DataColumn("colSMSDateCreate");
            DataColumn colSMSDateExpire = new DataColumn("colSMSDateExpire");
            DataColumn colSMSPolicyusr = new DataColumn("colSMSPolicyusr");
            DataColumn colpwdresetSMS = new DataColumn("colpwdresetSMS");


            //add vào table
            tblSMSUser.Columns.Add(colSMSUserID);
            tblSMSUser.Columns.Add(colSMSPhoneNo);
            tblSMSUser.Columns.Add(colSMSContractNo);
            tblSMSUser.Columns.Add(colSMSIsBroadcast);
            tblSMSUser.Columns.Add(colSMSDefaultAcctnoU);
            tblSMSUser.Columns.Add(colSMSDefaultLangU);
            tblSMSUser.Columns.Add(colSMSIsDefault1);
            tblSMSUser.Columns.Add(colSMSPinCode1);
            tblSMSUser.Columns.Add(colSMSStatus);
            tblSMSUser.Columns.Add(colSMSPhoneType);
            tblSMSUser.Columns.Add(colSMSUserCreate);
            tblSMSUser.Columns.Add(colSMSUserModify);
            tblSMSUser.Columns.Add(colSMSUserApprove);
            tblSMSUser.Columns.Add(colSMSLastModify);
            tblSMSUser.Columns.Add(colSMSDateCreate);
            tblSMSUser.Columns.Add(colSMSDateExpire);
            tblSMSUser.Columns.Add(colSMSPolicyusr);
            tblSMSUser.Columns.Add(colpwdresetSMS);

            //tao 1 dong du lieu
            string SMSP = "";
            foreach (DataRow rSMSU in tblSUM.Rows)
            {
                if (rSMSU["colSMSPhone"].ToString().Trim() != SMSP.Trim() && rSMSU["colSMSPhone"].ToString().Trim() != "")
                {
                    DataRow row4 = tblSMSUser.NewRow();
                    row4["colSMSUserID"] = rSMSU["colIBUserName"].ToString();
                    row4["colSMSPhoneNo"] = rSMSU["colSMSPhone"].ToString();
                    row4["colSMSContractNo"] = contractNo;
                    row4["colSMSIsBroadcast"] = "Y";
                    row4["colSMSDefaultAcctno"] = rSMSU["colSMSDefaultAcctno"].ToString();
                    row4["colSMSDefaultLang"] = rSMSU["colSMSDefaultLang"].ToString();
                    row4["colSMSIsDefault1"] = rSMSU["colSMSIsDefault"].ToString();
                    //row4["colSMSPinCode1"] = SmartPortal.SEMS.O9Encryptpass.sha_sha256(Encryption.Encrypt(rSMSU["colSMSPinCode"].ToString()), rSMSU["colSMSPhone"].ToString().Trim());
                    row4["colSMSPinCode1"] = SmartPortal.SEMS.O9Encryptpass.sha_sha256(rSMSU["colSMSPinCode"].ToString(), rSMSU["colSMSPhone"].ToString().Trim());
                    row4["colSMSStatus"] = status;
                    row4["colSMSPhoneType"] = "";
                    row4["colSMSUserCreate"] = userCreate;
                    row4["colSMSUserModify"] = userCreate;
                    row4["colSMSUserApprove"] = userApprove;
                    row4["colSMSLastModify"] = lastModify;
                    row4["colSMSDateCreate"] = uCreateDate;
                    row4["colSMSDateExpire"] = endDate;
                    row4["colSMSPolicyusr"] = rSMSU["colSMSPolicy"].ToString();
                    row4["colpwdresetSMS"] = Encryption.Encrypt(rSMSU["colSMSPinCode"].ToString());


                    //tblSMSUser.Rows.Add(row4);

                    SMSP = rSMSU["colSMSPhone"].ToString();
                }
            }
            #endregion

            #region Tạo bảng chứa User MB
            DataTable tblMBUser = new DataTable();
            DataColumn colMBUserID = new DataColumn("colMBUserID");
            DataColumn colMBUserName = new DataColumn("colMBUserName");
            DataColumn colMBPhoneNo = new DataColumn("colMBPhoneNo");
            DataColumn colMBLoginMethod = new DataColumn("colMBLoginMethod");
            DataColumn colMBAuthenType = new DataColumn("colMBAuthenType");
            DataColumn colMBNewPass = new DataColumn("colMBNewPass");
            DataColumn colMBNewPin = new DataColumn("colMBNewPin");
            DataColumn colMBPassU = new DataColumn("colMBPass");
            DataColumn colMBStatus = new DataColumn("colMBStatus");
            DataColumn colMBPinCode1 = new DataColumn("colMBPinCode1");
            DataColumn colMBPolicyusr = new DataColumn("colMBPolicyusr");
            DataColumn colpwdresetMB = new DataColumn("colpwdresetMB");
            DataColumn colPwdPinMB = new DataColumn("colPwdPinMB");
            DataColumn colContractType = new DataColumn("colContractType");

            //add vào table
            tblMBUser.Columns.Add(colMBUserID);
            tblMBUser.Columns.Add(colMBUserName);
            tblMBUser.Columns.Add(colMBPhoneNo);
            tblMBUser.Columns.Add(colMBLoginMethod);
            tblMBUser.Columns.Add(colMBAuthenType);
            tblMBUser.Columns.Add(colMBNewPass);
            tblMBUser.Columns.Add(colMBNewPin);
            tblMBUser.Columns.Add(colMBPassU);
            tblMBUser.Columns.Add(colMBStatus);
            tblMBUser.Columns.Add(colMBPinCode1);
            tblMBUser.Columns.Add(colMBPolicyusr);
            tblMBUser.Columns.Add(colpwdresetMB);
            tblMBUser.Columns.Add(colPwdPinMB);
            tblMBUser.Columns.Add(colContractType);

            int pincodeLenght = int.Parse(ConfigurationManager.AppSettings["PINCODELENGHT"].ToString());
            //tao 1 dong du lieu
            string MBP = "";
            foreach (DataRow rMBP in tblSUM.Rows)
            {
                if (rMBP["colMBPhone"].ToString().Trim() != MBP && rMBP["colMBPhone"].ToString().Trim() != "")
                {
                    //string passreveal = DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - pincodeLenght, pincodeLenght);
                    //string pwdreset = Encryption.Encrypt(passreveal);
                    //string PassPinMB = SmartPortal.SEMS.O9Encryptpass.sha_sha256(passreveal, USERCTK);

                    DataRow row5 = tblMBUser.NewRow();
                    row5["colMBUserID"] = rMBP["colIBUserName"].ToString();
                    row5["colMBUserName"] = UserNameMB;
                    row5["colMBPhoneNo"] = rMBP["colMBPhone"].ToString();
                    row5["colMBLoginMethod"] = rMBP["colLoginMethod"].ToString();
                    row5["colMBAuthenType"] = "PASSWORD";
                    row5["colMBNewPass"] = "Y";
                    row5["colMBNewPin"] = "N";
                    row5["colMBPass"] = rMBP["colMBPass"].ToString();
                    row5["colMBStatus"] = status;
                    row5["colMBPinCode1"] = string.Empty;
                    row5["colMBPolicyusr"] = rMBP["colMBPolicy"].ToString();
                    row5["colpwdresetMB"] = rMBP["colpwdreset"].ToString();
                    row5["colPwdPinMB"] = string.Empty;
                    row5["colContractType"] = SmartPortal.Constant.IPC.CONTRACTAGENTMERCHANT;
                    tblMBUser.Rows.Add(row5);

                    MBP = rMBP["colMBPhone"].ToString();
                }
            }
            #endregion

            #region Tạo bảng chứa quyền user Ibank
            //tao bang chua thong tin customer
            DataTable tblIbankUserRight = new DataTable();
            DataColumn colIBUserNameRight = new DataColumn("colIBUserNameRight");
            DataColumn colIBRoleID = new DataColumn("colIBRoleID");

            //add vào table
            tblIbankUserRight.Columns.Add(colIBUserNameRight);
            tblIbankUserRight.Columns.Add(colIBRoleID);

            //tao 1 dong du lieu
            DataRow[] arrIBR = tblSUM.Select("colServiceID='" + SmartPortal.Constant.IPC.IB + "'");
            foreach (DataRow rIBR in arrIBR)
            {
                if (tblIbankUserRight.Select("colIBUserNameRight='" + rIBR["colIBUserName"].ToString() + "' and colIBRoleID='" + rIBR["colRoleID"].ToString() + "'").Length == 0)
                {
                    DataRow newRowIBR = tblIbankUserRight.NewRow();
                    newRowIBR["colIBUserNameRight"] = rIBR["colIBUserName"].ToString();
                    newRowIBR["colIBRoleID"] = rIBR["colRoleID"].ToString();

                    tblIbankUserRight.Rows.Add(newRowIBR);
                }
            }
            #endregion

            #region Tạo bảng chứa quyền user SMS
            //tao bang chua thong tin customer
            DataTable tblSMSUserRight = new DataTable();
            DataColumn colSMSUserIDR = new DataColumn("colSMSUserID");
            DataColumn colSMSRoleID = new DataColumn("colSMSRoleID");

            //add vào table
            tblSMSUserRight.Columns.Add(colSMSUserIDR);
            tblSMSUserRight.Columns.Add(colSMSRoleID);

            //tao 1 dong du lieu
            DataRow[] arrSMSR = tblSUM.Select("colServiceID='" + SmartPortal.Constant.IPC.SMS + "'");
            foreach (DataRow rSMSR in arrSMSR)
            {
                if (tblSMSUserRight.Select("colSMSUserID='" + rSMSR["colIBUserName"].ToString() + "' and colSMSRoleID='" + rSMSR["colRoleID"].ToString() + "'").Length == 0)
                {
                    DataRow newRowSMSR = tblSMSUserRight.NewRow();
                    newRowSMSR["colSMSUserID"] = rSMSR["colIBUserName"].ToString();
                    newRowSMSR["colSMSRoleID"] = rSMSR["colRoleID"].ToString();

                    tblSMSUserRight.Rows.Add(newRowSMSR);
                }
            }
            #endregion

            #region Tạo bảng chứa quyền user MB
            //tao bang chua thong tin customer
            DataTable tblMBUserRight = new DataTable();
            DataColumn colMBUserIDUR = new DataColumn("colMBUserIDUR");
            DataColumn colMBRoleID = new DataColumn("colMBRoleID");
            DataColumn colMBRoleContracType = new DataColumn("colMBRoleContracType");


            //add vào table
            tblMBUserRight.Columns.Add(colMBUserIDUR);
            tblMBUserRight.Columns.Add(colMBRoleID);
            tblMBUserRight.Columns.Add(colMBRoleContracType);


            //tao 1 dong du lieu
            DataRow[] arrMBR = tblSUM.Select("colServiceID='" + SmartPortal.Constant.IPC.AM + "' and colRoleType='" + SmartPortal.Constant.IPC.MBA + "'");
            foreach (DataRow rMBR in arrMBR)
            {
                if (tblMBUserRight.Select("colMBUserIDUR='" + USERCTK + "' and colMBRoleID='" + rMBR["colRoleID"].ToString() + "'").Length == 0)
                {
                    DataRow newRowMBR = tblMBUserRight.NewRow();
                    newRowMBR["colMBUserIDUR"] = USERCTK;
                    newRowMBR["colMBRoleID"] = rMBR["colRoleID"].ToString();
                    newRowMBR["colMBRoleContracType"] = SmartPortal.Constant.IPC.CONTRACTAGENTMERCHANT;

                    tblMBUserRight.Rows.Add(newRowMBR);
                }
            }
            #endregion

            #region Tạo bảng chứa quyền user WL
            //tao 1 dong du lieu
            DataRow[] arrWLR = tblSUM.Select("colServiceID='" + SmartPortal.Constant.IPC.AM + "' and colRoleType='" + SmartPortal.Constant.IPC.EAM + "'");
            foreach (DataRow rWLR in arrWLR)
            {
                if (tblMBUserRight.Select("colMBUserIDUR='" + USERCTK + "' and colMBRoleID='" + rWLR["colRoleID"].ToString() + "'").Length == 0)
                {
                    DataRow newRowMBR = tblMBUserRight.NewRow();
                    newRowMBR["colMBUserIDUR"] = USERCTK;
                    newRowMBR["colMBRoleID"] = rWLR["colRoleID"].ToString();
                    newRowMBR["colMBRoleContracType"] = SmartPortal.Constant.IPC.CONTRACTAGENTMERCHANT;
                    tblMBUserRight.Rows.Add(newRowMBR);
                }
            }
            #endregion

            #region Tạo bảng chứa quyền cho Contract
            //tao bang chua thong tin customer
            DataTable tblContractRoleDetail = new DataTable();
            DataColumn colContractNo = new DataColumn("colContractNo");
            DataColumn colRoleID = new DataColumn("colRoleID");

            //add vào table
            tblContractRoleDetail.Columns.Add(colContractNo);
            tblContractRoleDetail.Columns.Add(colRoleID);

            //tao 1 dong du lieu
            DataTable tblroleContract = new SmartPortal.SEMS.Product().GetRoleOfProduct(ddlProduct.SelectedValue);

            foreach (DataRow rCR in tblroleContract.Rows)
            {
                if (tblContractRoleDetail.Select("colContractNo='" + contractNo + "' and colRoleID='" + rCR["colRoleID"].ToString() + "'").Length == 0)
                {
                    DataRow newRowCR = tblContractRoleDetail.NewRow();
                    newRowCR["colContractNo"] = contractNo;
                    newRowCR["colRoleID"] = rCR["colRoleID"].ToString();

                    tblContractRoleDetail.Rows.Add(newRowCR);
                }
            }
            #endregion

            #region Tạo bảng chứa Account của Contract

            #region lay tat ca cac account cua khach hang
            DataSet ds = new DataSet();
            switch (ddlCustType.SelectedValue.Trim())
            {
                case SmartPortal.Constant.IPC.PERSONAL:
                    ds = new SmartPortal.SEMS.Customer().GetAcctNo(txtCustCodeInfo.Text.Trim(), ddlCustType.SelectedValue, ref IPCERRORCODE, ref IPCERRORDESC);
                    break;
                    //case SmartPortal.Constant.IPC.PERSONALLKG:
                    //    ds = new SmartPortal.SEMS.Customer().GetAcctNo(txtLkgCode.Text.Trim(), ddlCustType.SelectedValue, ref IPCERRORCODE, ref IPCERRORDESC);
                    //    break;
                    //case SmartPortal.Constant.IPC.CORPORATE:
                    //    ds = new SmartPortal.SEMS.Customer().GetAcctNo(txtGrpCode.Text.Trim(), ddlCustType.SelectedValue, ref IPCERRORCODE, ref IPCERRORDESC);
                    //    break;
            }
            if (IPCERRORCODE != "0")
            {
                //goto ERROR;
            }
            DataTable dtAccount = new DataTable();
            dtAccount = ds.Tables[0];
            #endregion

            DataTable tblContractList = (DataTable)ViewState["AccountList"];

            //tao bang chua thong tin account
            DataTable tblContractAccount = new DataTable();
            DataColumn colAContractNo = new DataColumn("colAContractNo");
            DataColumn colAcctNo = new DataColumn("colAcctNo");
            DataColumn colAcctType = new DataColumn("colAcctType");
            DataColumn colCCYID = new DataColumn("colCCYID");
            DataColumn colStatus = new DataColumn("colStatus");
            DataColumn colBankAcctype = new DataColumn("colBankAcctype");
            DataColumn colBranchID = new DataColumn("colBranchID");

            //add vào table
            tblContractAccount.Columns.Add(colAContractNo);
            tblContractAccount.Columns.Add(colAcctNo);
            tblContractAccount.Columns.Add(colAcctType);
            tblContractAccount.Columns.Add(colCCYID);
            tblContractAccount.Columns.Add(colStatus);
            tblContractAccount.Columns.Add(colBankAcctype);
            tblContractAccount.Columns.Add(colBranchID);

            //Add all account
            foreach (DataRow dr in tblContractList.Rows)
            {
                DataRow newRowCA = tblContractAccount.NewRow();
                newRowCA["colAContractNo"] = contractNo;
                newRowCA["colAcctNo"] = dr["ACCOUNTNO"].ToString();
                newRowCA["colAcctType"] = dr["ACCOUNTTYPE"].ToString();
                newRowCA["colCCYID"] = dr["CCYID"].ToString();
                newRowCA["colStatus"] = dr["STATUS"].ToString();
                newRowCA["colBankAcctype"] = dr["ACCOUNTTYPE"].ToString();
                newRowCA["colBranchID"] = dr["BRANCHID"].ToString();
                tblContractAccount.Rows.Add(newRowCA);
            }
            //ADD ACCOUNT WALLET
            DataRow newRowCA1 = tblContractAccount.NewRow();
            newRowCA1["colAContractNo"] = contractNo;
            newRowCA1["colAcctNo"] = walletID;
            newRowCA1["colAcctType"] = SmartPortal.Constant.IPC.EW;
            newRowCA1["colCCYID"] = Resources.labels.lak;
            //if (tblSUM.Select("colRoleType='EAM'").Length == 0)
            //{
            //    newRowCA1["colStatus"] = "I";
            //}
            //else
            //{
            //    newRowCA1["colStatus"] = "A";
            //}
            newRowCA1["colStatus"] = "A";
            newRowCA1["colBankAcctype"] = ddlUserType.SelectedValue == SmartPortal.Constant.IPC.AGENT ? "AG" : "MC";
            newRowCA1["colBranchID"] = ddlBranch.SelectedValue;
            tblContractAccount.Rows.Add(newRowCA1);

            #endregion

            #region Tạo bảng chứa tranright
            DataTable tblTranrightDetail = new DataTable();
            DataColumn colUserIDTR = new DataColumn("colUserID");
            DataColumn colAcctNoTR = new DataColumn("colAcctNoTR");
            DataColumn colTranCode = new DataColumn("colTranCode");
            DataColumn colServiceID = new DataColumn("colServiceID");
            DataColumn colLimit = new DataColumn("colLimit");

            //add vào table
            tblTranrightDetail.Columns.Add(colUserIDTR);
            tblTranrightDetail.Columns.Add(colAcctNoTR);
            tblTranrightDetail.Columns.Add(colTranCode);
            tblTranrightDetail.Columns.Add(colServiceID);
            tblTranrightDetail.Columns.Add(colLimit);

            //add cung cho giao dich lay account IPC

            DataRow[] arrTran = tblSUM.Select("colServiceID='" + SmartPortal.Constant.IPC.AM + "' and colRoleType='" + SmartPortal.Constant.IPC.MBA + "'");

            foreach (DataRow rTR in arrTran)
            {
                if (tblTranrightDetail.Select("colUserID='" + rTR["colIBUserName"].ToString() + "' and colAcctNoTR='" + rTR["colAccount"].ToString() + "' and colTranCode='" + rTR["colTranCodeID"].ToString() + "'").Length == 0)
                {
                    if (rTR["colIBUserName"].ToString().Trim() != "")
                    {
                        DataRow newRowTR = tblTranrightDetail.NewRow();
                        newRowTR["colUserID"] = rTR["colIBUserName"].ToString();
                        newRowTR["colAcctNoTR"] = rTR["colAccount"].ToString();
                        newRowTR["colTranCode"] = rTR["colTranCodeID"].ToString();
                        newRowTR["colServiceID"] = rTR["colServiceID"].ToString();
                        newRowTR["colLimit"] = "0";

                        tblTranrightDetail.Rows.Add(newRowTR);
                    }
                }
            }

            DataRow[] arrTranWallet = tblSUM.Select("colServiceID='" + SmartPortal.Constant.IPC.AM + "' and colRoleType='" + SmartPortal.Constant.IPC.EAM + "' and colTranCodeID <> ''  ");

            foreach (DataRow rTR in arrTranWallet)
            {
                if (tblTranrightDetail.Select("colUserID='" + rTR["colIBUserName"].ToString() + "' and colAcctNoTR='" + rTR["colAccount"].ToString() + "' and colTranCode='" + rTR["colTranCodeID"].ToString() + "'").Length == 0)
                {
                    if (rTR["colIBUserName"].ToString().Trim() != "")
                    {
                        DataRow newRowTR = tblTranrightDetail.NewRow();
                        newRowTR["colUserID"] = rTR["colIBUserName"].ToString();
                        newRowTR["colAcctNoTR"] = walletID;
                        newRowTR["colTranCode"] = rTR["colTranCodeID"].ToString();
                        newRowTR["colServiceID"] = rTR["colServiceID"].ToString();
                        newRowTR["colLimit"] = "0";

                        tblTranrightDetail.Rows.Add(newRowTR);
                    }
                }
            }
            DataRow[] arrTranIB = tblSUM.Select("colServiceID='" + SmartPortal.Constant.IPC.IB + "' and colRoleType='" + SmartPortal.Constant.IPC.NORMAL + "'");

            foreach (DataRow rTR in arrTranIB)
            {
                if (tblTranrightDetail.Select("colUserID='" + rTR["colIBUserName"].ToString() + "' and colAcctNoTR='" + rTR["colAccount"].ToString() + "' and colTranCode='" + rTR["colTranCodeID"].ToString() + "'").Length == 0)
                {
                    if (rTR["colIBUserName"].ToString().Trim() != "")
                    {
                        DataRow newRowTR = tblTranrightDetail.NewRow();
                        newRowTR["colUserID"] = rTR["colIBUserName"].ToString();
                        newRowTR["colAcctNoTR"] = walletID;
                        newRowTR["colTranCode"] = rTR["colTranCodeID"].ToString();
                        newRowTR["colServiceID"] = rTR["colServiceID"].ToString();
                        newRowTR["colLimit"] = "0";

                        tblTranrightDetail.Rows.Add(newRowTR);
                    }
                }
            }
            #endregion

            #region Tạo bảng chứa UserAccount
            DataTable tblUserAccount = new DataTable();
            DataColumn colUserIDUC = new DataColumn("colUserIDUC");
            DataColumn colAcctNoUC = new DataColumn("colAcctNoUC");
            DataColumn colRoleIDUC = new DataColumn("colRoleIDUC");
            DataColumn colUseFull = new DataColumn("colUseFull");
            DataColumn colDesc = new DataColumn("colDesc");
            DataColumn colIsDefault = new DataColumn("colIsDefault");
            DataColumn colSst = new DataColumn("colStatus");
            //add vào table
            tblUserAccount.Columns.Add(colUserIDUC);
            tblUserAccount.Columns.Add(colAcctNoUC);
            tblUserAccount.Columns.Add(colRoleIDUC);
            tblUserAccount.Columns.Add(colUseFull);
            tblUserAccount.Columns.Add(colDesc);
            tblUserAccount.Columns.Add(colIsDefault);
            tblUserAccount.Columns.Add(colSst);
            //add cung cho giao dich lay account IPC


            DataRow[] arrUserAcc = tblSUM.Select("colServiceID='" + SmartPortal.Constant.IPC.AM + "' and colRoleType ='" + SmartPortal.Constant.IPC.MBA + "'");
            foreach (DataRow rUA in arrUserAcc)
            {
                if (tblUserAccount.Select("colUserIDUC='" + rUA["colIBUserName"].ToString() + "' and colAcctNoUC='" + rUA["colAccount"].ToString() + "' and colRoleIDUC='" + rUA["colRoleID"].ToString() + "'").Length == 0)
                {
                    DataRow newRowUA = tblUserAccount.NewRow();
                    newRowUA["colUserIDUC"] = rUA["colIBUserName"].ToString();
                    newRowUA["colAcctNoUC"] = rUA["colAccount"].ToString();
                    newRowUA["colRoleIDUC"] = rUA["colRoleID"].ToString();
                    newRowUA["colUseFull"] = "N";
                    newRowUA["colDesc"] = "";
                    newRowUA["colIsDefault"] = "N";
                    newRowUA["colStatus"] = "Y";
                    tblUserAccount.Rows.Add(newRowUA);
                }

            }
            //add Roles Wallet default
            if (tblUserAccount.Select("colAcctNoUC='" + phoneAccount + "'").Length == 0)
            {
                //DataRow[] arrUserWallet = tblSUM.Select("colServiceID='" + SmartPortal.Constant.IPC.AM + "' and colRoleType ='" + SmartPortal.Constant.IPC.EAM + "'");

                DataTable tblSS = new SmartPortal.SEMS.Contract().GetRoleByServiceAndProductid(SmartPortal.Constant.IPC.AM, ddlProduct.SelectedValue, SmartPortal.Constant.IPC.EAM);
                foreach (DataRow rUA in tblSS.Rows)
                {
                    DataRow newRowUA = tblUserAccount.NewRow();
                    newRowUA["colUserIDUC"] = USERCTK;
                    newRowUA["colAcctNoUC"] = walletID;
                    newRowUA["colRoleIDUC"] = rUA["RoleID"].ToString();
                    newRowUA["colUseFull"] = "N";
                    newRowUA["colDesc"] = "";
                    if (tblSUM.Select("colRoleType='EAM' and colRoleID = '" + rUA["RoleID"].ToString() + "'").Length == 0)
                    {
                        newRowUA["colIsDefault"] = "N";
                        newRowUA["colStatus"] = "N";
                    }
                    else
                    {
                        newRowUA["colIsDefault"] = "Y";
                        newRowUA["colStatus"] = "Y";
                    }

                    tblUserAccount.Rows.Add(newRowUA);
                }
            }
            #endregion

            #region vutt Tạo bảng chứa thông tin sms notify 04022016

            DataTable tblSMSNotify = ContractControl.CreateSMSNotifyDetailTable(tblUserAccount, tblContractAccount, "I", contractNo);

            //return;
            #endregion

            #region ADD KYC INFOR
            DataTable tblKYCInfor = new DataTable();
            DataColumn colKYCUserID = new DataColumn("colKYCUserID");
            DataColumn colKYCPaperNo = new DataColumn("colKYCPaperNo");
            DataColumn colKYCPhoneNo = new DataColumn("colKYCPhoneNo");
            DataColumn colPaperType = new DataColumn("colPaperType");
            DataColumn coKYCType = new DataColumn("coKYCType");
            DataColumn colKYCCustID = new DataColumn("colKYCCustID");
            DataColumn colKYCUserCreate = new DataColumn("colKYCUserCreate");
            DataColumn colKYCCustCode = new DataColumn("colKYCCustCode");
            DataColumn colKYCDocument = new DataColumn("colKYCDocument");

            tblKYCInfor.Columns.Add(colKYCUserID);
            tblKYCInfor.Columns.Add(colKYCPaperNo);
            tblKYCInfor.Columns.Add(colKYCPhoneNo);
            tblKYCInfor.Columns.Add(colPaperType);
            tblKYCInfor.Columns.Add(coKYCType);
            tblKYCInfor.Columns.Add(colKYCCustID);
            tblKYCInfor.Columns.Add(colKYCUserCreate);
            tblKYCInfor.Columns.Add(colKYCCustCode);
            tblKYCInfor.Columns.Add(colKYCDocument);


            if (!string.IsNullOrEmpty(contractNo))
            {
                DataRow rowKycInfor = tblKYCInfor.NewRow();
                rowKycInfor["colKYCUserID"] = userChuTK;
                rowKycInfor["colKYCPaperNo"] = txtNRICNewNumber.Text.Trim();
                rowKycInfor["colKYCPhoneNo"] = phoneAccount;
                rowKycInfor["colPaperType"] = ViewState["LICENSETYPE"].ToString();
                rowKycInfor["coKYCType"] = "AM";
                rowKycInfor["colKYCCustID"] = custID;
                rowKycInfor["colKYCUserCreate"] = Session["userName"].ToString();
                rowKycInfor["colKYCCustCode"] = txtCustCode.Text.Trim();
                if (lblNRICBackNew.Text == "NRB" && lblNRICFontNew.Text == "NRF")
                {
                    rowKycInfor["colKYCDocument"] = "";
                }
                else
                {
                    DataTable tblDocument = new DataTable();
                    tblDocument.Columns.AddRange(new DataColumn[] { new DataColumn("DOCUMENTTYPE"), new DataColumn("DOCUMENTNAME"), new DataColumn("FILEBIN") });

                    DataRow rowFont = tblDocument.NewRow();
                    rowFont["DOCUMENTTYPE"] = "NF";
                    rowFont["DOCUMENTNAME"] = "PP FRONT";
                    rowFont["FILEBIN"] = lblNRICFontNew.Text;
                    tblDocument.Rows.Add(rowFont);

                    DataRow rowBack = tblDocument.NewRow();
                    rowBack["DOCUMENTTYPE"] = "NB";
                    rowBack["DOCUMENTNAME"] = "PP BACK";
                    rowBack["FILEBIN"] = lblNRICBackNew.Text;
                    tblDocument.Rows.Add(rowBack);

                    if (!string.IsNullOrEmpty(lblSelfieNRIC.Text.Trim()))
                    {
                        DataRow rowSelfile = tblDocument.NewRow();
                        rowSelfile["DOCUMENTTYPE"] = "SN";
                        rowSelfile["DOCUMENTNAME"] = "SELFIE WITH PP";
                        rowSelfile["FILEBIN"] = lblSelfieNRIC.Text;
                        tblDocument.Rows.Add(rowSelfile);
                    }

                    DataTable tblDocumnentUpload = (DataTable)ViewState["TBLDOCUMENT"];

                    foreach (DataRow dr in tblDocumnentUpload.Rows)
                    {
                        if (string.IsNullOrEmpty(dr["colDocCode"].ToString()) || string.IsNullOrEmpty(dr["colDocType"].ToString()) || string.IsNullOrEmpty(dr["colImg"].ToString()))
                        {
                            continue;
                        }
                        DataRow row = tblDocument.NewRow();
                        row["DOCUMENTTYPE"] = dr["colDocCode"].ToString();
                        row["DOCUMENTNAME"] = dr["colDocType"].ToString();
                        row["FILEBIN"] = dr["colImg"].ToString();
                        tblDocument.Rows.Add(row);
                    }
                    rowKycInfor["colKYCDocument"] = JsonConvert.SerializeObject(tblDocument);
                }
                tblKYCInfor.Rows.Add(rowKycInfor);
            }
            #endregion

            DataTable tblTimeOpen = TimeOpenContract(contractNo);

            #region Tạo bảng chứa transaction alter of contract
            //tao bang chua thong tin customer
            DataTable tblTranAlter = new DataTable();
            DataColumn colContractNoAlter = new DataColumn("colContractNoAlter");
            DataColumn colTransactionAlert = new DataColumn("colTransactionAlert");

            //add vào table
            tblTranAlter.Columns.Add(colContractNoAlter);
            tblTranAlter.Columns.Add(colTransactionAlert);

            //tao 1 dong du lieu
            if (cbLINE.Checked)
            {
                DataRow newRow1 = tblTranAlter.NewRow();
                newRow1["colContractNoAlter"] = contractNo;
                newRow1["colTransactionAlert"] = "LINE";
                tblTranAlter.Rows.Add(newRow1);
            }
            if (cbSMS.Checked)
            {
                DataRow newRow1 = tblTranAlter.NewRow();
                newRow1["colContractNoAlter"] = contractNo;
                newRow1["colTransactionAlert"] = "SMS";
                tblTranAlter.Rows.Add(newRow1);
            }
            if (cbTELE.Checked)
            {
                DataRow newRow1 = tblTranAlter.NewRow();
                newRow1["colContractNoAlter"] = contractNo;
                newRow1["colTransactionAlert"] = "Telegram";
                tblTranAlter.Rows.Add(newRow1);
            }
            if (cbWAPP.Checked)
            {
                DataRow newRow1 = tblTranAlter.NewRow();
                newRow1["colContractNoAlter"] = contractNo;
                newRow1["colTransactionAlert"] = "WhatsApp";
                tblTranAlter.Rows.Add(newRow1);
            }
            #endregion
            #region INSERT

            new SmartPortal.SEMS.AgentMerchant().InsertAgentMerchantContract(walletID, MerchantCode, Session["userName"].ToString(), "+23", ViewState["LICENSETYPE"].ToString(), ddlContractLevel.SelectedValue.ToString(), custID, txtCustCode.Text.Trim(), fullName, shortName, DOB, re_addr, township, region, temp_addr, SEX, Nation, Tel, FAX, Email, LicenseType, LicenseID, issueDate, issuePlace, desc, job, officeAddr, officePhone, custType, txtCustCodeInfo.Text.Trim(), "B", branchID, custBranchID, contractNo, ddlSubUserType.SelectedValue, productID, startDate, endDate, lastModify, userCreate, userLastModify, userApprove, status, allAcct, isSpecialMan, (chkRenew.Checked ? "Y" : "N"), SmartPortal.Constant.IPC.CONTRACTAGENTMERCHANT, tblUser, tblIbankUser, tblSMSUser, tblMBUser, tblIbankUserRight, tblSMSUserRight, tblMBUserRight, tblContractRoleDetail, tblContractAccount, new DataTable(), tblUserAccount, null, null, tblSMSNotify, tblKYCInfor, tblTimeOpen, tblTranAlter, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE != "0")
            {
                lblError.Text = Resources.labels.createcontractfail;
                SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSCustomerList_Add_Widget", "btnCustSave_Click", IPCERRORDESC.ToString().ToString(), Request.Url.Query);
                return;
            }
            else
            {
                SmartPortal.Common.Log.WriteLogDatabase(contractNo, "INSERT", Request.Url.ToString(), Session["userName"].ToString(),
                 Request.UserHostAddress, "AM_USER", "Add new user", "", "New", "N");

                SmartPortal.Common.Log.WriteLogDatabase(contractNo, "INSERT", Request.Url.ToString(), Session["userName"].ToString(),
                    Request.UserHostAddress, "EBA_Contract", "Add new Account", "", "New", "N");

                SmartPortal.Common.Log.WriteLogDatabase(contractNo, "INSERT", Request.Url.ToString(), Session["userName"].ToString(),
                Request.UserHostAddress, "EBA_Contract", "Create agent merchant contract", "", "New", "N");
                SendInfoLogin();
                ReleaseSession();
            }

            #endregion


        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], "Widgets_SEMSCustomerList_Add_Widget", "btnCustSave_Click", IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);
        }

        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSCustomerList_Add_Widget", "btnCustSave_Click", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }

        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?po=4&p=183&returnURL=" + SmartPortal.Common.Encrypt.EncryptData(Request.Url.Query)));

    }

    void SendInfoLogin()
    {
        try
        {


            Antlr3.ST.StringTemplate tmpl = new Antlr3.ST.StringTemplate();
            tmpl = SmartPortal.Common.ST.GetStringTemplate("SEMSContractApprove", "ContractAttachment" + System.Globalization.CultureInfo.CurrentCulture.ToString());

            //lay thong tin hop dong de gui mail

            try
            {

                string hpcontractNo = txtContractNo.Text.Trim();
                string custID = "";
                tmpl.Reset();

                //lay thong tin hop dong boi contractno
                DataTable contractTable = (new SmartPortal.SEMS.Contract().GetContractByContractNo(hpcontractNo, ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
                if (IPCERRORCODE != "0")
                {
                    goto ERROR;
                }
                //gan thong tin hop dong vao stringtemplate
                if (contractTable.Rows.Count != 0)
                {
                    tmpl.SetAttribute("CONTRACTNO", contractTable.Rows[0]["CONTRACTNO"].ToString());
                    DataSet dsUserType = new DataSet();
                    dsUserType = new SmartPortal.SEMS.Services().GetUserType(contractTable.Rows[0]["USERTYPE"].ToString().Trim(), "", ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE == "0")
                    {
                        DataTable dtUserType = new DataTable();
                        dtUserType = dsUserType.Tables[0];

                        if (dtUserType.Rows.Count != 0)
                        {
                            tmpl.SetAttribute("CONTRACTTYPE", dtUserType.Rows[0]["USERTYPE"].ToString());
                        }
                    }
                    else
                    {
                        throw new SmartPortal.ExceptionCollection.IPCException(IPCERRORDESC);
                    }

                    tmpl.SetAttribute("OPENDATE", SmartPortal.Common.Utilities.Utility.IsDateTime2(contractTable.Rows[0]["CREATEDATE"].ToString()).ToString("dd/MM/yyyy"));
                    tmpl.SetAttribute("ENDDATE", SmartPortal.Common.Utilities.Utility.IsDateTime2(contractTable.Rows[0]["ENDDATE"].ToString()).ToString("dd/MM/yyyy"));


                    tmpl.SetAttribute("PRODUCT", contractTable.Rows[0]["PRODUCTNAME"].ToString());


                    custID = contractTable.Rows[0]["CUSTID"].ToString();
                }

                //lay thong tin khach hang
                DataTable custTable = (new SmartPortal.SEMS.Customer().GetCustomerByCustcode(custID, ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
                if (IPCERRORCODE != "0")
                {
                    goto ERROR;
                }
                //gan thong tin hop dong vao stringtemplate
                if (custTable.Rows.Count != 0)
                {
                    tmpl.SetAttribute("CUSTID", custTable.Rows[0]["CUSTID"].ToString());
                    tmpl.SetAttribute("FULLNAME", custTable.Rows[0]["FULLNAME"].ToString());
                    switch (custTable.Rows[0]["CFTYPE"].ToString().Trim())
                    {
                        case SmartPortal.Constant.IPC.PERSONAL:
                            tmpl.SetAttribute("CUSTTYPE", Resources.labels.canhan);
                            break;
                        case SmartPortal.Constant.IPC.CORPORATE:
                            tmpl.SetAttribute("CUSTTYPE", Resources.labels.doanhnghiep);
                            break;
                    }
                    tmpl.SetAttribute("PHONE", custTable.Rows[0]["TEL"].ToString());
                    tmpl.SetAttribute("EMAIL", custTable.Rows[0]["EMAIL"].ToString());
                    tmpl.SetAttribute("CIF", custTable.Rows[0]["CUSTCODE"].ToString());
                }

                #region lay thong tin tai khoan cua chu tai khoan
                DataTable userTable = (new SmartPortal.SEMS.Contract().GetUserByContractNo(hpcontractNo, contractTable.Rows[0]["USERTYPE"].ToString(), "", ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
                if (IPCERRORCODE != "0")
                {
                    goto ERROR;
                }

                StringBuilder st = new StringBuilder();
                st.Append("<div style='font-weight: bold; background-color: #C0C0C0;width:100%;height:16px;'>" + Resources.labels.thongtinchutaikhoan.ToUpper() + "</div>");
                //gan thong tin user vao stringtemplate
                int i = 0;
                foreach (DataRow row in userTable.Rows)
                {

                    st.Append("<table style='width:100%;'>");
                    st.Append("<tr>");
                    st.Append("<td width='25%'>");
                    st.Append(Resources.labels.tendaydu + " ");
                    st.Append("</td>");
                    st.Append("<td width='25%'>");
                    st.Append(row["FULLNAME"].ToString());
                    st.Append("</td>");
                    st.Append("<td width='25%'>");
                    st.Append("Email ");
                    st.Append("</td>");
                    st.Append("<td width='25%'>");
                    st.Append(row["EMAIL"].ToString());
                    st.Append("</td>");
                    st.Append("</tr>");

                    st.Append("<tr>");
                    st.Append("<td>");
                    st.Append(Resources.labels.phone + " ");
                    st.Append("</td>");
                    st.Append("<td>");
                    st.Append(row["PHONE"].ToString());
                    st.Append("</td>");
                    st.Append("<td>");
                    st.Append("");
                    st.Append("</td>");
                    st.Append("<td>");
                    st.Append("");
                    st.Append("</td>");
                    st.Append("</tr>");


                    //lay het các tai khoan Ibank cua user theo userID
                    DataSet accountIBDataset = new SmartPortal.SEMS.User().GetUserRoleByServiceID(SmartPortal.Constant.IPC.IB, row["USERID"].ToString().Trim(), string.Empty, ref IPCERRORCODE, ref IPCERRORDESC);

                    if (IPCERRORCODE != "0")
                    {
                        goto ERROR;
                    }

                    DataTable accountIBTable = accountIBDataset.Tables[0];
                    if (accountIBTable.Rows.Count != 0)
                    {
                        if (accountIBTable.Rows[0]["ROLEID"].ToString().Trim() != "")
                        {
                            st.Append("<tr>");
                            st.Append("<td colspan='4'>");
                            st.Append("<br/>");
                            st.Append("</td>");
                            st.Append("</tr>");

                            st.Append("<tr>");
                            st.Append("<td colspan='4'>");
                            st.Append("<B>Internet Banking</B>");
                            st.Append("</td>");
                            st.Append("</tr>");

                            st.Append("<tr>");
                            st.Append("<td width='25%'>");
                            st.Append(Resources.labels.username + " :");
                            st.Append("</td>");
                            st.Append("<td width='25%'>");
                            st.Append(accountIBTable.Rows[0]["USERNAME"].ToString());
                            st.Append("</td>");
                            st.Append("<td width='25%'>");
                            st.Append(Resources.labels.password + " :");
                            st.Append("</td>");
                            st.Append("<td width='25%'>");
                            st.Append("########");
                            //st.Append("<b>" + SmartPortal.Security.Encryption.Decrypt(accountIBTable.Rows[0]["PASSWORD"].ToString()) + "</b>");
                            st.Append("</td>");
                            st.Append("</tr>");
                        }

                    }

                    //lay het các tai khoan SMS cua user theo userID
                    DataSet accountSMSDataset = new SmartPortal.SEMS.User().GetUserRoleByServiceID(SmartPortal.Constant.IPC.SMS, row["USERID"].ToString().Trim(), string.Empty, ref IPCERRORCODE, ref IPCERRORDESC);

                    if (IPCERRORCODE != "0")
                    {
                        goto ERROR;
                    }

                    DataTable accountSMSTable = accountSMSDataset.Tables[0];
                    if (accountSMSTable.Rows.Count != 0)
                    {
                        if (accountSMSTable.Rows[0]["ROLEID"].ToString().Trim() != "")
                        {
                            st.Append("<tr>");
                            st.Append("<td colspan='4'>");
                            st.Append("<br/>");
                            st.Append("</td>");
                            st.Append("</tr>");

                            st.Append("<tr>");
                            st.Append("<td colspan='4'>");
                            st.Append("<B>SMS Banking</B>");
                            st.Append("</td>");
                            st.Append("</tr>");

                            st.Append("<tr>");
                            st.Append("<td width='25%'>");
                            st.Append(Resources.labels.phone + " :");
                            st.Append("</td>");
                            st.Append("<td width='25%'>");
                            st.Append(accountSMSTable.Rows[0]["UN"].ToString());
                            st.Append("</td>");
                            st.Append("<td width='25%'>");
                            st.Append(Resources.labels.taikhoanmacdinh + " :");
                            st.Append("</td>");
                            st.Append("<td width='25%'>");
                            st.Append(accountSMSTable.Rows[0]["DEFAULTACCTNO"].ToString());
                            st.Append("</td>");
                            st.Append("</tr>");
                        }
                    }

                    //lay het các tai khoan MBAM cua user theo userID
                    DataSet accountMBDataset = new SmartPortal.SEMS.User().GetUserSendInfor(SmartPortal.Constant.IPC.AM, row["USERID"].ToString().Trim(), ref IPCERRORCODE, ref IPCERRORDESC);

                    if (IPCERRORCODE != "0")
                    {
                        goto ERROR;
                    }

                    DataTable accountMBTable = accountMBDataset.Tables[0];
                    if (accountMBTable.Rows.Count != 0)
                    {
                        //st.Append("<tr>");
                        //st.Append("<td colspan='4'>");
                        //st.Append("<br/>");
                        //st.Append("</td>");
                        //st.Append("</tr>");

                        //st.Append("<tr>");
                        //st.Append("<td colspan='4'>");
                        //st.Append("<B>Mobile Banking</B>");
                        //st.Append("</td>");
                        //st.Append("</tr>");

                        //st.Append("<tr>");
                        //st.Append("<td width='25%'>");
                        //st.Append(Resources.labels.username + " :");
                        //st.Append("</td>");
                        //st.Append("<td width='25%'>");
                        //st.Append(accountMBTable.Rows[0]["USERNAME"].ToString());
                        //st.Append("</td>");
                        //st.Append("<td width='25%'>");
                        //st.Append(Resources.labels.phone + " :");
                        //st.Append("</td>");
                        //st.Append("<td width='25%'>");
                        //st.Append(accountMBTable.Rows[0]["UN"].ToString());
                        ////st.Append("<b>" + SmartPortal.Security.Encryption.Decrypt(accountMBTable.Rows[0]["PASS"].ToString()) + "</b>");
                        //st.Append("</td>");
                        //st.Append("</tr>");

                        //st.Append("<tr>");
                        //st.Append("<td width='25%'>");
                        //st.Append(Resources.labels.password + " :");
                        //st.Append("</td>");
                        //st.Append("<td width='25%'>");
                        //st.Append("########");
                        //st.Append("</td>");
                        if (sendPincode())
                        {
                            st.Append("<td colspan=1>  ");
                            st.Append(Resources.labels.pincode + " :");
                            st.Append("</td>");
                            st.Append("<td colspan=1>  ");
                            st.Append("########");
                            st.Append("</td>");
                        }
                        st.Append("</tr>");
                    }
                }
                tmpl.SetAttribute("USERINFO", st.ToString());

                #endregion

                if (ViewState["NGUOIUYQUYEN"] != null)
                {
                    DataTable tblNGUOIUYQUYEN = (DataTable)ViewState["NGUOIUYQUYEN"];
                    if (tblNGUOIUYQUYEN.Rows.Count != 0)
                    {

                        #region lay thong tin tai khoan cua nguoi dong so huu
                        DataTable nuyTable = (new SmartPortal.SEMS.Contract().GetUserByContractNo(hpcontractNo, SmartPortal.Constant.IPC.RP, "", ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
                        if (IPCERRORCODE != "0")
                        {
                            goto ERROR;
                        }

                        StringBuilder stNUY = new StringBuilder();
                        //minh modify tieng viet->tieng anh
                        //stNUY.Append("<div style='font-weight: bold; background-color: #C0C0C0;width:100%;height:16px;'>THÔNG TIN NGƯỜI ĐỒNG SỞ HỮU</div>");
                        stNUY.Append("<div style='font-weight: bold; background-color: #C0C0C0;width:100%;height:16px;'>" + Resources.labels.thongtinnguoidongsohuuaccount.ToUpper() + "</div>");
                        //gan thong tin user vao stringtemplate
                        int j = 0;
                        foreach (DataRow row in nuyTable.Rows)
                        {

                            stNUY.Append("<table style='width:100%;'>");


                            stNUY.Append("<tr>");
                            stNUY.Append("<td width='25%'>");
                            stNUY.Append(Resources.labels.tendaydu);
                            stNUY.Append("</td>");
                            stNUY.Append("<td width='25%'>");
                            stNUY.Append(row["FULLNAME"].ToString());
                            stNUY.Append("</td>");
                            stNUY.Append("<td width='25%'>");
                            stNUY.Append("Email ");
                            stNUY.Append("</td>");
                            stNUY.Append("<td width='25%'>");
                            stNUY.Append(row["EMAIL"].ToString());
                            stNUY.Append("</td>");
                            stNUY.Append("</tr>");

                            stNUY.Append("<tr>");
                            stNUY.Append("<td>");
                            stNUY.Append(Resources.labels.phone);
                            stNUY.Append("</td>");
                            stNUY.Append("<td>");
                            stNUY.Append(row["PHONE"].ToString());
                            stNUY.Append("</td>");
                            stNUY.Append("<td>");
                            stNUY.Append("");
                            stNUY.Append("</td>");
                            stNUY.Append("<td>");
                            stNUY.Append("");
                            stNUY.Append("</td>");
                            stNUY.Append("</tr>");


                            //lay het các tai khoan Ibank cua user theo userID
                            DataSet accountIBDatasetNUY = new SmartPortal.SEMS.User().GetUserRoleByServiceID(SmartPortal.Constant.IPC.IB, row["USERID"].ToString().Trim(), string.Empty, ref IPCERRORCODE, ref IPCERRORDESC);

                            if (IPCERRORCODE != "0")
                            {
                                goto ERROR;
                            }

                            DataTable accountIBTableNUY = accountIBDatasetNUY.Tables[0];
                            if (accountIBTableNUY.Rows.Count != 0)
                            {
                                if (accountIBTableNUY.Rows[0]["ROLEID"].ToString().Trim() != "")
                                {
                                    stNUY.Append("<tr>");
                                    stNUY.Append("<td colspan='4'>");
                                    stNUY.Append("<br/>");
                                    stNUY.Append("</td>");
                                    stNUY.Append("</tr>");

                                    stNUY.Append("<tr>");
                                    stNUY.Append("<td colspan='4'>");
                                    stNUY.Append("<B>" + Resources.labels.internetbanking + "</B>");
                                    stNUY.Append("</td>");
                                    stNUY.Append("</tr>");

                                    stNUY.Append("<tr>");
                                    stNUY.Append("<td width='25%'>");
                                    stNUY.Append(Resources.labels.username);
                                    stNUY.Append("</td>");
                                    stNUY.Append("<td width='25%'>");
                                    stNUY.Append(accountIBTableNUY.Rows[0]["USERNAME"].ToString());
                                    stNUY.Append("</td>");
                                    stNUY.Append("<td width='25%'>");
                                    stNUY.Append(Resources.labels.password);
                                    stNUY.Append("</td>");
                                    stNUY.Append("<td width='25%'>");
                                    stNUY.Append("########");
                                    //st.Append("<b>" + SmartPortal.Security.Encryption.Decrypt(accountIBTable.Rows[0]["PASSWORD"].ToString()) + "</b>");
                                    stNUY.Append("</td>");
                                    stNUY.Append("</tr>");
                                }
                            }

                            //lay het các tai khoan SMS cua user theo userID
                            DataSet accountSMSDatasetNUY = new SmartPortal.SEMS.User().GetUserRoleByServiceID(SmartPortal.Constant.IPC.SMS, row["USERID"].ToString().Trim(), string.Empty, ref IPCERRORCODE, ref IPCERRORDESC);

                            if (IPCERRORCODE != "0")
                            {
                                goto ERROR;
                            }

                            DataTable accountSMSTableNUY = accountSMSDatasetNUY.Tables[0];
                            if (accountSMSTableNUY.Rows.Count != 0)
                            {
                                if (accountSMSTableNUY.Rows[0]["ROLEID"].ToString().Trim() != "")
                                {
                                    stNUY.Append("<tr>");
                                    stNUY.Append("<td colspan='4'>");
                                    stNUY.Append("<br/>");
                                    stNUY.Append("</td>");
                                    stNUY.Append("</tr>");

                                    stNUY.Append("<tr>");
                                    stNUY.Append("<td colspan='4'>");
                                    stNUY.Append("<B>" + Resources.labels.smsbanking + "</B>");
                                    stNUY.Append("</td>");
                                    stNUY.Append("</tr>");

                                    stNUY.Append("<tr>");
                                    stNUY.Append("<td width='25%'>");
                                    stNUY.Append(Resources.labels.phone);
                                    stNUY.Append("</td>");
                                    stNUY.Append("<td width='25%'>");
                                    stNUY.Append(accountSMSTableNUY.Rows[0]["UN"].ToString());
                                    stNUY.Append("</td>");
                                    stNUY.Append("<td width='25%'>");
                                    stNUY.Append(Resources.labels.taikhoanmacdinh);
                                    stNUY.Append("</td>");
                                    stNUY.Append("<td width='25%'>");
                                    stNUY.Append(accountSMSTableNUY.Rows[0]["DEFAULTACCTNO"].ToString());
                                    stNUY.Append("</td>");
                                    stNUY.Append("</tr>");
                                }
                            }

                            //lay het các tai khoan MB cua user theo userID
                            DataSet accountMBDatasetNUY = new SmartPortal.SEMS.User().GetUserRoleByServiceID(SmartPortal.Constant.IPC.AM, row["USERID"].ToString().Trim(), SmartPortal.Constant.IPC.MBA, ref IPCERRORCODE, ref IPCERRORDESC);

                            if (IPCERRORCODE != "0")
                            {
                                goto ERROR;
                            }

                            DataTable accountMBTableNUY = accountMBDatasetNUY.Tables[0];
                            if (accountMBTableNUY.Rows.Count != 0)
                            {
                                if (accountMBTableNUY.Rows[0]["ROLEID"].ToString().Trim() != "")
                                {
                                    stNUY.Append("<tr>");
                                    stNUY.Append("<td colspan='4'>");
                                    stNUY.Append("<br/>");
                                    stNUY.Append("</td>");
                                    stNUY.Append("</tr>");

                                    stNUY.Append("<tr>");
                                    stNUY.Append("<td colspan='4'>");
                                    stNUY.Append("<B>" + Resources.labels.mobilebanking + "</B>");
                                    stNUY.Append("</td>");
                                    stNUY.Append("</tr>");

                                    stNUY.Append("<tr>");
                                    stNUY.Append("<td width='25%'>");
                                    stNUY.Append(Resources.labels.username);
                                    stNUY.Append("</td>");
                                    stNUY.Append("<td width='25%'>");
                                    stNUY.Append(accountMBTableNUY.Rows[0]["USERNAME"].ToString());
                                    stNUY.Append("</td>");
                                    stNUY.Append("<td width='25%'>");
                                    stNUY.Append(Resources.labels.password);
                                    stNUY.Append("</td>");
                                    stNUY.Append("<td width='25%'>");
                                    stNUY.Append("########");
                                    //st.Append("<b>" + SmartPortal.Security.Encryption.Decrypt(accountMBTable.Rows[0]["PASS"].ToString()) + "</b>");
                                    stNUY.Append("</td>");
                                    stNUY.Append("</tr>");
                                    stNUY.Append("<tr>");
                                    stNUY.Append("<td width='25%'>");
                                    stNUY.Append(Resources.labels.phone);
                                    stNUY.Append("</td>");
                                    stNUY.Append("<td width='25%'>");
                                    stNUY.Append(accountMBTableNUY.Rows[0]["PHONENO"].ToString());
                                    stNUY.Append("</td>");
                                    stNUY.Append("</tr>");

                                }
                            }
                            //lay het các tai khoan AM cua user theo userID
                            DataSet accountWLDatasetNUY = new SmartPortal.SEMS.User().GetUserRoleByServiceID(SmartPortal.Constant.IPC.EW, row["USERID"].ToString().Trim(), SmartPortal.Constant.IPC.EAM, ref IPCERRORCODE, ref IPCERRORDESC);

                            if (IPCERRORCODE != "0")
                            {
                                goto ERROR;
                            }

                            DataTable accountWLTableNUY = accountWLDatasetNUY.Tables[0];
                            if (accountWLTableNUY.Rows.Count != 0)
                            {
                                if (accountWLTableNUY.Rows[0]["ROLEID"].ToString().Trim() != "")
                                {
                                    stNUY.Append("<tr>");
                                    stNUY.Append("<td colspan='4'>");
                                    stNUY.Append("<br/>");
                                    stNUY.Append("</td>");
                                    stNUY.Append("</tr>");

                                    stNUY.Append("<tr>");
                                    stNUY.Append("<td colspan='4'>");
                                    stNUY.Append("<B>" + Resources.labels.agentmerchant + "</B>");
                                    stNUY.Append("</td>");
                                    stNUY.Append("</tr>");

                                    stNUY.Append("<tr>");
                                    stNUY.Append("<td width='25%'>");
                                    stNUY.Append(Resources.labels.phone);
                                    stNUY.Append("</td>");
                                    stNUY.Append("<td width='25%'>");
                                    stNUY.Append(accountWLTableNUY.Rows[0]["UN"].ToString());
                                    stNUY.Append("</td>");
                                    stNUY.Append("<td width='25%'>");
                                    stNUY.Append(Resources.labels.password);
                                    stNUY.Append("</td>");
                                    stNUY.Append("<td width='25%'>");
                                    stNUY.Append("########");
                                    //st.Append("<b>" + SmartPortal.Security.Encryption.Decrypt(accountMBTable.Rows[0]["PASS"].ToString()) + "</b>");
                                    stNUY.Append("</td>");
                                    stNUY.Append("</tr>");

                                }
                            }
                        }
                        tmpl.SetAttribute("NGUOIUYQUYEN", stNUY.ToString());

                        #endregion

                    }
                }

                //luu thong tin vao session de hien thi cho nguoi dung
                Session["tmpl"] = tmpl.ToString();

                //luu thong tin ban cung hop dong
                try
                {
                    new SmartPortal.SEMS.Contract().SaveContractReview(hpcontractNo, tmpl.ToString(), Server.MapPath("~/widgets/semscontractlist/contractfile/" + hpcontractNo + ".html"));
                }
                catch
                {
                }

                goto EXIT;
            }
            catch (IPCException IPCex)
            {
                SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], "Widgets_SEMSContractApprove_Widget", "SendInfoLogin", IPCex.ToString(), Request.Url.Query);
                SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);
            }
            catch (Exception ex)
            {
                SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSContractApprove_Widget", "SendInfoLogin", ex.ToString(), Request.Url.Query);
                SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

            }
        ERROR:
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], "Widgets_SEMSContractApprove_Widget", "SendInfoLogin", IPCERRORDESC, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);
        EXIT:

            ;
        }
        catch
        {

        }
    }
    protected void btnSearchWL_Click(object sender, EventArgs e)
    {
        try
        {
            PHONE = txtPhoneWL.Text;
            if (!CheckExistPhoneNumber(PHONE))
            {
                lblError.Text = Resources.labels.phonenumberisalreadyregistered;
                btnSearchWL.Visible = true;
                txtPhoneWL.Enabled = true;
                btnNext.Visible = false;
            }
            if (!CheckIsPhoneNumer(PHONE))
            {
                btnSearchWL.Visible = true;
                txtPhoneWL.Enabled = true;
                btnNext.Visible = false;
                lblError.Text = Resources.labels.phonenumberwrong;
            }
            else
            {
                btnNext.Visible = true;
            }
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(IPCex.ToString(), this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.ToString(), Request.Url.Query);
            //SmartPortal.Common.Log.GoToErrorPage(IPCex.Message, Request.Url.Query);         
            lblError.Text = (new SmartPortal.BLL.ErrorBLL().Load(Utility.IsInt(IPCex.Message), System.Globalization.CultureInfo.CurrentCulture.ToString())).ErrorDesc;

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

            lblError.Text = "";
            txtPhoneWL.Visible = true;
            txtCustCodeInfo.Text = "";
            txtFullName.Text = "";
            txtShortName.Text = "";
            txtBirth.Text = "";
            txtPhoneWL.Text = "";
            txtEmail.Text = "";
            txtResidentAddr.Text = "";
            txtFax.Text = "";
            string ctmType = string.Empty;
            //minh add to check customer exist 12/08/2015
            string custcode = string.Empty;

            Hashtable hasCustInfo = new Hashtable();
            //if (radCustCode.Checked)
            //{
            ctmType = SmartPortal.Constant.IPC.PERSONAL;
            custcode = txtCustCode.Text.Trim();
            hasCustInfo = new SmartPortal.SEMS.Customer().GetCustInfo(txtCustCode.Text.Trim(), ctmType, ref IPCERRORCODE, ref IPCERRORDESC);
            //}
            //else if (radLkgCode.Checked)
            //{
            //    ctmType = SmartPortal.Constant.IPC.PERSONALLKG;
            //    custcode = txtLkgCode.Text.Trim();
            //    hasCustInfo = new SmartPortal.SEMS.Customer().GetCustInfo(txtLkgCode.Text.Trim(), ctmType, ref IPCERRORCODE, ref IPCERRORDESC);
            //}
            //else if (radGrpCode.Checked)
            //{
            //    ctmType = SmartPortal.Constant.IPC.CORPORATE;
            //    custcode = txtGrpCode.Text.Trim();
            //    hasCustInfo = new SmartPortal.SEMS.Customer().GetCustInfo(txtGrpCode.Text.Trim(), ctmType, ref IPCERRORCODE, ref IPCERRORDESC);
            //}
            //else
            //{
            //    ctmType = SmartPortal.Constant.IPC.PERSONAL;
            //    custcode = txtTaxCode.Text.Trim();
            //    hasCustInfo = new SmartPortal.SEMS.Customer().GetCustInfoByLicense(txtTaxCode.Text.Trim(), ctmType, ref IPCERRORCODE, ref IPCERRORDESC);
            //}

            if (IPCERRORCODE != "0")
            {
                throw new IPCException(SmartPortal.Constant.IPC.ERRORCODE.CUSTNOTEXIST);
            }
            if (hasCustInfo[SmartPortal.Constant.IPC.CUSTCODE] == null)
            {
                throw new IPCException(SmartPortal.Constant.IPC.ERRORCODE.CUSTNOTEXIST);
            }

            #region Kiểm tra sự tồn tại của khách hàng
            DataTable tblCE = new SmartPortal.SEMS.Customer().CheckCustExists(SmartPortal.Common.Utilities.Utility.FormatStringCore(custcode), ctmType, SmartPortal.Constant.IPC.CONTRACTAGENTMERCHANT);
            if (tblCE.Rows.Count != 0)
            {
                lblError.Text = Resources.labels.khachhangnaydatontaitronghethong;
                btnNext.Visible = false;
                return;
            }
            #endregion


            string resultInfo = new Customer().CheckCustInfo(txtCustCode.Text.Trim(), PHONE, "B", SmartPortal.Constant.IPC.CONTRACTAGENTMERCHANT, ref IPCERRORCODE, ref IPCERRORDESC);
            if (resultInfo.Equals("-1"))
            {
                lblError.Text = Resources.labels.khachhangnaydatontaitronghethong;
                return;
            }
            txtCustCodeInfo.Text = SmartPortal.Common.Utilities.Utility.FormatStringCore(hasCustInfo[SmartPortal.Constant.IPC.CUSTCODE].ToString());

            if (hasCustInfo[SmartPortal.Constant.IPC.CUSTNAME] != null)
            {
                txtFullName.Text = hasCustInfo[SmartPortal.Constant.IPC.CUSTNAME].ToString();
            }

            if (hasCustInfo[SmartPortal.Constant.IPC.CUSTTYPE] != null)
            {
                if (string.IsNullOrEmpty(hasCustInfo[SmartPortal.Constant.IPC.CUSTTYPE].ToString().Trim()))
                {
                    lblError.Text = Resources.labels.thetypeofcustomerreturnsempty;
                }
                else
                {
                    ddlCustType.SelectedValue = hasCustInfo[SmartPortal.Constant.IPC.CUSTTYPE].ToString().Trim();
                    //load loai hinh san pham
                    LoadProductByUserType();
                }

            }

            if (hasCustInfo[SmartPortal.Constant.IPC.DOB] != null)
            {
                try
                {
                    //HaiNT Edit birthDate (12/08/2013)
                    string birthDate = SmartPortal.Common.Utilities.Utility.IsDateTime1(hasCustInfo[SmartPortal.Constant.IPC.DOB].ToString()).ToString("dd/MM/yyyy");
                    txtBirth.Text = birthDate.Equals("01/01/1900") ? "" : birthDate;

                    txtBirth.Enabled = string.IsNullOrEmpty(txtBirth.Text.Trim());
                }
                catch
                {
                }
            }
            //phone 
            if (hasCustInfo[SmartPortal.Constant.IPC.PHONE] != null)
            {

                string phone = hasCustInfo[SmartPortal.Constant.IPC.PHONE].ToString().Trim();

                //add test phone
                PHONE = txtPhoneWL.Text = phone;
                if (!CheckExistPhoneNumber(PHONE))
                {
                    lblError.Text = Resources.labels.phonenumberisalreadyregistered;
                    btnSearchWL.Visible = true;
                    txtPhoneWL.Enabled = true;
                    btnNext.Visible = false;
                }
                else if (!CheckIsPhoneNumer(PHONE))
                {
                    btnSearchWL.Visible = true;
                    txtPhoneWL.Enabled = true;
                    btnNext.Visible = false;
                    lblError.Text = Resources.labels.phonenumberwrong;
                }
                else
                {
                    btnSearchWL.Visible = false;
                    // txtPhoneWL.Enabled = false;
                }

            }
            else
            {
                btnSearchWL.Visible = true;
                txtPhoneWL.Enabled = true;
                btnNext.Visible = false;
                lblError.Text = Resources.labels.phonenumberwrong;
            }
            if (hasCustInfo[SmartPortal.Constant.IPC.EMAIL] != null)
            {
                txtEmail.Text = hasCustInfo[SmartPortal.Constant.IPC.EMAIL].ToString().Trim();
                if (!new EmailAddressAttribute().IsValid(txtEmail.Text))
                {
                    txtEmail.Enabled = true;
                }
                else
                {
                    txtEmail.Enabled = false;
                }
            }
            else
            {
                txtEmail.Enabled = true;
            }
            //quantxa checked kyc
            if (hasCustInfo["NRIC"] != null && hasCustInfo["IDTYPE"] != null)
            {
                txtNRICNewNumber.Text = hasCustInfo["NRIC"].ToString().Trim();
                ddlKYCName.SelectedValue = hasCustInfo["IDTYPE"].ToString().Trim();
                ddlKYCName.Enabled = false;
                txtNRICNewNumber.Enabled = false;
                loadData();
            }
            else if (hasCustInfo["NRIC"] != null && hasCustInfo["IDTYPE"] == null)
            {
                txtNRICNewNumber.Text = hasCustInfo["NRIC"].ToString().Trim();
                ddlKYCName.Enabled = true;
                txtNRICNewNumber.Enabled = true;
                btnCheckKYC.Visible = true;
            }
            else if (hasCustInfo["NRIC"] == null && hasCustInfo["IDTYPE"] != null)
            {
                ddlKYCName.SelectedValue = hasCustInfo["IDTYPE"].ToString().Trim();
                ddlKYCName.Enabled = true;
                txtNRICNewNumber.Enabled = true;
                btnCheckKYC.Visible = true;
            }
            else
            {
                ddlKYCName.Enabled = true;
                txtNRICNewNumber.Enabled = true;
                btnCheckKYC.Visible = true;
            }

            if (hasCustInfo[SmartPortal.Constant.IPC.ADDRESS] != null)
            {
                txtResidentAddr.Text = hasCustInfo[SmartPortal.Constant.IPC.ADDRESS].ToString().Trim();

                txtResidentAddr.Enabled = string.IsNullOrEmpty(txtResidentAddr.Text.Trim());
            }

            if (hasCustInfo[SmartPortal.Constant.IPC.LICENSE] != null)
            {
                txtIF.Text = hasCustInfo[SmartPortal.Constant.IPC.LICENSE].ToString().Trim();

                txtIF.Enabled = string.IsNullOrEmpty(txtIF.Text.Trim());
            }

            if (hasCustInfo[SmartPortal.Constant.IPC.LICDATE] != null)
            {
                try
                {
                    txtIssueDate.Text = SmartPortal.Common.Utilities.Utility.IsDateTime1(hasCustInfo[SmartPortal.Constant.IPC.LICDATE].ToString()).ToString("dd/MM/yyyy");

                    txtIssueDate.Enabled = string.IsNullOrEmpty(txtIssueDate.Text.Trim());
                }
                catch
                {
                }
            }

            if (hasCustInfo[SmartPortal.Constant.IPC.LICPLACE] != null)
            {
                txtIssuePlace.Text = hasCustInfo[SmartPortal.Constant.IPC.LICPLACE].ToString().Trim();

                txtIssuePlace.Enabled = string.IsNullOrEmpty(txtIssuePlace.Text.Trim());
            }

            if (hasCustInfo[SmartPortal.Constant.IPC.ORGNATION] != null)
            {
                txtOfficeAddr.Text = hasCustInfo[SmartPortal.Constant.IPC.ORGNATION].ToString().Trim();

                txtOfficeAddr.Enabled = string.IsNullOrEmpty(txtOfficeAddr.Text.Trim());
            }

            if (hasCustInfo[SmartPortal.Constant.IPC.SEX] != null)
            {
                try
                {
                    ddlGender.SelectedValue = SmartPortal.Common.Utilities.Utility.FormatStringCore(int.Parse(hasCustInfo[SmartPortal.Constant.IPC.SEX].ToString()).ToString()) == "1" ? "M" : "F";

                    ddlGender.Enabled = string.IsNullOrEmpty(hasCustInfo[SmartPortal.Constant.IPC.SEX].ToString().Trim());
                }
                catch
                {
                }
            }

            if (hasCustInfo[SmartPortal.Constant.IPC.SHORTNAME] != null)
            {
                txtShortName.Text = hasCustInfo[SmartPortal.Constant.IPC.SHORTNAME].ToString().Trim();

            }

            //hien thi ma hop dong
            //txtContractNo.Text = SmartPortal.Common.Utilities.Utility.GetID("HD", txtCustCodeInfo.Text.Trim(),"P",15);
            //MA HD
            //txtContractNo.Text = SmartPortal.Common.Utilities.Utility.GetID(SmartPortal.Constant.IPC.CONTRACTNOPREFIX, txtCustCodeInfo.Text.Trim(), ctmType, 15);
            //branch
            txtContractNo.Text = new SmartPortal.SEMS.User().CreatePrimaryKey(string.Empty, string.Empty, "CONTRACTNO", ref IPCERRORCODE, ref IPCERRORDESC);
            if (hasCustInfo[SmartPortal.Constant.IPC.BRID] != null)
            {
                try
                {
                    if (!string.IsNullOrEmpty(hasCustInfo[SmartPortal.Constant.IPC.BRID].ToString().Trim()))
                    {
                        ddlBranch.SelectedValue = SmartPortal.Common.Utilities.Utility.FormatStringCore(int.Parse(hasCustInfo[SmartPortal.Constant.IPC.BRID].ToString()).ToString());
                        ddlBranch.Enabled = false;
                    }
                }
                catch
                {
                }
            }


        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(IPCex.ToString(), this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.ToString(), Request.Url.Query);
            //SmartPortal.Common.Log.GoToErrorPage(IPCex.Message, Request.Url.Query);         
            lblError.Text = (new SmartPortal.BLL.ErrorBLL().Load(Utility.IsInt(IPCex.Message), System.Globalization.CultureInfo.CurrentCulture.ToString())).ErrorDesc;

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            //SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
            lblError.Text = Resources.labels.customerinformationreturnisincorrect;
        }
    }

    protected void btnThemChuTaiKhoan_Click(object sender, EventArgs e)
    {
        try
        {
            int passlenIB = 0;
            int passlenSMS = 0;
            int passlenMB = 0;
            #region Tao bang chua cac thong tin nguoi uy quyen
            string PassTemp = "";
            lblAlert.Text = string.Empty;
            #region check username

            if (txtUserNameMB.Text.Trim() == string.Empty)
            {
                lblAlert.Text = Resources.labels.bancannhaptendangnhap;
                return;
            }
            else if (txtUserNameMB.Text.Trim().Length < minlength || txtUserNameMB.Text.Trim().Length > maxlength)
            {
                lblAlert.Text = string.Format(Resources.labels.usernamemustbetween, minlength, maxlength);
                return;
            }

            DataSet dsResult = new Customer().CheckUserName("EBA_USERS_CHECKUSERNAMEMB", new object[] { txtUserNameMB.Text.Trim(), SmartPortal.Constant.IPC.CONTRACTAGENTMERCHANT }, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE != "0")
            {
                lblAlert.Text = IPCERRORDESC;
                return;
            }
            if (!validateusername(txtUserNameMB))
            {
                return;
            }

            #endregion
            userName = USERCTK;
            if (userName == string.Empty || userName == null)
            {
                lblAlert.Text = Resources.labels.bancannhaptendangnhap;
                return;
            }
            if (txtLocalFullName.Text == string.Empty || txtLocalFullName.Text == null)
            {
                lblAlert.Text = Resources.labels.youneedinputagentmerchantname;
                return;
            }
            if (userName == string.Empty || userName == null)
            {
                lblAlert.Text = Resources.labels.bancannhapsodienthoainguoisudung;
                return;
            }
            //check Phone Add
            if (!CheckExistPhoneNumber(PHONE))
            {
                lblError.Text = Resources.labels.phonenumberisalreadyregistered;
                return;
            }
            if (!CheckIsPhoneNumer(PHONE))
            {
                lblError.Text = Resources.labels.phonenumberwrong;
                return;
            }
            //minh add 13/5/2015 1 số điện thoại chỉ được dùng cho 1 tài khoản sms mặc định
            //#region check phone number and default account
            //string phoneNumber = txtSMSPhoneNo.Text.Trim();
            //string defaultAcc = ddlSMSDefaultAcctno.Text.Trim();
            //DataTable dt1 = new SmartPortal.SEMS.Customer().CheckPhoneNumber(phoneNumber, defaultAcc);
            //if (dt1.Rows.Count != 0)
            //{
            //    lblAlert.Text = Resources.labels.phonenumberassigned;
            //    return;
            //}


            //#endregion
            //27/8/2015 minh add to validate email if not empty
            string pattern = Resources.labels.emailpattern;

            if (!string.IsNullOrEmpty(txtReEmail.Text.Trim()))
            {
                if (!(System.Text.RegularExpressions.Regex.IsMatch(txtReEmail.Text, pattern)))
                {
                    ShowPopUpMsg(Resources.labels.emailkhongdinhdang1);
                    //lblAlert.Text = Resources.labels.emailkhongdinhdang1;
                    txtReEmail.Focus();
                    return;

                }
            }
            //28/5/2015 minh add to add fix force contract must have email or sms service
            if (txtReEmail.Text.Trim() != txtEmail.Text.Trim())
            {
                txtEmail.Text = txtReEmail.Text.Trim();
            }

            #region check sms notify vutt 30032016
            string errDesc = string.Empty;
            if (!string.IsNullOrEmpty(txtSMSPhoneNo.Text))
            {
                //if (!ContractControl.ValidateSMSNotifyCurrentUser(ddlProduct.SelectedValue, tvSMS, ref errDesc, radAllAccount.Checked ? "" : ddlAccount.SelectedValue, new List<DataTable> { (DataTable)ViewState["NGUOIUYQUYEN"] }))
                //phongtt sms notification fee
                if (!ContractControl.ValidateSMSNotifyCurrentUser(ddlProduct.SelectedValue, tvSMS, ref errDesc, ddlAccountMB.SelectedValue == "ALL" ? "" : ddlAccountMB.SelectedValue, lsAccNo, new List<DataTable> { (DataTable)ViewState["NGUOIUYQUYEN"] }))
                {
                    lblAlert.Text = errDesc;
                    return;
                }
            }
            #endregion
            //21.3.2016 load infor about policy selected to get len of pass generate
            DataSet dspolicy = new DataSet();
            string filterIB = "serviceid='IB'";
            if (ddlpolicyIB.SelectedValue.ToString() != "")
            {
                filterIB += " and policyid='" + ddlpolicyIB.SelectedValue.ToString() + "'";
            }
            string filterSMS = "serviceid='SMS'  and policyid='" + ddlpolicySMS.SelectedValue.ToString() + "'";
            string filterMB = "serviceid='AM' and policyid='" + ddlpolicyMB.SelectedValue.ToString() + "'";
            string stSort = "serviceid asc";

            dspolicy = new SmartPortal.SEMS.USERPOLICY().GetPolicybyCondition(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, Session["userName"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0" && dspolicy != null)
            {
                DataTable dt = dspolicy.Tables[0];
                DataTable dtIB = dt.Select(filterIB, stSort).Any() ? dt.Select(filterIB, stSort).CopyToDataTable() : null;
                DataTable dtSMS = null;
                DataTable dtMB = dt.Select(filterMB, stSort).Any() ? dt.Select(filterMB, stSort).CopyToDataTable() : null;


                passlenIB = dtIB == null ? int.Parse(ConfigurationManager.AppSettings["minpwdlen_default"].ToString()) : Convert.ToInt32(dtIB.Rows[0]["minpwdlen"].ToString());
                passlenSMS = dtSMS == null ? int.Parse(ConfigurationManager.AppSettings["minpwdlen_default"].ToString()) : Convert.ToInt32(dtSMS.Rows[0]["minpwdlen"].ToString());
                passlenMB = dtMB == null ? int.Parse(ConfigurationManager.AppSettings["minpwdlen_default"].ToString()) : Convert.ToInt32(dtMB.Rows[0]["minpwdlen"].ToString());

            }

            if (ddlAccountMB.SelectedValue != "ALL")
            {
                //PassTemp = Encryption.Encrypt(DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - 8, 8));
                //LuuThongTinQuyen("CHUTAIKHOAN", gvResultChuTaiKhoan, tvIB, tvSMS, tvMB, tvPHO, txtReFullName.Text, lblLevel.Text, txtReBirth.Text, ddlReGender.SelectedValue, txtReMobi.Text, txtReEmail.Text, txtReAddress.Text, userName, PassTemp, txtSMSPhoneNo.Text, ddlSMSDefaultAcctno.SelectedValue, ddlLanguage.SelectedValue, ((cbIsDefault.Checked == true) ? "Y" : "N"), DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - 8, 6), txtMBPhoneNo.Text, PassTemp, DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - 8, 6), txtPHOPhoneNo.Text, PassTemp, ddlPHODefaultACCTNO.SelectedValue, ddlAccount.SelectedValue);
                //24.2.2016 minh add encrypt pass new
                string passreveal = DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - passlenMB, passlenMB);
                string pwdreset = Encryption.Encrypt(passreveal);
                //PassTemp = SmartPortal.SEMS.O9Encryptpass.sha_sha256(pwdreset, userName);
                PassTemp = SmartPortal.SEMS.O9Encryptpass.sha_sha256(passreveal, USERCTK);
                string PinCode = DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - passlenMB, passlenMB);
                string PinCodeMB = SmartPortal.SEMS.O9Encryptpass.sha_sha256(PinCode, USERCTK);


                string pwdresetSMS = DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - passlenSMS, passlenSMS);


                LuuThongTinQuyen("CHUTAIKHOAN", gvResultChuTaiKhoan, tvIB, tvSMS, tvMB, tvAM, txtReFullName.Text, "0", txtReBirth.Text, ddlReGender.SelectedValue, txtReMobi.Text, txtReEmail.Text, txtReAddress.Text, userName, PassTemp, PHONE, ddlSMSDefaultAcctno.SelectedValue, ddlLanguage.SelectedValue, ((cbIsDefault.Checked == true) ? "Y" : "N"), pwdresetSMS, PHONE, PassTemp, PinCodeMB, SmartPortal.Constant.IPC.MBA, ddlAccountMB.SelectedValue, pwdreset, DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - 8, 6), "", PassTemp, txtLocalFullName.Text.Trim(), ddlLoginMethod.SelectedValue.Trim(), ddlauthenType.SelectedValue.Trim());

            }

            if (ddlAccountMB.SelectedValue == "ALL")
            {
                //lay tat ca tai khoan khach hang
                DataSet ds = new DataSet();
                switch (ddlCustType.SelectedValue.Trim())
                {
                    case SmartPortal.Constant.IPC.PERSONAL:
                        ds = new SmartPortal.SEMS.Customer().GetAcctNo(txtCustCodeInfo.Text.Trim(), ddlCustType.SelectedValue, ref IPCERRORCODE, ref IPCERRORDESC);
                        break;
                        //case SmartPortal.Constant.IPC.PERSONALLKG:
                        //    ds = new SmartPortal.SEMS.Customer().GetAcctNo(txtLkgCode.Text.Trim(), ddlCustType.SelectedValue, ref IPCERRORCODE, ref IPCERRORDESC);
                        //    break;
                        //case SmartPortal.Constant.IPC.CORPORATE:
                        //    ds = new SmartPortal.SEMS.Customer().GetAcctNo(txtGrpCode.Text.Trim(), ddlCustType.SelectedValue, ref IPCERRORCODE, ref IPCERRORDESC);
                        //    break;
                }
                if (IPCERRORCODE != "0")
                {
                    throw new IPCException(IPCERRORDESC);
                }
                DataTable dtAccount = new DataTable();
                dtAccount = ds.Tables[0];

                //luu tat ca account
                //PassTemp = Encryption.Encrypt(DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - 8, 8));
                //24.2.2016 minh add encrypt pass new
                string passreveal = DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - passlenMB, passlenMB);
                string pwdreset = Encryption.Encrypt(passreveal);
                PassTemp = SmartPortal.SEMS.O9Encryptpass.sha_sha256(passreveal, USERCTK);
                string pwdresetSMS = DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - passlenSMS, passlenSMS);

                string PinCode = DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - passlenMB, passlenMB);
                string PinCodeMB = SmartPortal.SEMS.O9Encryptpass.sha_sha256(PinCode, USERCTK);

                foreach (DataRow rowAccount in dtAccount.Rows)
                {
                    //LuuThongTinQuyen("CHUTAIKHOAN", gvResultChuTaiKhoan, tvIB, tvSMS, tvMB, tvPHO, txtReFullName.Text, lblLevel.Text, txtReBirth.Text, ddlReGender.SelectedValue, txtReMobi.Text, txtReEmail.Text, txtReAddress.Text, userName, PassTemp, txtSMSPhoneNo.Text, ddlSMSDefaultAcctno.SelectedValue, ddlLanguage.SelectedValue, ((cbIsDefault.Checked == true) ? "Y" : "N"), DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - 8, 6), txtMBPhoneNo.Text, PassTemp, DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - 8, 6), txtPHOPhoneNo.Text, PassTemp, ddlPHODefaultACCTNO.SelectedValue, rowAccount["ACCOUNTNO"].ToString());
                    LuuThongTinQuyen("CHUTAIKHOAN", gvResultChuTaiKhoan, tvIB, tvSMS, tvMB, tvAM, txtReFullName.Text, "0", txtReBirth.Text, ddlReGender.SelectedValue, txtReMobi.Text, txtReEmail.Text, txtReAddress.Text, userName, PassTemp, PHONE, ddlSMSDefaultAcctno.SelectedValue, ddlLanguage.SelectedValue, ((cbIsDefault.Checked == true) ? "Y" : "N"), pwdresetSMS, PHONE, PassTemp, PinCodeMB, SmartPortal.Constant.IPC.MBA, rowAccount["ACCOUNTNO"].ToString(), pwdreset, DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - 8, 6), "", PassTemp, txtLocalFullName.Text.Trim(), ddlLoginMethod.SelectedValue.Trim(), ddlauthenType.SelectedValue.Trim());
                }
            }
            if (TabAgentMerchantHelper.TabAMVisibility == 1)
            {
                // Luu thong tin Wallet
                string passreveal = DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - passlenMB, passlenMB);
                string pwdreset = Encryption.Encrypt(passreveal);
                //PassTemp = SmartPortal.SEMS.O9Encryptpass.sha_sha256(pwdreset, userName);
                PassTemp = SmartPortal.SEMS.O9Encryptpass.sha_sha256(passreveal, USERCTK);
                string PinCode = DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - passlenMB, passlenMB);
                string PinCodeMB = SmartPortal.SEMS.O9Encryptpass.sha_sha256(PinCode, USERCTK);


                string pwdresetSMS = DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - passlenSMS, passlenSMS);


                LuuThongTinQuyen("CHUTAIKHOAN", gvResultChuTaiKhoan, tvIB, tvSMS, tvMB, tvAM, txtReFullName.Text, "0", txtReBirth.Text, ddlReGender.SelectedValue, txtReMobi.Text, txtReEmail.Text, txtReAddress.Text, userName, PassTemp, "", ddlSMSDefaultAcctno.SelectedValue, ddlLanguage.SelectedValue, ((cbIsDefault.Checked == true) ? "Y" : "N"), pwdresetSMS, "", PassTemp, PinCodeMB, SmartPortal.Constant.IPC.EAM, PHONE, pwdreset, DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - 8, 6), PHONE, PassTemp, txtLocalFullName.Text.Trim(), ddlLoginMethod.SelectedValue.Trim(), ddlauthenType.SelectedValue.Trim());


            }

            #endregion
            //28/8/2015 minh fixed truong hop khong chon dich vu ma bam nut add
            if (gvResultChuTaiKhoan.Rows.Count == 0)
            {
                lblAlert.Text = Resources.labels.banchuadangkydichvu;
                return;
            }

        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], "Widgets_SEMSCustomerList_Add_Widget", "btnThemChuTaiKhoan_Click", IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);
        }

        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSCustomerList_Add_Widget", "btnThemChuTaiKhoan_Click", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void gvResultChuTaiKhoan_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvResultChuTaiKhoan.PageIndex = e.NewPageIndex;
            gvResultChuTaiKhoan.DataSource = (DataTable)ViewState["CHUTAIKHOAN"];
            gvResultChuTaiKhoan.DataBind();
        }
        catch
        {
        }
    }
    protected void gvResultChuTaiKhoan_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            DataTable tblResultChuTaiKhoan = (DataTable)ViewState["CHUTAIKHOAN"];

            tblResultChuTaiKhoan.Rows.RemoveAt(e.RowIndex + (gvResultChuTaiKhoan.PageIndex * gvResultChuTaiKhoan.PageSize));

            ViewState["CHUTAIKHOAN"] = tblResultChuTaiKhoan;
            gvResultChuTaiKhoan.DataSource = tblResultChuTaiKhoan;
            gvResultChuTaiKhoan.DataBind();

            lblAlert.Text = Resources.labels.recorddeleted;
        }
        catch
        {
        }
    }

    protected void btnResetNguoiUyQuyen_Click(object sender, EventArgs e)
    {
        try
        {
            #region reset thong tin nguoi uy quyen
            txtFullnameNguoiUyQuyen.Text = "";
            txtBirthNguoiUyQuyen.Text = "";
            txtPhoneNguoiUyQuyen.Text = "";
            txtEmailNguoiUyQuyen.Text = "";
            txtAddressNguoiUyQuyen.Text = "";

            txtSMSPhoneNguoiUyQuyen.Text = "";
            txtMBPhoneNguoiUyQuyen.Text = "";


            string strCode = txtCustCodeInfo.Text.Trim() + ddlCustType.SelectedValue.Trim() + DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - 6, 4);
            txtIBGenUserNameNguoiUyQuyen.Text = strCode;

            SetRadio();
            #endregion
        }
        catch
        {
        }
    }
    void SetRadio()
    {
        radAllAccountNguoiUyQuyen.Checked = true;
        radAccountNguoiUyQuyen.Checked = false;
    }
    //void LuuThongTinQuyen(string sessionName, GridView gvResult, TreeView tvRole, TreeView tvSMSRole, TreeView tvMBRole, TreeView tvPHORole, string fullName, string level, string birthday, string gender, string phone, string email, string address, string IBUserName, string IBPass, string SMSPhone, string SMSDefaultAcctno, string SMSDefaultLang, string isDefault, string SMSPinCode, string MBPhone, string MBPass, string MBPinCode, string PHOPhone, string PHOPass, string PHODefaultAcctno, string Account)
    void LuuThongTinQuyen(string sessionName, GridView gvResult, TreeView tvRole, TreeView tvSMSRole, TreeView tvMBRole, TreeView tvWLRole, string fullName, string level, string birthday, string gender, string phone, string email, string address, string IBUserName, string IBPass, string SMSPhone, string SMSDefaultAcctno, string SMSDefaultLang, string isDefault, string SMSPinCode, string MBPhone, string MBPass, string MBPinCode, string roleType, string Account, string pwdreset, string WLPinCode, string WLPhone, string WLPass, string localFullName, string loginMethod, string authenType)
    {

        SmartPortal.Common.Log.WriteLogFile("pwdreset===============", "", "", pwdreset);
        SmartPortal.Common.Log.WriteLogFile("pincodesms ==============", "", "", SMSPinCode);
        if (ViewState[sessionName] == null)
        {
            #region Khoi tao bang
            DataTable tblNguoiUyQuyen = new DataTable();

            DataColumn colFullName = new DataColumn("colFullName");
            DataColumn colLocalname = new DataColumn("colLocalname");
            DataColumn colLoginMethod = new DataColumn("colLoginMethod");
            DataColumn colAuthenType = new DataColumn("colAuthenType");
            DataColumn colLevel = new DataColumn("colLevel");
            DataColumn colBirthday = new DataColumn("colBirthday");
            DataColumn colGender = new DataColumn("colGender");
            DataColumn colPhone = new DataColumn("colPhone");
            DataColumn colEmail = new DataColumn("colEmail");
            DataColumn colAddress = new DataColumn("colAddress");
            DataColumn colIBUserName = new DataColumn("colIBUserName");
            DataColumn colIBPass = new DataColumn("colIBPass");
            DataColumn colSMSPhone = new DataColumn("colSMSPhone");
            DataColumn colSMSDefaultAcctno = new DataColumn("colSMSDefaultAcctno");
            DataColumn colSMSDefaultLang = new DataColumn("colSMSDefaultLang");
            DataColumn colSMSIsDefault = new DataColumn("colSMSIsDefault");
            DataColumn colSMSPinCode = new DataColumn("colSMSPinCode");
            DataColumn colMBPhone = new DataColumn("colMBPhone");
            DataColumn colMBPass = new DataColumn("colMBPass");
            DataColumn colRoleType = new DataColumn("colRoleType");
            DataColumn colWLPhone = new DataColumn("colWLPhone");
            DataColumn colWLPass = new DataColumn("colWlPass");
            DataColumn colWLPinCode = new DataColumn("colWLPinCode");
            DataColumn colMBPinCode = new DataColumn("colMBPinCode");
            DataColumn colAccount = new DataColumn("colAccount");
            DataColumn colRole = new DataColumn("colRole");
            DataColumn colRoleID = new DataColumn("colRoleID");
            DataColumn colTranCode = new DataColumn("colTranCode");
            DataColumn colTranCodeID = new DataColumn("colTranCodeID");
            DataColumn colServiceID = new DataColumn("colServiceID");
            DataColumn colIBPolicy = new DataColumn("colIBPolicy");
            DataColumn colSMSPolicy = new DataColumn("colSMSPolicy");
            DataColumn colMBPolicy = new DataColumn("colMBPolicy");
            DataColumn colWLPolicy = new DataColumn("colWLPolicy");
            DataColumn colpwdreset = new DataColumn("colpwdreset");

            tblNguoiUyQuyen.Columns.Add(colFullName);
            tblNguoiUyQuyen.Columns.Add(colLocalname);
            tblNguoiUyQuyen.Columns.Add(colLoginMethod);
            tblNguoiUyQuyen.Columns.Add(colAuthenType);
            tblNguoiUyQuyen.Columns.Add(colLevel);
            tblNguoiUyQuyen.Columns.Add(colBirthday);
            tblNguoiUyQuyen.Columns.Add(colGender);
            tblNguoiUyQuyen.Columns.Add(colPhone);
            tblNguoiUyQuyen.Columns.Add(colEmail);
            tblNguoiUyQuyen.Columns.Add(colAddress);
            tblNguoiUyQuyen.Columns.Add(colIBUserName);
            tblNguoiUyQuyen.Columns.Add(colIBPass);
            tblNguoiUyQuyen.Columns.Add(colSMSPhone);
            tblNguoiUyQuyen.Columns.Add(colSMSDefaultAcctno);
            tblNguoiUyQuyen.Columns.Add(colSMSDefaultLang);
            tblNguoiUyQuyen.Columns.Add(colSMSIsDefault);
            tblNguoiUyQuyen.Columns.Add(colSMSPinCode);
            tblNguoiUyQuyen.Columns.Add(colMBPhone);
            tblNguoiUyQuyen.Columns.Add(colMBPass);
            tblNguoiUyQuyen.Columns.Add(colMBPinCode);
            tblNguoiUyQuyen.Columns.Add(colRoleType);
            tblNguoiUyQuyen.Columns.Add(colWLPhone);
            tblNguoiUyQuyen.Columns.Add(colWLPass);
            tblNguoiUyQuyen.Columns.Add(colWLPinCode);
            tblNguoiUyQuyen.Columns.Add(colAccount);
            tblNguoiUyQuyen.Columns.Add(colRole);
            tblNguoiUyQuyen.Columns.Add(colRoleID);
            tblNguoiUyQuyen.Columns.Add(colTranCode);
            tblNguoiUyQuyen.Columns.Add(colTranCodeID);
            tblNguoiUyQuyen.Columns.Add(colServiceID);
            tblNguoiUyQuyen.Columns.Add(colIBPolicy);
            tblNguoiUyQuyen.Columns.Add(colSMSPolicy);
            tblNguoiUyQuyen.Columns.Add(colMBPolicy);
            tblNguoiUyQuyen.Columns.Add(colWLPolicy);

            tblNguoiUyQuyen.Columns.Add(colpwdreset);

            #endregion

            #region them giao dich IB
            //IB
            if (IBUserName != "" && WLPhone == "")
            {
                foreach (TreeNode nodeRoleIBNguoiUyQuyen in tvRole.Nodes)
                {
                    if (nodeRoleIBNguoiUyQuyen.Checked)
                    {
                        foreach (TreeNode nodeTrancodeIBNguoiUyQuyen in nodeRoleIBNguoiUyQuyen.ChildNodes)
                        {
                            DataRow rowNguoiUyQuyen = tblNguoiUyQuyen.NewRow();
                            rowNguoiUyQuyen["colFullName"] = fullName.Trim();
                            rowNguoiUyQuyen["colLocalname"] = localFullName.Trim();
                            rowNguoiUyQuyen["colLoginMethod"] = loginMethod.Trim();
                            rowNguoiUyQuyen["colAuthenType"] = authenType.Trim();
                            rowNguoiUyQuyen["colLevel"] = level.Trim();
                            rowNguoiUyQuyen["colBirthday"] = birthday.Trim();
                            rowNguoiUyQuyen["colGender"] = gender.Trim();
                            rowNguoiUyQuyen["colPhone"] = phone.Trim();
                            rowNguoiUyQuyen["colEmail"] = email.Trim();
                            rowNguoiUyQuyen["colAddress"] = address.Trim();
                            rowNguoiUyQuyen["colIBUserName"] = IBUserName.Trim();
                            rowNguoiUyQuyen["colIBPass"] = IBPass;
                            rowNguoiUyQuyen["colSMSPhone"] = SMSPhone.Trim();
                            rowNguoiUyQuyen["colSMSDefaultAcctno"] = SMSDefaultAcctno.Trim();
                            rowNguoiUyQuyen["colSMSDefaultLang"] = SMSDefaultLang.Trim();
                            rowNguoiUyQuyen["colSMSIsDefault"] = isDefault;
                            rowNguoiUyQuyen["colSMSPinCode"] = SMSPinCode;
                            rowNguoiUyQuyen["colMBPinCode"] = MBPinCode;
                            rowNguoiUyQuyen["colMBPhone"] = MBPhone.Trim();
                            rowNguoiUyQuyen["colMBPass"] = MBPass;
                            rowNguoiUyQuyen["colRoleType"] = string.Empty;
                            rowNguoiUyQuyen["colWLPinCode"] = WLPinCode;
                            rowNguoiUyQuyen["colWLPhone"] = WLPhone.Trim();
                            rowNguoiUyQuyen["colWLPass"] = WLPass;
                            rowNguoiUyQuyen["colAccount"] = Account;
                            rowNguoiUyQuyen["colRole"] = nodeRoleIBNguoiUyQuyen.Text;
                            rowNguoiUyQuyen["colRoleID"] = nodeRoleIBNguoiUyQuyen.Value;
                            rowNguoiUyQuyen["colTranCode"] = nodeTrancodeIBNguoiUyQuyen.Text;
                            rowNguoiUyQuyen["colTranCodeID"] = nodeTrancodeIBNguoiUyQuyen.Value;
                            rowNguoiUyQuyen["colServiceID"] = SmartPortal.Constant.IPC.IB;
                            switch (sessionName)
                            {
                                case "CHUTAIKHOAN":
                                    rowNguoiUyQuyen["colIBPolicy"] = ddlpolicyIB.SelectedValue.ToString();
                                    rowNguoiUyQuyen["colSMSPolicy"] = ddlpolicySMS.SelectedValue.ToString();
                                    rowNguoiUyQuyen["colMBPolicy"] = ddlpolicyMB.SelectedValue.ToString();
                                    rowNguoiUyQuyen["colWLPolicy"] = ddlpolicyMB.SelectedValue.ToString();
                                    break;
                                case "NGUOIUYQUYEN":
                                    rowNguoiUyQuyen["colIBPolicy"] = ddlpolicyIBco.SelectedValue.ToString();
                                    rowNguoiUyQuyen["colSMSPolicy"] = ddlpolicySMSco.SelectedValue.ToString();
                                    rowNguoiUyQuyen["colMBPolicy"] = ddlpolicyMBco.SelectedValue.ToString();
                                    rowNguoiUyQuyen["colWLPolicy"] = ddlpolicyWLco.SelectedValue.ToString();
                                    break;
                            }
                            rowNguoiUyQuyen["colpwdreset"] = pwdreset;

                            tblNguoiUyQuyen.Rows.Add(rowNguoiUyQuyen);

                        }
                    }
                    else
                    {
                    }
                }
            }
            #endregion

            #region them giao dich SMS
            //SMS
            if (SMSPhone != "")
            {
                foreach (TreeNode nodeRoleIBNguoiUyQuyen in tvSMSRole.Nodes)
                {
                    if (nodeRoleIBNguoiUyQuyen.Checked)
                    {
                        foreach (TreeNode nodeTrancodeIBNguoiUyQuyen in nodeRoleIBNguoiUyQuyen.ChildNodes)
                        {

                            DataRow rowNguoiUyQuyen = tblNguoiUyQuyen.NewRow();
                            rowNguoiUyQuyen["colFullName"] = fullName.Trim();
                            rowNguoiUyQuyen["colLocalname"] = localFullName.Trim();
                            rowNguoiUyQuyen["colLoginMethod"] = loginMethod.Trim();
                            rowNguoiUyQuyen["colAuthenType"] = authenType.Trim();
                            rowNguoiUyQuyen["colLocalname"] = localFullName.Trim();
                            rowNguoiUyQuyen["colLoginMethod"] = loginMethod.Trim();
                            rowNguoiUyQuyen["colAuthenType"] = authenType.Trim();
                            rowNguoiUyQuyen["colLevel"] = level.Trim();
                            rowNguoiUyQuyen["colBirthday"] = birthday.Trim();
                            rowNguoiUyQuyen["colGender"] = gender.Trim();
                            rowNguoiUyQuyen["colPhone"] = phone.Trim();
                            rowNguoiUyQuyen["colEmail"] = email.Trim();
                            rowNguoiUyQuyen["colAddress"] = address.Trim();
                            rowNguoiUyQuyen["colIBUserName"] = IBUserName.Trim();
                            rowNguoiUyQuyen["colIBPass"] = IBPass;
                            rowNguoiUyQuyen["colSMSPhone"] = SMSPhone.Trim();
                            rowNguoiUyQuyen["colSMSDefaultAcctno"] = SMSDefaultAcctno.Trim();
                            rowNguoiUyQuyen["colSMSDefaultLang"] = SMSDefaultLang.Trim();
                            rowNguoiUyQuyen["colSMSIsDefault"] = isDefault;
                            rowNguoiUyQuyen["colSMSPinCode"] = SMSPinCode;
                            rowNguoiUyQuyen["colMBPinCode"] = MBPinCode;
                            rowNguoiUyQuyen["colMBPhone"] = MBPhone.Trim();
                            rowNguoiUyQuyen["colMBPass"] = MBPass;
                            rowNguoiUyQuyen["colRoleType"] = string.Empty;
                            rowNguoiUyQuyen["colWLPinCode"] = WLPinCode;
                            rowNguoiUyQuyen["colWLPhone"] = WLPhone.Trim();
                            rowNguoiUyQuyen["colWLPass"] = WLPass;
                            rowNguoiUyQuyen["colAccount"] = Account;
                            rowNguoiUyQuyen["colRole"] = nodeRoleIBNguoiUyQuyen.Text;
                            rowNguoiUyQuyen["colRoleID"] = nodeRoleIBNguoiUyQuyen.Value;
                            rowNguoiUyQuyen["colTranCode"] = nodeTrancodeIBNguoiUyQuyen.Text;
                            rowNguoiUyQuyen["colTranCodeID"] = nodeTrancodeIBNguoiUyQuyen.Value;
                            rowNguoiUyQuyen["colServiceID"] = SmartPortal.Constant.IPC.SMS;
                            switch (sessionName)
                            {
                                case "CHUTAIKHOAN":
                                    rowNguoiUyQuyen["colIBPolicy"] = ddlpolicyIB.SelectedValue.ToString();
                                    rowNguoiUyQuyen["colSMSPolicy"] = ddlpolicySMS.SelectedValue.ToString();
                                    rowNguoiUyQuyen["colMBPolicy"] = ddlpolicyMB.SelectedValue.ToString();
                                    rowNguoiUyQuyen["colWLPolicy"] = ddlpolicyMB.SelectedValue.ToString();
                                    break;
                                case "NGUOIUYQUYEN":
                                    rowNguoiUyQuyen["colIBPolicy"] = ddlpolicyIBco.SelectedValue.ToString();
                                    rowNguoiUyQuyen["colSMSPolicy"] = ddlpolicySMSco.SelectedValue.ToString();
                                    rowNguoiUyQuyen["colMBPolicy"] = ddlpolicyMBco.SelectedValue.ToString();
                                    rowNguoiUyQuyen["colWLPolicy"] = ddlpolicyWLco.SelectedValue.ToString();
                                    break;
                            }
                            rowNguoiUyQuyen["colpwdreset"] = pwdreset;
                            tblNguoiUyQuyen.Rows.Add(rowNguoiUyQuyen);
                        }
                    }
                    else
                    {
                    }

                }
            }
            #endregion

            #region them giao dich MB
            //MB
            if (MBPhone != "")
            {
                foreach (TreeNode nodeRoleIBNguoiUyQuyen in tvMBRole.Nodes)
                {
                    if (nodeRoleIBNguoiUyQuyen.Checked)
                    {
                        foreach (TreeNode nodeTrancodeIBNguoiUyQuyen in nodeRoleIBNguoiUyQuyen.ChildNodes)
                        {

                            DataRow rowNguoiUyQuyen = tblNguoiUyQuyen.NewRow();
                            rowNguoiUyQuyen["colFullName"] = fullName.Trim();
                            rowNguoiUyQuyen["colLocalname"] = localFullName.Trim();
                            rowNguoiUyQuyen["colLoginMethod"] = loginMethod.Trim();
                            rowNguoiUyQuyen["colAuthenType"] = authenType.Trim();
                            rowNguoiUyQuyen["colLevel"] = level.Trim();
                            rowNguoiUyQuyen["colBirthday"] = birthday.Trim();
                            rowNguoiUyQuyen["colGender"] = gender.Trim();
                            rowNguoiUyQuyen["colPhone"] = phone.Trim();
                            rowNguoiUyQuyen["colEmail"] = email.Trim();
                            rowNguoiUyQuyen["colAddress"] = address.Trim();
                            rowNguoiUyQuyen["colIBUserName"] = IBUserName.Trim();
                            rowNguoiUyQuyen["colIBPass"] = IBPass;
                            rowNguoiUyQuyen["colSMSPhone"] = SMSPhone.Trim();
                            rowNguoiUyQuyen["colSMSDefaultAcctno"] = SMSDefaultAcctno.Trim();
                            rowNguoiUyQuyen["colSMSDefaultLang"] = SMSDefaultLang.Trim();
                            rowNguoiUyQuyen["colSMSIsDefault"] = isDefault;
                            rowNguoiUyQuyen["colSMSPinCode"] = SMSPinCode;
                            rowNguoiUyQuyen["colMBPinCode"] = MBPinCode;
                            rowNguoiUyQuyen["colMBPhone"] = MBPhone.Trim();
                            rowNguoiUyQuyen["colMBPass"] = MBPass;
                            rowNguoiUyQuyen["colRoleType"] = SmartPortal.Constant.IPC.MBA;
                            rowNguoiUyQuyen["colWLPinCode"] = WLPinCode;
                            rowNguoiUyQuyen["colWLPhone"] = WLPhone.Trim();
                            rowNguoiUyQuyen["colWLPass"] = WLPass;
                            rowNguoiUyQuyen["colAccount"] = Account;
                            rowNguoiUyQuyen["colRole"] = nodeRoleIBNguoiUyQuyen.Text;
                            rowNguoiUyQuyen["colRoleID"] = nodeRoleIBNguoiUyQuyen.Value;
                            rowNguoiUyQuyen["colTranCode"] = nodeTrancodeIBNguoiUyQuyen.Text;
                            rowNguoiUyQuyen["colTranCodeID"] = nodeTrancodeIBNguoiUyQuyen.Value;
                            rowNguoiUyQuyen["colServiceID"] = SmartPortal.Constant.IPC.AM;
                            switch (sessionName)
                            {
                                case "CHUTAIKHOAN":
                                    rowNguoiUyQuyen["colIBPolicy"] = ddlpolicyIB.SelectedValue.ToString();
                                    rowNguoiUyQuyen["colSMSPolicy"] = ddlpolicySMS.SelectedValue.ToString();
                                    rowNguoiUyQuyen["colMBPolicy"] = ddlpolicyMB.SelectedValue.ToString();
                                    rowNguoiUyQuyen["colWLPolicy"] = ddlpolicyMB.SelectedValue.ToString();
                                    break;
                                case "NGUOIUYQUYEN":
                                    rowNguoiUyQuyen["colIBPolicy"] = ddlpolicyIBco.SelectedValue.ToString();
                                    rowNguoiUyQuyen["colSMSPolicy"] = ddlpolicySMSco.SelectedValue.ToString();
                                    rowNguoiUyQuyen["colMBPolicy"] = ddlpolicyMBco.SelectedValue.ToString();
                                    rowNguoiUyQuyen["colWLPolicy"] = ddlpolicyWLco.SelectedValue.ToString();
                                    break;
                            }
                            rowNguoiUyQuyen["colpwdreset"] = pwdreset;

                            tblNguoiUyQuyen.Rows.Add(rowNguoiUyQuyen);

                        }
                    }
                    else
                    {
                    }
                }
            }
            #endregion

            #region them giao dich WL
            //PHO
            if (WLPhone != "")
            {
                foreach (TreeNode nodeRoleIBNguoiUyQuyen in tvWLRole.Nodes)
                {
                    if (nodeRoleIBNguoiUyQuyen.Checked)
                    {
                        foreach (TreeNode nodeTrancodeIBNguoiUyQuyen in nodeRoleIBNguoiUyQuyen.ChildNodes)
                        {

                            DataRow rowNguoiUyQuyen = tblNguoiUyQuyen.NewRow();
                            rowNguoiUyQuyen["colFullName"] = fullName.Trim();
                            rowNguoiUyQuyen["colLocalname"] = localFullName.Trim();
                            rowNguoiUyQuyen["colLoginMethod"] = loginMethod.Trim();
                            rowNguoiUyQuyen["colAuthenType"] = authenType.Trim();
                            rowNguoiUyQuyen["colLevel"] = level.Trim();
                            rowNguoiUyQuyen["colBirthday"] = birthday.Trim();
                            rowNguoiUyQuyen["colGender"] = gender.Trim();
                            rowNguoiUyQuyen["colPhone"] = phone.Trim();
                            rowNguoiUyQuyen["colEmail"] = email.Trim();
                            rowNguoiUyQuyen["colAddress"] = address.Trim();
                            rowNguoiUyQuyen["colIBUserName"] = IBUserName.Trim();
                            rowNguoiUyQuyen["colIBPass"] = IBPass;
                            rowNguoiUyQuyen["colSMSPhone"] = SMSPhone.Trim();
                            rowNguoiUyQuyen["colSMSDefaultAcctno"] = SMSDefaultAcctno.Trim();
                            rowNguoiUyQuyen["colSMSDefaultLang"] = SMSDefaultLang.Trim();
                            rowNguoiUyQuyen["colSMSIsDefault"] = isDefault;
                            rowNguoiUyQuyen["colSMSPinCode"] = SMSPinCode;
                            rowNguoiUyQuyen["colMBPinCode"] = MBPinCode;
                            rowNguoiUyQuyen["colMBPhone"] = MBPhone.Trim();
                            rowNguoiUyQuyen["colMBPass"] = MBPass;
                            rowNguoiUyQuyen["colRoleType"] = SmartPortal.Constant.IPC.EAM;
                            rowNguoiUyQuyen["colWLPinCode"] = WLPinCode;
                            rowNguoiUyQuyen["colWLPhone"] = WLPhone.Trim();
                            rowNguoiUyQuyen["colWLPass"] = WLPass;
                            rowNguoiUyQuyen["colAccount"] = Account;
                            rowNguoiUyQuyen["colRole"] = nodeRoleIBNguoiUyQuyen.Text;
                            rowNguoiUyQuyen["colRoleID"] = nodeRoleIBNguoiUyQuyen.Value;
                            rowNguoiUyQuyen["colTranCode"] = nodeTrancodeIBNguoiUyQuyen.Text;
                            rowNguoiUyQuyen["colTranCodeID"] = nodeTrancodeIBNguoiUyQuyen.Value;
                            rowNguoiUyQuyen["colServiceID"] = SmartPortal.Constant.IPC.AM;
                            switch (sessionName)
                            {
                                case "CHUTAIKHOAN":
                                    rowNguoiUyQuyen["colIBPolicy"] = ddlpolicyIB.SelectedValue.ToString();
                                    rowNguoiUyQuyen["colSMSPolicy"] = ddlpolicySMS.SelectedValue.ToString();
                                    rowNguoiUyQuyen["colMBPolicy"] = ddlpolicyMB.SelectedValue.ToString();
                                    rowNguoiUyQuyen["colWLPolicy"] = ddlpolicyMB.SelectedValue.ToString();
                                    break;
                                case "NGUOIUYQUYEN":
                                    rowNguoiUyQuyen["colIBPolicy"] = ddlpolicyIBco.SelectedValue.ToString();
                                    rowNguoiUyQuyen["colSMSPolicy"] = ddlpolicySMSco.SelectedValue.ToString();
                                    rowNguoiUyQuyen["colMBPolicy"] = ddlpolicyMBco.SelectedValue.ToString();
                                    rowNguoiUyQuyen["colWLPolicy"] = ddlpolicyWLco.SelectedValue.ToString();
                                    break;
                            }
                            rowNguoiUyQuyen["colpwdreset"] = pwdreset;
                            tblNguoiUyQuyen.Rows.Add(rowNguoiUyQuyen);

                        }
                    }
                    else
                    {
                    }
                }
            }
            #endregion

            ViewState[sessionName] = tblNguoiUyQuyen;
            //gvResult.DataSource = tblNguoiUyQuyen.DefaultView.ToTable(true, "colFullName", "colIBUserName", "colAccount", "colRole");
            gvResult.DataSource = tblNguoiUyQuyen.DefaultView.ToTable(true, "colFullName", "colPhone", "colAccount", "colRole");
            gvResult.DataBind();

        }
        else
        {
            DataTable tblNguoiUyQuyen = (DataTable)ViewState[sessionName];
            ////minh add 26/8/2015 de fix truong hop ban dau ko co email+sms , sau do nhap them email

            if (tblNguoiUyQuyen.Rows.Count != 0)
                foreach (DataRow r in tblNguoiUyQuyen.Rows)
                {
                    if (r["colEmail"].ToString() != email)
                    {
                        r["colEmail"] = email;
                    }
                }

            #region them giao dich IB truong hop khac NULL
            //IB
            if (IBUserName != "" && WLPhone == "")
            {
                foreach (TreeNode nodeRoleIBNguoiUyQuyen in tvRole.Nodes)
                {
                    if (nodeRoleIBNguoiUyQuyen.Checked)
                    {
                        foreach (TreeNode nodeTrancodeIBNguoiUyQuyen in nodeRoleIBNguoiUyQuyen.ChildNodes)
                        {
                            //if (nodeTrancodeIBNguoiUyQuyen.Checked)
                            //{
                            if (tblNguoiUyQuyen.Select("colFullName='" + fullName.Replace("'", "''") + "' and colRoleID='" + nodeRoleIBNguoiUyQuyen.Value + "' and colAccount='" + Account + "' and colTranCode='" + nodeTrancodeIBNguoiUyQuyen.Text + "'").Length == 0)
                            {
                                #region them quyen IB
                                DataRow rowNguoiUyQuyen = tblNguoiUyQuyen.NewRow();
                                rowNguoiUyQuyen["colFullName"] = fullName.Trim();
                                rowNguoiUyQuyen["colLocalname"] = localFullName.Trim();
                                rowNguoiUyQuyen["colLoginMethod"] = loginMethod.Trim();
                                rowNguoiUyQuyen["colAuthenType"] = authenType.Trim();
                                rowNguoiUyQuyen["colLevel"] = level.Trim();
                                rowNguoiUyQuyen["colBirthday"] = birthday.Trim();
                                rowNguoiUyQuyen["colGender"] = gender.Trim();
                                rowNguoiUyQuyen["colPhone"] = phone.Trim();
                                rowNguoiUyQuyen["colEmail"] = email.Trim();
                                rowNguoiUyQuyen["colAddress"] = address.Trim();
                                rowNguoiUyQuyen["colIBUserName"] = IBUserName.Trim();
                                rowNguoiUyQuyen["colIBPass"] = IBPass;
                                rowNguoiUyQuyen["colSMSPhone"] = SMSPhone.Trim();
                                rowNguoiUyQuyen["colSMSDefaultAcctno"] = SMSDefaultAcctno.Trim();
                                rowNguoiUyQuyen["colSMSDefaultLang"] = SMSDefaultLang.Trim();
                                rowNguoiUyQuyen["colSMSIsDefault"] = isDefault;
                                rowNguoiUyQuyen["colSMSPinCode"] = SMSPinCode;
                                rowNguoiUyQuyen["colMBPinCode"] = MBPinCode;
                                rowNguoiUyQuyen["colMBPhone"] = MBPhone.Trim();
                                rowNguoiUyQuyen["colMBPass"] = MBPass;
                                rowNguoiUyQuyen["colRoleType"] = string.Empty;
                                rowNguoiUyQuyen["colWLPinCode"] = WLPinCode;
                                rowNguoiUyQuyen["colWLPhone"] = WLPhone.Trim();
                                rowNguoiUyQuyen["colWLPass"] = WLPass;
                                rowNguoiUyQuyen["colAccount"] = Account;
                                rowNguoiUyQuyen["colRole"] = nodeRoleIBNguoiUyQuyen.Text;
                                rowNguoiUyQuyen["colRoleID"] = nodeRoleIBNguoiUyQuyen.Value;
                                rowNguoiUyQuyen["colTranCode"] = nodeTrancodeIBNguoiUyQuyen.Text;
                                rowNguoiUyQuyen["colTranCodeID"] = nodeTrancodeIBNguoiUyQuyen.Value;
                                rowNguoiUyQuyen["colServiceID"] = SmartPortal.Constant.IPC.IB;
                                switch (sessionName)
                                {
                                    case "CHUTAIKHOAN":
                                        rowNguoiUyQuyen["colIBPolicy"] = ddlpolicyIB.SelectedValue.ToString();
                                        rowNguoiUyQuyen["colSMSPolicy"] = ddlpolicySMS.SelectedValue.ToString();
                                        rowNguoiUyQuyen["colMBPolicy"] = ddlpolicyMB.SelectedValue.ToString();
                                        rowNguoiUyQuyen["colWLPolicy"] = ddlpolicyMB.SelectedValue.ToString();
                                        break;
                                    case "NGUOIUYQUYEN":
                                        rowNguoiUyQuyen["colIBPolicy"] = ddlpolicyIBco.SelectedValue.ToString();
                                        rowNguoiUyQuyen["colSMSPolicy"] = ddlpolicySMSco.SelectedValue.ToString();
                                        rowNguoiUyQuyen["colMBPolicy"] = ddlpolicyMBco.SelectedValue.ToString();
                                        rowNguoiUyQuyen["colWLPolicy"] = ddlpolicyWLco.SelectedValue.ToString();
                                        break;
                                }
                                rowNguoiUyQuyen["colpwdreset"] = pwdreset;
                                tblNguoiUyQuyen.Rows.Add(rowNguoiUyQuyen);
                                #endregion
                            }

                            //}
                            //else
                            //{
                            //    if (tblNguoiUyQuyen.Select("colFullName='" + fullName + "' and colRoleID='" + nodeRoleIBNguoiUyQuyen.Value + "' and colAccount='" + Account + "' and colTranCode='" + nodeTrancodeIBNguoiUyQuyen.Text + "'").Length != 0)
                            //    {
                            //        foreach (DataRow r in tblNguoiUyQuyen.Select("colFullName='" + fullName + "' and colRoleID='" + nodeRoleIBNguoiUyQuyen.Value + "' and colAccount='" + Account + "' and colTranCode='" + nodeTrancodeIBNguoiUyQuyen.Text + "'"))
                            //        {
                            //            tblNguoiUyQuyen.Rows.Remove(r);
                            //        }
                            //    }
                            //}
                        }
                    }
                    else
                    {
                        foreach (TreeNode nodeTrancodeIBNguoiUyQuyen in nodeRoleIBNguoiUyQuyen.ChildNodes)
                        {
                            if (tblNguoiUyQuyen.Select("colFullName='" + fullName.Replace("'", "''") + "' and colRoleID='" + nodeRoleIBNguoiUyQuyen.Value + "' and colAccount='" + Account + "' and colTranCode='" + nodeTrancodeIBNguoiUyQuyen.Text + "'").Length == 0)
                            {

                            }
                            else
                            {
                                if (tblNguoiUyQuyen.Select("colFullName='" + fullName.Replace("'", "''") + "' and colRoleID='" + nodeRoleIBNguoiUyQuyen.Value + "' and colAccount='" + Account + "' and colTranCode='" + nodeTrancodeIBNguoiUyQuyen.Text + "'").Length != 0)
                                {
                                    foreach (DataRow r in tblNguoiUyQuyen.Select("colFullName='" + fullName.Replace("'", "''") + "' and colRoleID='" + nodeRoleIBNguoiUyQuyen.Value + "' and colAccount='" + Account + "' and colTranCode='" + nodeTrancodeIBNguoiUyQuyen.Text + "'"))
                                    {
                                        tblNguoiUyQuyen.Rows.Remove(r);
                                    }
                                }
                            }
                        }
                    }
                }
            }


            #endregion

            #region them giao dich SMS truong hop khac NULL
            //SMS
            if (SMSPhone != "")
            {
                foreach (TreeNode nodeRoleIBNguoiUyQuyen in tvSMSRole.Nodes)
                {
                    if (nodeRoleIBNguoiUyQuyen.Checked)
                    {
                        foreach (TreeNode nodeTrancodeIBNguoiUyQuyen in nodeRoleIBNguoiUyQuyen.ChildNodes)
                        {
                            //if (nodeTrancodeIBNguoiUyQuyen.Checked)
                            //{
                            if (tblNguoiUyQuyen.Select("colFullName='" + fullName.Replace("'", "''") + "' and colAccount='" + Account + "' and colRoleID='" + nodeRoleIBNguoiUyQuyen.Value + "' and colTranCode='" + nodeTrancodeIBNguoiUyQuyen.Text + "'").Length == 0)
                            {
                                #region chi tiet
                                DataRow rowNguoiUyQuyen = tblNguoiUyQuyen.NewRow();
                                rowNguoiUyQuyen["colFullName"] = fullName.Trim();
                                rowNguoiUyQuyen["colLocalname"] = localFullName.Trim();
                                rowNguoiUyQuyen["colLoginMethod"] = loginMethod.Trim();
                                rowNguoiUyQuyen["colAuthenType"] = authenType.Trim();
                                rowNguoiUyQuyen["colLevel"] = level.Trim();
                                rowNguoiUyQuyen["colBirthday"] = birthday.Trim();
                                rowNguoiUyQuyen["colGender"] = gender.Trim();
                                rowNguoiUyQuyen["colPhone"] = phone.Trim();
                                rowNguoiUyQuyen["colEmail"] = email.Trim();
                                rowNguoiUyQuyen["colAddress"] = address.Trim();
                                rowNguoiUyQuyen["colIBUserName"] = IBUserName.Trim();
                                rowNguoiUyQuyen["colIBPass"] = IBPass;
                                rowNguoiUyQuyen["colSMSPhone"] = SMSPhone.Trim();
                                rowNguoiUyQuyen["colSMSDefaultAcctno"] = SMSDefaultAcctno.Trim();
                                rowNguoiUyQuyen["colSMSDefaultLang"] = SMSDefaultLang.Trim();
                                rowNguoiUyQuyen["colSMSIsDefault"] = isDefault;
                                rowNguoiUyQuyen["colSMSPinCode"] = SMSPinCode;
                                rowNguoiUyQuyen["colMBPinCode"] = MBPinCode;
                                rowNguoiUyQuyen["colMBPhone"] = MBPhone.Trim();
                                rowNguoiUyQuyen["colMBPass"] = MBPass;
                                rowNguoiUyQuyen["colRoleType"] = string.Empty;
                                rowNguoiUyQuyen["colWLPinCode"] = WLPinCode;
                                rowNguoiUyQuyen["colWLPhone"] = WLPhone.Trim();
                                rowNguoiUyQuyen["colWLPass"] = WLPass;
                                rowNguoiUyQuyen["colAccount"] = Account;
                                rowNguoiUyQuyen["colRole"] = nodeRoleIBNguoiUyQuyen.Text;
                                rowNguoiUyQuyen["colRoleID"] = nodeRoleIBNguoiUyQuyen.Value;
                                rowNguoiUyQuyen["colTranCode"] = nodeTrancodeIBNguoiUyQuyen.Text;
                                rowNguoiUyQuyen["colTranCodeID"] = nodeTrancodeIBNguoiUyQuyen.Value;
                                rowNguoiUyQuyen["colServiceID"] = SmartPortal.Constant.IPC.SMS;
                                switch (sessionName)
                                {
                                    case "CHUTAIKHOAN":
                                        rowNguoiUyQuyen["colIBPolicy"] = ddlpolicyIB.SelectedValue.ToString();
                                        rowNguoiUyQuyen["colSMSPolicy"] = ddlpolicySMS.SelectedValue.ToString();
                                        rowNguoiUyQuyen["colMBPolicy"] = ddlpolicyMB.SelectedValue.ToString();
                                        rowNguoiUyQuyen["colWLPolicy"] = ddlpolicyMB.SelectedValue.ToString();
                                        break;
                                    case "NGUOIUYQUYEN":
                                        rowNguoiUyQuyen["colIBPolicy"] = ddlpolicyIBco.SelectedValue.ToString();
                                        rowNguoiUyQuyen["colSMSPolicy"] = ddlpolicySMSco.SelectedValue.ToString();
                                        rowNguoiUyQuyen["colMBPolicy"] = ddlpolicyMBco.SelectedValue.ToString();
                                        rowNguoiUyQuyen["colWLPolicy"] = ddlpolicyWLco.SelectedValue.ToString();
                                        break;
                                }

                                rowNguoiUyQuyen["colpwdreset"] = pwdreset;
                                tblNguoiUyQuyen.Rows.Add(rowNguoiUyQuyen);
                                #endregion
                            }

                            //}
                            //else
                            //{
                            //    if (tblNguoiUyQuyen.Select("colFullName='" + fullName + "' and colAccount='" + Account + "' and colRoleID='" + nodeRoleIBNguoiUyQuyen.Value + "' and colTranCode='" + nodeTrancodeIBNguoiUyQuyen.Text + "'").Length != 0)
                            //    {
                            //        foreach (DataRow r in tblNguoiUyQuyen.Select("colFullName='" + fullName + "' and colRoleID='" + nodeRoleIBNguoiUyQuyen.Value + "' and colAccount='" + Account + "' and colTranCode='" + nodeTrancodeIBNguoiUyQuyen.Text + "'"))
                            //        {
                            //            tblNguoiUyQuyen.Rows.Remove(r);
                            //        }
                            //    }
                            //}
                        }
                    }
                    else
                    {
                        foreach (TreeNode nodeTrancodeIBNguoiUyQuyen in nodeRoleIBNguoiUyQuyen.ChildNodes)
                        {
                            //if (nodeTrancodeIBNguoiUyQuyen.Checked)
                            //{
                            if (tblNguoiUyQuyen.Select("colFullName='" + fullName.Replace("'", "''") + "' and colAccount='" + Account + "' and colRoleID='" + nodeRoleIBNguoiUyQuyen.Value + "' and colTranCode='" + nodeTrancodeIBNguoiUyQuyen.Text + "'").Length == 0)
                            {

                            }

                            //}
                            else
                            {
                                if (tblNguoiUyQuyen.Select("colFullName='" + fullName.Replace("'", "''") + "' and colAccount='" + Account + "' and colRoleID='" + nodeRoleIBNguoiUyQuyen.Value + "' and colTranCode='" + nodeTrancodeIBNguoiUyQuyen.Text + "'").Length != 0)
                                {
                                    foreach (DataRow r in tblNguoiUyQuyen.Select("colFullName='" + fullName.Replace("'", "''") + "' and colRoleID='" + nodeRoleIBNguoiUyQuyen.Value + "' and colAccount='" + Account + "' and colTranCode='" + nodeTrancodeIBNguoiUyQuyen.Text + "'"))
                                    {
                                        tblNguoiUyQuyen.Rows.Remove(r);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            #endregion

            #region them giao dich MB truong hop khac NULL
            //MB
            if (MBPhone != "")
            {
                foreach (TreeNode nodeRoleIBNguoiUyQuyen in tvMBRole.Nodes)
                {
                    if (nodeRoleIBNguoiUyQuyen.Checked)
                    {
                        foreach (TreeNode nodeTrancodeIBNguoiUyQuyen in nodeRoleIBNguoiUyQuyen.ChildNodes)
                        {
                            //if (nodeTrancodeIBNguoiUyQuyen.Checked)
                            //{
                            if (tblNguoiUyQuyen.Select("colFullName='" + fullName.Replace("'", "''") + "' and colAccount='" + Account + "' and colRoleID='" + nodeRoleIBNguoiUyQuyen.Value + "' and colTranCode='" + nodeTrancodeIBNguoiUyQuyen.Text + "'").Length == 0)
                            {
                                #region chi tiet
                                DataRow rowNguoiUyQuyen = tblNguoiUyQuyen.NewRow();
                                rowNguoiUyQuyen["colFullName"] = fullName.Trim();
                                rowNguoiUyQuyen["colLocalname"] = localFullName.Trim();
                                rowNguoiUyQuyen["colLoginMethod"] = loginMethod.Trim();
                                rowNguoiUyQuyen["colAuthenType"] = authenType.Trim();
                                rowNguoiUyQuyen["colLevel"] = level.Trim();
                                rowNguoiUyQuyen["colBirthday"] = birthday.Trim();
                                rowNguoiUyQuyen["colGender"] = gender.Trim();
                                rowNguoiUyQuyen["colPhone"] = phone.Trim();
                                rowNguoiUyQuyen["colEmail"] = email.Trim();
                                rowNguoiUyQuyen["colAddress"] = address.Trim();
                                rowNguoiUyQuyen["colIBUserName"] = IBUserName.Trim();
                                rowNguoiUyQuyen["colIBPass"] = IBPass;
                                rowNguoiUyQuyen["colSMSPhone"] = SMSPhone.Trim();
                                rowNguoiUyQuyen["colSMSDefaultAcctno"] = SMSDefaultAcctno.Trim();
                                rowNguoiUyQuyen["colSMSDefaultLang"] = SMSDefaultLang.Trim();
                                rowNguoiUyQuyen["colSMSIsDefault"] = isDefault;
                                rowNguoiUyQuyen["colSMSPinCode"] = SMSPinCode;
                                rowNguoiUyQuyen["colMBPinCode"] = MBPinCode;
                                rowNguoiUyQuyen["colMBPhone"] = MBPhone.Trim();
                                rowNguoiUyQuyen["colMBPass"] = MBPass;
                                rowNguoiUyQuyen["colRoleType"] = SmartPortal.Constant.IPC.MBA;
                                rowNguoiUyQuyen["colWLPinCode"] = WLPinCode;
                                rowNguoiUyQuyen["colWLPhone"] = WLPhone.Trim();
                                rowNguoiUyQuyen["colWLPass"] = WLPass;
                                rowNguoiUyQuyen["colAccount"] = Account;
                                rowNguoiUyQuyen["colRole"] = nodeRoleIBNguoiUyQuyen.Text;
                                rowNguoiUyQuyen["colRoleID"] = nodeRoleIBNguoiUyQuyen.Value;
                                rowNguoiUyQuyen["colTranCode"] = nodeTrancodeIBNguoiUyQuyen.Text;
                                rowNguoiUyQuyen["colTranCodeID"] = nodeTrancodeIBNguoiUyQuyen.Value;
                                rowNguoiUyQuyen["colServiceID"] = SmartPortal.Constant.IPC.AM;
                                switch (sessionName)
                                {
                                    case "CHUTAIKHOAN":
                                        rowNguoiUyQuyen["colIBPolicy"] = ddlpolicyIB.SelectedValue.ToString();
                                        rowNguoiUyQuyen["colSMSPolicy"] = ddlpolicySMS.SelectedValue.ToString();
                                        rowNguoiUyQuyen["colMBPolicy"] = ddlpolicyMB.SelectedValue.ToString();
                                        rowNguoiUyQuyen["colWLPolicy"] = ddlpolicyMB.SelectedValue.ToString();
                                        break;
                                    case "NGUOIUYQUYEN":
                                        rowNguoiUyQuyen["colIBPolicy"] = ddlpolicyIBco.SelectedValue.ToString();
                                        rowNguoiUyQuyen["colSMSPolicy"] = ddlpolicySMSco.SelectedValue.ToString();
                                        rowNguoiUyQuyen["colMBPolicy"] = ddlpolicyMBco.SelectedValue.ToString();
                                        rowNguoiUyQuyen["colWLPolicy"] = ddlpolicyWLco.SelectedValue.ToString();
                                        break;
                                }

                                rowNguoiUyQuyen["colpwdreset"] = pwdreset;
                                tblNguoiUyQuyen.Rows.Add(rowNguoiUyQuyen);
                                #endregion
                            }

                            //}
                            //else
                            //{
                            //    if (tblNguoiUyQuyen.Select("colFullName='" + fullName + "' and colAccount='" + Account + "' and colRoleID='" + nodeRoleIBNguoiUyQuyen.Value + "' and colTranCode='" + nodeTrancodeIBNguoiUyQuyen.Text + "'").Length != 0)
                            //    {
                            //        foreach (DataRow r in tblNguoiUyQuyen.Select("colFullName='" + fullName + "' and colRoleID='" + nodeRoleIBNguoiUyQuyen.Value + "' and colAccount='" + Account + "' and colTranCode='" + nodeTrancodeIBNguoiUyQuyen.Text + "'"))
                            //        {
                            //            tblNguoiUyQuyen.Rows.Remove(r);
                            //        }
                            //    }
                            //}
                        }
                    }
                    else
                    {
                        foreach (TreeNode nodeTrancodeIBNguoiUyQuyen in nodeRoleIBNguoiUyQuyen.ChildNodes)
                        {
                            //if (nodeTrancodeIBNguoiUyQuyen.Checked)
                            //{
                            if (tblNguoiUyQuyen.Select("colFullName='" + fullName.Replace("'", "''") + "' and colAccount='" + Account + "' and colRoleID='" + nodeRoleIBNguoiUyQuyen.Value + "' and colTranCode='" + nodeTrancodeIBNguoiUyQuyen.Text + "'").Length == 0)
                            {

                            }

                            //}
                            else
                            {
                                if (tblNguoiUyQuyen.Select("colFullName='" + fullName.Replace("'", "''") + "' and colAccount='" + Account + "' and colRoleID='" + nodeRoleIBNguoiUyQuyen.Value + "' and colTranCode='" + nodeTrancodeIBNguoiUyQuyen.Text + "'").Length != 0)
                                {
                                    foreach (DataRow r in tblNguoiUyQuyen.Select("colFullName='" + fullName.Replace("'", "''") + "' and colRoleID='" + nodeRoleIBNguoiUyQuyen.Value + "' and colAccount='" + Account + "' and colTranCode='" + nodeTrancodeIBNguoiUyQuyen.Text + "'"))
                                    {
                                        tblNguoiUyQuyen.Rows.Remove(r);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            #endregion

            #region them giao dich Wl truong hop khac NULL
            //MB
            if (WLPhone != "")
            {
                foreach (TreeNode nodeRoleWLNguoiUyQuyen in tvWLRole.Nodes)
                {
                    if (nodeRoleWLNguoiUyQuyen.Checked)
                    {
                        foreach (TreeNode nodeTrancodeIBNguoiUyQuyen in nodeRoleWLNguoiUyQuyen.ChildNodes)
                        {
                            //if (nodeTrancodeIBNguoiUyQuyen.Checked)
                            //{
                            if (tblNguoiUyQuyen.Select("colFullName='" + fullName.Replace("'", "''") + "' and colAccount='" + Account + "' and colRoleID='" + nodeRoleWLNguoiUyQuyen.Value + "' and colTranCode='" + nodeTrancodeIBNguoiUyQuyen.Text + "'").Length == 0)
                            {
                                #region chi tiet
                                DataRow rowNguoiUyQuyen = tblNguoiUyQuyen.NewRow();
                                rowNguoiUyQuyen["colFullName"] = fullName.Trim();
                                rowNguoiUyQuyen["colLocalname"] = localFullName.Trim();
                                rowNguoiUyQuyen["colLoginMethod"] = loginMethod.Trim();
                                rowNguoiUyQuyen["colAuthenType"] = authenType.Trim();
                                rowNguoiUyQuyen["colLevel"] = level.Trim();
                                rowNguoiUyQuyen["colBirthday"] = birthday.Trim();
                                rowNguoiUyQuyen["colGender"] = gender.Trim();
                                rowNguoiUyQuyen["colPhone"] = phone.Trim();
                                rowNguoiUyQuyen["colEmail"] = email.Trim();
                                rowNguoiUyQuyen["colAddress"] = address.Trim();
                                rowNguoiUyQuyen["colIBUserName"] = IBUserName.Trim();
                                rowNguoiUyQuyen["colIBPass"] = IBPass;
                                rowNguoiUyQuyen["colSMSPhone"] = SMSPhone.Trim();
                                rowNguoiUyQuyen["colSMSDefaultAcctno"] = SMSDefaultAcctno.Trim();
                                rowNguoiUyQuyen["colSMSDefaultLang"] = SMSDefaultLang.Trim();
                                rowNguoiUyQuyen["colSMSIsDefault"] = isDefault;
                                rowNguoiUyQuyen["colSMSPinCode"] = SMSPinCode;
                                rowNguoiUyQuyen["colMBPinCode"] = MBPinCode;
                                rowNguoiUyQuyen["colMBPhone"] = MBPhone.Trim();
                                rowNguoiUyQuyen["colMBPass"] = MBPass;
                                rowNguoiUyQuyen["colRoleType"] = SmartPortal.Constant.IPC.EAM;
                                rowNguoiUyQuyen["colWLPinCode"] = WLPinCode;
                                rowNguoiUyQuyen["colWLPhone"] = WLPhone.Trim();
                                rowNguoiUyQuyen["colWLPass"] = WLPass;
                                rowNguoiUyQuyen["colAccount"] = Account;
                                rowNguoiUyQuyen["colRole"] = nodeRoleWLNguoiUyQuyen.Text;
                                rowNguoiUyQuyen["colRoleID"] = nodeRoleWLNguoiUyQuyen.Value;
                                rowNguoiUyQuyen["colTranCode"] = nodeTrancodeIBNguoiUyQuyen.Text;
                                rowNguoiUyQuyen["colTranCodeID"] = nodeTrancodeIBNguoiUyQuyen.Value;
                                rowNguoiUyQuyen["colServiceID"] = SmartPortal.Constant.IPC.AM;
                                switch (sessionName)
                                {
                                    case "CHUTAIKHOAN":
                                        rowNguoiUyQuyen["colIBPolicy"] = ddlpolicyIB.SelectedValue.ToString();
                                        rowNguoiUyQuyen["colSMSPolicy"] = ddlpolicySMS.SelectedValue.ToString();
                                        rowNguoiUyQuyen["colMBPolicy"] = ddlpolicyMB.SelectedValue.ToString();
                                        rowNguoiUyQuyen["colWLPolicy"] = ddlpolicyMB.SelectedValue.ToString();
                                        break;
                                    case "NGUOIUYQUYEN":
                                        rowNguoiUyQuyen["colIBPolicy"] = ddlpolicyIBco.SelectedValue.ToString();
                                        rowNguoiUyQuyen["colSMSPolicy"] = ddlpolicySMSco.SelectedValue.ToString();
                                        rowNguoiUyQuyen["colMBPolicy"] = ddlpolicyMBco.SelectedValue.ToString();
                                        rowNguoiUyQuyen["colWLPolicy"] = ddlpolicyWLco.SelectedValue.ToString();
                                        break;
                                }

                                rowNguoiUyQuyen["colpwdreset"] = pwdreset;
                                tblNguoiUyQuyen.Rows.Add(rowNguoiUyQuyen);
                                #endregion
                            }

                        }
                    }
                    else
                    {
                        foreach (TreeNode nodeTrancodeIBNguoiUyQuyen in nodeRoleWLNguoiUyQuyen.ChildNodes)
                        {
                            //if (nodeTrancodeIBNguoiUyQuyen.Checked)
                            //{
                            if (tblNguoiUyQuyen.Select("colFullName='" + fullName.Replace("'", "''") + "' and colAccount='" + Account + "' and colRoleID='" + nodeRoleWLNguoiUyQuyen.Value + "' and colTranCode='" + nodeTrancodeIBNguoiUyQuyen.Text + "'").Length == 0)
                            {

                            }

                            //}
                            else
                            {
                                if (tblNguoiUyQuyen.Select("colFullName='" + fullName.Replace("'", "''") + "' and colAccount='" + Account + "' and colRoleID='" + nodeRoleWLNguoiUyQuyen.Value + "' and colTranCode='" + nodeTrancodeIBNguoiUyQuyen.Text + "'").Length != 0)
                                {
                                    foreach (DataRow r in tblNguoiUyQuyen.Select("colFullName='" + fullName.Replace("'", "''") + "' and colRoleID='" + nodeRoleWLNguoiUyQuyen.Value + "' and colAccount='" + Account + "' and colTranCode='" + nodeTrancodeIBNguoiUyQuyen.Text + "'"))
                                    {
                                        tblNguoiUyQuyen.Rows.Remove(r);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            #endregion

            ViewState[sessionName] = tblNguoiUyQuyen;
            // DataTable newTable=SmartPortal.Common.Utilities.Utility.
            gvResult.DataSource = tblNguoiUyQuyen.DefaultView.ToTable(true, "colFullName", "colPhone", "colAccount", "colRole");
            gvResult.DataBind();
        }
    }
    protected void txtPhoneNguoiUyQuyen_TextChanged(object sender, EventArgs e)
    {

        if (!CheckIsPhoneNumer(txtPhoneNguoiUyQuyen.Text))
        {
            lblError.Text = Resources.labels.phonenumberwrong + " for co-owner";
            return;
        }
        if (!CheckExistPhoneNumber(txtPhoneNguoiUyQuyen.Text))
        {
            lblError.Text = Resources.labels.phonenumberisalreadyregistered;
            return;
        }
        else
        {
            PHONENGUOIUYQUYEN = txtPhoneNguoiUyQuyen.Text;

            txtSMSPhoneNguoiUyQuyen.Text = PHONENGUOIUYQUYEN;
        }
    }
    protected void btnThemNguoiUyQuyen_Click(object sender, EventArgs e)
    {
        try
        {
            int passlenIB = 0;
            int passlenSMS = 0;
            int passlenMB = 0;
            int passlenWL = 0;
            #region Tao bang chua cac thong tin nguoi uy quyen
            string PassTemp = "";
            if (rbMBGenerateNguoiUyQuyen.Checked)
            {
                USERUYQUYEN = usernameAuthorized = txtMBPhoneNguoiUyQuyen.Text.Trim();
            }
            else if (rbMBTypeNguoiUyQuyen.Checked)
            {
                USERUYQUYEN = usernameAuthorized = txtMBGenUserNameNguoiUyQuyen.Text.Trim();
                #region check usernameAuthorized

                if (usernameAuthorized == string.Empty)
                {
                    lblAlertDSH.Text = Resources.labels.bancannhaptendangnhap;
                    return;
                }
                else if (usernameAuthorized.Length < minlength || usernameAuthorized.Length > maxlength)
                {
                    lblAlertDSH.Text = string.Format(Resources.labels.usernamemustbetween, minlength, maxlength);
                    return;
                }

                DataSet ds = new Customer().CheckUserName("EBA_Users_CheckUserName", new object[] { usernameAuthorized }, ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE != "0")
                {
                    lblAlertDSH.Text = IPCERRORDESC;
                    return;
                }
                if (!validateusername(txtMBPhoneNguoiUyQuyen))
                {
                    return;
                }
                #endregion
            }


            if (!CheckIsPhoneNumer(PHONENGUOIUYQUYEN))
            {
                lblAlertDSH.Text = "You need input phone number co-owner";
                return;
            }
            if (!CheckExistPhoneNumber(PHONENGUOIUYQUYEN))
            {
                lblAlertDSH.Text = Resources.labels.phonenumberisalreadyregistered;
                return;
            }

            if (usernameAuthorized == string.Empty || usernameAuthorized == null)
            {
                lblAlertDSH.Text = Resources.labels.bancannhaptendangnhap;
                return;
            }

            //minh add 13/5/2015 1 số điện thoại chỉ được dùng cho 1 tài khoản sms mặc định
            #region check phone number and default account

            string phoneNumber = txtSMSPhoneNguoiUyQuyen.Text.Trim();
            string defaultAcc = ddlSMSDefaultAcctnoUyQuyen.Text.Trim();
            DataTable dt1 = new Customer().CheckPhoneNumber(phoneNumber, defaultAcc);
            if (dt1.Rows.Count != 0)
            {
                lblAlertDSH.Text = Resources.labels.phonenumberassigned;
                return;
            }
            #endregion

            //27/8/2015 minh add to validate email if not empty
            string pattern = Resources.labels.emailpattern;

            if (!string.IsNullOrEmpty(txtEmailNguoiUyQuyen.Text.Trim()))
            {
                if (!(System.Text.RegularExpressions.Regex.IsMatch(txtEmailNguoiUyQuyen.Text, pattern)))
                {

                    ShowPopUpMsg(Resources.labels.emailkhongdinhdang1);
                    //lblAlertDSH.Text = Resources.labels.emailkhongdinhdang1;
                    txtEmailNguoiUyQuyen.Focus();
                    return;

                }
            }

            #region check sms notify vutt 30032016

            string errDesc = string.Empty;
            if (!string.IsNullOrEmpty(txtSMSPhoneNguoiUyQuyen.Text))
            {
                //if (!ContractControl.ValidateSMSNotifyCurrentUser(ddlProduct.SelectedValue, tvSMSUyQuyen, ref errDesc, radAllAccountNguoiUyQuyen.Checked ? "" : ddlAccountUyQuyen.SelectedValue, new List<DataTable> { (DataTable)ViewState["CHUTAIKHOAN"] }))
                //phongtt sms nitification fee
                if (!ContractControl.ValidateSMSNotifyCurrentUser(ddlProduct.SelectedValue, tvSMSUyQuyen, ref errDesc, radAllAccountNguoiUyQuyen.Checked ? "" : ddlAccountUyQuyen.SelectedValue, lsAccNo, new List<DataTable> { (DataTable)ViewState["CHUTAIKHOAN"] }))
                {
                    lblAlertDSH.Text = errDesc;
                    return;
                }
            }

            #endregion
            //21.3.2016 load infor about policy selected to get len of pass generate
            DataSet dspolicy = new DataSet();
            string filterIB = "serviceid='IB'";
            if (ddlpolicyIB.SelectedValue.ToString() != "")
            {
                filterIB += " and policyid='" + ddlpolicyIB.SelectedValue.ToString() + "'";
            }
            string filterSMS = "serviceid='SMS'  and policyid='" + ddlpolicySMSco.SelectedValue.ToString() + "'";
            string filterMB = "serviceid='MB' and policyid='" + ddlpolicyMBco.SelectedValue.ToString() + "'";
            string filterWL = "serviceid='" + SmartPortal.Constant.IPC.EW +
                "' and policyid='" + ddlpolicyWLco.SelectedValue.ToString() + "'";
            string stSort = "serviceid asc";

            dspolicy = new SmartPortal.SEMS.USERPOLICY().GetPolicybyCondition(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, Session["userName"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {

                DataTable dt = dspolicy.Tables[0];
                DataTable dtIB = dt.Select(filterIB, stSort).Any() ? dspolicy.Tables[0].Select(filterIB, stSort).CopyToDataTable() : null;
                DataTable dtSMS = dt.Select(filterSMS, stSort).Any() ? dspolicy.Tables[0].Select(filterSMS, stSort).CopyToDataTable() : null;
                DataTable dtMB = dt.Select(filterMB, stSort).Any() ? dspolicy.Tables[0].Select(filterMB, stSort).CopyToDataTable() : null;
                DataTable dtWL = dt.Select(filterWL, stSort).Any() ? dspolicy.Tables[0].Select(filterWL, stSort).CopyToDataTable() : null;
                passlenIB = dtIB == null ? int.Parse(ConfigurationManager.AppSettings["minpwdlen_default"].ToString()) : Convert.ToInt32(dtIB.Rows[0]["minpwdlen"].ToString());
                passlenSMS = dtSMS == null ? int.Parse(ConfigurationManager.AppSettings["minpwdlen_default"].ToString()) : Convert.ToInt32(dtSMS.Rows[0]["minpwdlen"].ToString());
                passlenMB = dtMB == null ? int.Parse(ConfigurationManager.AppSettings["minpwdlen_default"].ToString()) : Convert.ToInt32(dtMB.Rows[0]["minpwdlen"].ToString());
                passlenWL = dtWL == null ? int.Parse(ConfigurationManager.AppSettings["minpwdlen_default"].ToString()) : Convert.ToInt32(dtWL.Rows[0]["minpwdlen"].ToString());
            }


            //1.4.2016 fix userid khac phoneno 
            if (int.Parse(ConfigurationManager.AppSettings["MBWLSameUser"].ToString()) == 1)
            {
                //txtMBPhoneNo.Text = ownerUserName;
                //txtMBPhoneNguoiUyQuyen.Text = usernameAuthorized;

            }
            else
            {

            }


            if (ddlAccUyQuyen.SelectedValue != "ALL")
            {
                //PassTemp = Encryption.Encrypt(DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - 8, 8));
                // LuuThongTinQuyen("NGUOIUYQUYEN", gvResultNguoiUyQuyen, tvIBUyQuyen, tvSMSUyQuyen, tvMBUyQuyen, tvPHOUyQuyen, txtFullnameNguoiUyQuyen.Text, lblLevelNguoiUyQuyen.Text, txtBirthNguoiUyQuyen.Text, ddlGenderNguoiUyQuyen.SelectedValue, txtPhoneNguoiUyQuyen.Text, txtEmailNguoiUyQuyen.Text, txtAddressNguoiUyQuyen.Text, usernameAuthorized, PassTemp, txtSMSPhoneNguoiUyQuyen.Text, ddlSMSDefaultAcctnoUyQuyen.SelectedValue, ddlDefaultLanguageNguoiUyQuyen.SelectedValue, ((cbIsDefault.Checked == true) ? "Y" : "N"), DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - 8, 6), txtMBPhoneNguoiUyQuyen.Text, PassTemp, DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - 8, 6), txtPHOPhoneNguoiUyQuyen.Text, PassTemp, ddlPHODefaultACCTNO.SelectedValue, ddlAccountUyQuyen.SelectedValue);
                //24.2.2016 minh add encrypt pass new
                string passreveal = DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - passlenMB, passlenMB);
                string pwdreset = Encryption.Encrypt(passreveal);
                PassTemp = SmartPortal.SEMS.O9Encryptpass.sha_sha256(passreveal, usernameAuthorized);
                string pwdresetSMS = DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - passlenSMS, passlenSMS);
                LuuThongTinQuyen("NGUOIUYQUYEN", gvResultNguoiUyQuyen, tvIBUyQuyen, tvSMSUyQuyen, tvMBUyQuyen, tvWLUyQuyen, txtFullnameNguoiUyQuyen.Text, lblLevelNguoiUyQuyen.Text, txtBirthNguoiUyQuyen.Text, ddlGenderNguoiUyQuyen.SelectedValue, txtPhoneNguoiUyQuyen.Text, txtEmailNguoiUyQuyen.Text, txtAddressNguoiUyQuyen.Text, USERUYQUYEN, PassTemp, PHONENGUOIUYQUYEN, ddlSMSDefaultAcctnoUyQuyen.SelectedValue, ddlDefaultLanguageNguoiUyQuyen.SelectedValue, ((cbIsDefault.Checked == true) ? "Y" : "N"), pwdresetSMS, PHONENGUOIUYQUYEN, PassTemp, DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - 8, 6), SmartPortal.Constant.IPC.MBA, ddlAccUyQuyen.SelectedValue, pwdreset, DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - 8, 6), "", PassTemp, txtLocalFullName.Text.Trim(), ddlLoginMethod.SelectedValue.Trim(), ddlauthenType.SelectedValue.Trim());

            }
            if (ddlAccUyQuyen.SelectedValue == "ALL")
            {
                //lay tat ca tai khoan khach hang
                DataSet ds = new DataSet();
                switch (ddlCustType.SelectedValue.Trim())
                {
                    case SmartPortal.Constant.IPC.PERSONAL:
                        ds = new SmartPortal.SEMS.Customer().GetAcctNo(txtCustCodeInfo.Text.Trim(), ddlCustType.SelectedValue, ref IPCERRORCODE, ref IPCERRORDESC);
                        break;
                        //case SmartPortal.Constant.IPC.PERSONALLKG:
                        //    ds = new SmartPortal.SEMS.Customer().GetAcctNo(txtLkgCode.Text.Trim(), ddlCustType.SelectedValue, ref IPCERRORCODE, ref IPCERRORDESC);
                        //    break;
                        //case SmartPortal.Constant.IPC.CORPORATE:
                        //    ds = new SmartPortal.SEMS.Customer().GetAcctNo(txtGrpCode.Text.Trim(), ddlCustType.SelectedValue, ref IPCERRORCODE, ref IPCERRORDESC);
                        //    break;
                }
                if (IPCERRORCODE != "0")
                {
                    throw new IPCException(IPCERRORDESC);
                }
                DataTable dtAccount = new DataTable();
                dtAccount = ds.Tables[0];

                //luu tat ca account
                //PassTemp = Encryption.Encrypt(DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - 8, 8));
                //24.2.2016 minh add encrypt pass new
                string passreveal = DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - passlenMB, passlenMB);
                string pwdreset = Encryption.Encrypt(passreveal);
                PassTemp = SmartPortal.SEMS.O9Encryptpass.sha_sha256(passreveal, usernameAuthorized);
                string pwdresetSMS = DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - passlenSMS, passlenSMS);

                foreach (DataRow rowAccount in dtAccount.Rows)
                {
                    //LuuThongTinQuyen("NGUOIUYQUYEN", gvResultNguoiUyQuyen, tvIBUyQuyen, tvSMSUyQuyen, tvMBUyQuyen, tvPHOUyQuyen, txtFullnameNguoiUyQuyen.Text, lblLevelNguoiUyQuyen.Text, txtBirthNguoiUyQuyen.Text, ddlGenderNguoiUyQuyen.SelectedValue, txtPhoneNguoiUyQuyen.Text, txtEmailNguoiUyQuyen.Text, txtAddressNguoiUyQuyen.Text, usernameAuthorized, PassTemp, txtSMSPhoneNguoiUyQuyen.Text, ddlSMSDefaultAcctnoUyQuyen.SelectedValue, ddlDefaultLanguageNguoiUyQuyen.SelectedValue, ((cbIsDefault.Checked == true) ? "Y" : "N"), DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - 8, 6), txtMBPhoneNguoiUyQuyen.Text, PassTemp, DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - 8, 6), txtPHOPhoneNguoiUyQuyen.Text, PassTemp, ddlPHODefaultACCTNO.SelectedValue, rowAccount["ACCOUNTNO"].ToString());
                    LuuThongTinQuyen("NGUOIUYQUYEN", gvResultNguoiUyQuyen, tvIBUyQuyen, tvSMSUyQuyen, tvMBUyQuyen, tvWLUyQuyen, txtFullnameNguoiUyQuyen.Text, lblLevelNguoiUyQuyen.Text, txtBirthNguoiUyQuyen.Text, ddlGenderNguoiUyQuyen.SelectedValue, txtPhoneNguoiUyQuyen.Text, txtEmailNguoiUyQuyen.Text, txtAddressNguoiUyQuyen.Text, USERUYQUYEN, PassTemp, PHONENGUOIUYQUYEN, ddlSMSDefaultAcctnoUyQuyen.SelectedValue, ddlDefaultLanguageNguoiUyQuyen.SelectedValue, ((cbIsDefault.Checked == true) ? "Y" : "N"), pwdresetSMS, PHONENGUOIUYQUYEN, PassTemp, DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - 8, 6), SmartPortal.Constant.IPC.MBA, rowAccount["ACCOUNTNO"].ToString(), pwdreset, DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - 8, 6), "", PassTemp, txtLocalFullName.Text.Trim(), ddlLoginMethod.SelectedValue.Trim(), ddlauthenType.SelectedValue.Trim());
                }


            }
            if (TabCustomerInfoHelperCoOwner.TabWalletVisibility == 1)
            {
                string passreveal = DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - passlenMB, passlenMB);
                string pwdreset = Encryption.Encrypt(passreveal);
                PassTemp = SmartPortal.SEMS.O9Encryptpass.sha_sha256(passreveal, usernameAuthorized);
                string pwdresetSMS = DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - passlenSMS, passlenSMS);


                LuuThongTinQuyen("NGUOIUYQUYEN", gvResultNguoiUyQuyen, tvIBUyQuyen, tvSMSUyQuyen, tvMBUyQuyen, tvWLUyQuyen, txtFullnameNguoiUyQuyen.Text, lblLevelNguoiUyQuyen.Text, txtBirthNguoiUyQuyen.Text, ddlGenderNguoiUyQuyen.SelectedValue, txtPhoneNguoiUyQuyen.Text, txtEmailNguoiUyQuyen.Text, txtAddressNguoiUyQuyen.Text, USERUYQUYEN, PassTemp, "", ddlSMSDefaultAcctnoUyQuyen.SelectedValue, ddlDefaultLanguageNguoiUyQuyen.SelectedValue, ((cbIsDefault.Checked == true) ? "Y" : "N"), pwdresetSMS, "", PassTemp, DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - 8, 6), SmartPortal.Constant.IPC.EAM, PHONE, pwdreset, DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - 8, 6), PHONENGUOIUYQUYEN, PassTemp, txtLocalFullName.Text.Trim(), ddlLoginMethod.SelectedValue.Trim(), ddlauthenType.SelectedValue.Trim());

            }

            lblAlertDSH.Text = Resources.labels.recordsaved;
            #endregion

            //28/8/2015 minh fixed truong hop khong chon dich vu ma bam nut add
            if (gvResultNguoiUyQuyen.Rows.Count == 0)
            {
                lblAlertDSH.Text = Resources.labels.banchuadangkydichvu;
                return;
            }
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], "Widgets_SEMSCustomerList_Add_Widget", "btnThemNguoiUyQuyen_Click", IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);
        }

        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSCustomerList_Add_Widget", "btnThemNguoiUyQuyen_Click", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void gvResultNguoiUyQuyen_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvResultNguoiUyQuyen.PageIndex = e.NewPageIndex;
            gvResultNguoiUyQuyen.DataSource = (DataTable)ViewState["NGUOIUYQUYEN"];
            gvResultNguoiUyQuyen.DataBind();
        }
        catch
        {
        }
    }

    protected void gvResultNguoiUyQuyen_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            DataTable tblResultNguoiUyQuyen = (DataTable)ViewState["NGUOIUYQUYEN"];

            tblResultNguoiUyQuyen.Rows.RemoveAt(e.RowIndex + (gvResultNguoiUyQuyen.PageIndex * gvResultNguoiUyQuyen.PageSize));

            ViewState["NGUOIUYQUYEN"] = tblResultNguoiUyQuyen;
            gvResultNguoiUyQuyen.DataSource = tblResultNguoiUyQuyen;
            gvResultNguoiUyQuyen.DataBind();

            lblAlertDSH.Text = Resources.labels.recorddeleted;
        }
        catch
        {
        }
    }

    protected void btnHuy_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable tblResultChuTaiKhoan = (DataTable)ViewState["CHUTAIKHOAN"];

            tblResultChuTaiKhoan.Rows.Clear();

            ViewState["CHUTAIKHOAN"] = tblResultChuTaiKhoan;
            gvResultChuTaiKhoan.DataSource = tblResultChuTaiKhoan.DefaultView.ToTable(true, "colFullName", "colPhone", "colAccount", "colRole");
            gvResultChuTaiKhoan.DataBind();

            lblAlert.Text = Resources.labels.recorddeleted;
        }
        catch
        {
        }
    }
    //protected void radCustCodeWL_OnCheckedChanged(object sender, System.EventArgs e)
    //{
    //    if (radCustCodeWL.Checked == true)
    //    {
    //        txtPhoneWL.Text = string.Empty;
    //        btnSearchWL.Visible = true;
    //        btnSearch.Visible = false;
    //        txtPhoneWL.Visible = false;
    //        btnNext.Attributes.Add("onclick", "return validateWL();");

    //    }
    //    if (radCustCode.Checked == true|| radTaxCode.Checked==true|| radLkgCode.Checked==true|| radGrpCode.Checked==true)
    //    {
    //        txtPhoneWL.Text = string.Empty;
    //        txtPhoneWL.Visible = true;
    //        btnSearchWL.Visible = false;
    //        btnSearch.Visible = true;
    //        btnNext.Attributes.Add("onclick", "return validate();");
    //    }
    //}
    protected void btnHuyDSH_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable tblResultNguoiUyQuyen = (DataTable)ViewState["NGUOIUYQUYEN"];

            tblResultNguoiUyQuyen.Rows.Clear();

            ViewState["NGUOIUYQUYEN"] = tblResultNguoiUyQuyen;
            gvResultNguoiUyQuyen.DataSource = tblResultNguoiUyQuyen.DefaultView.ToTable(true, "colFullName", "colIBUserName", "colAccount", "colRole");
            gvResultNguoiUyQuyen.DataBind();

            lblAlertDSH.Text = Resources.labels.recorddeleted;
        }
        catch
        {
        }
    }
    protected void btnCoownerDetail_Click(object sender, EventArgs e)
    {
        try
        {

            lblError.Text = "";

            txtFullnameNguoiUyQuyen.Text = string.Empty;
            txtEmailNguoiUyQuyen.Text = string.Empty;
            txtPhoneNguoiUyQuyen.Text = string.Empty;
            txtMBPhoneNguoiUyQuyen.Text = string.Empty;
            ddlGenderNguoiUyQuyen.SelectedIndex = 0;
            txtBirthNguoiUyQuyen.Text = string.Empty;
            txtAddressNguoiUyQuyen.Text = string.Empty;
            string userInfo = string.Empty;
            Hashtable hasCoownerInfo = new Hashtable();
            string ctmType = SmartPortal.Constant.IPC.PERSONAL;
            hasCoownerInfo = new SmartPortal.SEMS.Customer().GetCustInfo(txtCoownerCode.Text.Trim(), ctmType, ref IPCERRORCODE, ref IPCERRORDESC);

            if (IPCERRORCODE != "0")
            {
                throw new IPCException(SmartPortal.Constant.IPC.ERRORCODE.CUSTNOTEXIST);
            }

            if (hasCoownerInfo[SmartPortal.Constant.IPC.CUSTCODE] == null)
            {
                ShowPopUpMsg(Resources.labels.makhachhangkhongtontaitronghethong);

                //throw new IPCException(SmartPortal.Constant.IPC.ERRORCODE.CUSTNOTEXIST);
                return;
            }

            if (hasCoownerInfo[SmartPortal.Constant.IPC.CUSTNAME] != null && hasCoownerInfo[SmartPortal.Constant.IPC.LICENSE] != null)
            {
                txtFullnameNguoiUyQuyen.Text = hasCoownerInfo[SmartPortal.Constant.IPC.CUSTNAME].ToString();
                USERUYQUYEN = txtMBPhoneNguoiUyQuyen.Text = txtIBGenUserNameNguoiUyQuyen.Text = SmartPortal.Common.Utilities.Utility.GetID(hasCoownerInfo[SmartPortal.Constant.IPC.CUSTNAME].ToString(), txtCoownerCode.Text.Trim(), hasCoownerInfo[SmartPortal.Constant.IPC.LICENSE].ToString(), txtUsernameIB.Text);
            }
            else
            {
                txtIBGenUserNameNguoiUyQuyen.Text = txtMBPhoneNguoiUyQuyen.Text = string.Empty;
                throw new IPCException(SmartPortal.Constant.IPC.ERRORCODE.NOTENOUGHINFO);
            }
            if (hasCoownerInfo[SmartPortal.Constant.IPC.EMAIL] != null)
                txtEmailNguoiUyQuyen.Text = hasCoownerInfo[SmartPortal.Constant.IPC.EMAIL].ToString();
            if (hasCoownerInfo[SmartPortal.Constant.IPC.PHONE] != null)
            {
                txtPhoneNguoiUyQuyen.Text = hasCoownerInfo[SmartPortal.Constant.IPC.PHONE].ToString();
            }

            if (hasCoownerInfo[SmartPortal.Constant.IPC.SEX] != null)
            {
                try
                {
                    if (hasCoownerInfo[SmartPortal.Constant.IPC.SEX].ToString().Trim() == "")
                    {
                        ddlGenderNguoiUyQuyen.Enabled = true;
                    }
                    else
                    {
                        ddlGenderNguoiUyQuyen.Enabled = false;
                        ddlGenderNguoiUyQuyen.SelectedValue = SmartPortal.Common.Utilities.Utility.FormatStringCore(int.Parse(hasCoownerInfo[SmartPortal.Constant.IPC.SEX].ToString()).ToString()) == "1" ? "M" : "F";
                    }
                }
                catch
                {
                }
            }
            if (hasCoownerInfo[SmartPortal.Constant.IPC.DOB] != null)
            {
                try
                {
                    string birthDate = SmartPortal.Common.Utilities.Utility.IsDateTime1(hasCoownerInfo[SmartPortal.Constant.IPC.DOB].ToString()).ToString("dd/MM/yyyy");
                    txtBirthNguoiUyQuyen.Text = birthDate.Equals("01/01/1900") ? "" : birthDate;

                    if (txtBirth.Text.Trim() == "")
                    {
                        txtBirthNguoiUyQuyen.Enabled = true;
                    }
                    else
                    {
                        txtBirthNguoiUyQuyen.Enabled = false;
                    }
                }
                catch
                {
                }
            }
            if (hasCoownerInfo[SmartPortal.Constant.IPC.ADDRESS] != null)
            {
                txtAddressNguoiUyQuyen.Text = hasCoownerInfo[SmartPortal.Constant.IPC.ADDRESS].ToString();
            }
            //Gan user cho MB User 
            string result = new Customer().CheckPhoneNumberCustInfo(txtPhoneNguoiUyQuyen.Text, SmartPortal.Constant.IPC.CONTRACTAGENTMERCHANT, ref IPCERRORCODE, ref IPCERRORDESC);
            if (result == "0")
            {
                PHONENGUOIUYQUYEN = txtPhoneNguoiUyQuyen.Text;
                //txtWLNguoiUyQuyen.Text = txtSMSPhoneNguoiUyQuyen.Text = txtMBPhoneNguoiUyQuyen.Text.Trim();
                //if (int.Parse(ConfigurationManager.AppSettings["MBWLSameUser"].ToString()) == 1)
                //{
                //    //txtMBPhoneNo.Text = ownerUserName;
                //    //txtWLNguoiUyQuyen.Text = userInfo;
                //    txtWLNguoiUyQuyen.Text = txtAMPhoneNo.Text;
                //}
                //else
                //{
                //    //txtMBPhoneNo.Text = SmartPortal.Common.Utilities.Utility.GetID("", txtCustCodeInfo.Text.Trim(), "", 10) + "1";
                //    //txtMBPhoneNguoiUyQuyen.Text = SmartPortal.Common.Utilities.Utility.GetID("", txtCustCodeInfo.Text.Trim(), "", 10) + "2";
                //}
            }
            else
            {
                lblError.Text = Resources.labels.phonenumberisalreadyregistered;
            }
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(IPCex.ToString(), this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(IPCex.Message, Request.Url.Query);

        }
        catch (Exception ex)
        {
        }
    }
    //28/8/2015 minh add to show message error trong truong hop su dung panel ajax
    private void ShowPopUpMsg(string msg)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("alert('");
        sb.Append(msg.Replace("\n", "\\n").Replace("\r", "").Replace("'", "\\'"));
        sb.Append("');");
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "showalert", sb.ToString(), true);
    }
    private bool validateusername(TextBox tx)
    {
        string usernamepattern = System.Configuration.ConfigurationManager.AppSettings["validateusername"].ToString();
        if (!(System.Text.RegularExpressions.Regex.IsMatch(tx.Text, usernamepattern)))
        {

            ShowPopUpMsg(Resources.labels.tendangnhapchichophepsovachulatin);

            tx.Focus();
            return false;

        }
        return true;
    }
    private bool validatedatetime(TextBox tx)
    {
        bool ret = false;
        try
        {
            if (string.IsNullOrEmpty(tx.Text))
                return true;
            //DateTime dt=DateTime.ParseExact("24/01/2013", "dd/MM/yyyy", CultureInfo.InvariantCulture);
            if (DateTime.ParseExact(tx.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).Year < int.Parse(ConfigurationManager.AppSettings["datetimelimityear"].ToString()))
            {
                tx.Focus();
                ret = false;
            }
            else
                ret = true;
        }
        catch (Exception ex)
        {
            ret = false;
        }



        return ret;
    }
    protected void ddlpolicyIB_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            ddlpolicyMB.SelectedValue = ddlpolicyMB.SelectedValue;
        }
        catch
        { }
    }
    protected void ddlpolicyWL_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            ddlpolicyMB.SelectedValue = ddlpolicyMB.SelectedValue;
        }
        catch
        { }
    }
    protected void ddlpolicyIBco_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlpolicyWLco.SelectedValue = ddlpolicyIBco.SelectedValue;
        }
        catch
        {
        }
    }
    protected void ddlpolicyMBco_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlpolicyWLco.SelectedValue = ddlpolicyMBco.SelectedValue;
        }
        catch
        {
        }
    }
    public bool CheckIsPhoneNumer(string phone)
    {
        string result = new Customer().CheckPhoneTeLCo(phone, ref IPCERRORCODE, ref IPCERRORDESC);
        if (result == SmartPortal.Constant.IPC.TRANSTATUS.BEGIN)
        {
            return true;
        }
        else
        {
            return false;
        }

    }
    public bool CheckExistPhoneNumber(string phone)
    {
        string resultInfo = new Customer().CheckPhoneNumberCustInfo(phone, SmartPortal.Constant.IPC.CONTRACTAGENTMERCHANT, ref IPCERRORCODE, ref IPCERRORDESC);
        if (resultInfo.Equals("1"))
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    //public void LoadProductByCombobox()
    //{
    //    try
    //    {
    //        DataTable dt = new SmartPortal.SEMS.Product().LoadProductCbb(ddlCustType.SelectedValue, SmartPortal.Constant.IPC.PRCAGENTMERCHANT, string.Empty, ref IPCERRORCODE, ref IPCERRORDESC).Tables[0];
    //        ddlProduct.DataSource = dt;
    //        ddlProduct.DataTextField = "PRODUCTNAME";
    //        ddlProduct.DataValueField = "PRODUCTID";
    //        ddlProduct.DataBind();
    //    }
    //    catch (Exception ex)
    //    {
    //        SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
    //        SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
    //    }
    //}

    public void LoadContractLevelByCombobox()
    {
        try
        {
            DataTable dtLevel = new SmartPortal.SEMS.Contract().LoadContractLevelCBB(ref IPCERRORCODE, ref IPCERRORDESC).Tables[0];
            ddlContractLevel.DataSource = dtLevel;
            ddlContractLevel.DataTextField = "CONTRACT_LEVEL_NAME";
            ddlContractLevel.DataValueField = "CONTRACT_LEVEL_ID";
            ddlContractLevel.DataBind();
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
    protected void UpLoadFile()
    {
        if (IsPostBack && FUNRICBackNew.PostedFile != null)
        {
            if (FUNRICBackNew.PostedFile.FileName.Length > 0)
            {
                //string path = "~/Widgets/SEMSCreateContractAgentMerchant/UpLoadFile/NewNRIC/";
                //SaveFile(FUNRICBackNew, path, IMAGE + "7.jpg");
                lblNRICBackNew.Text = Base64ImageAndShow(FUNRICBackNew, ImgNRICBackNew);
                PopupImgNRICBackNew.ImageUrl = "data:image/jpg;base64," + lblNRICBackNew.Text;
            }
        }
        if (IsPostBack && FUNRICFontNew.PostedFile != null)
        {
            if (FUNRICFontNew.PostedFile.FileName.Length > 0)
            {
                //string path = "~/Widgets/SEMSCreateContractAgentMerchant/UpLoadFile/NewNRIC/";
                //SaveFile(FUNRICFontNew, path, IMAGE + "6.jpg");
                lblNRICFontNew.Text = Base64ImageAndShow(FUNRICFontNew, ImgNRICFontNew);
                PopupImgNRICFontNew.ImageUrl = "data:image/jpg;base64," + lblNRICFontNew.Text;
            }
        }
        if (IsPostBack && FUSelfieNRIC.PostedFile != null)
        {
            if (FUSelfieNRIC.PostedFile.FileName.Length > 0)
            {
                //string path = "~/Widgets/SEMSCreateContractAgentMerchant/UpLoadFile/NewNRIC/";
                //SaveFile(FUNRICFontNew, path, IMAGE + "6.jpg");
                lblSelfieNRIC.Text = Base64ImageAndShow(FUSelfieNRIC, ImgSelfieNRIC);
                PopupImgSelfieNRIC.ImageUrl = "data:image/jpg;base64," + lblSelfieNRIC.Text;
            }
        }
    }
    private string SaveFile(FileUpload file, string path, string filename)
    {
        try
        {

            string savePath = Server.MapPath(path);
            string pathToCheck = savePath + filename;
            savePath += filename;
            file.SaveAs(savePath);
            return savePath;
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    private string Base64ImageAndShow(FileUpload fileUpload, Image Img)
    {
        try
        {
            string base64String;
            System.Drawing.Image imageToBeResized = System.Drawing.Image.FromStream(fileUpload.PostedFile.InputStream);
            int imageHeight = imageToBeResized.Height;
            int imageWidth = imageToBeResized.Width;
            int maxHeight = 400;
            int maxWidth = 600;
            imageHeight = (imageHeight * maxWidth) / imageWidth;
            imageWidth = maxWidth;
            if (imageHeight > maxHeight)
            {
                imageWidth = (imageWidth * maxHeight) / imageHeight;
                imageHeight = maxHeight;
            }
            System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(imageToBeResized, imageWidth, imageHeight);
            System.IO.MemoryStream stream = new MemoryStream();
            bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
            stream.Position = 0;
            byte[] byteImage = stream.ToArray();
            base64String = Convert.ToBase64String(byteImage);
            Img.ImageUrl = "data:image/png;base64," + base64String;
            Img.Visible = true;
            return base64String;
        }
        catch (Exception)
        {
            throw;
        }
    }
    public string GenIDImage(int length)
    {
        var chars = "0123456789";
        var stringChars = new char[length];
        var random = new Random();

        for (int i = 0; i < stringChars.Length; i++)
        {
            stringChars[i] = chars[random.Next(chars.Length)];
        }

        var finalString = new String(stringChars);
        return finalString;
    }
    public bool sendPincode()
    {
        try
        {
            string result = new SmartPortal.SEMS.User().CheckSendPincodeSysvar(ref IPCERRORCODE, ref IPCERRORDESC);
            if (result.Equals("1"))
                return true;
            else
                return false;
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
        return false;
    }
    private void SetDateTime()
    {
        txtFridayOpen.Text = txtThursdayOpen.Text = txtSaturdayOpen.Text = txtTuesdayOpen.Text = txtWednesdayOpen.Text = txtMondayOpen.Text = "09:00:00";
        txtFridayClose.Text = txtThursdayClose.Text = txtSaturdayClose.Text = txtTuesdayClose.Text = txtWednesdayClose.Text = txtMondayClose.Text = "17:00:00";
    }
    private DataTable TimeOpenContract(string contractNo)
    {
        #region ADD TIME OPEN
        DataTable tblTimeOpen = new DataTable();
        DataColumn colDay = new DataColumn("colDay");
        DataColumn colTimeOpen = new DataColumn("colTimeOpen");
        DataColumn colTimeClose = new DataColumn("colTimeClose");
        DataColumn colContractNo = new DataColumn("colContractNo");
        //add vào table
        tblTimeOpen.Columns.Add(colDay);
        tblTimeOpen.Columns.Add(colTimeOpen);
        tblTimeOpen.Columns.Add(colTimeClose);
        tblTimeOpen.Columns.Add(colContractNo);

        DataRow rowMonth = tblTimeOpen.NewRow();
        rowMonth["colTimeOpen"] = txtMondayOpen.Text;
        rowMonth["colDay"] = "Month";
        rowMonth["colTimeClose"] = txtMondayClose.Text;
        rowMonth["colContractNo"] = contractNo;
        tblTimeOpen.Rows.Add(rowMonth);

        DataRow rowTuesday = tblTimeOpen.NewRow();
        rowTuesday["colTimeOpen"] = txtTuesdayOpen.Text;
        rowTuesday["colDay"] = "Tuesday";
        rowTuesday["colTimeClose"] = txtTuesdayClose.Text;
        rowTuesday["colContractNo"] = contractNo;
        tblTimeOpen.Rows.Add(rowTuesday);


        DataRow rowWednesday = tblTimeOpen.NewRow();
        rowWednesday["colTimeOpen"] = txtWednesdayOpen.Text;
        rowWednesday["colDay"] = "Wednesday";
        rowWednesday["colTimeClose"] = txtWednesdayClose.Text;
        rowWednesday["colContractNo"] = contractNo;
        tblTimeOpen.Rows.Add(rowWednesday);

        DataRow rowThursday = tblTimeOpen.NewRow();
        rowThursday["colTimeOpen"] = txtThursdayOpen.Text;
        rowThursday["colDay"] = "Thursday";
        rowThursday["colTimeClose"] = txtThursdayClose.Text;
        rowThursday["colContractNo"] = contractNo;
        tblTimeOpen.Rows.Add(rowThursday);

        DataRow rowFriday = tblTimeOpen.NewRow();
        rowFriday["colTimeOpen"] = txtFridayOpen.Text;
        rowFriday["colDay"] = "Friday";
        rowFriday["colTimeClose"] = txtFridayClose.Text;
        rowFriday["colContractNo"] = contractNo;
        tblTimeOpen.Rows.Add(rowFriday);

        DataRow rowSaturday = tblTimeOpen.NewRow();
        rowSaturday["colTimeOpen"] = txtSaturdayOpen.Text;
        rowSaturday["colDay"] = "Saturday";
        rowSaturday["colTimeClose"] = txtSaturdayClose.Text;
        rowSaturday["colContractNo"] = contractNo;
        tblTimeOpen.Rows.Add(rowSaturday);


        return tblTimeOpen;

        #endregion
    }

    private DataTable UpdateContractforDocument(string Custid, string userCreate, string phone, string userid, string contracttype)
    {
        DataTable dt = (DataTable)ViewState["TBLDOCUMENT"];

        DataColumn colCustid = new DataColumn("colCustid", typeof(System.String));
        colCustid.DefaultValue = Custid;
        dt.Columns.Add(colCustid);

        DataColumn colPhone = new DataColumn("colPhone", typeof(System.String));
        colPhone.DefaultValue = phone;
        dt.Columns.Add(colPhone);

        DataColumn colUserCreate = new DataColumn("colUserCreate", typeof(System.String));
        colUserCreate.DefaultValue = userCreate;
        dt.Columns.Add(colUserCreate);
        ViewState["TBLDOCUMENT"] = dt;

        DataColumn colUserID = new DataColumn("colUserID", typeof(System.String));
        colUserID.DefaultValue = userid;
        dt.Columns.Add(colUserID);

        DataColumn colTyContract = new DataColumn("colTyContract", typeof(System.String));
        colTyContract.DefaultValue = contracttype;
        dt.Columns.Add(colTyContract);

        ViewState["TBLDOCUMENT"] = dt;
        return dt;
    }
    private DataTable DocumentContract()
    {
        DataTable tblDocument = new DataTable();
        DataColumn colDocCode = new DataColumn("colDocCode");
        DataColumn colDocName = new DataColumn("colDocName");
        DataColumn colDocType = new DataColumn("colDocType");
        DataColumn colImg = new DataColumn("colImg");
        DataColumn colStatus = new DataColumn("colStatus");

        tblDocument.Columns.Add(colDocCode);
        tblDocument.Columns.Add(colDocName);
        tblDocument.Columns.Add(colDocType);
        tblDocument.Columns.Add(colImg);
        tblDocument.Columns.Add(colStatus);

        return tblDocument;
    }
    protected void btnImportFile_Click(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = string.Empty;
            //add Agreement
            DataTable dt = (DataTable)ViewState["TBLDOCUMENT"];
            if (FUAC.HasFile)
            {
                String fileExtension = System.IO.Path.GetExtension(FUAC.FileName).ToLower();
                string[] allowedExtensions = { ".PNG", ".JPG", ".JPEG", ".BMP", ".PDF", ".WEBP" };
                bool checkExtensions = false;
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
                    lblError.Text = "Extensions " + fileExtension + " not support";
                    return;
                }

                System.IO.Stream fs = FUAC.PostedFile.InputStream;
                System.IO.BinaryReader br = new System.IO.BinaryReader(fs);
                Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                DataTable dtResult = dt.Select("colDocName= '" + FUAC.FileName + "'  AND colDocType='Agreement Contract'", "colDocName asc").Any() ? dt.Select("colDocName= '" + FUAC.FileName + "'  AND colDocType='Agreement Contract'", "colDocName asc").CopyToDataTable() : null;
                if (dtResult == null)
                {
                    DataRow dr = dt.NewRow();
                    dr["colDocCode"] = "AC";
                    dr["colDocName"] = FUAC.FileName;
                    dr["colDocType"] = "Agreement Contract";
                    dr["colImg"] = base64String;
                    dr["colStatus"] = "P";
                    dt.Rows.Add(dr);
                }
                TBLDOCUMENT = dt;
                GvImg.DataSource = dt;
                GvImg.DataBind();
            }
            //add Business
            if (FUBDoc.HasFile)
            {
                String fileExtension = System.IO.Path.GetExtension(FUBDoc.FileName).ToLower();
                string[] allowedExtensions = { ".PNG", ".JPG", ".JPEG", ".BMP", ".PDF", ".WEBP" };
                bool checkExtensions = false;
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
                    lblError.Text = "Extensions " + fileExtension + " not support";
                    return;
                }

                System.IO.Stream fs = FUBDoc.PostedFile.InputStream;
                System.IO.BinaryReader br = new System.IO.BinaryReader(fs);
                Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                DataTable dtResult = dt.Select("colDocName= '" + FUBDoc.FileName + "' AND colDocType='Business Document'", "colDocName asc").Any() ? dt.Select("colDocName= '" + FUBDoc.FileName + "' AND colDocType='Business Document'", "colDocName asc").CopyToDataTable() : null;
                if (dtResult == null)
                {
                    DataRow dr = dt.NewRow();
                    dr["colDocCode"] = "BD";
                    dr["colDocName"] = FUBDoc.FileName;
                    dr["colDocType"] = "Business Document";
                    dr["colImg"] = base64String;
                    dr["colStatus"] = SmartPortal.Constant.IPC.PENDING;
                    dt.Rows.Add(dr);
                }
                TBLDOCUMENT = dt;
                GvImg.DataSource = dt;
                GvImg.DataBind();
            }
        }
        catch (Exception ex)
        {
        }
    }
    protected void GvImg_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        try
        {
            DataTable dt = TBLDOCUMENT;
            dt.Rows.RemoveAt(e.RowIndex);
            GvImg.DataSource = dt;
            GvImg.DataBind();
            TBLDOCUMENT = dt;
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"],
                this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
    protected void GvImg_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            Image Font_IMG;
            Image PopupFont_IMG;
            DataRowView drv;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                drv = (DataRowView)e.Row.DataItem;
                Font_IMG = (Image)e.Row.FindControl("Font_IMG");
                PopupFont_IMG = (Image)e.Row.FindControl("PopupFont_IMG");
                if (!string.IsNullOrEmpty(drv["colImg"].ToString()))
                {
                    Font_IMG.ImageUrl = "data:image/jpg;base64," + drv["colImg"].ToString();
                    PopupFont_IMG.ImageUrl = "data:image/jpg;base64," + drv["colImg"].ToString();
                }
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(ConfigurationManager.AppSettings["sysec"],
                this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(ConfigurationManager.AppSettings["sysec"], "");
        }

    }
    protected void ddlUserType_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlUserType.SelectedValue.Equals("A"))
            {
                ddlSubUserType.Items.Clear();
                DataTable dsUserType = new SmartPortal.SEMS.Services().GetUserType(string.Empty, "A", ref IPCERRORCODE, ref IPCERRORDESC).Tables[0];
                ddlSubUserType.DataSource = dsUserType;
                ddlSubUserType.DataTextField = "SUBUSERTYPE";
                ddlSubUserType.DataValueField = "SUBUSERCODE";
                ddlSubUserType.DataBind();
                ddlMerchantCode.Visible = false;
                lblMerchantCategory.Visible = false;
            }
            else
            {
                ddlSubUserType.Items.Clear();
                DataTable dsUserType = new SmartPortal.SEMS.Services().GetUserType(string.Empty, "M", ref IPCERRORCODE, ref IPCERRORDESC).Tables[0];
                ddlSubUserType.DataSource = dsUserType;
                ddlSubUserType.DataTextField = "SUBUSERTYPE";
                ddlSubUserType.DataValueField = "SUBUSERCODE";
                ddlSubUserType.DataBind();
                ddlMerchantCode.Visible = true;
                lblMerchantCategory.Visible = true;
            }
            LoadProductByUserType();
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void ddlSubUserType_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            LoadProductByUserType();
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    public string LoadKYCType(string type)
    {
        try
        {
            string result = string.Empty;
            DataTable resultInfo = new Customer().LoadKYCType(type, ref IPCERRORCODE, ref IPCERRORDESC);
            result = resultInfo.Rows[0]["KycCode"].ToString();
            return result;
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
        return string.Empty;
    }
    public bool CheckPagerNo(string paperNo, string type)
    {
        try
        {
            if (paperNo.Equals(""))
            {

            }
            string resultInfo = new Customer().CheckPaperNumer(paperNo, type, ref IPCERRORCODE, ref IPCERRORDESC);
            if (resultInfo.Equals("0"))
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
        return false;
    }
    public void LoadKYC()
    {
        try
        {
            DataSet ds = new DataSet();
            object[] searchObject = new object[] { string.Empty, 0, 15 };
            ds = _service.common("SEMS_WAL_KYCSEARCH", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);

            DataTable dtkyc = new DataTable();
            dtkyc = ds.Tables[0];
            foreach (DataRow dr in dtkyc.Rows)
            {
                if (dr["KYCCODE"].ToString() == "NRIC")
                {
                    // radNewNRIC.Text = dr["KYCNAME"].ToString();
                    showNRIC = dr["STATUS"].ToString() == "A" ? true : false;
                }
                if (dr["KYCCODE"].ToString() == "PASSPORT")
                {
                    //   radPassport.Text = dr["KYCNAME"].ToString();
                    showPassport = dr["STATUS"].ToString() == "A" ? true : false;
                }
                if (dr["KYCCODE"].ToString() == "LICENSE")
                {
                    //  radLicense.Text = dr["KYCNAME"].ToString();
                    showLicense = dr["STATUS"].ToString() == "A" ? true : false;
                }
            }
        }
        catch
        {

        }
    }
    public bool showTabNRIC()
    {
        return showNRIC;
    }
    public bool showTabPassport()
    {
        return showPassport;
    }
    public bool showTabLicense()
    {
        return showLicense;

    }
    public void resetImage()
    {
        lblSelfieNRIC.Text = string.Empty;
        lblNRICBackNew.Text = string.Empty;
        lblNRICFontNew.Text = string.Empty;
    }
    private bool CheckUserNameExists(string userName)
    {
        DataSet ds = new Customer().CheckUserName("EBA_Users_CheckUserNameMB", new object[] { userName, SmartPortal.Constant.IPC.CONTRACTAGENTMERCHANT }, ref IPCERRORCODE, ref IPCERRORDESC);
        if (IPCERRORCODE != "0")
        {
            lblAlert.Text = IPCERRORDESC;
            return false;
        }
        if (!validateusername(txtUserNameMB))
        {
            return false;
        }
        return true;
    }
    private void LoadMerchantCategoryCodes()
    {
        try
        {
            DataSet ds = new DataSet();
            ds = new AgentMerchant().LoadMerchantCode(ref IPCERRORCODE, ref IPCERRORDESC);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    ddlMerchantCode.DataSource = ds;
                    ddlMerchantCode.DataTextField = "DESCRIPTION";
                    ddlMerchantCode.DataValueField = "MERCHANTCODE";
                    ddlMerchantCode.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
    protected void CreateUserName_Click(object sender, EventArgs e)
    {
        NumberReg = 0;
        txtUserNameMB.Text = GenerateUserName();
    }
    private string GenerateUserName()
    {
        NumberReg++;
        switch (ddlSubUserType.SelectedValue)
        {
            case "0103":
                resultUserName = new Customer().UsernameGeneration("1", ref IPCERRORCODE, ref IPCERRORDESC);
                break;
            case "0102":
                resultUserName = new Customer().UsernameGeneration("2", ref IPCERRORCODE, ref IPCERRORDESC);
                break;
            case "0101":
                resultUserName = new Customer().UsernameGeneration("3", ref IPCERRORCODE, ref IPCERRORDESC);
                break;
            case "0301":
                resultUserName = new Customer().UsernameGeneration("4", ref IPCERRORCODE, ref IPCERRORDESC);
                break;
            default:
                resultUserName = new Customer().UsernameGeneration("7", ref IPCERRORCODE, ref IPCERRORDESC);
                break;
        }
        if (resultUserName.Equals(string.Empty) || resultUserName.Length != 10 || !CheckUserNameExists(resultUserName))
        {
            if (NumberReg < 10)
            {
                return GenerateUserName();
            }
            else
            {
                lblAlert.Text = IPCERRORDESC;
                return string.Empty;
            }
        }
        btnThemChuTaiKhoan.Visible = true;
        lblAlert.Text = string.Empty;
        return resultUserName;
    }
    protected void ChangeUserName_TextChanged(object sender, EventArgs e)
    {
        if (!CheckUserNameExists(txtUserNameMB.Text.Trim()))
        {
            btnThemChuTaiKhoan.Visible = false;
            lblAlert.Text = Resources.labels.usernamehasexists;
        }
        else
        {
            btnThemChuTaiKhoan.Visible = true;
            lblAlert.Text = string.Empty;
            txtUsernameIB.Text = txtUserInputMB.Text;
        }
    }
    public void LoadProductByUserType()
    {
        try
        {
            string userType = ddlUserType.SelectedValue == SmartPortal.Constant.IPC.AGENT ? "01" : "03";
            DataTable dt = new SmartPortal.SEMS.Product().LoadProductByUserType(ddlSubUserType.SelectedValue, SmartPortal.Constant.IPC.PRCAGENTMERCHANT, "1", ref IPCERRORCODE, ref IPCERRORDESC).Tables[0];
            ddlProduct.DataSource = dt;
            ddlProduct.DataTextField = "PRODUCTNAME";
            ddlProduct.DataValueField = "PRODUCTID";
            ddlProduct.DataBind();
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
    public void LoadTownshipFromRegion(string region)

    {
        try
        {
            DataTable dtTownship = new SmartPortal.SEMS.Township().LoadTownShipFormRegion(region, ref IPCERRORCODE, ref IPCERRORDESC).Tables[0];
            ddlTownship.DataSource = dtTownship;
            ddlTownship.DataTextField = "TOWNSHIPNAME";
            ddlTownship.DataValueField = "TOWNSHIPCODE";
            ddlTownship.DataBind();
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
    protected void ddlRegion_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            LoadTownshipFromRegion(ddlRegion.SelectedValue);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    private bool ValidateNext()
    {
        try
        {
            if (string.IsNullOrEmpty(txtBirth.Text.Trim()) || string.IsNullOrEmpty(txtFullName.Text.Trim()))
            {
                lblError.Text = Resources.labels.customerinformationreturnisincorrect;
                return false;
            }
            string dateExpire_date = new SmartPortal.SEMS.EBASYSVAR().ViewDetail("YEAROLD", ref IPCERRORCODE, ref IPCERRORDESC).Tables[0].Rows[0]["VARVALUE"].ToString(); ;
            DateTime myDate = DateTime.ParseExact(txtBirth.Text.Trim(), "dd/MM/yyyy",
                                       System.Globalization.CultureInfo.InvariantCulture);
            if(ddlKYCName.SelectedValue != "4")
            {
                if (myDate > DateTime.Now.AddYears(-int.Parse(dateExpire_date)))
                {
                    txtBirth.Enabled = true;
                    lblError.Text = Resources.labels.underageuser;
                    return false;
                }
            }

            if (string.IsNullOrEmpty(ddlRegion.SelectedValue))
            {
                lblError.Text = "Region is not null";
                return false;
            }
            if (string.IsNullOrEmpty(ddlTownship.SelectedValue))
            {
                lblError.Text = "Township is not null";
                return false;
            }
            return true;
        }
        catch
        {
            return false;
        }
    }
    private bool ValidateSave()
    {
        try
        {
            if (string.IsNullOrEmpty(txtReBirth.Text.Trim()) || string.IsNullOrEmpty(txtReFullName.Text.Trim()))
            {
                lblError.Text = Resources.labels.customerinformationreturnisincorrect;
                txtReBirth.Enabled = true;
                return false;
            }
            string dateExpire_date = new SmartPortal.SEMS.EBASYSVAR().ViewDetail("YEAROLD", ref IPCERRORCODE, ref IPCERRORDESC).Tables[0].Rows[0]["VARVALUE"].ToString(); ;
            DateTime myDate = DateTime.ParseExact(txtBirth.Text.Trim(), "dd/MM/yyyy",
                                      System.Globalization.CultureInfo.InvariantCulture);
            if (myDate > DateTime.Now.AddYears(-int.Parse(dateExpire_date)))
            {
                lblError.Text = Resources.labels.underageuser;
                txtReBirth.Enabled = true;
                return false;
            }
            return true;
        }
        catch
        {
            return false;
        }
    }
    private void loadCombobox_KYCLevel()
    {
        DataSet ds = new DataSet();
        object[] loadKYCLevel = new object[] { string.Empty };
        ds = _service.common("SEMS_BO_GET_INFO_KYC", loadKYCLevel, ref IPCERRORCODE, ref IPCERRORDESC);
        if (IPCERRORCODE == "0")
        {
            if (ds.Tables.Count > 0)
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlKYCName.DataSource = ds;
                    ddlKYCName.DataValueField = "KycId";
                    ddlKYCName.DataTextField = "KycName";
                    ddlKYCName.DataBind();
                }
        }

    }

    protected void rptData_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                //if (ACTION == IPC.ACTIONPAGE.EDIT || ACTION == IPC.ACTIONPAGE.APPROVE)
                //{
                //    if (!(ViewState["APPROVE_STATUS"].ToString() != "P" || ViewState["APPROVE_STATUS"].ToString() != "N"))
                //    {
                //        LinkButton lbtnDelete;
                //        lbtnDelete = (LinkButton)e.Item.FindControl("lbtnDelete");
                //        lbtnDelete.Enabled = false;
                //        lbtnDelete.CssClass = "btn btn-secondary";
                //        lbtnDelete.OnClientClick = null;

                //        fileUpdate.Enabled = false;
                //        pannelModal.Enabled = false;
                //    }
                //}
                if (pnDocument.Enabled == false)
                {
                    LinkButton lbtnDelete;
                    lbtnDelete = (LinkButton)e.Item.FindControl("lbtnDelete");
                    lbtnDelete.Enabled = false;
                    lbtnDelete.CssClass = "btn btn-secondary";
                    lbtnDelete.OnClientClick = null;
                }

            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
    protected void rptData_ItemCommand1(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            string commandName = e.CommandName;
            string commandArg = e.CommandArgument.ToString();

            foreach (DocumentModel item in listDocumentModel)
            {
                if (item.No.ToString() == commandArg)
                {
                    //loadCombobox_KYCDocumentName_Repeater(item.DocumentType);
                    //ddlDocumentType.SelectedValue = item.DocumentType;
                    //txtDocumentName.Text = item.DocumentName;
                    //Image1.Attributes.Add("src", FORMAT_IMAGE + item.File);
                    //ViewState["No"] = item.No.ToString();
                    //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Pop", "closeModal('" + Image.ClientID + "');openModal('" + Image.ClientID + "');", true);
                    return;
                }
            }


        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }

    public List<DocumentModel> listDocumentModel
    {
        get
        {
            // check if not exist to make new (normally before the post back)
            // and at the same time check that you did not use the same viewstate for other object
            if (!(ViewState["listDoc"] is List<DocumentModel>))
            {
                // need to fix the memory and added to viewstate
                ViewState["listDoc"] = new List<DocumentModel>();
            }

            return (List<DocumentModel>)ViewState["listDoc"];
        }
    }
    private void loadData_Repeater(DataSet ds)
    {
        //  if (!IsPostBack)
        //  {
        loadData_ListDocument(ds);
        // }
        //if
        //  {
        if (listDocumentModel != null)
        {
            rptData.DataSource = listDocumentModel;
            rptData.DataBind();
        }
        else
        {
            loadData_ListDocument(ds);
        }
        //  }
    }
    private void loadData_ListDocument(DataSet ds)
    {
        listDocumentModel.Clear();
        if (ds != null)
        {
            if (ds.Tables.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DocumentModel item = new DocumentModel();
                    item.IsNew = false;
                    item.No = int.Parse(ds.Tables[0].Rows[i]["No"].ToString().Trim());
                    item.DocumentID = int.Parse(ds.Tables[0].Rows[i]["DocumentID"].ToString().Trim());
                    item.DocumentName = ds.Tables[0].Rows[i]["DocumentName"].ToString().Trim();
                    item.DocumentType = ds.Tables[0].Rows[i]["DocumentType"].ToString().Trim();
                    item.File = ds.Tables[0].Rows[i]["File"].ToString();
                    item.Status = ds.Tables[0].Rows[i]["Status"].ToString();
                    if(item.Status.Equals("D")) continue;
                    listDocumentModel.Add(item);
                }
                rptData.DataSource = listDocumentModel;
                rptData.DataBind();

                rptData.Visible = true;
                pnDocument.Visible = true;
            }
        }
    }
    private string getRequestID(string ppNumber, string kycID)
    {
        DataSet ds = new SmartPortal.SEMS.Contract().GetRequestIDByPPNumber(ppNumber, kycID, "AM", ref IPCERRORCODE, ref IPCERRORDESC);
        if (ds != null)
        {
            DataTable countryTable = ds.Tables[0];
            if (countryTable.Rows.Count != 0)
            {
                return countryTable.Rows[0]["RequestID"].ToString();

            }
        }
        return null;
    }
    private void loadData()
    {
        try
        {
            DataSet ds = new DataSet();
            object[] searchObject = new object[] { Utility.KillSqlInjection(getRequestID(txtNRICNewNumber.Text.Trim().ToString(), ddlKYCName.SelectedValue.Trim().ToString())) };
            ds = _service.common("SEMS_BO_MERCHANT_KYC", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count < 1) return;
                    DataTable dataTable = ds.Tables[0];
                    ViewState["CustID"] = dataTable.Rows[0]["CustID"].ToString();
                    ViewState["REQUESTNO"] = ID;
                    loadData_Repeater(ds);
                    ViewState["LICENSETYPE"] = ddlKYCName.SelectedValue = dataTable.Rows[0]["KYC_LEVEL"].ToString();
                    if (!string.IsNullOrEmpty(dataTable.Rows[0]["PAPER_NO"].ToString().Trim()))
                    {
                        txtNRICNewNumber.Text = dataTable.Rows[0]["PAPER_NO"].ToString().Trim();
                    }
                    if (!string.IsNullOrEmpty(dataTable.Rows[0]["FULLNAME"].ToString().Trim()))
                    {
                        txtFullName.Text = dataTable.Rows[0]["FULLNAME"].ToString().Trim();
                    }

                    if (!string.IsNullOrEmpty(dataTable.Rows[0]["DOB"].ToString().Trim()))
                    {
                        txtBirth.Text = dataTable.Rows[0]["DOB"].ToString().Trim();
                    }
                    if (!string.IsNullOrEmpty(dataTable.Rows[0]["Paper_No"].ToString().Trim()))
                    {
                        txtIF.Text = dataTable.Rows[0]["Paper_No"].ToString().Trim();
                    }

                    if (!string.IsNullOrEmpty(dataTable.Rows[0]["ADDR_RESIDENT"].ToString().Trim()))
                    {
                        txtResidentAddr.Text = dataTable.Rows[0]["ADDR_RESIDENT"].ToString().Trim();
                    }
                    if (!string.IsNullOrEmpty(dataTable.Rows[0]["EMAIL"].ToString().Trim()))
                    {
                        txtEmail.Text = dataTable.Rows[0]["EMAIL"].ToString().Trim();
                    }

                    if (!string.IsNullOrEmpty(dataTable.Rows[0]["ISSUEDATE"].ToString().Trim()))
                    {
                        txtIssueDate.Text = dataTable.Rows[0]["ISSUEDATE"].ToString().Trim();
                    }
                    if (!string.IsNullOrEmpty(dataTable.Rows[0]["NATION"].ToString().Trim()))
                    {
                        ddlNation.SelectedValue = dataTable.Rows[0]["NATION"].ToString().Trim();
                    }
                    hiddenKycUpload();
                }
            }
        }
        catch (Exception)
        {

        }
    }
    private void hiddenKycUpload()
    {
        lblNRICFontNew.Visible = false;
        FUNRICFontNew.Visible = false;
        FUNRICBackNew.Visible = false;
        lblNRICBackNew.Visible = false;
        FUSelfieNRIC.Visible = false;
        lblSelfieNRIC.Visible = false;
        btnUpLoadFile.Visible = false;
        PanelLabelNRC.Visible = false;
        PanelKycUploadBussiness.Visible = false;
        lblNRICBackNew.Text = "NRB";
        lblNRICFontNew.Text = "NRF";
        ddlKYCName.Enabled = false;
        txtNRICNewNumber.Enabled = false;
    }
    protected void btnCheckKYC_Click(object sender, EventArgs e)
    {
        loadData();
    }

    protected void ddlAccountMB_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!ddlAccountMB.SelectedValue.ToString().Equals("ALL"))
        {
            ddlDefaultAccountMB.SelectedValue = ddlAccountMB.SelectedValue.ToString();
            ddlDefaultAccountMB.DataBind();
        }
        else
        {
            ddlDefaultAccountMB.DataSource = ViewState["AccountList"];
            ddlDefaultAccountMB.DataTextField = "ACCOUNTNO";
            ddlDefaultAccountMB.DataValueField = "ACCOUNTNO";
            ddlDefaultAccountMB.DataBind();
            ddlDefaultAccountMB.Items.Insert(0, new ListItem(PHONE.Trim(), PHONE.Trim()));
        }

    }
}