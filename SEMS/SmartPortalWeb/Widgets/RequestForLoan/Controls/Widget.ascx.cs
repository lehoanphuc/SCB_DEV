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

public partial class Widgets_RequestForLoan_Controls_Widget : WidgetBase
{
    private string ACTION = string.Empty;
    private string IPCERRORCODE = string.Empty;
    private string IPCERRORDESC = string.Empty;
    public string _TITLE
    {
        get { return lblTitleimgDoc.Text; }
        set { lblTitleimgDoc.Text = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = string.Empty;
            ACTION = GetActionPage();
            if (!IsPostBack)
            {
                BindData();
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
                    break;
                default:
                    string rqid = GetParamsPage("rqid")[0].Trim();
                    string type = GetParamsPage("type")[0].Trim();
                    DataTable dt = new SmartPortal.SEMS.RequestForLoan().LoadFileForDocument(rqid, type);
                    if (dt.Rows.Count >0 )
                    {
                        var ACB =  dt.Rows[0].ItemArray[0].ToString();
                        imgdoc.ImageUrl = "data:image/png;base64," + ACB;
                        switch (type)
                        {
                            case "WAR":
                                lblTitle.Text = "Ward Recommendation Image";
                                break;
                            case "HOU":
                                lblTitle.Text = "Household registration Image";
                                break;
                            case "LIV":
                                lblTitle.Text = "Live Selfie Image";
                                break;
                            case "PAY":
                                lblTitle.Text = "Pay Slip And Salary Image";
                                break;
                            case "SAL":
                                lblTitle.Text = "Salary Confirmation";
                                break;
                            default:
                                lblTitle.Text = "Image Document";

                                break;

                        }
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


}
