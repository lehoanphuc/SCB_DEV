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

public partial class Widgets_SEMSUser_AddCorp_Widget : WidgetBase
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
            return ViewState["USERTK"] != null ? ViewState["USERTK"].ToString() : "";
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
    public string USERTYPECK
    {
        get
        {
            return ViewState["USERTYPECK"] != null ? ViewState["USERTYPECK"].ToString() : "";
        }
        set { ViewState["USERTYPECK"] = value; }
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
    //public string CFCODE
    //   {
    //       get { return ViewState["CFCODE"] != null ? (string)ViewState["CFCODE"] : ""; }
    //       set { ViewState["CFCODE"] = value; }
    //   }
    public bool USEWALLET
    {
        get
        {
            return ViewState["USEWALLET"] != null ? ((bool)ViewState["USEWALLET"]) : false;
        }
        set { ViewState["USEWALLET"] = value; }
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

            if (!IsPostBack)
            {
                InitData();
                //23.2.2016 minh add policy
                //load policy to dropdownlist
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
                    LoadFormCorpMatrix(contractNo);
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
                    // contractNo = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cn"].ToString();
                    // minh 26/7/2015 add phan nay cho giong voi create contract
                    //get contractno

                    userTableTemp = (new SmartPortal.SEMS.Contract().GetUserByContractNo(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cn"].ToString(), "", "", ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
                    if (userTableTemp.Rows.Count != 0)
                    {
                        contractNo = userTableTemp.Rows[0]["CONTRACTNO"].ToString();
                        userType = userTableTemp.Rows[0]["USERTYPE"].ToString();
                        productType = userTableTemp.Rows[0]["PRODUCTID"].ToString();
                        CUSTID = userTableTemp.Rows[0]["CUSTID"].ToString();
                    }
                    if (userTableTemp != null)
                    {
                        contractNo = userTableTemp.Rows[0]["CONTRACTNO"].ToString();
                    }

                }




                #region load usertype
                DataSet dsUserType = new DataSet();
                DataTable dtUserType = new DataTable();

                USERTYPE = userTableTemp.Rows[0]["USERTYPE"].ToString();
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
           //     DataTable uTable = new DataTable();

                // if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["uid"] == null)
                //     {
                //         ds = new SmartPortal.IB.Customer().GetAllAcctno("", contractNo, ref IPCERRORCODE, ref IPCERRORDESC);
                //       }
                //     else
                //    { 
                //			SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["uid"].ToString()
             //  uTable = (new SmartPortal.SEMS.User().GetFullUserByUID(txtUserID.Text, ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];

                //if (uTable.Rows.Count != 0)
                //{
                    ds = new SmartPortal.SEMS.Customer().GetAcctNo(CUSTCODE, CFTYPE, ref IPCERRORCODE, ref IPCERRORDESC);
             //   }
                //   }

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

                    if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["uid"] == null)
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

                        ddlDefaultAccountQT.DataSource = dtAccount;
                        ddlDefaultAccountQT.DataTextField = "ACCOUNTNO";
                        ddlDefaultAccountQT.DataValueField = "ACCOUNTNO";
                        ddlDefaultAccountQT.DataBind();
                        
                    }
                    else
                    {
                        try
                        {
                            DataTable DefaultAccount = new SmartPortal.SEMS.Contract().GETUSERACCOUNTDEFAULT(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["uid"]);

                            ddlDefaultAccountQT.DataSource = DefaultAccount;
                            ddlDefaultAccountQT.DataTextField = "ACCOUNTNO";
                            ddlDefaultAccountQT.DataValueField = "ACCOUNTNO";
                            ddlDefaultAccountQT.DataBind();
				ddlDefaultAccountQT.SelectedValue = dtAccount.Rows[0]["ACCOUNTNO"].ToString();

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


                    DataSet dswl = new Customer().GetWalletID(contractNo, ref IPCERRORCODE, ref IPCERRORDESC);
                    if (dswl.Tables[0].Rows.Count > 0 && dswl != null)
                    {
                        PHONEWL = dswl.Tables[0].Rows[0]["ACCOUNTNO"].ToString();
                        ACCOUNTWL = dswl.Tables[0].Rows[0]["WALLETID"].ToString();
                        isWallet.Checked = true;
                        USEWALLET = true;
                        //lstAccount.Items.Insert(0, new ListItem(PHONEWL, ACCOUNTWL));
                        ddlDefaultAccountQT.Items.Insert(0, new ListItem(PHONEWL, ACCOUNTWL));
                    }

                }


                #endregion

                if (ACTION == SmartPortal.Constant.IPC.EDIT)
                {
                    //PHONEUSER = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["uid"].ToString().Trim();
                    //if (int.Parse(ConfigurationManager.AppSettings["MBWLSameUser"].ToString()) == 1)
                    //{
                    //    txtUserNameMB.Text = txtWLPhoneNo.Text = PHONEUSER;
                    //}
                    //if (int.Parse(ConfigurationManager.AppSettings["MBSMSSameUser"].ToString()) == 1)
                    //{
                    //    txtUserNameMB.Text = txtSMSPhoneNo.Text = PHONEUSER;
                    //}

                }
                ddlUserType_SelectedIndexChanged(sender, e);
                BindData();

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
        if (!CheckExistPhoneNumber(txtReMobi.Text.Trim().ToString()))
        {
            lblError.Text = Resources.labels.phonenumberisalreadyregistered;
        }
        //if (!CheckIsPhoneNumer(txtReMobi.Text.Trim().ToString()))
        //{
        //    lblError.Text = Resources.labels.phonenumberwrong;
        //}
        else
        {
            PHONE = txtMBPhoneNo.Text = txtSMSPhoneNo.Text = txtReMobi.Text.Trim();
			txtMBPhoneNo.Text = txtReMobi.Text.Trim();
            btnThemNQT.Visible = true;
            btsaveandcont.Enabled = true;
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
                txtUserNameMB.Enabled = false;
                lbCreateusername.Visible = false;
                lblChangePhone.Visible = false;
                txtLocalFullName.Enabled = false;
                ddlLoginMethod.Enabled = false;
                isWallet.Visible = false;
                tvRole.Enabled = false;

                break;
            case SmartPortal.Constant.IPC.EDIT:
                lblChangePhone.Visible = false;
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
                ddlUserType.Enabled = false;
                ddlType.Enabled = false;
                btsaveandcont.Visible = true;
                //btn_HuySMS.Visible = true;
                ddlpolicyIB.Enabled = false;
                ddlpolicySMS.Enabled = true;
                ddlpolicyMB.Enabled = true;
                ddlpolicyMB.Enabled = false;
                ddlAccountQT.Enabled = true;
                lbCreateusername.Visible = true;
                //cbIsForeign.Enabled = true;
                break;

                //case SmartPortal.Constant.IPC.ADD:
                //    lblTitleUser.Text = Resources.labels.themnguoidungmoi;
                //    txtWLPhoneNo.Text = ACCOUNTWL;
                //    if (walletOnly)
                //    {
                //        txtUserNameMB.Text = USERWALLETONLY;
                //    }
                //    break;
        }

        if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["uid"] != null)
        {
            #region Lấy thông tin user
            USERTK = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["uid"].ToString();

            userTable = (new SmartPortal.SEMS.User().GetFullUserByUID(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["uid"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];

            if (userTable.Rows.Count != 0)
            {
                //ucChargefee.loadAccount(userTable.Rows[0]["ContractNo"].ToString());
                //QuangTV - add old value to log
                emailOld = txtReEmail.Text = userTable.Rows[0]["EMAIL"].ToString();
                genderOld = ddlReGender.SelectedValue = userTable.Rows[0]["GENDER"].ToString();
                addressOld = txtReAddress.Text = userTable.Rows[0]["ADDRESS"].ToString();
                txtMBPhoneNo.Text = phoneOld = userTable.Rows[0]["PHONE"].ToString();
                txtReFullName.Text = userTable.Rows[0]["FULLNAME"].ToString();
                PHONE = txtReMobi.Text = userTable.Rows[0]["PHONE"].ToString();
                DataTable DsPrefix = new SmartPortal.SEMS.User().SEMSGetAllPrefix();
                //cbIsForeign.Checked = true;
                //for (int i =0; i< DsPrefix.Rows.Count; i++) 
                //{
                //    if (PHONE.StartsWith(DsPrefix.Rows[i]["PREFIX"].ToString()))
                //    {
                //        cbIsForeign.Checked = false;
                //    }
                //}              
                txtReEmail.Text = userTable.Rows[0]["EMAIL"].ToString();
                localFullName = txtLocalFullName.Text = userTable.Rows[0]["LOCALFULLNAME"].ToString();
                USERNAMEMB = txtUserNameMB.Text = userTable.Rows[0]["USERNAME"].ToString();
                txtIBUserName.Text = userTable.Rows[0]["USERNAMEIB"].ToString();
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
                    if (ACTION != SmartPortal.Constant.IPC.EDIT)
                    {
                        LoadRejectReason(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["uid"].ToString(), "CUSTOMER");
                        divReject.Visible = true;
                    }

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

                txtUserID.Text = tblMB.Rows[0]["USERID"].ToString().Trim();
                service = SmartPortal.Constant.IPC.MB;
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

        }
        else
        {
            ViewState["IsUseMB"] = false;
            ViewState["UserRolesOld"] = null;
        }
        #region Hien thi tat cac cac role theo serviceid va usertype len cay

        LoadDataALLRoleInTreeview(string.Empty, tvRole, ddlUserType.SelectedValue, SmartPortal.Constant.IPC.NORMAL);

        LoadAccountRoleByUserID(USERTK);
        #endregion

        #region ẩn button huỷ sms
        switch (ACTION)
        {
            case SmartPortal.Constant.IPC.VIEWDETAIL:
                btn_HuySMS.Visible = false;
                break;
            case SmartPortal.Constant.IPC.ADD:
                CheckAllRole();
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
    void GetRoleDefault(TreeView tvRoles, string roleType, string Service)
    {
        DataTable tblRoleDefault = new DataTable();
        //lay role mac dinh MOBILE AM
        tblRoleDefault = (new SmartPortal.SEMS.Role().GetRolesByUserID(txtMBPhoneNo.Text, Service, roleType, CONTRACTNO, ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];

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
            PHONE = txtMBPhoneNo.Text.Trim();

            if (!IsValidEmail(txtReEmail.Text.Trim()) && txtReEmail.Text.Trim() != "")
            {
                lblAlert.Text = Resources.labels.emailkhongdinhdang1;
                return;
            }
            #region check sms notify vutt 30032016
            string errDesc = string.Empty;
            string username = USERTK;
            DataTable dtAcctRoles = (new SmartPortal.SEMS.Transactions().DoStored("EBA_GETACCOUNTROLE_BYCONTRACT", new object[3] { contractNo, "SNO", username }, ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
            if (!string.IsNullOrEmpty(txtSMSPhoneNo.Text))
            {
                //if (!ContractControl.ValidateSMSNotifyCurrentUser(productType, tvSMSQT, ref errDesc, radAllAccountQT.Checked ? "" : ddlAccountQT.SelectedValue, new List<DataTable> { dtAcctRoles }, "AcctNo", "RoleID"))
                //phongtt sms notification fee
                if (!ContractControl.ValidateSMSNotifyCurrentUser(productType, tvSMSQT, ref errDesc, ddlAccountQT.SelectedValue == "ALL" ? "" : ddlAccountQT.SelectedValue, lsAccNo, new List<DataTable> { dtAcctRoles }, "AcctNo", "RoleID"))
                {
                    lblAlert.Text = errDesc;
                    return;
                }
            }
            #endregion

            #region Tao bang chua cac thong tin nguoi quan tri
            string PassTemp = "";
            //27/8/2015 minh add to validate information for owner acount
            string action = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["a"].ToString();
            switch (action)
            {
                case SmartPortal.Constant.IPC.ADD:
                    if (string.IsNullOrEmpty(txtCustomerCode.Text.Trim()))
                    {
                        //lblError.Visible = true;

                        ShowPopUpMsg(Resources.labels.machutaikhoankhongduoctrong);
                        txtCustomerCode.Focus();
                        return;
                    }
                    break;
                case SmartPortal.Constant.IPC.EDIT:
                    break;
            }

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



            string contracttype = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["ct"].ToString();
            switch (contracttype)
            {
                case SmartPortal.Constant.IPC.PERSONAL:
                    if (!string.IsNullOrEmpty(txtReEmail.Text.Trim()))
                    {
                        if (!(System.Text.RegularExpressions.Regex.IsMatch(txtReEmail.Text, pattern)))
                        {
                            ShowPopUpMsg(Resources.labels.emailkhongdinhdang1);
                            //lblAlert.Text = Resources.labels.emailkhongdinhdang1;
                            return;

                        }
                    }
                    break;
                case SmartPortal.Constant.IPC.CORPORATECONTRACT:
                    if (!(System.Text.RegularExpressions.Regex.IsMatch(txtReEmail.Text, pattern)))
                    {

                        ShowPopUpMsg(Resources.labels.emailkhongdinhdang1);
                        //lblError.Text = Resources.labels.emailkhongdinhdang1;
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
            string filterMB = "serviceid='MB' and policyid='" + ddlpolicyMB.SelectedValue.ToString() + "'";
            string stSort = "serviceid asc";

            dspolicy = new SmartPortal.SEMS.USERPOLICY().GetPolicybyCondition(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, Session["userName"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {

                DataTable dt = dspolicy.Tables[0];
                DataTable dtIB = dt.Select(filterIB, stSort).Any() ? dspolicy.Tables[0].Select(filterIB, stSort).CopyToDataTable() : null;
                DataTable dtSMS = null;
                DataTable dtMB = dt.Select(filterMB, stSort).Any() ? dspolicy.Tables[0].Select(filterMB, stSort).CopyToDataTable() : null;
                passlenIB = dtIB == null ? int.Parse(ConfigurationManager.AppSettings["minpwdlen_default"].ToString()) : Convert.ToInt32(dtIB.Rows[0]["minpwdlen"].ToString());
                passlenMB = dtMB == null ? int.Parse(ConfigurationManager.AppSettings["minpwdlen_default"].ToString()) : Convert.ToInt32(dtMB.Rows[0]["minpwdlen"].ToString());
            }


            //string passreveal = DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - passlenIB, passlenIB);     
            string passreveal = DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - passlenMB, passlenMB);
            string pwdreset = Encryption.Encrypt(passreveal);
            //PassTemp = SmartPortal.SEMS.O9Encryptpass.sha_sha256(pwdreset, userName);
            PassTemp = SmartPortal.SEMS.O9Encryptpass.sha_sha256(passreveal, USERTK);
            string pwdresetSMS = DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - passlenSMS, passlenSMS);

            string PinCode = DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - passlenMB, passlenMB);
            string PinCodeMB = SmartPortal.SEMS.O9Encryptpass.sha_sha256(PinCode, USERTK);


            LuuThongTinQuyen("NGUOIQUANTRI", gvResultQuanTri, tvIB, tvSMSQT, tvMBQT, txtReFullName.Text, ddlUserLevel.Text, ddlGroup.SelectedValue, txtReBirth.Text, ddlReGender.SelectedValue, txtReMobi.Text, txtReEmail.Text, txtReAddress.Text, USERTK, PassTemp, PHONE, ddlSMSDefaultAcctno.SelectedValue, ddlDefaultLang.SelectedValue, ((cbIsDefault.Checked == true) ? "Y" : "N"), pwdresetSMS, txtMBPhoneNo.Text, PassTemp, PinCodeMB, CONTRACTTYPE == SmartPortal.Constant.IPC.CONTRACTINDIVIDUAL ? SmartPortal.Constant.IPC.NORMAL : SmartPortal.Constant.IPC.MBA, ddlAccountQT.SelectedValue, pwdreset, DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - 8, 6), "", PassTemp, txtLocalFullName.Text.Trim(), ddlLoginMethod.SelectedValue.Trim(), ddlauthenType.SelectedValue.Trim());

            #endregion
            //28/8/2015 minh fixed truong hop khong chon dich vu ma bam nut add
            if (gvResultQuanTri.Rows.Count == 0)
            {
                lblAlert.Text = Resources.labels.banchuadangkydichvu;
                return;
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
    void LuuThongTinQuyen(string sessionName, GridView gvResult, TreeView tvRole, TreeView tvSMSRole, TreeView tvMBRole, string fullName, string level, string group, string birthday, string gender, string phone, string email, string address, string IBUserName, string IBPass, string SMSPhone, string SMSDefaultAcctno, string SMSDefaultLang, string isDefault, string SMSPinCode, string MBPhone, string MBPass, string MBPinCode, string roleType, string Account, string pwdreset, string WLPinCode, string WLPhone, string WLPass, string localName, string loginMethod, string authenType)
    {
        if (ViewState[sessionName] == null)
        {
            #region NULL
            #region khoi tao bang
            DataTable tblNguoiUyQuyen = new DataTable();

            DataColumn colUserID = new DataColumn("colUserID");
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

            tblNguoiUyQuyen.Columns.Add(colUserID);
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
                            #region luu quyen IB khi NULL
                            DataRow rowNguoiUyQuyen = tblNguoiUyQuyen.NewRow();
                            rowNguoiUyQuyen["colUserID"] = txtUserID.Text.Trim();
                            rowNguoiUyQuyen["colFullName"] = fullName.Trim();
                            rowNguoiUyQuyen["colLocalname"] = localName.Trim();
                            rowNguoiUyQuyen["colLoginMethod"] = loginMethod.Trim();
                            rowNguoiUyQuyen["colAuthenType"] = authenType.Trim();
                            rowNguoiUyQuyen["colLevel"] = level.Trim();
                            rowNguoiUyQuyen["colGroup"] = group.Trim();
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
                            rowNguoiUyQuyen["colServiceID"] = SmartPortal.Constant.IPC.IB;
                            rowNguoiUyQuyen["colIBPolicy"] = ddlpolicyIB.SelectedValue.ToString();
                            rowNguoiUyQuyen["colSMSPolicy"] = ddlpolicySMS.SelectedValue.ToString();
                            rowNguoiUyQuyen["colMBPolicy"] = ddlpolicyMB.SelectedValue.ToString();
                            rowNguoiUyQuyen["colWLPolicy"] = ddlpolicyMB.SelectedValue.ToString();
                            rowNguoiUyQuyen["colpwdreset"] = pwdreset;

                            tblNguoiUyQuyen.Rows.Add(rowNguoiUyQuyen);
                            #endregion
                            //}
                            //else
                            //{
                            //}
                        }
                    }
                    else
                    {
                    }
                }
            }

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
                            #region luu thong tin SMS khi null
                            DataRow rowNguoiUyQuyen = tblNguoiUyQuyen.NewRow();
                            rowNguoiUyQuyen["colUserID"] = txtUserID.Text.Trim();
                            rowNguoiUyQuyen["colFullName"] = fullName.Trim();
                            rowNguoiUyQuyen["colLocalname"] = localName.Trim();
                            rowNguoiUyQuyen["colLoginMethod"] = loginMethod.Trim();
                            rowNguoiUyQuyen["colAuthenType"] = authenType.Trim();
                            rowNguoiUyQuyen["colLevel"] = level.Trim();
                            rowNguoiUyQuyen["colGroup"] = group.Trim();
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
                            rowNguoiUyQuyen["colServiceID"] = SmartPortal.Constant.IPC.SMS;
                            rowNguoiUyQuyen["colIBPolicy"] = ddlpolicyIB.SelectedValue.ToString();
                            rowNguoiUyQuyen["colSMSPolicy"] = ddlpolicySMS.SelectedValue.ToString();
                            rowNguoiUyQuyen["colMBPolicy"] = ddlpolicyMB.SelectedValue.ToString();
                            rowNguoiUyQuyen["colWLPolicy"] = ddlpolicyMB.SelectedValue.ToString();
                            rowNguoiUyQuyen["colpwdreset"] = pwdreset;

                            tblNguoiUyQuyen.Rows.Add(rowNguoiUyQuyen);
                            #endregion
                            //}
                            //else
                            //{
                            //}
                        }
                    }
                    else
                    {
                    }
                }
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

                            DataRow rowNguoiUyQuyen = tblNguoiUyQuyen.NewRow();
                            rowNguoiUyQuyen["colUserID"] = txtUserID.Text.Trim();
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


            ViewState[sessionName] = tblNguoiUyQuyen;
            gvResult.DataSource = tblNguoiUyQuyen.DefaultView.ToTable(true, "colFullName", "colPhone", "colAccount", "colRole");
            gvResult.DataBind();
            #endregion
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
                                #region luu thong tin IB khac null
                                DataRow rowNguoiUyQuyen = tblNguoiUyQuyen.NewRow();
                                rowNguoiUyQuyen["colFullName"] = fullName.Trim();
                                rowNguoiUyQuyen["colLocalname"] = localName.Trim();
                                rowNguoiUyQuyen["colLoginMethod"] = loginMethod.Trim();
                                rowNguoiUyQuyen["colAuthenType"] = authenType.Trim();
                                rowNguoiUyQuyen["colLevel"] = level.Trim();
                                rowNguoiUyQuyen["colGroup"] = group.Trim();
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
                                rowNguoiUyQuyen["colServiceID"] = SmartPortal.Constant.IPC.IB;
                                rowNguoiUyQuyen["colIBPolicy"] = ddlpolicyIB.SelectedValue.ToString();
                                rowNguoiUyQuyen["colSMSPolicy"] = ddlpolicySMS.SelectedValue.ToString();
                                rowNguoiUyQuyen["colMBPolicy"] = ddlpolicyMB.SelectedValue.ToString();
                                rowNguoiUyQuyen["colWLPolicy"] = ddlpolicyMB.SelectedValue.ToString();
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
                            //if (nodeTrancodeIBNguoiUyQuyen.Checked)
                            //{
                            if (tblNguoiUyQuyen.Select("colFullName='" + fullName.Replace("'", "''") + "' and colRoleID='" + nodeRoleIBNguoiUyQuyen.Value + "' and colAccount='" + Account + "' and colTranCode='" + nodeTrancodeIBNguoiUyQuyen.Text + "'").Length == 0)
                            {

                            }

                            //}
                            else
                            {
                                if (tblNguoiUyQuyen.Select("colFullName='" + fullName.Replace("'", "''") + "' and colRoleID='" + nodeRoleIBNguoiUyQuyen.Value + "' and colAccount='" + Account + "' and colTranCode='" + nodeTrancodeIBNguoiUyQuyen.Text + "'").Length != 0)
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
                                #region luu thong tin quyen SMS khac null
                                DataRow rowNguoiUyQuyen = tblNguoiUyQuyen.NewRow();
                                rowNguoiUyQuyen["colFullName"] = fullName.Trim();
                                rowNguoiUyQuyen["colLocalname"] = localName.Trim();
                                rowNguoiUyQuyen["colLoginMethod"] = loginMethod.Trim();
                                rowNguoiUyQuyen["colAuthenType"] = authenType.Trim();
                                rowNguoiUyQuyen["colLevel"] = level.Trim();
                                rowNguoiUyQuyen["colGroup"] = group.Trim();
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
                                rowNguoiUyQuyen["colServiceID"] = SmartPortal.Constant.IPC.SMS;
                                rowNguoiUyQuyen["colIBPolicy"] = ddlpolicyIB.SelectedValue.ToString();
                                rowNguoiUyQuyen["colSMSPolicy"] = ddlpolicySMS.SelectedValue.ToString();
                                rowNguoiUyQuyen["colMBPolicy"] = ddlpolicyMB.SelectedValue.ToString();
                                rowNguoiUyQuyen["colWLPolicy"] = ddlpolicyMB.SelectedValue.ToString();
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
            if (!CheckIsPhoneNumer(PHONE)
                || !CheckIsPhoneNumer(txtReMobi.Text.Trim()))
            {
                lblError.Text = Resources.labels.phonenumberwrong;
                return;
            }
            if (!IsValidEmail(txtReEmail.Text.Trim()) || string.IsNullOrEmpty(txtReEmail.Text.Trim()))
            {
                lblError.Text = Resources.labels.emailkhongdinhdang1;
                return;
            }
            if (!CheckExistPhoneNumber(txtReMobi.Text))
            {
                lblError.Text = Resources.labels.phonenumberisalreadyregistered;
                return;
            }
            if (txtIBUserName.Text.Trim().Length < minlength || txtIBUserName.Text.Trim().Length > maxlength)
            {
                lblError.Text = string.Format(Resources.labels.usernamemustbetween, minlength, maxlength);
                return;
            }
            if (string.IsNullOrEmpty(txtReBirth.Text.Trim()) || string.IsNullOrEmpty(txtReFullName.Text.Trim()))
            {
                lblError.Text = Resources.labels.youneedtoenterfullinformation;
                return;
            }
            if (!CheckUserNameExists(txtIBUserName.Text.Trim()))
            {
                lblError.Text = Resources.labels.usernamehasexists;
                return;
            }
            else
            {
                lblError.Text = string.Empty;
            }
            string userTK = string.Empty;
            //ktra ngay sinh
            string dateExpire_date = new SmartPortal.SEMS.EBASYSVAR().ViewDetail("YEAROLD", ref IPCERRORCODE, ref IPCERRORDESC).Tables[0].Rows[0]["VARVALUE"].ToString();
            int year = Int32.Parse(dateExpire_date);
            if (SmartPortal.Common.Utilities.Utility.IsDateTime1(txtReBirth.Text) >= DateTime.Now.AddYears(-year))
            {
                lblError.Text = "Invalid Birth Day";
                return;
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


            if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["ct"] == SmartPortal.Constant.IPC.CORPORATECONTRACT)
            {
                type = ddlUserType.SelectedValue.Trim();
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
            //string IBPassword = Encryption.Encrypt(DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - 8, 6));
            string IBStatus = "";
            string SMSStatus = "";
            string MBStatus = "";
            string PHOStatus = "";

            string SMSPhoneNo = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtSMSPhoneNo.Text.Trim());
            string SMSDefaultAcctno = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlSMSDefaultAcctno.SelectedValue.Trim());

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

            DataSet dspolicy = new DataSet();
            string filterIB = "serviceid='IB'";
            if (ddlpolicyIB.SelectedValue.ToString() != "")
            {
                filterIB += " and policyid='" + ddlpolicyIB.SelectedValue.ToString() + "'";
            }
            string filterSMS = "serviceid='SMS'  and policyid='" + ddlpolicySMS.SelectedValue.ToString() + "'";
            string filterMB = "serviceid='MB' and policyid='" + ddlpolicyMB.SelectedValue.ToString() + "'";
            string stSort = "serviceid asc";
            int passlenIB = 0;
            int passlenMB = 0;
            string PassTempMB = string.Empty;
            string PassTempIB = string.Empty;
            string emailnqt = "";
            string smsphonenqt = "";
            string pwdresetsms = string.Empty;
            string walletID = string.Empty;
            string IBPassword = string.Empty;
            string MBPass = string.Empty;
            string WLPass = string.Empty;
            string passrevealMB = string.Empty;
            string passrevealIB = string.Empty;
            string pwdresetMB = string.Empty;
            string pwdresetIB = string.Empty;
            dspolicy = new SmartPortal.SEMS.USERPOLICY().GetPolicybyCondition(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, Session["userName"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {

                DataTable dt = dspolicy.Tables[0];
                DataTable dtIB = dt.Select(filterIB, stSort).Any() ? dspolicy.Tables[0].Select(filterIB, stSort).CopyToDataTable() : null;
                DataTable dtSMS = null;
                //DataTable dtMB = dt.Select(filterMB, stSort).Any() ? dspolicy.Tables[0].Select(filterMB, stSort).CopyToDataTable() : null;
                passlenIB = dtIB == null ? int.Parse(ConfigurationManager.AppSettings["minpwdlen_default"].ToString()) : Convert.ToInt32(dtIB.Rows[0]["minpwdlen"].ToString());
                //passlenMB = dtMB == null ? int.Parse(ConfigurationManager.AppSettings["minpwdlen_default"].ToString()) : Convert.ToInt32(dtMB.Rows[0]["minpwdlen"].ToString());
            }
            if (ConfigurationManager.AppSettings["IBMBSamePassword"].ToString().Equals("1"))
            {

                passrevealIB = passrevealMB = DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - passlenMB, passlenMB);
                pwdresetIB = pwdresetMB = Encryption.Encrypt(passrevealMB);
                PassTempIB = PassTempMB = SmartPortal.SEMS.O9Encryptpass.sha_sha256(passrevealMB, USERTK);
                IBPassword = PassTempMB;
                MBPass = PassTempMB;
                WLPass = PassTempMB;
            }
            else
            {
                passrevealIB = DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - passlenIB, passlenIB);
                passrevealMB = DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - passlenMB, passlenMB);
                pwdresetMB = Encryption.Encrypt(passrevealMB);
                pwdresetIB = Encryption.Encrypt(passrevealIB);
                PassTempMB = SmartPortal.SEMS.O9Encryptpass.sha_sha256(passrevealMB, USERTK);
                PassTempIB = SmartPortal.SEMS.O9Encryptpass.sha_sha256(passrevealIB, USERTK);
                IBPassword = PassTempIB;
                MBPass = PassTempMB;
                WLPass = PassTempMB;
            }
            emailnqt = txtReEmail.Text;
            string UsernameMB = txtUserNameMB.Text.Trim();
            string loginMethod = ddlLoginMethod.SelectedValue.Trim();
            string authenType = ddlauthenType.SelectedValue.Trim();
            //string smsphonenuq = "";

            DataTable tblAccountRole = ViewState["ACCOUNTROLE"] != null ? (DataTable)ViewState["ACCOUNTROLE"] : CreateTableAccontRole();
            #region Tạo bảng chứa quyền user Ibank
            //tao bang chua thong tin customer
            DataTable tblIbankUserRight = new DataTable();
            DataColumn colIBUserNameRight = new DataColumn("colIBUserNameRight");
            DataColumn colIBRoleID = new DataColumn("colIBRoleID");

            //add vào table
            tblIbankUserRight.Columns.Add(colIBUserNameRight);
            tblIbankUserRight.Columns.Add(colIBRoleID);

            //tao 1 dong du lieu

            DataRow[] arrIB = tblAccountRole.Select("colServiceID='" + SmartPortal.Constant.IPC.IB + "'");
            foreach (DataRow rWLR in arrIB)
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


            #region Tạo bảng chứa quyền user MB
            //tao bang chua thong tin customer
            DataTable tblMBUserRight = new DataTable();
            DataColumn colMBPhoneNoR = new DataColumn("colMBPhoneNo");
            DataColumn colMBRoleID = new DataColumn("colMBRoleID");
            DataColumn colMBRoleContracType = new DataColumn("colMBRoleContracType");
            //add vào table
            tblMBUserRight.Columns.Add(colMBPhoneNoR);
            tblMBUserRight.Columns.Add(colMBRoleID);
            tblMBUserRight.Columns.Add(colMBRoleContracType);

            DataRow[] arrMB = tblAccountRole.Select("colServiceID='" + SmartPortal.Constant.IPC.MB + "'");
            foreach (DataRow rWLR in arrMB)
            {
                if (tblMBUserRight.Select("colMBPhoneNo='" + USERTK + "' and colMBRoleID='" + rWLR["colRoleID"].ToString() + "'").Length == 0)
                {
                    DataRow newRowMBR = tblMBUserRight.NewRow();
                    newRowMBR["colMBPhoneNo"] = USERTK;
                    newRowMBR["colMBRoleID"] = rWLR["colRoleID"].ToString();
                    newRowMBR["colMBRoleContracType"] = CONTRACTTYPE;

                    tblMBUserRight.Rows.Add(newRowMBR);
                }
            }
            //add role wallet
            DataRow[] arrWL = tblAccountRole.Select("colServiceID='" + SmartPortal.Constant.IPC.WAL + "'");
            foreach (DataRow rWLR in arrWL)
            {
                if (tblMBUserRight.Select("colMBPhoneNo='" + USERTK + "' and colMBRoleID='" + rWLR["colRoleID"].ToString() + "'").Length == 0)
                {
                    DataRow newRowMBR = tblMBUserRight.NewRow();
                    newRowMBR["colMBPhoneNo"] = USERTK;
                    newRowMBR["colMBRoleID"] = rWLR["colRoleID"].ToString();
                    newRowMBR["colMBRoleContracType"] = CONTRACTTYPE;

                    tblMBUserRight.Rows.Add(newRowMBR);
                }
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
            DataSet ds = new SmartPortal.SEMS.Customer().GetAcctNo(CUSTCODE, CFTYPE, ref IPCERRORCODE, ref IPCERRORDESC);
            DataTable dtAccount = ds.Tables[0];
            foreach (DataRow rCA in dtAccount.Rows)
            {
                if (tblContractAccount.Select("colAContractNo='" + CONTRACTNO + "' and colAcctNo='" + rCA["ACCOUNTNO"].ToString() + "'").Length == 0)
                {
                    if (rCA["ACCOUNTNO"].ToString() != "ACCOUNTNO")
                    {
                        DataRow newRowCA = tblContractAccount.NewRow();
                        newRowCA["colAContractNo"] = CONTRACTNO;
                        newRowCA["colAcctNo"] = rCA["ACCOUNTNO"].ToString();

                        DataRow[] ro = dtAccount.Select("ACCOUNTNO='" + rCA["ACCOUNTNO"].ToString().Trim() + "'");
                        if (ro.Length != 0)
                        {
                            newRowCA["colAcctType"] = ro[0]["ACCOUNTTYPE"].ToString();
                            newRowCA["colCCYID"] = ro[0]["CCYID"].ToString();
                            newRowCA["colStatus"] = ro[0]["STATUS"].ToString();
                            newRowCA["colBranchID"] = SmartPortal.Common.Utilities.Utility.FormatStringCore(ro[0]["BRANCHID"].ToString());
                        }
                        else
                        {
                            newRowCA["colAcctType"] = "DD";
                            newRowCA["colCCYID"] = Resources.labels.lak;
                            newRowCA["colStatus"] = "A";
                        }

                        tblContractAccount.Rows.Add(newRowCA);
                    }
                }
                //add role wallet
                if (USEWALLET)
                {
                    if (tblContractAccount.Select("colAContractNo='" + CONTRACTNO + "' and colAcctNo='" + ACCOUNTWL + "'").Length == 0)
                    {
                        DataRow newRowCA = tblContractAccount.NewRow();
                        newRowCA["colAContractNo"] = CONTRACTNO;
                        newRowCA["colAcctNo"] = ACCOUNTWL;
                        newRowCA["colAcctType"] = "WL";
                        newRowCA["colCCYID"] = Resources.labels.lak;
                        newRowCA["colStatus"] = "A";

                        tblContractAccount.Rows.Add(newRowCA); ;
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
            foreach (DataRow rUA in tblAccountRole.Rows)
            {
                if (tblUserAccount.Select("colUserIDUC='" + USERTK + "' and colAcctNoUC='" + rUA["colAccount"].ToString() + "' and colRoleIDUC='" + rUA["colRoleID"].ToString() + "'").Length == 0)
                {
                    if (rUA["colAccount"].ToString() != "ACCOUNTNO")
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
            #endregion
            //if (ddlpolicyMB.SelectedValue.ToString().Equals(""))
            //{
                //lblError.Text = "policy not null";
                //return;
            //}
            switch (ACTION)
            {
                case SmartPortal.Constant.IPC.ADD:
                    #region insert user
                    SMSPhoneNo = "";
                    IBStatus = SmartPortal.Constant.IPC.NEW;
                    SMSStatus = SmartPortal.Constant.IPC.NEW;
                    MBStatus = SmartPortal.Constant.IPC.NEW;
                    PHOStatus = SmartPortal.Constant.IPC.NEW;
                    status = SmartPortal.Constant.IPC.NEW;
                    btsaveandcont.Enabled = false;
                    new SmartPortal.SEMS.User().InsertUserCrop(CONTRACTNO, CONTRACTTYPE, endDate, lastModify, userCreate, userLastModify, userApprove, status, USERTK,
                        userFullName, userLocalFullName, userGender, userAddress, userEmail, userPhone, uCreateDate, userCreate, userType, userLevel, type, "",
                        tokenID, tokenIssueDate, smsOTP, userBirthday, IBUserName, IBPassword, IBStatus, UserNameMB, PHONE, MBPass, MBStatus, tblIbankUserRight,
                        tblMBUserRight, tblUserAccount, tblContractAccount, ddlpolicyIB.SelectedValue.ToString(), ddlpolicyMB.SelectedValue.ToString(),
                        pwdresetMB, pwdresetIB, authenType, loginMethod, ref IPCERRORCODE, ref IPCERRORDESC);


                    if (IPCERRORCODE != "0")
                    {
                        SmartPortal.Common.Log.RaiseError(IPCERRORCODE.ToString() + CONTRACTNO, this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCERRORCODE.ToString(), Request.Url.Query);
                        lblError.Text = "add user unsuccessfull";
                        return;
                    }
                    if (IPCERRORCODE == "0")
                    {
                        ViewState["NGUOIQUANTRI"] = null;
                        ltrError.Text = Resources.labels.addusersuccessfull;
                        btsaveandcont.Visible = false;
                        DisOrenable(false);
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

                    #endregion

                    break;
                case SmartPortal.Constant.IPC.EDIT:

                    if (STS_USER.Equals(SmartPortal.Constant.IPC.NEW) ||
                        STS_USER.Equals(SmartPortal.Constant.IPC.PENDINGFORACTIVE) ||
                        STS_USER.Equals(SmartPortal.Constant.IPC.PENDINGFORAPPROVE) ||
                         STS_USER.Equals(SmartPortal.Constant.IPC.REJECTFORMNEW)
                        )
                    {
                        IBStatus = SmartPortal.Constant.IPC.PENDING;
                        loginMethod = SmartPortal.Constant.IPC.USERNAME;
                        //minh add 29/5/2015 phân biệt trường hợp xóa, thêm_thay đổi                            
                        SMSStatus = SmartPortal.Constant.IPC.PENDINGFORDELETE;
                        MBStatus = SmartPortal.Constant.IPC.PENDING;
                        PHOStatus = SmartPortal.Constant.IPC.PENDING;
                        status = SmartPortal.Constant.IPC.PENDING;
                    }
                    else
                    {
                        IBStatus = SmartPortal.Constant.IPC.PENDING;
                        //minh add 29/5/2015 phân biệt trường hợp xóa, thêm_thay đổi                            
                        SMSStatus = SmartPortal.Constant.IPC.PENDINGFORDELETE;
                        MBStatus = SmartPortal.Constant.IPC.PENDING;
                        PHOStatus = SmartPortal.Constant.IPC.PENDING;
                        status = SmartPortal.Constant.IPC.PENDING;
                    }

                    USERTK = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["uid"].ToString();

                    //DataTable contractInfo = new DataTable();

                    contractInfo = (new SmartPortal.SEMS.Contract().GetContractByContractNo(contractNo, ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
                    if (contractInfo.Rows.Count != 0)
                    {
                        endDate = SmartPortal.Common.Utilities.Utility.IsDateTime2(contractInfo.Rows[0]["ENDDATE"].ToString().Trim()).ToString("dd/MM/yyyy");
                    }


                    #region update user
                    new SmartPortal.SEMS.User().UpdateUserCrop(CONTRACTTYPE, lastModify, status, USERTK,
                         userFullName, userLocalFullName, userGender, userAddress, userEmail, userPhone, userCreate, userType, userLevel, type, "",
                         tokenID, smsOTP, userBirthday, UserNameMB, string.Empty, IBStatus, UserNameMB, PHONE, MBPass, MBStatus, tblIbankUserRight,
                         tblMBUserRight, tblUserAccount, tblContractAccount, ddlpolicyIB.SelectedValue.ToString(), ddlpolicyMB.SelectedValue.ToString(),
                         authenType, loginMethod, ref IPCERRORCODE, ref IPCERRORDESC);


                    if (IPCERRORCODE == "0")
                    {
                        //ucChargefee.WriteLogTransaction(CONTRACTNO.ToString(), "User");
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
                        SmartPortal.Common.Log.RaiseError(IPCERRORCODE.ToString() + CONTRACTNO, this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCERRORCODE.ToString(), Request.Url.Query);
                        lblError.Text = "Update user unsuccessfull";
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

        LoadDataALLRoleInTreeview(string.Empty, tvRole, ddlUserType.SelectedValue, SmartPortal.Constant.IPC.NORMAL);
        if (!ddlUserType.SelectedValue.Equals(USERTYPECK))
        {
            DataTable tblAccountRole = ViewState["ACCOUNTROLE"] != null ? (DataTable)ViewState["ACCOUNTROLE"] : CreateTableAccontRole();
            RemoveAccountRoleUserID(USERTK, tblAccountRole);
            CheckAllRole();
        }
        USERTYPECK = ddlUserType.SelectedValue;
        lstAccount_SelectedIndexChanged(null, null);
    }
    private void RemoveAccountRoleUserID(string userID, DataTable tblRole)
    {
        try
        {
            DataTable dtRole = tblRole.Select("colUserID <> '" + userID + "'").Any() ? tblRole.Select("colUserID <> '" + userID + "'").CopyToDataTable() : null;
            ViewState["ACCOUNTROLE"] = dtRole;
        }
        catch (Exception ex)
        {

        }

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
        }
        catch
        {
        }
    }
    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["ct"] == SmartPortal.Constant.IPC.PERSONALCONTRACT)
        //{
        //    //load for IB
        //    LoadDataInTreeview(SmartPortal.Constant.IPC.IB, tvIB, contractNo, string.Empty);

        //    //load for MB
        //    LoadDataInTreeview(SmartPortal.Constant.IPC.MB, tvMBQT, contractNo, SmartPortal.Constant.IPC.NORMAL);
        //    ;

        //}
        //if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["ct"] == SmartPortal.Constant.IPC.CORPORATECONTRACT)
        //{
        //    //load for IB
        //    LoadDataInTreeview1(SmartPortal.Constant.IPC.IB, tvIB, ddlType.SelectedValue.Split('#')[0]);
        //    //load for MB
        //    LoadDataInTreeview1(SmartPortal.Constant.IPC.MB, tvMBQT, userType);

        //}
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
            btsaveandcont.Enabled = true;
            ltrError.Text = string.Empty;
            lblError.Text = string.Empty;
            txtReFullName.Text = string.Empty;
            txtReEmail.Text = string.Empty;
            txtReMobi.Text = string.Empty;
            ddlReGender.SelectedIndex = 0;
            txtReBirth.Text = string.Empty;
            txtReAddress.Text = string.Empty;
            btsaveandcont.Enabled = true;
            txtIBUserName.Enabled = true;
            string resultUserName = new Customer().UsernameGeneration("6", ref IPCERRORCODE, ref IPCERRORDESC);
            if (resultUserName.Equals(string.Empty) || resultUserName.Length != 10 || !CheckUserNameExists(resultUserName))
            {
                lblError.Text = Resources.labels.usernamehasexists;
                return;
            }
            txtIBUserName.Text = txtUserNameMB.Text = resultUserName;

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

                //throw new IPCException(SmartPortal.Constant.IPC.ERRORCODE.CUSTNOTEXIST);
                return;
            }

            switch (ddlUserType.SelectedValue.ToString().Trim())
            {
                case SmartPortal.Constant.IPC.CORPORATECONTRACT:

                    break;
                default:
                    break;

            }
            //#region Kiểm tra sự tồn tại của khách hàng
            //DataTable tblCE = new SmartPortal.SEMS.Customer().CheckCustExists(SmartPortal.Common.Utilities.Utility.FormatStringCore(txtCustomerCode.Text.Trim()), ctmType, SmartPortal.Constant.IPC.CONTRACTINDIVIDUAL);
            //if (tblCE.Rows.Count != 0)
            //{
            //    lblError.Text = Resources.labels.khachhangnaydatontaitronghethong;
            //    return;
            //}

            //string resultInfo = new Customer().CheckCustInfo(txtCustomerCode.Text.Trim(), string.Empty, "B", SmartPortal.Constant.IPC.CONTRACTINDIVIDUAL, ref IPCERRORCODE, ref IPCERRORDESC);
            //if (resultInfo.Equals("-1"))
            //{
            //    lblError.Text = Resources.labels.khachhangnaydatontaitronghethong;
            //    return;
            //}
            //#endregion


            //minh modify 3.11.2015 tach generate user name for corporate
            if (hasCustomerInfo[SmartPortal.Constant.IPC.CUSTNAME] != null && hasCustomerInfo[SmartPortal.Constant.IPC.LICENSE] != null)
            {
                txtReFullName.Text = hasCustomerInfo[SmartPortal.Constant.IPC.CUSTNAME].ToString();

            }
            if (hasCustomerInfo[SmartPortal.Constant.IPC.EMAIL] != null)
                txtReEmail.Text = hasCustomerInfo[SmartPortal.Constant.IPC.EMAIL].ToString();
            if (hasCustomerInfo[SmartPortal.Constant.IPC.PHONE] != null)
            {
                txtReMobi.Text = hasCustomerInfo[SmartPortal.Constant.IPC.PHONE].ToString();

                if (!CheckExistPhoneNumber(txtReMobi.Text))
                {
                    lblError.Text = Resources.labels.phonenumberisalreadyregistered;
                    btsaveandcont.Enabled = false;
                }
                if (!CheckIsPhoneNumer(txtReMobi.Text.Trim()))
                {
                    lblError.Text = Resources.labels.phonenumberwrong;
                    btsaveandcont.Enabled = false;
                }
                else
                {
                    txtSMSPhoneNo.Text = txtReMobi.Text;
                    PHONE = txtMBPhoneNo.Text = txtReMobi.Text;
                }
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

            if (hasCustomerInfo[SmartPortal.Constant.IPC.CUSTTYPE] != null)
            {
                hasCustomerInfo[SmartPortal.Constant.IPC.CUSTTYPE].ToString().Trim();
            }
            SmartPortal.Common.Utilities.Utility.FormatStringCore(hasCustomerInfo[SmartPortal.Constant.IPC.CUSTCODE].ToString());
            CheckAllRole();
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
    private bool CheckUserNameExists(string userName, TextBox textBox)
    {
        DataSet ds = new Customer().CheckUserName("EBA_Users_CheckUserNameMB", new object[] { userName, SmartPortal.Constant.IPC.CONTRACTINDIVIDUAL }, ref IPCERRORCODE, ref IPCERRORDESC);
        if (IPCERRORCODE != "0")
        {
            lblError.Text = IPCERRORDESC;
            return false;
        }
        if (!validateusername(textBox))
        {
            return false;
        }
        return true;
    }
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

    public bool CheckIsPhoneNumer(string phone)
    {
        //if (cbIsForeign.Checked)
        //{
        //    return true;
        //}
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
        string resultInfo = new Customer().CheckPhoneInContract(phone, USERTK, CONTRACTNO, ref IPCERRORCODE, ref IPCERRORDESC);
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
        if (CONTRACTTYPE.Equals(SmartPortal.Constant.IPC.CONTRACTINDIVIDUAL) ||
            CONTRACTTYPE.Equals(SmartPortal.Constant.IPC.CONTRACTCORPSIMPLE) ||
            CONTRACTTYPE.Equals(SmartPortal.Constant.IPC.CONTRACTCORPMATRIX) ||
            CONTRACTTYPE.Equals(SmartPortal.Constant.IPC.CONTRACTCORPADVANCE))
        {
            ds = new Customer().CheckUserName("EBA_USERS_CHECKUSERNAMEEDIT", new object[] { userName, USERTK, SmartPortal.Constant.IPC.CONTRACTINDIVIDUAL }, ref IPCERRORCODE, ref IPCERRORDESC);
        }
        else
        {
            ds = new Customer().CheckUserName("EBA_USERS_CHECKUSERNAMEEDIT", new object[] { userName, USERTK, SmartPortal.Constant.IPC.AGENTMERCHANT }, ref IPCERRORCODE, ref IPCERRORDESC);
        }
        if (IPCERRORCODE != "0")
        {
            lblAlert.Text = IPCERRORDESC;
            return false;
        }
        lblAlert.Text = string.Empty;
        lblError.Text = string.Empty;
        ltrError.Text = string.Empty;
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
        //string userType = LoadSubUserType(USERTYPE);
        resultUserName = new Customer().UsernameGeneration("6", ref IPCERRORCODE, ref IPCERRORDESC);
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
        txtIBUserName.Enabled = true;
        txtUserNameMB.Text = txtIBUserName.Text = GenerateUserName();
    }
    protected void ChangePhone_Click(object sender, EventArgs e)
    {
        txtMBPhoneNo.Enabled = true;
    }
    protected void ChangePhoneAndUserName_Click(object sender, EventArgs e)
    {
        txtUserNameMB.Text = txtIBUserName.Text;
        if (txtUserNameMB.Text.Trim().Length < minlength || txtUserNameMB.Text.Trim().Length > maxlength)
        {
            lblError.Text = string.Format(Resources.labels.usernamemustbetween, minlength, maxlength);
            return;
        }
        else if (!CheckUserNameExists(txtUserNameMB.Text.Trim()))
        {
            lblError.Text = Resources.labels.usernamehasexists;
            return;

        }
        else
        {
            txtReMobi.Text = txtMBPhoneNo.Text;
            txtIBUserName.Text = txtUserNameMB.Text.Trim();
            UpdatemobileService(txtMBPhoneNo.Text);
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
        tvRole.Enabled = false;
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
    protected void tvRole_OnSelectedIndexChanged(object sender, TreeNodeEventArgs e)
    {
        try
        {
            DataTable tblAccountRole = ViewState["ACCOUNTROLE"] != null ? (DataTable)ViewState["ACCOUNTROLE"] : CreateTableAccontRole();
            DataTable tblRole = ViewState["TBLROLE"] != null ? (DataTable)ViewState["TBLROLE"] : CreateTableRole();
            //add row moi
            string role = string.Empty;
            foreach (TreeNode node in tvRole.Nodes)
            {
                role = node.Value.Trim();
                if (node.Checked)
                {
                    if (lstAccount.SelectedValue.Trim().Equals(ACCOUNTWL))
                    {
                        AddAccountRole(USERTK, lstAccount.SelectedValue.Trim(), role, SmartPortal.Constant.IPC.WAL, CONTRACTNO);
                    }
                    else
                    {
                        foreach (DataRow dr in tblRole.Rows)
                        {
                            if (dr["colRoleID"].ToString().Equals(role))
                            {
                                AddAccountRole(USERTK, lstAccount.SelectedValue.Trim(), role, dr["colServiceID"].ToString(), CONTRACTNO);
                            }
                        }

                    }
                }
                else
                {
                    RemoteAccountRole(USERTK, lstAccount.SelectedValue.Trim(), role, string.Empty, CONTRACTNO);
                }
            }

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSSetLimitTeller_Widget", "lstAccount_SelectedIndexChanged", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    }
    private void AddAccountRole(string userID, string account, string roleID, string serviceID, string contractNo)
    {
        try
        {
            DataTable tblAccountRole = ViewState["ACCOUNTROLE"] != null ? (DataTable)ViewState["ACCOUNTROLE"] : CreateTableAccontRole();
            if (tblAccountRole.Select("colAccount='" + account + "' and colRoleID='" + roleID + "' and colUserID='" + userID + "' and colServiceID='" + serviceID + "'").Length == 0 && !string.IsNullOrEmpty(account))
            {
                DataRow rowUser = CreateTableAccontRole().NewRow();
                rowUser["colUserID"] = userID;
                rowUser["colAccount"] = account;
                rowUser["colRoleID"] = roleID;
                rowUser["colServiceID"] = serviceID;
                rowUser["colContractNo"] = contractNo;
                tblAccountRole.Rows.Add(rowUser.ItemArray);
                ViewState["ACCOUNTROLE"] = tblAccountRole;
                return;
            }
        }
        catch (Exception ex)
        {

        }

    }
    private void RemoteAccountRole(string userID, string account, string roleID, string serviceID, string contractNo)
    {
        try
        {
            DataTable tblAccountRole = ViewState["ACCOUNTROLE"] != null ? (DataTable)ViewState["ACCOUNTROLE"] : CreateTableAccontRole();
            foreach (DataRow dr in tblAccountRole.Rows)
            {
                if (dr["colAccount"].ToString() == account && dr["colRoleID"].ToString() == (string.IsNullOrEmpty(roleID) ? dr["colRoleID"].ToString() : roleID) && dr["colUserID"].ToString() == userID)
                {
                    dr.Delete();
                }
            }
            tblAccountRole.AcceptChanges();
            ViewState["ACCOUNTROLE"] = tblAccountRole;
        }
        catch
        {

        }

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
		tvPage.CollapseAll();
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
    public void IsWallet_OnCheckedChaned(object sender, EventArgs e)
    {

        string phone = string.Empty;
        DataTable dtAccountQT = (DataTable)ViewState["ACCOUNTCORE"];
        if (isWallet.Checked)
        {
            if (string.IsNullOrEmpty(ACCOUNTWL))
            {
                ACCOUNTWL = new SmartPortal.SEMS.User().CreatePrimaryKey(string.Empty, string.Empty, "WALLETID", ref IPCERRORCODE, ref IPCERRORDESC);
            }
            USEWALLET = true;
            lstAccount.DataSource = dtAccountQT;
            lstAccount.DataTextField = "ACCOUNTNO";
            lstAccount.DataValueField = "ACCOUNTNO";
            lstAccount.DataBind();
            lstAccount.SelectedValue = dtAccountQT.Rows[0]["ACCOUNTNO"].ToString();
            lstAccount.Items.Insert(0, new ListItem(PHONEWL, ACCOUNTWL));

            ddlDefaultAccountQT.DataSource = dtAccountQT;
            ddlDefaultAccountQT.DataTextField = "ACCOUNTNO";
            ddlDefaultAccountQT.DataValueField = "ACCOUNTNO";
            ddlDefaultAccountQT.DataBind();
            ddlDefaultAccountQT.Items.Insert(ddlDefaultAccountQT.Items.Count , new ListItem(PHONEWL, ACCOUNTWL));
        }
        else
        {
            USEWALLET = false;
            lstAccount.DataSource = dtAccountQT;
            lstAccount.DataTextField = "ACCOUNTNO";
            lstAccount.DataValueField = "ACCOUNTNO";
            lstAccount.DataBind();
            lstAccount.SelectedValue = dtAccountQT.Rows[0]["ACCOUNTNO"].ToString();
            RemoteAccountRole(USERTK, ACCOUNTWL, string.Empty, string.Empty, CONTRACTNO);
            ddlDefaultAccountQT.DataSource = dtAccountQT;
            ddlDefaultAccountQT.DataTextField = "ACCOUNTNO";
            ddlDefaultAccountQT.DataValueField = "ACCOUNTNO";
            ddlDefaultAccountQT.DataBind();

        }

    }
    private void InitData()
    {
        string CustType = string.Empty;
        userTableTemp = GetUserContract();
        if (userTableTemp.Rows.Count != 0)
        {
            ltrError.Text = string.Empty;
            lblAlert.Text = string.Empty;
            lblError.Text = string.Empty;
            CONTRACTNO = contractNo = userTableTemp.Rows[0]["CONTRACTNO"].ToString();
            DataColumnCollection columns = userTableTemp.Columns;
            //if (columns.Contains("PHONE"))
            //{
            //txtMBPhoneNo.Text = userTableTemp.Rows[0]["PHONE"].ToString();
            //}
            userType = userTableTemp.Rows[0]["USERTYPE"].ToString();
            CONTRACTTYPE = corptype = userTableTemp.Rows[0]["CONTRACTTYPE"].ToString();
            PRODUCTID = productType = userTableTemp.Rows[0]["PRODUCTID"].ToString();
            CFTYPE = userTableTemp.Rows[0]["CFTYPE"].ToString().Trim();
            //	CFCODE = userTableTemp.Rows[0]["CFCODE"].ToString().Trim();
            PHONEWL = userTableTemp.Rows[0]["TEL"].ToString().Trim();
            CUSTCODE = userTableTemp.Rows[0]["CUSTCODE"].ToString().Trim();
            DataTable dtProduct = new SmartPortal.SEMS.Product().GetProductByCondition(productType.Trim(), "", CustType, "", ref IPCERRORCODE, ref IPCERRORDESC).Tables[0];
            string result = dtProduct.Rows[0]["PRODUCTTYPE"].ToString();
        }
        else
        {
            userName = DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - 8, 8);
        }
        if (ACTION == SmartPortal.Constant.IPC.VIEWDETAIL || ACTION == SmartPortal.Constant.IPC.EDIT || ACTION == SmartPortal.Constant.IPC.APPROVER)
        {
            ddlUserType.Items.Add(new ListItem("Internal", "IN"));
        }
        else
        {
            USERTK = new SmartPortal.SEMS.User().CreatePrimaryKey(string.Empty, string.Empty, "USERID", ref IPCERRORCODE, ref IPCERRORDESC);
        }
    }
    private void DisOrenable(bool ischeck)
    {
        pnAccountInfo.Enabled = ischeck;
        pnMB.Enabled = ischeck;
        PnIB.Enabled = ischeck;
        pnAdd.Enabled = ischeck;
    }

    public void CheckAllRole()
    {
        DataTable tblRole = ViewState["TBLROLE"] != null ? (DataTable)ViewState["TBLROLE"] : CreateTableRole();
        DataTable dtAccountQT = (DataTable)ViewState["ACCOUNTCORE"];
        foreach (TreeNode node in tvRole.Nodes)
        {
            node.Checked = true;
            string role = node.Value.Trim();
        }
        foreach (DataRow dr in tblRole.Rows)
        {
            foreach (DataRow drAct in dtAccountQT.Rows)
            {
                AddAccountRole(USERTK, drAct["ACCOUNTNO"].ToString(), dr["colRoleID"].ToString(), dr["colServiceID"].ToString(), CONTRACTNO);
            }

        }
        LoadAccountRole(USERTK, lstAccount.SelectedValue.ToString().Trim());
    }
    public void LoadRoleAndAccountCore()
    {
        DataTable tblRole = (DataTable)ViewState["TBLROLE"];
        DataTable dtAccountQT = (DataTable)ViewState["ACCOUNTCORE"];


    }
    protected void txtReEmail_OnTextChanged(object sender, EventArgs e)
    {
        //ucChargefee.CalculateTotalFee("SEMS_ChangeEmail");
    }
}


