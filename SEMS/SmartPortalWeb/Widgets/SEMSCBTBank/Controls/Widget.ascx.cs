using SmartPortal.Common.Utilities;
using SmartPortal.Constant;
using SmartPortal.ExceptionCollection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Widgets_SEMSCBTBank_Controls_Widget : WidgetBase
{
    private string ACTION = string.Empty;
    private string IPCERRORCODE = string.Empty;
    private string IPCERRORDESC = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ACTION = GetActionPage();
            if (!IsPostBack)
            {
                loadCurrency();
                BindData();
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    private void loadCurrency()
    {
        try
        {
            ddlCurrency.Items.Insert(0, new ListItem("USD", "USD"));
            ddlCurrency.Items.Insert(1, new ListItem("THB", "THB"));
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        try
        {
            string currency = SmartPortal.Common.Utilities.Utility.KillSqlInjection(ddlCurrency.SelectedValue);
            string swiftcode = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtSwiftCode.Text.Trim());
            string bankName = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtBankName.Text.Trim());
            string nostro = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtNostro.Text.Trim());
            string address = SmartPortal.Common.Utilities.Utility.KillSqlInjection(txtAddress.Text.Trim());

            switch (ACTION)
            {
                case IPC.ACTIONPAGE.ADD:
                    try
                    {
                        if (!CheckPermitPageAction(IPC.ACTIONPAGE.ADD)) return;

                        Hashtable hasInput = new Hashtable();
                        Hashtable hasOutput = new Hashtable();

                        hasInput = new Hashtable(){
                            {"IPCTRANCODE", "SEMSCBTBANKINSERT"},
                            {"SERVICEID", "SEMS"},
                            {"SOURCEID", "SEMS"},
                            {"SWIFTCODE", swiftcode},
                            {"BANKNAME", bankName},
                            {"NOSTRO", nostro},
                            {"CURRENCY", currency},
                            {"ADDRESS", address},
                            {"USERCREATED", Session["userName"].ToString()}
                        };
                        hasOutput = new SmartPortal.SEMS.Transactions().CommonProcessTrans(hasInput, ref IPCERRORCODE, ref IPCERRORDESC);
                        if (IPCERRORCODE != "0")
                        {
                            lblError.Text = IPCERRORDESC;
                            return;
                        }
                        else
                        {
                            ClearInputs(Page.Controls);
                            lblError.Text = Resources.labels.themnganhangthanhcong;
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
                    break;
                case IPC.ACTIONPAGE.EDIT:
                    if (!CheckPermitPageAction(IPC.ACTIONPAGE.EDIT)) return;
                    try
                    {
                        string ID = GetParamsPage(IPC.ID)[0].Trim();
                        Hashtable hasInput = new Hashtable();
                        Hashtable hasOutput = new Hashtable();

                        hasInput = new Hashtable(){
                            {"IPCTRANCODE", "SEMSCBTBANKUPDATE"},
                            {"SERVICEID", "SEMS"},
                            {"SOURCEID", "SEMS"},
                            {SmartPortal.Constant.IPC.BANKID, ID},
                            {"SWIFTCODE", swiftcode},
                            {"BANKNAME", bankName},
                            {"NOSTRO", nostro},
                            {"CURRENCY", currency},
                            {"ADDRESS", address},
                            {"USERMODIFIED", Session["userName"].ToString()}
                        };
                        hasOutput = new SmartPortal.SEMS.Transactions().CommonProcessTrans(hasInput, ref IPCERRORCODE, ref IPCERRORDESC);
                        
                        if (IPCERRORCODE == "0")
                        {
                            lblError.Text = Resources.labels.suathanhcong;
                            pnAdd.Enabled = false;
                            btsave.Enabled = false;
                        }
                        else
                        {
                            lblError.Text = IPCERRORDESC;
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
                    break;

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
                    btsave.Text = Resources.labels.add;
                    break;
                default:
                    btnClear.Visible = false;
                    string ID = GetParamsPage(IPC.ID)[0].Trim();
                    DataSet dsCountry = new DataSet();

                    Hashtable hasInput = new Hashtable();
                    Hashtable hasOutput = new Hashtable();

                    hasInput = new Hashtable(){
                        {"IPCTRANCODE", "CBTBANKDETAILS"},
                        {"SERVICEID", "SEMS"},
                        {"SOURCEID", "SEMS"},
                        {SmartPortal.Constant.IPC.BANKID, ID}
                    };
                    hasOutput = new SmartPortal.SEMS.Transactions().CommonProcessTrans(hasInput, ref IPCERRORCODE, ref IPCERRORDESC);
                    dsCountry = (DataSet)hasOutput[SmartPortal.Constant.IPC.DATASET];

                    if (IPCERRORCODE == "0")
                    {
                        if (dsCountry != null)
                        {
                            DataTable dt = dsCountry.Tables[0];
                            if (dt.Rows.Count != 0)
                            {
                                ddlCurrency.SelectedValue = dt.Rows[0]["Currency"].ToString();
                                txtSwiftCode.Text = dt.Rows[0]["SwiftCode"].ToString();
                                txtBankName.Text = dt.Rows[0]["BankName"].ToString();
                                txtNostro.Text = dt.Rows[0]["Nostro"].ToString();
                                txtAddress.Text = dt.Rows[0]["Address"].ToString();
                            }
                        }
                        break;
                    }
                    else
                    {
                        lblError.Text = IPCERRORDESC;
                        return;
                    }
            }
            switch (ACTION)
            {
                case IPC.ACTIONPAGE.DETAILS:
                    pnAdd.Enabled = false;
                    btsave.Enabled = false;
                    btnClear.Enabled = false;
                    break;
                case IPC.ACTIONPAGE.EDIT:
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
    protected void Clear_Click(object sender, EventArgs e)
    {
        lblError.Text = String.Empty;
        ClearInputs(Page.Controls);
        pnAdd.Enabled = true;
        btsave.Enabled = true;
    }
    void ClearInputs(ControlCollection ctrls)
    {
        foreach (Control ctrl in ctrls)
        {
            if (ctrl is TextBox)
                ((TextBox)ctrl).Text = string.Empty;
            ClearInputs(ctrl.Controls);
        }
    }


}