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
using SmartPortal.Security;
using SmartPortal;
using SmartPortal.ExceptionCollection;
using System.Collections.Generic;
using System.Text;
using SmartPortal.Model;
using System.Linq;
using SmartPortal.SEMS;
using SmartPortal.Constant;
public partial class Widgets_SEMSUserApprove_Wallet_Widget : WidgetBase
{
    string ACTION = "";
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    DataTable userTable = new DataTable();
    DataTable userTableTemp = new DataTable();
    DataTable tblRoleDefault = new DataTable();
    string contractNo = "";
    string userName = "";
    string userID = "";
    string corptype = "";
    public bool ADDACCOUNTWALLET
    {
        get { return ViewState["ADDACCOUNTWALLET"] != null ? (bool)ViewState["ADDACCOUNTWALLET"] : false; }
        set { ViewState["ADDACCOUNTWALLET"] = value; }
    }
    string USERTK
    {
        get
        {
            return ViewState["USERTK"] != null ? ViewState["USERTK"].ToString() : "";
        }
        set { ViewState["USERTK"] = value; }
    }
    public string SHOWTAB
    {
        get { return ViewState["SHOWTAB"] != null ? (string)ViewState["SHOWTAB"] : string.Empty; }
        set { ViewState["SHOWTAB"] = value; }
    }
    public string CONTRACTTYPE
    {
        get { return ViewState["CONTRACTTYPE"] != null ? (string)ViewState["CONTRACTTYPE"] : string.Empty; }
        set { ViewState["CONTRACTTYPE"] = value; }
    }
    protected void Page_Init(object sender, EventArgs e)
    {
        TabCustomerInfoHelper.LoadConfig();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                ltrError.Text = string.Empty;
                DataSet dspolicy = new DataSet();
                string filterIB = "serviceid='IB'";
                string filterSMS = "serviceid='SMS'";
                string filterMB = "serviceid='MB'";


                string stSort = "serviceid asc";

                dspolicy = new SmartPortal.SEMS.USERPOLICY().GetPolicybyCondition(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, Session["userName"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE == "0")
                {


                    DataTable dt = dspolicy.Tables[0];


                    DataTable dtIB = dt.Select(filterIB, stSort).Any() ? dspolicy.Tables[0].Select(filterIB, stSort).CopyToDataTable() : null;
                    DataTable dtSMS = dt.Select(filterSMS, stSort).Any() ? dspolicy.Tables[0].Select(filterSMS, stSort).CopyToDataTable() : null;
                    DataTable dtMB = dt.Select(filterMB, stSort).Any() ? dspolicy.Tables[0].Select(filterMB, stSort).CopyToDataTable() : null;


                    ddlpolicyIB.DataSource = dtIB;
                    ddlpolicySMS.DataSource = dtSMS;
                    ddlpolicyMB.DataSource = dtMB;
                    ddlpolicyWL.DataSource = dtMB;
                    ddlpolicyAM.DataSource = dtMB;

                    ddlpolicyIB.DataTextField = "policytx";
                    ddlpolicyIB.DataValueField = "policyid";
                    ddlpolicySMS.DataTextField = "policytx";
                    ddlpolicySMS.DataValueField = "policyid";
                    ddlpolicyMB.DataTextField = "policytx";
                    ddlpolicyMB.DataValueField = "policyid";
                    ddlpolicyWL.DataTextField = "policytx";
                    ddlpolicyWL.DataValueField = "policyid";
                    ddlpolicyAM.DataTextField = "policytx";
                    ddlpolicyAM.DataValueField = "policyid";

                    ddlpolicyIB.DataBind();
                    ddlpolicySMS.DataBind();
                    ddlpolicyMB.DataBind();
                    ddlpolicyWL.DataBind();
                    ddlpolicyAM.DataBind();
                    ddlpolicyAM.Enabled = false;
                    ddlpolicyIB.Enabled = false;
                    ddlpolicyWL.Enabled = false;


                }

                BindData();
                ShowTab();
            }

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_IBCorpUser_Controls_Widget", "Page_Load", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

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
            ddlGroup.Items.Insert(0, new ListItem("No Group", ""));
        }
        else
        {
            ShowError(IPCERRORCODE);
        }
        VisibleCorpMatrix(corptype.Equals(SmartPortal.Constant.IPC.CONTRACTCORPMATRIX));
    }
    private void ShowError(string msg = "")
    {
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ShowError", "ShowError('" + msg + "');", true);
    }
    void VisibleCorpMatrix(bool isCorpMatrix)
    {
        //lblLevel.Visible = !isCorpMatrix;
        //ddlUserLevel.Visible = !isCorpMatrix;
        lblGroup.Visible = isCorpMatrix;
        ddlGroup.Visible = isCorpMatrix;
    }
    private bool IsSuccess()
    {
        return IPCERRORCODE.Equals("0") ? true : false;
    }
    private bool IsDataSetNotNull(DataSet ds)
    {
        return ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 ? true : false;
    }

    void BindData()
    {
        try
        {
            //string contractNo = "";
            ddlAccountQT.Enabled = false;
            lblAlert.Text = "";

            ACTION = GetActionPage();
            if (ACTION.Equals(IPC.ACTIONPAGE.APPROVE))
            {
                btnApprove.Visible = true;
                btnReject.Visible = false;
            }
            else
            {
                btnApprove.Visible = false;
                btnReject.Visible = true;
                tblsendinfor.Visible = false;
            }

            //get contractno            

            if (Session["userIDA"] == null)
            {
                RedirectBackToMainPage();
            }
            else
            {
                List<string> lstTran = new List<string>();
                lstTran = (List<string>)Session["userIDA"];
                USERTK = userID = lstTran[0];
            }

            txtMBUserName.Text = USERTK = userID;

            userTableTemp = (new SmartPortal.SEMS.User().GetFullUserByUID(userID, ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
            if (userTableTemp.Rows.Count != 0)
            {
                contractNo = userTableTemp.Rows[0]["CONTRACTNO"].ToString();
                userName = userTableTemp.Rows[0]["CUSTCODE"].ToString().Trim() + userTableTemp.Rows[0]["CFTYPE"].ToString().Trim() + DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - 6, 4);

                //lannth- them corptype vaf structure de xac dinh typeuser, level va group.
                corptype = userTableTemp.Rows[0]["contracttype"].ToString();
                string CustType = userTableTemp.Rows[0]["CFTYPE"].ToString().Trim();
                string productType = userTableTemp.Rows[0]["PRODUCTID"].ToString();
                string typeid = userTableTemp.Rows[0]["TYPEID"].ToString();
                DataTable dtProduct = new SmartPortal.SEMS.Product().GetProductByCondition(productType.Trim(), "", CustType, "", ref IPCERRORCODE, ref IPCERRORDESC).Tables[0];
                string result = dtProduct.Rows[0]["PRODUCTTYPE"].ToString();

                CONTRACTTYPE = userTableTemp.Rows[0]["CONTRACTTYPE"].ToString();
                // delete tabview
                if (userTableTemp.Rows.Count > 0)
                {
                    string TYPEID = userTableTemp.Rows[0]["TypeID"].ToString();
                    if (result == SmartPortal.Constant.IPC.PRCTYPECONSUMER && TYPEID != SmartPortal.Constant.IPC.CHUTAIKHOAN)
                    {
                        ADDACCOUNTWALLET = false;
                    }

                }

                string accWL = txtWLPhoneNo.Text = new Customer().GetAccountWallet(contractNo, ref IPCERRORCODE, ref IPCERRORDESC);
                //lannth - 27.9.2018 - load va hien thi dropdown level, group voi contract la corp matrix
                if (corptype.Equals(SmartPortal.Constant.IPC.CONTRACTCORPMATRIX))
                {
                    LoadFormCorpMatrix(contractNo);
                }

                DataTable dtUserType1 = new DataTable();
                dtUserType1 = new SmartPortal.SEMS.Contract().LoadContractType(SmartPortal.Constant.IPC.PERSONAL, "Y");

                if (result.Equals(SmartPortal.Constant.IPC.PRCAGENTMERCHANT))
                {
                    SHOWTAB = Resources.labels.agentandmerchant;
                    lbltype.Visible = false;
                    ddlUserType.Items.Add(new ListItem(Resources.labels.Agent, "A"));
                    ddlUserType.Items.Add(new ListItem(Resources.labels.Merchant, "M"));
                }
                else
                {
                    lbltype.Visible = false;
                    ddlUserType.Items.Add(new ListItem(Resources.labels.Consumer, "C"));
                    if (typeid.Equals(SmartPortal.Constant.IPC.DONGCHUTAIKHOAN))
                    {
                        SHOWTAB = string.Empty;
                    }
                    else
                    {
                        if (userTableTemp.Rows[0]["CTYPE"].ToString() == "W")
                        {

                        }
                        SHOWTAB = Resources.labels.walletbanking;
                    }
                }


                //ddlUserType.DataSource = dtUserType1;
                //ddlUserType.DataTextField = "TYPENAME";
                //ddlUserType.DataValueField = "USERTYPE";
                //ddlUserType.DataBind();



                DataColumnCollection columns = userTableTemp.Columns;
                if (columns.Contains("IBpolicyid"))
                {
                    if (userTableTemp.Rows[0]["IBpolicyid"].ToString().Trim() != string.Empty)
                        ddlpolicyIB.SelectedValue = userTableTemp.Rows[0]["IBpolicyid"].ToString().Trim();
                }
                if (columns.Contains("SMSpolicyid"))
                {
                    if (userTableTemp.Rows[0]["SMSpolicyid"].ToString().Trim() != string.Empty)
                        ddlpolicySMS.SelectedValue = userTableTemp.Rows[0]["SMSpolicyid"].ToString().Trim();
                }
                if (columns.Contains("WLpolicyid"))
                {
                    if (userTableTemp.Rows[0]["WLpolicyid"].ToString().Trim() != string.Empty)
                        ddlpolicyWL.SelectedValue = userTableTemp.Rows[0]["WLpolicyid"].ToString().Trim();
                }
                if (columns.Contains("MBpolicyid"))
                {
                    if (userTableTemp.Rows[0]["MBpolicyid"].ToString().Trim() != string.Empty)
                        ddlpolicyMB.SelectedValue = userTableTemp.Rows[0]["MBpolicyid"].ToString().Trim();
                }


            }
            else
            {
                userName = DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - 8, 8);
            }



            //load cap bac
            //get Contractno

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

            //DataSet dsLevel = new SmartPortal.IB.CorpUser().LoadCorpUserlevelByContractNo(contractNo, ref IPCERRORCODE, ref IPCERRORDESC);
            //if (IPCERRORCODE == "0")
            //{
            //    ddlUserLevel.DataSource = dsLevel;
            //    ddlUserLevel.DataTextField = "DESCRIPTION";
            //    ddlUserLevel.DataValueField = "USERLEVEL";
            //    ddlUserLevel.DataBind();
            //}
            //else
            //{
            //    throw new Exception();
            //}

            //if (ViewState["NGUOIQUANTRI"] == null)
            {
                txtIBUserName.Text = userName;
                txtIBUserName.Enabled = false;

                #region load usertype
                DataSet dsUserType = new DataSet();
                DataTable dtUserType = new DataTable();

                if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["ut"] == SmartPortal.Constant.IPC.PERSONALCONTRACT)
                {
                    dtUserType = new SmartPortal.SEMS.Contract().LoadContractType(SmartPortal.Constant.IPC.PERSONAL, "Y");
                    lbltype.Visible = false;
                    ddlType.Visible = false;
                }
                if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["ut"] == SmartPortal.Constant.IPC.CORPORATECONTRACT)
                {
                    dtUserType = new SmartPortal.SEMS.Contract().LoadContractType(SmartPortal.Constant.IPC.CORPORATE, "Y");
                    lbltype.Visible = true;
                    ddlType.Visible = true;
                }

                if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["ut"] != null)
                {
                    ddlUserType.DataSource = dtUserType;
                    ddlUserType.DataTextField = "TYPENAME";
                    ddlUserType.DataValueField = "USERTYPE";
                    ddlUserType.DataBind();
                }

                #endregion

                #region lay tat ca cac account cua khach hang
                DataSet ds = new DataSet();
                ds = new SmartPortal.IB.Customer().GetAllAcctno("", contractNo, ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE != "0")
                {
                    throw new IPCException("4017");
                }
                DataTable dtAccount = new DataTable();
                dtAccount = ds.Tables[0];
                if (dtAccount.Rows.Count != 0)
                {
                    //ddlSMSDefaultAcctno.DataSource = dtAccount;
                    //ddlSMSDefaultAcctno.DataTextField = "ACCTNO";
                    //ddlSMSDefaultAcctno.DataValueField = "ACCTNO";
                    //ddlSMSDefaultAcctno.DataBind();

                    //ddlPHODefaultAcctno.DataSource = dtAccount;
                    //ddlPHODefaultAcctno.DataTextField = "ACCTNO";
                    //ddlPHODefaultAcctno.DataValueField = "ACCTNO";
                    //ddlPHODefaultAcctno.DataBind();

                    //ddlAccountQT.DataSource = dtAccount;
                    //ddlAccountQT.DataTextField = "ACCTNO";
                    //ddlAccountQT.DataValueField = "ACCTNO";
                    //ddlAccountQT.DataBind();

                    ddlSMSDefaultAcctno.DataSource = dtAccount;
                    ddlSMSDefaultAcctno.DataTextField = "ACCOUNTNO";
                    ddlSMSDefaultAcctno.DataValueField = "ACCOUNTNO";
                    ddlSMSDefaultAcctno.DataBind();



                    ddlAccountQT.DataSource = dtAccount;
                    ddlAccountQT.DataTextField = "ACCOUNTNO";
                    ddlAccountQT.DataValueField = "ACCOUNTNO";
                    ddlAccountQT.DataBind();
                    ddlAccountQT.Items.Insert(0, new ListItem("ALL", "ALL"));


                }
                #endregion

                ddlUserType_SelectedIndexChanged(null, null);
                //BindData();

            }
            //enable(disable) theo action
            switch (ACTION)
            {
                case IPC.ACTIONPAGE.APPROVE:
                case IPC.ACTIONPAGE.REJECT:
                    txtReBirth.Enabled = false;
                    txtReFullName.Enabled = false;
                    txtReMobi.Enabled = false;
                    ddlUserType.Enabled = false;
                    ddlType.Enabled = false;
                    txtReEmail.Enabled = false;
                    ddlReGender.Enabled = false;
                    ddlUserType.Enabled = false;
                    txtReAddress.Enabled = false;
                    txtIBUserName.Enabled = false;
                    txtSMSPhoneNo.Enabled = false;
                    txtMBUserName.Enabled = false;
                    ddlGroup.Enabled = false;


                    btnThemNQT.Visible = false;
                    tvIBQT.Enabled = false;
                    tvMBQT.Enabled = false;
                    tvAM.Enabled = false;
                    tvSMSQT.Enabled = false;
                    tvWL.Enabled = false;

                    ddlDefaultLang.Enabled = false;
                    ddlSMSDefaultAcctno.Enabled = false;
                    cbIsDefault.Enabled = false;
                    ddlpolicyIB.Enabled = false;
                    ddlpolicyMB.Enabled = false;
                    ddlpolicySMS.Enabled = false;
                    ddlpolicyWL.Enabled = false;

                    break;


            }
            #region Hien thi tat cac cac role theo serviceid va usertype len cay
            //load for IB
            LoadDataInTreeview(SmartPortal.Constant.IPC.IB, tvIBQT, contractNo, string.Empty);

            //load for SMS
            LoadDataInTreeview(SmartPortal.Constant.IPC.SMS, tvSMSQT, contractNo, string.Empty);

            if (CONTRACTTYPE.Equals(SmartPortal.Constant.IPC.CONTRACTINDIVIDUAL))
            {
                LoadDataInTreeview(SmartPortal.Constant.IPC.MB, tvMBQT, contractNo, SmartPortal.Constant.IPC.NORMAL);
            }
            else
            {
                LoadDataInTreeview(SmartPortal.Constant.IPC.AM, tvMBQT, contractNo, SmartPortal.Constant.IPC.MBA);
            }

            //load for wallet
            LoadDataInTreeview(SmartPortal.Constant.IPC.MB, tvWL, contractNo, SmartPortal.Constant.IPC.WAL);


            //load for A


            //load for wallet
            LoadDataInTreeview(SmartPortal.Constant.IPC.AM, tvAM, contractNo, SmartPortal.Constant.IPC.EAM);
            #endregion


            #region hien thi tat ca cac role boi contractno
            //hien thi cac role cua internet banking
            //cblIB.DataSource = new SmartPortal.IB.Role().GetRoleByServiceID(SmartPortal.Constant.IPC.IB, contractNo, ref IPCERRORCODE, ref IPCERRORDESC);
            //cblIB.DataTextField = "ROLENAME";
            //cblIB.DataValueField = "ROLEID";
            //cblIB.DataBind();
            ////hien thi cac role cua sms
            //cblSMS.DataSource = new SmartPortal.IB.Role().GetRoleByServiceID(SmartPortal.Constant.IPC.SMS, contractNo, ref IPCERRORCODE, ref IPCERRORDESC);
            //cblSMS.DataTextField = "ROLENAME";
            //cblSMS.DataValueField = "ROLEID";
            //cblSMS.DataBind();
            ////hien thi cac role cua mobile
            //cblMB.DataSource = new SmartPortal.IB.Role().GetRoleByServiceID(SmartPortal.Constant.IPC.MB, contractNo, ref IPCERRORCODE, ref IPCERRORDESC);
            //cblMB.DataTextField = "ROLENAME";
            //cblMB.DataValueField = "ROLEID";
            //cblMB.DataBind();
            ////hien thi cac role cua phone
            //cblPHO.DataSource = new SmartPortal.IB.Role().GetRoleByServiceID(SmartPortal.Constant.IPC.PHO, contractNo, ref IPCERRORCODE, ref IPCERRORDESC);
            //cblPHO.DataTextField = "ROLENAME";
            //cblPHO.DataValueField = "ROLEID";
            //cblPHO.DataBind();
            #endregion

            if (userID != "")
            {
                #region Lấy thông tin user

                userTable = (new SmartPortal.SEMS.User().GetFullUserByUID(userID, ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
                if (userTable.Rows.Count != 0)
                {

                    txtReFullName.Text = userTable.Rows[0]["FULLNAME"].ToString();
                    txtReMobi.Text = userTable.Rows[0]["PHONE"].ToString();
                    ddlUserType.SelectedValue = userTable.Rows[0]["USERTYPE"].ToString();
                    txtReEmail.Text = userTable.Rows[0]["EMAIL"].ToString();
                    ddlReGender.SelectedValue = userTable.Rows[0]["GENDER"].ToString();


                    //Lannth - do source cho drop
                    LoadFormCorpMatrix(contractNo);

                    ///ddlUserLevel.SelectedValue = userTable.Rows[0]["USERLEVEL"].ToString();
                    //lannth - lay ra group cua user
                    #region lay group cua user
                    if (userTable.Rows.Count > 0 && userTable.Rows[0]["contracttype"].ToString().Equals(SmartPortal.Constant.IPC.MTRUSER))
                    {
                        DataTable tblGroup = new SmartPortal.SEMS.User().GetGroupByUser(userID, ref IPCERRORCODE, ref IPCERRORDESC).Tables[0];
                        if (tblGroup.Rows.Count != 0)
                        {
                            ddlGroup.SelectedValue = tblGroup.Rows[0]["GroupID"].ToString().Trim();
                        }
                    }
                    #endregion

                    txtUserNameMB.Text = userTable.Rows[0]["USERNAME"].ToString();

                    ddlUserType.SelectedValue = userTable.Rows[0]["USERTYPE"].ToString();
                    txtReAddress.Text = userTable.Rows[0]["ADDRESS"].ToString();
                    //if (userTable.Rows[0]["USERTYPE"].ToString().Trim() != SmartPortal.Constant.IPC.PERSONALCONTRACT)
                    if (!userTable.Rows[0]["TYPEID"].ToString().Equals(SmartPortal.Constant.IPC.MTRUSER))
                    {
                        ddlType.SelectedValue = userTable.Rows[0]["TYPEID"].ToString() + "#" + userTable.Rows[0]["USERLEVEL"].ToString();
                    }
                    else
                        ddlType.SelectedValue = userTable.Rows[0]["TYPEID"].ToString();


                    if (userTable.Rows[0]["BIRTHDAY"].ToString() != "")
                    {
                        txtReBirth.Text = SmartPortal.Common.Utilities.Utility.IsDateTime2(userTable.Rows[0]["BIRTHDAY"].ToString()).ToString("dd/MM/yyyy");
                    }
                    lblStatus.Text = userTable.Rows[0]["STATUS"].ToString();
                    lblStatusG.Text = userTable.Rows[0]["STATUS"].ToString();
                }


                #endregion
                //minh add/modify 28/5/2015 to show/hide item notification for account
                bool gotemail = false;
                bool gotsms = false;
                if (txtReEmail.Text.Trim() != string.Empty)
                {
                    gotemail = true;
                }
                #region Lấy thông tin tài khoản User
                DataTable tblIB = new SmartPortal.IB.User().GetAccountDetail(userID, SmartPortal.Constant.IPC.IB);
                if (tblIB.Rows.Count != 0)
                {
                    txtIBUserName.Text = tblIB.Rows[0]["USERNAME"].ToString().Trim();
                    txtIBUserName.Enabled = false;
                    //ddlStatusIB.SelectedValue = tblIB.Rows[0]["STATUS"].ToString().Trim();
                    //txtIBPass.Attributes.Add("value", Encryption.Decrypt(tblIB.Rows[0]["PASSWORD"].ToString().Trim()));
                    //txtIBRePass.Attributes.Add("value", Encryption.Decrypt(tblIB.Rows[0]["PASSWORD"].ToString().Trim()));
                }

                //DataTable tblSMS = new SmartPortal.IB.User().GetAccountDetail(userID, SmartPortal.Constant.IPC.SMS);
                //if (tblSMS.Rows.Count != 0)
                //{
                //    txtSMSPhoneNo.Text = tblSMS.Rows[0]["PHONENO"].ToString().Trim();
                //    //ddlStatusSMS.SelectedValue = tblSMS.Rows[0]["STATUS"].ToString().Trim();
                //    ddlSMSDefaultAcctno.SelectedValue = tblSMS.Rows[0]["DEFAULTACCTNO"].ToString().Trim();
                //    ddlDefaultLang.SelectedValue = tblSMS.Rows[0]["DEFAULTLANG"].ToString().Trim();

                //    if (tblSMS.Rows[0]["ISDEFAULT"].ToString().Trim() == "Y")
                //    {
                //        cbIsDefault.Checked = true;
                //    }
                //    else
                //    {
                //        cbIsDefault.Checked = true;
                //    }
                //    if (tblSMS.Rows[0]["Status"].ToString().Trim() != SmartPortal.Constant.IPC.DELETE && tblSMS.Rows[0]["Status"].ToString().Trim() != SmartPortal.Constant.IPC.PENDINGFORDELETE)
                //    {
                //        gotsms = true;
                //    }
                //    else
                //    {
                //        gotsms = false;
                //    }

                //}
                //else
                //{
                //    gotsms = false;
                //}
                //show/hide item
                gotsms = false;
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
                    ddlSendinfo.Items.Add(new ListItem(Resources.labels.sendcontractinforsms, SmartPortal.Constant.IPC.SMS));
                    ddlSendinfo.Items.Add(new ListItem(Resources.labels.sendcontractinforboth, SmartPortal.Constant.IPC.BOTH));
                }
                else
                        if (!gotemail && gotsms)
                {
                    ddlSendinfo.Items.Add(new ListItem(Resources.labels.sendcontractinforsms, SmartPortal.Constant.IPC.EMAIL));
                    ddlSendinfo.Items.Add(new ListItem(Resources.labels.sendcontractinforsms, SmartPortal.Constant.IPC.SMS));
                    ddlSendinfo.Items.Add(new ListItem(Resources.labels.sendcontractinforboth, SmartPortal.Constant.IPC.BOTH));
                }
                else
                {
                    tblsendinfor.Visible = false;
                }

                DataTable tblMB = new SmartPortal.IB.User().GetAccountDetail(userID, SmartPortal.Constant.IPC.MB);
                if (tblMB.Rows.Count != 0)
                {
                    txtMBUserName.Text = USERTK;
                    txtWLPhoneNo.Text = txtMBPhoneNo.Text = tblMB.Rows[0]["PHONENO"].ToString().Trim();
                    //txtMBUserName.Text = tblMB.Rows[0]["PHONENO"].ToString().Trim();
                    txtMBUserName.Enabled = false;
                    //ddlStatusMB.SelectedValue = tblMB.Rows[0]["STATUS"].ToString().Trim();
                    //txtMBPass.Attributes.Add("value", Encryption.Decrypt(tblMB.Rows[0]["PASS"].ToString().Trim()));
                    //txtMBRePass.Attributes.Add("value", Encryption.Decrypt(tblMB.Rows[0]["PASS"].ToString().Trim()));
                }

                DataTable tblAM = new SmartPortal.IB.User().GetAccountDetail(userID, SmartPortal.Constant.IPC.AM);
                if (tblAM.Rows.Count != 0)
                {
                    txtMBPhoneNo.Text = txtAMPhoneNo.Text = tblAM.Rows[0]["PHONENO"].ToString().Trim();
                    txtMBUserName.Enabled = false;
                    //ddlStatusMB.SelectedValue = tblMB.Rows[0]["STATUS"].ToString().Trim();
                    //txtMBPass.Attributes.Add("value", Encryption.Decrypt(tblMB.Rows[0]["PASS"].ToString().Trim()));
                    //txtMBRePass.Attributes.Add("value", Encryption.Decrypt(tblMB.Rows[0]["PASS"].ToString().Trim()));
                }

                #endregion

                #region lấy thông tin tất cả các quyền
                DataTable tblRoleDetail = new SmartPortal.IB.User().GetRoleDetail(userID);

                //luu thông tin vào session và hiển thị lên lưới
                DataTable tblNguoiUyQuyen = new DataTable();

                DataColumn colFullName = new DataColumn("colFullName");
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
                DataColumn colWLPhone = new DataColumn("colWLPhone");
                DataColumn colWLPass = new DataColumn("colWLPass");
                DataColumn colWLPinCode = new DataColumn("colWLPinCode");
                DataColumn colAccount = new DataColumn("colAccount");
                DataColumn colRole = new DataColumn("colRole");
                DataColumn colRoleID = new DataColumn("colRoleID");
                DataColumn colTranCode = new DataColumn("colTranCode");
                DataColumn colTranCodeID = new DataColumn("colTranCodeID");
                DataColumn colServiceID = new DataColumn("colServiceID");

                tblNguoiUyQuyen.Columns.Add(colFullName);
                //tblNguoiUyQuyen.Columns.Add(colLevel);
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
                tblNguoiUyQuyen.Columns.Add(colWLPhone);
                tblNguoiUyQuyen.Columns.Add(colWLPass);
                tblNguoiUyQuyen.Columns.Add(colWLPinCode);
                tblNguoiUyQuyen.Columns.Add(colAccount);
                tblNguoiUyQuyen.Columns.Add(colRole);
                tblNguoiUyQuyen.Columns.Add(colRoleID);
                tblNguoiUyQuyen.Columns.Add(colTranCode);
                tblNguoiUyQuyen.Columns.Add(colTranCodeID);
                tblNguoiUyQuyen.Columns.Add(colServiceID);

                foreach (DataRow rRole in tblRoleDetail.Rows)
                {
                    DataRow r = tblNguoiUyQuyen.NewRow();
                    r["colFullName"] = txtReFullName.Text;
                    //r["colLevel"] = ddlUserLevel.SelectedValue;
                    r["colBirthday"] = txtReBirth.Text;
                    r["colGender"] = ddlReGender.SelectedValue;
                    r["colPhone"] = txtReMobi.Text;
                    r["colEmail"] = txtReEmail.Text;
                    r["colAddress"] = txtReAddress.Text;
                    r["colIBUserName"] = USERTK;
                    r["colIBPass"] = "";
                    r["colSMSPhone"] = txtSMSPhoneNo.Text;
                    r["colSMSDefaultAcctno"] = ddlSMSDefaultAcctno.SelectedValue;
                    r["colSMSDefaultLang"] = ddlDefaultLang.SelectedValue;
                    r["colMBPhone"] = txtMBUserName.Text;
                    r["colMBPass"] = "";
                    r["colWLPhone"] = txtMBUserName.Text;
                    r["colWLPass"] = "";
                    r["colAccount"] = rRole["ACCTNO"].ToString();
                    r["colRole"] = rRole["ROLENAME"].ToString();
                    r["colRoleID"] = rRole["ROLEID"].ToString();
                    r["colTranCode"] = rRole["PAGENAME"].ToString();
                    r["colTranCodeID"] = rRole["IPCTRANCODE"].ToString();
                    r["colServiceID"] = rRole["SERVICEID"].ToString();

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
                    r["colWLPinCode"] = "";

                    tblNguoiUyQuyen.Rows.Add(r);
                }

                ViewState["NGUOIQUANTRI"] = tblNguoiUyQuyen;

                gvResultQuanTri.DataSource = tblNguoiUyQuyen.DefaultView.ToTable(true, "colFullName", "colPhone", "colAccount", "colRole");
                gvResultQuanTri.DataBind();
                #endregion
            }
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(IPCex.ToString(), this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(IPCex.ToString(), Request.Url.Query);

        }
    }

    void LoadDataInTreeview(string serviceID, TreeView tvPage, string cn, string roleType)
    {
        tvPage.Nodes.Clear();

        DataTable tblSS = new DataTable();
        //if (IPCERRORCODE == "0")
        //{
        //    tblSS = ds.Tables[0];
        //}
        tblSS = new SmartPortal.SEMS.Contract().GetRoleByServiceAndContract(serviceID, cn, roleType);

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
            #region Tao bang chua cac thong tin nguoi quan tri
            string PassTemp = "";
            if (ddlAccountQT.SelectedValue != "ALL")
            {
                PassTemp = Encryption.Encrypt(DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - 8, 8));
                LuuThongTinQuyen("NGUOIQUANTRI", gvResultQuanTri, tvIBQT, tvSMSQT, tvMBQT, txtReFullName.Text, "0", txtReBirth.Text, ddlReGender.SelectedValue, txtReMobi.Text, txtReEmail.Text, txtReAddress.Text, txtIBUserName.Text, PassTemp, txtSMSPhoneNo.Text, ddlSMSDefaultAcctno.SelectedValue, ddlDefaultLang.SelectedValue, (cbIsDefault.Checked == true ? "Y" : "N"), DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - 8, 6), txtMBUserName.Text, PassTemp, DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - 8, 6), ddlAccountQT.SelectedValue);
            }

            if (ddlAccountQT.SelectedValue == "ALL")
            {
                //lay tat ca tai khoan khach hang
                DataSet ds = new DataSet();
                ds = new SmartPortal.IB.Customer().GetAllAcctno("", contractNo, ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE != "0")
                {
                    throw new IPCException(IPCERRORDESC);
                }
                DataTable dtAccount = new DataTable();
                dtAccount = ds.Tables[0];

                //luu tat ca account
                PassTemp = Encryption.Encrypt(DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - 8, 8));
                foreach (DataRow rowAccount in dtAccount.Rows)
                {
                    LuuThongTinQuyen("NGUOIQUANTRI", gvResultQuanTri, tvIBQT, tvSMSQT, tvMBQT, txtReFullName.Text, "0", txtReBirth.Text, ddlReGender.SelectedValue, txtReMobi.Text, txtReEmail.Text, txtReAddress.Text, txtIBUserName.Text, PassTemp, txtSMSPhoneNo.Text, ddlSMSDefaultAcctno.SelectedValue, ddlDefaultLang.SelectedValue, (cbIsDefault.Checked == true ? "Y" : "N"), DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - 8, 6), txtMBUserName.Text, PassTemp, DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - 8, 6), rowAccount["ACCTNO"].ToString());
                }

                lblAlert.Text = Resources.labels.recordsaved;
            }
            #endregion

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

    void LuuThongTinQuyen(string sessionName, GridView gvResult, TreeView tvRole, TreeView tvSMSRole, TreeView tvMBRole, string fullName, string level, string birthday, string gender, string phone, string email, string address, string IBUserName, string IBPass, string SMSPhone, string SMSDefaultAcctno, string SMSDefaultLang, string isDefault, string SMSPinCode, string MBPhone, string MBPass, string MBPinCode, string Account)
    {
        if (ViewState[sessionName] == null)
        {
            #region NULL
            DataTable tblNguoiUyQuyen = new DataTable();

            DataColumn colFullName = new DataColumn("colFullName");
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
            DataColumn colAccount = new DataColumn("colAccount");
            DataColumn colRole = new DataColumn("colRole");
            DataColumn colRoleID = new DataColumn("colRoleID");
            DataColumn colTranCode = new DataColumn("colTranCode");
            DataColumn colTranCodeID = new DataColumn("colTranCodeID");
            DataColumn colServiceID = new DataColumn("colServiceID");

            tblNguoiUyQuyen.Columns.Add(colFullName);
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
            tblNguoiUyQuyen.Columns.Add(colAccount);
            tblNguoiUyQuyen.Columns.Add(colRole);
            tblNguoiUyQuyen.Columns.Add(colRoleID);
            tblNguoiUyQuyen.Columns.Add(colTranCode);
            tblNguoiUyQuyen.Columns.Add(colTranCodeID);
            tblNguoiUyQuyen.Columns.Add(colServiceID);

            //IB
            if (IBUserName != "")
            {
                foreach (TreeNode nodeRoleIBNguoiUyQuyen in tvRole.Nodes)
                {
                    foreach (TreeNode nodeTrancodeIBNguoiUyQuyen in nodeRoleIBNguoiUyQuyen.ChildNodes)
                    {
                        if (nodeTrancodeIBNguoiUyQuyen.Checked)
                        {
                            DataRow rowNguoiUyQuyen = tblNguoiUyQuyen.NewRow();
                            rowNguoiUyQuyen["colFullName"] = fullName.Trim();
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
                            rowNguoiUyQuyen["colAccount"] = Account;
                            rowNguoiUyQuyen["colRole"] = nodeRoleIBNguoiUyQuyen.Text;
                            rowNguoiUyQuyen["colRoleID"] = nodeRoleIBNguoiUyQuyen.Value;
                            rowNguoiUyQuyen["colTranCode"] = nodeTrancodeIBNguoiUyQuyen.Text;
                            rowNguoiUyQuyen["colTranCodeID"] = nodeTrancodeIBNguoiUyQuyen.Value;
                            rowNguoiUyQuyen["colServiceID"] = SmartPortal.Constant.IPC.IB;

                            tblNguoiUyQuyen.Rows.Add(rowNguoiUyQuyen);
                        }
                        else
                        {
                        }
                    }
                }
            }

            //SMS
            if (SMSPhone != "")
            {
                foreach (TreeNode nodeRoleIBNguoiUyQuyen in tvSMSRole.Nodes)
                {
                    foreach (TreeNode nodeTrancodeIBNguoiUyQuyen in nodeRoleIBNguoiUyQuyen.ChildNodes)
                    {
                        if (nodeTrancodeIBNguoiUyQuyen.Checked)
                        {
                            DataRow rowNguoiUyQuyen = tblNguoiUyQuyen.NewRow();
                            rowNguoiUyQuyen["colFullName"] = fullName.Trim();
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
                            rowNguoiUyQuyen["colAccount"] = Account;
                            rowNguoiUyQuyen["colRole"] = nodeRoleIBNguoiUyQuyen.Text;
                            rowNguoiUyQuyen["colRoleID"] = nodeRoleIBNguoiUyQuyen.Value;
                            rowNguoiUyQuyen["colTranCode"] = nodeTrancodeIBNguoiUyQuyen.Text;
                            rowNguoiUyQuyen["colTranCodeID"] = nodeTrancodeIBNguoiUyQuyen.Value;
                            rowNguoiUyQuyen["colServiceID"] = SmartPortal.Constant.IPC.SMS;

                            tblNguoiUyQuyen.Rows.Add(rowNguoiUyQuyen);
                        }
                        else
                        {
                        }
                    }
                }
            }

            //MB
            if (MBPhone != "")
            {
                foreach (TreeNode nodeRoleIBNguoiUyQuyen in tvMBRole.Nodes)
                {
                    foreach (TreeNode nodeTrancodeIBNguoiUyQuyen in nodeRoleIBNguoiUyQuyen.ChildNodes)
                    {
                        if (nodeTrancodeIBNguoiUyQuyen.Checked)
                        {
                            DataRow rowNguoiUyQuyen = tblNguoiUyQuyen.NewRow();
                            rowNguoiUyQuyen["colFullName"] = fullName.Trim();
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
                            rowNguoiUyQuyen["colAccount"] = Account;
                            rowNguoiUyQuyen["colRole"] = nodeRoleIBNguoiUyQuyen.Text;
                            rowNguoiUyQuyen["colRoleID"] = nodeRoleIBNguoiUyQuyen.Value;
                            rowNguoiUyQuyen["colTranCode"] = nodeTrancodeIBNguoiUyQuyen.Text;
                            rowNguoiUyQuyen["colTranCodeID"] = nodeTrancodeIBNguoiUyQuyen.Value;
                            rowNguoiUyQuyen["colServiceID"] = SmartPortal.Constant.IPC.MB;

                            tblNguoiUyQuyen.Rows.Add(rowNguoiUyQuyen);
                        }
                        else
                        {
                        }
                    }
                }
            }

            ViewState[sessionName] = tblNguoiUyQuyen;
            gvResult.DataSource = tblNguoiUyQuyen;
            gvResult.DataBind();
            #endregion
        }
        else
        {
            DataTable tblNguoiUyQuyen = (DataTable)ViewState[sessionName];

            //IB
            if (IBUserName != "")
            {
                foreach (TreeNode nodeRoleIBNguoiUyQuyen in tvRole.Nodes)
                {
                    foreach (TreeNode nodeTrancodeIBNguoiUyQuyen in nodeRoleIBNguoiUyQuyen.ChildNodes)
                    {
                        if (nodeTrancodeIBNguoiUyQuyen.Checked)
                        {
                            if (tblNguoiUyQuyen.Select("colFullName='" + fullName + "' and colRoleID='" + nodeRoleIBNguoiUyQuyen.Value + "' and colAccount='" + Account + "' and colTranCode='" + nodeTrancodeIBNguoiUyQuyen.Text + "'").Length == 0)
                            {
                                DataRow rowNguoiUyQuyen = tblNguoiUyQuyen.NewRow();
                                rowNguoiUyQuyen["colFullName"] = fullName.Trim();
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
                                rowNguoiUyQuyen["colAccount"] = Account;
                                rowNguoiUyQuyen["colRole"] = nodeRoleIBNguoiUyQuyen.Text;
                                rowNguoiUyQuyen["colRoleID"] = nodeRoleIBNguoiUyQuyen.Value;
                                rowNguoiUyQuyen["colTranCode"] = nodeTrancodeIBNguoiUyQuyen.Text;
                                rowNguoiUyQuyen["colTranCodeID"] = nodeTrancodeIBNguoiUyQuyen.Value;
                                rowNguoiUyQuyen["colServiceID"] = SmartPortal.Constant.IPC.IB;

                                tblNguoiUyQuyen.Rows.Add(rowNguoiUyQuyen);
                            }

                        }
                        else
                        {
                            if (tblNguoiUyQuyen.Select("colFullName='" + fullName + "' and colRoleID='" + nodeRoleIBNguoiUyQuyen.Value + "' and colAccount='" + Account + "' and colTranCode='" + nodeTrancodeIBNguoiUyQuyen.Text + "'").Length != 0)
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

            //SMS
            if (SMSPhone != "")
            {
                foreach (TreeNode nodeRoleIBNguoiUyQuyen in tvSMSRole.Nodes)
                {
                    foreach (TreeNode nodeTrancodeIBNguoiUyQuyen in nodeRoleIBNguoiUyQuyen.ChildNodes)
                    {
                        if (nodeTrancodeIBNguoiUyQuyen.Checked)
                        {
                            if (tblNguoiUyQuyen.Select("colFullName='" + fullName + "' and colAccount='" + Account + "' and colRoleID='" + nodeRoleIBNguoiUyQuyen.Value + "' and colTranCode='" + nodeTrancodeIBNguoiUyQuyen.Text + "'").Length == 0)
                            {
                                DataRow rowNguoiUyQuyen = tblNguoiUyQuyen.NewRow();
                                rowNguoiUyQuyen["colFullName"] = fullName.Trim();
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
                                rowNguoiUyQuyen["colAccount"] = Account;
                                rowNguoiUyQuyen["colRole"] = nodeRoleIBNguoiUyQuyen.Text;
                                rowNguoiUyQuyen["colRoleID"] = nodeRoleIBNguoiUyQuyen.Value;
                                rowNguoiUyQuyen["colTranCode"] = nodeTrancodeIBNguoiUyQuyen.Text;
                                rowNguoiUyQuyen["colTranCodeID"] = nodeTrancodeIBNguoiUyQuyen.Value;
                                rowNguoiUyQuyen["colServiceID"] = SmartPortal.Constant.IPC.SMS;

                                tblNguoiUyQuyen.Rows.Add(rowNguoiUyQuyen);
                            }

                        }
                        else
                        {
                            if (tblNguoiUyQuyen.Select("colFullName='" + fullName + "' and colAccount='" + Account + "' and colRoleID='" + nodeRoleIBNguoiUyQuyen.Value + "' and colTranCode='" + nodeTrancodeIBNguoiUyQuyen.Text + "'").Length != 0)
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

            //MB
            if (MBPhone != "")
            {
                foreach (TreeNode nodeRoleIBNguoiUyQuyen in tvMBRole.Nodes)
                {
                    foreach (TreeNode nodeTrancodeIBNguoiUyQuyen in nodeRoleIBNguoiUyQuyen.ChildNodes)
                    {
                        if (nodeTrancodeIBNguoiUyQuyen.Checked)
                        {
                            if (tblNguoiUyQuyen.Select("colFullName='" + fullName + "' and colAccount='" + Account + "' and colRoleID='" + nodeRoleIBNguoiUyQuyen.Value + "' and colTranCode='" + nodeTrancodeIBNguoiUyQuyen.Text + "'").Length == 0)
                            {
                                DataRow rowNguoiUyQuyen = tblNguoiUyQuyen.NewRow();
                                rowNguoiUyQuyen["colFullName"] = fullName.Trim();
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
                                rowNguoiUyQuyen["colAccount"] = Account;
                                rowNguoiUyQuyen["colRole"] = nodeRoleIBNguoiUyQuyen.Text;
                                rowNguoiUyQuyen["colRoleID"] = nodeRoleIBNguoiUyQuyen.Value;
                                rowNguoiUyQuyen["colTranCode"] = nodeTrancodeIBNguoiUyQuyen.Text;
                                rowNguoiUyQuyen["colTranCodeID"] = nodeTrancodeIBNguoiUyQuyen.Value;
                                rowNguoiUyQuyen["colServiceID"] = SmartPortal.Constant.IPC.MB;

                                tblNguoiUyQuyen.Rows.Add(rowNguoiUyQuyen);
                            }

                        }
                        else
                        {
                            if (tblNguoiUyQuyen.Select("colFullName='" + fullName + "' and colAccount='" + Account + "' and colRoleID='" + nodeRoleIBNguoiUyQuyen.Value + "' and colTranCode='" + nodeTrancodeIBNguoiUyQuyen.Text + "'").Length != 0)
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

            ViewState[sessionName] = tblNguoiUyQuyen;
            gvResult.DataSource = tblNguoiUyQuyen;
            gvResult.DataBind();
        }
    }


    protected void btback_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState["NGUOIQUANTRI"] = null;
            RedirectBackToMainPage();
        }
        catch
        {

        }
    }
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        try
        {
            //approve
            string userID = "";
            string status = lblStatus.Text.Trim();

            if (Session["userIDA"] != null)
            {
                List<string> lstTran = new List<string>();
                lstTran = (List<string>)Session["userIDA"];

                userID = lstTran[0].Trim();

                //xử lý            


                new SmartPortal.SEMS.User().ApproveUser(userID, SmartPortal.Constant.IPC.ACTIVE, Session["userID"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
                if (status == SmartPortal.Constant.IPC.NEW)
                {
                    SendInfoLogin(userID);
                }
                else
                {
                    //vutran luon luon gui mail danh cho SMS va MB
                    SendInfoLogin(userID);
                }

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
                    ltrError.Text = IPCERRORDESC;

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
                new SmartPortal.SEMS.User().ApproveUser(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["uid"].ToString(), SmartPortal.Constant.IPC.ACTIVE, Session["userID"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);

                if (status == SmartPortal.Constant.IPC.NEW)
                {
                    SendInfoLogin(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["uid"].ToString());
                }
                else
                {
                    //vutran luon luon gui mail danh cho SMS va MB
                    SendInfoLogin(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["uid"].ToString());
                }

                if (IPCERRORCODE == "0")
                {
                    ltrError.Text = Resources.labels.nguoidung + " " + SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["uid"].ToString() + " " + Resources.labels.duyetthanhcong;

                    goto REDI;
                }
                else
                {
                    throw new IPCException(IPCERRORDESC);

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

    void SendInfoLogin(string userID)
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
        string SMSdefaultAccount = "";
        string SMSpincode = "";
        string PHOuser = "";
        string PHOpass = "";
        string PHOpincode = "";
        string PhoneNo = "";
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

                if (boolIB || boolMB || boolSMS || boolAM)
                {
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

                    if (boolMB)
                    {
                        #region lay het các tai khoan MB cua user theo userID
                        DataSet accountMBDataset;
                        if (!row["CTYPE"].ToString().Equals("W"))
                        {
                            accountMBDataset = new SmartPortal.SEMS.User().GetUserRoleByServiceID(SmartPortal.Constant.IPC.MB, row["USERID"].ToString().Trim(), SmartPortal.Constant.IPC.NORMAL, ref IPCERRORCODE, ref IPCERRORDESC);

                            if (IPCERRORCODE != "0")
                            {
                                goto ERROR;
                            }
                        }
                        else
                        {
                            accountMBDataset = new SmartPortal.SEMS.User().GetUserRoleByServiceID(SmartPortal.Constant.IPC.MB, row["USERID"].ToString().Trim(), SmartPortal.Constant.IPC.WAL, ref IPCERRORCODE, ref IPCERRORDESC);

                            if (IPCERRORCODE != "0")
                            {
                                goto ERROR;
                            }
                        }

                        DataTable accountMBTable = accountMBDataset.Tables[0];
                        if (accountMBTable.Rows.Count != 0)
                        {
                            PasswordMB = string.IsNullOrEmpty(accountMBTable.Rows[0]["PASS"] as string) ? "******" : SmartPortal.Security.Encryption.Decrypt(accountMBTable.Rows[0]["PASS"].ToString());
                            st.Append("<tr>");
                            st.Append("<td colspan='4'>");
                            st.Append("<B>MOBILE BANKING</B><hr/>");
                            st.Append("</td>");
                            st.Append("</tr>");

                            st.Append("<tr>");
                            st.Append("<td>");
                            st.Append(Resources.labels.phone);
                            st.Append("</td>");
                            st.Append("<td>");
                            st.Append("<b>" + accountMBTable.Rows[0]["UN"].ToString() + "</b>");
                            st.Append("</td>");
                            st.Append("<td>");
                            st.Append(Resources.labels.matkhau);
                            st.Append("</td>");
                            st.Append("<td>");
                            st.Append("<b>" + PasswordMB + "</b>");
                            st.Append("</td>");
                            st.Append("</tr>");

                            st.Append("<tr>");
                            st.Append("<td colspan=1>  ");
                            st.Append(Resources.labels.username + " :");
                            st.Append("</td>");
                            st.Append("<td colspan=1>  ");
                            st.Append("<b>" + accountMBTable.Rows[0]["USERNAME"].ToString() + "</b>");
                            st.Append("</td>");
                            if (sendPincode())
                            {
                                st.Append("<td colspan=1>  ");
                                st.Append(Resources.labels.pincode + " :");
                                st.Append("</td>");
                                st.Append("<td colspan=1>  ");
                                st.Append("<b>" + SmartPortal.Security.Encryption.Decrypt(accountMBTable.Rows[0]["PINCODE"].ToString()) + "</b>");
                                st.Append("</td>");
                            }
                            st.Append("</tr>");

                            PhoneMB = accountMBTable.Rows[0]["UN"].ToString();
                            PassMB = SmartPortal.Security.Encryption.Decrypt(accountMBTable.Rows[0]["PASS"].ToString());
                            PincodeMB = SmartPortal.Security.Encryption.Decrypt(accountMBTable.Rows[0]["PINCODE"].ToString());
                        }
                        #endregion
                    }
                    if (boolAM)
                    {
                        #region lay het các tai khoan MB cua user theo userID
                        DataSet accountAMDataset = new SmartPortal.SEMS.User().GetUserRoleByServiceID(SmartPortal.Constant.IPC.AM, row["USERID"].ToString().Trim(), SmartPortal.Constant.IPC.MBA, ref IPCERRORCODE, ref IPCERRORDESC);

                        if (IPCERRORCODE != "0")
                        {
                            goto ERROR;
                        }

                        DataTable  accountAMTable = accountAMDataset.Tables[0];
                        if (accountAMTable.Rows.Count != 0)
                        {
                            PasswordMB = string.IsNullOrEmpty(accountAMTable.Rows[0]["PASS"] as string) ? "******" : SmartPortal.Security.Encryption.Decrypt(accountAMTable.Rows[0]["PASS"].ToString());
                            st.Append("<tr>");
                            st.Append("<td colspan='4'>");
                            st.Append("<B>MOBILE BANKING</B><hr/>");
                            st.Append("</td>");
                            st.Append("</tr>");

                            st.Append("<tr>");
                            st.Append("<td>");
                            st.Append(Resources.labels.phone);
                            st.Append("</td>");
                            st.Append("<td>");
                            st.Append("<b>" + accountAMTable.Rows[0]["UN"].ToString() + "</b>");
                            st.Append("</td>");
                            st.Append("<td>");
                            st.Append(Resources.labels.matkhau);
                            st.Append("</td>");
                            st.Append("<td>");
                            st.Append("<b>" + PasswordMB + "</b>");
                            st.Append("</td>");
                            st.Append("</tr>");

                            st.Append("<tr>");
                            st.Append("<td colspan=1>  ");
                            st.Append(Resources.labels.username + " :");
                            st.Append("</td>");
                            st.Append("<td colspan=1>  ");
                            st.Append("<b>" + accountAMTable.Rows[0]["USERNAME"].ToString() + "</b>");
                            st.Append("</td>");
                            if (sendPincode())
                            {
                                st.Append("<td colspan=1>  ");
                                st.Append(Resources.labels.pincode + " :");
                                st.Append("</td>");
                                st.Append("<td colspan=1>  ");
                                st.Append("<b>" + SmartPortal.Security.Encryption.Decrypt(accountAMTable.Rows[0]["PINCODE"].ToString()) + "</b>");
                                st.Append("</td>");
 
                            }
                            st.Append("</tr>");
                            st.Append("<br><br>");

                            PhoneMB = accountAMTable.Rows[0]["UN"].ToString();
                            PassMB = SmartPortal.Security.Encryption.Decrypt(accountAMTable.Rows[0]["PASS"].ToString());
                            PincodeMB = SmartPortal.Security.Encryption.Decrypt(accountAMTable.Rows[0]["PINCODE"].ToString());
                        }
                        #endregion
                    }
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
                    str += "<div><br/><p><a href='https://www.scb.co.th/en/personal-banking.html' target='blank'></a></p><span style='font-weight:bold;'>" + Resources.labels.AbankthankyouforusingAYAebankingservices + "!	</span></div>";
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
                        if (!IBuser.Equals(string.Empty))
                        {
                            ibuserinfo = "- IB :User-" + IBuser + ",password-" + IBpass;
                        }
                        else if (boolMB)
                        {
                            mbuserinfo = "/MB:PhoneNo-" + PhoneMB + ",password-" + PassMB;
                            if (sendPincode())
                            {
                                mbuserinfo += ",Pincode-" + PincodeMB;
                            }
                        }
                        else if (boolAM)
                        {
                            mbuserinfo = "/AM:PhoneNo-" + PhoneMB + ",password-" + PassMB;
                            if (sendPincode())
                            {
                                mbuserinfo += ",Pincode-" + PincodeMB;
                            }
                        }
                        //if (!PhoneNo.Equals(string.Empty))
                        //{
                        //    smsuserinfo = "/SB:phoneno-" + PhoneNo + ",pincode-" + SMSpincode;
                        //}
                        SENDINFO = contractinfo + ibuserinfo + smsuserinfo + mbuserinfo;
                    }
                    //else
                    //{
                    //    goto N;
                    //}



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
    protected void btnReject_Click(object sender, EventArgs e)
    {
        try
        {
            //approve
            string userID = "";

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
                    ltrError.Text = Resources.labels.nguoidung + " " + contractNo + " " + Resources.labels.khongduocduyetmoibanchonnguoidungtieptheo;

                    //cập nhật lại List
                    lstTran.RemoveAt(0);
                }
                else
                {
                    throw new IPCException(IPCERRORDESC);

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
                new SmartPortal.SEMS.User().ApproveUser(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["uid"].ToString(), SmartPortal.Constant.IPC.REJECT, Session["userID"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);

                SendInfoLogin(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["uid"].ToString());

                if (IPCERRORCODE == "0")
                {
                    ltrError.Text = Resources.labels.nguoidung + " " + SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["uid"].ToString() + " " + Resources.labels.khongduocduyet;

                    goto REDI;
                }
                else
                {
                    throw new IPCException(IPCERRORDESC);

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
    protected void ddlUserType_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet dsType = new DataSet();

        dsType = new SmartPortal.SEMS.User().LoadTypeByParentType(SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlUserType.SelectedValue.ToString()), ref IPCERRORCODE, ref IPCERRORDESC);

        if (dsType.Tables[0].Rows.Count == 0)
        {
            ddlType.Items.Clear();
            ddlType.Items.Insert(0, new ListItem(Resources.labels.khongtimthaynguoidung, ""));
        }
        else
        {
            ddlType.DataSource = dsType;
            ddlType.DataTextField = "TYPENAME";
            ddlType.DataValueField = "TYPELEVEL";
            ddlType.DataBind();
        }
    }
    protected void ddlpolicyIB_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            ddlpolicyMB.SelectedValue = ddlpolicyIB.SelectedValue;
        }
        catch
        { }
    }
    public void ShowTab()
    {
        try
        {
            if (SHOWTAB.Equals(Resources.labels.walletbanking))
            {
                liTabWL.Visible = true;
                liTabAM.Visible = false;
            }
            else if (SHOWTAB.Equals(Resources.labels.agentmerchant))
            {
                liTabWL.Visible = false;
                liTabAM.Visible = true;
            }
            else
            {
                liTabWL.Visible = false;
                liTabAM.Visible = false;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
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
    private string generateUserName()
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
    private bool CheckUserNameExists(string userName)
    {
        DataSet ds = new Customer().CheckUserName("EBA_Users_CheckUserNameMB", new object[] { userName, SmartPortal.Constant.IPC.CONTRACTINDIVIDUAL }, ref IPCERRORCODE, ref IPCERRORDESC);
        if (IPCERRORCODE != "0")
        {
            lblAlert.Text = IPCERRORDESC;
            return false;
        }
        return true;
    }
    protected void txtUserNameMB_TextChanged(object sender, EventArgs e)
    {
        if (!CheckUserNameExists(txtUserNameMB.Text))
        {
            ltrError.Text = Resources.labels.usernamehasexists;
        }
    }
}