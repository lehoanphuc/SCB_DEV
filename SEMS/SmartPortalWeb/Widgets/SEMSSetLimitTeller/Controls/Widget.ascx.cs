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
using SmartPortal.BLL;
using SmartPortal.Common.Utilities;
using SmartPortal.Constant;
using SmartPortal.ExceptionCollection;
using SmartPortal.Model;

public partial class Widgets_SEMSSetLimitTeller_Controls_Widget : WidgetBase
{
    string ACTION = "";
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";

    public string _TITLE
    {
        get { return lblTitleProduct.Text; }
        set { lblTitleProduct.Text = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            txtlimit.Attributes.Add("onkeypress", "executeComma('" + txtlimit.ClientID + "')");
            txtlimit.Attributes.Add("onkeyup", "executeComma('" + txtlimit.ClientID + "')");
            ACTION = GetActionPage();
            if (!IsPostBack)
            {
                //load ten teller
                ddlTeller.DataSource = new SmartPortal.SEMS.User().GetUserByUserType(SmartPortal.Constant.IPC.TELLER, "", "", "", ref IPCERRORCODE, ref IPCERRORDESC);
                ddlTeller.DataTextField = "USERID";
                ddlTeller.DataValueField = "USERID";
                ddlTeller.DataBind();
                //load các giao dịch
                ddlTrans.DataSource = new SmartPortal.SEMS.Product().GetTranNameByTrancode(ref IPCERRORCODE, ref IPCERRORDESC);
                ddlTrans.DataTextField = "PAGENAME";
                ddlTrans.DataValueField = "TRANCODE";
                ddlTrans.DataBind();
                //load tiền tệ
                ddlCCYID.DataSource = new SmartPortal.SEMS.Product().LoaddAllCCYID(ref IPCERRORCODE, ref IPCERRORDESC);
                ddlCCYID.DataTextField = "CCYID";
                ddlCCYID.DataValueField = "CCYID";
                ddlCCYID.DataBind();

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
            switch (ACTION)
            {
                case IPC.ACTIONPAGE.ADD:
                    break;
                default:
                    btnClear.Enabled = false;
                    #region Lấy thông tin san pham
                    string ID = GetParamsPage(IPC.ID)[0].Trim();
                    string[] key = ID.Split('|');
                    DataSet ds = new SmartPortal.SEMS.Transactions().GetAllLimitTellerByUID(key[0], key[1], key[2], 0, 0, ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE == "0")
                    {
                        DataTable TellerLMTable = ds.Tables[0];
                        if (TellerLMTable.Rows.Count != 0)
                        {
                            ddlTeller.SelectedValue = TellerLMTable.Rows[0]["USERID"].ToString();
                            ddlTrans.SelectedValue = TellerLMTable.Rows[0]["IPCTRANCODE"].ToString();
                            txtlimit.Text = SmartPortal.Common.Utilities.Utility.FormatMoney(TellerLMTable.Rows[0]["LIMITAPPROVE"].ToString(), TellerLMTable.Rows[0]["CCYID"].ToString());
                            ddlCCYID.SelectedValue = TellerLMTable.Rows[0]["CCYID"].ToString();
                            txtDesc.Text = TellerLMTable.Rows[0]["DECRIPTION"].ToString();
                        }
                    }
                    else
                    {
                        throw new IPCException(IPCERRORDESC);
                    }
                    #endregion
                    break;
            }
            #region Enable/Disable theo Action
            switch (ACTION)
            {
                case IPC.ACTIONPAGE.DETAILS:
                    pnAdd.Enabled = false;
                    btsave.Enabled = false;
                    break;
                case IPC.ACTIONPAGE.EDIT:
                    ddlTeller.Enabled = false;
                    ddlTrans.Enabled = false;
                    ddlCCYID.Enabled = false;
                    break;
            }
            #endregion 

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
            string userid = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlTeller.SelectedValue.Trim());
            string trancode = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlTrans.SelectedValue.Trim());
            string ccyid = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlCCYID.SelectedValue.Trim());
            string limit = SmartPortal.Common.Utilities.Utility.KillSqlInjection(SmartPortal.Common.Utilities.Utility.FormatMoneyInput(txtlimit.Text.Trim(), ddlCCYID.SelectedValue.Trim()));
            string desc = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtDesc.Text.Trim());
            switch (ACTION)
            {
                case IPC.ACTIONPAGE.ADD:
                    if (!CheckPermitPageAction(IPC.ACTIONPAGE.ADD)) return;
                    new SmartPortal.SEMS.Transactions().InsertUserApproveLimit(userid, trancode, ccyid, limit, desc, ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE == "0")
                    {
                        lblError.Text = Resources.labels.themhanmucduyetgiaodichthanhcong;
                        pnAdd.Enabled = false;
                        btsave.Enabled = false;
                    }
                    else
                    {
                        ErrorCodeModel EM = new ErrorCodeModel();
                        EM = new ErrorBLL().Load(Utility.IsInt(IPC.ERRORCODE.INVALIDAPPROVELIMIT), System.Globalization.CultureInfo.CurrentCulture.ToString());
                        lblError.Text = EM.ErrorDesc;
                    }
                    break;
                case IPC.ACTIONPAGE.EDIT:
                    if (!CheckPermitPageAction(IPC.ACTIONPAGE.EDIT)) return;
                    new SmartPortal.SEMS.Transactions().UpdateUserApproveLimit(userid, trancode, ccyid, limit, desc, ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE == "0")
                    {
                        lblError.Text = Resources.labels.suahanmucduyetgiaodichthanhcong;
                        pnAdd.Enabled = false;
                        btsave.Enabled = false;
                    }
                    else
                    {
                        ErrorCodeModel EM = new ErrorCodeModel();
                        EM = new ErrorBLL().Load(Utility.IsInt(IPC.ERRORCODE.INVALIDAPPROVELIMIT), System.Globalization.CultureInfo.CurrentCulture.ToString());
                        lblError.Text = EM.ErrorDesc;
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
        pnAdd.Enabled = true;
        btsave.Enabled = true;
        ddlTeller.SelectedIndex = 0;
        ddlTrans.SelectedIndex = 0;
        txtlimit.Text = string.Empty;
        ddlCCYID.SelectedIndex = 0;
        txtDesc.Text = string.Empty;
    }
}
