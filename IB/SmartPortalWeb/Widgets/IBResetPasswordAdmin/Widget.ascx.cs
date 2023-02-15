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
using System.Text;
using System.Drawing;
using System.Linq;
using SmartPortal.ExceptionCollection;
public partial class Widgets_SEMSResetPasswords_Widget : WidgetBase
{
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = "";
            ResCheck.Visible = false;
            if (!IsPostBack)
            {
                DataSet dsdep = new SmartPortal.SEMS.Dept().GetAllByUID(Session["userID"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE=="0")
                {
                    DataTable deptTable = new DataTable();
                    deptTable = dsdep.Tables[0];


                    dllDept.DataSource = deptTable;
                    dllDept.DataTextField = "DEPTNAME";
                    dllDept.DataValueField = "DEPTID";
                    dllDept.DataBind();
           
                }
                string contractNo = "";
                DataTable userTableTemp = (new SmartPortal.SEMS.User().GetFullUserByUID(Session["userID"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
                if (userTableTemp.Rows.Count != 0)
                {
                    contractNo = userTableTemp.Rows[0]["CONTRACTNO"].ToString();
                }

                DataTable userTable = new DataTable();
                DataSet dsUser = new SmartPortal.SEMS.User().GetUserByDeptID(dllDept.SelectedValue, contractNo, ref IPCERRORCODE, ref IPCERRORDESC);

                if (IPCERRORCODE != "0")
                {
                    throw new SmartPortal.ExceptionCollection.IPCException(IPCERRORCODE);
                }
                else
                {
                    ddluser.DataSource = dsUser;
                    ddluser.DataTextField = "TXTNAME";
                    ddluser.DataValueField = "UserName";
                    ddluser.DataBind();
                }
                btsave.Visible = false;
                btsave.Enabled = false;
                dAuthenType.Visible = false;
                dSeninfo.Visible = false;

                ddlsendinfo.Items.Clear();
                ddlsendinfo.Items.Add(new ListItem(Resources.labels.sendcontractinforemail, SmartPortal.Constant.IPC.EMAIL));
                ddlsendinfo.Items.Add(new ListItem(Resources.labels.sendcontractinforsms, SmartPortal.Constant.IPC.SMS));

                ddlauthentype.Items.Clear();
                ddlauthentype.Items.Add(new ListItem(Resources.labels.matkhau, SmartPortal.Constant.IPC.PASSWORD));
                ddlauthentype.Items.Add(new ListItem(Resources.labels.pincode, "PINCODE"));

                ddlloginmethod.Items.Clear();
                ddlloginmethod.Items.Add(new ListItem(Resources.labels.username, SmartPortal.Constant.IPC.USERNAME));
                ddlloginmethod.Items.Add(new ListItem(Resources.labels.phone, SmartPortal.Constant.IPC.PHONENO));


                #region load service
                DataSet ds = new DataSet();
                ds = new SmartPortal.SEMS.Services().GetAll(SmartPortal.Constant.IPC.ACTIVE, ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE == "0")
                {
                    DataTable dtService = new DataTable();
                    dtService = ds.Tables[0];

                    ddlService.DataSource = dtService;
                    ddlService.DataTextField = "SERVICENAME";
                    ddlService.DataValueField = "SERVICEID";
                    ddlService.DataBind();
                    ddlService.Items.Remove(ddlService.Items.FindByValue(SmartPortal.Constant.IPC.SEMS));
                    ddlService.Items.Remove(ddlService.Items.FindByValue(SmartPortal.Constant.IPC.SMS));
                    ddlService.Items.Remove(ddlService.Items.FindByValue(SmartPortal.Constant.IPC.AM));
                    ddlService.Items.Remove(ddlService.Items.FindByValue(SmartPortal.Constant.IPC.MB));
                    ddlService.Enabled = false;
                }
                else
                {
                    throw new IPCException(IPCERRORCODE);
                }
                #endregion
            }
        }

        catch (IPCException IPCex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], "Widgets_SEMSResetPasswords_Widget", "Page_Load", IPCex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSResetPasswords_Widget", "Page_Load", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    protected void btsaveandcont_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable resetdt = new DataTable();
            DataTable resetdt2service = new DataTable();
            string ERRORCODE = string.Empty;
            string ERRORDESC = string.Empty;
            string email = "";
            string fullname = "";
            string typeid = "";
            string username = string.Empty;
            string phoneno = string.Empty;
            string authenType = string.Empty;
            string passreveal = string.Empty;
            string passoldenc = string.Empty;
            string Pass = string.Empty;
            int passlen = 0;
            //Hunglt Check exist userid by Phone MB

           btcheckUsSend_OnClick();
            if (ddlService.SelectedValue.Equals(SmartPortal.Constant.IPC.MB) || ddlService.SelectedValue.Equals(SmartPortal.Constant.IPC.IB))
            {
                DataSet DTMBUser = new DataSet();
                DataSet DTUSERINFO = new DataSet();

                DTMBUser = new SmartPortal.SEMS.User().GETDetailsUser(ddluser.SelectedValue, ddlService.SelectedValue.ToString(), ddlloginmethod.SelectedValue.ToString(), ddlauthentype.SelectedValue.ToString(), ref ERRORCODE, ref ERRORDESC);
                if (ERRORCODE == "0")
                {
                    if (DTMBUser.Tables[0].Rows.Count == 0)
                    {
                        lblError.Text = Resources.labels.phoneanduserinvalid;
                        return;
                    }
                    else if (DTMBUser.Tables[0].Rows[0]["STATUS"].ToString() == SmartPortal.Constant.IPC.ACTIVE || DTMBUser.Tables[0].Rows[0]["STATUS"].ToString() == SmartPortal.Constant.IPC.PENDINGFORACTIVE)
                    {
                        if (ddlsendinfo.SelectedValue.ToString() == SmartPortal.Constant.IPC.EMAIL && String.IsNullOrEmpty(DTMBUser.Tables[0].Rows[0]["Email"].ToString()))
                        {
                            lblError.Text = Resources.labels.taikhoanchuadangkyemail;
                            return;
                        }

                        bool flag = false;
                        for (int i = 0; i < DTMBUser.Tables[0].Rows.Count; i++)
                        {
                            flag = ddlauthentype.SelectedValue.Equals(DTMBUser.Tables[0].Rows[i]["type"].ToString()) ? true : false;
                            if (flag)
                            {
                                break;
                            }
                        }
                        if (!flag)
                        {
                            lblError.Text = "User doesn't login  with " + ddlauthentype.SelectedValue.ToString();
                            return;
                        }

                        username = DTMBUser.Tables[0].Rows[0]["USERID"].ToString();
                        authenType = DTMBUser.Tables[0].Rows[0]["AUTHENTYPE"].ToString();
                        if (ddlService.SelectedValue.Equals(SmartPortal.Constant.IPC.IB))
                        {
                            phoneno = DTMBUser.Tables[0].Rows[0]["phone"].ToString();
                        }
                        else
                        {
                            phoneno = DTMBUser.Tables[0].Rows[0]["PHONENO"].ToString();

                        }
                        email = DTMBUser.Tables[0].Rows[0]["Email"].ToString();
                    }
                    else
                    {
                        lblError.Text = Resources.labels.accnotactive;
                        return;
                    }
                }
                else
                {
                    lblError.Text = ERRORDESC;
                }
            }
            // check exist userid by phone AM_USER 
            if (ddlService.SelectedValue.Equals(SmartPortal.Constant.IPC.AM))
            {
                DataSet dtAMUser = new DataSet();
                dtAMUser = new SmartPortal.SEMS.User().GETDetailsUser(ddluser.SelectedValue, ddlService.SelectedValue.ToString(), ddlloginmethod.SelectedValue.ToString(), ddlauthentype.SelectedValue.ToString(), ref ERRORCODE, ref ERRORDESC);
                if (ERRORCODE == "0")
                {
                    if (dtAMUser.Tables[0].Rows.Count == 0)
                    {
                        lblError.Text = Resources.labels.phoneanduserinvalid;
                        return;
                    }
                    else if (dtAMUser.Tables[0].Rows[0]["STATUS"].ToString() == SmartPortal.Constant.IPC.ACTIVE || dtAMUser.Tables[0].Rows[0]["STATUS"].ToString() == SmartPortal.Constant.IPC.PENDINGFORACTIVE)
                    {
                        if (ddlsendinfo.SelectedValue.ToString() == SmartPortal.Constant.IPC.EMAIL && String.IsNullOrEmpty(dtAMUser.Tables[0].Rows[0]["Email"].ToString()))
                        {
                            lblError.Text = Resources.labels.taikhoanchuadangkyemail;
                            return;
                        }

                        bool flag = false;
                        for (int i = 0; i < dtAMUser.Tables[0].Rows.Count; i++)
                        {
                            flag = ddlauthentype.SelectedValue.Equals(dtAMUser.Tables[0].Rows[i]["type"].ToString()) ? true : false;
                            if (flag)
                            {
                                break;
                            }
                        }
                        if (!flag)
                        {
                            lblError.Text = "User doesn't login with " + ddlauthentype.SelectedValue.ToString();
                            return;
                        }
                        username = dtAMUser.Tables[0].Rows[0]["USERID"].ToString();
                        authenType = dtAMUser.Tables[0].Rows[0]["AUTHENTYPE"].ToString();
                        phoneno = dtAMUser.Tables[0].Rows[0]["PHONENO"].ToString();
                        email = dtAMUser.Tables[0].Rows[0]["Email"].ToString();
                    }
                    else
                    {
                        lblError.Text = Resources.labels.accnotactive;
                        return;
                    }
                }
                else
                {
                    lblError.Text = ERRORDESC;
                }
            }
            checkSendMethod(ddlsendinfo.SelectedValue.Trim(), email, phoneno);
            DataSet dspolicy = new DataSet();
            dspolicy = new SmartPortal.SEMS.USERPOLICY().GetPolicybyConditionforreset(string.Empty, string.Empty, string.Empty, string.Empty, ddlService.SelectedValue.ToString(), username, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                if (dspolicy.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = dspolicy.Tables[0];
                    passlen = Convert.ToInt32(dt.Rows[0]["minpwdlen"].ToString());
                }
                else
                {
                    lblError.Text = Resources.labels.phoneanduserinvalid;
                    return;
                }
            }

            int pincodeLenght = !string.IsNullOrEmpty(ConfigurationManager.AppSettings["PINCODELENGHT"]) ? int.Parse(ConfigurationManager.AppSettings["PINCODELENGHT"].ToString()) : 6;
            //int pincodeLenght = ConfigurationManager.AppSettings["PINCODELENGHT"].ToString() !=  int.Parse(ConfigurationManager.AppSettings["PINCODELENGHT"].ToString());
            //Check Type Login User By Password
            switch (ddlauthentype.SelectedValue.ToString())
            {
                case "PINCODE":
                    passreveal = DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - pincodeLenght, pincodeLenght).ToString();
                    passoldenc = Encryption.Encrypt(passreveal);
                    Pass = SmartPortal.SEMS.O9Encryptpass.sha_sha256(passreveal, username.Trim());
                    break;
                default:
                    passreveal = DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - passlen, passlen).ToString();
                    passoldenc = Encryption.Encrypt(passreveal);
                    Pass = SmartPortal.SEMS.O9Encryptpass.sha_sha256(passreveal, username.Trim());
                    break;
            }
            resetdt = (new SmartPortal.SEMS.User().ResetPassword(ddlService.SelectedValue.ToString(), username, Pass, passoldenc, ddlauthentype.SelectedValue.ToString(), ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
            if (resetdt.Rows.Count != 0 && IPCERRORCODE == "0")
            {
                //get contractno
                DataTable dtuser = (new SmartPortal.SEMS.User().GetFullUserByUID(username, ref IPCERRORCODE, ref IPCERRORDESC)).Tables[0];
                string contractNo = dtuser.Rows[0]["CONTRACTNO"].ToString();
                //write logDB
                SmartPortal.Common.Log.WriteLogDatabase(contractNo, username, Request.Url.ToString(), Session["userName"].ToString(),
                Request.UserHostAddress, "EBA_CONTRACTACCOUNT", "Change Password/Pin", "", "");
                string userID = username.Trim();
                email = resetdt.Rows[0]["EMAIL"].ToString();
                fullname = resetdt.Rows[0]["FULLNAME"].ToString();
                typeid = resetdt.Rows[0]["TYPEID"].ToString();
                SendInfoLogin(ddluser.SelectedValue, phoneno, email, Encryption.Decrypt(passoldenc), ddlService.SelectedItem.Text, fullname, typeid, ddlauthentype.SelectedValue.ToString());
            }
            else
            {
                lblError.Text = Resources.labels.tendangnhaphoacemailkhongtontaitronghethong;
                SmartPortal.Common.Log.WriteLogFile("", "", "", "SEMSUSERRESETPASS FAILED,USERID NOT EXISTS ---" + ddluser.SelectedValue);
                return;
            }
            ////goto EXIT;
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSResetPasswords_Widget", "btsaveandcont_Click", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    //hunglt truyen them userid de lay phone
    void SendInfoLogin(string userid, string PhoneNo, string email, string pass, string service, string fullname, string typeID, string authenType)
    {
        try
        {
            Antlr3.ST.StringTemplate tmpl = new Antlr3.ST.StringTemplate();
            switch (authenType)
            {
                case "PINCODE":
                    tmpl = SmartPortal.Common.ST.GetStringTemplate("IBResetPasswordAdmin", "ResetPincodes" + System.Globalization.CultureInfo.CurrentCulture.ToString());
                    break;
                default:
                    tmpl = SmartPortal.Common.ST.GetStringTemplate("IBResetPasswordAdmin", "ResetPasswords" + System.Globalization.CultureInfo.CurrentCulture.ToString());
                    break;
            }
            tmpl.Reset();
            StringBuilder st = new StringBuilder();
            st.Append(pass);
            tmpl.SetAttribute("USER", userid);
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
            switch (ddlsendinfo.SelectedValue.ToString())
            {
                case SmartPortal.Constant.IPC.EMAIL:
                    //send mail
                    SmartPortal.Common.EmailHelper.SendMailMessageAsync(System.Configuration.ConfigurationManager.AppSettings["contractapprovemailfrom"], email, System.Configuration.ConfigurationManager.AppSettings["contractapprovemailtitle"], tmpl.ToString());
                    tmpl.RemoveAttribute("PASS");
                    lblError.Text = msgsendemail;
                    SmartPortal.Common.Log.WriteLogFile("", "", "", "SEMSUSERRESETPASS SUCCESS, SENT EMAIL TO " + email + "-USERNAME:" + ddluser.SelectedValue);
                    break;
                case SmartPortal.Constant.IPC.SMS:
                    //check sms registered
                    //send sms
                    SmartPortal.SEMS.Contract.sendSMS_RESETPASS(PhoneNo, SENDINFO, ref errorcode, ref errorDesc);
                    lblError.Text = msgsendsms;
                    SmartPortal.Common.Log.WriteLogFile("", "", "", "SEMSUSERRESETPASS SUCCESS,SENT SMS TO " + PhoneNo + "-USERNAME:" + ddluser.SelectedValue);
                    break;
            }
            btsave.Visible = false;
            btsave.Enabled = false;
            dAuthenType.Visible = false;
            dSeninfo.Visible = false;
            ResCheck.Visible = false;
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSResetPasswords_Widget", "SendInfoLogin", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void btcheckUs_OnClick(object sender, EventArgs e)
    {
        try
        {
            string ERRORCODE = string.Empty;
            string ERRORDESC = string.Empty;

            ddlsendinfo.Items.Clear();
            ddlsendinfo.Items.Add(new ListItem(Resources.labels.sendcontractinforemail, SmartPortal.Constant.IPC.EMAIL));
            //ddlsendinfo.Items.Add(new ListItem(Resources.labels.sendcontractinforsms, SmartPortal.Constant.IPC.SMS));

            ddlauthentype.Items.Clear();
            ddlauthentype.Items.Add(new ListItem(Resources.labels.matkhau, SmartPortal.Constant.IPC.PASSWORD));
            ddlauthentype.Items.Add(new ListItem(Resources.labels.pincode, "PINCODE"));

            if (ddluser.SelectedValue != null && ddluser.SelectedValue != "")
            {
                DataSet dtUser = new DataSet();
                dtUser = new SmartPortal.SEMS.User().GETDetailsUser(ddluser.SelectedValue, ddlService.SelectedValue.ToString(), ddlloginmethod.SelectedValue.ToString(), ddlauthentype.SelectedValue.ToString(), ref ERRORCODE, ref ERRORDESC);
                if (ERRORCODE == "0")
                {
                    ResCheck.Visible = true;
                    if (dtUser.Tables[0].Rows.Count == 0)
                    {
                        lblError.Text = Resources.labels.phoneanduserinvalid;
                        btsave.Visible = false;
                        btsave.Enabled = false;
                        dAuthenType.Visible = false;
                        dSeninfo.Visible = false;
                        ResCheck.Visible = false;
                        return;
                    }
                    else
                    {
                        btsave.Visible = true;
                        btsave.Enabled = true;
                        dAuthenType.Visible = true;
                        dSeninfo.Visible = true;
                        txtReFullName.Text = dtUser.Tables[0].Rows[0]["FullName"].ToString();
                        txtReEmail.Text = dtUser.Tables[0].Rows[0]["Email"].ToString();
                        if (ddlService.SelectedValue.ToString().Equals("IB"))
                        {
                            txtRephone.Text = dtUser.Tables[0].Rows[0]["phone"].ToString();
                        }
                        else
                        {
                            txtRephone.Text = dtUser.Tables[0].Rows[0]["PHONENO"].ToString();
                        }
                        txtReAddress.Text = dtUser.Tables[0].Rows[0]["Address"].ToString();
                        txtReauthentype.Text = string.Empty;
                        string txtpass = string.Empty;
                        string txtpin = string.Empty;
                        if (txtReEmail.Text.Trim() == "")
                        {
                            ddlsendinfo.Items.Remove(ddlsendinfo.Items.FindByValue(SmartPortal.Constant.IPC.EMAIL));
                        }
                        if (ddluser.SelectedValue == "")
                        {
                            ddlsendinfo.Items.Remove(ddlsendinfo.Items.FindByValue(SmartPortal.Constant.IPC.SMS));
                        }
                        ddlauthentype.Items.Clear();
                        for (int i = 0; i < dtUser.Tables[0].Rows.Count; i++)
                        {
                            txtReauthentype.Text += dtUser.Tables[0].Rows[i]["type"].ToString() + " ";

                            if (dtUser.Tables[0].Rows[i]["type"].ToString() == SmartPortal.Constant.IPC.PASSWORD)
                            {
                                ddlauthentype.Items.Add(new ListItem(Resources.labels.matkhau, SmartPortal.Constant.IPC.PASSWORD));
                            }
                            if (dtUser.Tables[0].Rows[i]["type"].ToString() == "PINCODE")
                            {
                                ddlauthentype.Items.Add(new ListItem(Resources.labels.pincode, "PINCODE"));
                            }
                        }
                        DateTime da = new DateTime();
                        if (DateTime.TryParse(dtUser.Tables[0].Rows[0]["Birthday"].ToString(), out da))
                        {
                            txtRebirtday.Text = DateTime.Parse(dtUser.Tables[0].Rows[0]["Birthday"].ToString()).ToString("dd/MM/yyyy");
                        }
                        else
                        {
                            txtRebirtday.Text = "";
                        }
                        switch (dtUser.Tables[0].Rows[0]["Gender"].ToString())
                        {
                            case "M":
                                txtReSex.Text = Resources.labels.male;
                                break;
                            case "F":
                                txtReSex.Text = Resources.labels.nu;
                                break;
                            default:
                                txtReSex.Text = string.Empty;
                                break;
                        }
                        return;
                    }
                }
                else
                {
                    ResCheck.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSResetPasswords_Widget", "btcheckUs_OnClick", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    protected void btcheckUsSend_OnClick()
    {
        try
        {
            string ERRORCODE = string.Empty;
            string ERRORDESC = string.Empty;

            if (ddluser.SelectedValue != null && ddluser.SelectedValue != "")
            {
                DataSet dtUser = new DataSet();
                dtUser = new SmartPortal.SEMS.User().GETDetailsUser(ddluser.SelectedValue, ddlService.SelectedValue.ToString(), ddlloginmethod.SelectedValue.ToString(), ddlauthentype.SelectedValue.ToString(), ref ERRORCODE, ref ERRORDESC);
                if (ERRORCODE == "0")
                {
                    ResCheck.Visible = true;
                    if (dtUser.Tables[0].Rows.Count == 0)
                    {
                        lblError.Text = Resources.labels.phoneanduserinvalid;
                        btsave.Visible = false;
                        btsave.Enabled = false;
                        dAuthenType.Visible = false;
                        dSeninfo.Visible = false;
                        ResCheck.Visible = false;
                        return;
                    }
                    else
                    {
                        btsave.Visible = true;
                        btsave.Enabled = true;
                        dAuthenType.Visible = true;
                        dSeninfo.Visible = true;
                        txtReFullName.Text = dtUser.Tables[0].Rows[0]["FullName"].ToString();
                        txtReEmail.Text = dtUser.Tables[0].Rows[0]["Email"].ToString();
                        if (ddlService.SelectedValue.ToString().Equals("IB"))
                        {
                            txtRephone.Text = dtUser.Tables[0].Rows[0]["phone"].ToString();
                        }
                        else
                        {
                            txtRephone.Text = dtUser.Tables[0].Rows[0]["PHONENO"].ToString();
                        }
                        //txtRephone.Text = dtUser.Tables[0].Rows[0]["PHONENO"].ToString();
                        txtReAddress.Text = dtUser.Tables[0].Rows[0]["Address"].ToString();
                        txtReauthentype.Text = string.Empty;
                        string txtpass = string.Empty;
                        string txtpin = string.Empty;
                        for (int i = 0; i < dtUser.Tables[0].Rows.Count; i++)
                        {
                            txtReauthentype.Text += dtUser.Tables[0].Rows[i]["type"].ToString() + " ";
                        }

                        DateTime da = new DateTime();
                        if (DateTime.TryParse(dtUser.Tables[0].Rows[0]["Birthday"].ToString(), out da))
                        {
                            txtRebirtday.Text = DateTime.Parse(dtUser.Tables[0].Rows[0]["Birthday"].ToString()).ToString("dd/MM/yyyy");
                        }
                        else
                        {
                            txtRebirtday.Text = "";
                        }
                        switch (dtUser.Tables[0].Rows[0]["Gender"].ToString())
                        {
                            case "M":
                                txtReSex.Text = Resources.labels.male;
                                break;
                            case "F":
                                txtReSex.Text = Resources.labels.nu;
                                break;
                            default:
                                txtReSex.Text = string.Empty;
                                break;
                        }
                        return;
                    }
                }
                else
                {
                    ResCheck.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSResetPasswords_Widget", "btcheckUs_OnClick", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }


    protected void checkSendMethod(string type, string email, string phoneno)
    {
        try
        {
            switch (ddlsendinfo.SelectedValue.Trim())
            {
                case SmartPortal.Constant.IPC.EMAIL:
                    if (email == "" || email == null)
                    {
                        lblError.Text = "Email information is not available";
                        return;
                    }
                    break;
                case SmartPortal.Constant.IPC.SMS:
                    if (String.IsNullOrEmpty(phoneno))
                    {
                        lblError.Text = Resources.labels.phonenumberdoesnotexist;
                        return;
                    }
                    break;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], "Widgets_SEMSResetPasswords_Widget", "checkSendMethod", ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }

    }
}


