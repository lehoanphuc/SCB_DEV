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
using System.Linq;
using System.Text;

public partial class Widgets_IBCorpUser_Controls_Widget : WidgetBase
{

    string ACTION = "";
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    DataTable userTable = new DataTable();
    DataTable userTableTemp = new DataTable();
    DataTable tblRoleDefault = new DataTable();
    DataTable tbAllAccount = new DataTable();

    string contractNo = "";
    string userName = "";
    private int yearold = 18;
    public string PRODUCTID
    {
        get { return ViewState["PRODUCTID"] != null ? (ViewState["PRODUCTID"] as string) : ""; }
        set { ViewState["PRODUCTID"] = value; }
    }
    public string USERTYPE
    {
        get { return ViewState["USERTYPE"] != null ? (ViewState["USERTYPE"] as string) : ""; }
        set { ViewState["USERTYPE"] = value; }
    }
    public string TYPEID
    {
        get { return ViewState["TYPEID"] != null ? (ViewState["TYPEID"] as string) : ""; }
        set { ViewState["TYPEID"] = value; }
    }
    public string USERID
    {
        get { return ViewState["USERID"] != null ? (ViewState["USERID"] as string) : ""; }
        set { ViewState["USERID"] = value; }
    }
    public string CONTRACTNO
    {
        get
        {
            return ViewState["CONTRACTNO"] != null ? (ViewState["CONTRACTNO"] as string) : "";
        }
        set { ViewState["CONTRACTNO"] = value; }
    }
    protected void Page_Init(object sender, EventArgs e)
    {
        TabCustomerInfoHelper.LoadConfig();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            ltrError.Text = "";
            lblAlert.Text = "";
            DataSet dsold = new DataSet();
            string old = string.Empty;
            dsold = new SmartPortal.SEMS.EBASYSVAR().ViewDetail("YEAROLD", ref IPCERRORCODE, ref IPCERRORDESC);
            if (dsold.Tables[0].Rows.Count > 0)
            {
                old = dsold.Tables[0].Rows[0]["VARVALUE"].ToString();
            }
            yearold = old != string.Empty ? Int32.Parse(old) : 18;

            txtReBirth.Text = DateTime.Now.AddYears(-yearold).ToString("dd/MM/yyyy");
            tbAllAccount = ViewState["ALLACCOUNT"] != null ? (DataTable)ViewState["ALLACCOUNT"] : LoadAllAccount();
            if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["uid"] != null)
            {
                USERID = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["uid"];
            }
            else if (USERID == null || USERID == "")
            {
                USERID = new SmartPortal.SEMS.User().CreatePrimaryKey(string.Empty, string.Empty, "USERID", ref IPCERRORCODE, ref IPCERRORDESC);
            }
            ACTION = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["a"].ToString().Trim();
            TYPEID = "IN";
            //get contractno
            userTableTemp = (new SmartPortal.SEMS.User().GetFullUserByUID(Session["userID"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
            //
            userName = GenerateUserName(txtIBUserName);
            if (userTableTemp.Rows.Count != 0)
            {

                hdContractNo.Value= contractNo = userTableTemp.Rows[0]["CONTRACTNO"].ToString();
                PRODUCTID = userTableTemp.Rows[0]["PRODUCTID"].ToString();
                txtPHOPhoneNo.Text = SmartPortal.Common.Utilities.Utility.GetID("", userTableTemp.Rows[0]["CUSTCODE"].ToString().Trim(), "", 10) + "1";
            }
            else
            {
            }
            if (!IsPostBack)
            {
                //load cap bac
                //get Contractno
                string contractNo = "";
                DataSet ContrNoDS = new SmartPortal.IB.Transactions().GetUserOfContractNoByUID(Utility.KillSqlInjection(Session["userID"].ToString()), "", ref IPCERRORCODE, ref IPCERRORDESC);
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

                DataSet dsLevel = new SmartPortal.IB.CorpUser().LoadUserGrByContractNo(contractNo, ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE == "0")
                {
                    if (dsLevel.Tables[0].Rows.Count != 0)
                    {
                        ddlGroupUser.DataSource = dsLevel;
                        ddlGroupUser.DataTextField = "GroupName";
                        ddlGroupUser.DataValueField = "GroupID";
                        ddlGroupUser.DataBind();
                        lblvalidategroup.Visible = false;

                    }
                    else
                    {
                        if (ACTION == SmartPortal.Constant.IPC.VIEWDETAIL || ACTION == SmartPortal.Constant.IPC.EDIT || ACTION == "DETAILS" || ACTION == "EDIT")
                        {
                            ddlGroupUser.Items.Insert(0, new ListItem("Default", ""));
                            lblvalidategroup.Visible = true;
                            lblvalidategroup.Text = "User groups are not set by default";
                        }
                        else
                        {
                            ddlGroupUser.Items.Insert(0, new ListItem("Default", ""));
                            lblvalidategroup.Visible = true;
                            lblvalidategroup.Text = "User groups are not set by default";

                            //throw new IPCException(SmartPortal.Constant.IPC.ERRORCODE.MUSTADDLEVEL);
                        }
                    }
                }
                else
                {
                    throw new Exception();
                }

                if (ViewState["NGUOIQUANTRI"] == null)
                {
                    txtIBUserName.Text = userName;
                    txtIBUserName.Enabled = true;
                    txtMBPhoneNo.Text = userName;

                    #region load usertype
                    DataSet dsUserType = new DataSet();
                    DataTable dtUserType = new DataTable();

                    dtUserType = new SmartPortal.SEMS.Contract().LoadContractType(SmartPortal.Constant.IPC.CORPORATE, "Y");

                    ddlUserType.DataSource = dtUserType;
                    ddlUserType.DataTextField = "TYPENAME";
                    ddlUserType.DataValueField = "USERTYPE";
                    ddlUserType.DataBind();

                    #endregion

                    #region lay tat ca cac account cua khach hang
                    DataSet ds = new DataSet();

                    if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["uid"] != null)
                    {
                        ds = new SmartPortal.IB.Customer().GetAllAcc(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["uid"], ref IPCERRORCODE, ref IPCERRORDESC);
                    }
                    else
                    {
                        ds = new SmartPortal.IB.Customer().GetAllAcc(Session["userID"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
                    }
                 
                    if (IPCERRORCODE != "0")
                    {
                        throw new IPCException(SmartPortal.Constant.IPC.ERRORCODE.IPC);
                    }
                    DataTable dtAccount = new DataTable();
                    dtAccount = ds.Tables[0];
                    tbAllAccount = ds.Tables[0];

                    if (dtAccount.Rows.Count == 0)
                    {
                        throw new IPCException(SmartPortal.Constant.IPC.ERRORCODE.IPC);
                    }
                    else
                    {
                        ddlSMSDefaultAcctno.DataSource = dtAccount;
                        ddlSMSDefaultAcctno.DataTextField = "DATAText";
                        ddlSMSDefaultAcctno.DataValueField = "DATAValue";
                        ddlSMSDefaultAcctno.DataBind();

                        ddlPHODefaultAcctno.DataSource = dtAccount;
                        ddlPHODefaultAcctno.DataTextField = "DATAText";
                        ddlPHODefaultAcctno.DataValueField = "DATAValue";
                        ddlPHODefaultAcctno.DataBind();

                        lstDept.DataSource = dtAccount.AsEnumerable().Where(x => x.Field<string>("ACCOUNTTYPE") != "WL").CopyToDataTable<DataRow>();
                        lstDept.DataTextField = "DATAText";
                        lstDept.DataValueField = "DATAValue";
                        lstDept.DataBind();

                        //if (lstDept.Items.Count != 0)
                        //{
                        //    lstDept.Items[0].Selected = true;
                        //    if (dtAccount.AsEnumerable().Where(x => x.Field<string>("ACCOUNTTYPE") == "WL").Any())
                        //    {
                        //        isWallet.Checked = false;
                        //        isWallet.Visible = true;
                        //    }
                        //    else
                        //    {
                        //        isWallet.Visible = false;
                        //    }
                        //}

                        if (dtAccount.AsEnumerable().Where(x => x.Field<string>("ACCOUNTTYPE") == "WL" && x.Field<string>("DATAValue") == lstDept.SelectedValue.ToString().Trim()).Any())
                        {
                            tvRole.Visible = false;
                            tvWallet.Visible = true;

                        }
                        else
                        {
                            tvRole.Visible = true;
                            tvWallet.Visible = false;

                        }
                    }
                    #endregion
                    BindData();
                    if (isWallet.Checked)
                    {
                        lstDept.DataSource = dtAccount;
                        lstDept.DataTextField = "DATAText";
                        lstDept.DataValueField = "DATAValue";
                        lstDept.DataBind();
                    }

                }
            }


        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(IPCex.Message, this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(IPCex.Message, Request.Url.Query);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }

    }


    void BindData()
    {
        //enable(disable) theo action

        switch (ACTION)
        {
            case SmartPortal.Constant.IPC.VIEWDETAIL:
            case "DETAILS":

                if (!IBCheckPermitPageAction("DETAILS")) return;
                txtReBirth.Enabled = false;
                txtReFullName.Enabled = false;
                txtReMobi.Enabled = false;
                txtIBUserName.Enabled = false;
                ddlUserType.Enabled = false;
                txtReEmail.Enabled = false;
                ddlReGender.Enabled = false;
                ddlGroupUser.Enabled = false;
                txtReAddress.Enabled = false;
                txtSMSPhoneNo.Enabled = false;
                txtMBPhoneNo.Enabled = false;
                txtPHOPhoneNo.Enabled = false;
                btsaveandcont.Visible = false;
                btnThemNQT.Visible = false;
                btnHuy.Visible = false;
                tvIBQT.Enabled = false;
                tvMBQT.Enabled = false;
                tvPHOQT.Enabled = false;
                tvSMSQT.Enabled = false;
                txtIBPass.Enabled = false;
                txtIBRePass.Enabled = false;
                ddlStatusIB.Enabled = false;
                ddlpolicyIB.Enabled = false;
                ddlDefaultLang.Enabled = false;
                ddlSMSDefaultAcctno.Enabled = false;
                ddlStatusSMS.Enabled = false;
                cbIsDefault.Enabled = false;
                txtMBPass.Enabled = false;
                txtMBRePass.Enabled = false;
                ddlStatusMB.Enabled = false;
                ddlpolicyMB.Enabled = false;
                txtPHOPass.Enabled = false;
                txtPHORepass.Enabled = false;
                ddlPHODefaultAcctno.Enabled = false;
                ddlStatusPHO.Enabled = false;
                ddlGroupUser.Enabled = false;
                genuser.Enabled = false;
                isWallet.Enabled = false;
                tvRole.Enabled = false;
                tvWallet.Enabled = false;
                break;
            case SmartPortal.Constant.IPC.EDIT:
            case "EDIT":
                if (!IBCheckPermitPageAction("EDIT")) return;
                txtMBPhoneNo.Enabled = true;
                txtReBirth.Enabled = true;
                txtReFullName.Enabled = true;
                txtReMobi.Enabled = true;
                ddlUserType.Enabled = true;
                txtReEmail.Enabled = true;
                ddlReGender.Enabled = true;
                ddlGroupUser.Enabled = true;
                txtReAddress.Enabled = true;
                txtSMSPhoneNo.Enabled = true;
                txtMBPhoneNo.Enabled = false;
                txtPHOPhoneNo.Enabled = false;
                btsaveandcont.Visible = true;
                break;


        }
        #region Hien thi tat cac cac role theo serviceid va usertype len cay

        LoadDataALLRoleInTreeview("", tvRole, "IN", "NOR");

        LoadDataALLRoleInTreeviewWAL("", tvWallet, "IN", "WAL");

        #endregion
        if (ACTION == "ADD" || ACTION == "add")
        {
            CheckAllRole();
        }
        #region lay role mac dinh
        GetRoleDefault(tvIBQT, tvSMSQT, tvMBQT, tvPHOQT, contractNo);
        #endregion

        #region hien thi tat ca cac role boi contractno

        #endregion

        if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["uid"] != null)
        {

            txtIBPass.Enabled = false;
            txtIBRePass.Enabled = false;
            #region Lấy thông tin user
            userTable = (new SmartPortal.SEMS.User().GetFullUserByUID(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["uid"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
            if (userTable.Rows.Count != 0)
            {

                txtReFullName.Text = userTable.Rows[0]["FULLNAME"].ToString();
                txtReMobi.Text = userTable.Rows[0]["PHONE"].ToString();
                ddlUserType.SelectedValue = userTable.Rows[0]["USERTYPE"].ToString();
                txtReEmail.Text = userTable.Rows[0]["EMAIL"].ToString();
                ddlReGender.SelectedValue = userTable.Rows[0]["GENDER"].ToString();
                txtReAddress.Text = userTable.Rows[0]["ADDRESS"].ToString();
                if (userTable.Rows[0]["BIRTHDAY"].ToString() != "")
                {
                    txtReBirth.Text = SmartPortal.Common.Utilities.Utility.IsDateTime2(userTable.Rows[0]["BIRTHDAY"].ToString()).ToString("dd/MM/yyyy");
                }
                lblStatus.Text = userTable.Rows[0]["STATUS"].ToString();
                if(!hdContractNo.Value.Equals(userTable.Rows[0]["CONTRACTNO"].ToString()))
                {
                    SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
                }
            }


            #endregion



            #region Lấy group của user 
            DataSet dsgroup = new DataSet();
            dsgroup = new SmartPortal.SEMS.User().GetGroupByUser(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["uid"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
            ddlGroupUser.SelectedValue = dsgroup.Tables[0].Rows[0]["GroupID"].ToString();
            #endregion


            #region Lấy thông tin tài khoản User
            DataTable tblIB = new SmartPortal.IB.User().GetAccountDetail(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["uid"].ToString(), SmartPortal.Constant.IPC.IB);
            if (tblIB.Rows.Count != 0)
            {
                txtIBUserName.Text = tblIB.Rows[0]["USERNAME"].ToString().Trim();
                ddlStatusIB.SelectedValue = tblIB.Rows[0]["STATUS"].ToString().Trim();
            }

            DataTable tblSMS = new SmartPortal.IB.User().GetAccountDetail(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["uid"].ToString(), SmartPortal.Constant.IPC.SMS);
            if (tblSMS.Rows.Count != 0)
            {
                txtSMSPhoneNo.Text = tblSMS.Rows[0]["PHONENO"].ToString().Trim();
                ddlStatusSMS.SelectedValue = tblSMS.Rows[0]["STATUS"].ToString().Trim();
                ddlSMSDefaultAcctno.SelectedValue = tblSMS.Rows[0]["DEFAULTACCTNO"].ToString().Trim();
                ddlDefaultLang.SelectedValue = tblSMS.Rows[0]["DEFAULTLANG"].ToString().Trim();

                if (tblSMS.Rows[0]["ISDEFAULT"].ToString().Trim() == "Y")
                {
                    cbIsDefault.Checked = true;
                }
                else
                {
                    cbIsDefault.Checked = true;
                }
            }
            DataTable tblMB = new SmartPortal.IB.User().GetAccountDetail(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["uid"].ToString(), SmartPortal.Constant.IPC.MB);
            if (tblMB.Rows.Count != 0)
            {
                txtMBPhoneNo.Text = tblMB.Rows[0]["PHONENO"].ToString().Trim();
                txtMBPhoneNo.Enabled = false;
                ddlStatusMB.SelectedValue = tblMB.Rows[0]["STATUS"].ToString().Trim();
            }

            DataTable tblPHO = new SmartPortal.IB.User().GetAccountDetail(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["uid"].ToString(), SmartPortal.Constant.IPC.PHO);
            if (tblPHO.Rows.Count != 0)
            {
                txtPHOPhoneNo.Text = tblPHO.Rows[0]["PHONENO"].ToString().Trim();
                txtPHOPhoneNo.Enabled = false;
                ddlStatusPHO.SelectedValue = tblPHO.Rows[0]["STATUS"].ToString().Trim();
                txtPHOPass.Attributes.Add("value", Encryption.Decrypt(tblPHO.Rows[0]["PASS"].ToString().Trim()));
                txtPHORepass.Attributes.Add("value", Encryption.Decrypt(tblPHO.Rows[0]["PASS"].ToString().Trim()));

                ddlPHODefaultAcctno.SelectedValue = tblPHO.Rows[0]["DEFAULTACCTNO"].ToString().Trim();
            }
            #endregion

            #region lấy thông tin tất cả các quyền
            DataTable tblRoleDetail = new SmartPortal.IB.User().GetRoleDetail(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["uid"].ToString());

            //luu thông tin vào ViewState và hiển thị lên lưới
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
            DataColumn colPHOPhone = new DataColumn("colPHOPhone");
            DataColumn colPHOPass = new DataColumn("colPHOPass");
            DataColumn colPHODefaultAcctno = new DataColumn("colPHODefaultAcctno");
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
            tblNguoiUyQuyen.Columns.Add(colPHOPhone);
            tblNguoiUyQuyen.Columns.Add(colPHOPass);
            tblNguoiUyQuyen.Columns.Add(colPHODefaultAcctno);
            tblNguoiUyQuyen.Columns.Add(colAccount);
            tblNguoiUyQuyen.Columns.Add(colRole);
            tblNguoiUyQuyen.Columns.Add(colRoleID);
            tblNguoiUyQuyen.Columns.Add(colTranCode);
            tblNguoiUyQuyen.Columns.Add(colTranCodeID);
            tblNguoiUyQuyen.Columns.Add(colServiceID);

            DataTable tblAccountRole = ViewState["ACCOUNTROLE"] != null ? (DataTable)ViewState["ACCOUNTROLE"] : CreateTableAccontRole();
            DataTable tblAccountRoleWL = ViewState["ACCOUNTROLEWL"] != null ? (DataTable)ViewState["ACCOUNTROLEWL"] : CreateTableAccontRole();

            foreach (DataRow rRole in tblRoleDetail.Rows)
            {
                DataRow r = tblNguoiUyQuyen.NewRow();
                r["colFullName"] = txtReFullName.Text;
                r["colLevel"] = ddlGroupUser.SelectedValue;
                r["colBirthday"] = ddlGroupUser.SelectedValue;
                r["colGender"] = ddlReGender.SelectedValue;
                r["colPhone"] = txtReMobi.Text;
                r["colEmail"] = txtReEmail.Text;
                r["colAddress"] = txtReAddress.Text;
                r["colIBUserName"] = txtIBUserName.Text;
                r["colIBPass"] = txtIBPass.Text;
                r["colSMSPhone"] = txtSMSPhoneNo.Text;
                r["colSMSDefaultAcctno"] = ddlSMSDefaultAcctno.SelectedValue;
                r["colSMSDefaultLang"] = ddlDefaultLang.SelectedValue;
                r["colMBPhone"] = txtMBPhoneNo.Text;
                r["colMBPass"] = txtMBPass.Text;
                r["colPHOPhone"] = txtPHOPhoneNo.Text;
                r["colPHOPass"] = txtPHOPass.Text;
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
                r["colPHODefaultAcctno"] = ddlPHODefaultAcctno.SelectedValue;

                tblNguoiUyQuyen.Rows.Add(r);
                if (r["colAccount"].ToString().Equals(rRole["ACCTNO"].ToString()) && !rRole["RoleType"].ToString().Equals("WAL"))
                {
                    foreach (TreeNode node in tvRole.Nodes)
                    {
                        if (node.Value.Equals(r["colRoleID"].ToString()))
                        {

                            DataRow rowUser = CreateTableAccontRole().NewRow();
                            rowUser["colAccount"] = rRole["ACCTNO"].ToString();
                            rowUser["colRoleID"] = rRole["ROLEID"].ToString();
                            rowUser["colServiceID"] = rRole["SERVICEID"].ToString();
                            rowUser["colContractNo"] = contractNo;
                            rowUser["colUserID"] = USERID; if (!tblAccountRole.AsEnumerable().Where(x => x.Field<string>("colUserID") == USERID && x.Field<string>("colAccount") == rRole["ACCTNO"].ToString() && x.Field<string>("colRoleID") == rRole["ROLEID"].ToString()).Any())
                            {
                                node.Checked = true;
                                tblAccountRole.Rows.Add(rowUser.ItemArray);
                            }
                            ViewState["ACCOUNTROLE"] = tblAccountRole;
                        }
                    }
                }
                else
                {
                    foreach (TreeNode node in tvWallet.Nodes)
                    {
                        if (node.Value.Equals(r["colRoleID"].ToString()))
                        {

                            DataRow rowUser = CreateTableAccontRole().NewRow();
                            rowUser["colUserID"] = USERID;
                            rowUser["colAccount"] = rRole["ACCTNO"].ToString();
                            rowUser["colRoleID"] = rRole["ROLEID"].ToString();
                            rowUser["colServiceID"] = rRole["SERVICEID"].ToString();
                            rowUser["colContractNo"] = contractNo;
                            rowUser["colUserID"] = USERID;
                            if (!tblAccountRoleWL.AsEnumerable().Where(x => x.Field<string>("colUserID") == USERID && x.Field<string>("colAccount") == rRole["ACCTNO"].ToString() && x.Field<string>("colRoleID") == rRole["ROLEID"].ToString()).Any())
                            {
                                node.Checked = true;
                                tblAccountRoleWL.Rows.Add(rowUser.ItemArray);
                            }
                            ViewState["ACCOUNTROLEWL"] = tblAccountRoleWL;
                            isWallet.Checked = true;
                        }
                    }
                }
            }
            ViewState["NGUOIQUANTRI"] = tblNguoiUyQuyen;
            gvResultQuanTri.Visible = false;
            gvResultQuanTri.DataSource = tblNguoiUyQuyen.DefaultView.ToTable(true, "colFullName", "colIBUserName", "colAccount", "colRole");
            gvResultQuanTri.DataBind();
            foreach (TreeNode node in tvRole.Nodes)
            {
                node.Checked = false;
            }
            foreach (TreeNode node in tvWallet.Nodes)
            {
                node.Checked = false;
            }

            lstDept.SelectedIndex = 0;
            LoadAccountRole(USERID, lstDept.SelectedValue.ToString().Trim());
            #endregion
        }
    }

    void LoadDataInTreeview(string serviceID, TreeView tvPage, string contracNo)
    {
        tvPage.Nodes.Clear();
        //DataSet ds=new SmartPortal.IB.Role().GetRoleByServiceID(serviceID, contractNo, ref IPCERRORCODE, ref IPCERRORDESC);
        DataSet ds = new SmartPortal.IB.Role().GetRoleByServiceID_CoprUser(serviceID, contractNo, ref IPCERRORCODE, ref IPCERRORDESC);

        DataTable tblSS = new DataTable();
        if (IPCERRORCODE == "0")
        {
            tblSS = ds.Tables[0];
        }

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

                node.ChildNodes.Add(node1);
            }
            if (tblPage.Rows.Count != 0)
            {
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

            #endregion

        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], "Widgets_SEMSCustomerList_Add_Widget", "btnThemNQT_Click", IPCex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);
        }

        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSCustomerList_Add_Widget", "btnThemNQT_Click", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    void LuuThongTinQuyen(string sessionName, Repeater gvResult, TreeView tvRole, TreeView tvSMSRole, TreeView tvMBRole, TreeView tvPHORole, string fullName, string level, string birthday, string gender, string phone, string email, string address, string IBUserName, string IBPass, string SMSPhone, string SMSDefaultAcctno, string SMSDefaultLang, string isDefault, string SMSPinCode, string MBPhone, string MBPass, string MBPinCode, string PHOPhone, string PHOPass, string PHODefaultAcctno, string Account)
    {
        if (ViewState[sessionName] == null)
        {
            #region NULL


            #region khoi tao bang
            DataTable tblNguoiUyQuyen = new DataTable();
            DataTable tbAllRoles = new DataTable();
            DataTable tbAllRolesWal = new DataTable();

            tbAllRoles = (DataTable)ViewState["AllRoleWal"];
            tbAllRolesWal = (DataTable)ViewState["AllRole"];

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
            DataColumn colPHOPhone = new DataColumn("colPHOPhone");
            DataColumn colPHOPass = new DataColumn("colPHOPass");
            DataColumn colPHODefaultAcctno = new DataColumn("colPHODefaultAcctno");
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
            tblNguoiUyQuyen.Columns.Add(colPHOPhone);
            tblNguoiUyQuyen.Columns.Add(colPHOPass);
            tblNguoiUyQuyen.Columns.Add(colPHODefaultAcctno);
            tblNguoiUyQuyen.Columns.Add(colAccount);
            tblNguoiUyQuyen.Columns.Add(colRole);
            tblNguoiUyQuyen.Columns.Add(colRoleID);
            tblNguoiUyQuyen.Columns.Add(colTranCode);
            tblNguoiUyQuyen.Columns.Add(colTranCodeID);
            tblNguoiUyQuyen.Columns.Add(colServiceID);
            #endregion
            //IB


            DataTable tblAccountRole = ViewState["ACCOUNTROLE"] != null ? (DataTable)ViewState["ACCOUNTROLE"] : CreateTableAccontRole();
            foreach (DataRow dr in tblAccountRole.Rows)
            {
                if (dr["colAccount"].ToString().Equals(Account))
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

            if (IBUserName != "")
            {
                foreach (TreeNode nodeRoleIBNguoiUyQuyen in tvRole.Nodes)
                {
                    if (nodeRoleIBNguoiUyQuyen.Checked)
                    {
                        foreach (TreeNode nodeTrancodeIBNguoiUyQuyen in nodeRoleIBNguoiUyQuyen.ChildNodes)
                        {
                            //if (nodeTrancodeIBNguoiUyQuyen.Checked)
                            //{
                            #region luu thong tin IB khi NULL
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
                            rowNguoiUyQuyen["colPHOPhone"] = PHOPhone.Trim();
                            rowNguoiUyQuyen["colPHOPass"] = PHOPass;
                            rowNguoiUyQuyen["colPHODefaultAcctno"] = PHODefaultAcctno;
                            rowNguoiUyQuyen["colAccount"] = Account;
                            rowNguoiUyQuyen["colRole"] = nodeRoleIBNguoiUyQuyen.Text;
                            rowNguoiUyQuyen["colRoleID"] = nodeRoleIBNguoiUyQuyen.Value;
                            rowNguoiUyQuyen["colTranCode"] = nodeTrancodeIBNguoiUyQuyen.Text;
                            rowNguoiUyQuyen["colTranCodeID"] = nodeTrancodeIBNguoiUyQuyen.Value;
                            rowNguoiUyQuyen["colServiceID"] = SmartPortal.Constant.IPC.IB;
                            if (tbAllRoles.Select("ServiceID='" + SmartPortal.Constant.IPC.IB + "' and RoleID='" +
                                                   nodeRoleIBNguoiUyQuyen.Value + "'").Length != 0)
                            {
                                tblNguoiUyQuyen.Rows.Add(rowNguoiUyQuyen);
                            }
                            else if (tbAllRolesWal.Select("ServiceID='" + SmartPortal.Constant.IPC.IB + "' and RoleID='" +
                                                  nodeRoleIBNguoiUyQuyen.Value + "'").Length != 0)
                            {
                                tblNguoiUyQuyen.Rows.Add(rowNguoiUyQuyen);
                            }
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
                            #region luu thong tin SMS khi NULL
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
                            rowNguoiUyQuyen["colPHOPhone"] = PHOPhone.Trim();
                            rowNguoiUyQuyen["colPHOPass"] = PHOPass;
                            rowNguoiUyQuyen["colPHODefaultAcctno"] = PHODefaultAcctno;
                            rowNguoiUyQuyen["colAccount"] = Account;
                            rowNguoiUyQuyen["colRole"] = nodeRoleIBNguoiUyQuyen.Text;
                            rowNguoiUyQuyen["colRoleID"] = nodeRoleIBNguoiUyQuyen.Value;
                            rowNguoiUyQuyen["colTranCode"] = nodeTrancodeIBNguoiUyQuyen.Text;
                            rowNguoiUyQuyen["colTranCodeID"] = nodeTrancodeIBNguoiUyQuyen.Value;
                            rowNguoiUyQuyen["colServiceID"] = SmartPortal.Constant.IPC.SMS;

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
                foreach (TreeNode nodeRoleIBNguoiUyQuyen in tvRole.Nodes)
                {
                    if (nodeRoleIBNguoiUyQuyen.Checked)
                    {
                        foreach (TreeNode nodeTrancodeIBNguoiUyQuyen in nodeRoleIBNguoiUyQuyen.ChildNodes)
                        {
                            //if (nodeTrancodeIBNguoiUyQuyen.Checked)
                            //{
                            #region luu thong tin MB khi NULL
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
                            rowNguoiUyQuyen["colPHOPhone"] = PHOPhone.Trim();
                            rowNguoiUyQuyen["colPHOPass"] = PHOPass;
                            rowNguoiUyQuyen["colPHODefaultAcctno"] = PHODefaultAcctno;
                            rowNguoiUyQuyen["colAccount"] = Account;
                            rowNguoiUyQuyen["colRole"] = nodeRoleIBNguoiUyQuyen.Text;
                            rowNguoiUyQuyen["colRoleID"] = nodeRoleIBNguoiUyQuyen.Value;
                            rowNguoiUyQuyen["colTranCode"] = nodeTrancodeIBNguoiUyQuyen.Text;
                            rowNguoiUyQuyen["colTranCodeID"] = nodeTrancodeIBNguoiUyQuyen.Value;
                            rowNguoiUyQuyen["colServiceID"] = SmartPortal.Constant.IPC.MB;
                            if (tbAllRoles.Select("ServiceID='" + SmartPortal.Constant.IPC.MB + "' and RoleID='" +
                                                  nodeRoleIBNguoiUyQuyen.Value + "'").Length != 0)
                            {
                                tblNguoiUyQuyen.Rows.Add(rowNguoiUyQuyen);
                            }
                            else if (tbAllRolesWal.Select("ServiceID='" + SmartPortal.Constant.IPC.MB + "' and RoleID='" +
                                                  nodeRoleIBNguoiUyQuyen.Value + "'").Length != 0)
                            {
                                tblNguoiUyQuyen.Rows.Add(rowNguoiUyQuyen);
                            }
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

            //PHO
            if (PHOPhone != "")
            {
                foreach (TreeNode nodeRoleIBNguoiUyQuyen in tvPHORole.Nodes)
                {
                    if (nodeRoleIBNguoiUyQuyen.Checked)
                    {
                        foreach (TreeNode nodeTrancodeIBNguoiUyQuyen in nodeRoleIBNguoiUyQuyen.ChildNodes)
                        {
                            //if (nodeTrancodeIBNguoiUyQuyen.Checked)
                            //{
                            #region luu thong tin PHO khi NULL
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
                            rowNguoiUyQuyen["colPHOPhone"] = PHOPhone.Trim();
                            rowNguoiUyQuyen["colPHOPass"] = PHOPass;
                            rowNguoiUyQuyen["colPHODefaultAcctno"] = PHODefaultAcctno;
                            rowNguoiUyQuyen["colAccount"] = Account;
                            rowNguoiUyQuyen["colRole"] = nodeRoleIBNguoiUyQuyen.Text;
                            rowNguoiUyQuyen["colRoleID"] = nodeRoleIBNguoiUyQuyen.Value;
                            rowNguoiUyQuyen["colTranCode"] = nodeTrancodeIBNguoiUyQuyen.Text;
                            rowNguoiUyQuyen["colTranCodeID"] = nodeTrancodeIBNguoiUyQuyen.Value;
                            rowNguoiUyQuyen["colServiceID"] = SmartPortal.Constant.IPC.PHO;

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

            ViewState[sessionName] = tblNguoiUyQuyen;
            gvResult.DataSource = tblNguoiUyQuyen.DefaultView.ToTable(true, "colFullName", "colIBUserName", "colAccount", "colRole");
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
                    if (nodeRoleIBNguoiUyQuyen.Checked)
                    {
                        foreach (TreeNode nodeTrancodeIBNguoiUyQuyen in nodeRoleIBNguoiUyQuyen.ChildNodes)
                        {
                            //if (nodeTrancodeIBNguoiUyQuyen.Checked)
                            //{
                            if (tblNguoiUyQuyen.Select("colFullName='" + fullName + "' and colRoleID='" + nodeRoleIBNguoiUyQuyen.Value + "' and colAccount='" + Account + "' and colTranCode='" + nodeTrancodeIBNguoiUyQuyen.Text + "'").Length == 0)
                            {
                                #region luu thong tin quyen IB khac NULL
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
                                rowNguoiUyQuyen["colPHOPhone"] = PHOPhone.Trim();
                                rowNguoiUyQuyen["colPHOPass"] = PHOPass;
                                rowNguoiUyQuyen["colPHODefaultAcctno"] = PHODefaultAcctno;
                                rowNguoiUyQuyen["colAccount"] = Account;
                                rowNguoiUyQuyen["colRole"] = nodeRoleIBNguoiUyQuyen.Text;
                                rowNguoiUyQuyen["colRoleID"] = nodeRoleIBNguoiUyQuyen.Value;
                                rowNguoiUyQuyen["colTranCode"] = nodeTrancodeIBNguoiUyQuyen.Text;
                                rowNguoiUyQuyen["colTranCodeID"] = nodeTrancodeIBNguoiUyQuyen.Value;
                                rowNguoiUyQuyen["colServiceID"] = SmartPortal.Constant.IPC.IB;

                                tblNguoiUyQuyen.Rows.Add(rowNguoiUyQuyen);
                                #endregion
                            }
                        }
                    }
                    else
                    {
                        foreach (TreeNode nodeTrancodeIBNguoiUyQuyen in nodeRoleIBNguoiUyQuyen.ChildNodes)
                        {

                            if (tblNguoiUyQuyen.Select("colFullName='" + fullName + "' and colRoleID='" + nodeRoleIBNguoiUyQuyen.Value + "' and colAccount='" + Account + "' and colTranCode='" + nodeTrancodeIBNguoiUyQuyen.Text + "'").Length == 0)
                            {

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
                            if (tblNguoiUyQuyen.Select("colFullName='" + fullName + "' and colAccount='" + Account + "' and colRoleID='" + nodeRoleIBNguoiUyQuyen.Value + "' and colTranCode='" + nodeTrancodeIBNguoiUyQuyen.Text + "'").Length == 0)
                            {
                                #region luu thong tin SMS khac NULL
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
                                rowNguoiUyQuyen["colPHOPhone"] = PHOPhone.Trim();
                                rowNguoiUyQuyen["colPHOPass"] = PHOPass;
                                rowNguoiUyQuyen["colPHODefaultAcctno"] = PHODefaultAcctno;
                                rowNguoiUyQuyen["colAccount"] = Account;
                                rowNguoiUyQuyen["colRole"] = nodeRoleIBNguoiUyQuyen.Text;
                                rowNguoiUyQuyen["colRoleID"] = nodeRoleIBNguoiUyQuyen.Value;
                                rowNguoiUyQuyen["colTranCode"] = nodeTrancodeIBNguoiUyQuyen.Text;
                                rowNguoiUyQuyen["colTranCodeID"] = nodeTrancodeIBNguoiUyQuyen.Value;
                                rowNguoiUyQuyen["colServiceID"] = SmartPortal.Constant.IPC.SMS;

                                tblNguoiUyQuyen.Rows.Add(rowNguoiUyQuyen);
                                #endregion
                            }

                        }
                    }
                    else
                    {
                        foreach (TreeNode nodeTrancodeIBNguoiUyQuyen in nodeRoleIBNguoiUyQuyen.ChildNodes)
                        {
                            if (tblNguoiUyQuyen.Select("colFullName='" + fullName + "' and colAccount='" + Account + "' and colRoleID='" + nodeRoleIBNguoiUyQuyen.Value + "' and colTranCode='" + nodeTrancodeIBNguoiUyQuyen.Text + "'").Length == 0)
                            {

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
                            if (tblNguoiUyQuyen.Select("colFullName='" + fullName + "' and colAccount='" + Account + "' and colRoleID='" + nodeRoleIBNguoiUyQuyen.Value + "' and colTranCode='" + nodeTrancodeIBNguoiUyQuyen.Text + "'").Length == 0)
                            {
                                #region luu thong tin MB khac NULL
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
                                rowNguoiUyQuyen["colPHOPhone"] = PHOPhone.Trim();
                                rowNguoiUyQuyen["colPHOPass"] = PHOPass;
                                rowNguoiUyQuyen["colPHODefaultAcctno"] = PHODefaultAcctno;
                                rowNguoiUyQuyen["colAccount"] = Account;
                                rowNguoiUyQuyen["colRole"] = nodeRoleIBNguoiUyQuyen.Text;
                                rowNguoiUyQuyen["colRoleID"] = nodeRoleIBNguoiUyQuyen.Value;
                                rowNguoiUyQuyen["colTranCode"] = nodeTrancodeIBNguoiUyQuyen.Text;
                                rowNguoiUyQuyen["colTranCodeID"] = nodeTrancodeIBNguoiUyQuyen.Value;
                                rowNguoiUyQuyen["colServiceID"] = SmartPortal.Constant.IPC.MB;

                                tblNguoiUyQuyen.Rows.Add(rowNguoiUyQuyen);
                                #endregion
                            }
                        }
                    }
                    else
                    {
                        foreach (TreeNode nodeTrancodeIBNguoiUyQuyen in nodeRoleIBNguoiUyQuyen.ChildNodes)
                        {

                            if (tblNguoiUyQuyen.Select("colFullName='" + fullName + "' and colAccount='" + Account + "' and colRoleID='" + nodeRoleIBNguoiUyQuyen.Value + "' and colTranCode='" + nodeTrancodeIBNguoiUyQuyen.Text + "'").Length == 0)
                            {

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
            }

            //PHO
            if (PHOPhone != "")
            {
                foreach (TreeNode nodeRoleIBNguoiUyQuyen in tvPHORole.Nodes)
                {
                    if (nodeRoleIBNguoiUyQuyen.Checked)
                    {
                        foreach (TreeNode nodeTrancodeIBNguoiUyQuyen in nodeRoleIBNguoiUyQuyen.ChildNodes)
                        {

                            if (tblNguoiUyQuyen.Select("colFullName='" + fullName + "' and colAccount='" + Account + "' and colRoleID='" + nodeRoleIBNguoiUyQuyen.Value + "' and colTranCode='" + nodeTrancodeIBNguoiUyQuyen.Text + "'").Length == 0)
                            {
                                #region luu thong tin PHO khi NULL
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
                                rowNguoiUyQuyen["colPHODefaultAcctno"] = PHODefaultAcctno;
                                rowNguoiUyQuyen["colPHOPhone"] = PHOPhone.Trim();
                                rowNguoiUyQuyen["colPHOPass"] = PHOPass;
                                rowNguoiUyQuyen["colAccount"] = Account;
                                rowNguoiUyQuyen["colRole"] = nodeRoleIBNguoiUyQuyen.Text;
                                rowNguoiUyQuyen["colRoleID"] = nodeRoleIBNguoiUyQuyen.Value;
                                rowNguoiUyQuyen["colTranCode"] = nodeTrancodeIBNguoiUyQuyen.Text;
                                rowNguoiUyQuyen["colTranCodeID"] = nodeTrancodeIBNguoiUyQuyen.Value;
                                rowNguoiUyQuyen["colServiceID"] = SmartPortal.Constant.IPC.PHO;

                                tblNguoiUyQuyen.Rows.Add(rowNguoiUyQuyen);
                                #endregion
                            }
                        }
                    }
                    else
                    {
                        foreach (TreeNode nodeTrancodeIBNguoiUyQuyen in nodeRoleIBNguoiUyQuyen.ChildNodes)
                        {

                            if (tblNguoiUyQuyen.Select("colFullName='" + fullName + "' and colAccount='" + Account + "' and colRoleID='" + nodeRoleIBNguoiUyQuyen.Value + "' and colTranCode='" + nodeTrancodeIBNguoiUyQuyen.Text + "'").Length == 0)
                            {

                            }
                            else
                            {
                                if (tblNguoiUyQuyen.Select("colFullName='" + fullName + "' and colAccount='" + Account + "' and colRoleID='" + nodeRoleIBNguoiUyQuyen.Value + "' and colTranCode='" + nodeTrancodeIBNguoiUyQuyen.Text + "'").Length != 0)
                                {
                                    foreach (DataRow r in tblNguoiUyQuyen.Select("colFullName='" + fullName + "' and colAccount='" + Account + "' and colRoleID='" + nodeRoleIBNguoiUyQuyen.Value + "' and colTranCode='" + nodeTrancodeIBNguoiUyQuyen.Text + "'"))
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
            gvResult.DataSource = tblNguoiUyQuyen.DefaultView.ToTable(true, "colFullName", "colIBUserName", "colAccount", "colRole");
            gvResult.DataBind();
        }
    }

    protected void btsaveandcont_Click(object sender, EventArgs e)
    {
        try
        {

            #region MyRegion
            if (!IBCheckPermitPageAction(ACTION)) return;
            string PassTemp = "";
            if ((ACTION == "ADD" || ACTION == "add"))
            {
                if (txtIBPass.Text.Equals("") || string.IsNullOrEmpty(txtIBPass.Text))
                {
                    ltrError.Text = Resources.labels.passwordrequire;
                    return;
                }
                if (txtIBRePass.Text.Equals("") || string.IsNullOrEmpty(txtIBRePass.Text))
                {
                    ltrError.Text = Resources.labels.passwordrequire;
                    return;
                }
                if (!txtIBPass.Text.Trim().Equals(txtIBRePass.Text.Trim()))
                {
                    ltrError.Text = Resources.labels.passwordcompare;
                    return;
                }
                if (txtIBPass.Text.Length < 6)
                {
                    ltrError.Text = Resources.labels.passwordlength;
                    return;
                }
            }
            if (((ACTION == "EDIT" || ACTION == "edit") && (txtIBPass.Text != "" || txtIBRePass.Text != "")))
            {
                txtIBPass.Text = "";
                txtIBRePass.Text = "";
            }
            if (txtReBirth.Text.Equals("") || string.IsNullOrEmpty(txtReBirth.Text))
            {
                ltrError.Text = Resources.labels.ngaysinhkhongdetrong;
                return;
            }
            if (txtIBUserName.Text.Length < 6 || txtIBUserName.Text.Length > 20)
            {
                ltrError.Text = Resources.labels.usernamelength;
                return;
            }
            if (!CheckUserNameExists(txtIBUserName.Text, "IB") || !CheckUserNameExists(txtMBPhoneNo.Text, "MB"))
            {
                ltrError.Text = Resources.labels.usernameinvalid;
                return;
            }
            if (!CheckIsPhoneNumer(txtReMobi.Text.Trim()))
            {
                ltrError.Text = Resources.labels.phonenumberwrong;
                return;
            }

            if (!CheckExistPhoneContract(txtReMobi.Text, contractNo, USERID))
            {
                ltrError.Text = Resources.labels.phonenumberisalreadyregistered;
                return;
            }
            //luu tat ca account
            PassTemp = Encryption.Encrypt(DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - 8, 8));
            foreach (DataRow rowAccount in tbAllAccount.Rows)
            {
                LuuThongTinQuyen("NGUOIQUANTRI", gvResultQuanTri, tvRole, tvSMSQT, tvMBQT, tvPHOQT, txtReFullName.Text, "0", txtReBirth.Text, ddlReGender.SelectedValue, txtReMobi.Text, txtReEmail.Text, txtReAddress.Text, txtIBUserName.Text, PassTemp, txtSMSPhoneNo.Text, ddlSMSDefaultAcctno.SelectedValue, ddlDefaultLang.SelectedValue, (cbIsDefault.Checked == true ? "Y" : "N"), DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - 8, 6), txtMBPhoneNo.Text, PassTemp, DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - 8, 6), txtPHOPhoneNo.Text, PassTemp, ddlPHODefaultAcctno.SelectedValue, rowAccount["DATAValue"].ToString());
            }

            #endregion

            string pwdreset = string.Empty;
            if (ViewState["NGUOIQUANTRI"] == null)
            {
                throw new IPCException(SmartPortal.Constant.IPC.ERRORCODE.FULLINFOERROR);
            }

            //ktra ngay sinh
            //ktra 18 tuoi
            if (txtReBirth.Text.Trim() != "")
            {
                bool flag = SmartPortal.Common.Validator.CheckYearOld(txtReBirth.Text.Trim(), yearold);
                if (flag)
                {
                    ltrError.Text = Resources.labels.birthdaylessthan18year.Replace("18", yearold.ToString());
                    return;
                }
            }

            string lastModify = SmartPortal.Common.Utilities.Utility.KillSqlInjection(DateTime.Now.ToString("dd/MM/yyyy"));
            string userCreate = SmartPortal.Common.Utilities.Utility.KillSqlInjection(Session["userName"].ToString().Trim());
            string userLastModify = SmartPortal.Common.Utilities.Utility.KillSqlInjection(Session["userName"].ToString().Trim());
            string userApprove = "";
            string endDate = SmartPortal.Constant.IPC.DATEDEFAULT;


            string status = ddlStatusIB.SelectedValue;

            string userID = USERID;
            string userFullName = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtReFullName.Text.Trim());
            string userGender = ddlReGender.SelectedValue;
            string userAddress = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtReAddress.Text.Trim());
            string userEmail = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtReEmail.Text.Trim());
            string userPhone = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtReMobi.Text.Trim());
            string userType = "0603";
            string userLevel = "0";
            string deptID = SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["did"].ToString().Trim();
            string userBirthday = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtReBirth.Text.Trim());
            if (userBirthday == "")
            {
                userBirthday = SmartPortal.Constant.IPC.DATEDEFAULT;
            }
            string uCreateDate = DateTime.Now.ToString("dd/MM/yyyy");
            string tokenID = "";
            string tokenIssueDate = "01/01/1900";
            string smsOTP = "";

            string IBUserName = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtIBUserName.Text.Trim());
            //string IBPassword = Encryption.Encrypt(txtIBPass.Text.Trim());
            pwdreset = Encryption.Encrypt(txtIBPass.Text.Trim());
            string IBPassword = txtIBPass.Text.Trim() == string.Empty ? string.Empty : SmartPortal.SEMS.O9Encryptpass.sha_sha256(txtIBPass.Text.Trim(), SmartPortal.Common.Utilities.Utility.KillSqlInjection(USERID.Trim()));
            string IBStatus = ddlStatusIB.SelectedValue;

            string SMSPhoneNo = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtSMSPhoneNo.Text.Trim());
            string SMSDefaultAcctno = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlSMSDefaultAcctno.SelectedValue.Trim());
            string SMSStatus = ddlStatusSMS.SelectedValue;

            string MBPhoneNo = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtReMobi.Text.Trim());
            //string MBPhoneNo = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtMBPhoneNo.Text.Trim());
            //string MBPass = Encryption.Encrypt(txtMBPass.Text.Trim());
            txtMBPass.Text = txtMBPass.Text.Trim() == string.Empty ? txtIBPass.Text.Trim() : txtMBPass.Text.Trim();
            string MBPass = txtMBPass.Text.Trim() == string.Empty ? string.Empty : SmartPortal.SEMS.O9Encryptpass.sha_sha256(txtMBPass.Text.Trim(), SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtMBPhoneNo.Text.Trim()));
            string MBStatus = ddlStatusMB.SelectedValue;

            string PHOPhoneNo = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtPHOPhoneNo.Text.Trim());
            string PHOPass = Encryption.Encrypt(txtPHOPass.Text.Trim());
            string PHOStatus = ddlStatusPHO.SelectedValue;



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
            }

            #region Tạo 1 bảng tổng hợp 3 user trên
            DataTable tblSUM = new DataTable();

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
            DataColumn colPHOPhone = new DataColumn("colPHOPhone");
            DataColumn colPHOPass = new DataColumn("colPHOPass");
            DataColumn colPHODefaultAcctno = new DataColumn("colPHODefaultAcctno");
            DataColumn colAccount = new DataColumn("colAccount");
            DataColumn colRole = new DataColumn("colRole");
            DataColumn colRoleIDS = new DataColumn("colRoleID");
            DataColumn colTranCodeS = new DataColumn("colTranCode");
            DataColumn colTranCodeID = new DataColumn("colTranCodeID");
            DataColumn colServiceIDS = new DataColumn("colServiceID");

            tblSUM.Columns.Add(colFullName);
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
            tblSUM.Columns.Add(colPHOPhone);
            tblSUM.Columns.Add(colPHOPass);
            tblSUM.Columns.Add(colPHODefaultAcctno);
            tblSUM.Columns.Add(colAccount);
            tblSUM.Columns.Add(colRole);
            tblSUM.Columns.Add(colRoleIDS);
            tblSUM.Columns.Add(colTranCodeS);
            tblSUM.Columns.Add(colTranCodeID);
            tblSUM.Columns.Add(colServiceIDS);

            //lấy thông tin trong bảng QUAN TRI
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
                rowNguoiUyQuyen["colPHOPhone"] = dongCTK["colPHOPhone"].ToString();
                rowNguoiUyQuyen["colPHOPass"] = dongCTK["colPHOPass"].ToString();
                rowNguoiUyQuyen["colPHODefaultAcctno"] = dongCTK["colPHODefaultAcctno"].ToString();
                rowNguoiUyQuyen["colAccount"] = dongCTK["colAccount"].ToString();
                rowNguoiUyQuyen["colRole"] = dongCTK["colRole"].ToString();
                rowNguoiUyQuyen["colRoleID"] = dongCTK["colRoleID"].ToString();
                rowNguoiUyQuyen["colTranCode"] = dongCTK["colTranCode"].ToString();
                rowNguoiUyQuyen["colTranCodeID"] = dongCTK["colTranCodeID"].ToString();
                rowNguoiUyQuyen["colServiceID"] = dongCTK["colServiceID"].ToString();

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
            DataRow[] arrIBR = tblSUM.Select("colServiceID='" + SmartPortal.Constant.IPC.IB + "'");
            foreach (DataRow rIBR in arrIBR)
            {
                if (tblIbankUserRight.Select("colIBUserNameRight='" + USERID + "' and colIBRoleID='" + rIBR["colRoleID"].ToString() + "'").Length == 0 && rIBR["colServiceID"].Equals("IB"))
                {
                    DataRow newRowIBR = tblIbankUserRight.NewRow();
                    newRowIBR["colIBUserNameRight"] = USERID;
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
                    newRowSMSR["colSMSUserID"] = USERID;
                    newRowSMSR["colSMSRoleID"] = rSMSR["colRoleID"].ToString();

                    tblSMSUserRight.Rows.Add(newRowSMSR);
                }
            }
            #endregion

            #region Tạo bảng chứa quyền user MB
            //tao bang chua thong tin customer
            DataTable tblMBUserRight = new DataTable();
            DataColumn colMBPhoneNoR = new DataColumn("colMBPhoneNo");
            DataColumn colMBRoleID = new DataColumn("colMBRoleID");
            DataColumn colMBContractType = new DataColumn("colMBContractType");

            //add vào table
            tblMBUserRight.Columns.Add(colMBPhoneNoR);
            tblMBUserRight.Columns.Add(colMBRoleID);
            tblMBUserRight.Columns.Add(colMBContractType);

            //tao 1 dong du lieu
            DataRow[] arrMBR = tblSUM.Select("colServiceID='" + SmartPortal.Constant.IPC.MB + "'");
            foreach (DataRow rMBR in arrMBR)
            {
                if (tblMBUserRight.Select("colMBPhoneNo='" + rMBR["colMBPhone"].ToString() + "' and colMBRoleID='" + rMBR["colRoleID"].ToString() + "'").Length == 0)
                {
                    DataRow newRowMBR = tblMBUserRight.NewRow();
                    newRowMBR["colMBPhoneNo"] = USERID;
                    newRowMBR["colMBRoleID"] = rMBR["colRoleID"].ToString();
                    newRowMBR["colMBContractType"] = "IND";
                    tblMBUserRight.Rows.Add(newRowMBR);
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
                        newRowTR["colUserID"] = USERID;
                        newRowTR["colAcctNoTR"] = rTR["colAccount"].ToString();
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
            DataColumn colDefault = new DataColumn("colDefault");
            DataColumn colStatus = new DataColumn("colStatus");

            //add vào table
            tblUserAccount.Columns.Add(colUserIDUC);
            tblUserAccount.Columns.Add(colAcctNoUC);
            tblUserAccount.Columns.Add(colRoleIDUC);
            tblUserAccount.Columns.Add(colUseFull);
            tblUserAccount.Columns.Add(colDesc);
            tblUserAccount.Columns.Add(colDefault);
            tblUserAccount.Columns.Add(colStatus);

            DataTable useraccbank = ViewState["ACCOUNTROLE"] != null ? (DataTable)ViewState["ACCOUNTROLE"] : CreateTableAccontRole();

            foreach (DataRow item in useraccbank.Rows)
            {
                DataRow newRowUA = tblUserAccount.NewRow();
                newRowUA["colUserIDUC"] = USERID;
                newRowUA["colAcctNoUC"] = item["colAccount"].ToString();
                newRowUA["colRoleIDUC"] = item["colRoleID"].ToString();
                newRowUA["colUseFull"] = "N";
                newRowUA["colDesc"] = "";
                newRowUA["colDefault"] = "N";
                newRowUA["colStatus"] = "Y";

                tblUserAccount.Rows.Add(newRowUA);
            }
            DataTable useraccwl = ViewState["ACCOUNTROLEWL"] != null ? (DataTable)ViewState["ACCOUNTROLEWL"] : CreateTableAccontRole();

            if (isWallet.Checked)
            {
                foreach (DataRow item in useraccwl.Rows)
                {
                    DataRow newRowUA = tblUserAccount.NewRow();
                    newRowUA["colUserIDUC"] = USERID;
                    newRowUA["colAcctNoUC"] = item["colAccount"].ToString();
                    newRowUA["colRoleIDUC"] = item["colRoleID"].ToString();
                    newRowUA["colUseFull"] = "N";
                    newRowUA["colDesc"] = "";
                    newRowUA["colDefault"] = "N";
                    newRowUA["colStatus"] = "Y";
                    tblUserAccount.Rows.Add(newRowUA);
                }
            }

            #endregion

            //HUNGLT
            #region tao bang EBA_CM_UserGroup
            DataTable tblCMUserGroup = new DataTable();
            DataColumn colCMUserID = new DataColumn("colCMUserID");
            DataColumn colCMGroupID = new DataColumn("colCMGroupID");

            //add vào table
            tblCMUserGroup.Columns.Add(colCMUserID);
            tblCMUserGroup.Columns.Add(colCMGroupID);
            DataRow newCMUserRow = tblCMUserGroup.NewRow();
            newCMUserRow["colCMUserID"] = USERID;
            newCMUserRow["colCMGroupID"] = ddlGroupUser.SelectedValue;
            tblCMUserGroup.Rows.Add(newCMUserRow);
            #endregion


            //HUNGLT
            #region tao bang WAL_CONTRACT_WALLET
            DataTable tblWallet = new DataTable();
            DataColumn colWalContractNo = new DataColumn("colWalContractNo");
            DataColumn colWalWalletID = new DataColumn("colWalWalletID");
            DataColumn colWalCCYID = new DataColumn("colWalCCYID");
            //AllRole
            //add vào table
            tblWallet.Columns.Add(colWalContractNo);
            tblWallet.Columns.Add(colWalWalletID);
            tblWallet.Columns.Add(colWalCCYID);

            foreach (TreeNode tvWa in tvWallet.Nodes)
            {

                foreach (TreeNode tv in tvWa.ChildNodes)
                {
                    DataRow newWalletRow = tblWallet.NewRow();
                    newWalletRow["colWalContractNo"] = USERID;
                    newWalletRow["colWalWalletID"] = "";
                    newWalletRow["colWalCCYID"] = "MMK";
                    tblWallet.Rows.Add(newWalletRow);
                }

            }

            #endregion


            switch (ACTION)
            {
                case SmartPortal.Constant.IPC.ADD:
                case "ADD":
                    #region insert user

                    new SmartPortal.SEMS.User().Insert(contractNo, endDate, lastModify, userCreate, userLastModify, userApprove, status, userID,
                        userFullName, userGender, userAddress, userEmail, userPhone, uCreateDate, userCreate, userType, userLevel, TYPEID,
                        SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["did"].ToString().Trim(),
                        tokenID, tokenIssueDate, smsOTP, userBirthday, IBUserName, IBPassword, IBStatus, SMSPhoneNo, SMSStatus, SMSDefaultAcctno,
                        ddlDefaultLang.SelectedValue, (cbIsDefault.Checked == true ? "Y" : "N"), MBPhoneNo, IBPassword, MBStatus, PHOPhoneNo, PHOPass,
                        PHOStatus, ddlPHODefaultAcctno.SelectedValue, tblIbankUserRight, tblSMSUserRight, tblMBUserRight, tblPHOUserRight, tblCMUserGroup,
                        tblTranrightDetail, tblUserAccount, ddlpolicyIB.SelectedValue.ToString(), "0", ddlpolicyMB.SelectedValue.ToString(), pwdreset, pwdreset,
                        ref IPCERRORCODE, ref IPCERRORDESC);

                    if (IPCERRORCODE == "0")
                    {
                        ViewState["NGUOIQUANTRI"] = null;
                        ltrError.Text = Resources.labels.addusersuccessfull;
                        txtReBirth.Enabled = false;
                        txtReFullName.Enabled = false;
                        txtReMobi.Enabled = false;
                        ddlUserType.Enabled = false;
                        txtReEmail.Enabled = false;
                        ddlReGender.Enabled = false;
                        ddlGroupUser.Enabled = false;
                        txtReAddress.Enabled = false;
                        txtIBUserName.Enabled = false;
                        txtSMSPhoneNo.Enabled = false;
                        txtMBPhoneNo.Enabled = false;
                        txtPHOPhoneNo.Enabled = false;
                        btsaveandcont.Visible = false;
                        btnThemNQT.Visible = false;
                        btnHuy.Visible = false;
                        tvIBQT.Enabled = false;
                        tvMBQT.Enabled = false;
                        tvPHOQT.Enabled = false;
                        tvSMSQT.Enabled = false;
                        txtIBPass.Enabled = false;
                        txtIBRePass.Enabled = false;
                        ddlStatusIB.Enabled = false;
                        ddlpolicyIB.Enabled = false;
                        ddlDefaultLang.Enabled = false;
                        ddlSMSDefaultAcctno.Enabled = false;
                        ddlStatusSMS.Enabled = false;
                        cbIsDefault.Enabled = false;
                        txtMBPass.Enabled = false;
                        txtMBRePass.Enabled = false;
                        ddlStatusMB.Enabled = false;
                        ddlpolicyMB.Enabled = false;
                        txtPHOPass.Enabled = false;
                        txtPHORepass.Enabled = false;
                        ddlPHODefaultAcctno.Enabled = false;
                        ddlStatusPHO.Enabled = false;
                        ddlGroupUser.Enabled = false;
                        SendInfoLogin(txtIBUserName.Text.Trim(), txtReMobi.Text.Trim(), txtReEmail.Text.Trim(), txtIBPass.Text.Trim(), userFullName, "CTK", "");
                    }
                    else
                    {
                        ViewState["NGUOIQUANTRI"] = null;
                        throw new IPCException(SmartPortal.Constant.IPC.ERRORCODE.ADDUSERERROR);
                    }

                    #endregion

                    break;
                case SmartPortal.Constant.IPC.EDIT:
                case "EDIT":

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
                    #region update user
                    string ibpassnew = SmartPortal.SEMS.O9Encryptpass.sha_sha256(txtIBPass.Text.Trim(), SmartPortal.Common.Utilities.Utility.KillSqlInjection(USERID.Trim()));
                    string mbpassnew = SmartPortal.SEMS.O9Encryptpass.sha_sha256(txtMBPass.Text.Trim(), SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtMBPhoneNo.Text.Trim()));

                    new SmartPortal.SEMS.User().Update(contractNo, userID, userFullName, status, userGender, userAddress, userEmail, userPhone,
                        userCreate, userType, userLevel, TYPEID, deptID, tokenID, smsOTP, userBirthday, IBUserName, IBPassword, ddlStatusIB.SelectedValue,
                        lastModify, SMSPhoneNo, SMSDefaultAcctno, (cbIsDefault.Checked == true ? "Y" : "N"), ddlStatusSMS.SelectedValue,
                        ddlDefaultLang.SelectedValue, MBPhoneNo, IBPassword, ddlStatusMB.SelectedValue, PHOPhoneNo, Encryption.Encrypt(txtPHOPass.Text.Trim()),
                        ddlStatusPHO.SelectedValue, ddlPHODefaultAcctno.SelectedValue, tblIbankUserRight, tblSMSUserRight, tblMBUserRight, tblPHOUserRight, tblCMUserGroup,
                        tblTranrightDetail, tblUserAccount, null, ddlpolicyIB.SelectedValue.ToString(), "0", ddlpolicyMB.SelectedValue.ToString(), string.Empty,
                        ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE == "0")
                    {
                        ViewState["NGUOIQUANTRI"] = null;
                        ltrError.Text = Resources.labels.editusersuccessfull;
                        txtReBirth.Enabled = false;
                        txtReFullName.Enabled = false;
                        txtReMobi.Enabled = false;
                        ddlUserType.Enabled = false;
                        txtReEmail.Enabled = false;
                        ddlReGender.Enabled = false;
                        ddlGroupUser.Enabled = false;
                        txtReAddress.Enabled = false;
                        txtIBUserName.Enabled = false;
                        txtSMSPhoneNo.Enabled = false;
                        txtMBPhoneNo.Enabled = false;
                        txtPHOPhoneNo.Enabled = false;
                        btsaveandcont.Visible = false;
                        btnThemNQT.Visible = false;
                        btnHuy.Visible = false;
                        tvIBQT.Enabled = false;
                        tvMBQT.Enabled = false;
                        tvPHOQT.Enabled = false;
                        tvSMSQT.Enabled = false;
                        txtIBPass.Enabled = false;
                        txtIBRePass.Enabled = false;
                        ddlStatusIB.Enabled = false;
                        ddlpolicyIB.Enabled = false;
                        ddlDefaultLang.Enabled = false;
                        ddlSMSDefaultAcctno.Enabled = false;
                        ddlStatusSMS.Enabled = false;
                        cbIsDefault.Enabled = false;
                        txtMBPass.Enabled = false;
                        txtMBRePass.Enabled = false;
                        ddlStatusMB.Enabled = false;
                        ddlpolicyMB.Enabled = false;
                        txtPHOPass.Enabled = false;
                        txtPHORepass.Enabled = false;
                        ddlPHODefaultAcctno.Enabled = false;
                        ddlStatusPHO.Enabled = false;
                        ddlGroupUser.Enabled = false;
                    }
                    else
                    {
                        ViewState["NGUOIQUANTRI"] = null;
                        throw new IPCException(SmartPortal.Constant.IPC.ERRORCODE.UPDATEUSERERROR);
                    }
                    #endregion
                    break;
            }

        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(IPCex.Message, this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(IPCex.Message, Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSUser_Widget", "btsaveandcont_Click", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void btback_Click(object sender, EventArgs e)
    {
        ViewState["NGUOIQUANTRI"] = null;
        RedirectBackToMainPage();
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
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSUser_Widget", "btsaveandcont_Click", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    void GetRoleDefault(TreeView treeIB, TreeView treeSMS, TreeView treeMB, TreeView treePHO, string contractNo)
    {
        string productID = string.Empty;
        DataTable dataTable = (new SmartPortal.SEMS.Contract().GetContractByContractNo(contractNo, ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
        if (dataTable.Rows.Count > 0)
        {
            productID = dataTable.Rows[0]["PRODUCTID"].ToString();
        }
        DataSet ds = new DataSet();
        DataTable tblRoleDefault = new DataTable();
        ds = new SmartPortal.SEMS.Role().GetRoleDefaultByServiceID(SmartPortal.Constant.IPC.IB, productID, ref IPCERRORCODE, ref IPCERRORDESC);
        if (!DataSetIsEmpty(ds))
        {
            tblRoleDefault = ds.Tables[0];
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
        }


        //lay role mac dinh SMS
        ds = new SmartPortal.SEMS.Role().GetRoleDefaultByServiceID(SmartPortal.Constant.IPC.SMS, productID, ref IPCERRORCODE, ref IPCERRORDESC);
        if (!DataSetIsEmpty(ds))
        {
            tblRoleDefault = ds.Tables[0];
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
        }



        //lay role mac dinh MB
        ds = new SmartPortal.SEMS.Role().GetRoleDefaultByServiceID(SmartPortal.Constant.IPC.MB, productID, ref IPCERRORCODE, ref IPCERRORDESC);
        if (!DataSetIsEmpty(ds))
        {
            tblRoleDefault = ds.Tables[0];
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
        }

        //lay role mac dinh PHO
        ds = new SmartPortal.SEMS.Role().GetRoleDefaultByServiceID(SmartPortal.Constant.IPC.PHO, productID, ref IPCERRORCODE, ref IPCERRORDESC);
        if (!DataSetIsEmpty(ds))
        {
            tblRoleDefault = ds.Tables[0];
            foreach (TreeNode liPHO in treePHO.Nodes)
            {
                if (tblRoleDefault.Select("ROLEID=" + liPHO.Value).Length != 0)
                {
                    liPHO.Checked = true;

                    //check node con (Trancode)
                    foreach (TreeNode tnTranCode in liPHO.ChildNodes)
                    {
                        tnTranCode.Checked = true;
                    }
                }
                else
                {
                    liPHO.Checked = false;
                }
            }
        }
    }
    bool DataSetIsEmpty(DataSet dataSet)
    {
        foreach (DataTable table in dataSet.Tables)
            if (table.Rows.Count != 0) return false;

        return true;
    }

    protected void lstDept_OnSelectedIndexChangedxChanged(object sender, EventArgs e)
    {
        try
        {
            if (tbAllAccount.AsEnumerable().Where(x => x.Field<string>("ACCOUNTTYPE") == "WL" && x.Field<string>("DATAValue") == lstDept.SelectedValue.ToString().Trim()).Any())
            {
                tvRole.Visible = false;
                tvWallet.Visible = true;

            }
            else
            {
                tvRole.Visible = true;
                tvWallet.Visible = false;

            }
            foreach (TreeNode node in tvRole.Nodes)
            {
                node.Checked = false;
            }
            if (true)
            {

            }

            if (tvWallet.Visible)
            {
                LoadAccountRole(USERID, lstDept.SelectedItem.ToString().Trim());
            }
            else
            {
                LoadAccountRole(USERID, lstDept.SelectedValue.ToString().Trim());
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSSetLimitTeller_Widget", "lstDept_SelectedIndexChanged", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    }

    #region MyRegion

    #endregion
    protected void tvRole_OnTreeNodeCheckChanged(object sender, TreeNodeEventArgs e)
    {
        DataTable tblAccountRole = ViewState["ACCOUNTROLE"] != null ? (DataTable)ViewState["ACCOUNTROLE"] : CreateTableAccontRole();
        //add row moi
        string role = string.Empty;
        foreach (TreeNode node in tvRole.Nodes)
        {
            role = node.Value;
            if (node.Checked)
            {
                AddAccountRole(USERID, lstDept.SelectedValue.Trim(), role, string.Empty, CONTRACTNO);
            }
            else
            {
                RemoteAccountRole(USERID, lstDept.SelectedValue.Trim(), role, string.Empty, CONTRACTNO);
            }
        }
    }


    protected void tvWl_OnTreeNodeCheckChanged(object sender, TreeNodeEventArgs e)
    {
        DataTable tblAccountRole = ViewState["ACCOUNTROLEWL"] != null ? (DataTable)ViewState["ACCOUNTROLEWL"] : CreateTableAccontRole();
        //add row moi
        string role = string.Empty;
        foreach (TreeNode node in tvWallet.Nodes)
        {
            role = node.Value;
            if (node.Checked)
            {
                AddAccountRoleWL(USERID, lstDept.SelectedValue.Trim(), role, string.Empty, CONTRACTNO);
            }
            else
            {
                RemoteAccountRoleWL(USERID, lstDept.SelectedValue.Trim(), role, string.Empty, CONTRACTNO);
            }
        }

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
    DataTable LoadAllAccount()
    {
        DataSet ds = new DataSet();
        ds = new SmartPortal.IB.Customer().GetAllAcc(Session["userID"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
        if (IPCERRORCODE != "0")
        {
            throw new IPCException(SmartPortal.Constant.IPC.ERRORCODE.IPC);
        }
        DataTable dtAccount = new DataTable();
        dtAccount = ds.Tables[0];
        ViewState["ALLACCOUNT"] = dtAccount;
        return dtAccount;
    }
    private void AddAccountRole(string userID, string account, string roleID, string serviceID, string contractNo)
    {
        try
        {
            DataTable tblAccountRole = ViewState["ACCOUNTROLE"] != null ? (DataTable)ViewState["ACCOUNTROLE"] : CreateTableAccontRole();
            if (tblAccountRole.Rows.Count.Equals(0))
            {
                DataRow rowUser = CreateTableAccontRole().NewRow();
                rowUser["colUserID"] = userID;
                rowUser["colAccount"] = account;
                rowUser["colRoleID"] = roleID;
                rowUser["colServiceID"] = serviceID;
                rowUser["colContractNo"] = contractNo;
                tblAccountRole.Rows.Add(rowUser.ItemArray);
                ViewState["ACCOUNTROLE"] = tblAccountRole;
            }
            foreach (DataRow dr in tblAccountRole.Rows)
            {
                if (!(dr["colAccount"].ToString().Equals(account) && dr["colRoleID"].ToString().Equals(roleID) && dr["colUserID"].ToString().Equals(userID)))
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
                if (dr["colAccount"].ToString() == account && dr["colRoleID"].ToString() == roleID && dr["colUserID"].ToString() == userID)
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

    private void AddAccountRoleWL(string userID, string account, string roleID, string serviceID, string contractNo)
    {
        try
        {
            DataTable tblAccountRole = ViewState["ACCOUNTROLEWL"] != null ? (DataTable)ViewState["ACCOUNTROLEWL"] : CreateTableAccontRole();
            if (tblAccountRole.Rows.Count.Equals(0))
            {
                DataRow rowUser = CreateTableAccontRole().NewRow();
                rowUser["colUserID"] = userID;
                rowUser["colAccount"] = account;
                rowUser["colRoleID"] = roleID;
                rowUser["colServiceID"] = serviceID;
                rowUser["colContractNo"] = contractNo;
                tblAccountRole.Rows.Add(rowUser.ItemArray);
                ViewState["ACCOUNTROLEWL"] = tblAccountRole;
            }
            foreach (DataRow dr in tblAccountRole.Rows)
            {
                if (!(dr["colAccount"].ToString().Equals(account) && dr["colRoleID"].ToString().Equals(roleID) && dr["colUserID"].ToString().Equals(userID)))
                {
                    DataRow rowUser = CreateTableAccontRole().NewRow();
                    rowUser["colUserID"] = userID;
                    rowUser["colAccount"] = account;
                    rowUser["colRoleID"] = roleID;
                    rowUser["colServiceID"] = serviceID;
                    rowUser["colContractNo"] = contractNo;
                    tblAccountRole.Rows.Add(rowUser.ItemArray);
                    ViewState["ACCOUNTROLEWL"] = tblAccountRole;
                    return;
                }
            }
        }
        catch (Exception ex)
        {

        }

    }


    private void RemoteAccountRoleWL(string userID, string account, string roleID, string serviceID, string contractNo)
    {
        try
        {
            DataTable tblAccountRole = ViewState["ACCOUNTROLEWL"] != null ? (DataTable)ViewState["ACCOUNTROLEWL"] : CreateTableAccontRole();

            foreach (DataRow dr in tblAccountRole.Rows)
            {
                if (dr["colAccount"].ToString() == account && dr["colRoleID"].ToString() == roleID && dr["colUserID"].ToString() == userID)
                {
                    dr.Delete();
                }
            }
            tblAccountRole.AcceptChanges();
            ViewState["ACCOUNTROLEWL"] = tblAccountRole;
        }
        catch
        {

        }

    }

    void LoadDataALLRoleInTreeview(string serviceID, TreeView tvPage, string userType, string roleType)
    {
        tvPage.Nodes.Clear();
        DataTable tblSS = new DataTable();
        tblSS = new SmartPortal.SEMS.Role().GetByServiceAndUserType(serviceID, userType, PRODUCTID, roleType);
        ViewState["AllRole"] = tblSS;
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
        //if (!tvPage.Equals(tvRole))
        //{
        //    foreach (TreeNode node in tvRole.Nodes)
        //    {
        //        node.Checked = true;
        //    }
        //}
        tvPage.Attributes.Add("onclick", "OnTreeClick(event)");
        tvRole.Attributes.Add("onclick", "postBackByObject()");
        tvWallet.Attributes.Add("onclick", "postBackByObject()");
    }
    void LoadDataALLRoleInTreeviewWAL(string serviceID, TreeView tvPage, string userType, string roleType)
    {
        tvPage.Nodes.Clear();
        DataTable tblSS = new DataTable();
        tblSS = new SmartPortal.SEMS.Role().GetByServiceAndUserType(serviceID, userType, PRODUCTID, roleType);
        ViewState["AllRoleWal"] = tblSS;
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
        tvRole.Attributes.Add("onclick", "postBackByObject()");
        tvWallet.Attributes.Add("onclick", "postBackByObject()");
    }

    private void LoadAccountRole(string userID, string account)
    {
        try
        {
            DataTable tblAccountRole = ViewState["ACCOUNTROLE"] != null ? (DataTable)ViewState["ACCOUNTROLE"] : CreateTableAccontRole();
            DataTable tblAccountRoleWL = ViewState["ACCOUNTROLEWL"] != null ? (DataTable)ViewState["ACCOUNTROLEWL"] : CreateTableAccontRole();
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
            foreach (DataRow dr in tblAccountRoleWL.Rows)
            {
                if (dr["colAccount"].ToString().Equals(account) && dr["colUserID"].ToString().Equals(userID))
                {
                    foreach (TreeNode node in tvWallet.Nodes)
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
    private static int NumberReg = 0;
    private string GenerateUserName(TextBox textBox)
    {
        NumberReg++;
        string resultUserName = new SmartPortal.SEMS.User().UsernameGeneration("6", ref IPCERRORCODE, ref IPCERRORDESC);
        if (resultUserName.Equals(string.Empty) || resultUserName.Length != 10 || !CheckUserNameExists(resultUserName, textBox))
        {
            if (NumberReg < 10)
            {
                return GenerateUserName(textBox);
            }
            else
            {
                return string.Empty;
            }
        }
        return resultUserName;
    }

    private bool CheckUserNameExists(string userName, TextBox textBox)
    {
        DataSet ds = new SmartPortal.SEMS.User().CheckUserName("EBA_Users_CheckUserNameMB", new object[] { userName, SmartPortal.Constant.IPC.CONTRACTINDIVIDUAL }, ref IPCERRORCODE, ref IPCERRORDESC);
        if (IPCERRORCODE != "0")
        {
            return false;
        }
        if (!validateusername(textBox))
        {
            return false;
        }
        return true;
    }

    private bool validateusername(TextBox tx)
    {
        string usernamepattern = System.Configuration.ConfigurationManager.AppSettings["validateusername"].ToString();
        if (!(System.Text.RegularExpressions.Regex.IsMatch(tx.Text, usernamepattern)))
        {
            tx.Focus();
            return false;

        }
        return true;
    }

    protected void cbwallet_OnCheckedChanged(object sender, EventArgs e)
    {


        DataTable dtAccountQT = tbAllAccount;


        if (isWallet.Checked)
        {
            tvWallet.Visible = true;
            lstDept.DataSource = dtAccountQT;
            lstDept.DataTextField = "DATAText";
            lstDept.DataValueField = "DATAValue";
            lstDept.DataBind();
            lstDept.SelectedValue = dtAccountQT.Rows[0]["DATAValue"].ToString();
            LoadAccountRole(USERID, lstDept.SelectedValue.ToString().Trim());
            if (dtAccountQT.AsEnumerable().Where(x => x.Field<string>("ACCOUNTTYPE") == "WL" && x.Field<string>("DATAValue") == lstDept.SelectedValue.ToString().Trim()).Any())
            {
                tvRole.Visible = false;
                tvWallet.Visible = false;
            }
            else
            {
                tvRole.Visible = true;
                tvWallet.Visible = false;
            }
        }
        else
        {
            var dataRow = dtAccountQT.AsEnumerable().Where(x => x.Field<string>("ACCOUNTTYPE") != "WL").CopyToDataTable<DataRow>();
            tvWallet.Visible = false;
            lstDept.DataSource = dataRow;
            lstDept.DataTextField = "DATAText";
            lstDept.DataValueField = "DATAValue";
            lstDept.DataBind();
            lstDept.SelectedValue = dtAccountQT.Rows[0]["DATAValue"].ToString();
            LoadAccountRole(USERID, lstDept.SelectedValue.ToString().Trim());
            if (dtAccountQT.AsEnumerable().Where(x => x.Field<string>("ACCOUNTTYPE") == "WL" && x.Field<string>("DATAValue") == lstDept.SelectedValue.ToString().Trim()).Any())
            {
                tvRole.Visible = false;
                tvWallet.Visible = true;
            }
            else
            {
                tvRole.Visible = true;
                tvWallet.Visible = false;
            }
        }
    }


    #region checkexist user IB , MB
    private bool CheckUserNameExists(string userName, string services)
    {
        try
        {
            if (services.Equals("MB"))
            {
                DataSet ds = new SmartPortal.SEMS.User().CheckUserName("EBA_Users_CheckUserNameMB", new object[] { userName, SmartPortal.Constant.IPC.CONTRACTINDIVIDUAL }, ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE != "0")
                {
                    txtMBPhoneNo.Focus();
                    return false;
                }
            }
            if (services.Equals("IB"))
            {
                DataSet ds = new SmartPortal.SEMS.User().CheckUserName("EBA_Users_CheckUserNameIB", new object[] { USERID, userName, SmartPortal.Constant.IPC.CONTRACTINDIVIDUAL }, ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE != "0")
                {
                    txtIBUserName.Focus();
                    return false;
                }
            }

            return true;
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToIBErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
            throw;
        }


    }
    #endregion


    #region CheckIsPhoneNumer 
    public bool CheckIsPhoneNumer(string phone)
    {
        string result = new SmartPortal.SEMS.User().CheckPhoneTeLCo(phone, ref IPCERRORCODE, ref IPCERRORDESC);
        if (result == SmartPortal.Constant.IPC.TRANSTATUS.BEGIN)
        {
            return true;
        }
        else
        {
            return false;
        }

    }
    #endregion
    public bool CheckExistPhoneNumber(string phone)
    {
        string resultInfo = new SmartPortal.SEMS.User().CheckPhoneNumberCustInfo(phone, SmartPortal.Constant.IPC.CORPORATECONTRACT, ref IPCERRORCODE, ref IPCERRORDESC);
        if (resultInfo.Equals("1"))
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    public bool CheckExistPhoneContract(string phone, string contract, string userid)
    {
        string resultInfo =
            new SmartPortal.SEMS.User().CheckPhoneInContract(phone, userid, contractNo, ref IPCERRORCODE,
                ref IPCERRORDESC);
        if (resultInfo.Equals("1"))
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    protected void GenUserName(object sender, EventArgs e)
    {
        try
        {
            userName = GenerateUserName(txtIBUserName);
            txtIBUserName.Text = userName;
            txtMBPhoneNo.Text = userName;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void txtIBUserName_OnTextChanged(object sender, EventArgs e)
    {
        txtMBPhoneNo.Text = txtIBUserName.Text;

    }

    public void CheckAllRole()
    {
        DataTable tblRole = ViewState["ACCOUNTROLE"] != null ? (DataTable)ViewState["ACCOUNTROLE"] : CreateTableAccontRole();
        DataTable dtAccountQT = new DataTable();
        dtAccountQT = ViewState["ALLACCOUNT"] != null ? (DataTable)ViewState["ALLACCOUNT"] : LoadAllAccount();
        string role = string.Empty;
        dtAccountQT = dtAccountQT.AsEnumerable().Where(x => x.Field<string>("ACCOUNTTYPE") != "WL").CopyToDataTable<DataRow>();
        foreach (DataRow drAct in dtAccountQT.Rows)
        {
            foreach (TreeNode node in tvRole.Nodes)
            {
                node.Checked = true;
                role = node.Value.Trim();
                AddAccountRole(USERID, drAct["DATAText"].ToString(), role, string.Empty, CONTRACTNO);
            }
        }

    }


    void SendInfoLogin(string userid, string PhoneNo, string email, string pass, string fullname, string typeID, string authenType)
    {
        try
        {
            Antlr3.ST.StringTemplate tmpl = new Antlr3.ST.StringTemplate();
            tmpl = SmartPortal.Common.ST.GetStringTemplate("IBCorpUser", "IBCreatedUser" + System.Globalization.CultureInfo.CurrentCulture.ToString());
            tmpl.SetAttribute("USER", userid);
            tmpl.SetAttribute("PASS", pass);
            tmpl.SetAttribute("FULLNAME", fullname);
            switch (typeID.Trim().ToUpper())
            {
                case "CTK":
                    tmpl.SetAttribute("TYPEID", Resources.labels.chutaikhoan);
                    break;
                case "QTHT":
                    tmpl.SetAttribute("TYPEID", Resources.labels.quantrihethong);
                    break;
                case "C2":
                    tmpl.SetAttribute("TYPEID", Resources.labels.nguoidungcap2);
                    break;
                default:
                    tmpl.SetAttribute("TYPEID", Resources.labels.chutaikhoan);
                    break;
            }
            SmartPortal.Common.EmailHelper.SendMailMessageAsync(System.Configuration.ConfigurationManager.AppSettings["contractapprovemailfrom"], email, System.Configuration.ConfigurationManager.AppSettings["contractapprovemailtitle"], tmpl.ToString());
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_IBCorpUser_Controls_Widget", "SendInfoLogin", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
}


