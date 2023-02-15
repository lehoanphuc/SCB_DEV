using System;
using System.Data;
using System.Web.UI.WebControls;
using SmartPortal.Common.Utilities;
using SmartPortal.Constant;

public partial class Widgets_SEMSREGIONFEE_Widget : WidgetBase
{
    public static bool isAscend = false;
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    SmartPortal.SEMS.Common _service = new SmartPortal.SEMS.Common();
    public static int flag = 1;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = string.Empty;
            lbCurentWorkingdate.Text = Convert.ToDateTime(Session["Workingdate"].ToString()).ToString("dd/MM/yyyy");
            if (!CheckPermitPageAction(IPC.ACTIONPAGE.EDIT))
            {
                btUpdate.Enabled = false;
            }
            else
            {
                btUpdate.Enabled = true;
            }    
        }
        catch (Exception ex)
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
        }
    }



    protected void btUpdate_Click(object sender, EventArgs e)
    {
        if (CheckPermitPageAction(IPC.ACTIONPAGE.EDIT))
        {
            try
            {

                DataSet ds = new DataSet();
                object[] searchObject = new object[] { };
                ds = _service.common("SEMSWORKINGDATE", searchObject, ref IPCERRORCODE, ref IPCERRORDESC);
                if (IPCERRORCODE == "0" || IPCERRORCODE.Equals(""))
                {
                    Session["Workingdate"] = ds.Tables[0].Rows[0]["Workingdate"].ToString();
                    lbCurentWorkingdate.Text = Convert.ToDateTime(Session["Workingdate"].ToString()).ToString("dd/MM/yyyy");
                    lblError.Text = Resources.labels.Successfully;
                }
                else
                    lblError.Text = IPCERRORDESC;
            }
            catch (Exception ex)
            {
                SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["sysec"], this.GetType().BaseType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString(), Request.Url.Query);
                SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["sysec"], Request.Url.Query);
            }
        }
        else
        {
            lblError.Text = "This action cannot be performed by the user!";
        }    
        
    }
}
