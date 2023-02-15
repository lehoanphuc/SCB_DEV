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

public partial class Widgets_SEMSUser_Wallet_Widget : WidgetBase
{
    string ACTION = "";
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    DataTable userTable = new DataTable();
    DataTable userTableTemp = new DataTable();
    DataTable tblRoleDefault = new DataTable();
    static List<string> lsAccNo = new List<string>();
    string contractNo
    {
        get
        {
            return ViewState["contractNo"] != null ? ViewState["contractNo"].ToString() : "";
        }
        set { ViewState["contractNo"] = value; }
    }
    public string CONTRACTNO
    {
        get { return ViewState["CONTRACTNO"] != null ? (string)ViewState["CONTRACTNO"] : string.Empty; }
        set { ViewState["CONTRACTNO"] = value; }
    }
    public string USERTNAMEMB
    {
        get { return ViewState["USERTNAMEMB"] != null ? (string)ViewState["USERTNAMEMB"] : string.Empty; }
        set { ViewState["USERTNAMEMB"] = value; }
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
    string userName
    {
        get { return ViewState["userName"] != null ? ViewState["userName"].ToString() : ""; }
        set { ViewState["userName"] = value; }
    }
    string userType = "";
    string productType = "";
    //string productid = "";
    string corptype = "";
    private int passlenIB;
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
    public string localFullName
    {
        get { return ViewState["LOCALFULLNAME"] != null ? (string)ViewState["LOCALFULLNAME"] : string.Empty; }
        set { ViewState["LOCALFULLNAME"] = value; }
    }
    string addressOld
    {
        get { return ViewState["addressOld"] != null ? ViewState["addressOld"].ToString() : ""; }
        set { ViewState["addressOld"] = value; }
    }
    public string USERTYPE
    {
        get
        {
            return ViewState["USERTYPE"] != null ? ViewState["USERTYPE"].ToString() : "";
        }
        set { ViewState["USERTYPE"] = value; }
    }
    protected void Page_Init(object sender, EventArgs e)
    {
        TabCustomerInfoHelperWalletOnly.LoadConfig();
    }
    protected void Page_Load(object sender, EventArgs e)
    {


        //txtWLPhoneNo.Enabled = false;
        try
        {
            lblAlert.Text = string.Empty;
            lblError.Text = string.Empty;
            CONTRACTTYPE = SmartPortal.Constant.IPC.CONTRACTINDIVIDUAL;
            ACTION = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["a"].ToString().Trim();
            ddlReGender.Items.Add(new ListItem(Resources.labels.male, "M"));
            ddlReGender.Items.Add(new ListItem(Resources.labels.female, "F"));
            #region load usertype
            DataSet dsUserType = new DataSet();
            DataTable dtUserType = new DataTable();
            //if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["ct"] == SmartPortal.Constant.IPC.PERSONALCONTRACT)
            //{
            //    dtUserType = new SmartPortal.SEMS.Contract().LoadContractType(SmartPortal.Constant.IPC.PERSONAL, "Y");
            //    ddlUserType.Visible = false;
            //    lbltype.Visible = false;
            //}
            //if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["ct"] == SmartPortal.Constant.IPC.CORPORATECONTRACT)
            //{
            //    dtUserType = new SmartPortal.SEMS.Contract().LoadContractType(SmartPortal.Constant.IPC.CORPORATE, "Y");
            //    ddlUserType.Visible = true;
            //    lbltype.Visible = true;
            //}
            //if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["ct"] == "RP")
            //{
            //    dtUserType = new SmartPortal.SEMS.Contract().LoadContractType(SmartPortal.Constant.IPC.PERSONAL, "Y");
            //    ddlUserType.Visible = false;
            //    lbltype.Visible = false;
            //}
            dtUserType = new SmartPortal.SEMS.Contract().LoadContractType(SmartPortal.Constant.IPC.PERSONAL, "Y");

            ddlUserType.DataSource = dtUserType;
            ddlUserType.DataTextField = "TYPENAME";
            ddlUserType.DataValueField = "USERTYPE";
            ddlUserType.DataBind();

            ddlUserType.SelectedValue = SmartPortal.Constant.IPC.CONSUMER;
            ddlUserType.Enabled = false;

            #endregion
            //load chi nhánh
            DropDownListBranch.DataSource = new SmartPortal.SEMS.Branch().GetAll(ref IPCERRORCODE, ref IPCERRORDESC);
            DropDownListBranch.DataTextField = "BRANCHNAME";
            DropDownListBranch.DataValueField = "BRANCHID";
            DropDownListBranch.DataBind();
            if (!IsPostBack)
            {
                //23.2.2016 minh add policy
                //load policy to dropdownlist
                DataSet dspolicy = new DataSet();
                string filterWL = "serviceid='MB'";
                string stSort = "serviceid asc";

                dspolicy = new SmartPortal.SEMS.USERPOLICY().GetPolicybyCondition(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, Session["userName"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE == "0")
                {



                    DataTable dtWL = dspolicy.Tables[0].Select(filterWL, stSort).CopyToDataTable();
                    ddlpolicyWL.DataSource = dtWL;
                    passlenIB = Convert.ToInt32(dtWL.Rows[0]["minpwdlen"].ToString());

                    ddlpolicyWL.DataSource = dtWL;
                    ddlpolicyWL.DataTextField = "policytx";
                    ddlpolicyWL.DataValueField = "policyid";

                    ddlpolicyWL.DataBind();
                    //disable policymb
                    // ddlpolicyMB.Enabled = false;


                }
                BindData();
            }

        }
        catch
        {
        }

    }
    private void BindData()
    {
        switch (ACTION)
        {
            case SmartPortal.Constant.IPC.VIEWDETAIL:
            case SmartPortal.Constant.IPC.APPROVER:
                txtWLPhoneNo.Enabled = false;
                txtReMobi.Enabled = false;
                ddlReGender.Enabled = false;
                btnThemChuTaiKhoan.Visible = false;
                btnHuy.Visible = false;
                txtReBirth.Enabled = false;
                txtReEmail.Enabled = false;
                txtReAddress.Enabled = false;
                ddlpolicyWL.Enabled = false;
                ddlpolicySMS.Enabled = false;
                tvWL.Enabled = false;
                tvSMS.Enabled = false;
                txtSMSPhoneNo.Enabled = false;
                ddlSMSDefaultAcctno.Enabled = false;
                ddlLanguage.Enabled = false;
                txtReFullName.Enabled = false;
                btnCustSave.Visible = false;
                txtLocalName.Enabled = false;
                break;
            case SmartPortal.Constant.IPC.EDIT:
                ddlUserType.Visible = true;
                ddlUserType.Enabled = false;
                txtReMobi.Enabled = false;
                lbltype.Visible = true;
                ddlReGender.Enabled = false;
                ddlDefaultAccountQT.Enabled = true;
                break;
        }
        userTableTemp = GetUserContract();
        if (userTableTemp.Rows.Count != 0)
        {
            
            
            CONTRACTNO = contractNo = userTableTemp.Rows[0]["CONTRACTNO"].ToString();

            //userName = userTableTemp.Rows[0]["CUSTCODE"].ToString().Trim() + userTableTemp.Rows[0]["CFTYPE"].ToString().Trim() + DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - 6, 4);

            //userName = userTableTemp.Rows[0]["contracttype"].Equals("MTR") ? generateUserName() : generateUserName(false);
            //productid = userTableTemp.Rows[0]["PRODUCTID"].ToString();
            userName = userTableTemp.Rows[0]["UserID"].ToString();
            userType = userTableTemp.Rows[0]["USERTYPE"].ToString();
            corptype = userTableTemp.Rows[0]["contracttype"].ToString();
            productType = userTableTemp.Rows[0]["PRODUCTID"].ToString();
        }
        if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["uid"] != null)
        {


            USERTK = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["uid"].ToString();
            #region Lấy thông tin user

            userTable = (new SmartPortal.SEMS.User().GetFullUserByUID(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["uid"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
            if (userTable.Rows.Count != 0)
            {

                //QuangTV - add old value to log
                emailOld = txtReEmail.Text = userTable.Rows[0]["EMAIL"].ToString();
                genderOld = ddlReGender.SelectedValue = userTable.Rows[0]["GENDER"].ToString();
                addressOld = txtReAddress.Text = userTable.Rows[0]["ADDRESS"].ToString();
                phoneOld = txtWLPhoneNo.Text = userTable.Rows[0]["PHONE"].ToString();

                txtReFullName.Text = userTable.Rows[0]["FULLNAME"].ToString();
                localFullName = txtLocalName.Text = userTable.Rows[0]["LOCALFULLNAME"].ToString();
                txtReMobi.Text = userTable.Rows[0]["PHONE"].ToString();
                USERTYPE = ddlUserType.SelectedValue = userTable.Rows[0]["USERTYPE"].ToString();
                txtReEmail.Text = userTable.Rows[0]["EMAIL"].ToString();
                USERTNAMEMB = userTable.Rows[0]["USERNAME"].ToString();
                ddlReGender.SelectedValue = userTable.Rows[0]["GENDER"].ToString();
                ddlUserLevel.SelectedValue = userTable.Rows[0]["USERLEVEL"].ToString();
                txtReAddress.Text = userTable.Rows[0]["ADDRESS"].ToString();
                ddlUserType.SelectedValue = userTable.Rows[0]["USERTYPE"].ToString();
                txtReFullName.Enabled = false;


                if (userTable.Rows[0]["BIRTHDAY"].ToString() != "")
                {
                    //HaiNT Edit birthDate (12/08/2013)
                    string birthDate = SmartPortal.Common.Utilities.Utility.IsDateTime2(userTable.Rows[0]["BIRTHDAY"].ToString()).ToString("dd/MM/yyyy");
                    txtReBirth.Text = birthDate.Equals("01/01/1900") ? "" : birthDate;
                    birthdayOld = txtReBirth.Text = birthDate.Equals("01/01/1900") ? "" : birthDate;
                }
                //lblStatus.Text = userTable.Rows[0]["STATUS"].ToString();
                //branch
                DropDownListBranch.Enabled = false;
                if (!userTable.Rows[0][SmartPortal.Constant.IPC.BRANCHID].ToString().Trim().Equals(""))
                    DropDownListBranch.SelectedValue = SmartPortal.Common.Utilities.Utility.FormatStringCore(int.Parse(userTable.Rows[0][SmartPortal.Constant.IPC.BRANCHID].ToString().Trim()).ToString());
                //if (userTable.Rows[0]["IBpolicyid"].ToString().Trim() != string.Empty)
                //    ddlpolicyIB.SelectedValue = userTable.Rows[0]["IBpolicyid"].ToString().Trim();
                ////disable policy of corp when edit
                //if (userTable.Rows[0]["TYPEID"].ToString().Trim() == "QTHT")
                //    ddlpolicyIB.Enabled = false;

                //DataColumnCollection columns = userTable.Columns;
                //if (columns.Contains("SMSpolicyid"))
                //{
                //    if (userTable.Rows[0]["SMSpolicyid"].ToString().Trim() != string.Empty)
                //        ddlpolicySMS.SelectedValue = userTable.Rows[0]["SMSpolicyid"].ToString().Trim();
                //    if (userTable.Rows[0]["TYPEID"].ToString().Trim() == "QTHT")
                //        ddlpolicySMS.Enabled = false;

                //}
                if (userTable.Rows[0]["WLpolicyid"].ToString().Trim() != string.Empty)
                    ddlpolicyWL.SelectedValue = userTable.Rows[0]["MBpolicyid"].ToString().Trim();
                if (userTable.Rows[0]["TYPEID"].ToString().Trim() == "QTHT")
                    ddlpolicyWL.Enabled = false;
                if (userTable.Rows[0]["STATUS"].ToString().Equals(SmartPortal.Constant.IPC.REJECT))
                {
                    LoadRejectReason(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["uid"].ToString(), "CUSTOMER");
                    divReject.Visible = true;
                }
            }
            #endregion

            #region Lấy thông tin tài khoản User
            DataTable tblWL = new SmartPortal.IB.User().GetAccountDetail(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["uid"].ToString(), SmartPortal.Constant.IPC.EW);
            if (tblWL.Rows.Count != 0)
            {
                txtUserName.Text = tblWL.Rows[0]["UserID"].ToString().Trim();
                txtUserName.Enabled = false;
            }
            #endregion

            #region lấy thông tin tất cả các quyền
            DataTable tblRoleDetail = new SmartPortal.IB.User().GetRoleDetail(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["uid"].ToString());
            if (tblRoleDetail.Select("SERVICEID = 'WL'").Length > 0)
            {
                ViewState["IsUseWL"] = true;
            }
            else
            {
                ViewState["IsUseWL"] = false;
            }
            //luu thông tin vào session và hiển thị lên lưới
            DataTable tblNguoiUyQuyen = new DataTable();
            DataColumn colFullName = new DataColumn("colFullName");
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
                r["colLevel"] = ddlUserLevel.SelectedValue;
                r["colGroup"] = string.Empty;
                //r["colBirthday"] = ddlUserLevel.SelectedValue;
                r["colGender"] = ddlReGender.SelectedValue;
                r["colPhone"] = txtReMobi.Text;
                r["colEmail"] = txtReEmail.Text;
                r["colAddress"] = txtReAddress.Text;
                r["colIBUserName"] = userName;
                r["colIBPass"] = DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - 8, 6);
                //r["colSMSPhone"] = txtSMSPhoneNo.Text;
                //r["colSMSDefaultAcctno"] = ddlSMSDefaultAcctno.SelectedValue;
                //r["colSMSDefaultLang"] = ddlDefaultLang.SelectedValue;
                r["colMBPhone"] = string.Empty;
                r["colMBPass"] = string.Empty;
                r["colWLPhone"] = txtWLPhoneNo.Text;
                r["colWLPass"] = DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - 8, 6);
                r["colPHOPhone"] = string.Empty;
                r["colPHOPass"] = DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - 8, 6);
                r["colAccount"] = rRole["ACCTNO"].ToString();
                r["colRole"] = rRole["ROLENAME"].ToString();
                r["colRoleID"] = rRole["ROLEID"].ToString();
                r["colTranCode"] = rRole["PAGENAME"].ToString();
                r["colTranCodeID"] = rRole["IPCTRANCODE"].ToString();
                r["colServiceID"] = rRole["SERVICEID"].ToString();
                r["colIBPolicy"] = string.Empty;
                r["colSMSPolicy"] = string.Empty;
                r["colMBPolicy"] = string.Empty;
                r["colWLPolicy"] = ddlpolicyWL.SelectedValue.ToString();
                r["colpwdreset"] = string.Empty;


                //if (cbIsDefault.Checked)
                //{
                //    r["colSMSIsDefault"] = "Y";
                //}
                //else
                //{
                //    r["colSMSIsDefault"] = "N";
                //}

                r["colSMSPinCode"] = "";
                r["colMBPinCode"] = "";
                //r["colPHODefaultAcctno"] = ddlPHODefaultAcctno.SelectedValue;

                tblNguoiUyQuyen.Rows.Add(r);
            }

            ViewState["NGUOIQUANTRI"] = tblNguoiUyQuyen;

            DataTable dtResultQuanTri = tblNguoiUyQuyen.DefaultView.ToTable(true, "colFullName", "colPhone", "colAccount", "colRole");
            ViewState["UserRolesOld"] = dtResultQuanTri.DefaultView.ToTable(true, "colPhone", "colAccount"); ;
            gvResultChuTaiKhoan.DataSource = dtResultQuanTri;
            gvResultChuTaiKhoan.DataBind();
            #endregion

            #region Hien thi tat cac cac role theo serviceid va usertype len cay
            //load for WL
            LoadDataInTreeview(SmartPortal.Constant.IPC.MB, tvWL, contractNo, SmartPortal.Constant.IPC.WAL);

            GetRoleDefault(tvWL, SmartPortal.Constant.IPC.WAL, SmartPortal.Constant.IPC.MB);
            #endregion

            DataTable DefaultAccount = new SmartPortal.SEMS.Contract().GETUSERACCOUNTDEFAULT(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["uid"]);
            ddlDefaultAccountQT.DataSource = DefaultAccount;
            ddlDefaultAccountQT.DataTextField = "ACCOUNTNO";
            ddlDefaultAccountQT.DataValueField = "ACCOUNTNO";
            ddlDefaultAccountQT.DataBind();
            ddlDefaultAccountQT.SelectedValue = DefaultAccount.Rows[0]["ACCOUNTNO"].ToString();
            LoadComboboxReason();
        }
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

        //  DataSet ds = new SmartPortal.IB.Role().GetRoleByServiceID(serviceID, cn, ref IPCERRORCODE, ref IPCERRORDESC);
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

    void LuuThongTinQuyen(string sessionName, GridView gvResult, TreeView tvWLRole, string fullName, string level, string birthday, string gender, string phone, string email, string address, string IBUserName, string IBPass, string SMSPhone, string SMSDefaultAcctno, string SMSDefaultLang, string isDefault, string SMSPinCode, string MBPhone, string MBPass, string MBPinCode, string PHOPhone, string PHOPass, string PHODefaultAcctno, string Account, string pwdreset, string WLPinCode, string WLPhone, string WLPass)
    {
        SmartPortal.Common.Log.WriteLogFile("pwdreset===============", "", "", pwdreset);
        SmartPortal.Common.Log.WriteLogFile("pincodesms ==============", "", "", SMSPinCode);
        if (ViewState[sessionName] == null)
        {
            #region Khoi tao bang
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
            DataColumn colWLPhone = new DataColumn("colWLPhone");
            DataColumn colWLPass = new DataColumn("colWlPass");
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

            #endregion

            #region them giao dich WL
            //PHO
            if (WLPhone != "")
            {
                foreach (TreeNode nodeRoleIBNguoiUyQuyen in tvWL.Nodes)
                {
                    if (nodeRoleIBNguoiUyQuyen.Checked)
                    {
                        foreach (TreeNode nodeTrancodeIBNguoiUyQuyen in nodeRoleIBNguoiUyQuyen.ChildNodes)
                        {

                            DataRow rowNguoiUyQuyen = tblNguoiUyQuyen.NewRow();
                            rowNguoiUyQuyen["colFullName"] = fullName.Trim();
                            rowNguoiUyQuyen["colLevel"] = level.Trim();
                            rowNguoiUyQuyen["colBirthday"] = birthday.Trim();
                            rowNguoiUyQuyen["colGender"] = gender.Trim();
                            rowNguoiUyQuyen["colPhone"] = phone.Trim();
                            rowNguoiUyQuyen["colEmail"] = email.Trim();
                            rowNguoiUyQuyen["colAddress"] = address.Trim();
                            //rowNguoiUyQuyen["colIBUserName"] = IBUserName.Trim();
                            rowNguoiUyQuyen["colIBUserName"] = userName.Trim();
                            rowNguoiUyQuyen["colIBPass"] = IBPass;
                            rowNguoiUyQuyen["colSMSPhone"] = SMSPhone.Trim();
                            rowNguoiUyQuyen["colSMSDefaultAcctno"] = SMSDefaultAcctno.Trim();
                            rowNguoiUyQuyen["colSMSDefaultLang"] = SMSDefaultLang.Trim();
                            rowNguoiUyQuyen["colSMSIsDefault"] = isDefault;
                            rowNguoiUyQuyen["colSMSPinCode"] = SMSPinCode;
                            rowNguoiUyQuyen["colMBPinCode"] = MBPinCode;
                            rowNguoiUyQuyen["colMBPhone"] = MBPhone.Trim();
                            rowNguoiUyQuyen["colMBPass"] = MBPass;
                            rowNguoiUyQuyen["colWLPinCode"] = WLPinCode;
                            rowNguoiUyQuyen["colWLPhone"] = WLPhone.Trim();
                            rowNguoiUyQuyen["colWLPass"] = WLPass;
                            rowNguoiUyQuyen["colPHOPhone"] = PHOPhone.Trim();
                            rowNguoiUyQuyen["colPHOPass"] = PHOPass;
                            rowNguoiUyQuyen["colPHODefaultAcctno"] = PHODefaultAcctno;
                            rowNguoiUyQuyen["colAccount"] = Account;
                            rowNguoiUyQuyen["colRole"] = nodeRoleIBNguoiUyQuyen.Text;
                            rowNguoiUyQuyen["colRoleID"] = nodeRoleIBNguoiUyQuyen.Value;
                            rowNguoiUyQuyen["colTranCode"] = nodeTrancodeIBNguoiUyQuyen.Text;
                            rowNguoiUyQuyen["colTranCodeID"] = nodeTrancodeIBNguoiUyQuyen.Value;
                            rowNguoiUyQuyen["colServiceID"] = SmartPortal.Constant.IPC.MB;
                            rowNguoiUyQuyen["colWLPolicy"] = ddlpolicyWL.SelectedValue.ToString();
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
            // DataTable newTable=SmartPortal.Common.Utilities.Utility.
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

            #region them giao dich WL
            //PHO
            if (WLPhone != "")
            {
                foreach (TreeNode nodeRoleIBNguoiUyQuyen in tvWL.Nodes)
                {
                    if (nodeRoleIBNguoiUyQuyen.Checked)
                    {
                        foreach (TreeNode nodeTrancodeIBNguoiUyQuyen in nodeRoleIBNguoiUyQuyen.ChildNodes)
                        {

                            DataRow rowNguoiUyQuyen = tblNguoiUyQuyen.NewRow();
                            rowNguoiUyQuyen["colFullName"] = fullName.Trim();
                            rowNguoiUyQuyen["colLevel"] = level.Trim();
                            rowNguoiUyQuyen["colBirthday"] = birthday.Trim();
                            rowNguoiUyQuyen["colGender"] = gender.Trim();
                            rowNguoiUyQuyen["colPhone"] = phone.Trim();
                            rowNguoiUyQuyen["colEmail"] = email.Trim();
                            rowNguoiUyQuyen["colAddress"] = address.Trim();
                            //rowNguoiUyQuyen["colIBUserName"] = IBUserName.Trim();
                            rowNguoiUyQuyen["colIBUserName"] = userName.Trim();
                            rowNguoiUyQuyen["colIBPass"] = IBPass;
                            rowNguoiUyQuyen["colSMSPhone"] = SMSPhone.Trim();
                            rowNguoiUyQuyen["colSMSDefaultAcctno"] = SMSDefaultAcctno.Trim();
                            rowNguoiUyQuyen["colSMSDefaultLang"] = SMSDefaultLang.Trim();
                            rowNguoiUyQuyen["colSMSIsDefault"] = isDefault;
                            rowNguoiUyQuyen["colSMSPinCode"] = SMSPinCode;
                            rowNguoiUyQuyen["colMBPinCode"] = MBPinCode;
                            rowNguoiUyQuyen["colMBPhone"] = MBPhone.Trim();
                            rowNguoiUyQuyen["colMBPass"] = MBPass;
                            rowNguoiUyQuyen["colWLPinCode"] = WLPinCode;
                            rowNguoiUyQuyen["colWLPhone"] = WLPhone.Trim();
                            rowNguoiUyQuyen["colWLPass"] = WLPass;
                            rowNguoiUyQuyen["colPHOPhone"] = PHOPhone.Trim();
                            rowNguoiUyQuyen["colPHOPass"] = PHOPass;
                            rowNguoiUyQuyen["colPHODefaultAcctno"] = PHODefaultAcctno;
                            rowNguoiUyQuyen["colAccount"] = Account;
                            rowNguoiUyQuyen["colRole"] = nodeRoleIBNguoiUyQuyen.Text;
                            rowNguoiUyQuyen["colRoleID"] = nodeRoleIBNguoiUyQuyen.Value;
                            rowNguoiUyQuyen["colTranCode"] = nodeTrancodeIBNguoiUyQuyen.Text;
                            rowNguoiUyQuyen["colTranCodeID"] = nodeTrancodeIBNguoiUyQuyen.Value;
                            rowNguoiUyQuyen["colServiceID"] = SmartPortal.Constant.IPC.MB;
                            rowNguoiUyQuyen["colWLPolicy"] = ddlpolicyWL.SelectedValue.ToString();
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
            // DataTable newTable=SmartPortal.Common.Utilities.Utility.
            gvResult.DataSource = tblNguoiUyQuyen.DefaultView.ToTable(true, "colFullName", "colPhone", "colAccount", "colRole");
            gvResult.DataBind();
        }
    }
    private void ShowPopUpMsg(string msg)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("alert('");
        sb.Append(msg.Replace("\n", "\\n").Replace("\r", "").Replace("'", "\\'"));
        sb.Append("');");
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "showalert", sb.ToString(), true);
    }
    protected void ddlUserType_SelectedIndexChanged(object sender, EventArgs e)
    { }
    protected void btnHuy_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable tblResultChuTaiKhoan = (DataTable)ViewState["NGUOIQUANTRI"];

            tblResultChuTaiKhoan.Rows.Clear();

            ViewState["NGUOIQUANTRI"] = tblResultChuTaiKhoan;
            gvResultChuTaiKhoan.DataSource = tblResultChuTaiKhoan.DefaultView.ToTable(true, "colFullName", "colPhone", "colAccount", "colRole");
            gvResultChuTaiKhoan.DataBind();

            lblAlert.Text = Resources.labels.recorddeleted;
        }
        catch
        {
        }
    }
    protected void btnThemChuTaiKhoan_Click(object sender, EventArgs e)
    {
        try
        {
            int passlenWL = 0;
            string PassTemp = "";
            lblAlert.Text = string.Empty;
            //if (userName == string.Empty || userName == null)
            //{
            //    lblAlert.Text = Resources.labels.bancannhapsodienthoainguoisudung;
            //    return;
            //}
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
            if (txtReEmail.Text.Trim() != txtReEmail.Text.Trim())
            {
                txtReEmail.Text = txtReEmail.Text.Trim();
            }
            DataSet dspolicy = new DataSet();
            string filterWL = "serviceid='MB' and policyid='" + ddlpolicyWL.SelectedValue.ToString() + "'";
            string stSort = "serviceid asc";
            dspolicy = new SmartPortal.SEMS.USERPOLICY().GetPolicybyCondition(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, Session["userName"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                DataTable dtWL = dspolicy.Tables[0].Select(filterWL, stSort).CopyToDataTable();
                passlenWL = Convert.ToInt32(dtWL.Rows[0]["minpwdlen"].ToString());

            }
            string passreveal = DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - passlenWL, passlenWL);
            string pwdreset = Encryption.Encrypt(passreveal);
            //PassTemp = SmartPortal.SEMS.O9Encryptpass.sha_sha256(pwdreset, userName);
            PassTemp = SmartPortal.SEMS.O9Encryptpass.sha_sha256(passreveal, userName);

            LuuThongTinQuyen("NGUOIQUANTRI", gvResultChuTaiKhoan, tvWL, txtReFullName.Text, "", txtReBirth.Text, ddlReGender.SelectedValue, txtReMobi.Text, txtReEmail.Text, txtReAddress.Text, userName, PassTemp, "", "", "", "", "", "", PassTemp, DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - 8, 6), "", PassTemp, "", txtWLPhoneNo.Text.Trim(), pwdreset, DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - 8, 6), txtWLPhoneNo.Text.Trim(), PassTemp);

            if (gvResultChuTaiKhoan.Rows.Count == 0)
            {
                lblAlert.Text = Resources.labels.banchuadangkydichvu;
                return;
            }
        }
        catch
        {
        }

    }
    protected void btnCustSave_Click(object sender, EventArgs e)
    {
        string loginMethod = string.Empty;
        string authenType = string.Empty;
        string AccountWallet = string.Empty;
        if (ACTION == SmartPortal.Constant.IPC.EDIT || ACTION == SmartPortal.Constant.IPC.VIEWDETAIL)
        {
            AccountWallet = new Customer().GetAccountWalletID(contractNo, ref IPCERRORCODE, ref IPCERRORDESC);
        }
        else
        {
            Guid id = Guid.NewGuid();
            AccountWallet = id.ToString().ToUpper();
        }
        if (AccountWallet.Equals(""))
            return;
        if (ViewState["NGUOIQUANTRI"] == null)
        {
            lblError.Text = SmartPortal.Constant.IPC.ERRORCODE.FULLINFOERROR;
        }
        if (txtReBirth.Text.Trim() != "")
        {
            if (SmartPortal.Common.Utilities.Utility.IsDateTime1(txtReBirth.Text.Trim()) > DateTime.Now)
            {
                lblError.Text = SmartPortal.Constant.IPC.ERRORCODE.BE;
            }
        }
        string lastModify = SmartPortal.Common.Utilities.Utility.KillSqlInjection(DateTime.Now.ToString("dd/MM/yyyy"));
        string userCreate = SmartPortal.Common.Utilities.Utility.KillSqlInjection(Session["userName"].ToString().Trim());
        string userLastModify = SmartPortal.Common.Utilities.Utility.KillSqlInjection(Session["userName"].ToString().Trim());
        string userApprove = "";
        string endDate = SmartPortal.Constant.IPC.DATEDEFAULT;
        string status = "";//SmartPortal.Constant.IPC.ACTIVE;

        string userID = userName;
        string userFullName = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtReFullName.Text.Trim());
        string userGender = ddlReGender.SelectedValue;
        string userAddress = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtReAddress.Text.Trim());
        string userLocalFullName = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtLocalName.Text.Trim());
        string userEmail = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtReEmail.Text.Trim());
        string localName = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtLocalName.Text.Trim());
        string userPhone = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtReMobi.Text.Trim());
        userType = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlUserType.SelectedValue.Trim());
        string userLevel = "0";
        string type = IPC.CHUTAIKHOAN;
        if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["ct"] == SmartPortal.Constant.IPC.CORPORATECONTRACT)
        {
            string[] TypeLevel = ddlUserType.SelectedValue.Split('#');
            type = SmartPortal.Common.Utilities.Utility.KillSqlInjection(TypeLevel[0].ToString().Trim());
            if (!type.Equals(IPC.CONTRACTCORPMATRIX))
                userLevel = SmartPortal.Common.Utilities.Utility.KillSqlInjection(TypeLevel[1].ToString().Trim());
        }

        userType = USERTYPE;

        string deptID = "";
        string userBirthday = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtReBirth.Text.Trim());
        if (userBirthday == "")
        {
            userBirthday = SmartPortal.Constant.IPC.DATEDEFAULT;
        }
        string uCreateDate = DateTime.Now.ToString("dd/MM/yyyy");
        string tokenID = "";
        string tokenIssueDate = "01/01/1900";
        string smsOTP = "";

        string IBUserName = string.Empty; ;
        //string IBPassword = Encryption.Encrypt(DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - 8, 6));
        string IBStatus = "";
        string SMSStatus = "";
        string MBStatus = "";
        string PHOStatus = "";

        string SMSPhoneNo = "";
        string SMSDefaultAcctno = "";


        string MBPhoneNo = userName;
        //string MBPass = Encryption.Encrypt(DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - 8, 6));


        string PHOPhoneNo = "";
        //string PHOPass = Encryption.Encrypt(DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - 8, 6));




        //get contract info
        DataTable contractInfo = new DataTable();

        contractInfo = (new SmartPortal.SEMS.Contract().GetContractByContractNo(contractNo, ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
        if (contractInfo.Rows.Count != 0)
        {
            endDate = SmartPortal.Common.Utilities.Utility.IsDateTime2(contractInfo.Rows[0]["ENDDATE"].ToString().Trim()).ToString("dd/MM/yyyy");
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
        }
        //minhadd 26/8/2015
        string emailnqt = "";
        string smsphonenqt = "";
        //26.2.2016 minh add password reset and change value of ibpass,mbpass,phopass
        string pwdreset = string.Empty;
        string pwdresetsms = string.Empty;
        foreach (DataRow r in tblNGUOIQUANTRI.Rows)
        {
            if (!string.IsNullOrEmpty(r["colSMSPinCode"] as string))
            {
                pwdresetsms = Encryption.Encrypt(r["colSMSPinCode"].ToString());
                break;
            }
        }
        //if(pwdresetsms.Equals(string.Empty))
        //{
        //    ShowPopUpMsg("pass reset sms null");
        //    return;

        //}

        try
        {
            pwdreset = tblNGUOIQUANTRI.Rows[0]["colpwdreset"].ToString();
            string IBPassword = tblNGUOIQUANTRI.Rows[0]["colIBPass"].ToString();
            string MBPass = tblNGUOIQUANTRI.Rows[0]["colMBPass"].ToString();
            string PHOPass = tblNGUOIQUANTRI.Rows[0]["colPHOPass"].ToString();
            string WLPass = tblNGUOIQUANTRI.Rows[0]["colWLPass"].ToString();
            loginMethod = tblNGUOIQUANTRI.Rows[0]["colLoginMethod"].ToString();
            authenType = tblNGUOIQUANTRI.Rows[0]["colAuthenType"].ToString();

            emailnqt = tblNGUOIQUANTRI.Rows[0]["colEmail"].ToString();
        }
        catch
        {

        }

        //string smsphonenuq = "";
        foreach (DataRow r in tblNGUOIQUANTRI.Rows)
        {
            if (r["colServiceID"].ToString() == SmartPortal.Constant.IPC.SMS)
            {
                smsphonenqt = r["colSMSPhone"].ToString();
                break;
            }
        }

        //if (emailnqt == string.Empty && smsphonenqt == string.Empty)
        //{

        //    lblAlert.Text = Resources.labels.nguoiquantriphaidangkyitnhatemailhoacsmsservice;
        //    return;
        //}

        #region Tạo 1 bảng tổng hợp 3 user trên
        DataTable tblSUM = new DataTable();

        DataColumn colFullName = new DataColumn("colFullName");
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
        DataColumn colWLPhone = new DataColumn("colWLPhone");
        DataColumn colWLPass = new DataColumn("colWLPass");
        DataColumn colWLPinCode = new DataColumn("colWLPinCode");
        DataColumn colPHOPhone = new DataColumn("colPHOPhone");
        DataColumn colPHOPass = new DataColumn("colPHOPass");
        DataColumn colPHODefaultAcctno = new DataColumn("colPHODefaultAcctno");
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
        tblSUM.Columns.Add(colWLPhone);
        tblSUM.Columns.Add(colWLPass);
        tblSUM.Columns.Add(colWLPinCode);
        tblSUM.Columns.Add(colPHOPhone);
        tblSUM.Columns.Add(colPHOPass);
        tblSUM.Columns.Add(colPHODefaultAcctno);
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
        foreach (DataRow dongCTK in tblNGUOIQUANTRI.Rows)
        {
            DataRow rowNguoiUyQuyen = tblSUM.NewRow();
            rowNguoiUyQuyen["colFullName"] = dongCTK["colFullName"].ToString();
            rowNguoiUyQuyen["colLevel"] = dongCTK["colLevel"].ToString();
            rowNguoiUyQuyen["colBirthday"] = dongCTK["colBirthday"].ToString();
            rowNguoiUyQuyen["colGender"] = dongCTK["colGender"].ToString();
            rowNguoiUyQuyen["colPhone"] = dongCTK["colPhone"].ToString();
            rowNguoiUyQuyen["colEmail"] = dongCTK["colEmail"].ToString();
            rowNguoiUyQuyen["colAddress"] = dongCTK["colAddress"].ToString();
            rowNguoiUyQuyen["colUT"] = SmartPortal.Constant.IPC.PCO;
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
            rowNguoiUyQuyen["colWLPhone"] = dongCTK["colIBUserName"].ToString();
            rowNguoiUyQuyen["colWLPass"] = dongCTK["colWLPass"].ToString();
            rowNguoiUyQuyen["colWlPinCode"] = dongCTK["colWLPinCode"].ToString();
            rowNguoiUyQuyen["colPHOPhone"] = dongCTK["colPHOPhone"].ToString();
            rowNguoiUyQuyen["colPHOPass"] = dongCTK["colPHOPass"].ToString();
            rowNguoiUyQuyen["colPHODefaultAcctno"] = dongCTK["colPHODefaultAcctno"].ToString();
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

        #endregion

        #region Tạo bảng chứa quyền user WL
        //tao bang chua thong tin customer

        DataTable tblWLUserRight = new DataTable();
        DataColumn colWLPhoneNoR = new DataColumn("colWLPhoneNo");
        DataColumn colWLRoleID = new DataColumn("colWLRoleID");
        DataColumn colWLRoleContracType = new DataColumn("colWLRoleContracType");

        //add vào table
        tblWLUserRight.Columns.Add(colWLPhoneNoR);
        tblWLUserRight.Columns.Add(colWLRoleID);
        tblWLUserRight.Columns.Add(colWLRoleContracType);

        //tao 1 dong du lieu
        DataRow[] arrWLR = tblSUM.Select("colServiceID='" + SmartPortal.Constant.IPC.MB + "'");
        foreach (DataRow rWLR in arrWLR)
        {
            if (tblWLUserRight.Select("colWLPhoneNo='" + rWLR["colWLPhone"].ToString() + "' and colWLRoleID='" + rWLR["colRoleID"].ToString() + "'").Length == 0)
            {
                DataRow newRowWLR = tblWLUserRight.NewRow();

                newRowWLR["colWLPhoneNo"] = rWLR["colWLPhone"].ToString();
                newRowWLR["colWLRoleID"] = rWLR["colRoleID"].ToString();
                newRowWLR["colWLRoleContracType"] = SmartPortal.Constant.IPC.CONTRACTINDIVIDUAL;
                tblWLUserRight.Rows.Add(newRowWLR);
            }
        }
        #endregion

        #region Tạo bảng chứa Account của Contract

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
        //add Account Wallet


        //add cung cho giao dich lay account IPC
        foreach (DataRow rCA in tblSUM.Rows)
        {
            if (tblContractAccount.Select("colAContractNo='" + contractNo + "' and colAcctNo='" + rCA["colAccount"].ToString() + "'").Length == 0)
            {
                DataRow newRowCA = tblContractAccount.NewRow();
                newRowCA["colAContractNo"] = contractNo;
                newRowCA["colAcctNo"] = AccountWallet;
                newRowCA["colAcctType"] = "EW";
                newRowCA["colCCYID"] = Resources.labels.lak;
                newRowCA["colStatus"] = "A";
                newRowCA["colBankAcctype"] = SmartPortal.Constant.IPC.EW;
                newRowCA["colBranchID"] = DropDownListBranch.SelectedValue;
                tblContractAccount.Rows.Add(newRowCA);
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
        foreach (DataRow rUA in tblSUM.Rows)
        {
            if (tblUserAccount.Select("colUserIDUC='" + rUA["colIBUserName"].ToString() + "' and colAcctNoUC='" + AccountWallet + "' and colRoleIDUC='" + rUA["colRoleID"].ToString() + "'").Length == 0)
            {
                DataRow newRowUA = tblUserAccount.NewRow();
                newRowUA["colUserIDUC"] = rUA["colIBUserName"].ToString();
                newRowUA["colAcctNoUC"] = AccountWallet;
                newRowUA["colRoleIDUC"] = rUA["colRoleID"].ToString();
                newRowUA["colUseFull"] = "N";
                newRowUA["colDesc"] = "";
                newRowUA["colIsDefault"] = "Y";
                newRowUA["colStatus"] = "Y";

                tblUserAccount.Rows.Add(newRowUA);
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
        foreach (DataRow rTR in tblSUM.Rows)
        {
            if (tblTranrightDetail.Select("colUserID='" + rTR["colIBUserName"].ToString() + "' and colAcctNoTR='" + rTR["colAccount"].ToString() + "' and colTranCode='" + rTR["colTranCodeID"].ToString() + "'").Length == 0)
            {
                if (rTR["colIBUserName"].ToString().Trim() != "")
                {
                    DataRow newRowTR = tblTranrightDetail.NewRow();
                    newRowTR["colUserID"] = rTR["colIBUserName"].ToString();
                    newRowTR["colAcctNoTR"] = AccountWallet;
                    newRowTR["colTranCode"] = rTR["colTranCodeID"].ToString();
                    newRowTR["colServiceID"] = rTR["colServiceID"].ToString();
                    newRowTR["colLimit"] = "0";

                    tblTranrightDetail.Rows.Add(newRowTR);
                }
            }
        }
        #endregion

        #region Tạo bảng UPDATE PhoneNO
        //tao bang chua thong tin customer
        DataTable tblUpdatePhoneNo = new DataTable();
        DataColumn colUserIDUD = new DataColumn("colUserIDUD");
        DataColumn colPhoneNoUP = new DataColumn("colPhoneNoUP");
        DataColumn colCONTRACTTYPEUP = new DataColumn("colCONTRACTTYPEUP");

        //add vào table
        tblUpdatePhoneNo.Columns.Add(colUserIDUD);
        tblUpdatePhoneNo.Columns.Add(colPhoneNoUP);
        tblUpdatePhoneNo.Columns.Add(colCONTRACTTYPEUP);

        //tao 1 dong du lieu
        DataRow neR = tblUpdatePhoneNo.NewRow();

        neR["colUserIDUD"] = USERTK;
        neR["colPhoneNoUP"] = txtWLPhoneNo.Text;
        neR["colCONTRACTTYPEUP"] = CONTRACTTYPE;


        tblUpdatePhoneNo.Rows.Add(neR);

        #endregion


        switch (ACTION)
        {
            case SmartPortal.Constant.IPC.EDIT:
                IBStatus = SmartPortal.Constant.IPC.PENDING;
                //minh add 29/5/2015 phân biệt trường hợp xóa, thêm_thay đổi                            
                SMSStatus = SmartPortal.Constant.IPC.PENDINGFORDELETE;
                MBStatus = SmartPortal.Constant.IPC.PENDING;
                PHOStatus = SmartPortal.Constant.IPC.PENDING;
                status = SmartPortal.Constant.IPC.PENDING;

                userID = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["uid"].ToString();

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
                new SmartPortal.SEMS.User().Update(contractNo, localName, USERTNAMEMB, "", "", SmartPortal.Constant.IPC.CONTRACTINDIVIDUAL, userID, userFullName, status, userGender, userAddress, userEmail, txtWLPhoneNo.Text.Trim(), userCreate, userType, userLevel, type, deptID, tokenID, smsOTP, userBirthday, IBUserName, "", IBStatus, lastModify, SMSPhoneNo, SMSDefaultAcctno, "N", SMSStatus, "ENG", txtWLPhoneNo.Text.Trim(), "", MBStatus, PHOPhoneNo, "", PHOStatus, "", new DataTable(), new DataTable(), tblWLUserRight, new DataTable(), new DataTable(), new DataTable(), tblUserAccount, tblContractAccount, tblUpdatePhoneNo, "", "", ddlpolicyWL.SelectedValue.ToString(), pwdresetsms, new DataTable(), ref IPCERRORCODE, ref IPCERRORDESC);

                if (IPCERRORCODE == "0")
                {
                    txtWLPhoneNo.Enabled = false;
                    ViewState["NGUOIQUANTRI"] = null;
                    lblError.Text = Resources.labels.saveusersuccessfully;
                    btnCustSave.Visible = false;
                    hideAll();
                    string text = "";
                    if (birthdayOld != txtReBirth.Text.Trim())
                    {
                        text = birthdayOld == "" ? text = "Add" : "Change";
                        SmartPortal.Common.Log.WriteLogDatabase(contractNo, userID, Request.Url.ToString(), Session["userName"].ToString(),
                             Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLCONTRACT"],
                   text + " Birthday", birthdayOld, txtReBirth.Text.Trim(), SmartPortal.Constant.IPC.PENDING);
                        birthdayOld = null;
                    }
                    if (emailOld != userEmail)
                    {
                        text = emailOld == "" ? text = "Add" : "Change";
                        SmartPortal.Common.Log.WriteLogDatabase(contractNo, userID, Request.Url.ToString(), Session["userName"].ToString(),
                       Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLCONTRACT"],
                       text + " Email", emailOld, userEmail, SmartPortal.Constant.IPC.PENDING);
                        emailOld = null;
                    }
                    if (localFullName != userLocalFullName)
                    {
                        text = localFullName == "" ? text = "Add" : "Change";
                        SmartPortal.Common.Log.WriteLogDatabase(contractNo, userID, Request.Url.ToString(), Session["userName"].ToString(),
                       Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLCONTRACT"],
                       text + " Local Full Name", localFullName, userLocalFullName, SmartPortal.Constant.IPC.PENDING);
                        localFullName = null;
                    }
                    if (genderOld != userGender)
                    {
                        text = genderOld == "" ? text = "Add" : "Change";
                        SmartPortal.Common.Log.WriteLogDatabase(contractNo, userID, Request.Url.ToString(), Session["userName"].ToString(),
                          Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLCONTRACT"],
                        text + " Gender", genderOld == "M" ? Resources.labels.nam : Resources.labels.nu, userGender == "M" ? Resources.labels.nam : Resources.labels.nu, SmartPortal.Constant.IPC.PENDING);
                        genderOld = null;
                    }
                    if (phoneOld != userPhone)
                    {
                        text = phoneOld == "" ? text = "Add" : "Change";
                        SmartPortal.Common.Log.WriteLogDatabase(contractNo, userID, Request.Url.ToString(), Session["userName"].ToString(),
                      Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLCONTRACT"],
                      text + " Phone", phoneOld, userPhone, SmartPortal.Constant.IPC.PENDING);
                        phoneOld = null;
                    }
                    if (addressOld != userAddress)
                    {
                        text = addressOld == "" ? text = "Add" : "Change";
                        SmartPortal.Common.Log.WriteLogDatabase(contractNo, userID, Request.Url.ToString(), Session["userName"].ToString(),
                     Request.UserHostAddress, System.Configuration.ConfigurationManager.AppSettings["TBLCONTRACT"],
                     text + " Address", addressOld, userAddress, SmartPortal.Constant.IPC.PENDING);
                        addressOld = null;
                    }

                    //  acount tblUserAccount
                    try
                    {
                        DataTable tblUserAccountold = new DataTable();
                        DataColumn UserID = new DataColumn("colIBUserName");
                        DataColumn Account = new DataColumn("colAccount");
                        tblUserAccountold.Columns.Add(UserID);
                        tblUserAccountold.Columns.Add(Account);
                        if (ViewState["UserRolesOld"] != null)
                            tblUserAccountold = (DataTable)ViewState["UserRolesOld"];
                        text = "Add";
                        DataTable tblUserAcct = tblUserAccount.DefaultView.ToTable(true, "colUserIDUC", "colAcctNoUC");
                        foreach (DataRow dr in tblUserAcct.Rows)
                        {
                            try
                            {
                                if (tblUserAccountold.Select("colAccount = '" + dr["colAcctNoUC"].ToString() + "' and colIBUserName = '" + dr["colUserIDUC"].ToString() + "'").Length == 0)
                                {
                                    SmartPortal.Common.Log.WriteLogDatabase(contractNo, dr["colUserIDUC"].ToString(), Request.Url.ToString(), Session["userName"].ToString(),
                             Request.UserHostAddress, "EBA_CONTRACTACCOUNT",
                             text + " Account", "", dr["colAcctNoUC"].ToString(), SmartPortal.Constant.IPC.PENDING);
                                }
                            }
                            catch { }
                        }
                    }
                    catch { }
                    //bool isUseMB = (bool)ViewState["IsUseMB"];
                    //if (tblMBUserRight.Rows.Count > 0 && !isUseMB)
                    //{
                    //    text = "Add";
                    //    try
                    //    {
                    //        SmartPortal.Common.Log.WriteLogDatabase(contractNo, userID, Request.Url.ToString(), Session["userName"].ToString(),
                    //        Request.UserHostAddress, "MB_USER", text + " Link Moblie", "", "MB", SmartPortal.Constant.IPC.PENDING);
                    //    }
                    //    catch { }
                    //    ViewState["IsUseMB"] = null;
                    //}
                }
                else
                {
                    lblError.Text = "Insert User Unsuccessfully";                  
                    SmartPortal.Common.Log.RaiseError("Update User Fail:" + userID, "Widgets_SEMSUser_Widget", "btnCustSave_Click", IPCERRORDESC, Request.Url.Query);
                    return;
                }
                break;
        }

    }
    protected void btnBack_Click(object sender, EventArgs e)
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

    protected void gvResultChuTaiKhoan_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvResultChuTaiKhoan.PageIndex = e.NewPageIndex;
            gvResultChuTaiKhoan.DataSource = (DataTable)ViewState["NGUOIQUANTRI"];
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
            DataTable tblResultChuTaiKhoan = (DataTable)ViewState["NGUOIQUANTRI"];

            tblResultChuTaiKhoan.Rows.RemoveAt(e.RowIndex + (gvResultChuTaiKhoan.PageIndex * gvResultChuTaiKhoan.PageSize));

            ViewState["NGUOIQUANTRI"] = tblResultChuTaiKhoan;
            gvResultChuTaiKhoan.DataSource = tblResultChuTaiKhoan;
            gvResultChuTaiKhoan.DataBind();

            lblAlert.Text = Resources.labels.recorddeleted;
        }
        catch
        {
        }
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
    private void LoadComboboxReason()
    {
        try
        {
            DataTable dtReason = new SmartPortal.SEMS.Contract().GetReason("", "", "", "IND", "CU", ref IPCERRORCODE, ref IPCERRORDESC).Tables[0];
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
        btnCustSave.Visible = false;
        btnHuy.Visible = false;
        tvWL.Enabled = false;
        btnThemChuTaiKhoan.Visible = false;
        txtLocalName.Enabled = false;
        ddlpolicyWL.Enabled = false;
    }

    protected void ChangePhone_Click(object sender, EventArgs e)
    {
        txtWLPhoneNo.Enabled = true;
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
    protected void ChangePhoneAndUserName_Click(object sender, EventArgs e)
    {
        bool allowSave = true;
        if (!CheckIsPhoneNumer(txtWLPhoneNo.Text.Trim().ToString()) || txtWLPhoneNo.Text.Trim().ToString().Equals(""))
        {
            lblError.Text = Resources.labels.phonenumberwrong;
            allowSave = false;
        }
        else if (!CheckExistPhoneNumber(txtWLPhoneNo.Text.Trim().ToString()))
        {
            lblError.Text = Resources.labels.phonenumberisalreadyregistered;
            allowSave = false;
        }
        else
        {
            string result = string.Empty;
            result = new User().CheckPhoneAndUserName(USERTK, SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtWLPhoneNo.Text.Trim()), "PHONENO",
                        CONTRACTTYPE, ref IPCERRORCODE, ref IPCERRORDESC);
            if (!result.Equals("0"))
            {
                lblError.Text = Resources.labels.phonenumberisalreadyregistered;
                allowSave = false;
            }
            
        }
        if (!allowSave)
        {
            
            btnCustSave.Visible = false;
        }
        else
        {
            ReloadDropdownlistbyphonechaning(txtWLPhoneNo.Text.Trim().ToString());
            UpdatemobileService(txtWLPhoneNo.Text.Trim() .ToString());
            txtReMobi.Text = txtWLPhoneNo.Text ;                      
            btnCustSave.Visible = true;
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
                dr["colWLPhone"] = phoneNo;               
                dr["colAccount"] = phoneNo;
               
            }
            ViewState["NGUOIQUANTRI"] = tblNGUOIQUANTRI;
            gvResultChuTaiKhoan.DataSource = tblNGUOIQUANTRI.DefaultView.ToTable(true, "colFullName", "colPhone", "colAccount", "colRole");
            gvResultChuTaiKhoan.DataBind();
        }

    }
    private void ReloadDropdownlistbyphonechaning(string phoneno)
    {
        int index = ddlDefaultAccountQT.Items.IndexOf(ddlDefaultAccountQT.Items.FindByValue(txtReMobi.Text));
        if (index != -1)
        {
            ddlDefaultAccountQT.Items.RemoveAt(index);
            //var selectedItem = ddlDefaultAccountQT.SelectedItem; //returns a ListItem object
            // selectedItem.Text = phoneno;
            ddlDefaultAccountQT.Items.Insert(index, new ListItem(phoneno, phoneno));
            ddlDefaultAccountQT.DataBind();  //Rebind it so you see the change.
        }

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
}