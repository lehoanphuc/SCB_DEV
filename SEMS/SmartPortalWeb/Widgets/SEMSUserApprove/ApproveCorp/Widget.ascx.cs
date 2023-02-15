using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using SmartPortal.Common.Utilities;
using SmartPortal.Security;
using SmartPortal;
using SmartPortal.ExceptionCollection;
using SmartPortal.SEMS;
using SmartPortal.Model;
using SmartPortal.Constant;
using System.Linq;
using DayPilot.Web.Ui;
using System.Activities.Expressions;

public partial class Widgets_SEMSUserApprove_AddCorp_Widget : WidgetBase
{
    string ACTION = "";
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    DataTable userTable = new DataTable();
    DataTable userTableTemp = new DataTable();
    DataTable tblRoleDefault = new DataTable();
    static List<string> lsAccNo = new List<string>();
    string contractNo = "";
    string userName = "";
    string userType = "";
    string productType = "";
    //string productid = "";
    string corptype = "";
    #region BIEN
    string PHONE
    {
        get
        {
            return ViewState["PHONE"] != null ? ViewState["PHONE"].ToString() : "";
        }
        set { ViewState["PHONE"] = value; }
    }
    string USERTK
    {
        get
        {
            return ViewState["USERTK"] != null ? ViewState["USERTK"].ToString() : "";
        }
        set { ViewState["USERTK"] = value; }

    }
    public string CONTRACTTYPE
    {
        get { return ViewState["CONTRACTTYPE"] != null ? (string)ViewState["CONTRACTTYPE"] : string.Empty; }
        set { ViewState["CONTRACTTYPE"] = value; }
    }
    public string CUSTID
    {
        get { return ViewState["CUSTID"] != null ? (string)ViewState["CUSTID"] : ""; }
        set { ViewState["CUSTID"] = value; }
    }
    public string ACCOUNTWL
    {
        get { return ViewState["ACCOUNTWL"] != null ? (string)ViewState["ACCOUNTWL"] : ""; }
        set { ViewState["ACCOUNTWL"] = value; }
    }
    public string PHONEWL
    {
        get { return ViewState["PHONEWL"] != null ? (string)ViewState["PHONEWL"] : ""; }
        set { ViewState["PHONEWL"] = value; }
    }
    public string CONTRACTNO
    {
        get { return ViewState["CONTRACTNO"] != null ? (string)ViewState["CONTRACTNO"] : string.Empty; }
        set { ViewState["CONTRACTNO"] = value; }
    }
    public string PRODUCTID
    {
        get { return ViewState["PRODUCTID"] != null ? (string)ViewState["PRODUCTID"] : string.Empty; }
        set { ViewState["PRODUCTID"] = value; }
    }
    public string STS_USER
    {
        get { return ViewState["STS_USER"] != null ? (string)ViewState["STS_USER"] : string.Empty; }
        set { ViewState["STS_USER"] = value; }
    }
    #endregion
    protected void Page_Init(object sender, EventArgs e)
    {
        TabCustomerInfoHelper.LoadConfig();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {


            ACTION = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["a"].ToString().Trim();

            if (!IsPostBack)
            {
                InitData();
                DataSet dspolicy = new DataSet();
                string filterIB = "serviceid='IB'";
                string filterSMS = "serviceid='SMS'";
                string filterMB = string.Empty;
                filterMB = "serviceid='MB'";
                pnLoginMethod.Visible = true;
                string stSort = "serviceid asc";

                //lannth - 27.9.2018 - load va hien thi dropdown level, group voi contract la corp matrix
                if (corptype.Equals(SmartPortal.Constant.IPC.CONTRACTCORPMATRIX))
                {
                    LoadFormCorpMatrix(CONTRACTNO);
                }

                dspolicy = new SmartPortal.SEMS.USERPOLICY().GetPolicybyCondition(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, Session["userName"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE == "0")
                {
                    DataTable dt = dspolicy.Tables[0];
                    DataTable dtIB = dt.Select(filterIB, stSort).Any() ? dt.Select(filterIB, stSort).CopyToDataTable() : null;
                    DataTable dtSMS = null;
                    DataTable dtMB = dt.Select(filterMB, stSort).Any() ? dt.Select(filterMB, stSort).CopyToDataTable() : null;

                    ddlpolicyIB.DataSource = dtIB;
                    ddlpolicySMS.DataSource = dtSMS;

                    ddlpolicyMB.DataSource = dtMB;

                    ddlpolicyIB.DataTextField = "policytx";
                    ddlpolicyIB.DataValueField = "policyid";
                    ddlpolicySMS.DataTextField = "policytx";
                    ddlpolicySMS.DataValueField = "policyid";
                    ddlpolicyMB.DataTextField = "policytx";
                    ddlpolicyMB.DataValueField = "policyid";


                    ddlpolicyIB.DataBind();
                    ddlpolicySMS.DataBind();
                    ddlpolicyMB.DataBind();

                }

                #region load usertype
                DataSet dsUserType = new DataSet();
                DataTable dtUserType = new DataTable();

                if (!CONTRACTTYPE.Equals(SmartPortal.Constant.IPC.CONTRACTCORPADVANCE))
                {
                    ddlUserType.Items.FindByValue("AD").Enabled = false;
                    ddlUserType.Items.FindByValue("MK").Enabled = true;
                }
                else
                {
                    ddlUserType.Items.FindByValue("AD").Enabled = true;
                    ddlUserType.Items.FindByValue("MK").Enabled = false;
                };

                DropDownListBranch.DataSource = new SmartPortal.SEMS.Branch().GetAll(ref IPCERRORCODE, ref IPCERRORDESC);
                DropDownListBranch.DataTextField = "BRANCHNAME";
                DropDownListBranch.DataValueField = "BRANCHID";
                DropDownListBranch.DataBind();


                #endregion


                #region lay tat ca cac account cua khach hang
                DataSet ds = new DataSet();
                DataTable uTable = new DataTable();

                if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["uid"] == null)
                {
                    ds = new SmartPortal.IB.Customer().GetAllAcctno("", CONTRACTNO, ref IPCERRORCODE, ref IPCERRORDESC);
                }
                else
                {
                    uTable = (new SmartPortal.SEMS.User().GetFullUserByUID(USERTK, ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];


                    if (uTable.Rows.Count != 0)
                    {
                        ds = new SmartPortal.SEMS.Customer().GetAcctNo(uTable.Rows[0]["CFCODE"].ToString().Trim(), uTable.Rows[0]["CFTYPE"].ToString().Trim(), ref IPCERRORCODE, ref IPCERRORDESC);
                    }
                }

                if (IPCERRORCODE != "0")
                {
                    throw new IPCException("13");
                }
                DataTable dtAccount = new DataTable();
                dtAccount = ds.Tables[0];
                ViewState["AccountList"] = dtAccount;
                foreach (DataRow dr in dtAccount.Rows)
                {
                    if (dr["ACCOUNTTYPE"].ToString() == "WL")
                    {
                        dr.Delete();
                    }
                }
                dtAccount.AcceptChanges();
                if (dtAccount.Rows.Count != 0)
                {

                    if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["ID"] == null)
                    {

                        ddlAccountQT.DataSource = dtAccount;
                        ddlAccountQT.DataTextField = "ACCOUNTNO";
                        ddlAccountQT.DataValueField = "ACCOUNTNO";
                        ddlAccountQT.DataBind();
                        ddlAccountQT.Items.Insert(0, new ListItem("ALL", "ALL"));
                        ViewState["ACCOUNTCORE"] = dtAccount;
                        lstAccount.DataSource = dtAccount;
                        lstAccount.DataTextField = "ACCOUNTNO";
                        lstAccount.DataValueField = "ACCOUNTNO";
                        lstAccount.DataBind();
                        lstAccount.SelectedValue = dtAccount.Rows[0]["ACCOUNTNO"].ToString();
                    }
                    else
                    {
                        try
                        {

                            DataTable DefaultAccount = new SmartPortal.SEMS.Contract().GETUSERACCOUNTDEFAULT(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["ID"]);
                            ddlDefaultAccountQT.DataSource = DefaultAccount;
                            ddlDefaultAccountQT.DataTextField = "ACCOUNTNO";
                            ddlDefaultAccountQT.DataValueField = "ACCOUNTNO";
                            ddlDefaultAccountQT.DataBind();
                            ddlDefaultAccountQT.SelectedValue = DefaultAccount.Rows[0]["ACCOUNTNO"].ToString();

                            ddlAccountQT.DataSource = dtAccount;
                            ddlAccountQT.DataTextField = "ACCOUNTNO";
                            ddlAccountQT.DataValueField = "ACCOUNTNO";
                            ddlAccountQT.DataBind();
                            ddlAccountQT.Items.Insert(0, new ListItem("ALL", "ALL"));
                            //phongtt sms notification fee
                            lsAccNo.Clear();
                            foreach (DataRow dr in dtAccount.Rows)
                            {
                                if (!lsAccNo.Contains(dr["ACCOUNTNO"].ToString()))
                                {
                                    lsAccNo.Add(dr["ACCOUNTNO"].ToString());
                                }
                            }
                            ViewState["ACCOUNTCORE"] = dtAccount;
                            lstAccount.DataSource = dtAccount;
                            lstAccount.DataTextField = "ACCOUNTNO";
                            lstAccount.DataValueField = "ACCOUNTNO";
                            lstAccount.DataBind();
                            lstAccount.SelectedValue = dtAccount.Rows[0]["ACCOUNTNO"].ToString();
                        }
                        catch (Exception ex)
                        {
                            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Load Accout Type", System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
                            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

                        }
                    }


                    DataSet dswl = new Customer().GetWalletID(CONTRACTNO, ref IPCERRORCODE, ref IPCERRORDESC);
                    if (dswl.Tables[0].Rows.Count > 0 && dswl != null)
                    {
                        PHONEWL = dswl.Tables[0].Rows[0]["ACCOUNTNO"].ToString();
                        ACCOUNTWL = dswl.Tables[0].Rows[0]["WALLETID"].ToString();
                        //isWallet.Checked = true;
                    }
                    lstAccount.Items.Insert(0, new ListItem(PHONEWL, ACCOUNTWL));
                }


                #endregion

                ddlUserType_SelectedIndexChanged(sender, e);

                BindData();
                LoadddlSendInfo();
                txtSMSPhoneNo.Enabled = false;
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
    protected void txtReMobi_TextChanged(object sender, EventArgs e)
    {
        lblError.Text = string.Empty;
        if (!CheckIsPhoneNumer(txtReMobi.Text.Trim().ToString()))
        {
            lblError.Text = Resources.labels.phonenumberwrong;
        }
        else
        {
            txtMBPhoneNo.Text = txtSMSPhoneNo.Text = txtReMobi.Text.Trim();
        }
    }
    void LoadFormCorpMatrix(string contractNo)
    {
        DataSet ds = new SmartPortal.SEMS.ApprovalStructure().GetContractGroupDetail(contractNo, ref IPCERRORCODE, ref IPCERRORDESC);
        if (IPCERRORCODE.Equals("0"))
        {
            DataTable dt = ds.Tables[0];
            ddlGroup.DataSource = dt;
            ddlGroup.DataBind();
            //if (dt.Rows.Count == 0)
            ddlGroup.Items.Insert(0, new ListItem("Choice group", ""));
            ddlGroup.SelectedIndex = 0;
            ddlGroup.Enabled = dt.Rows.Count != 0;
        }
        else
        {
            ShowError(IPCERRORCODE);
        }
        VisibleCorpMatrix(true);
    }
    void VisibleCorpMatrix(bool isCorpMatrix)
    {
        ddlUserLevel.Visible = !isCorpMatrix;
        lblLevel.Visible = !isCorpMatrix;
        lblGroup.Visible = isCorpMatrix;
        ddlGroup.Visible = isCorpMatrix;
    }
    private void ShowError(string msg = "")
    {
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ShowError", "ShowError('" + msg + "');", true);
    }
    void BindData()
    {
        string service = string.Empty;
        lblTitleUser.Text = "Approve Customer";
        txtReBirth.Enabled = false;
        txtReFullName.Enabled = false;
        txtReMobi.Enabled = false;
        ddlUserType.Enabled = false;
        txtReEmail.Enabled = false;
        ddlReGender.Enabled = false;
        ddlUserLevel.Enabled = false;
        txtReAddress.Enabled = false;
        txtIBUserName.Enabled = false;
        txtSMSPhoneNo.Enabled = false;
        ddlType.Enabled = false;
        tvIB.Enabled = false;
        tvMBQT.Enabled = false;
        tvSMSQT.Enabled = false;
        ddlDefaultLang.Enabled = false;
        ddlSMSDefaultAcctno.Enabled = false;
        cbIsDefault.Enabled = false;
        ddlpolicyIB.Enabled = false;
        ddlpolicySMS.Enabled = false;
        ddlpolicyMB.Enabled = false;
        ddlpolicyMB.Enabled = false;
        ddlGroup.Enabled = false;
        ddlAccountQT.Enabled = false;
        txtUserNameMB.Enabled = false;
        lbCreateusername.Visible = false;
        lblChangePhone.Visible = false;
        txtLocalFullName.Enabled = false;
        ddlLoginMethod.Enabled = false;
        //isWallet.Enabled = false;
        tvRole.Enabled = false;


        #region Lấy thông tin user
        userTable = (new SmartPortal.SEMS.User().GetFullUserByUID(USERTK, ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];

        if (userTable.Rows.Count != 0)
        {
            //QuangTV - add old value to log
            txtReEmail.Text = userTable.Rows[0]["EMAIL"].ToString();
            ddlReGender.SelectedValue = userTable.Rows[0]["GENDER"].ToString();
            txtReAddress.Text = userTable.Rows[0]["ADDRESS"].ToString();
            txtMBPhoneNo.Text = userTable.Rows[0]["PHONE"].ToString();
            txtReFullName.Text = userTable.Rows[0]["FULLNAME"].ToString();
            PHONE = txtReMobi.Text = userTable.Rows[0]["PHONE"].ToString();
            txtReEmail.Text = userTable.Rows[0]["EMAIL"].ToString();
            txtLocalFullName.Text = userTable.Rows[0]["LOCALFULLNAME"].ToString();
            txtIBUserName.Text = userTable.Rows[0]["USERNAMEIB"].ToString();
            txtUserNameMB.Text = userTable.Rows[0]["USERNAME"].ToString();
            ddlReGender.SelectedValue = userTable.Rows[0]["GENDER"].ToString();
            ddlUserLevel.SelectedValue = userTable.Rows[0]["USERLEVEL"].ToString();
            ddlauthenType.SelectedValue = userTable.Rows[0]["AUTHENTYPE"].ToString();
            ddlLoginMethod.SelectedValue = userTable.Rows[0]["LOGINMETHOD"].ToString();
            txtReAddress.Text = userTable.Rows[0]["ADDRESS"].ToString();
            STS_USER = userTable.Rows[0]["STATUS"].ToString();
            if (STS_USER.Equals(SmartPortal.Constant.IPC.NEW))
            {
                ddlLoginMethod.Enabled = false;
            }

            ddlUserType.SelectedValue = userTable.Rows[0]["TYPEID"].ToString();
            ddlType.SelectedValue = userTable.Rows[0]["TYPEID"].ToString() + "#" + userTable.Rows[0]["USERLEVEL"].ToString();

            //lannth - 28.9.2018
            if (userTable.Rows[0]["TYPEID"].ToString().Equals(SmartPortal.Constant.IPC.MTRUSER))
            {
                ddlType.SelectedValue = userTable.Rows[0]["TYPEID"].ToString();
            }
            else
                ddlType.SelectedValue = userTable.Rows[0]["TYPEID"].ToString() + "#" + userTable.Rows[0]["USERLEVEL"].ToString();

            //27.4.2016 minh add to disable fullname of owner:
            if ((userTable.Rows[0]["USERTYPE"].ToString() == SmartPortal.Constant.IPC.PERSONALCONTRACT && userTable.Rows[0]["USERLEVEL"].ToString() == "0"
                || userTable.Rows[0]["USERTYPE"].ToString() == SmartPortal.Constant.IPC.CORPORATECONTRACT && userTable.Rows[0]["TYPEID"].ToString() == "CTK")
                && ACTION == SmartPortal.Constant.IPC.EDIT)
                txtReFullName.Enabled = false;

            if (userTable.Rows[0]["BIRTHDAY"].ToString() != "")
            {
                //HaiNT Edit birthDate (12/08/2013)
                string birthDate = SmartPortal.Common.Utilities.Utility.IsDateTime2(userTable.Rows[0]["BIRTHDAY"].ToString()).ToString("dd/MM/yyyy");
                txtReBirth.Text = birthDate.Equals("01/01/1900") ? "" : birthDate;
                txtReBirth.Text = birthDate.Equals("01/01/1900") ? "" : birthDate;
            }
            lblStatus.Text = userTable.Rows[0]["STATUS"].ToString();
            //branch
            DropDownListBranch.Enabled = false;
            if (!userTable.Rows[0][SmartPortal.Constant.IPC.BRANCHID].ToString().Trim().Equals(""))
                DropDownListBranch.SelectedValue = SmartPortal.Common.Utilities.Utility.FormatStringCore(int.Parse(userTable.Rows[0][SmartPortal.Constant.IPC.BRANCHID].ToString().Trim()).ToString());

            if (userTable.Rows[0]["IBpolicyid"].ToString().Trim() != string.Empty)
                ddlpolicyIB.SelectedValue = userTable.Rows[0]["IBpolicyid"].ToString().Trim();
            //disable policy of corp when edit
            if (userTable.Rows[0]["TYPEID"].ToString().Trim() == "QTHT")
                ddlpolicyIB.Enabled = false;

            DataColumnCollection columns = userTable.Columns;
            if (columns.Contains("SMSpolicyid"))
            {
                if (userTable.Rows[0]["SMSpolicyid"].ToString().Trim() != string.Empty)
                    ddlpolicySMS.SelectedValue = userTable.Rows[0]["SMSpolicyid"].ToString().Trim();
                if (userTable.Rows[0]["TYPEID"].ToString().Trim() == "QTHT")
                    ddlpolicySMS.Enabled = false;

            }
            if (userTable.Rows[0]["MBpolicyid"].ToString().Trim() != string.Empty)
                ddlpolicyMB.SelectedValue = ddlpolicyMB.SelectedValue = ddlpolicyMB.SelectedValue = userTable.Rows[0]["MBpolicyid"].ToString().Trim();
            if (userTable.Rows[0]["TYPEID"].ToString().Trim() == "QTHT")
                ddlpolicyMB.Enabled = false;
        }
        #endregion

        #region Lấy thông tin tài khoản User
        DataTable tblIB = new SmartPortal.IB.User().GetAccountDetail(USERTK, SmartPortal.Constant.IPC.IB);
        if (tblIB.Rows.Count != 0)
        {
            txtIBUserName.Text = tblIB.Rows[0]["USERNAME"].ToString().Trim();
            txtIBUserName.Enabled = false;
            //ddlStatusIB.SelectedValue = tblIB.Rows[0]["STATUS"].ToString().Trim();
            //txtIBPass.Attributes.Add("value", Encryption.Decrypt(tblIB.Rows[0]["PASSWORD"].ToString().Trim()));
            //txtIBRePass.Attributes.Add("value", Encryption.Decrypt(tblIB.Rows[0]["PASSWORD"].ToString().Trim()));
        }

        DataTable tblSMS = new SmartPortal.IB.User().GetAccountDetail(USERTK, SmartPortal.Constant.IPC.SMS);
        if (tblSMS.Rows.Count != 0)
        {
            txtSMSPhoneNo.Text = tblSMS.Rows[0]["PHONENO"].ToString().Trim();

            //ddlStatusSMS.SelectedValue = tblSMS.Rows[0]["STATUS"].ToString().Trim();
            ddlSMSDefaultAcctno.SelectedValue = tblSMS.Rows[0]["DEFAULTACCTNO"].ToString().Trim();
            ddlDefaultLang.SelectedValue = tblSMS.Rows[0]["DEFAULTLANG"].ToString().Trim();

            if (tblSMS.Rows[0]["ISDEFAULT"].ToString().Trim() == "Y")
            {
                cbIsDefault.Checked = true;
            }
            else
            {
                cbIsDefault.Checked = false;
            }
            if (tblSMS.Rows[0]["STATUS"].ToString().Trim() == SmartPortal.Constant.IPC.DELETE)
            {
                txtSMSPhoneNo.Text = "";
            }
            if (txtSMSPhoneNo.Text.Trim() != "")
            {
                //btn_HuySMS.Visible = true;
            }
        }
        DataTable tblMB = new SmartPortal.IB.User().GetAccountDetail(USERTK, SmartPortal.Constant.IPC.MB);
        if (tblMB.Rows.Count != 0)
        {

            txtUserID.Text = tblMB.Rows[0]["USERID"].ToString().Trim();
            service = SmartPortal.Constant.IPC.MB;
        }

        #endregion


        //lannth - lay ra group cua user
        #region lay group cua user
        if (userTable.Rows.Count > 0 && userTable.Rows[0]["contracttype"].ToString().Equals(SmartPortal.Constant.IPC.MTRUSER))
        {
            DataTable tblGroup = new SmartPortal.SEMS.User().GetGroupByUser(USERTK, ref IPCERRORCODE, ref IPCERRORDESC).Tables[0];
            if (tblGroup.Rows.Count != 0)
            {
                ddlGroup.SelectedValue = tblGroup.Rows[0]["GroupID"].ToString().Trim();
            }
        }
        #endregion

        #region Hien thi tat cac cac role theo serviceid va usertype len cay

        LoadDataALLRoleInTreeview(string.Empty, tvRole, ddlUserType.SelectedValue, SmartPortal.Constant.IPC.NORMAL);

        LoadAccountRoleByUserID(USERTK);
        #endregion

        #region ẩn button huỷ sms
        switch (ACTION)
        {
            case SmartPortal.Constant.IPC.VIEWDETAIL:
                break;
            case SmartPortal.Constant.IPC.EDIT:
                //btn_HuySMS.Visible = true;
                break;
        }
        #endregion

        #region  Fill UserName, Phone
        if (!USERTK.Equals(string.Empty))
        {
            txtUserID.Text = USERTK;
            txtSMSPhoneNo.Text = PHONE;
            txtSMSPhoneNo.Enabled = false;
        }
        #endregion
        LoadComboboxReason();
    }
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        try
        {
            string userID = "";
            string status = lblStatus.Text.Trim();

            if (Session["userIDA"] != null)
            {
                List<string> lstTran = new List<string>();
                lstTran = (List<string>)Session["userIDA"];

                userID = lstTran[0].Trim();

                //xử lý            


                new SmartPortal.SEMS.User().ApproveUser(userID, SmartPortal.Constant.IPC.ACTIVE, Session["userID"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);

                SmartPortal.Common.Log.UpdateLogDatabase(CONTRACTNO, "Add new user", Session["userName"].ToString(), SmartPortal.Constant.IPC.USER);
                SmartPortal.Common.Log.UpdateLogDatabase(CONTRACTNO, "Add new Account", Session["userName"].ToString(), SmartPortal.Constant.IPC.USER);
                SmartPortal.Common.Log.UpdateLogDatabase(CONTRACTNO, "Add Bank Account", Session["userName"].ToString(), "CHANGE");

                SmartPortal.Common.Log.UpdateLogDatabase(CONTRACTNO, "Delete Bank Account", Session["userName"].ToString(), "CHANGE");
                SendInfoLogin(userID);


                if (IPCERRORCODE == "0")
                {
                    //cập nhật lại List
                    lstTran.RemoveAt(0);

                    if (lstTran.Count > 0)
                    {
                        ltrError.Text = Resources.labels.duyetthanhcongmoibanduyetnguoidungtieptheo;
                    }
                    else
                    {
                        ltrError.Text = Resources.labels.duyetthanhcong;
                    }
                }
                else
                {
                    ltrError.Text = "Approve customer Fail ";
                    SmartPortal.Common.Log.WriteLogFile(IPCERRORCODE, "Approve Customer Fail:", "", userID + ":" + IPCERRORDESC);
                    //throw new IPCException();

                }

                if (lstTran.Count != 0)
                {
                    Session["userIDA"] = lstTran;
                    //chuyển qua giao dịch kế

                    BindData();
                }
                else
                {
                    Session["userIDA"] = null;
                    goto REDI;
                }

            }
            else
            {
                //approve khi duyệt 1 record
                new SmartPortal.SEMS.User().ApproveUser(USERTK, SmartPortal.Constant.IPC.ACTIVE, Session["userID"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);

                SmartPortal.Common.Log.UpdateLogDatabase(CONTRACTNO, "Add new user", Session["userName"].ToString(), SmartPortal.Constant.IPC.USER);
                SmartPortal.Common.Log.UpdateLogDatabase(CONTRACTNO, "Add Bank Account", Session["userName"].ToString(), "CHANGE");

                SmartPortal.Common.Log.UpdateLogDatabase(CONTRACTNO, "Delete Bank Account", Session["userName"].ToString(), "CHANGE");

                SendInfoLogin(USERTK);


                if (IPCERRORCODE == "0")
                {
                    ltrError.Text = Resources.labels.nguoidung + " " + USERTK + " " + Resources.labels.duyetthanhcong;

                    goto REDI;
                }
                else
                {

                    ltrError.Text = "Approve customer Fail ";
                    SmartPortal.Common.Log.WriteLogFile(IPCERRORCODE, "Approve Customer Fail:", "", USERTK + ":" + IPCERRORDESC);

                }

                goto REDI;
            }
            goto EXIT;
        }

        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], "Widgets_SEMSContractApprove_Widget", "SendInfoLogin", IPCex.ToString(), Request.Url.Query);
            //SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSContractApprove_Widget", "SendInfoLogin", ex.ToString(), Request.Url.Query);
            //SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    REDI:
        btnApprove.Visible = false;
        btnReject.Visible = false;
    EXIT:
        ;
    }
    protected void btnReject_Click(object sender, EventArgs e)
    {
        try
        {
            //approve
            string userID = "";
            if (ddlReason.SelectedValue.Equals(""))
            {
                ltrError.Text = "Reason name is not empty";
                return;
            }
            if (Session["userIDA"] != null)
            {
                List<string> lstTran = new List<string>();
                lstTran = (List<string>)Session["userIDA"];

                userID = lstTran[0].Trim();

                //xử lý            


                new SmartPortal.SEMS.User().ApproveUser(userID, SmartPortal.Constant.IPC.REJECT, Session["userID"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
                SendInfoLogin(userID);

                if (IPCERRORCODE == "0")
                {

                    DataSet dsReject = new SmartPortal.SEMS.Contract().InsertRejectReason(ddlReason.SelectedValue, userID, txtDescription.Text, "CUSTOMER", ref IPCERRORCODE, ref IPCERRORDESC);

                    if (IPCERRORCODE == "0")
                    {
                        ltrError.Text =  Resources.labels.khongduocduyetmoibanchonnguoidungtieptheo;

                        //cập nhật lại List
                        lstTran.RemoveAt(0);
                    }
                    else
                    {
                        ltrError.Text = "Rejcect customer Fail ";
                        SmartPortal.Common.Log.WriteLogFile(IPCERRORCODE, "Rejcect Customer Fail:", "", userID + ":" + IPCERRORDESC);
                    }

                }
                else
                {
                    ltrError.Text = "Rejcect customer Fail ";
                    SmartPortal.Common.Log.WriteLogFile(IPCERRORCODE, "Rejcect Customer Fail:", "", userID + ":" + IPCERRORDESC);
                    //throw new IPCException();
                }

                if (lstTran.Count != 0)
                {
                    Session["userIDA"] = lstTran;
                    //chuyển qua giao dịch kế
                    txtDescription.Text = string.Empty;
                    BindData();
                }
                else
                {

                    ltrError.Text = Resources.labels.nguoidung + " " + userID + " " + Resources.labels.huythanhcong;
                    Session["userIDA"] = null;
                    ddlReason.Enabled = false;
                    txtDescription.Enabled = false;
                    goto REDI;
                }

            }
            else
            {
                //approve khi duyệt 1 record
                new SmartPortal.SEMS.User().ApproveUser(USERTK, SmartPortal.Constant.IPC.REJECT, Session["userID"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);

                SendInfoLogin(USERTK);

                if (IPCERRORCODE == "0")
                {
                    DataSet dsReject = new SmartPortal.SEMS.Contract().InsertRejectReason(ddlReason.SelectedValue, USERTK, txtDescription.Text, "CUSTOMER", ref IPCERRORCODE, ref IPCERRORDESC);

                    if (IPCERRORCODE == "0")
                    {
                        ltrError.Text = Resources.labels.nguoidung + " " + USERTK + " " + Resources.labels.khongduocduyet;

                        goto REDI;
                    }
                    else
                    {
                        ltrError.Text = "Approve customer Fail ";
                        SmartPortal.Common.Log.WriteLogFile(IPCERRORCODE, "Approve Customer Fail:", "", userID + ":" + IPCERRORDESC);
                    }
                }
                else
                {
                    ltrError.Text = "Approve customer Fail ";
                    SmartPortal.Common.Log.WriteLogFile(IPCERRORCODE, "Approve Customer Fail:", "", userID + ":" + IPCERRORDESC);
                }

                goto REDI;
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
    REDI:
        btnApprove.Visible = false;
        btnReject.Visible = false;
    EXIT:
        ;
    }
    protected void btback_Click(object sender, EventArgs e)
    {
        RedirectBackToMainPage();
    }
    protected void ddlUserType_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadDataALLRoleInTreeview(string.Empty, tvRole, ddlUserType.SelectedValue, SmartPortal.Constant.IPC.NORMAL);
    }
    protected void ddlpolicyIB_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            ddlpolicyMB.SelectedValue = ddlpolicyMB.SelectedValue;
            ddlpolicyMB.SelectedValue = ddlpolicyMB.SelectedValue;
        }
        catch
        { }
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
    private string generateUserNameMB()
    {
        string username = string.Empty;
        if (string.IsNullOrEmpty(username))
            if (!string.IsNullOrEmpty(txtReFullName.Text.Trim()) && !string.IsNullOrEmpty(txtReMobi.Text.Trim()))
            {
                string cifno = txtReMobi.Text.Length < 2 ? txtReMobi.Text + "0123456789" : txtReMobi.Text;
                string preid = txtReAddress.Text + txtReEmail.Text + "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                username = SmartPortal.Common.Utilities.Utility.GetID(txtReFullName.Text, cifno, preid);

            }
            else
            {
                Random random = new Random();
                string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                StringBuilder result = new StringBuilder(10);
                for (int i = 0; i < 2; i++)
                {
                    var a = characters[random.Next(characters.Length)];
                    result.Append(characters[random.Next(characters.Length)]);
                }
                string number = "0123456789";
                for (int i = 0; i < 6; i++)
                {
                    result.Append(number[random.Next(number.Length)]);
                }
                username = result.ToString();
            }
        return username;
    }
    public bool CheckExistPhoneNumber(string phone)
    {
        string resultInfo = new Customer().CheckPhoneNumberCustInfo(phone, SmartPortal.Constant.IPC.CONTRACTINDIVIDUAL, ref IPCERRORCODE, ref IPCERRORDESC);
        if (resultInfo.Equals("1"))
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    public bool CheckPhoneByCustID(string Phone, string CustID)
    {
        string result = new Customer().CheckPhoneCustID(Phone, CUSTID, ref IPCERRORCODE, ref IPCERRORDESC);
        if (result.Equals("1"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void LoadComboboxReason()
    {
        try
        {
            string contractType = string.Empty;
            if (CONTRACTTYPE.Equals(SmartPortal.Constant.IPC.CONTRACTCORPMATRIX) ||
               CONTRACTTYPE.Equals(SmartPortal.Constant.IPC.CONTRACTCORPADVANCE) ||
               CONTRACTTYPE.Equals(SmartPortal.Constant.IPC.CONTRACTCORPSIMPLE))
            {
                 contractType = SmartPortal.Constant.IPC.CORPORATECONTRACT;
            }
       else
            {
                contractType = CONTRACTTYPE;
            }
            DataTable dtReason = new SmartPortal.SEMS.Contract().GetReason("", "", "", contractType, "CU", ref IPCERRORCODE, ref IPCERRORDESC).Tables[0];
            ddlReason.DataSource = dtReason;
            ddlReason.DataTextField = "REASONNAME";
            ddlReason.DataValueField = "REASONID";
            ddlReason.DataBind();
        }
        catch (Exception ex)
        {

        }
    }
    protected void lstAccount_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (lstAccount.SelectedValue.Trim().Equals(ACCOUNTWL))
            {
                LoadDataALLRoleInTreeview(string.Empty, tvRole, ddlUserType.SelectedValue, SmartPortal.Constant.IPC.WAL);
            }
            else
            {
                LoadDataALLRoleInTreeview(string.Empty, tvRole, ddlUserType.SelectedValue, SmartPortal.Constant.IPC.NORMAL);
            }
            foreach (TreeNode node in tvRole.Nodes)
            {
                node.Checked = false;
            }
            LoadAccountRole(USERTK, lstAccount.SelectedValue.ToString().Trim());
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSSetLimitTeller_Widget", "lstAccount_SelectedIndexChanged", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    }
    DataTable CreateTableRole()
    {

        DataColumn colRoleID = new DataColumn("colRoleID");
        DataColumn colServiceID = new DataColumn("colServiceID");

        DataTable tblRole = new DataTable();

        tblRole.Columns.Add(colRoleID);
        tblRole.Columns.Add(colServiceID);

        return tblRole;
    }
    void LoadDataALLRoleInTreeview(string serviceID, TreeView tvPage, string userType, string roleType)
    {
        tvPage.Nodes.Clear();
        DataTable tblSS = new DataTable();


        DataTable tblRole = new DataTable();
        tblRole = CreateTableRole();

        tblSS = new SmartPortal.SEMS.Role().GetByServiceAndUserType(serviceID, userType, PRODUCTID, roleType);

        foreach (DataRow row in tblSS.Rows)
        {

            //add view state
            DataRow rowUser = tblRole.NewRow();
            rowUser["colRoleID"] = row["ROLEID"].ToString();
            rowUser["colServiceID"] = row["SERVICEID"].ToString();
            tblRole.Rows.Add(rowUser);
            ViewState["TBLROLE"] = tblRole;


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
                node1.ToolTip = row1["PageDescription"].ToString();
                //node1.SelectAction = TreeNodeSelectAction.None;

                node.ChildNodes.Add(node1);
            }
            if (tblPage.Rows.Count != 0)
            {
                tvPage.Nodes.Add(node);
            }
        }
        tvPage.Attributes.Add("onclick", "OnTreeClick(event)");
        tvRole.Attributes.Add("onclick", "postBackByObject()");
    }
    DataTable CreateTableAccontRole()
    {
        DataColumn colUserID = new DataColumn("colUserID");
        DataColumn colAccount = new DataColumn("colAccount");
        DataColumn colRoleID = new DataColumn("colRoleID");
        DataColumn colServiceID = new DataColumn("colServiceID");
        DataColumn colContractNo = new DataColumn("colContractNo");

        DataTable tblAccount = new DataTable();

        tblAccount.Columns.Add(colUserID);
        tblAccount.Columns.Add(colAccount);
        tblAccount.Columns.Add(colRoleID);
        tblAccount.Columns.Add(colServiceID);
        tblAccount.Columns.Add(colContractNo);
        return tblAccount;
    }
    private void LoadAccountRole(string userID, string account)
    {
        try
        {
            DataTable tblAccountRole = ViewState["ACCOUNTROLE"] != null ? (DataTable)ViewState["ACCOUNTROLE"] : CreateTableAccontRole();
            foreach (DataRow dr in tblAccountRole.Rows)
            {
                if (dr["colAccount"].ToString().Equals(account) && dr["colUserID"].ToString().Equals(userID))
                {
                    foreach (TreeNode node in tvRole.Nodes)
                    {
                        if (node.Value.Equals(dr["colRoleID"].ToString()))
                        {
                            node.Checked = true;
                        }
                    }
                }
            }
        }
        catch
        {

        }

    }
    private void LoadAccountRoleByUserID(string userID)
    {
        try
        {

            DataTable dtuser = (new SmartPortal.SEMS.User().GetUserAccountRole(userID, ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];

            DataTable tblAccountRole = ViewState["ACCOUNTROLE"] != null ? (DataTable)ViewState["ACCOUNTROLE"] : CreateTableAccontRole();
            ViewState["ACCOUNTROLE"] = dtuser;
            LoadAccountRole(userID, lstAccount.SelectedValue);
        }
        catch
        {

        }

    }
    // public void IsWallet_OnCheckedChaned(object sender, EventArgs e)
    // {

        // string phone = string.Empty;
        // DataTable dtAccountQT = (DataTable)ViewState["ACCOUNTCORE"];
        // if (isWallet.Checked)
        // {
            // if (string.IsNullOrEmpty(ACCOUNTWL))
            // {
                // ACCOUNTWL = new SmartPortal.SEMS.User().CreatePrimaryKey(string.Empty, string.Empty, "WALLETID", ref IPCERRORCODE, ref IPCERRORDESC);
            // }
            // lstAccount.DataSource = dtAccountQT;
            // lstAccount.DataTextField = "ACCOUNTNO";
            // lstAccount.DataValueField = "ACCOUNTNO";
            // lstAccount.DataBind();
            // lstAccount.SelectedValue = dtAccountQT.Rows[0]["ACCOUNTNO"].ToString();
            // lstAccount.Items.Insert(0, new ListItem(PHONEWL, ACCOUNTWL));
        // }
        // else
        // {
            // lstAccount.DataSource = dtAccountQT;
            // lstAccount.DataTextField = "ACCOUNTNO";
            // lstAccount.DataValueField = "ACCOUNTNO";
            // lstAccount.DataBind();
            // lstAccount.SelectedValue = dtAccountQT.Rows[0]["ACCOUNTNO"].ToString();

        // }

    // }
    private void InitData()
    {

        ACTION = GetActionPage();
        if (ACTION.Equals(IPC.ACTIONPAGE.APPROVE))
        {
            btnApprove.Visible = true;
            btnReject.Visible = false;
            divReject.Visible = false;
            tblsendinfor.Visible = true;
            //if (Session["userIDA"] == null)
            //{
            //    RedirectBackToMainPage();
            //}
            //else
            //{
            //List<string> lstTran = new List<string>();
            //lstTran = (List<string>)Session["userIDA"];
            //USERTK = lstTran[0];
            //}
        }
        else if (ACTION.Equals(IPC.ACTIONPAGE.REJECT))
        {
            btnApprove.Visible = false;
            btnReject.Visible = true;
            tblsendinfor.Visible = false;
            divReject.Visible = true;
            ddlReason.Enabled = true;
            txtDescription.Enabled = true;
            //if (Session["userIDA"] == null)
            //{
            //        RedirectBackToMainPage();
            //    }
            //    else
            //    {
            //        List<string> lstTran = new List<string>();
            //        lstTran = (List<string>)Session["userIDA"];
            //        USERTK = lstTran[0];
            //    }
        }
        else
        {
            divReject.Visible = false;
            btnApprove.Visible = false;
            btnReject.Visible = false;
            tblsendinfor.Visible = false;
        }
        USERTK = GetParamsPage(IPC.ID)[0].Trim();
        txtCustomerCode.Text = "R110000041";
        string CustType = string.Empty;
        userTableTemp = (new SmartPortal.SEMS.User().GetFullUserByUID(USERTK, ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
        if (userTableTemp.Rows.Count != 0)
        {
            ltrError.Text = string.Empty;
            lblAlert.Text = string.Empty;
            lblError.Text = string.Empty;
            CONTRACTNO = contractNo = userTableTemp.Rows[0]["CONTRACTNO"].ToString();
            DataColumnCollection columns = userTableTemp.Columns;
            userType = userTableTemp.Rows[0]["USERTYPE"].ToString();
            CONTRACTTYPE = corptype = userTableTemp.Rows[0]["CONTRACTTYPE"].ToString();
            PRODUCTID = productType = userTableTemp.Rows[0]["PRODUCTID"].ToString();
            userTableTemp.Rows[0]["CFTYPE"].ToString().Trim();
            userTableTemp.Rows[0]["CUSTCODE"].ToString().Trim();
            DataTable dtProduct = new SmartPortal.SEMS.Product().GetProductByCondition(productType.Trim(), "", CustType, "", ref IPCERRORCODE, ref IPCERRORDESC).Tables[0];
            string result = dtProduct.Rows[0]["PRODUCTTYPE"].ToString();
        }
        else
        {
            userName = DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - 8, 8);
        }
    }
    void SendInfoLogin(string userID)
    {
        try
        {


            Antlr3.ST.StringTemplate tmpl = new Antlr3.ST.StringTemplate();
            tmpl = SmartPortal.Common.ST.GetStringTemplate("SEMSUserApprove", "UserApprove" + System.Globalization.CultureInfo.CurrentCulture.ToString());
            //minh add parameter 27/7/2015 for sms user info
            //declare parameters for sms contract

            string IBuser = "";
            string IBpass = "";
            string SMSphone = "";
            string PhoneMB = string.Empty;
            string PincodeMB = string.Empty;
            string PassMB = string.Empty;
            string PasswordMB = string.Empty;
            string UsernameMB = string.Empty;
            string SMSdefaultAccount = "";
            string SMSpincode = "";
            string PHOuser = "";
            string PHOpass = "";
            string PHOpincode = "";
            string PhoneNo = "";
            string authenType = string.Empty;
            string loginMethod = string.Empty;
            DataSet ContrNoDS = new SmartPortal.IB.Transactions().GetUserOfContractNoByUID(Utility.KillSqlInjection(userID), "", ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE != "0")
            {
                throw new Exception();
            }
            DataTable ContrNoDT = new DataTable();
            ContrNoDT = ContrNoDS.Tables[0];
            if (ContrNoDT.Rows.Count != 0)
            {
                contractNo = ContrNoDT.Rows[0]["CONTRACTNO"].ToString();
            }
            //lay thong tin hop dong de gui mail       
            DataSet UserContractTable = new DataSet();
            try
            {

                tmpl.Reset();

                //lay thong tin tai khoan
                DataTable userTable = (new SmartPortal.SEMS.User().GetFullUserByUID(userID, ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
                if (IPCERRORCODE != "0")
                {
                    goto ERROR;
                }


                //gan thong tin user vao stringtemplate
                foreach (DataRow row in userTable.Rows)
                {
                    //edit by vutran 11102014
                    //Kiem tra xem khach hang da dung IBMBSMS_Banking chua
                    bool boolIB = new SmartPortal.SEMS.User().CheckServiceIsUsed(row["USERID"].ToString().Trim(), "IB", ref IPCERRORCODE, ref IPCERRORDESC);
                    bool boolMB = new SmartPortal.SEMS.User().CheckServiceIsUsed(row["USERID"].ToString().Trim(), "MB", ref IPCERRORCODE, ref IPCERRORDESC);
                    bool boolAM = new SmartPortal.SEMS.User().CheckServiceIsUsed(row["USERID"].ToString().Trim(), "AM", ref IPCERRORCODE, ref IPCERRORDESC);
                    bool boolSMS = new SmartPortal.SEMS.User().CheckServiceIsUsed(row["USERID"].ToString().Trim(), "SMS", ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE != "0")
                    {
                        goto ERROR;
                    }

                    StringBuilder st = new StringBuilder();

                    st.Append("<table style='width:100%;'>");

                    #region lay thong tin khach hang
                    st.Append("<tr>");
                    st.Append("<td>");
                    st.Append(Resources.labels.tendaydu + " :");
                    st.Append("</td>");
                    st.Append("<td>");
                    st.Append(row["FULLNAME"].ToString());
                    st.Append("</td>");
                    st.Append("<td>");
                    st.Append("Email :");
                    st.Append("</td>");
                    st.Append("<td>");
                    st.Append(row["EMAIL"].ToString());
                    st.Append("</td>");
                    st.Append("</tr>");

                    st.Append("<tr>");
                    st.Append("<td>");
                    st.Append(Resources.labels.dienthoai + " :");
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
                    st.Append("<br><br>");
                    #endregion

                    if (boolIB)
                    {
                        #region lay het các tai khoan Ibank cua user theo userID
                        DataSet accountIBDataset = new SmartPortal.SEMS.User().GetUserRoleByServiceID(SmartPortal.Constant.IPC.IB, row["USERID"].ToString().Trim(), string.Empty, ref IPCERRORCODE, ref IPCERRORDESC);

                        if (IPCERRORCODE != "0")
                        {
                            goto ERROR;
                        }

                        DataTable accountIBTable = accountIBDataset.Tables[0];
                        if (accountIBTable.Rows.Count != 0)
                        {
                            IBpass = string.IsNullOrEmpty(accountIBTable.Rows[0]["PASSWORD"] as string) ? "******" : SmartPortal.Security.Encryption.Decrypt(accountIBTable.Rows[0]["PASSWORD"].ToString());
                            st.Append("<tr>");
                            st.Append("<td colspan='4'>");
                            st.Append("<B>INTERNET BANKING</B><hr/>");
                            st.Append("</td>");
                            st.Append("</tr>");

                            st.Append("<tr>");
                            st.Append("<td>");
                            st.Append(Resources.labels.username);
                            st.Append("</td>");
                            st.Append("<td>");
                            st.Append("<b>" + accountIBTable.Rows[0]["USERNAME"].ToString() + "</b>");
                            st.Append("</td>");
                            st.Append("<td>");
                            st.Append(Resources.labels.matkhau);
                            st.Append("</td>");
                            st.Append("<td>");
                            st.Append("<b>" + IBpass + "</b>");
                            st.Append("</td>");
                            st.Append("</tr>");
                            st.Append("<br><br>");
                            IBuser = accountIBTable.Rows[0]["USERNAME"].ToString();


                        }
                        #endregion
                    }

                    if (boolSMS)
                    {
                        #region lay het các tai khoan SMS cua user theo userID
                        DataSet accountSMSDataset = new SmartPortal.SEMS.User().GetUserRoleByServiceID(SmartPortal.Constant.IPC.SMS, row["USERID"].ToString().Trim(), string.Empty, ref IPCERRORCODE, ref IPCERRORDESC);

                        if (IPCERRORCODE != "0")
                        {
                            goto ERROR;
                        }

                        DataTable accountSMSTable = accountSMSDataset.Tables[0];
                        if (accountSMSTable.Rows.Count != 0)
                        {
                            SMSpincode = string.IsNullOrEmpty(accountSMSTable.Rows[0]["PINCODE"] as string) ? "******" : SmartPortal.Security.Encryption.Decrypt(accountSMSTable.Rows[0]["PINCODE"].ToString());
                            st.Append("<tr>");
                            st.Append("<td colspan='4'>");
                            st.Append("<B>SMS BANKING</B><hr/>");
                            st.Append("</td>");
                            st.Append("</tr>");

                            st.Append("<tr>");
                            st.Append("<td>");
                            st.Append(Resources.labels.phone);
                            st.Append("</td>");
                            st.Append("<td>");
                            st.Append("<b>" + accountSMSTable.Rows[0]["UN"].ToString() + "</b>");
                            st.Append("</td>");
                            st.Append("<td>");
                            st.Append(Resources.labels.taikhoanmacdinh);
                            st.Append("</td>");
                            st.Append("<td>");
                            st.Append("<b>" + accountSMSTable.Rows[0]["DEFAULTACCTNO"].ToString() + "</b>");
                            st.Append("</td>");
                            st.Append("</tr>");

                            st.Append("<tr>");
                            st.Append("<td>");
                            st.Append(Resources.labels.mapin + " :");
                            st.Append("</td>");
                            st.Append("<td>");
                            //st.Append("<b>" + accountSMSTable.Rows[0]["PINCODE"].ToString() + "</b>");
                            st.Append("<b>" + SMSpincode + "</b>");
                            st.Append("</td>");
                            st.Append("<td>");
                            st.Append("");
                            st.Append("</td>");
                            st.Append("<td>");
                            st.Append("");
                            st.Append("</td>");
                            st.Append("</tr>");
                            st.Append("<br><br>");
                            SMSphone = accountSMSTable.Rows[0]["UN"].ToString();
                            SMSdefaultAccount = accountSMSTable.Rows[0]["DEFAULTACCTNO"].ToString();
                            //SMSpincode = accountSMSTable.Rows[0]["PINCODE"].ToString();


                        }
                        #endregion
                    }
                    #region lay het các tai khoan MB cua user theo userID
                    DataSet accountMBDataset;
                    accountMBDataset = new SmartPortal.SEMS.User().GetUserSendInfor(SmartPortal.Constant.IPC.MB, row["USERID"].ToString().Trim(), ref IPCERRORCODE, ref IPCERRORDESC);

                    if (IPCERRORCODE != "0")
                    {
                        goto ERROR;
                    }
                    DataTable accountMBTable = accountMBDataset.Tables[0];
                    if (accountMBTable.Rows.Count != 0)
                    {
                        UsernameMB = accountMBTable.Rows[0]["USERNAME"].ToString();
                        PhoneMB = accountMBTable.Rows[0]["UN"].ToString();
                        if (!accountMBTable.Rows[0]["PASS"].ToString().Equals(""))
                        {
                            PassMB = SmartPortal.Security.Encryption.Decrypt(accountMBTable.Rows[0]["PASS"].ToString());
                        }
                        if (!accountMBTable.Rows[0]["PINCODE"].ToString().Equals(""))
                        {
                            PincodeMB = SmartPortal.Security.Encryption.Decrypt(accountMBTable.Rows[0]["PINCODE"].ToString());
                        }
                        authenType = accountMBTable.Rows[0]["AUTHENTYPE"].ToString();
                        loginMethod = accountMBTable.Rows[0]["LOGINMETHOD"].ToString();

                        st.Append("<tr>");
                        st.Append("<td colspan='4'>");
                        st.Append("<B>MOBILE BANKING</B><hr/>");
                        st.Append("</td>");
                        st.Append("</tr>");

                        st.Append("<tr>");
                        if (loginMethod.Equals("PHONENO"))
                        {
                            st.Append("<td colspan=1>  ");
                            st.Append(Resources.labels.phone);
                            st.Append("</td>");
                            st.Append("<td colspan=1>  ");
                            st.Append("<b>" + accountMBTable.Rows[0]["UN"].ToString() + "</b>");
                            st.Append("</td>");
                        }
                        else
                        {
                            st.Append("<td colspan=1>  ");
                            st.Append(Resources.labels.username + " :");
                            st.Append("</td>");
                            st.Append("<td colspan=1>  ");
                            st.Append("<b>" + accountMBTable.Rows[0]["USERNAME"].ToString() + "</b>");
                            st.Append("</td>");
                        }
                        if (!accountMBTable.Rows[0]["PASS"].ToString().Equals(""))
                        {
                            st.Append("<td colspan=1>  ");
                            st.Append(Resources.labels.matkhau);
                            st.Append("</td>");
                            st.Append("<td colspan=1>  ");
                            st.Append("<b>" + accountMBTable.Rows[0]["PASS"].ToString() == "" ? "******" : SmartPortal.Security.Encryption.Decrypt(accountMBTable.Rows[0]["PASS"].ToString()) + "</b>");
                            st.Append("</td>");
                        }
                        st.Append("</tr>");

                        if (!accountMBTable.Rows[0]["PINCODE"].ToString().Equals(""))
                        {
                            st.Append("<tr>");
                            st.Append("<td colspan=1>  ");
                            st.Append(Resources.labels.pincode + " :");
                            st.Append("</td>");
                            st.Append("<td colspan=1>  ");
                            st.Append("<b>" + accountMBTable.Rows[0]["PINCODE"].ToString() == "" ? "******" : SmartPortal.Security.Encryption.Decrypt(accountMBTable.Rows[0]["PINCODE"].ToString()) + "</b>");
                            st.Append("</td>");
                            st.Append("<td colspan=1>  ");
                            st.Append("</td>");
                            st.Append("<td colspan=1>  ");
                            st.Append("</td>");
                            st.Append("</tr>");
                        }
                        boolMB = true;
                    }
                    #endregion

                    #region lay het các tai khoan AM cua user theo userID
                    DataSet accountAMDataset = new SmartPortal.SEMS.User().GetUserRoleByServiceID(SmartPortal.Constant.IPC.AM, row["USERID"].ToString().Trim(), SmartPortal.Constant.IPC.MBA, ref IPCERRORCODE, ref IPCERRORDESC);

                    if (IPCERRORCODE != "0")
                    {
                        goto ERROR;
                    }

                    DataTable accountAMTable = accountAMDataset.Tables[0];
                    if (accountAMTable.Rows.Count != 0)
                    {
                        UsernameMB = accountAMTable.Rows[0]["USERNAME"].ToString();
                        PhoneMB = accountAMTable.Rows[0]["UN"].ToString();
                        if (!accountAMTable.Rows[0]["PASS"].ToString().Equals(""))
                        {
                            PassMB = SmartPortal.Security.Encryption.Decrypt(accountAMTable.Rows[0]["PASS"].ToString());
                        }
                        if (!accountAMTable.Rows[0]["PINCODE"].ToString().Equals(""))
                        {
                            PincodeMB = SmartPortal.Security.Encryption.Decrypt(accountAMTable.Rows[0]["PINCODE"].ToString());
                        }
                        authenType = accountAMTable.Rows[0]["AUTHENTYPE"].ToString();
                        loginMethod = accountAMTable.Rows[0]["LOGINMETHOD"].ToString();

                        st.Append("<tr>");
                        st.Append("<td colspan='4'>");
                        st.Append("<B>MOBILE BANKING</B><hr/>");
                        st.Append("</td>");
                        st.Append("</tr>");

                        st.Append("<tr>");
                        if (loginMethod.Equals("PHONENO"))
                        {
                            st.Append("<td>");
                            st.Append(Resources.labels.phone);
                            st.Append("</td>");
                            st.Append("<td>");
                            st.Append("<b>" + accountAMTable.Rows[0]["UN"].ToString() + "</b>");
                            st.Append("</td>");
                        }
                        else
                        {
                            st.Append("<td colspan=1>  ");
                            st.Append(Resources.labels.username + " :");
                            st.Append("</td>");
                            st.Append("<td colspan=1>  ");
                            st.Append("<b>" + accountAMTable.Rows[0]["USERNAME"].ToString() + "</b>");
                            st.Append("</td>");
                        }
                        if (!accountAMTable.Rows[0]["PASS"].ToString().Equals(""))
                        {
                            st.Append("<td colspan=1>  ");
                            st.Append(Resources.labels.matkhau);
                            st.Append("</td>");
                            st.Append("<td colspan=1>  ");
                            st.Append("<b>" + accountAMTable.Rows[0]["PASS"].ToString() == "" ? "******" : SmartPortal.Security.Encryption.Decrypt(accountAMTable.Rows[0]["PASS"].ToString()) + "</b>");
                            st.Append("</td>");
                            st.Append("</tr>");
                        }
                        if (!accountAMTable.Rows[0]["PINCODE"].ToString().Equals(""))
                        {
                            st.Append("<tr>");
                            st.Append("<td colspan=1>  ");
                            st.Append(Resources.labels.pincode + " :");
                            st.Append("</td>");
                            st.Append("<td colspan=1>  ");
                            st.Append("<b>" + accountAMTable.Rows[0]["PINCODE"].ToString() == "" ? "******" : SmartPortal.Security.Encryption.Decrypt(accountAMTable.Rows[0]["PINCODE"].ToString()) + "</b>");
                            st.Append("</td>");
                            st.Append("<td colspan=1>  ");
                            st.Append("</td>");
                            st.Append("<td colspan=1>  ");
                            st.Append("</td>");
                            st.Append("</tr>");
                        }
                        boolAM = true;
                    }
                    #endregion

                    if (ConfigurationManager.AppSettings["tabPhoneVisibility"] == "1")
                    {
                        #region lay het các tai khoan PHO cua user theo userID
                        DataSet accountPHODataset = new SmartPortal.SEMS.User().GetUserRoleByServiceID(SmartPortal.Constant.IPC.PHO, row["USERID"].ToString().Trim(), string.Empty, ref IPCERRORCODE, ref IPCERRORDESC);

                        if (IPCERRORCODE != "0")
                        {
                            goto ERROR;
                        }

                        DataTable accountPHOTable = accountPHODataset.Tables[0];
                        if (accountPHOTable.Rows.Count != 0)
                        {
                            st.Append("<tr>");
                            st.Append("<td colspan='4'>");
                            st.Append("<B>PHONE</B><hr/>");
                            st.Append("</td>");
                            st.Append("</tr>");

                            st.Append("<tr>");
                            st.Append("<td>");
                            st.Append(Resources.labels.username);
                            st.Append("</td>");
                            st.Append("<td>");
                            st.Append("<b>" + accountPHOTable.Rows[0]["UN"].ToString() + "</b>");
                            st.Append("</td>");
                            st.Append("<td>");
                            st.Append(Resources.labels.matkhau);
                            st.Append("</td>");
                            st.Append("<td>");
                            st.Append("<b>" + SmartPortal.Security.Encryption.Decrypt(accountPHOTable.Rows[0]["PASS"].ToString()) + "</b>");
                            st.Append("</td>");
                            st.Append("</tr>");
                        }

                        #endregion
                    }


                    st.Append("</table>");

                    tmpl.SetAttribute("USERINFO", st.ToString());

                    string str = "";
                    str += "<div><h1>" + Resources.labels.nganhangphuongnam.ToUpper() + "</h1><br/><br/></div>";
                    str += tmpl.ToString();
                    str += "<div><br/><p><a href='https://www.scb.co.th/en/personal-banking.html' target='blank'></a></p><span style='font-weight:bold;'>" + "Thank You !" + "!	</span></div>";
                    PhoneNo = PhoneMB;
                    //truong hop da login ib, chưa login MB
                    //if (IBuser == string.Empty)
                    //{
                    //    IBuser = PHOuser;
                    //    IBpass = "******";
                    //}
                    //17.9.2015 minh modify template sms message. this template generate from code
                    string SENDINFO = string.Empty;
                    string ibuserinfo = string.Empty;
                    string mbuserinfo = string.Empty;
                    string smsuserinfo = string.Empty;
                    string contractinfo = string.Empty;
                    contractinfo = "Contract info: Contractno." + contractNo;
                    if (!IBuser.Equals(string.Empty) || !PhoneNo.Equals(string.Empty))
                    {
                       
                        if (!ConfigurationManager.AppSettings["IBMBSamePassword"].ToString().Equals("1"))
                        {
                            ibuserinfo = " /IB:User-" + IBuser + ", password-" + IBpass;
                            if (!UsernameMB.Equals(""))
                            {
                                if (loginMethod.Equals("PHONENO"))
                                {
                                    mbuserinfo = "/MB:PhoneNo-" + PhoneMB;
                                }
                                else
                                {
                                    mbuserinfo = "/ :UserName-" + UsernameMB;
                                }
                            }
                            else
                            {
                                mbuserinfo = " /MB:PhoneNo-" + PhoneMB + ", password-" + PassMB;
                            }
                            if (!PincodeMB.Equals(""))
                            {
                                mbuserinfo += ", Pincode-" + PincodeMB;
                            }
                            if (!PassMB.Equals(""))
                            {
                                mbuserinfo += ", Password-" + PassMB;
                            }
                        }
                        else
                        {
                            ibuserinfo = " MB/IB:User-" + IBuser + ", password-" + IBpass;
                        }
                        SENDINFO = contractinfo + ibuserinfo + smsuserinfo + mbuserinfo;
                    }
                    //send mail
                    //SmartPortal.Common.EmailHelper.SendMailMessageAsync(System.Configuration.ConfigurationManager.AppSettings["contractapprovemailfrom"], row["EMAIL"].ToString(), System.Configuration.ConfigurationManager.AppSettings["contractapprovemailtitle"], str);
                    string errorcode = "";
                    string errorDesc = "";
                    //24/7/2015 minh add to send sms contract
                    switch (ddlSendinfo.SelectedValue)
                    {
                        case SmartPortal.Constant.IPC.EMAIL:
                            //SmartPortal.Common.EmailHelper.SendMailMessageAsync(strEmailFrom, strEmailTo, strSubject, strBody, strAttachment, strAttachmentFileName);
                            SmartPortal.Common.EmailHelper.SendMailMessageAsync(System.Configuration.ConfigurationManager.AppSettings["contractapprovemailfrom"], row["EMAIL"].ToString(), System.Configuration.ConfigurationManager.AppSettings["contractapprovemailtitle"], str);
                            SmartPortal.Common.Log.WriteLogFile("EMAIL LOG", "", "", "sent to " + row["EMAIL"].ToString());
                            break;
                        case SmartPortal.Constant.IPC.SMS:
                            SmartPortal.SEMS.Contract.sendSMS_Contract(PhoneNo, SENDINFO, ref errorcode, ref errorDesc);
                            SmartPortal.Common.Log.WriteLogFile("SMS LOG", "", "", "sent to " + PhoneNo);
                            //SmartPortal.SEMS.Contract.sendSMS_Contract(PhoneNo, contractNo, IBuser, IBpass, SMSphone, SMSdefaultAccount, SMSpincode, PHOuser, PHOpass, PHOpincode, ref errorcode, ref errorDesc);
                            break;
                        default://send both sms and email
                            SmartPortal.Common.EmailHelper.SendMailMessageAsync(System.Configuration.ConfigurationManager.AppSettings["contractapprovemailfrom"], row["EMAIL"].ToString(), System.Configuration.ConfigurationManager.AppSettings["contractapprovemailtitle"], str);
                            //SmartPortal.Common.EmailHelper.SendMailMessageAsync(strEmailFrom, strEmailTo, strSubject, strBody, strAttachment, strAttachmentFileName);
                            SmartPortal.SEMS.Contract.sendSMS_Contract(PhoneNo, SENDINFO, ref errorcode, ref errorDesc);
                            //SmartPortal.SEMS.Contract.sendSMS_Contract(PhoneNo, contractNo, IBuser, IBpass, SMSphone, SMSdefaultAccount, SMSpincode, PHOuser, PHOpass, PHOpincode, ref errorcode, ref errorDesc);
                            SmartPortal.Common.Log.WriteLogFile("EMAIL+SMS LOG", "", "", "sent mail to " + row["EMAIL"].ToString() + " sms send to " + PhoneNo);
                            break;
                    }


                N:
                    tmpl.RemoveAttribute("USERINFO");
                }
                goto EXIT;

            }
            catch (Exception ex)
            {
                SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSUserApprove_Widget", "SendInfoLogin", ex.ToString(), Request.Url.Query);
                SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

            }
        ERROR:
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], "Widgets_SEMSUserApprove_Widget", "SendInfoLogin", IPCERRORDESC, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);
        EXIT:

            ;
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.WriteLogFile(ex.ToString(), "SendInfoLogin", "", userID);
        }
    }
    private void LoadddlSendInfo()
    {
        bool gotemail = false;
        bool gotsms = false;
        if (txtReEmail.Text.Trim() != string.Empty)
        {
            gotemail = true;
        }
        #region Lấy thông tin tài khoản User
        DataTable tblMB = new SmartPortal.IB.User().GetAccountDetail(USERTK, SmartPortal.Constant.IPC.MB);
        if (tblMB.Rows.Count != 0)
        {
            gotsms = true;
        }
        if (gotemail && gotsms)
        {
            ddlSendinfo.Items.Add(new ListItem(Resources.labels.sendcontractinforemail, SmartPortal.Constant.IPC.EMAIL));
            ddlSendinfo.Items.Add(new ListItem(Resources.labels.sendcontractinforsms, SmartPortal.Constant.IPC.SMS));
            ddlSendinfo.Items.Add(new ListItem(Resources.labels.sendcontractinforboth, SmartPortal.Constant.IPC.BOTH));
        }
        else
        if (gotemail && !gotsms)
        {
            ddlSendinfo.Items.Add(new ListItem(Resources.labels.sendcontractinforemail, SmartPortal.Constant.IPC.EMAIL));
        }
        else
        if (!gotemail && gotsms)
        {
            ddlSendinfo.Items.Add(new ListItem(Resources.labels.sendcontractinforsms, SmartPortal.Constant.IPC.SMS));
        }
        else
        {
            tblsendinfor.Visible = false;
        }
        #endregion
    }
}

