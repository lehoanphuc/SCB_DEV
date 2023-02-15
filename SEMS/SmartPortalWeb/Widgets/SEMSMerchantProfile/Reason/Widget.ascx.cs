using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;
using SmartPortal.Constant;
public partial class Widgets_SEMSKYCMerchantReason_Widget : WidgetBase
{
    public static bool isAscend = false;
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    string FORMAT_DATE = "dd/MM/yyyy";
    string merchant_ID = "";
    string UserID = "";
    SmartPortal.SEMS.Common _service = new SmartPortal.SEMS.Common();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            merchant_ID = GetParamsPage(IPC.ID)[0].Trim();
            UserID = HttpContext.Current.Session["userID"].ToString();
            lblError.Text = string.Empty;
            txtReason.setEvent("R");
            if (!IsPostBack)
            {
                BindData();
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }

    private void setDefaultControl()
    {
        //txtReasonName.Attributes.Add("placeholder", "Code - Name");
    }
    private void setDisableControl()
    {
        
    }

    private void setDefaultColor()
    {
        txtReason.SetDefault();
    }

    void BindData()
    {
        try
        {
            setDefaultControl();
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }

    void SetPara(Dictionary<object, object> para)
    {
        para.Add("USERID", merchant_ID);
        para.Add("USER_REJECT", HttpContext.Current.Session["userID"].ToString());
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
            //object[] _object = new object[] { merchant_ID, HttpContext.Current.Session["userID"].ToString()};
            Dictionary<object, object> _para = new Dictionary<object, object>();
            SetPara(_para);
            ds = _service.CallStore("SEMS_MER_REJECT", _para,"Reject Merchant Profile","N", ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                lblError.Text = Resources.labels.RejectSuccessfully;
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