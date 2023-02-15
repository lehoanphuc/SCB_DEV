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
using System.Globalization;

using System.Reflection;
using SmartPortal.Model;
using SmartPortal.Common.Utilities;
using System.Linq;
using System.ComponentModel.DataAnnotations;

public partial class Widgets_SEMSCorpList_Add_Widget : WidgetBase
{
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    //ContractModel model;
    public ContractModel model
    {
        get { return ViewState["ContractModel"] != null ? (ViewState["ContractModel"] as ContractModel) : null; }
        set { ViewState["ContractModel"] = value; }
    }
    internal int minlength = int.Parse(ConfigurationManager.AppSettings["minlengthloginname"].ToString());
    internal int maxlength = int.Parse(ConfigurationManager.AppSettings["maxlengthloginname"].ToString());

    public string ISIBGENERATEUSERNAME
    {
        get { return ViewState["ISIBGENERATEUSERNAME"] != null ? (ViewState["ISIBGENERATEUSERNAME"] as string) : "N"; }
        set { ViewState["ISIBGENERATEUSERNAME"] = value; }
    }

    public string ISMBGENERATEUSERNAME
    {
        get { return ViewState["ISMBGENERATEUSERNAME"] != null ? (ViewState["ISMBGENERATEUSERNAME"] as string) : "N"; }
        set { ViewState["ISMBGENERATEUSERNAME"] = value; }
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

    public string USERTYPE
    {
        get
        {
            return ViewState["USERTYPE"] != null ? (ViewState["USERTYPE"] as string) : "";
        }
        set { ViewState["USERTYPE"] = value; }
    }
    public string WALLET
    {
        get
        {
            return ViewState["WALLET"] != null ? (ViewState["WALLET"] as string) : "";
        }
        set { ViewState["WALLET"] = value; }
    }
    public bool USEWALLET
    {
        get
        {
            return ViewState["USEWALLET"] != null ? ((bool)ViewState["USEWALLET"]) : false;
        }
        set { ViewState["USEWALLET"] = value; }
    }
    public string ACTION
    {
        get
        {
            return ViewState["ACTION"] != null ? (ViewState["ACTION"] as string) : "";
        }
        set { ViewState["ACTION"] = value; }
    }
    protected void Page_Init(object sender, EventArgs e)
    {
        if (int.Parse(ConfigurationManager.AppSettings["IBMBSameUser"].ToString()) == 1)
        {
            txtUserNameMB.Enabled = false;
            lbMBGenerate.Visible = false;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            if (!IsPostBack)
            {
                InitData();
                pnCustInfo.Visible = true;
                pnCorp.Visible = false;
                pnLuu.Visible = false;

            }
            //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "postBackByObject();", true);
            //tvRole.Attributes.Add("onclick", "postBackByObject()");
        }
        catch (IPCException IPCex)
        {
            // RaiseIPCException(this.GetType().BaseType.Name, IPCex.ToString(), MethodBase.GetCurrentMethod().Name, ref lblError, "", true);
        }

        catch (Exception ex)
        {
            // RaiseException(this.GetType().BaseType.Name, ex.ToString(), MethodBase.GetCurrentMethod().Name);
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


    private void InitData()
    {
        WALLET = new SmartPortal.SEMS.User().CreatePrimaryKey(string.Empty, string.Empty, "WALLETID", ref IPCERRORCODE, ref IPCERRORDESC);
        #region Load User Type
        ddlContractType.DataSource = new Contract().LoadContractType(SmartPortal.Constant.IPC.CORPORATE, "Y");
        ddlContractType.DataTextField = "TYPENAME";
        ddlContractType.DataValueField = "USERTYPE";
        ddlContractType.DataBind();
        #endregion
        //transaction alter

        #region Load Branch
        ddlBranch.DataSource = new Branch().GetAll(ref IPCERRORCODE, ref IPCERRORDESC);
        ddlBranch.DataTextField = "BRANCHNAME";
        ddlBranch.DataValueField = "BRANCHID";
        ddlBranch.DataBind();
        ddlBranch.Enabled = true;
        #endregion

        #region Load Product
        ddlProduct.DataSource = new Product().GetProductCorpByCondition("", "", SmartPortal.Constant.IPC.PERSONAL, "CCO", "", ref IPCERRORCODE, ref IPCERRORDESC).Tables[0];
        ddlProduct.DataTextField = "PRODUCTNAME";
        ddlProduct.DataValueField = "PRODUCTID";
        ddlProduct.DataBind();
        #endregion

        #region Load Basic information
        string dateExpire_date = new SmartPortal.SEMS.EBASYSVAR().ViewDetail("EXPIRE_DATE", ref IPCERRORCODE, ref IPCERRORDESC).Tables[0].Rows[0]["VARVALUE"].ToString(); ;
        txtStartDate.Text = SmartPortal.Common.Utilities.Utility.IsDateTime1(SmartPortal.Constant.IPC.LoadWorkingDate()).ToString("dd/MM/yyyy");
        txtEndDate.Text = SmartPortal.Common.Utilities.Utility.IsDateTime1(SmartPortal.Constant.IPC.LoadWorkingDate()).AddYears(int.Parse(dateExpire_date)).AddDays(-1).ToString("dd/MM/yyyy");
        #endregion

        #region load region
        DataTable dtRegion;

        dtRegion = new SmartPortal.SEMS.Township().GetAllRegion(ref IPCERRORCODE, ref IPCERRORDESC).Tables[0];
        ddlRegion.DataSource = dtRegion;
        ddlRegion.DataTextField = "REGIONNAME";
        ddlRegion.DataValueField = "REGIONID";
        ddlRegion.DataBind();

        LoadTownshipFromRegion(ddlRegion.SelectedValue);
        #endregion
        USERID = new SmartPortal.SEMS.User().CreatePrimaryKey(string.Empty, string.Empty, "USERID", ref IPCERRORCODE, ref IPCERRORDESC);
    }

    //protected void ddlProduct_OnSelectedChanged(object sender, EventArgs e) {
    //    switch (ddlProduct.SelectedValue) {
    //        case "Matrix":
    //            ddlCorpType.Items.FindByText("Simple").Selected = false;
    //            ddlCorpType.Items.FindByText("Advanced").Selected = false;
    //            ddlCorpType.Items.FindByText("Matrix").Selected = true;
    //            break;
    //        case "Simple":
    //            ddlCorpType.Items.FindByText("Simple").Selected = true;
    //            ddlCorpType.Items.FindByText("Advanced").Selected = false;
    //            ddlCorpType.Items.FindByText("Matrix").Selected = false;
    //            break;
    //        case "Advance":
    //            ddlCorpType.Items.FindByText("Simple").Selected = false;
    //            ddlCorpType.Items.FindByText("Advanced").Selected = true;
    //            ddlCorpType.Items.FindByText("Matrix").Selected = false;
    //            break;

    //    }
    //    //ddlCorpType.Items.FindByText("Matrix").Selected = false;
    //    //ddlCorpType.Items.FindByText("Simple").Selected = true;
    //    ddlCorpType.DataBind();
    //}
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            #region Set Empty for fields
            lblError.Text = txtCustCodeInfo.Text = txtFullName.Text = txtShortName.Text = txtBirth.Text = txtMobi.Text = txtEmail.Text = txtResidentAddr.Text = txtFax.Text = string.Empty;
            string ctmType = string.Empty;
            string custCode = string.Empty;
            #endregion

            Hashtable hasCustInfo = new Hashtable();
            ctmType = SmartPortal.Constant.IPC.PERSONAL;
            custCode = Utility.KillSqlInjection(txtPersioner.Text.Trim());

            hasCustInfo = new Customer().GetCustInfo(custCode, ctmType, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE != "0")
            {
                throw new IPCException(SmartPortal.Constant.IPC.ERRORCODE.CUSTNOTEXIST);
            }
            if (hasCustInfo[SmartPortal.Constant.IPC.CUSTCODE] == null)
            {
                throw new IPCException(SmartPortal.Constant.IPC.ERRORCODE.CUSTNOTEXIST);
            }
            #region Kiểm tra sự tồn tại của khách hàng
            DataTable tblCE = new Customer().CheckCustExists(Utility.FormatStringCore(custCode), ctmType, "CCO");
            if (tblCE.Rows.Count != 0)
            {
                lblError.Text = Resources.labels.khachhangnaydatontaitronghethong;
                return;
            }
            #endregion
			
			//#region Persional customer are not allowed to create corporate contract
            //if (hasCustInfo[SmartPortal.Constant.IPC.CUSTTYPE].ToString().Trim().Equals("P")) {
            //    lblError.Text = Resources.labels.refusepersonal;
            //    return;
            //}
            //#endregion


            txtCustCodeInfo.Text = Utility.FormatStringCore(hasCustInfo[SmartPortal.Constant.IPC.CUSTCODE].ToString());

            if (hasCustInfo[SmartPortal.Constant.IPC.CUSTCODE] != null)
            {
                txtCustCodeInfo.Text = hasCustInfo[SmartPortal.Constant.IPC.CUSTCODE].ToString();
            }

            if (hasCustInfo[SmartPortal.Constant.IPC.CUSTNAME] != null)
            {
                txtFullName.Text = hasCustInfo[SmartPortal.Constant.IPC.CUSTNAME].ToString();
            }

            if (hasCustInfo[SmartPortal.Constant.IPC.CUSTTYPE] != null)
            {
                ddlCustType.SelectedValue = hasCustInfo[SmartPortal.Constant.IPC.CUSTTYPE].ToString().Trim();


                //load loai hinh san pham
                if (ConfigurationManager.AppSettings["AYACorporate"].ToString().Equals("0"))
                {
                    ddlProduct.DataSource = new Product().GetProductCorpByCondition("", "", ddlCustType.SelectedValue, "CCO", "", ref IPCERRORCODE, ref IPCERRORDESC).Tables[0];
                }
                else
                {
                    ddlProduct.DataSource = new Product().GetProductCorpByCondition("", "", SmartPortal.Constant.IPC.PERSONAL, "CCO", "", ref IPCERRORCODE, ref IPCERRORDESC).Tables[0];
                }

                ddlProduct.DataTextField = "PRODUCTNAME";
                ddlProduct.DataValueField = "PRODUCTID";
                ddlProduct.DataBind();
            }

            if (hasCustInfo[SmartPortal.Constant.IPC.DOB] != null)
            {
                try
                {
                    txtBirth.Text = Utility.IsDateTime1(hasCustInfo[SmartPortal.Constant.IPC.DOB].ToString()).ToString("dd/MM/yyyy");

                    if (txtBirth.Text.Trim() == "")
                    {
                        txtBirth.Enabled = true;
                    }
                    else
                    {
                        txtBirth.Enabled = false;
                    }
                }
                catch
                {
                }
            }

            if (hasCustInfo[SmartPortal.Constant.IPC.PHONE] != null)
            {
                txtMobi.Text = hasCustInfo[SmartPortal.Constant.IPC.PHONE].ToString();
                if (!CheckExistPhoneNumber(txtMobi.Text))
                {
                    lblError.Text = Resources.labels.phonenumberisalreadyregistered;
                }
                else if (!CheckIsPhoneNumer(txtMobi.Text))
                {
                    lblError.Text = Resources.labels.phonenumberwrong;
                }
                else
                {
                    lblError.Text = string.Empty;
                }
                if (txtMobi.Text.Trim() == "")
                {
                    txtMobi.Enabled = true;
                }
                else
                {
                    txtMobi.Enabled = true;
                }
            }

            if (hasCustInfo[SmartPortal.Constant.IPC.EMAIL] != null)
            {
                txtEmail.Text = hasCustInfo[SmartPortal.Constant.IPC.EMAIL].ToString().Trim();
                if (!new EmailAddressAttribute().IsValid(txtEmail.Text))
                {
                    txtEmail.Enabled = true;
                }
            }
            else
            {
                txtEmail.Enabled = true;
            }


            if (hasCustInfo[SmartPortal.Constant.IPC.ADDRESS] != null)
            {
                txtResidentAddr.Text = hasCustInfo[SmartPortal.Constant.IPC.ADDRESS].ToString();

                if (txtResidentAddr.Text.Trim() == "")
                {
                    txtResidentAddr.Enabled = true;
                }
                else
                {
                    txtResidentAddr.Enabled = false;
                }
            }

            if (hasCustInfo[SmartPortal.Constant.IPC.LICENSE] != null)
            {
                txtIF.Text = hasCustInfo[SmartPortal.Constant.IPC.LICENSE].ToString();

                if (txtIF.Text.Trim() == "")
                {
                    txtIF.Enabled = true;
                }
                else
                {
                    txtIF.Enabled = false;
                }
            }
            CONTRACTNO = txtContractNo.Text = new SmartPortal.SEMS.User().CreatePrimaryKey(string.Empty, string.Empty, "CONTRACTNO", ref IPCERRORCODE, ref IPCERRORDESC);
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(IPCex.ToString(), this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.ToString(), Request.Url.Query);
            lblError.Text = (new SmartPortal.BLL.ErrorBLL().Load(Utility.IsInt(IPCex.Message), System.Globalization.CultureInfo.CurrentCulture.ToString())).ErrorDesc;

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(ex.ToString(), this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            lblError.Text = Resources.labels.customerinformationreturnisincorrect;
        }
    }

    private bool validatedatetime(TextBox tx)
    {
        bool ret = false;
        try
        {
            if (string.IsNullOrEmpty(tx.Text))
                return true;
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

    protected void btnNext_Click(object sender, EventArgs e)
    {
        try
        {
            //check ngày tháng
            if (!ValidateNext())
            {
                return;
            }
            lblError.Text = string.Empty;
            #region Validate Before Next Page
            if (CONTRACTNO.Equals("") || ddlRegion.SelectedValue.Equals("") || string.IsNullOrEmpty(txtMobi.Text.Trim()) || ddlTownship.SelectedValue.Equals("") || string.IsNullOrEmpty(txtIF.Text.Trim()))
            {
                lblError.Text = "You need to fill in all the information";
                return;
            }
            #endregion
            if (!CheckIsPhoneNumer(txtMobi.Text))
            {
                lblError.Text = Resources.labels.phonenumberwrong;
                return;
            }
            else
            {
                lblError.Text = string.Empty;
            }
            #region Load Data for next page
            if (model != null)
            {
                if (model.corpType != ddlCorpType.SelectedValue)
                {
                    DeleteAllViewState();
                }
            }

            loadCorpMatrix();
            LoadData();
            ddlUserType_TextChanged(null, null);
            #endregion

            #region Show/Hide
            if (!model.corpType.Equals(SmartPortal.Constant.IPC.CONTRACTCORPADVANCE))
            {
                ddlUserType.Items.FindByValue("AD").Enabled = false;
                ddlUserType.Items.FindByValue("MK").Enabled = true;
            }
            else
            {
                ddlUserType.Items.FindByValue("AD").Enabled = true;
                ddlUserType.Items.FindByValue("MK").Enabled = false;
            }

            if (ACTION.Equals(SmartPortal.Constant.IPC.DETAILS))
            {
                pnCustInfo.Visible = false;
                pnCorp.Visible = true;
                btnConfirm.Visible = true;
                pnLuu.Visible = true;
                btnAddUserMT.Enabled = false;
                btnAddNewUser.Visible = true;
                btnAddUserMT.Visible = true;
            }
            else if (ACTION.Equals(SmartPortal.Constant.IPC.ADD))
            {
                btnConfirm.Visible = true;
                pnCustInfo.Visible = false;
                pnCorp.Visible = true;
                btnAddUserMT.Enabled = true;
                btnAddUserMT.Visible = true;
                rdAdd.Enabled = true;
                txtMTCuscode.Enabled = true;
                pnLuu.Visible = true;

            }
            else
            {
                btnConfirm.Visible = true;
                btnAddNewUser.Visible = false;
                pnCustInfo.Visible = false;
                pnCorp.Visible = true;
                btnMTCancel.Visible = false;
                pnLuu.Visible = true;
                rdAdd.Checked = true;
            }
            #endregion
            LoadAccountRole(USERID, lstAccount.SelectedValue);
        }
        catch (IPCException IPCex)
        {
            //RaiseIPCException(this.GetType().BaseType.Name, IPCex.ToString(), MethodBase.GetCurrentMethod().Name, ref lblError, (new SmartPortal.BLL.ErrorBLL().Load(Utility.IsInt(IPCex.Message), CultureInfo.CurrentCulture.ToString())).ErrorDesc);
        }
        catch (Exception ex)
        {
            //RaiseException(this.GetType().BaseType.Name, ex.ToString(), MethodBase.GetCurrentMethod().Name);
        }
    }

    void loadCorpMatrix()
    {
        model = new SmartPortal.Model.ContractModel();

        model.cusType = ddlCustType.SelectedValue;
        model.cusCode = txtCustCodeInfo.Text.Trim();
        model.fullName = txtFullName.Text.Trim();
        model.shortName = txtShortName.Text.Trim();
        model.phone = txtMobi.Text.Trim();
        model.email = txtEmail.Text.Trim();
        model.fax = txtFax.Text;
        model.addrResident = txtResidentAddr.Text;
        model.dob = txtBirth.Text.Trim();
        model.licenseId = txtIF.Text;
        model.custBranchId = ddlBranch.SelectedValue;
        model.desc = txtDesc.Text;
        model.contractNo = txtContractNo.Text.Trim();
        model.contractType = "0603";
        model.createDate = txtStartDate.Text;
        model.endDate = txtEndDate.Text;
        model.corpType = ddlCorpType.SelectedValue;
        model.productId = ddlProduct.SelectedValue;
        model.status = chkRenew.Checked ? "Y" : "N";
        model.townshipCust = ddlTownship.SelectedValue;
        model.regionCust = ddlRegion.SelectedValue;
        model.walletID = WALLET;
    }

    private void LoadData()
    {
        try
        {

            //kiem tra neu contract duoc tao tu mot cusid co san
            DataTable dtCustInfo = new DataTable();
            if (!model.isNew)
            {
                DataSet dsUserInfo = new SmartPortal.SEMS.Customer().GetCustomerByCustcode(model.cusID, ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE == "0")
                {
                    dtCustInfo = dsUserInfo.Tables[0];
                    if (dtCustInfo.Rows.Count != 0)
                    {
                        model.cusCode = dtCustInfo.Rows[0]["CFCODE"].ToString().Trim();
                        model.cusType = dtCustInfo.Rows[0]["CFTYPE"].ToString().Trim();

                        //load du lieu tu customer len form tao user
                        txtMTFullName.Text = dtCustInfo.Rows[0]["FULLNAME"].ToString();
                        txtMTAddress.Text = dtCustInfo.Rows[0]["ADDR_RESIDENT"].ToString();
                        txtMTEmail.Text = dtCustInfo.Rows[0]["EMAIL"].ToString();
                        txtMTPhone.Text = dtCustInfo.Rows[0]["TEL"].ToString();

                        ddlMTGender.SelectedValue = dtCustInfo.Rows[0]["SEX"].ToString().Trim().Equals("M") || dtCustInfo.Rows[0]["SEX"].ToString().Trim().Equals("F") ? dtCustInfo.Rows[0]["SEX"].ToString() : "F";
                        txtMTBirth.Text = SmartPortal.Common.Utilities.Utility.IsDateTime2(dtCustInfo.Rows[0]["DOB"].ToString()).ToString("dd/MM/yyyy");


                        model.fullName = dtCustInfo.Rows[0]["FULLNAME"].ToString();
                        model.licenseId = dtCustInfo.Rows[0]["LICENSEID"].ToString().Trim();
                        if (int.Parse(ConfigurationManager.AppSettings["IBMBSameUser"].ToString()) == 1)
                        {
                            txtUserNameMB.Text = txtIBTypeUserName.Text;
                        }
                        else
                        {
                        }
                    }
                    else
                    {
                        throw new IPCException(IPCERRORCODE);
                    }
                }
                else
                {
                    throw new IPCException(IPCERRORCODE);
                }
            }

            #region lay tat ca cac account cua khach hang
            DataSet ds = new SmartPortal.SEMS.Customer().GetAcctNo(model.cusCode, model.cusType, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE != "0")
            {
                //throw new IPCException(SmartPortal.Constant.IPC.ERRORCODE.IPC);
                lblError.Text = Resources.labels.khachhangnaykhongtontaitaikhoandedangky;
                return;
            }
            if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                //throw new IPCException("4017");
                lblError.Text = Resources.labels.khachhangnaykhongtontaitaikhoandedangky;
                return;
            }
            DataTable dtAccountQT = new DataTable();
            dtAccountQT = ds.Tables[0];
            if (dtAccountQT.Rows.Count == 0)
            {
                lblError.Text = Resources.labels.khachhangnaykhongtontaitaikhoandedangky;
                return;
            }
            else
            {
                try
                {
                    ddlIBAccount.DataSource = dtAccountQT;
                    ddlIBAccount.DataTextField = "ACCOUNTNO";
                    ddlIBAccount.DataValueField = "ACCOUNTNO";
                    ddlIBAccount.DataBind();
                    ddlIBAccount.Items.Insert(0, new ListItem("All", "ALL"));

                    lstAccount.DataSource = dtAccountQT;
                    lstAccount.DataTextField = "ACCOUNTNO";
                    lstAccount.DataValueField = "ACCOUNTNO";
                    lstAccount.DataBind();

                    ddlDefaultAccountQT.DataSource = dtAccountQT;
                    ddlDefaultAccountQT.DataTextField = "ACCOUNTNO";
                    ddlDefaultAccountQT.DataValueField = "ACCOUNTNO";
                    ddlDefaultAccountQT.DataBind();

                    isWallet.Checked = false;
                    ViewState["ACCOUNTCORE"] = dtAccountQT;
                    lstAccount.SelectedValue = dtAccountQT.Rows[0]["ACCOUNTNO"].ToString();
                }
                catch
                {
                    lblError.Text = Resources.labels.khachhangnaykhongtontaitaikhoandedangky;
                    return;
                }
            }

            //ASLevelModel level = new ASLevelModel();
            //level.STRUCTUREID = model.structureId;
            //DataSet dsResult = new SmartPortal.SEMS.ApprovalStructure().LevelGetAll(level, ref IPCERRORCODE, ref IPCERRORDESC);

            //if (IsSuccess() && IsDataSetNotNull(dsResult))
            //{
            //    DataTable dt = dsResult.Tables[0];
            //    ddlUserLevel.DataSource = dt;
            //    ddlUserLevel.DataTextField = "LevelShortName";
            //    ddlUserLevel.DataValueField = "UserLevel";
            //    ddlUserLevel.DataBind();
            //    ddlUserLevel_SelectedIndexChanged(ddlUserLevel, null);
            //}

            #endregion

            string usertype = ddlUserType.SelectedValue;
            //if (model.corpType.Equals(SmartPortal.Constant.IPC.MTRUSER))
            //{
            //    usertype = SmartPortal.Constant.IPC.MTRUSER;
            //}
            LoadDataInTreeview(SmartPortal.Constant.IPC.IB, tvIBRole, usertype, SmartPortal.Constant.IPC.NORMAL);
            //LoadDataInTreeview(SmartPortal.Constant.IPC.SMS, tvSMS, SmartPortal.Constant.IPC.MTRUSER);
            LoadDataInTreeview(SmartPortal.Constant.IPC.MB, tvMB, usertype, SmartPortal.Constant.IPC.NORMAL);
            //LoadDataInTreeview(SmartPortal.Constant.IPC.PHO, tvPHO, SmartPortal.Constant.IPC.MTRUSER);
            GetRoleDefault(tvIBRole, new TreeView(), tvMB, new TreeView());


            LoadDataALLRoleInTreeview(string.Empty, tvRole, usertype, SmartPortal.Constant.IPC.NORMAL);



            DataSet dspolicy = new DataSet();
            string filterIB = "serviceid='IB'";
            //string filterSMS = "serviceid='SMS'";
            // string filterMB = "serviceid='MB'";
            string stSort = "serviceid asc";
            dspolicy = new USERPOLICY().GetPolicybyCondition(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, Session["userName"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {


                DataTable dtIB = dspolicy.Tables[0].Select(filterIB, stSort).CopyToDataTable();
                //DataTable dtSMS = dspolicy.Tables[0].Select(filterSMS, stSort).CopyToDataTable();
                // DataTable dtMB = dspolicy.Tables[0].Select(filterMB, stSort).CopyToDataTable();

                //advance contract
                ddlPolicyIB.DataSource = dtIB;

                ddlPolicyIB.DataTextField = "policytx";
                ddlPolicyIB.DataValueField = "policyid";
                ddlPolicyIB.DataBind();
                //mb advance
                // ddlPolicyMB.DataSource = dtMB;
                // ddlPolicyMB.DataTextField = "policytx";
                // ddlPolicyMB.DataValueField = "policyid";
                // ddlPolicyMB.DataBind();
            }
            btnCustSave.Enabled = false;
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    void LoadDataInTreeview(string serviceID, TreeView tvPage, string userType, string roleType)
    {
        tvPage.Nodes.Clear();
        DataTable tblSS = new DataTable();
        tblSS = new SmartPortal.SEMS.Role().GetByServiceAndUserType(serviceID, userType, model.productId, roleType);

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



    void LoadDataALLRoleInTreeview(string serviceID, TreeView tvPage, string userType, string roleType)
    {


        tvPage.Nodes.Clear();
        DataTable tblSS = new DataTable();

        DataTable tblRole = new DataTable();
        tblRole = CreateTableRole();

        tblSS = new SmartPortal.SEMS.Role().GetByServiceAndUserType(serviceID, userType, model.productId, roleType);

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
    protected void MTAction_onChange(object sender, EventArgs e)
    {
        RadioButton radio = sender as RadioButton;
        if (rdAdd.Equals(radio))
        {
            txtMTCuscode.Enabled = true;
            ddlUser.Enabled = false;
        }
        else
        {
            txtMTCuscode.Enabled = false;
            ddlUser.Enabled = true;
        }
    }

    protected void btnMatrixDetail_Click(object sender, EventArgs e)
    {

        lblError.Text = string.Empty;
        LoadViewDetail();
        if (rdAdd.Checked)
        {
            if (string.IsNullOrEmpty(txtMTCuscode.Text))
            {
                lblError.Text = Resources.labels.khachhangnaydatontaitronghethong;
            }
            LoadDetailsAdd(txtMTCuscode.Text.Trim());
            CheckAllRole();
            ACTION = SmartPortal.Constant.IPC.ADD;
        }

        else
        {
            LoadDetailsEdit();
        }
    }

    private void LoadDetailsAdd(string cuscode)
    {
        try
        {
            btnAddUserMT.Enabled = true;
            lblError.Text = "";
            txtMTFullName.Text = string.Empty;
            txtMTEmail.Text = string.Empty;
            txtMTPhone.Text = string.Empty;
            ddlMTGender.SelectedIndex = 0;
            txtMTBirth.Text = string.Empty;
            txtMTAddress.Text = string.Empty;
            ddlUserType.Enabled = true;
            Hashtable hasUserInfo = new Hashtable();
            string ctmType = "P";
            hasUserInfo = new SmartPortal.SEMS.Customer().GetCustInfo(cuscode, ctmType, ref IPCERRORCODE, ref IPCERRORDESC);

            if (IPCERRORCODE != "0")
            {
                throw new IPCException(SmartPortal.Constant.IPC.ERRORCODE.CUSTNOTEXIST);
            }
            if (hasUserInfo[SmartPortal.Constant.IPC.CUSTCODE] == null)
            {
                throw new IPCException(SmartPortal.Constant.IPC.ERRORCODE.CUSTNOTEXIST);
            }

            txtMTFullName.Text = hasUserInfo[SmartPortal.Constant.IPC.CUSTNAME] != null ? hasUserInfo[SmartPortal.Constant.IPC.CUSTNAME].ToString() : string.Empty;
            txtMTEmail.Text = hasUserInfo[SmartPortal.Constant.IPC.EMAIL] != null ? hasUserInfo[SmartPortal.Constant.IPC.EMAIL].ToString().Trim() : string.Empty;
            txtPhoneMB.Text = txtMTPhone.Text = hasUserInfo[SmartPortal.Constant.IPC.PHONE] != null ? hasUserInfo[SmartPortal.Constant.IPC.PHONE].ToString() : string.Empty;
            if (!CheckPhoneInCust(txtPhoneMB.Text, USERID))
            {
                lblError.Text = Resources.labels.phonenumberisalreadyregistered;
            }
            //if (!CheckExistPhoneNumber(txtPhoneMB.Text))
            //{
            //    lblError.Text = Resources.labels.phonenumberisalreadyregistered;
            //}
            else if (!CheckIsPhoneNumer(txtPhoneMB.Text))
            {
                lblError.Text = Resources.labels.phonenumberwrong;
            }
            else
            {
                lblError.Text = string.Empty;
            }
            txtMTAddress.Text = hasUserInfo[SmartPortal.Constant.IPC.ADDRESS] != null ? hasUserInfo[SmartPortal.Constant.IPC.ADDRESS].ToString() : string.Empty;

            if (hasUserInfo[SmartPortal.Constant.IPC.SEX] != null)
            {
                try
                {
                    if (!string.IsNullOrEmpty(hasUserInfo[SmartPortal.Constant.IPC.SEX].ToString().Trim()))
                    {
                        ddlMTGender.SelectedValue = Utility.FormatStringCore(int.Parse(hasUserInfo[SmartPortal.Constant.IPC.SEX].ToString()).ToString()) == "1" ? "M" : "F";

                    }
                    else
                    {
                        ddlMTGender.Enabled = true;
                    }

                }
                catch
                {
                }
            }
            if (hasUserInfo[SmartPortal.Constant.IPC.DOB] != null)
            {
                try
                {
                    string birthDate = SmartPortal.Common.Utilities.Utility.IsDateTime1(hasUserInfo[SmartPortal.Constant.IPC.DOB].ToString()).ToString("dd/MM/yyyy");
                    txtMTBirth.Text = birthDate.Equals("01/01/1900") ? "" : birthDate;
                    txtMTBirth.Enabled = txtMTBirth.Text.Trim() == string.Empty;
                }
                catch
                {
                }

            }
        }
        catch (Exception ex)
        {
            lblError.Text = (new SmartPortal.BLL.ErrorBLL().Load(Utility.IsInt(ex.Message), System.Globalization.CultureInfo.CurrentCulture.ToString())).ErrorDesc;
        }
    }

    private void LoadDetailsEdit()
    {
        try
        {
            lblError.Text = string.Empty;
            txtMTCuscode.Enabled = false;
            //rdEdit.Enabled = false;
            if (ViewState["MTRUSER"] != null)
            {
                //lannth - 27.9.2018 - khi sua user, bat buoc phai save user truoc khi chon them user moi
                rdAdd.Enabled = false;

                //cho phep xoa user khi da chon user der edit
                btnMTCancel.Visible = true;

                string value = ddlUser.SelectedValue.Trim();
                DataTable tblSUM = (DataTable)ViewState["MTRUSER"];


                var user = (from DataRow dRow in tblSUM.Rows
                            select new
                            {
                                order = dRow["colOrder"],
                                typeID = dRow["colTypeID"],
                                fullname = dRow["colFullName"],
                                userid = dRow["colUserID"],
                                level = dRow["colLevel"],
                                igroup = dRow["colGroup"],
                                dob = dRow["colBirthday"],
                                gender = dRow["colGender"],
                                phone = dRow["colPhone"],
                                email = dRow["colEmail"],
                                address = dRow["colAddress"],
                                renderIBUserName = dRow["colIsRenderUserName"],
                                username = dRow["colIBUserName"],
                                ibpass = dRow["colIBPass"],
                                smsphone = dRow["colSMSPhone"],
                                smsdfact = dRow["colSMSDefaultAcctno"],
                                smsdflang = dRow["colSMSDefaultLang"],
                                smsisdf = dRow["colSMSIsDefault"],
                                mbphone = dRow["colMBPhone"],
                                mbpass = dRow["colMBPass"]
                            }).FirstOrDefault(m => m.order.Equals(value));

                //txtMTCuscode.Text = user.cuscode.ToString();
                //txtMTCuscode.Enabled = false;

                //kiểm tra nếu user chưa bị xóa mới cho hiển thị.
                if (user != null)
                {
                    txtMTFullName.Text = user.fullname.ToString();
                    //hdGroupID.Value = user.igroup.ToString();
                    //ddlUserLevel.SelectedValue = user.level.ToString();
                    //ddlUserLevel_SelectedIndexChanged(ddlUserLevel, null);
                    //ddlGroup.SelectedValue = user.igroup.ToString();
                    txtMTBirth.Text = user.dob.ToString();
                    ddlMTGender.SelectedValue = user.gender.ToString();
                    USERTYPE = ddlUserType.SelectedValue = user.typeID.ToString();
                    ddlUserType.Enabled = false;
                    txtMTPhone.Text = user.phone.ToString();
                    txtMTEmail.Text = user.email.ToString();
                    txtMTAddress.Text = user.address.ToString();
                    txtUserNameMB.Text = user.username.ToString();
                    txtPhoneMB.Text = user.mbphone.ToString();

                    txtIBTypeUserName.Text = user.username.ToString();

                    //set role theo user edit
                    //TreeView[] aTree = new TreeView[] { tvIBRole, tvMB, tvPHO, tvSMS };
                    //SetRoleUserEdit(aTree, tblSUM, value);
                    btnAddNewUser.Visible = true;
                    GetRoleDefault(tvIBRole, new TreeView(), tvMB, new TreeView());

                    //sau khi xoa user, reset lai form, set lai source cho gridview
                    DataTable dtSource = tblSUM.Select("colOrder='" + value + "'").CopyToDataTable();
                    ViewState["DataCurrent"] = dtSource;
                    gvMTUser.DataSource = dtSource.DefaultView.ToTable(true, "colFullName", "colIBUserName", "colAccount", "colRole");
                    gvMTUser.DataBind();
                }
                else
                {
                    btnAddNewUser_Click(null, null);
                    lblError.Text = Resources.labels.usernotexist;
                }


                LoadDataALLRoleInTreeview(string.Empty, tvRole, ddlUserType.SelectedValue, SmartPortal.Constant.IPC.NORMAL);
                DataTable tblAccountRole = ViewState["ACCOUNTROLE"] != null ? (DataTable)ViewState["ACCOUNTROLE"] : CreateTableAccontRole();
                if (ACTION.Equals(SmartPortal.Constant.IPC.ADD))
                {
                    ViewState["ACCOUNTROLE"] = tblAccountRole = tblAccountRole.Select("colUserID <>'" + USERID + "'").CopyToDataTable();
                }
                ACTION = SmartPortal.Constant.IPC.DETAILS;
                if (USEWALLET)
                {
                    isWallet.Checked = true;
                    IsWallet_OnCheckedChaned(null, null);
                }
                USERID = user.userid.ToString();
                LoadAccountRole(user.userid.ToString(), lstAccount.SelectedValue.ToString().Trim());
            }
            btnCustSave.Enabled = true;

        }
        catch (Exception ex)
        {

        }

    }

    void GetRoleDefault(TreeView treeIB, TreeView treeSMS, TreeView treeMB, TreeView treePHO)
    {
        DataTable tblRoleDefault = new DataTable();
        //lay role mac dinh IB
        tblRoleDefault = (new Role().GetRoleDefaultByServiceID(SmartPortal.Constant.IPC.IB, model.productId, 0, string.Empty, ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];

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
        tblRoleDefault = (new SmartPortal.SEMS.Role().GetRoleDefaultByServiceID(SmartPortal.Constant.IPC.SMS, model.productId, 0, string.Empty, ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];

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


        //lay role mac dinh MB
        tblRoleDefault = (new SmartPortal.SEMS.Role().GetRoleDefaultByServiceID(SmartPortal.Constant.IPC.MB, model.productId, 0, string.Empty, ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];

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

        //lay role mac dinh PHO
        tblRoleDefault = (new SmartPortal.SEMS.Role().GetRoleDefaultByServiceID(SmartPortal.Constant.IPC.PHO, model.productId, 0, string.Empty, ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];

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

    protected void btnAddNewUser_Click(object sender, EventArgs e)
    {
        lblError.Text = string.Empty;
        rdAdd.Checked = true;
        rdAdd.Enabled = true;
        rdEdit.Checked = false;
        rdEdit.Enabled = true;
        txtMTCuscode.Text = string.Empty;
		txtLocalFulname.Text = string.Empty;
		ddlMTGender.SelectedIndex = 0;
        ddlUserType.SelectedIndex = 0;
        txtMTCuscode.Enabled = true;
        DivExistUser.Visible = true;
        btnAddUserMT.Visible = true;
        foreach (TreeNode node in tvRole.Nodes)
        {
            node.Checked = false;
        }
        txtMTFullName.Text = string.Empty;
        txtMTEmail.Text = string.Empty;
        txtMTPhone.Text = string.Empty;
        ddlMTGender.SelectedIndex = 0;
        txtMTBirth.Text = string.Empty;
        txtMTBirth.Enabled = true;
        txtMTAddress.Text = string.Empty;
        //ddlUserLevel.SelectedIndex = 0;
        //ddlGroup.SelectedIndex = 0;
        //hdGroupID.Value = string.Empty;
        ddlPolicyIB.SelectedIndex = 0;

        txtIBTypeUserName.Text = string.Empty;

        txtUserNameMB.Text = string.Empty;
        //ddlPolicyMB.SelectedIndex = 0;

        btnMTCancel.Visible = false;
        //btnAddUserMT.Visible = rbIBType.Checked;

        gvMTUser.DataSource = new DataTable();
        gvMTUser.DataBind();

        rdAdd.Checked = true;

        btnAddNewUser.Visible = false;

        GetRoleDefault(tvIBRole, new TreeView(), tvMB, new TreeView());

        ViewState["DataCurrent"] = null;
        if (ViewState["MTRUSER"] != null)
        {
            DataTable tblSUM = (DataTable)ViewState["MTRUSER"];
            var distinctUser = (from DataRow dRow in tblSUM.Rows
                                select new
                                {
                                    fullname = dRow["colFullName"],
                                    order = dRow["colOrder"]
                                }).Distinct().ToList();
            if (distinctUser.Count > 0)
            {
                ddlUser.DataSource = distinctUser;
                ddlUser.DataTextField = "fullname";
                ddlUser.DataValueField = "order";
                ddlUser.DataBind();
            }
            else
            {
                ddlUser.DataSource = new List<object>();
                ddlUser.DataBind();
            }
        }
        DisableOrEnableUser(true);
        USERID = new SmartPortal.SEMS.User().CreatePrimaryKey(string.Empty, string.Empty, "USERID", ref IPCERRORCODE, ref IPCERRORDESC);
        if (isWallet.Checked)
        {
            DataTable dtAccountQT = (DataTable)ViewState["ACCOUNTCORE"];
            lstAccount.SelectedValue = dtAccountQT.Rows[0]["ACCOUNTNO"].ToString();
        }
        LoadDataALLRoleInTreeview(string.Empty, tvRole, ddlUserType.SelectedValue, SmartPortal.Constant.IPC.NORMAL);
        CheckAllRole();
        ACTION = SmartPortal.Constant.IPC.ADD;
    }
    private static int NumberReg = 0;

    protected void CreateUserName_Click(object sender, EventArgs e)
    {
        NumberReg = 0;
        LinkButton textBox = sender as LinkButton;
        if (int.Parse(ConfigurationManager.AppSettings["IBMBSameUser"].ToString()) == 1)
        {
            txtIBTypeUserName.Text = txtUserNameMB.Text = GenerateUserName(txtIBTypeUserName);
        }
        else
        {
            if (textBox.Equals(lbIBGenerate))
            {
                txtIBTypeUserName.Text = txtUserNameMB.Text = GenerateUserName(txtIBTypeUserName);
            }
            else
            {
                txtUserNameMB.Text = txtUserNameMB.Text = GenerateUserName(txtUserNameMB);
            }
        }
    }
    protected void ChangeUserName_TextChanged(object sender, EventArgs e)
    {
        if (txtIBTypeUserName.Text.Length < minlength || txtIBTypeUserName.Text.Length > maxlength)
        {
            lblError.Text = string.Format(Resources.labels.usernamemustbetween, minlength, maxlength);
            return;
        }
        txtUserNameMB.Text = txtIBTypeUserName.Text;
        if (!CheckUserNameExists(txtIBTypeUserName.Text, txtUserNameMB) || !CheckUserNameInCust(txtIBTypeUserName.Text.Trim(), USERID))
        {
            lblError.Text = Resources.labels.usernamehasexists;
            return;
        }
        else
        {
            lblError.Text = string.Empty;
        }
        //update username 

    }
    private string GenerateUserName(TextBox textBox)
    {
        NumberReg++;
        string resultUserName = new Customer().UsernameGeneration("6", ref IPCERRORCODE, ref IPCERRORDESC);
        if (resultUserName.Equals(string.Empty) || resultUserName.Length != 10 || !CheckUserNameExists(resultUserName, textBox))
        {
            if (NumberReg < 10)
            {
                return GenerateUserName(textBox);
            }
            else
            {
                lblError.Text = IPCERRORDESC;
                return string.Empty;
            }
        }
        lblError.Text = string.Empty;
        return resultUserName;
    }
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

    bool validateUser()
    {
        bool result = true;
        if (txtMTFullName.Text.Trim().Equals(string.Empty))
        {
            lblError.Text = Resources.labels.banphainhaptenuser;
            txtMTFullName.Focus();
            result = false;
        }
        else if (txtMTEmail.Text.Trim().Equals(string.Empty))
        {
            lblError.Text = Resources.labels.banphainhapemailuser;
            txtMTEmail.Focus();
            result = false;
        }
        else if (txtMTPhone.Text.Trim().Equals(string.Empty))
        {
            lblError.Text = Resources.labels.banphainhapphoneuser;
            txtMTPhone.Focus();
            result = false;
        }

        //DUYVK 20190704
        else if (ViewState["MTRUSER"] != null)
        {
            //if (!ddlIBAccount.SelectedValue.Equals("ALL"))
            //{
            //    dt.DefaultView.RowFilter = "colIBUSERNAME = '" + userID + "' and colAccount='" + ddlIBAccount.SelectedValue + "'";
            //}
            //else
            //{
            //    dt.DefaultView.RowFilter = "colIBUSERNAME = '" + userID + "'";
            //}
            //if (dt.DefaultView.Count > 0)
            //{
            //    txtIBTypeUserName.Focus();
            //    lblError.Text = Resources.labels.Duplicatedusername;
            //    result = false;
            //}

            //result = true;
            result = true;
        }

        return result;
    }

    protected void btnAddUserMT_Click(object sender, EventArgs e)
    {
        if (!validateUser() || string.IsNullOrEmpty(USERID))
            return;
        try
        {
            int passlenIB = 0;
            int passlenMB = 0;

            UserContractInfo info = new UserContractInfo();
            //check userib
            info.MBUserID = info.IBUserID = USERID;
            info.isRenderUserName = false;
            info.IBUserName = txtIBTypeUserName.Text.Trim();
            info.MBUserName = txtUserNameMB.Text.Trim();
            info.MBPhone = txtPhoneMB.Text.Trim();
            #region check userName
            if (!CheckPhoneInCust(txtMTPhone.Text.Trim(), USERID))
            {
                lblError.Text = Resources.labels.phonenumberisalreadyregistered;
                return;
            }
            if (!CheckExistPhoneNumber(txtMobi.Text.Trim()))
            {
                lblError.Text = Resources.labels.phonenumberisalreadyregistered;
                return;
            }
            if (string.IsNullOrEmpty(info.IBUserName))
            {
                lblError.Text = Resources.labels.bancannhaptendangnhap;
                return;
            }
            if (string.IsNullOrEmpty(info.MBPhone))
            {
                lblError.Text = Resources.labels.bancannhapsodienthoainguoisudung;
                return;
            }
            if (string.IsNullOrEmpty(txtMTBirth.Text.Trim()))
            {
                lblError.Text = Resources.labels.Youneedtoinputdateofbirth;
                return;
            }
            if (!CheckIsPhoneNumer(info.MBPhone))
            {
                lblError.Text = Resources.labels.phonenumberwrong;
                return;
            }
            else if (info.IBUserName.Length < minlength || info.IBUserName.Length > maxlength)
            {
                lblError.Text = string.Format(Resources.labels.usernamemustbetween, minlength, maxlength);
                return;
            }
            txtUserNameMB.Text = txtIBTypeUserName.Text;
            if (!CheckUserNameExists(txtIBTypeUserName.Text, txtUserNameMB) || !CheckUserNameInCust(txtIBTypeUserName.Text.Trim(), USERID))
            {
                lblError.Text = Resources.labels.usernamehasexists;
                return;
            }
            else
            {
                lblError.Text = string.Empty;
            }
            DataSet dsUsername = new Customer().CheckUserName("EBA_Users_CheckUserName", new object[] { info.IBUserName }, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE != "0")
            {
                lblError.Text = IPCERRORDESC;
                txtIBTypeUserName.Focus();
                return;
            }
            if (!validateusername(txtIBTypeUserName))
            {
                return;
            }
            #endregion

            if (isWallet.Checked)
            {
                USEWALLET = true;
            }
            string dateExpire_date = new SmartPortal.SEMS.EBASYSVAR().ViewDetail("YEAROLD", ref IPCERRORCODE, ref IPCERRORDESC).Tables[0].Rows[0]["VARVALUE"].ToString();
            int year = Int32.Parse(dateExpire_date);
            if (SmartPortal.Common.Utilities.Utility.IsDateTime1(txtMTBirth.Text) >= DateTime.Now.AddYears(-year))
            {
                lblError.Text = "Invalid Birth Day";
                txtMTBirth.Enabled = true;
                return;
            }
            DataTable dtCR = ViewState["DataCurrent"] != null ? (DataTable)ViewState["DataCurrent"] : new DataTable();

            //Tao key cho user, dung khi edit user
            if (dtCR.Rows.Count == 0)
                info.order = DateTime.Now.GetHashCode().ToString();
            else
                info.order = dtCR.Rows[0]["colOrder"].ToString();

            DataTable dtUser = ViewState["MTRUSER"] != null ? (DataTable)ViewState["MTRUSER"] : new DataTable();


            info.cusCode = txtMTCuscode.Text.Trim();
            info.fullName = txtMTFullName.Text;
            info.localName = txtLocalFulname.Text;
            info.level = "0";//ddlUserLevel.Text;
                             //info.group = ddlGroup.SelectedValue;
            info.dob = txtMTBirth.Text;
            info.gender = ddlMTGender.SelectedValue;
            info.phone = txtMTPhone.Text;
            info.email = txtMTEmail.Text;
            info.address = txtMTAddress.Text;
            info.SMSPhone = "";
            info.SMSDefaultAcctno = "";
            info.SMSDefaultLang = "";
            info.PHODefaultAcctno = "";
            info.loginMethod = SmartPortal.Constant.IPC.USERNAME;
            info.authenType = SmartPortal.Constant.IPC.PASSWORD;

            #region Tao bang chua cac thong tin user

            // load infor about policy selected to get len of pass generate
            DataSet dspolicy = new DataSet();
            string filterIB = "serviceid='IB' and policyid='" + ddlPolicyIB.SelectedValue.ToString() + "'";
            //string filterMB = "serviceid='MB' and policyid='" + ddlPolicyMB.SelectedValue.ToString() + "'";
            string stSort = "serviceid asc";

            dspolicy = new SmartPortal.SEMS.USERPOLICY().GetPolicybyCondition(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, Session["userName"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE.Equals("0"))
            {
                DataTable dtIB = dspolicy.Tables[0].Select(filterIB, stSort).CopyToDataTable();
                // DataTable dtMB = dspolicy.Tables[0].Select(filterMB, stSort).CopyToDataTable();
                passlenIB = Convert.ToInt32(dtIB.Rows[0]["minpwdlen"].ToString());
                // passlenMB = Convert.ToInt32(dtMB.Rows[0]["minpwdlen"].ToString());
            }

            //if (ConfigurationManager.AppSettings["IBMBSamePassword"].ToString().Equals("1"))
            //{
    //            string passreveal = DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - passlenIB, passlenMB);
				//info.pwdresetIB = Encryption.Encrypt(passreveal);
    //            info.IBUserPass = SmartPortal.SEMS.O9Encryptpass.sha_sha256(passreveal, info.IBUserID);
    //        //}
            //else
            //{
                string passreveal = DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - passlenIB, passlenIB);
                info.pwdresetIB = Encryption.Encrypt(passreveal);
                info.IBUserPass = SmartPortal.SEMS.O9Encryptpass.sha_sha256(passreveal, info.IBUserID);

                // string passrevealMB = DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - passlenMB, passlenMB);
                // info.pwdresetMB = Encryption.Encrypt(passrevealMB);
                // info.MBPass = SmartPortal.SEMS.O9Encryptpass.sha_sha256(passrevealMB, info.IBUserID);
            //}
            info.SMSPinCode = "";

            if (int.Parse(ConfigurationManager.AppSettings["IBMBSameUser"].ToString()) == 1)
            {
                info.MBUserName = info.IBUserName;
            }

            //Luu account va user cua contract
            List<string> lsAccNo = new List<string>();

            if (!ddlIBAccount.SelectedValue.Equals("ALL"))
            {
                lsAccNo.Add(ddlIBAccount.SelectedValue);
            }
            else
            {
                foreach (var item in ddlIBAccount.Items.Cast<ListItem>().Select(i => i.Value))
                {
                    if (!item.Equals("ALL")) lsAccNo.Add(item);
                }

                ////lay tat ca tai khoan khach hang
                //DataSet ds = new SmartPortal.SEMS.Customer().GetAcctNo(model.cusCode, model.cusType, ref IPCERRORCODE, ref IPCERRORDESC);
                //if (IPCERRORCODE != "0")
                //{
                //    throw new IPCException(IPCERRORDESC);
                //}
                //DataTable dtAccount = ds.Tables[0];
                ////luu tat ca account
                //foreach (DataRow rowAccount in dtAccount.Rows)
                //{
                //    lsAccNo.Add(rowAccount["ACCOUNTNO"].ToString());
                //}
            }

            SaveRoleInfo(info, gvMTUser, tvIBRole, new TreeView(), tvMB, new TreeView(), lsAccNo);
            btnAddNewUser.Visible = true;
            btnMTCancel.Visible = true;
            lblError.Text = Resources.labels.recordsaved;
            // SetControlEnable();

            DisableOrEnableUser(false);
            btnCustSave.Enabled = true;
            //btnAddUserMT.Visible = false;
            #endregion


            ACTION = SmartPortal.Constant.IPC.DETAILS;
        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    void SaveRoleInfo(UserContractInfo info, GridView gvResult, TreeView tvRole, TreeView tvSMSRole, TreeView tvMBRole, TreeView tvPHORole, List<string> lsAccNo)
    {
        // khoi tao bang de luu user info hien tai
        DataTable tblUser = ViewState["DataCurrent"] != null ? (DataTable)ViewState["DataCurrent"] : ContructorTableUser();
        // table dung luu tat ca user cua contract
        DataTable dtUser = ViewState["MTRUSER"] != null ? (DataTable)ViewState["MTRUSER"] : ContructorTableUser();
        foreach (var account in lsAccNo)
        {
            //IB
            if (info.IBUserName != "")
            {
                UpdateViewState(dtUser, tblUser, tvRole, info, SmartPortal.Constant.IPC.IB, account, false);
            }

            //SMS
            if (info.SMSPhone != "")
            {
                UpdateViewState(dtUser, tblUser, tvSMSRole, info, SmartPortal.Constant.IPC.SMS, account, false);
            }

            //MB
            if (info.MBPhone != "")
            {
                UpdateViewState(dtUser, tblUser, tvMBRole, info, SmartPortal.Constant.IPC.MB, account, false);
            }

            //PHO
            if (info.PHOPhone != "")
            {
                UpdateViewState(dtUser, tblUser, tvPHORole, info, SmartPortal.Constant.IPC.PHO, account, false);
            }
        }
        btnAddNewUser.Visible = tblUser.Rows.Count > 0;
        ViewState["MTRUSER"] = dtUser;
        ViewState["DataCurrent"] = tblUser;
        gvResult.DataSource = tblUser.DefaultView.ToTable(true, "colFullName", "colIBUserName", "colAccount", "colRole");
        gvResult.DataBind();

        //ddlUser.DataSource = dtUser.DefaultView.ToTable(true, "colFullName", "colOrder");

        //ddlUser.DataTextField = "colFullName";
        //ddlUser.DataValueField = "colOrder";
        //ddlUser.DataBind();
        if (tblUser.Rows.Count > 0)
        {
            ListItem item = new ListItem();
            item.Text = tblUser.Rows[0]["colFullName"].ToString();
            item.Value = tblUser.Rows[0]["colOrder"].ToString();
            ddlUser.Items.Add(item);
            ddlUser.Enabled = rdEdit.Checked;
        }

    }

    DataTable ContructorTableUser()
    {
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
        DataColumn colIsRenderUserName = new DataColumn("colIsRenderUserName");
        DataColumn colIBUserName = new DataColumn("colIBUserName");
        DataColumn colUserID = new DataColumn("colUserID");
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
        DataColumn colTypeID = new DataColumn("colTypeID");
        DataColumn colOrder = new DataColumn("colOrder");


        DataColumn colIBPolicy = new DataColumn("colIBPolicy");
        DataColumn colSMSPolicy = new DataColumn("colSMSPolicy");
        DataColumn colMBPolicy = new DataColumn("colMBPolicy");
        DataColumn colpwdresetIB = new DataColumn("colpwdresetIB");
        DataColumn colpwdresetMB = new DataColumn("colpwdresetMB");

        DataTable tblUser = new DataTable();
        tblUser.Columns.Add(colFullName);
        tblUser.Columns.Add(colLocalname);
        tblUser.Columns.Add(colLoginMethod);
        tblUser.Columns.Add(colAuthenType);
        tblUser.Columns.Add(colLevel);
        tblUser.Columns.Add(colGroup);
        tblUser.Columns.Add(colBirthday);
        tblUser.Columns.Add(colGender);
        tblUser.Columns.Add(colPhone);
        tblUser.Columns.Add(colEmail);
        tblUser.Columns.Add(colAddress);
        tblUser.Columns.Add(colIsRenderUserName);
        tblUser.Columns.Add(colIBUserName);
        tblUser.Columns.Add(colUserID);
        tblUser.Columns.Add(colIBPass);
        tblUser.Columns.Add(colSMSPhone);
        tblUser.Columns.Add(colSMSDefaultAcctno);
        tblUser.Columns.Add(colSMSDefaultLang);
        tblUser.Columns.Add(colSMSIsDefault);
        tblUser.Columns.Add(colSMSPinCode);
        tblUser.Columns.Add(colMBPhone);
        tblUser.Columns.Add(colMBPass);
        tblUser.Columns.Add(colMBPinCode);
        tblUser.Columns.Add(colRoleType);
        tblUser.Columns.Add(colWLPhone);
        tblUser.Columns.Add(colWLPass);
        tblUser.Columns.Add(colWLPinCode);
        tblUser.Columns.Add(colAccount);
        tblUser.Columns.Add(colRole);
        tblUser.Columns.Add(colRoleID);
        tblUser.Columns.Add(colTranCode);
        tblUser.Columns.Add(colTranCodeID);
        tblUser.Columns.Add(colServiceID);

        tblUser.Columns.Add(colTypeID);
        tblUser.Columns.Add(colOrder);
        tblUser.Columns.Add(colIBPolicy);
        tblUser.Columns.Add(colSMSPolicy);
        tblUser.Columns.Add(colMBPolicy);
        tblUser.Columns.Add(colpwdresetIB);
        tblUser.Columns.Add(colpwdresetMB);
        return tblUser;
    }

    void UpdateViewState(DataTable dtVt, DataTable dtCR, TreeView role, UserContractInfo info, string serviceid, string account, bool ispho)
    {
        foreach (TreeNode nodeRole in role.Nodes)
        {
            foreach (DataRow r in dtCR.Select("colOrder='" + info.order + "' and colRoleID='" + nodeRole.Value + "' and colAccount='" + account + "'"))
            {
                dtCR.Rows.Remove(r);
            }
            if (nodeRole.Checked)
            {
                // them 1 row vao gridview
                dtCR.Rows.Add(CreateRowToAddUser(dtCR, info, nodeRole, null, serviceid, account, ispho));
            }
            foreach (TreeNode nodeTrancode in nodeRole.ChildNodes)
            {
                //Delete row exit  and add new row in viewstate
                foreach (DataRow r in dtVt.Select("colOrder='" + info.order + "' and colRoleID='" + nodeRole.Value + "' and colAccount='" + account + "' and colTranCode='" + nodeTrancode.Text + "'"))
                {
                    dtVt.Rows.Remove(r);
                }
                if (nodeRole.Checked)
                {
                    dtVt.Rows.Add(CreateRowToAddUser(dtVt, info, nodeRole, nodeTrancode, serviceid, account, ispho));
                }
            }

        }
    }

    void SetControlEnable()
    {
        txtMTCuscode.Enabled = rdAdd.Checked;
        //ddlUser.Enabled = rdEdit.Checked;
    }

    DataRow CreateRowToAddUser(DataTable tblUser, UserContractInfo info, TreeNode nodeRole, TreeNode nodeTrancode, string serviceID, string Account, bool ispho)
    {
        DataRow rowUser = tblUser.NewRow();
        rowUser["colFullName"] = info.fullName;
        rowUser["colLocalname"] = info.localName;
        rowUser["colLoginMethod"] = info.loginMethod;
        rowUser["colAuthenType"] = info.authenType;
        rowUser["colLevel"] = info.level;
        rowUser["colGroup"] = info.group;
        rowUser["colBirthday"] = info.dob;
        rowUser["colGender"] = info.gender;
        rowUser["colPhone"] = info.phone;
        rowUser["colEmail"] = info.email;
        rowUser["colAddress"] = info.address;
        rowUser["colIsRenderUserName"] = info.isRenderUserName;
        rowUser["colUserID"] = info.IBUserID;
        rowUser["colIBUserName"] = info.IBUserName;
        rowUser["colIBPass"] = info.IBUserPass;
        rowUser["colSMSPhone"] = info.SMSPhone;
        rowUser["colSMSDefaultAcctno"] = info.SMSDefaultAcctno;
        rowUser["colSMSDefaultLang"] = info.SMSDefaultLang;
        rowUser["colSMSIsDefault"] = info.isDefault;
        rowUser["colSMSPinCode"] = info.SMSPinCode;
        rowUser["colMBPinCode"] = info.MBPinCode;
        rowUser["colMBPhone"] = info.MBPhone;
        rowUser["colMBPass"] = info.MBPass;
        rowUser["colRoleType"] = SmartPortal.Constant.IPC.NORMAL;
        rowUser["colWLPinCode"] = "";
        rowUser["colWLPhone"] = "";
        rowUser["colWLPass"] = "";
        rowUser["colAccount"] = Account;
        rowUser["colRole"] = nodeRole.Text;
        rowUser["colRoleID"] = nodeRole.Value;
        if (nodeTrancode != null)
        {
            rowUser["colTranCode"] = nodeTrancode.Text;
            rowUser["colTranCodeID"] = nodeTrancode.Value;
        }

        rowUser["colServiceID"] = serviceID;
        rowUser["colTypeID"] = Utility.KillSqlInjection(ddlUserType.SelectedValue.Trim());
        rowUser["colOrder"] = info.order;

        if (!ispho)
        {
            rowUser["colIBPolicy"] = ddlPolicyIB.SelectedValue.ToString();
            rowUser["colSMSPolicy"] = "";
            //rowUser["colMBPolicy"] = ddlPolicyMB.SelectedValue.ToString();
        }
        rowUser["colpwdresetIB"] = info.pwdresetIB;
        //rowUser["colpwdresetMB"] = info.pwdresetMB;
        return rowUser;
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        pnCustInfo.Visible = true;
        pnCorp.Visible = false;
        pnLuu.Visible = false;
    }

    protected void btnCustSave_Click(object sender, EventArgs e)
    {
        try
        {
            //Check session
            if (ViewState["MTRUSER"] == null)
            {
                lblError.Text = Resources.labels.bancandiendayduthongtinuser;
                return;
            }

            DataTable tblSUM = (DataTable)ViewState["MTRUSER"];
            if (tblSUM.Rows.Count == 0)
            {
                lblError.Text = Resources.labels.bancandiendayduthongtinuser;
                return;
            }

            //Kiem tra user co nhap email hoac so dien thoai khi dang ky sms service hay khong.
            string emailctk = "";
            string smsphonectk = "";

            foreach (DataRow r in tblSUM.Rows)
            {
                if (r["colServiceID"].ToString() == SmartPortal.Constant.IPC.SMS)
                {
                    smsphonectk = r["colSMSPhone"].ToString();
                    emailctk = r["colEmail"].ToString();
                    if (emailctk == string.Empty && smsphonectk == string.Empty)
                    {
                        lblError.Text = Resources.labels.chutaikhoanphaidangkyitnhatemailhoacsmsservice;
                        return;
                    }
                }
            }

            DataTable tblAccountRole = ViewState["ACCOUNTROLE"] != null ? (DataTable)ViewState["ACCOUNTROLE"] : CreateTableAccontRole();

            #region Biến thông tin Customer và Contract

            model.addrTemp = string.Empty;
            model.gender = string.Empty;
            model.nation = string.Empty;
            model.licenseType = "";
            model.issueDate = SmartPortal.Constant.IPC.DATEDEFAULT;
            model.issuePlace = string.Empty;
            model.job = string.Empty;
            model.officeAddr = string.Empty;
            model.officePhone = string.Empty;
            //string cfcode = "";
            model.isAutorenew = "Y";
            model.branchId = Session["branch"].ToString();

            model.lastModify = DateTime.Now.ToString("dd/MM/yyyy");
            model.userCreate = Session["userName"].ToString();
            model.userLastModify = Session["userName"].ToString();
            model.userApprove = "";
            model.status = SmartPortal.Constant.IPC.NEW;
            model.allAcct = "Y";
            model.isSpecialMan = "";
            model.branchId = Session["branch"].ToString();
            model.createDate = DateTime.Now.ToString("dd/MM/yyyy");
            #endregion



            string deptID = "";
            string tokenID = "";
            string tokenIssueDate = "01/01/1900";
            string smsOTP = "";

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
            DataColumn[] colUsers = new DataColumn[] { colUserID, colUContractNo, colULocalName,colUFullName, colUGender, colUAddress , colUEmail , colUPhone
                    , colUStatus,colUUserCreate,colUDateCreate,colUUserModify,colULastModify ,colUUserApprove,colUserType,colUserLevel,colDeptID,colTokenID
                    ,colTokenIssueDate,colSMSOTP,colSMSBirthday,colTypeIDUser,colUSContractType};
            //add column vào table
            tblUser.Columns.AddRange(colUsers);



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

            DataColumn[] colbankUser = new DataColumn[] { colUserName, colIBUserID, colIBPassword, colLastLoginTime, colIBStatus
                    , colIBUserCreate, colIBDateCreate , colIBUserModify,colIBLastModify,colIBUserApprove,colIBIsLogin ,colIBDateExpire
                    ,colIBExpireTime,colIBPolicyusr,colpwdresetIB,colIBLoginMethod,colIBAuthenType};




            //add column vào table
            tblIbankUser.Columns.AddRange(colbankUser);
            #endregion

            var distinctUser = (from DataRow dRow in tblSUM.Rows
                                select new
                                {
                                    userid = dRow["colUserID"],
                                    username = dRow["colIBUserName"],
                                    ibpass = dRow["colIBPass"],
                                    fullname = dRow["colFullName"],
                                    localname = dRow["colLocalname"],
                                    gender = dRow["colGender"],
                                    address = dRow["colAddress"],
                                    email = dRow["colEmail"],
                                    phone = dRow["colPhone"],
                                    level = dRow["colLevel"],
                                    dob = dRow["colBirthday"],
                                    typeid = dRow["colTypeID"],
                                    policy = dRow["colIBPolicy"],
                                    pwdreset = dRow["colpwdresetIB"],
                                    loginMethod = dRow["colLoginMethod"],
                                    authenType = dRow["colAuthenType"],
                                }).Distinct().ToList();
            //add row vafo table user infor vaf ib user
            foreach (var item in distinctUser)
            {
                // add row in table user info
                if (tblUser.Select("colUserID='" + item.userid + "'").Length == 0 && !string.IsNullOrEmpty(item.userid.ToString()))
                {
                    DataRow row2 = tblUser.NewRow();
                    row2["colUserID"] = item.userid;
                    row2["colUContractNo"] = model.contractNo;
                    row2["colUlocalname"] = item.localname;
                    row2["colUFullName"] = item.fullname;
                    row2["colUGender"] = item.gender;
                    row2["colUAddress"] = item.address;
                    row2["colUEmail"] = item.email;
                    row2["colUPhone"] = item.phone;
                    row2["colUStatus"] = model.status;
                    row2["colUUserCreate"] = model.userCreate;
                    row2["colUDateCreate"] = model.createDate;
                    row2["colUUserModify"] = model.userCreate;
                    row2["colULastModify"] = model.lastModify;

                    row2["colUUserApprove"] = model.userApprove;
                    row2["colUserType"] = model.contractType;
                    row2["colUserLevel"] = item.level;
                    row2["colDeptID"] = deptID;
                    row2["colTokenID"] = tokenID;
                    row2["colTokenIssueDate"] = tokenIssueDate;
                    row2["colSMSOTP"] = smsOTP;
                    row2["colSMSBirthday"] = item.dob;
                    row2["colTypeID"] = item.typeid;
                    row2["colUSContractType"] = model.corpType;
                    tblUser.Rows.Add(row2);
                }


                //add row in user bank table
                if (tblIbankUser.Select("colIBUserID='" + item.userid + "'").Length == 0 && !string.IsNullOrEmpty(item.userid.ToString()))
                {
                    DataRow row3 = tblIbankUser.NewRow();
                    row3["colUserName"] = item.username;
                    row3["colIBUserID"] = item.userid;
                    row3["colIBPassword"] = item.ibpass;
                    row3["colLastLoginTime"] = model.createDate;
                    row3["colIBStatus"] = model.status;
                    row3["colIBUserCreate"] = model.userCreate;
                    row3["colIBDateCreate"] = model.createDate;
                    row3["colIBUserModify"] = model.userCreate;
                    row3["colIBLastModify"] = model.lastModify;
                    row3["colIBUserApprove"] = model.userApprove;
                    row3["colIBIsLogin"] = "0";
                    row3["colIBDateExpire"] = model.endDate;
                    row3["colIBExpireTime"] = model.createDate;
                    row3["colIBPolicyusr"] = item.policy;
                    row3["colpwdresetIB"] = item.pwdreset;
                    row3["colIBLoginMethod"] = item.loginMethod;
                    row3["colIBAuthenType"] = item.authenType;
                    tblIbankUser.Rows.Add(row3);

                }
            }



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

            DataColumn[] colSMSUser = new DataColumn[] { colSMSUserID, colSMSPhoneNo, colSMSContractNo, colSMSIsBroadcast, colSMSDefaultAcctnoU
                    , colSMSDefaultLangU, colSMSIsDefault1 , colSMSPinCode1,colSMSStatus,colSMSPhoneType,colSMSUserCreate ,colSMSUserModify
                    ,colSMSUserApprove,colSMSLastModify,colSMSDateCreate,colSMSDateExpire,colSMSPolicyusr,colpwdresetSMS};
            //add column vào table
            tblSMSUser.Columns.AddRange(colSMSUser);

            var distinctSMSUser = (from DataRow dRow in tblSUM.Rows
                                   select new
                                   {
                                       username = dRow["colIBUserName"],
                                       smsphone = dRow["colSMSPhone"],
                                       smsdfAcct = dRow["colSMSDefaultAcctno"],
                                       smsdfLang = dRow["colSMSDefaultLang"],
                                       smsisdf = dRow["colSMSIsDefault"],
                                       smspin = dRow["colSMSPinCode"],
                                       smspolicy = dRow["colSMSPolicy"]
                                   }).Distinct().Where(m => m.smsphone.ToString() != string.Empty).ToList();
            //tao 1 dong du lieu
            foreach (var item in distinctSMSUser)
            {
                // add row in table user info
                DataRow row = tblSMSUser.NewRow();
                row["colSMSUserID"] = item.username;
                row["colSMSPhoneNo"] = item.smsphone;
                row["colSMSContractNo"] = model.contractNo;
                row["colSMSIsBroadcast"] = "Y";
                row["colSMSDefaultAcctno"] = item.smsdfAcct;
                row["colSMSDefaultLang"] = item.smsdfLang;
                row["colSMSIsDefault1"] = item.smsisdf;
                row["colSMSPinCode1"] = SmartPortal.SEMS.O9Encryptpass.sha_sha256(item.smspin.ToString(), item.smsphone.ToString().Trim());
                row["colSMSStatus"] = model.status;
                row["colSMSPhoneType"] = "";
                row["colSMSUserCreate"] = model.userCreate;
                row["colSMSUserModify"] = model.userCreate;
                row["colSMSUserApprove"] = model.userApprove;
                row["colSMSLastModify"] = model.lastModify;
                row["colSMSDateCreate"] = model.createDate;
                row["colSMSDateExpire"] = model.endDate;
                row["colSMSPolicyusr"] = item.smspolicy;
                row["colpwdresetSMS"] = item.smspin;
                tblSMSUser.Rows.Add(row);
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

            DataColumn[] colMBUser = new DataColumn[] { colMBUserID, colMBUserName, colMBPhoneNo, colMBLoginMethod, colMBAuthenType, colMBNewPass,colMBNewPin,
                    colMBPassU, colMBStatus, colMBPinCode1, colMBPolicyusr, colpwdresetMB,colPwdPinMB,colContractType};

            tblMBUser.Columns.AddRange(colMBUser);
            var distinctMBUser = (from DataRow dRow in tblSUM.Rows
                                  select new
                                  {
                                      userid = dRow["colUserID"],
                                      username = dRow["colIBUserName"],
                                      phone = dRow["colMBPhone"],
                                      loginMetod = "USERNAME",
                                      authenType = "PASSWORD",
                                      newPass = "Y",
                                      newPin = "N",
                                      passU = dRow["colMBPass"],
                                      status = SmartPortal.Constant.IPC.NEW,
                                      pincode1 = string.Empty,
                                      policyusr = dRow["colMBPolicy"],
                                      pwdReset = dRow["colpwdresetMB"],
                                      pwdPin = string.Empty,
                                      contractType = SmartPortal.Constant.IPC.CORPORATECONTRACT,

                                  }).Distinct().Where(m => m.phone.ToString() != string.Empty).ToList();
            //tao 1 dong du lieu
            foreach (var item in distinctMBUser)
            {
                if (tblMBUser.Select("colMBUserID='" + item.userid + "'").Length == 0 && !string.IsNullOrEmpty(item.userid.ToString()))
                {
                    DataRow row = tblMBUser.NewRow();
                    row["colMBUserID"] = item.userid;
                    row["colMBUserName"] = item.username;
                    row["colMBPhoneNo"] = item.phone;
                    row["colMBLoginMethod"] = item.loginMetod;
                    row["colMBAuthenType"] = item.authenType;
                    row["colMBNewPass"] = item.newPass;
                    row["colMBNewPin"] = item.newPin;
                    row["colMBPass"] = item.passU;
                    row["colMBStatus"] = model.status;
                    row["colMBPinCode1"] = item.pincode1;
                    row["colMBPolicyusr"] = 1;
                    row["colpwdresetMB"] = item.pwdReset;
                    row["colPwdPinMB"] = item.pwdPin;
                    row["colContractType"] = item.contractType;
                    tblMBUser.Rows.Add(row);
                }
            }

            #endregion

            #region Tạo bảng chứa User PHO
            DataTable tblPHOUser = new DataTable();
            DataColumn colPHOUserID = new DataColumn("colPHOUserID");
            DataColumn colPHOPhoneNo = new DataColumn("colPHOPhoneNo");
            DataColumn colPHOPassU = new DataColumn("colPHOPass");
            DataColumn colPHOStatus = new DataColumn("colPHOStatus");
            DataColumn colPHODefaultAcctno1 = new DataColumn("colPHODefaultAcctno1");
            DataColumn[] colPHOUser = new DataColumn[] { colPHOUserID, colPHOPhoneNo, colPHOPassU, colPHOStatus, colPHODefaultAcctno1 };

            tblPHOUser.Columns.AddRange(colPHOUser);
            //var distinctPHOUser = (from DataRow dRow in tblSUM.Rows
            //                       select new
            //                       {
            //                           username = dRow["colIBUserName"],
            //                           phone = dRow["colPHOPhone"],
            //                           pass = dRow["colPHOPass"],
            //                           dtacct = dRow["colPHODefaultAcctno"]
            //                       }).Distinct().Where(m => m.phone.ToString() != string.Empty).ToList();
            ////tao 1 dong du lieu
            //foreach (var item in distinctPHOUser)
            //{
            //    DataRow row = tblPHOUser.NewRow();
            //    row["colPHOUserID"] = item.username;
            //    row["colPHOPhoneNo"] = item.phone;
            //    row["colPHOPass"] = item.pass;
            //    row["colPHOStatus"] = model.status;
            //    row["colPHODefaultAcctno1"] = item.dtacct;

            //    tblPHOUser.Rows.Add(row);
            //}
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
            DataRow[] arrIB = tblAccountRole.Select("colServiceID='" + SmartPortal.Constant.IPC.IB + "'");
            foreach (DataRow rWLR in arrIB)
            {
                if (tblIbankUserRight.Select("colIBUserNameRight='" + rWLR["coluserID"].ToString() + "' and colIBRoleID='" + rWLR["colRoleID"].ToString() + "'").Length == 0)
                {
                    if (tblIbankUser.Select("colIBUserID='" + rWLR["coluserID"].ToString() + "'").Length != 0)
                    {
                        DataRow newRowIBR = tblIbankUserRight.NewRow();
                        newRowIBR["colIBUserNameRight"] = rWLR["coluserID"].ToString();
                        newRowIBR["colIBRoleID"] = rWLR["colRoleID"].ToString();

                        tblIbankUserRight.Rows.Add(newRowIBR);
                    }
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
            DataRow[] arrMB = tblAccountRole.Select("colServiceID='" + SmartPortal.Constant.IPC.MB + "'");
            foreach (DataRow rWLR in arrMB)
            {
                if (tblMBUserRight.Select("colMBUserIDU='" + rWLR["coluserID"].ToString() + "' and colMBRoleID='" + rWLR["colRoleID"].ToString() + "'").Length == 0)
                {
                    if (tblMBUser.Select("colMBUserID='" + rWLR["coluserID"].ToString() + "'").Length != 0)
                    {
                        DataRow newRowMBR = tblMBUserRight.NewRow();
                        newRowMBR["colMBUserIDU"] = rWLR["coluserID"].ToString();
                        newRowMBR["colMBRoleID"] = rWLR["colRoleID"].ToString();
                        newRowMBR["colMBRoleContracType"] = model.corpType;

                        tblMBUserRight.Rows.Add(newRowMBR);
                    }

                }
            }
            //add role wallet
            DataRow[] arrWL = tblAccountRole.Select("colServiceID='" + SmartPortal.Constant.IPC.WAL + "'");

            foreach (DataRow rWLR in arrWL)
            {
                try
                {
                    if (tblMBUserRight.Select("colMBUserIDU='" + rWLR["coluserID"].ToString() + "' and colMBRoleID='" + rWLR["colRoleID"].ToString() + "'").Length == 0)
                    {
                        DataTable role = new SmartPortal.SEMS.Role().GetRoleInforByID(rWLR["colRoleID"].ToString());
                        if (role.Rows[0]["ServiceID"].Equals(SmartPortal.Constant.IPC.MB))
                        {
                            if (tblMBUser.Select("colMBUserID='" + rWLR["coluserID"].ToString() + "'").Length != 0)
                            {
                                DataRow newRowMBR = tblMBUserRight.NewRow();
                                newRowMBR["colMBUserIDU"] = rWLR["coluserID"].ToString();
                                newRowMBR["colMBRoleID"] = rWLR["colRoleID"].ToString();
                                newRowMBR["colMBRoleContracType"] = model.corpType;

                                tblMBUserRight.Rows.Add(newRowMBR);
                            }

                        }

                    }
                    if (tblIbankUserRight.Select("colIBUserNameRight='" + rWLR["coluserID"].ToString() + "' and colIBRoleID='" + rWLR["colRoleID"].ToString() + "'").Length == 0)
                    {
                        DataTable role = new SmartPortal.SEMS.Role().GetRoleInforByID(rWLR["colRoleID"].ToString());
                        if (role.Rows[0]["ServiceID"].Equals(SmartPortal.Constant.IPC.IB))
                        {
                            if (tblIbankUser.Select("colIBUserID='" + rWLR["coluserID"].ToString() + "'").Length != 0)
                            {
                                DataRow newRowIBR = tblIbankUserRight.NewRow();
                                newRowIBR["colIBUserNameRight"] = rWLR["coluserID"].ToString();
                                newRowIBR["colIBRoleID"] = rWLR["colRoleID"].ToString();

                                tblIbankUserRight.Rows.Add(newRowIBR);
                            }

                        }
                    }
                }
                catch
                {

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
            //DataRow[] arrPHOR = tblSUM.Select("colServiceID='" + SmartPortal.Constant.IPC.PHO + "'");
            //foreach (DataRow rPHOR in arrPHOR)
            //{
            //    if (tblPHOUserRight.Select("colPHOPhoneNo='" + rPHOR["colPHOPhone"].ToString() + "' and colPHORoleID='" + rPHOR["colRoleID"].ToString() + "'").Length == 0)
            //    {
            //        DataRow newRowPHOR = tblPHOUserRight.NewRow();
            //        newRowPHOR["colPHOPhoneNo"] = rPHOR["colPHOPhone"].ToString();
            //        newRowPHOR["colPHORoleID"] = rPHOR["colRoleID"].ToString();

            //        tblPHOUserRight.Rows.Add(newRowPHOR);
            //    }
            //}
            #endregion

            #region Tạo bảng chứa group của user 
            DataTable tblUserGroup = new DataTable();
            DataColumn colUGUserID = new DataColumn("colUserID");
            DataColumn colUGGroupID = new DataColumn("colGroupID");

            //add vào table
            tblUserGroup.Columns.Add(colUGUserID);
            tblUserGroup.Columns.Add(colUGGroupID);
            //var distinctUserGroup = (from DataRow dRow in tblSUM.Rows
            //                         select new
            //                         {
            //                             username = dRow["colIBUserName"],
            //                             groupid = dRow["colGroup"]
            //                         }).Distinct().ToList();
            ////add row vafo table user infor vaf ib user
            //foreach (var item in distinctUserGroup)
            //{
            //    DataRow row = tblUserGroup.NewRow();
            //    row["colUserID"] = item.username;
            //    row["colGroupID"] = item.groupid;
            //    tblUserGroup.Rows.Add(row);
            //}
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
            DataTable tblroleContract = new SmartPortal.SEMS.Product().GetRoleOfProduct(model.productId);

            foreach (DataRow rCR in tblroleContract.Rows)
            {
                if (tblContractRoleDetail.Select("colContractNo='" + model.contractNo + "' and colRoleID='" + rCR["colRoleID"].ToString() + "'").Length == 0)
                {
                    DataRow newRowCR = tblContractRoleDetail.NewRow();
                    newRowCR["colContractNo"] = model.contractNo;
                    newRowCR["colRoleID"] = rCR["colRoleID"].ToString();

                    tblContractRoleDetail.Rows.Add(newRowCR);
                }
            }
            #endregion

            #region Tạo bảng chứa Account của Contract

            //DataTable tblContractList = (DataTable)ViewState["AccountList"];



            //truong hop contract duoc tao tu mot customer da co trong he thong.
            if (model.isNew)
            {
                model.cusID = new SmartPortal.SEMS.User().CreatePrimaryKey(string.Empty, string.Empty, "CUSTID", ref IPCERRORCODE, ref IPCERRORDESC); ;

            }
            else
            {
                DataSet dsUserInfo = new SmartPortal.SEMS.Customer().GetCustomerByCustcode(model.cusID, ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE == "0")
                {
                    DataTable dtCustInfo = dsUserInfo.Tables[0];
                    if (dtCustInfo.Rows.Count != 0)
                    {
                        model.cusCode = dtCustInfo.Rows[0]["CFCODE"].ToString().Trim();
                        model.cusType = dtCustInfo.Rows[0]["CFTYPE"].ToString().Trim();
                    }
                    else
                    {
                        throw new IPCException(IPCERRORCODE);
                    }
                }
                else
                {
                    throw new IPCException(IPCERRORCODE);
                }
            }

            DataSet ds = new SmartPortal.SEMS.Customer().GetAcctNo(model.cusCode, model.cusType, ref IPCERRORCODE, ref IPCERRORDESC);
            DataTable dtAccount = ds.Tables[0];

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


            //add cung cho giao dich lay account IPC
            foreach (DataRow rCA in tblSUM.Rows)
            {
                if (tblContractAccount.Select("colAContractNo='" + model.contractNo + "' and colAcctNo='" + rCA["colAccount"].ToString() + "'").Length == 0)
                {
                    DataRow newRowCA = tblContractAccount.NewRow();
                    newRowCA["colAContractNo"] = model.contractNo;
                    newRowCA["colAcctNo"] = rCA["colAccount"].ToString();

                    DataRow[] ro = dtAccount.Select("ACCOUNTNO='" + rCA["colAccount"].ToString().Trim() + "'");
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
                if (tblContractAccount.Select("colAContractNo='" + model.contractNo + "' and colAcctNo='" + WALLET + "'").Length == 0)
                {
                    DataRow newRowCA = tblContractAccount.NewRow();
                    newRowCA["colAContractNo"] = model.contractNo;
                    newRowCA["colAcctNo"] = WALLET;
                    newRowCA["colAcctType"] = "WL";
                    newRowCA["colCCYID"] = Resources.labels.lak;
                    newRowCA["colStatus"] = "A";

                    tblContractAccount.Rows.Add(newRowCA); ;
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
            //foreach (DataRow rTR in tblSUM.Rows)
            //{
            //    if (tblTranrightDetail.Select("colUserID='" + rTR["colIBUserName"].ToString() + "' and colAcctNoTR='" + rTR["colAccount"].ToString() + "' and colTranCode='" + rTR["colTranCodeID"].ToString() + "'").Length == 0)
            //    {
            //        if (rTR["colIBUserName"].ToString().Trim() != "")
            //        {
            //            DataRow newRowTR = tblTranrightDetail.NewRow();
            //            newRowTR["colUserID"] = rTR["colIBUserName"].ToString();
            //            newRowTR["colAcctNoTR"] = rTR["colAccount"].ToString();
            //            newRowTR["colTranCode"] = rTR["colTranCodeID"].ToString();
            //            newRowTR["colServiceID"] = rTR["colServiceID"].ToString();
            //            newRowTR["colLimit"] = "0";

            //            tblTranrightDetail.Rows.Add(newRowTR);
            //        }
            //    }
            //}
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
            foreach (DataRow rUA in tblAccountRole.Rows)
            {
                if (tblUserAccount.Select("colUserIDUC='" + rUA["coluserID"].ToString() + "' and colAcctNoUC='" + rUA["colAccount"].ToString() + "' and colRoleIDUC='" + rUA["colRoleID"].ToString() + "'").Length == 0)
                {
                    if (tblMBUser.Select("colMBUserID='" + rUA["coluserID"].ToString() + "'").Length != 0)
                    {
                        DataRow newRowUA = tblUserAccount.NewRow();
                        newRowUA["colUserIDUC"] = rUA["coluserID"].ToString();
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
            #region Tạo bảng chứa phòng ban mặc định
            DataTable tblDeptDefault = new DataTable();

            DataColumn colDeptID1 = new DataColumn("colDeptID1");
            DataColumn colDeptName = new DataColumn("colDeptName");
            DataColumn colDeptDesc = new DataColumn("colDeptDesc");
            DataColumn colDeptContractNo = new DataColumn("colDeptContractNo");

            tblDeptDefault.Columns.Add(colDeptID1);
            tblDeptDefault.Columns.Add(colDeptName);
            tblDeptDefault.Columns.Add(colDeptDesc);
            tblDeptDefault.Columns.Add(colDeptContractNo);

            DataRow rowDept = tblDeptDefault.NewRow();
            rowDept["colDeptID1"] = SmartPortal.Constant.IPC.DEPTIDPREFIX + DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - 8, 8);
            rowDept["colDeptName"] = Resources.labels.all;
            rowDept["colDeptDesc"] = Resources.labels.chuathongtintatcanguoidungdoanhnghiep;
            rowDept["colDeptContractNo"] = model.contractNo;

            tblDeptDefault.Rows.Add(rowDept);

            #endregion

            #region Tạo bảng tạo quyền mặc định cho hợp đồng
            DataTable tblRoleDefault = new DataTable();

            DataColumn colRoleName = new DataColumn("colRoleName");
            DataColumn colRoleDesc = new DataColumn("colRoleDesc");
            DataColumn colUserCreated = new DataColumn("colUserCreated");
            DataColumn colRoleContractNo = new DataColumn("colRoleContractNo");
            DataColumn colRoleServiceID = new DataColumn("colRoleServiceID");
            DataColumn colRoleProductID = new DataColumn("colRoleProductID");

            tblRoleDefault.Columns.Add(colRoleName);
            tblRoleDefault.Columns.Add(colRoleDesc);
            tblRoleDefault.Columns.Add(colUserCreated);
            tblRoleDefault.Columns.Add(colRoleContractNo);
            tblRoleDefault.Columns.Add(colRoleServiceID);
            tblRoleDefault.Columns.Add(colRoleProductID);
            #endregion
            #region vutt Tạo bảng chứa thông tin sms notify 04022016


            #region Tạo bảng chứa transaction alter of contract
            //tao bang chua thong tin customer
            DataTable tblTranAlter = new DataTable();
            DataColumn colContractNoAlter = new DataColumn("colContractNoAlter");
            DataColumn colTransactionAlert = new DataColumn("colTransactionAlert");

            //add vào table
            tblTranAlter.Columns.Add(colContractNoAlter);
            tblTranAlter.Columns.Add(colTransactionAlert);

            //tao 1 dong du lieu
            // if (cbLINE.Checked)
            // {
                // DataRow newRow1 = tblTranAlter.NewRow();
                // newRow1["colContractNoAlter"] = model.contractNo; 
                // newRow1["colTransactionAlert"] = "LINE";
                // tblTranAlter.Rows.Add(newRow1);
            // }
            // if (cbSMS.Checked)
            // {
                // DataRow newRow1 = tblTranAlter.NewRow();
                // newRow1["colContractNoAlter"] = model.contractNo; 
                // newRow1["colTransactionAlert"] = "SMS";
                // tblTranAlter.Rows.Add(newRow1);
            // }
            // if (cbTELE.Checked)
            // {
                // DataRow newRow1 = tblTranAlter.NewRow();
                // newRow1["colContractNoAlter"] = model.contractNo; 
                // newRow1["colTransactionAlert"] = "Telegram";
                // tblTranAlter.Rows.Add(newRow1);
            // }
            // if (cbWAPP.Checked)
            // {
                // DataRow newRow1 = tblTranAlter.NewRow();
                // newRow1["colContractNoAlter"] = model.contractNo;
                // newRow1["colTransactionAlert"] = "WhatsApp";
                // tblTranAlter.Rows.Add(newRow1);
            // }
            #endregion

            DataTable tblSMSNotify = ContractControl.CreateSMSNotifyDetailTable(tblUserAccount, tblContractAccount, "I", model.contractNo);
            #endregion
            #region INSERT
            if (model.isNew)
            {
                new SmartPortal.SEMS.Customer().InsertCorpMaTrix(model, tblUser, tblIbankUser,
                    tblSMSUser, tblMBUser, tblPHOUser, tblIbankUserRight, tblSMSUserRight, tblMBUserRight, tblPHOUserRight, tblUserGroup, tblContractRoleDetail,
                    tblContractAccount, tblTranrightDetail, tblUserAccount, tblDeptDefault, tblRoleDefault, new DataTable(), tblTranAlter, ref IPCERRORCODE, ref IPCERRORDESC);
            }
            else
            {
                new SmartPortal.SEMS.Contract().InsertCorpMaTrix(model, tblUser, tblIbankUser,
                      tblSMSUser, tblMBUser, tblPHOUser, tblIbankUserRight, tblSMSUserRight, tblMBUserRight, tblPHOUserRight, tblUserGroup, tblContractRoleDetail,
                      tblContractAccount, tblTranrightDetail, tblUserAccount, tblDeptDefault, tblRoleDefault, tblSMSNotify, tblTranAlter, ref IPCERRORCODE, ref IPCERRORDESC);

            }
            if (IPCERRORCODE != "0")
            {
                lblError.Text = "Insert contract unsuccessful";
                return;
            }
            else
            {
                SmartPortal.Common.Log.WriteLogDatabase(model.contractNo, "INSERT", Request.Url.ToString(), Session["userName"].ToString(),
               Request.UserHostAddress, "EBA_Contract", "Create corporate contract", "", "New", "N");
            }
            #endregion

        }
        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, IPCex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }

        SendInfoLogin();
        ViewState["MTRUSER"] = null;
        Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/Default.aspx?p=183"));

    }

    void SendInfoLogin()
    {
        Antlr3.ST.StringTemplate tmpl = new Antlr3.ST.StringTemplate();
        tmpl = SmartPortal.Common.ST.GetStringTemplate("SEMSContractApprove", "ContractAttachment" + System.Globalization.CultureInfo.CurrentCulture.ToString());

        //lay thong tin hop dong de gui mail

        try
        {
            string custID = "";
            string typeID = string.Empty;
            tmpl.Reset();

            //lay thong tin hop dong boi contractno
            DataTable contractTable = (new SmartPortal.SEMS.Contract().GetContractByContractNo(model.contractNo, ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
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

            #region advance

            if (ViewState["MTRUSER"] != null)
            {
                DataTable tblUser = (DataTable)ViewState["MTRUSER"];
                if (tblUser.Rows.Count != 0)
                {

                    #region lay thong tin tai khoan cua nguoi dong so huu
                    DataTable ctkTable = (new SmartPortal.SEMS.Contract().GetUserByContractNo(model.contractNo, "0603", "", ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
                    if (IPCERRORCODE != "0")
                    {
                        goto ERROR;
                    }

                    StringBuilder stctk = new StringBuilder();
                    stctk.Append("<div style='font-weight: bold; background-color: #C0C0C0;width:100%;height:16px;'>" + Resources.labels.thongtintaikhoan.ToUpper() + "</div>");
                    //gan thong tin user vao stringtemplate
                    int j = 0;
                    foreach (DataRow row in ctkTable.Rows)
                    {
                        typeID = row["TypeID"].ToString();
                        switch (row["TypeID"].ToString())
                        {
                            case "AD":
                                typeID = "Administrators"; break;
                            case "MK":
                                typeID = "Maker"; break;
                            case "CK":
                                typeID = "Checker"; break;

                        }
                        stctk.Append("<table style='width:100%;'>");
                        stctk.Append("<tr>");
                        stctk.Append("<td width='25%'>");
                        stctk.Append(Resources.labels.tendaydu + " ");
                        stctk.Append("</td>");
                        stctk.Append("<td width='25%'>");
                        stctk.Append(row["FULLNAME"].ToString());
                        stctk.Append("</td>");
                        stctk.Append("<td width='25%'>");
                        stctk.Append("Email ");
                        stctk.Append("</td>");
                        stctk.Append("<td width='25%'>");
                        stctk.Append(row["EMAIL"].ToString());
                        stctk.Append("</td>");
                        stctk.Append("</tr>");

                        stctk.Append("<tr>");
                        stctk.Append("<td>");
                        stctk.Append(Resources.labels.dienthoai + " ");
                        stctk.Append("</td>");
                        stctk.Append("<td>");
                        stctk.Append(row["PHONE"].ToString());
                        stctk.Append("</td>");
                        stctk.Append("<td>");
                        stctk.Append("Type ID" + " ");
                        stctk.Append("</td>");
                        stctk.Append("<td>");
                        stctk.Append(typeID);
                        stctk.Append("</td>");
                        stctk.Append("</tr>");


                        //lay het các tai khoan Ibank cua user theo userID
                        DataSet accountIBDatasetctk = new SmartPortal.SEMS.User().GetUserRoleByServiceID(SmartPortal.Constant.IPC.IB, row["USERID"].ToString().Trim(), string.Empty, ref IPCERRORCODE, ref IPCERRORDESC);

                        if (IPCERRORCODE != "0")
                        {
                            goto ERROR;
                        }

                        DataTable accountIBTablectk = accountIBDatasetctk.Tables[0];
                        if (accountIBTablectk.Rows.Count != 0)
                        {
                            if (accountIBTablectk.Rows[0]["ROLEID"].ToString().Trim() != "")
                            {
                                stctk.Append("<tr>");
                                stctk.Append("<td colspan='4'>");
                                stctk.Append("<br/>");
                                stctk.Append("</td>");
                                stctk.Append("</tr>");

                                stctk.Append("<tr>");
                                stctk.Append("<td colspan='4'>");
                                stctk.Append("<B>Internet Banking</B>");
                                stctk.Append("</td>");
                                stctk.Append("</tr>");

                                stctk.Append("<tr>");
                                stctk.Append("<td width='25%'>");
                                stctk.Append(Resources.labels.tendangnhap + " :");
                                stctk.Append("</td>");
                                stctk.Append("<td width='25%'>");
                                stctk.Append(accountIBTablectk.Rows[0]["USERNAME"].ToString());
                                stctk.Append("</td>");
                                stctk.Append("<td width='25%'>");
                                stctk.Append(Resources.labels.matkhau + " :");
                                stctk.Append("</td>");
                                stctk.Append("<td width='25%'>");
                                stctk.Append("########");
                                //st.Append("<b>" + SmartPortal.Security.Encryption.Decrypt(accountIBTable.Rows[0]["PASSWORD"].ToString()) + "</b>");
                                stctk.Append("</td>");
                                stctk.Append("</tr>");
                            }
                        }

                        //lay het các tai khoan SMS cua user theo userID
                        DataSet accountSMSDatasetctk = new SmartPortal.SEMS.User().GetUserRoleByServiceID(SmartPortal.Constant.IPC.SMS, row["USERID"].ToString().Trim(), string.Empty, ref IPCERRORCODE, ref IPCERRORDESC);

                        if (IPCERRORCODE != "0")
                        {
                            goto ERROR;
                        }

                        DataTable accountSMSTablectk = accountSMSDatasetctk.Tables[0];
                        if (accountSMSTablectk.Rows.Count != 0)
                        {
                            if (accountSMSTablectk.Rows[0]["ROLEID"].ToString().Trim() != "")
                            {
                                stctk.Append("<tr>");
                                stctk.Append("<td colspan='4'>");
                                stctk.Append("<br/>");
                                stctk.Append("</td>");
                                stctk.Append("</tr>");

                                stctk.Append("<tr>");
                                stctk.Append("<td colspan='4'>");
                                stctk.Append("<B>SMS Banking</B>");
                                stctk.Append("</td>");
                                stctk.Append("</tr>");

                                stctk.Append("<tr>");
                                stctk.Append("<td width='25%'>");
                                stctk.Append(Resources.labels.sodienthoai + " :");
                                stctk.Append("</td>");
                                stctk.Append("<td width='25%'>");
                                stctk.Append(accountSMSTablectk.Rows[0]["UN"].ToString());
                                stctk.Append("</td>");
                                stctk.Append("<td width='25%'>");
                                stctk.Append(Resources.labels.taikhoanmacdinh + " :");
                                stctk.Append("</td>");
                                stctk.Append("<td width='25%'>");
                                stctk.Append(accountSMSTablectk.Rows[0]["DEFAULTACCTNO"].ToString());
                                stctk.Append("</td>");
                                stctk.Append("</tr>");
                            }
                        }

                        //lay het các tai khoan MB cua user theo userID
                        DataSet accountMBDatasetctk = new SmartPortal.SEMS.User().GetUserSendInfor(SmartPortal.Constant.IPC.MB, row["USERID"].ToString().Trim(), ref IPCERRORCODE, ref IPCERRORDESC);

                        if (IPCERRORCODE != "0")
                        {
                            goto ERROR;
                        }

                        DataTable accountMBTablectk = accountMBDatasetctk.Tables[0];
                        if (accountMBTablectk.Rows.Count != 0)
                        {
                            //stctk.Append("<tr>");
                            //stctk.Append("<td colspan='4'>");
                            //stctk.Append("<br/>");
                            //stctk.Append("</td>");
                            //stctk.Append("</tr>");

                            //stctk.Append("<tr>");
                            //stctk.Append("<td colspan='4'>");
                            //stctk.Append("<B>Mobile Banking</B>");
                            //stctk.Append("</td>");
                            //stctk.Append("</tr>");

                            //stctk.Append("<tr>");
                            //stctk.Append("<td width='25%'>");
                            //stctk.Append(Resources.labels.tendangnhap + " :");
                            //stctk.Append("</td>");
                            //stctk.Append("<td width='25%'>");
                            //stctk.Append(accountMBTablectk.Rows[0]["USERNAME"].ToString());
                            //stctk.Append("</td>");
                            //stctk.Append("<td width='25%'>");
                            //stctk.Append(Resources.labels.matkhau + " :");
                            //stctk.Append("</td>");
                            //stctk.Append("<td width='25%'>");
                            //stctk.Append("########");
                            ////st.Append("<b>" + SmartPortal.Security.Encryption.Decrypt(accountMBTable.Rows[0]["PASS"].ToString()) + "</b>");
                            //stctk.Append("</td>");
                            //stctk.Append("</tr>");

                        }
                        stctk.Append("</table>");
                        j += 1;
                        if (j < ctkTable.Rows.Count)
                        {
                            stctk.Append("<hr/>");
                        }
                    }
                    tmpl.SetAttribute("USERINFO", stctk.ToString());

                    #endregion

                }
            }

            #endregion

            //luu thong tin vao session de hien thi cho nguoi dung
            Session["tmpl"] = tmpl.ToString();
            //luu thong tin ban cung hop dong
            try
            {
                new SmartPortal.SEMS.Contract().SaveContractReview(model.contractNo, tmpl.ToString(), Server.MapPath("~/widgets/semscontractlist/contractfile/" + model.contractNo + ".html"));
            }
            catch
            {
            }

            goto EXIT;
        }

        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], "Widgets_SEMSContractApprove_Widget", "SendInfoLogin", IPCex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSContractApprove_Widget", "SendInfoLogin", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    ERROR:
        SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], "Widgets_SEMSContractApprove_Widget", "SendInfoLogin", IPCERRORDESC, Request.Url.Query);
        SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);
    EXIT:

        ;
    }

    protected void OntextChangeMobi_Click(object sender, EventArgs e)
    {
        if (!CheckExistPhoneNumber(txtMobi.Text.Trim()))
        {
            lblError.Text = Resources.labels.phonenumberisalreadyregistered;
        }
        else if (!CheckIsPhoneNumer(txtMobi.Text.Trim()))
        {
            lblError.Text = Resources.labels.phonenumberwrong;
        }
        else
        {
            lblError.Text = string.Empty;
        }
    }
    protected void ddlUserType_TextChanged(object sender, EventArgs e)
    {
        LoadDataInTreeview(SmartPortal.Constant.IPC.IB, tvIBRole, ddlUserType.SelectedValue, SmartPortal.Constant.IPC.NORMAL);
        LoadDataInTreeview(SmartPortal.Constant.IPC.MB, tvMB, ddlUserType.SelectedValue, SmartPortal.Constant.IPC.NORMAL);
        LoadDataALLRoleInTreeview(string.Empty, tvRole, ddlUserType.SelectedValue, SmartPortal.Constant.IPC.NORMAL);
        GetRoleDefault(tvIBRole, new TreeView(), tvMB, new TreeView());
        if (!ddlUserType.SelectedValue.Equals(USERTYPE))
        {
            DataTable tblAccountRole = ViewState["ACCOUNTROLE"] != null ? (DataTable)ViewState["ACCOUNTROLE"] : CreateTableAccontRole();
            RemoveAccountRoleUserID(USERID, tblAccountRole);
            CheckAllRole();
        }
        USERTYPE = ddlUserType.SelectedValue;
    }
    protected void btnChangePhone_Click(object sender, EventArgs e)
    {
        try
        {
            string phone = string.Empty;
            phone = txtMTPhone.Text.Trim();
            // if (!CheckIsPhoneNumer(phone))
            // {
                // lblError.Text = Resources.labels.phonenumberwrong;
                // return;
            // }
            // else
            // {
                 txtPhoneMB.Text = phone;
            // }
            CheckPhoneInCust(phone, USERID);
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
    public bool CheckExistPhoneNumber(string phone)
    {
        string resultInfo = new Customer().CheckPhoneNumberCustInfo(phone, SmartPortal.Constant.IPC.CORPORATECONTRACT, ref IPCERRORCODE, ref IPCERRORDESC);
        if (resultInfo.Equals("1"))
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    public bool CheckIsPhoneNumer(string phone)
    {
        if (cbIsForeign.Checked)
        {
            return true;
        }
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
    protected void lstAccount_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (lstAccount.SelectedValue.Trim().Equals(WALLET))
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
            LoadAccountRole(USERID, lstAccount.SelectedValue.ToString().Trim());
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSSetLimitTeller_Widget", "lstAccount_SelectedIndexChanged", ex.Message, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

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
            DataTable tblRole = ViewState["TBLROLE"] != null ? (DataTable)ViewState["TBLROLE"] : CreateTableRole();
            //add row moi
            string role = string.Empty;
            foreach (TreeNode node in tvRole.Nodes)
            {
                role = node.Value.Trim();
                if (node.Checked)
                {
                    if (lstAccount.SelectedValue.Trim().Equals(WALLET))
                    {
                        AddAccountRole(USERID, lstAccount.SelectedValue.Trim(), role, SmartPortal.Constant.IPC.WAL, CONTRACTNO);
                    }
                    else
                    {
                        foreach (DataRow dr in tblRole.Rows)
                        {
                            if (dr["colRoleID"].ToString().Equals(role))
                            {
                                AddAccountRole(USERID, lstAccount.SelectedValue.Trim(), role, dr["colServiceID"].ToString(), CONTRACTNO);
                            }
                        }

                    }
                }
                else
                {
                    RemoveAccountRole(USERID, lstAccount.SelectedValue.Trim(), role, string.Empty, CONTRACTNO);
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
    private void RemoveAccountRole(string userID, string account, string roleID, string serviceID, string contractNo)
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
    public void IsWallet_OnCheckedChaned(object sender, EventArgs e)
    {

        string phone = string.Empty;
        DataTable dtAccountQT = (DataTable)ViewState["ACCOUNTCORE"];
        if (isWallet.Checked)
        {
            model.walletID = WALLET;
            if (CheckIsPhoneNumer(txtMobi.Text.Trim()))
            {
                phone = txtMobi.Text.Trim();
            }
            else
            {
                lblError.Text = Resources.labels.phonenumberwrong;
                return;
            }
            lstAccount.DataSource = dtAccountQT;
            lstAccount.DataTextField = "ACCOUNTNO";
            lstAccount.DataValueField = "ACCOUNTNO";
            lstAccount.DataBind();
            lstAccount.SelectedValue = dtAccountQT.Rows[0]["ACCOUNTNO"].ToString();
            lstAccount.Items.Insert(0, new ListItem(phone, WALLET));

            ddlDefaultAccountQT.DataSource = dtAccountQT;
            ddlDefaultAccountQT.DataTextField = "ACCOUNTNO";
            ddlDefaultAccountQT.DataValueField = "ACCOUNTNO";
            ddlDefaultAccountQT.DataBind();
            ddlDefaultAccountQT.SelectedValue = dtAccountQT.Rows[0]["ACCOUNTNO"].ToString();
            ddlDefaultAccountQT.Items.Insert(0, new ListItem(phone, WALLET));
        }
        else
        {
            lstAccount.DataSource = dtAccountQT;
            lstAccount.DataTextField = "ACCOUNTNO";
            lstAccount.DataValueField = "ACCOUNTNO";
            lstAccount.DataBind();
            lstAccount.SelectedValue = dtAccountQT.Rows[0]["ACCOUNTNO"].ToString();

            ddlDefaultAccountQT.DataSource = dtAccountQT;
            ddlDefaultAccountQT.DataTextField = "ACCOUNTNO";
            ddlDefaultAccountQT.DataValueField = "ACCOUNTNO";
            ddlDefaultAccountQT.DataBind();
            ddlDefaultAccountQT.SelectedValue = dtAccountQT.Rows[0]["ACCOUNTNO"].ToString();


        }

    }
    public void LoadViewDetail()
    {

        CreateUserName_Click(null, null);
        isWallet.Checked = false;
        foreach (TreeNode node in tvRole.Nodes)
        {
            node.Checked = false;
        }
        DataTable dtAccountQT = (DataTable)ViewState["ACCOUNTCORE"];
        lstAccount.DataSource = dtAccountQT;
        lstAccount.DataTextField = "ACCOUNTNO";
        lstAccount.DataValueField = "ACCOUNTNO";
        lstAccount.DataBind();
        lstAccount.SelectedValue = dtAccountQT.Rows[0]["ACCOUNTNO"].ToString();
        DisableOrEnableUser(true);
    }
    private void DisableOrEnableUser(bool isbool)
    {
        PnUserInfor.Enabled = isbool;
        pnAccountInfo.Enabled = isbool;
        PnIB.Enabled = isbool;
        pnMobile.Enabled = isbool;
        pnSearchCust.Enabled = isbool;
        btnAddUserMT.Enabled = isbool;
    }
    public void btndelete_Click(object sender, EventArgs e)
    {
        DeleteAllViewState();
        DisableOrEnableUser(true);
        ddlUser.DataBind();
        btnAddNewUser.Visible = false;
        lblError.Text = "Delete rows successfully";
    }
    private void DeleteAllViewState()
    {
        USERTYPE = string.Empty;
        ViewState["ACCOUNTROLE"] = new DataTable();
        ViewState["ACCOUNTROLE"] = CreateTableAccontRole();
        ViewState["MTRUSER"] = new DataTable();
        ViewState["MTRUSER"] = ContructorTableUser();
        ViewState["DataCurrent"] = new DataTable();
        ViewState["DataCurrent"] = ContructorTableUser();
        rdAdd.Checked = true;
        rdAdd.Enabled = true;
        txtMTCuscode.Enabled = true;
        DivExistUser.Visible = false;
    }
    public bool CheckPhoneInCust(string phone, string userid)
    {
        if (ViewState["MTRUSER"] != null)
        {
            DataTable tblSUM = (DataTable)ViewState["MTRUSER"];
            DataTable dtRole = tblSUM.Select("colMBPhone = '" + phone + "' and colUserID <>'" + userid + "'").Any() ? tblSUM.Select("colMBPhone = '" + phone + "' and colUserID <>'" + userid + "'").CopyToDataTable() : null;
            if (dtRole != null)
            {
                lblError.Text = Resources.labels.phonenumberisalreadyregistered;
                return false;
            }
            else
            {
                lblError.Text = string.Empty;
                return true;
            }
        }
        else
        {
            lblError.Text = string.Empty;
            return true;
        }

    }

    public bool CheckUserNameInCust(string userName, string userid)
    {
        if (ViewState["MTRUSER"] != null)
        {
            DataTable tblSUM = (DataTable)ViewState["MTRUSER"];
            DataTable dtRole = tblSUM.Select("colIBUserName = '" + userName + "' and colUserID <>'" + userid + "'").Any() ? tblSUM.Select("colIBUserName = '" + userName + "' and colUserID <>'" + userid + "'").CopyToDataTable() : null;
            if (dtRole != null)
            {
                lblError.Text = Resources.labels.usernamehasexists;
                return false;
            }
            else
            {
                lblError.Text = string.Empty;
                return true;
            }
        }
        else
        {
            lblError.Text = string.Empty;
            return true;
        }

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
                AddAccountRole(USERID, drAct["ACCOUNTNO"].ToString(), dr["colRoleID"].ToString(), dr["colServiceID"].ToString(), CONTRACTNO);
            }

        }
        LoadAccountRole(USERID, lstAccount.SelectedValue.ToString().Trim());
    }
    private bool ValidateNext()
    {
        try
        {
            if (!CheckExistPhoneNumber(txtMobi.Text.Trim()))
            {
                lblError.Text = Resources.labels.phonenumberisalreadyregistered;
                txtMobi.Enabled = true;
                return false;
            }
            else if (!CheckIsPhoneNumer(txtMobi.Text.Trim()))
            {
                lblError.Text = Resources.labels.phonenumberwrong;
                txtMobi.Enabled = true;
                return false;
            }
            if (string.IsNullOrEmpty(txtBirth.Text.Trim()) || string.IsNullOrEmpty(txtFullName.Text.Trim()))
            {
                lblError.Text = Resources.labels.customerinformationreturnisincorrect;
                return false;
            }
            return true;
        }
        catch
        {
            return false;
        }
    }
}