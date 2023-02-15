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
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = string.Empty;
            ACTION = GetActionPage();
            if (!IsPostBack)
            {
                LoadDll();
                BindData();
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    void LoadDll()
    {
        //load ten san pham
        //load các giao dịch
        ddlTrans.DataSource = new SmartPortal.IB.Schedule().LoadTransferType(Utility.KillSqlInjection(SmartPortal.Constant.IPC.ISOTC), Utility.KillSqlInjection(SmartPortal.Constant.IPC.bittrue), ref IPCERRORCODE, ref IPCERRORDESC);
        if (IPCERRORCODE != "0")
        {
            throw new IPCException(IPCERRORDESC);
        }
        ddlTrans.DataTextField = "PAGENAME";
        ddlTrans.DataValueField = "TRANCODE";
        ddlTrans.DataBind();
        //load tien te 
        ddlCCYID.DataSource = new SmartPortal.SEMS.Product().LoaddAllCCYID(ref IPCERRORCODE, ref IPCERRORDESC);
        ddlCCYID.DataTextField = "CCYID";
        ddlCCYID.DataValueField = "CCYID";
        ddlCCYID.DataBind();

        DataSet ds = new SmartPortal.SEMS.Fee().SearchFee(string.Empty, string.Empty, string.Empty, Utility.KillSqlInjection(ddlCCYID.SelectedValue), 0, 0, ref IPCERRORCODE, ref IPCERRORDESC);
        if (IPCERRORCODE == "0")
        {
            ddlFee.DataSource = ds;
            ddlFee.DataTextField = "FEENAME";
            ddlFee.DataValueField = "FEEID";
            ddlFee.DataBind();
        }
        else
        {
            throw new IPCException(IPCERRORDESC);
        }

        DataSet ListContractLevel = new SmartPortal.SEMS.Product().LoadContractLevelActive(ref IPCERRORCODE, ref IPCERRORDESC);
        if (IPCERRORCODE == "0")
        {
            ddlContractLevel.DataSource = ListContractLevel;
            ddlContractLevel.DataTextField = "ContractLevelName";
            ddlContractLevel.DataValueField = "ContractLevelID";
            ddlContractLevel.DataBind();
        }
        else
        {
            lblError.Text = IPCERRORDESC;
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
                    string ID = GetParamsPage(IPC.ID)[0].Trim();
                    string[] key = ID.Split('|');
                    DataSet ds = new SmartPortal.SEMS.OtcFee().SearchOtcFee(key[0], key[1], key[2],key[3],key[4],0,0, ref IPCERRORCODE, ref IPCERRORDESC);
                    if (IPCERRORCODE == "0")
                    {
                        DataTable productLMTable = new DataTable();
                        productLMTable = ds.Tables[0];
                        if (productLMTable.Rows.Count > 0)
                        {
                            ddlTrans.SelectedValue = productLMTable.Rows[0]["TranCode"].ToString();
                            txtDesc.Text = productLMTable.Rows[0]["DESCRIPTION"].ToString();
                            ddlCCYID.SelectedValue = productLMTable.Rows[0]["CCYID"].ToString();
                            ddlCCYID_OnSelectedIndexChanged(null, EventArgs.Empty);
                            ddlFee.SelectedValue = productLMTable.Rows[0]["FEEID"].ToString();
                            ddlContractLevel.SelectedValue = productLMTable.Rows[0]["ContractLevelId"].ToString();
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

            #region Enable/Disable theo Action
            switch (ACTION)
            {
                case IPC.ACTIONPAGE.DETAILS:
                    ddlFee.Enabled = false;
                    ddlTrans.Enabled = false;
                    ddlCCYID.Enabled = false;
                    ddlContractLevel.Enabled = false;
                    txtDesc.Enabled = false;
                    btnClear.Visible = false;
                    btsave.Visible = false;
                    break;
                case IPC.ACTIONPAGE.EDIT:
                    ddlFee.Enabled = false;
                    ddlTrans.Enabled = false;
                    ddlCCYID.Enabled = false;
                    ddlContractLevel.Enabled = false;
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
            string trancode = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlTrans.SelectedValue.Trim());
            string ccyid = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlCCYID.SelectedValue.Trim());
            string contractlevel = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlContractLevel.SelectedValue.Trim());
            string desc = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtDesc.Text.Trim());
            string fee = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlFee.SelectedValue.Trim());
            switch (ACTION)
            {
                case IPC.ACTIONPAGE.ADD:
                    try
                    {
                        if (!CheckPermitPageAction(IPC.ACTIONPAGE.ADD)) return;
                        if (ddlTrans.SelectedValue == "")
                        {
                            lblError.Text = "Transaction can not be blank";
                            break;
                        }
                        new SmartPortal.SEMS.OtcFee().InsertOtcFee(trancode, desc, ccyid, "D", Session["userName"].ToString(), contractlevel,fee, ref IPCERRORCODE, ref IPCERRORDESC);
                        if (IPCERRORCODE == "0")
                        {
                            lblError.Text = Resources.labels.themphiotcthanhcong;
                            pnAdd.Enabled = false;
                            btsave.Enabled = false;
                        }
                        else
                        {
                            lblError.Text = Resources.labels.otcfeeexists;
                        }
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
                        new SmartPortal.SEMS.OtcFee().UpdateOtcFee(trancode, desc, ccyid, "D", Session["userName"].ToString(), contractlevel, fee, ref IPCERRORCODE, ref IPCERRORDESC);
                        if (IPCERRORCODE == "0")
                        {
                            lblError.Text = Resources.labels.Editotcsuccessfull;
                            pnAdd.Enabled = false;
                            btsave.Enabled = false;
                        }
                        else
                        {
                            lblError.Text = Resources.labels.otcfeeexists;
                        }
                    }
                    catch (Exception ex)
                    {
                        SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
                        SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
                    }
                    break;
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
    protected void btback_Click(object sender, EventArgs e)
    {
        RedirectBackToMainPage();
    }
    protected void ddlCCYID_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = new SmartPortal.SEMS.Fee().SearchFee(string.Empty, string.Empty, string.Empty, Utility.KillSqlInjection(ddlCCYID.SelectedValue), 0, 0, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                ddlFee.DataSource = ds;
                ddlFee.DataTextField = "FEENAME";
                ddlFee.DataValueField = "FEEID";
                ddlFee.DataBind();
            }
            else
            {
                throw new IPCException(IPCERRORDESC);
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        lblError.Text = string.Empty;
        pnAdd.Enabled = true;
        btsave.Enabled = true;
        txtDesc.Text = string.Empty;
        ddlCCYID.SelectedIndex = 0;
        ddlContractLevel.SelectedIndex = 0;
        ddlCCYID_OnSelectedIndexChanged(sender, EventArgs.Empty);
        ddlFee.SelectedIndex = 0;
		ddlTrans.SelectedIndex = 0;
    }

}
