using System;
using SmartPortal.ExceptionCollection;
using System.Data;
using SmartPortal.Constant;
using System.Web;
using System.Web.UI.WebControls;
using SmartPortal.Common.Utilities;
using System.Collections.Generic;

public partial class Widgets_SEMSReversalApprove_reject_Widget : WidgetBase
{
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    public static string documentId;
    public static string UserId;

    SmartPortal.SEMS.Common _service = new SmartPortal.SEMS.Common();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            UserId = HttpContext.Current.Session["userID"].ToString();
            lblError.Text = "";
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (!CheckPermitPageAction(IPC.ACTIONPAGE.REJECT)) return;

            Dictionary<object, object> infor = new Dictionary<object, object>();
            infor.Add("RRID", Utility.KillSqlInjection(GetParamsPage(IPC.ID)[0].Trim()));
            infor.Add("DES", Utility.KillSqlInjection(txtReasonReject.Text.Trim()));
            infor.Add("USERID", Utility.KillSqlInjection(HttpContext.Current.Session["userID"].ToString()));

            DataSet ds = new DataSet();
            ds = _service.CallStore("SEMS_REVERSAL_REJECT", infor, "Reject Reversal request", "N", ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {
                lblError.Text = Resources.labels.RejectSuccessfully;
                btnSubmit.Enabled = false;
                txtReasonReject.Enabled = false;
                try
                {
                    SmartPortal.Common.Log.WriteLogDatabase("RR" + GetParamsPage(IPC.ID)[0].Trim(), "", Request.Url.ToString(), Session["userName"].ToString(),
                            Request.UserHostAddress, "IPCLOGTRANS", "Reject request Reversal Transaction", "", "", "A", Session["UserID"].ToString());
                }
                catch (Exception ex)
                {

                }

            }
            else
            {
                lblError.Text = IPCERRORDESC;
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);

        }

    }
    protected void btncancel_click(object sender, EventArgs e)
    {
        RedirectBackToMainPage();
    }

}
