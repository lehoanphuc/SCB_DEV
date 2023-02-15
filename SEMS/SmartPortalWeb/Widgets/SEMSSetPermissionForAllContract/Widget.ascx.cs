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

using SmartPortal.Common;
using SmartPortal.Common.Utilities;
using SmartPortal.ExceptionCollection;


public partial class Widgets_SEMSSetPermissionForAllContract_Widget : WidgetBase
{
    public static bool isAscend = false;
    private const string ASCENDING = " ASC";
    private const string DESCENDING = " DESC";
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    string detailPageId = "1041";
    string addPageId = "1030";
    string editPageId = "1031";
    string deletePageId = "1032";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            

            if (!IsPostBack)
            {
                //load serrviceType


            }
            ddlProduct.DataSource = new SmartPortal.SEMS.Contract().getAllProduct(ref IPCERRORCODE, ref IPCERRORDESC);
            ddlProduct.DataTextField = "ProductName";
            ddlProduct.DataValueField = "ProductID";
            ddlProduct.DataBind();
            ddlTransactionSetData();
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

    protected void ddlService_SelectedIndexChanged(object sender, EventArgs e)
    {

        ddlTransactionSetData();
    }
    public void ddlTransactionSetData()
    {
        try
        {
            string service = ddlService.SelectedValue.ToString();
            ddlTransaction.DataSource = new SmartPortal.SEMS.Contract().getAllTransactionByService(service, ref IPCERRORCODE, ref IPCERRORDESC);
            ddlTransaction.DataTextField = "PageName";
            ddlTransaction.DataValueField = "TranCode";
            ddlTransaction.DataBind();
            lblWarning.Text = "";
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
    protected void btnOK_Click(object sender, EventArgs e)
    {
        try
        {
            string productId = ddlProduct.SelectedValue.ToString().Trim();
            string serviceId = ddlService.SelectedValue.ToString().Trim();
            string trancode = ddlTransaction.SelectedValue.ToString().Trim();
            new SmartPortal.SEMS.Contract().updateNewServiceAllContract(productId, serviceId, trancode, ref IPCERRORCODE, ref IPCERRORDESC);
            lblWarning.Text = Resources.labels.applysuccessfully;
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
    protected void ddlProduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblWarning.Text = "";
    }
    protected void ddlTransaction_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblWarning.Text = "";
    }
}
