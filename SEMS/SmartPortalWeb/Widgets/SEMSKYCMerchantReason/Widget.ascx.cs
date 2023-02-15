using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using SmartPortal.Constant;
public partial class Widgets_SEMSKYCMerchantReason_Widget : WidgetBase
{
    public static bool isAscend = false;
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    string FORMAT_DATE = "dd/MM/yyyy";
    string request_ID = "";
    SmartPortal.SEMS.Common _service = new SmartPortal.SEMS.Common();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            request_ID = GetParamsPage(IPC.ID)[0].Trim();
            lblError.Text = string.Empty;
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
            DataSet ds = new DataSet();
            object[] _object = new object[] { request_ID };
            ds = _service.common("SEMS_BO_MERCHANT_KYC", _object, ref IPCERRORCODE, ref IPCERRORDESC);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ViewState["UserID"] = ds.Tables[0].Rows[0]["USERID"].ToString();
                }
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, "");
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], "");
        }
    }

    protected void btnComplete_Click(object sender, EventArgs e)
    {
        if (!CheckPermitPageAction(IPC.ACTIONPAGE.APPROVE)) return;
        try
        {
            setDefaultColor();

            #region validatiton
            if (txtReason.Text.Trim() == string.Empty)
            {
                lblError.Text = Resources.labels.ReasonName + Resources.labels.IsNotNull;
                txtReason.SetError();
                return;
            }
            #endregion

            DataSet ds = new DataSet();
            //object[] _object = new object[] { int.Parse(request_ID), UserID, int.Parse(txtReason.getReasonID()), txtDescription.Text};
            Dictionary<object, object> _para = new Dictionary<object, object>();

            _para.Add("REQUEST_ID", int.Parse(request_ID));
            _para.Add("USERID", ViewState["UserID"]);
            _para.Add("USER_REJECT", HttpContext.Current.Session["userID"].ToString());
            _para.Add("REASONID", int.Parse(txtReason.getReasonID()));
            _para.Add("DESCRIPTION", txtDescription.Text);
            ds = _service.CallStore("SEMS_BO_REJECT_KYC", _para,"Reject KYC Merchant","N", ref IPCERRORCODE, ref IPCERRORDESC);
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