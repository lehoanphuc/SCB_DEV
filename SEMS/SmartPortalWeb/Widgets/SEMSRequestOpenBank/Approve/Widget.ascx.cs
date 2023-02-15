using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;
using SmartPortal.Constant;
using SmartPortal.Common.Utilities;
using SmartPortal.Security;
using SmartPortal.SEMS;
using System.Collections;
using System.Text;
using SmartPortal.ExceptionCollection;

public partial class Widgets_SEMSREGIONFEE_Controls_Widget : WidgetBase
{
    public static bool isAscend = false;
    string ACTION = "";
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    string FORMAT_DATE = "dd/MM/yyyy";
    string FORMAT_IMAGE = "data:image/jpg;base64,";
    String PaperTypeDefault = String.Empty;
    SmartPortal.SEMS.Common _service = new SmartPortal.SEMS.Common();
    private static bool pendingforapprove = false;
    public string CONTRACTNO
    {
        get { return ViewState["CONTRACTNO"] != null ? (string)ViewState["CONTRACTNO"] : string.Empty; }
        set { ViewState["CONTRACTNO"] = value; }
    }
    public string CUSTID
    {
        get { return ViewState["CUSTID"] != null ? (string)ViewState["CUSTID"] : string.Empty; }
        set { ViewState["CUSTID"] = value; }
    }
    public string DPTTYPE
    {
        get { return ViewState["DPTTYPE"] != null ? (string)ViewState["DPTTYPE"] : string.Empty; }
        set { ViewState["DPTTYPE"] = value; }
    }
    public string CATCODE
    {
        get { return ViewState["CATCODE"] != null ? (string)ViewState["CATCODE"] : string.Empty; }
        set { ViewState["CATCODE"] = value; }
    }
    public string CUSTCODE
    {
        get { return ViewState["CUSTCODE"] != null ? (string)ViewState["CUSTCODE"] : string.Empty; }
        set { ViewState["CUSTCODE"] = value; }
    }
    public string CUSTTYPE
    {
        get { return ViewState["CUSTTYPE"] != null ? (string)ViewState["CUSTTYPE"] : string.Empty; }
        set { ViewState["CUSTTYPE"] = value; }
    }
    public string ACCTYPE
    {
        get { return ViewState["ACCTYPE"] != null ? (string)ViewState["ACCTYPE"] : string.Empty; }
        set { ViewState["ACCTYPE"] = value; }
    }
    public string CONTENT
    {
        get { return ViewState["CONTENT"] != null ? (string)ViewState["CONTENT"] : string.Empty; }
        set { ViewState["CONTENT"] = value; }
    }
    public string USERID
    {
        get { return ViewState["USERID"] != null ? (string)ViewState["USERID"] : string.Empty; }
        set { ViewState["USERID"] = value; }
    }
    public string EMAIL
    {
        get { return ViewState["EMAIL"] != null ? (string)ViewState["EMAIL"] : string.Empty; }
        set { ViewState["EMAIL"] = value; }
    }
    public string CCYID
    {
        get { return ViewState["CCYID"] != null ? (string)ViewState["CCYID"] : string.Empty; }
        set { ViewState["CCYID"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ACTION = GetActionPage();
            ViewState["REQUESTNO"] = GetParamsPage(IPC.ID)[0].Trim();
            if (!IsPostBack)
            {
                pendingforapprove = true;
                setDefauleControl();
                loadCombobox();
                BindData();

            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    public void setDefauleControl()
    {
        txtPhoneNumber.Enabled = false;
        txtFullName.Enabled = false;
        //ddlPaperType.Enabled = false;
        txtPaperNumber.Enabled = false;
        ddlWalletLevel.Enabled = false;
        ddlStatus.Enabled = false;
        txtIssueDate.Attributes.Add("placeholder", FORMAT_DATE);
        txtExpireDate.Attributes.Add("autocomplete", "off");
        txtIssueDate.Attributes.Add("placeholder", FORMAT_DATE);
        txtExpireDate.Attributes.Add("autocomplete", "off");
        txtBirthday.Attributes.Add("placeholder", FORMAT_DATE);
        txtBirthday.Attributes.Add("autocomplete", "off");
        txtIssueDate.Text = DateTime.Now.ToString(FORMAT_DATE);
        txtExpireDate.Text = DateTime.Now.ToString(FORMAT_DATE);
        txtBirthday.Text = DateTime.Now.ToString(FORMAT_DATE);
        switch (ACTION)
        {
            case IPC.ACTIONPAGE.DETAILS:
                lblTitleBranch.Text = "Request open bank account view ";
                btnApprove.Visible = false;
                btReject.Visible = false;
                break;
            case IPC.ACTIONPAGE.APPROVE:
                lblTitleBranch.Text = "Approve open bank account";
                break;
            case IPC.ACTIONPAGE.REJECT:
                lblTitleBranch.Text = "Reject open bank account";
                break;
        }
    }
    private void loadCombobox_Gender()
    {
        DataSet ds = new DataSet();
        ds = _service.GetValueList("GENDER", "SYS", ref IPCERRORCODE, ref IPCERRORDESC);
        if (IPCERRORCODE == "0")
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlGender.DataSource = ds;
                ddlGender.DataValueField = "VALUEID";
                ddlGender.DataTextField = "CAPTION";
                ddlGender.DataBind();
            }
        }
    }
    private void loadCombobox_KYCLevel()
    {
        // Save list KYCLevel Cache
        try
        {
            DataSet ds = new DataSet();
            ds = (DataSet)Cache["Wallet_KYCLevel"];
            if (ds == null)
            {
                object[] searchObject = new object[] { string.Empty };
                ds = _service.common("SEMS_BO_GET_INFO_KYC", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE == "0")
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlKycLevel.DataSource = ds;
                        ddlKycLevel.DataValueField = "KycID";
                        ddlKycLevel.DataTextField = "KycName";
                        ddlKycLevel.DataBind();

                        ddlPaperType.DataSource = ds;
                        ddlPaperType.DataValueField = "KycCode";
                        ddlPaperType.DataTextField = "KycName";
                        ddlPaperType.DataBind();
                    }
                }
                Cache.Insert("Wallet_KYCLevel", ds, null, DateTime.MaxValue, TimeSpan.FromMinutes(10));
            }
            else
            {
                ddlKycLevel.DataSource = (DataSet)Cache["Wallet_KYCLevel"];
                ddlKycLevel.DataValueField = "KycID";
                ddlKycLevel.DataTextField = "KycName";
                ddlKycLevel.DataBind();

                ddlPaperType.DataSource = (DataSet)Cache["Wallet_KYCLevel"];
                ddlPaperType.DataValueField = "KycCode";
                ddlPaperType.DataTextField = "KycName";
                ddlPaperType.DataBind();
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void OnTextChanged_KYCLevel(object sender, EventArgs e)
    {
        ddlPaperType.SelectedItem.Text = ddlKycLevel.SelectedItem.Text;
    }

    private void loadCombobox_WalletLevel()
    {
        // Save list WalletLevel Cache
        try
        {
            DataSet ds = new DataSet();
            ds = (DataSet)Cache["Wallet_WalletLevel"];
            if (ds == null)
            {
                object[] _object = new object[] { string.Empty };
                ds = _service.common("SEMS_BO_LST_CON_LV", _object, ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE == "0")
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlWalletLevel.DataSource = ds;
                        ddlWalletLevel.DataValueField = "ContractLevelID";
                        ddlWalletLevel.DataTextField = "ContractLevelName";
                        ddlWalletLevel.DataBind();
                    }
                }
                Cache.Insert("Wallet_WalletLevel", ds, null, DateTime.MaxValue, TimeSpan.FromMinutes(10));
            }
            else
            {
                ddlWalletLevel.DataSource = (DataSet)Cache["Wallet_WalletLevel"];
                ddlWalletLevel.DataValueField = "ContractLevelID";
                ddlWalletLevel.DataTextField = "ContractLevelName";
                ddlWalletLevel.DataBind();
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
                        ddlNationality.DataSource = ds;
                        ddlNationality.DataValueField = "NationCode";
                        ddlNationality.DataTextField = "NationName";
                        ddlNationality.DataBind();
                    }
                }
                Cache.Insert("Wallet_Nation", ds, null, DateTime.MaxValue, TimeSpan.FromMinutes(10));
            }
            else
            {
                ddlNationality.DataSource = (DataSet)Cache["Wallet_Nation"];
                ddlNationality.DataValueField = "NationCode";
                ddlNationality.DataTextField = "NationName";
                ddlNationality.DataBind();
            }
        }
        catch (Exception ex)
        {

        }
    }
    private void loadCombobox_PaperType()
    {
        // Save list PaperType Cache
        try
        {
            //DataSet ds = new DataSet();
            //ds = (DataSet)Cache["Wallet_PaperType"];
            //if (ds == null)
            //{
            //    ds = _service.GetValueList("EBA_CustInfo", "PAP", ref IPCERRORCODE, ref IPCERRORDESC);
            //    if (IPCERRORCODE == "0")
            //    {
            //        if (ds.Tables[0].Rows.Count > 0)
            //        {
            //            ddlPaperType.DataSource = ds;
            //            ddlPaperType.DataValueField = "ValueID";
            //            ddlPaperType.DataTextField = "Caption";
            //            ddlPaperType.DataBind();
            //        }
            //    }
            //    Cache.Insert("Wallet_PaperType", ds, null, DateTime.MaxValue, TimeSpan.FromMinutes(10));
            //}
            //else
            //{
            //    ddlPaperType.DataSource = (DataSet)Cache["Wallet_PaperType"];
            //    ddlPaperType.DataValueField = "ValueID";
            //    ddlPaperType.DataTextField = "Caption";
            //    ddlPaperType.DataBind();
            //}
            //for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            //    if (ds.Tables[0].Rows[i]["CAPTION"].ToString().Equals("NRC")) PaperTypeDefault = ds.Tables[0].Rows[i]["VALUEID"].ToString();
        }
        catch (Exception ex)
        {

        }
    }
    private void loadCombobox_Status()
    {
        // Save list STT KYC Request Cache
        try
        {
            DataSet ds = new DataSet();
            ds = (DataSet)Cache["Wallet_STT_KYCRequest"];
            if (ds == null)
            {
                ds = _service.GetValueList("WAL_KYC_REQUEST", "STT", ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE == "0")
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        //ddlStatus.DataSource = ds;
                        //ddlStatus.DataValueField = "ValueID";
                        //ddlStatus.DataTextField = "Caption";
                        //ddlStatus.DataBind();
                    }
                }
                Cache.Insert("Wallet_STT_KYCRequest", ds, null, DateTime.MaxValue, TimeSpan.FromMinutes(10));
            }
            else
            {
                //ddlStatus.DataSource = (DataSet)Cache["Wallet_STT_KYCRequest"];
                //ddlStatus.DataValueField = "ValueID";
                //ddlStatus.DataTextField = "Caption";
                //ddlStatus.DataBind();
            }
            ddlStatus.Items.Add(new ListItem(Resources.labels.connew, SmartPortal.Constant.IPC.NEW));
            ddlStatus.Items.Add(new ListItem(Resources.labels.conactive, SmartPortal.Constant.IPC.ACTIVE));
            ddlStatus.Items.Add(new ListItem(Resources.labels.conreject, SmartPortal.Constant.IPC.REJECT));
        }
        catch (Exception ex)
        {

        }
    }
    private void loadCombobox()
    {
        loadCombobox_KYCLevel();
        loadCombobox_WalletLevel();
        loadCombobox_Nation();
        loadCombobox_PaperType();
        loadCombobox_Status();
        loadCombobox_Gender();

    }
    void BindData()
    {
        try
        {

            switch (ACTION)
            {
                case IPC.ACTIONPAGE.ADD:
                    break;
                default:
                    if (ACTION.Equals(IPC.ACTIONPAGE.REJECT))
                        btnApprove.Visible = false;
                    if (ACTION.Equals(IPC.ACTIONPAGE.APPROVE))
                        btReject.Visible = false;
                    string ID = GetParamsPage(IPC.ID)[0].Trim();
                    DataSet ds = new DataSet();
                    object[] searchObject = new object[] { Utility.KillSqlInjection(ID) };
                    ds = _service.common("LOADOPENBANKRQ", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);
                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0)
                        {
                            if (ds.Tables[0].Rows.Count < 1) return;
                            DataTable dataTable = ds.Tables[0];
                            USERID = dataTable.Rows[0]["UserID"].ToString();
                            CUSTID = dataTable.Rows[0]["CUSTID"].ToString();
                            txtPhoneNumber.Text = dataTable.Rows[0]["PHONE"].ToString();
                            txtFullName.Text = dataTable.Rows[0]["FULLNAME"].ToString();
                            ddlPaperType.SelectedValue = dataTable.Rows[0]["KYC_CODE"].ToString();
                            txtPaperNumber.Text = dataTable.Rows[0]["PaperNO"].ToString();
                            txtIssueDate.Text = dataTable.Rows[0]["ISSUEDATE"].ToString();
                            txtExpireDate.Text = dataTable.Rows[0]["ExpiryDate"].ToString();
                            txtBirthday.Text = dataTable.Rows[0]["DOB"].ToString();
                            CONTRACTNO = txtContractNo.Text = dataTable.Rows[0]["CONTRACTNO"].ToString();
                            txtCustomerType.Text = dataTable.Rows[0]["CUSTYPE"].ToString();
                            CCYID= txtccyid.Text = dataTable.Rows[0]["CCYID"].ToString();
                            CONTENT=txtDesc.Text = dataTable.Rows[0]["CONTENT"].ToString();
                            txtCityName.Text = dataTable.Rows[0]["CITYNAME"].ToString();
                            txtAccountType.Text = dataTable.Rows[0]["ACCOUNTTYPE"].ToString();
                            ACCTYPE = dataTable.Rows[0]["ACCTYPE"].ToString();
                            CUSTCODE = dataTable.Rows[0]["CUSTCODE"].ToString();
                            CATCODE = dataTable.Rows[0]["CATCODE"].ToString();
                            DPTTYPE = dataTable.Rows[0]["DPTTYPE"].ToString();
                            txtbranchName.Text = dataTable.Rows[0]["BranchID"].ToString();
                            string sex = dataTable.Rows[0]["SEX"].ToString();
                            EMAIL = dataTable.Rows[0]["EMAIL"].ToString();
                            if (sex.Trim() != string.Empty)
                            {
                                ddlGender.SelectedValue = sex;
                            }
                            else
                            {
                                ddlGender.Items.Insert(0, new ListItem(string.Empty, string.Empty));
                                ddlGender.SelectedValue = string.Empty;
                            }

                            txtAddress.Text = dataTable.Rows[0]["ADDRRESIDENT"].ToString();
                            ddlWalletLevel.SelectedValue = dataTable.Rows[0]["WALLET_LEVEL"].ToString().Trim();
                            ddlStatus.SelectedValue = dataTable.Rows[0]["STSREQUEST"].ToString().Trim();
                            if (!dataTable.Rows[0]["STSREQUEST"].ToString().Trim().Equals(SmartPortal.Constant.IPC.NEW))
                            {
                                btnApprove.Visible = false;
                                btReject.Visible = false;
                            }
                            string nation = dataTable.Rows[0]["NATION"].ToString().Trim();
                            if (nation.Trim() != string.Empty)
                            {
                                ddlNationality.SelectedValue = nation;
                            }
                            else
                            {
                                ddlNationality.Items.Insert(0, new ListItem(string.Empty, string.Empty));
                                ddlNationality.SelectedValue = string.Empty;
                            }

                            ddlKycLevel.SelectedValue = dataTable.Rows[0]["KYC_LEVEL"].ToString();
                            txtcreatedate.Text = dataTable.Rows[0]["DateCreated"].ToString();
                            txtCreateby.Text = dataTable.Rows[0]["UserCreated"].ToString();
                            ViewState["CustID"] = dataTable.Rows[0]["CustID"].ToString();
                            ViewState["REQUESTNO"] = ID;
                            //if (!dataTable.Rows[0]["APPROVE_STATUS"].ToString().Equals("N"))
                            //{
                            //    btReject.Enabled = false;
                            //    btnApprove.Enabled = false;
                            //    pnRegion.Enabled = false;
                            //}
                        }
                        if (ds.Tables.Count > 0)
                        {
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                ds.Tables[0].Rows[i]["File"] = FORMAT_IMAGE + ds.Tables[0].Rows[i]["File"].ToString();
                            }
                            rptData.DataSource = ds.Tables[0];
                            rptData.DataBind();

                        }
                    }
                    break;

            }

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    private bool CheckValidate()
    {
        txtAddress.BorderColor = System.Drawing.Color.Gainsboro;
        if (string.IsNullOrEmpty(txtAddress.Text.Trim()))
        {
            lblError.Text = Resources.labels.accountnumber + ' ' + Resources.labels.notallowedtonull;
            txtAddress.BorderColor = System.Drawing.Color.Red;
            return false;
        }

        return true;
    }


    protected void btBack_Click(object sender, EventArgs e)
    {
        RedirectBackToMainPage();
    }

    protected void btReject_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet hasCust = new SmartPortal.SEMS.Contract().RejectOpenBank(USERID, SmartPortal.Constant.IPC.REJECT, Session["userName"].ToString().Trim(), ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE.Equals("0"))
            {
                lblError.Text = "Reject Open bank Successfully";
                btnApprove.Visible = false;
                BindData();
            }
            else
            {
                lblError.Text = "Reject Open bank Fail";
            }
        }
        catch
        {

        }

    }
    private void setDefaultColor()
    {
        txtExpireDate.BorderColor = System.Drawing.Color.Empty;
        txtIssueDate.BorderColor = System.Drawing.Color.Empty;
    }
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        try
        {
            string accountNo = string.Empty;
            if(string.IsNullOrEmpty(CUSTCODE))
            {
                lblError.Text = "Approve Open bank Fail";
            }
            Hashtable hasCust = new SmartPortal.SEMS.Contract().OpenAccountBank(GetParamsPage(IPC.ID)[0].Trim(),CONTRACTNO,USERID, CUSTCODE, DPTTYPE, CATCODE, CONTENT, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE.Equals("0"))
            {
                lblError.Text = "Approve Open bank Successfully";
                btnApprove.Visible = false;

 
                if (IPCERRORCODE.Equals("0"))
                {
                   // SendInfoLogin(CONTRACTNO);
                    BindData();
                }

            }
            else
            {
                lblError.Text = "Approve Open bank Fail";
            }
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }



    protected void rptData_ItemCommand1(object source, RepeaterCommandEventArgs e)
    {
        string commandName = e.CommandName;
        string commandArg = e.CommandArgument.ToString();

        switch (commandName)
        {
            case IPC.ACTIONPAGE.REVIEW:
                PreviewImage.ViewImage(commandArg);
                break;
        }
    }
    public void sendMail(string username, string cif, string email)
    {
        try
        {
            Antlr3.ST.StringTemplate tmpl = new Antlr3.ST.StringTemplate();
            tmpl = SmartPortal.Common.ST.GetStringTemplate("SEMSRequestOpenBank", "ContractApprove" + System.Globalization.CultureInfo.CurrentCulture.ToString());
            tmpl.Reset();
            StringBuilder st = new StringBuilder();
            tmpl.SetAttribute("username", username);
            tmpl.SetAttribute("CIF", cif);
            SmartPortal.Common.EmailHelper.SendMailMessageAsync(System.Configuration.ConfigurationManager.AppSettings["contractapprovemailfrom"], email, System.Configuration.ConfigurationManager.AppSettings["contractapprovemailtitle"], tmpl.ToString());
        }
        catch (Exception ex)
        {

        }
    }

    private void SendInfoLogin(string contractNo)
    {
        try
        {
            string hpcontractNo = contractNo;
            string custID = "";
            string contracttype = "";
            string UserNameMB = string.Empty;
            string authenType = string.Empty;
            string loginMethod = string.Empty;
            //lay thong tin hop dong boi contractno
            DataTable contractTable = (new SmartPortal.SEMS.Contract().GetContractByContractNo(hpcontractNo, ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
            if (IPCERRORCODE != "0")
            {
                throw new IPCException(IPCERRORDESC);
            }
            if (contractTable.Rows.Count != 0)
            {
                contracttype = contractTable.Rows[0]["contracttype"].ToString().Trim();

                if (contracttype == SmartPortal.Constant.IPC.CONTRACTCORPMATRIX)
                {
                    //SendInfoLoginCorpMatrix(contractNo, contractTable);
                    return;
                }
            }
            SmartPortal.Common.Log.WriteLogFile("", "", "", "==start send info");
            Antlr3.ST.StringTemplate tmplAttachment = new Antlr3.ST.StringTemplate();
            tmplAttachment = SmartPortal.Common.ST.GetStringTemplate("SEMSContractApprove", "ContractAttachment" + System.Globalization.CultureInfo.CurrentCulture.ToString());

            Antlr3.ST.StringTemplate tmplBody = new Antlr3.ST.StringTemplate();
            tmplBody = SmartPortal.Common.ST.GetStringTemplate("SEMSContractApprove", "ContractApprove" + System.Globalization.CultureInfo.CurrentCulture.ToString());


            //lay thong tin hop dong de gui mail

            tmplAttachment.Reset();
            tmplBody.Reset();
            //gan thong tin hop dong vao stringtemplate
            if (contractTable.Rows.Count != 0)
            {
                tmplAttachment.SetAttribute("CONTRACTNO", contractTable.Rows[0]["CONTRACTNO"].ToString());
                tmplBody.SetAttribute("CONTRACTNO", contractTable.Rows[0]["CONTRACTNO"].ToString());

                switch (contractTable.Rows[0]["USERTYPE"].ToString())
                {
                    case "0101":
                    case "0102":
                    case "0103":
                    case "0104":
                        tmplAttachment.SetAttribute("CONTRACTTYPE", Resources.labels.Agent); break;
                    case "0201":
                    case "0202":
                    case "0203":
                    case "0204":
                        tmplAttachment.SetAttribute("CONTRACTTYPE", Resources.labels.Consumer); break;
                    case "0301":
                        tmplAttachment.SetAttribute("CONTRACTTYPE", Resources.labels.Merchant); break;
                }

                tmplAttachment.SetAttribute("OPENDATE", SmartPortal.Common.Utilities.Utility.IsDateTime2(contractTable.Rows[0]["CREATEDATE"].ToString()).ToString("dd/MM/yyyy"));
                tmplAttachment.SetAttribute("ENDDATE", SmartPortal.Common.Utilities.Utility.IsDateTime2(contractTable.Rows[0]["ENDDATE"].ToString()).ToString("dd/MM/yyyy"));


                tmplAttachment.SetAttribute("PRODUCT", contractTable.Rows[0]["PRODUCTNAME"].ToString());


                custID = contractTable.Rows[0]["CUSTID"].ToString();
            }

            //lay thong tin khach hang
            DataTable custTable = (new SmartPortal.SEMS.Customer().GetCustomerByCustcode(custID, ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];

            if (IPCERRORCODE != "0")
            {
                throw new IPCException(IPCERRORDESC);
            }
            //gan thong tin hop dong vao stringtemplate
            if (custTable.Rows.Count != 0)
            {
                tmplAttachment.SetAttribute("CUSTID", custTable.Rows[0]["CUSTID"].ToString());
                tmplAttachment.SetAttribute("FULLNAME", custTable.Rows[0]["FULLNAME"].ToString());
                switch (custTable.Rows[0]["CFTYPE"].ToString().Trim())
                {
                    case SmartPortal.Constant.IPC.PERSONAL:
                        tmplAttachment.SetAttribute("CUSTTYPE", Resources.labels.canhan);
                        break;
                    case SmartPortal.Constant.IPC.CORPORATE:
                        tmplAttachment.SetAttribute("CUSTTYPE", Resources.labels.doanhnghiep);
                        break;
                }
                tmplAttachment.SetAttribute("PHONE", custTable.Rows[0]["TEL"].ToString());
                tmplAttachment.SetAttribute("EMAIL", custTable.Rows[0]["EMAIL"].ToString());
                tmplBody.SetAttribute("EMAIL", custTable.Rows[0]["EMAIL"].ToString());
                tmplAttachment.SetAttribute("CIF", custTable.Rows[0]["CUSTCODE"].ToString());
            }

            //lay thong tin tai khoan

            DataTable userTable = (new SmartPortal.SEMS.Contract().GetUserByContractNo(hpcontractNo, "", "", ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
            if (IPCERRORCODE != "0")
            {
                throw new IPCException(IPCERRORDESC);
            }

            DataView dv = userTable.AsDataView();
            dv.Sort = "UserLevel DESC";
            userTable = dv.ToTable();

            string userInfo = string.Empty;

            //gan thong tin user vao stringtemplate
            foreach (DataRow row in userTable.Rows)
            {
                //04.10.2016 minh modify this like edit user: New or Unused-> send, logined-> not send
                //declare parameters for sms contract

                string IBuser = "";
                string IBpass = "";
                string PassMB = string.Empty;
                string PinCodeMB = string.Empty;
                string SMSphone = "";
                string PhoneNoMB = string.Empty;
                string SMSdefaultAccount = "";
                string SMSpincode = "";
                string PHOuser = "";
                string PHOpass = "";
                string PHOpincode = "";
                string PhoneNo = "";
                //edit by vutran 11102014
                //Kiem tra xem khach hang da dung IBMBSMS_Banking chua
                bool boolIB = new SmartPortal.SEMS.User().CheckServiceIsUsed(row["USERID"].ToString().Trim(), "IB", ref IPCERRORCODE, ref IPCERRORDESC);
                bool boolMB = new SmartPortal.SEMS.User().CheckServiceIsUsed(row["USERID"].ToString().Trim(), "MB", ref IPCERRORCODE, ref IPCERRORDESC);
                bool boolAM = new SmartPortal.SEMS.User().CheckServiceIsUsed(row["USERID"].ToString().Trim(), "AM", ref IPCERRORCODE, ref IPCERRORDESC);
                bool boolSMS = new SmartPortal.SEMS.User().CheckServiceIsUsed(row["USERID"].ToString().Trim(), "SMS", ref IPCERRORCODE, ref IPCERRORDESC);
                //if (boolIB || boolMB || boolSMS)
                //{

                //lay kieu nguoi dung
                string ut = "";
                switch (row["TYPEID"].ToString().Trim())
                {
                    case "":
                        ut = Resources.labels.chutaikhoan;
                        break;
                    case "QTHT":
                        ut = Resources.labels.quantrihethong;
                        break;
                    case "CTK":
                        ut = Resources.labels.chutaikhoan;
                        break;
                    case "C2":
                        ut = Resources.labels.nguoidungcap2;
                        break;
                    case "NUQ":
                        ut = Resources.labels.nguoiuyquyen;
                        break;
                    case SmartPortal.Constant.IPC.QUANLYTAICHINH:
                        ut = Resources.labels.quanlytaichinh;
                        break;
                    case SmartPortal.Constant.IPC.KETOAN:
                        ut = Resources.labels.ketoan;
                        break;
                    case SmartPortal.Constant.IPC.DONGCHUTAIKHOAN:
                        ut = Resources.labels.dongchutaikhoan;
                        break;


                }

                StringBuilder st = new StringBuilder();
                st.Append("<table style='width:100%;font-size:10px;'>");

                st.Append("<tr>");
                st.Append("<td style='background:silver;background-color: #C0C0C0; line-height:13px;' colspan=4>");
                st.Append("<b>  ACCOUNT INFORMATION</b>");
                st.Append("</td>");
                st.Append("</tr>");

                st.Append("<tr>");
                st.Append("<td witdth='15%' style='width:15%' colspan=1>  ");
                st.Append(Resources.labels.tendaydu + " :");
                st.Append("</td>");
                st.Append("<td witdth='35%' style='width:35%' colspan=1>  ");
                st.Append(row["FULLNAME"].ToString());
                st.Append("</td>");
                st.Append("<td witdth='15%' style='width:15%' colspan=1>  ");
                st.Append("Email :");
                st.Append("</td>");
                st.Append("<td witdth='35%'  style='width:35%' colspan=1>  ");
                st.Append(row["EMAIL"].ToString());
                st.Append("</td>");
                st.Append("</tr>");

                st.Append("<tr>");
                st.Append("<td colspan=1>  ");
                st.Append(Resources.labels.phone + " :");
                st.Append("</td>");
                st.Append("<td colspan=1>  ");
                st.Append(row["PHONE"].ToString());
                st.Append("</td>");
                st.Append("<td colspan=1>  ");
                st.Append(Resources.labels.kieunguoidung + " :");
                st.Append("</td>");
                st.Append("<td colspan=1>  ");
                st.Append(ut);
                st.Append("</td>");
                st.Append("</tr>");
                st.Append("<br><br>");

                //if (boolIB)
                //{
                //lay het các tai khoan Ibank cua user theo userID
                DataSet accountIBDataset = new SmartPortal.SEMS.User().GetUserRoleByServiceID(SmartPortal.Constant.IPC.IB, row["USERID"].ToString().Trim(), string.Empty, ref IPCERRORCODE, ref IPCERRORDESC);

                if (IPCERRORCODE != "0")
                {
                    throw new IPCException(IPCERRORDESC);
                }

                DataTable accountIBTable = accountIBDataset.Tables[0];
                if (accountIBTable.Rows.Count != 0 && !string.IsNullOrEmpty(accountIBTable.Rows[0][0].ToString()))
                {
                    if (accountIBTable.Rows[0]["ROLEID"].ToString().Trim() != "")
                    {
                        st.Append("<tr>");
                        st.Append("<td colspan='4'>");
                        st.Append("<B>  INTERNET BANKING</B><hr/>");
                        st.Append("</td>");
                        st.Append("</tr>");

                        st.Append("<tr>");
                        st.Append("<td colspan=1>  ");
                        st.Append(Resources.labels.username + " :");
                        st.Append("</td>");
                        st.Append("<td colspan=1>  ");
                        st.Append("<b>" + accountIBTable.Rows[0]["USERNAME"].ToString() + "</b>");
                        st.Append("</td>");
                        st.Append("<td colspan=1>  ");
                        st.Append(Resources.labels.password + " :");
                        st.Append("</td>");
                        st.Append("<td colspan=1>  ");
                        st.Append("<b>" + ((boolIB) ? DecryptPass(accountIBTable.Rows[0]["PASSWORD"].ToString()) : "********") + "</b>");
                        st.Append("</td>");
                        st.Append("</tr>");
                        if (boolIB)
                        {
                            IBuser = accountIBTable.Rows[0]["USERNAME"].ToString();
                            IBpass = DecryptPass(accountIBTable.Rows[0]["PASSWORD"].ToString());
                        }
                    }
                }
                //}
                //if (boolSMS)
                //{
                //lay het các tai khoan SMS cua user theo userID
                DataSet accountSMSDataset = new SmartPortal.SEMS.User().GetUserRoleByServiceID(SmartPortal.Constant.IPC.SMS, row["USERID"].ToString().Trim(), string.Empty, ref IPCERRORCODE, ref IPCERRORDESC);

                if (IPCERRORCODE != "0")
                {
                    throw new IPCException(IPCERRORDESC);
                }

                DataTable accountSMSTable = accountSMSDataset.Tables[0];
                if (accountSMSTable.Rows.Count != 0)
                {
                    if (accountSMSTable.Rows.Count != 0 && !string.IsNullOrEmpty(accountSMSTable.Rows[0][0].ToString()))
                    {
                        st.Append("<tr>");
                        st.Append("<td colspan='4'>");
                        st.Append("<br/>");
                        st.Append("</td>");
                        st.Append("</tr>");

                        st.Append("<tr>");
                        st.Append("<td colspan='4'>");
                        st.Append("<B>  SMS</B><hr/>");
                        st.Append("</td>");
                        st.Append("</tr>");

                        st.Append("<tr>");
                        st.Append("<td colspan=1>  ");
                        st.Append(Resources.labels.phone + " :");
                        st.Append("</td>");
                        st.Append("<td colspan=1>  ");
                        st.Append("<b>" + accountSMSTable.Rows[0]["UN"].ToString() + "</b>");
                        st.Append("</td>");
                        st.Append("<td colspan=1>  ");
                        st.Append(Resources.labels.taikhoanmacdinh + " :");
                        st.Append("</td>");
                        st.Append("<td colspan=1>  ");
                        st.Append("<b>" + accountSMSTable.Rows[0]["DEFAULTACCTNO"].ToString() + "</b>");
                        st.Append("</td>");
                        st.Append("</tr>");

                        st.Append("<tr>");
                        st.Append("<td colspan=1>  ");
                        st.Append(Resources.labels.mapin + " :");
                        st.Append("</td>");
                        st.Append("<td colspan=1>  ");
                        //st.Append("<b>" + accountSMSTable.Rows[0]["PINCODE"].ToString() + "</b>");
                        if (!string.IsNullOrEmpty(accountSMSTable.Rows[0]["PINCODE"].ToString()))
                            st.Append("<b>" + SmartPortal.Security.Encryption.Decrypt(accountSMSTable.Rows[0]["PINCODE"].ToString()) + "</b>");
                        st.Append("</td>");
                        st.Append("<td colspan=1>  ");
                        st.Append("");
                        st.Append("</td>");
                        st.Append("<td colspan=1>  ");
                        st.Append("");
                        st.Append("</td>");
                        st.Append("</tr>");
                        if (boolSMS)
                        {
                            SMSphone = accountSMSTable.Rows[0]["UN"].ToString();
                            SMSdefaultAccount = accountSMSTable.Rows[0]["DEFAULTACCTNO"].ToString();
                            //SMSpincode = accountSMSTable.Rows[0]["PINCODE"].ToString();
                            if (!string.IsNullOrEmpty(accountSMSTable.Rows[0]["PINCODE"].ToString()))
                                SMSpincode = SmartPortal.Security.Encryption.Decrypt(accountSMSTable.Rows[0]["PINCODE"].ToString());
                        }

                    }
                }
                //}
                //if (boolMB)
                //{
                //lay het các tai khoan MOBILE AM cua user theo userID
                #region MBA

                DataSet accountMBADataset = new SmartPortal.SEMS.User().GetUserRoleByServiceID(SmartPortal.Constant.IPC.AM, row["USERID"].ToString().Trim(), string.Empty, ref IPCERRORCODE, ref IPCERRORDESC);

                if (IPCERRORCODE != "0")
                {
                    throw new IPCException(IPCERRORDESC);
                }
                DataTable accountMBATable = accountMBADataset.Tables[0];

                if (accountMBATable.Rows.Count != 0 && !string.IsNullOrEmpty(accountMBATable.Rows[0][0].ToString()))
                {
                    if (accountMBATable.Rows[0]["ROLEID"].ToString().Trim() != "")
                    {
                        authenType = accountMBATable.Rows[0]["AUTHENTYPE"].ToString();
                        loginMethod = accountMBATable.Rows[0]["LOGINMETHOD"].ToString();
                        UserNameMB = accountMBATable.Rows[0]["USERNAME"].ToString();
                        PhoneNoMB = accountMBATable.Rows[0]["UN"].ToString();
                        if (!accountMBATable.Rows[0]["PASS"].ToString().Equals(""))
                        {
                            PassMB = DecryptPass(accountMBATable.Rows[0]["PASS"].ToString());
                        }
                        //PinCodeMB = SmartPortal.Security.Encryption.Decrypt(accountMBATable.Rows[0]["PINCODE"].ToString());
                    }
                    st.Append("<tr>");
                    st.Append("<td colspan='4'>");
                    st.Append("<br/>");
                    st.Append("</td>");
                    st.Append("</tr>");

                    st.Append("<tr>");
                    st.Append("<td colspan='4'>");
                    st.Append("<B>  MOBILE BANKING</B><hr/>");
                    st.Append("</td>");
                    st.Append("</tr>");

                    st.Append("<tr>");
                    if (loginMethod.Equals("PHONENO"))
                    {
                        st.Append("<td colspan=1>  ");
                        st.Append(Resources.labels.phone + " :");
                        st.Append("</td>");
                        st.Append("<td colspan=1>  ");
                        st.Append("<b>" + accountMBATable.Rows[0]["UN"].ToString() + "</b>");
                        st.Append("</td>");
                    }
                    else
                    {
                        st.Append("<td colspan=1>  ");
                        st.Append(Resources.labels.username + " :");
                        st.Append("</td>");
                        st.Append("<td colspan=1>  ");
                        st.Append("<b>" + accountMBATable.Rows[0]["USERNAME"].ToString() + "</b>");
                        st.Append("</td>");
                    }
                    st.Append("<td colspan=1>  ");
                    st.Append(Resources.labels.password + " :");
                    st.Append("</td>");
                    st.Append("<td colspan=1>  ");
                    st.Append("<b>" + accountMBATable.Rows[0]["PASS"].ToString() == "" ? "******" : DecryptPass(accountMBATable.Rows[0]["PASS"].ToString()) + "</b>");
                    st.Append("</td>");
                    st.Append("</tr>");

                    //st.Append("<tr>");
                    //st.Append("<td colspan=1>  ");
                    //st.Append(Resources.labels.pincode + " :");
                    //st.Append("</td>");
                    //st.Append("<td colspan=1>  ");
                    //st.Append("<b>" + accountMBATable.Rows[0]["PINCODE"].ToString() == "" ? "******" : SmartPortal.Security.Encryption.Decrypt(accountMBATable.Rows[0]["PINCODE"].ToString()) + "</b>");
                    //st.Append("</td>");
                    //st.Append("<td colspan=1>  ");
                    //st.Append("</td>");
                    //st.Append("<td colspan=1>  ");
                    //st.Append("</td>");
                    //st.Append("</tr>");
                }
                #endregion

                #region MB
                //lay het các tai khoan MB cua user theo userID
                DataSet accountMBDataset = new SmartPortal.SEMS.User().GetUserRoleByServiceID(SmartPortal.Constant.IPC.MB, row["USERID"].ToString().Trim(), string.Empty, ref IPCERRORCODE, ref IPCERRORDESC);

                if (IPCERRORCODE != "0")
                {
                    throw new IPCException(IPCERRORDESC);
                }

                DataTable accountMBTable = accountMBDataset.Tables[0];
                if ((accountMBTable.Rows.Count != 0 && !string.IsNullOrEmpty(accountMBTable.Rows[0][0].ToString())) || pendingforapprove)
                {
                    if (accountMBTable.Rows[0]["ROLEID"].ToString().Trim() != "" || pendingforapprove)
                    {
                        authenType = accountMBTable.Rows[0]["AUTHENTYPE"].ToString();
                        loginMethod = accountMBTable.Rows[0]["LOGINMETHOD"].ToString();
                        UserNameMB = accountMBTable.Rows[0]["USERNAME"].ToString();
                        PhoneNoMB = accountMBTable.Rows[0]["UN"].ToString();
                        if (!accountMBTable.Rows[0]["PASS"].ToString().Equals(""))
                        {
                            PassMB = DecryptPass(accountMBTable.Rows[0]["PASS"].ToString());
                        }
                        //if (accountMBTable.Rows[0]["PINCODE"].ToString().Equals(""))
                        //{
                        //    PinCodeMB = SmartPortal.Security.Encryption.Decrypt(accountMBTable.Rows[0]["PINCODE"].ToString());
                        //}

                    }
                    //st.Append("<tr>");
                    //st.Append("<td colspan='4'>");
                    //st.Append("<br/>");
                    //st.Append("</td>");
                    //st.Append("</tr>");

                    //st.Append("<tr>");
                    //st.Append("<td colspan='4'>");
                    //st.Append("<B>  MOBILE</B><hr/>");
                    //st.Append("</td>");
                    //st.Append("</tr>");

                    //st.Append("<tr>");
                    //if (loginMethod.Equals("PHONENO"))
                    //{
                    //    st.Append("<td colspan=1>  ");
                    //    st.Append(Resources.labels.phone + " :");
                    //    st.Append("</td>");
                    //    st.Append("<td colspan=1>  ");
                    //    st.Append("<b>" + accountMBTable.Rows[0]["UN"].ToString() + "</b>");
                    //    st.Append("</td>");
                    //}
                    //else
                    //{
                    //    st.Append("<td colspan=1>  ");
                    //    st.Append(Resources.labels.username + " :");
                    //    st.Append("</td>");
                    //    st.Append("<td colspan=1>  ");
                    //    st.Append("<b>" + accountMBTable.Rows[0]["USERNAME"].ToString() + "</b>");
                    //    st.Append("</td>");
                    //}
                    //st.Append("<td colspan=1>  ");
                    //st.Append(Resources.labels.password + " :");
                    //st.Append("</td>");
                    //st.Append("<td colspan=1>  ");
                    //st.Append("<b>" + accountMBTable.Rows[0]["PASS"].ToString() == "" ? "******" : DecryptPass(accountMBTable.Rows[0]["PASS"].ToString()) + "</b>");
                    //st.Append("</td>");
                    //st.Append("</tr>");

                    //st.Append("<tr>");
                    //st.Append("<td colspan=1>  ");
                    //st.Append(Resources.labels.pincode + " :");
                    //st.Append("</td>");
                    //st.Append("<td colspan=1>  ");
                    //st.Append("<b>" + accountMBTable.Rows[0]["PINCODE"].ToString() == "" ? "******" : SmartPortal.Security.Encryption.Decrypt(accountMBTable.Rows[0]["PINCODE"].ToString()) + "</b>");
                    //st.Append("</td>");
                    //st.Append("<td colspan=1>  ");
                    //st.Append("</td>");
                    //st.Append("<td colspan=1>  ");
                    //st.Append("</td>");
                    //st.Append("</tr>");
                }


                DataSet accountCardDataset = new SmartPortal.SEMS.User().GetUserRoleByServiceID("CAR", row["USERID"].ToString().Trim(), SmartPortal.Constant.IPC.NORMAL, ref IPCERRORCODE, ref IPCERRORDESC);

                if (IPCERRORCODE != "0")
                {
                    throw new IPCException(IPCERRORDESC);
                }

                DataTable accountCardTable = accountCardDataset.Tables[0];
                if (accountCardTable.Rows.Count != 0)
                {
                    st.Append("<tr>");
                    st.Append("<td colspan='4'>");
                    st.Append("<br/>");
                    st.Append("</td>");
                    st.Append("</tr>");

                    st.Append("<tr>");
                    st.Append("<td colspan='4'>");
                    st.Append("<B>  LINKED CARD</B><hr/>");
                    st.Append("</td>");
                    st.Append("</tr>");

                    foreach (DataRow drcard in accountCardTable.Rows)
                    {
                        SmartPortal.Common.Log.WriteLogFile("", "", "", "==CardNo" + "//" + drcard["CardNo"].ToString());
                        SmartPortal.Common.Log.WriteLogFile("", "", "", "==HolderName" + "//" + drcard["HolderName"].ToString());
                        st.Append("<tr>");
                        st.Append("<td colspan=1>  ");
                        st.Append(Resources.labels.sothe + " :");
                        st.Append("</td>");
                        st.Append("<td colspan=1>  ");
                        st.Append("<b>" + SmartPortal.Common.Utilities.Utility.MaskDigits(drcard["CardNo"].ToString()) + "</b>");
                        st.Append("</td>");
                        st.Append("<td colspan=1>  ");
                        st.Append(Resources.labels.tendaydu + " :");
                        st.Append("</td>");
                        st.Append("<td colspan=1>  ");
                        st.Append("<b>" + drcard["HolderName"].ToString() + "</b>");
                        st.Append("</td>");
                        st.Append("</tr>");

                        //st.Append("<tr>");
                        //st.Append("<td>");
                        //st.Append(Resources.labels.mapin + " :");
                        //st.Append("</td>");
                        //st.Append("<td>");
                        //st.Append("<b>" + accountMBTable.Rows[0]["PINCODE"].ToString() + "</b>");
                        //st.Append("</td>");
                        //st.Append("<td>");
                        //st.Append("");
                        //st.Append("</td>");
                        //st.Append("<td>");
                        //st.Append("");
                        //st.Append("</td>");
                        //st.Append("</tr>");
                    }
                }
                #endregion
                st.Append("</table>");
                userInfo += st.ToString();
                tmplAttachment.RemoveAttribute("USERINFO");
                if (contractTable.Rows[0]["contracttype"].ToString().Trim().Equals(SmartPortal.Constant.IPC.CONTRACTCORPSIMPLE)
                    && row["typeid"].ToString().Trim().Equals(SmartPortal.Constant.IPC.CHUTAIKHOAN))
                {
                    tmplAttachment.SetAttribute("USERINFO", userInfo);
                }
                else
                {
                    tmplAttachment.SetAttribute("USERINFO", st.ToString());
                }

                //tmplAttachment.SetAttribute("USERINFO", st.ToString());
                string strAttachment = "";
                string strBody = "";
                string strAttachmentFileName = "Contract Information";
                string strEmailFrom = System.Configuration.ConfigurationManager.AppSettings["contractapprovemailfrom"];
                string strEmailTo = row["EMAIL"].ToString();
                string strSubject = System.Configuration.ConfigurationManager.AppSettings["contractapprovemailtitle"];
                strBody = tmplBody.ToString();
                strAttachment += "<span style='font-size: 12.0pt'><img width='160' height='50' src='#'></span>";
                strAttachment += tmplAttachment.ToString();
                strAttachment += "<div><br/><p><a href=' target='blank'></a></p><span style='font-weight:bold;'>" + Resources.labels.AbankthankyouforusingAYAebankingservices + "!	</span></div>";
                PhoneNo = PhoneNoMB;
                SmartPortal.Common.Log.WriteLogFile("SEND MAIL APPROVE CONTRACT: " + contractNo, "", "", "PHONENUMBER: " + PhoneNoMB);
                //send mail
                //Session["test1"] = tmplBody.ToString();

                //    SmartPortal.Common.EmailHelper.SendMailMessageAsync(strEmailFrom, strEmailTo, strSubject, strBody, strAttachment, strAttachmentFileName);
                //test
                string errorcode = "";
                string errorDesc = "";

                //24/7/2015 minh add to send sms contract
                contracttype = string.Empty;
                bool isowner = false;
                bool iscoowner = false;
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
                        if (!UserNameMB.Equals(""))
                        {
                            if (loginMethod.Equals("PHONENO"))
                            {
                                mbuserinfo = "/MB:PhoneNo-" + PhoneNoMB;
                            }
                            else
                            {
                                mbuserinfo = "/MB:UserName-" + UserNameMB;
                            }
                        }
                        else
                        {
                            mbuserinfo = "/MB:PhoneNo-" + PhoneNoMB + ",password-" + PassMB;
                        }
                        if (!PassMB.Equals(""))
                        {
                            mbuserinfo += ",Password-" + PassMB;
                        }
                    }
                    else if (boolAM)
                    {
                        if (!UserNameMB.Equals(""))
                        {
                            if (loginMethod.Equals("PHONENO"))
                            {
                                mbuserinfo = "/AM:PhoneNo-" + PhoneNoMB;
                            }
                            else
                            {
                                mbuserinfo = "/AM:UserName-" + UserNameMB;
                            }
                        }
                        else
                        {
                            mbuserinfo = "/AM:PhoneNo-" + PhoneNoMB + ",password-" + PassMB;
                        }
                        if (!PassMB.Equals(""))
                        {
                            mbuserinfo += ",Password-" + PassMB;
                        }
                    }
                    else
                    {
                        if (!UserNameMB.Equals(""))
                        {
                            if (loginMethod.Equals("PHONENO"))
                            {
                                mbuserinfo = "/:PhoneNo-" + PhoneNoMB;
                            }
                            else
                            {
                                mbuserinfo = "/:UserName-" + UserNameMB;
                            }
                        }
                        else
                        {
                            mbuserinfo = "/:PhoneNo-" + PhoneNoMB + ",password-" + PassMB;
                        }
                        if (!PassMB.Equals(""))
                        {
                            mbuserinfo += ",Password-" + PassMB;
                        }
                    }
                    SENDINFO = contractinfo + ibuserinfo + smsuserinfo + mbuserinfo;
                }
                contracttype = contractTable.Rows[0]["contracttype"].ToString().Trim();

                SmartPortal.Common.EmailHelper.SendMailMessageAsync(strEmailFrom, strEmailTo, strSubject, strBody, strAttachment, strAttachmentFileName);
                SmartPortal.SEMS.Contract.sendSMS_Contract(PhoneNo, SENDINFO, ref errorcode, ref errorDesc);

                SmartPortal.Common.Log.WriteLogFile("EMAIL+SMS LOG", "", "", "sent mail to " + strEmailTo + " sms send to " + PhoneNo);
            }
            //        }

        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.WriteLogFile("SEND MAIL " + contractNo + " ERROR", "", "", "ERROR CONTENT: " + ex.ToString());
        }
    }
    private string DecryptPass(string values)
    {
        if (values.Equals(""))
        {
            return "******";
        }
        else
        {
            return SmartPortal.Security.Encryption.Decrypt(values);
        }
    }
}
