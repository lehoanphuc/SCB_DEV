using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using SmartPortal.Constant;
using SmartPortal.ExceptionCollection;
using SmartPortal.Security;
using System.Linq;

public partial class Widgets_SEMSResetPassByUser_Detail_Widget : WidgetBase
{
    string IPCERRORCODE = "";
    string IPCERRORCODEINFO = "";
    string IPCERRORDESC = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                BindData();
            }
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
            #region Lấy thông tin
            string ID = GetParamsPage(IPC.ID)[0].Trim();
            DataSet ds = (new SmartPortal.SEMS.User().GetDetailResetPassByUser(ID, ref IPCERRORCODE, ref IPCERRORDESC));
            if (IPCERRORCODE.Equals("0"))
            {
                DataTable dt = ds.Tables[0];
                txtUserID.Text = dt.Rows[0]["UserID"] is DBNull ? "" : dt.Rows[0]["UserID"].ToString();
                txtName.Text = dt.Rows[0]["Name"] is DBNull ? "" : dt.Rows[0]["Name"].ToString();
                txtService.Text = dt.Rows[0]["SourceID"] is DBNull ? "" : dt.Rows[0]["SourceID"].ToString();
                txtPhone.Text = dt.Rows[0]["PhoneNumber"] is DBNull ? "" : dt.Rows[0]["PhoneNumber"].ToString();
                txtEmail.Text = dt.Rows[0]["Email"] is DBNull ? "" : dt.Rows[0]["Email"].ToString();
                txtNRIC.Text = dt.Rows[0]["NRIC"] is DBNull ? "" : dt.Rows[0]["NRIC"].ToString();
                txtDOB.Text = dt.Rows[0]["DOB"] is DBNull ? "" : dt.Rows[0]["DOB"].ToString();
                txtDateCreated.Text = dt.Rows[0]["DateSend"] is DBNull ? "" : dt.Rows[0]["DateSend"].ToString();
                txtActNo.Text = dt.Rows[0]["ActNo"] is DBNull ? "" : dt.Rows[0]["ActNo"].ToString();
                lbltyperes.Text = dt.Rows[0]["ResetType"] is DBNull ? "" : dt.Rows[0]["ResetType"].ToString();
              

                btnApprove.Visible = false;
                btnReject.Visible = false;
                switch (dt.Rows[0]["Status"].ToString().Trim())
                {
                    case SmartPortal.Constant.IPC.NEW:
                        txtStatus.Text = Resources.labels.connew;
                        btnApprove.Visible = true;
                        btnReject.Visible = true;
                        break;
                    case SmartPortal.Constant.IPC.DELETE:
                        txtStatus.Text = Resources.labels.condelete;

                        break;
                    case SmartPortal.Constant.IPC.ACTIVE:
                        txtStatus.Text = Resources.labels.conactive;
                        break;
                    case SmartPortal.Constant.IPC.BLOCK:
                        txtStatus.Text = Resources.labels.conblock;

                        break;
                    case SmartPortal.Constant.IPC.REJECT:
                        txtStatus.Text = Resources.labels.conreject;

                        break;
                }
                if (!CheckPermitPageAction(IPC.ACTIONPAGE.APPROVE))
                {
                    btnApprove.Enabled = false;
                    btnApprove.Visible = false;
                }
                if (!CheckPermitPageAction(IPC.ACTIONPAGE.REJECT))
                {
                    btnReject.Enabled = false;
                    btnReject.Visible = false;
                }
                ds = (new SmartPortal.SEMS.User().GetImageResetPassByUser(ID, ref IPCERRORCODE, ref IPCERRORDESC));
                if (IPCERRORCODE.Equals("0"))
                {
                    rptImage.DataSource = ds.Tables[0];
                    rptImage.DataBind();
                }

                ds = (new SmartPortal.SEMS.User().GetFullUserByUID(txtUserID.Text, ref IPCERRORCODE, ref IPCERRORDESC));
                dt = ds.Tables[0];
                txtUserID_sys.Text = dt.Rows[0]["UserID"] is DBNull ? "" : dt.Rows[0]["UserID"].ToString();
                txtName_sys.Text = dt.Rows[0]["FullName"] is DBNull ? "" : dt.Rows[0]["FullName"].ToString();
                txtService_sys.Text = txtService.Text;
                txtPhone_sys.Text = dt.Rows[0]["Phone"] is DBNull ? "" : dt.Rows[0]["Phone"].ToString();
                txtEmail_sys.Text = dt.Rows[0]["Email"] is DBNull ? "" : dt.Rows[0]["Email"].ToString();
                txtDOB_sys.Text = dt.Rows[0]["Birthday"] is DBNull ? "" : ((DateTime)dt.Rows[0]["Birthday"]).ToString("dd/MM/yyyy");
                txtDateCreated_sys.Text = dt.Rows[0]["DateCreate"] is DBNull ? "" : ((DateTime)dt.Rows[0]["DateCreate"]).ToString("dd/MM/yyyy");
                txtNRIC_sys.Text = dt.Rows[0]["LICENSEID"] is DBNull ? "" : dt.Rows[0]["LICENSEID"].ToString();
                switch (dt.Rows[0]["Status"].ToString().Trim())
                {
                    case SmartPortal.Constant.IPC.NEW:
                        txtStatus_sys.Text = Resources.labels.connew;
                        break;
                    case SmartPortal.Constant.IPC.DELETE:
                        txtStatus_sys.Text = Resources.labels.condelete;
                        break;
                    case SmartPortal.Constant.IPC.ACTIVE:
                        txtStatus_sys.Text = Resources.labels.conactive;
                        break;
                    case SmartPortal.Constant.IPC.BLOCK:
                        txtStatus_sys.Text = Resources.labels.conblock;
                        break;
                    case SmartPortal.Constant.IPC.REJECT:
                        txtStatus_sys.Text = Resources.labels.conreject;
                        break;
                }
                ddlSendinfo.Items.Add(new ListItem(Resources.labels.sendcontractinforsms, SmartPortal.Constant.IPC.SMS));
                ddlSendinfo.Items.Add(new ListItem(Resources.labels.sendcontractinforemail, SmartPortal.Constant.IPC.EMAIL));
                DataTable tblSMS = new SmartPortal.IB.User().GetAccountDetail(txtUserID.Text, SmartPortal.Constant.IPC.SMS);
                if (tblSMS.Rows.Count != 0)
                {
                    if (tblSMS.Rows[0]["Status"].ToString().Trim() != SmartPortal.Constant.IPC.DELETE && tblSMS.Rows[0]["Status"].ToString().Trim() != SmartPortal.Constant.IPC.PENDINGFORDELETE)
                    {
                        ddlSendinfo.Items.Add(new ListItem(Resources.labels.sendcontractinforsms, SmartPortal.Constant.IPC.SMS));
                    }
                }
                ds = new SmartPortal.IB.Customer().GetAllAcctno("", dt.Rows[0]["ContractNo"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE != "0")
                {
                    throw new IPCException("13");
                }
                DataTable dtAccount = new DataTable();
                dtAccount = ds.Tables[0];
                string ActNo = string.Empty;
                foreach (DataRow row in dtAccount.Rows)
                {
                    if (row["ACCOUNTNO"].ToString().EndsWith(txtActNo.Text))
                    {
                        ActNo = row["ACCOUNTNO"].ToString();
                        break;
                    }
                }

                if (txtService.Text.ToUpper() != txtService_sys.Text.ToUpper())
                {
                    txtService.ForeColor = System.Drawing.Color.Blue;
                }
                if (txtUserID.Text.ToUpper() != txtUserID_sys.Text.ToUpper())
                {
                    txtUserID.ForeColor = System.Drawing.Color.Red;
                }
                if (txtName.Text.ToUpper() != txtName_sys.Text.ToUpper())
                {
                    txtName.ForeColor = System.Drawing.Color.Red;
                }
                if (txtNRIC.Text.ToUpper() != txtNRIC_sys.Text.ToUpper())
                {
                    txtNRIC.ForeColor = System.Drawing.Color.Red;
                }
                if (txtDOB.Text.ToUpper() != txtDOB_sys.Text.ToUpper())
                {
                    txtDOB.ForeColor = System.Drawing.Color.Red;
                }
                if (txtEmail.Text.ToUpper() != txtEmail_sys.Text.ToUpper())
                {
                    txtEmail.ForeColor = System.Drawing.Color.Red;
                }
                if (txtPhone.Text.ToUpper() != txtPhone_sys.Text.ToUpper())
                {
                    txtPhone.ForeColor = System.Drawing.Color.Red;
                }

                if (!string.IsNullOrEmpty(ActNo))
                {
                    lblAlertAct.Text = string.Format(Resources.labels.userhaveaccountandwith7digits, txtUserID.Text, ActNo, txtActNo.Text);
                    lblAlertAct.ForeColor = System.Drawing.Color.Blue;
                }
                else
                {
                    lblAlertAct.Text = string.Format(Resources.labels.userhavenotaccountandwith7digits, txtUserID.Text, txtActNo.Text);
                    lblAlertAct.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                throw new IPCException(IPCERRORDESC);
            }

            #endregion
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void rptImage_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            string base64 = (string)((DataRowView)e.Item.DataItem).Row.ItemArray[2];

            Image img = e.Item.FindControl("img") as Image;
            img.ImageUrl = "data:image/jpeg;base64," + base64;
            if (e.Item.ItemIndex % 2 == 0)
            {
                img.CssClass = "img even";
            }
            else
            {
                img.CssClass = "img odd";
            }
        }
    }
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        string ID = GetParamsPage(IPC.ID)[0].Trim();
        DataSet ds = (new SmartPortal.SEMS.User().GetDetailResetPassByUser(ID, ref IPCERRORCODE, ref IPCERRORDESC));
        DataSet dsinfo = (new SmartPortal.SEMS.User().GetFullUserByUID(txtUserID.Text, ref IPCERRORCODEINFO, ref IPCERRORDESC));

        if (IPCERRORCODE.Equals("0") && IPCERRORCODEINFO.Equals("0") && CheckPermitPageAction(IPC.ACTIONPAGE.APPROVE))
        {
            DataTable dtReset = ds.Tables[0];
            DataTable dtResetinfo = dsinfo.Tables[0];
            try
            {
                DataTable resetdt = new DataTable();
                DataTable resetdt2service = new DataTable();
                string email = dtResetinfo.Rows[0]["Email"].ToString();
                string fullname = "";
                string typeid = "";
                string username = string.Empty;
                string passreveal = string.Empty;
                string passoldenc = string.Empty;
                string Pass = string.Empty;
                string authenType = dtReset.Rows[0]["ResetType"].ToString();
                string userid = dtResetinfo.Rows[0]["UserID"].ToString();
                string phonenumber = dtResetinfo.Rows[0]["Phone"] is DBNull
                    ? string.Empty
                    : dtResetinfo.Rows[0]["Phone"].ToString();
                string serviceid = dtReset.Rows[0]["SourceID"].ToString();

                username = dtResetinfo.Rows[0]["USERNAME"].Equals(string.Empty) ? phonenumber: dtResetinfo.Rows[0]["USERNAME"].ToString();
                //string Pass = Encryption.Encrypt(DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - 6, 6).ToString());
                int passlen = 0;
                txtusername.Value = userid;
                string servicename = serviceid;
                //21.3.2016 load infor about policy selected to get len of pass generate
                DataSet dspolicy = new DataSet();
                // validate send mail or sms BY dtResetinfo
                switch (ddlSendinfo.SelectedValue.Trim())
                {
                    case SmartPortal.Constant.IPC.EMAIL:
                        if (email == "" || email == null)
                        {
                            lblError.Text = "Email information is not available";
                            return;
                        }
                        break;
                    case SmartPortal.Constant.IPC.SMS:
                        if (String.IsNullOrEmpty(phonenumber))
                        {
                            lblError.Text = "Phone number information is not available";
                            return;
                        }
                        break;
                }
                dspolicy = new SmartPortal.SEMS.USERPOLICY().GetPolicybyConditionforreset(string.Empty, string.Empty, string.Empty, string.Empty, serviceid, userid, ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE == "0")
                {
                    DataTable dt = dspolicy.Tables[0];
                    passlen = Convert.ToInt32(dt.Rows[0]["minpwdlen"].ToString());
                }
                int pincodeLenght = int.Parse(ConfigurationManager.AppSettings["PINCODELENGHT"].ToString());
                //Check Type Login User By Password
                switch (authenType)
                {
                    case "PINCODE":
                        passreveal = DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - pincodeLenght, pincodeLenght).ToString();
                        passoldenc = Encryption.Encrypt(passreveal);
                        Pass = SmartPortal.SEMS.O9Encryptpass.sha_sha256(passreveal, userid.Trim());
                        break;
                    default:
                        passreveal = DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - passlen, passlen).ToString();
                        passoldenc = Encryption.Encrypt(passreveal);
                        Pass = SmartPortal.SEMS.O9Encryptpass.sha_sha256(passreveal, userid.Trim());
                        break;
                }
                resetdt = (new SmartPortal.SEMS.User().ResetPassword(serviceid, userid, Pass, passoldenc, authenType, ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
                if (resetdt.Rows.Count != 0 || resetdt2service.Rows.Count != 0)
                {
                    email = dtResetinfo.Rows[0]["EMAIL"].ToString();
                    fullname = dtResetinfo.Rows[0]["FULLNAME"].ToString();
                    typeid = resetdt.Rows[0]["TYPEID"].ToString();
                    (new SmartPortal.SEMS.User()).UpdateResetPassword(ID, Session["UserID"].ToString(), IPC.ACTIVE, ref IPCERRORCODE, ref IPCERRORDESC);
                    SendInfoLogin(phonenumber, username, email, Encryption.Decrypt(passoldenc), servicename, fullname, typeid, authenType);
                    string contractNo = dtResetinfo.Rows[0]["CONTRACTNO"].ToString();
                    SmartPortal.Common.Log.WriteLogDatabase(contractNo, phonenumber, Request.Url.ToString(), Session["userName"].ToString(),
                        Request.UserHostAddress, "EBA_CONTRACTACCOUNT", "Change Password/Pin by User", "", "","A", Session["UserID"].ToString());
                    btnApprove.Visible = false;
                    btnReject.Visible = false;
                }
                else
                {
                    if (authenType == SmartPortal.Constant.IPC.PASSWORD)
                    {
                        lblError.Text = Resources.labels.usernamedoesnothavepass;
                        SmartPortal.Common.Log.WriteLogFile("", "", "", "SEMSUSERRESETPASS FAILED,Password  ---" + userid);
                        return;
                    }
                    if (authenType == "PINCODE")
                    {
                        lblError.Text = Resources.labels.usernamedoesnothavepin;
                        SmartPortal.Common.Log.WriteLogFile("", "", "", "SEMSUSERRESETPASS FAILED,Pincode  ---" + userid);
                        return;
                    }
                    lblError.Text = Resources.labels.tendangnhaphoacemailkhongtontaitronghethong;
                    SmartPortal.Common.Log.WriteLogFile("", "", "", "SEMSUSERRESETPASS FAILED,USERID NOT EXISTS ---" + userid);
                    return;
                }
            }
            catch (Exception ex)
            {
                SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
                SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
            }
        }
    }
    protected void btnReject_Click(object sender, EventArgs e)
    {
        try
        {
            if (CheckPermitPageAction(IPC.ACTIONPAGE.REJECT))
            {
                SendRejectResetPassword();
          
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    void SendRejectResetPassword()
    {
        string ID = GetParamsPage(IPC.ID)[0].Trim();
        DataSet ds = (new SmartPortal.SEMS.User().GetDetailResetPassByUser(ID, ref IPCERRORCODE, ref IPCERRORDESC));
        DataSet dsinfo = (new SmartPortal.SEMS.User().GetFullUserByUID(txtUserID.Text, ref IPCERRORCODEINFO, ref IPCERRORDESC));
        if (IPCERRORCODE.Equals("0") && IPCERRORCODEINFO.Equals("0"))
        {
            DataTable dtReset = ds.Tables[0];
            DataTable dtResetInfo = dsinfo.Tables[0];
            Antlr3.ST.StringTemplate tmpl = new Antlr3.ST.StringTemplate();
    
            tmpl = SmartPortal.Common.ST.GetStringTemplate("SEMSResetPasswords",
                "RejectResetPasswords" + System.Globalization.CultureInfo.CurrentCulture.ToString());
            switch (dtReset.Rows[0]["ResetType"].ToString())
            {
                case "PINCODE":
                    tmpl = SmartPortal.Common.ST.GetStringTemplate("SEMSResetPasswords", "RejectResetPincodes" + System.Globalization.CultureInfo.CurrentCulture.ToString());
                    break;
                default:
                    tmpl = SmartPortal.Common.ST.GetStringTemplate("SEMSResetPasswords", "RejectResetPasswords" + System.Globalization.CultureInfo.CurrentCulture.ToString());
                    break;
            }
            try
            {
                string userid = dtReset.Rows[0]["UserID"].ToString();
                string phonenumber = dtResetInfo.Rows[0]["Phone"] is DBNull
                    ? userid
                    : dtResetInfo.Rows[0]["Phone"].ToString();
                string email = dtResetInfo.Rows[0]["Email"] is DBNull ? "" : dtResetInfo.Rows[0]["Email"].ToString();
                string username = string.Empty;
                username = dtResetInfo.Rows[0]["USERNAME"].Equals(string.Empty)? phonenumber : dtResetInfo.Rows[0]["USERNAME"].ToString();

                tmpl.Reset();
                tmpl.SetAttribute("USERNAME", username.Trim());

                string SENDINFO = "";
                string errorcode = "";
                string errorDesc = "";
                SENDINFO = "Siam Commercial Bank operator declined due to missing or inaccurate account information";
                switch (ddlSendinfo.SelectedValue.Trim())
                {
                    case SmartPortal.Constant.IPC.EMAIL:
                        if (email == "")
                        {
                            lblError.Text = "Email information is not available";
                            return;
                        }
                        break;
                    case SmartPortal.Constant.IPC.SMS:
                        if (String.IsNullOrEmpty(phonenumber))
                        {
                            lblError.Text = "Phone number information is not available";
                            return;
                        }
                        break;
                }
                (new SmartPortal.SEMS.User()).UpdateResetPassword(ID, Session["UserID"].ToString(), IPC.REJECT, ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE.Equals("0"))
                {
                    switch (ddlSendinfo.SelectedValue.Trim())
                    {
                        case SmartPortal.Constant.IPC.EMAIL:
                            SmartPortal.Common.EmailHelper.SendMailMessageAsync(System.Configuration.ConfigurationManager.AppSettings["contractapprovemailfrom"], email, System.Configuration.ConfigurationManager.AppSettings["contractapprovemailtitle"], tmpl.ToString());
                            tmpl.RemoveAttribute("PASS");
                            lblError.Text = Resources.labels.huythanhcong;
                            SmartPortal.Common.Log.WriteLogFile("", "", "", "SEMSUSERRESETPASS REJECT, SENT EMAIL TO " + email + "-USERNAME:" + txtusername.Value);
                            break;
                        case SmartPortal.Constant.IPC.SMS:
                            //send sms
                            SmartPortal.SEMS.Contract.sendSMS_RESETPASS(phonenumber, SENDINFO, ref errorcode, ref errorDesc);
                            lblError.Text = Resources.labels.huythanhcong;
                            SmartPortal.Common.Log.WriteLogFile("", "", "", "SEMSUSERRESETPASS REJECT,SENT SMS TO " + phonenumber + "-USERNAME:" + txtusername.Value);
                            break;
                    }
                    btnReject.Visible = false;
                    btnApprove.Visible = false;
                }
                else
                {
                    lblError.Text = "Reject Failed";
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }

    void SendInfoLogin(string phonenumber,string username, string email, string pass, string service, string fullname, string typeID, string authenType)
    {
        string ID = GetParamsPage(IPC.ID)[0].Trim();
        DataSet ds = (new SmartPortal.SEMS.User().GetDetailResetPassByUser(ID, ref IPCERRORCODE, ref IPCERRORDESC));
        if (IPCERRORCODE.Equals("0"))
        {
            DataTable dtReset = ds.Tables[0];
            Antlr3.ST.StringTemplate tmpl = new Antlr3.ST.StringTemplate();
            try
            {
                switch (authenType)
                {
                    case "PINCODE":
                        tmpl = SmartPortal.Common.ST.GetStringTemplate("SEMSResetPasswords", "ResetPincodes" + System.Globalization.CultureInfo.CurrentCulture.ToString());
                        break;
                    default:
                        tmpl = SmartPortal.Common.ST.GetStringTemplate("SEMSResetPasswords", "ResetPasswords" + System.Globalization.CultureInfo.CurrentCulture.ToString());
                        break;
                }
                tmpl.Reset();
                StringBuilder st = new StringBuilder();
                st.Append(pass);
                tmpl.SetAttribute("USER", username.Trim());
                tmpl.SetAttribute("PASS", st.ToString());
                tmpl.SetAttribute("SERVICE", service);
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
                //minh add 11/8/2015
                string SENDINFO = string.Empty;
                string errorcode = string.Empty;
                string errorDesc = string.Empty;
                string msgsendemail = string.Empty;
                string msgsendsms = string.Empty;
                switch (authenType)
                {
                    case "PINCODE":
                        SENDINFO = service + " " + Resources.labels.newpinis + " " + pass;
                        msgsendemail = Resources.labels.pincodeduocguiquaemailcuakhachhang; 
                        msgsendsms = Resources.labels.pincodeduocguiquaphonesmscuakhachhang;
                        break; 
                    default:
                        SENDINFO = service + " " + Resources.labels.newpassis + " " + pass;
                        msgsendemail = Resources.labels.matkhaumoiduocguiquaemailcuakhachhang;
                        msgsendsms = Resources.labels.matkhaumoiduocguiquaphonesmscuakhachhang;
                        break;
                }
                switch (ddlSendinfo.SelectedValue.Trim())
                {
                    case SmartPortal.Constant.IPC.EMAIL:
                        //send mail
                        SmartPortal.Common.EmailHelper.SendMailMessageAsync(System.Configuration.ConfigurationManager.AppSettings["contractapprovemailfrom"], email, System.Configuration.ConfigurationManager.AppSettings["contractapprovemailtitle"], tmpl.ToString());
                        tmpl.RemoveAttribute("PASS");
                        lblError.Text = msgsendemail;
                        SmartPortal.Common.Log.WriteLogFile("", "", "", "SEMSUSERRESETPASS SUCCESS, SENT EMAIL TO " + email + "-USERNAME:" + txtusername.Value);
                        break;
                    case SmartPortal.Constant.IPC.SMS:
                        //check sms registered
                        //send sms
                        SmartPortal.SEMS.Contract.sendSMS_RESETPASS(phonenumber, SENDINFO, ref errorcode, ref errorDesc);
                        lblError.Text = msgsendsms;
                        SmartPortal.Common.Log.WriteLogFile("", "", "", "SEMSUSERRESETPASS SUCCESS,SENT SMS TO " + phonenumber + "-USERNAME:" + txtusername.Value);
                        break;
                }

      
            }
            catch (Exception ex)
            {
                SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
                SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
            }
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        RedirectBackToMainPage();
    }
}
