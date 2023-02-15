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

using SmartPortal.Common.Utilities;

public partial class Widgets_IBDept_Delete_Widget : WidgetBase
{
    string IPCERRORCODE = "";
    string IPCERRORDESC = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
        }

        catch (Exception ex)
        {
        }
    }

    protected void btsaveandcont_Click(object sender, EventArgs e)
    {
        DataSet DeptDeleteTable = new DataSet();
        //string SSBranchID = "";
        try
        {
        //    if (Session["_BranchID"] != null)
        //    {
        //        SSBranchID = Session["_BranchID"].ToString();
        //        string[] brchs = SSBranchID.Split('#');
        //        foreach (string brch in brchs)
        //        {
        //            BranchTable = new SmartPortal.SEMS.Branch().DeleteBranch(brch, ref IPCERRORCODE, ref IPCERRORDESC);
        //            if (IPCERRORCODE == "0")
        //            {

        //                //Response.Redirect("~/Default.aspx?po=4&p=141");
        //            }
        //        }
        //        Session["_BranchID"] = null;
        //        //Response.Redirect(SmartPortal.Common.Encrypt.DecryptData(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["returnUrl"].ToString()));

        //    }
        //    else
            //{
            DeptDeleteTable = new SmartPortal.IB.Dept().DeleteDeptByDepID(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["did"].ToString(), ref IPCERRORCODE, ref IPCERRORDESC);

            //}
        }
        catch (Exception ex)
        { }

        if (IPCERRORCODE == "0")
        {
            Response.Redirect(SmartPortal.Common.Encrypt.EncryptURL("~/default.aspx?po=3&p=190"));
            //Response.Redirect(SmartPortal.Common.Encrypt.DecryptData(SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["returnUrl"].ToString()));
        }
        else
        {
            SmartPortal.Common.Log.RaiseError(System.Configuration.ConfigurationManager.AppSettings["ipcec"], "Widgets_IBDept_Delete_Widget", "BindData", IPCERRORDESC, Request.Url.Query);
            SmartPortal.Common.Log.GoToErrorPage(System.Configuration.ConfigurationManager.AppSettings["ipcec"], Request.Url.Query);
        }
    }
}


