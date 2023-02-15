using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using SmartPortal.Common.Utilities;
using SmartPortal.Constant;
public partial class Widgets_SEMSKYCDefinition_ADD_Widget : WidgetBase
{
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    SmartPortal.SEMS.Common _service = new SmartPortal.SEMS.Common();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            txtReason.setReasonAction("A");
            txtReason.setEvent("RR");
            txtReason.setReasonType("BO");

            if (!IsPostBack)
            {
                btConfirm.Visible = CheckPermitPageAction(IPC.ACTIONPAGE.APPROVE);
                getCustid();
            }
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }
    protected void btConfirm_OnClick(object sender, EventArgs e)
    {
        try
        {
            setDefaultColor();
            #region validatiton
            if (txtReason.Text == string.Empty)
            {
                lblError.Text = Resources.labels.ReasonName + " cannot be null";
                txtReason.SetError();
                return;
            }
            #endregion
            DataSet ds = new DataSet();
            Dictionary<object, object> _para = new Dictionary<object, object>();
            _para.Add("TXREF", GetParamsPage(IPC.ID)[0].Trim());
            _para.Add("USERMAKE", HttpContext.Current.Session["userID"].ToString());
            _para.Add("USERCHECK", HttpContext.Current.Session["userID"].ToString());
            _para.Add("REASONID", int.Parse(Utility.KillSqlInjection(txtReason.getReasonID())));

            ds = _service.CallStore("SEMS_BO_RRT_TRAN", _para, "Request Reversal Transaction (RRT)", "N", ref IPCERRORCODE, ref IPCERRORDESC);
            if (IPCERRORCODE == "0")
            {              
                DataTable dataTable = ds.Tables[0];
                ViewState["RequestID"] = dataTable.Rows[0]["RRCode"].ToString();
                try
                {
                    SmartPortal.Common.Log.WriteLogDatabase(dataTable.Rows[0]["RRCode"].ToString(), "", Request.Url.ToString(), Session["userName"].ToString(),
                            Request.UserHostAddress, "IPCLOGTRANS", "Send request Reversal transaction", "", "", "A", Session["UserID"].ToString());
                }
                catch (Exception ex)
                {

                }
                NextPage();

               
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
    void NextPage()
    {
        string commandName = IPC.ACTIONPAGE.EXPORT;
        string commandArg = Utility.KillSqlInjection(ViewState["RequestID"].ToString());
        if (CheckPermitPageAction(commandName))
        {
            switch (commandName)
            {
                case IPC.ACTIONPAGE.EXPORT:
                    RedirectToActionPage(IPC.ACTIONPAGE.EXPORT, "&" + SmartPortal.Constant.IPC.ID + "=" + commandArg);
                    break;
            }
        }
    }
    private void setDefaultColor()
    {
        txtReason.SetDefault();
    }
    void getCustid()
    {
        string tranID = GetParamsPage(IPC.ID)[0].Trim();
        DataSet ds = new DataSet();
        ds = new SmartPortal.SEMS.Transactions().GetTranByTranID(tranID, ref IPCERRORCODE, ref IPCERRORDESC);
        if (IPCERRORCODE == "0")
        {
            DataTable dt = new DataTable();
            dt = ds.Tables[0];
            if (dt.Rows.Count != 0)
            {
                ViewState["Usermake"] = ds.Tables[3].Rows[0][0].ToString();

            }
        }
    }
    protected void btback_OnClick(object sender, EventArgs e)
    {
        RedirectBackToMainPage();
    }

}
