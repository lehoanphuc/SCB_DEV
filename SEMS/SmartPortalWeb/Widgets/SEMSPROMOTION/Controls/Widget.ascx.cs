using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using SmartPortal.BLL;
using SmartPortal.ExceptionCollection;
using SmartPortal.Common.Utilities;
using SmartPortal.Constant;
using SmartPortal.Model;

public partial class Widgets_SEMSOTCFEE_Controls_Widget : WidgetBase
{
    private string ACTION = string.Empty;
    private string IPCERRORCODE = string.Empty;
    private string IPCERRORDESC = string.Empty;
    public string _TITLE
    {
        get { return lblTitleProduct.Text; }
        set { lblTitleProduct.Text = value; }
    }
    public string PROMOTIONCODE
    {
        get { return ViewState["PROMOTIONCODE"] != null ? (string)ViewState["PROMOTIONCODE"] : string.Empty; }
        set { ViewState["PROMOTIONCODE"] = value; }
    }
    public string EXPIREDDATE
    {
        get { return ViewState["EXPIREDDATE"] != null ? (string)ViewState["EXPIREDDATE"] : string.Empty; }
        set { ViewState["EXPIREDDATE"] = value; }
    }
    public string STATUS
    {
        get { return ViewState["STATUS"] != null ? (string)ViewState["STATUS"] : string.Empty; }
        set { ViewState["STATUS"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = string.Empty;
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
            #region Enable/Disable theo Action
            switch (ACTION)
            {
                case IPC.ACTIONPAGE.DETAILS:
                    txtPromotionCode.Enabled = false;
                    txtExpireDate.Enabled = false;
                    ddlStatus.Enabled = false;
                    btnClear.Enabled = false;
                    btsave.Enabled = false;
                    ddlStatus.Items.Add(new ListItem(Resources.labels.active, "A"));
                    ddlStatus.Items.Add(new ListItem(Resources.labels.inactive, "I"));
                    ddlStatus.Items.Add(new ListItem(Resources.labels.expired, "Y"));
                    break;
                case IPC.ACTIONPAGE.EDIT:
                    txtPromotionCode.Enabled = false;
                    txtExpireDate.Enabled = true;
                    ddlStatus.Enabled = true;
                    btnClear.Enabled = false;
                    btsave.Enabled = true;
                    ddlStatus.Items.Add(new ListItem(Resources.labels.active, "A"));
                    ddlStatus.Items.Add(new ListItem(Resources.labels.inactive, "I"));
                    break;
                case IPC.ACTIONPAGE.ADD:
                    ddlStatus.Items.Add(new ListItem(Resources.labels.active, "A"));
                    ddlStatus.Items.Add(new ListItem(Resources.labels.inactive, "I"));
                    break;
            }
            #endregion
            switch (ACTION)
            {
                case IPC.ACTIONPAGE.ADD:
                    ddlStatus.Enabled = false;
                    break;
                default:
                    btnClear.Enabled = false;
                    string ID = GetParamsPage(IPC.ID)[0].Trim();
                    hdID.Value = ID;
                    DataSet ds = new SmartPortal.SEMS.PROMOTION().PROMOTIONCODE_GETID(ID, ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE == "0")
                    {
                        DataTable dataTable = new DataTable();
                        dataTable = ds.Tables[0];
                        if (dataTable.Rows.Count > 0)
                        {
                            txtPromotionCode.Text = dataTable.Rows[0]["PROMOTIONCODE"].ToString();
                            txtExpireDate.Text =  Utility.FormatDatetime(dataTable.Rows[0]["EXPIREDDATE"].ToString(), "dd/MM/yyyy");
                            ddlStatus.SelectedValue = dataTable.Rows[0]["STATUS"].ToString();

                            PROMOTIONCODE = dataTable.Rows[0]["PROMOTIONCODE"].ToString();
                            STATUS = dataTable.Rows[0]["STATUS"].ToString();
                            EXPIREDDATE = Utility.FormatDatetime(dataTable.Rows[0]["EXPIREDDATE"].ToString(), "dd/MM/yyyy");
                        }
                        else
                        {
                            throw new IPCException(IPCERRORDESC);
                        }
                    }
                    else
                    {
                        throw new IPCException(IPCERRORCODE);
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
    protected void btsave_Click(object sender, EventArgs e)
    {
        try
        {
            string id = SmartPortal.Common.Utilities.Utility.KillSqlInjection(hdID.Value.Trim());
            string promotionCode = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtPromotionCode.Text.Trim());
            string expireDate = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtExpireDate.Text.Trim());
            string status = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlStatus.SelectedValue.Trim());
            DateTime dt = DateTime.ParseExact(expireDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            switch (ACTION)
            {
                case IPC.ACTIONPAGE.ADD:
                    try
                    {
                        if (!CheckPermitPageAction(IPC.ACTIONPAGE.ADD)) return;
                        int result = (dt - DateTime.Now.Date).Days;
                        if (result < 0)
                        {
                            lblError.Text = Resources.labels.expiredategreatercurrentdate;
                            return;
                        }

                        new SmartPortal.SEMS.PROMOTION().PROMOTIONCODE_INSERT(promotionCode.ToUpper(), status, Session["userName"].ToString(), expireDate, ref IPCERRORCODE, ref IPCERRORDESC);
                        if (IPCERRORCODE == "0")
                        {
                            lblError.Text = Resources.labels.thempromotionthanhcong;
                            pnAdd.Enabled = false;
                            btsave.Enabled = false;
                        }
                        else
                        {
                            lblError.Text = IPCERRORDESC;
                        }
                        SmartPortal.Common.Log.WriteLogDatabase(promotionCode.ToUpper(), "INSERT", Request.Url.ToString(), Session["userName"].ToString(),
                            Request.UserHostAddress, "EBA_PromotionCode", " PROMOTIONCODE", "", promotionCode.ToUpper(), "");
                        SmartPortal.Common.Log.WriteLogDatabase(promotionCode.ToUpper(), "INSERT", Request.Url.ToString(), Session["userName"].ToString(),
                            Request.UserHostAddress, "EBA_PromotionCode", " STATUS", "", status, "");
                        SmartPortal.Common.Log.WriteLogDatabase(promotionCode.ToUpper(), "INSERT", Request.Url.ToString(), Session["userName"].ToString(),
                            Request.UserHostAddress, "EBA_PromotionCode", " EXPIREDDATE", "", expireDate, "");
                    }
                    catch (Exception ex)
                    {
                        SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
                        SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
                    }
                    break;
                case IPC.ACTIONPAGE.EDIT:
                    try
                    {
                        if (!CheckPermitPageAction(IPC.ACTIONPAGE.EDIT)) return;
                        DateTime dt2 = DateTime.ParseExact(EXPIREDDATE, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        int result = (dt - dt2).Days;
                        if (result < 0)
                        {
                            lblError.Text = Resources.labels.expiredategreaterdateold + EXPIREDDATE;
                            return;
                        }
                        new SmartPortal.SEMS.PROMOTION().PROMOTIONCODE_UPDATE(id, promotionCode.ToUpper(), status, expireDate, ref IPCERRORCODE, ref IPCERRORDESC);
                        if (IPCERRORCODE == "0")
                        {
                            lblError.Text = Resources.labels.editpromotionsuccessfull;
                            pnAdd.Enabled = false;
                            btsave.Enabled = false;
                        }
                        else
                        {
                            lblError.Text = IPCERRORDESC;
                        }
                        SmartPortal.Common.Log.WriteLogDatabase(promotionCode.ToUpper(), "UPDATE", Request.Url.ToString(), Session["userName"].ToString(),
                            Request.UserHostAddress, "EBA_PromotionCode", " PROMOTIONCODE", PROMOTIONCODE, promotionCode.ToUpper(), "");
                        SmartPortal.Common.Log.WriteLogDatabase(promotionCode.ToUpper(), "UPDATE", Request.Url.ToString(), Session["userName"].ToString(),
                            Request.UserHostAddress, "EBA_PromotionCode", " STATUS", STATUS, status, "");
                        SmartPortal.Common.Log.WriteLogDatabase(promotionCode.ToUpper(), "UPDATE", Request.Url.ToString(), Session["userName"].ToString(),
                            Request.UserHostAddress, "EBA_PromotionCode", " EXPIREDDATE", EXPIREDDATE, expireDate, "");
                    }
                    catch (Exception ex)
                    {
                        SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
                        SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
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
    protected void btback_Click(object sender, EventArgs e)
    {
        RedirectBackToMainPage();
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        lblError.Text = string.Empty;
        txtPromotionCode.Text = string.Empty;
        txtExpireDate.Text = string.Empty;
        ddlStatus.SelectedIndex = 0;
        pnAdd.Enabled = true;
        btsave.Enabled = true;
    }
}
