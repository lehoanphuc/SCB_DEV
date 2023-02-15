using SmartPortal.Common.Utilities;
using SmartPortal.ExceptionCollection;
using SmartPortal.Model;
using SmartPortal.Security;
using SmartPortal.SEMS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Widgets_SEMSCustomerListCorp_Controls_CorporateMatrix : System.Web.UI.UserControl
{
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    ContractModel model;
    EventHandler backMainPage;
    internal int minlength = int.Parse(ConfigurationManager.AppSettings["minlengthloginname"].ToString());
    internal int maxlength = int.Parse(ConfigurationManager.AppSettings["maxlengthloginname"].ToString());

    public Widgets_SEMSCustomerListCorp_Controls_CorporateMatrix() { }
    public Widgets_SEMSCustomerListCorp_Controls_CorporateMatrix(ContractModel info, EventHandler e)
    {
        model = info;
        backMainPage = e;
        if ((!string.IsNullOrEmpty(model.cusCode) && model.isNew) || (!model.isNew && !string.IsNullOrEmpty(model.cusID)))
            LoadData();
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    private void LoadData()
    {
        try
        {
            //btnAddNewUser.Visible = false;
            //btnAddUserMT.Visible = rbIBType.Checked;
            if (rbIBGenerate.Checked)
            {
                txtIBTypeUserName.Enabled = false;
                if (model.isNew)
                    txtIBGenUserName.Text = generateUserName();
                //txtIBGenUserName.Text = SmartPortal.Common.Utilities.Utility.GetID(model.fullName, model.cusCode, model.licenseId);
            }
            else
                txtIBTypeUserName.Enabled = true;

            SetControlEnable();

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

                        radIBAllAccount.Checked = true;
                        rbIBGenerate.Checked = true;
                        ddlIBAccount.Enabled = false;

                        model.fullName = dtCustInfo.Rows[0]["FULLNAME"].ToString();
                        model.licenseId = dtCustInfo.Rows[0]["LICENSEID"].ToString().Trim();
                        //txtIBGenUserName.Text = SmartPortal.Common.Utilities.Utility.GetID(dtCustInfo.Rows[0]["FULLNAME"].ToString().Trim(), dtCustInfo.Rows[0][SmartPortal.Constant.IPC.CUSTCODE].ToString().Trim(), dtCustInfo.Rows[0]["LICENSEID"].ToString().Trim()) + "O1";
                        txtIBGenUserName.Text = SmartPortal.Common.Utilities.Utility.GetID(dtCustInfo.Rows[0]["FULLNAME"].ToString().Trim(), model.cusCode, dtCustInfo.Rows[0]["LICENSEID"].ToString().Trim()) + "O1";
                        if (int.Parse(ConfigurationManager.AppSettings["IBMBSameUser"].ToString()) == 1)
                        {
                            txtMBPhone.Text = txtIBGenUserName.Text;
                            txtPHOPhone.Text = txtIBGenUserName.Text;
                        }
                        else
                        {
                            txtMBPhone.Text = SmartPortal.Common.Utilities.Utility.GetID("", dtCustInfo.Rows[0][SmartPortal.Constant.IPC.CUSTCODE].ToString().Trim(), "", 10) + "O5";
                            txtPHOPhone.Text = txtMBPhone.Text;
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
                throw new IPCException(SmartPortal.Constant.IPC.ERRORCODE.IPC);
            }
            if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                throw new IPCException("4017");
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
                ddlIBAccount.DataSource = dtAccountQT;
                ddlIBAccount.DataTextField = "ACCOUNTNO";
                ddlIBAccount.DataValueField = "ACCOUNTNO";
                ddlIBAccount.DataBind();

                ddlSMSDefaultAccount.DataSource = dtAccountQT;
                ddlSMSDefaultAccount.DataTextField = "ACCOUNTNO";
                ddlSMSDefaultAccount.DataValueField = "ACCOUNTNO";
                ddlSMSDefaultAccount.DataBind();

            }

            ASLevelModel level = new ASLevelModel();
            level.STRUCTUREID = model.structureId;
            DataSet dsResult = new SmartPortal.SEMS.ApprovalStructure().LevelGetAll(level, ref IPCERRORCODE, ref IPCERRORDESC);

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
            txtIBGenUserName.Enabled = false;
            LoadDataInTreeview(SmartPortal.Constant.IPC.IB, tvIBRole, SmartPortal.Constant.IPC.MTRUSER);
            LoadDataInTreeview(SmartPortal.Constant.IPC.SMS, tvSMS, SmartPortal.Constant.IPC.MTRUSER);
            LoadDataInTreeview(SmartPortal.Constant.IPC.MB, tvMB, SmartPortal.Constant.IPC.MTRUSER);
            LoadDataInTreeview(SmartPortal.Constant.IPC.PHO, tvPHO, SmartPortal.Constant.IPC.MTRUSER);
            GetRoleDefault(tvIBRole, tvSMS, tvMB, tvPHO);

            DataSet dspolicy = new DataSet();
            string filterIB = "serviceid='IB'";
            string filterSMS = "serviceid='SMS'";
            string filterMB = "serviceid='MB'";
            string stSort = "serviceid asc";
            dspolicy = new USERPOLICY().GetPolicybyCondition(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, Session["userName"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {


                DataTable dtIB = dspolicy.Tables[0].Select(filterIB, stSort).CopyToDataTable();
                DataTable dtSMS = dspolicy.Tables[0].Select(filterSMS, stSort).CopyToDataTable();
                DataTable dtMB = dspolicy.Tables[0].Select(filterMB, stSort).CopyToDataTable();

                //advance contract
                ddlPolicyIB.DataSource = dtIB;

                ddlPolicyIB.DataTextField = "policytx";
                ddlPolicyIB.DataValueField = "policyid";
                ddlPolicyIB.DataBind();
                //sms advance
                ddlPolicySMS.DataSource = dtSMS;
                ddlPolicySMS.DataTextField = "policytx";
                ddlPolicySMS.DataValueField = "policyid";
                ddlPolicySMS.DataBind();
                //mb advance
                ddlPolicyMB.DataSource = dtMB;
                ddlPolicyMB.DataTextField = "policytx";
                ddlPolicyMB.DataValueField = "policyid";
                ddlPolicyMB.DataBind();
            }

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
    private bool IsSuccess()
    {
        return IPCERRORCODE.Equals("0") ? true : false;
    }
    private bool IsDataSetNotNull(DataSet ds)
    {
        return ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 ? true : false;
    }
    protected void btnMatrixDetail_Click(object sender, EventArgs e)
    {
        if (rdAdd.Checked)
            LoadDetailsAdd(txtMTCuscode.Text.Trim());
        else
        {
            LoadDetailsEdit();
        }
        ddlIBAccount.Enabled = radIBAccount.Checked;
        //btnAddUserMT.Visible = true;
    }
    protected void btnMatrixDetailAdd_Click(object sender, EventArgs e)
    {
        LoadDetailsAdd(txtMTCuscodeAdd.Text.Trim());
    }
    private void LoadDetailsAdd(string cuscode)
    {
        try
        {
            lblError.Text = "";
            //rdEdit.Enabled = false;
            //ddlUser.Enabled = false;

            txtMTFullName.Text = string.Empty;
            txtMTEmail.Text = string.Empty;
            txtMTPhone.Text = string.Empty;
            ddlMTGender.SelectedIndex = 0;
            txtMTBirth.Text = string.Empty;
            txtMTAddress.Text = string.Empty;

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
            txtMTEmail.Text = hasUserInfo[SmartPortal.Constant.IPC.EMAIL] != null ? hasUserInfo[SmartPortal.Constant.IPC.EMAIL].ToString() : string.Empty;
            txtMTPhone.Text = hasUserInfo[SmartPortal.Constant.IPC.PHONE] != null ? hasUserInfo[SmartPortal.Constant.IPC.PHONE].ToString() : string.Empty;
            txtMTAddress.Text = hasUserInfo[SmartPortal.Constant.IPC.ADDRESS] != null ? hasUserInfo[SmartPortal.Constant.IPC.ADDRESS].ToString() : string.Empty;

            if (hasUserInfo[SmartPortal.Constant.IPC.SEX] != null)
            {
                try
                {
                    ddlMTGender.SelectedValue = Utility.FormatStringCore(int.Parse(hasUserInfo[SmartPortal.Constant.IPC.SEX].ToString()).ToString());
                    ddlMTGender.Visible = hasUserInfo[SmartPortal.Constant.IPC.SEX].ToString().Trim() == string.Empty;
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

            // add to generate userid
            SetGenerateUser(true, txtMTFullName.Text);

            if (string.IsNullOrEmpty(txtIBGenUserName.Text))
                txtIBGenUserName.Text = SmartPortal.Common.Utilities.Utility.GetID(hasUserInfo[SmartPortal.Constant.IPC.CUSTNAME].ToString(), hasUserInfo[SmartPortal.Constant.IPC.CUSTCODE].ToString(), hasUserInfo[SmartPortal.Constant.IPC.LICENSE].ToString()) + "O1";

        }
        catch (Exception ex)
        {
        }
    }
    private void LoadDetailsEdit()
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
                            fullname = dRow["colFullName"],
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
                            mbpass = dRow["colMBPass"],
                            phophone = dRow["colPHOPhone"],
                            phopass = dRow["colPHOPass"],
                            phodfact = dRow["colPHODefaultAcctno"]
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
                txtMTPhone.Text = user.phone.ToString();
                txtMTEmail.Text = user.email.ToString();
                txtMTAddress.Text = user.address.ToString();
                txtSMSPhone.Text = user.smsphone.ToString();
                ddlSMSDefaultAccount.SelectedValue = user.smsdfact.ToString();
                ddlSMSDefaultLang.SelectedValue = user.smsdflang.ToString();
                txtMBPhone.Text = user.mbphone.ToString();
                ddlMTPHODefaultAcctno.SelectedValue = user.phodfact.ToString();

                SetGenerateUser(Convert.ToBoolean(user.renderIBUserName));
                if (rbIBGenerate.Checked)
                {
                    txtIBGenUserName.Text = user.username.ToString();
                    txtIBTypeUserName.Text = string.Empty;
                }
                else
                {
                    txtIBTypeUserName.Text = user.username.ToString();
                    txtIBGenUserName.Text = string.Empty;
                }

                //set role theo user edit
                //TreeView[] aTree = new TreeView[] { tvIBRole, tvMB, tvPHO, tvSMS };
                //SetRoleUserEdit(aTree, tblSUM, value);
                btnAddNewUser.Visible = true;
                GetRoleDefault(tvIBRole, tvSMS, tvMB, tvPHO);

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
        }
    }

    protected void ddlUserLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ApprovalWorkflowModel workflow = new ApprovalWorkflowModel();
        //workflow.STRUCTUREID = model.structureId;
        //workflow.USERLEVEL = ddlUserLevel.SelectedValue;
        //DataSet dsResult = new SmartPortal.SEMS.ApprovalWorkflow().ApprovalWorkflowGetGroup(workflow, ref IPCERRORCODE, ref IPCERRORDESC);
        //if (IsSuccess())
        //{
        //    if (IsDataSetNotNull(dsResult))
        //    {
        //        ddlGroup.DataSource = dsResult.Tables[0];
        //        ddlGroup.DataTextField = "GroupIDName";
        //        ddlGroup.DataValueField = "GroupID";
        //        ddlGroup.DataBind();
        //    }
        //}
        //try
        //{
        //    if (!string.IsNullOrEmpty(hdGroupID.Value))
        //        ddlGroup.SelectedValue = hdGroupID.Value;
        //}
        //catch { }
    }

    protected void ddlMTPolicyIB_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void btnAddUserMT_Click(object sender, EventArgs e)
    {
        if (!validateUser())
            return;
        try
        {
            int passlenIB = 0;
            int passlenSMS = 0;
            int passlenMB = 0;

            UserContractInfo info = new UserContractInfo();
            //check userib
            info.isRenderUserName = rbIBGenerate.Checked;
            if (info.isRenderUserName)
            {
                info.IBUserName = txtIBGenUserName.Text.Trim();
            }
            else if (rbIBType.Checked)
            {
                info.IBUserName = txtIBTypeUserName.Text.Trim();
            }
            #region check userName

            if (info.IBUserName == string.Empty)
            {
                lblError.Text = Resources.labels.bancannhaptendangnhap;
                return;
            }
            else if (info.IBUserName.Length < minlength || info.IBUserName.Length > maxlength)
            {
                lblError.Text = string.Format(Resources.labels.usernamemustbetween, minlength, maxlength);
                return;
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


            DataTable dtCR = ViewState["DataCurrent"] != null ? (DataTable)ViewState["DataCurrent"] : new DataTable();

            //Tao key cho user, dung khi edit user
            if (dtCR.Rows.Count == 0)
                info.order = DateTime.Now.GetHashCode().ToString();
            else
                info.order = dtCR.Rows[0]["colOrder"].ToString();

            DataTable dtUser = ViewState["MTRUSER"] != null ? (DataTable)ViewState["MTRUSER"] : new DataTable();
            //if (dtUser.Rows.Count > 0 && dtUser.Select("colIBUserName='" + info.IBUserName + "' and colOrder<>'" + info.order + "'").Length > 0)
            //{
            //    lblError.Text = Resources.labels.usernameexist;
            //    return;
            //}


            info.cusCode = txtMTCuscode.Text.Trim();
            info.fullName = txtMTFullName.Text;
            info.level = "0";//ddlUserLevel.Text;
            //info.group = ddlGroup.SelectedValue;
            info.dob = txtMTBirth.Text;
            info.gender = ddlMTGender.SelectedValue;
            info.phone = txtMTPhone.Text;
            info.email = txtMTEmail.Text;
            info.address = txtMTAddress.Text;
            info.SMSPhone = txtSMSPhone.Text;
            info.SMSDefaultAcctno = ddlSMSDefaultAccount.SelectedValue;
            info.SMSDefaultLang = ddlSMSDefaultLang.SelectedValue;
            info.MBPhone = txtMBPhone.Text;
            info.PHODefaultAcctno = ddlMTPHODefaultAcctno.SelectedValue;
            #region Tao bang chua cac thong tin user

            // load infor about policy selected to get len of pass generate
            DataSet dspolicy = new DataSet();
            string filterIB = "serviceid='IB' and policyid='" + ddlPolicyIB.SelectedValue.ToString() + "'";
            string filterSMS = "serviceid='SMS'  and policyid='" + ddlPolicySMS.SelectedValue.ToString() + "'";
            string filterMB = "serviceid='MB' and policyid='" + ddlPolicyMB.SelectedValue.ToString() + "'";
            string stSort = "serviceid asc";

            dspolicy = new SmartPortal.SEMS.USERPOLICY().GetPolicybyCondition(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, Session["userName"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE.Equals("0"))
            {
                DataTable dtIB = dspolicy.Tables[0].Select(filterIB, stSort).CopyToDataTable();
                DataTable dtSMS = dspolicy.Tables[0].Select(filterSMS, stSort).CopyToDataTable();
                DataTable dtMB = dspolicy.Tables[0].Select(filterMB, stSort).CopyToDataTable();
                passlenIB = Convert.ToInt32(dtIB.Rows[0]["minpwdlen"].ToString());
                passlenSMS = Convert.ToInt32(dtSMS.Rows[0]["minpwdlen"].ToString());
                passlenMB = Convert.ToInt32(dtMB.Rows[0]["minpwdlen"].ToString());
            }

            string passreveal = DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - passlenIB, passlenIB);
            //info.pwdreset = Encryption.Encrypt(passreveal);
            info.IBUserPass = SmartPortal.SEMS.O9Encryptpass.sha_sha256(passreveal, info.IBUserName);
            info.SMSPinCode = DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - passlenSMS, passlenSMS);

            if (int.Parse(ConfigurationManager.AppSettings["IBMBSameUser"].ToString()) == 1)
            {
                info.MBPhone = info.IBUserName;
                info.PHOPhone = info.IBUserName;
            }
            else
            {
                info.MBPhone = SmartPortal.Common.Utilities.Utility.GetID(info.fullName, info.cusCode, model.cusType) + "O5";
                info.PHOPhone = info.IBUserName;
            }


            //Luu account va user cua contract
            List<string> lsAccNo = new List<string>();

            if (radIBAccount.Checked)
            {
                lsAccNo.Add(ddlIBAccount.SelectedValue);
            }
            else
            {
                foreach (var item in ddlIBAccount.Items.Cast<ListItem>().Select(i => i.Value))
                {
                    lsAccNo.Add(item);
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

            SaveRoleInfo(info, gvMTUser, tvIBRole, tvSMS, tvMB, tvPHO, lsAccNo);
            btnAddNewUser.Visible = true;
            btnMTCancel.Visible = true;
            lblError.Text = Resources.labels.recordsaved;
            SetControlEnable();

            //btnAddUserMT.Visible = false;
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
    }

    protected void btnMTCancel_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dtCr = ViewState["DataCurrent"] != null ? (DataTable)ViewState["DataCurrent"] : new DataTable();
            if (dtCr.Rows.Count > 0)
            {

                ViewState["DataCurrent"] = null;
                DataTable dtUser = ViewState["MTRUSER"] != null ? ((DataTable)ViewState["MTRUSER"]) : new DataTable();
                string arg = dtCr.Rows[0]["colOrder"].ToString();
                if (!string.IsNullOrEmpty(dtCr.Rows[0]["colOrder"].ToString()))
                {
                    if (dtUser.Rows.Count > 0)
                    {
                        foreach (DataRow r in dtUser.Select("colOrder='" + arg + "'"))
                        {
                            dtUser.Rows.Remove(r);
                        }
                        ViewState["MTRUSER"] = dtUser;

                        ddlUser.Enabled = rdEdit.Checked;
                    }
                }
                else
                {
                    ViewState["MTRUSER"] = null;
                }
                //dtCr.Rows.Clear();
                //ViewState["DataCurrent"] = dtCr;
            }
            //khi chua luu user hien tai, khong duoc phep tao user moi.
            //btnAddNewUser.Visible = false;

            lblError.Text = Resources.labels.recorddeleted;
            gvMTUser.DataSource = new DataTable();
            gvMTUser.DataBind();
            SetControlEnable();
        }
        catch
        {
        }
    }

    protected void gvMTUser_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvMTUser.PageIndex = e.NewPageIndex;
            gvMTUser.DataSource = ((DataTable)ViewState["MTRUSER"]).Select("colFullName='" + gvMTUser.Rows[0].Cells[0].Text + "'").CopyToDataTable();
            gvMTUser.DataBind();
        }
        catch
        {
        }
    }

    protected void gvMTUser_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            DataTable tblResultUser = (DataTable)ViewState["MTRUSER"];
            DataRow[] delRow = tblResultUser.Select("colIBUserName='" + gvMTUser.Rows[e.RowIndex].Cells[0].Text + "' AND colAccount='" + gvMTUser.Rows[e.RowIndex].Cells[2].Text + "' AND colRole='" + gvMTUser.Rows[e.RowIndex].Cells[3].Text + "'");
            foreach (DataRow r in delRow)
            {
                tblResultUser.Rows.Remove(r);
            }

            ViewState["MTRUSER"] = tblResultUser;
            DataTable dt = tblResultUser.Select("colFullName='" + gvMTUser.Rows[e.RowIndex].Cells[0].Text + "'").CopyToDataTable();
            gvMTUser.DataSource = dt.DefaultView.ToTable(true, "colFullName", "colIBUserName", "colAccount", "colRole");
            gvMTUser.DataBind();

            lblError.Text = Resources.labels.recorddeleted;
        }
        catch
        {
        }
    }
    void LoadDataInTreeview(string serviceID, TreeView tvPage, string userType)
    {
        tvPage.Nodes.Clear();
        DataTable tblSS = new DataTable();
        tblSS = new SmartPortal.SEMS.Role().GetByServiceAndUserType(serviceID, userType, model.productId);

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
    void GetRoleDefault(TreeView treeIB, TreeView treeSMS, TreeView treeMB, TreeView treePHO)
    {
        DataTable tblRoleDefault = new DataTable();
        //lay role mac dinh IB
        tblRoleDefault = (new SmartPortal.SEMS.Role().GetRoleDefaultByServiceID(SmartPortal.Constant.IPC.IB, model.productId, 0, string.Empty, ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];

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
    private void ShowPopUpMsg(string msg)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("alert('");
        sb.Append(msg.Replace("\n", "\\n").Replace("\r", "").Replace("'", "\\'"));
        sb.Append("');");
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "showalert", sb.ToString(), true);
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
    //Update viewstate usser for contract
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

    //define user table
    DataTable ContructorTableUser()
    {
        DataColumn colFullName = new DataColumn("colFullName");
        DataColumn colLevel = new DataColumn("colLevel");
        DataColumn colGroup = new DataColumn("colGroup");
        DataColumn colBirthday = new DataColumn("colBirthday");
        DataColumn colGender = new DataColumn("colGender");
        DataColumn colPhone = new DataColumn("colPhone");
        DataColumn colEmail = new DataColumn("colEmail");
        DataColumn colAddress = new DataColumn("colAddress");
        DataColumn colIsRenderUserName = new DataColumn("colIsRenderUserName");
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
        DataColumn colTypeID = new DataColumn("colTypeID");
        DataColumn colOrder = new DataColumn("colOrder");


        DataColumn colIBPolicy = new DataColumn("colIBPolicy");
        DataColumn colSMSPolicy = new DataColumn("colSMSPolicy");
        DataColumn colMBPolicy = new DataColumn("colMBPolicy");
        DataColumn colpwdreset = new DataColumn("colpwdreset");


        DataTable tblUser = new DataTable();
        tblUser.Columns.Add(colFullName);
        tblUser.Columns.Add(colLevel);
        tblUser.Columns.Add(colGroup);
        tblUser.Columns.Add(colBirthday);
        tblUser.Columns.Add(colGender);
        tblUser.Columns.Add(colPhone);
        tblUser.Columns.Add(colEmail);
        tblUser.Columns.Add(colAddress);
        tblUser.Columns.Add(colIsRenderUserName);
        tblUser.Columns.Add(colIBUserName);
        tblUser.Columns.Add(colIBPass);
        tblUser.Columns.Add(colSMSPhone);
        tblUser.Columns.Add(colSMSDefaultAcctno);
        tblUser.Columns.Add(colSMSDefaultLang);
        tblUser.Columns.Add(colSMSIsDefault);
        tblUser.Columns.Add(colSMSPinCode);
        tblUser.Columns.Add(colMBPhone);
        tblUser.Columns.Add(colMBPass);
        tblUser.Columns.Add(colMBPinCode);
        tblUser.Columns.Add(colPHOPhone);
        tblUser.Columns.Add(colPHOPass);
        tblUser.Columns.Add(colPHODefaultAcctno);
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
        tblUser.Columns.Add(colpwdreset);
        return tblUser;
    }

    // add row in user table
    DataRow CreateRowToAddUser(DataTable tblUser, UserContractInfo info, TreeNode nodeRole, TreeNode nodeTrancode, string serviceID, string Account, bool ispho)
    {
        DataRow rowUser = tblUser.NewRow();
        rowUser["colFullName"] = info.fullName;
        rowUser["colLevel"] = info.level;
        rowUser["colGroup"] = info.group;
        rowUser["colBirthday"] = info.dob;
        rowUser["colGender"] = info.gender;
        rowUser["colPhone"] = info.phone;
        rowUser["colEmail"] = info.email;
        rowUser["colAddress"] = info.address;
        rowUser["colIsRenderUserName"] = info.isRenderUserName;
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
        rowUser["colPHOPhone"] = info.PHOPhone;
        rowUser["colPHOPass"] = info.PHOPass;
        rowUser["colPHODefaultAcctno"] = info.PHODefaultAcctno;
        rowUser["colAccount"] = Account;
        rowUser["colRole"] = nodeRole.Text;
        rowUser["colRoleID"] = nodeRole.Value;
        if (nodeTrancode != null)
        {
            rowUser["colTranCode"] = nodeTrancode.Text;
            rowUser["colTranCodeID"] = nodeTrancode.Value;
        }

        rowUser["colServiceID"] = serviceID;
        rowUser["colTypeID"] = SmartPortal.Constant.IPC.MTRUSER;
        rowUser["colOrder"] = info.order;

        if (!ispho)
        {
            rowUser["colIBPolicy"] = ddlPolicyIB.SelectedValue.ToString();
            rowUser["colSMSPolicy"] = ddlPolicySMS.SelectedValue.ToString();
            rowUser["colMBPolicy"] = ddlPolicyMB.SelectedValue.ToString();
        }
        //rowUser["colpwdreset"] = info.pwdreset;
        return rowUser;
    }
    protected void btnAddNewUser_Click(object sender, EventArgs e)
    {
        lblError.Text = string.Empty;
        rdAdd.Checked = true;
        rdEdit.Checked = false;
        rdEdit.Enabled = true;
        txtMTCuscode.Text = string.Empty;
        txtMTCuscode.Enabled = true;
        SetControlEnable();


        txtMTFullName.Text = string.Empty;
        txtMTEmail.Text = string.Empty;
        txtMTPhone.Text = string.Empty;
        ddlMTGender.SelectedIndex = 0;
        txtMTBirth.Text = string.Empty;
        txtMTAddress.Text = string.Empty;
        //ddlUserLevel.SelectedIndex = 0;
        //ddlGroup.SelectedIndex = 0;
        //hdGroupID.Value = string.Empty;
        ddlPolicyIB.SelectedIndex = 0;

        SetGenerateUser(true);
        txtIBGenUserName.Text = string.Empty;
        txtIBGenUserName.Text = generateUserName();
        //txtIBGenUserName.Text = SmartPortal.Common.Utilities.Utility.GetID(model.fullName, model.cusCode, model.licenseId);
        txtIBTypeUserName.Text = string.Empty;

        txtSMSPhone.Text = string.Empty;
        ddlSMSDefaultAccount.SelectedIndex = 0;
        ddlSMSDefaultLang.SelectedIndex = 0;
        ddlPolicySMS.SelectedIndex = 0;
        cbSMSIsDefault.Checked = true;
        txtMBPhone.Text = string.Empty;
        ddlPolicyMB.SelectedIndex = 0;
        txtPHOPhone.Text = string.Empty;
        ddlMTPHODefaultAcctno.SelectedIndex = 0;

        btnMTCancel.Visible = false;
        //btnAddUserMT.Visible = rbIBType.Checked;

        gvMTUser.DataSource = new DataTable();
        gvMTUser.DataBind();

        rdAdd.Enabled = true;

        btnAddNewUser.Visible = false;

        GetRoleDefault(tvIBRole, tvSMS, tvMB, tvPHO);

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
                trAddUser.Visible = false;
                ddlUser.DataSource = distinctUser;
                ddlUser.DataTextField = "fullname";
                ddlUser.DataValueField = "order";
                ddlUser.DataBind();
            }
            else
            {
                trAddUser.Visible = true;
            }
        }
        else
        {
            trAddUser.Visible = true;
        }
        divAction.Visible = !trAddUser.Visible;
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        backMainPage(sender, e);
    }
    protected void btnCustSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (model.corpType.Equals(SmartPortal.Constant.IPC.CONTRACTCORPMATRIX))
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
                DataColumn colNewTypeID = new DataColumn("colNewTypeID");
                DataColumn[] colUsers = new DataColumn[] { colUserID, colUContractNo, colUFullName, colUGender, colUAddress , colUEmail , colUPhone
                    , colUStatus,colUUserCreate,colUDateCreate,colUUserModify,colULastModify ,colUUserApprove,colUserType,colUserLevel,colDeptID,colTokenID
                    ,colTokenIssueDate,colSMSOTP,colSMSBirthday,colNewTypeID};
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

                DataColumn[] colbankUser = new DataColumn[] { colUserName, colIBUserID, colIBPassword, colLastLoginTime, colIBStatus
                    , colIBUserCreate, colIBDateCreate , colIBUserModify,colIBLastModify,colIBUserApprove,colIBIsLogin ,colIBDateExpire
                    ,colIBExpireTime,colIBPolicyusr,colpwdresetIB};
                //add column vào table
                tblIbankUser.Columns.AddRange(colbankUser);
                #endregion
                var distinctUser = (from DataRow dRow in tblSUM.Rows
                                    select new
                                    {
                                        username = dRow["colIBUserName"],
                                        ibpass = dRow["colIBPass"],
                                        fullname = dRow["colFullName"],
                                        gender = dRow["colGender"],
                                        address = dRow["colAddress"],
                                        email = dRow["colEmail"],
                                        phone = dRow["colPhone"],
                                        level = dRow["colLevel"],
                                        dob = dRow["colBirthday"],
                                        typeid = dRow["colTypeID"],
                                        policy = dRow["colIBPolicy"],
                                        pwdreset = dRow["colpwdreset"],
                                    }).Distinct().ToList();
                //add row vafo table user infor vaf ib user
                foreach (var item in distinctUser)
                {
                    // add row in table user info
                    DataRow row2 = tblUser.NewRow();
                    row2["colUserID"] = item.username;
                    row2["colUContractNo"] = model.contractNo;
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
                    row2["colNewTypeID"] = item.typeid;
                    tblUser.Rows.Add(row2);

                    //add row in user bank table
                    DataRow row3 = tblIbankUser.NewRow();
                    row3["colUserName"] = item.username;
                    row3["colIBUserID"] = item.username;
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
                    tblIbankUser.Rows.Add(row3);

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
                DataColumn colMBPhoneNo = new DataColumn("colMBPhoneNo");
                DataColumn colMBPassU = new DataColumn("colMBPass");
                DataColumn colMBStatus = new DataColumn("colMBStatus");
                DataColumn colMBPinCode1 = new DataColumn("colMBPinCode1");
                DataColumn colMBPolicyusr = new DataColumn("colMBPolicyusr");
                DataColumn colpwdresetMB = new DataColumn("colpwdresetMB");

                DataColumn[] colMBUser = new DataColumn[] { colMBUserID, colMBPhoneNo, colMBPassU, colMBStatus, colMBPinCode1, colMBPolicyusr, colpwdresetMB };

                tblMBUser.Columns.AddRange(colMBUser);
                var distinctMBUser = (from DataRow dRow in tblSUM.Rows
                                      select new
                                      {
                                          username = dRow["colIBUserName"],
                                          phone = dRow["colMBPhone"],
                                          pass = dRow["colMBPass"],
                                          pin = dRow["colMBPinCode"],
                                          policy = dRow["colMBPolicy"],
                                          pwdreset = dRow["colpwdreset"]
                                      }).Distinct().Where(m => m.phone.ToString() != string.Empty).ToList();
                //tao 1 dong du lieu
                foreach (var item in distinctMBUser)
                {
                    DataRow row = tblMBUser.NewRow();
                    row["colMBUserID"] = item.username;
                    row["colMBPhoneNo"] = item.phone;
                    row["colMBPass"] = item.pass;
                    row["colMBStatus"] = model.status;
                    row["colMBPinCode1"] = item.pin;
                    row["colMBPolicyusr"] = item.policy;
                    row["colpwdresetMB"] = item.pwdreset;
                    tblMBUser.Rows.Add(row);
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
                var distinctPHOUser = (from DataRow dRow in tblSUM.Rows
                                       select new
                                       {
                                           username = dRow["colIBUserName"],
                                           phone = dRow["colPHOPhone"],
                                           pass = dRow["colPHOPass"],
                                           dtacct = dRow["colPHODefaultAcctno"]
                                       }).Distinct().Where(m => m.phone.ToString() != string.Empty).ToList();
                //tao 1 dong du lieu
                foreach (var item in distinctPHOUser)
                {
                    DataRow row = tblPHOUser.NewRow();
                    row["colPHOUserID"] = item.username;
                    row["colPHOPhoneNo"] = item.phone;
                    row["colPHOPass"] = item.pass;
                    row["colPHOStatus"] = model.status;
                    row["colPHODefaultAcctno1"] = item.dtacct;

                    tblPHOUser.Rows.Add(row);
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
                    if (tblIbankUserRight.Select("colIBUserNameRight='" + rIBR["colIBUserName"].ToString() + "' and colIBRoleID='" + rIBR["colRoleID"].ToString() + "'").Length == 0)
                    {
                        DataRow newRowIBR = tblIbankUserRight.NewRow();
                        newRowIBR["colIBUserNameRight"] = rIBR["colIBUserName"].ToString();
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
                        newRowSMSR["colSMSUserID"] = rSMSR["colIBUserName"].ToString();
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

                //add vào table
                tblMBUserRight.Columns.Add(colMBPhoneNoR);
                tblMBUserRight.Columns.Add(colMBRoleID);

                //tao 1 dong du lieu
                DataRow[] arrMBR = tblSUM.Select("colServiceID='" + SmartPortal.Constant.IPC.MB + "'");
                foreach (DataRow rMBR in arrMBR)
                {
                    if (tblMBUserRight.Select("colMBPhoneNo='" + rMBR["colMBPhone"].ToString() + "' and colMBRoleID='" + rMBR["colRoleID"].ToString() + "'").Length == 0)
                    {
                        DataRow newRowMBR = tblMBUserRight.NewRow();
                        newRowMBR["colMBPhoneNo"] = rMBR["colMBPhone"].ToString();
                        newRowMBR["colMBRoleID"] = rMBR["colRoleID"].ToString();

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
                    model.cusID = SmartPortal.Common.Utilities.Utility.GetID(SmartPortal.Constant.IPC.CUSNOPREFIX, model.cusCode, SmartPortal.Constant.IPC.CUSCOPORATETYPEPREFIX, 15);

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

                DataRow rowRoleIB = tblRoleDefault.NewRow();
                rowRoleIB["colRoleName"] = "Nhóm quyền sử dụng Internet Banking";
                rowRoleIB["colRoleDesc"] = "Nhóm quyền sử dụng Internet Banking";
                rowRoleIB["colUserCreated"] = model.userCreate;
                rowRoleIB["colRoleContractNo"] = model.contractNo;
                rowRoleIB["colRoleServiceID"] = SmartPortal.Constant.IPC.IB;
                rowRoleIB["colRoleProductID"] = model.productId;

                DataRow rowRoleSMS = tblRoleDefault.NewRow();
                rowRoleSMS["colRoleName"] = "Nhóm quyền sử dụng SMS Banking";
                rowRoleSMS["colRoleDesc"] = "Nhóm quyền sử dụng SMS Banking";
                rowRoleSMS["colUserCreated"] = model.userCreate;
                rowRoleSMS["colRoleContractNo"] = model.contractNo;
                rowRoleSMS["colRoleServiceID"] = SmartPortal.Constant.IPC.SMS;
                rowRoleSMS["colRoleProductID"] = model.productId;

                DataRow rowRoleMB = tblRoleDefault.NewRow();
                rowRoleMB["colRoleName"] = "Nhóm quyền sử dụng Mobile Banking";
                rowRoleMB["colRoleDesc"] = "Nhóm quyền sử dụng Mobile Banking";
                rowRoleMB["colUserCreated"] = model.userCreate;
                rowRoleMB["colRoleContractNo"] = model.contractNo;
                rowRoleMB["colRoleServiceID"] = SmartPortal.Constant.IPC.MB;
                rowRoleMB["colRoleProductID"] = model.productId;

                DataRow rowRolePHO = tblRoleDefault.NewRow();
                rowRolePHO["colRoleName"] = "Nhóm quyền sử dụng Phone Banking";
                rowRolePHO["colRoleDesc"] = "Nhóm quyền sử dụng Phone Banking";
                rowRolePHO["colUserCreated"] = model.userCreate;
                rowRolePHO["colRoleContractNo"] = model.contractNo;
                rowRolePHO["colRoleServiceID"] = SmartPortal.Constant.IPC.PHO;
                rowRolePHO["colRoleProductID"] = model.productId;

                tblRoleDefault.Rows.Add(rowRoleIB);
                tblRoleDefault.Rows.Add(rowRoleSMS);
                tblRoleDefault.Rows.Add(rowRoleMB);
                tblRoleDefault.Rows.Add(rowRolePHO);
                #endregion


                //#region vutt Tạo bảng chứa thông tin sms notify 04022016
                //string username = rbIBGenerate.Checked ? txtIBGenUserName.Text.Trim() : txtIBTypeUserName.Text.Trim();
                //DataTable dtAcctRoles = (new SmartPortal.SEMS.Transactions().DoStored("EBA_GETACCOUNTROLE_BYCONTRACT", new object[3] { model.contractNo, "SNO", username }, ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
                //DataTable tblSMSNotify = ContractControl.CreateSMSNotifyDetailTable(tblUserAccount, tblContractAccount, "U", model.contractNo, dtAcctRoles);
                ////return;
                //#endregion
                #region vutt Tạo bảng chứa thông tin sms notify 04022016

                DataTable tblSMSNotify = ContractControl.CreateSMSNotifyDetailTable(tblUserAccount, tblContractAccount, "I", model.contractNo);

                #endregion

                #region INSERT
                //if (model.isNew)
                //{
                //    new SmartPortal.SEMS.Customer().InsertCorpMaTrix(model, tblUser, tblIbankUser,
                //        tblSMSUser, tblMBUser, tblPHOUser, tblIbankUserRight, tblSMSUserRight, tblMBUserRight, tblPHOUserRight, tblUserGroup, tblContractRoleDetail,
                //        tblContractAccount, tblTranrightDetail, tblUserAccount, tblDeptDefault, tblRoleDefault, tblSMSNotify, ref IPCERRORCODE, ref IPCERRORDESC);
                //}
                //else
                //{
                //    new SmartPortal.SEMS.Contract().InsertCorpMaTrix(model, tblUser, tblIbankUser,
                //          tblSMSUser, tblMBUser, tblPHOUser, tblIbankUserRight, tblSMSUserRight, tblMBUserRight, tblPHOUserRight, tblUserGroup, tblContractRoleDetail,
                //          tblContractAccount, tblTranrightDetail, tblUserAccount, tblDeptDefault, tblRoleDefault, tblSMSNotify, ref IPCERRORCODE, ref IPCERRORDESC);

                //}
                if (IPCERRORCODE != "0")
                {
                    throw new IPCException(IPCERRORDESC);
                }
                #endregion
            }

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

            if (model.corpType.Equals(SmartPortal.Constant.IPC.CONTRACTCORPMATRIX))
            {
                #region advance

                if (ViewState["MTRUSER"] != null)
                {
                    DataTable tblUser = (DataTable)ViewState["MTRUSER"];
                    if (tblUser.Rows.Count != 0)
                    {

                        #region lay thong tin tai khoan cua nguoi dong so huu
                        DataTable ctkTable = (new SmartPortal.SEMS.Contract().GetUserByContractNo(model.contractNo, SmartPortal.Constant.IPC.MTRUSER, "", ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
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
                            stctk.Append("");
                            stctk.Append("</td>");
                            stctk.Append("<td>");
                            stctk.Append("");
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
                            DataSet accountMBDatasetctk = new SmartPortal.SEMS.User().GetUserRoleByServiceID(SmartPortal.Constant.IPC.MB, row["USERID"].ToString().Trim(), string.Empty, ref IPCERRORCODE, ref IPCERRORDESC);

                            if (IPCERRORCODE != "0")
                            {
                                goto ERROR;
                            }

                            DataTable accountMBTablectk = accountMBDatasetctk.Tables[0];
                            if (accountMBTablectk.Rows.Count != 0)
                            {
                                if (accountMBTablectk.Rows[0]["ROLEID"].ToString().Trim() != "")
                                {
                                    stctk.Append("<tr>");
                                    stctk.Append("<td colspan='4'>");
                                    stctk.Append("<br/>");
                                    stctk.Append("</td>");
                                    stctk.Append("</tr>");

                                    stctk.Append("<tr>");
                                    stctk.Append("<td colspan='4'>");
                                    stctk.Append("<B>Mobile Banking</B>");
                                    stctk.Append("</td>");
                                    stctk.Append("</tr>");

                                    stctk.Append("<tr>");
                                    stctk.Append("<td width='25%'>");
                                    stctk.Append(Resources.labels.tendangnhap + " :");
                                    stctk.Append("</td>");
                                    stctk.Append("<td width='25%'>");
                                    stctk.Append(accountMBTablectk.Rows[0]["UN"].ToString());
                                    stctk.Append("</td>");
                                    stctk.Append("<td width='25%'>");
                                    stctk.Append(Resources.labels.matkhau + " :");
                                    stctk.Append("</td>");
                                    stctk.Append("<td width='25%'>");
                                    stctk.Append("########");
                                    //st.Append("<b>" + SmartPortal.Security.Encryption.Decrypt(accountMBTable.Rows[0]["PASS"].ToString()) + "</b>");
                                    stctk.Append("</td>");
                                    stctk.Append("</tr>");
                                }

                            }

                            //lay het các tai khoan PHO cua user theo userID
                            DataSet accountPHODatasetctk = new SmartPortal.SEMS.User().GetUserRoleByServiceID(SmartPortal.Constant.IPC.PHO, row["USERID"].ToString().Trim(), string.Empty, ref IPCERRORCODE, ref IPCERRORDESC);

                            if (IPCERRORCODE != "0")
                            {
                                goto ERROR;
                            }

                            DataTable accountPHOTablectk = accountPHODatasetctk.Tables[0];
                            if (accountPHOTablectk.Rows.Count != 0)
                            {
                                if (accountPHOTablectk.Rows[0]["ROLEID"].ToString().Trim() != "")
                                {
                                    stctk.Append("<tr>");
                                    stctk.Append("<td colspan='4'>");
                                    stctk.Append("<br/>");
                                    stctk.Append("</td>");
                                    stctk.Append("</tr>");

                                    stctk.Append("<tr>");
                                    stctk.Append("<td colspan='4'>");
                                    stctk.Append("<B>Phone Banking</B>");
                                    stctk.Append("</td>");
                                    stctk.Append("</tr>");

                                    stctk.Append("<tr>");
                                    stctk.Append("<td width='25%'>");
                                    stctk.Append(Resources.labels.tendangnhap + " :");
                                    stctk.Append("</td>");
                                    stctk.Append("<td width='25%'>");
                                    stctk.Append(accountPHOTablectk.Rows[0]["UN"].ToString());
                                    stctk.Append("</td>");
                                    stctk.Append("<td width='25%'>");
                                    stctk.Append(Resources.labels.matkhau + " :");
                                    stctk.Append("</td>");
                                    stctk.Append("<td width='25%'>");
                                    stctk.Append("########");
                                    //st.Append("<b>" + SmartPortal.Security.Encryption.Decrypt(accountPHOTable.Rows[0]["PASS"].ToString()) + "</b>");
                                    stctk.Append("</td>");
                                    stctk.Append("</tr>");
                                }
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
            }

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
    void SetRoleUserEdit(TreeView[] tv, DataTable tblUser, string userid)
    {
        var lstRole = (from DataRow dRow in tblUser.Rows
                       select new
                       {
                           order = dRow["colOrder"],
                           fullname = dRow["colFullName"],
                           roleid = dRow["colRoleID"]
                       }).Distinct().Where(m => m.order.Equals(userid)).ToList();
        foreach (var tree in tv)
        {
            foreach (TreeNode liIB in tree.Nodes)
            {
                if (lstRole.Where(m => m.roleid.Equals(liIB.Value)).ToList().Count != 0)
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
            DataTable dt = (DataTable)ViewState["MTRUSER"];
            string userID = rbIBGenerate.Checked ? txtIBGenUserName.Text : txtIBTypeUserName.Text;
            if (radIBAccount.Checked)
            {
                dt.DefaultView.RowFilter = "colIBUSERNAME = '" + userID + "' and colAccount='" + ddlIBAccount.SelectedValue + "'";
            }
            else
                dt.DefaultView.RowFilter = "colIBUSERNAME = '" + userID + "'";
            if (rbIBGenerate.Checked)
            {
                txtIBGenUserName.Focus();
            }
            if (dt.DefaultView.Count > 0)
            {
                if (rbIBGenerate.Checked)
                {
                    txtIBGenUserName.Focus();
                }
                else
                {
                    txtIBTypeUserName.Focus();
                }
                lblError.Text = Resources.labels.Duplicatedusername;
                result = false;
            }
        }

        return result;
    }
    void SetControlEnable()
    {
        txtMTCuscode.Enabled = rdAdd.Checked;
        //ddlUser.Enabled = rdEdit.Checked;
        ddlIBAccount.Enabled = radIBAccount.Checked;
    }
    void SetGenerateUser(bool isGenerate, string prefix = "")
    {
        rbIBGenerate.Checked = isGenerate;
        rbIBType.Checked = !isGenerate;
        txtIBTypeUserName.Enabled = !isGenerate;
        txtIBGenUserName.Text = isGenerate ? generateUserName() : "";

        //txtIBGenUserName.Text = isGenerate ? SmartPortal.Common.Utilities.Utility.GetID(model.fullName, model.cusCode, model.licenseId, preID) : "";
    }

    protected void rbIBGenerate_CheckedChanged(object sender, EventArgs e)
    {
        if (rbIBGenerate.Checked && string.IsNullOrEmpty(txtIBGenUserName.Text))
        {
            txtIBGenUserName.Text = generateUserName();
            //txtIBGenUserName.Text = SmartPortal.Common.Utilities.Utility.GetID(model.fullName, model.cusCode, model.licenseId);
        }
    }
    string generateUserName()//string prefix = ""
    {

        string[] preID;
        string name = !string.IsNullOrEmpty(txtMTFullName.Text) && txtMTFullName.Text.Length > 1 ? txtMTFullName.Text : model.fullName;
        name = !string.IsNullOrEmpty(name) ? name : RandomString(false, 2);
        string cuscode = !string.IsNullOrEmpty(model.cusCode) ? model.cusCode : RandomString(true, 2);
        string licenseid = !string.IsNullOrEmpty(model.licenseId) ? model.licenseId : RandomString(true, 2);
        if (ViewState["MTRUSER"] != null)
        {
            DataTable dt = (DataTable)ViewState["MTRUSER"];
            preID = new string[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                preID[i] = dt.Rows[i]["colIBUserName"].ToString();
            }
            return SmartPortal.Common.Utilities.Utility.GetID(name, cuscode, licenseid, preID) + "O1";
        }
        else { return SmartPortal.Common.Utilities.Utility.GetID(name, cuscode, licenseid) + "O1"; }

        //string username = txtIBGenUserName.Text;
        ////if (string.IsNullOrEmpty(username))
        //if (!string.IsNullOrEmpty(txtMTFullName.Text.Trim()) && !string.IsNullOrEmpty(txtMTPhone.Text.Trim()))
        //{
        //    string cifno = txtMTPhone.Text.Length < 2 ? txtMTPhone.Text + "0123456789" : txtMTPhone.Text;
        //    string preid = txtMTAddress.Text + txtMTEmail.Text + "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        //    username = SmartPortal.Common.Utilities.Utility.GetID(txtMTFullName.Text, cifno, preid) + "O1";

        //}
        //else
        //{
        //    Random random = new Random();
        //    string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        //    StringBuilder result = new StringBuilder(10);
        //    if (!string.IsNullOrEmpty(prefix) || prefix.Length < 2)
        //        for (int i = 0; i < 2; i++)
        //        {
        //            var a = characters[random.Next(characters.Length)];
        //            result.Append(characters[random.Next(characters.Length)]);
        //        }
        //    else result.Append(prefix.Substring(0, 2));
        //    string number = "0123456789";
        //    for (int i = 0; i < 6; i++)
        //    {
        //        result.Append(number[random.Next(number.Length)]);
        //    }
        //    username = result.ToString() + "O1";
        //}
        //return username;
    }
    private string RandomString(bool isNumber, int lenght)
    {
        Random random = new Random();
        StringBuilder result = new StringBuilder(lenght);
        if (!isNumber)
        {
            string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            for (int i = 0; i < 2; i++)
            {
                var a = characters[random.Next(characters.Length)];
                result.Append(characters[random.Next(characters.Length)]);
            }
        }
        else
        {
            string number = "0123456789";
            for (int i = 0; i < 6; i++)
            {
                result.Append(number[random.Next(number.Length)]);
            }
        }
        return result.ToString();
    }
}



