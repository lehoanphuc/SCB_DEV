using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Windows.Forms;
using SmartPortal.Common.Utilities;
using SmartPortal.Constant;
public partial class Widgets_SEMSKYCMerchantReason_Widget : WidgetBase
{
    public static bool isAscend = false;
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    SmartPortal.SEMS.Common _service = new SmartPortal.SEMS.Common();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ViewState["REQUESTNO"] = GetParamsPage(IPC.ID)[0].Trim();
            lblError.Text = string.Empty;
            txtReason.setEvent("K");
            txtReason.setReasonType("IND");
            getCustid();
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
    void  getCustid()
    {
        DataSet ds = new DataSet();
        object[] searchObject = new object[] { Utility.KillSqlInjection( ViewState["REQUESTNO"].ToString()) };
        ds = _service.common("SEMS_GET_INFOKYC_CON", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);
        if (ds != null)
        {
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count < 1) return;
                DataTable dataTable = ds.Tables[0];
                ViewState["CustID"] = dataTable.Rows[0]["CustID"].ToString();

            }
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
            Dictionary<object, object> _para = new Dictionary<object, object>();
            _para.Add("REQUESTID", int.Parse(ViewState["REQUESTNO"].ToString()));
            _para.Add("CUSTID", Utility.KillSqlInjection(ViewState["CustID"].ToString()));
            _para.Add("USERAPPROVED", HttpContext.Current.Session["userID"].ToString());
            _para.Add("REASONID", int.Parse(Utility.KillSqlInjection(txtReason.getReasonID())));
            _para.Add("DESCRIPTION", Utility.KillSqlInjection( txtDescription.Text));

            ds = _service.CallStore("SEMS_BO_REKYC_CON", _para, "Reject Consumer KYC Request", "N", ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                lblError.Text = Resources.labels.RejectSuccessfully;
                panel.Enabled = false;
                btnApprove.Enabled = false;
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