using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using ICSharpCode.SharpZipLib.Zip;
using SmartPortal.Common.Utilities;
using SmartPortal.Constant;
using SmartPortal.ExceptionCollection;
using SmartPortal.Model;
using SmartPortal.Security;
using SmartPortal.SEMS;

public partial class Widgets_SEMSContractList_Controls_Widget : WidgetBase
{
    string ACTION = "";
    string cn = "";
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    static string Pagecardadd = "1043";
    static string Pagecarddelete = "1044";
    public static string imgfont = string.Empty;
    public static string imgback = string.Empty;
    public static bool showNRIC = false;
    public static bool showPassport = false;
    public static bool showLicense = false;
    string cusid = string.Empty;
    SmartPortal.SEMS.Common _service = new SmartPortal.SEMS.Common();
    private static bool linkBack = false;
    private static bool syncdata = false;
    private static string StatusContract = string.Empty;
    private static string Kyctype = "NRIC";

    public string CONTRACTNO
    {
        get { return ViewState["CONTRACTNO"] != null ? (string)ViewState["CONTRACTNO"] : string.Empty; }
        set { ViewState["CONTRACTNO"] = value; }
    }

    public string CONTRACTTYPE
    {
        get { return ViewState["CONTRACTTYPE"] != null ? (string)ViewState["CONTRACTTYPE"] : string.Empty; }
        set { ViewState["CONTRACTTYPE"] = value; }
    }
    public DataTable USERTABLE
    {
        get { return ViewState["USERTABLE"] != null ? (DataTable)ViewState["USERTABLE"] : new DataTable(); }
        set { ViewState["USERTABLE"] = value; }
    }
    public bool WALLETONLY

    {
        get { return ViewState["WALLETONLY"] != null ? (bool)ViewState["WALLETONLY"] : false; }
        set { ViewState["WALLETONLY"] = value; }
    }
    public string WALLETPHONE

    {
        get { return ViewState["WALLETPHONE"] != null ? (string)ViewState["WALLETPHONE"] : string.Empty; }
        set { ViewState["WALLETPHONE"] = value; }
    }
    public string USERID
    {
        get { return ViewState["USERID"] != null ? (string)ViewState["USERID"] : string.Empty; }
        set { ViewState["USERID"] = value; }
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
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = "";

            ACTION = GetActionPage();
            CONTRACTNO = GetParamsPage(IPC.ID)[0].Trim();
            if (!IsPostBack)
            {

                linkBack = false;
                syncdata = false;
                LoadKYC();
                LoadMerchantCategoryCodes();
                radNewNRIC.Visible = showNRIC;
                radPassport.Visible = showPassport;
                radLicense.Visible = showLicense;
                divNRIC.Visible = showNRIC;
                divPassport.Visible = showPassport;
                divLicense.Visible = showLicense;
                imgback = imgfont = string.Empty;
                liTabWorkingCard.Visible = false;

                #region load contract level
                DataTable dt = new SmartPortal.SEMS.Contract().LoadContractLevelCBB(ref IPCERRORCODE, ref IPCERRORDESC).Tables[0];
                ddlContractLevel.DataSource = dt;
                ddlContractLevel.DataTextField = "CONTRACT_LEVEL_NAME";
                ddlContractLevel.DataValueField = "CONTRACT_LEVEL_ID";
                ddlContractLevel.DataBind();
                #endregion

                ddlCustType.Items.Add(new ListItem(Resources.labels.canhan, "P"));
                ddlCustType.Items.Add(new ListItem("", ""));
                //ddlGender.Items.Add(new ListItem("", ""));
                //ddlGender.Items.Add(new ListItem(Resources.labels.male, "M"));
                //ddlGender.Items.Add(new ListItem(Resources.labels.female, "F"));
                //load chi nhánh
                ddlBranchCust.DataSource = new SmartPortal.SEMS.Branch().GetAll(ref IPCERRORCODE, ref IPCERRORDESC);

                ddlBranchCust.DataTextField = "BRANCHNAME";
                ddlBranchCust.DataValueField = "BRANCHID";
                ddlBranchCust.DataBind();
                ddlBranchCust.Items.Add(new ListItem("", ""));


                DataTable dtTownship, dtRegion;
                dtTownship = new SmartPortal.SEMS.Township().GetAllTownship(ref IPCERRORCODE, ref IPCERRORDESC).Tables[0];
                ddlTownship.DataSource = dtTownship;
                ddlTownship.DataTextField = "TOWNSHIPNAME";
                ddlTownship.DataValueField = "TOWNSHIPCODE";
                ddlTownship.DataBind();

                dtRegion = new SmartPortal.SEMS.Township().GetAllRegion(ref IPCERRORCODE, ref IPCERRORDESC).Tables[0];
                ddlRegion.DataSource = dtRegion;
                ddlRegion.DataTextField = "REGIONNAME";
                ddlRegion.DataValueField = "REGIONID";
                ddlRegion.DataBind();

                #region load nation
                DataSet dsNation = new DataSet();
                object[] _object = new object[] { string.Empty, string.Empty, null, null };
                dsNation = _service.common("WAL_BO_GET_NATION", _object, ref IPCERRORCODE, ref IPCERRORDESC);

                ddlNation.DataSource = dsNation;
                ddlNation.DataTextField = "NATIONNAME";
                ddlNation.DataValueField = "NATIONCODE";
                ddlNation.DataBind();
                ddlNation.Items.Add(new ListItem("", ""));
                ddlNation.SelectedValue = "MM";

                ddlContryPassport.DataSource = dsNation;
                ddlContryPassport.DataTextField = "NATIONNAME";
                ddlContryPassport.DataValueField = "NATIONCODE";
                ddlContryPassport.DataBind();

                ddlContryPassport.SelectedValue = "MM";

                if (dsNation.Tables[0].Rows.Count == 0)
                {
                    ddlNation.Items.Insert(0, new ListItem("Không tồn tại quốc gia", ""));
                }
                #endregion


                #region hien thị status
                ddlStatus.Items.Add(new ListItem(Resources.labels.connew, SmartPortal.Constant.IPC.NEW));
                ddlStatus.Items.Add(new ListItem(Resources.labels.conactive, SmartPortal.Constant.IPC.ACTIVE));
                ddlStatus.Items.Add(new ListItem(Resources.labels.condelete, SmartPortal.Constant.IPC.DELETE));
                ddlStatus.Items.Add(new ListItem(Resources.labels.conblock, SmartPortal.Constant.IPC.BLOCK));
                ddlStatus.Items.Add(new ListItem(Resources.labels.conpending, SmartPortal.Constant.IPC.PENDING));
                ddlStatus.Items.Add(new ListItem(Resources.labels.conreject, SmartPortal.Constant.IPC.REJECT));
                ddlStatus.Items.Add(new ListItem(Resources.labels.pendingforapprove, SmartPortal.Constant.IPC.PENDINGFORAPPROVE));
                ddlStatus.Items.Insert(0, new ListItem(Resources.labels.all, SmartPortal.Constant.IPC.ALL));
                #endregion


                #region load branch
                DataSet dsBranch = new DataSet();
                dsBranch = new SmartPortal.SEMS.Branch().LoadBranchByCBB("", "", ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE == "0")
                {
                    ddlBranch.DataSource = dsBranch;
                    ddlBranch.DataTextField = "BRANCHNAME";
                    ddlBranch.DataValueField = "BRANCHID";
                    ddlBranch.DataBind();
                }
                else
                {
                    throw new IPCException(IPCERRORDESC);
                }
                #endregion

                BindData();
            }
            loadCombobox_KYCDocumentName();
            loadCombobox_Nation();
            if (ddlDocumentTypeImport.Items != null)
            {
                if (ddlDocumentTypeImport.SelectedItem != null)
                {
                    txtDocumentNameImport.Text = ddlDocumentTypeImport.SelectedItem.Text.Trim();
                }
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

    void BindData()
    {
        try
        {
            //enable(disable) theo action
            switch (ACTION)
            {
                case IPC.ACTIONPAGE.DETAILS:
                    cbLINE.Enabled = cbSMS.Enabled = cbTELE.Enabled = cbWAPP.Enabled = false;
                    ddlBranch.Enabled = false;
                    txtcontractno.Enabled = false;
                    ddlMerchantCategory.Enabled = false;
                    ddlUserType.Enabled = false;
                    txtcustname.Enabled = false;
                    txtenddate.Enabled = false;
                    txtmodifydate.Enabled = false;
                    txtopendate.Enabled = false;
                    ddlProductType.Enabled = false;
                    ddlStatus.Enabled = false;
                    ddlContractLevel.Enabled = false;
                    txtuserapprove.Enabled = false;
                    txtusercreate.Enabled = false;
                    txtuserlastmodify.Enabled = false;
                    ddlSubUserType.Enabled = false;
                    cbIsReceiver.Enabled = false;
                    ddlRegion.Enabled = false;
                    ddlTownship.Enabled = false;
                    btSave.Visible = false;
                    chkRenew.Enabled = false;
                    pnCard.Visible = false;
                    // KienVT - Load KYC Information
                    btSaveDocument.Visible = false;
                    pnImportNewDocument.Enabled = false;
                    btnImport.Visible = false;
                    documentUpload.Visible = false;
                    pnLinkBank.Visible = false;
                    break;
                case IPC.ACTIONPAGE.EDIT:
                    ddlBranch.Enabled = false;
                    txtcontractno.Enabled = false;
                    ddlUserType.Enabled = false;
                    txtcustname.Enabled = false;
                    txtenddate.Enabled = true;
                    txtmodifydate.Enabled = false;
                    txtopendate.Enabled = true;
                    ddlProductType.Enabled = true;
                    ddlRegion.Enabled = false;
                    ddlTownship.Enabled = false;
                    ddlStatus.Enabled = false;
                    ddlMerchantCategory.Enabled = true;
                    txtuserapprove.Enabled = false;
                    txtusercreate.Enabled = false;
                    txtuserlastmodify.Enabled = false;
                    btSave.Visible = true;
                    pnCard.Visible = true;
                    break;
            }
            #region Lấy thông tin hợp đồng
            DataTable contractTable = new DataTable();
            contractTable = (new SmartPortal.SEMS.Contract().GetContractByContractNo(CONTRACTNO, ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
            cusid = contractTable.Rows[0]["CUSTID"].ToString();
            loadData_Repeater_KYC_Info();
            loadData_Repeater();
            LoadRejectReason(CONTRACTNO, "CONTRACT");
            if (contractTable.Rows.Count != 0)
            {
                #region load san pham

                #region load KYC
                DataTable KYCTable = new DataTable();
                KYCTable = new SmartPortal.SEMS.Contract().LoaKYCInfor(contractTable.Rows[0]["USERID"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC).Tables[0];

                #endregion

                #endregion



                ddlBranch.SelectedValue = contractTable.Rows[0]["BRANCHID"].ToString();
                ddlRegion.SelectedValue = contractTable.Rows[0]["REGION"].ToString();
                ddlTownship.SelectedValue = contractTable.Rows[0]["TOWNSHIP"].ToString();

                txtcontractno.Text = contractTable.Rows[0]["CONTRACTNO"].ToString();
                //quantxa 15/04/2022 charge fee load list accounts
                //ucChargefee.loadAccount(contractTable.Rows[0]["CONTRACTNO"].ToString());

                hfSubUserType.Value = contractTable.Rows[0]["USERTYPE"].ToString();
                string userType = LoadSubUserType(contractTable.Rows[0]["USERTYPE"].ToString());
                CONTRACTTYPE = contractTable.Rows[0]["CONTRACTTYPE"].ToString();
                LoadUserType(contractTable.Rows[0]["CONTRACTTYPE"].ToString());
                hfSubUserTypeOld.Value = contractTable.Rows[0]["USERTYPE"].ToString();
                switch (userType)
                {
                    case "A":
                        hfuserType.Value = "01";
                        ddlUserType.SelectedValue = SmartPortal.Constant.IPC.AGENT;
                        LoadSubUserTypeByUserType(SmartPortal.Constant.IPC.AGENT);
                        ddlSubUserType.SelectedValue = hfSubUserType.Value;
                        break;
                    case "M":
                        hfuserType.Value = "03";
                        ddlUserType.SelectedValue = SmartPortal.Constant.IPC.MERCHANT;
                        LoadSubUserTypeByUserType(SmartPortal.Constant.IPC.MERCHANT);
                        ddlSubUserType.SelectedValue = hfSubUserType.Value;
                        break;
                    case "C":
                        hfuserType.Value = "02";
                        ddlUserType.SelectedValue = SmartPortal.Constant.IPC.CONSUMER;
                        LoadSubUserTypeByUserType(SmartPortal.Constant.IPC.CONSUMER);
                        ddlSubUserType.SelectedValue = hfSubUserType.Value;
                        break;
                    case "CCO":
                        hfuserType.Value = "06";
                        ddlUserType.SelectedValue = SmartPortal.Constant.IPC.CORPORATECONTRACT;
                        LoadSubUserTypeByUserType(SmartPortal.Constant.IPC.CORPORATECONTRACT);
                        ddlSubUserType.SelectedValue = hfSubUserType.Value;
                        ddlProductType.Enabled = false;
                        break;
                    default:
                        hfuserType.Value = "01";
                        ddlUserType.SelectedValue = SmartPortal.Constant.IPC.AGENT;
                        LoadSubUserTypeByUserType(SmartPortal.Constant.IPC.AGENT);
                        ddlSubUserType.SelectedValue = hfSubUserType.Value;
                        break;
                }
                if (ddlUserType.SelectedValue.Equals(SmartPortal.Constant.IPC.MERCHANT))
                {
                    lblMerchantCategory.Text = ddlMerchantCategory.SelectedValue = contractTable.Rows[0]["MER_CODE"].ToString();
                    divMerchantCategory.Visible = true;
                }
                else
                {
                    divMerchantCategory.Visible = false;
                }
                LoadProductByUserType(hfSubUserType.Value);
                txtcustname.Text = contractTable.Rows[0]["FULLNAME"].ToString();
                txtenddate.Text = SmartPortal.Common.Utilities.Utility.IsDateTime2(contractTable.Rows[0]["ENDDATE"].ToString()).ToString("dd/MM/yyyy");
                txtmodifydate.Text = SmartPortal.Common.Utilities.Utility.IsDateTime2(contractTable.Rows[0]["LASTMODIFY"].ToString()).ToString("dd/MM/yyyy");
                txtopendate.Text = SmartPortal.Common.Utilities.Utility.IsDateTime2(contractTable.Rows[0]["CREATEDATE"].ToString()).ToString("dd/MM/yyyy");
                ddlProductType.SelectedValue = contractTable.Rows[0]["PRODUCTID"].ToString();
                ddlContractLevel.SelectedValue = contractTable.Rows[0]["CONTRACTLEVELID"].ToString();
                lblContractLevel.Text = contractTable.Rows[0]["CONTRACTLEVELID"].ToString();
                lblProductType.Text = contractTable.Rows[0]["PRODUCTID"].ToString();
                StatusContract = ddlStatus.SelectedValue = contractTable.Rows[0]["STATUS"].ToString();
                txtuserapprove.Text = contractTable.Rows[0]["USERAPPROVE"].ToString();
                txtusercreate.Text = contractTable.Rows[0]["USERCREATE"].ToString();
                txtuserlastmodify.Text = contractTable.Rows[0]["USERLASTMODIFY"].ToString();
                ddlRegion.SelectedValue = contractTable.Rows[0]["REGION"].ToString();
                ddlTownship.SelectedValue = contractTable.Rows[0]["TOWNSHIP"].ToString();
                txtCustCode.Text = contractTable.Rows[0]["CFCODE"].ToString();
                if (!string.IsNullOrEmpty(txtCustCode.Text.ToString()) && ACTION.Equals(IPC.ACTIONPAGE.EDIT))
                {
                    btnSync.Visible = true;
                }
                USERID = contractTable.Rows[0]["USERID"].ToString();
                if (contractTable.Rows[0]["STATUS"].ToString().Equals(SmartPortal.Constant.IPC.REJECT))
                {
                    divReject.Visible = true;
                    ddlReason.Enabled = false;
                    txtDescription.Enabled = false;
                }
                else
                {
                    ddlReason.Enabled = false;
                    divReject.Visible = false;
                }
                switch (contractTable.Rows[0]["ISRECEIVERLIST"].ToString().Trim())
                {
                    case "Y":
                        cbIsReceiver.Checked = true;
                        break;
                    case "N":
                        cbIsReceiver.Checked = false;
                        break;
                }
                switch (contractTable.Rows[0]["IsAutorenew"].ToString().Trim())
                {
                    case "Y":
                        chkRenew.Checked = true;
                        break;
                    default:
                        chkRenew.Checked = false;
                        break;
                }

                //an cac button neu hop dong bi dong
                if (contractTable.Rows[0]["STATUS"].ToString().Trim() == SmartPortal.Constant.IPC.DELETE)
                {
                    pnToolbar.Visible = false;
                }
                else
                {
                    switch (ACTION)
                    {
                        case IPC.ACTIONPAGE.DETAILS:
                            pnToolbar.Visible = false;
                            break;
                        case IPC.ACTIONPAGE.EDIT:
                            pnToolbar.Visible = true;
                            break;
                    }
                }
            }



            DataTable tblAlter = new DataTable();
            tblAlter = (new SmartPortal.SEMS.Contract().GetTransactionAlterOfContract(CONTRACTNO, ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
            if (tblAlter.Rows.Count != 0)
            {
                DataRow[] drsms = tblAlter.Select("TransactionAlert='SMS'");
                if (drsms.Length > 0)
                {
                    cbSMS.Checked = true;
                    hfsms.Value = cbSMS.Checked.ToString();
                }
                DataRow[] drWhatsApp = tblAlter.Select("TransactionAlert='WhatsApp'");
                if (drWhatsApp.Length > 0)
                {
                    cbWAPP.Checked = true;
                    hfwhatsapp.Value = cbWAPP.Checked.ToString();
                }
                DataRow[] drLine = tblAlter.Select("TransactionAlert='LINE'");
                if (drLine.Length > 0)
                {
                    cbLINE.Checked = true;
                    hfline.Value = cbLINE.Checked.ToString();
                }
                DataRow[] drTelegram = tblAlter.Select("TransactionAlert='Telegram'");
                if (drTelegram.Length > 0)
                {
                    cbTELE.Checked = true;
                    hftele.Value = cbTELE.Checked.ToString();
                }
            }

            #endregion

            #region Lấy thông tin card của hỡp đồng - vutt card
            DataTable cardTable = new DataTable();
            cardTable = (new SmartPortal.SEMS.Card().GetCardByContractNo(CONTRACTNO));

            gvCard.DataSource = cardTable;
            gvCard.DataBind();
            #endregion

            #region Lấy thông tin khách hàng
            DataTable custTable = new DataTable();
            custTable = (new SmartPortal.SEMS.Contract().GetCustomerByContractNo(CONTRACTNO, ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
            if (custTable.Rows.Count != 0)
            {
                txtcustID.Text = custTable.Rows[0]["CUSTID"].ToString();
                //lbcusttype.Text = custTable.Rows[0]["CFTYPE"].ToString();
                txtFullNameCust.Text = custTable.Rows[0]["FULLNAME"].ToString();
                txtShortName.Text = custTable.Rows[0]["SHORTNAME"].ToString();
                //HaiNT Edit birthDate (12/08/2013)
                string birthDate = SmartPortal.Common.Utilities.Utility.IsDateTime2(custTable.Rows[0]["DOB"].ToString()).ToString("dd/MM/yyyy");
                lblbirth.Text = txtBirth.Text = birthDate.Equals("01/01/1900") ? "" : birthDate;
                lblMobi.Text = txtMobi.Text = custTable.Rows[0]["TEL"].ToString();
                lblEmail.Text = txtEmail.Text = custTable.Rows[0]["EMAIL"].ToString();
                lblResidentAddress.Text = txtResidentAddress.Text = custTable.Rows[0]["ADDRRESIDENT"].ToString();
                lblTempStay.Text = txtTempStay.Text = custTable.Rows[0]["ADDRTEMP"].ToString();
                lblPassportCmdn.Text = txtPassportCmdn.Text = custTable.Rows[0]["LICENSEID"].ToString();
                lblRelease.Text = txtRelease.Text = custTable.Rows[0]["ISSUEPLACE"].ToString();
                lblddlNation.Text = custTable.Rows[0]["NATIONNAME"].ToString();
                ddlNation.SelectedValue = custTable.Rows[0]["NATION"].ToString();
                lblAddressOffice.Text = txtAddressOffice.Text = custTable.Rows[0]["OFFICEADDR"].ToString();
                lblOfficePhone.Text = txtOfficePhone.Text = custTable.Rows[0]["OFFICEPHONE"].ToString();
                lblbranchcust.Text = custTable.Rows[0]["BRANCHNAME"].ToString();
                ddlBranchCust.SelectedValue = custTable.Rows[0]["BRANCHID"].ToString().Trim();
                ddlCustType.SelectedValue = custTable.Rows[0]["CFTYPE"].ToString().Trim();
                //lblGender.Text = ddlGender.SelectedValue = custTable.Rows[0]["SEX"].ToString();
                if (custTable.Rows[0]["CTYPE"].ToString().Equals("W") && contractTable.Rows[0]["CONTRACTTYPE"].ToString().Equals("IND"))
                {
                    LoadSubUserTypeByUserTypeWL(contractTable.Rows[0]["CONTRACTLEVELID"].ToString());
                    pnToolbar.Visible = true;
                    btnDelete.Visible = false;
                }
                if (contractTable.Rows[0]["CONTRACTTYPE"].ToString().Equals(SmartPortal.Constant.IPC.CONTRACTINDIVIDUAL))
                {
                    if (contractTable.Rows[0]["PRODUCTID"].ToString().Equals("IND_MERCHANT_MULTIUSER"))
                    {
                        pnToolbar.Visible = true;
                    }
                    else
                    {
                        pnToolbar.Visible = false;
                    }
                }
                else
                {
                    pnToolbar.Visible = true;
                }
                if (!custTable.Rows[0]["ISSUEDATE"].ToString().Equals(""))
                    txtReleasedate.Text = SmartPortal.Common.Utilities.Utility.IsDateTime2(custTable.Rows[0]["ISSUEDATE"].ToString()).ToString("dd/MM/yyyy");
            }

            //if (!hfuserType.Value.Equals("02") && ddlStatus.SelectedValue != "N")
            //{
            //    divScanQR.Visible = true;

            //    string linkqr = "window.open('" + "widgets/SEMSContractList/Controls/print.aspx?ID=" + USERID + "&" + "cul" +
            //                                                                            System.Globalization.CultureInfo.CurrentCulture.ToString() + ",BienLai" + ",menubar=1,scrollbars=1,width=500,height=650" + "')";
            //    btnPrint.OnClientClick = linkqr;

            //}
            //else
            //{
            //    divScanQR.Visible = false;
            //}

            #endregion

            #region Lấy thông tin user của hỡp đồng
            DataTable userTable = new DataTable();
            userTable = (new SmartPortal.SEMS.Contract().GetUserByContractNo(CONTRACTNO, "", "", ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
            ViewState["USERTABLE"] = userTable;
            gvCustomerList.DataSource = userTable;
            gvCustomerList.DataBind();

            txtPassPortNo.Text = userTable.Rows[0]["LICENSEID"].ToString().Trim();
            txtPaperNumberImport.Text = userTable.Rows[0]["LICENSEID"].ToString().Trim();
            txtLicenseNumber.Text = userTable.Rows[0]["LICENSEID"].ToString().Trim();
            if (userTable.Rows[0]["ISSUEDATE"].ToString().Trim() != string.Empty)
            {
                DateTime issueDate = Convert.ToDateTime(userTable.Rows[0]["ISSUEDATE"].ToString().Trim(), CultureInfo.InvariantCulture);
                txtIssueDateImport.Text = issueDate.ToString("dd/MM/yyyy");
                ddlDatetimePassport.Text = issueDate.ToString("dd/MM/yyyy");
            }

            #endregion

            //#region lấy thông tin account được register
            //if (ddlSubUserType.SelectedValue == "0201")
            //{
            //    DataSet dsacc = new DataSet();
            //    object[] searchObject = new object[] { CONTRACTNO };
            //    dsacc = _service.common("SEMS_GETACCNOREQ", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);
            //    if (dsacc != null)
            //    {
            //        if (dsacc.Tables[0].Rows.Count != 0)
            //        {
            //            pnregister.Visible = true;
            //            if (dsacc.Tables[0].Rows[0]["ACCNAME"].ToString() != "NONE")
            //            {
            //                gvlistacc.DataSource = dsacc.Tables[0];
            //                gvlistacc.DataBind();
            //            }
            //            else
            //            {
            //                gvlistacc.Visible = false;
            //            }
            //            txtaccname.Text = dsacc.Tables[0].Rows[0]["ACCNAME"].ToString();
            //            txtloginame.Text = dsacc.Tables[0].Rows[0]["LOGINNAME"].ToString();
            //            txthouse.Text = dsacc.Tables[0].Rows[0]["HOUSENO"].ToString();
            //            txtunit.Text = dsacc.Tables[0].Rows[0]["UNIT"].ToString();
            //            txtvillage.Text = dsacc.Tables[0].Rows[0]["VILLAGE"].ToString();
            //            txtdistrict.Text = dsacc.Tables[0].Rows[0]["DISTRICT"].ToString();
            //            txtprovince.Text = dsacc.Tables[0].Rows[0]["PROVINCE"].ToString();
            //            txtphoneno.Text = dsacc.Tables[0].Rows[0]["PHONENO"].ToString();
            //            txtPapertype.Text = dsacc.Tables[0].Rows[0]["LICENSETYPE"].ToString();
            //            txtPaperID.Text = dsacc.Tables[0].Rows[0]["LICENSEID"].ToString();
            //            txtissuedate.Text = dsacc.Tables[0].Rows[0]["ISSUEDATE"].ToString();
            //            txtexpirydate.Text = dsacc.Tables[0].Rows[0]["EXPIRYDATE"].ToString();
            //        }
            //    }
            //}

            //#endregion

            //set to session to export
            LoadComboboxReason();

            Session["DataExport"] = contractTable;
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
        }
    }

    void loadData_Repeater_KYC_Info()
    {
        try
        {
            DataSet ds = new DataSet();
            object[] searchObject = new object[] { cusid, GridViewPaging.pageIndex * GridViewPaging.pageSize, GridViewPaging.pageSize };
            ds = _service.common("SEMS_CO_KYCRE_CUSID", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    Repeater1.DataSource = ds.Tables[0];
                    Repeater1.DataBind();
                }
            }
        }
        catch (Exception ex)
        {

        }
    }

    // =============================================
    // Author:		<KienVT>
    // Create date: <29/07/2020>
    // Description:	Load document type
    // =============================================
    private void loadCombobox_KYCDocumentName()
    {
        // Save list STT KYC Request Cache
        try
        {
            DataSet ds = new DataSet();
            Cache.Remove("Wallet_KYCDocumentName");
            ds = (DataSet)Cache["Wallet_KYCDocumentName"];
            if (ds == null)
            {
                ds = _service.GetValueList("KYCDocumentName", "TYPE", ref IPCERRORCODE, ref IPCERRORDESC);
                Cache.Insert("Wallet_KYCDocumentName", ds, null, DateTime.MaxValue, TimeSpan.FromMinutes(10));
                ViewState["listDocumentType"] = ds.Tables[0];

                ddlDocumentTypeImport.DataSource = ds;
                ddlDocumentTypeImport.DataValueField = "ValueID";
                ddlDocumentTypeImport.DataTextField = "Caption";
                ddlDocumentTypeImport.DataBind();

                ListItem removeItemPP = ddlDocumentTypeImport.Items.FindByValue("PP");
                ListItem removeItemAC = ddlDocumentTypeImport.Items.FindByValue("AC");
                ListItem removeItemBD = ddlDocumentTypeImport.Items.FindByValue("BD");
                ListItem removeItemLC = ddlDocumentTypeImport.Items.FindByValue("LC");
                ddlDocumentTypeImport.Items.Remove(removeItemPP);
                ddlDocumentTypeImport.Items.Remove(removeItemAC);
                ddlDocumentTypeImport.Items.Remove(removeItemBD);
                ddlDocumentTypeImport.Items.Remove(removeItemLC);
            }
            else
            {
                ViewState["listDocumentType"] = ds.Tables[0];

                ddlDocumentTypeImport.DataSource = ds;
                ddlDocumentTypeImport.DataValueField = "ValueID";
                ddlDocumentTypeImport.DataTextField = "Caption";
                ddlDocumentTypeImport.DataBind();

                ListItem removeItemPP = ddlDocumentTypeImport.Items.FindByValue("PP");
                ListItem removeItemAC = ddlDocumentTypeImport.Items.FindByValue("AC");
                ListItem removeItemBD = ddlDocumentTypeImport.Items.FindByValue("BD");
                ListItem removeItemLC = ddlDocumentTypeImport.Items.FindByValue("LC");
                ddlDocumentTypeImport.Items.Remove(removeItemPP);
                ddlDocumentTypeImport.Items.Remove(removeItemAC);
                ddlDocumentTypeImport.Items.Remove(removeItemBD);
                ddlDocumentTypeImport.Items.Remove(removeItemLC);
            }
        }
        catch (Exception ex)
        {

        }
    }

    private void loadCombobox_KYCDocumentName_Repeater()
    {
        // Save list STT KYC Request Cache
        try
        {
            DataSet ds = new DataSet();
            ds = (DataSet)Cache["Wallet_KYCDocumentType"];
            if (ds == null)
            {
                ds = _service.GetValueList("KYCDocumentName", "TYPE", ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE == "0")
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlDocumentType.DataSource = ds;
                        ddlDocumentType.DataValueField = "ValueID";
                        ddlDocumentType.DataTextField = "Caption";
                        ddlDocumentType.DataBind();

                        ddlDocumentTypeImport.DataSource = ds;
                        ddlDocumentTypeImport.DataValueField = "ValueID";
                        ddlDocumentTypeImport.DataTextField = "Caption";
                        ddlDocumentTypeImport.DataBind();

                        ddlDocumentTypeImport.Items.Remove(ddlDocumentType.Items.FindByValue("PP"));
                        ddlDocumentTypeImport.Items.Remove(ddlDocumentType.Items.FindByValue("AC"));
                        ddlDocumentTypeImport.Items.Remove(ddlDocumentType.Items.FindByValue("BD"));
                        ddlDocumentTypeImport.Items.Remove(ddlDocumentType.Items.FindByValue("LC"));
                    }
                }
                Cache.Insert("Wallet_KYCDocumentType", ds, null, DateTime.MaxValue, TimeSpan.FromMinutes(10));
            }
            else
            {
                ddlDocumentType.DataSource = ds;
                ddlDocumentType.DataValueField = "ValueID";
                ddlDocumentType.DataTextField = "Caption";
                ddlDocumentType.DataBind();

                ddlDocumentTypeImport.DataSource = ds;
                ddlDocumentTypeImport.DataValueField = "ValueID";
                ddlDocumentTypeImport.DataTextField = "Caption";
                ddlDocumentTypeImport.DataBind();

                ddlDocumentTypeImport.Items.Remove(ddlDocumentType.Items.FindByValue("PP"));
                ddlDocumentTypeImport.Items.Remove(ddlDocumentType.Items.FindByValue("AC"));
                ddlDocumentTypeImport.Items.Remove(ddlDocumentType.Items.FindByValue("BD"));
                ddlDocumentTypeImport.Items.Remove(ddlDocumentType.Items.FindByValue("LC"));
            }
            if (!IsPostBack)
            {
                ViewState["listDocumentType"] = ds.Tables[0];
            }
        }
        catch (Exception ex)
        {

        }
    }

    private void loadCombobox_Nation()
    {
        // Save list Nation Cache
        try
        {
            DataSet ds = new DataSet();
            ds = (DataSet)Cache["Wallet_Nation"];
            if (ds == null)
            {
                object[] _object = new object[] { string.Empty, string.Empty, null, null };
                ds = _service.common("WAL_BO_GET_NATION", _object, ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE == "0")
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlContryPassport.DataSource = ds;
                        ddlContryPassport.DataValueField = "NationCode";
                        ddlContryPassport.DataTextField = "NationName";
                        ddlContryPassport.DataBind();
                    }
                }
                Cache.Insert("Wallet_Nation", ds, null, DateTime.MaxValue, TimeSpan.FromMinutes(10));
            }
            else
            {
                ddlContryPassport.DataSource = (DataSet)Cache["Wallet_Nation"];
                ddlContryPassport.DataValueField = "NationCode";
                ddlContryPassport.DataTextField = "NationName";
                ddlContryPassport.DataBind();
            }
        }
        catch (Exception ex)
        {

        }
    }


    // =============================================
    // Author:		<KienVT>
    // Create date: <29/07/2020>
    // Description:	Save edit name file
    // =============================================
    protected void btnOK_Click(object sender, EventArgs e)
    {
        try
        {
            txtDocumentName.BorderColor = System.Drawing.Color.Empty;

            if (!Utility.CheckSpecialCharacters(txtDocumentName.Text.Trim()))
            {
                lblError.Text = Resources.labels.DocumentName + Resources.labels.ErrorSpeacialCharacters;
                txtDocumentName.BorderColor = System.Drawing.Color.Red;
                txtDocumentName.Focus();
                return;
            }

            string idModal = "Modal" + txtNo.Text;
            if (txtDocumentName.Text.Trim().Equals(string.Empty))
            {
                txtDocumentName.BorderColor = System.Drawing.Color.Red;
                txtDocumentName.Focus();
                lblErrorPopup.InnerText = Resources.labels.DocumentName + Resources.labels.IsNotNull;
                txtDocumentName.Text = txtDocname.Text;
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Pop", "closeModal('" + idModal + "');", true);
                //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Pop", "ShowModal("+ idModal + ");", true);
            }
            else
            {
                foreach (DocumentModel item in listDocumentModel)
                {
                    if (item.No.ToString() == txtNo.Text)
                    {
                        item.DocumentName = txtDocumentName.Text.Trim();
                        if (item.IsNew == false)
                        {
                            item.IsUpdate = true;
                        }
                        break;
                    }
                }
                loadData_Repeater();
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Pop", "closeModal('" + idModal + "');", true);
            }
        }
        catch (Exception ex) { }
    }

    private void loadData_Repeater()
    {
        try
        {
            if (!IsPostBack)
            {
                loadData_ListDocument();
            }
            else
            {
                if (listDocumentModel != null)
                {
                    rptData.DataSource = listDocumentModel;
                    rptData.DataBind();
                }
                else
                {
                    loadData_ListDocument();
                }
            }
        }
        catch
        {

        }
    }
    // =============================================
    // Author:		<KienVT>
    // Create date: <29/07/2020>
    // Description:	Load document list
    // =============================================
    private void loadData_ListDocument()
    {
        listDocumentModel.Clear();
        DataTable contractTable = new DataTable();
        contractTable = (new SmartPortal.SEMS.Contract().GetContractByContractNo(CONTRACTNO, ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
        DataTable ds = new DataTable();
        ds = new SmartPortal.SEMS.Contract().LoaKYCInfor(contractTable.Rows[0]["USERID"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC).Tables[0];
        if (ds != null)
        {
            for (int i = 0; i < ds.Rows.Count; i++)
            {
                string KYC_Status = ds.Rows[i]["KYC_Status"].ToString();

                if (KYC_Status == "P")
                {
                    pnImportNewDocument.Enabled = false;
                    documentUpload.Enabled = false;
                    btSaveDocument.Enabled = false;
                }
            }

            for (int i = 0; i < ds.Rows.Count; i++)
            {
                DocumentModel item = new DocumentModel();
                item.IsNew = false;
                item.No = int.Parse(ds.Rows[i]["No"].ToString());
                item.DocumentID = int.Parse(ds.Rows[i]["DocumentID"].ToString());
                item.DateCreated = ds.Rows[i]["DateCreated"].ToString();
                item.UserCreated = ds.Rows[i]["UserCreated"].ToString();
                item.DocumentCode = ds.Rows[i]["DocumentCode"].ToString();
                item.DocumentName = ds.Rows[i]["DocumentName"].ToString();
                item.DocumentType = ds.Rows[i]["DocumentType"].ToString();
                item.Status = ds.Rows[i]["Caption"].ToString();
                item.ValueStatus = ds.Rows[i]["ValueStatus"].ToString();
                item.File = ds.Rows[i]["File"].ToString();
                listDocumentModel.Add(item);
            }
            rptData.DataSource = listDocumentModel;
            rptData.DataBind();
        }
    }

    protected void Select_PaperType(object sender, EventArgs e)
    {
        txtDocumentNameImport.Text = ddlDocumentTypeImport.SelectedItem.Text;
    }

    private bool Check_Import()
    {
        if (radPassport.Checked == true)
        {
            foreach (var item in listDocumentModel)
            {
                if (item.DocumentType == "LC" && item.ValueStatus == "A")
                {
                    return false;
                }
                foreach (ListItem item2 in ddlDocumentTypeImport.Items)
                {
                    if (item.DocumentType == item2.Value)
                    {
                        return false;
                    }
                }
            }
        }
        if (radNewNRIC.Checked == true)
        {
            foreach (var item in listDocumentModel)
            {

                if (item.DocumentType == "PP" && item.ValueStatus == "A")
                {
                    return false;
                }
                if (item.DocumentType == "LC" && item.ValueStatus == "A")
                {
                    return false;
                }
            }
        }
        if (radLicense.Checked == true)
        {
            foreach (var item in listDocumentModel)
            {

                if (item.DocumentType == "PP" && item.ValueStatus == "A")
                {
                    return false;
                }
                foreach (ListItem item2 in ddlDocumentTypeImport.Items)
                {
                    if (item.DocumentType == item2.Value)
                    {
                        return false;
                    }
                }
            }
        }
        return true;
    }


    // =============================================
    // Author:		<KienVT>
    // Create date: <29/07/2020>
    // Description:	import file
    // =============================================
    protected void btnImportFile_Click(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = string.Empty;

            DocumentModel doc = new DocumentModel();
            #region Validation

            //if (!Check_Import())
            //{
            //    lblError.Text = Resources.labels.ErrorKYCType;
            //    return;
            //}

            if (radNewNRIC.Checked == true)
            {
                txtPaperNumberImport.BorderColor = System.Drawing.Color.Empty;
                txtDocumentNameImport.BorderColor = System.Drawing.Color.Empty;
                if (txtPaperNumberImport.Text.Trim().Equals(string.Empty))
                {
                    lblError.Text = Resources.labels.PaperNumber + Resources.labels.IsNotNull;
                    txtPaperNumberImport.BorderColor = System.Drawing.Color.Red;
                    txtPaperNumberImport.Focus();
                    return;
                }
                if (txtDocumentNameImport.Text.Trim().Equals(string.Empty))
                {
                    lblError.Text = Resources.labels.DocumentName + Resources.labels.IsNotNull;
                    txtDocumentNameImport.BorderColor = System.Drawing.Color.Red;
                    txtDocumentNameImport.Focus();
                    return;
                }
            }
            if (radPassport.Checked == true)
            {
                txtPassPortNo.BorderColor = System.Drawing.Color.Empty;
                ddlDatetimePassport.BorderColor = System.Drawing.Color.Empty;
                if (txtPassPortNo.Text.Trim().Equals(string.Empty))
                {
                    lblError.Text = Resources.labels.passportnonotnull;
                    txtPassPortNo.BorderColor = System.Drawing.Color.Red;
                    txtPassPortNo.Focus();
                    return;
                }
                if (ddlDatetimePassport.Text.Trim().Equals(string.Empty))
                {
                    lblError.Text = Resources.labels.Datetimepassport + Resources.labels.IsNotNull;
                    ddlDatetimePassport.BorderColor = System.Drawing.Color.Red;
                    ddlDatetimePassport.Focus();
                    return;
                }
            }
            if (radLicense.Checked == true)
            {
                txtLicenseNumber.BorderColor = System.Drawing.Color.Empty;
                if (txtLicenseNumber.Text.Trim().Equals(string.Empty))
                {
                    lblError.Text = Resources.labels.licensenonotnull;
                    txtLicenseNumber.BorderColor = System.Drawing.Color.Red;
                    txtLicenseNumber.Focus();
                    return;
                }
            }
            #endregion
            if (documentUpload.HasFile)
            {
                String fileExtension = System.IO.Path.GetExtension(documentUpload.FileName).ToLower();
                //string[] allowedExtensions = { ".PNG", ".JPG", ".JPEG", ".BMP", ".PDF", ".WEBP" };
                string[] allowedExtensions = { ".PNG", ".JPG", ".JPEG", ".BMP" };
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

                //Resize image
                System.Drawing.Image imageToBeResized = System.Drawing.Image.FromStream(documentUpload.PostedFile.InputStream);
                string base64String;
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
                Bitmap bitmap = new Bitmap(imageToBeResized, imageWidth, imageHeight);
                System.IO.MemoryStream stream = new MemoryStream();
                bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                stream.Position = 0;
                byte[] byteImage = stream.ToArray();
                base64String = Convert.ToBase64String(byteImage);

                DocumentModel item = new DocumentModel();
                DataTable tb = (DataTable)ViewState["listDocumentType"];
                item.IsNew = true;
                item.DocumentCode = string.Empty;
                item.DateCreated = DateTime.Now.ToString("dd/MM/yyyy");
                item.Status = "Pending";
                item.ValueStatus = "P";
                int no = listDocumentModel.Count;
                item.No = no + 1;
                int index = item.No;
                while (index > tb.Rows.Count)
                {
                    index -= tb.Rows.Count;
                }
                if (radNewNRIC.Checked == true)
                {
                    item.DocumentName = txtDocumentNameImport.Text.Trim();
                    item.DocumentType = ddlDocumentTypeImport.SelectedValue;

                    if (!checkDocumentType(item))
                    {
                        lblError.Text = Resources.labels.TheDocumentTypeHasAnError;
                        return;
                    }
                }
                if (radPassport.Checked == true)
                {
                    item.DocumentName = "Passport";
                    item.DocumentType = "PP";
                }
                if (radAgreement.Checked == true)
                {
                    item.DocumentName = txtAgreementNameImport.Text.Trim();
                    item.DocumentType = "AC";
                }
                if (radBusiness.Checked == true)
                {
                    item.DocumentName = txtBusinessNameImport.Text.Trim();
                    item.DocumentType = "BD";
                }
                item.File = base64String;
                listDocumentModel.Add(item);
                loadData_Repeater2();
            }
            else
            {
                lblError.Text = Resources.labels.Importfileuploadnotfound;
            }
        }
        catch (Exception ex)
        {
        }
    }

    //import file in popup
    protected void btnImportFileUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = string.Empty;
            string idModal = "Modal" + txtNo.Text;
            if (fileUpdate.HasFile)
            {
                String fileExtension = System.IO.Path.GetExtension(fileUpdate.FileName).ToLower();
                //string[] allowedExtensions = { ".PNG", ".JPG", ".JPEG", ".BMP", ".PDF", ".WEBP" };
                string[] allowedExtensions = { ".PNG", ".JPG", ".JPEG", ".BMP" };
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
                //Resize image
                System.Drawing.Image imageToBeResized = System.Drawing.Image.FromStream(fileUpdate.PostedFile.InputStream);
                string base64String;
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
                Bitmap bitmap = new Bitmap(imageToBeResized, imageWidth, imageHeight);
                System.IO.MemoryStream stream = new MemoryStream();
                bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                stream.Position = 0;
                byte[] byteImage = stream.ToArray();
                base64String = Convert.ToBase64String(byteImage);

                foreach (DocumentModel item in listDocumentModel)
                {
                    if (item.No.ToString() == txtNo.Text)
                    {
                        item.File = base64String;
                        item.IsUpdate = true;
                        break;
                    }
                }
                rptData.DataSource = listDocumentModel;
                rptData.DataBind();
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Pop", "closeModal('" + idModal + "');", true);
            }
            else
            {
                lblError.Text = Resources.labels.Importfileuploadnotfound;
            }
        }
        catch (Exception ex)
        {
        }
    }

    private void loadData_Repeater2()
    {
        rptData.DataSource = listDocumentModel;
        rptData.DataBind();
    }

    // =============================================
    // Author:		<KienVT>
    // Create date: <29/07/2020>
    // Description:	Download all document in Zip
    // =============================================
    protected void btnDownloadAll_Click(object sender, EventArgs e)
    {
        try
        {

            ZipOutputStream zos = null;
            MemoryStream ms = null;

            string ID = GetParamsPage(IPC.ID)[0].Trim();
            DataTable contractTable = new DataTable();
            contractTable = (new SmartPortal.SEMS.Contract().GetContractByContractNo(CONTRACTNO, ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
            DataTable ds = new DataTable();
            ds = new SmartPortal.SEMS.Contract().LoaKYCInfor(contractTable.Rows[0]["USERID"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC).Tables[0];
            string filename = contractTable.Rows[0]["CUSTID"].ToString() + "_" + DateTime.Now.ToString("dd/MM/yyyy");
            if (ds != null)
            {

                ms = new MemoryStream();
                zos = new ZipOutputStream(ms);
                for (int i = 0; i < ds.Rows.Count; i++)
                {
                    string ImgStr = ds.Rows[i]["File"].ToString();
                    string ImgName = ds.Rows[i]["DocumentCode"].ToString();
                    int indexname = ImgStr.IndexOf(",");
                    string path = ImgStr.Substring(indexname + 1);
                    string imageName = ImgName + ".jpg";
                    byte[] imageBytes = Convert.FromBase64String(path);

                    ZipEntry imgEntry = new ZipEntry(imageName);
                    imgEntry.Size = imageBytes.Length;

                    zos.PutNextEntry(imgEntry);
                    zos.Write(imageBytes, 0, imageBytes.Length);
                }
                zos.Finish();
                zos.Close();

                Response.ContentType = "application/octet-stream";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + filename + ".zip");

                Response.Clear();
                Response.BinaryWrite(ms.ToArray());
                Response.End();
            }
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }

    protected void DocumentNameChage(object sender, EventArgs e)
    {
        foreach (var item in listDocumentModel)
        {
            if (item.No.ToString() == txtNo.Text)
            {
                item.DocumentName = txtDocumentName.Text;
            }
        }
    }

    // =============================================
    // Author:		<KienVT>
    // Create date: <29/07/2020>
    // Description:	Download document
    // =============================================
    public void SaveImage(string ImgName, string ImgStr)
    {
        try
        {
            int indexname = ImgStr.IndexOf(",");
            string path = ImgStr.Substring(indexname + 1);
            string imageName = ImgName + ".jpg";
            byte[] imageBytes = Convert.FromBase64String(path);
            Response.Clear();
            Response.ContentType = "image/jpg";
            Response.AddHeader("content-disposition", "attachment; filename=" + imageName);
            Response.BinaryWrite(imageBytes);
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }

    }

    // =============================================
    // Author:		<KienVT>
    // Create date: <29/07/2020>
    // Description:	View - Edit - Delete - Download document
    // =============================================
    protected void rptData_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        string commandName = e.CommandName;
        string commandArg = e.CommandArgument.ToString();
        //if (CheckPermitPageAction(commandName))
        //{
        switch (commandName)
        {
            case IPC.ACTIONPAGE.EDIT:
                break;
            case IPC.ACTIONPAGE.DETAILS:

                break;
            case IPC.ACTIONPAGE.EXPORT:
                int indexname = commandArg.IndexOf("---");
                string name = commandArg.Substring(0, indexname);
                string path = commandArg.Substring(indexname + 3);
                SaveImage(name, path);
                break;
            case IPC.ACTIONPAGE.DELETE:
                if (listDocumentModel.Count > 0)
                {
                    foreach (var item in listDocumentModel)
                    {
                        // Trường hợp Del file vừa import
                        string[] arrListStr = commandArg.Split('|');

                        if (item.No.ToString() == arrListStr[0])
                        {
                            if (item.IsNew)
                            {
                                listDocumentModel.Remove(item);
                                resetNoDocument();
                                rptData.DataSource = listDocumentModel;
                                rptData.DataBind();
                                break;
                            }
                            else // Trường hợp Del file trong database
                            {
                                deleteDocument(arrListStr[1]);
                                if (IPCERRORCODE.Equals("0"))
                                {
                                    //listDocumentModel.Remove(item);
                                    //resetNoDocument();
                                    rptData.DataSource = listDocumentModel;
                                    rptData.DataBind();
                                }
                                break;
                            }
                        }

                    }
                }
                break;
        }
    }

    // =============================================
    // Author:		<KienVT>
    // Create date: <29/07/2020>
    // Description:	Reset No for document after delete
    // =============================================
    private void resetNoDocument()
    {
        int index = 1;
        foreach (var item in listDocumentModel)
        {
            item.No = index;
            index++;
        }
    }
    // =============================================
    // Author:		<KienVT>
    // Create date: <29/07/2020>
    // Description:	Delete document
    // =============================================
    public void deleteDocument(String DocumentID)
    {
        try
        {
            DataSet ds = new DataSet();
            string UserId = HttpContext.Current.Session["userID"].ToString();
            object[] searchObject = new object[] { DocumentID.Trim(), UserId.Trim() };
            ds = _service.common("SEMS_DOC_DELETE_FILE", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                foreach (var item in listDocumentModel)
                {
                    if (item.DocumentID.ToString().Trim() == DocumentID.Trim())
                    {
                        item.ValueStatus = ds.Tables[0].Rows[0]["VALUESTATUS"].ToString();
                        item.Status = ds.Tables[0].Rows[0]["CAPTION"].ToString();
                    }
                }
                lblError.Text = Resources.labels.deletesuccessfully;
            }
            else lblError.Text = IPCERRORDESC;

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }

    // =============================================
    // Author:		<KienVT>
    // Create date: <29/07/2020>
    // Description:	Load list document and enable button delete or not
    // =============================================
    protected void rptData_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                TextBox txtDocumentName = (TextBox)e.Item.FindControl("txtDocumentName");
                //LinkButton lbtnDelete = (LinkButton)e.Item.FindControl("lbtnDelete");
                Button btnOK = (Button)e.Item.FindControl("btnOK");
                DropDownList ddlDocumentType = (DropDownList)e.Item.FindControl("ddlDocumentType");
                //HtmlGenericControl lbViewEdit = (HtmlGenericControl)e.Item.FindControl("lbViewEdit");

                loadCombobox_KYCDocumentName_Repeater();
                ddlDocumentType.SelectedValue = hdfDocumentType.Value;

                if (ACTION == IPC.ACTIONPAGE.DETAILS)
                {
                    txtDocumentName.Enabled = false;
                    ddlDocumentType.Enabled = false;
                    //lbViewEdit.Text = Resources.labels.view;
                    fileUpdate.Visible = false;
                    btnImportUpdate.Visible = false;
                    //lbtnDelete.Enabled = false;
                    btnOK.Visible = false;
                }
                fileUpdate.Visible = false;
                btnImportUpdate.Visible = false;

                //lbtnDelete.CssClass = "btn btn-secondary";
                //lbtnDelete.OnClientClick = null;
                btnOK.CssClass = "btn btn-primary";
                HtmlGenericControl lbStatusDocument = (HtmlGenericControl)e.Item.FindControl("lbStatusDocument");
                string status = lbStatusDocument.InnerText;
                HtmlGenericControl lbisNew = (HtmlGenericControl)e.Item.FindControl("lbisNew");
                string isNew = lbisNew.InnerText;
                if (!status.Equals("A"))
                {
                    //lbtnDelete.Enabled = false;
                    txtDocumentName.Enabled = false;
                    ddlDocumentType.Enabled = false;
                    fileUpdate.Visible = false;
                    btnImportUpdate.Visible = false;
                    btnOK.Visible = false;
                    //btnImport.Visible = false;
                    //documentUpload.Visible = false;
                }
                else
                {
                    fileUpdate.Visible = false;
                    btnImportUpdate.Visible = false;
                    ddlDocumentType.Enabled = false;
                    txtDocumentName.Enabled = false;
                    btnOK.Visible = false;
                }
                //if (isNew.ToUpper().Equals("TRUE"))
                //{
                //    lbtnDelete.Enabled = true;
                //}
            }
        }
        catch (Exception ex)
        {

        }
    }

    // Check duplicate document name
    private bool checkDocumentType(DocumentModel itemDoc)
    {
        foreach (var item in listDocumentModel)
        {
            if (itemDoc.No != item.No)
            {
                if (itemDoc.DocumentType == item.DocumentType)
                {
                    if (itemDoc.ValueStatus == item.ValueStatus)
                    {
                        return false;
                    }
                }
            }
        }
        return true;
    }

    // =============================================
    // Author:		<KienVT>
    // Create date: <29/07/2020>
    // Description:	Save document in database
    // =============================================
    protected void btSaveDoc_Click(object sender, EventArgs e)
    {
        try
        {
            //if (!checkDocumentType())
            //{
            //    lblError.Text = Resources.labels.TheDocumentTypeHasAnError;
            //    return;
            //}
            DataSet ds = new DataSet();
            DataTable contractTable = new DataTable();
            contractTable = (new SmartPortal.SEMS.Contract().GetContractByContractNo(CONTRACTNO, ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
            string MerchantID = contractTable.Rows[0]["USERID"].ToString();
            if (listDocumentModel.Count > 0)
            {
                foreach (var item in listDocumentModel)
                {
                    if (item.IsNew)
                    {
                        object[] insertDoc = new object[] { MerchantID, item.DocumentName, item.DocumentType, item.File, HttpContext.Current.Session["userID"].ToString() };

                        ds = _service.common("SEMS_IMPORT_DOCUMENT", insertDoc, ref IPCERRORCODE, ref IPCERRORDESC);
                        if (IPCERRORCODE == "0")
                        {
                            if (ds != null)
                            {
                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    item.DocumentCode = ds.Tables[0].Rows[0]["DocumentCode"].ToString();
                                    item.DateCreated = ds.Tables[0].Rows[0]["DateCreate"].ToString();
                                    item.UserCreated = ds.Tables[0].Rows[0]["UserCreated"].ToString();
                                    item.Status = ds.Tables[0].Rows[0]["Status"].ToString();
                                }
                            }
                        }
                    }
                    else
                    {
                        if (item.IsUpdate == true)
                        {
                            object[] updateDoc = new object[] { item.DocumentID, item.UserId, txtDocumentName.Text, item.DocumentType, item.File, HttpContext.Current.Session["userID"].ToString() };
                            ds = _service.common("SEMS_DOC_UPDATE", updateDoc, ref IPCERRORCODE, ref IPCERRORDESC);
                            if (IPCERRORCODE != "0")
                            {
                                lblError.Text = "Update Unsuccess - " + IPCERRORDESC;
                            }
                        }
                    }
                }
            }
            if (IPCERRORCODE == "0")
            {
                lblError.Text = Resources.labels.thanhcong;
                loadData_ListDocument();
                BindData();
            }
            else
            {
                lblError.Text = "Import file unsuccess - " + IPCERRORDESC;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }

    }

    protected void gvCustomerList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            CheckBox cbxSelect;
            HyperLink hpFullName;
            HyperLink hpUID;
            Label lblPhone;
            Label lblEmail;
            Label lblUserType;
            Label lblType;
            Label lblStatus;
            HyperLink hpEdit;
            HyperLink hpDelete;
            HyperLink hpQR;
            DropDownList ddlAccountQR;

            DataRowView drv;

            if (e.Row.RowType == DataControlRowType.Header)
            {
                cbxSelect = new CheckBox();
                cbxSelect.ID = "cbxSelectAll";
                cbxSelect.Attributes.Add("onclick", "SelectCbx(this);");
                e.Row.Cells[0].Controls.Add(cbxSelect);
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                drv = (DataRowView)e.Row.DataItem;


                cbxSelect = (CheckBox)e.Row.FindControl("cbxSelect");

                if (drv["STATUS"].ToString().Trim() == SmartPortal.Constant.IPC.DELETE)
                {
                    cbxSelect.Enabled = false;
                }

                e.Row.Attributes.Add("onmousemove", "this.className='hightlight';");
                e.Row.Attributes.Add("onmouseout", "this.className='nohightlight';");

                hpUID = (HyperLink)e.Row.FindControl("hpUID");
                hpFullName = (HyperLink)e.Row.FindControl("hpUserFullName");
                lblPhone = (Label)e.Row.FindControl("lblPhone");
                lblEmail = (Label)e.Row.FindControl("lblEmail");
                lblUserType = (Label)e.Row.FindControl("lblUserType");
                lblType = (Label)e.Row.FindControl("lblType");
                lblStatus = (Label)e.Row.FindControl("lblStatus");
                hpEdit = (HyperLink)e.Row.FindControl("hpEdit");
                hpDelete = (HyperLink)e.Row.FindControl("hpDelete");
                hpQR = (HyperLink)e.Row.FindControl("hpQR");
                hpQR.Text = "Gen QR";
                ddlAccountQR = (DropDownList)e.Row.FindControl("ddlAccountQR");

                //cbxSelect.Enabled = true;
                //cbxSelect.Attributes.Add("onclick", "HighLightCBX('" + e.Row.ClientID + "',this)");

                switch (drv["TYPEID"].ToString())
                {
                    case SmartPortal.Constant.IPC.MAKER:
                        lblType.Text = "Maker";
                        break;
                    case SmartPortal.Constant.IPC.CHECKER:
                        lblType.Text = "Checker";
                        break;
                    case SmartPortal.Constant.IPC.ADMIN:
                        lblType.Text = "Administrator";
                        break;
                    case "IN":
                        lblType.Text = "Internal";
                        break;
                    case "CTK":
                        lblType.Text = "Account holder";
                        break;
                    case "DCKT":
                        lblType.Text = "Account Co-owner";
                        break;

                }
                hpFullName.Text = drv["FULLNAME"].ToString();
                hpUID.Text = drv["USERID"].ToString();
                DataTable dtAccount = new Contract().GETACCOUNTQR(CONTRACTNO);
                               
                if (dtAccount.Rows.Count > 0)
                {
                    if (CONTRACTTYPE.Equals(IPC.CONTRACTAGENTMERCHANT))
                    {
                        ddlAccountQR.DataSource = dtAccount;
                        ddlAccountQR.DataTextField = "ACCOUNTNO";
                        ddlAccountQR.DataValueField = "ACTNO";
                        ddlAccountQR.DataBind();
                    }
                    else
                    {
                        ddlAccountQR.DataSource = dtAccount;
                        ddlAccountQR.DataTextField = "ACCOUNTNO";
                        ddlAccountQR.DataValueField = "ACTNO";
                        ddlAccountQR.DataBind();
                    }
                    
                }
                string linkqr = "window.open('" + "widgets/SEMSContractList/Controls/print.aspx?ID=" + hpUID.Text + "&Acctno=" + ddlAccountQR.SelectedValue + "&" + "cul" + System.Globalization.CultureInfo.CurrentCulture.ToString() + ",BienLai" + ",menubar=1,scrollbars=1,width=500,height=650" + "')";
                if (!drv["STATUS"].ToString().Equals("A"))
                {
                    hpQR.Enabled = false;
                }
                else
                {
                    hpQR.Attributes.Add("onclick", linkqr);
                }
                //quangtv add edit Wallet
                if (drv["CUSTCODE"].ToString().Equals(string.Empty) && drv["CTYPE"].ToString().Trim() == "W")
                {
                    if (ACTION.Equals(IPC.ACTIONPAGE.EDIT))
                    {
                        hpFullName.NavigateUrl = SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=1119&a=viewdetail&uid=" + drv["USERID"].ToString() + "&ct=" + ddlUserType.SelectedValue + "&returnUrl=" + SmartPortal.Common.Encrypt.EncryptData(HttpContext.Current.Request.Url.PathAndQuery) + "&pagefather=" + "SEMSContractListEdit");
                    }
                    else
                    {
                        hpFullName.NavigateUrl = SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=1119&a=viewdetail&uid=" + drv["USERID"].ToString() + "&ct=" + ddlUserType.SelectedValue + "&returnUrl=" + SmartPortal.Common.Encrypt.EncryptData(HttpContext.Current.Request.Url.PathAndQuery) + "&pagefather=" + "DETAILS");
                    }
                    WALLETONLY = true;
                    hpDelete.Enabled = false;
                    hpDelete.Text = Resources.labels.delete;
                    if (ACTION.Equals(IPC.ACTIONPAGE.ADD) && !StatusContract.Equals("D"))
                    {
                        pnLinkBank.Visible = true;
                    }
                }
                else
                {
                    if (ACTION.Equals(IPC.ACTIONPAGE.EDIT))
                    {
                        if (CONTRACTTYPE.Equals(SmartPortal.Constant.IPC.CONTRACTCORPSIMPLE)
                            || CONTRACTTYPE.Equals(SmartPortal.Constant.IPC.CONTRACTCORPADVANCE)
                            || CONTRACTTYPE.Equals(SmartPortal.Constant.IPC.CONTRACTCORPMATRIX)
                            )
                        {
                            hpFullName.NavigateUrl = SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=1157&a=viewdetail&uid=" + drv["USERID"].ToString() + "&ct=" + ddlUserType.SelectedValue + "&wl=" + txtMobi.Text.ToString() + "&returnUrl=" + SmartPortal.Common.Encrypt.EncryptData(HttpContext.Current.Request.Url.PathAndQuery) + "&pagefather=" + "SEMSContractListEdit");

                        }
                        else
                        {
                            hpFullName.NavigateUrl = SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=157&a=viewdetail&uid=" + drv["USERID"].ToString() + "&ct=" + ddlUserType.SelectedValue + "&returnUrl=" + SmartPortal.Common.Encrypt.EncryptData(HttpContext.Current.Request.Url.PathAndQuery) + "&pagefather=" + "SEMSContractListEdit");

                        }
                    }
                    else
                    {
                        if (CONTRACTTYPE.Equals(SmartPortal.Constant.IPC.CONTRACTCORPSIMPLE)
                            || CONTRACTTYPE.Equals(SmartPortal.Constant.IPC.CONTRACTCORPADVANCE)
                            || CONTRACTTYPE.Equals(SmartPortal.Constant.IPC.CONTRACTCORPMATRIX)
                            )
                        {
                            hpFullName.NavigateUrl = SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=1157&a=viewdetail&uid=" + drv["USERID"].ToString() + "&ct=" + ddlUserType.SelectedValue + "&wl=" + txtMobi.Text.ToString() + "&returnUrl=" + SmartPortal.Common.Encrypt.EncryptData(HttpContext.Current.Request.Url.PathAndQuery) + "&pagefather=" + "DETAILS");

                        }
                        else
                        {
                            hpFullName.NavigateUrl = SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=157&a=viewdetail&uid=" + drv["USERID"].ToString() + "&ct=" + ddlUserType.SelectedValue + "&returnUrl=" + SmartPortal.Common.Encrypt.EncryptData(HttpContext.Current.Request.Url.PathAndQuery) + "&pagefather=" + "DETAILS");


                        }
                    }
                    WALLETONLY = false;
                    pnLinkBank.Visible = false;
                }
                lblPhone.Text = drv["PHONE"].ToString();
                lblEmail.Text = drv["EMAIL"].ToString();
                //lblUserType.Text = drv["USERTYPE"].ToString();
                DataSet dsUserType = new DataSet();
                switch (drv["USERTYPE"].ToString().Trim())
                {
                    case SmartPortal.Constant.IPC.CHECKER:
                    case SmartPortal.Constant.IPC.MAKER:
                    case SmartPortal.Constant.IPC.ADMIN:
                        dsUserType = new SmartPortal.SEMS.Services().GetUserType("0603", "", ref IPCERRORCODE, ref IPCERRORDESC); break;
                    default:
                        dsUserType = new SmartPortal.SEMS.Services().GetUserType(drv["USERTYPE"].ToString().Trim(), "", ref IPCERRORCODE, ref IPCERRORDESC);
                        break;
                }
                if (IPCERRORCODE == "0")
                {
                    DataTable dtUserType = new DataTable();
                    dtUserType = dsUserType.Tables[0];

                    if (dtUserType.Rows.Count != 0)
                    {
                        lblUserType.Text = dtUserType.Rows[0]["USERTYPE"].ToString();
                    }
                }
                else
                {
                    throw new SmartPortal.ExceptionCollection.IPCException(IPCERRORDESC);
                }

                switch (drv["STATUS"].ToString().Trim())
                {
                    case SmartPortal.Constant.IPC.NEW:
                        lblStatus.Text = Resources.labels.connew;
                        break;
                    case SmartPortal.Constant.IPC.DELETE:
                        lblStatus.Text = Resources.labels.condelete;
                        break;
                    case SmartPortal.Constant.IPC.ACTIVE:
                        lblStatus.Text = Resources.labels.conactive;
                        break;
                    case SmartPortal.Constant.IPC.BLOCK:
                        lblStatus.Text = Resources.labels.conblock;
                        break;
                    case SmartPortal.Constant.IPC.PENDING:
                        lblStatus.Text = Resources.labels.conpending;
                        break;
                    case SmartPortal.Constant.IPC.PENDINGFORDELETE:
                        lblStatus.Text = Resources.labels.pendingfordelete;
                        break;
                    case SmartPortal.Constant.IPC.PENDINGFORAPPROVE:
                        lblStatus.Text = Resources.labels.pendingformnew;
                        break;
                    case SmartPortal.Constant.IPC.REJECTFORMNEW:
                        lblStatus.Text = Resources.labels.rejectformnew;
                        break;
                    case SmartPortal.Constant.IPC.PENDINGFORACTIVE:
                        lblStatus.Text = Resources.labels.pendingforactive;
                        break;
                    case SmartPortal.Constant.IPC.REJECT:
                        lblStatus.Text = Resources.labels.conreject;
                        break;
                }
                if (drv["IB_FailNumber"].ToString() == "5" || drv["MB_FailNumber"].ToString() == "5" || drv["AM_FailNumber"].ToString() == "5")
                {
                    lblStatus.Text = Resources.labels.conblock;
                }

                if (drv["STATUS"].ToString().Trim() == SmartPortal.Constant.IPC.DELETE)
                {
                    hpEdit.Enabled = false;
                    hpDelete.Enabled = false;
                    hpDelete.Text = Resources.labels.delete;
                    hpEdit.Text = Resources.labels.edit;
                    hpQR.Enabled = false;
                }
                else
                {
                    switch (ACTION)
                    {
                        case IPC.ACTIONPAGE.DETAILS:
                            hpDelete.Enabled = false;
                            hpEdit.Enabled = false;
                            hpDelete.Text = Resources.labels.delete;
                            hpEdit.Text = Resources.labels.edit;
                            break;
                        case IPC.ACTIONPAGE.EDIT:
                            hpEdit.Text = Resources.labels.edit;
                            hpEdit.ToolTip = Resources.labels.edit;
                            //quangtv Edit
                            if (drv["CUSTCODE"].ToString().Equals(string.Empty) && drv["CTYPE"].ToString().Trim() == "W")
                            {
                                //is Account Walle
                                hpEdit.NavigateUrl = SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=1119&a=edit&uid=" + drv["USERID"].ToString() + "&ct=" + ddlUserType.SelectedValue + "&cn=" + CONTRACTNO + "&returnUrl=" + SmartPortal.Common.Encrypt.EncryptData(HttpContext.Current.Request.Url.PathAndQuery));
                            }
                            else
                            {
                                if (CONTRACTTYPE.Equals(SmartPortal.Constant.IPC.CONTRACTCORPADVANCE)
                                   || CONTRACTTYPE.Equals(SmartPortal.Constant.IPC.CONTRACTCORPSIMPLE)
                                   || CONTRACTTYPE.Equals(SmartPortal.Constant.IPC.CONTRACTCORPMATRIX)
                                   )
                                {
                                    hpEdit.NavigateUrl = SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=1157&a=edit&uid=" + drv["USERID"].ToString() + "&ct=" + ddlUserType.SelectedValue + "&cn=" + CONTRACTNO + "&returnUrl=" + SmartPortal.Common.Encrypt.EncryptData(HttpContext.Current.Request.Url.PathAndQuery));
                                }
                                else
                                {
                                    hpEdit.NavigateUrl = SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=157&a=edit&uid=" + drv["USERID"].ToString() + "&ct=" + ddlUserType.SelectedValue + "&cn=" + CONTRACTNO + "&returnUrl=" + SmartPortal.Common.Encrypt.EncryptData(HttpContext.Current.Request.Url.PathAndQuery));

                                }
                            }
                            hpDelete.Text = Resources.labels.delete;
                            hpDelete.ToolTip = Resources.labels.delete;
                            hpDelete.NavigateUrl = SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=167&uid=" + drv["USERID"].ToString() + "&returnUrl=" + SmartPortal.Common.Encrypt.EncryptData(HttpContext.Current.Request.Url.PathAndQuery));
                            break;
                    }

                }
                if (drv["TYPEID"].ToString().Trim() == "IN")
                {
                    hpDelete.Enabled = false;
                    hpEdit.Enabled = false;
                    hpDelete.Text = Resources.labels.delete;
                    hpEdit.Text = Resources.labels.edit;
                    hpQR.Enabled = false;

                }
                if (drv["TYPEID"].ToString().Equals("CTK") || drv["TYPEID"].ToString().Equals("AD"))
                {
                    hpDelete.Enabled = false;
                    hpDelete.Text = Resources.labels.delete;
                    hpEdit.Text = Resources.labels.edit;
                }
            }
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], "Widgets_SEMSContractList_Controls_Widget", "gvCustomerList_RowDataBound", IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSContractList_Controls_Widget", "gvCustomerList_RowDataBound", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    }

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        if (CONTRACTTYPE.Equals(SmartPortal.Constant.IPC.CONTRACTCORPSIMPLE)
                                    || CONTRACTTYPE.Equals(SmartPortal.Constant.IPC.CONTRACTCORPADVANCE)
                                    || CONTRACTTYPE.Equals(SmartPortal.Constant.IPC.CONTRACTCORPMATRIX)
                                    )
        {
            Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/default.aspx?a=add&p=1157&cn=" + CONTRACTNO + "&ct=" + ddlUserType.SelectedValue + "&wl=" + txtMobi.Text.ToString() + "&returnUrl=" + SmartPortal.Common.Encrypt.EncryptData(HttpContext.Current.Request.Url.PathAndQuery)));

        }
        else
        {
            if (CONTRACTTYPE.Equals(SmartPortal.Constant.IPC.CONTRACTINDIVIDUAL))
            {
                Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/default.aspx?a=add&p=SEMSADDUSERIND&cn=" + CONTRACTNO + "&ct=" + ddlUserType.SelectedValue + "&wl=" + txtMobi.Text.ToString() + "&returnUrl=" + SmartPortal.Common.Encrypt.EncryptData(HttpContext.Current.Request.Url.PathAndQuery)));
            }
            else
            {
                Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/default.aspx?a=add&p=1167&cn=" + CONTRACTNO + "&ct=" + ddlUserType.SelectedValue + "&wl=" + txtMobi.Text.ToString() + "&returnUrl=" + SmartPortal.Common.Encrypt.EncryptData(HttpContext.Current.Request.Url.PathAndQuery)));
            }
        }

    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        CheckBox cbxDelete;
        HyperLink hpUID;

        string SUserid = "";
        try
        {
            foreach (GridViewRow gvr in gvCustomerList.Rows)
            {
                cbxDelete = (CheckBox)gvr.Cells[0].FindControl("cbxSelect");
                if (cbxDelete.Checked == true)
                {
                    hpUID = (HyperLink)gvr.Cells[1].FindControl("hpUID");
                    SUserid += hpUID.Text.Trim() + "#";
                }
            }

            if (SUserid == "")
            {
                lblError.Text = Resources.labels.youmustusertodelete;

                DataTable userTable = new DataTable();
                userTable = (new SmartPortal.SEMS.Contract().GetUserByContractNo(CONTRACTNO, "", "", ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];

                gvCustomerList.DataSource = userTable;
                gvCustomerList.DataBind();

                goto EXIT;
            }
            else
            {
                Session["_Userid"] = SUserid.Substring(0, SUserid.Length - 1);
                goto REDI;
            }
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(IPCex.ToString(), this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(IPCex.ToString(), Request.Url.Query);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSContractList_Controls_Widget", "btnDelete_Click", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    REDI:
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=167" + "&returnUrl=" + SmartPortal.Common.Encrypt.EncryptData(HttpContext.Current.Request.Url.PathAndQuery)));
    EXIT:;
    }

    protected void btSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (!linkBack)
            {
                #region Ghi log
                try
                {
                    SmartPortal.Common.Log.WriteLog("SEMSCONTRACTUPDATE", DateTime.Now.ToString(), Session["userName"].ToString(), "EBA_CONTRACT", "CONTRACTNO='" + CONTRACTNO + "'");
                }
                catch
                {
                }
                #endregion

                //new SmartPortal.SEMS.Contract().UpdateContract(CONTRACTNO, SmartPortal.Constant.IPC.PENDING, txtopendate.Text.Trim(), txtenddate.Text.Trim(),(cbIsReceiver.Checked==true?"Y":"N"), ref IPCERRORCODE, ref IPCERRORDESC);

                new SmartPortal.SEMS.Contract().UpdateContract(CONTRACTNO, SmartPortal.Constant.IPC.PENDING, txtopendate.Text.Trim(), txtenddate.Text.Trim(), (cbIsReceiver.Checked == true ? "Y" : "N"), (chkRenew.Checked == true ? "Y" : "N"), ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE != "0")
                {
                    goto ERROR;
                }

                //update san pham
                if (ddlProductType.SelectedValue.Trim() != lblProductType.Text.Trim())
                {
                    #region Ghi log
                    try
                    {
                        SmartPortal.Common.Log.WriteLog("UPDATEPRODUCT", DateTime.Now.ToString(), Session["userName"].ToString(), "EBA_CONTRACT", "CONTRACTNO='" + CONTRACTNO + "'");
                        SmartPortal.Common.Log.WriteLog("UPDATEPRODUCT", DateTime.Now.ToString(), Session["userName"].ToString(), "EBA_CONTRACTROLEDETAIL", "CONTRACTNO='" + CONTRACTNO + "'");
                        SmartPortal.Common.Log.WriteLog("UPDATEPRODUCT", DateTime.Now.ToString(), Session["userName"].ToString(), "IBS_USERINROLE", "USERNAME IN (SELECT USERNAME FROM IBS_USERS WHERE USERID IN(SELECT USERID FROM EBA_USERS WHERE CONTRACTNO='" + CONTRACTNO + "'))");
                        SmartPortal.Common.Log.WriteLog("UPDATEPRODUCT", DateTime.Now.ToString(), Session["userName"].ToString(), "SMS_USERINROLE", "USERID IN(SELECT USERID FROM EBA_USERS WHERE CONTRACTNO='" + CONTRACTNO + "')");
                        SmartPortal.Common.Log.WriteLog("UPDATEPRODUCT", DateTime.Now.ToString(), Session["userName"].ToString(), "MB_USERINROLE", "PHONENO IN (SELECT PHONENO FROM MB_USER WHERE USERID IN(SELECT USERID FROM EBA_USERS WHERE CONTRACTNO='" + CONTRACTNO + "'))");
                        SmartPortal.Common.Log.WriteLog("UPDATEPRODUCT", DateTime.Now.ToString(), Session["userName"].ToString(), "PHO_USERINROLE", "PHONENO IN (SELECT PHONENO FROM PHO_USER WHERE USERID IN(SELECT USERID FROM EBA_USERS WHERE CONTRACTNO='" + CONTRACTNO + "'))");
                        SmartPortal.Common.Log.WriteLog("UPDATEPRODUCT", DateTime.Now.ToString(), Session["userName"].ToString(), "EBA_USERACCOUNT", "USERID IN(SELECT USERID FROM EBA_USERS WHERE CONTRACTNO='" + CONTRACTNO + "')");
                        SmartPortal.Common.Log.WriteLog("UPDATEPRODUCT", DateTime.Now.ToString(), Session["userName"].ToString(), "EBA_TRANRIGHTDETAIL", "USERID IN(SELECT USERID FROM EBA_USERS WHERE CONTRACTNO='" + CONTRACTNO + "')");
                    }
                    catch
                    {
                    }
                    #endregion

                    //goi ham update san pham
                    new SmartPortal.SEMS.Contract().UpdateProduct(CONTRACTNO, ddlProductType.SelectedValue, ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE != "0")
                    {
                        goto ERROR;
                    }
                    else
                    {
                        if (ddlUserType.SelectedValue.Equals("A") || ddlUserType.SelectedValue.Equals("M"))
                        {
                            SmartPortal.Common.Log.WriteLogDatabase(CONTRACTNO, Session["userName"].ToString(), Request.Url.ToString(), Session["userName"].ToString(),
                             Request.UserHostAddress, "EBA_CONTRACT", "Change contract of Agent merchant", "", ddlProductType.SelectedItem.ToString());
                        }

                    }
                    //new SmartPortal.SEMS.Contract().UpdateContract(CONTRACTNO, SmartPortal.Constant.IPC.PENDING, txtopendate.Text.Trim(), txtenddate.Text.Trim(), (cbIsReceiver.Checked == true ? "Y" : "N"), (chkRenew.Checked == true ? "Y" : "N"), ref IPCERRORCODE, ref IPCERRORDESC);

                }
                //update Level contract
                if (ddlContractLevel.SelectedValue.Trim() != lblContractLevel.Text.Trim())
                {
                    #region Ghi log
                    try
                    {
                        SmartPortal.Common.Log.WriteLog("UPDATELEVELCONTRACT", DateTime.Now.ToString(), Session["userName"].ToString(), "EBA_CONTRACT", "CONTRACTNO='" + CONTRACTNO + "'");
                    }
                    catch
                    {
                    }

                    #endregion

                    //goi ham update san pham
                    new SmartPortal.SEMS.Contract().UpdateLevelContract(CONTRACTNO, ddlContractLevel.SelectedValue, ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE != "0")
                    {
                        goto ERROR;
                    }
                    //new SmartPortal.SEMS.Contract().UpdateContract(CONTRACTNO, SmartPortal.Constant.IPC.PENDING, txtopendate.Text.Trim(), txtenddate.Text.Trim(), (cbIsReceiver.Checked == true ? "Y" : "N"), (chkRenew.Checked == true ? "Y" : "N"), ref IPCERRORCODE, ref IPCERRORDESC);

                }
                if (ddlMerchantCategory.SelectedValue.Trim() != lblMerchantCategory.Text.Trim() && ddlUserType.SelectedValue.Equals("M"))
                {
                    #region Ghi log
                    try
                    {
                        SmartPortal.Common.Log.WriteLog("UPDATEMERCHANTCATEGORY", DateTime.Now.ToString(), Session["userName"].ToString(), "EBA_CONTRACT", "CONTRACTNO='" + CONTRACTNO + "'");
                    }
                    catch
                    {
                    }
                    #endregion
                    new SmartPortal.SEMS.Contract().UpdateMerchantCode(CONTRACTNO, ddlMerchantCategory.SelectedValue, ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE != "0")
                    {
                        goto ERROR;
                    }
                    else
                    {

                        SmartPortal.Common.Log.WriteLogDatabase(CONTRACTNO, Session["userName"].ToString(), Request.Url.ToString(), Session["userName"].ToString(),
                        Request.UserHostAddress, "EBA_CONTRACT", "Change Merchant Category", "", ddlMerchantCategory.SelectedItem.ToString());
                    }

                }
                if (ddlSubUserType.SelectedValue.Trim() != hfSubUserType.Value.Trim())
                {
                    #region Ghi log
                    try
                    {
                        SmartPortal.Common.Log.WriteLog("UPDATEUSERTYPE", DateTime.Now.ToString(), Session["userName"].ToString(), "EBA_CONTRACT", "CONTRACTNO='" + CONTRACTNO + "'");
                    }
                    catch
                    {
                    }
                    #endregion
                    new SmartPortal.SEMS.Contract().UpdateSubUserType(CONTRACTNO, ddlSubUserType.SelectedValue, ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE != "0")
                    {
                        goto ERROR;
                    }
                    else
                    {
                        if (ddlUserType.SelectedValue.Equals("A") || ddlUserType.SelectedValue.Equals("M"))
                        {
                            SmartPortal.Common.Log.WriteLogDatabase(CONTRACTNO, Session["userName"].ToString(), Request.Url.ToString(), Session["userName"].ToString(),
                             Request.UserHostAddress, "EBA_CONTRACT", "Change contract of Agent merchant", "", ddlSubUserType.SelectedItem.ToString());
                        }

                    }
                }
                if (cbLINE.Checked.ToString() != hfline.Value || cbSMS.Checked.ToString() != hfsms.Value || cbTELE.Checked.ToString() != hftele.Value || cbWAPP.Checked.ToString() != hfwhatsapp.Value)
                {
                    #region Ghi log
                    try
                    {
                        SmartPortal.Common.Log.WriteLog("UPDATETRANALTER", DateTime.Now.ToString(), Session["userName"].ToString(), "EBA_CONTRACT", "CONTRACTNO='" + CONTRACTNO + "'");
                    }
                    catch
                    {
                    }
                    #endregion
                    if (cbLINE.Checked.ToString() != hfline.Value)
                    {
                        new SmartPortal.SEMS.Contract().UpdateTransactionAlterOfContract(CONTRACTNO, "LINE", cbLINE.Checked == true ? "Y" : "N", ref IPCERRORCODE, ref IPCERRORDESC);
                    }
                    if (IPCERRORCODE != "0")
                    {
                        goto ERROR;
                    }
                    if (cbSMS.Checked.ToString() != hfsms.Value)
                    {
                        new SmartPortal.SEMS.Contract().UpdateTransactionAlterOfContract(CONTRACTNO, "SMS", cbSMS.Checked == true ? "Y" : "N", ref IPCERRORCODE, ref IPCERRORDESC);
                    }
                    if (IPCERRORCODE != "0")
                    {
                        goto ERROR;
                    }
                    if (cbTELE.Checked.ToString() != hftele.Value)
                    {
                        new SmartPortal.SEMS.Contract().UpdateTransactionAlterOfContract(CONTRACTNO, "Telegram", cbTELE.Checked == true ? "Y" : "N", ref IPCERRORCODE, ref IPCERRORDESC);
                    }
                    if (IPCERRORCODE != "0")
                    {
                        goto ERROR;
                    }
                    if (cbWAPP.Checked.ToString() != hfwhatsapp.Value)
                    {
                        new SmartPortal.SEMS.Contract().UpdateTransactionAlterOfContract(CONTRACTNO, "WhatsApp", cbWAPP.Checked == true ? "Y" : "N", ref IPCERRORCODE, ref IPCERRORDESC);
                    }
                    if (IPCERRORCODE != "0")
                    {
                        goto ERROR;
                    }
                }
                if (syncdata)
                {
                    new SmartPortal.SEMS.Customer().SyncFromCore(txtcustID.Text, txtcustname.Text.Trim(), txtBirth.Text, txtMobi.Text.Trim(), txtEmail.Text.Trim(), txtResidentAddress.Text.Trim(), txtPassportCmdn.Text.Trim(), Kyctype.Trim(), "", txtRelease.Text.Trim(), txtAddressOffice.Text.Trim(), "M", ref IPCERRORCODE, ref IPCERRORDESC);
                }
                if (IPCERRORCODE != "0")
                {
                    goto ERROR;
                }
                else
                {
                    //ucChargefee.WriteLogTransaction(txtcontractno.Text, "CONTRACT");
                    gvCustomerList.Enabled = false;
                    btSave.Visible = false;
                    btSaveDocument.Visible = false;
                    Button2.Visible = false;
                    hideAll();
                    lblError.Text = Resources.labels.editcontractsuccessfull;
                }
                goto EXIT;
            }
            else
            {
                #region Ghi log
                try
                {
                    SmartPortal.Common.Log.WriteLog("SEMSLINKBACKACCOUNT", DateTime.Now.ToString(), Session["userName"].ToString(), "EBA_CONTRACT", "CONTRACTNO='" + CONTRACTNO + "'");
                }
                catch
                {
                }
                //new SmartPortal.SEMS.Contract().UpdateContract(CONTRACTNO, SmartPortal.Constant.IPC.PENDINGFORAPPROVE, txtopendate.Text.Trim(), txtenddate.Text.Trim(), (cbIsReceiver.Checked == true ? "Y" : "N"), (chkRenew.Checked == true ? "Y" : "N"), ref IPCERRORCODE, ref IPCERRORDESC);


                LinkBankAccount(CONTRACTNO, SmartPortal.Constant.IPC.PENDINGFORAPPROVE, txtopendate.Text.Trim(), txtenddate.Text.Trim(), (cbIsReceiver.Checked == true ? "Y" : "N"), (chkRenew.Checked == true ? "Y" : "N"));
                if (IPCERRORCODE != "0")
                {
                    goto ERROR;
                }
                try
                {
                    SmartPortal.Common.Log.WriteLogDatabase(CONTRACTNO, Session["userName"].ToString(), Request.Url.ToString(), Session["userName"].ToString(),
                    Request.UserHostAddress, "EBA_CONTRACT", "Link mobile banking", "", Resources.labels.pendingforactive, Resources.labels.pendingforactive);
                }
                catch { }

                hideAll();
                goto EXIT;
                #endregion
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSContractList_Controls_Widget", "btSave_Click", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    ERROR:
        SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], "Widgets_SEMSContractList_Controls_Widget", "btSave_Click", IPCERRORDESC, Request.Url.Query);
        SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);
    EXIT:
        if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["returnURL"] != null)
        {
            Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL(SmartPortal.Common.Encrypt.DecryptData(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["returnURL"].ToString())));
        }
        else
        {
            //Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=141"));
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        RedirectBackToMainPage();
    }

    #region card managerment - vutt 06052017
    protected void btnAddCard_Click(object sender, EventArgs e)
    {
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/default.aspx?a=add&p=" + Pagecardadd + "&cn=" + CONTRACTNO + "&ct=" + ddlUserType.SelectedValue + "&returnUrl=" + SmartPortal.Common.Encrypt.EncryptData(HttpContext.Current.Request.Url.PathAndQuery)));
    }

    protected void btnDeleteCard_Click(object sender, EventArgs e)
    {
        CheckBox cbxDelete;
        HyperLink hpUID;

        string SUserid = "";
        try
        {
            foreach (GridViewRow gvr in gvCard.Rows)
            {
                cbxDelete = (CheckBox)gvr.Cells[0].FindControl("cbxSelect");
                if (cbxDelete.Checked == true)
                {
                    hpUID = (HyperLink)gvr.Cells[1].FindControl("hpUID");
                    SUserid += hpUID.Text.Trim() + "#";
                }
            }

            if (SUserid == "")
            {
                lblError.Text = Resources.labels.youmustchoosecardtodelete;

                //DataTable userTable = new DataTable();
                //userTable = (new SmartPortal.SEMS.Contract().GetUserByContractNo(CONTRACTNO, "", "", ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];

                //gvCustomerList.DataSource = userTable;
                //gvCustomerList.DataBind();

                goto EXIT;
            }
            else
            {
                Session["_dCardID"] = SUserid.Substring(0, SUserid.Length - 1);
                goto REDI;
            }
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(IPCex.ToString(), this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToIBErrorPage(IPCex.ToString(), Request.Url.Query);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSContractList_Controls_Widget", "btnDelete_Click", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    REDI:
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=" + Pagecarddelete + "&returnUrl=" + SmartPortal.Common.Encrypt.EncryptData(HttpContext.Current.Request.Url.PathAndQuery)));
    EXIT:;
    }

    protected void gvCard_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            CheckBox cbxSelect;
            HyperLink hpHolderCIF;
            HyperLink hpUID;
            Label lblFullName;
            Label lblNoCard;
            Label lblType;
            Label lblStatus;
            HyperLink hpEdit;
            HyperLink hpDelete;

            DataRowView drv;

            if (e.Row.RowType == DataControlRowType.Header)
            {
                cbxSelect = new CheckBox();
                cbxSelect.ID = "cbxSelectAll";
                cbxSelect.Attributes.Add("onclick", "SelectCbx(this);");
                e.Row.Cells[0].Controls.Add(cbxSelect);
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                drv = (DataRowView)e.Row.DataItem;


                cbxSelect = (CheckBox)e.Row.FindControl("cbxSelect");

                if (drv["STATUS"].ToString().Trim() == SmartPortal.Constant.IPC.DELETE)
                {
                    cbxSelect.Enabled = false;
                }

                e.Row.Attributes.Add("onmousemove", "this.className='hightlight';");
                e.Row.Attributes.Add("onmouseout", "this.className='nohightlight';");

                hpUID = (HyperLink)e.Row.FindControl("hpUID");
                hpHolderCIF = (HyperLink)e.Row.FindControl("hpHolderCIF");
                lblFullName = (Label)e.Row.FindControl("lblFullName");
                lblNoCard = (Label)e.Row.FindControl("lblNoCard");
                lblType = (Label)e.Row.FindControl("lblType");
                lblStatus = (Label)e.Row.FindControl("lblStatus");

                hpEdit = (HyperLink)e.Row.FindControl("hpEdit");
                hpDelete = (HyperLink)e.Row.FindControl("hpDelete");

                //cbxSelect.Enabled = true;
                //cbxSelect.Attributes.Add("onclick", "HighLightCBX('" + e.Row.ClientID + "',this)");

                hpUID.Text = txtcontractno.Text.Trim() + "|" + drv["CardHolderCFCode"].ToString().Trim();
                lblType.Text = drv["LinkType"].ToString().Equals("OWN") ? "Own" : "Other";
                hpHolderCIF.Text = drv["CardHolderCFCode"].ToString();
                hpHolderCIF.NavigateUrl = SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=1043&a=viewdetail&hcif=" + drv["CardHolderCFCode"].ToString() + "&ct=" + ddlUserType.SelectedValue + "&cn=" + CONTRACTNO.Trim() + "&returnUrl=" + SmartPortal.Common.Encrypt.EncryptData(HttpContext.Current.Request.Url.PathAndQuery));

                lblFullName.Text = drv["CardHolderName"].ToString();
                lblNoCard.Text = drv["TOTAL"].ToString();
                DataSet dsUserType = new DataSet();
                dsUserType = new SmartPortal.SEMS.Services().GetUserType(drv["USERTYPE"].ToString().Trim(), "", ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE == "0")
                {
                    DataTable dtUserType = new DataTable();
                    dtUserType = dsUserType.Tables[0];

                    if (dtUserType.Rows.Count != 0)
                    {
                        lblType.Text = dtUserType.Rows[0]["USERTYPE"].ToString();
                    }
                }
                else
                {
                    throw new SmartPortal.ExceptionCollection.IPCException(IPCERRORDESC);
                }

                switch (drv["STATUS"].ToString().Trim())
                {
                    case SmartPortal.Constant.IPC.NEW:
                        lblStatus.Text = Resources.labels.connew;
                        break;
                    case SmartPortal.Constant.IPC.DELETE:
                        lblStatus.Text = Resources.labels.condelete;
                        break;
                    case SmartPortal.Constant.IPC.ACTIVE:
                        lblStatus.Text = Resources.labels.conactive;
                        break;
                    case SmartPortal.Constant.IPC.BLOCK:
                        lblStatus.Text = Resources.labels.conblock;
                        break;
                    case SmartPortal.Constant.IPC.PENDING:
                        lblStatus.Text = Resources.labels.conpending;
                        break;
                    case SmartPortal.Constant.IPC.REJECT:
                        lblStatus.Text = Resources.labels.conreject;
                        break;
                }

                if (Session["branch"].ToString().Trim() == drv["BRANCHID_CT"].ToString().Trim())
                {
                    if (drv["STATUS"].ToString().Trim() == SmartPortal.Constant.IPC.DELETE)
                    {
                        //hpEdit.Text = "<img src='Widgets/SEMSContractList/Images/stop.png'/>";
                        hpDelete.Text = "<img src='Widgets/SEMSContractList/Images/stop.png'/>";
                        switch (ACTION)
                        {
                            case "viewdetail":
                                hpEdit.Text = "<img src='Widgets/SEMSContractList/Images/stop.png'/>";
                                break;
                            case "edit":
                                hpEdit.Text = Resources.labels.edit;
                                hpEdit.ToolTip = Resources.labels.edit;
                                hpEdit.NavigateUrl = SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=1043&a=edit&hcif=" + drv["CardHolderCFCode"].ToString() + "&ct=" + ddlUserType.SelectedValue + "&cn=" + CONTRACTNO.Trim() + "&returnUrl=" + SmartPortal.Common.Encrypt.EncryptData(HttpContext.Current.Request.Url.PathAndQuery));
                                break;
                        }
                    }
                    else
                    {
                        switch (ACTION)
                        {
                            case "viewdetail":
                                hpEdit.Text = "<img src='Widgets/SEMSContractList/Images/stop.png'/>";
                                hpDelete.Text = "<img src='Widgets/SEMSContractList/Images/stop.png'/>";
                                break;
                            case "edit":
                                hpEdit.Text = Resources.labels.edit;
                                hpEdit.ToolTip = Resources.labels.edit;
                                hpEdit.NavigateUrl = SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=1043&a=edit&hcif=" + drv["CardHolderCFCode"].ToString() + "&ct=" + ddlUserType.SelectedValue + "&cn=" + SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cn"].ToString().Trim() + "&returnUrl=" + SmartPortal.Common.Encrypt.EncryptData(HttpContext.Current.Request.Url.PathAndQuery));

                                hpDelete.Text = Resources.labels.delete;
                                hpDelete.ToolTip = Resources.labels.delete;
                                hpDelete.NavigateUrl = SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=1044&hcif=" + drv["CardHolderCFCode"].ToString() + "&cn=" + SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cn"].ToString().Trim() + "&returnUrl=" + SmartPortal.Common.Encrypt.EncryptData(HttpContext.Current.Request.Url.PathAndQuery));
                                break;
                        }

                    }
                }
                else
                {
                    hpEdit.Text = "<img src='Widgets/SEMSContractList/Images/stop.png'/>";
                    hpDelete.Text = "<img src='Widgets/SEMSContractList/Images/stop.png'/>";
                }
            }
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], "Widgets_SEMSContractList_Controls_Widget", "gvCard_RowDataBound", IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSContractList_Controls_Widget", "gvCard_RowDataBound", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    }
    #endregion

    protected void Show_viewfile(object sender, EventArgs e)
    {
        Image1.ImageUrl = imgfont;
        //Image2.Visible = false;
    }
    protected void Show_viewfile1(object sender, EventArgs e)
    {
        //Image2.ImageUrl = imgback ;
        Image2.ImageUrl = imgback;
    }

    private void LoadKYC()
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
                    radNewNRIC.Text = dr["KYCNAME"].ToString();
                    showNRIC = dr["STATUS"].ToString() == "A" ? true : false;
                }
                if (dr["KYCCODE"].ToString() == "PASSPORT")
                {
                    radPassport.Text = dr["KYCNAME"].ToString();
                    showPassport = dr["STATUS"].ToString() == "A" ? true : false;
                }
                if (dr["KYCCODE"].ToString() == "LICENSE")
                {
                    radLicense.Text = dr["KYCNAME"].ToString();
                    showLicense = dr["STATUS"].ToString() == "A" ? true : false;
                }
            }
            checkrad();
        }
        catch
        {

        }
    }
    private void checkrad()
    {
        if (showNRIC == true)
        {
            radNewNRIC.Checked = true;
        }
        if (showPassport == true && showNRIC == false)
        {
            radPassport.Checked = true;
        }
        if (showLicense == true && showPassport == false && showNRIC == false)
        {
            radLicense.Checked = true;
        }
        if (showLicense == false && showPassport == false && showNRIC == false)
        {
            radAgreement.Checked = true;
        }
    }

    public static bool showTabNRIC()
    {
        //divNRIC.Visible = showNRIC;
        return showNRIC;
    }
    public static bool showTabPassport()
    {
        return showPassport;
    }
    public static bool showTabLicense()
    {
        return showLicense;

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
                    ddlMerchantCategory.DataSource = ds;
                    ddlMerchantCategory.DataTextField = "DESCRIPTION";
                    ddlMerchantCategory.DataValueField = "MERCHANTCODE";
                    ddlMerchantCategory.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
    protected void btnLinkBank_Click(object sender, EventArgs e)
    {
        try
        {
            string customerCode = string.Empty;
            Hashtable hasCustInfo = new Hashtable();
            customerCode = txtCustomerCode.Text.Trim();
            hasCustInfo = new SmartPortal.SEMS.Customer().GetCustInfo(customerCode, SmartPortal.Constant.IPC.PERSONAL, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE != "0")
            {
                lblError.Text = Resources.labels.custnotexist;
                btSave.Visible = false;
                return;
            }
            if (hasCustInfo[SmartPortal.Constant.IPC.CUSTCODE] == null)
            {
                lblError.Text = Resources.labels.custnotexist;
                btSave.Visible = false;
                return;
            }
            #region Kiểm tra sự tồn tại của khách hàng
            DataTable tblCE = new SmartPortal.SEMS.Customer().CheckCustExists(SmartPortal.Common.Utilities.Utility.FormatStringCore(customerCode), "P", SmartPortal.Constant.IPC.CONTRACTINDIVIDUAL);
            if (tblCE.Rows.Count != 0)
            {
                lblError.Text = Resources.labels.khachhangnaydatontaitronghethong;
                btSave.Visible = false;
                return;
            }       
            string resultInfo = new Customer().CheckCustInfo(txtCustCode.Text.Trim(), txtMobi.Text.Trim(), "B", SmartPortal.Constant.IPC.CONTRACTINDIVIDUAL, ref IPCERRORCODE, ref IPCERRORDESC);
            if (resultInfo.Equals("-1"))
            {
                lblError.Text = Resources.labels.khachhangnaydatontaitronghethong;
                btSave.Visible = false;
                return;
            }
            #endregion
            if (hasCustInfo[SmartPortal.Constant.IPC.CUSTCODE] != null)
            {
                txtCustCode.Text = hasCustInfo[SmartPortal.Constant.IPC.CUSTCODE].ToString();
                txtCustCode.ForeColor = Color.Green;
            }
            if (hasCustInfo[SmartPortal.Constant.IPC.CUSTNAME] != null)
            {
                if (lblFullname.Text.Trim().Equals(""))
                {
                    txtFullNameCust.Text = hasCustInfo[SmartPortal.Constant.IPC.CUSTNAME].ToString();
                    txtFullNameCust.ForeColor = Color.Green;
                }
            }
            if (hasCustInfo[SmartPortal.Constant.IPC.CUSTTYPE] != null)
            {
                if (lblCustType.Text.Trim().Equals(""))
                {
                    if (!hasCustInfo[SmartPortal.Constant.IPC.CUSTTYPE].ToString().Trim().Equals(""))
                    {
                        ddlCustType.SelectedValue = hasCustInfo[SmartPortal.Constant.IPC.CUSTTYPE].ToString().Trim();
                    }

                    ddlCustType.ForeColor = Color.Green;
                }
            }
            if (hasCustInfo[SmartPortal.Constant.IPC.DOB] != null && hasCustInfo[SmartPortal.Constant.IPC.DOB].ToString() != "")
            {
                if (lblbirth.Text.Trim().Equals(""))
                {
                    txtBirth.Text = SmartPortal.Common.Utilities.Utility.IsDateTime1(hasCustInfo[SmartPortal.Constant.IPC.DOB].ToString()).ToString("dd/MM/yyyy");
                    txtBirth.ForeColor = Color.Green;
                }
            }
            if (hasCustInfo[SmartPortal.Constant.IPC.EMAIL] != null)
            {
                if (lblEmail.Text.Trim().Equals(""))
                {
                    txtEmail.Text = hasCustInfo[SmartPortal.Constant.IPC.EMAIL].ToString();
                    txtEmail.ForeColor = Color.Green;
                }
            }
            if (hasCustInfo[SmartPortal.Constant.IPC.ADDRESS] != null)
            {
                if (lblResidentAddress.Text.Trim().Equals(""))
                {
                    txtResidentAddress.Text = hasCustInfo[SmartPortal.Constant.IPC.ADDRESS].ToString(); ;
                    txtResidentAddress.ForeColor = Color.Green;
                }
            }
            if (hasCustInfo[SmartPortal.Constant.IPC.LICDATE] != null && hasCustInfo[SmartPortal.Constant.IPC.LICDATE].ToString() != "")
            {
                if (lblReleasedate.Text.Trim().Equals(""))
                {
                    txtReleasedate.Text = SmartPortal.Common.Utilities.Utility.IsDateTime1(hasCustInfo[SmartPortal.Constant.IPC.LICDATE].ToString()).ToString("dd/MM/yyyy");
                    txtReleasedate.ForeColor = Color.Green;
                }
            }
            if (hasCustInfo[SmartPortal.Constant.IPC.LICPLACE] != null && hasCustInfo[SmartPortal.Constant.IPC.LICDATE].ToString() != "")
            {
                if (lblRelease.Text.Trim().Equals(""))
                {
                    txtRelease.Text = SmartPortal.Common.Utilities.Utility.IsDateTime1(hasCustInfo[SmartPortal.Constant.IPC.LICDATE].ToString()).ToString("dd/MM/yyyy");
                    txtRelease.ForeColor = Color.Green;
                }
            }
            if (hasCustInfo[SmartPortal.Constant.IPC.ORGNATION] != null)
            {
                if (lblTempStay.Text.Trim().Equals(""))
                {
                    txtTempStay.Text = hasCustInfo[SmartPortal.Constant.IPC.ORGNATION].ToString();
                    txtTempStay.ForeColor = Color.Green;
                }
            }
            //if (hasCustInfo[SmartPortal.Constant.IPC.NATION] != null)
            //{
            //    if (lblddlNation.Text.Trim().Equals(""))
            //    {
            //        if (!hasCustInfo[SmartPortal.Constant.IPC.NATION].ToString().Equals(""))
            //        {
            //            ddlNation.SelectedValue = hasCustInfo[SmartPortal.Constant.IPC.NATION].ToString();
            //            ddlNation.ForeColor = Color.Green;
            //        }


            //    }
            //}
            if (hasCustInfo[SmartPortal.Constant.IPC.SEX] != null)
            {
                //if (lblGender.Text.Trim().Equals(""))
                //{
                //    if (!hasCustInfo[SmartPortal.Constant.IPC.SEX].ToString().Equals(""))
                //    {
                //        ddlGender.SelectedValue = hasCustInfo[SmartPortal.Constant.IPC.SEX].ToString() == "1" ? "M" : "F";
                //        ddlGender.ForeColor = Color.Green;
                //    }
                //}
            }
            if (hasCustInfo[SmartPortal.Constant.IPC.SHORTNAME] != null)
            {
                if (lblShortName.Text.Trim().Equals(""))
                {
                    txtShortName.Text = hasCustInfo[SmartPortal.Constant.IPC.SHORTNAME].ToString();
                    txtShortName.ForeColor = Color.Green;
                }

            }
            if (hasCustInfo[SmartPortal.Constant.IPC.BRID] != null)
            {
                if (lblbranchcust.Text.Trim().Equals(""))
                {
                    if (!hasCustInfo[SmartPortal.Constant.IPC.BRID].ToString().Equals(""))
                    {
                        ddlBranchCust.SelectedValue = hasCustInfo[SmartPortal.Constant.IPC.BRID].ToString();
                        ddlBranchCust.ForeColor = Color.Green;
                    }

                }
            }
            if (hasCustInfo[SmartPortal.Constant.IPC.LICENSE] != null)
            {
                if (lblPassportCmdn.Text.Trim().Equals(""))
                {
                    txtPassportCmdn.Text = hasCustInfo[SmartPortal.Constant.IPC.LICENSE].ToString();
                    txtPassportCmdn.ForeColor = Color.Green;
                }

            }
            linkBack = true;
            btSave.Visible = true;
            CheckResult();
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSContractList_Controls_Widget", "btnLinkBank_Click", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    }

    private void LinkBankAccount(string contractNo, string status, string createDate, string endDate, string Isreceiverlist, string autoNew)
    {
        try
        {
            #region update contract
            //tao bang chua thong tin customer
            DataTable tblContract = new DataTable();
            DataColumn colContractNo = new DataColumn("colContractNo");
            DataColumn colCTStatus = new DataColumn("colCTStatus");
            DataColumn colCreateDate = new DataColumn("colCreateDate");
            DataColumn colEndDate = new DataColumn("colEndDate");
            DataColumn colIsreceiverlist = new DataColumn("colIsreceiverlist");
            DataColumn colAutoNew = new DataColumn("colAutoNew");
            DataColumn colUserCreate = new DataColumn("colUserCreate");

            tblContract.Columns.Add(colContractNo);
            tblContract.Columns.Add(colCTStatus);
            tblContract.Columns.Add(colCreateDate);
            tblContract.Columns.Add(colEndDate);
            tblContract.Columns.Add(colIsreceiverlist);
            tblContract.Columns.Add(colAutoNew);
            tblContract.Columns.Add(colUserCreate);

            DataRow row12 = tblContract.NewRow();
            row12["colContractNo"] = contractNo;
            row12["colCTStatus"] = status;
            row12["colCreateDate"] = createDate;
            row12["colEndDate"] = endDate;
            row12["colIsreceiverlist"] = Isreceiverlist;
            row12["colAutoNew"] = autoNew;
            row12["colUserCreate"] = Session["userName"];
            tblContract.Rows.Add(row12);
            #endregion

            #region  Create table Customer
            //tao bang chua thong tin customer
            DataTable tblCustInfo = new DataTable();
            DataColumn colCustID = new DataColumn("colCustID");
            DataColumn colCustCode = new DataColumn("colCustCode");
            DataColumn colFullName = new DataColumn("colFullName");
            DataColumn colShortName = new DataColumn("colShortName");
            DataColumn colBranchID = new DataColumn("colBranchID");
            DataColumn colDOB = new DataColumn("colDOB");
            DataColumn colSex = new DataColumn("colSex");
            DataColumn colTel = new DataColumn("colTel");
            DataColumn colEmail = new DataColumn("colEmail");
            DataColumn colIssueDate = new DataColumn("colIssueDate");
            DataColumn colIssuePlace = new DataColumn("colIssuePlace");
            DataColumn colNation = new DataColumn("colNation");
            DataColumn colAddr_Resident = new DataColumn("colAddr_Resident");
            DataColumn colAddrTemp = new DataColumn("colAddrTemp");
            DataColumn colCFCode = new DataColumn("colCFCode");
            DataColumn colCTYPE = new DataColumn("colCTYPE");
            DataColumn colUserCreated = new DataColumn("colUserCreated");

            //add vào table
            tblCustInfo.Columns.Add(colCustID);
            tblCustInfo.Columns.Add(colCustCode);
            tblCustInfo.Columns.Add(colFullName);
            tblCustInfo.Columns.Add(colShortName);
            tblCustInfo.Columns.Add(colBranchID);
            tblCustInfo.Columns.Add(colDOB);
            tblCustInfo.Columns.Add(colSex);
            tblCustInfo.Columns.Add(colTel);
            tblCustInfo.Columns.Add(colEmail);
            tblCustInfo.Columns.Add(colIssueDate);
            tblCustInfo.Columns.Add(colIssuePlace);
            tblCustInfo.Columns.Add(colNation);
            tblCustInfo.Columns.Add(colAddr_Resident);
            tblCustInfo.Columns.Add(colAddrTemp);
            tblCustInfo.Columns.Add(colCFCode);
            tblCustInfo.Columns.Add(colCTYPE);
            tblCustInfo.Columns.Add(colUserCreated);
            ///add row for table

            DataRow row = tblCustInfo.NewRow();
            row["colCustID"] = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtcustID.Text.Trim());
            row["colCustCode"] = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtCustCode.Text.Trim());
            row["colFullName"] = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtFullNameCust.Text.Trim());
            row["colShortName"] = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtShortName.Text.Trim());
            row["colBranchID"] = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlBranch.SelectedValue.Trim());
            row["colDOB"] = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtBirth.Text.Trim());
            //row["colSex"] = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlGender.SelectedValue.Trim());
            row["colTel"] = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtMobi.Text.Trim());
            row["colEmail"] = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtEmail.Text.Trim());
            row["colIssueDate"] = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtReleasedate.Text.Trim());
            row["colIssuePlace"] = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtRelease.Text.Trim());
            row["colNation"] = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlNation.SelectedValue.Trim());
            row["colAddr_Resident"] = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtResidentAddress.Text.Trim());
            row["colAddrTemp"] = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtAddressOffice.Text.Trim());
            row["colCFCode"] = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtCustCode.Text.Trim());
            row["colCTYPE"] = "B";
            row["colUserCreated"] = Session["userName"];
            tblCustInfo.Rows.Add(row);

            #endregion

            #region Tạo bảng chứa Account của Contract

            #region lay tat ca cac account cua khach hang
            DataSet ds = new DataSet();
            ds = new SmartPortal.SEMS.Customer().GetAcctNo(txtCustCode.Text.Trim(), "P", ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE != "0")
            {
                //goto ERROR;
            }
            DataTable dtAccount = new DataTable();
            dtAccount = ds.Tables[0];
            if (dtAccount.Rows.Count == 0)
            {
                lblError.Text = Resources.labels.phonenumberisalreadyregistered;
                return;
            }
            #endregion

            //tao bang chua thong tin account
            DataTable tblContractAccount = new DataTable();
            DataColumn colAContractNo = new DataColumn("colAContractNo");
            DataColumn colAcctNo = new DataColumn("colAcctNo");
            DataColumn colAcctType = new DataColumn("colAcctType");
            DataColumn colCCYID = new DataColumn("colCCYID");
            DataColumn colStatus = new DataColumn("colStatus");
            DataColumn colBranchIDAC = new DataColumn("colBranchIDAC");

            //add vào table
            tblContractAccount.Columns.Add(colAContractNo);
            tblContractAccount.Columns.Add(colAcctNo);
            tblContractAccount.Columns.Add(colAcctType);
            tblContractAccount.Columns.Add(colCCYID);
            tblContractAccount.Columns.Add(colStatus);
            tblContractAccount.Columns.Add(colBranchIDAC);

            //Add all account
            foreach (DataRow dr in dtAccount.Rows)
            {
                DataRow newRowCA = tblContractAccount.NewRow();
                newRowCA["colAContractNo"] = CONTRACTNO;
                newRowCA["colAcctNo"] = dr["ACCOUNTNO"].ToString();
                newRowCA["colAcctType"] = dr["ACCOUNTTYPE"].ToString();
                newRowCA["colCCYID"] = dr["CCYID"].ToString();
                newRowCA["colStatus"] = dr["STATUS"].ToString();
                newRowCA["colBranchIDAC"] = dr["BRANCHID"].ToString();
                tblContractAccount.Rows.Add(newRowCA);
            }
            #endregion
            //DataTable dt = (new SmartPortal.SEMS.User().GetFullUserByUID(USERID, ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
            string passreveal = DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - 8, 8);
            string pwdreset = Encryption.Encrypt(passreveal);
            //PassTemp = SmartPortal.SEMS.O9Encryptpass.sha_sha256(pwdreset, userName);
            string PassTemp = SmartPortal.SEMS.O9Encryptpass.sha_sha256(passreveal, USERID);

            #region update Eba_user

            DataTable tblUser = new DataTable();
            DataColumn colUserID = new DataColumn("colUserID");
            DataColumn colUFullName = new DataColumn("colUFullName");
            DataColumn colUGender = new DataColumn("colUGender");
            DataColumn colUAddress = new DataColumn("colUAddress");
            DataColumn colUUserModify = new DataColumn("colUUserModify");
            DataColumn colPwd = new DataColumn("colPwd");
            DataColumn colPwdReset = new DataColumn("colPwdReset");

            //add vào table
            tblUser.Columns.Add(colUserID);
            tblUser.Columns.Add(colUFullName);
            tblUser.Columns.Add(colUGender);
            tblUser.Columns.Add(colUAddress);
            tblUser.Columns.Add(colUUserModify);
            tblUser.Columns.Add(colPwd);
            tblUser.Columns.Add(colPwdReset);
            DataRow row2 = tblUser.NewRow();

            row2["colUserID"] = USERID;
            row2["colUFullName"] = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtFullNameCust.Text.Trim());
            //row2["colUGender"] = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlGender.SelectedValue.Trim());
            row2["colUAddress"] = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtResidentAddress.Text.Trim());
            row2["colUUserModify"] = Session["UserName"].ToString();
            row2["colPwd"] = PassTemp;
            row2["colPwdReset"] = pwdreset;
            tblUser.Rows.Add(row2);

            #endregion



            DataSet dsResult = new DataSet();
            dsResult = new Contract().LinkBankAccount(CONTRACTNO, tblContract, tblContractAccount, tblCustInfo, tblUser, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE.Equals("0"))
            {
                lblError.Text = Resources.labels.linkbanksuccessfully;
                pnLinkBank.Visible = false;
                WALLETONLY = false;
                BindData();
            }
            else
            {
                lblError.Text = Resources.labels.linkbankfail;
            }
            btSave.Visible = false;
            btSaveDocument.Visible = false;
            Button2.Visible = false;
        }
        catch
        {

        }

    }
    private void LoadComboboxReason()
    {
        try
        {
            if (CONTRACTTYPE.Equals(SmartPortal.Constant.IPC.CONTRACTCORPADVANCE) ||
                CONTRACTTYPE.Equals(SmartPortal.Constant.IPC.CONTRACTCORPMATRIX) ||
                CONTRACTTYPE.Equals(SmartPortal.Constant.IPC.CONTRACTCORPSIMPLE))
            {
                DataTable dtReason = new SmartPortal.SEMS.Contract().GetReason("", "", "", "CCO", "CO", ref IPCERRORCODE, ref IPCERRORDESC).Tables[0];
                ddlReason.DataSource = dtReason;
                ddlReason.DataTextField = "REASONNAME";
                ddlReason.DataValueField = "REASONID";
                ddlReason.DataBind();
            }
            else
            {
                DataTable dtReason = new SmartPortal.SEMS.Contract().GetReason("", "", "", CONTRACTTYPE, "CO", ref IPCERRORCODE, ref IPCERRORDESC).Tables[0];
                ddlReason.DataSource = dtReason;
                ddlReason.DataTextField = "REASONNAME";
                ddlReason.DataValueField = "REASONID";
                ddlReason.DataBind();
            }

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

    private string LoadSubUserType(string SubUserType)
    {

        DataTable dsUserType1 = new SmartPortal.SEMS.Services().GetUserType(SubUserType, string.Empty, ref IPCERRORCODE, ref IPCERRORDESC).Tables[0];
        if (IPCERRORCODE.Equals("0"))
        {
            return dsUserType1.Rows[0]["TYPE"].ToString();
        }
        else
        {
            return string.Empty;
        }

    }
    private void LoadSubUserTypeByUserType(string userType)
    {
        try
        {
            DataTable dtUserType = new SmartPortal.SEMS.Services().GetUserType(string.Empty, userType, ref IPCERRORCODE, ref IPCERRORDESC).Tables[0];
            if (userType.Equals(SmartPortal.Constant.IPC.CONSUMER))
            {
                dtUserType = dtUserType.Select("SUBUSERCODE ='0203' AND TYPE='C'", "ID DESC").Any() ? dtUserType.Select("SUBUSERCODE ='0203' AND TYPE='C'", "ID DESC").CopyToDataTable() : null;
            }
            if (userType.Equals(SmartPortal.Constant.IPC.CONTRACTCORPMATRIX))
            {
                dtUserType = dtUserType.Select("SUBUSERCODE ='0603' AND TYPE='CCO'", "ID DESC").Any() ? dtUserType.Select("SUBUSERCODE ='0603' AND TYPE='CCO'", "ID DESC").CopyToDataTable() : null;
            }
            ddlSubUserType.DataSource = dtUserType;
            ddlSubUserType.DataTextField = "SUBUSERTYPE";
            ddlSubUserType.DataValueField = "SUBUSERCODE";
            ddlSubUserType.DataBind();
        }
        catch { }
    }
    private void LoadSubUserTypeByUserTypeWL(string contractLevel)
    {
        try
        {
            if (contractLevel.Equals("1"))
            {
                ddlSubUserType.Items.Clear();
                DataTable dtUserType = new SmartPortal.SEMS.Services().GetUserType(string.Empty, SmartPortal.Constant.IPC.CONSUMER, ref IPCERRORCODE, ref IPCERRORDESC).Tables[0];
                dtUserType = dtUserType.Select("SUBUSERCODE <>'0203' AND SUBUSERCODE <>'0202'  AND TYPE='C'", "ID DESC").Any() ? dtUserType.Select("SUBUSERCODE <>'0203'AND SUBUSERCODE <>'0202'  AND TYPE='C'", "ID DESC").CopyToDataTable() : null;
                ddlSubUserType.DataSource = dtUserType;
                ddlSubUserType.DataTextField = "SUBUSERTYPE";
                ddlSubUserType.DataValueField = "SUBUSERCODE";
                ddlSubUserType.DataBind();
            }
            else
            {
                ddlSubUserType.Items.Clear();
                DataTable dtUserType = new SmartPortal.SEMS.Services().GetUserType(string.Empty, SmartPortal.Constant.IPC.CONSUMER, ref IPCERRORCODE, ref IPCERRORDESC).Tables[0];
                dtUserType = dtUserType.Select("SUBUSERCODE <>'0203' AND SUBUSERCODE <>'0201'  AND TYPE='C'", "ID DESC").Any() ? dtUserType.Select("SUBUSERCODE <>'0203'AND SUBUSERCODE <>'0201'  AND TYPE='C'", "ID DESC").CopyToDataTable() : null;
                ddlSubUserType.DataSource = dtUserType;
                ddlSubUserType.DataTextField = "SUBUSERTYPE";
                ddlSubUserType.DataValueField = "SUBUSERCODE";
                ddlSubUserType.DataBind();
            }

        }
        catch { }
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
                divMerchantCategory.Visible = false;
            }
            else
            {
                ddlSubUserType.Items.Clear();
                DataTable dsUserType = new SmartPortal.SEMS.Services().GetUserType(string.Empty, "M", ref IPCERRORCODE, ref IPCERRORDESC).Tables[0];
                ddlSubUserType.DataSource = dsUserType;
                ddlSubUserType.DataTextField = "SUBUSERTYPE";
                ddlSubUserType.DataValueField = "SUBUSERCODE";
                ddlSubUserType.DataBind();
                divMerchantCategory.Visible = true;
            }
            LoadProductByUserType(ddlSubUserType.SelectedValue);
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
            LoadProductByUserType(ddlSubUserType.SelectedValue);
            //ucChargefee.CaculateFee("SEMS_UpdateSubUserType");
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void ddlProductType_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            LoadProductByUserType(ddlSubUserType.SelectedValue);
            //ucChargefee.CaculateFee("SEMS_UpdateProductType");
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    public void LoadProductByUserType(string subUserType)
    {
        try
        {
            string Type = string.Empty; ;
            switch (CONTRACTTYPE)
            {
                case SmartPortal.Constant.IPC.CONTRACTINDIVIDUAL:
                    Type = SmartPortal.Constant.IPC.PRCTYPECONSUMER; break;
                case SmartPortal.Constant.IPC.AGENTMERCHANT:
                    Type = SmartPortal.Constant.IPC.PRCAGENTMERCHANT; break;
                case SmartPortal.Constant.IPC.CONTRACTCORPMATRIX:
                case SmartPortal.Constant.IPC.CONTRACTCORPADVANCE:
                case SmartPortal.Constant.IPC.CONTRACTCORPSIMPLE:
                    Type = SmartPortal.Constant.IPC.CORPORATECONTRACT; break;
            }
            string SelectedvalueTemp = ddlProductType.SelectedValue;
            DataTable dt = new SmartPortal.SEMS.Product().LoadProductByUserType(subUserType, Type, "1", ref IPCERRORCODE, ref IPCERRORDESC).Tables[0];
            ddlProductType.DataSource = dt;
            ddlProductType.DataTextField = "PRODUCTNAME";
            ddlProductType.DataValueField = "PRODUCTID";
            ddlProductType.DataBind();
            if(!linkBack)
            ddlProductType.SelectedValue = SelectedvalueTemp;
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
    private void LoadUserType(string ContractType)
    {

        DataTable dtUserType = new SmartPortal.SEMS.Services().GetAllUserType("", "Y", ref IPCERRORCODE, ref IPCERRORDESC).Tables[0];
        if (ContractType.Equals(SmartPortal.Constant.IPC.CONTRACTAGENTMERCHANT))
        {
            dtUserType = dtUserType.Select("UserType <>'C'", "TypeName desc").Any() ? dtUserType.Select("UserType <>'C'", "TypeName desc").CopyToDataTable() : null;
        }
        if (ContractType.Equals(SmartPortal.Constant.IPC.CONSUMER))
        {
            dtUserType = dtUserType.Select("UserType ='C'", "TypeName desc").Any() ? dtUserType.Select("UserType ='C'", "TypeName desc").CopyToDataTable() : null;
        }
        if (ContractType.Equals(SmartPortal.Constant.IPC.CONTRACTCORPMATRIX) || ContractType.Equals(SmartPortal.Constant.IPC.CONTRACTCORPADVANCE) || ContractType.Equals(SmartPortal.Constant.IPC.CONTRACTCORPSIMPLE))
        {

            dtUserType = dtUserType.Select("UserType ='CCO'", "TypeName desc").Any() ? dtUserType.Select("UserType ='CCO'", "TypeName desc").CopyToDataTable() : null;
        }
        if (IPCERRORCODE == "0")
        {
            ddlUserType.DataSource = dtUserType;
            ddlUserType.DataTextField = "TYPENAME";
            ddlUserType.DataValueField = "USERTYPE";
            ddlUserType.DataBind();
        }
        else
        {
            throw new IPCException(IPCERRORDESC);
        }
    }
    private void CheckResult()
    {
        txtopendate.Enabled = false;
        ddlProductType.Enabled = false;
        txtenddate.Enabled = false;
        if (txtShortName.Text.Trim().Equals(""))
        {
            txtShortName.Enabled = true;
            txtShortName.ForeColor = Color.Green;
        }
        if (txtBirth.Text.Trim().Equals(""))
        {
            txtBirth.Enabled = true;
            txtBirth.ForeColor = Color.Green;
        }
        //if (ddlGender.SelectedValue.Trim().Equals(""))
        //{
        //    ddlGender.Items.RemoveAt(0);
        //    ddlGender.Enabled = true;
        //    ddlGender.ForeColor = Color.Green;
        //}
        if (txtEmail.Text.Trim().Equals(""))
        {
            txtEmail.Enabled = true;
            txtEmail.ForeColor = Color.Green;
        }
        if (txtRelease.Text.Trim().Equals(""))
        {
            txtRelease.Enabled = true;
            txtRelease.ForeColor = Color.Green;
        }
        if (txtReleasedate.Text.Trim().Equals(""))
        {
            txtReleasedate.Enabled = true;
            txtReleasedate.ForeColor = Color.Green;
        }
        if (txtTempStay.Text.Trim().Equals(""))
        {
            txtTempStay.Enabled = true;
            txtTempStay.ForeColor = Color.Green;
        }
        if (txtResidentAddress.Text.Trim().Equals(""))
        {
            txtResidentAddress.Enabled = true;
            txtResidentAddress.ForeColor = Color.Green;
        }
    }
    private void hideAll()
    {
        ddlBranch.Enabled = false;
        txtcontractno.Enabled = false;
        ddlMerchantCategory.Enabled = false;
        ddlUserType.Enabled = false;
        txtcustname.Enabled = false;
        txtenddate.Enabled = false;
        txtmodifydate.Enabled = false;
        txtopendate.Enabled = false;
        ddlProductType.Enabled = false;
        ddlStatus.Enabled = false;
        ddlContractLevel.Enabled = false;
        txtuserapprove.Enabled = false;
        txtusercreate.Enabled = false;
        txtuserlastmodify.Enabled = false;
        cbIsReceiver.Enabled = false;
        btSave.Visible = false;
        btnSync.Visible = false;
        ddlSubUserType.Enabled = false;
        pnCard.Visible = false;
        // KienVT - Load KYC Information
        btSaveDocument.Visible = false;
        pnImportNewDocument.Enabled = false;
        btnImport.Visible = false;
        documentUpload.Visible = false;
        pnLinkBank.Visible = false;
        txtEmail.Enabled = false;
        txtRelease.Enabled = false;
        txtTempStay.Enabled = false;
        chkRenew.Enabled = cbLINE.Enabled = cbSMS.Enabled = cbTELE.Enabled = cbWAPP.Enabled = false;
    }


    protected void gvlistacc_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            Label lblaccnumber;
            Label lblccyid;

            DataRowView drv;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                drv = (DataRowView)e.Row.DataItem;

                e.Row.Attributes.Add("onmousemove", "this.className='hightlight';");
                e.Row.Attributes.Add("onmouseout", "this.className='nohightlight';");

                lblaccnumber = (Label)e.Row.FindControl("lblaccnumber");
                lblccyid = (Label)e.Row.FindControl("lblccyid");

                lblaccnumber.Text = drv["ACCNUMBER"].ToString();
                lblccyid.Text = drv["CURRENCY"].ToString();
            }
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], "Widgets_SEMSContractList_Controls_Widget", "gvlistacc", IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSContractList_Controls_Widget", "gvlistacc", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    }
    protected void ddlAccountQR_SelectedIndexChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow row in gvCustomerList.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddlAccountQR = (sender as DropDownList);
                HyperLink hpQR = (HyperLink)row.FindControl("hpQR");
                HyperLink hpUID = (HyperLink)row.FindControl("hpUID");
                string linkqr = "window.open('" + "widgets/SEMSContractList/Controls/print.aspx?ID=" + hpUID.Text + "&Acctno=" + ddlAccountQR.SelectedValue + "&" + "cul" + System.Globalization.CultureInfo.CurrentCulture.ToString() + ",BienLai" + ",menubar=1,scrollbars=1,width=500,height=650" + "')";
                hpQR.Attributes.Add("onclick", linkqr);
            }
        }
    }
    protected void btnSync_OnClick(object sender, EventArgs e)
    {
        try
        {
            string custName = "";
            string birth = "";
            string mobi = "";
            string email = "";
            string residentadd = "";
            string license = "";
            string issuedate = "";
            string issueplace = "";
            string officeAdd = "";
            string gender = "";

            Hashtable hasCustInfo = new Hashtable();

            string ctmType = IPC.PERSONAL;
            hasCustInfo = new Customer().GetCustInfo(txtCustCode.Text, ctmType, ref IPCERRORCODE, ref IPCERRORDESC);
            //hasCustInfo = new SmartPortal.PhuongNamInterface.Customer().GetCustInfo(lblCustCode.Text.Trim(),
            //    hfCustType.Value, ref IPCERRORCODE, ref IPCERRORDESC);

            if (IPCERRORCODE != "0")
            {
                throw new IPCException(IPC.ERRORCODE.CUSTNOTEXIST);
            }

            if (hasCustInfo[IPC.CUSTCODE] == null)
            {
                throw new IPCException(IPC.ERRORCODE.CUSTNOTEXIST);
            }

            
            if (hasCustInfo[IPC.PHONE] != null)
            {
                txtMobi.Text = mobi = hasCustInfo[IPC.PHONE].ToString();
            }

            if (hasCustInfo[IPC.EMAIL] != null)
            {
                txtEmail.Text = email = hasCustInfo[IPC.EMAIL].ToString();
            }
            if (hasCustInfo[IPC.ADDRESS] != null)
            {
                txtResidentAddress.Text = residentadd = hasCustInfo[IPC.ADDRESS].ToString();
            }           
            if (hasCustInfo["NRIC"] != null)
            {
                txtPassportCmdn.Text = license = hasCustInfo["NRIC"].ToString();
            }
            if (hasCustInfo["IDTYPE"] != null)
            {
                Kyctype = loadCombobox_KYCLevel(hasCustInfo["IDTYPE"].ToString());
            }
            if (hasCustInfo[IPC.LICPLACE] != null)
            {
                issueplace = hasCustInfo[IPC.LICPLACE].ToString();
            }

            if (hasCustInfo[IPC.ORGNATION] != null)
            {
                officeAdd = hasCustInfo[IPC.ORGNATION].ToString();
            }
            if (hasCustInfo[IPC.SEX] != null)
            {
                try
                {
                    //ddlGender.SelectedValue = hasCustInfo[IPC.SEX].ToString().Split('.')[0];
                }
                catch
                {
                }
            }
            if (hasCustInfo[IPC.DOB] != null)
            {

                txtBirth.Text = birth = Utility.IsDateTime2(
                    hasCustInfo[IPC.DOB].ToString()).ToString("dd/MM/yyyy"); ;
            }
            //cap nhat dong bo
            //new Customer().DongBoKH(txtCustID.Text, custName, birth, mobi, email, residentadd, license,
            //    issuedate, issueplace, officeAdd, (gender == "1") ? "M" : "F");
            CheckResultSync();
            syncdata = true;
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"],
                "Widgets_SEMSCustomerList_Controls_Widget", "btnSave_Click", IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"],
                Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"],
                "Widgets_SEMSCustomerList_Controls_Widget", "btnSave_Click", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"],
                Request.Url.Query);

        }

        //Response.Redirect(Encrypt.EncryptURL("~/Default.aspx?po=4&p=153&a=edit&cid=" + Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["cid"].ToString() + "&reload=Y"));
    }

    private void CheckResultSync()
    {
        txtopendate.Enabled = false;
        ddlProductType.Enabled = false;
        txtenddate.Enabled = false;

        if (!txtMobi.Text.Trim().Equals(""))
        {
            txtMobi.ForeColor = Color.Green;
        }
        if (!txtPassportCmdn.Text.Trim().Equals(""))
        {
            txtPassportCmdn.ForeColor = Color.Green;
        }
        if (!txtEmail.Text.Trim().Equals(""))
        {
            txtEmail.ForeColor = Color.Green;
        }
        if (!txtRelease.Text.Trim().Equals(""))
        {
            txtRelease.ForeColor = Color.Green;
        }
        if (!txtReleasedate.Text.Trim().Equals(""))
        {
            txtReleasedate.ForeColor = Color.Green;
        }
        if (!txtTempStay.Text.Trim().Equals(""))
        {
            txtTempStay.ForeColor = Color.Green;
        }
        if (!txtResidentAddress.Text.Trim().Equals(""))
        {
            txtResidentAddress.ForeColor = Color.Green;
        }
        if (!txtBirth.Text.Trim().Equals(""))
        {
            txtBirth.ForeColor = Color.Green;
        }

    }
    private string loadCombobox_KYCLevel(string kyclevel)
    {
        string kycname = "NRIC";
        DataSet ds = new DataSet();
        object[] loadKYCLevel = new object[] { kyclevel };
        ds = _service.common("SEMS_BO_GET_INFO_KYC", loadKYCLevel, ref IPCERRORCODE, ref IPCERRORDESC);
        if (IPCERRORCODE == "0")
        {
            if (ds.Tables.Count > 0)
                if (ds.Tables[0].Rows.Count > 0)
                {
                    kycname = ds.Tables[0].Rows[0]["KycCode"].ToString();
                    return kycname;
                }
        }
        return kycname;
    }

}