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

public partial class Widgets_SEMSUser_AddAgent_Widget : WidgetBase
{
    private static int NumberReg = 0;
    string ACTION = "";
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    DataTable userTable = new DataTable();
    DataTable userTableTemp = new DataTable();
    DataTable tblRoleDefault = new DataTable();
    internal int minlength = int.Parse(ConfigurationManager.AppSettings["minlengthloginname"].ToString());
    internal int maxlength = int.Parse(ConfigurationManager.AppSettings["maxlengthloginname"].ToString());
    static List<string> lsAccNo = new List<string>();
    string contractNo = "";
    string userName = "";
    string userType = "";
    string productType = "";
    //string productid = "";
    string corptype = "";
    #region BIEN
    string USERNAMEMB
    {
        get
        {
            return ViewState["USERNAMEMB"] != null ? ViewState["USERNAMEMB"].ToString() : "";
        }
        set { ViewState["USERNAMEMB"] = value; }
    }
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
            return ViewState["USERTK"] != null ? ViewState["USERTK"].ToString() : string.Empty;
        }
        set { ViewState["USERTK"] = value; }

    }
    public string CONTRACTTYPE
    {
        get { return ViewState["CONTRACTTYPE"] != null ? (string)ViewState["CONTRACTTYPE"] : string.Empty; }
        set { ViewState["CONTRACTTYPE"] = value; }
    }
    public string USERTYPE
    {
        get
        {
            return ViewState["USERTYPE"] != null ? ViewState["USERTYPE"].ToString() : "";
        }
        set { ViewState["USERTYPE"] = value; }
    }
    string TYPEID
    {
        get
        {
            return ViewState["TYPEID"] != null ? ViewState["TYPEID"].ToString() : "";
        }
        set { ViewState["TYPEID"] = value; }
    }
    string birthdayOld
    {
        get { return ViewState["birthdayOld"] != null ? ViewState["birthdayOld"].ToString() : ""; }
        set { ViewState["birthdayOld"] = value; }
    }
    string emailOld
    {
        get { return ViewState["emailOld"] != null ? ViewState["emailOld"].ToString() : ""; }
        set { ViewState["emailOld"] = value; }
    }

    string genderOld
    {
        get { return ViewState["genderOld"] != null ? ViewState["genderOld"].ToString() : ""; }
        set { ViewState["genderOld"] = value; }
    }
    string phoneOld
    {
        get { return ViewState["phoneOld"] != null ? ViewState["phoneOld"].ToString() : ""; }
        set { ViewState["phoneOld"] = value; }
    }
    string addressOld
    {
        get { return ViewState["addressOld"] != null ? ViewState["addressOld"].ToString() : ""; }
        set { ViewState["addressOld"] = value; }
    }
    public bool walletOnly
    {
        get { return ViewState["WALLETONLY"] != null ? (bool)ViewState["WALLETONLY"] : false; }
        set { ViewState["WALLETONLY"] = value; }
    }
    public string localFullName
    {
        get { return ViewState["LOCALFULLNAME"] != null ? (string)ViewState["LOCALFULLNAME"] : string.Empty; }
        set { ViewState["LOCALFULLNAME"] = value; }
    }
    public string CUSTID
    {
        get { return ViewState["CUSTID"] != null ? (string)ViewState["CUSTID"] : ""; }
        set { ViewState["CUSTID"] = value; }
    }
    public string ISSUEDATE
    {
        get { return ViewState["ISSUEDATE"] != null ? (string)ViewState["ISSUEDATE"] : ""; }
        set { ViewState["ISSUEDATE"] = value; }
    }
    public string SEX
    {
        get { return ViewState["SEX"] != null ? (string)ViewState["SEX"] : ""; }
        set { ViewState["SEX"] = value; }
    }
    public string ISSUEPLACE
    {
        get { return ViewState["ISSUEPLACE"] != null ? (string)ViewState["ISSUEPLACE"] : ""; }
        set { ViewState["ISSUEPLACE"] = value; }
    }
    public string LICENSEID
    {
        get { return ViewState["LICENSEID"] != null ? (string)ViewState["LICENSEID"] : ""; }
        set { ViewState["LICENSEID"] = value; }
    }

    public string CUSTCODE
    {
        get { return ViewState["CUSTCODE"] != null ? (string)ViewState["CUSTCODE"] : ""; }
        set { ViewState["CUSTCODE"] = value; }
    }
    public string CFTYPE
    {
        get { return ViewState["CFTYPE"] != null ? (string)ViewState["CFTYPE"] : ""; }
        set { ViewState["CFTYPE"] = value; }
    }
    public string USERWALLETONLY
    {
        get { return ViewState["USERWALLETONLY"] != null ? (string)ViewState["USERWALLETONLY"] : ""; }
        set { ViewState["USERWALLETONLY"] = value; }
    }
    public string ACCOUNTWL
    {
        get { return ViewState["ACCOUNTWL"] != null ? (string)ViewState["ACCOUNTWL"] : ""; }
        set { ViewState["ACCOUNTWL"] = value; }
    }
    public string SHOWTAB
    {
        get { return ViewState["SHOWTAB"] != null ? (string)ViewState["SHOWTAB"] : string.Empty; }
        set { ViewState["SHOWTAB"] = value; }
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
    public DataTable TBLROLE
    {
        get { return ViewState["TBLROLE"] != null ? (DataTable)ViewState["TBLROLE"] : new DataTable(); }
        set { ViewState["TBLROLE"] = TBLROLE; }
    }
    #endregion
    //Haint 02/08/2013
    protected void Page_Init(object sender, EventArgs e)
    {
        TabCustomerInfoHelper.LoadConfig();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {


            ACTION = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["a"].ToString().Trim();

            //Lannth - 27.9.2018 - load user and contract
            userTableTemp = GetUserContract();
            if (userTableTemp.Rows.Count != 0)
            {
                ltrError.Text = string.Empty;
                lblAlert.Text = string.Empty;
                lblError.Text = string.Empty;
                CONTRACTNO = contractNo = userTableTemp.Rows[0]["CONTRACTNO"].ToString();
                //userName = userTableTemp.Rows[0]["CUSTCODE"].ToString().Trim() + userTableTemp.Rows[0]["CFTYPE"].ToString().Trim() + DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - 6, 4);

                userName = GenerateUserName();
                //productid = userTableTemp.Rows[0]["PRODUCTID"].ToString();
                DataColumnCollection columns = userTableTemp.Columns;
                if (columns.Contains("PHONE"))
                {
                    PHONE = txtAMPhoneNo.Text = ACCOUNTWL = userTableTemp.Rows[0]["PHONE"].ToString().Trim();
                }
                userType = userTableTemp.Rows[0]["USERTYPE"].ToString();
                USERWALLETONLY = userTableTemp.Rows[0]["USERID"].ToString();
                CONTRACTTYPE = corptype = userTableTemp.Rows[0]["CONTRACTTYPE"].ToString();
                PRODUCTID = productType = userTableTemp.Rows[0]["PRODUCTID"].ToString();
                CUSTCODE = userTableTemp.Rows[0]["CUSTCODE"].ToString();
                string CustType = userTableTemp.Rows[0]["CFTYPE"].ToString().Trim();

                DataTable dtProduct = new SmartPortal.SEMS.Product().GetProductByCondition(productType.Trim(), "", CustType, "", ref IPCERRORCODE, ref IPCERRORDESC).Tables[0];
                string result = dtProduct.Rows[0]["PRODUCTTYPE"].ToString();
                #region check Chu tk
                if (ACTION == SmartPortal.Constant.IPC.EDIT || ACTION == SmartPortal.Constant.IPC.VIEWDETAIL || ACTION == SmartPortal.Constant.IPC.APPROVER)
                {
                    string userid = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["uid"].ToString().Trim();
                    string filterCTK = "Userid='" + userid + "'";
                    string stSort = "Userid asc";

                    DataTable dtUser = userTableTemp.Select(filterCTK, stSort).Any() ? userTableTemp.Select(filterCTK, stSort).CopyToDataTable() : null;
                    if (dtUser.Rows.Count > 0)
                    {

                        TYPEID = dtUser.Rows[0]["TypeID"].ToString().Trim() == "" ? SmartPortal.Constant.IPC.CHUTAIKHOAN : dtUser.Rows[0]["TypeID"].ToString();
                        if (result == SmartPortal.Constant.IPC.PRCTYPECONSUMER && TYPEID == SmartPortal.Constant.IPC.CHUTAIKHOAN)
                        {
                            SHOWTAB = Resources.labels.walletbanking;
                            CONTRACTTYPE = SmartPortal.Constant.IPC.CONTRACTINDIVIDUAL;
                            if (userTableTemp.Rows[0]["CTYPE"].ToString() == "W")
                            {
                                walletOnly = true;
                                ddlLoginMethod.Enabled = true;
                            }
                        }
                        else
                        {
                            if (result == SmartPortal.Constant.IPC.PRCTYPECONSUMER)
                            {
                                SHOWTAB = string.Empty;
                                CONTRACTTYPE = SmartPortal.Constant.IPC.CONTRACTINDIVIDUAL;
                            }
                            else
                            {
                                SHOWTAB = Resources.labels.agentmerchant;
                            }
                        }

                    }
                }
                else
                {
                    if (result == SmartPortal.Constant.IPC.PRCTYPECONSUMER && userTableTemp.Rows[0]["CTYPE"].ToString().Trim().Equals("W"))
                    {
                        SHOWTAB = Resources.labels.walletbanking;
                        CONTRACTTYPE = SmartPortal.Constant.IPC.CONTRACTINDIVIDUAL;
                    }
                    else if (result == SmartPortal.Constant.IPC.PRCAGENTMERCHANT)
                    {
                        CONTRACTTYPE = SmartPortal.Constant.IPC.CONTRACTAGENTMERCHANT;
                        SHOWTAB = Resources.labels.agentmerchant;
                    }
                    else if(result == SmartPortal.Constant.IPC.PRCTYPECONSUMER)
                    {
                        CONTRACTTYPE = SmartPortal.Constant.IPC.CONTRACTINDIVIDUAL;
                        SHOWTAB = Resources.labels.walletbanking;
                    }
                    else
                    {
                        CONTRACTTYPE = SmartPortal.Constant.IPC.CONTRACTINDIVIDUAL;
                        SHOWTAB = string.Empty;
                    }
                }
                #endregion

            }

            if (!IsPostBack)
            {
                //23.2.2016 minh add policy
                //load policy to dropdownlist
                DataSet dspolicy = new DataSet();
                string filterIB = "serviceid='IB'";
                string filterSMS = "serviceid='SMS'";
                string filterMB = string.Empty;
                if (CONTRACTTYPE.Equals(SmartPortal.Constant.IPC.CONTRACTINDIVIDUAL))
                {
                    filterMB = "serviceid='MB'";
                    pnLoginMethod.Visible = true;
                }
                else
                {
                    filterMB = "serviceid='AM'";
                    pnLoginMethod.Visible = false;
                }
                string stSort = "serviceid asc";

                //lannth - 27.9.2018 - load va hien thi dropdown level, group voi contract la corp matrix
                if (corptype.Equals(SmartPortal.Constant.IPC.CONTRACTCORPMATRIX))
                {
                    LoadFormCorpMatrix(contractNo);
                }

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


                    ddlpolicyIB.DataTextField = "policytx";
                    ddlpolicyIB.DataValueField = "policyid";
                    ddlpolicySMS.DataTextField = "policytx";
                    ddlpolicySMS.DataValueField = "policyid";
                    ddlpolicyMB.DataTextField = "policytx";
                    ddlpolicyMB.DataValueField = "policyid";


                    ddlpolicyIB.DataBind();
                    ddlpolicySMS.DataBind();
                    ddlpolicyMB.DataBind();
                    ddlpolicyIB.Enabled = false;

                }

                if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cn"] == null)
                {
                    DataSet ContrNoDS = new SmartPortal.IB.Transactions().GetUserOfContractNoByUID(Utility.KillSqlInjection(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["uid"].ToString()), "", ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE != "0")
                    {
                        throw new IPCException("13");
                    }
                    DataTable ContrNoDT = new DataTable();
                    ContrNoDT = ContrNoDS.Tables[0];
                    if (ContrNoDT.Rows.Count != 0)
                    {
                        contractNo = ContrNoDT.Rows[0]["CONTRACTNO"].ToString();
                    }
                }
                else
                {

                    userTableTemp = (new SmartPortal.SEMS.Contract().GetUserByContractNo(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cn"].ToString(), "", "", ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];

                    if (userTableTemp.Rows.Count != 0)
                    {
                        contractNo = userTableTemp.Rows[0]["CONTRACTNO"].ToString();
                        userType = userTableTemp.Rows[0]["USERTYPE"].ToString();
                        productType = userTableTemp.Rows[0]["PRODUCTID"].ToString();
                        CUSTID = userTableTemp.Rows[0]["CUSTID"].ToString();
                    }

                    userTableTemp = userTableTemp.Select("TYPEID='CTK'").Any() ? userTableTemp.Select("TYPEID='CTK'").CopyToDataTable() : userTableTemp.Select("TYPEID=TYPEID").CopyToDataTable();
                    if (userTableTemp != null)
                    {
                        contractNo = userTableTemp.Rows[0]["CONTRACTNO"].ToString();

                        if (int.Parse(ConfigurationManager.AppSettings["MBAMSameUser"].ToString()) == 1 && SHOWTAB.Equals(Resources.labels.agentmerchant))
                        {
                            string accWL = new Customer().GetAccountWallet(contractNo, ref IPCERRORCODE, ref IPCERRORDESC);
                            ACCOUNTWL = accWL;
                            if (accWL.Equals(string.Empty))
                            {
                                lblError.Text = Resources.labels.youneedaccountwallet;
                                return;
                            }
                            txtAMPhoneNo.Text = PHONE = accWL;
                            txtAMPhoneNo.Enabled = false;
                        }

                        if (userTableTemp.Rows[0]["CTYPE"].ToString() == "W")
                        {
                            walletOnly = true;
                            SHOWTAB = Resources.labels.walletbanking;
                        }
                        if (walletOnly)
                            USERWALLETONLY = new Customer().GetUserIDByCustID(CUSTID, ref IPCERRORCODE, ref IPCERRORDESC);
                        walletOnly = userTableTemp.Rows[0]["CUSTCODE"].ToString() == string.Empty ? true : false;
                    }
                }

                if (ViewState["NGUOIQUANTRI"] == null)
                {
                    txtIBUserName.Text = userName;
                    txtIBUserName.Enabled = false;

                    #region load usertype
                    DataSet dsUserType = new DataSet();
                    DataTable dtUserType = new DataTable();

                    USERTYPE = userTableTemp.Rows[0]["USERTYPE"].ToString();
                    dtUserType = new SmartPortal.SEMS.Services().GetUserType(userTableTemp.Rows[0]["USERTYPE"].ToString(), string.Empty, ref IPCERRORCODE, ref IPCERRORDESC).Tables[0];

                    ddlUserType.DataSource = dtUserType;
                    ddlUserType.DataTextField = "SUBUSERTYPE";
                    ddlUserType.DataValueField = "SUBUSERCODE";
                    ddlUserType.DataBind();

                    DropDownListBranch.DataSource = new SmartPortal.SEMS.Branch().GetAll(ref IPCERRORCODE, ref IPCERRORDESC);
                    DropDownListBranch.DataTextField = "BRANCHNAME";
                    DropDownListBranch.DataValueField = "BRANCHID";
                    DropDownListBranch.DataBind();


                    #endregion

                    #region lay tat ca cac account cua khach hang
                    DataSet ds = new DataSet();


                    ds = new SmartPortal.SEMS.Customer().GetAcctNo(CUSTCODE, SmartPortal.Constant.IPC.PERSONAL, ref IPCERRORCODE, ref IPCERRORDESC);
                    DataTable dtAccount = new DataTable();
                    dtAccount = ds.Tables[0];

                    if (!dtAccount.Columns.Contains("ACCOUNTNO") || IPCERRORCODE != "0")
                    {
                        btsaveandcont.Visible = false;
                        lblError.Text = Resources.labels.khachhangnaykhongtontaitaikhoandedangky;
                        return;
                    }
                    ViewState["AccountList"] = dtAccount;


                    if (dtAccount.Rows.Count != 0)
                    {
                        if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["uid"] == null)
                        {
                            //Add new user
                            ACCOUNTWL = new Customer().GetAccountWallet(contractNo, ref IPCERRORCODE, ref IPCERRORDESC);
                            ddlAccountQT.DataSource = dtAccount;
                            ddlAccountQT.DataTextField = "ACCOUNTNO";
                            ddlAccountQT.DataValueField = "ACCOUNTNO";
                            ddlAccountQT.DataBind();
                            ddlAccountQT.Items.Insert(0, new ListItem("ALL", "ALL"));

                            ddlDefaultAccountQT.DataSource = dtAccount;
                            ddlDefaultAccountQT.DataTextField = "ACCOUNTNO";
                            ddlDefaultAccountQT.DataValueField = "ACCOUNTNO";
                            ddlDefaultAccountQT.DataBind();
                            ddlDefaultAccountQT.Items.Insert(0, new ListItem(PHONE, PHONE));
                        }
                    }

                    #endregion
                    ddlUserType_SelectedIndexChanged(sender, e);

                    BindData();
                    txtSMSPhoneNo.Enabled = false;
                }
                ShowTab();
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
        if (!CheckIsPhoneNumer(txtReMobi.Text.Trim().ToString()))
        {
            lblError.Text = Resources.labels.phonenumberwrong;
        }
        else if (!CheckExistPhoneNumber(txtReMobi.Text.Trim().ToString()))
        {
            lblError.Text = Resources.labels.phonenumberisalreadyregistered;
        }
        else
        {
            txtMBPhoneNo.Text = txtSMSPhoneNo.Text = txtReMobi.Text.Trim();
            btnThemNQT.Visible = true;
            ChangePhoneAndUserName_Click(sender, e);
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
    DataTable GetUserContract()
    {
        DataTable dtuser;
        if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cn"] != null)
        {
            //get contractno
            dtuser = (new SmartPortal.SEMS.Contract().GetUserByContractNo(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cn"].ToString(), "", "", ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
        }
        else
        {
            //get contractno
            dtuser = (new SmartPortal.SEMS.User().GetFullUserByUID(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["uid"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];

        }
        return dtuser;
    }

    private void ShowError(string msg = "")
    {
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ShowError", "ShowError('" + msg + "');", true);
    }
    private bool IsSuccess()
    {
        return IPCERRORCODE.Equals("0") ? true : false;
    }
    private bool IsDataSetNotNull(DataSet ds)
    {
        return ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 ? true : false;
    }

    void LoadDataInTreeview1(string serviceID, TreeView tvPage, string userType)
    {
        tvPage.Nodes.Clear();
        DataTable tblSS = new DataTable();
        tblSS = new SmartPortal.SEMS.Role().GetByServiceAndUserType(serviceID, userType, productType);
        //tblSS = (new SmartPortal.SEMS.Role().GetRoleDefaultByServiceID(serviceID, ddlProduct.SelectedValue, ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];

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
    }

    void BindData()
    {
        string service = string.Empty;
        //enable(disable) theo action
        switch (ACTION)
        {
            case SmartPortal.Constant.IPC.ADD:
                USERTK = new SmartPortal.SEMS.User().CreatePrimaryKey(string.Empty, string.Empty, "USERID", ref IPCERRORCODE, ref IPCERRORDESC);
                break;
            case SmartPortal.Constant.IPC.VIEWDETAIL:
            case SmartPortal.Constant.IPC.APPROVER:
                lblTitleUser.Text = Resources.labels.xemchitietthongtinnguoidung;
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
                btsaveandcont.Visible = false;
                btnThemNQT.Visible = false;
                //gvResultQuanTri.Columns[5].Visible = false;
                btnHuy.Visible = false;
                tvIB.Enabled = false;
                tvMBQT.Enabled = false;
                tvSMSQT.Enabled = false;
                tvAM.Enabled = false;
                tvWL.Enabled = false;
                ddlDefaultLang.Enabled = false;
                ddlSMSDefaultAcctno.Enabled = false;
                cbIsDefault.Enabled = false;
                btn_HuySMS.Visible = false;
                // btnSave.Visible = false;
                ddlpolicyIB.Enabled = false;
                ddlpolicySMS.Enabled = false;
                ddlpolicyMB.Enabled = false;
                ddlpolicyMB.Enabled = false;
                ddlGroup.Enabled = false;
                ddlAccountQT.Enabled = false;
                ddlDefaultAccountQT.Enabled = false;
                txtUserNameMB.Enabled = false;
                lbCreateusername.Visible = false;
                lblChangePhone.Visible = false;
                txtLocalFullName.Enabled = false;
                ddlLoginMethod.Enabled = false;
                break;
            case SmartPortal.Constant.IPC.EDIT:
                lblChangePhone.Visible = true;
                lblTitleUser.Text = Resources.labels.suadoithongtinnguoidung;
                txtReBirth.Enabled = true;
                txtReFullName.Enabled = false;
                txtReMobi.Enabled = true;
                ddlUserType.Enabled = true;
                ddlType.Enabled = true;
                txtReEmail.Enabled = true;
                ddlReGender.Enabled = true;
                ddlUserLevel.Enabled = true;
                txtReAddress.Enabled = true;
                txtIBUserName.Enabled = true;
                txtSMSPhoneNo.Enabled = true;
                txtReMobi.Enabled = false;
                ddlUserType.Enabled = false;
                ddlType.Enabled = false;
                btsaveandcont.Visible = true;
                //btn_HuySMS.Visible = true;
                ddlpolicyIB.Enabled = false;
                ddlpolicySMS.Enabled = true;
                ddlpolicyMB.Enabled = true;
                ddlpolicyMB.Enabled = false;
                ddlAccountQT.Enabled = true;
                ddlDefaultAccountQT.Enabled = true;
                lbCreateusername.Visible = true;
                break;
        }

        if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["uid"] != null)
        {
            #region Lấy thông tin user
            USERTK = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["uid"].ToString();

            userTable = (new SmartPortal.SEMS.User().GetFullUserByUID(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["uid"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];

            if (userTable.Rows.Count != 0)
            {
                //QuangTV - add old value to log
                emailOld = txtReEmail.Text = userTable.Rows[0]["EMAIL"].ToString();
                genderOld = ddlReGender.SelectedValue = userTable.Rows[0]["GENDER"].ToString();
                addressOld = txtReAddress.Text = userTable.Rows[0]["ADDRESS"].ToString();
                phoneOld = userTable.Rows[0]["PHONE"].ToString();
                if (PHONE == "")
                {
                    DataColumnCollection columns1 = userTableTemp.Columns;
                    if (columns1.Contains("TEL"))
                    {
                        txtAMPhoneNo.Text = ACCOUNTWL = PHONE = userTableTemp.Rows[0]["TEL"].ToString();
                    }
                }
                txtReFullName.Text = userTable.Rows[0]["FULLNAME"].ToString();
                txtReMobi.Text = userTable.Rows[0]["PHONE"].ToString();
                txtReEmail.Text = userTable.Rows[0]["EMAIL"].ToString();
                localFullName = txtLocalFullName.Text = userTable.Rows[0]["LOCALFULLNAME"].ToString();
                USERNAMEMB = txtUserNameMB.Text = userTable.Rows[0]["USERNAME"].ToString();
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
                    birthdayOld = txtReBirth.Text = birthDate.Equals("01/01/1900") ? "" : birthDate;
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
                if (userTable.Rows[0]["STATUS"].ToString().Equals(SmartPortal.Constant.IPC.REJECT) || userTable.Rows[0]["STATUS"].ToString().Equals(SmartPortal.Constant.IPC.REJECTFORMNEW))
                {
                    LoadRejectReason(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["uid"].ToString(), "CUSTOMER");
                    divReject.Visible = true;
                }
            }


            #endregion

            #region Lấy thông tin tài khoản User
            DataTable tblIB = new SmartPortal.IB.User().GetAccountDetail(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["uid"].ToString(), SmartPortal.Constant.IPC.IB);
            if (tblIB.Rows.Count != 0)
            {
                txtIBUserName.Text = tblIB.Rows[0]["USERNAME"].ToString().Trim();
                txtIBUserName.Enabled = false;
                //ddlStatusIB.SelectedValue = tblIB.Rows[0]["STATUS"].ToString().Trim();
                //txtIBPass.Attributes.Add("value", Encryption.Decrypt(tblIB.Rows[0]["PASSWORD"].ToString().Trim()));
                //txtIBRePass.Attributes.Add("value", Encryption.Decrypt(tblIB.Rows[0]["PASSWORD"].ToString().Trim()));
            }

            DataTable tblSMS = new SmartPortal.IB.User().GetAccountDetail(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["uid"].ToString(), SmartPortal.Constant.IPC.SMS);
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
            DataTable tblMB = new SmartPortal.IB.User().GetAccountDetail(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["uid"].ToString(), SmartPortal.Constant.IPC.MB);
            if (tblMB.Rows.Count != 0)
            {
                txtWLPhoneNo.Text = txtMBPhoneNo.Text = tblMB.Rows[0]["PHONENO"].ToString().Trim();
                txtUserID.Text = tblMB.Rows[0]["USERID"].ToString().Trim();
                service = SmartPortal.Constant.IPC.MB;
            }

            DataTable tblAM = new SmartPortal.IB.User().GetAccountDetail(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["uid"].ToString(), SmartPortal.Constant.IPC.AM);
            if (tblAM.Rows.Count != 0)
            {
                service = SmartPortal.Constant.IPC.AM;
                txtUserID.Text = tblAM.Rows[0]["USERID"].ToString().Trim();
                txtAMPhoneNo.Text = tblAM.Rows[0]["PHONENO"].ToString().Trim();
                txtMBPhoneNo.Text = tblAM.Rows[0]["PHONENO"].ToString().Trim();
            }
            #endregion


            //lannth - lay ra group cua user
            #region lay group cua user
            if (userTable.Rows.Count > 0 && userTable.Rows[0]["contracttype"].ToString().Equals(SmartPortal.Constant.IPC.MTRUSER))
            {
                DataTable tblGroup = new SmartPortal.SEMS.User().GetGroupByUser(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["uid"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC).Tables[0];
                if (tblGroup.Rows.Count != 0)
                {
                    ddlGroup.SelectedValue = tblGroup.Rows[0]["GroupID"].ToString().Trim();
                }
            }
            #endregion

            #region lấy thông tin tất cả các quyền
            DataTable tblRoleDetail = new SmartPortal.IB.User().GetRoleDetail(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["uid"].ToString());
            if (tblRoleDetail.Select("SERVICEID = 'MB' OR SERVICEID = 'AM'").Length > 0)
            {
                ViewState["IsUseMB"] = true;
            }
            else
            {
                ViewState["IsUseMB"] = false;
            }
            //luu thông tin vào session và hiển thị lên lưới
            DataTable tblNguoiUyQuyen = new DataTable();
            DataColumn colFullName = new DataColumn("colFullName");
            DataColumn colLocalname = new DataColumn("colLocalname");
            DataColumn colLoginMethod = new DataColumn("colLoginMethod");
            DataColumn colAuthenType = new DataColumn("colAuthenType");
            DataColumn colLevel = new DataColumn("colLevel");
            DataColumn colGroup = new DataColumn("colGroup");
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
            DataColumn colWLPass = new DataColumn("colWLPass");
            DataColumn colWLPinCode = new DataColumn("colWLPinCode");
            DataColumn colMBPinCode = new DataColumn("colMBPinCode");
            DataColumn colPHOPhone = new DataColumn("colPHOPhone");
            DataColumn colPHOPass = new DataColumn("colPHOPass");
            DataColumn colPHODefaultAcctno = new DataColumn("colPHODefaultAcctno");
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

            tblNguoiUyQuyen.Columns.Add(colGroup);
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
            tblNguoiUyQuyen.Columns.Add(colPHOPhone);
            tblNguoiUyQuyen.Columns.Add(colPHOPass);
            tblNguoiUyQuyen.Columns.Add(colPHODefaultAcctno);
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


            foreach (DataRow rRole in tblRoleDetail.Rows)
            {
                DataRow r = tblNguoiUyQuyen.NewRow();
                r["colFullName"] = txtReFullName.Text;
                r["colLocalname"] = txtLocalFullName.Text;
                r["colLoginMethod"] = ddlLoginMethod.SelectedValue;
                r["colAuthenType"] = ddlauthenType.SelectedValue;
                r["colLevel"] = ddlUserLevel.SelectedValue;
                r["colGroup"] = ddlGroup.SelectedValue;
                r["colBirthday"] = ddlUserLevel.SelectedValue;
                r["colGender"] = ddlReGender.SelectedValue;
                r["colPhone"] = txtReMobi.Text;
                r["colEmail"] = txtReEmail.Text;
                r["colAddress"] = txtReAddress.Text;
                r["colIBUserName"] = USERTK;
                //   r["colIBUserName"] = USERTK;
                r["colIBPass"] = DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - 8, 6);
                r["colSMSPhone"] = txtSMSPhoneNo.Text;
                r["colSMSDefaultAcctno"] = ddlSMSDefaultAcctno.SelectedValue;
                r["colSMSDefaultLang"] = ddlDefaultLang.SelectedValue;
                r["colMBPhone"] = txtReMobi.Text;
                r["colMBPass"] = DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - 8, 6);
                r["colRoleType"] = rRole["ROLETYPE"].ToString();
                if (CONTRACTTYPE.Equals(SmartPortal.Constant.IPC.CONTRACTINDIVIDUAL))
                {
                    r["colWLPhone"] = txtWLPhoneNo.Text;
                    liTabIB.Visible = true;
                }
                else
                {
                    liTabIB.Visible = false;
                    r["colWLPhone"] = txtAMPhoneNo.Text;
                }
                r["colWLPass"] = DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - 8, 6);
                r["colPHOPhone"] = "";
                r["colPHOPass"] = DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - 8, 6);
                r["colAccount"] = rRole["ACCTNO"].ToString();
                r["colRole"] = rRole["ROLENAME"].ToString();
                r["colRoleID"] = rRole["ROLEID"].ToString();
                r["colTranCode"] = rRole["PAGENAME"].ToString();
                r["colTranCodeID"] = rRole["IPCTRANCODE"].ToString();
                r["colServiceID"] = rRole["SERVICEID"].ToString();
                r["colIBPolicy"] = ddlpolicyIB.SelectedValue.ToString();
                r["colSMSPolicy"] = ddlpolicySMS.SelectedValue.ToString();
                r["colMBPolicy"] = ddlpolicyMB.SelectedValue.ToString();
                r["colWLPolicy"] = ddlpolicyMB.SelectedValue.ToString();
                r["colpwdreset"] = string.Empty;


                if (cbIsDefault.Checked)
                {
                    r["colSMSIsDefault"] = "Y";
                }
                else
                {
                    r["colSMSIsDefault"] = "N";
                }

                r["colSMSPinCode"] = "";
                r["colMBPinCode"] = "";

                tblNguoiUyQuyen.Rows.Add(r);
            }

            ViewState["NGUOIQUANTRI"] = tblNguoiUyQuyen;

            DataTable dtResultQuanTri = tblNguoiUyQuyen.DefaultView.ToTable(true, "colFullName", "colPhone", "colAccount", "colRole");
            ViewState["UserRolesOld"] = tblNguoiUyQuyen.DefaultView.ToTable(true, "colPhone", "colAccount", "colRoleType"); ;
            gvResultQuanTri.DataSource = dtResultQuanTri;
            gvResultQuanTri.DataBind();
            #endregion
        }
        else
        {
            ViewState["IsUseMB"] = false;
            ViewState["UserRolesOld"] = null;
        }
        #region Hien thi tat cac cac role theo serviceid va usertype len cay
        //string ct = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["ct"].ToString();
        //if (ct == SmartPortal.Constant.IPC.PERSONALCONTRACT || ct == SmartPortal.Constant.IPC.CONSUMER || ct == "A" || ct == "M")

        //{

        //load for IB
        LoadDataInTreeview(SmartPortal.Constant.IPC.IB, tvIB, contractNo, SmartPortal.Constant.IPC.NORMAL);

        //load for SMS
        LoadDataInTreeview(SmartPortal.Constant.IPC.SMS, tvSMSQT, contractNo, string.Empty);


        if (CONTRACTTYPE.Equals(SmartPortal.Constant.IPC.CONTRACTINDIVIDUAL))
        {
            LoadDataInTreeview(SmartPortal.Constant.IPC.MB, tvMBQT, contractNo, SmartPortal.Constant.IPC.NORMAL);
            LoadDataInTreeview("B", tvWL, contractNo, SmartPortal.Constant.IPC.WAL);
            if (ACTION == SmartPortal.Constant.IPC.VIEWDETAIL || ACTION == SmartPortal.Constant.IPC.EDIT || ACTION == SmartPortal.Constant.IPC.APPROVER)
            {
                GetRoleDefault(tvWL, SmartPortal.Constant.IPC.WAL, "B");
                GetRoleDefault(tvMBQT, SmartPortal.Constant.IPC.NORMAL, SmartPortal.Constant.IPC.MB);
            }
        }
        else
        {
            LoadDataInTreeview(SmartPortal.Constant.IPC.AM, tvAM, contractNo, SmartPortal.Constant.IPC.EAM);
            LoadDataInTreeview(SmartPortal.Constant.IPC.AM, tvMBQT, contractNo, SmartPortal.Constant.IPC.MBA);
            if (ACTION == SmartPortal.Constant.IPC.VIEWDETAIL || ACTION == SmartPortal.Constant.IPC.EDIT || ACTION == SmartPortal.Constant.IPC.APPROVER)
            {
                GetRoleDefault(tvAM, SmartPortal.Constant.IPC.EAM, SmartPortal.Constant.IPC.AM);
                GetRoleDefault(tvMBQT, SmartPortal.Constant.IPC.MBA, SmartPortal.Constant.IPC.AM);
            }

        }

        #endregion

        #region ẩn button huỷ sms
        switch (ACTION)
        {
            case SmartPortal.Constant.IPC.VIEWDETAIL:
                btn_HuySMS.Visible = false;
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
            txtWLPhoneNo.Text = PHONE;
            txtSMSPhoneNo.Text = PHONE;
            txtSMSPhoneNo.Enabled = false;
        }
        #endregion
        LoadComboboxReason();
    }
    void GetRoleDefault(TreeView tvRoles, string roleType, string Service)
    {
        DataTable tblRoleDefault = new DataTable();
        //lay role mac dinh MOBILE AM
        tblRoleDefault = (new SmartPortal.SEMS.Role().GetRolesByUserID(USERTK, Service, roleType, CONTRACTNO, ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];

        foreach (TreeNode liMB in tvRoles.Nodes)
        {
            DataRow dr = tblRoleDefault.Select("").FirstOrDefault(x => (string)x["ROLEID"] == liMB.Value);
            if (dr != null)
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
    }
    void LoadDataInTreeview(string serviceID, TreeView tvPage, string cn, string roleType)
    {
        tvPage.Nodes.Clear();
        DataTable tblSS = new DataTable();

        //DataSet ds=new SmartPortal.IB.Role().GetRoleByServiceID(serviceID, cn, ref IPCERRORCODE, ref IPCERRORDESC);
        tblSS = new SmartPortal.SEMS.Contract().GetRoleByServiceAndContract(serviceID, cn, roleType);
        if (serviceID == "B")
        {
            ViewState["TBLROLE"] = tblSS;
        }

        //if (IPCERRORCODE == "0")
        //{
        //    tblSS = ds.Tables[0];
        //}

        foreach (DataRow row in tblSS.Rows)
        {
            TreeNode node = new TreeNode(row["ROLENAME"].ToString(), row["ROLEID"].ToString());
            node.ShowCheckBox = true;
            //
            //8.12.2016 minh add to uncheck sms notify on edit contract
            if (serviceID.Equals(SmartPortal.Constant.IPC.SMS) && row["RoleType"].ToString().Equals("SNO"))
            {

                node.Checked = false;
            }
            else
                node.Checked = true;


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
                //
                node1.Checked = true;

                node.ChildNodes.Add(node1);
            }
            if (tblPage.Rows.Count != 0)
            {
                node.CollapseAll();
                tvPage.Nodes.Add(node);
            }
        }
        tvPage.Attributes.Add("onclick", "OnTreeClick(event)");


    }

    protected void btnThemNQT_Click(object sender, EventArgs e)
    {
        try
        {
            int passlenIB = 0;
            int passlenSMS = 0;
            int passlenMB = 0;
            int passlenWL = 0;
            PHONE = txtAMPhoneNo.Text.Trim();

            if (txtWLPhoneNo.Text == "")
            {
                lblAlert.Text = Resources.labels.bancannhapsodienthoainguoisudung;
                return;
            }
            string resultCK = string.Empty;
            resultCK = new User().CheckPhoneAndUserName(USERTK, SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtReMobi.Text.Trim()), "PHONENO", CONTRACTTYPE, ref IPCERRORCODE, ref IPCERRORDESC);
            if (!resultCK.Equals("0") || txtReMobi.Text.Trim().ToString().Equals(""))
            {
                lblError.Text = Resources.labels.phonenumberisalreadyregistered;
                return;
            }
            if (!IsValidEmail(txtReEmail.Text.Trim()) && txtReEmail.Text.Trim() != "")
            {
                lblAlert.Text = Resources.labels.emailkhongdinhdang1;
                return;
            }
            if(ddlUserType.SelectedValue.Trim() != "0203")
            {
                if (txtLocalFullName.Text == string.Empty || txtLocalFullName.Text == null)
                {
                    lblAlert.Text = Resources.labels.youneedinputagentmerchantname;
                    return;
                }
            }

            #region Tao bang chua cac thong tin nguoi quan tri
            string PassTemp = "";
            //27/8/2015 minh add to validate information for owner acount
            string action = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["a"].ToString();
            if (string.IsNullOrEmpty(txtReFullName.Text.Trim()))
            {
                ShowPopUpMsg(Resources.labels.bannhaptenchutaikhoan);
                txtReFullName.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtReMobi.Text.Trim()))
            {
                ShowPopUpMsg(Resources.labels.bannhapsodienthoaichutaikhoan);

                txtReMobi.Focus();
                return;
            }

            string pattern = Resources.labels.emailpattern;

            //truong hop individual email = "" chap nhan, corporate validate ca truong hop rong

            string contracttype = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["ct"].ToString();
            switch (contracttype)
            {
                case SmartPortal.Constant.IPC.PERSONAL:
                    if (!string.IsNullOrEmpty(txtReEmail.Text.Trim()))
                    {
                        if (!(System.Text.RegularExpressions.Regex.IsMatch(txtReEmail.Text, pattern)))
                        {
                            ShowPopUpMsg(Resources.labels.emailkhongdinhdang1);
                            return;

                        }
                    }
                    break;
                case SmartPortal.Constant.IPC.CORPORATECONTRACT:
                    if (!(System.Text.RegularExpressions.Regex.IsMatch(txtReEmail.Text, pattern)))
                    {

                        ShowPopUpMsg(Resources.labels.emailkhongdinhdang1);
                        txtReEmail.Focus();
                        return;

                    }
                    break;
            }
            DataSet dspolicy = new DataSet();
            string filterIB = "serviceid='IB'";
            if (ddlpolicyIB.SelectedValue.ToString() != "")
            {
                filterIB += " and policyid='" + ddlpolicyIB.SelectedValue.ToString() + "'";
            }
            string filterSMS = "serviceid='SMS'  and policyid='" + ddlpolicySMS.SelectedValue.ToString() + "'";
            string filterMB = string.Empty;
            if (CONTRACTTYPE.Equals(SmartPortal.Constant.IPC.CONTRACTINDIVIDUAL))
            {
                filterMB = "serviceid='MB' and policyid='" + ddlpolicyMB.SelectedValue.ToString() + "'";
            }
            else
            {
                filterMB = "serviceid='AM' and policyid='" + ddlpolicyMB.SelectedValue.ToString() + "'";
            }

            string stSort = "serviceid asc";

            dspolicy = new SmartPortal.SEMS.USERPOLICY().GetPolicybyCondition(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, Session["userName"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {

                DataTable dt = dspolicy.Tables[0];
                DataTable dtIB = dt.Select(filterIB, stSort).Any() ? dspolicy.Tables[0].Select(filterIB, stSort).CopyToDataTable() : null;
                DataTable dtSMS = null;
                DataTable dtMB = dt.Select(filterMB, stSort).Any() ? dspolicy.Tables[0].Select(filterMB, stSort).CopyToDataTable() : null;
                passlenIB = dtIB == null ? int.Parse(ConfigurationManager.AppSettings["minpwdlen_default"].ToString()) : Convert.ToInt32(dtIB.Rows[0]["minpwdlen"].ToString());
                passlenSMS = dtSMS == null ? int.Parse(ConfigurationManager.AppSettings["minpwdlen_default"].ToString()) : Convert.ToInt32(dtSMS.Rows[0]["minpwdlen"].ToString());
                passlenMB = dtMB == null ? int.Parse(ConfigurationManager.AppSettings["minpwdlen_default"].ToString()) : Convert.ToInt32(dtMB.Rows[0]["minpwdlen"].ToString());
            }

            if (ddlAccountQT.SelectedValue != "ALL")
            {

                //string passreveal = DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - passlenIB, passlenIB);     
                string passreveal = DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - passlenMB, passlenMB);
                string pwdreset = Encryption.Encrypt(passreveal);
                //PassTemp = SmartPortal.SEMS.O9Encryptpass.sha_sha256(pwdreset, userName);
                PassTemp = SmartPortal.SEMS.O9Encryptpass.sha_sha256(passreveal, USERTK);
                string pwdresetSMS = DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - passlenSMS, passlenSMS);

                string PinCode = DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - passlenMB, passlenMB);
                string PinCodeMB = SmartPortal.SEMS.O9Encryptpass.sha_sha256(PinCode, USERTK);
                ddlDefaultAccountQT.Items.Remove(ddlAccountQT.SelectedValue.ToString());
                ddlDefaultAccountQT.Items.Insert(ddlDefaultAccountQT.Items.Count, new ListItem(ddlAccountQT.SelectedValue.ToString(), ddlAccountQT.SelectedValue.ToString()));

                LuuThongTinQuyen("NGUOIQUANTRI", gvResultQuanTri, tvIB, tvSMSQT, tvMBQT, tvWL, txtReFullName.Text, ddlUserLevel.Text, ddlGroup.SelectedValue, txtReBirth.Text, ddlReGender.SelectedValue, txtReMobi.Text, txtReEmail.Text, txtReAddress.Text, USERTK, PassTemp, txtMBPhoneNo.Text, ddlSMSDefaultAcctno.SelectedValue, ddlDefaultLang.SelectedValue, ((cbIsDefault.Checked == true) ? "Y" : "N"), pwdresetSMS, txtMBPhoneNo.Text, PassTemp, PinCodeMB, CONTRACTTYPE == SmartPortal.Constant.IPC.CONTRACTINDIVIDUAL ? SmartPortal.Constant.IPC.NORMAL : SmartPortal.Constant.IPC.MBA, ddlAccountQT.SelectedValue, pwdreset, DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - 8, 6), "", PassTemp, txtLocalFullName.Text.Trim(), ddlLoginMethod.SelectedValue.Trim(), ddlauthenType.SelectedValue.Trim());
            }

            if (ddlAccountQT.SelectedValue == "ALL")
            {
                //lay tat ca tai khoan khach han
                DataTable dtAccount = (DataTable)ViewState["AccountList"];
                if (dtAccount.Rows.Count > 0)
                {
                    //luu tat ca account
                    //PassTemp = Encryption.Encrypt(DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - 8, 8));
                    //string passreveal = DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - passlenIB, passlenIB);
                    string passreveal = DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - passlenMB, passlenMB);
                    string pwdreset = Encryption.Encrypt(passreveal);
                    PassTemp = SmartPortal.SEMS.O9Encryptpass.sha_sha256(passreveal, USERTK);
                    string pwdresetSMS = DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - passlenSMS, passlenSMS);

                    string PinCode = DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - passlenMB, passlenMB);
                    string PinCodeMB = SmartPortal.SEMS.O9Encryptpass.sha_sha256(PinCode, USERTK);

                    foreach (DataRow rowAccount in dtAccount.Rows)
                    {

                        ddlDefaultAccountQT.Items.Remove(rowAccount["ACCOUNTNO"].ToString());
                        ddlDefaultAccountQT.Items.Insert(ddlDefaultAccountQT.Items.Count, new ListItem(rowAccount["ACCOUNTNO"].ToString(), rowAccount["ACCOUNTNO"].ToString()));

                        //LuuThongTinQuyen("NGUOIQUANTRI", gvResultQuanTri, tvIB, tvSMSQT, tvMBQT, tvPHOQT, txtReFullName.Text, ddlUserLevel.Text, txtReBirth.Text, ddlReGender.SelectedValue, txtReMobi.Text, txtReEmail.Text, txtReAddress.Text, txtIBUserName.Text, PassTemp, txtSMSPhoneNo.Text, ddlSMSDefaultAcctno.SelectedValue, ddlDefaultLang.SelectedValue, ((cbIsDefault.Checked == true) ? "Y" : "N"), DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - 8, 6), txtUserID.Text, PassTemp, DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - 8, 6), txtPHOPhoneNo.Text, PassTemp, ddlPHODefaultAcctno.SelectedValue, rowAccount["ACCOUNTNO"].ToString());
                        LuuThongTinQuyen("NGUOIQUANTRI", gvResultQuanTri, tvIB, tvSMSQT, tvMBQT, tvWL, txtReFullName.Text, ddlUserLevel.Text, ddlGroup.SelectedValue, txtReBirth.Text, ddlReGender.SelectedValue, txtReMobi.Text, txtReEmail.Text, txtReAddress.Text, USERTK, PassTemp, txtMBPhoneNo.Text, ddlSMSDefaultAcctno.SelectedValue, ddlDefaultLang.SelectedValue, ((cbIsDefault.Checked == true) ? "Y" : "N"), pwdresetSMS, txtMBPhoneNo.Text, PassTemp, PinCodeMB, CONTRACTTYPE == SmartPortal.Constant.IPC.CONTRACTINDIVIDUAL ? SmartPortal.Constant.IPC.NORMAL : SmartPortal.Constant.IPC.MBA, rowAccount["ACCOUNTNO"].ToString(), pwdreset, DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - 8, 6), ACCOUNTWL, PassTemp, txtLocalFullName.Text.Trim(), ddlLoginMethod.SelectedValue.Trim(), ddlauthenType.SelectedValue.Trim());
                    }

                    lblAlert.Text = Resources.labels.recordsaved;
                }
                else
                {
                    lblError.Text = Resources.labels.youneedmbservice;
                    return;
                }

            }
            if (TabCustomerInfoHelper.TabAMVisibility == 1 && SHOWTAB.Equals(Resources.labels.agentandmerchant))
            {
                ACCOUNTWL = txtAMPhoneNo.Text;
                // Luu thong tin Wallet
                //string passreveal = DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - passlenIB, passlenIB);
                string passreveal = DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - passlenMB, passlenMB);
                string pwdreset = Encryption.Encrypt(passreveal);
                //PassTemp = SmartPortal.SEMS.O9Encryptpass.sha_sha256(pwdreset, userName);
                PassTemp = SmartPortal.SEMS.O9Encryptpass.sha_sha256(passreveal, USERTK);
                string PinCode = DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - passlenMB, passlenMB);
                string PinCodeMB = SmartPortal.SEMS.O9Encryptpass.sha_sha256(PinCode, USERTK);




                string pwdresetSMS = DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - passlenSMS, passlenSMS);

                LuuThongTinQuyen("NGUOIQUANTRI", gvResultQuanTri, tvIB, tvSMSQT, tvMBQT, tvAM, txtReFullName.Text, ddlUserLevel.SelectedValue, ddlGroup.SelectedValue, txtReBirth.Text, ddlReGender.SelectedValue, txtReMobi.Text, txtReEmail.Text, txtReAddress.Text, USERTK, PassTemp, "", ddlSMSDefaultAcctno.SelectedValue, ddlDefaultLang.SelectedValue, ((cbIsDefault.Checked == true) ? "Y" : "N"), pwdresetSMS, "", PassTemp, PinCodeMB, SmartPortal.Constant.IPC.EAM, ACCOUNTWL, pwdreset, DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - 8, 6), txtMBPhoneNo.Text, PassTemp, txtLocalFullName.Text.Trim(), ddlLoginMethod.SelectedValue.Trim(), ddlauthenType.SelectedValue.Trim());

            }

            

            #endregion
            //28/8/2015 minh fixed truong hop khong chon dich vu ma bam nut add
            if (gvResultQuanTri.Rows.Count == 0)
            {
                lblAlert.Text = Resources.labels.banchuadangkydichvu;
                btsaveandcont.Visible = false;
                return;
            }
            else
            {
                btsaveandcont.Visible = true;
            }

        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], "Widgets_SEMSCustomerList_Add_Widget", "btnThemNQT_Click", IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);
        }

        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSCustomerList_Add_Widget", "btnThemNQT_Click", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void gvResultQuanTri_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {

            gvResultQuanTri.PageIndex = e.NewPageIndex;
            gvResultQuanTri.DataSource = (DataTable)ViewState["NGUOIQUANTRI"];
            gvResultQuanTri.DataBind();
        }
        catch
        {
        }
    }

    protected void gvResultQuanTri_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {

            DataTable tblResultQT = (DataTable)ViewState["NGUOIQUANTRI"];

            tblResultQT.Rows.RemoveAt(e.RowIndex + (gvResultQuanTri.PageIndex * gvResultQuanTri.PageSize));

            ViewState["NGUOIQUANTRI"] = tblResultQT;
            gvResultQuanTri.DataSource = tblResultQT;
            gvResultQuanTri.DataBind();

            lblAlert.Text = Resources.labels.recorddeleted;
        }
        catch
        {
        }
    }

    //void LuuThongTinQuyen(string sessionName, GridView gvResult, TreeView tvRole, TreeView tvSMSRole, TreeView tvMBRole, TreeView tvPHORole, string fullName, string level, string birthday, string gender, string phone, string email, string address, string IBUserName, string IBPass, string SMSPhone, string SMSDefaultAcctno, string SMSDefaultLang, string isDefault, string SMSPinCode, string MBPhone, string MBPass, string MBPinCode, string PHOPhone, string PHOPass, string PHODefaultAcctno, string Account)
    void LuuThongTinQuyen(string sessionName, GridView gvResult, TreeView tvRole, TreeView tvSMSRole, TreeView tvMBRole, TreeView tvWlRole, string fullName, string level, string group, string birthday, string gender, string phone, string email, string address, string IBUserName, string IBPass, string SMSPhone, string SMSDefaultAcctno, string SMSDefaultLang, string isDefault, string SMSPinCode, string MBPhone, string MBPass, string MBPinCode, string roleType, string Account, string pwdreset, string WLPinCode, string WLPhone, string WLPass, string localName, string loginMethod, string authenType)
    {
        if (ViewState[sessionName] == null)
        {
            #region NULL
            #region khoi tao bang
            DataTable tblNguoiUyQuyen = new DataTable();

            DataColumn colFullName = new DataColumn("colFullName");
            DataColumn colLocalname = new DataColumn("colLocalname");
            DataColumn colLoginMethod = new DataColumn("colLoginMethod");
            DataColumn colAuthenType = new DataColumn("colAuthenType");
            DataColumn colLevel = new DataColumn("colLevel");
            DataColumn colGroup = new DataColumn("ColGroup");
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
            DataColumn colMBPinCode = new DataColumn("colMBPinCode");
            DataColumn colRoleType = new DataColumn("colRoleType");
            DataColumn colWLPhone = new DataColumn("colWLPhone");
            DataColumn colWLPass = new DataColumn("colWLPass");
            DataColumn colWLPinCode = new DataColumn("colWLPinCode");
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
            tblNguoiUyQuyen.Columns.Add(colGroup);
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
                            rowNguoiUyQuyen["colLocalname"] = localName.Trim();
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
                            rowNguoiUyQuyen["colRoleType"] = roleType;
                            rowNguoiUyQuyen["colWLPinCode"] = WLPinCode;
                            rowNguoiUyQuyen["colWLPhone"] = WLPhone.Trim();
                            rowNguoiUyQuyen["colWLPass"] = WLPass;
                            rowNguoiUyQuyen["colAccount"] = Account;
                            rowNguoiUyQuyen["colRole"] = nodeRoleIBNguoiUyQuyen.Text;
                            rowNguoiUyQuyen["colRoleID"] = nodeRoleIBNguoiUyQuyen.Value;
                            rowNguoiUyQuyen["colTranCode"] = nodeTrancodeIBNguoiUyQuyen.Text;
                            rowNguoiUyQuyen["colTranCodeID"] = nodeTrancodeIBNguoiUyQuyen.Value;
                            if (CONTRACTTYPE.Equals(SmartPortal.Constant.IPC.CONTRACTINDIVIDUAL))
                            {
                                rowNguoiUyQuyen["colServiceID"] = SmartPortal.Constant.IPC.MB;
                            }
                            else
                            {
                                rowNguoiUyQuyen["colServiceID"] = SmartPortal.Constant.IPC.AM;
                            }

                            rowNguoiUyQuyen["colIBPolicy"] = ddlpolicyIB.SelectedValue.ToString();
                            rowNguoiUyQuyen["colSMSPolicy"] = ddlpolicySMS.SelectedValue.ToString();
                            rowNguoiUyQuyen["colMBPolicy"] = ddlpolicyMB.SelectedValue.ToString();
                            rowNguoiUyQuyen["colWLPolicy"] = ddlpolicyMB.SelectedValue.ToString();
                            rowNguoiUyQuyen["colpwdreset"] = pwdreset;
                            tblNguoiUyQuyen.Rows.Add(rowNguoiUyQuyen);

                        }
                    }
                    else
                    {
                    }
                }
            }
            //add wallet account
            if (WLPhone != "")
            {
                foreach (TreeNode nodeRoleIBNguoiUyQuyen in tvWlRole.Nodes)
                {
                    if (nodeRoleIBNguoiUyQuyen.Checked)
                    {
                        DataTable dtRole = (DataTable)ViewState["TBLROLE"];
                        DataTable dtIB = dtRole.Select("ROLEID='" + nodeRoleIBNguoiUyQuyen.Value.Trim() + "'").Any() ? dtRole.Select("ROLEID='" + nodeRoleIBNguoiUyQuyen.Value.Trim() + "'").CopyToDataTable() : null;
                        string ServiceID = "MB";
                        foreach (DataRow dr in dtRole.Rows)
                        {
                            if (nodeRoleIBNguoiUyQuyen.Value.ToString().Trim().Equals(dr["ROLEID"].ToString().Trim()))
                            {
                                ServiceID = dr["SERVICEID"].ToString();
                            }
                        }
                        foreach (TreeNode nodeTrancodeIBNguoiUyQuyen in nodeRoleIBNguoiUyQuyen.ChildNodes)
                        {

                            DataRow rowNguoiUyQuyen = tblNguoiUyQuyen.NewRow();
                            rowNguoiUyQuyen["colFullName"] = fullName.Trim();
                            rowNguoiUyQuyen["colLocalname"] = localName.Trim();
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
                            rowNguoiUyQuyen["colRoleType"] = roleType;
                            rowNguoiUyQuyen["colWLPinCode"] = WLPinCode;
                            rowNguoiUyQuyen["colWLPhone"] = WLPhone.Trim();
                            rowNguoiUyQuyen["colWLPass"] = WLPass;
                            rowNguoiUyQuyen["colAccount"] = WLPhone;
                            rowNguoiUyQuyen["colRole"] = nodeRoleIBNguoiUyQuyen.Text;
                            rowNguoiUyQuyen["colRoleID"] = nodeRoleIBNguoiUyQuyen.Value;
                            rowNguoiUyQuyen["colTranCode"] = nodeTrancodeIBNguoiUyQuyen.Text;
                            rowNguoiUyQuyen["colTranCodeID"] = nodeTrancodeIBNguoiUyQuyen.Value;
                            rowNguoiUyQuyen["colTranCodeID"] = nodeTrancodeIBNguoiUyQuyen.Value;
                            rowNguoiUyQuyen["colServiceID"] = ServiceID;
                            rowNguoiUyQuyen["colIBPolicy"] = ddlpolicyIB.SelectedValue.ToString();
                            rowNguoiUyQuyen["colSMSPolicy"] = ddlpolicySMS.SelectedValue.ToString();
                            rowNguoiUyQuyen["colMBPolicy"] = ddlpolicyMB.SelectedValue.ToString();
                            if (SHOWTAB.Equals(Resources.labels.walletbanking))
                            {
                                rowNguoiUyQuyen["colWLPolicy"] = ddlpolicyMB.SelectedValue.ToString();
                            }
                            else if (SHOWTAB.Equals(Resources.labels.agentmerchant))
                            {
                                rowNguoiUyQuyen["colWLPolicy"] = ddlpolicyMB.SelectedValue.ToString();
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


            //IB
            if (Account != "")
            {
                foreach (TreeNode nodeRoleIBNguoiUyQuyen in tvRole.Nodes)
                {
                    if (nodeRoleIBNguoiUyQuyen.Checked)
                    {
                        foreach (TreeNode nodeTrancodeIBNguoiUyQuyen in nodeRoleIBNguoiUyQuyen.ChildNodes)
                        {
                            if (tblNguoiUyQuyen.Select("colFullName='" + fullName.Replace("'", "''") + "' and colAccount='" + Account + "' and colRoleID='" + nodeRoleIBNguoiUyQuyen.Value + "' and colTranCode='" + nodeTrancodeIBNguoiUyQuyen.Text + "'").Length == 0)
                            {
                                #region luu thong tin quyen IB khac null
                                DataRow rowNguoiUyQuyen = tblNguoiUyQuyen.NewRow();
                                rowNguoiUyQuyen["colFullName"] = fullName.Trim();
                                rowNguoiUyQuyen["colLocalname"] = localName.Trim();
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
                                rowNguoiUyQuyen["colRoleType"] = roleType;
                                rowNguoiUyQuyen["colWLPinCode"] = WLPinCode;
                                rowNguoiUyQuyen["colWLPhone"] = WLPhone.Trim();
                                rowNguoiUyQuyen["colWLPass"] = WLPass;
                                rowNguoiUyQuyen["colAccount"] = Account;
                                rowNguoiUyQuyen["colRole"] = nodeRoleIBNguoiUyQuyen.Text;
                                rowNguoiUyQuyen["colRoleID"] = nodeRoleIBNguoiUyQuyen.Value;
                                rowNguoiUyQuyen["colTranCode"] = nodeTrancodeIBNguoiUyQuyen.Text;
                                rowNguoiUyQuyen["colTranCodeID"] = nodeTrancodeIBNguoiUyQuyen.Value;
                                if (CONTRACTTYPE.Equals(SmartPortal.Constant.IPC.CONTRACTINDIVIDUAL))
                                {
                                    rowNguoiUyQuyen["colServiceID"] = SmartPortal.Constant.IPC.IB;
                                }
                                else
                                {
                                    rowNguoiUyQuyen["colServiceID"] = SmartPortal.Constant.IPC.AM;
                                }
                                rowNguoiUyQuyen["colIBPolicy"] = ddlpolicyIB.SelectedValue.ToString();
                                rowNguoiUyQuyen["colSMSPolicy"] = ddlpolicySMS.SelectedValue.ToString();
                                rowNguoiUyQuyen["colMBPolicy"] = ddlpolicyMB.SelectedValue.ToString();
                                rowNguoiUyQuyen["colWLPolicy"] = ddlpolicyMB.SelectedValue.ToString();
                                rowNguoiUyQuyen["colpwdreset"] = pwdreset;

                                tblNguoiUyQuyen.Rows.Add(rowNguoiUyQuyen);
                                #endregion
                            }
                        }
                    }
                    else
                    {
                        foreach (TreeNode nodeTrancodeIBNguoiUyQuyen in nodeRoleIBNguoiUyQuyen.ChildNodes)
                        {
                            if (tblNguoiUyQuyen.Select("colFullName='" + fullName.Replace("'", "''") + "' and colAccount='" + Account + "' and colRoleID='" + nodeRoleIBNguoiUyQuyen.Value + "' and colTranCode='" + nodeTrancodeIBNguoiUyQuyen.Text + "'").Length == 0)
                            {

                            }

                            else
                            {
                                if (tblNguoiUyQuyen.Select("colFullName='" + fullName.Replace("'", "''") + "' and colAccount='" + Account + "' and colRoleID='" + nodeRoleIBNguoiUyQuyen.Value + "' and colTranCode='" + nodeTrancodeIBNguoiUyQuyen.Text + "'").Length != 0)
                                {
                                    foreach (DataRow r in tblNguoiUyQuyen.Select("colFullName='" + fullName + "' and colRoleID='" + nodeRoleIBNguoiUyQuyen.Value + "' and colAccount='" + Account + "' and colTranCode='" + nodeTrancodeIBNguoiUyQuyen.Text + "'"))
                                    {
                                        tblNguoiUyQuyen.Rows.Remove(r);
                                    }
                                }
                            }
                        }
                    }
                }
                #endregion
            }

            ViewState[sessionName] = tblNguoiUyQuyen;
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
                    r["colGroup"] = ddlGroup.SelectedValue.Trim();
                }
            if (SMSPhone == "" && tblNguoiUyQuyen.Rows.Count != 0 && WLPhone == "")
            {
                SMSPhone = tblNguoiUyQuyen.Rows[0]["colSMSPhone"].ToString().Trim();
            }
            //MB
            if (MBPhone != "")
            {
                foreach (TreeNode nodeRoleIBNguoiUyQuyen in tvMBRole.Nodes)
                {
                    if (nodeRoleIBNguoiUyQuyen.Checked)
                    {
                        foreach (TreeNode nodeTrancodeIBNguoiUyQuyen in nodeRoleIBNguoiUyQuyen.ChildNodes)
                        {
                            if (tblNguoiUyQuyen.Select("colFullName='" + fullName.Replace("'", "''") + "' and colAccount='" + Account + "' and colRoleID='" + nodeRoleIBNguoiUyQuyen.Value + "' and colTranCode='" + nodeTrancodeIBNguoiUyQuyen.Text + "'").Length == 0)
                            {
                                #region luu thong tin quyen MB khac null
                                DataRow rowNguoiUyQuyen = tblNguoiUyQuyen.NewRow();
                                rowNguoiUyQuyen["colFullName"] = fullName.Trim();
                                rowNguoiUyQuyen["colLocalname"] = localName.Trim();
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
                                rowNguoiUyQuyen["colRoleType"] = roleType;
                                rowNguoiUyQuyen["colWLPinCode"] = WLPinCode;
                                rowNguoiUyQuyen["colWLPhone"] = WLPhone.Trim();
                                rowNguoiUyQuyen["colWLPass"] = WLPass;
                                rowNguoiUyQuyen["colAccount"] = Account;
                                rowNguoiUyQuyen["colRole"] = nodeRoleIBNguoiUyQuyen.Text;
                                rowNguoiUyQuyen["colRoleID"] = nodeRoleIBNguoiUyQuyen.Value;
                                rowNguoiUyQuyen["colTranCode"] = nodeTrancodeIBNguoiUyQuyen.Text;
                                rowNguoiUyQuyen["colTranCodeID"] = nodeTrancodeIBNguoiUyQuyen.Value;
                                if (CONTRACTTYPE.Equals(SmartPortal.Constant.IPC.CONTRACTINDIVIDUAL))
                                {
                                    rowNguoiUyQuyen["colServiceID"] = SmartPortal.Constant.IPC.MB;
                                }
                                else
                                {
                                    rowNguoiUyQuyen["colServiceID"] = SmartPortal.Constant.IPC.AM;
                                }
                                rowNguoiUyQuyen["colIBPolicy"] = ddlpolicyIB.SelectedValue.ToString();
                                rowNguoiUyQuyen["colSMSPolicy"] = ddlpolicySMS.SelectedValue.ToString();
                                rowNguoiUyQuyen["colMBPolicy"] = ddlpolicyMB.SelectedValue.ToString();
                                rowNguoiUyQuyen["colWLPolicy"] = ddlpolicyMB.SelectedValue.ToString();
                                rowNguoiUyQuyen["colpwdreset"] = pwdreset;

                                tblNguoiUyQuyen.Rows.Add(rowNguoiUyQuyen);
                                #endregion
                            }
                        }
                    }
                    else
                    {
                        foreach (TreeNode nodeTrancodeIBNguoiUyQuyen in nodeRoleIBNguoiUyQuyen.ChildNodes)
                        {
                            if (tblNguoiUyQuyen.Select("colFullName='" + fullName.Replace("'", "''") + "' and colAccount='" + Account + "' and colRoleID='" + nodeRoleIBNguoiUyQuyen.Value + "' and colTranCode='" + nodeTrancodeIBNguoiUyQuyen.Text + "'").Length == 0)
                            {

                            }

                            else
                            {
                                if (tblNguoiUyQuyen.Select("colFullName='" + fullName.Replace("'", "''") + "' and colAccount='" + Account + "' and colRoleID='" + nodeRoleIBNguoiUyQuyen.Value + "' and colTranCode='" + nodeTrancodeIBNguoiUyQuyen.Text + "'").Length != 0)
                                {
                                    foreach (DataRow r in tblNguoiUyQuyen.Select("colFullName='" + fullName + "' and colRoleID='" + nodeRoleIBNguoiUyQuyen.Value + "' and colAccount='" + Account + "' and colTranCode='" + nodeTrancodeIBNguoiUyQuyen.Text + "'"))
                                    {
                                        tblNguoiUyQuyen.Rows.Remove(r);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            //Wallet
            if (WLPhone != "")
            {
                foreach (TreeNode nodeRoleWLNguoiUyQuyen in tvWlRole.Nodes)
                {
                    if (nodeRoleWLNguoiUyQuyen.Checked)
                    {
                        DataTable dtRole = (DataTable)ViewState["TBLROLE"];

                        string ServiceID = CONTRACTTYPE == SmartPortal.Constant.IPC.AGENTMERCHANT ? SmartPortal.Constant.IPC.AGENTMERCHANT : "MB";
                        if (dtRole != null)
                        {
                            DataTable dtIB = dtRole.Select("ROLEID='" + nodeRoleWLNguoiUyQuyen.Value.Trim() + "'").Any() ? dtRole.Select("ROLEID='" + nodeRoleWLNguoiUyQuyen.Value.Trim() + "'").CopyToDataTable() : null;
                            foreach (DataRow dr in dtRole.Rows)
                            {
                                if (nodeRoleWLNguoiUyQuyen.Value.ToString().Trim().Equals(dr["ROLEID"].ToString().Trim()))
                                {
                                    ServiceID = dr["SERVICEID"].ToString();
                                }
                            }
                        }
                        foreach (TreeNode nodeTrancodeIBNguoiUyQuyen in nodeRoleWLNguoiUyQuyen.ChildNodes)
                        {
                            //if (nodeTrancodeIBNguoiUyQuyen.Checked)
                            //{
                            if (tblNguoiUyQuyen.Select("colFullName='" + fullName.Replace("'", "''") + "' and colAccount='" + Account + "' and colRoleID='" + nodeRoleWLNguoiUyQuyen.Value + "' and colTranCode='" + nodeTrancodeIBNguoiUyQuyen.Text + "'").Length == 0)
                            {
                                #region chi tiet
                                DataRow rowNguoiUyQuyen = tblNguoiUyQuyen.NewRow();
                                rowNguoiUyQuyen["colFullName"] = fullName.Trim();
                                rowNguoiUyQuyen["colLocalname"] = localName.Trim();
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
                                rowNguoiUyQuyen["colRoleType"] = roleType;
                                rowNguoiUyQuyen["colWLPinCode"] = WLPinCode;
                                rowNguoiUyQuyen["colWLPhone"] = WLPhone.Trim();
                                rowNguoiUyQuyen["colWLPass"] = WLPass;
                                rowNguoiUyQuyen["colAccount"] = WLPhone;
                                rowNguoiUyQuyen["colRole"] = nodeRoleWLNguoiUyQuyen.Text;
                                rowNguoiUyQuyen["colRoleID"] = nodeRoleWLNguoiUyQuyen.Value;
                                rowNguoiUyQuyen["colTranCode"] = nodeTrancodeIBNguoiUyQuyen.Text;
                                rowNguoiUyQuyen["colTranCodeID"] = nodeTrancodeIBNguoiUyQuyen.Value;
                                rowNguoiUyQuyen["colServiceID"] = ServiceID;
                                rowNguoiUyQuyen["colIBPolicy"] = ddlpolicyIB.SelectedValue.ToString();
                                rowNguoiUyQuyen["colSMSPolicy"] = ddlpolicySMS.SelectedValue.ToString();
                                rowNguoiUyQuyen["colMBPolicy"] = ddlpolicyMB.SelectedValue.ToString();
                                if (SHOWTAB.Equals(Resources.labels.walletbanking))
                                {
                                    rowNguoiUyQuyen["colWLPolicy"] = ddlpolicyMB.SelectedValue.ToString();
                                }
                                else if (SHOWTAB.Equals(Resources.labels.agentmerchant))
                                {
                                    rowNguoiUyQuyen["colWLPolicy"] = ddlpolicyMB.SelectedValue.ToString();
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

            //IB
            if (Account != "")
            {
                foreach (TreeNode nodeRoleIBNguoiUyQuyen in tvRole.Nodes)
                {
                    if (nodeRoleIBNguoiUyQuyen.Checked)
                    {
                        foreach (TreeNode nodeTrancodeIBNguoiUyQuyen in nodeRoleIBNguoiUyQuyen.ChildNodes)
                        {
                            if (tblNguoiUyQuyen.Select("colFullName='" + fullName.Replace("'", "''") + "' and colAccount='" + Account + "' and colRoleID='" + nodeRoleIBNguoiUyQuyen.Value + "' and colTranCode='" + nodeTrancodeIBNguoiUyQuyen.Text + "'").Length == 0)
                            {
                                #region luu thong tin quyen IB khac null
                                DataRow rowNguoiUyQuyen = tblNguoiUyQuyen.NewRow();
                                rowNguoiUyQuyen["colFullName"] = fullName.Trim();
                                rowNguoiUyQuyen["colLocalname"] = localName.Trim();
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
                                rowNguoiUyQuyen["colRoleType"] = roleType;
                                rowNguoiUyQuyen["colWLPinCode"] = WLPinCode;
                                rowNguoiUyQuyen["colWLPhone"] = WLPhone.Trim();
                                rowNguoiUyQuyen["colWLPass"] = WLPass;
                                rowNguoiUyQuyen["colAccount"] = Account;
                                rowNguoiUyQuyen["colRole"] = nodeRoleIBNguoiUyQuyen.Text;
                                rowNguoiUyQuyen["colRoleID"] = nodeRoleIBNguoiUyQuyen.Value;
                                rowNguoiUyQuyen["colTranCode"] = nodeTrancodeIBNguoiUyQuyen.Text;
                                rowNguoiUyQuyen["colTranCodeID"] = nodeTrancodeIBNguoiUyQuyen.Value;
                                if (CONTRACTTYPE.Equals(SmartPortal.Constant.IPC.CONTRACTINDIVIDUAL))
                                {
                                    rowNguoiUyQuyen["colServiceID"] = SmartPortal.Constant.IPC.IB;
                                }
                                else
                                {
                                    rowNguoiUyQuyen["colServiceID"] = SmartPortal.Constant.IPC.AM;
                                }
                                rowNguoiUyQuyen["colIBPolicy"] = ddlpolicyIB.SelectedValue.ToString();
                                rowNguoiUyQuyen["colSMSPolicy"] = ddlpolicySMS.SelectedValue.ToString();
                                rowNguoiUyQuyen["colMBPolicy"] = ddlpolicyMB.SelectedValue.ToString();
                                rowNguoiUyQuyen["colWLPolicy"] = ddlpolicyMB.SelectedValue.ToString();
                                rowNguoiUyQuyen["colpwdreset"] = pwdreset;

                                tblNguoiUyQuyen.Rows.Add(rowNguoiUyQuyen);
                                #endregion
                            }
                        }
                    }
                    else
                    {
                        foreach (TreeNode nodeTrancodeIBNguoiUyQuyen in nodeRoleIBNguoiUyQuyen.ChildNodes)
                        {
                            if (tblNguoiUyQuyen.Select("colFullName='" + fullName.Replace("'", "''") + "' and colAccount='" + Account + "' and colRoleID='" + nodeRoleIBNguoiUyQuyen.Value + "' and colTranCode='" + nodeTrancodeIBNguoiUyQuyen.Text + "'").Length == 0)
                            {

                            }

                            else
                            {
                                if (tblNguoiUyQuyen.Select("colFullName='" + fullName.Replace("'", "''") + "' and colAccount='" + Account + "' and colRoleID='" + nodeRoleIBNguoiUyQuyen.Value + "' and colTranCode='" + nodeTrancodeIBNguoiUyQuyen.Text + "'").Length != 0)
                                {
                                    foreach (DataRow r in tblNguoiUyQuyen.Select("colFullName='" + fullName + "' and colRoleID='" + nodeRoleIBNguoiUyQuyen.Value + "' and colAccount='" + Account + "' and colTranCode='" + nodeTrancodeIBNguoiUyQuyen.Text + "'"))
                                    {
                                        tblNguoiUyQuyen.Rows.Remove(r);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            ViewState[sessionName] = tblNguoiUyQuyen;
            gvResult.DataSource = tblNguoiUyQuyen.DefaultView.ToTable(true, "colFullName", "colPhone", "colAccount", "colRole");
            gvResult.DataBind();
        }
    }

    protected void btsaveandcont_Click(object sender, EventArgs e)
    {
        try
        {
            string PhoneNumberMB = string.Empty;
            string UserNameMB = string.Empty;
            UserNameMB = txtUserNameMB.Text.Trim();
            PhoneNumberMB = txtMBPhoneNo.Text.Trim();
            string resultCK = string.Empty;
            if (txtUserNameMB.Text.Trim().Length < minlength || txtUserNameMB.Text.Trim().Length > maxlength)
            {
                lblAlert.Text = string.Format(Resources.labels.usernamemustbetween, minlength, maxlength);
                return;
            }
            resultCK = new User().CheckPhoneAndUserName(USERTK, SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtUserNameMB.Text.Trim()), "USERNAME", CONTRACTTYPE, ref IPCERRORCODE, ref IPCERRORDESC);
            if (!resultCK.Equals("0") || txtUserNameMB.Text.Trim().ToString().Equals(""))
            {
                lblError.Text = Resources.labels.usernamehasexists;
                return;
            }
            if (string.IsNullOrEmpty(txtReBirth.Text.Trim()) || string.IsNullOrEmpty(txtReFullName.Text.Trim()))
            {
                lblError.Text = Resources.labels.youneedtoenterfullinformation;
                return;
            }
            if (!IsValidEmail(txtReEmail.Text.Trim()) && txtReEmail.Text.Trim() != "")
            {
                lblError.Text = Resources.labels.emailkhongdinhdang1;
                return;
            }
            if ((DataTable)ViewState["NGUOIQUANTRI"] == null)
            {
                throw new IPCException(SmartPortal.Constant.IPC.ERRORCODE.FULLINFOERROR);
            }
            //ktra ngay sinh
            if (txtReBirth.Text.Trim() != "")
            {
                if (SmartPortal.Common.Utilities.Utility.IsDateTime1(txtReBirth.Text.Trim()) > DateTime.Now)
                {
                    lblError.Text = "Incorrect date of birthday";
                    return;
                }
            }

            string lastModify = SmartPortal.Common.Utilities.Utility.KillSqlInjection(DateTime.Now.ToString("dd/MM/yyyy"));
            string userCreate = SmartPortal.Common.Utilities.Utility.KillSqlInjection(Session["userName"].ToString().Trim());
            string userLastModify = SmartPortal.Common.Utilities.Utility.KillSqlInjection(Session["userName"].ToString().Trim());
            string userApprove = "";
            string endDate = SmartPortal.Constant.IPC.DATEDEFAULT;


            string status = "";//SmartPortal.Constant.IPC.ACTIVE;

            string userFullName = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtReFullName.Text.Trim());
            string userGender = ddlReGender.SelectedValue;
            string userAddress = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtReAddress.Text.Trim());
            string userLocalFullName = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtLocalFullName.Text.Trim());
            string userEmail = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtReEmail.Text.Trim());
            string userPhone = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtReMobi.Text.Trim());
            userType = USERTYPE;

            string userLevel = "0";
            string type = string.Empty;
            if (walletOnly)
            {
                type = IPC.CHUTAIKHOAN;
            }
            else
            {
                type = TYPEID == "" ? IPC.DONGCHUTAIKHOAN : TYPEID;
            }


            if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["ct"] == SmartPortal.Constant.IPC.CORPORATECONTRACT)
            {
                string[] TypeLevel = ddlType.SelectedValue.Split('#');
                type = SmartPortal.Common.Utilities.Utility.KillSqlInjection(TypeLevel[0].ToString().Trim());
                if (!type.Equals(IPC.CONTRACTCORPMATRIX))
                    userLevel = SmartPortal.Common.Utilities.Utility.KillSqlInjection(TypeLevel[1].ToString().Trim());
            }
            string deptID = "";
            string userBirthday = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtReBirth.Text.Trim());
            string localName = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtLocalFullName.Text.Trim());
            if (userBirthday == "")
            {
                userBirthday = SmartPortal.Constant.IPC.DATEDEFAULT;
            }
            string uCreateDate = DateTime.Now.ToString("dd/MM/yyyy");
            string tokenID = "";
            string tokenIssueDate = "01/01/1900";
            string smsOTP = "";

            string IBUserName = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtIBUserName.Text.Trim());
            string MBPhoneNo = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtMBPhoneNo.Text.Trim());
            //string IBPassword = Encryption.Encrypt(DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - 8, 6));
            string IBStatus = "";
            string SMSStatus = "";
            string MBStatus = "";
            string PHOStatus = "";

            string SMSPhoneNo = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtSMSPhoneNo.Text.Trim());
            string SMSDefaultAcctno = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlSMSDefaultAcctno.SelectedValue.Trim());


            //string MBPass = Encryption.Encrypt(DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - 8, 6));

            //get contract info
            DataTable contractInfo = new DataTable();

            contractInfo = (new SmartPortal.SEMS.Contract().GetContractByContractNo(contractNo, ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
            if (contractInfo.Rows.Count != 0)
            {
                endDate = SmartPortal.Common.Utilities.Utility.IsDateTime2(contractInfo.Rows[0]["ENDDATE"].ToString().Trim()).ToString("dd/MM/yyyy");
            }
            if (int.Parse(ConfigurationManager.AppSettings["IBMBSameUser"].ToString()) == 1)
            {
                IBUserName = UserNameMB;
            }
            DataTable tblNGUOIQUANTRI = new DataTable();

            if (ViewState["NGUOIQUANTRI"] != null)
            {
                tblNGUOIQUANTRI = (DataTable)ViewState["NGUOIQUANTRI"];
                if (tblNGUOIQUANTRI.Rows.Count == 0)
                {
                    lblAlert.Text = Resources.labels.youneedmbservice;
                    return;
                }
                if (CONTRACTTYPE.Equals(SmartPortal.Constant.IPC.CONTRACTINDIVIDUAL))
                {
                    DataRow dr = tblNGUOIQUANTRI.Select("").FirstOrDefault(x => (string)x["colRoleType"] == SmartPortal.Constant.IPC.NORMAL);
                    if (dr == null)
                    {
                        lblError.Text = Resources.labels.youneedmbservice;
                        return;
                    }
                }
                else
                {
                    DataRow dr = tblNGUOIQUANTRI.Select("").FirstOrDefault(x => (string)x["colRoleType"] == SmartPortal.Constant.IPC.MBA);
                    if (dr == null)
                    {
                        lblError.Text = Resources.labels.youneedmbservice;
                        return;
                    }
                }

                //userTK = tblNGUOIQUANTRI.Rows[0]["colIBUserName"].ToString();
            }
            //minhadd 26/8/2015
            string emailnqt = "";
            string smsphonenqt = "";
            //26.2.2016 minh add password reset and change value of ibpass,mbpass,phopass
            string pwdreset = string.Empty;
            string pwdresetsms = string.Empty;
            string walletID = string.Empty;

            foreach (DataRow r in tblNGUOIQUANTRI.Rows)
            {
                if (!string.IsNullOrEmpty(r["colSMSPinCode"] as string))
                {
                    pwdresetsms = Encryption.Encrypt(r["colSMSPinCode"].ToString());
                    pwdreset = Encryption.Encrypt(r["colSMSPinCode"].ToString());
                    break;
                }
            }
            //if(pwdresetsms.Equals(string.Empty))
            //{
            //    ShowPopUpMsg("pass reset sms null");
            //    return;

            //}
            walletID = new Customer().GetAccountWalletID(contractNo, ref IPCERRORCODE, ref IPCERRORDESC);

            pwdreset = tblNGUOIQUANTRI.Rows[0]["colpwdreset"].ToString();
            string IBPassword = tblNGUOIQUANTRI.Rows[0]["colIBPass"].ToString();
            string MBPass = tblNGUOIQUANTRI.Rows[0]["colMBPass"].ToString();
            string WLPass = tblNGUOIQUANTRI.Rows[0]["colWLPass"].ToString();
            emailnqt = tblNGUOIQUANTRI.Rows[0]["colEmail"].ToString();
            string UsernameMB = txtUserNameMB.Text.Trim();
            string loginMethod = tblNGUOIQUANTRI.Rows[0]["colLoginMethod"].ToString();
            string authenType = tblNGUOIQUANTRI.Rows[0]["colAuthenType"].ToString();
            //string smsphonenuq = "";
            foreach (DataRow r in tblNGUOIQUANTRI.Rows)
            {
                if (r["colServiceID"].ToString() == SmartPortal.Constant.IPC.SMS)
                {
                    smsphonenqt = r["colSMSPhone"].ToString();
                    break;
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
            DataColumn colGroup = new DataColumn("colGroup");


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
            tblSUM.Columns.Add(colGroup);


            //lấy thông tin trong bảng QUAN TRI
            foreach (DataRow dongCTK in tblNGUOIQUANTRI.Rows)
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
                rowNguoiUyQuyen["colWLPinCode"] = dongCTK["colWLPinCode"].ToString();
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
                rowNguoiUyQuyen["colGroup"] = dongCTK["colGroup"].ToString();


                tblSUM.Rows.Add(rowNguoiUyQuyen);
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
            DataRow[] arrIBR = tblSUM.Select("colServiceID='" + SmartPortal.Constant.IPC.IB + "' and colRoleTYpe='" + SmartPortal.Constant.IPC.NORMAL + "'");
            foreach (DataRow rIBR in arrIBR)
            {
                if (tblIbankUserRight.Select("colIBUserNameRight='" + USERTK + "' and colIBRoleID='" + rIBR["colRoleID"].ToString() + "'").Length == 0)
                {
                    DataRow newRowIBR = tblIbankUserRight.NewRow();
                    newRowIBR["colIBUserNameRight"] = USERTK;
                    newRowIBR["colIBRoleID"] = rIBR["colRoleID"].ToString();

                    tblIbankUserRight.Rows.Add(newRowIBR);
                }
            }
            #endregion

            #region Tạo bảng chứa quyền user WL
            //tao 1 dong du lieu
            DataRow[] arrIBWLR = tblSUM.Select("colServiceID='" + SmartPortal.Constant.IPC.IB + "' and colRoleType='" + SmartPortal.Constant.IPC.WAL + "'");
            foreach (DataRow rWLR in arrIBWLR)
            {
                if (tblIbankUserRight.Select("colIBUserNameRight='" + USERTK + "' and colIBRoleID='" + rWLR["colRoleID"].ToString() + "'").Length == 0)
                {

                    DataRow newRowIBR = tblIbankUserRight.NewRow();
                    newRowIBR["colIBUserNameRight"] = USERTK;
                    newRowIBR["colIBRoleID"] = rWLR["colRoleID"].ToString();
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
            DataColumn colMBUserIDU = new DataColumn("colMBUserIDU");
            DataColumn colMBRoleID = new DataColumn("colMBRoleID");
            DataColumn colMBRoleContracType = new DataColumn("colMBRoleContracType");
            //add vào table
            tblMBUserRight.Columns.Add(colMBUserIDU);
            tblMBUserRight.Columns.Add(colMBRoleID);
            tblMBUserRight.Columns.Add(colMBRoleContracType);
            //tao 1 dong du lieu
            if (CONTRACTTYPE.Equals(SmartPortal.Constant.IPC.CONTRACTINDIVIDUAL))
            {
                DataRow[] arrMBR = tblSUM.Select("colServiceID='" + SmartPortal.Constant.IPC.MB + "' and colRoleTYpe='" + SmartPortal.Constant.IPC.NORMAL + "'");
                foreach (DataRow rMBR in arrMBR)
                {
                    if (tblMBUserRight.Select("colMBUserIDU='" + USERTK + "' and colMBRoleID='" + rMBR["colRoleID"].ToString() + "'").Length == 0)
                    {
                        DataRow newRowMBR = tblMBUserRight.NewRow();
                        newRowMBR["colMBUserIDU"] = USERTK;
                        newRowMBR["colMBRoleID"] = rMBR["colRoleID"].ToString();
                        newRowMBR["colMBRoleContracType"] = CONTRACTTYPE;
                        tblMBUserRight.Rows.Add(newRowMBR);
                    }
                }
            }
            else
            {
                DataRow[] arrMBR = tblSUM.Select("colServiceID='" + SmartPortal.Constant.IPC.AM + "' and colRoleType='" + SmartPortal.Constant.IPC.MBA + "'");
                foreach (DataRow rMBR in arrMBR)
                {
                    if (tblMBUserRight.Select("colMBUserIDU='" + USERTK + "' and colMBRoleID='" + rMBR["colRoleID"].ToString() + "'").Length == 0)
                    {
                        DataRow newRowMBR = tblMBUserRight.NewRow();
                        newRowMBR["colMBUserIDU"] = USERTK;
                        newRowMBR["colMBRoleID"] = rMBR["colRoleID"].ToString();
                        newRowMBR["colMBRoleContracType"] = SmartPortal.Constant.IPC.CONTRACTAGENTMERCHANT;

                        tblMBUserRight.Rows.Add(newRowMBR);
                    }
                }
            }

            #endregion

            #region Tạo bảng chứa quyền user PHO
            //tao bang chua thong tin customer
            DataTable tblPHOUserRight = new DataTable();
            DataColumn colPHOPhoneNoR = new DataColumn("colPHOPhoneNo");
            DataColumn colPHORoleID = new DataColumn("colPHORoleID");

            //add vào table
            tblPHOUserRight.Columns.Add(colPHOPhoneNoR);
            tblPHOUserRight.Columns.Add(colPHORoleID);

            //tao 1 dong du lieu
            DataRow[] arrPHOR = tblSUM.Select("colServiceID='" + SmartPortal.Constant.IPC.PHO + "'");
            foreach (DataRow rPHOR in arrPHOR)
            {
                if (tblPHOUserRight.Select("colPHOPhoneNo='" + rPHOR["colPHOPhone"].ToString() + "' and colPHORoleID='" + rPHOR["colRoleID"].ToString() + "'").Length == 0)
                {
                    DataRow newRowPHOR = tblPHOUserRight.NewRow();
                    newRowPHOR["colPHOPhoneNo"] = rPHOR["colPHOPhone"].ToString();
                    newRowPHOR["colPHORoleID"] = rPHOR["colRoleID"].ToString();

                    tblPHOUserRight.Rows.Add(newRowPHOR);
                }
            }
            #endregion

            #region Tạo bảng chứa quyền user WL
            //tao bang chua thong tin customer
            DataTable tblWLUserRight = new DataTable();
            DataColumn colWLUserID = new DataColumn("colWLUserID");
            DataColumn colWLRoleID = new DataColumn("colWLRoleID");
            DataColumn colWLRoleContracType = new DataColumn("colWLRoleContracType");

            //add vào table
            tblWLUserRight.Columns.Add(colWLUserID);
            tblWLUserRight.Columns.Add(colWLRoleID);
            tblWLUserRight.Columns.Add(colWLRoleContracType);
            //tao 1 dong du lieu
            DataRow[] arrWLR;
            if (SHOWTAB.Equals(Resources.labels.walletbanking))
            {
                arrWLR = tblSUM.Select("colServiceID='" + SmartPortal.Constant.IPC.MB + "' and colRoleType='" + SmartPortal.Constant.IPC.WAL + "'");

            }
            else
            {
                arrWLR = tblSUM.Select("colServiceID='" + SmartPortal.Constant.IPC.AM + "' and colRoleType='" + SmartPortal.Constant.IPC.EAM + "'");
            }
            foreach (DataRow rWLR in arrWLR)
            {
                if (tblWLUserRight.Select("colWLUserID='" + USERTK + "' and colWLRoleID='" + rWLR["colRoleID"].ToString() + "'").Length == 0)
                {
                    DataRow newRowWLR = tblWLUserRight.NewRow();

                    newRowWLR["colWLUserID"] = USERTK;
                    newRowWLR["colWLRoleID"] = rWLR["colRoleID"].ToString();
                    newRowWLR["colWLRoleContracType"] = CONTRACTTYPE;
                    tblWLUserRight.Rows.Add(newRowWLR);

                    DataRow newRowMBR = tblMBUserRight.NewRow();
                    newRowMBR["colMBUserIDU"] = USERTK;
                    newRowMBR["colMBRoleID"] = rWLR["colRoleID"].ToString();
                    newRowMBR["colMBRoleContracType"] = CONTRACTTYPE;

                    tblMBUserRight.Rows.Add(newRowMBR);
                }
            }
            #endregion
            //LanNTH
            #region Tạo bảng chứa group của user 
            DataTable tblUserGroup = new DataTable();
            DataColumn colUGUserID = new DataColumn("colUserID");
            DataColumn colUGGroupID = new DataColumn("colGroupID");

            //add vào table
            tblUserGroup.Columns.Add(colUGUserID);
            tblUserGroup.Columns.Add(colUGGroupID);

            //add row vafo table user infor vaf ib user
            if (!string.IsNullOrEmpty(tblSUM.Rows[0]["colGroup"].ToString()))
            {
                DataRow row = tblUserGroup.NewRow();
                row["colUserID"] = tblSUM.Rows[0]["colIBUserName"];
                row["colGroupID"] = tblSUM.Rows[0]["colGroup"];
                tblUserGroup.Rows.Add(row);
            }
            #endregion

            #region Tạo bảng chứa Account của Contract

            #region lay tat ca cac account cua khach hang
            DataTable tblContractAccount = new DataTable();
            //tao bang chua thong tin account

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
            DataTable tblContractList = new DataTable();
            #endregion

            tblContractList = (DataTable)ViewState["AccountList"];
            if (tblContractList.Rows.Count > 0)
            {
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
                //if (tblSUM.Select("colRoleType='WAL' or colRoleType='EAM'").Length == 0)
                //{
                //    newRowCA1["colStatus"] = "I";
                //}
                //else
                //{
                //    newRowCA1["colStatus"] = "A";
                //}
                newRowCA1["colStatus"] = "A";
                if (CONTRACTTYPE.Equals(SmartPortal.Constant.IPC.CONTRACTINDIVIDUAL))
                {
                    newRowCA1["colBankAcctype"] = SmartPortal.Constant.IPC.EW;
                }
                else
                {
                    string result = new SmartPortal.SEMS.Services().GetUserType(userType, "", ref IPCERRORCODE, ref IPCERRORDESC).Tables[0].Rows[0]["TYPE"].ToString();
                    newRowCA1["colBankAcctype"] = result == SmartPortal.Constant.IPC.MERCHANT ? "MC" : "AG";
                }
                newRowCA1["colBranchID"] = DropDownListBranch.SelectedValue;
                tblContractAccount.Rows.Add(newRowCA1);
            }
            else
            {
                if (walletOnly)
                {
                    //ADD ACCOUNT WALLET
                    DataRow newRowCA1 = tblContractAccount.NewRow();
                    newRowCA1["colAContractNo"] = contractNo;
                    newRowCA1["colAcctNo"] = walletID;
                    newRowCA1["colAcctType"] = SmartPortal.Constant.IPC.EW;
                    newRowCA1["colCCYID"] = Resources.labels.lak;
                    //if (tblSUM.Select("colRoleType='WAL' or colRoleType='EAM'").Length == 0)
                    //{
                    //    newRowCA1["colStatus"] = "I";
                    //}
                    //else
                    //{
                    //    newRowCA1["colStatus"] = "A";
                    //}
                    newRowCA1["colStatus"] = "A";
                    if (CONTRACTTYPE.Equals(SmartPortal.Constant.IPC.CONTRACTINDIVIDUAL))
                    {
                        newRowCA1["colBankAcctype"] = SmartPortal.Constant.IPC.EW;
                    }
                    else
                    {
                        string result = new SmartPortal.SEMS.Services().GetUserType(userType, "", ref IPCERRORCODE, ref IPCERRORDESC).Tables[0].Rows[0]["TYPE"].ToString();
                        newRowCA1["colBankAcctype"] = result == SmartPortal.Constant.IPC.MERCHANT ? "MC" : "AG";
                    }
                    newRowCA1["colBranchID"] = DropDownListBranch.SelectedValue;
                    tblContractAccount.Rows.Add(newRowCA1);
                }
            }

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


            DataRow[] arrTran = tblSUM.Select(" colTranCodeID <> ''  ");



            foreach (DataRow rTR in arrTran)
            {
                if (tblTranrightDetail.Select("colUserID='" + rTR["colIBUserName"].ToString() + "' and colAcctNoTR='" + rTR["colAccount"].ToString() + "' and colTranCode='" + rTR["colTranCodeID"].ToString() + "'").Length == 0)
                {
                    if (rTR["colIBUserName"].ToString().Trim() != "")
                    {
                        DataRow newRowTR = tblTranrightDetail.NewRow();
                        newRowTR["colUserID"] = rTR["colIBUserName"].ToString();
                        if (rTR["colRoleType"].ToString() == SmartPortal.Constant.IPC.WAL || rTR["colRoleType"].ToString() == SmartPortal.Constant.IPC.EAM)
                        {
                            newRowTR["colAcctNoTR"] = walletID;
                        }
                        else
                        {
                            newRowTR["colAcctNoTR"] = rTR["colAccount"].ToString();
                        }

                        newRowTR["colTranCode"] = rTR["colTranCodeID"].ToString();
                        newRowTR["colServiceID"] = rTR["colServiceID"].ToString();
                        newRowTR["colLimit"] = "0";

                        tblTranrightDetail.Rows.Add(newRowTR);
                    }
                }
            }
            #endregion
            //return;
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
            if (CONTRACTTYPE.Equals(SmartPortal.Constant.IPC.CONTRACTINDIVIDUAL))
            {
                DataRow[] arrUserAcc = tblSUM.Select("colServiceID='" + SmartPortal.Constant.IPC.MB + "' and colRoleType ='" + SmartPortal.Constant.IPC.NORMAL + "'");
                foreach (DataRow rUA in arrUserAcc)
                {
                    if (tblUserAccount.Select("colUserIDUC='" + rUA["colIBUserName"].ToString() + "' and colAcctNoUC='" + rUA["colAccount"].ToString() + "' and colRoleIDUC='" + rUA["colRoleID"].ToString() + "'").Length == 0)
                    {
                        DataRow newRowUA = tblUserAccount.NewRow();
                        newRowUA["colUserIDUC"] = USERTK;
                        newRowUA["colAcctNoUC"] = rUA["colAccount"].ToString();
                        newRowUA["colRoleIDUC"] = rUA["colRoleID"].ToString();
                        newRowUA["colUseFull"] = "N";
                        newRowUA["colDesc"] = "";
                        if ((newRowUA["colAcctNoUC"]).Equals(ddlDefaultAccountQT.SelectedValue.ToString().Trim()))
                        {
                            newRowUA["colIsDefault"] = "Y";
                        }
                        else
                        {
                            newRowUA["colIsDefault"] = "N";
                        }
                        newRowUA["colStatus"] = "Y";
                        tblUserAccount.Rows.Add(newRowUA);
                    }
                }
            }
            else
            {
                DataRow[] arrUserAcc = tblSUM.Select("colServiceID='" + SmartPortal.Constant.IPC.AM + "' and colRoleType ='" + SmartPortal.Constant.IPC.MBA + "'");
                foreach (DataRow rUA in arrUserAcc)
                {
                    if (tblUserAccount.Select("colUserIDUC='" + rUA["colIBUserName"].ToString() + "' and colAcctNoUC='" + rUA["colAccount"].ToString() + "' and colRoleIDUC='" + rUA["colRoleID"].ToString() + "'").Length == 0)
                    {
                        DataRow newRowUA = tblUserAccount.NewRow();
                        newRowUA["colUserIDUC"] = USERTK;
                        newRowUA["colAcctNoUC"] = rUA["colAccount"].ToString();
                        newRowUA["colRoleIDUC"] = rUA["colRoleID"].ToString();
                        newRowUA["colUseFull"] = "N";
                        newRowUA["colDesc"] = "";
                        if ((newRowUA["colAcctNoUC"]).Equals(ddlDefaultAccountQT.SelectedValue.ToString().Trim()))
                        {
                            newRowUA["colIsDefault"] = "Y";
                        }
                        else
                        {
                            newRowUA["colIsDefault"] = "N";
                        }
                        newRowUA["colStatus"] = "Y";
                        tblUserAccount.Rows.Add(newRowUA);
                    }
                }
            }

            DataRow[] arrUserAccIB = tblSUM.Select("colServiceID='" + SmartPortal.Constant.IPC.IB + "'and colRoleType ='" + SmartPortal.Constant.IPC.NORMAL + "'");
            foreach (DataRow rUA in arrUserAccIB)
            {
                if (tblUserAccount.Select("colUserIDUC='" + USERTK + "' and colAcctNoUC='" + rUA["colAccount"].ToString() + "' and colRoleIDUC='" + rUA["colRoleID"].ToString() + "'").Length == 0)
                {
                    DataRow newRowUA = tblUserAccount.NewRow();
                    newRowUA["colUserIDUC"] = USERTK;
                    newRowUA["colAcctNoUC"] = rUA["colAccount"].ToString();
                    newRowUA["colRoleIDUC"] = rUA["colRoleID"].ToString();
                    newRowUA["colUseFull"] = "N";
                    newRowUA["colDesc"] = "";
                    if ((newRowUA["colAcctNoUC"]).Equals(ddlDefaultAccountQT.SelectedValue.ToString().Trim()))
                    {
                        newRowUA["colIsDefault"] = "Y";
                    }
                    else
                    {
                        newRowUA["colIsDefault"] = "N";
                    }
                    newRowUA["colStatus"] = "Y";
                    tblUserAccount.Rows.Add(newRowUA);
                }

            }
            if (tblUserAccount.Select("colAcctNoUC='" + walletID + "'").Length == 0 && !SHOWTAB.Equals(string.Empty))
            {
                if (CONTRACTTYPE.Equals(SmartPortal.Constant.IPC.CONTRACTINDIVIDUAL))
                {
                    DataTable tblSS = new SmartPortal.SEMS.Contract().GetRoleByServiceAndContract("B", contractNo, SmartPortal.Constant.IPC.WAL);
                    foreach (DataRow rUA in tblSS.Rows)
                    {
                        DataRow newRowUA = tblUserAccount.NewRow();
                        newRowUA["colUserIDUC"] = USERTK;
                        newRowUA["colAcctNoUC"] = walletID;
                        newRowUA["colRoleIDUC"] = rUA["RoleID"].ToString();
                        newRowUA["colUseFull"] = "N";
                        newRowUA["colDesc"] = "";

                        if (tblSUM.Select("colRoleType='WAL' and colRoleID = '" + rUA["RoleID"].ToString() + "'").Length == 0)
                        {
                            newRowUA["colStatus"] = "Y";
                        }
                        else
                        {
                            newRowUA["colStatus"] = "N";
                        }
                        if (PHONE.Equals(ddlDefaultAccountQT.SelectedValue.ToString().Trim()))
                        {
                            newRowUA["colIsDefault"] = "Y";
                        }
                        else
                        {
                            newRowUA["colIsDefault"] = "N";
                        }
                        foreach(DataRow dr in tblUserAccount.Rows)
                        {
                            if (dr["colRoleIDUC"].ToString() == rUA["RoleID"].ToString())
                            {
                                tblUserAccount.Rows.Remove(dr);
                                break;
                            }
                        }
                        tblUserAccount.Rows.Add(newRowUA);
                    }
                }
                else
                {
                    DataTable tblSS = new SmartPortal.SEMS.Contract().GetRoleByServiceAndContract(SmartPortal.Constant.IPC.AM, contractNo, SmartPortal.Constant.IPC.EAM);

                    foreach (DataRow rUA in tblSS.Rows)
                    {
                        DataRow newRowUA = tblUserAccount.NewRow();
                        newRowUA["colUserIDUC"] = USERTK;
                        newRowUA["colAcctNoUC"] = walletID;
                        newRowUA["colRoleIDUC"] = rUA["RoleID"].ToString();
                        newRowUA["colUseFull"] = "N";
                        newRowUA["colDesc"] = "";

                        if (tblSUM.Select("colRoleType='EAM' and colRoleID = '" + rUA["RoleID"].ToString() + "'").Length == 0)
                        {
                            newRowUA["colStatus"] = "N";
                        }
                        else
                        {
                            newRowUA["colStatus"] = "Y";
                        }
                        if (PHONE.Equals(ddlDefaultAccountQT.SelectedValue.ToString().Trim()))
                        {
                            newRowUA["colIsDefault"] = "Y";
                        }
                        else
                        {
                            newRowUA["colIsDefault"] = "N";
                        }

                        tblUserAccount.Rows.Add(newRowUA);
                    }
                }
            }
            #endregion

            #region vutt Tạo bảng chứa thông tin sms notify 04022016
            string username = (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["a"].ToString().Equals(SmartPortal.Constant.IPC.EDIT) ? txtIBUserName.Text.Trim() : "");
            DataTable dtAcctRoles = (new SmartPortal.SEMS.Transactions().DoStored("EBA_GETACCOUNTROLE_BYCONTRACT", new object[3] { contractNo, "SNO", username }, ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
            DataTable tblSMSNotify = ContractControl.CreateSMSNotifyDetailTable(tblUserAccount, tblContractAccount, "U", contractNo, dtAcctRoles);
            //return;
            #endregion

            #region Tạo bảng UPDATE PhoneNO
            //tao bang chua thong tin customer
            DataTable tblUpdatePhoneNo = new DataTable();
            DataColumn colUserIDUD = new DataColumn("colUserIDUD");
            DataColumn colPhoneNoUP = new DataColumn("colPhoneNoUP");

            //add vào table
            tblUpdatePhoneNo.Columns.Add(colUserIDUD);
            tblUpdatePhoneNo.Columns.Add(colPhoneNoUP);
            //tao 1 dong du lieu
            DataRow neR = tblUpdatePhoneNo.NewRow();

            neR["colUserIDUD"] = USERTK;
            neR["colPhoneNoUP"] = txtMBPhoneNo.Text;

            tblUpdatePhoneNo.Rows.Add(neR);

            #endregion


            if (ddlpolicyMB.SelectedValue.ToString().Equals(""))
            {
                lblError.Text = "policy not null";
                return;
            }
            switch (ACTION)
            {
                case SmartPortal.Constant.IPC.ADD:
                    #region insert user
                    if (tblSMSUserRight.Rows.Count == 0)
                    {
                        SMSPhoneNo = "";
                    }
                    IBStatus = SmartPortal.Constant.IPC.NEW;
                    SMSStatus = SmartPortal.Constant.IPC.NEW;
                    MBStatus = SmartPortal.Constant.IPC.NEW;
                    PHOStatus = SmartPortal.Constant.IPC.NEW;
                    status = SmartPortal.Constant.IPC.NEW;


                    if (CONTRACTTYPE.Equals(IPC.CONTRACTINDIVIDUAL))
                    {
                        new SmartPortal.SEMS.AgentMerchant().AddConsummer(contractNo, CONTRACTTYPE, localName, UserNameMB, lastModify, userCreate, userApprove, status, USERTK, userFullName, userGender, userAddress, userEmail, userPhone, uCreateDate, userCreate, userType, userLevel, type, "", tokenID, tokenIssueDate, smsOTP, userBirthday, MBPhoneNo, MBPass, MBStatus, tblMBUserRight, tblIbankUserRight, tblUserGroup, new DataTable(), tblUserAccount, ddlpolicyMB.SelectedValue.ToString(),ddlpolicyIB.SelectedValue.ToString(), pwdreset, Session["userID"].ToString(), new DataTable(), ref IPCERRORCODE, ref IPCERRORDESC);
                    }
                    else
                    {
                        new SmartPortal.SEMS.AgentMerchant().AddAgentMerchant(contractNo, CONTRACTTYPE, localName, UserNameMB, lastModify, userCreate, userApprove, status, USERTK, userFullName, userGender, userAddress, userEmail, userPhone, uCreateDate, userCreate, userType, userLevel, type, "", tokenID, tokenIssueDate, smsOTP, userBirthday, MBPhoneNo, MBPass, MBStatus, tblMBUserRight, tblUserGroup, new DataTable(), tblUserAccount, ddlpolicyMB.SelectedValue.ToString(), pwdreset, new DataTable(), ref IPCERRORCODE, ref IPCERRORDESC);
                    }

                    if (IPCERRORCODE != "0")
                    {
                        lblError.Text = IPCERRORDESC.ToString();
                        return;
                    }

                    if (IPCERRORCODE == "0")
                    {
                        ViewState["NGUOIQUANTRI"] = null;
                        ltrError.Text = Resources.labels.addusersuccessfull;
                        btsaveandcont.Visible = false;
                        btnThemNQT.Visible = false;
                        btnHuy.Visible = false;
                        // write log db add link mobile
                        if (tblMBUserRight.Rows.Count > 0)
                        {
                            try
                            {
                                SmartPortal.Common.Log.WriteLogDatabase(contractNo, USERTK, Request.Url.ToString(), Session["userName"].ToString(),
                                Request.UserHostAddress, "MB_USER", "Add Link Moblie", "", "MB", SmartPortal.Constant.IPC.PENDING);
                            }
                            catch { }
                        }
                        // write log db add new user
                        try
                        {
                            SmartPortal.Common.Log.WriteLogDatabase(contractNo, USERTK, Request.Url.ToString(), Session["userName"].ToString(),
                            Request.UserHostAddress, "EBA_USER", "Add new user", "", USERTK, SmartPortal.Constant.IPC.PENDING);
                        }
                        catch { }
                    }
                    else
                    {
                        ViewState["NGUOIQUANTRI"] = null;
                        throw new IPCException(SmartPortal.Constant.IPC.ERRORCODE.ADDUSERERROR);
                    }

                    #endregion

                    break;
                case SmartPortal.Constant.IPC.EDIT:
                    IBStatus = SmartPortal.Constant.IPC.PENDING;
                    MBStatus = SmartPortal.Constant.IPC.PENDING;
                    PHOStatus = SmartPortal.Constant.IPC.PENDING;
                    status = SmartPortal.Constant.IPC.PENDING;

                    USERTK = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["uid"].ToString();

                    //DataTable contractInfo = new DataTable();

                    contractInfo = (new SmartPortal.SEMS.Contract().GetContractByContractNo(contractNo, ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
                    if (contractInfo.Rows.Count != 0)
                    {
                        endDate = SmartPortal.Common.Utilities.Utility.IsDateTime2(contractInfo.Rows[0]["ENDDATE"].ToString().Trim()).ToString("dd/MM/yyyy");
                    }

                    //DataTable tblNGUOIQUANTRI = new DataTable();

                    if (ViewState["NGUOIQUANTRI"] != null)
                    {
                        tblNGUOIQUANTRI = (DataTable)ViewState["NGUOIQUANTRI"];
                    }

                    #region update user
                    new SmartPortal.SEMS.User().Update(contractNo, localName, UserNameMB, authenType, loginMethod, CONTRACTTYPE, USERTK, userFullName, status, userGender, userAddress, userEmail, PhoneNumberMB, userCreate, userType, userLevel, type, deptID, tokenID, smsOTP, userBirthday, UserNameMB, "", IBStatus, lastModify, SMSPhoneNo, SMSDefaultAcctno, (cbIsDefault.Checked == true ? "Y" : "N"), SMSStatus, ddlDefaultLang.SelectedValue, PhoneNumberMB, "", MBStatus, string.Empty, "", PHOStatus, string.Empty, tblIbankUserRight, tblSMSUserRight, tblMBUserRight, tblPHOUserRight, tblUserGroup, new DataTable(), tblUserAccount, tblContractAccount, tblUpdatePhoneNo, ddlpolicyIB.SelectedValue.ToString(), ddlpolicySMS.SelectedValue.ToString(), ddlpolicyMB.SelectedValue.ToString(), pwdresetsms, tblSMSNotify, ref IPCERRORCODE, ref IPCERRORDESC);


                    if (IPCERRORCODE == "0")
                    {
                        ViewState["NGUOIQUANTRI"] = null;
                        ltrError.Text = Resources.labels.editusersuccessfull;
                        btsaveandcont.Visible = false;
                        hideAll();
                        string text = "";
                        if (birthdayOld != txtReBirth.Text.Trim())
                        {
                            text = birthdayOld == "" ? text = "Add" : "Change";
                            SmartPortal.Common.Log.WriteLogDatabase(contractNo, USERTK, Request.Url.ToString(), Session["userName"].ToString(),
                                 Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLCONTRACT"],
                       text + " Birthday", birthdayOld, txtReBirth.Text.Trim(), SmartPortal.Constant.IPC.PENDING);
                            birthdayOld = null;
                        }
                        if (emailOld != userEmail)
                        {
                            text = emailOld == "" ? text = "Add" : "Change";
                            SmartPortal.Common.Log.WriteLogDatabase(contractNo, USERTK, Request.Url.ToString(), Session["userName"].ToString(),
                           Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLCONTRACT"],
                           text + " Email", emailOld, userEmail, SmartPortal.Constant.IPC.PENDING);
                            emailOld = null;
                        }
                        if (localFullName != userLocalFullName)
                        {
                            text = localFullName == "" ? text = "Add" : "Change";
                            SmartPortal.Common.Log.WriteLogDatabase(contractNo, USERTK, Request.Url.ToString(), Session["userName"].ToString(),
                           Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLCONTRACT"],
                           text + " Local Full Name", localFullName, userLocalFullName, SmartPortal.Constant.IPC.PENDING);
                            localFullName = null;
                        }
                        if (genderOld != userGender)
                        {
                            text = genderOld == "" ? text = "Add" : "Change";
                            SmartPortal.Common.Log.WriteLogDatabase(contractNo, USERTK, Request.Url.ToString(), Session["userName"].ToString(),
                              Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLCONTRACT"],
                            text + " Gender", genderOld == "M" ? Resources.labels.nam : Resources.labels.nu, userGender == "M" ? Resources.labels.nam : Resources.labels.nu, SmartPortal.Constant.IPC.PENDING);
                            genderOld = null;
                        }
                        if (phoneOld != userPhone)
                        {
                            text = phoneOld == "" ? text = "Add" : "Change";
                            SmartPortal.Common.Log.WriteLogDatabase(contractNo, USERTK, Request.Url.ToString(), Session["userName"].ToString(),
                          Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLCONTRACT"],
                          text + " Phone", phoneOld, userPhone, SmartPortal.Constant.IPC.PENDING);
                            phoneOld = null;
                        }
                        if (addressOld != userAddress)
                        {
                            text = addressOld == "" ? text = "Add" : "Change";
                            SmartPortal.Common.Log.WriteLogDatabase(contractNo, USERTK, Request.Url.ToString(), Session["userName"].ToString(),
                         Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLCONTRACT"],
                         text + " Address", addressOld, userAddress, SmartPortal.Constant.IPC.PENDING);
                            addressOld = null;
                        }

                        //  acount tblUserAccount
                        try
                        {
                            DataTable tblUserAccountold = new DataTable();
                            tblUserAccountold = (DataTable)ViewState["UserRolesOld"];

                            DataTable tblUserAcct = tblUserAccount.DefaultView.ToTable(true, "colUserIDUC", "colAcctNoUC");

                            for (int i = 0; i < tblUserAcct.Rows.Count; i++)
                            {
                                if (tblUserAcct.Rows[i]["colAcctNoUC"].Equals(walletID))
                                {
                                    tblUserAcct.Rows.RemoveAt(i);
                                }
                            }
                            for (int i = 0; i < tblUserAccountold.Rows.Count; i++)
                            {
                                if (tblUserAccountold.Rows[i]["colRoleType"].Equals(SmartPortal.Constant.IPC.WAL) || tblUserAccountold.Rows[i]["colRoleType"].Equals(SmartPortal.Constant.IPC.EAM))
                                {
                                    tblUserAccountold.Rows.RemoveAt(i);
                                }
                            }
                            foreach (DataRow dr in tblUserAcct.Rows)
                            {
                                try
                                {
                                    if (tblUserAccountold.Select("colAccount = '" + dr["colAcctNoUC"].ToString() + "'").Length == 0)
                                    {
                                        SmartPortal.Common.Log.WriteLogDatabase(contractNo, dr["colUserIDUC"].ToString(), Request.Url.ToString(), Session["userName"].ToString(),
                                        Request.UserHostAddress, "EBA_USERACCOUNT", "Add Bank Account", "", dr["colAcctNoUC"].ToString(), SmartPortal.Constant.IPC.PENDING);
                                    }
                                }
                                catch (Exception ex)
                                { }
                            }
                            foreach (DataRow dr in tblUserAccountold.Rows)
                            {
                                try
                                {
                                    if (tblUserAcct.Select("colAcctNoUC = '" + dr["colAccount"].ToString() + "'").Length == 0)
                                    {
                                        SmartPortal.Common.Log.WriteLogDatabase(contractNo, dr["colAccount"].ToString(), Request.Url.ToString(), Session["userName"].ToString(),
                                           Request.UserHostAddress, "EBA_USERACCOUNT", "Delete Bank Account", dr["colAccount"].ToString(), dr["colAccount"].ToString(), SmartPortal.Constant.IPC.PENDING);
                                    }
                                }
                                catch (Exception ex)
                                { }
                            }
                        }
                        catch { }
                    }
                    else
                    {
                        ViewState["NGUOIQUANTRI"] = null;
                        //SmartPortal.Common.Log.RaiseError(IPCERRORDESC.ToString(), this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCERRORDESC.ToString(), Request.Url.Query);
                        throw new IPCException(SmartPortal.Constant.IPC.ERRORCODE.UPDATEUSERERROR);
                    }
                    #endregion
                    break;
            }

        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(IPCex.ToString(), this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(IPCex.Message, Request.Url.Query);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSUser_Widget", "btsaveandcont_Click", ex.StackTrace, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }

    }
    protected void btback_Click(object sender, EventArgs e)
    {
        ViewState["NGUOIQUANTRI"] = null;
        if (CONTRACTNO != "")
        {
            switch (ACTION)
            {
                case SmartPortal.Constant.IPC.EDIT:
                    Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/default.aspx?p=SEMSContractListEdit&a=EDIT&ID=" + CONTRACTNO), false);

                    break;
                case SmartPortal.Constant.IPC.VIEWDETAIL:
                    try
                    {
                        if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["pagefather"].ToString().Equals("SEMSDetailContractApprove"))
                        {
                            Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/default.aspx?p=SEMSDetailContractApprove&a=DETAILS&ID=" + CONTRACTNO), false);
                            break;
                        }
                        else
                        {
                            if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["pagefather"].ToString().Equals("SEMSContractListEdit"))
                            {
                                Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/default.aspx?p=SEMSContractListEdit&a=EDIT&ID=" + CONTRACTNO), false);
                                break;

                            }
                            else
                            {
                                Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/default.aspx?p=146&a=DETAILS&ID=" + CONTRACTNO), false);
                                break;
                            }

                        }
                    }
                    catch
                    {
                        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/default.aspx?p=146&a=DETAILS&ID=" + CONTRACTNO), false);
                        break;
                    }

                case SmartPortal.Constant.IPC.APPROVER:
                    Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/default.aspx?p=149&a=APPROVE"), false);
                    break;
                case SmartPortal.Constant.IPC.ADD:
                    Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/default.aspx?p=SEMSContractListEdit&a=EDIT&ID=" + CONTRACTNO), false);

                    break;
            }
        }
        else
        {
            RedirectBackToMainPage();
        }
    }
    protected void ddlUserType_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (!corptype.Equals(SmartPortal.Constant.IPC.CONTRACTCORPMATRIX))
        {
            DataSet dsType = new DataSet();

            dsType = new SmartPortal.SEMS.User().LoadTypeByParentType(SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlUserType.SelectedValue.ToString()), ref IPCERRORCODE, ref IPCERRORDESC);

            if (dsType.Tables[0].Rows.Count == 0)
            {
                ddlType.Items.Clear();
                ddlType.Items.Insert(0, new ListItem(Resources.labels.khongtimthayloainguoidung, ""));
            }
            else
            {
                ddlType.DataSource = dsType;
                ddlType.DataTextField = "TYPENAME";
                ddlType.DataValueField = "TYPELEVEL";
                ddlType.DataBind();
            }
        }
        else
        {
            ddlType.Enabled = false;
        }
        txtIBUserName.Text = userName;
        //txtUserNameMB.Text = txtIBUserName.Text;
        txtUserNameMB.Text = userName;
    }
    protected void btnHuy_Click(object sender, EventArgs e)
    {
        try
        {

            DataTable tblResultQT = (DataTable)ViewState["NGUOIQUANTRI"];

            tblResultQT.Rows.Clear();

            ViewState["NGUOIQUANTRI"] = tblResultQT;
            gvResultQuanTri.DataSource = tblResultQT;
            gvResultQuanTri.DataBind();

            lblAlert.Text = Resources.labels.recorddeleted;
            btsaveandcont.Visible = false;
        }
        catch
        {
        }
    }
    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["ct"] == SmartPortal.Constant.IPC.PERSONALCONTRACT)
        {
            //load for IB
            LoadDataInTreeview(SmartPortal.Constant.IPC.IB, tvIB, contractNo, string.Empty);

            //load for SMSsss
            LoadDataInTreeview(SmartPortal.Constant.IPC.SMS, tvSMSQT, contractNo, string.Empty);

            //load for MB
            LoadDataInTreeview(SmartPortal.Constant.IPC.MB, tvMBQT, contractNo, SmartPortal.Constant.IPC.NORMAL);

            //load for WL
            LoadDataInTreeview(SmartPortal.Constant.IPC.MB, tvWL, contractNo, SmartPortal.Constant.IPC.WAL);

            ////load for AM
            //LoadDataInTreeview(SmartPortal.Constant.IPC.AM, tvAM, contractNo);

        }
        if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["ct"] == SmartPortal.Constant.IPC.CORPORATECONTRACT)
        {
            //load for IB
            LoadDataInTreeview1(SmartPortal.Constant.IPC.IB, tvIB, ddlType.SelectedValue.Split('#')[0]);

            //load for SMS
            LoadDataInTreeview1(SmartPortal.Constant.IPC.SMS, tvSMSQT, userType);

            //load for MB
            LoadDataInTreeview1(SmartPortal.Constant.IPC.MB, tvMBQT, userType);



            ////load for AM
            //LoadDataInTreeview1(SmartPortal.Constant.IPC.PHO, tvPHOQT, userType);
        }
      //  txtIBUserName.Text = GetUserName(ddlUserType.SelectedValue.ToString().Trim(), ddlType.SelectedValue.Trim(), txtReFullName.Text);
        txtUserID.Text = txtIBUserName.Text;
    }
    protected void btn_HuySMS_Click(object sender, EventArgs e)
    {
        int result = new SmartPortal.SEMS.Contract().DeleteSMSPhone(txtSMSPhoneNo.Text.Trim(), SmartPortal.Constant.IPC.DELETE, Session["UserName"].ToString(), DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
        if (result != 0)
        {
            lblerrorSMS.Text = Resources.labels.dahuysodienthoaidangkysms;
            txtSMSPhoneNo.Text = "";
            btsaveandcont.Visible = false;
        }
        else
        {

        }
    }
    protected void btnCustomerDetail_Click(object sender, EventArgs e)
    {
        try
        {
            ltrError.Text = string.Empty;
            lblError.Text = string.Empty;
            txtReFullName.Text = string.Empty;
            txtReEmail.Text = string.Empty;
            txtReMobi.Text = string.Empty;
            ddlReGender.SelectedIndex = 0;
            txtReBirth.Text = string.Empty;
            txtReAddress.Text = string.Empty;
            string custcode = string.Empty;
            custcode = txtCustomerCode.Text.Trim();

            string resultUserName = new Customer().UsernameGeneration("6", ref IPCERRORCODE, ref IPCERRORDESC);
            if (resultUserName.Equals(string.Empty) || resultUserName.Length != 10 || !CheckUserNameExists(resultUserName))
            {
                lblError.Text = Resources.labels.usernamehasexists;
                return;
            }
            txtUserNameMB.Text = resultUserName;

            if (txtCustomerCode.Equals("") || txtCustomerCode.Equals(string.Empty))
            {
                return;
            }
            Hashtable hasCustomerInfo = new Hashtable();
            string ctmType = "P";
            hasCustomerInfo = new SmartPortal.SEMS.Customer().GetCustInfo(txtCustomerCode.Text.Trim(), ctmType, ref IPCERRORCODE, ref IPCERRORDESC);

            if (IPCERRORCODE != "0")
            {
                lblError.Text = Resources.labels.makhachhangkhongtontaitronghethong;
            }
            if (hasCustomerInfo[SmartPortal.Constant.IPC.CUSTCODE] == null)
            {
                ShowPopUpMsg(Resources.labels.makhachhangkhongtontaitronghethong);
                return;
            }

            //minh modify 3.11.2015 tach generate user name for corporate
            if (hasCustomerInfo[SmartPortal.Constant.IPC.CUSTNAME] != null && hasCustomerInfo[SmartPortal.Constant.IPC.LICENSE] != null)
            {
                txtReFullName.Text = hasCustomerInfo[SmartPortal.Constant.IPC.CUSTNAME].ToString();
            }
            if (hasCustomerInfo[SmartPortal.Constant.IPC.EMAIL] != null)
                txtReEmail.Text = hasCustomerInfo[SmartPortal.Constant.IPC.EMAIL].ToString();
            if (hasCustomerInfo[SmartPortal.Constant.IPC.PHONE] != null)
            {
                txtReMobi.Text = hasCustomerInfo[SmartPortal.Constant.IPC.PHONE].ToString().Trim();
                txtMBPhoneNo.Text = txtReMobi.Text;
            }
            else
            {
                btnThemNQT.Visible = false;
                lblError.Text = Resources.labels.phonenumberwrong;
            }
            if (hasCustomerInfo[SmartPortal.Constant.IPC.SEX] != null)
            {
                try
                {
                    SEX = ddlReGender.SelectedValue = SmartPortal.Common.Utilities.Utility.FormatStringCore(int.Parse(hasCustomerInfo[SmartPortal.Constant.IPC.SEX].ToString()).ToString());

                    if (hasCustomerInfo[SmartPortal.Constant.IPC.SEX].ToString().Trim() == "")
                    {
                        ddlReGender.Enabled = true;
                    }
                    else
                    {
                        ddlReGender.Enabled = false;
                    }
                }
                catch
                {
                }
            }
            if (hasCustomerInfo[SmartPortal.Constant.IPC.DOB] != null)
            {
                try
                {
                    string birthDate = SmartPortal.Common.Utilities.Utility.IsDateTime2(hasCustomerInfo[SmartPortal.Constant.IPC.DOB].ToString()).ToString("dd/MM/yyyy");
                    txtReBirth.Text = birthDate.Equals("01/01/1900") ? "" : birthDate;

                    if (txtReBirth.Text.Trim() == "")
                    {
                        txtReBirth.Enabled = true;
                    }
                    else
                    {
                        txtReBirth.Enabled = false;
                    }
                }
                catch
                {
                }
            }
            if (hasCustomerInfo[SmartPortal.Constant.IPC.ADDRESS] != null)
            {
                txtReAddress.Text = hasCustomerInfo[SmartPortal.Constant.IPC.ADDRESS].ToString();
            }
            if (hasCustomerInfo[SmartPortal.Constant.IPC.BRID] != null)
            {
                DropDownListBranch.SelectedValue = hasCustomerInfo[SmartPortal.Constant.IPC.BRID].ToString();
            }
            if (hasCustomerInfo[SmartPortal.Constant.IPC.LICPLACE] != null)
            {
                ISSUEPLACE = hasCustomerInfo[SmartPortal.Constant.IPC.LICPLACE].ToString();
            }
            if (hasCustomerInfo[SmartPortal.Constant.IPC.LICDATE] != null)
            {
                try
                {
                    ISSUEDATE = SmartPortal.Common.Utilities.Utility.IsDateTime1(hasCustomerInfo[SmartPortal.Constant.IPC.LICDATE].ToString()).ToString("dd/MM/yyyy");
                }
                catch
                {
                }
            }
            if (hasCustomerInfo[SmartPortal.Constant.IPC.LICENSE] != null)
            {
                LICENSEID = hasCustomerInfo[SmartPortal.Constant.IPC.LICENSE].ToString();
            }
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(IPCex.ToString(), this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(IPCex.Message, Request.Url.Query);

        }
        catch (Exception ex)

        {
            //SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSCustomerList_Add_Widget", "btnThemNQT_Click", ex.ToString(), Request.Url.Query);
            //SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    //28/8/2015 minh add to show message error trong truong hop su dung panel ajax

    private string GetUserName(string usertype, string type, string cusname, string license = "ID")
    {
        string userName = string.Empty;


        Random random = new Random();
        StringBuilder result = new StringBuilder(10);

        if (string.IsNullOrEmpty(cusname) || cusname.Length < 2)
        {
            string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            for (int i = 0; i < 2; i++)
            {
                var a = characters[random.Next(characters.Length)];
                result.Append(characters[random.Next(characters.Length)]);
            }
            cusname += result.ToString();
        }
        string cuscode = txtCustomerCode.Text.Trim();
        if (string.IsNullOrEmpty(cuscode) || cuscode.Length < 2)
        {
            result = new StringBuilder(10);
            string number = "0123456789";
            for (int i = 0; i < 6; i++)
            {
                result.Append(number[random.Next(number.Length)]);
            }
            cuscode += result.ToString();
        }
        switch (usertype)
        {
            case SmartPortal.Constant.IPC.CORPORATECONTRACT:
                //string test;
                //test = ddlType.SelectedValue.ToString().Trim().Substring(ddlType.SelectedValue.ToString().Trim().IndexOf("#")+1);
                string userlevel = string.Empty;
                string itemadd = string.Empty;
                userlevel = type.Split('#')[0];
                switch (userlevel)
                {
                    case "MTR":
                        itemadd = "1";
                        break;
                    case "QLTC":
                        itemadd = "2";
                        break;
                    case "KT":
                        itemadd = "3";
                        break;
                    case "CTK":
                        itemadd = "1";
                        break;
                    case "C2":
                        itemadd = "3";
                        break;
                    case "QTHT":
                        itemadd = "4";
                        break;
                    case "NUY":
                        itemadd = "2";
                        break;
                    case "NB":
                        itemadd = "2";
                        break;

                }
                //userName = generateUserName();
                userName = SmartPortal.Common.Utilities.Utility.GetID(cusname, cuscode, license) + "O" + itemadd;
                break;
            default:
                //userName = generateUserName();
                userName = SmartPortal.Common.Utilities.Utility.GetID(cusname, cuscode, license);
                break;

        }
        return userName;
    }
    private void ShowPopUpMsg(string msg)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("alert('");
        sb.Append(msg.Replace("\n", "\\n").Replace("\r", "").Replace("'", "\\'"));
        sb.Append("');");
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "showalert", sb.ToString(), true);
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
    string generateUserName(bool isMatrix = true)
    {
        string username = txtIBUserName.Text;
        if (string.IsNullOrEmpty(username))
            if (!string.IsNullOrEmpty(txtReFullName.Text.Trim()) && !string.IsNullOrEmpty(txtReMobi.Text.Trim()))
            {
                string cifno = txtReMobi.Text.Length < 2 ? txtReMobi.Text + "0123456789" : txtReMobi.Text;
                string preid = txtReAddress.Text + txtReEmail.Text + "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                username = SmartPortal.Common.Utilities.Utility.GetID(txtReFullName.Text, cifno, preid) + (isMatrix ? "O1" : "");

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
                username = result.ToString() + (isMatrix ? "O1" : "");
            }
        return username;
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
        string resultInfo = new Customer().CheckPhoneNumberAgent(phone, CONTRACTNO, ref IPCERRORCODE, ref IPCERRORDESC);
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
    public void ShowTab()
    {
        try
        {
            if (SHOWTAB.Equals(Resources.labels.walletbanking))
            {
                liTabWL.Visible = true;
                liTabAM.Visible = false;
                liTabIB.Visible = true;
            }
            else if (SHOWTAB.Equals(Resources.labels.agentmerchant))
            {
                liTabWL.Visible = false;
                liTabAM.Visible = true;
                liTabIB.Visible = false;
            }
            else
            {
                liTabWL.Visible = false;
                liTabAM.Visible = false;
                liTabIB.Visible = false;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
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
    private bool CheckUserNameExists(string userName)
    {

        DataSet ds;
        if (CONTRACTTYPE.Equals(SmartPortal.Constant.IPC.CONTRACTINDIVIDUAL))
        {
            ds = new Customer().CheckUserName("EBA_Users_CheckUserNameMB", new object[] { userName, SmartPortal.Constant.IPC.CONTRACTINDIVIDUAL }, ref IPCERRORCODE, ref IPCERRORDESC);
        }
        else
        {
            ds = new Customer().CheckUserName("EBA_Users_CheckUserNameMB", new object[] { userName, SmartPortal.Constant.IPC.AGENTMERCHANT }, ref IPCERRORCODE, ref IPCERRORDESC);
        }
        if (IPCERRORCODE != "0")
        {
            lblAlert.Text = IPCERRORDESC;
            return false;
        }
        //if (!validateusername(txtUserNameMB))
        //{
        //    return false;
        //}
        return true;

    }
    private bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }
    private string GenerateUserName()
    {
        NumberReg++;
        string resultUserName = string.Empty;
        string userType = LoadSubUserType(USERTYPE);
        switch (userType)
        {
            case "0101":
                resultUserName = new Customer().UsernameGeneration("3", ref IPCERRORCODE, ref IPCERRORDESC);
                break;
            case "0102":
                resultUserName = new Customer().UsernameGeneration("2", ref IPCERRORCODE, ref IPCERRORDESC);
                break;
            case "0103":
                resultUserName = new Customer().UsernameGeneration("1", ref IPCERRORCODE, ref IPCERRORDESC);
                break;
            case "0201":
            case "0202":
            case "0203":
                resultUserName = new Customer().UsernameGeneration("6", ref IPCERRORCODE, ref IPCERRORDESC);
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
        lblAlert.Text = string.Empty;
        return resultUserName;
    }
    protected void CreateUserName_Click(object sender, EventArgs e)
    {
        NumberReg = 0;
        txtUserNameMB.Enabled = true;
        txtUserNameMB.Text = GenerateUserName();
    }
    protected void ChangePhone_Click(object sender, EventArgs e)
    {
        txtMBPhoneNo.Enabled = true;
    }
    protected void ChangePhoneAndUserName_Click(object sender, EventArgs e)
    {
        bool allowSave = true;
        if (!CheckIsPhoneNumer(txtMBPhoneNo.Text.Trim().ToString()) || txtMBPhoneNo.Text.Trim().ToString().Equals(""))
        {
            lblError.Text = Resources.labels.phonenumberwrong;
            allowSave = false;
        }
        else
        {
            string result = string.Empty;
            if (txtUserNameMB.Text.Trim().Length < minlength || txtUserNameMB.Text.Trim().Length > maxlength)
            {
                lblAlert.Text = string.Format(Resources.labels.usernamemustbetween, minlength, maxlength);
                allowSave = false;
            }
            result = new User().CheckPhoneAndUserName(USERTK, SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtUserNameMB.Text.Trim()), "USERNAME", CONTRACTTYPE, ref IPCERRORCODE, ref IPCERRORDESC);
            if (!result.Equals("0") || txtUserNameMB.Text.Trim().ToString().Equals(""))
            {
                lblError.Text = Resources.labels.usernamehasexists;
                allowSave = false;
            }
        }
        if (!allowSave)
        {
            btnThemNQT.Visible = false;
        }
        else
        {
            txtReMobi.Text = txtMBPhoneNo.Text;
            txtIBUserName.Text = txtUserNameMB.Text.Trim();
            txtWLPhoneNo.Text = txtMBPhoneNo.Text;
            UpdatemobileService(txtMBPhoneNo.Text);
            btnThemNQT.Visible = true;
        }

    }
    private void UpdatemobileService(string phoneNo)
    {
        DataTable tblNGUOIQUANTRI;
        if (ViewState["NGUOIQUANTRI"] != null)
        {
            tblNGUOIQUANTRI = (DataTable)ViewState["NGUOIQUANTRI"];
            foreach (DataRow dr in tblNGUOIQUANTRI.Rows)
            {
                dr["colPhone"] = phoneNo;
                dr["colMBPhone"] = phoneNo;
                dr["colWLPhone"] = phoneNo;
                if (dr["colRoleType"].Equals(SmartPortal.Constant.IPC.EAM) || dr["colRoleType"].Equals(SmartPortal.Constant.IPC.WAL))
                {
                    dr["colAccount"] = phoneNo;
                }
            }
            ViewState["NGUOIQUANTRI"] = tblNGUOIQUANTRI;
            gvResultQuanTri.DataSource = tblNGUOIQUANTRI.DefaultView.ToTable(true, "colFullName", "colPhone", "colAccount", "colRole");
            gvResultQuanTri.DataBind();
        }

    }
    protected void ddlLoginMethod_Click(object sender, EventArgs e)
    {
        DataTable tblNGUOIQUANTRI;
        if (ViewState["NGUOIQUANTRI"] != null)
        {
            tblNGUOIQUANTRI = (DataTable)ViewState["NGUOIQUANTRI"];
            foreach (DataRow dr in tblNGUOIQUANTRI.Rows)
            {
                dr["colLoginMethod"] = ddlLoginMethod.SelectedValue;
            }
            ViewState["NGUOIQUANTRI"] = tblNGUOIQUANTRI;
            gvResultQuanTri.DataSource = tblNGUOIQUANTRI.DefaultView.ToTable(true, "colFullName", "colPhone", "colAccount", "colRole");
            gvResultQuanTri.DataBind();
        }

    }
    private void LoadComboboxReason()
    {
        try
        {
            DataTable dtReason = new SmartPortal.SEMS.Contract().GetReason("", "", "", CONTRACTTYPE, "CU", ref IPCERRORCODE, ref IPCERRORDESC).Tables[0];
            ddlReason.DataSource = dtReason;
            ddlReason.DataTextField = "REASONNAME";
            ddlReason.DataValueField = "REASONID";
            ddlReason.DataBind();
        }
        catch (Exception ex)
        {

        }
    }
    private void LoadRejectReason(string id, string type)
    {
        try
        {
            DataTable dtReason = new SmartPortal.SEMS.Contract().GetRejectReason(id, type, ref IPCERRORCODE, ref IPCERRORDESC).Tables[0];
            if (!IPCERRORCODE.Equals(0) && dtReason.Rows.Count > 0)
            {
                ddlReason.SelectedValue = dtReason.Rows[0]["ReasonID"].ToString();
                txtDescription.Text = dtReason.Rows[0]["Description"].ToString();
            }
        }
        catch (Exception ex)
        {

        }
    }
    private void hideAll()
    {
        txtReBirth.Enabled = false;
        txtReFullName.Enabled = false;
        txtReMobi.Enabled = false;
        ddlUserType.Enabled = false;
        txtReEmail.Enabled = false;
        ddlReGender.Enabled = false;
        ddlUserLevel.Enabled = false;
        txtReAddress.Enabled = false;
        txtSMSPhoneNo.Enabled = false;
        btsaveandcont.Visible = false;
        btnHuy.Visible = false;
        tvAM.Enabled = false;
        tvWL.Enabled = false;
        ddlpolicyMB.Enabled = false;
        ddlGroup.Enabled = false;
        ddlAccountQT.Enabled = false;
        txtUserNameMB.Enabled = false;
        lbCreateusername.Visible = false;
        lblChangePhone.Visible = false;
        txtLocalFullName.Enabled = false;
        ddlLoginMethod.Enabled = false;
        btnThemNQT.Visible = false;
        tvMBQT.Enabled = false;
    }
    private string LoadSubUserType(string SubUserType)
    {

        DataTable dsUserType1 = new SmartPortal.SEMS.Services().GetUserType(SubUserType, string.Empty, ref IPCERRORCODE, ref IPCERRORDESC).Tables[0];
        if (IPCERRORCODE.Equals("0"))
        {
            return dsUserType1.Rows[0]["SUBUSERCODE"].ToString();
        }
        else
        {
            return string.Empty;
        }

    }

    protected void ddlAccountQT_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!ddlAccountQT.SelectedValue.ToString().Equals("ALL"))
        {        
            ddlDefaultAccountQT.SelectedValue = ddlAccountQT.SelectedValue.ToString();
            ddlDefaultAccountQT.DataBind();
        }
        else
        {
            ddlDefaultAccountQT.Items.RemoveAt(0);
            ddlDefaultAccountQT.Items.Insert(0, new ListItem(PHONE.Trim(), PHONE.Trim()));           
            ddlDefaultAccountQT.SelectedIndex = 0;
        }

    }
    
}


