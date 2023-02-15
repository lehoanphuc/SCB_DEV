using System;
using System.Data;
using System.Web;
using SmartPortal.Common.Utilities;
using SmartPortal.Constant;
public partial class Widgets_SEMSDocumentManagementReason_Widget : WidgetBase
{
    public static bool isAscend = false;
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    static string documentId;
    static string UserId;
    static string evenCode;
    static string ContractType;
    SmartPortal.SEMS.Common _service = new SmartPortal.SEMS.Common();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            documentId = GetParamsPage(IPC.ID)[0].Trim();
            UserId = HttpContext.Current.Session["userID"].ToString();
            loadDocumentMannageData();
            CheckEvenandType();
            lblError.Text = string.Empty;
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }

    private void setDefaultColor()
    {
        txtReason.SetDefault();
    }



    void CheckEvenandType()
    {
        if (evenCode == "P")
        {
            txtReason.setEvent("I");
            txtReason.setReasonType(ContractType);
        }
        if (evenCode == "G")
        {
            txtReason.setEvent("D");
            txtReason.setReasonType(ContractType);
        }
    }

    void loadDocumentMannageData()
    {
        try
        {
            DataSet ds = new DataSet();
            object[] searchObject = new object[] { Utility.KillSqlInjection(documentId) };
            ds = _service.common("SEMS_BO_DOCUMENTVIEW", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);

            if (ds.Tables[0].Rows.Count > 0)
            {
                DataTable tb = ds.Tables[0];
                ContractType = tb.Rows[0]["ContractType"].ToString();
                evenCode = tb.Rows[0]["STATUS"].ToString();
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }
    }


    protected void btnComplete_Click(object sender, EventArgs e)
    {
        if (!CheckPermitPageAction(IPC.ACTIONPAGE.APPROVE)) return;
        try
        {
            setDefaultColor();
            #region validatiton
            if (txtReason.Text == string.Empty)
            {
                lblError.Text = Resources.labels.ReasonName + " is not null";
                txtReason.SetError();
                return;
            }
            #endregion

            DataSet ds = new DataSet();
            object[] _object = new object[] { Utility.KillSqlInjection(documentId), Utility.KillSqlInjection(UserId), int.Parse(txtReason.getReasonID()), Utility.KillSqlInjection(txtDescription.Text)};
            ds = _service.common("SEMS_BO_DOC_REJECT", _object, ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                lblError.Text = Resources.labels.RejectSuccessfully;
                btnComplete.Enabled = false;
                panel.Enabled = false;
                txtReason.Enabled = false;
            }
            else
            {
                lblError.Text = IPCERRORDESC;
            }
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        RedirectBackToMainPage();
    }
}