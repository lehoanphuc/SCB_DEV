using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartPortal.Model;
using SmartPortal.BLL;
using SmartPortal.Common.Utilities;

public partial class Widgets_Error_Widget : WidgetBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["ec"] != null)
            {
                ErrorCodeModel EM = new ErrorCodeModel();
                EM = new ErrorBLL().Load(Utility.IsInt(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["ec"]), System.Globalization.CultureInfo.CurrentCulture.ToString());

                lblErrorCode.Text = EM.ErrorCode.ToString();
                lblErrorDesc.Text = EM.ErrorDesc;
            }
        }
        catch (Exception ex)
        {
        }
    }
    protected void btnExit_Click(object sender, EventArgs e)
    {
        if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["returnurl"] != null)
        {
            Response.Redirect(SmartPortal.Common.Encrypt.DecryptData(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["returnurl"]));
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        Response.Redirect(System.Configuration.ConfigurationManager.AppSettings["homebank"]);
    }
}
