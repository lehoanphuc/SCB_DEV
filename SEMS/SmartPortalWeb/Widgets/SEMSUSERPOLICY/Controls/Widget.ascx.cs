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
using SmartPortal.Constant;
using SmartPortal.SEMS;
using SmartPortal.Model;

public partial class Widgets_SEMSUSERPOLICY_Controls_Widget : WidgetBase
{
    string ACTION = string.Empty;
    string IPCERRORCODE = string.Empty;
    string IPCERRORDESC = string.Empty;
    string serviceid = string.Empty;

    public string _TITLE
    {
        get { return lblTitleProduct.Text; }
        set { lblTitleProduct.Text = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, typeof(Page), "display cplx", " hidepwdcpdetail();", true);
        ScriptManager.RegisterStartupScript(this, typeof(Page), "display timelogin", " hidetimelogindetail();", true);
        try
        {
            ACTION = GetActionPage();
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
            DataSet dsservice = new Services().GetAll("GMS", ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                DataTable dtservice = dsservice.Tables[0];
                cblServiceID.DataSource = dtservice;
                cblServiceID.DataTextField = "ServiceName";
                cblServiceID.DataValueField = "ServiceID";
                cblServiceID.DataBind();
                cblServiceID.Items.Remove(cblServiceID.Items.FindByValue(SmartPortal.Constant.IPC.SMS));
                ddlisdefault.Enabled = false;
            }
            switch (ACTION)
            {
                case IPC.ACTIONPAGE.ADD:
                    txPolicyID.Enabled = false;
                    txPwdhis.Text = ConfigurationManager.AppSettings["pwdhis_default"].ToString();
                    txPwdagemax.Text = ConfigurationManager.AppSettings["pwdagemax_default"].ToString();
                    txMinpwdlen.Text = ConfigurationManager.AppSettings["minpwdlen_default"].ToString();
                    txlginfr.Text = ConfigurationManager.AppSettings["lginfr_default"].ToString();
                    txlginto.Text = ConfigurationManager.AppSettings["lginto_default"].ToString();
                    txLKOUTTHRS.Text = ConfigurationManager.AppSettings["lkoutthrs_default"].ToString();
                    txRESETLKOUT.Text = ConfigurationManager.AppSettings["resetlkout_default"].ToString();
                    break;
                default:
                    txPolicyID.Enabled = false;
                    cblServiceID.Enabled = false;
                    string commandArg = GetParamsPage(IPC.ID)[0].Trim();
                    string[] obj = commandArg.Split('|');
                    DataSet dtCL = new DataSet();
                    serviceid = obj[1].ToString();
                    dtCL = new SmartPortal.SEMS.USERPOLICY().GetPolicybyCondition(obj[0], string.Empty, string.Empty, string.Empty, obj[1], Session["userName"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE == "0")
                    {
                        //gan du lieu ra cac control
                        DataTable dtpolicy = dtCL.Tables[0];
                        txPolicyID.Text = dtpolicy.Rows[0]["policyid"].ToString().Trim();
                        foreach (ListItem item in cblServiceID.Items)
                        {
                            if (item.Value == obj[1].ToString())
                            {
                                item.Selected = true;
                            }

                        }
                        txDescr.Text = dtpolicy.Rows[0]["descr"].ToString().Trim();
                        txEffrom.Text = dtpolicy.Rows[0]["effromtx"].ToString().Trim();
                        txefto.Text = dtpolicy.Rows[0]["eftotx"].ToString().Trim();
                        txPwdhis.Text = dtpolicy.Rows[0]["pwdhis"].ToString().Trim();
                        txPwdagemax.Text = dtpolicy.Rows[0]["pwdagemax"].ToString().Trim();
                        txMinpwdlen.Text = dtpolicy.Rows[0]["minpwdlen"].ToString().Trim();
                        ddlpwdcplx.SelectedValue = dtpolicy.Rows[0]["pwdcplx"].ToString().Trim();
                        ddlpwdcplxlc.SelectedValue = dtpolicy.Rows[0]["pwdcplxlc"].ToString().Trim();
                        ddlpwdcplxuc.SelectedValue = dtpolicy.Rows[0]["pwdcplxuc"].ToString().Trim();
                        ddlpwdcplxsc.SelectedValue = dtpolicy.Rows[0]["pwdcplxsc"].ToString().Trim();
                        ddlpwdcplxnc.SelectedValue = dtpolicy.Rows[0]["pwccplxsn"].ToString().Trim();
                        ddltimelogin.SelectedValue = dtpolicy.Rows[0]["timelginrequire"].ToString().Trim();
                        txlginfr.Text = dtpolicy.Rows[0]["lginfr"].ToString().Trim();
                        txlginto.Text = dtpolicy.Rows[0]["lginto"].ToString().Trim();
                        txLKOUTTHRS.Text = dtpolicy.Rows[0]["llkoutthrs"].ToString().Trim();
                        txRESETLKOUT.Text = dtpolicy.Rows[0]["resetlkout"].ToString().Trim();
                        ddlisdefault.SelectedValue = dtpolicy.Rows[0]["isdefaulttx"].ToString().Trim();
                    }
                    else
                    {
                        lblError.Text = IPCERRORDESC;
                    }
                    break;
            }
            //HIEN THI
            switch (ACTION)
            {
                case IPC.ACTIONPAGE.EDIT:
                    ddlisdefault.Enabled = false;
                    btsave.Text = Resources.labels.capnhat;
                    btnClear.Enabled = false;
                    break;
                case IPC.ACTIONPAGE.DETAILS:
                    pnAdd.Enabled = false;
                    btsave.Enabled = false;
                    btnClear.Enabled = false;
                    break;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        try
        {
            if (!validateinput())
            {
                return;
            }
            UserPolicyModel policy = new UserPolicyModel();
            switch (ACTION)
            {
                case IPC.ACTIONPAGE.ADD:
                    if (!CheckPermitPageAction(IPC.ACTIONPAGE.ADD)) return;
                    policy.serviceID = serviceid;
                    policy.descr = txDescr.Text.Trim();
                    policy.isdefault = bool.Parse(ddlisdefault.SelectedValue);
                    policy.effrom = txEffrom.Text.Trim();
                    policy.efto = txefto.Text.Trim();
                    policy.pwdhis = Convert.ToInt32(txPwdhis.Text.Trim());
                    policy.pwdagemax = Convert.ToInt32(txPwdagemax.Text.Trim());
                    policy.minpwdlen = Convert.ToInt32(txMinpwdlen.Text.Trim());
                    policy.pwdcplx = bool.Parse(ddlpwdcplx.SelectedValue);
                    if (policy.pwdcplx)
                    {
                        policy.pwdcplxlc = bool.Parse(ddlpwdcplxlc.SelectedValue);
                        policy.pwdcplxuc = bool.Parse(ddlpwdcplxuc.SelectedValue);
                        policy.pwdcplxsc = bool.Parse(ddlpwdcplxsc.SelectedValue);
                        policy.pwdcplxsn = bool.Parse(ddlpwdcplxnc.SelectedValue);
                    }
                    else
                    {
                        policy.pwdcplxlc = false;
                        policy.pwdcplxuc = false;
                        policy.pwdcplxsc = false;
                        policy.pwdcplxsn = false;
                    }
                    policy.timelginrequire = bool.Parse(ddltimelogin.SelectedValue);
                    if (policy.timelginrequire)
                    {
                        policy.lginfr = txlginfr.Text.Trim();
                        policy.lginto = txlginto.Text.Trim();
                    }
                    else
                    {
                        policy.lginfr = ConfigurationManager.AppSettings["lginfr_default"].ToString();
                        policy.lginto = ConfigurationManager.AppSettings["lginto_default"].ToString();
                    }
                    policy.lkoutthrs = Convert.ToInt32(txLKOUTTHRS.Text.Trim());
                    policy.resetlkout = Convert.ToInt32(txRESETLKOUT.Text.Trim());
                    try
                    {
                        new SmartPortal.SEMS.USERPOLICY().PolicyInsert(policy, Session["userName"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
                        if (IPCERRORCODE == "0")
                        {
                            pnAdd.Enabled = false;
                            lblError.Text = Resources.labels.thempolicythanhcong;
                            btsave.Enabled = false;
                        }
                        else
                        {
                            lblError.Text = Resources.labels.thempolicyloi;
                        }

                    }
                    catch (Exception ex)
                    {
                        lblError.Text = ex.ToString();
                        return;
                    }
                    break;
                case IPC.ACTIONPAGE.EDIT:
                    if (!CheckPermitPageAction(IPC.ACTIONPAGE.EDIT)) return;
                    policy.policyid = Convert.ToInt32(txPolicyID.Text.Trim());
                    policy.serviceID = serviceid;
                    policy.descr = txDescr.Text.Trim();
                    policy.isdefault = bool.Parse(ddlisdefault.SelectedValue);
                    policy.effrom = txEffrom.Text.Trim();
                    policy.efto = txefto.Text.Trim();
                    policy.pwdhis = Convert.ToInt32(txPwdhis.Text.Trim());
                    policy.pwdagemax = Convert.ToInt32(txPwdagemax.Text.Trim());
                    policy.minpwdlen = Convert.ToInt32(txMinpwdlen.Text.Trim());
                    policy.pwdcplx = bool.Parse(ddlpwdcplx.SelectedValue);
                    if (policy.pwdcplx)
                    {
                        policy.pwdcplxlc = bool.Parse(ddlpwdcplxlc.SelectedValue);
                        policy.pwdcplxuc = bool.Parse(ddlpwdcplxuc.SelectedValue);
                        policy.pwdcplxsc = bool.Parse(ddlpwdcplxsc.SelectedValue);
                        policy.pwdcplxsn = bool.Parse(ddlpwdcplxnc.SelectedValue);
                    }
                    else
                    {
                        policy.pwdcplxlc = false;
                        policy.pwdcplxuc = false;
                        policy.pwdcplxsc = false;
                        policy.pwdcplxsn = false;
                    }
                    policy.timelginrequire = bool.Parse(ddltimelogin.SelectedValue);
                    if (policy.timelginrequire)
                    {
                        policy.lginfr = txlginfr.Text.Trim();
                        policy.lginto = txlginto.Text.Trim();
                    }
                    else
                    {
                        policy.lginfr = ConfigurationManager.AppSettings["lginfr_default"].ToString();
                        policy.lginto = ConfigurationManager.AppSettings["lginto_default"].ToString();
                    }
                    policy.lkoutthrs = Convert.ToInt32(txLKOUTTHRS.Text.Trim());
                    policy.resetlkout = Convert.ToInt32(txRESETLKOUT.Text.Trim());
                    try
                    {
                        new SmartPortal.SEMS.USERPOLICY().PolicyUpdate(policy, Session["userName"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
                        if (IPCERRORCODE == "0")
                        {
                            pnAdd.Enabled = false;
                            lblError.Text = Resources.labels.suapolicythanhcong;
                            btsave.Enabled = false;
                        }
                        else
                        {
                            lblError.Text = IPCERRORDESC;
                        }
                    }
                    catch (Exception ex)
                    {
                        lblError.Text = ex.ToString();
                        return;
                    }
                    updatepolicybaseonthispolicy();
                    break;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    private bool validateinput()
    {
        serviceid = string.Empty;
        foreach (ListItem i in cblServiceID.Items)
        {
            if (i.Selected)
            {
                if (serviceid == string.Empty)
                {
                    serviceid = serviceid + i.Value;
                }
                else
                {
                    serviceid = serviceid + "#" + i.Value;
                }
            }
        }
        if (serviceid == string.Empty)
        {
            lblError.Text = Resources.labels.banphaichonmotdichvu;
            return false;
        }
        foreach (string serviceidcheck in serviceid.Split('#'))
        {
            DataTable dtservice = new SmartPortal.SEMS.USERPOLICY().Checkdefaultpolicy(txPolicyID.Text.Trim(), serviceidcheck);
            //check default policy
            if (bool.Parse(ddlisdefault.SelectedValue.ToString()))
            {
                if (dtservice.Rows.Count == 0)
                {

                }
                else if (dtservice == null)
                {
                    lblError.Text = Resources.labels.laythongtinpolicyloi;
                    return false;
                }
                else
                {
                    lblError.Text = Resources.labels.policychoservicedatontai;
                    lblError.Text = string.Format(Resources.labels.defaultpolicydatontaihaychonsuadefaultpolicy, dtservice.Rows[0]["serviceid"].ToString());
                    return false;
                }

            }
            else
            {
                if (dtservice.Rows.Count == 0)
                {
                    lblError.Text = string.Format(Resources.labels.haythempolicydefaultchodichvutruoc, serviceidcheck);
                    return false;
                }
                else if (dtservice == null)
                {

                    lblError.Text = Resources.labels.laythongtinpolicyloi;
                    return false;
                }
                else
                {

                }
            }
        }
        //validate description of policy:
        if (string.IsNullOrEmpty(txDescr.Text.Trim()))
        {
            lblError.Text = Resources.labels.tenpolicykhongduocdetrong;
            return false;
        }

        //validate fromdate
        if (txEffrom.Text.Trim() == string.Empty)
        {
            lblError.Text = Resources.labels.tungaykhongduocdetrong;
            return false;
        }
        try
        {
            DateTime dtfrom = DateTime.ParseExact(txEffrom.Text.Trim(), ConfigurationManager.AppSettings["shortdateformat"], null);
        }
        catch (Exception ex)
        {
            lblError.Text = string.Format(Resources.labels.tungayphailakieungaythang, ConfigurationManager.AppSettings["shortdateformat"].ToString());
            return false;
        }
        if (txefto.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime dtfrom = DateTime.ParseExact(txefto.Text.Trim(), ConfigurationManager.AppSettings["shortdateformat"], null);
                int i = DateTime.Compare(DateTime.ParseExact(txEffrom.Text.Trim(), ConfigurationManager.AppSettings["shortdateformat"], null), DateTime.ParseExact(txefto.Text.Trim(), ConfigurationManager.AppSettings["shortdateformat"], null));
                if (i > 0)
                {
                    lblError.Text = Resources.labels.thoigianhieulucketthucphailonhonbangthoigianhieulucbatdau;
                    return false;
                }
            }
            catch
            {
                lblError.Text = string.Format(Resources.labels.denngayphailakieungaythang, ConfigurationManager.AppSettings["shortdateformat"].ToString());
                return false;
            }
        }
        if (txPwdhis.Text.Trim() == string.Empty)
        {
            lblError.Text = Resources.labels.lichsumatkhaukhongduocdetrong;
            return false;
        }
        else
        {
            try
            {
                int pwdhis = Convert.ToInt32(txPwdhis.Text.Trim());
                if (pwdhis > 6)
                {
                    lblError.Text = Resources.labels.lichsumatkhauphainhohonbangsau;
                    return false;
                }
            }
            catch
            {
                lblError.Text = Resources.labels.lichsumatkhauphailakieuso;
                return false;
            }
        }
        if (txPwdagemax.Text.Trim() == string.Empty)
        {
            lblError.Text = Resources.labels.tuoimatkhautoidakhongduocdetrong;
            return false;
        }
        else
        {
            try
            {
                int pwdage = Convert.ToInt32(txPwdagemax.Text.Trim());
                if (pwdage < 0)
                {
                    lblError.Text = Resources.labels.tuoimatkhauphailonhonhoacbangkhong;
                    return false;
                }
            }
            catch
            {
                lblError.Text = Resources.labels.tuoimatkhautoidaphailakieusonguyen;
                return false;
            }
        }
        if (txMinpwdlen.Text.Trim() == string.Empty)
        {
            lblError.Text = Resources.labels.dodaimatkhautoithieukhongduoctrong;
            return false;
        }
        else
        {
            try
            {
                int pwdlen = Convert.ToInt32(txMinpwdlen.Text.Trim());
                if (pwdlen < 6)
                {
                    lblError.Text = Resources.labels.dodaitoithieumatkhauphailonhonhoacbangsau;
                    return false;
                }
            }
            catch
            {
                lblError.Text = Resources.labels.dodaimatkhautoithieuphailakieusonguyen;
                return false;
            }
        }

        if (bool.Parse(ddltimelogin.SelectedValue.ToString()))
        {
            if (txlginfr.Text.Trim() == string.Empty)
            {
                lblError.Text = Resources.labels.thoigianbatdaudangnhapkhongduocdetrong;
                return false;
            }
            else
            {
                try
                {
                    DateTime dtloginto = DateTime.ParseExact(txlginfr.Text.Trim(), ConfigurationManager.AppSettings["shorttimeformat"], null);
                }
                catch
                {
                    lblError.Text = string.Format(Resources.labels.thoigianbatdaudangnhapphailakieugio, ConfigurationManager.AppSettings["shorttimeformat"].ToString());
                    return false;
                }
            }
            if (txlginto.Text.Trim() == string.Empty)
            {
                lblError.Text = Resources.labels.thoigiandendangnhapkhongduocdetrong;
                return false;
            }
            else
            {
                try
                {
                    DateTime dtloginto = DateTime.ParseExact(txlginto.Text.Trim(), ConfigurationManager.AppSettings["shorttimeformat"], null);
                }
                catch
                {
                    lblError.Text = string.Format(Resources.labels.thoigiandendangnhapphailakieugio, ConfigurationManager.AppSettings["shorttimeformat"].ToString());
                    return false;
                }
            }

        }
        //validate time from and time to
        int timecompare = DateTime.Compare(DateTime.ParseExact(txlginfr.Text.Trim(), ConfigurationManager.AppSettings["shorttimeformat"], null), DateTime.ParseExact(txlginto.Text.Trim(), ConfigurationManager.AppSettings["shorttimeformat"], null));
        if (timecompare > 0)
        {
            lblError.Text = Resources.labels.thoigianloginketthucphailonhonbangthoigianloginbatdau;
            return false;
        }

        if (txLKOUTTHRS.Text.Trim() == string.Empty)
        {
            lblError.Text = Resources.labels.solandangnhapsaikhongduocdetrong;
            return false;
        }
        else
        {
            try
            {
                int pwdhis = Convert.ToInt32(txLKOUTTHRS.Text.Trim());
                if (pwdhis < 0)
                {
                    lblError.Text = Resources.labels.solansaimatkhauphailonhonbangkhong;
                    return false;
                }
            }
            catch
            {
                lblError.Text = Resources.labels.solandangnhapsaiphailakieusonguyen;
                return false;

            }
        }
        if (txRESETLKOUT.Text.Trim() == string.Empty)
        {
            lblError.Text = Resources.labels.thoigiankhoiphucdangnhapsaikhongdetrong;
            return false;
        }
        else
        {
            try
            {
                int pwdhis = Convert.ToInt32(txRESETLKOUT.Text.Trim());
                if (pwdhis < 0)
                {
                    lblError.Text = Resources.labels.sophutresetphailonhonbangkhong;
                    return false;
                }
            }
            catch
            {
                lblError.Text = Resources.labels.thoigiankhoiphucdangnhapsaiphailakieusonguyen;
                return false;
            }
        }
        DataTable dtregion = new SmartPortal.SEMS.USERPOLICY().Checkpolicyname(txPolicyID.Text.Trim(), txDescr.Text.Trim());
        if (dtregion.Rows.Count == 0)
        {
            return true;
        }
        else if (dtregion == null)
        {

            lblError.Text = Resources.labels.laythongtinpolicyloi;
            return false;
        }
        else
        {
            lblError.Text = Resources.labels.tenpolicydatontaihaychonmieutakhac;
            return false;
        }
        return true;
    }
    protected void btback_Click(object sender, EventArgs e)
    {
        RedirectBackToMainPage();
    }
    private void updatepolicybaseonthispolicy()
    {
        DataTable dtpolicybaseon = new SmartPortal.SEMS.USERPOLICY().GetPolicyBaseonthispolicy(txPolicyID.Text.Trim(), serviceid);
        if (dtpolicybaseon.Rows.Count > 0)
        {
            foreach (DataRow row in dtpolicybaseon.Rows)
            {
                #region compare and assign
                //pass history
                if (int.Parse(txPwdhis.Text.Trim()) > 0)
                {
                    if (Convert.ToInt32(row["pwdhis"]) < int.Parse(txPwdhis.Text.Trim()))
                        row["pwdhis"] = int.Parse(txPwdhis.Text.Trim());

                }
                // pass age max
                if (int.Parse(txPwdagemax.Text.Trim()) > 0)
                {
                    if (Convert.ToInt32(row["pwdagemax"]) < int.Parse(txPwdagemax.Text.Trim()))
                        row["pwdagemax"] = int.Parse(txPwdagemax.Text.Trim());

                }
                //pass min len
                if (Convert.ToInt32(row["minpwdlen"]) < int.Parse(txMinpwdlen.Text.Trim()))
                    row["minpwdlen"] = int.Parse(txMinpwdlen.Text.Trim());
                // check complexity of pass
                if (bool.Parse(ddlpwdcplx.SelectedValue))
                {
                    if (!bool.Parse(row["pwdcplx"].ToString()))
                        row["pwdcplx"] = bool.Parse(ddlpwdcplx.SelectedValue);
                    if (bool.Parse(ddlpwdcplxlc.SelectedValue))
                    {
                        if (!bool.Parse(row["pwdcplxlc"].ToString()))
                            row["pwdcplxlc"] = bool.Parse(ddlpwdcplx.SelectedValue);
                    }
                    if (bool.Parse(ddlpwdcplxuc.SelectedValue))
                    {
                        if (!bool.Parse(row["pwdcplxuc"].ToString()))
                            row["pwdcplxuc"] = bool.Parse(ddlpwdcplx.SelectedValue);
                    }
                    if (bool.Parse(ddlpwdcplxsc.SelectedValue))
                    {
                        if (!bool.Parse(row["pwdcplxsc"].ToString()))
                            row["pwdcplxsc"] = bool.Parse(ddlpwdcplx.SelectedValue);
                    }
                    if (bool.Parse(ddlpwdcplxnc.SelectedValue))
                    {
                        if (!bool.Parse(row["pwccplxsn"].ToString()))
                            row["pwccplxsn"] = bool.Parse(ddlpwdcplx.SelectedValue);
                    }
                }
                // check timelogin
                if (bool.Parse(ddltimelogin.SelectedValue))
                {
                    if (!bool.Parse(row["timelginrequire"].ToString()))
                        row["timelginrequire"] = bool.Parse(ddlpwdcplx.SelectedValue);
                    int timecompare = DateTime.Compare(DateTime.ParseExact(row["lginfr"].ToString(), ConfigurationManager.AppSettings["shorttimeformat"], null), DateTime.ParseExact(txlginfr.Text.Trim(), ConfigurationManager.AppSettings["shorttimeformat"], null));
                    int timecomparetofr2 = DateTime.Compare(DateTime.ParseExact(txlginto.Text.Trim(), ConfigurationManager.AppSettings["shorttimeformat"], null), DateTime.ParseExact(row["lginfr"].ToString(), ConfigurationManager.AppSettings["shorttimeformat"], null));
                    if (timecompare < 0 || (timecompare >= 0 & timecomparetofr2 <= 0))
                    {
                        row["lginfr"] = txlginfr.Text.Trim();
                    }
                    int timecompareto = DateTime.Compare(DateTime.ParseExact(row["lginto"].ToString(), ConfigurationManager.AppSettings["shorttimeformat"], null), DateTime.ParseExact(txlginto.Text.Trim(), ConfigurationManager.AppSettings["shorttimeformat"], null));
                    int timecompareto2 = DateTime.Compare(DateTime.ParseExact(row["lginto"].ToString(), ConfigurationManager.AppSettings["shorttimeformat"], null), DateTime.ParseExact(row["lginfr"].ToString(), ConfigurationManager.AppSettings["shorttimeformat"], null));
                    if (timecompareto > 0 || (timecompareto <= 0 & timecompareto2 <= 0))
                    {
                        row["lginto"] = txlginto.Text.Trim();
                    }

                }
                //check number fail
                if (Convert.ToInt32(row["llkoutthrs"]) < int.Parse(txLKOUTTHRS.Text.Trim()))
                    row["llkoutthrs"] = int.Parse(txLKOUTTHRS.Text.Trim());
                //check auto reset
                if (int.Parse(txRESETLKOUT.Text.Trim()) > 0)
                {
                    if (Convert.ToInt32(row["resetlkout"]) < int.Parse(txRESETLKOUT.Text.Trim()))
                        row["resetlkout"] = int.Parse(txRESETLKOUT.Text.Trim());

                }
                #endregion
                #region update
                UserPolicyModel policy = new UserPolicyModel();
                policy.policyid = Convert.ToInt32(txPolicyID.Text.Trim());
                policy.policyid = Convert.ToInt32(row["policyid"].ToString());
                policy.serviceID = row["serviceID"].ToString();
                policy.descr = row["descr"].ToString();
                policy.isdefault = bool.Parse(row["isdefaulttx"].ToString());
                policy.effrom = row["effromtx"].ToString();
                policy.efto = row["eftotx"].ToString();
                policy.pwdhis = Convert.ToInt32(row["pwdhis"].ToString());
                policy.pwdagemax = Convert.ToInt32(row["pwdagemax"].ToString());
                policy.minpwdlen = Convert.ToInt32(row["minpwdlen"].ToString());
                policy.pwdcplx = bool.Parse(row["pwdcplx"].ToString());
                policy.pwdcplxlc = bool.Parse(row["pwdcplxlc"].ToString());
                policy.pwdcplxuc = bool.Parse(row["pwdcplxuc"].ToString());
                policy.pwdcplxsc = bool.Parse(row["pwdcplxsc"].ToString());
                policy.pwdcplxsn = bool.Parse(row["pwccplxsn"].ToString());
                policy.timelginrequire = bool.Parse(row["timelginrequire"].ToString());
                policy.lginfr = row["lginfr"].ToString();
                policy.lginto = row["lginto"].ToString();
                policy.lkoutthrs = Convert.ToInt32(row["llkoutthrs"].ToString());
                policy.resetlkout = Convert.ToInt32(row["resetlkout"].ToString());
                try
                {
                    new SmartPortal.SEMS.USERPOLICY().PolicyUpdate(policy, Session["userName"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE == "0")
                    {
                        lblError.Text = Resources.labels.suapolicythanhcong;
                    }
                    else
                    {
                        lblError.Text = IPCERRORDESC;
                    }

                }
                catch (Exception ex)
                {
                    lblError.Text = ex.ToString();
                    return;
                }
                #endregion
            }
        }
    }

    protected void btnClear_OnClick(object sender, EventArgs e)
    {
        lblError.Text = string.Empty;
        pnAdd.Enabled = true;
        ScriptManager.RegisterStartupScript(this, typeof(Page), "display cplx", " hidepwdcpdetail();", true);
        ScriptManager.RegisterStartupScript(this, typeof(Page), "display timelogin", " hidetimelogindetail();", true);
        txPolicyID.Text = string.Empty;
        foreach (ListItem item in cblServiceID.Items)
        {
            item.Selected = false;
        }
        txDescr.Text = string.Empty;
        txEffrom.Text = string.Empty;
        txefto.Text = string.Empty;
        txPwdhis.Text = ConfigurationManager.AppSettings["pwdhis_default"].ToString();
        txPwdagemax.Text = ConfigurationManager.AppSettings["pwdagemax_default"].ToString();
        txMinpwdlen.Text = ConfigurationManager.AppSettings["minpwdlen_default"].ToString();
        ddlpwdcplx.SelectedIndex = 0;
        ddlpwdcplxlc.SelectedIndex = 0;
        ddlpwdcplxuc.SelectedIndex = 0;
        ddlpwdcplxsc.SelectedIndex = 0;
        ddlpwdcplxnc.SelectedIndex = 0;
        ddltimelogin.SelectedIndex = 0;
        txlginfr.Text = ConfigurationManager.AppSettings["lginfr_default"].ToString();
        txlginto.Text = ConfigurationManager.AppSettings["lginto_default"].ToString();
        txLKOUTTHRS.Text = ConfigurationManager.AppSettings["lkoutthrs_default"].ToString();
        txRESETLKOUT.Text = ConfigurationManager.AppSettings["resetlkout_default"].ToString();
        btsave.Enabled = true;
    }
}
