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

using SmartPortal.Security;
using System.Text;
using SmartPortal.ExceptionCollection;
using SmartPortal.SEMS;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Globalization;
using SmartPortal.Common.Utilities;

public partial class Widgets_SEMSContractList_AddWallet_Widget : WidgetBase
{

    private string IPCERRORDESC = "";
    private string IPCERRORCODE = "";
    private string userName;
    private int passlenIB;
    internal int minlength = int.Parse(ConfigurationManager.AppSettings["minlengthloginname"].ToString());
    internal int maxlength = int.Parse(ConfigurationManager.AppSettings["maxlengthloginname"].ToString());
    protected void Page_Init(object sender, EventArgs e)
    {
        TabCustomerInfoHelper.LoadConfig();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            lblError.Text = string.Empty;
            if (!IsPostBack)
            {
                pnPersonal.Visible = false;
                ddlCustType.Items.Add(new ListItem(Resources.labels.canhan, "P"));
                ddlCustType.Items.Add(new ListItem(Resources.labels.linkage, "J"));
                ddlCustType.Items.Add(new ListItem(Resources.labels.doanhnghiep, "O"));
                ddlGender.Items.Add(new ListItem(Resources.labels.male, "M"));
                ddlGender.Items.Add(new ListItem(Resources.labels.female, "F"));
                ddlContractType.Items.Add(new ListItem(Resources.labels.canhan, "PCO"));
                ddlContractType.Items.Add(new ListItem(Resources.labels.doanhnghiep, "CCO"));
                ddlProduct.Items.Add(new ListItem(Resources.labels.ebankingcanhan, "PCN"));
                ddlProduct.Items.Add(new ListItem(Resources.labels.ebankingdoanhnghiep, "PDN"));
                ddlReGender.Items.Add(new ListItem(Resources.labels.male, "M"));
                ddlReGender.Items.Add(new ListItem(Resources.labels.female, "F"));

                #region load usertype
                DataSet dsUserType = new DataSet();
                DataTable dtUserType = new DataTable();

                dtUserType = new SmartPortal.SEMS.Contract().LoadContractType(SmartPortal.Constant.IPC.PERSONAL, "Y");

                ddlContractType.DataSource = dtUserType;
                ddlContractType.DataTextField = "TYPENAME";
                ddlContractType.DataValueField = "USERTYPE";
                ddlContractType.DataBind();

                #endregion
                #region load nation
                DataSet dsNation = new DataSet();

                dsNation = new SmartPortal.SEMS.Nation().LoadNation(ref IPCERRORCODE, ref IPCERRORDESC);

                ddlNation.DataSource = dsNation;
                ddlNation.DataTextField = "NATIONNAME";
                ddlNation.DataValueField = "NATIONCODE";
                ddlNation.DataBind();

                if (dsNation.Tables[0].Rows.Count == 0)
                {
                    ddlNation.Items.Insert(0, new ListItem("Không tồn tại quốc gia", ""));
                }

                #endregion
                ddlProduct.DataSource = new SmartPortal.SEMS.Product().GetProductByCondition("", "", ddlCustType.SelectedValue, "", ref IPCERRORCODE, ref IPCERRORDESC).Tables[0];
                ddlProduct.DataTextField = "PRODUCTNAME";
                ddlProduct.DataValueField = "PRODUCTID";
                ddlProduct.DataBind();
                txtContractNo.Enabled = false;
                txtStartDate.Text = SmartPortal.Common.Utilities.Utility.IsDateTime1(SmartPortal.Constant.IPC.LoadWorkingDate()).ToString("dd/MM/yyyy");
                txtEndDate.Text = SmartPortal.Common.Utilities.Utility.IsDateTime1(SmartPortal.Constant.IPC.LoadWorkingDate()).AddYears(1).ToString("dd/MM/yyyy");


                //load chi nhánh
                ddlBranch.DataSource = new SmartPortal.SEMS.Branch().GetAll(ref IPCERRORCODE, ref IPCERRORDESC);

                ddlBranch.DataTextField = "BRANCHNAME";
                ddlBranch.DataValueField = "BRANCHID";
                ddlBranch.DataBind();
                ddlBranch.Enabled = true;

                DataSet dspolicy = new DataSet();
                string filterWL = "serviceid='EW'";
                string stSort = "serviceid asc";
                dspolicy = new SmartPortal.SEMS.USERPOLICY().GetPolicybyCondition(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, Session["userName"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE == "0")
                {
                    DataTable dtWL = dspolicy.Tables[0].Select(filterWL, stSort).CopyToDataTable();
                    ddlpolicyWL.DataSource = dtWL;
                    passlenIB = Convert.ToInt32(dtWL.Rows[0]["minpwdlen"].ToString());
                    ddlpolicyWL.DataTextField = "policytx";
                    ddlpolicyWL.DataValueField = "policyid";
                    ddlpolicyWL.DataBind();
                }
            }
        }
        catch
        {

        }
    }
    protected void btnSearchWL_Click(object sender, EventArgs e)
    {
        string ctmType = SmartPortal.Constant.IPC.PERSONAL;
        txtContractNo.Text = SmartPortal.Common.Utilities.Utility.GetID(SmartPortal.Constant.IPC.CONTRACTNOPREFIX, txtWLPhoneNo.Text.Trim(), ctmType, 15);
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {


        if (txtContractNo.Text.Trim() == "")
        {
            lblError.Text = Resources.labels.mahopdongkhongrong;
            return;
        }

        #region lấy thông tin khách hàng qua người dùng
        txtReFullName.Text = txtFullName.Text;
        txtReBirth.Text = txtBirth.Text;
        ddlReGender.SelectedValue = ddlGender.SelectedValue;
        string usernameInfo = txtCustCodeWL.Text;
        txtWLPhoneNo.Text = txtReMobi.Text = usernameInfo;
        txtReEmail.Text = txtEmail.Text;
        txtReAddress.Text = txtResidentAddr.Text;
        txtReFullName.Enabled = false;
        txtReBirth.Enabled = false;
        ddlReGender.Enabled = false;
        txtReMobi.Enabled = false;
        txtReEmail.Enabled = true;
        txtReAddress.Enabled = false;
        #endregion

        #region Hien thi tat cac cac role theo serviceid va usertype len cay
        LoadDataInTreeview(SmartPortal.Constant.IPC.EW, tvWL, ddlContractType.SelectedValue);
        #endregion

        #region lay role mac dinh
        GetRoleDefault(tvWL);
        #endregion
        gvResultChuTaiKhoan.DataSource = null;
        gvResultChuTaiKhoan.DataBind();
        pnCustInfo.Visible = false;
        pnPersonal.Visible = true;

    }
    private void LoadDataInTreeview(string serviceID, TreeView tvPage, string userType)
    {
        tvPage.Nodes.Clear();
        DataTable tblSS = new DataTable();
        //tblSS = new SmartPortal.SEMS.Role().GetByServiceAndUserType(serviceID, userType);
        tblSS = (new SmartPortal.SEMS.Role().GetRoleDefaultByServiceID(serviceID, ddlProduct.SelectedValue, 0,string.Empty ,ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];

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
    void GetRoleDefault(TreeView treeWL)
    {
        DataTable tblRoleDefault = new DataTable();
        //lay role mac dinh WL
        tblRoleDefault = (new SmartPortal.SEMS.Role().GetRoleDefaultByServiceID(SmartPortal.Constant.IPC.EW, ddlProduct.SelectedValue, 0, string.Empty, ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];

        foreach (TreeNode liWL in treeWL.Nodes)
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
    protected void btnCustSave_Click(object sender, EventArgs e)
    {
        string tokenID = "";
        string tokenIssueDate = "01/01/1900";
        string userType = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlContractType.SelectedValue);
        string uCreateDate = DateTime.Now.ToString("dd/MM/yyyy");
        string deptID = "";
        string smsOTP = "";
        userName = txtWLPhoneNo.Text.Trim().ToString();
        if (txtEmail.Text == "")
        {
            lblError.Text = Resources.labels.emailkhongdinhdang;
            return;
        }

        if (ViewState["CHUTAIKHOAN"] == null)
        {
            lblError.Text = Resources.labels.bancandiendayduthongtinchutaikhoan;
            return;
        }
        DataTable tblCHUTAIKHOAN = new DataTable();
        if (ViewState["CHUTAIKHOAN"] != null)
        {
            tblCHUTAIKHOAN = (DataTable)ViewState["CHUTAIKHOAN"];
            if (tblCHUTAIKHOAN.Rows.Count == 0)
            {
                lblError.Text = Resources.labels.bancandiendayduthongtinchutaikhoan;
                return;
            }
        }
        string emailctk = "";
        string smsphonectk = "";
        emailctk = tblCHUTAIKHOAN.Rows[0]["colEmail"].ToString();



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
        foreach (DataRow dongCTK in tblCHUTAIKHOAN.Rows)
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
            rowNguoiUyQuyen["colWLPhone"] = dongCTK["colWLPhone"].ToString();
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

        #region Biến thông tin Customer và Contract
        string custID = SmartPortal.Common.Utilities.Utility.GetID(SmartPortal.Constant.IPC.CUSNOPREFIX, txtWLPhoneNo.Text.Trim(), SmartPortal.Constant.IPC.CUSINDIVIDUALTYPEPREFIX, 15);
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
        string Tel = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtCustCodeWL.Text.Trim());
        string FAX = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtFax.Text.Trim());
        string Email = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtEmail.Text.Trim());
        string LicenseType = "";
        string LicenseID = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtIF.Text.Trim());
        string issueDate = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtIssueDate.Text.Trim());
        if (issueDate == "")
        {
            issueDate = SmartPortal.Constant.IPC.DATEDEFAULT;
        }
        string issuePlace = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtIssuePlace.Text.Trim());
        string desc = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtDesc.Text.Trim());
        string job = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtJob.Text.Trim());
        string officeAddr = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtOfficeAddr.Text.Trim());
        string officePhone = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtCompanyPhone.Text.Trim());
        string custType = string.Empty;
        string cfCode = string.Empty;
        string branchID = Session["branch"].ToString();
        string custBranchID = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlBranch.SelectedValue);

        string contractNo = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtContractNo.Text.Trim());
        string contractType = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlContractType.SelectedValue.Trim());
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
        #endregion

        #region Tạo bảng chứa thông tin user
        DataTable tblUser = new DataTable();
        DataColumn colUserID = new DataColumn("colUserID");
        DataColumn colUContractNo = new DataColumn("colUContractNo");
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

        //add vào table
        tblUser.Columns.Add(colUserID);
        tblUser.Columns.Add(colUContractNo);
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

        //tao 1 dong du lieu
        string UID = "";
        for (int i = 0; i < tblSUM.Rows.Count; i++)
        {
            if (tblSUM.Rows[i]["colIBUserName"].ToString().Trim() != UID.Trim())
            {

                DataRow row2 = tblUser.NewRow();

                row2["colUserID"] = txtWLPhoneNo.Text.Trim().ToString();
                row2["colUContractNo"] = contractNo;
                row2["colUFullName"] = fullName;
                row2["colUGender"] = SEX;
                row2["colUAddress"] = txtReAddress.Text.Trim().ToString();
                row2["colUEmail"] = Email;
                row2["colUPhone"] = txtWLPhoneNo.Text.Trim().ToString();
                row2["colUStatus"] = status;
                row2["colUUserCreate"] = userCreate;
                row2["colUDateCreate"] = uCreateDate;
                row2["colUUserModify"] = userCreate;
                row2["colULastModify"] = lastModify;
                row2["colUUserApprove"] = userApprove;
                row2["colUserType"] = userType;
                row2["colUserLevel"] = lblLevel.Text.Trim().ToString();
                row2["colDeptID"] = deptID;
                row2["colTokenID"] = tokenID;
                row2["colTokenIssueDate"] = tokenIssueDate;
                row2["colSMSOTP"] = smsOTP;
                row2["colSMSBirthday"] = DOB;
                row2["colTypeID"] = SmartPortal.Constant.IPC.CHUTAIKHOAN;
                tblUser.Rows.Add(row2);
                UID = tblSUM.Rows[i]["colIBUserName"].ToString().Trim();
            }
        }
        #endregion

        #region Tạo bảng chứa User Wallet
        DataTable tblWLUser = new DataTable();
        DataColumn colWLUserID = new DataColumn("colWLUserID");
        DataColumn colWLPhoneNo = new DataColumn("colWLPhoneNo");
        DataColumn colWLPassU = new DataColumn("colWLPass");
        DataColumn colWLStatus = new DataColumn("colWLStatus");
        DataColumn colWLPinCode1 = new DataColumn("colWLPinCode1");
        DataColumn colWLPolicyusr = new DataColumn("colWLPolicyusr");
        DataColumn colpwdresetWL = new DataColumn("colpwdresetWL");

        //add vào table
        tblWLUser.Columns.Add(colWLUserID);
        tblWLUser.Columns.Add(colWLPhoneNo);
        tblWLUser.Columns.Add(colWLPassU);
        tblWLUser.Columns.Add(colWLStatus);
        tblWLUser.Columns.Add(colWLPinCode1);
        tblWLUser.Columns.Add(colWLPolicyusr);
        tblWLUser.Columns.Add(colpwdresetWL);

        //tao 1 dong du lieu
        string WLP = "";
        foreach (DataRow rWLP in tblSUM.Rows)
        {
            if (rWLP["colWLPhone"].ToString().Trim() != WLP && rWLP["colWLPhone"].ToString().Trim() != "")
            {
                DataRow row1 = tblWLUser.NewRow();
                row1["colWLUserID"] = rWLP["colIBUserName"].ToString();
                row1["colWLPhoneNo"] = rWLP["colWLPhone"].ToString();
                row1["colWLPass"] = rWLP["colWLPass"].ToString();
                row1["colWLStatus"] = status;
                row1["colWLPinCode1"] = rWLP["colWLPinCode"].ToString();
                row1["colWLPolicyusr"] = rWLP["colWLPolicy"].ToString();
                row1["colpwdresetWL"] = rWLP["colpwdreset"].ToString();

                tblWLUser.Rows.Add(row1);

                WLP = rWLP["colWLPhone"].ToString();
            }
        }
        #endregion

        #region Tạo bảng chứa quyền user WL
        //tao bang chua thong tin customer

        DataTable tblWLUserRight = new DataTable();
        DataColumn colWLPhoneNoR = new DataColumn("colWLPhoneNo");
        DataColumn colWLRoleID = new DataColumn("colWLRoleID");

        //add vào table
        tblWLUserRight.Columns.Add(colWLPhoneNoR);
        tblWLUserRight.Columns.Add(colWLRoleID);

        //tao 1 dong du lieu
        DataRow[] arrWLR = tblSUM.Select("colServiceID='" + SmartPortal.Constant.IPC.EW + "'");
        foreach (DataRow rWLR in arrWLR)
        {
            if (tblWLUserRight.Select("colWLPhoneNo='" + rWLR["colWLPhone"].ToString() + "' and colWLRoleID='" + rWLR["colRoleID"].ToString() + "'").Length == 0)
            {
                DataRow newRowWLR = tblWLUserRight.NewRow();

                newRowWLR["colWLPhoneNo"] = rWLR["colWLPhone"].ToString();
                newRowWLR["colWLRoleID"] = rWLR["colRoleID"].ToString();
                tblWLUserRight.Rows.Add(newRowWLR);
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

        //tao bang chua thong tin account
        DataTable tblContractAccount = new DataTable();
        DataColumn colAContractNo = new DataColumn("colAContractNo");
        DataColumn colAcctNo = new DataColumn("colAcctNo");
        DataColumn colAcctType = new DataColumn("colAcctType");
        DataColumn colCCYID = new DataColumn("colCCYID");
        DataColumn colStatus = new DataColumn("colStatus");
        DataColumn colBranchID = new DataColumn("colBranchID");

        //add vào table
        tblContractAccount.Columns.Add(colAContractNo);
        tblContractAccount.Columns.Add(colAcctNo);
        tblContractAccount.Columns.Add(colAcctType);
        tblContractAccount.Columns.Add(colCCYID);
        tblContractAccount.Columns.Add(colStatus);
        tblContractAccount.Columns.Add(colBranchID);
        //add Account Wallet


        //add cung cho giao dich lay account IPC
        foreach (DataRow rCA in tblSUM.Rows)
        {
            if (tblContractAccount.Select("colAContractNo='" + contractNo + "' and colAcctNo='" + rCA["colAccount"].ToString() + "'").Length == 0)
            {
                DataRow newRowCA = tblContractAccount.NewRow();
                newRowCA["colAContractNo"] = contractNo;
                newRowCA["colAcctNo"] = rCA["colAccount"].ToString();
                newRowCA["colAcctType"] = "EW";
                newRowCA["colCCYID"] = Resources.labels.lak;
                newRowCA["colStatus"] = "A";
                newRowCA["colBranchID"] = ddlBranch.SelectedValue;
                //DataRow[] ro = tblAccount.Select("ACCOUNTNO='" + rCA["colAccount"].ToString().Trim() + "'");
                //if (ro.Length != 0)
                //{
                //    newRowCA["colAcctType"] = ro[0]["ACCOUNTTYPE"].ToString();
                //    newRowCA["colCCYID"] = ro[0]["CCYID"].ToString();
                //    newRowCA["colStatus"] = ro[0]["STATUS"].ToString();
                //    newRowCA["colBranchID"] = SmartPortal.Common.Utilities.Utility.FormatStringCore(ro[0]["BRANCHID"].ToString());
                //}
                //else
                //{
                //    newRowCA["colAcctType"] = "DD";
                //    newRowCA["colCCYID"] = Resources.labels.mmk;
                //    newRowCA["colStatus"] = "A";
                //}

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

        //add vào table
        tblUserAccount.Columns.Add(colUserIDUC);
        tblUserAccount.Columns.Add(colAcctNoUC);
        tblUserAccount.Columns.Add(colRoleIDUC);
        tblUserAccount.Columns.Add(colUseFull);
        tblUserAccount.Columns.Add(colDesc);

        //add cung cho giao dich lay account IPC
        foreach (DataRow rUA in tblSUM.Rows)
        {
            if (tblUserAccount.Select("colUserIDUC='" + rUA["colIBUserName"].ToString() + "' and colAcctNoUC='" + rUA["colAccount"].ToString() + "' and colRoleIDUC='" + rUA["colRoleID"].ToString() + "'").Length == 0)
            {
                DataRow newRowUA = tblUserAccount.NewRow();
                newRowUA["colUserIDUC"] = rUA["colIBUserName"].ToString();
                newRowUA["colAcctNoUC"] = rUA["colAccount"].ToString();
                newRowUA["colRoleIDUC"] = rUA["colRoleID"].ToString();
                newRowUA["colUseFull"] = "N";
                newRowUA["colDesc"] = "";

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
                    newRowTR["colAcctNoTR"] = rTR["colAccount"].ToString();
                    newRowTR["colTranCode"] = rTR["colTranCodeID"].ToString();
                    newRowTR["colServiceID"] = rTR["colServiceID"].ToString();
                    newRowTR["colLimit"] = "0";

                    tblTranrightDetail.Rows.Add(newRowTR);
                }
            }
        }
        #endregion
/*        new SmartPortal.SEMS.Customer().InsertCorp(custID, txtWLPhoneNo.Text.Trim(), fullName, shortName, DOB, re_addr, temp_addr, SEX, Nation, Tel, FAX, Email, LicenseType, LicenseID, issueDate, issuePlace, desc, job, officeAddr, officePhone, custType, cfCode,"W", branchID, custBranchID, contractNo, contractType, productID, startDate, endDate, lastModify, userCreate, userLastModify, userApprove, status, allAcct, isSpecialMan, (chkRenew.Checked ? "Y" : "N"), SmartPortal.Constant.IPC.CONTRACTINDIVIDUAL, tblUser, new DataTable(), new DataTable(), tblWLUser, new DataTable(), new DataTable(), new DataTable(), tblWLUserRight, new DataTable(), tblContractRoleDetail, tblContractAccount, tblTranrightDetail, tblUserAccount, null, new DataTable(), ref IPCERRORCODE, ref IPCERRORDESC)*/;

        if (IPCERRORCODE != "0")
        {
            throw new IPCException(IPCERRORDESC);
        }
        else
        {
            SendInfoLogin();
            ReleaseSession();
        }
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=183&returnURL=" + SmartPortal.Common.Encrypt.EncryptData(Request.Url.Query)));
    }
    void SendInfoLogin()
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
                dsUserType = new SmartPortal.SEMS.Services().GetAllUserType(contractTable.Rows[0]["USERTYPE"].ToString().Trim(), "", ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE == "0")
                {
                    DataTable dtUserType = new DataTable();
                    dtUserType = dsUserType.Tables[0];

                    if (dtUserType.Rows.Count != 0)
                    {
                        tmpl.SetAttribute("CONTRACTTYPE", dtUserType.Rows[0]["TYPENAME"].ToString());
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
            DataTable userTable = (new SmartPortal.SEMS.Contract().GetUserByContractNo(hpcontractNo, SmartPortal.Constant.IPC.PCO, "", ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
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
                DataSet accountSMSDataset = new SmartPortal.SEMS.User().GetUserRoleByServiceID(SmartPortal.Constant.IPC.SMS, row["USERID"].ToString().Trim(), string.Empty,ref IPCERRORCODE, ref IPCERRORDESC);

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

                //lay het các tai khoan MB cua user theo userID
                DataSet accountMBDataset = new SmartPortal.SEMS.User().GetUserRoleByServiceID(SmartPortal.Constant.IPC.MB, row["USERID"].ToString().Trim(), string.Empty,ref IPCERRORCODE, ref IPCERRORDESC);

                if (IPCERRORCODE != "0")
                {
                    goto ERROR;
                }

                DataTable accountMBTable = accountMBDataset.Tables[0];
                if (accountMBTable.Rows.Count != 0)
                {
                    if (accountMBTable.Rows[0]["ROLEID"].ToString().Trim() != "")
                    {
                        st.Append("<tr>");
                        st.Append("<td colspan='4'>");
                        st.Append("<br/>");
                        st.Append("</td>");
                        st.Append("</tr>");

                        st.Append("<tr>");
                        st.Append("<td colspan='4'>");
                        st.Append("<B>Mobile Banking</B>");
                        st.Append("</td>");
                        st.Append("</tr>");

                        st.Append("<tr>");
                        st.Append("<td width='25%'>");
                        st.Append(Resources.labels.username + " :");
                        st.Append("</td>");
                        st.Append("<td width='25%'>");
                        st.Append(accountMBTable.Rows[0]["UN"].ToString());
                        st.Append("</td>");
                        st.Append("<td width='25%'>");
                        st.Append(Resources.labels.password + " :");
                        st.Append("</td>");
                        st.Append("<td width='25%'>");
                        st.Append("########");
                        //st.Append("<b>" + SmartPortal.Security.Encryption.Decrypt(accountMBTable.Rows[0]["PASS"].ToString()) + "</b>");
                        st.Append("</td>");
                        st.Append("</tr>");

                    }
                }

                //lay het các tai khoan PHO cua user theo userID
                DataSet accountPHODataset = new SmartPortal.SEMS.User().GetUserRoleByServiceID(SmartPortal.Constant.IPC.PHO, row["USERID"].ToString().Trim(), string.Empty,ref IPCERRORCODE, ref IPCERRORDESC);

                if (IPCERRORCODE != "0")
                {
                    goto ERROR;
                }

                DataTable accountPHOTable = accountPHODataset.Tables[0];
                if (accountPHOTable.Rows.Count != 0)
                {
                    if (accountPHOTable.Rows[0]["ROLEID"].ToString().Trim() != "")
                    {
                        st.Append("<tr>");
                        st.Append("<td colspan='4'>");
                        st.Append("<br/>");
                        st.Append("</td>");
                        st.Append("</tr>");

                        st.Append("<tr>");
                        st.Append("<td colspan='4'>");
                        st.Append("<B>Phone Banking</B>");
                        st.Append("</td>");
                        st.Append("</tr>");

                        st.Append("<tr>");
                        st.Append("<td width='25%'>");
                        st.Append(Resources.labels.username + " :");
                        st.Append("</td>");
                        st.Append("<td width='25%'>");
                        st.Append(accountPHOTable.Rows[0]["UN"].ToString());
                        st.Append("</td>");
                        st.Append("<td width='25%'>");
                        st.Append(Resources.labels.password + " :");
                        st.Append("</td>");
                        st.Append("<td width='25%'>");
                        st.Append("########");
                        //st.Append("<b>" + SmartPortal.Security.Encryption.Decrypt(accountPHOTable.Rows[0]["PASS"].ToString()) + "</b>");
                        st.Append("</td>");
                        st.Append("</tr>");
                    }
                }

                st.Append("</table>");
                //i += 1;
                //if (i == userTable.Rows.Count)
                //{
                //    st.Append("<hr/>");
                //}
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
                        DataSet accountIBDatasetNUY = new SmartPortal.SEMS.User().GetUserRoleByServiceID(SmartPortal.Constant.IPC.IB, row["USERID"].ToString().Trim(), string.Empty,ref IPCERRORCODE, ref IPCERRORDESC);

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
                        DataSet accountSMSDatasetNUY = new SmartPortal.SEMS.User().GetUserRoleByServiceID(SmartPortal.Constant.IPC.SMS, row["USERID"].ToString().Trim(), string.Empty,ref IPCERRORCODE, ref IPCERRORDESC);

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
                        DataSet accountMBDatasetNUY = new SmartPortal.SEMS.User().GetUserRoleByServiceID(SmartPortal.Constant.IPC.MB, row["USERID"].ToString().Trim(), string.Empty, ref IPCERRORCODE, ref IPCERRORDESC);

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

                            }
                        }

                        //lay het các tai khoan WL cua user theo userID
                        DataSet accountWLDatasetNUY = new SmartPortal.SEMS.User().GetUserRoleByServiceID(SmartPortal.Constant.IPC.MB, row["USERID"].ToString().Trim(), string.Empty,ref IPCERRORCODE, ref IPCERRORDESC);

                        if (IPCERRORCODE != "0")
                        {
                            goto ERROR;
                        }

                        DataTable accountWLTableNUY = accountMBDatasetNUY.Tables[0];
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
                                stNUY.Append("<B>" + Resources.labels.walletbanking + "</B>");
                                stNUY.Append("</td>");
                                stNUY.Append("</tr>");

                                stNUY.Append("<tr>");
                                stNUY.Append("<td width='25%'>");
                                stNUY.Append(Resources.labels.phone);
                                stNUY.Append("</td>");
                                stNUY.Append("<td width='25%'>");
                                stNUY.Append(accountMBTableNUY.Rows[0]["UN"].ToString());
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


                        //lay het các tai khoan PHO cua user theo userID
                        DataSet accountPHODatasetNUY = new SmartPortal.SEMS.User().GetUserRoleByServiceID(SmartPortal.Constant.IPC.PHO, row["USERID"].ToString().Trim(), string.Empty,ref IPCERRORCODE, ref IPCERRORDESC);

                        if (IPCERRORCODE != "0")
                        {
                            goto ERROR;
                        }

                        DataTable accountPHOTableNUY = accountPHODatasetNUY.Tables[0];
                        if (accountPHOTableNUY.Rows.Count != 0)
                        {
                            if (accountPHOTableNUY.Rows[0]["ROLEID"].ToString().Trim() != "")
                            {
                                stNUY.Append("<tr>");
                                stNUY.Append("<td colspan='4'>");
                                stNUY.Append("<br/>");
                                stNUY.Append("</td>");
                                stNUY.Append("</tr>");

                                stNUY.Append("<tr>");
                                stNUY.Append("<td colspan='4'>");
                                stNUY.Append("<B>" + Resources.labels.phonebanking + "</B>");
                                stNUY.Append("</td>");
                                stNUY.Append("</tr>");

                                stNUY.Append("<tr>");
                                stNUY.Append("<td width='25%'>");
                                stNUY.Append(Resources.labels.username);
                                stNUY.Append("</td>");
                                stNUY.Append("<td width='25%'>");
                                stNUY.Append(accountPHOTableNUY.Rows[0]["UN"].ToString());
                                stNUY.Append("</td>");
                                stNUY.Append("<td width='25%'>");
                                stNUY.Append(Resources.labels.password);
                                stNUY.Append("</td>");
                                stNUY.Append("<td width='25%'>");
                                stNUY.Append("########");
                                //st.Append("<b>" + SmartPortal.Security.Encryption.Decrypt(accountPHOTable.Rows[0]["PASS"].ToString()) + "</b>");
                                stNUY.Append("</td>");
                                stNUY.Append("</tr>");
                            }
                        }

                        stNUY.Append("</table>");
                        j += 1;
                        if (j < nuyTable.Rows.Count)
                        {
                            stNUY.Append("<hr/>");
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
    void ReleaseSession()
    {
        ViewState["CHUTAIKHOAN"] = null;
        ViewState["NGUOIUYQUYEN"] = null;

    }
    protected void btnBack_Click(object sender, EventArgs e)
    {

    }
    protected void btnThemChuTaiKhoan_Click(object sender, EventArgs e)
    {
        int passlenWL = 0;
        string PassTemp = "";
        userName = txtWLPhoneNo.Text;
        if (userName == string.Empty || userName == null)
        {
            lblAlert.Text = Resources.labels.bancannhapsodienthoainguoisudung;
            return;
        }
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
        DataSet dspolicy = new DataSet();
        string filterWL = "serviceid='EW' and policyid='" + ddlpolicyWL.SelectedValue.ToString() + "'";
        string stSort = "serviceid asc";
        dspolicy = new SmartPortal.SEMS.USERPOLICY().GetPolicybyCondition(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, Session["userName"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
        if (IPCERRORCODE == "0")
        {
            DataTable dtWL = dspolicy.Tables[0].Select(filterWL, stSort).CopyToDataTable();
            passlenWL = Convert.ToInt32(dtWL.Rows[0]["minpwdlen"].ToString());

        }

        string passreveal = DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - passlenIB, passlenIB);
        string pwdreset = Encryption.Encrypt(passreveal);
        //PassTemp = SmartPortal.SEMS.O9Encryptpass.sha_sha256(pwdreset, userName);
        PassTemp = SmartPortal.SEMS.O9Encryptpass.sha_sha256(passreveal, userName);

        LuuThongTinQuyen("CHUTAIKHOAN", gvResultChuTaiKhoan, tvWL, txtReFullName.Text, lblLevel.Text, txtReBirth.Text, ddlReGender.SelectedValue, txtReMobi.Text, txtReEmail.Text, txtReAddress.Text, userName, PassTemp, "", "", "", "", "", "", PassTemp, DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - 8, 6), "", PassTemp, "", txtWLPhoneNo.Text.Trim().ToString(), pwdreset, DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - 8, 6), txtWLPhoneNo.Text.Trim().ToString(), PassTemp);

        if (gvResultChuTaiKhoan.Rows.Count == 0)
        {
            lblAlert.Text = Resources.labels.banchuadangkydichvu;
            return;
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
    protected void btnHuy_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable tblResultChuTaiKhoan = (DataTable)ViewState["CHUTAIKHOAN"];

            tblResultChuTaiKhoan.Rows.Clear();

            ViewState["CHUTAIKHOAN"] = tblResultChuTaiKhoan;
            gvResultChuTaiKhoan.DataSource = tblResultChuTaiKhoan.DefaultView.ToTable(true, "colFullName", "colIBUserName", "colAccount", "colRole");
            gvResultChuTaiKhoan.DataBind();

            lblAlert.Text = Resources.labels.recorddeleted;
        }
        catch
        {
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
                            rowNguoiUyQuyen["colServiceID"] = SmartPortal.Constant.IPC.EW;
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
            gvResult.DataSource = tblNguoiUyQuyen.DefaultView.ToTable(true, "colFullName", "colIBUserName", "colAccount", "colRole");
            gvResult.DataBind();
        }
    }
}
